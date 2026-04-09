# DrOk — Phase Tracker & Cost Estimates
## Internal Document — Learned Geek LLC

**Last Updated:** March 31, 2026
**Purpose:** Master tracking document for all phases — costs, hours, tasks, and status. Internal only — the client-facing version is a subset of this.

---

## Cost Summary — All Phases

| Phase | Mark Hours | Mark Time Value | Mark Cash Costs | Martin Cash | Martin Hours | Status |
|---|---|---|---|---|---|---|
| Pre-project (sunk) | 350 | $52,500 | $0 | $0 | 40 | ✅ Complete |
| Phase 1 — Discovery | 60 | $9,000 | $500 | $2,500 | 20 | 🔴 Not started |
| Phase 2 — Product Chatbot | 80 | $12,000 | $600 | $2,500 | 15 | 🔴 Not started |
| Phase 3 — Physician System | 120 | $18,000 | $900 | $2,500 | 30 | 🔴 Not started |
| Phase 4 — Dashboard + Go-Live | 80 | $12,000 | $700 | $2,500 | 40 | 🔴 Not started |
| Post-launch (Year 1) | 520 | $78,000 | $5,300 | Shared per equity | Ongoing | 🔴 Future |
| **Totals (through go-live)** | **690** | **$103,500** | **$2,700** | **$10,000** | **145** | |
| **Totals (Year 1 all-in)** | **1,210** | **$181,500** | **$8,000** | **$10,000 + shared** | | |

---

## Pre-Project — Sunk Investment (Complete)

Already invested before any agreement. This is Mark's in-kind contribution to the partnership.

| Task | Hours | Value | Status |
|---|---|---|---|
| ANI confabulation research (6 months) | 200 | $30,000 | ✅ |
| POC build + architecture design | 80 | $12,000 | ✅ |
| Legal document drafting (term sheet, MSA, NDA, DPA, SOW, etc.) | 40 | $6,000 | ✅ |
| Client communication, meetings, translation | 30 | $4,500 | ✅ |
| **Total** | **350** | **$52,500** | ✅ |

---

## Phase 1 — Discovery (4 weeks)

**Trigger:** Martin signs engagement + pays $2,500
**Deliverable:** Complete requirements spec, legal framework confirmed, architecture finalized

### Mark's Tasks

| # | Task | Est. Hours | Status | Notes |
|---|---|---|---|---|
| 1.1 | Finalize functional requirements with Martin | 8 | 🔴 | |
| 1.2 | Map functional requirements → technical requirements | 8 | 🔴 | |
| 1.3 | Finalize system architecture document | 6 | 🔴 | |
| 1.4 | Set up Azure resource group + dev environment | 4 | 🔴 | See azure-deployment-plan.md |
| 1.5 | Set up GitHub Project board with milestones + issues | 4 | 🔴 | |
| 1.6 | Bind insurance (E&O + Cyber) | 2 | 🔴 | TechInsurance call pending |
| 1.7 | Review Twilio WhatsApp Business Policy | 2 | 🔴 | |
| 1.8 | Review/execute Twilio DPA | 2 | 🔴 | |
| 1.9 | Review/execute Anthropic DPA | 2 | 🔴 | |
| 1.10 | Register NCBI API key (PubMed) | 0.5 | 🔴 | Free, 2 minutes |
| 1.11 | Start WhatsApp Business API application | 2 | 🔴 | 1-4 week lead time |
| 1.12 | Draft consent flow UX for minors | 4 | 🔴 | |
| 1.13 | Intern onboarding prep (Hannah) | 8 | 🔴 | Before June 8 |
| 1.14 | Data model design (patients, conversations, triage, VoBo) | 8 | 🔴 | |
| | **Subtotal** | **60** | | |

### Mark's Cash Costs — Phase 1

| Item | Cost |
|---|---|
| Insurance (first month) | $125 |
| Azure dev environment | $50 |
| Anthropic API (testing) | $50 |
| Twilio (sandbox) | $20 |
| Domain/DNS | $15 |
| Misc | $40 |
| **Subtotal** | **~$300** |
| Pro-rated annual insurance | +$200 |
| **Phase 1 total** | **~$500** |

### Martin's Tasks — Phase 1

| # | Task | Est. Hours | Status | Notes |
|---|---|---|---|---|
| M1.1 | Pay Phase 1 investment ($2,500) | — | 🔴 | Trigger for phase start |
| M1.2 | Review and approve requirements spec | 4 | 🔴 | |
| M1.3 | Provide product documentation (Infanzia catalog) | 4 | 🔴 | Datasheets, dosing, ingredients |
| M1.4 | Define emergency keyword list (parent-language Spanish) | 3 | 🔴 | Clinical document — Martin owns |
| M1.5 | Define VoBo sampling rate | 1 | 🔴 | |
| M1.6 | Confirm franchise restrictions (Infanzia/Kezer-Lab) | 2 | 🔴 | Still unanswered |
| M1.7 | Begin DrOk trademark registration | 2 | 🔴 | |
| M1.8 | Carlos: DIGEMID formal opinion | 4 | 🔴 | Carlos's deliverable |
| | **Subtotal** | **~20** | | |

### Outstanding Questions — Phase 1

| # | Question | Owner | Status |
|---|---|---|---|
| Q1 | DIGEMID classification — formal opinion | Carlos | 🟡 Pursuing |
| Q2 | Ley 30421 telemedicine — formal position | Carlos | 🟡 Partially addressed |
| Q3 | SCC vs. traceability — reconcile positions | Carlos | ❓ Needs reconciliation |
| Q4 | Franchise restrictions | Martin | 🔴 Unanswered |
| Q5 | WhatsApp Business API health data policy | Mark | 🔴 Not started |
| Q6 | Anthropic DPA availability | Mark | 🔴 Not started |
| Q7 | Twilio DPA/BAA scope for Peru | Mark | 🔴 Not started |
| Q8 | US tax accountant — FBAR/8938 implications | Mark | 🔴 Not started |
| Q9 | Peru tax/IP lawyer consultation (via Erika) | Erika | 🔴 Offered, not scheduled |

---

## Phase 2 — Product Chatbot (4–5 weeks)

**Trigger:** Phase 1 complete + Martin pays $2,500
**Deliverable:** Infanzia product chatbot live on WhatsApp (text-based)

### Mark's Tasks

| # | Task | Est. Hours | Status | Notes |
|---|---|---|---|---|
| 2.1 | Build product knowledge base from Martin's documentation | 12 | 🔴 | Depends on M1.3 |
| 2.2 | Implement product chatbot conversation flow | 16 | 🔴 | |
| 2.3 | Build admin interface for product doc management | 12 | 🔴 | Blazor — Martin can update docs |
| 2.4 | WhatsApp Business API integration (production) | 8 | 🔴 | Depends on 1.11 approval |
| 2.5 | Deploy to Azure (production environment) | 4 | 🔴 | |
| 2.6 | Testing + bug fixes | 8 | 🔴 | |
| 2.7 | Intern task assignment + mentoring | 12 | 🔴 | Hannah starts June 8 |
| 2.8 | Documentation | 4 | 🔴 | |
| 2.9 | AI disclosure message implementation (Anthropic requirement) | 4 | 🔴 | First message every session |
| | **Subtotal** | **80** | | |

### Mark's Cash Costs — Phase 2

| Item | Cost |
|---|---|
| Azure (production) | $100 |
| Anthropic API | $100 |
| Twilio (WhatsApp production) | $100 |
| Insurance | $125 |
| Misc | $75 |
| **Phase 2 total** | **~$500** (+$100 from Phase 1 ongoing) |

### Martin's Tasks — Phase 2

| # | Task | Est. Hours | Status | Notes |
|---|---|---|---|---|
| M2.1 | Pay Phase 2 investment ($2,500) | — | 🔴 | |
| M2.2 | Provide complete product documentation | 6 | 🔴 | Catalogs, FAQs, dosing tables |
| M2.3 | Review and test chatbot responses | 4 | 🔴 | |
| M2.4 | Provide WhatsApp Business number for production | 1 | 🔴 | |
| M2.5 | Begin physician outreach for pilot | 4 | 🔴 | Pre-selling to network |
| | **Subtotal** | **~15** | | |

---

## Phase 3 — Physician Triage System (6–8 weeks)

**Trigger:** Phase 2 complete + Martin pays $2,500
**Deliverable:** Full triage system backend — PubMed RAG, conversation management, emergency detection, VoBo queue

### Mark's Tasks

| # | Task | Est. Hours | Status | Notes |
|---|---|---|---|---|
| 3.1 | Implement Gate 1 — evidence retrieval + confidence scoring | 16 | 🔴 | ANI findings applied |
| 3.2 | Implement citation enforcement + null response path | 12 | 🔴 | |
| 3.3 | Implement emergency detection (deterministic, not LLM) | 8 | 🔴 | Martin's keyword list required |
| 3.4 | Build conversation management (multi-physician, multi-patient) | 12 | 🔴 | Migrate from file-based to PostgreSQL |
| 3.4b | Implement identity persistence (patient/guardian) | 8 | 🔴 | Per ANI findings: dedicated table, not memory. Guardian ≠ patient. Audit log for changes. See ANI-answer-identity-persistence.md |
| 3.5 | Build VoBo queue + physician notification (SMS alert) | 10 | 🔴 | |
| 3.6 | Implement consent capture flow | 8 | 🔴 | Per Ley 29733 requirements |
| 3.7 | Implement data blocking mechanism (ARCO rights) | 6 | 🔴 | |
| 3.8 | Build audit trail / traceability logging | 8 | 🔴 | Immutable event log |
| 3.9 | PostgreSQL schema + EF Core migrations | 8 | 🔴 | |
| 3.10 | Azure PostgreSQL provisioning | 4 | 🔴 | |
| 3.10b | Encryption Layer 1 — TLS 1.2+ for all transit (HTTPS, inter-service, webhooks) | 2 | 🔴 | HSTS enforced, cert management via Let's Encrypt or Azure |
| 3.10c | Encryption Layer 2a — Storage-level encryption (TDE + Azure SSE) | 2 | 🔴 | AES-256 at rest on PostgreSQL, blob storage, backups, disks. Keys in Azure Key Vault |
| 3.10d | Encryption Layer 2b — Application-level field encryption (IEncryptionService) | 8 | 🔴 | AES-256-GCM on child_name, parent_name, parent_phone, transcript, symptoms. Envelope encryption with per-tenant DEKs. 90-day key rotation. See implementation-plan.md §4a |
| 3.10e | Encryption verification — confirm dual-layer coverage + HIPAA safe harbor compliance | 2 | 🔴 | Verify: no plaintext PII at rest, no unencrypted transit, no sensitive fields in logs |
| 3.11 | Testing + security review | 16 | 🔴 | Includes encryption layer verification |
| 3.12 | Intern tasks (documentation, testing, UI components) | 12 | 🔴 | Hannah |
| | **Subtotal** | **120** | | |

### Mark's Cash Costs — Phase 3

| Item | Cost |
|---|---|
| Azure (App Service + PostgreSQL) | $200 |
| Anthropic API (heavier usage) | $150 |
| Twilio | $150 |
| Insurance | $125 |
| Misc | $75 |
| **Phase 3 total** | **~$700** (+$200 from prior ongoing) |

### Martin's Tasks — Phase 3

| # | Task | Est. Hours | Status | Notes |
|---|---|---|---|---|
| M3.1 | Pay Phase 3 investment ($2,500) | — | 🔴 | |
| M3.2 | Finalize emergency keyword list | 4 | 🔴 | Must be signed off |
| M3.3 | Define critical lab value thresholds by age group | 6 | 🔴 | Clinical decisions |
| M3.4 | ANPDP data bank registration | 4 | 🔴 | Martin's obligation as controller |
| M3.5 | Carlos: DPA review + SCC guidance | 4 | 🔴 | |
| M3.6 | Physician onboarding plan (first 10) | 8 | 🔴 | |
| M3.7 | DrOk trademark — confirm filed | 2 | 🔴 | |
| M3.8 | Determine pricing for physicians | 2 | 🔴 | Martin sets the market price |
| | **Subtotal** | **~30** | | |

---

## Phase 4 — Dashboard + Go-Live (4–5 weeks)

**Trigger:** Phase 3 complete + Martin pays $2,500
**Deliverable:** Physician dashboard (Blazor), UAT sign-off, production launch

### Mark's Tasks

| # | Task | Est. Hours | Status | Notes |
|---|---|---|---|---|
| 4.1 | Build physician dashboard (Blazor Server) | 24 | 🔴 | Priority queue, VoBo workflow, on/off toggle |
| 4.2 | Build admin portal (Learned Geek — system monitoring) | 12 | 🔴 | Audit logs, error tracking, conversation replay |
| 4.3 | Implement physician override tracking | 6 | 🔴 | Accuracy signal for Gate 1 tuning |
| 4.4 | Production deployment + security hardening | 8 | 🔴 | |
| 4.5 | UAT support — run scenarios with Martin | 8 | 🔴 | |
| 4.6 | Performance testing + load simulation | 4 | 🔴 | |
| 4.7 | Production monitoring setup (Application Insights) | 4 | 🔴 | |
| 4.8 | Go-live checklist execution | 4 | 🔴 | |
| 4.9 | Documentation — operations manual | 6 | 🔴 | |
| 4.10 | Intern final evaluation + WCTC paperwork | 4 | 🔴 | Hannah — due by July 31 |
| | **Subtotal** | **80** | | |

### Mark's Cash Costs — Phase 4

| Item | Cost |
|---|---|
| Azure (production — scaled) | $200 |
| Anthropic API | $150 |
| Twilio (production WhatsApp) | $150 |
| Insurance | $125 |
| Misc | $75 |
| **Phase 4 total** | **~$700** |

### Martin's Tasks — Phase 4

| # | Task | Est. Hours | Status | Notes |
|---|---|---|---|---|
| M4.1 | Pay Phase 4 investment ($2,500) | — | 🔴 | |
| M4.2 | UAT testing — realistic scenarios including emergencies | 12 | 🔴 | Must sign off before go-live |
| M4.3 | Formal sign-off on emergency detection logic | 2 | 🔴 | Documented, on file |
| M4.4 | Formal sign-off on AI accuracy (override rate gate) | 2 | 🔴 | <20% override rate required |
| M4.5 | Onboard first 5 pilot physicians | 12 | 🔴 | |
| M4.6 | Physician training materials | 8 | 🔴 | How to use dashboard, VoBo process |
| M4.7 | Go-live announcement to physician network | 4 | 🔴 | |
| | **Subtotal** | **~40** | | |

---

## Post-Launch — Year 1 (Ongoing)

### Mark's Ongoing Costs (Monthly)

| Item | Monthly | Annual |
|---|---|---|
| Azure hosting | $100-200 | $1,200-2,400 |
| Anthropic API | $100-200 | $1,200-2,400 |
| Twilio (WhatsApp) | $50-150 | $600-1,800 |
| Insurance | $125 | $1,500 |
| Misc | $50 | $600 |
| **Total** | **$425-725** | **$5,100-8,700** |

### Mark's Ongoing Time (Weekly)

| Task | Hours/Week |
|---|---|
| Platform maintenance + bug fixes | 4-6 |
| Security monitoring + updates | 2 |
| Feature development | 4-6 |
| Martin/physician support | 2 |
| **Total** | **~10-15 hrs/week** |

### Martin's Ongoing (Weekly)

| Task | Hours/Week |
|---|---|
| Physician sales + onboarding | 4-6 |
| VoBo reviews + clinical oversight | 2-4 |
| Physician support | 2 |
| Regulatory compliance | 1 |
| **Total** | **~10-15 hrs/week** |

### Revenue Projections (Post-Launch)

See `drok-strategy.md` for full scenarios. Summary at $100/physician:

| Physicians | Monthly Revenue | Platform Costs | Net Available | Mark (50%) | Martin (50%) |
|---|---|---|---|---|---|
| 10 | $1,000 | $500 | $500 | $250 | $250 |
| 25 | $2,500 | $575 | $1,925 | $963 | $963 |
| 50 | $5,000 | $650 | $4,350 | $2,175 | $2,175 |
| 100 | $10,000 | $800 | $9,200 | $4,600 | $4,600 |
| 500 | $50,000 | $2,500 | $47,500 | $23,750 | $23,750 |

*Note: Equity updated to 50/50 per March 30, 2026 counter-proposal. Platform operating costs deducted before split.*

**Break-even for Mark (cash costs only):** ~8 physicians at $100/month (covers ~$500/mo platform costs)
**Break-even for Martin ($10K investment):** ~25 physicians for ~4 months

---

## Master Outstanding Questions

Consolidated from all meetings and documents. Updated as answers arrive.

| # | Question | Owner | Phase | Status |
|---|---|---|---|---|
| Q1 | DIGEMID classification | Carlos | 1 | 🟡 |
| Q2 | Ley 30421 telemedicine applicability | Carlos | 1 | 🟡 |
| Q3 | SCC vs. traceability — reconcile | Carlos | 1 | ❓ |
| Q4 | Franchise restrictions (Infanzia/Kezer-Lab) | Martin | 1 | 🔴 |
| Q5 | WhatsApp Business health data policy | Mark | 1 | 🔴 |
| Q6 | Anthropic DPA | Mark | 1 | 🔴 |
| Q7 | Twilio DPA/BAA | Mark | 1 | 🔴 |
| Q8 | US tax accountant consultation | Mark | 1 | 🔴 |
| Q9 | Peru tax/IP lawyer (via Erika) | Erika | 1 | 🔴 |
| Q10 | Martin's budget confirmation | Martin | 1 | 🔴 → Term sheet answers this |
| Q11 | VoBo sampling rate | Martin | 1 | 🔴 |
| Q12 | Product documentation | Martin | 2 | 🔴 |
| Q13 | Emergency keyword list | Martin | 3 | 🔴 |
| Q14 | Critical lab value thresholds | Martin | 3 | 🔴 |
| Q15 | Physician pricing ($/month) | Martin | 3 | 🔴 |
| Q16 | ANPDP registration | Martin | 3 | 🔴 |
| Q17 | DrOk trademark filed | Martin | 1 | 🟡 |
| Q18 | Indecopi software registration (platform IP) | Mark | 2 | 🔴 |
| Q19 | Cloud hosting region decision (US vs. Latin America) | Mark | 1 | 🔴 |
| Q20 | WhatsApp Business API approval | Mark | 2 | 🔴 |

---

*Internal document — not for client distribution.*
*Updated March 31, 2026.*
