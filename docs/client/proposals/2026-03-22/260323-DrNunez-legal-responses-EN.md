# Martin Núñez — Legal Analysis Responses (EN)
**Date:** March 23, 2026
**Source:** 260323 Proyecto DrOk - M. McArthey.docx
**Context:** Martin's annotated responses to the Pre-Discovery Action Items, focused on Peruvian legal compliance. Pending review by Carlos Rojas (Rebaza, Alcázar & De Las Casas).

---

## Overall Assessment

Martin reviewed the proposals against Ley 29733 and related regulations and believes **the proposal as designed would comply** with Peruvian law. All responses below are subject to Carlos Rojas' final review.

---

## Q1 — APDP Registration

**Martin's answer:** He summarized the framework and views compliance as achievable. He defers final ruling to Carlos Rojas.

**New law flagged — Ley N° 30024 (modified by Ley N° 31750):**
Regulates the Electronic Medical Record (Historia Clínica Electrónica / HCE) in Peru. Creates the **RENHICE** (Registro Nacional de Historias Clínicas Electrónicas) — a national infrastructure requiring all health establishments (public, private, mixed) to progressively implement standardized, interoperable electronic records.

Key implications:
- All health centers must share standardized data through RENHICE
- Patients (or legal representatives) can access their own records via DNI
- Records are protected against commercial use unrelated to health
- **Potential future requirement:** DrOk may need RENHICE integration/compliance as adoption progresses

---

## Q2 — Cross-Border Data Transfer (Peru → US)

**Martin's answer:** Based on his experience as a health provider, explicit consent is sufficient for international consultations/second opinions. He defers further confirmation to Carlos Rojas.

**Detailed legal analysis (Martin's own):**

Explicit consent alone is **NOT sufficient** for Peru → US data transfer. Reasons:

1. **No adequacy determination** — Peru's ANPD has not declared the US a country with adequate data protection. The US lacks a comprehensive federal privacy law (HIPAA is sectoral only).

2. **Required mechanisms (Reglamento de Ley 29733):**
   - **Standard Contractual Clauses (SCC):** Contract between Peruvian health center and US recipient obligating the US entity to maintain equivalent security/confidentiality standards
   - **ANPD Notification:** Cross-border transfer must be declared to the ANPD when registering or updating the data bank
   - **Transfer Impact Assessment (TIA):** Recommended evaluation confirming US surveillance laws don't prevent the recipient from honoring protection clauses

**Action required:** Learned Geek must sign SCCs with Anthropic and Twilio, or ensure those vendors have equivalent mechanisms in place. Confirm with Carlos Rojas.

---

## Q3 — Consent for Minors

**Martin's answer:** Based on health provider experience, procedures for minors (up to age 14) are authorized by the designated legal guardian (must be pre-verified). He defers to Carlos Rojas.

**Detailed legal analysis (Martin's own):**

Under Ley N° 29733 and Reglamento (D.S. N° 003-2013-JUS):

| Age | Who consents |
|---|---|
| Under 14 | Legal guardian required (parents with parental authority, or court-appointed guardian) |
| 14 and older | Minor can consent themselves if language is age-appropriate |

**Both parents required?**
- For routine medical acts: one parent presumed to act with the other's consent
- For **sensitive data** (pediatric health) and especially **cross-border transfers**: institutions typically require both parents, or the parent with exclusive legal custody

**Cross-border exception:** For transfers outside Peru, requiring parental authorization regardless of the minor's age is standard practice due to the elevated risk of sensitive data leaving national territory.

**Right to revoke:** Consent granted by legal representatives can be revoked at any time via the ARCO rights procedure.

---

## Q4 — Data Retention vs. Right to Erasure

**Martin's answer:** Records are national legal assets governed by NTS N° 139-MINSA/2018/DGAIN and Ley N° 30024. They can only be deleted after the legally required retention period: **5 years from the date of last treatment.**

**Key ruling — Retention prevails over erasure:**

- Ley N° 29733's right of erasure does **not apply** when:
  - A legal obligation to retain exists
  - Data is required for health care or public health interest

- **Minimum retention:** 5 years from last treatment date (Ley General de Salud + NTS N° 139-MINSA/2018/DGAIN)
- A physician or clinic **cannot** delete a pediatric medical record on parental request — doing so constitutes a serious administrative violation

**What CAN be deleted at parent's request:**
- Marketing/promotional data
- Newsletter subscriptions
- Outdated contact information

**Blocking mechanism:** If a parent requests deletion but the law prohibits it, the data must be "blocked":
- Physically/digitally retained to meet the legal retention period
- No further processing or viewing permitted except for strictly legal or medical emergency purposes

---

## Summary — Engineering Impact

| Finding | Impact on System Design |
|---|---|
| RENHICE compliance (Ley 30024) | Future integration requirement; track as Phase 3+ item |
| SCCs required for cross-border transfer | Legal agreement with Anthropic + Twilio before launch |
| ANPD notification required | Registration step before go-live |
| TIA recommended | Pre-launch assessment task |
| Parental consent for under-14s | Consent form must capture parent/guardian identity; verify custody status |
| Both parents for cross-border (sensitive data) | Consent form may need dual-parent signature for international data flow |
| 5-year retention minimum | Deletion is blocked, not executed; implement "block" state in data model |
| ARCO rights | Admin portal must support ARCO request processing |
