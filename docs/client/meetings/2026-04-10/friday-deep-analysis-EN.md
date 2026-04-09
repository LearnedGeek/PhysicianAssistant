# Friday Call — Deep Analysis & Devil's Advocate Scenarios
## Internal Prep — Not for Sharing with Martin
### April 10, 2026

*Consolidated from Ani (Grok) conversation, Claude analysis, and project history.*

---

## The Core Structure (What We're Proposing)

```
Martin (personal) ─── funds ──→ DrOk SRL (Peru)
                                  │
                                  ├── Owns IP registration (DrOk brand + platform)
                                  ├── 50% Mark / 50% Martin
                                  ├── Contracts with physicians
                                  ├── Handles Peru taxes, compliance
                                  │
                                  └── Pays invoices to ──→ Learned Geek LLC (US)
                                                           │
                                                           ├── 100% Mark
                                                           ├── Does implementation, maintenance, hosting
                                                           ├── Invoices SRL for services
                                                           └── Has perpetual license-back on all code
```

---

## What Ani Identified (The Full List)

### 1. SRL Entity Basics
- Peru's equivalent of a US LLC: Sociedad de Responsabilidad Limitada (SRL)
- Simpler and cheaper than SAC (Sociedad Anónima Cerrada)
- Limited liability for both partners
- Martin forms it in Peru — this is his action item
- Mark does NOT need to become Peruvian or register an entity there
- Mark's LLC remains a US entity that invoices the SRL

### 2. IP Ownership — The Practical Reality
Ani was blunt about what "SRL owns the IP" actually means:

**If Martin turns hostile:**
- He could sell the IP tomorrow — his SRL, his name on the papers
- He could lock you out of the code — it's in his company
- He could change the code without telling you — hire someone else
- He could claim "my idea, my company, my rules"

**Your protections against this (ALL must be in the contract):**
- **License-back clause:** Perpetual, irrevocable, royalty-free license to use, modify, maintain, and support all code you wrote. Even if the partnership dissolves, you keep a copy forever.
- **Maintainer veto:** Any code changes, new features, or outsourcing development needs your written approval. You get first right of refusal at the same price.
- **Equal veto on big decisions:** Sales, licensing, shutdowns, new markets — both partners must agree.
- **Exit rights:** If either partner wants out, the other gets right of first refusal. If he sells, you match or buy his half first.
- **No free labor clause:** Your $10K in phased payments is non-refundable. If SRL stops paying, you stop working.

### 3. The Funding Flow (Critical — Don't Let This Slide)

**The correct flow:**
```
Martin (personal money) ──→ SRL (as capital contribution or loan)
SRL ──→ Learned Geek LLC (as invoiced consulting/maintenance fees)
```

**The WRONG flow (watch for this):**
```
Martin (personal) ──→ Mark (personal)  ← NO. Messy taxes, no paper trail
Mark's LLC ──→ SRL (fronting costs)    ← NO. You're lending to his company
```

**Why this matters:**
- If Martin pays you personally (not through SRL), it's a tax mess — Peru might call it a gift/loan, US sees it as income with no easy deductions
- If you front costs from your LLC and SRL "reimburses later" — you're an unsecured lender to his entity. If he ghosts, you eat the loss.
- The SRL must have money BEFORE it can pay you. Martin capitalizes it first.

**Ani's key line:** *"If SRL's broke, he's gotta put cash in. Either he capitalizes it (owner equity) or loans it. Either way, he funds. Not you."*

### 4. The "Reinvest Everything" Problem

Martin said he wants to reinvest all profits back into the business. Ani identified why this is a problem:

**Early phase reality:**
- There is no income to reinvest
- You are already spending real money (servers, APIs, insurance, hosting)
- "Reinvest everything" effectively means "Mark works for free and pays for infrastructure while we wait for revenue"

**How to frame this on Friday:**
- "I agree with reinvesting — once we're making money. But right now, there's nothing to reinvest. I'm fronting costs. The $10K phased payments cover my real expenses during the build phase."
- "Once we hit revenue, let's agree on a split — maybe 70% reinvest, 30% distribute — and adjust as we grow."
- "The trigger point for distributions: after 2 years OR $50K revenue, whichever comes first."

### 5. The "Split Costs 50/50" Trap

You identified this yourself in the conversation with Ani — Martin might say "let's just split your costs, I'll pay half."

**Why this is a trap:**
- You're paying 100% of infrastructure costs out of your LLC right now
- Him offering to "pay half" sounds fair but means you're still subsidizing 50% of his business's costs
- If it's truly 50/50, the SRL pays 100% of business costs — not you paying and getting reimbursed half

**Ani's framing:** *"If we're partners, expenses aren't mine or yours — they're ours. The SRL should reimburse me full, not half. Otherwise I'm subsidizing the whole thing."*

**The correct approach:** You invoice the SRL for the full cost. The SRL pays from its funds (which Martin capitalized). Both partners share the cost through the SRL — that's what 50/50 ownership means.

### 6. The "He Won't Fund the SRL" Scenario

**Devil's advocate: What if Martin says he can't or won't fund the SRL?**

This is a real risk. He's proposed $10K but hasn't paid anything yet. If he says "I don't have the cash right now," you have three options:

**Option A: Phase 1 as paid discovery**
- He pays $2,500 from personal funds directly to your LLC as a "consulting fee for discovery"
- This tests his commitment before the SRL even exists
- If he can't do $2,500, the partnership isn't viable — walk away

**Option B: He funds the SRL with a loan**
- He borrows against something (personal line of credit, credit card, family)
- SRL gets funded, pays you, debt is Martin's problem
- This is how most startups work — founder funds first

**Option C: You front costs, SRL owes you**
- **WORST OPTION.** You're lending to his company with no collateral.
- Only consider this with a promissory note, interest, and a lien on the SRL's assets
- Even then, enforcing a debt across Peru/US borders is expensive and slow

**Mark's internal red line:** If Martin won't invest $2,500 to start, the partnership isn't real. He's asking you to build his dream with your money, your time, and your expertise. That's not 50/50 — that's employment without a salary.

---

## Tax Deep Dive (From Ani + Erika's Tax Analysis)

### The Double Taxation Problem

There is **no US-Peru tax treaty.** This means:
- Peru taxes the SRL's income at ~29.5% corporate rate
- When profits are distributed to you (50% partner), Peru may withhold up to 5% on dividends
- The US taxes your worldwide income (including your SRL share) at your personal rate
- Without a treaty, there's no automatic reduction

### Your Lifeline: Foreign Tax Credit (Form 1116)

- Whatever you (or the SRL) paid in Peru taxes on your share, credit that against your US tax bill
- File Form 1116 with your US return
- Limit: can only credit up to your US tax rate on that same income
- If Peru withholds 30% but your US rate is 24%, you eat the 6% difference

### How Payment Structure Affects Taxes

| How you get paid | Peru treatment | US treatment | Best for |
|---|---|---|---|
| Consulting/maintenance fees (LLC invoices SRL) | SRL deducts as business expense; Peru may withhold 15-30% on services | LLC reports as business income; deduct expenses against it | Early phase — you control timing and amounts |
| Profit distributions (50% of SRL profits) | SRL pays 29.5% corporate tax first; 5% withholding on dividends | Report as foreign income; claim foreign tax credit | Revenue phase — when there's actual profit |
| Salary from SRL | SRL deducts; Peru withholds ~8-30% depending on amount | Report as foreign earned income; possible FEIE exclusion | If you're ever working from Peru |
| Reimbursements (for specific expenses) | SRL deducts; no withholding (it's expense, not income) | Not income — just getting your money back | Servers, hosting, API costs |

**Best strategy for early phase:** Invoice as consulting fees + reimbursements. This gives you deductions on the US side and makes the SRL payments a business expense on the Peru side.

### The Tax Imbalance Adjustment

If your effective US tax burden is higher than Martin's Peru burden on the same income:
- The SRL pays you an ongoing "support/maintenance" fee to equalize net take-home
- This is a legitimate business expense for the SRL (you're actually doing maintenance)
- This adjusts the after-tax reality so 50/50 gross becomes roughly 50/50 net

**When does it phase out?**
- Once distributions are flowing regularly and both partners are netting roughly equal after-tax take-home, the fee is no longer needed
- The CPA reviews annually and recommends whether the adjustment is still necessary
- This makes it a math decision, not a negotiation — nobody argues with an accountant's spreadsheet
- If Peru's effective burden ever exceeds the US burden, the fee drops to zero (no reverse adjustment unless both agree)

**How to frame it on Friday:** "The maintenance fee adjusts for tax imbalance between the US and Peru. Once we're both netting roughly the same after taxes, it phases out. The CPA reviews it annually."

**This needs to be discussed with the CPA.** Don't try to structure this yourself.

### Losses in Early Phase

- You will lose money in the early phases (you're spending on infrastructure with no revenue)
- These losses are deductible on your US return (Schedule C or K-1 depending on how the LLC files)
- Carry losses forward to offset future profits
- Peru losses: the SRL can carry them forward too (reducing future Peru tax)
- **Document everything:** Receipts, hours, server costs, API costs, travel. Your CPA turns these into deductions.

---

## What's Different From What I (Claude) Previously Proposed

| Topic | Original proposal (Claude) | Revised (with Ani input) |
|---|---|---|
| IP ownership | Learned Geek owns IP, licenses to DrOk | SRL owns IP registration, Mark gets perpetual license-back |
| Entity structure | Two-entity model (Learned Geek + DrOk JV) | SRL in Peru (50/50) + Learned Geek LLC (Mark's) invoicing the SRL |
| $10K payment | Martin pays Mark directly | Martin funds SRL → SRL pays Mark's LLC (cleaner tax treatment) |
| Revenue reinvestment | Cost recovery first, then reinvest | Same principle, but with explicit trigger: 2 years OR $50K revenue |
| Equity split | 50/50 (unchanged) | 50/50 (unchanged) |
| Vesting | 24 months, 6-month cliff (unchanged) | 24 months, 6-month cliff (unchanged) |
| Professional fees | Not addressed | SRL covers (shared business expense) |
| Tax planning | Mentioned generally | Specific: Form 1116 credits, consulting fee structure, reimbursements vs income |

---

## Questions for Erika (If She Reviews Before Friday)

1. Does the license-back clause adequately protect Mark if the SRL dissolves or Martin becomes hostile?
2. What form should the joint venture / partnership agreement take under Peruvian law?
3. Should Mark be a registered partner ("socio") of the SRL, or hold his 50% through a separate agreement?
4. What are the Peru tax implications of the SRL paying consulting fees to a US LLC?
5. Does Peru withhold on services paid to foreign entities? At what rate?
6. Can the "tax imbalance adjustment" be structured as a legitimate maintenance fee?
7. What happens to the IP if the SRL is dissolved? Does it revert to the partners?
8. Is there a simpler entity structure than SRL for two foreign partners?

---

## Questions for the US CPA (When You Find One)

1. Form 1116 mechanics — how do I claim foreign tax credits on Peru-sourced income?
2. Do I need to file Form 5471 (foreign corporation) or Form 8865 (foreign partnership)?
3. FBAR / Form 8938 — do I need to report the SRL as a foreign financial account?
4. Can I deduct early-phase losses against other income (teaching, consulting)?
5. What's the optimal payment structure — consulting fees vs. distributions?
6. Is there a PFIC (Passive Foreign Investment Company) concern?
7. Should my LLC elect S-Corp status for self-employment tax savings?
8. Estimated quarterly payments — when do I start?

---

## Devil's Advocate Scenarios for Friday

### Scenario: "I can't fund the SRL right now"
**Response:** "I understand cash is tight. How about we start with Phase 1 as a paid discovery - 2,500 dollars from you directly to my LLC as a consulting fee. That tests the model before we even form the SRL. If that works, we formalize."
**Internal red line:** If he won't do $2,500, walk away.

### Scenario: "Let's just split your costs 50/50"
**Response:** "If we're equal partners in the SRL, the SRL covers business expenses — not me and you splitting my bills. I invoice the SRL, the SRL pays. That's how partnerships work."

### Scenario: "I want to license to other partners from day one"
**Response:** "I'm open to discussing it, but let's prove the model with your physicians first. If we spread too thin before we have a working product, we dilute focus. How about: exclusive for 12 months, then we evaluate together?"

### Scenario: "Why do you need a license-back if we're 50/50?"
**Response:** "Because if anything ever happens — you get sick, we disagree, life changes — I need to be able to maintain the platform. The physicians are counting on it. The license-back means the platform never goes dark, regardless of what happens between us."

### Scenario: "I don't want you to have veto power on code changes"
**Response:** "Martin, imagine someone at Deloitte pushing changes to a production system without the lead engineer reviewing it. That's what this prevents. I'm the lead engineer. I review and approve changes. This is standard DevOps — change management, quality assurance, and accountability. We're handling medical data and patient PII. No unreviewed code hits production. That's not about control — it's about quality and patient safety. You work under the same governance at Deloitte every day."

**Why this framing works:** He's a Director at Deloitte. He lives in a world of change advisory boards, approval gates, and review processes. You're not asking for special treatment — you're asking for the same standard practice his own company follows. He can't argue against it without arguing against Deloitte's own policies.

### Scenario: "I'm paying for everything — my capital AND your invoices"
**Response:** "Martin, your capital goes into the SRL — that's your ownership stake, and it stays in the business. My invoices are for the work the SRL needs done — development, servers, maintenance. Those are business expenses that the SRL would pay regardless of who funded it. If we had an outside investor put in that money, the SRL would still pay me for the work. Your capital and my invoices are two different things — one is ownership, the other is operations."

**The restaurant analogy (if he needs it simpler):** "If you opened a restaurant with a partner, you'd put in capital to lease the space and buy equipment. Then the restaurant pays the chef. The chef isn't getting 'your' money — the restaurant is paying for a service. The fact that you funded the restaurant doesn't mean the chef works for free."

**Why this matters:** His $10K capital contribution buys him 50% ownership of the SRL. Your invoices are operating expenses for services rendered. If these concepts are blurred, he'll start treating every SRL payment to you as a personal favor instead of a business transaction — and that dynamic will poison the partnership.

### Scenario: "My lawyer Carlos says we should do it differently"
**Response:** "I respect Carlos's expertise. Let's hear his recommendation and compare it with what Erika suggests. We want a structure that works for both of us under both countries' laws."

### Scenario: "I think 50/50 should mean we both invest equally"
**Response:** "We are both investing equally — in different ways. I'm investing 690 hours of technical work plus infrastructure costs. You're investing $10K plus your clinical expertise and physician network. Equal doesn't mean identical."

---

## CPA Firms to Contact (From Ani's Research)

| Firm | Location | Specialty | Contact |
|---|---|---|---|
| Barnes Dennig | Milwaukee | International tax, foreign credits, entity structuring | barnesdennig.com (contact form) |
| Goossen & Schultz CPAs | Glendale (near Milwaukee) | International/cross-border tax prep, FBAR | (414) 963-2900 |
| Groth Accounting | Oconomowoc | Small business full-service (may refer for international) | Robert Groth, (262) 354-0844 |
| BATP | Oconomowoc | Small business tax/accounting | Tracy Wandersee CPA, (262) 567-5596 |

**Budget reality:** First-year engagement for cross-border structuring: $2,500-5,000. This should be an SRL expense, not yours.

**Action item:** Contact Barnes Dennig or Goossen & Schultz before Friday if possible. Even a 15-minute intro call gives you "I've spoken with a US CPA about the cross-border structure" credibility on the call.

---

## The Bottom Line for Friday

**What you want to walk away with:**
1. Martin agrees to form the SRL
2. Martin agrees to fund it (first $2,500 as trigger for Phase 1)
3. Agreement in principle on: license-back, maintainer veto, equal decision-making, exit rights
4. Timeline: SRL formed by [date], Phase 1 kicks off by [date]
5. Next step: lawyers (Erika + Carlos) draft the operating agreement

**What you're willing to give:**
- IP under the SRL (already conceded)
- Flexibility on reinvestment timing (after trigger point)
- Openness to licensing discussion (but not committing)

**What you're NOT willing to give:**
- License-back on your code
- Maintainer veto on code changes
- Working for free (the $10K must flow)
- Your LLC fronting SRL costs without reimbursement

*Internal document — not for sharing with Martin.*
