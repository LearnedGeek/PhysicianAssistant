# Meeting Summary — March 27, 2026
## DrOk Platform — Discovery Alignment Call

**Date:** 2026-03-27
**Duration:** ~35 minutes
**Attendees:** Mark McArthey (Learned Geek LLC), Dr. Martin Nunez (Physician / Client), Carlos Rojas Klauer (Legal Advisor — Innovation & Technology), Karen McArthey (Translation Support)
**Format:** Google Meet
**Recording:** 2 MP3 files, transcribed via Whisper large-v3

---

## 1. Decisions Made

| # | Decision | Owner | Notes |
|---|---|---|---|
| D001 | Martin (or his clinical entity) is the **data controller**; Learned Geek is the **data processor** | All | Carlos confirmed per Ley 29733 |
| D002 | A **Data Processing Agreement (DPA)** is required between Martin and Learned Geek | Mark + Carlos | Must define purposes, security, sub-processors, data return/destruction |
| D003 | ANPDP registration is Martin's obligation as data controller | Martin | Learned Geek declared as processor; Anthropic/Twilio as sub-processors |
| D004 | **One parent/guardian** consent is sufficient in practice for minor's data | Carlos | Both parents retain authority to request changes |
| D005 | Medical records **cannot be deleted** — only blocked; raw data must be preserved | All | Peruvian law; system must maintain full traceability |
| D006 | Data retention: **5 years electronic**, 20 years physical (electronic applies to us) | Martin | Per NTS N° 139-MINSA and Ley General de Salud |
| D007 | Cross-border data transfer requires **traceability** — audit trail of data flows | Martin + Carlos | See "What Changed" below |
| D008 | AI provides **advice only**; physician bears all clinical responsibility | All | VoBo validates all AI output |
| D009 | **DIGEMID classification is not an MVP blocker**, but voluntary registration has long-term strategic value | All | Carlos prefers to pursue registration proactively |

---

## 2. What Changed

Corrections or updates to previous assumptions based on meeting discussion and improved Whisper transcription.

| Previous Understanding | Updated Understanding | Source |
|---|---|---|
| SCCs required for Peru→US data transfer (Martin's written analysis, March 23) | Meeting position: only traceability required. **These positions need formal reconciliation.** | Carlos + Martin in meeting — contradicts written analysis |
| Both parents required for cross-border sensitive data (Martin's written analysis) | One parent sufficient in practice; both retain authority to request changes | Carlos in meeting |
| DIGEMID: gray area, wait and see | Carlos **prefers proactive registration** and asked Martin about a clinic contact to confirm scope. ANPDP is fast; DIGEMID is slower — plan accordingly. | Carlos in meeting (Whisper transcript) |
| Telemedicine law (Ley 30421) not discussed | Carlos addressed this: there IS a telemedicine law with limitations on automated notifications, but he doesn't believe they fall under medical device classification. Still needs formal opinion. | Carlos in meeting (Whisper transcript — missed by Otter) |
| Martin's Apple Watch analogy = not a clinical record system | Martin also acknowledged they need to "register as agents who control, register, and store clinical history" with the Ministry of Health. Position is more nuanced than a simple "not regulated." | Martin in meeting (Whisper transcript — missed by Otter) |
| MVP timeline unknown | **July 2026** proposed by Carlos — with user feedback before market launch | Carlos in meeting (Whisper transcript — attribution corrected) |

---

## 3. Summary of Discussion

### ANPDP Registration & Data Roles
Carlos confirmed the data controller/processor split clearly. Martin is the controller (determines purpose of data processing); Learned Geek is the processor (provides technology). A DPA is required. All sub-processors (Anthropic, Twilio) must be declared in the ANPDP registration. Carlos noted the ANPDP is a responsive authority that works efficiently.

### Cross-Border Data Transfer
Martin and Carlos stated that Peru requires traceability of data flows but does not currently have specific blocking restrictions on Peru→US transfer. Mark noted that cloud infrastructure options exist in Latin America (Azure Brazil) if data residency becomes a concern. Mark confirmed he has already reviewed Anthropic and Twilio AUPs for healthcare use — no issues. **However:** this position is simpler than Martin's own written analysis from March 23, which stated SCCs are required. This contradiction must be resolved formally.

### Consent for Minors
Carlos confirmed one parent is sufficient in practice. Express consent (consentimiento expreso) is required given sensitive health data. Both parents retain authority to request changes or restrictions. Carlos suggested the consent mechanism should be digital and captured within the platform — either through a click/confirmation flow with identity verification.

### Data Retention & Immutability
Medical records cannot be deleted under Peruvian law. The system must preserve raw data without modification. Martin emphasized traceability as the core requirement. Retention is 5 years for electronic records. Martin's position is that the platform is not a formal clinical record system (HCE), but he also acknowledged a registration obligation with the Ministry of Health for managing clinical histories — a more nuanced position than the Apple Watch analogy alone suggests.

### Medical Device Classification (DIGEMID)
This was the most nuanced discussion. Martin maintains the system provides advice, not diagnosis. Carlos acknowledged the gray area but said he **prefers to pursue registration proactively** — he asked Martin about a contact at a specific clinic to help determine scope. Carlos noted that DIGEMID is a slower authority than ANPDP, which has timeline implications. Carlos also addressed the telemedicine law (Ley 30421): there are limitations on automated system notifications, but he doesn't believe this constitutes a medical device. Not a blocker for MVP, but Carlos wants clarity.

### DrOk Brand & Trademark
"DrOk" is not trademarked. Martin acknowledged this needs to be done. Carlos framed DrOk as "the Uber of health" — he's already thinking about market positioning. Franchise restrictions from Infanzia/Kezer-Lab still need to be reviewed.

### Timeline & Scope
No hard external deadline. Carlos proposed **MVP by July 2026** with user testing and feedback before market launch. He specifically asked about UX/UI design, user trust in AI channels, and how long the technical build would take. Martin said he can provide 50-100 physicians for MVP testing. Martin referenced physicians adopting AI since COVID (especially diagnostic imaging) and existing platforms (PubMed, OpenEvidence with Cleveland Clinic / Mayo Clinic) as market validation.

### AI Authenticity & Safety
Mark described his 6-month research on AI confabulation and the PubMed citation-based approach. Carlos independently raised AI authenticity as a critical concern — referencing specific incidents in Peru of youth suicide linked to AI conversations. Carlos used the word "veracidad" (truthfulness/veracity) to describe the concept. Both Carlos and Martin want to see the authenticity boundary document. Mark committed to sharing it.

### Team Dynamics
Carlos explicitly validated the three-way expertise split: Martin (medical perspective), Mark (technical perspective, data privacy awareness), Carlos (legal perspective). He said: "Me gusta que seas cuidadoso con la data porque este negocio necesita esa óptica." He sees himself as part of the team, not a passive reviewer. Martin reinforced the worldwide vision. Mark echoed: "This is a problem many are trying to solve, but no one has solved it yet. Why not us?"

---

## 4. Action Items

| # | Owner | Action | Due Date | Status |
|---|---|---|---|---|
| A001 | Mark | Send meeting summary via email | March 28 | 🔴 |
| A002 | Mark | Set up Google Drive shared folder with project documents | March 28 | 🔴 |
| A003 | Mark | Share authenticity boundary document | March 28 | 🔴 |
| A004 | Mark | Share technical references and AI research | March 28 | 🔴 |
| A005 | Mark | Prepare updated proposal with scope and phases | Before next meeting | 🔴 |
| A006 | Carlos | Create WhatsApp group chat (+51 940 247 601) | This week | 🔴 |
| A007 | Carlos | Contact clinic re: DIGEMID scope determination | TBD | 🔴 |
| A008 | Martin | Review and begin DrOk trademark registration | In progress | 🟡 |
| A009 | Martin | Review and respond to open questions (franchise, budget, VoBo rate, US market, product docs) | Before next meeting | 🔴 |
| A010 | All | Schedule follow-up meeting | Next week (est.) | 🔴 |

---

## 5. Outstanding Questions

### For Carlos (Legal)

| # | Question | Priority | Status |
|---|---|---|---|
| Q-C001 | **DIGEMID classification:** Formal opinion needed. Carlos prefers proactive registration; needs clinic contact to confirm scope. | High | 🟡 Carlos pursuing |
| Q-C002 | **Ley 30421 (Telemedicine):** Carlos acknowledged the law exists with limitations on automated notifications. Formal written position needed. | High | 🟡 Partially addressed |
| Q-C003 | **SCC vs. traceability:** Written analysis (March 23) says SCCs required; meeting says traceability only. Which is the formal position? | High | ❓ Needs reconciliation |
| Q-C004 | **RENHICE / Ley 30024:** Does the system constitute an HCE? Martin says no, but acknowledged clinical history registration obligation. | Medium | 🟡 Partially addressed |
| Q-C005 | **Indemnification clause:** Review clinical responsibility language for engagement letter. | Medium | 🔴 Not started |

### For Martin (Clinical / Business)

| # | Question | Priority | Status |
|---|---|---|---|
| Q-M001 | **Franchise restrictions:** Infanzia/Kezer-Lab agreement — AI tool deployment restrictions? | High | 🔴 Raised — no answer |
| Q-M002 | **Budget range:** General budget range for the project. | High | 🔴 Not discussed |
| Q-M003 | **VoBo sampling rate:** % of routine cases for physician review. | Medium | 🔴 Not discussed |
| Q-M004 | **US market status:** Active interest? English support needed? Medical tourism angle discussed briefly. | Medium | 🟡 Briefly mentioned |
| Q-M005 | **Product documentation:** Product sheets, dosing tables, ingredients for chatbot knowledge base. | Medium | 🔴 Not discussed |
| Q-M006 | **DrOk trademark:** Begin registration. | Medium | 🟡 Acknowledged |
| Q-M007 | **DIGEMID clinic contact:** Carlos asked Martin for a contact at a specific clinic to confirm scope. | High | 🔴 Pending Martin |

---

## 6. Parking Lot

Items mentioned but not fully discussed. Tracked here so they are not lost.

| # | Topic | Raised By | Notes |
|---|---|---|---|
| P001 | Ministry of Health registration as clinical history managers | Martin | Acknowledged obligation but said "not in that specific point of the law." Needs clarification — separate from ANPDP registration. |
| P002 | Data selling / monetization to physicians and institutions | Martin | Long-term revenue model: patients pay, physicians pay, institutions pay. Mentioned in context of DIGEMID registration benefit. |
| P003 | Medical tourism use case | Martin | Patients with physicians in both Peru and US; cross-border medical records. Potential Workstream C. |
| P004 | Carlos's sister is a geriatric doctor | Carlos | Mentioned in context of physician openness to technology. Potential future user/tester. |
| P005 | Shared Drive for ongoing collaboration | Carlos | Carlos suggested using Drive to track research answers and shape contracts. Mark raised privacy concerns about cloud docs. |

---

## 7. Documents

### Shared During Meeting
| Document | Shared By | Notes |
|---|---|---|
| Pre-Discovery Action Items (EN + ES) | Mark (previously sent) | Carlos reviewed; basis for discussion |
| Martin's Legal Analysis (260323 Proyecto DrOk) | Martin (previously sent) | Referenced during cross-border and consent discussions |

### Promised — Not Yet Shared
| Document | Owner | Target Date |
|---|---|---|
| Meeting summary (this document) | Mark | March 28 |
| AI authenticity boundary document | Mark | March 28 |
| Technical specification | Mark | March 28 |
| Project tracker (shared) | Mark | March 28 |
| Carlos's written notes from meeting | Carlos | Sent to Martin during meeting; Mark needs copy |

---

## 8. Next Meeting

**Tentative date:** Week of March 30 – April 3
**Proposed agenda:**
1. Review action items from this meeting
2. Carlos: formal position on DIGEMID classification and Ley 30421
3. Carlos: reconcile SCC vs. traceability position for cross-border transfer
4. Martin: franchise restrictions, budget range, timeline confirmation
5. Mark: walk through authenticity boundary document
6. Mark: present scope and phases proposal
7. Discuss Google Drive / secure document sharing approach

---

*Prepared by Mark McArthey, Learned Geek LLC*
*Source: Google Meet recording, transcribed via Whisper large-v3 + meeting notes*
*Transcript quality note: Whisper large-v3 handled Spanish/English code-switching significantly better than Otter.ai. All quotes and attributions verified against Whisper transcript.*
