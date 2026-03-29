# Legal & Compliance Tracker — Proyecto DrOk
## Learned Geek — Internal Document

**Last updated:** March 23, 2026
**Purpose:** Authoritative tracking of every legal, regulatory, and compliance requirement. Each item maps to a project phase and has a clear owner and status. Zero gaps are acceptable — if we go to production with an open item, it is a conscious, documented decision, not an oversight.

**Sources:**
- Martin Núñez legal analysis — March 23, 2026 (see `docs/client/proposals/2026-03-22/260323-DrNunez-legal-responses-EN.md`)
- Internal compliance gap analysis — March 2026
- Carlos Rojas review pending (Rebaza, Alcázar & De Las Casas)

**Legal counsel available:**

| Name | Role | Firm | Coverage |
|---|---|---|---|
| Carlos Rojas | Technology / New Tech Specialist | Rebaza, Alcázar & De Las Casas (Lima) | DIGEMID, Ley 29733, Ley 30421, tech regulatory |
| Independent counsel (informal) | Corporate & International Business Law, LLM in International Business Law | Estudio Muñiz (Lima) | Engagement letter, DPA, cross-border contracts, international business — informal gut-check role |

Having both available is strong coverage: Carlos on the tech/regulatory side, independent counsel as informal review of contractual and cross-border business language.

---

## Status Legend

| Status | Meaning |
|---|---|
| 🔴 Open | Not started — blocks go-live if not resolved |
| 🟡 In Progress | Action underway |
| 🟢 Resolved | Confirmed closed with documentation |
| ⚪ Deferred | Consciously deferred to a later phase |
| ❓ Awaiting Counsel | Waiting on Carlos Rojas or other legal input |

---

## Part 1 — Peruvian Law (Ley 29733 + Related)

### 1.1 APDP / ANPD Registration

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L001 | Register data bank with ANPD before storing any patient data | Pre-Launch | Martin (responsible) / Learned Geek (encargado) | ❓ | Martin believes compliant; Carlos Rojas reviewing |
| L002 | Clarify registration owner — physician (responsable) vs. Learned Geek (encargado) | Discovery | Mark | ❓ | Awaiting Carlos Rojas |
| L003 | Notify ANPD of cross-border data transfer (Peru → US) when registering/updating data bank | Pre-Launch | Martin + Learned Geek | 🔴 | Required per Reglamento Ley 29733 |

### 1.2 Cross-Border Data Transfer (Peru → US)

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L004 | Execute Standard Contractual Clauses (SCCs) with Anthropic | Pre-Launch | Learned Geek | 🔴 | US has no adequacy determination; SCCs required |
| L005 | Execute Standard Contractual Clauses (SCCs) with Twilio | Pre-Launch | Learned Geek | 🔴 | Same basis as L004 |
| L006 | Verify existing DPA/SCC mechanisms with Anthropic (may already exist in ToS) | Discovery | Mark | 🔴 | Check Anthropic's DPA documentation |
| L007 | Verify existing DPA/SCC mechanisms with Twilio | Discovery | Mark | 🔴 | Check Twilio's DPA documentation |
| L008 | Transfer Impact Assessment (TIA) — evaluate whether US surveillance laws prevent Anthropic/Twilio from honoring protection clauses | Pre-Launch | Learned Geek + Carlos Rojas | 🔴 | Recommended by Martin's analysis; determines if SCCs are sufficient |

### 1.3 Consent — Minors and Parents

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L009 | Consent form: must be prior, informed, express, and in writing | Phase 3 Build | Learned Geek | 🔴 | Per Ley 29733 |
| L010 | For children under 14: consent from legal guardian only (not the child) | Phase 3 Build | Learned Geek | 🔴 | Must be captured and verified in onboarding flow |
| L011 | For children 14+: minor may consent in age-appropriate language; but cross-border requires parental consent regardless of age | Phase 3 Build | Learned Geek | 🔴 | Two-tier consent logic in UI |
| L012 | For cross-border transfers of sensitive pediatric data: require both parents (or custodial parent with sole custody) | Phase 3 Build | Learned Geek + Carlos Rojas | ❓ | Standard practice per Martin; confirm with Carlos Rojas |
| L013 | Consent must capture: parent/guardian identity, custody status verification process | Phase 3 Build | Learned Geek | 🔴 | Needs design spec in Discovery |
| L014 | Consent revocation must be supported (ARCO rights) | Phase 3 Build | Learned Geek | 🔴 | Revocation ≠ deletion (see L018) |

### 1.4 Data Retention

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L015 | Minimum 5-year retention from date of last treatment (NTS N° 139-MINSA/2018/DGAIN + Ley General de Salud) | Phase 3 Build | Learned Geek | 🔴 | Hard deletion must be blocked |
| L016 | Implement "blocking" mechanism — data retained for legal period but inaccessible except for medical emergency or legal purposes | Phase 3 Build | Learned Geek | 🔴 | `blocked_at` flag in patients/conversations tables |
| L017 | Define which data CAN be deleted on parent request vs. which is blocked (marketing data = deletable; medical records = blocked) | Phase 3 Build | Learned Geek + Martin | 🔴 | Requires data classification in schema design |

### 1.5 ARCO Rights (Acceso, Rectificación, Cancelación, Oposición)

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L018 | Admin portal must support ARCO request intake and processing | Phase 4 Build | Learned Geek | 🔴 | Acceso, Rectificación, Cancelación (blocked not deleted), Oposición |
| L019 | Response time to ARCO requests (check Reglamento for deadlines) | Discovery | Learned Geek + Carlos Rojas | ❓ | Typically 20 business days in similar frameworks |
| L020 | ARCO request audit trail — every request logged, timestamped, outcome recorded | Phase 4 Build | Learned Geek | 🔴 | Zero gaps for audit purposes |

---

## Part 2 — Ley N° 30024 / RENHICE (Electronic Medical Records)

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L021 | Understand current RENHICE implementation timeline and scope (public vs. private clinics) | Discovery | Martin + Learned Geek | 🔴 | Martin flagged this law — need to understand if DrOk falls under mandatory scope |
| L022 | Assess whether DrOk constitutes an "HCE" system subject to Ley 30024 | Discovery | Carlos Rojas | ❓ | Could impose interoperability and standardization requirements |
| L023 | If subject to Ley 30024: plan RENHICE integration as Phase 6+ item | Phase 6 | Learned Geek | ⚪ | Deferred — track for future |

---

## Part 3 — Medical Device Classification

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L024 | Determine whether the AI system qualifies as a medical device under DIGEMID regulations | Discovery | Martin + Carlos Rojas | ❓ | If classified as medical device: registration, clinical testing, approval process required — could be project-viability question |
| L025 | Determine whether the AI system qualifies as a medical device under US FDA (if US market is pursued) | Phase 6 | Learned Geek | ⚪ | Not urgent for Peru launch — revisit before US market |
| L026 | Review Ley N° 30421 (Telemedicine) applicability to WhatsApp-based triage | Discovery | Carlos Rojas | ❓ | May impose licensing or reporting requirements |

---

## Part 4 — Vendor Policies (Anthropic, Meta/WhatsApp, Twilio)

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L027 | Review Anthropic Acceptable Use Policy for medical/clinical restrictions | Immediate | Mark | 🔴 | Confirm no restrictions on clinical impression generation |
| L028 | Review Meta/WhatsApp Business Policy for health data handling restrictions | Immediate | Mark | 🔴 | Health data over WhatsApp may have specific restrictions |
| L029 | Review Twilio Acceptable Use Policy for medical use | Immediate | Mark | 🔴 | Twilio handles PHI in some configurations — understand requirements |
| L030 | Confirm Anthropic's BAA/DPA availability (Business Associate Agreement equivalent) | Discovery | Mark | 🔴 | If Anthropic processes PHI, a BAA or equivalent may be required |
| L031 | Confirm Twilio's BAA/DPA for health data | Discovery | Mark | 🔴 | Twilio offers HIPAA-eligible services — confirm Peru equivalence |

---

## Part 5 — Contractual / Engagement

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L032 | Engagement letter — signed before any billable work begins | Pre-Discovery | Mark | 🔴 | Must include: scope, IP ownership, liability cap, indemnification, impresión Dx. definition |
| L033 | Define "impresión diagnóstica" precisely in engagement letter — not a diagnosis, requires physician validation | Pre-Discovery | Mark + Martin | 🔴 | Martin's language: "substantiated and verifiable analysis" |
| L034 | Liability cap: Learned Geek's liability capped at engagement value | Pre-Discovery | Mark | 🔴 | Standard for software engagements; critical for medical-adjacent systems |
| L035 | Indemnification clause: Martin (as physician-of-record) indemnifies Learned Geek for clinical decisions | Pre-Discovery | Mark + Carlos Rojas | ❓ | Needs legal review |
| L036 | IP ownership: Learned Geek retains platform IP; Martin licenses per engagement | Pre-Discovery | Mark | 🔴 | Especially important given productization potential |
| L037 | Data Processing Agreement (DPA) between Learned Geek and Martin's practice | Pre-Launch | Learned Geek + Carlos Rojas | 🔴 | Formalizes encargado relationship |

---

## Part 6 — Learned Geek Business Compliance

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L038 | Get E&O (Errors & Omissions) insurance quotes — medical-adjacent software | Immediate | Mark | 🔴 | No current policy beyond LLC |
| L039 | Get professional liability insurance quotes | Immediate | Mark | 🔴 | Same |
| L040 | Get cyber liability insurance quotes — handling pediatric health data | Immediate | Mark | 🔴 | Especially relevant given PHI handling |
| L041 | Confirm Learned Geek LLC adequately protects Mark personally for this type of engagement | Immediate | Mark (attorney review) | 🔴 | Medical-adjacent + international adds risk |
| L042 | Assess whether Learned Geek needs to register as a foreign entity doing business in Peru | Discovery | Mark (attorney) | 🔴 | Receiving payment from Peru for services |
| L043 | Assess Wisconsin / US tax implications of Peruvian client revenue | Immediate | Mark (accountant) | 🔴 | International income may have specific reporting |

---

## Part 7 — Security & Infrastructure Compliance

| # | Requirement | Phase | Owner | Status | Notes |
|---|---|---|---|---|---|
| L044 | All PHI encrypted at rest and in transit | Phase 3 Build | Learned Geek | 🔴 | Standard; document compliance |
| L045 | Access control: physician data isolated by account; no cross-physician data access | Phase 3 Build | Learned Geek | 🔴 | Multi-tenant isolation |
| L046 | Admin portal access: MFA required for all admin accounts | Phase 4 Build | Learned Geek | 🔴 | |
| L047 | Security audit / penetration test before go-live | Pre-Launch | Learned Geek | 🔴 | External audit recommended for medical data |
| L048 | Incident response plan — data breach notification procedures for Peru (Ley 29733 + ANPD) | Pre-Launch | Learned Geek + Carlos Rojas | ❓ | Check ANPD notification timeline requirements |
| L049 | Data breach notification to patients — process and timeline | Pre-Launch | Learned Geek + Carlos Rojas | ❓ | |

---

## Phase Mapping Summary

| Phase | Legal Items | Critical Blockers |
|---|---|---|
| **Immediate (now)** | L027, L028, L029, L038, L039, L040, L041, L043 | Insurance + vendor policy review |
| **Pre-Discovery** | L032, L033, L034, L035, L036 | Engagement letter must be signed |
| **Discovery** | L001, L002, L006, L007, L013, L019, L021, L022, L024, L026, L030, L031, L042 | DIGEMID classification is a viability question |
| **Pre-Launch** | L003, L004, L005, L008, L037, L044–L049 | SCCs + ANPD notification + security audit |
| **Phase 3 Build** | L009–L017 | Consent capture + data blocking in schema |
| **Phase 4 Build** | L018, L019, L020 | ARCO rights in admin portal |
| **Phase 6+ / Deferred** | L023, L025 | RENHICE integration + US FDA if expanding |

---

## Open Items from Carlos Rojas (Pending)

Carlos Rojas (Rebaza, Alcázar & De Las Casas) has been engaged by Martin. Items awaiting his review:

- L001 — Overall Ley 29733 compliance confirmation
- L002 — Registration owner: physician vs. Learned Geek
- L008 — TIA assessment for US vendors
- L012 — Both-parents requirement for cross-border transfers
- L019 — ARCO response timeframes
- L022 — Ley 30024 / RENHICE scope
- L024 — DIGEMID medical device classification
- L026 — Ley 30421 telemedicine applicability
- L035 — Indemnification clause review
- L048, L049 — Breach notification requirements

---

*Learned Geek — Internal document. Not for client distribution.*
