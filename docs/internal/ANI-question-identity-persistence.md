# Question for ANI Project (OC) — Identity Persistence by Phone Number

**Date:** March 30, 2026
**From:** PhysicianAssistant / DrOk project
**Context:** We have two services routing SMS/WhatsApp by phone number. We want to remember who's texting (at minimum their name) across sessions. This is a shared problem with ANI.

---

## The Problem

We track conversations by phone number. Users tell us their name during conversation. We want to:
1. Remember their name next time they text
2. Use it naturally ("Hey Kevin, good to hear from you again")
3. Handle corrections ("Actually my name is spelled Kevyn")
4. Handle identity changes ("This isn't Kevin, it's his wife Sarah")
5. Handle shared phones (parent texts from child's phone, etc.)

## What We Need From ANI's Experience

1. **How does ANI handle identity persistence?** What worked, what didn't?
2. **Where is identity stored?** In the conversation history? Separate profile? Database?
3. **How does ANI handle identity corrections?** ("That's not my name" / "I'm someone else")
4. **How does ANI handle shared devices?** Does it detect when a different person is using the same device?
5. **What are the failure modes?** What caused problems in production? What assumptions were wrong?
6. **What's the simplest implementation that works?** We don't need perfection — we need "good enough for SMS" where sessions are ephemeral and context is limited.

## Our Constraints

- SMS/WhatsApp — no login, no auth, just phone numbers
- TXT-GEEK: ephemeral sessions (30 min timeout), low stakes
- DrOk: persistent records, patient identity is critical (Ley 29733 compliance)
- We can't ask for verification every time — it needs to feel natural
- Phone number is the only identifier we have

## What We're Thinking

- Simple key-value store: phone number → { name, first_seen, last_seen, notes }
- On first message of a new session: if we have a name on file, greet them by name
- If they correct us, update the record
- If they say "this is someone else," create a new identity note but keep the number mapping
- For DrOk: more formal — consent flow captures parent/guardian identity, verified

## The Real Question

Before we implement this, what did ANI learn the hard way that we should know? What's the one thing you'd do differently if you started identity persistence from scratch?

---

*Take this to the ANI project Claude and bring back the answers before implementing.*
