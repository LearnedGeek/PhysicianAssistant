# Project Initiation Checklist — Learned Geek / Mark McArthey
## Proyecto DrOk — Pre-Engagement Must-Do Items

**Last updated:** March 23, 2026
**Purpose:** Mark's personal action tracker for everything that must be done *before* production work begins. These are non-code obligations — legal, business, compliance, vendor — that cannot be delegated to an intern and cannot be skipped. Each item has a deadline tier and a hard blocker flag.

**Deadline tiers:**
- 🔥 **This week** — Cannot responsibly continue without this
- 📅 **Before Discovery call** — Must be done before the formal kickoff
- 🏁 **Before engagement letter** — Gate to signing
- 🚀 **Before go-live** — Must be done before production launch

---

## 1. Insurance (🔥 This Week)

| # | Item | Status | Notes |
|---|---|---|---|
| I001 | Get E&O (Errors & Omissions) insurance quotes | 🔴 Open | Medical-adjacent software; standard solo consultant need |
| I002 | Get professional liability insurance quotes | 🔴 Open | Overlaps with E&O — some policies combine |
| I003 | Get cyber liability insurance quotes | 🔴 Open | You will handle pediatric PHI — this is not optional |
| I004 | Confirm Learned Geek LLC protects Mark personally for medical-adjacent + international work | 🔴 Open | Quick call with a business attorney; LLC may need supplementing |

**Why this week:** You are currently uninsured for this category of work. If Discovery happens before insurance is in place and something goes sideways, you have no coverage. Insurance takes days to weeks to bind — start now.

**Suggested providers to quote:** Hiscox, Embroker, Argo (E&O + cyber packages for tech consultants). Check WCTC connections or professional associations for group rates.

---

## 2. Vendor Policy Review (🔥 This Week)

These are short reads. Do not sign an engagement letter or write production code that touches these vendors without clearing them.

| # | Item | Status | Notes |
|---|---|---|---|
| V001 | Read Anthropic Acceptable Use Policy — check for medical/clinical restrictions | 🔴 Open | Specifically: clinical impressions, medical advice, pediatric data |
| V002 | Read Meta/WhatsApp Business Policy — check for health data restrictions | 🔴 Open | Health data over WhatsApp may have explicit terms |
| V003 | Read Twilio Acceptable Use Policy — check for medical use restrictions | 🔴 Open | Twilio has HIPAA-eligible services; confirm what's required for Peru |
| V004 | Check whether Anthropic offers a DPA / BAA for health data processing | 🔴 Open | If they process PHI through the API, a formal agreement may be required |
| V005 | Check whether Twilio offers a DPA / BAA for health data | 🔴 Open | Twilio does offer HIPAA Business Associate Agreements — understand scope |

**If any vendor prohibits the intended use:** that is a project architecture change, not a workaround. Find out now.

---

## 3. Legal / Engagement (📅 Before Discovery Call)

| # | Item | Status | Notes |
|---|---|---|---|
| L001 | Draft engagement letter — or have attorney draft | 🔴 Open | Must include: scope, liability cap, IP ownership, indemnification, impresión Dx. definition |
| L002 | Confirm engagement letter reviewed by US attorney | 🔴 Open | You are a US LLC; US law governs your contract |
| L003 | Assess whether Learned Geek LLC needs to register as a foreign entity doing business in Peru | 🔴 Open | Receiving payment from a Peruvian client for services; quick attorney/accountant question |
| L004 | Assess Wisconsin / US tax treatment of Peruvian client revenue | 🔴 Open | International income may have FBAR or Form 8938 implications — accountant question |
| L005 | Confirm IP ownership language: Learned Geek retains platform IP; Martin licenses | 🔴 Open | Especially critical given productization potential |

---

## 4. Confirm Carlos Rojas is Actively Working (📅 Before Discovery Call)

Carlos Rojas received the action items document via Martin. But "received" is not the same as "working on it."

| # | Item | Status | Notes |
|---|---|---|---|
| C001 | Nudge Martin to confirm Carlos is actively reviewing — not just filed | 🔴 Open | Polite: "¿Carlos ya pudo revisar el documento?" |
| C002 | Confirm DIGEMID classification question is explicitly in front of Carlos | 🔴 Open | Most critical single legal question — viability depends on the answer |
| C003 | Confirm Ley 30421 (telemedicine) question is explicitly in front of Carlos | 🔴 Open | May impose licensing requirements |
| C004 | Set a soft deadline expectation with Martin for Carlos's responses | 🔴 Open | "Sin prisa" is fine for Martin; Carlos should have a timeline |
| C005 | Establish whether Carlos will communicate directly with Mark or only through Martin | 🔴 Open | Direct line to Carlos would be cleaner for legal back-and-forth |

---

## 5. Vendor Account Setup (📅 Before Discovery Call)

These are free or low-cost and take minutes. No reason to wait.

| # | Item | Status | Notes |
|---|---|---|---|
| A001 | Register NCBI API key (PubMed) | 🔴 Open | Free; takes 2 minutes; required for production rate limits |
| A002 | Confirm Twilio WhatsApp Business API application started | 🔴 Open | 1–4 week approval lead time; start now |
| A003 | Identify cloud hosting provider for production (Azure, AWS, or other) | 🔴 Open | Affects SCC analysis — which US entity is receiving Peru data |
| A004 | Confirm blob storage provider for attachments (Azure Blob, AWS S3) | 🔴 Open | PHI in attachments — provider must support encryption at rest |

---

## 6. GitHub Project Setup (📅 Before Discovery Call)

| # | Item | Status | Notes |
|---|---|---|---|
| G001 | Create GitHub Project under LearnedGeek org | 🔴 Open | Board with milestone view |
| G002 | Define labels: phase, type (feature/legal/doc/infra/test), priority | 🔴 Open | |
| G003 | Add custom fields: Estimate (hrs), Actual (hrs), Phase, Blocker (Y/N) | 🔴 Open | Required for intern hour tracking vs. WCTC 144-hr requirement |
| G004 | Create milestone structure matching project phases | 🔴 Open | Phase 0 (done) → Phase 1 Discovery → Phase 2 → ... |
| G005 | Seed initial issues from task breakdown | 🔴 Open | Start with Phase 1 Discovery tasks and Learned Geek initiation items |

---

## 7. Engagement Letter Gates (🏁 Before Engagement Letter)

Nothing billable ships without these resolved.

| # | Item | Status | Notes |
|---|---|---|---|
| E001 | Budget range confirmed with Martin | 🔴 Open | Tiered scope options prepared if budget is tight |
| E002 | DIGEMID classification answer received from Carlos Rojas | 🔴 Open | Project viability question — must know before scoping the contract |
| E003 | Ley 30421 telemedicine applicability answered | 🔴 Open | May change scope/architecture |
| E004 | Insurance bound (I001–I003) | 🔴 Open | Do not sign before insured |
| E005 | Engagement letter reviewed by US attorney | 🔴 Open | |
| E006 | Engagement letter reviewed by Carlos Rojas (Peruvian law side) | 🔴 Open | DPA / data processing agreement language especially |
| E007 | Martin signs engagement letter | 🔴 Open | This is the starting gun |

---

## 8. Pre-Launch Compliance (🚀 Before Go-Live)

These are not initiation items but are tracked here so nothing falls through the cracks.

| # | Item | Status | Notes |
|---|---|---|---|
| P001 | SCCs executed with Anthropic | 🔴 Open | Required for Peru → US data transfer |
| P002 | SCCs executed with Twilio | 🔴 Open | Same |
| P003 | ANPD data bank registration filed | 🔴 Open | Required before storing any patient data |
| P004 | Transfer Impact Assessment (TIA) completed | 🔴 Open | Recommended by Martin's analysis |
| P005 | Data Processing Agreement (DPA) signed between Learned Geek and Martin's practice | 🔴 Open | Formalizes encargado relationship under Ley 29733 |
| P006 | Security audit / penetration test completed | 🔴 Open | External audit recommended for medical-adjacent data |
| P007 | Martin formal sign-off on AI accuracy (UAT gate) | 🔴 Open | Documented sign-off required; kept on file |
| P008 | WhatsApp Business API approved and in production | 🔴 Open | |

---

## Summary — What to Do This Week

1. **Insurance quotes** (I001–I004) — call Hiscox or Embroker, get quotes in hand
2. **Read three vendor policies** (V001–V003) — 30–60 min total; do not skip
3. **Nudge Martin on Carlos** (C001–C004) — one WhatsApp message, politely
4. **Register NCBI API key** (A001) — literally 2 minutes
5. **Start WhatsApp Business API application** (A002) — lead time is the constraint

Everything else can follow in the next 1–2 weeks, but these five unblock everything downstream.

---

*Learned Geek — Internal document. Not for client distribution.*
