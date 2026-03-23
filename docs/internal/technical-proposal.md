# Infanzia / Kezer-Lab — Internal Technical Proposal
## Learned Geek — Working Document

**Prepared by:** Mark McArthey  
**Date:** March 17, 2026 (rev 3 — Martin's legal analysis incorporated, 2026-03-23)
**Status:** Internal only — do not share with client  
**Purpose:** Full technical reference, architecture decisions, resolved questions, risk assessment, and pricing framework for the Infanzia engagement

---

## Project Summary

Dr. Martin Núñez is a Peruvian pediatrician and sole operator of an Infanzia® / Kezer-Lab franchise — a precision infant nutrition brand with EU-certified products manufactured in Germany. He was introduced through Karen's personal network and is a fully commercial engagement.

The project has two workstreams of very different complexity:

- **Workstream A — Infanzia Product Chatbot:** A WhatsApp AI assistant for the product line. Answers parent questions about Biomilk, Infabiotix, Infavit, and other products. Lower complexity, faster delivery, earlier revenue.
- **Workstream B — Physician After-Hours Patient Communication System:** An AI assistant that operates on behalf of pediatricians during off-hours. Parents contact via WhatsApp, the AI captures symptoms, assesses urgency, keeps parents calm, and queues everything for physician review. Complex, medically sensitive, potentially white-labelable across Latin America.

Martin was at Expo West (Anaheim) when he sent the process diagram — he has active US market interest, not just aspirational.

**Status as of March 17:** Martin has responded to all six scope questions in writing. Answers are detailed, clinically precise, and reveal a more sophisticated vision than initially scoped. Key implications are documented below.

---

## Martin's Responses — Full Analysis

Martin submitted written answers to all six questions on March 17. His responses are summarized and analyzed here. The original document is on file.

---

### Q1 — Images: Analyze or Forward?

**Martin's answer:** He distinguishes three distinct image categories, each with different handling:

**Category A — Radiology (X-ray, CT, MRI, ultrasound)**
- AI analysis depends on more than one image; requires direct integration with HIS/RIS/PACS systems
- Notes that today's AI is often more accurate than specialist physicians in radiology interpretation
- His recommendation: defer to a future phase
- **Decision: OUT of current scope. Flag for Phase 2 expansion.**

**Category B — Camera/photo (dermatology, trauma, secretions, fluids)**
- Parents frequently send these types of images
- AI interpretation is acceptable and generally accurate
- Mandatory disclaimer required: *"Debe usted tener necesariamente la opinión de su médico tratante o especialista"*
- **Decision: IN scope for MVP. AI analyzes with hardcoded disclaimer. Price separately.**

**Category C — Lab results (numeric values)**
- Lab result numbers are a meaningful, analyzable input
- AI can use these to provide more specific guidance
- **Decision: IN scope for MVP. Parse and interpret numeric lab values. Factor into urgency assessment.**

**Scope implication:** Image handling is now three sub-problems, not one. Category B adds vision model integration and liability language. Category C requires structured parsing of numeric data. Budget both separately from the base system.

---

### Q2 — Impresión Dx.: How Should It Be Framed?

**Martin's answer:** He makes a precise clinical distinction that matters for both implementation and liability:

- "Impresión diagnóstica" in medicine implies a *substantiated and verifiable analysis* — it is not a rough guess or symptom summary
- It is categorically different from the "diagnóstico definitivo," which belongs exclusively to the treating physician
- The physician audit/validation process that follows is *indispensable*

**What this means:** Martin is comfortable with — and expects — the AI to produce a proper clinical impression. He is using the term correctly and deliberately. The key flow is: AI produces the impression → physician validates or overrides → validation is mandatory, not optional.

**Implementation guidance:**
- The `impresion_dx` field should be presented as a substantiated AI analysis, not a vague hypothesis
- Suggested label: *"Impresión diagnóstica IA: [output] — Requiere validación del médico tratante"*
- Physician cannot close/archive an urgent or emergency case without completing the VoBo
- This term and its precise meaning must be explicitly defined in the engagement letter

---

### Q3 — VoBo: How Does It Work in Practice?

**Martin's answer:** He provides an important nuance on validation scope:

- Physician must validate what the AI worked and suggested — this validation improves AI training over time
- **Not required on every single interaction**
- **Mandatory for urgent / emergency cases** — no exceptions
- For routine (non-urgent) cases: statistically significant sampling is sufficient — he cites Pareto 80/20

**Implementation guidance:**
- Dashboard: urgent/emergency cases are locked until VoBo is completed
- Routine cases: system presents a sampling prompt periodically (e.g., "You have 12 unreviewed routine cases — review a sample?")
- VoBo data feeds back into the AI training loop — this is a feature, not just compliance
- Mechanism: dashboard web interface (not WhatsApp)
- Sampling rate (e.g., 20% of routine cases) to be confirmed with Martin in Discovery

---

### Q4 — Patient Records: Single Conversation or Ongoing History?

**Martin's answer:** Longitudinal records, with significant monetization implications he has already thought through:

- System must recognize patients by their ID
- Must connect with conversation history to provide more specific responses
- **Monetization thinking:** physician pays for the system; but storing patient history opens the possibility of charging parents/guardians monthly for data storage and historical data download
- Further vision: linking with hospital and health center records (databases, labs, imaging, rehabilitation centers)

**What this means:** Martin is thinking about a longitudinal health record platform with a B2C layer on top — a significantly larger vision than the initial scope. Build the data model correctly now so these paths are not blocked. Do not build B2C or EHR integration in Phase 1, but do not architect something that makes them impossible.

**Data model note:** The `patients` table needs a stable patient ID persisting across contacts from the same WhatsApp number. Edge case: multiple guardians contacting from different numbers for the same child — flag for Discovery.

---

### Q5 — Voice Responses: Required from the Start?

**Martin's answer:** Text is sufficient to begin.

**Decision: Voice output (ElevenLabs TTS) deferred to Phase 5.** System receives voice messages (STT transcription still required for incoming audio) but responds in text for MVP.

---

### Q6 — "Resultados *": What Does It Mean?

**Martin's answer:** Two-part clarification:

- Refers to results from evaluations or referrals ordered by the treating physician (lab work, specialist consultations, imaging)
- **Critical results** are a distinct subcategory — these cannot wait. Parent reporting a critical result must trigger the emergency pathway: contact physician immediately and go to ER

**Implementation guidance:**
- Add "critical lab result" to emergency detection logic — not just symptoms
- Parents should be able to describe or upload a result; system assesses whether it is within normal range or flagged critical
- Intersects with Q1 Category C (lab value parsing) — consistent design needed
- Critical numeric thresholds (hemoglobin, glucose, etc.) to be defined with Martin in Discovery

---

## Updated Scope Implications Summary

| Item | Previous Assumption | Confirmed Reality | Impact |
|---|---|---|---|
| Image handling | TBD — one question | Three categories; B and C in MVP, A deferred | Vision model + lab parser; price separately |
| Impresión Dx. | Cautious hypothesis | Full substantiated clinical impression | More capable output; stronger liability language |
| VoBo | All cases sign-off | Mandatory urgent/emergency; sampled routine | Dashboard workflow redesign; reduces physician burden |
| Patient records | TBD | Longitudinal by patient ID; B2C + EHR on roadmap | Larger data model; architecture must not block future |
| Voice output | TBD | Text-only for MVP | Removes ElevenLabs TTS from Phase 3; reduces cost |
| Resultados * | Lab results | Physician-ordered results + critical result emergency path | Critical results added to emergency detection |

---

## Architecture — Recommended Stack

### Core: Twilio + Claude API + ElevenLabs TTS (deferred)

```
Parent (WhatsApp)
      │
      ▼
Twilio WhatsApp
      │
      ▼
Learned Geek Webhook Backend (C# / ASP.NET Core)
      ├── Session manager (PostgreSQL-backed — per-number conversation state)
      ├── STT transcription (incoming voice → text)
      ├── Image router:
      │     ├── Category A (radiology) → store + forward, flag for future
      │     ├── Category B (photos) → vision model + mandatory disclaimer
      │     └── Category C (lab results) → numeric parser + urgency assessment
      ├── Critical result detector (runs alongside emergency keyword check)
      ├── Claude API — conversation logic + clinical impression generation
      │     └── System prompt (guardrails hardcoded)
      ├── PubMed E-utilities API — RAG context injection
      └── Structured JSON output parser
            │
            ▼
      Patient Data Warehouse (PostgreSQL)
            ├── Per-patient longitudinal records (stable patient ID)
            ├── Conversation history (threaded per patient)
            ├── Attachments (S3 — categorized by image type)
            ├── Lab values (structured numeric storage)
            ├── Clinical impressions (AI-generated, physician-validated)
            └── VoBo audit log (mandatory urgent; sampled routine)
                  │
                  ▼
      Physician Dashboard (Blazor)
            ├── Prioritized queue (EMG → urgent → routine)
            ├── Full transcript + image viewer + lab values
            ├── Impresión Dx. (AI clinical impression — validated/overridden)
            ├── VoBo workflow (locked urgent; sampling prompt routine)
            ├── Action tracking (called back / referred / no action)
            ├── System on/off toggle
            └── AI training feedback loop (VoBo outcomes → model improvement)
```

### Anthropic AUP — Compliance Requirements (Verified 2026-03-23)

The system is classified as a **High-Risk Use Case: Healthcare** under Anthropic's Acceptable Use Policy. Use is **permitted** with two mandatory requirements that are architectural, not optional:

**Requirement 1 — Human-in-the-loop:** A qualified professional must review AI output before it is disseminated to end users. **Already satisfied by the VoBo physician validation loop.** Physician review is mandatory for urgent/emergency cases; sampled for routine cases.

**Requirement 2 — Session-level AI disclosure:** Users must be informed that AI assisted in producing output. This disclosure **must appear at the beginning of every session** — not in a one-time consent form or terms document.

Mandatory opening message (every new WhatsApp conversation):
> *"Este servicio usa inteligencia artificial para asistir al Dr. [Nombre]. Toda la información será revisada por el médico antes de tomar cualquier decisión clínica."*

This must be the first message sent in every new conversation thread. It is a hard compliance requirement — not a recommendation.

**Source:** https://www.anthropic.com/legal/aup — High-Risk Use Cases section

---

### Updated System Prompt

```
You are a compassionate pediatric health assistant operating on behalf of
[Physician Name]. You communicate in Spanish.

MANDATORY FIRST MESSAGE (every new session — Anthropic AUP requirement):
"Este servicio usa inteligencia artificial para asistir al Dr. [Nombre].
Toda la información será revisada por el médico antes de tomar cualquier
decisión clínica."

YOUR ONLY FUNCTIONS:
1. Greet the parent warmly and gather: child name, age, symptoms,
   medical history, and any results they mention
2. If an image is shared:
   - Photo/camera image: describe findings, always add disclaimer
   - Lab result values: assess against normal ranges, flag if critical
   - Radiology image: acknowledge receipt, tell parent doctor will review
3. Assess urgency — emergency indicators trigger IMMEDIATE redirect:
   [hardcoded symptom + critical result keyword list]
4. Generate a substantiated clinical impression based on symptoms,
   history, and lab values (for physician review only — never for parent)
5. Non-emergency → reassure, confirm physician follow-up
6. Close with structured summary

YOU ARE NOT THE TREATING PHYSICIAN. The impresión diagnóstica you
generate is for the physician's review and validation only.
Every interaction must include: "El médico tratante revisará y
validará esta información. Todas las decisiones clínicas son
exclusivamente suyas."

MANDATORY IMAGE DISCLAIMER (Category B):
"La interpretación de imágenes por IA es orientativa. Debe usted
tener necesariamente la opinión de su médico tratante o especialista."
```

### Updated Emergency Trigger List

**Symptom-based:**
- dificultad para respirar / no puede respirar / no respira
- convulsiones / convulsionando / temblores
- inconsciente / no responde / no despierta
- sangrado severo / mucha sangre
- labios azules / morado / cianosis
- fiebre en bebé menor de 3 meses
- golpe en la cabeza / traumatismo craneal

**Result-based (new — from Q6):**
- resultado crítico / valores críticos
- médico pidió llamar urgente / resultado urgente
- [specific numeric thresholds — to be defined with Martin in Discovery]

---

## Data Architecture — Updated Schema

```sql
patients (
  id UUID PRIMARY KEY,
  physician_id UUID REFERENCES physicians(id),
  whatsapp_numbers JSONB,          -- array: multiple guardians may contact
  full_name VARCHAR,
  date_of_birth DATE,
  first_seen TIMESTAMPTZ,
  last_seen TIMESTAMPTZ,
  ehr_patient_id VARCHAR,          -- null until future EHR integration
  -- Legal compliance (Ley 29733 + NTS 139-MINSA)
  consent_captured_at TIMESTAMPTZ, -- when guardian consented
  consent_guardian_identity JSONB, -- guardian name, relationship, custody status
  blocked_at TIMESTAMPTZ,          -- set when parent requests "erasure" — data retained but inaccessible
  block_reason TEXT,               -- reason for blocking
  retain_until DATE,               -- 5 years from last_seen (calculated, enforced by application)
  arco_requests JSONB              -- log of ARCO requests and outcomes
)

conversations (
  id UUID PRIMARY KEY,
  patient_id UUID REFERENCES patients(id),
  started_at TIMESTAMPTZ,
  ended_at TIMESTAMPTZ,
  urgency_level VARCHAR,           -- emergency / urgent / non_urgent
  desenlace VARCHAR,               -- EMG / OBS
  impresion_dx TEXT,               -- AI clinical impression — physician eyes only
  impresion_dx_validated BOOLEAN,
  impresion_dx_override TEXT,      -- physician correction if overridden
  symptoms JSONB,
  lab_values JSONB,                -- structured numeric lab data
  summary TEXT,
  pubmed_refs JSONB,
  vobo_required BOOLEAN,           -- true for urgent/emergency; sampled for routine
  vobo_signed_at TIMESTAMPTZ,
  vobo_notes TEXT,
  physician_action VARCHAR         -- called_back / referred / no_action
)

messages (
  id UUID PRIMARY KEY,
  conversation_id UUID REFERENCES conversations(id),
  role VARCHAR,                    -- user / assistant
  content TEXT,
  sent_at TIMESTAMPTZ,
  has_attachment BOOLEAN
)

attachments (
  id UUID PRIMARY KEY,
  message_id UUID REFERENCES messages(id),
  s3_key VARCHAR,
  content_type VARCHAR,
  image_category VARCHAR,          -- radiology / camera / lab_result
  ai_analyzed BOOLEAN,
  ai_description TEXT,             -- Category B: vision model output
  lab_values_parsed JSONB,         -- Category C: structured numeric extraction
  disclaimer_shown BOOLEAN         -- Category B: mandatory disclaimer confirmed
)

physicians (
  id UUID PRIMARY KEY,
  name VARCHAR,
  whatsapp_number VARCHAR,
  system_active BOOLEAN DEFAULT false,
  enrolled_at TIMESTAMPTZ,
  vobo_sampling_rate FLOAT         -- e.g. 0.2 = 20% of routine cases
)
```

---

## Open Questions

### Resolved ✓

| # | Question | Answer |
|---|---|---|
| S001 | Image handling | Three categories: radiology deferred; photos analyzed with disclaimer; lab values parsed |
| S002 | Voice output for MVP? | Text only — voice deferred to Phase 5 |
| S003 | Impresión Dx. framing | Full substantiated clinical impression; mandatory physician validation |
| S004 | VoBo mechanism | Dashboard web interface; mandatory urgent/emergency; sampled routine |
| S005 | Resultados * | Physician-ordered results; critical results trigger emergency pathway |
| S006 | Longitudinal records | Yes — by patient ID; B2C and EHR integration on future roadmap |

### Still Open — Discovery Phase

| # | Question | Priority |
|---|---|---|
| D001 | What is Martin's budget range? | High — not yet discussed |
| D002 | Is physician network beyond Martin identified or aspirational? | High |
| D003 | Expo West — is US market active now? | High |
| D004 | Does franchise agreement restrict AI tool deployment? | Medium |
| D005 | Physician notification (SMS/push) when new conversation queued? | Medium |
| D006 | Languages beyond Spanish for MVP? | Medium |
| D007 | What is his timeline expectation? | High |
| D008 | Critical lab value thresholds — specific numeric triggers? | High — needed before emergency logic can be written |
| D009 | Multiple guardians per patient — identity resolution approach? | Medium |
| D010 | B2C parent subscription — Phase 1 or later? | Medium |
| D011 | VoBo sampling rate for routine cases? | Medium |
| D012 | Emergency keyword list — Martin defines in parent-language Spanish | **Critical — go-live blocker** |
| D013 | Critical lab value numeric thresholds — Martin defines with clinical input | **Critical — go-live blocker** |
| D014 | Physician override rate threshold agreement — what % triggers a go-live hold? | High |
| D015 | UAT participation commitment from Martin — contractual, not optional | High |

### Technical — Answer During POC

| # | Question |
|---|---|
| T001 | ~~Does Twilio WhatsApp sandbox behave consistently for testing?~~ **Answered — Yes, POC validated.** |
| T002 | ~~What is PubMed ESearch + EFetch combined latency mid-conversation?~~ **Answered — acceptable; POC validated.** |
| T003 | Which vision model gives best results for dermatology/trauma photos at acceptable cost? |
| T004 | Can Claude reliably parse lab result images and extract numeric values? |
| T005 | ~~Redis for session management or PostgreSQL-backed sessions?~~ **Decision: PostgreSQL-backed. Stack is C#/.NET — Redis adds unnecessary infrastructure.** |

---

## Risk Register — Updated

| Risk | Likelihood | Impact | Mitigation |
|---|---|---|---|
| Impresión Dx. misinterpreted as a diagnosis by parent or physician | Medium | Very High | Precise label, engagement letter language, physician onboarding |
| Category B image analysis inaccurate — AI misreads a photo | Medium | High | Mandatory disclaimer on every analysis; physician always validates |
| Critical lab thresholds not defined before build | Medium | High | Resolve D008 in Discovery before writing emergency logic |
| Ley 29733 compliance — overall | Low-Medium | High | Martin's analysis: proposal likely compliant; Carlos Rojas (Rebaza, Alcázar) reviewing |
| SCCs not yet in place with Anthropic/Twilio for Peru → US data transfer | High | Very High | Explicit consent alone is NOT sufficient; SCCs + ANPD notification required before launch |
| DIGEMID medical device classification — unknown | Unknown | Very High | If classified as medical device: registration, clinical testing, approval required — viability question |
| RENHICE / Ley 30024 applicability — unknown | Unknown | Medium | May impose interoperability requirements; Phase 6+ at minimum |
| Consent design for cross-border pediatric data — complex | Medium | High | Under-14 guardian required; cross-border requires parental consent regardless of age; both parents for sensitive data |
| No data blocking mechanism in current schema | High | High | Hard delete is illegal; must implement 5-year blocking with restricted access — schema change required |
| ARCO rights not yet designed into admin portal | High | Medium | Required by Ley 29733; intake + tracking + response deadlines must be in admin portal |
| Multiple guardians per patient — identity confusion | Medium | Medium | Design deduplication in Discovery; MVP can default to one number = one patient |
| Scope creep into EHR integration before foundation is solid | Medium | High | Explicitly defer in engagement letter |
| WhatsApp Business API approval delays | Medium | Medium | Start at Phase 1 kickoff |
| Martin's budget below project scope | Unknown | High | Discover in next call; prepare tiered scope options |

---

## The Authenticity Boundary — Confabulation Risk and Mitigation

**This is the most important architectural consideration in the project.** The root cause is well understood and named: **smoothness over truth** — the LLM will generate confident, plausible-sounding output to maintain conversational flow even when its evidence is insufficient to support it. In a pediatric triage system, this failure mode is not acceptable at any tier. The architecture must make confabulation structurally difficult, not merely discouraged by instruction.

Client-facing explanation: see `docs/client/authenticity-boundary-en.md`

---

### Risk Tiers

Confabulation risk is tiered by consequence. The mitigations are calibrated to tier.

| Tier | Scenario | Likelihood | Impact | Notes |
|---|---|---|---|---|
| 1 | Confabulated impression for routine non-urgent case | Medium | Moderate | Physician catches it; child was never at risk; trust erosion over time |
| 2 | Confabulated impression understates urgency | Low-Medium | Serious | Physician nudged toward routine when urgent was correct; delayed response |
| 3 | Emergency detection failure — emergency routed as non-urgent | Low | Catastrophic | Bypasses physician entirely during time-critical window; primary architectural threat |
| 3 | Critical lab value misread as normal | Low | Catastrophic | Same consequence as Tier 3 above |

**Tier 3 is the design target.** Tiers 1 and 2 are managed by the two-gate system. Tier 3 requires a categorically different architecture.

---

### The Two-Gate System (Tiers 1 and 2)

**Gate 1 — Evidence Gate (pre-physician)**

**Architectural principle (validated by ANI Runtime production debugging, March 2026):**
**Architecture over instruction.** Control what the model sees, not what it does with what it sees. Adding anti-confabulation instructions to the prompt consumes Claude's attention budget and competes with its trained honest-uncertainty behavior — producing robotic, parroting output rather than better reasoning. The fix is upstream: control what reaches the model.

See: `docs/internal/ANI-Cross-Project-Insight-Confabulation.md`

Operates in sequence before any clinical impression reaches the physician queue:

1. **Retrieval confidence floor — the most important gate.**
PubMed results are scored for semantic relevance. Start conservative: threshold 0.65+. At lower thresholds, tangentially related abstracts pass and the model incorporates them regardless of warning instructions. When nothing passes the threshold, inject nothing — do not retrieve-then-warn. A null impression queued for physician review is always better than a confident fabrication. Lower the threshold only after retrieval quality is validated in production.

2. **Schema-first null response — honesty as the default state.**
The output schema is structured so that `evidence_sufficient: false` is the default. The model must actively justify `true` with citations. Do NOT instruct Claude to say "insufficient evidence" — it was trained to express honest uncertainty naturally. Instruction competes with trained behavior and produces rule-following robotic output instead. Let the schema enforce the behavior.

```json
{
  "impresion_dx": null,
  "sources": [],
  "confidence": null,
  "evidence_sufficient": false,
  "retrieval_score": 0.41
}
```

```json
{
  "impresion_dx": "Possible viral pharyngitis based on symptom duration and fever pattern",
  "sources": ["PMID:12345678", "PMID:23456789"],
  "confidence": "moderate",
  "evidence_sufficient": true,
  "retrieval_score": 0.84
}
```

If `evidence_sufficient` is false or `sources` is empty — the impression field is null in the physician queue. No post-generation re-generation. The retrieval floor is the fix, not a downstream recovery step.

3. **Low-temperature generation — empirically validated.**
Temperature 0.2–0.3 for clinical impression generation. Validated by ANI Runtime production testing. Trades fluency for factual conservatism. Do not raise for clinical output.

4. **Lean system prompt — fewer competing instructions.**
The system prompt for clinical impression generation should be as short as possible. Every added instruction consumes attention budget. Claude's trained behavior handles honest uncertainty — do not re-instruct it. Add only what is architecturally necessary and not already in the output schema.

**Gate 2 — Physician Gate (VoBo)**

All cases that pass Gate 1 require physician review before any clinical decision. Urgent/emergency cases are locked until VoBo is completed. Routine cases are sampled. Physician override rate is tracked from day one — if overrides exceed ~15–20%, the system is not production-ready.

**Physician-curated fallback layer** — query categories with consistently low retrieval scores or high override rates are identified and routed directly to physician review without AI impression, bypassing Gate 1 entirely.

**Confabulation type tracking** — when a physician overrides an impression, record not just that an override occurred but why: Was the impression factually invented, or were real facts misattributed to the wrong source? These have different architectural causes. Also correlate overrides with retrieval score — if the physician overrides when retrieval_score was high, the problem is model reasoning; if overrides correlate with low retrieval_score, the confidence floor needs raising.

---

### Workstream A — Infanzia Product Chatbot

Same two-gate principle applies at lower stakes:

- **Retrieval confidence thresholding** — if product documentation does not clearly support the query, escalate to representative: *"I don't have that information — a representative will follow up."*
- **Citation enforcement** — factual claims (dosage, ingredients, certifications) must cite specific document and passage; unprovable claims are blocked
- **Constrained system prompt** — *"Answer only from provided product documentation. If the answer is not clearly supported by the documents, say so."*

---

### Emergency Pathway — Separate Architecture, Zero LLM Latitude

The emergency detection pathway operates under fundamentally different rules. **The LLM has no creative latitude here.**

**Design principle: over-inclusive, not precise.**
A false positive (non-emergency escalated to ER) is an inconvenience.
A false negative (real emergency routed as routine) is the catastrophic Tier 3 scenario.
When in doubt, the system escalates. Every time. There is no "probably not an emergency" path.

**Implementation:**
- Emergency keyword matching runs **before** any retrieval or impression generation
- Matching is deterministic — keyword list, not LLM judgment
- On match: hardcoded response fires immediately:
  > *"Lo que describes suena como una emergencia médica. Por favor llame al 112 / vaya a urgencias AHORA. No espere la respuesta del médico."*
- Claude does not write or modify this message under any circumstances
- Physician receives simultaneous SMS alert
- Case is queued as EMERGENCY with mandatory VoBo — locked until physician reviews

**The emergency keyword list is a clinical document, not a technical one.**
Martin must define it in the language parents actually use — colloquial Spanish, not medical terminology. He must sign off on it formally before go-live. This sign-off is documented and retained. It establishes that clinical boundaries were set by a physician, not by software engineers.

**Critical lab value thresholds** follow the same principle — Martin defines the specific numeric boundaries that trigger the emergency pathway. These are resolved in Discovery (item D008).

---

### Testing Requirements — Martin's Participation is Mandatory

AI accuracy in a clinical context cannot be validated in a lab. Martin's active testing participation is a project requirement and a go-live gate.

- UAT: Martin runs realistic parent scenarios through the system — including ambiguous cases near but not clearly at the emergency threshold
- Override rate tracking active from day one of testing
- Go-live gate: override rate must be below ~15–20% sustained across a meaningful sample
- Emergency detection stress-tested with ambiguous inputs — the hardest cases, not the obvious ones
- Image analysis (Category B) tested against real clinical photos, not stock images
- **Formal written sign-off from Martin on accuracy acceptability is required before go-live** — documented, retained on file, referenced in the engagement letter

This is shared responsibility: Learned Geek builds the safeguards; Martin validates they work in the clinical context he understands and we do not.

---

### Risk Register — Authenticity Boundary

| Risk | Likelihood | Impact | Mitigation |
|---|---|---|---|
| AI confabulates clinical impression with no supporting PubMed evidence | Medium | Very High | Gate 1: retrieval confidence threshold + null response path |
| AI misapplies retrieved abstracts — wrong clinical context | Medium | High | Gate 1: citation enforcement + PMID attribution verification |
| Emergency detection failure — Tier 3 | Low | Catastrophic | Deterministic keyword matching; LLM excluded from emergency pathway; over-inclusive by design |
| Critical lab value misread as normal | Low | Catastrophic | Same deterministic pathway; Martin defines numeric thresholds in Discovery |
| Physician overtrusts AI impression after repeated accuracy — stops critically reviewing | Medium | High | Override rate tracking; VoBo UX designed to require active review, not passive approval |
| AI confabulates product information (Workstream A) | Medium | High | Confidence thresholding + citation enforcement + escalation path |
| Category B image analysis inaccurate in real clinical photos | Medium | High | Mandatory disclaimer + physician always reviews + UAT with real images |
| Override rate not tracked before go-live | Low | Very High | Override tracking is a Phase 3 build requirement, not a Phase 4 add-on |
| Martin does not participate in UAT | Low | Very High | Engagement letter: UAT participation is a contractual obligation, not a courtesy |

---

## Phasing Plan — Updated

### Phase 0 — POC (Internal, 1–2 weeks, not billable) ✓ COMPLETE
- ✅ Twilio → Claude API → PubMed RAG → structured JSON output — validated
- ✅ Bilingual (Spanish/English) triage conversation — validated
- ✅ File-backed conversation persistence per phone number — validated
- ✅ PubMed latency acceptable mid-conversation — validated
- Add: vision model test with sample Category B photo
- Add: lab value parsing test with sample numeric result

### Phase 1 — Discovery & Specification (2–3 weeks, billable)
- Resolve D001–D011
- Define critical lab value thresholds with Martin (D008)
- Map full physician workflow including VoBo sampling design
- Legal review — scope now includes longitudinal records + potential B2C
- Define liability boundaries for clinical impression and image analysis
- Detailed technical specification for client sign-off

### Phase 2 — Infanzia Product Chatbot (4–6 weeks)
- WhatsApp AI assistant for product line
- Knowledge base: all Infanzia products
- Admin interface (Blazor): document upload, conversation log, on/off toggle

### Phase 3 — Physician System Backend (7–9 weeks)
- Twilio + Claude API + PubMed RAG pipeline
- Image routing: Category A store/forward; Category B vision + disclaimer; Category C lab parser
- Critical result emergency detection
- Longitudinal patient data warehouse (PostgreSQL)
- Session management (PostgreSQL-backed)
- VoBo audit log with mandatory/sampling logic
- Clinical impression generation

### Phase 4 — Physician Dashboard (4–6 weeks, concurrent with Phase 3)
- Prioritized queue (EMG → urgent → routine)
- Transcript + image viewer + lab values panel
- Impresión Dx. with physician override
- VoBo workflow (locked urgent; sampling prompt routine)
- AI feedback loop — physician validations feed training data

### Phase 5 — Voice Integration (2–3 weeks, deferred)
- ElevenLabs TTS for Spanish voice output
- Activate after text-only MVP is validated in production

### Phase 6 — Future Platform (separate scoping)
- B2C parent subscription layer
- Hospital / EHR integration
- Radiology image analysis (HIS/RIS/PACS)
- Multi-tenant productization layer

### Phase 7 — Ongoing Retainer
- Hosting and infrastructure
- Knowledge base updates
- PubMed query tuning
- VoBo sampling rate adjustment

---

## Pricing Framework (Internal — Do Not Share)

| Deliverable | Est. Range | Notes |
|---|---|---|
| Discovery & Specification | $2,500 – $3,500 | Legal coordination, full spec doc |
| Infanzia Product Chatbot | $8,000 – $10,000 | Full build including admin interface |
| Physician System Backend | $16,000 – $20,000 | Twilio + Claude + PubMed + image analysis + lab parser + data warehouse |
| Physician Dashboard | $6,000 – $8,000 | VoBo workflow, sampling logic, queue management |
| Voice Integration (deferred) | $3,000 – $4,000 | Phase 5 — not in MVP |
| **Total MVP Build Estimate** | **$32,500 – $41,500** | Excluding voice and retainer |
| Monthly Retainer | $500 – $1,200/mo | Hosting + maintenance + KB updates + support |

**Separate future line items (do not include in current proposal):**
- Radiology integration (HIS/RIS/PACS) — significant complexity, separate scoping
- B2C parent subscription layer — separate scoping
- Hospital/EHR integration — separate scoping
- Multi-tenant productization layer — $8,000–12,000 when ready

**Internal note on budget:** Martin's budget has not been discussed. His sophisticated responses suggest he understands this is a serious investment — but do not assume. Prepare a tiered scope option: a leaner MVP (no image analysis, no lab parsing) for a lower entry point if needed.

---

## Productization Potential — Updated

Martin's Q4 response reveals he is already thinking about this as a platform: B2C subscriptions, EHR linking, multi-institutional data. He may be a co-visionary, not just a first client. The correct posture is to build the foundation correctly now (patient ID system, longitudinal records, clean data model) and keep all future paths open. A separate conversation about the platform vision is worth having once the initial engagement is underway.

---

## Immediate Next Steps

### Completed
- [x] Receive and analyze Martin's written Q&A responses ✓
- [x] Build POC (Phase 0) — core pipeline validated (Twilio + Claude + PubMed RAG + bilingual + persistence) ✓
- [x] Comprehensive task breakdown — ~271 tasks across 16 milestones (see `docs/technical/TASK-BREAKDOWN.md`) ✓
- [x] Legal & compliance gap analysis — 58 legal tasks identified including medical device classification risk ✓
- [x] Pre-Discovery action items document sent to Martin via WhatsApp + email ✓
- [x] Martin's email addresses established (tyrmanb@gmail.com, martin.nunez@schweringmbh-lab.com) ✓
- [x] Carlos Rojas (Rebaza, Alcázar & De Las Casas) engaged as legal counsel — carlos.rojas@rebaza-alcazar.com ✓
- [x] Martin's legal analysis received (2026-03-23) — 4 Ley 29733 questions answered in depth; more responses coming ✓
- [x] Legal findings documented and summarized (see `docs/internal/LEGAL-COMPLIANCE.md`) ✓

### Awaiting from Martin ("vuelvo en breve con lo demás")
- [ ] Remaining responses to non-legal action items (franchise restrictions, budget, timeline, etc.)
- [ ] Carlos Rojas formal review of legal items (L001, L002, L008, L012, L019, L022, L024, L026, L035, L048)

### Send to Martin (Priority — drives everything else)
- [x] ~~Send Martin pre-Discovery action items document~~ ✓ Done
- [x] ~~Ask for Peruvian legal counsel recommendation~~ ✓ Carlos Rojas engaged
- [ ] Schedule Discovery call — he said "sin prisa" but nudge gently once remaining responses arrive
- [ ] Ask Martin to check DIGEMID medical device classification risk with Carlos Rojas
- [ ] Ask Martin to review franchise agreement for AI deployment restrictions
- [ ] Request budget range and timeline expectations (if not in next response)

### Learned Geek Actions (Parallel — don't wait for Martin)
- [ ] Register free NCBI API key: ncbi.nlm.nih.gov/account (2 min)
- [ ] Review Anthropic Acceptable Use Policy for medical/clinical restrictions
- [ ] Review Meta/WhatsApp Business Policy for health data messaging restrictions
- [ ] Review Twilio policies for medical use
- [ ] Get E&O / professional liability / cyber liability insurance quotes — no current policy beyond LLC
- [ ] Prepare tiered scope options (full vs lean MVP) for budget conversation
- [ ] Update POC to include Category B vision model test + lab value parsing
- [ ] Draft engagement letter template (scope, IP ownership, liability cap, indemnification)
- [ ] Begin WhatsApp Business API application (1–4 week lead time)
- [ ] Set up GitHub Project board — milestones, labels, custom fields
- [ ] Backfill internal docs — phasing plan now includes admin portal, legal, security, deployment

### Blockers (Cannot proceed past Discovery without these)
- [ ] Peruvian legal counsel engaged and reviewing Ley 29733 compliance
- [ ] DIGEMID medical device classification — answer received (project-viability question)
- [ ] Telemedicine law (Ley 30421) applicability — answer received
- [ ] Budget alignment with Martin
- [ ] Engagement letter signed

---

## Updated Phasing (reflects full task breakdown)

The original phasing underestimated operational overhead. The complete task breakdown (see `docs/technical/TASK-BREAKDOWN.md`) reveals the true scope:

| Phase | Original Estimate | Revised Estimate | Delta |
|---|---|---|---|
| Build (Phases 1–6) | $32.5K–$41.5K | Similar | Build cost hasn't changed much |
| Legal & Compliance | Not estimated | 120–180 hrs | Was invisible; now explicit |
| Admin Portal | Not scoped | 80–120 hrs | Audit trail, monitoring, compliance tools |
| Testing (full) | Not estimated | 50–75 hrs | Automated + UAT with Martin |
| Documentation | Not estimated | 30–45 hrs | README, API docs, runbooks, client guide |
| Deployment & DevOps | Not estimated | 45–65 hrs | Environments, CI/CD, go-live |
| **Total project** | **315–460 hrs** | **740–1,060 hrs** | **~2x when operational overhead is honest** |

**Key insight:** The build is ~50% of the real effort. Legal, compliance, testing, documentation, deployment, and the admin portal are the other ~50%. This is normal for regulated, international, medical-adjacent systems — but it must be reflected in pricing and timeline if we're honest with ourselves and the client.

**Pricing implication:** The current pricing framework ($32.5K–$41.5K) covers the build. It does not cover legal counsel, insurance, compliance tooling, or the full admin portal. Either these are absorbed as business cost (investment in a productizable platform) or they're scoped into the engagement. Decision needed before Discovery call.

---

*Learned Geek — Internal document. Not for client distribution.*
