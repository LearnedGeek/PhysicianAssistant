# Learned Geek LLC — Working Notes & Task Tracker

---

## Attorney Review Guidance

For the Operating Agreement, MSA, NDA, and Internship Agreement — Erika's gut-check plus your own careful reading is sufficient for day-to-day use. Courts generally uphold clearly written contracts even when they're not attorney-drafted, as long as the intent is unambiguous.

**The one place to spend money:** The DrOk final engagement letter / service agreement. International counterparty, meaningful IP, revenue share, healthcare-adjacent data. A targeted one-time review of just that agreement is worth $500. Services like Clerky, Contractbook, and Wisconsin Bar referral programs offer flat-fee contract reviews ($150–$250) for small businesses.

---

## Document Tasks — By Priority

### P0 — Before Discovery Call

| # | Task | Owner | Status | Notes |
|---|---|---|---|---|
| 1 | Send term sheet to Erika for gut check | Mark | ✅ Sent 2026-03-23 | In erika-review/ directory |
| 2 | Update NDA template to mutual | Claude | ✅ Done | templates/NDA — now mutual with permitted disclosures |
| 3 | Create DrOk-specific mutual NDA | Claude | ✅ Done | drok-engagement/NDA-MUTUAL-DROK.md |
| 4 | Update MSA: add medical disclaimer | Claude | ✅ Done | MSA §6.3 — clinical disclaimer |
| 5 | Update MSA: add DPA reference clause | Claude | ✅ Done | MSA §5 — data processing section |
| 6 | Update MSA: add force majeure | Claude | ✅ Done | MSA §11 — includes cloud/API outages |
| 7 | Update MSA: add regulatory compliance split | Claude | ✅ Done | MSA §9 — client responsible for local regulatory |
| 8 | Update MSA: add international clause | Claude | ✅ Done | MSA §13.3 — dual governing law |
| 9 | Get insurance quotes (E&O + cyber liability) | Mark | ❌ Pending | **This week.** No current coverage. |
| 10 | Schedule Discovery call with Martin | Mark | ❌ Pending | Waiting on Carlos Rojas DIGEMID response |

### P1 — Before Engagement Letter

| # | Task | Owner | Status | Notes |
|---|---|---|---|---|
| 11 | Draft SOW — Discovery Phase | Claude | ✅ Done | drok-engagement/SOW-DISCOVERY.md |
| 12 | Draft Engagement Letter | Claude | ❌ Pending | Formalizes relationship; references MSA. Draft after Discovery. |
| 13 | Draft Revenue Share Addendum | Claude | ✅ Done | drok-engagement/EXCLUSIVITY-ADDENDUM.md (§5 Revenue Share) |
| 14 | Draft Clinical Disclaimer Addendum | Claude | ✅ Done | drok-engagement/CLINICAL-DISCLAIMER-ADDENDUM.md |
| 15 | Draft Exclusivity Addendum | Claude | ✅ Done | drok-engagement/EXCLUSIVITY-ADDENDUM.md |
| 16 | Draft DPA | Claude | ✅ Done | drok-engagement/DPA-DROK.md (Annex A SCCs pending Carlos) |
| 17 | Formal attorney review of final DrOk agreement | Mark | ❌ Pending | Budget $250–500 |
| 18 | Check Anthropic AUP for medical use | Mark | ✅ Done 2026-03-23 | Medical use permitted with disclaimers; no diagnostic claims. Added to technical spec. |
| 19 | Check Twilio AUP for health data | Mark | ✅ Done 2026-03-24 | No healthcare restrictions. Compliance burden on customer. Need separate DPA/security review. |
| 20 | Check Anthropic BAA availability | Mark | ❌ Pending | |
| 21 | Check Twilio BAA availability | Mark | ❌ Pending | |

### P2 — Before Intern Starts (June 8, 2026)

| # | Task | Owner | Status | Notes |
|---|---|---|---|---|
| 22 | Update Internship Agreement: add PHI restriction | Claude | ✅ Done | templates/INTERNSHIP-AGREEMENT.md §6 |
| 23 | Update Internship Agreement: add WCTC refs | Claude | ✅ Done | templates/INTERNSHIP-AGREEMENT.md §2.4 |
| 24 | Update Internship Agreement: add access termination | Claude | ✅ Done | templates/INTERNSHIP-AGREEMENT.md §7.3 |
| 25 | Fill in dates/hours in Internship Agreement | Claude | ✅ Done | June 8 – July 31, 72–144 hrs, 18 hrs/week |
| 26 | Create Intern NDA | Claude | ✅ Done | intern-engagement/NDA-INTERN.md |
| 27 | Create Intern SOW / Scope Document | Claude | ✅ Done | docs/intern/INTERN-SOW.md — 3 phases, 12 deliverables |
| 28 | Obtain WCTC Training Agreement from Ellen | Mark | ❌ Pending | Required by program before start |
| 29 | Create SMART Goals template | Claude | ✅ Done | docs/intern/SMART-GOALS-TEMPLATE.md — 5 goals |
| 30 | Set up GitHub Project board for intern tracking | Mark/Claude | ❌ Pending | Issues, milestones, hour tracking fields |
| 31 | Create Independent Contractor Agreement template | Claude | ✅ Done | intern-engagement/INDEPENDENT-CONTRACTOR-AGREEMENT.md |
| 32 | Send Erika Internship Agreement for gut check | Mark | ❌ Pending | After updates complete |

### P3 — Before Go-Live

| # | Task | Owner | Status | Notes |
|---|---|---|---|---|
| 33 | Obtain BAA from Twilio | Mark | ❌ Pending | |
| 34 | Obtain BAA from Anthropic | Mark | ❌ Pending | |
| 35 | W-8BEN from Martin (or determine payment direction) | Mark | ❌ Pending | Tax form for international payments |
| 36 | Create W-9 for Learned Geek | Mark | ❌ Pending | |
| 37 | Complete APDP registration (with Carlos Rojas) | Martin/Carlos | ❌ Pending | Required under Ley 29733 |
| 38 | Confirm DIGEMID classification | Carlos Rojas | ❌ Pending | **Blocker.** Changes entire scope if classified as medical device. |
| 39 | Privacy Policy (public-facing) | Claude | ❌ Pending | Before end users access system |
| 40 | Terms of Service (public-facing) | Claude | ❌ Pending | Before end users access system |

---

## Pending Responses

| From | Item | Date Sent | Status |
|---|---|---|---|
| Carlos Rojas | DIGEMID classification, Ley 30421, regulatory review | 2026-03-22 (via Martin) | Martin confirmed Carlos is reviewing |
| Erika | Term sheet gut check | 2026-03-23 | Sent, awaiting response |
| Martin | Budget discussion (item 1.4) | 2026-03-19 | No response yet |
| Cathy Fang (MIT) | arXiv endorsement | 2026-03-23 (resent) | No response |
| Ethan Perez (Anthropic/NYU) | arXiv endorsement | 2026-03-24 | Sent, no bounce |

---

## Key Decisions Made

| Decision | Date | Rationale |
|---|---|---|
| Learned Geek retains all platform IP; Martin licenses | 2026-03-23 | Protects long-term value; reduces Mark's marketing/sales exposure |
| SaaS/platform fee model (not one-time project fee) | 2026-03-23 | Recurring revenue; scales with Martin's growth |
| Revenue split: 60/40 (platform), 20/80 (onboarding), 40/60 (B2C) | 2026-03-23 | Starting framework; reflects who carries cost vs. who sells |
| Emergency detection is deterministic, not LLM-generated | 2026-03-24 | ANI research validated: architecture over instruction |
| Null response is schema default, not exception | 2026-03-24 | ANI cross-project insight: prevents smoothness-over-truth |
| Stack: C# / Blazor / PostgreSQL | 2026-03-23 | Aligns with POC, Mark's expertise, intern skillset |

---

## Insurance — Action Required

**Current status:** No E&O or cyber liability insurance. Learned Geek LLC provides corporate veil only.

**Needed:**
- E&O / Professional Liability — covers claims from professional services
- Cyber Liability — covers data breach, privacy violations
- Confirm: does policy cover 1099 contractors and unpaid interns?
- Confirm: does WCTC carry student liability coverage for placement sites? (Ask Ellen)

**Get quotes this week.** This is the single most exposed risk item for Mark personally.
