# DrOk — Project Tracker / Seguimiento del Proyecto
## Shared Document — Mark, Martin, Carlos

**Last Updated / Última actualización:** March 27, 2026
**Purpose / Propósito:** Single source of truth for all open questions, decisions, and action items across all parties. Updated after each meeting.

---

## Decisions Made / Decisiones Tomadas

Decisions that are agreed and documented. These do not need further discussion unless new information changes them.

| # | Decision / Decisión | Date | Source |
|---|---|---|---|
| D001 | Martin (or his clinical entity) is the **data controller**; Learned Geek is the **data processor** | 2026-03-27 | Meeting — Carlos confirmed |
| D002 | A Data Processing Agreement (DPA) is required between Martin and Learned Geek | 2026-03-27 | Meeting — Carlos confirmed |
| D003 | ANPDP registration is Martin's responsibility as data controller | 2026-03-27 | Meeting — Carlos confirmed |
| D004 | All third-party providers (Anthropic, Twilio) must be declared as sub-processors | 2026-03-27 | Meeting — Carlos confirmed |
| D005 | One parent/guardian consent is sufficient in practice for minor's data | 2026-03-27 | Meeting — Carlos confirmed |
| D006 | Both parents retain authority to request changes or restrictions | 2026-03-27 | Meeting — Carlos confirmed |
| D007 | Medical records cannot be deleted — only blocked. Raw data must be preserved. | 2026-03-27 | Meeting — Martin confirmed (Peruvian law) |
| D008 | Data retention: 5 years electronic, 20 years physical. We follow electronic standard. | 2026-03-27 | Meeting — Martin confirmed |
| D009 | Cross-border data transfer requires **traceability** — audit trail of data flows | 2026-03-27 | Meeting — Martin & Carlos. ⚠️ Contradicts March 23 written analysis (SCCs required). Needs formal reconciliation. |
| D010 | The AI provides **advice only** — physician bears all clinical responsibility | 2026-03-27 | Meeting — all agreed |
| D011 | Physician VoBo (Visto Bueno) validates all AI output before clinical decisions | 2026-03-27 | Meeting — all agreed |
| D016 | Carlos **prefers proactive DIGEMID registration** — not just wait-and-see | 2026-03-27 | Meeting — Carlos (Whisper transcript). He is pursuing a clinic contact to confirm scope. |
| D017 | ANPDP is a fast, responsive authority; DIGEMID is slower — plan timelines accordingly | 2026-03-27 | Meeting — Carlos |
| D018 | Carlos sees himself as **part of the team**, not a passive reviewer. Three-way split: Martin (medical), Mark (technical), Carlos (legal). | 2026-03-27 | Meeting — Carlos: "Me gusta que seas cuidadoso con la data porque este negocio necesita esa óptica" |
| D019 | **MVP target: July 2026** — with user feedback before market launch | 2026-03-27 | Meeting — proposed by Carlos (not Martin). Martin defers to Mark on build timeline. |
| D012 | Anthropic AUP permits healthcare use with human-in-the-loop + AI disclosure | 2026-03-23 | Mark's vendor review |
| D013 | PubMed API + PAHO + physician-curated RAG as clinical knowledge base (not OpenEvidence) | 2026-03-07 | Architecture decision |
| D014 | Twilio + Claude API as orchestrator; ElevenLabs for TTS only | 2026-03-07 | Architecture decision |
| D015 | System must disclose AI involvement at start of every conversation (Anthropic requirement) | 2026-03-23 | Anthropic AUP |

---

## Open Questions / Preguntas Pendientes

Questions that need answers before we can proceed. Organized by owner.

### For Carlos Rojas (Legal)

| # | Question / Pregunta | Priority | Status | Notes |
|---|---|---|---|---|
| Q-C001 | **DIGEMID classification:** Is the AI system a medical device? Carlos **prefers proactive registration** and is pursuing a clinic contact to confirm scope. Not an MVP blocker but needs formal opinion. | High | 🟡 Carlos actively pursuing | Carlos asked Martin for a clinic contact to confirm whether they fall under DIGEMID scope |
| Q-C002 | **Ley 30421 (Telemedicine):** Carlos acknowledged this law exists with limitations on automated system notifications. He doesn't believe they fall under medical device classification, but needs a formal written position. | High | 🟡 Partially addressed in meeting | Whisper transcript captured Carlos's comments — missed by Otter |
| Q-C003 | **SCCs vs. traceability:** Martin's written analysis (March 23) said SCCs are required for Peru→US transfer. In the meeting, Carlos and Martin said only traceability is needed. Which is the formal position? | High | ❓ Needs reconciliation | These two positions contradict — must be resolved in writing before architecture decisions |
| Q-C004 | **RENHICE / Ley 30024:** Does DrOk constitute an electronic medical record (HCE)? Martin says no (Apple Watch analogy) but also acknowledged obligation to register as clinical history managers with Ministry of Health. These positions need clarification. | Medium | 🟡 Partially discussed — nuanced | Martin's position is more complex than initially captured |
| Q-C005 | **Indemnification clause:** Review language for physician-assumes-clinical-responsibility clause in engagement letter | Medium | 🔴 Not started | Needs to be reviewed before engagement letter |
| Q-C006 | **ARCO rights response timeframes:** What are the legal deadlines for responding to data access/rectification/cancellation requests? | Low | 🔴 Not started | Needed for Phase 4 build |
| Q-C007 | **Data breach notification:** What are ANPD notification requirements if a breach occurs? | Low | 🔴 Not started | Needed pre-launch |

### For Martin (Clinical / Business)

| # | Question / Pregunta | Priority | Status | Notes |
|---|---|---|---|---|
| Q-M001 | **Franchise restrictions:** Does the Infanzia/Kezer-Lab franchise agreement restrict AI tool deployment? | High | 🔴 Raised in meeting — no answer yet | Could affect scope |
| Q-M002 | **Budget range:** What is the general budget range for the project? | High | 🔴 Not discussed | Needed to prepare right-sized proposal |
| Q-M003 | **VoBo sampling rate:** What % of routine (non-urgent) cases should the physician review? 20%? 30%? | Medium | 🔴 Not discussed | Affects system design and physician workload |
| Q-M004 | **US market status:** Is there active US interest for Infanzia products? Does the chatbot need English + US support? | Medium | 🟡 Briefly mentioned (medical tourism angle) | Martin mentioned patients with physicians in both countries |
| Q-M005 | **Product documentation:** Product sheets, dosing tables, ingredient lists, FAQs for Infanzia products (Biomilk, Infabiotix, Infavit, etc.) | Medium | 🔴 Not started | Needed for product chatbot knowledge base |
| Q-M006 | **DrOk trademark:** Register "DrOk" as a brand/trademark | Medium | 🟡 Acknowledged — not yet done | Martin confirmed it's not trademarked |
| Q-M007 | **Emergency keyword list:** Define emergency keywords in parent-language Spanish (not medical terminology) | Medium | 🔴 Not started | Needed before go-live — clinical document, Martin must own it |
| Q-M008 | **Critical lab value thresholds:** Define numeric boundaries for emergency routing by age group | Medium | 🔴 Not started | Needed before go-live — clinical decision |
| Q-M009 | **Physician network:** 50-100 physicians mentioned for MVP testing — are they identified and in contact? | Low | 🟡 Partially answered | Martin said he can provide them; need to confirm readiness |
| Q-M010 | **DIGEMID clinic contact:** Carlos asked Martin for a contact at a specific clinic to help determine DIGEMID scope. | High | 🔴 Pending Martin | Raised by Carlos during meeting |

### For Mark (Technical / Business)

| # | Question / Pregunta | Priority | Status | Notes |
|---|---|---|---|---|
| Q-K001 | **Insurance:** Get bundled Tech E&O + Cyber quotes (TechInsurance, Insureon, Hiscox) | High | 🔴 In progress | Target: under $1,500/yr; must be bound before engagement letter |
| Q-K002 | **WhatsApp Business Policy:** Review Meta/WhatsApp health data restrictions | High | 🔴 Not started | Anthropic AUP resolved ✅; WhatsApp + Twilio still open |
| Q-K003 | **Twilio AUP:** Review Twilio acceptable use policy for medical use | High | 🔴 Not started | |
| Q-K004 | **DPA/BAA with Anthropic:** Check if Anthropic offers a Data Processing Agreement | Medium | 🔴 Not started | Required before production |
| Q-K005 | **DPA/BAA with Twilio:** Check if Twilio offers a DPA/BAA for health data | Medium | 🔴 Not started | Required before production |
| Q-K006 | **NCBI API key:** Register for PubMed API access | Low | 🔴 Not started | Free, 2 minutes — just needs to be done |
| Q-K007 | **WhatsApp Business API application:** Start approval process (1-4 week lead time) | Medium | 🔴 Not started | Start early to avoid bottleneck |
| Q-K008 | **Cloud hosting decision:** Azure, AWS, or other — affects SCC analysis | Medium | 🔴 Not started | Which US entity receives Peru data? |
| Q-K009 | **Engagement letter draft:** Create after Discovery, before billable work | High | 🔴 Not started | Depends on Q-M002 (budget), Q-C001 (DIGEMID), insurance |
| Q-K010 | **Share authenticity boundary document with Martin and Carlos** | High | 🔴 Committed in meeting | Promised during March 27 meeting |
| Q-K011 | **Set up Google Drive shared folder for project documents** | High | 🔴 Committed in meeting | Promised during March 27 meeting |
| Q-K012 | **Foreign entity registration:** Does Learned Geek need to register in Peru? | Medium | 🔴 Not started | Erika may advise |
| Q-K013 | **US tax implications:** International income reporting (FBAR / Form 8938) | Medium | 🔴 Not started | Accountant question |

---

---

## Parking Lot / Temas Pendientes

Items mentioned during the March 27 meeting but not fully discussed. Tracked here so they are not lost.

| # | Topic / Tema | Raised By | Notes |
|---|---|---|---|
| P001 | Ministry of Health registration as clinical history managers | Martin | Acknowledged obligation — separate from ANPDP registration. Needs clarification. |
| P002 | Data monetization: selling anonymized data to physicians, institutions | Martin | Long-term revenue model. Context for DIGEMID registration benefit. |
| P003 | Medical tourism use case — patients with physicians in both Peru and US | Martin | Cross-border medical records. Potential future workstream. |
| P004 | Carlos's sister is a geriatric doctor — potential future user/tester | Carlos | Mentioned in context of physician openness to technology. |
| P005 | Secure document sharing approach — Mark has privacy concerns about cloud docs | Mark + Carlos | Carlos suggested Drive; Mark raised data privacy concerns. Need to agree on approach. |
| P006 | Carlos called DrOk "the Uber of health" — market positioning language | Carlos | Useful for future pitch materials and branding discussions. |

---

## Action Items from March 27 Meeting

| # | Owner | Action | Target Date | Status |
|---|---|---|---|---|
| A001 | Mark | Send meeting summary via email | March 28 | 🔴 |
| A002 | Mark | Set up Google Drive shared folder with project documents | March 28 | 🔴 |
| A003 | Mark | Share authenticity boundary document | March 28 | 🔴 |
| A004 | Mark | Share technical references and AI research document | March 28 | 🔴 |
| A005 | Mark | Prepare updated proposal with scope and phases | Before next meeting | 🔴 |
| A006 | Carlos | Create WhatsApp group chat (+51 940 247 601) | This week | 🔴 |
| A007 | Carlos | Contact clinic re: DIGEMID scope determination | TBD | 🔴 |
| A008 | Martin | Provide Carlos with clinic contact for DIGEMID scope check | This week | 🔴 |
| A009 | Martin | Review and begin DrOk trademark registration | In progress | 🟡 |
| A010 | Martin | Review and respond to open questions (Q-M001 through Q-M005) | Before next meeting | 🔴 |
| A011 | Carlos | Provide formal opinion on DIGEMID classification (Q-C001) | Before next meeting | 🔴 |
| A012 | Carlos | Clarify SCC vs. traceability position (Q-C003) | Before next meeting | 🔴 |
| A013 | All | Schedule follow-up meeting | Next week (est.) | 🔴 |

---

## Documents Shared / Documentos Compartidos

| # | Document | Date Sent | Sent To | Channel |
|---|---|---|---|---|
| DOC001 | Pre-Discovery Action Items (EN + ES) | 2026-03-18 | Martin | WhatsApp + Email |
| DOC002 | Martin's Legal Analysis (260323 Proyecto DrOk) | 2026-03-19 | Mark, Carlos (CC) | Email |
| DOC003 | Meeting Summary — March 27 | Pending | Martin, Carlos | Email |
| DOC004 | Authenticity Boundary Document | Pending | Martin, Carlos | Google Drive |
| DOC005 | Technical Specification | Pending | Martin, Carlos | Google Drive |
| DOC006 | Term Sheet (non-binding) | Not yet — pending Erika review | Martin | TBD |

---

## Documents Pending Review (Internal — Not Shared)

These are drafted but awaiting legal review before sharing with Martin/Carlos.

| Document | Status | Reviewer | Notes |
|---|---|---|---|
| Term Sheet | Draft complete | Erika (sent 2026-03-23) | Non-binding; share after Erika's review |
| MSA (Master Services Agreement) | Draft complete | Erika | Updated with clinical disclaimer, DPA reference |
| NDA (Mutual) | Draft complete | Erika | Converted to mutual; permitted disclosures for Carlos |
| SOW — Discovery Phase | Draft complete | Erika | 8 deliverables, milestone payment |
| DPA (Data Processing Agreement) | Draft complete | Erika, then Carlos | Ley 29733, SCCs, ARCO rights |
| Clinical Disclaimer Addendum | Draft complete | Carlos | AI limitations, physician responsibility |
| Exclusivity Addendum | Draft complete | Erika | 24-mo exclusive, revenue share, ROFR |

---

## Timeline / Cronograma

| Phase | Target | Status |
|---|---|---|
| Phase 0 — POC / Proof of Concept | Complete | ✅ |
| Phase 1 — Discovery + Legal Alignment | Now — April 2026 | 🟡 In Progress |
| Phase 2 — Infanzia Product Chatbot | TBD (after Discovery) | 🔴 |
| Phase 3 — Physician Triage System Backend | TBD | 🔴 |
| Phase 4 — Physician Dashboard + Go-Live | Target: ~July 2026 (MVP) | 🔴 |
| Phase 5 — Multi-tenant / Physician Network | TBD | 🔴 |
| Phase 6 — International Expansion / DIGEMID Registration | Long-term | ⚪ Deferred |

---

## Meeting History / Historial de Reuniones

| Date | Attendees | Summary |
|---|---|---|
| 2026-03-07 | Mark, Martin | Initial meeting — project scope, architecture decisions |
| 2026-03-27 | Mark, Martin, Carlos, Karen | Legal alignment — data roles, consent, retention, DIGEMID, timeline |

---

*This is a shared working document. Updated after each meeting or significant communication.*
*Este es un documento de trabajo compartido. Se actualiza después de cada reunión o comunicación importante.*
