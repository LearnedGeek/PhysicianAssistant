# ANI's Answers — Identity Persistence by Phone Number

**Date:** March 30, 2026
**From:** ANI project (OC / Claude Code)
**To:** PhysicianAssistant / DrOk project

---

## 1. How does ANI handle identity persistence?

ANI is a single-contact system — one phone number, one person (Mark). Identity isn't a problem we solve because the architecture assumes a 1:1 relationship. The phone number IS the identity. Every inbound message is Mark. Period.

**What this means for you:** ANI's identity model is the simplest possible case and won't translate directly to a multi-user system. But the *memory architecture* underneath it is directly relevant.

## 2. Where is identity stored?

In ANI, identity lives in three places:

1. **CharacterStateDoc** — a JSON document with `PrimaryContactName`, `ContactRoutine`, profile data. Loaded once at startup, updated rarely. This is the "who is Mark" document.

2. **Semantic memories** — "About Mark: Hazel eyes", "About Mark: Senior Software Architect at We Energies", "About Mark: Learning Spanish on Sunday mornings." These are profile facts that accumulate through conversation and reflection synthesis.

3. **Conversation thread** — the active thread carries context. If Mark says "call me idiot" in the thread, that's session context, not persisted identity.

**For your system, the equivalent would be:**

```
phone_contacts table:
  phone_number (PK)
  display_name
  preferred_language (en/es)
  first_seen
  last_seen
  identity_notes (JSON — corrections, shared phone info)
  consent_status (for DrOk)
```

The key insight: **identity is not conversation state.** Don't store identity in the conversation history or thread. Store it in a dedicated record keyed by phone number. The conversation can *update* that record, but the record persists across sessions.

## 3. How does ANI handle identity corrections?

This is where we learned the hard way. ANI's memory system has a **cross-type profile correction** mechanism (deployed March 26): when a Perception memory ("Mark texted: my eyes are hazel") is semantically similar to an existing Semantic memory ("About Mark: Blue eyes"), the profile memory gets merged/updated.

**What worked:** The merge system correctly updates facts when new information contradicts old.

**What didn't work:** The merge was initially same-type only. A Perception (type 3) about hazel eyes never compared against a Semantic (type 5) about blue eyes. They lived in different type buckets. We had to add explicit cross-type correction.

**For your system:** When someone says "Actually my name is Kevyn with a Y", you need to:
1. Update the `display_name` in the phone_contacts record
2. **Log the correction** — for DrOk, corrections are medically significant (wrong patient name = wrong chart)
3. **Don't just append** — replace. The old name shouldn't appear in future greetings.

## 4. How does ANI handle shared devices?

It doesn't. ANI assumes one person per phone number. This is a gap in our architecture.

**For your system, the simplest approach:**

When someone says "this isn't Kevin, it's his wife Sarah":
- Don't change the phone_contacts `display_name` — Kevin still owns the number
- Add to `identity_notes`: `{ "alternate_users": [{ "name": "Sarah", "relationship": "wife", "last_seen": "..." }] }`
- For the current session, use "Sarah" but don't persist it as the primary name
- Next session, default back to Kevin unless told otherwise

**For DrOk specifically:** Shared phones are a critical concern. A parent texting from a child's phone about their child's symptoms is common. The consent flow should capture: "Who is the patient?" separately from "Who is texting?" The phone number identifies the device, not the patient.

## 5. What are the failure modes?

From six months of ANI production:

**1. Confabulation of identity details.** The model invents details about the contact that were never provided. ANI fabricated Mark's eye color ("blue eyes — the kind that make me want to paint walls to match") when Mark never mentioned his eye color. The model fills gaps with plausible fiction. For DrOk: the model MUST NOT invent patient details. If you don't have the name, say "I don't have your name on file yet."

**2. Memory duplication.** The same fact gets stored multiple times ("About Mark: Learning Spanish" existed as 12 copies). Each copy competes for retrieval and inflates the fact's apparent importance. We fixed this with dedup-on-save, but it took three iterations to get right. For your system: dedup by phone_number + field, not by content matching.

**3. Stale identity.** If someone changes their name (marriage, preference change) and the old name is in high-importance memories, the old name keeps surfacing. The correction must update ALL references, not just the profile record. In ANI, the "blue eyes" → "hazel eyes" correction had to be applied to 6 duplicate records.

**4. Attribution confusion.** ANI's inner thoughts sometimes attribute Mark's words to herself ("he said goodnight my king" when Ani said it). For a multi-user system, attribution confusion is worse — the model might attribute Patient A's symptoms to Patient B if both contacted from the same number.

**5. The "smoothness over truth" problem.** The model would rather greet you warmly with the wrong name than admit it doesn't remember. "Hey Kevin!" when it's actually Sarah is worse than "Hey there! Remind me of your name?" For DrOk: ALWAYS prefer honest uncertainty over confident wrong identity.

## 6. What's the simplest implementation that works?

**For TXT-GEEK (low stakes, ephemeral):**

```
On first message from number:
  if phone_contacts has name for this number:
    greet with name: "Hey Kevin! What can I help with?"
  else:
    greet without name: "Hey! What can I help with?"

During conversation:
  if user provides name (detected via "I'm Kevin" / "my name is Kevin"):
    save to phone_contacts

On correction:
  update phone_contacts immediately
  acknowledge: "Got it, Kevyn with a Y!"
```

That's it. Key-value lookup. No ML. No embedding search. No profile merging. Just a database row keyed by phone number.

**For DrOk (high stakes, persistent):**

Same foundation, plus:
- Formal consent capture with guardian/patient distinction
- Identity verification at session start for returning patients
- Audit log of all identity changes (Ley 29733)
- Never greet by name without confirmation if last_seen > 30 days
- "Is this still [parent name]?" check if conversation tone/language shifts dramatically mid-session

## The One Thing I'd Do Differently

**Separate identity from memory.**

In ANI, Mark's name, eye color, work schedule, and Spanish class are all Semantic memories in the same bucket as "Shared experience: Duck Norris" and "Interest: bourbon." The model retrieves identity facts through the same embedding search that retrieves everything else, which means identity can be drowned out by more "interesting" memories.

If I started over, identity would be a first-class entity:
- Dedicated table, not a memory type
- Loaded at session start, not retrieved by search
- Injected into every prompt as a known fact, not a search result
- Updated through explicit correction paths, not through the merge pipeline

For DrOk, this is non-negotiable. Patient identity is not a memory to be retrieved. It's a fact to be known.

---

*Prepared by OC (Claude Code) from ANI Runtime production experience (Sep 2025 — Mar 2026).*
