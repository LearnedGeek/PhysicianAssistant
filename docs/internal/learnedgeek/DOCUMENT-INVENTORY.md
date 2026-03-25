# Learned Geek LLC — Document Inventory

## Directory Structure

```
learnedgeek/
├── templates/              General LLC documents (reusable across engagements)
├── drok-engagement/        DrOk project-specific agreements and addendums
├── intern-engagement/      Intern-specific agreements
├── DOCUMENT-INVENTORY.md   This file
└── NOTES.md                Task tracker and working notes
```

---

## Tier 1 — General LLC Templates (`templates/`)

Reusable across any client engagement. Project-specific terms live in Tier 2.

| Document | Status | Needs Review By | Needs Signature By | Notes |
|---|---|---|---|---|
| Articles of Organization (PDF) | ✅ Filed | — | — | On file with Wisconsin DFI |
| EIN Letter (PDF) | ✅ Filed | — | — | 41-3648316 |
| Operating Agreement | ✅ Draft | Erika (gut check) | Mark | Article 6 (IP) is strong. Sign after Erika reviews. |
| MSA | ✅ Updated | Erika (gut check) | Mark + each Client | Added: clinical disclaimer (§6.3), DPA reference (§5), force majeure (§11), regulatory split (§9), international clause (§13.3), AI output disclaimer (§7.3) |
| NDA | ✅ Updated | Erika (gut check) | Mark + counterparty | Converted to mutual. Added: permitted disclosures (§3.5), compliance retention (§5), no-obligation clause (§7). |
| Internship Agreement | ✅ Updated | Erika (gut check) | Mark + Intern | Added: PHI restriction (§6), WCTC program refs (§2.4), access termination (§7.3). Dates/hours filled in. |
| Independent Contractor Agreement | ✅ Created | Erika (gut check) | Mark + Contractor | In intern-engagement/. IP assignment, data access restrictions, non-solicitation. |
| W-9 (Learned Geek) | ❌ Not created | — | Mark | Standard tax form. Needed for receiving payments. |

---

## Tier 2a — DrOk Engagement (`drok-engagement/`)

All reference Tier 1 templates. Carry project-specific terms for the Martin / DrOk engagement.

| Document | Status | Needs Review By | Needs Signature By | Timing |
|---|---|---|---|---|
| Term Sheet (non-binding) | ✅ Draft | Erika → then Martin | Neither (non-binding) | Before Discovery call. Sent to Erika 2026-03-23. |
| NDA (mutual, DrOk-specific) | ✅ Draft | Erika | Mark + Martin | NDA-MUTUAL-DROK.md. Named parties, permitted disclosures for Carlos Rojas. |
| SOW — Discovery Phase | ✅ Draft | Erika | Mark + Martin | SOW-DISCOVERY.md. 8 deliverables, milestone payment. |
| Engagement Letter | ❌ Not created | Erika, Carlos Rojas | Mark + Martin | After Discovery, before billable work |
| DPA (Data Processing Agreement) | ✅ Draft | Erika, Carlos Rojas | Mark + Martin | DPA-DROK.md. Ley 29733, SCCs, ARCO rights, breach notification. Annex A (SCCs) pending Carlos. |
| Revenue Share Addendum | ✅ Draft | Erika, attorney review | Mark + Martin | Combined into EXCLUSIVITY-ADDENDUM.md §5. |
| Clinical Disclaimer Addendum | ✅ Draft | Carlos Rojas | Mark + Martin | CLINICAL-DISCLAIMER-ADDENDUM.md. AI limitations, physician responsibility, indemnification. |
| Exclusivity Addendum | ✅ Draft | Erika | Mark + Martin | EXCLUSIVITY-ADDENDUM.md. 24-mo exclusive, revenue share, min performance, ROFR. |
| BAA — Twilio | ❌ Not obtained | Mark (review terms) | Twilio standard | Before go-live |
| BAA — Anthropic | ❌ Not obtained | Mark (review terms) | Anthropic standard | Before go-live |
| W-8BEN (Martin) | ❌ Not obtained | — | Martin | Before first payment (either direction) |

**Signing order for DrOk:**
1. NDA (mutual) → before Discovery
2. Term Sheet shared → before Discovery
3. SOW (Discovery phase) → at Discovery
4. Engagement Letter + Revenue Share + Clinical Disclaimer + Exclusivity → after Discovery, before billable work
5. DPA → before any patient data
6. BAAs (vendor) → before go-live

---

## Tier 2b — Intern Engagement (`intern-engagement/`)

| Document | Status | Needs Review By | Needs Signature By | Timing |
|---|---|---|---|---|
| Internship Agreement (from template) | ✅ Updated | Erika (gut check) | Mark + Intern | Template updated with PHI, WCTC, access termination. Ready for use. |
| NDA (intern-specific) | ✅ Draft | Erika | Mark + Intern | NDA-INTERN.md. Portfolio exception, social media restrictions. |
| WCTC Training Agreement | ❌ Not obtained | Ellen Umentum | Mark + Intern + WCTC | Required by WCTC before start |
| Intern SOW / Scope Document | ❌ Not created | Mark | Mark + Intern | Before June 8, 2026 |
| PHI / Data Access Restriction | ✅ Built into template | Erika | Mark + Intern | Internship Agreement §6. No separate doc needed. |
| SMART Goals Document | ❌ Not created | Mark + Intern | Mark + Intern | First week of internship |

**Notes:**
- Hours: 72–144 total (min 72, max 144 per Ellen's Mar 19 update). 18 hrs/week max for unpaid.
- Duration: June 8 – July 31, 2026
- WCTC requires: Training Agreement, mid-term evaluation, final evaluation, instructor site visit
- Intern must NOT have access to PHI or real patient data — test data only

---

## Not Yet Needed (Track for Later)

| Document | When Needed |
|---|---|
| Certificate of Authority (other states) | If Learned Geek does business formally outside Wisconsin |
| Certificate of Good Standing | If a client or partner requests it |
| Equity/Investment Agreement | If angel round or co-investor scenario materializes |
| Privacy Policy (public-facing) | Before DrOk platform goes live to end users |
| Terms of Service (public-facing) | Before DrOk platform goes live to end users |
