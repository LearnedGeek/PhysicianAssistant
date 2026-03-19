# Task Breakdown — By Component

**Purpose:** Map deliverables to GitHub Issues. Each section becomes a milestone or epic; each task becomes an issue with an hour estimate. Estimates are rough — refine during Discovery (Phase 1).

**Traceability:** During Discovery, functional requirements (FRs) are documented and numbered. Each FR maps to one or more technical requirements (TRs). Each TR maps to one or more tasks below. GitHub Issues should reference the FR/TR they satisfy (e.g., "Implements FR-012 / TR-012.1"). This creates a traceable chain: Functional Requirement → Technical Requirement → Task/Issue → PR → Test, and makes phase progress measurable against requirements, not just task counts.

**Estimate key:** Sm = 2–4 hrs, Md = 4–8 hrs, Lg = 8–16 hrs, XL = 16–24 hrs

---

## Phase 1 — Discovery & Specification

> Milestone: `discovery`

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 1.1 | Schedule and conduct Discovery call with Martin | Sm | Mark | Resolve D001–D011 |
| 1.2 | Define critical lab value thresholds (D008) | Sm | Mark + Martin | Blocker for emergency logic |
| 1.3 | Map full physician VoBo workflow + sampling design | Md | Mark | Input from Martin |
| 1.4 | Data flow diagram — every vendor, what data, where processed | Md | Mark | Required for Ley 29733 |
| 1.5 | Draft engagement letter with liability boundaries | Md | Mark | Legal review needed |
| 1.6 | Engage Peruvian legal contact for Ley 29733 review | Sm | Mark | Longitudinal records + B2C scope |
| 1.7 | Register NCBI API key | Sm | Mark | 2 min — just do it |
| 1.8 | Prepare tiered scope options (full vs lean MVP) | Md | Mark | In case budget is tight |
| 1.9 | Produce detailed technical specification for sign-off | Lg | Mark | Discovery deliverable |
| 1.10 | WhatsApp Business API application | Sm | Mark | Start early — approval can be slow |

---

## Phase 2 — Infanzia Product Chatbot

> Milestone: `product-chatbot`

### 2A. Knowledge Base & RAG

| # | Task | Est. | Notes |
|---|---|---|---|
| 2A.1 | Define product knowledge base schema | Sm | Products, ingredients, dosing, certifications |
| 2A.2 | Ingest Infanzia product catalog into knowledge base | Md | PDFs, product sheets from Martin |
| 2A.3 | Build RAG retrieval pipeline for product docs | Lg | Semantic search + confidence thresholding |
| 2A.4 | Implement citation enforcement on product claims | Md | Must cite source doc for dosage/ingredient claims |
| 2A.5 | Implement confidence fallback ("I don't have that info") | Sm | Below-threshold queries route to escalation |

### 2B. Conversation Engine (Product)

| # | Task | Est. | Notes |
|---|---|---|---|
| 2B.1 | System prompt — product chatbot persona + guardrails | Sm | Spanish-first, no medical advice |
| 2B.2 | Twilio webhook for product chatbot WhatsApp number | Md | Separate number from physician system |
| 2B.3 | Conversation session management | Md | Per phone number, with context window |
| 2B.4 | Escalation path — route to human representative | Sm | Low-confidence or out-of-scope queries |
| 2B.5 | Claims intake structured data capture | Md | Form-like flow within conversation |
| 2B.6 | Customer satisfaction survey flow | Sm | Post-conversation survey trigger |

### 2C. Admin Interface (Blazor)

| # | Task | Est. | Notes |
|---|---|---|---|
| 2C.1 | Blazor project scaffold + auth | Md | Basic layout, login |
| 2C.2 | Document upload page — PDF/Word → knowledge base | Lg | Upload, parse, trigger KB rebuild |
| 2C.3 | Conversation log viewer | Md | Paginated list, search, filter by date |
| 2C.4 | Basic analytics — volume, topics, escalation rate | Md | Simple charts/counts |
| 2C.5 | Channel on/off toggle | Sm | Enable/disable chatbot |

---

## Phase 3 — Physician System Backend

> Milestone: `physician-backend`

### 3A. Data Model & Persistence

| # | Task | Est. | Notes |
|---|---|---|---|
| 3A.1 | EF Core entity models — Physician, Patient, Conversation, Message, Attachment | Lg | From schema in technical proposal |
| 3A.2 | Initial migration + seed data | Md | PostgreSQL, seed a test physician |
| 3A.3 | Patient identity resolution — WhatsApp number → patient record | Md | Stable patient ID across contacts |
| 3A.4 | Longitudinal conversation threading per patient | Md | Link conversations to patient history |
| 3A.5 | Attachment storage — S3/Azure Blob integration | Md | Upload, categorize, retrieve |
| 3A.6 | Lab values structured storage (JSONB) | Sm | Parsed numeric data per conversation |

### 3B. Conversation Engine (Physician)

| # | Task | Est. | Notes |
|---|---|---|---|
| 3B.1 | System prompt — physician assistant persona + guardrails | Md | Spanish, liability language, emergency triggers |
| 3B.2 | Twilio webhook — receive parent messages, route to Claude | Md | Extend POC to production patterns |
| 3B.3 | PubMed RAG pipeline — query construction + retrieval | Md | Already validated in POC; productionize |
| 3B.4 | Structured JSON output parser — symptoms, urgency, impression | Md | Claude structured output → typed C# models |
| 3B.5 | Session management — PostgreSQL-backed per phone number | Md | Replace file-backed POC persistence |
| 3B.6 | Conversation lifecycle — start, active, ended, archived | Sm | State machine for conversation status |
| 3B.7 | Parent consent capture — first interaction consent flow | Sm | Cross-border data transfer disclosure |

### 3C. Emergency Detection

| # | Task | Est. | Notes |
|---|---|---|---|
| 3C.1 | Keyword-based emergency trigger list (Spanish) | Sm | From technical proposal — hardcoded list |
| 3C.2 | Critical lab result detection — numeric threshold checks | Md | Thresholds from Martin (D008) |
| 3C.3 | Emergency response flow — redirect + notify physician | Md | Immediate SMS/push to physician + ER guidance |
| 3C.4 | Emergency detection integration tests | Md | Ambiguous inputs, edge cases |

### 3D. Image Handling

| # | Task | Est. | Notes |
|---|---|---|---|
| 3D.1 | Image category router — classify incoming attachment | Md | Radiology / camera / lab result |
| 3D.2 | Category A (radiology) — store + forward, flag for future | Sm | Acknowledge, don't analyze |
| 3D.3 | Category B (camera/photo) — vision model analysis + disclaimer | Lg | Claude vision API, mandatory disclaimer |
| 3D.4 | Category C (lab results) — numeric value extraction | Lg | Parse lab images → structured values |
| 3D.5 | Image analysis integration tests | Md | Real-ish test images |

### 3E. Clinical Impression & VoBo

| # | Task | Est. | Notes |
|---|---|---|---|
| 3E.1 | Clinical impression generation — Claude + PubMed evidence | Md | Low temperature, PMID attribution |
| 3E.2 | RAG fallback — null impression when evidence insufficient | Sm | "Insufficient evidence — physician review required" |
| 3E.3 | PMID source attribution enforcement | Md | Validate every claim maps to retrieved abstract |
| 3E.4 | VoBo flag logic — mandatory urgent/emergency, sampled routine | Md | Based on physician's sampling rate |
| 3E.5 | VoBo audit log — write on every validation | Sm | Timestamp, physician, outcome |

### 3F. API Layer

| # | Task | Est. | Notes |
|---|---|---|---|
| 3F.1 | REST API — conversation queue (filtered, sorted by urgency) | Md | Dashboard consumes this |
| 3F.2 | REST API — conversation detail (transcript, attachments, lab values) | Md | Single conversation full view |
| 3F.3 | REST API — VoBo submit (validate/override impression) | Sm | POST with notes + override |
| 3F.4 | REST API — physician action (called_back / referred / no_action) | Sm | Status update |
| 3F.5 | REST API — physician settings (system toggle, sampling rate) | Sm | GET/PUT |
| 3F.6 | REST API — patient history (longitudinal view) | Md | All conversations for a patient |
| 3F.7 | API authentication — physician JWT | Md | Per-physician access control |
| 3F.8 | Swagger/OpenAPI spec generation | Sm | For dashboard development |

---

## Phase 4 — Physician Dashboard (Blazor)

> Milestone: `physician-dashboard`
> **Primary intern scope** — see INTERNSHIP-PLAN.md

### 4A. Shell & Auth

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 4A.1 | Blazor project scaffold — layout, navigation, routing | Md | Intern | Shared shell with product admin or separate app TBD |
| 4A.2 | Physician login — JWT auth flow | Md | Intern | Consume auth API |
| 4A.3 | Responsive layout — desktop primary, tablet secondary | Sm | Intern | Physician at desk or on phone |

### 4B. Conversation Queue

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 4B.1 | Queue page — prioritized list (EMG → urgent → routine) | Md | Intern | Color-coded urgency, sorted |
| 4B.2 | Queue filtering — by urgency, date range, VoBo status | Md | Intern | Filter controls |
| 4B.3 | Queue auto-refresh / real-time updates | Sm | Intern | Poll or SignalR |
| 4B.4 | Unread/new conversation indicators | Sm | Intern | Badge counts |

### 4C. Conversation Detail View

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 4C.1 | Transcript viewer — full message thread | Md | Intern | Chat-style layout, timestamps |
| 4C.2 | Image viewer — inline display of Category B photos | Md | Intern | Thumbnail + lightbox |
| 4C.3 | Lab values panel — structured numeric display | Md | Intern | Highlight out-of-range values |
| 4C.4 | Patient info header — name, age, contact, history link | Sm | Intern | From patient record |
| 4C.5 | PubMed references panel — linked PMIDs | Sm | Intern | Clickable links to abstracts |

### 4D. VoBo Workflow

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 4D.1 | Impresión Dx. display — AI clinical impression card | Sm | Intern | Prominent, clearly labeled |
| 4D.2 | VoBo validation form — approve or override with notes | Md | Intern | Override replaces impression text |
| 4D.3 | Urgent/emergency lock — cannot archive without VoBo | Md | Intern | UI enforces mandatory validation |
| 4D.4 | Routine sampling prompt — "12 unreviewed — review a sample?" | Sm | Intern | Periodic prompt, not blocking |

### 4E. Physician Actions & Settings

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 4E.1 | Action buttons — called back / referred / no action needed | Sm | Intern | Per conversation |
| 4E.2 | System on/off toggle | Sm | Intern | Prominent, with confirmation |
| 4E.3 | Physician profile/settings page | Sm | Intern | Sampling rate, notification prefs |

### 4F. Patient History

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 4F.1 | Patient longitudinal view — all conversations for a patient | Md | Intern | Timeline or list view |
| 4F.2 | Patient search | Sm | Intern | By name or phone number |

---

## Phase 5 — Admin Portal (Blazor — Internal Learned Geek)

> Milestone: `admin-portal`
> This is NOT the physician dashboard. This is Learned Geek's internal operations, audit, and support tool. If something goes wrong or we are audited, this portal must provide complete, gap-free traceability of every system action.

### 5A. Conversation Audit Trail

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 5A.1 | Full conversation search — by patient, phone number, physician, date range, urgency | Md | Mark | Cross-physician visibility |
| 5A.2 | Complete message log — every message sent and received, with timestamps | Md | Mark | Immutable; no edits/deletes |
| 5A.3 | AI decision trace — system prompt version, Claude request/response, temperature, model used | Lg | Mark | Reproduce exactly what the AI saw and said |
| 5A.4 | RAG retrieval log — PubMed queries sent, PMIDs returned, confidence scores, what was injected | Md | Mark | Prove what evidence the AI used |
| 5A.5 | Clinical impression audit — original AI impression, physician override (if any), VoBo timestamp | Md | Mark | Full chain: AI generated → physician validated/overrode |
| 5A.6 | Emergency detection log — what triggered (keyword? lab value?), timestamp, response sent | Md | Mark | Prove the system acted correctly on emergencies |
| 5A.7 | Image analysis log — what was uploaded, category assigned, AI analysis output, disclaimer shown | Md | Mark | Category B: prove disclaimer was delivered |
| 5A.8 | Conversation replay — step-through view of an entire conversation as it happened | Lg | Mark | Reconstruct the parent's experience exactly |

### 5B. System Health & Monitoring

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 5B.1 | Dashboard — system status overview (active conversations, queue depth, error rate) | Md | Mark | At-a-glance operational health |
| 5B.2 | API latency tracking — Twilio, Claude, PubMed response times per request | Md | Mark | Detect degradation before users notice |
| 5B.3 | Error log viewer — structured errors with stack traces, request context | Md | Mark | Searchable, filterable |
| 5B.4 | Failed message log — messages that failed to send/receive, with retry status | Sm | Mark | Twilio delivery failures, webhook errors |
| 5B.5 | Health check endpoints — app, database, Twilio, Claude API, PubMed, blob storage | Md | Mark | Automated monitoring can poll these |
| 5B.6 | Alerting — email/SMS notification on critical errors, downtime, emergency detection failures | Md | Mark | Don't wait for Martin to tell you it's broken |
| 5B.7 | Uptime tracking — system availability history | Sm | Mark | SLA evidence if needed |

### 5C. Consent & Compliance Audit

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 5C.1 | Consent log — every parent consent captured, timestamp, exact language shown, version | Md | Mark | Prove consent was obtained before data processing |
| 5C.2 | Physician enrollment log — ToS acceptance, system activation, consent version | Sm | Mark | Prove physician acknowledged limitations |
| 5C.3 | Data retention dashboard — records approaching retention limit, deletion queue | Md | Mark | Ley 29733 compliance |
| 5C.4 | Data export — per-patient export for data subject access requests | Md | Mark | Ley 29733 right of access |
| 5C.5 | Data deletion — per-patient deletion with audit trail of the deletion itself | Md | Mark | Ley 29733 right of erasure; log that deletion happened without logging deleted content |
| 5C.6 | Cross-border transfer log — which vendor processed which data, when, to which jurisdiction | Md | Mark | Prove data flow matches consent language |
| 5C.7 | DPA compliance tracker — which DPAs are active, expiry dates, renewal needed | Sm | Mark | Don't let a DPA lapse |

### 5D. User & Access Management

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 5D.1 | Physician account management — create, suspend, deactivate | Md | Mark | Onboarding/offboarding |
| 5D.2 | Admin user management — Learned Geek staff access with roles | Sm | Mark | Who can see what in admin portal |
| 5D.3 | Access audit log — every admin portal login, what was viewed, by whom | Md | Mark | Who looked at patient data and when |
| 5D.4 | Session management — active sessions, force logout | Sm | Mark | Security incident response |

### 5E. Support Tools

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| 5E.1 | Conversation lookup by phone number — support case triage | Sm | Mark | Parent calls Martin, Martin calls us |
| 5E.2 | System prompt version history — what prompt was active when | Md | Mark | Prompt changes affect AI behavior; need before/after |
| 5E.3 | Configuration audit — who changed what setting, when | Sm | Mark | Sampling rate, emergency keywords, system toggle |
| 5E.4 | Physician VoBo compliance report — override rate, unsigned urgent cases, sampling adherence | Md | Mark | Is the physician actually reviewing? |
| 5E.5 | AI accuracy metrics — override rate trends, null impression rate, low-confidence query rate | Md | Mark | Track improvement over time |
| 5E.6 | Usage/billing report — conversations, API calls, storage, by physician | Md | Mark | Cost allocation, client invoicing |
| 5E.7 | Incident report generator — pull all relevant logs for a specific conversation into one export | Md | Mark | When something goes wrong, one-click package |

---

## Phase 6 — Voice Integration (Deferred)

> Milestone: `voice-integration`

| # | Task | Est. | Notes |
|---|---|---|---|
| 6.1 | STT integration — incoming voice → text transcription | Md | Azure Speech or Whisper |
| 6.2 | ElevenLabs TTS integration — response text → voice | Md | Spanish/LatAm voice |
| 6.3 | Voice message playback in dashboard | Sm | Audio player component |
| 6.4 | Voice/text toggle per physician preference | Sm | Settings |

---

## Cross-Cutting / Infrastructure

> Milestone: `infrastructure`

| # | Task | Est. | Notes |
|---|---|---|---|
| X.1 | CI/CD pipeline — GitHub Actions → build + test | Md | Dev / staging / production |
| X.2 | Environment configuration — appsettings per environment | Sm | Dev, staging, prod |
| X.3 | PostgreSQL provisioning + connection management | Sm | Local dev + cloud |
| X.4 | Blob storage provisioning (S3 or Azure Blob) | Sm | For attachments |
| X.5 | Logging + monitoring — structured logging, health checks | Md | Serilog or similar |
| X.6 | Rate limiting — WhatsApp abuse prevention | Sm | Per-number throttle |
| X.7 | Backup strategy — database + blob storage | Sm | Automated, documented |
| X.8 | HTTPS + encryption at rest | Sm | Required for Ley 29733 |

---

## Project Administration

> Milestone: `admin`

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| A.1 | GitHub Project board setup — milestones, labels, custom fields | Sm | Mark | One-time setup |
| A.2 | Issue creation — seed all tasks from this breakdown | Md | Mark | Bulk create via `gh` CLI |
| A.3 | Weekly issue triage / backlog grooming | Sm/wk | Mark | Ongoing — reprioritize, re-estimate |
| A.4 | Client status updates — Discovery progress, demo scheduling | Sm/wk | Mark | Martin communication |
| A.5 | WCTC coordination — Training Agreement, eval forms, instructor meeting | Sm | Mark | Before and during internship |
| A.6 | Intern onboarding — environment setup, repo access, architecture walkthrough | Md | Mark | Week 1 of internship |
| A.7 | Time tracking review — verify intern hours against 144 target | Sm/wk | Mark | Weekly during internship |
| A.8 | Midterm evaluation (WCTC) | Sm | Mark | ~Week 4 of internship |
| A.9 | Final evaluation (WCTC) | Sm | Mark | ~Week 8 of internship |
| A.10 | Engagement letter / contract management | Sm | Mark | Before Phase 1 billable work |
| A.11 | Vendor account management — Twilio, Anthropic, NCBI, blob storage | Sm | Mark | API keys, billing, DPAs |

---

## Documentation

> Milestone: `documentation`

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| D.1 | README.md — project setup, prerequisites, how to run locally | Md | Mark | Must exist before intern starts |
| D.2 | CONTRIBUTING.md — branch naming, PR process, code review expectations | Sm | Mark | Intern reference doc |
| D.3 | Architecture decision records (ADRs) — key design decisions | Sm/each | Mark | As decisions are made |
| D.4 | API documentation — Swagger + endpoint usage guide | Md | Mark | Consumed by dashboard; intern reference |
| D.5 | System prompt documentation — what each prompt does and why | Sm | Mark | Internal reference for tuning |
| D.6 | Deployment runbook — how to deploy each environment | Md | Mark | Dev / staging / production |
| D.7 | Intern weekly reflection logs | Sm/wk | Intern | WCTC requirement + learning evidence |
| D.8 | Handoff documentation — what the intern built, how it works, known issues | Md | Intern | Final week deliverable |
| D.9 | Client-facing user guide — physician dashboard usage | Md | TBD | Martin onboarding material |
| D.10 | Data flow diagram — finalized from Discovery | Md | Mark | Ley 29733 compliance artifact |

---

## Testing

> Milestone: `testing`

### Automated

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| T.1 | Test project scaffold — xUnit, test fixtures, CI integration | Md | Mark | Must exist before intern writes tests |
| T.2 | Unit tests — conversation engine (session, state transitions) | Md | Mark | Core logic coverage |
| T.3 | Unit tests — emergency detection (keywords + numeric thresholds) | Md | Mark | Safety-critical — high coverage |
| T.4 | Unit tests — image category routing | Sm | Mark | Classification logic |
| T.5 | Unit tests — VoBo sampling logic | Sm | Mark | Mandatory vs sampled |
| T.6 | Integration tests — Twilio webhook → Claude → structured response | Lg | Mark | End-to-end conversation flow |
| T.7 | Integration tests — PubMed RAG retrieval + confidence thresholding | Md | Mark | Fallback behavior |
| T.8 | Integration tests — REST API endpoints | Md | Mark/Intern | Dashboard API contract |
| T.9 | Blazor component tests — queue, transcript, VoBo form | Md | Intern | bUnit or similar |
| T.10 | CI test gate — PRs blocked if tests fail | Sm | Mark | GitHub Actions |

### Manual / UAT

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| T.11 | Manual test plan — realistic parent scenarios (Spanish) | Md | Mark | Written scenarios for UAT |
| T.12 | UAT with Martin — clinical impression accuracy review | Lg | Mark + Martin | Go/no-go gate |
| T.13 | Emergency detection stress testing — ambiguous inputs | Md | Mark + Martin | Near-miss scenarios |
| T.14 | Image analysis UAT — real dermatology/trauma photos | Md | Mark + Martin | Category B accuracy |
| T.15 | Physician dashboard UAT — Martin walks through full workflow | Md | Mark + Martin | Queue → transcript → VoBo → action |
| T.16 | Load/abuse testing — rate limiting, conversation length caps | Sm | Mark | WhatsApp is public-facing |

---

## Mentoring & Learning (Intern)

> Milestone: `intern-learning`

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| M.1 | Blazor tutorial path — Microsoft Learn or equivalent | Lg | Intern | Weeks 1–2 |
| M.2 | REST API consumption patterns — HttpClient, auth headers, error handling | Md | Intern | Guided self-study |
| M.3 | Git workflow tutorial — branches, PRs, rebasing, code review | Sm | Intern | Week 1 |
| M.4 | Code review participation — review Mark's PRs (read-only at first) | Sm/wk | Intern | Learning by reading |
| M.5 | Weekly 1:1 — progress review, blockers, teaching moments | Sm/wk | Mark | 30–60 min |
| M.6 | Architecture walkthrough — how the full system fits together | Md | Mark | Week 1, revisit week 4 |
| M.7 | Technical design exercise — write design doc before implementing a feature | Md | Intern | SMART goal #4 |
| M.8 | Final demo preparation — build and rehearse presentation | Md | Intern | Week 8 |
| M.9 | SMART goal check-ins — progress against 3–5 goals | Sm/wk | Mark + Intern | Part of weekly 1:1 |

---

## Deployment & Environments

> Milestone: `deployment`

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| E.1 | Development environment — local Docker Compose or direct run | Md | Mark | PostgreSQL + app + blob storage |
| E.2 | Staging environment provisioning | Md | Mark | Cloud — mirrors production |
| E.3 | Production environment provisioning | Md | Mark | Cloud — region TBD (US or São Paulo) |
| E.4 | Environment-based configuration — appsettings per env | Sm | Mark | Connection strings, API keys, feature flags |
| E.5 | Database migration strategy — EF Core migrations in CI/CD | Md | Mark | Auto-apply on deploy or manual gate |
| E.6 | SSL/TLS certificates | Sm | Mark | Required for Ley 29733 |
| E.7 | Domain/DNS setup — dashboard URL | Sm | Mark | Physician-facing URL |
| E.8 | Twilio phone number provisioning — WhatsApp Business approval | Md | Mark | Can take weeks — start early |
| E.9 | Production go-live checklist | Md | Mark | Security, backups, monitoring, legal sign-off |
| E.10 | Post-launch monitoring — first 48 hours | Md | Mark | Watch for errors, latency, abuse |

---

## Legal & Compliance

> Milestone: `legal-compliance`
> **This is the section that keeps you out of trouble.** The international element, pediatric health data, AI-generated clinical impressions, and cross-border data flows create a legal surface area much larger than a typical web project. Every item below exists because getting it wrong could result in regulatory action, civil liability, or loss of the ability to operate.

### L-A. Contracts & Engagement

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| L.1 | Engagement letter — scope, liability, IP ownership, payment terms | Md | Mark | Must be signed before Phase 1 billable work |
| L.2 | IP ownership clause — who owns the platform vs. client data vs. prompts | Md | Mark + Legal | Critical: if Martin thinks he owns the platform, you can't white-label it |
| L.3 | Limitation of liability — cap on damages, exclusion of consequential damages | Md | Mark + Legal | AI gives wrong impression → child harmed → who pays? Must be explicit |
| L.4 | Indemnification — mutual or one-way, scope | Md | Mark + Legal | Martin indemnifies for clinical decisions; Learned Geek indemnifies for system uptime |
| L.5 | Termination clause — what happens to patient data if engagement ends | Sm | Mark + Legal | Data export, deletion timeline, transition period |
| L.6 | Change order process — how scope changes are priced and approved | Sm | Mark | Martin's vision is expanding; need a mechanism |
| L.7 | Payment terms — milestones, net-30, currency (USD or PEN?) | Sm | Mark | International payment: wire transfer, PayPal, Wise? |
| L.8 | Non-compete / exclusivity — can Martin hire another firm to build a competing system? | Sm | Mark + Legal | Protect your investment if this becomes a product |

### L-B. Peruvian Data Privacy (Ley 29733)

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| L.9 | Engage Peruvian legal counsel — specialist in data privacy / health data | Md | Mark | Non-negotiable; you cannot self-assess Peruvian law |
| L.10 | Data controller vs. data processor classification — who is who? | Md | Legal | Martin is likely controller; Learned Geek is processor. Changes obligations. |
| L.11 | Registro Nacional de Protección de Datos Personales — registration required? | Md | Legal | Peru requires data bank registration with the APDP (Autoridad Nacional de Protección de Datos Personales). Is Martin required to register? Are you? |
| L.12 | Sensitive data classification — health data of minors | Sm | Legal | Pediatric health data is doubly sensitive: health + minors |
| L.13 | Explicit consent requirements — what "explicit" means under Ley 29733 | Md | Legal | Written? Digital? What constitutes valid consent via WhatsApp? |
| L.14 | Consent for minors — who can consent for a child's health data? | Md | Legal | Legal guardian only? Both parents? Age thresholds? |
| L.15 | Cross-border data transfer — legal mechanism | Md | Legal | Ley 29733 Art. 15: requires adequate protection or explicit consent. Is US "adequate"? If not, what mechanism? |
| L.16 | Data subject rights — access, rectification, cancellation, opposition (ARCO rights) | Md | Legal + Mark | Must implement technical capability for all four rights |
| L.17 | Data breach notification — requirements and timeline | Md | Legal | Does Peru require notification to APDP? To data subjects? Within what timeframe? |
| L.18 | Data retention limits — legal maximum for health data | Sm | Legal | What does Peruvian law say vs. what Martin wants for longitudinal records? |
| L.19 | Right of erasure vs. medical record retention — conflict resolution | Md | Legal | Parent requests deletion but medical records have retention requirements. Which wins? |

### L-C. Medical / Regulatory Classification

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| L.20 | Medical device classification — is the AI system a "medical device" under Peruvian law? | Lg | Legal | DIGEMID (Peru's FDA equivalent) may classify AI diagnostic tools as medical devices. If so: registration, approval, and ongoing compliance required. This could be a project-killer if discovered late. |
| L.21 | Telemedicine regulations — does this system constitute telemedicine? | Md | Legal | Peru's Ley 30421 (Telemedicine Law) — does an AI acting on behalf of a physician count? Different rules may apply. |
| L.22 | Peruvian medical practice regulations — can an AI generate an "impresión diagnóstica"? | Md | Legal | Is this reserved for licensed professionals? Even if labeled as AI-generated + physician-validated? |
| L.23 | MINSA (Ministry of Health) notification — required for health technology platforms? | Md | Legal | Some jurisdictions require registration of health IT systems |
| L.24 | Clinical liability chain — who is liable for an incorrect AI clinical impression? | Lg | Mark + Legal | Martin (as treating physician)? Learned Geek (as system builder)? Both? Must be defined in engagement letter AND in physician enrollment ToS |
| L.25 | Emergency detection failure liability — system misses an emergency | Lg | Mark + Legal | Highest-stakes scenario. If the AI fails to detect an emergency and a child is harmed: who is responsible? What does the disclaimer say? Is it legally sufficient? |
| L.26 | Practice of medicine without a license — does Learned Geek risk this? | Md | Legal | In some jurisdictions, building and operating a clinical decision tool can be construed as unauthorized practice of medicine. Learned Geek is a tech company, not a medical practice. |

### L-D. Vendor Compliance

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| L.27 | Anthropic Acceptable Use Policy — medical/clinical use permitted? | Sm | Mark | Review AUP for restrictions on medical decision-making |
| L.28 | Anthropic DPA — execute and retain | Sm | Mark | Required before production |
| L.29 | Twilio DPA — execute and retain | Sm | Mark | Required before production |
| L.30 | Twilio WhatsApp Business Policy — medical use permitted? | Sm | Mark | Meta/WhatsApp has policies on health-related messaging; verify compliance |
| L.31 | WhatsApp Commerce Policy — restrictions on health data in messages | Sm | Mark | WhatsApp prohibits certain health data in messages. Does the AI response content comply? |
| L.32 | Cloud provider DPA — AWS or Azure | Sm | Mark | Standard but must be executed |
| L.33 | Cloud provider BAA (Business Associate Agreement) — needed? | Sm | Legal | If any US health data law applies (unlikely for Peru-only, but relevant if US expansion) |
| L.34 | PubMed/NCBI terms of use — compliance for commercial use | Sm | Mark | NCBI is free but has usage policies for commercial applications |
| L.35 | Sub-processor notification — Ley 29733 may require disclosing all sub-processors | Md | Legal | Martin (controller) must know every entity processing patient data |

### L-E. Insurance & Business Protection

> **Current state:** Learned Geek LLC provides liability protection as a corporate entity. There is no explicit E&O, professional liability, or cyber liability insurance policy in place. The tasks below are about evaluating whether this engagement changes that calculus.

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| L.36 | Evaluate need for E&O insurance — AI/medical context | Md | Mark | Get quotes. Understand what a policy would and wouldn't cover for this project. |
| L.37 | Evaluate need for professional liability insurance | Md | Mark | Clinical decision support tools may create exposure beyond what the LLC shields. |
| L.38 | Evaluate need for cyber liability insurance | Sm | Mark | Pediatric health data breach = significant exposure. What would a policy cost? |
| L.39 | International coverage — does policy cover claims originating from Peru? | Sm | Mark + Agent | US policy may not cover foreign jurisdiction claims |
| L.40 | Contract value vs. liability exposure assessment | Sm | Mark | $32K–$41K contract value vs. potentially uncapped liability. Engagement letter cap is essential. |

### L-F. Consent Architecture (Technical + Legal)

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| L.41 | Parent consent flow — first WhatsApp interaction | Md | Mark + Legal | Must capture: identity, acknowledgment, data processing consent, cross-border consent, retention consent. All before any health data is discussed. |
| L.42 | Consent versioning — track which version of consent language each parent agreed to | Md | Mark | If you update consent language, old consents are still valid under old version |
| L.43 | Consent withdrawal mechanism — parent can revoke consent via WhatsApp | Md | Mark + Legal | What happens to existing data? Stop processing? Delete? Archive? |
| L.44 | Re-consent trigger — when consent language changes, existing parents must re-consent | Md | Mark | Can't silently update consent terms |
| L.45 | Physician enrollment consent — ToS acceptance with specific AI limitation acknowledgment | Md | Mark + Legal | Physician acknowledges: AI may be wrong, VoBo is mandatory, they are the decision-maker |
| L.46 | Physician consent: impresión Dx. terminology agreement | Sm | Mark + Legal | Physician signs that they understand what this term means in this system |
| L.47 | Image disclaimer consent — Category B mandatory disclaimer, logged per image | Sm | Mark | Prove disclaimer was shown every time |

### L-G. Incident Response & Breach

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| L.48 | Incident response plan — documented procedure for security incidents | Md | Mark | Who does what, in what order, within what timeframe |
| L.49 | Data breach notification procedure — Peru (APDP) | Md | Legal | Timeline, format, who to notify |
| L.50 | Data breach notification procedure — affected individuals | Md | Legal | How, when, what to say |
| L.51 | Clinical incident procedure — AI gives incorrect/harmful guidance | Lg | Mark + Legal | Different from a data breach. The data is fine; the advice was wrong. What happens? |
| L.52 | Emergency detection failure procedure — system misses a real emergency | Lg | Mark + Legal | Worst-case scenario playbook. Immediate steps, notification chain, documentation, preservation of evidence |
| L.53 | Vendor outage procedure — Twilio, Claude, or PubMed goes down mid-conversation | Md | Mark | Parent is mid-triage and the system stops responding. What message do they get? Who is notified? |
| L.54 | Evidence preservation — legal hold capability for any conversation | Sm | Mark | If a complaint or claim arises, relevant data must be preserved immediately |

### L-H. Intern Legal

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| L.55 | FLSA compliance documentation — time allocation evidence | Sm | Mark | Primary beneficiary test artifacts |
| L.56 | Intern NDA — intern will see architecture docs, potentially patient data schemas | Sm | Mark | Standard NDA before repo access |
| L.57 | Intern IP assignment — work product belongs to Learned Geek | Sm | Mark | Standard work-for-hire/assignment clause in training agreement or separate |
| L.58 | Intern access restrictions — no production data, no patient PII | Sm | Mark | Document what they can and cannot access |

---

## Vendor Registrations & Subscriptions

> Milestone: `vendor-setup`

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| V.1 | Twilio account — upgrade from trial if needed | Sm | Mark | Verify WhatsApp sandbox → production path |
| V.2 | Twilio WhatsApp Business Profile — submit for approval | Md | Mark | Requires business verification; can take 1–4 weeks |
| V.3 | Twilio phone number — provision dedicated number(s) | Sm | Mark | Separate numbers for product chatbot vs physician system? |
| V.4 | Anthropic API account — production tier, billing, usage limits | Sm | Mark | Monitor costs during dev |
| V.5 | NCBI API key — register at ncbi.nlm.nih.gov/account | Sm | Mark | Free; increases PubMed rate limits from 3/sec to 10/sec |
| V.6 | Cloud provider account — AWS or Azure | Sm | Mark | For PostgreSQL, blob storage, hosting |
| V.7 | PostgreSQL provisioning — managed instance (RDS, Azure DB, etc.) | Md | Mark | Region decision: US or São Paulo |
| V.8 | Blob storage — S3 bucket or Azure Blob container | Sm | Mark | For image/attachment storage |
| V.9 | Domain registration — dashboard URL + any branded subdomains | Sm | Mark | e.g., dashboard.infanzia.learnedgeek.com |
| V.10 | Email/notification service — SendGrid or similar | Sm | Mark | Physician notifications, system alerts |
| V.11 | ElevenLabs account — when voice phase begins | Sm | Mark | Deferred to Phase 5 |
| V.12 | GitHub — repo visibility, branch protection rules, team access | Sm | Mark | Before intern starts |
| V.13 | Azure Speech / OpenAI Whisper — STT service selection | Sm | Mark | For incoming voice messages |
| V.14 | Cost monitoring — monthly spend tracking across all vendors | Sm/mo | Mark | Ongoing; set billing alerts |

---

## Security

> Milestone: `security`

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| S.1 | Secrets management — no hardcoded keys, env-based injection | Sm | Mark | Azure Key Vault, AWS Secrets Manager, or dotnet user-secrets |
| S.2 | API authentication — JWT implementation + token refresh | Md | Mark | Physician dashboard access |
| S.3 | Role-based access control — physician sees only their patients | Md | Mark | Multi-tenant data isolation |
| S.4 | Encryption at rest — database + blob storage | Sm | Mark | Required for Ley 29733 |
| S.5 | Encryption in transit — TLS everywhere | Sm | Mark | HTTPS, encrypted DB connections |
| S.6 | Input validation — sanitize all WhatsApp message content | Sm | Mark | Prevent injection via parent messages |
| S.7 | Rate limiting — per-phone-number throttle on WhatsApp webhook | Sm | Mark | Abuse prevention |
| S.8 | Conversation length cap — max messages per session | Sm | Mark | Prevent runaway API costs |
| S.9 | Audit logging — who accessed what patient data, when | Md | Mark | Compliance + security forensics |
| S.10 | Dependency scanning — Dependabot or similar | Sm | Mark | GitHub native; enable on repo |
| S.11 | Security review — pre-launch checklist | Md | Mark | OWASP top 10, auth flows, data exposure |
| S.12 | Intern access scoping — repo access yes, production/secrets no | Sm | Mark | Principle of least privilege |
| S.13 | PII handling policy — what logs can/cannot contain | Sm | Mark | No patient data in application logs |
| S.14 | Session expiry / token revocation — dashboard auto-logout | Sm | Mark | Physician may walk away from screen |

---

## International & Localization

> Milestone: `international`

| # | Task | Est. | Owner | Notes |
|---|---|---|---|---|
| I.1 | Timezone handling — Peru (PET, UTC-5) vs server time | Sm | Mark | All timestamps stored UTC, display in physician's TZ |
| I.2 | Locale-aware formatting — dates, numbers, currency | Sm | Mark | Peruvian conventions (dd/mm/yyyy, comma decimal) |
| I.3 | Spanish system prompts — clinical terminology review with Martin | Md | Mark + Martin | Medical Spanish is precise; get it right |
| I.4 | Emergency keyword list — regional variations | Sm | Mark + Martin | Peruvian Spanish vs generic Spanish |
| I.5 | WhatsApp message templates — pre-approved by Meta (Spanish) | Md | Mark | Required for outbound/notification messages |
| I.6 | Currency handling — pricing in PEN (Peruvian Sol) for B2C | Sm | Mark | Future — but schema should accommodate |
| I.7 | Data residency decision — US vs São Paulo region | Sm | Mark + Legal | Inform by Ley 29733 guidance |
| I.8 | Cross-border data transfer consent language — finalize | Sm | Mark + Legal | Name specific jurisdictions in consent |
| I.9 | Multilingual foundation — string externalization for future languages | Sm | Mark | Don't hardcode Spanish strings in code |
| I.10 | Network latency testing — Peru ↔ hosting region | Sm | Mark | Dashboard responsiveness for Martin |

---

## Summary — Rough Hour Estimates by Phase

| Phase | Tasks | Est. Hours | Notes |
|---|---|---|---|
| 1 — Discovery | 10 | 30–45 | Billable; mostly Mark |
| 2 — Product Chatbot | 16 | 60–90 | RAG + conversation + admin UI |
| 3 — Physician Backend | 30 | 120–170 | Data model, conversation engine, images, VoBo, APIs |
| 4 — Physician Dashboard | 18 | 70–100 | **Intern scope: ~60–80 hrs of this** |
| 5 — Admin Portal | 33 | 80–120 | Audit trail, monitoring, compliance, support tools |
| 6 — Voice (deferred) | 4 | 15–25 | Post-MVP |
| A — Project Admin | 11 | 25–40 | Ongoing; heavier during intern weeks |
| D — Documentation | 10 | 30–45 | README, API docs, ADRs, runbooks, client guide |
| T — Testing | 16 | 50–75 | Automated + manual UAT with Martin |
| M — Mentoring/Learning | 9 | 35–50 | Intern tutorials, 1:1s, code review, demo prep |
| E — Deployment | 10 | 25–35 | Environments, CI/CD, go-live |
| X — Infrastructure | 8 | 20–30 | Logging, rate limiting, backups |
| L — Legal & Compliance | 58 | 120–180 | Contracts, Ley 29733, medical classification, vendor compliance, insurance, consent, incident response, intern |
| V — Vendor Registrations | 14 | 20–30 | Twilio, Anthropic, NCBI, cloud, domain, billing alerts |
| S — Security | 14 | 25–40 | Secrets, RBAC, encryption, audit logging, PII policy |
| I — International | 10 | 15–25 | Timezone, locale, WhatsApp templates, data residency |
| **Total** | **~271** | **740–1,060** | Full project including all operational overhead |

### Intern Coverage (Phase 4)

The dashboard tasks (4A–4F) total roughly **60–80 hours** of development work. Combined with ~30 hours of tutorials, mentoring, documentation, and WCTC requirements, this fits within the 144-hour internship window. If the intern moves faster, tasks from Phase 2C (product admin interface) can be pulled in — they're the same Blazor skill set.

---

## GitHub Structure

When ready to create:

```
Milestones:     discovery, product-chatbot, physician-backend, physician-dashboard,
                admin-portal, voice-integration, infrastructure, admin, documentation,
                testing, intern-learning, deployment, legal-compliance, vendor-setup,
                security, international
Labels:         feature, learning, documentation, bug, blocked, intern, backend,
                frontend, api, devops, testing, admin, mentoring, uat, legal,
                security, vendor, i18n, audit, monitoring, compliance, support
Custom Fields:  Estimate (number, hours), Actual (number, hours), Component (single select)
```

*Internal planning document — task-level estimates are for planning only, not client-facing.*
