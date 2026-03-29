# DrOk — Full Strategic Analysis
## Mark's Internal Document — Eyes Only

**Date:** March 27-28, 2026
**Context:** Consolidated from partnership analysis, cost scenarios, and bar-talk strategy sessions. This is the complete picture in one place.

---

## Part 1: The Situation

Martin proposed a three-way equity partnership (Mark, Martin, Carlos) after the March 27 meeting. He hasn't told Carlos yet — came to Mark first. Says percentages can be discussed later. If Peru is complicated, they can start in the US.

Karen's concern: Martin may be bringing Carlos in for funding, which means Martin doesn't have cash. This changes everything from "partnership" to "everyone contributes sweat equity and Mark pays the bills."

Mark has not responded yet.

---

## Part 2: What Mark Has (Rare Combination)

- Real AI safety research (ANI — 6 months, confabulation taxonomy)
- Working POC with solid architecture
- A physician partner who's genuinely committed (brought his best lawyer, proposed partnership unprompted)
- A platform that's model-agnostic and white-labelable
- Teaching income (WCTC) as a safety net during build

### Why DrOk Is Easier Than ANI
- ANI is a **research problem** — making AI honest about uncertainty. One of the hardest unsolved problems in AI.
- DrOk is an **engineering problem** — all pieces exist (Twilio, Claude, PubMed, Blazor). Complexity is regulatory/operational, not technical.
- ANI already solved DrOk's hardest part — confabulation mitigation, citation enforcement, authenticity boundary.
- Complexity (legal, admin, Spanish) can be delegated to Hannah. Core AI research cannot.

---

## Part 3: Real Costs — All-In Year 1

### Cash Out of Pocket

| Item | Annual Cost |
|---|---|
| Insurance (E&O + Cyber bundled) | $1,500 |
| Cloud hosting (NO GPU needed — all API calls) | $2,400 |
| Twilio production | $1,200 |
| Anthropic API | $1,200 |
| Domain, DNS, misc | $200 |
| UniFi for intern network isolation | $300-500 |
| Local doc storage | $0 (existing Buffalo NAS) |
| **Total cash** | **~$7,000-7,500** |

**Key insight:** No GPU server needed. The entire architecture is API calls — Claude does inference, Twilio does messaging, PubMed does retrieval. Orchestration server is a $50/month VM.

### Time Investment

| Work | Hours |
|---|---|
| Development (actual platform) | 200-250 |
| Legal/admin/regulatory | 150-200 |
| Martin/Carlos communication, meetings, translation | 50-75 |
| Intern management | 40-50 |
| Document prep, proposals, Drive setup | 30-40 |
| Insurance, vendor setup, compliance | 20-30 |
| **Total forward** | **~500-650 hours** |

**50% of time is NOT coding.** Legal, admin, meetings, translation, compliance. Hannah can absorb a lot of this.

### Sunk Costs (Already Invested)

| Item | Hours | Market Value |
|---|---|---|
| ANI research | ~200+ | $30,000+ |
| POC + architecture | ~80 | $12,000 |
| Legal document drafting | ~40 | $6,000 |
| Client communication | ~30 | $4,500 |
| **Total sunk** | **~350 hrs** | **~$52,500** |

### Grand Total (Sunk + Forward)

| | Hours | Market Value |
|---|---|---|
| Mark's total | ~730 hrs | ~$110,740 |
| Cash out of pocket | — | ~$7,000-7,500 |

---

## Part 4: What Martin Brings

| Item | Hours | Market Value |
|---|---|---|
| Clinical expertise + consultation | ~30 | $7,500 |
| Legal analysis (March 23) | ~10 | $2,500 |
| Carlos Rojas engagement (pro bono) | — | $5,000-15,000 |
| Professional reputation risk | — | Unquantifiable |
| UAT testing + clinical validation | ~40 | $10,000 |
| Physician network activation (50-100+) | ~100+ | $25,000+ |
| DIGEMID + regulatory navigation | ~20 | $5,000 |
| DrOk brand development | ~40 | $10,000 |
| **Total** | **~240+ hrs** | **~$65,000-75,000** |

### Side-by-Side

| | Mark | Martin |
|---|---|---|
| Time | ~730 hrs | ~240+ hrs |
| Market value | ~$110,000 | ~$65,000-75,000 |
| Cash | ~$7,000/yr | $0 to date |
| Post-launch weekly | 10-15 hrs | 10-15 hrs |
| Unique contribution | The platform | The physicians |
| Risk if fails | Time + cash | Time + reputation |

**Neither succeeds alone.** A platform without physicians is a demo. Physicians without a platform have no product.

---

## Part 5: Pricing Reality Check

### Peru
| Type | Monthly/Physician |
|---|---|
| Basic EMR | $30-80 |
| Telemedicine platform | $50-150 |
| AI clinical decision support | $100-300 |
| **Realistic DrOk Peru** | **$30-75** |

Peruvian physician salaries are $2,000-4,000/month. $100/month for an unproven AI tool is a hard sell.

### US (Comparison)
| Type | Monthly/Physician |
|---|---|
| AI clinical decision support | $200-500 |
| **DrOk US potential** | **$150-300** |

3-5x Peru pricing. Clearer regulatory path. Mark speaks the language.

---

## Part 6: Revenue Scenarios

### Scenario A: Conservative (Slow Adoption)

| | Year 1 | Year 2 | Year 3 |
|---|---|---|---|
| Physicians | 5→15 | 30 | 50 |
| Price/month | $75 | $75 | $100 |
| Annual revenue | ~$9,000 | ~$27,000 | ~$60,000 |
| Net (after platform costs) | ~$4,000 | ~$19,000 | ~$48,000 |

### Scenario B: Moderate (Martin Delivers)

| | Year 1 | Year 2 | Year 3 |
|---|---|---|---|
| Physicians | 15→50 | 100 | 200 |
| Price/month | $100 | $100 | $100 |
| Annual revenue | ~$39,000 | ~$120,000 | ~$240,000 |
| Net | ~$30,000 | ~$102,000 | ~$210,000 |

### Scenario C: Aggressive (Multi-Market)

| | Year 1 | Year 2 | Year 3 | Year 5 |
|---|---|---|---|---|
| Physicians | 30→100 | 300 | 500 | 2,000 |
| Price/month | $100 | $100 | $100 | $100 |
| Annual revenue | ~$78,000 | ~$360,000 | ~$600,000 | ~$2,400,000 |
| Net | ~$63,000 | ~$324,000 | ~$550,000 | ~$2,250,000 |

### Realistic Peru Pricing ($50/month)

| Physicians | Annual Revenue | Mark's Take (Model 3) |
|---|---|---|
| 50 | $30,000 | ~$18,000 |
| 100 | $60,000 | ~$36,000 |
| 300 | $180,000 | ~$108,000 |
| 500 | $300,000 | ~$180,000 |

At $50/month, need 300+ physicians for meaningful income. At $100 (if achievable), 100-150 is enough.

---

## Part 7: Partnership Models Compared

### Model 1: Original Term Sheet (Client-Vendor)

| | Mark Year 1 | Martin Year 1 |
|---|---|---|
| Build fee | +$17,500 | -$17,500 |
| Revenue (60/40, Scenario B) | +$18,000 | +$12,000 |
| **Total** | **$35,500** | **-$5,500** |

Martin is negative Year 1. Breaks even mid-Year 2. No equity, no exit upside. Martin would reasonably reject this.

### Model 2: Pure Equity (What Martin Proposed)

55/45 split, no build fee, no platform license.

| | Mark Year 1 | Martin Year 1 |
|---|---|---|
| Revenue (55/45, Scenario B) | +$16,500 | +$13,500 |
| Platform costs | -$9,000 | $0 |
| **Total** | **$7,500** | **$13,500** |

Mark earns $7,500 (~$11/hr) while Martin earns $13,500 with $0 invested. **Unacceptable.**

### Model 3: Two-Entity Hybrid (Recommended)

Learned Geek retains IP. DrOk JV. Build fee + platform license + equity.

| | Mark Year 1 | Martin Year 1 |
|---|---|---|
| Build fee | +$10,000 | -$10,000 |
| Platform license ($15/physician) | +$4,500 | (paid by DrOk) |
| Equity (55/45 of net) | +$9,350 | +$7,650 |
| **Total** | **$23,850** | **-$2,350** |

Martin breaks even early Year 2. At scale:

| Year | Mark | Martin |
|---|---|---|
| Year 2 (Scenario B) | $55,730 | $30,870 |
| Year 3 (Scenario B) | $113,550 | $63,450 |
| Year 5 (Scenario C) | $1,102,500 | $607,500 |

---

## Part 8: Platform Valuation — Where the Millions Are

SaaS = 5-10x ARR. Medical SaaS commands the higher end.

### DrOk Entity Value

| Physicians | ARR | Valuation (6-8x) | Mark 55% | Martin 45% |
|---|---|---|---|---|
| 100 | $120K | $720K-$960K | $396K-$528K | $324K-$432K |
| 500 | $600K | $3.6M-$4.8M | $2.0M-$2.6M | $1.6M-$2.2M |
| 1,000 | $1.2M | $7.2M-$9.6M | $4.0M-$5.3M | $3.2M-$4.3M |
| 2,000 | $2.4M | $14.4M-$19.2M | $7.9M-$10.6M | $6.5M-$8.6M |

### Learned Geek Platform Value (Separate — 100% Mark)

| Additional Markets | Additional ARR | Platform Valuation (6-8x) |
|---|---|---|
| 1 white-label | $120K | $720K-$960K |
| 3 markets | $500K | $3M-$4M |
| US market license | $1.2M+ | $7.2M-$9.6M |

### Mark's Combined Exit

| Scenario | DrOk (55%) | Platform (100%) | Total |
|---|---|---|---|
| 500 physicians, no white-label | $2.0M-$2.6M | Retained | $2.0M+ |
| 1,000 physicians + 1 white-label | $4.0M-$5.3M | $720K-$960K | $4.7M-$6.3M |
| 2,000 physicians + US market | $7.9M-$10.6M | $7.2M-$9.6M | $15.1M-$20.2M |

**The millions aren't in subscription fees. They're in the exit.** Peru is the proving ground. Martin is the first customer. The platform is the prize.

---

## Part 9: The Funding Problem & Solutions

Karen's concern is valid: if Martin needs Carlos for funding, he likely can't put $10K in.

### Option A: Phased Buy-In

| Milestone | Martin Pays |
|---|---|
| Discovery signed | $2,500 |
| Phase 2 complete | $2,500 |
| Phase 3 complete | $2,500 |
| Go-live | $2,500 |
| **Total** | **$10,000 over 4-5 months** |

$2,500 at a time is digestible. If Martin can't do $2,500 to start — that's the answer.

### Option B: Discovery as Paid Service First (Recommended)

- Charge Discovery as normal consulting: $3,000-3,500
- Client-vendor. No equity discussion yet.
- Tests whether Martin has any cash
- Answers regulatory questions before anyone commits
- Partnership conversation happens AFTER Discovery
- Doesn't close any doors

**This is the firewall.** Nobody signs anything until Discovery is done and paid for.

### Option C: Martin Covers Peru-Side Costs

| Martin Absorbs | Estimated Value |
|---|---|
| Carlos's legal work (free) | $10,000-20,000 |
| Trademark registration | $500-1,000 |
| ANPDP registration filing | $500-1,000 |
| DIGEMID clinic consultation | $500+ |
| All Peruvian regulatory filings | $1,000+ |

Mark covers all tech costs. Fair split, just not in cash.

---

## Part 10: What If It Doesn't Work?

| Scenario | Mark | Martin |
|---|---|---|
| 0 physicians after 12 months | Got build fee + has platform. Licenses elsewhere. | Lost cash + time. Keeps brand. |
| 5 physicians, stalls | Small ongoing income. Platform licensable. | Small income. Keeps relationships. |
| Martin loses interest | DrOk dissolves. Keeps platform. New partner. | Loses equity. Keeps brand. |
| Someone acquires DrOk | 55% of sale + keeps platform. | 45% of sale. |
| Someone acquires platform | Sells at his price. DrOk continues. | Unaffected. |
| Peru regulatory blocker | Pivot to US (Martin suggested this). | Same. |

**Both protected in every downside scenario.**

---

## Part 11: The Strategy

### Do This:
1. **Say yes to partnership — on your terms.** Two-entity model. Discovery first as paid service.
2. **Build the MVP fast.** 8-10 weeks. One doctor (Martin), real patients. Prove it works before solving every legal question.
3. **Let Hannah handle admin.** Documents, Drive, compliance tracking, meeting notes. Her internship — your saved hours.
4. **Keep US market in back pocket.** Martin said it himself. US has higher prices, clearer regs, and you speak the language.
5. **Don't solve all 49 legal questions before building.** Half are pre-launch, not pre-build. Build first, comply second.
6. **Set a deadline.** No 10 paying physicians by March 2027 → reassess.

### Don't Do This:
- Don't give up IP. Ever.
- Don't work for free. Martin invests cash or this isn't a partnership.
- Don't spend 6 months on regulatory compliance before one paying customer.
- Don't quit teaching.
- Don't let this become a five-year side project.

### Non-Negotiables:
1. Learned Geek retains 100% platform IP
2. Martin invests cash (phased or upfront)
3. All equity vests 24 months, 6-month cliff
4. 10 paying physicians in 12 months or exclusivity lapses
5. Mark has technical authority
6. Platform buyout separate from DrOk
7. Carlos is advisor (3-5%), not co-founder

---

## Part 12: The Bottom Line

This is a **$7,000 cash bet with 500-650 hours of time as the real stake.** Keep the WCTC job. Build fast. Don't let the legal tail wag the dog.

If it works: the platform is worth millions.
If it doesn't: you still own the platform and the ANI research.

Take the bet. Build fast. Prove the concept. Then scale.

---

*Internal document — Mark's eyes only. Not for Martin, Carlos, or Erika.*
*Consolidated March 28, 2026.*
