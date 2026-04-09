# Infanzia — Implementation Plan
**Last Updated:** 2026-03-07 (rev 2 — vendor strategy added)  
**Status:** Pre-engagement / Scoping  
**Author:** Mark McArthey / Learn Geek LLC

---

## Overview

This project splits into two parallel workstreams of very different complexity and risk profiles. The Infanzia Product Chatbot is the fast, low-risk deliverable. The Physician AI Triage System is the flagship product — innovative, complex, and potentially white-labelable beyond this client.

---

## Workstream A — Infanzia Product Chatbot

### Purpose
A WhatsApp-based AI assistant for the Infanzia product line. Answers parent questions about products, dosing, ingredients, where to buy, certifications. Handles claims and surveys. Operates 24/7 in Spanish (and additional languages as needed).

### Complexity
Low-Medium

### Estimated Timeline
4–6 weeks from engagement start

### Stack
- **ElevenLabs Agents** — voice + text agent, WhatsApp native integration
- **ElevenLabs Knowledge Base** — product catalog, dosing tables, FAQs, certifications
- **ElevenLabs RAG** — document upload for product updates
- **Custom webhook endpoint** — conversation logging, analytics
- **Admin interface** — simple web UI for Martin to upload new product docs and review chatbot interactions

### Feature Scope
- Spanish-first, English secondary (additional languages Phase 2)
- Text and voice support via WhatsApp
- Product Q&A (ingredients, dosing, certifications, where to buy)
- Claims intake form (structured data capture)
- Customer satisfaction surveys
- Conversation history log for Martin to review
- Escalation path: complex questions → "a representative will contact you"
- Hard guardrail: no medical advice, always refer to physician

### Admin Interface
- Document upload (PDF, Word) → triggers knowledge base rebuild
- Conversation log viewer
- Basic analytics (volume, topics, escalations)
- On/off toggle per channel

---

## Workstream B — Physician AI Triage System

### Purpose
An after-hours AI assistant that operates on behalf of enrolled pediatricians. Parents contact via WhatsApp when the physician is unavailable. The AI captures symptoms, triages urgency, keeps parents calm, and queues all information for physician review at next availability.

### Complexity
High — medical context, regulatory considerations, multi-party system

### Estimated Timeline
12–16 weeks from engagement start (following discovery phase)

### Discovery Phase First
Before any build begins, a paid discovery/scoping phase is required to:
- Map Martin's full process diagram (he will provide)
- Investigate OpenEvidence API access and integration path
- Assess Peruvian health data regulations (Ley 29733)
- Define physician onboarding and consent process
- Define liability boundaries in writing
- Produce detailed technical specification

---

### System Architecture

```
Parent (WhatsApp)
       │
       ▼
Twilio WhatsApp Business API
  - Receives parent text/voice messages
  - Delivers AI responses (text + optional voice)
       │
       ▼
Learned Geek Backend (Node.js)
  - Webhook receiver + session manager
  - Claude API — conversation logic + triage reasoning
  - PubMed RAG — clinical context (symptom keywords only, no PII)
  - Emergency detection logic (hardcoded triggers)
       │
       ├──► [Emergency detected] → Immediate redirect to emergency services
       │
       ├──► [Non-emergency] → Reassure parent, confirm doctor follow-up time
       │
       ├──► ElevenLabs TTS (optional) → voice synthesis of response text only
       │
       ▼
Data Storage (region-configurable)
  - Structured conversation payload
  - Symptom summary + triage classification
  - Parent contact details + child name/age
  - Full transcript + timestamps
  - PubMed references used
       │
       ▼
Physician Dashboard (Web)
  - Queued conversations by priority
  - Full transcript + audio playback
  - Child/parent details
  - AI triage classification
  - Action buttons: Called back / Referred / No action needed
  - System ON/OFF toggle per physician
  - Knowledge base management
```

---

### Key System Components

#### 1. Conversation Engine (Twilio + Claude API)
- Twilio WhatsApp receives parent messages, routes to our webhook
- Claude API handles conversation logic with custom system prompt
- Strict liability guardrails — captures and calms, never diagnoses
- Full control over conditional conversation flow (emergency vs. non-emergency paths)
- Physician on/off toggle via our backend (not dependent on any vendor feature)
- Session management per parent WhatsApp number

#### 2. Medical Knowledge Layer (PubMed + Physician-Curated RAG)
OpenEvidence has been ruled out — requires US NPI, enterprise-only API, US-centric clinical guidelines (see 04-decisions-and-questions.md for full research). The confirmed approach is a custom RAG layer combining:
- **PubMed E-utilities API** — free, open, 35M+ biomedical citations, no geographic restrictions, symptom keywords only sent (no PII)
- **WHO/PAHO guidelines** — Latin American clinical context, geographically appropriate for Peru
- **Physician-curated content** — Martin uploads Peruvian MINSA guidelines, AAP guidelines he trusts, Infanzia product clinical references
- Physician-managed via admin interface — Martin controls what the AI knows
- This is actually better than OpenEvidence for this use case: no NPI, no vendor dependency, content appropriate for Peruvian practice

#### 3. Voice Layer (ElevenLabs TTS — Optional)
- Used for voice synthesis of AI responses only — never receives patient data
- Spanish/Latin American voice selection (warm, calm, professional)
- Fully swappable — Azure Cognitive Services, Google TTS as alternatives
- Text-only WhatsApp is sufficient for MVP — voice is a Phase 2 enhancement

#### 4. Data Capture & Storage
- Structured JSON per conversation
- Fields: child_name, child_age, parent_name, parent_phone, symptoms[], triage_level, timestamp, physician_id, conversation_transcript, audio_url, pubmed_references[]
- Ley 29733 compliance — explicit consent, dual-layer encryption (see Encryption Architecture below)
- Region-configurable hosting (AWS São Paulo available if residency required)
- Configurable retention policy

#### 4a. Encryption Architecture — Dual-Layer ("Double Encryption")

Medical data requires encryption at two independent layers. This satisfies HIPAA safe harbor provisions (US) and Ley 29733 "appropriate technical measures" requirements (Peru). Both layers are mandatory — one without the other is insufficient.

**Layer 1 — Encryption in Transit (TLS)**
- All client-server communication over TLS 1.2+ (HTTPS only, no plaintext HTTP)
- All inter-service communication (backend → database, backend → APIs) over TLS
- Certificate management via Let's Encrypt or Azure-managed certificates
- HSTS headers enforced — browsers cannot downgrade to HTTP
- Twilio webhook endpoints require TLS — this is enforced by Twilio
- API keys and tokens transmitted only over encrypted channels

**Layer 2 — Encryption at Rest (Storage + Application)**

This layer has two sub-components:

**2a. Storage-Level Encryption (Transparent Data Encryption / TDE)**
- PostgreSQL: enable TDE or use Azure Database for PostgreSQL which provides AES-256 encryption at rest by default
- Blob storage (patient images, audio): Azure Blob Storage with SSE (Server-Side Encryption) using AES-256
- Backup encryption: all database backups and exports encrypted at rest
- Disk-level encryption on all servers (Azure Disk Encryption / BitLocker)
- Key management via Azure Key Vault — encryption keys never stored alongside data

**2b. Application-Level Field Encryption (Column/Field Encryption)**
- Sensitive fields encrypted in the application layer *before* writing to the database
- Even a DBA with full database access sees ciphertext for protected fields
- Encrypted fields:
  - `child_name` — AES-256-GCM
  - `parent_name` — AES-256-GCM
  - `parent_phone` — AES-256-GCM
  - `conversation_transcript` — AES-256-GCM
  - `symptoms[]` — AES-256-GCM
  - `audio_url` (if pointing to patient recordings) — AES-256-GCM
- Non-encrypted fields (needed for querying/sorting):
  - `triage_level` — needed for priority queue sorting
  - `timestamp` — needed for chronological ordering
  - `physician_id` — needed for routing/filtering
  - `pubmed_references[]` — non-sensitive, public data
- Encryption key rotation policy: keys rotated every 90 days via Azure Key Vault
- Key hierarchy: master key → data encryption keys (DEKs) per tenant, envelope encryption pattern
- Decryption occurs only in the application layer at read time, only for authenticated/authorized users

**Implementation Notes:**
- Use a dedicated `IEncryptionService` interface for all field-level encryption — abstracts provider and enables testing
- AES-256-GCM preferred over AES-256-CBC (GCM provides authentication + encryption, prevents tampering)
- Never log decrypted sensitive fields — logging pipeline must use the encrypted form
- Search on encrypted fields requires deterministic encryption or a separate search index with hashed/tokenized values
- Performance consideration: field-level encryption adds ~1-2ms per field per operation — negligible for this use case

**What Each Layer Protects Against:**

| Threat | TLS (Transit) | TDE (Storage) | Field Encryption (App) |
|---|---|---|---|
| Network interception / man-in-the-middle | ✅ | — | — |
| Stolen database backup / disk | — | ✅ | ✅ |
| Unauthorized DBA access | — | — | ✅ |
| Cloud provider breach / insider threat | — | Partial | ✅ |
| Application-level SQL injection | — | — | ✅ (data is ciphertext) |
| Regulatory compliance (HIPAA / Ley 29733) | Required | Required | Best practice / safe harbor |

**HIPAA Safe Harbor:** If a breach occurs but all compromised data was encrypted per these standards, HIPAA does not require patient notification — the data is considered "unsecured" only if it was unencrypted. This is a significant liability reduction for US market expansion.

#### 5. Physician Dashboard
- Authentication (per physician login)
- Conversation queue (sorted by triage priority)
- Full transcript + audio playback
- Structured symptom summary (AI-generated)
- Action/status tracking
- Feedback loop — physician marks outcome, feeds back into model improvement
- System on/off toggle
- Knowledge base document management

#### 6. Physician Onboarding
- Simple enrollment flow
- Terms of service / liability acknowledgment
- WhatsApp number linking
- Initial knowledge base setup
- Training on system use

---

### Liability & Safety Architecture

This is non-negotiable and must be built into the system from day one:

1. **The AI never diagnoses.** Every response includes appropriate language: *"This is not medical advice. Your physician will review this information and contact you."*
2. **Emergency detection is hardcoded.** Specific symptom keywords trigger immediate emergency redirect regardless of conversation state.
3. **Physician is always the decision-maker.** The AI explicitly positions itself as a data capture and communication tool, not a clinical tool.
4. **Audit trail is complete.** Every interaction is logged, timestamped, and attributable.
5. **Physician consent is documented.** Enrollment includes explicit acknowledgment of the system's scope and limitations.
6. **Parent consent is captured.** First interaction includes consent to data capture and processing.

---

### Regulatory Considerations (Peru)

- **Ley 29733** — Ley de Protección de Datos Personales (Personal Data Protection Law)
- Health data is sensitive data under Peruvian law — requires explicit consent
- Data must be stored securely — dual-layer encryption required (see Section 4a: Encryption Architecture)
- Data retention limits apply
- Cross-border data transfer requires explicit consent and adequate protection guarantees (see Data Residency section below)
- **Recommendation:** Engage a Peruvian legal advisor to review before launch

---

### Data Residency & Cross-Border Transfer

This is a critical consideration for a system processing pediatric health data from Peru through US-based cloud services. Ley 29733 classifies health data as sensitive — cross-border transfer is not prohibited, but requires explicit consent and adequate protection guarantees (similar to GDPR adequacy).

#### Where Patient Data Gets Processed

Every conversation involves patient data leaving Peru and being processed by third-party services. The discovery phase must produce a complete data flow map showing exactly which vendor sees which data and where it is processed.

| Data Type | Vendor | Processing Location | What They See |
|---|---|---|---|
| Parent text messages | Twilio (WhatsApp) | US | Full message content |
| Conversation logic + triage reasoning | Claude API (Anthropic) | US | Symptoms, child details, parent messages |
| Voice synthesis (response audio only) | ElevenLabs TTS | US/EU | AI response text only — NOT patient data |
| PubMed clinical context | NCBI/NLM | US | Symptom keywords only — no PII |
| Stored conversation records | Our database | Configurable | Everything — full transcripts, PII, triage data |

#### Key Design Decisions for Data Residency

1. **ElevenLabs TTS receives response text only.** In the Twilio + Claude architecture, ElevenLabs never sees patient symptoms, child names, or parent details — it only converts the AI's response into audio. This is a meaningful compliance advantage over using ElevenLabs as full orchestrator.

2. **PubMed queries use symptom keywords only.** No PII is sent to the NCBI API — only medical terms extracted from the conversation.

3. **Parent consent must explicitly cover cross-border transfer.** The first WhatsApp interaction consent flow must state clearly that data is processed by cloud services outside Peru, and name the jurisdictions (United States).

4. **Data Processing Agreements (DPAs) required.** Twilio and Anthropic both offer DPAs. These must be executed before production launch and provided to Martin's legal advisor.

5. **Database hosting is our choice.** Conversation records and physician dashboard data can be hosted in any region. Options:
   - **AWS São Paulo (sa-east-1)** — closest to Peru, keeps stored data in Latin America
   - **AWS US regions** — simpler, lower cost, but all data in US
   - Decision should be informed by Martin's legal advisor's guidance on Ley 29733

#### Future Option: Regional LLM Processing

If cross-border processing of conversation content becomes a regulatory blocker:
- **AWS Bedrock (São Paulo)** — Claude API available through Bedrock, can run in sa-east-1, keeps LLM inference within Latin America
- **Google Cloud Vertex AI (São Paulo)** — Claude also available here
- This is not needed for POC or MVP, but the architecture supports it — swapping `anthropic.messages.create()` for a Bedrock client is a configuration change, not a rebuild

#### Discovery Phase Deliverable

The discovery phase specification must include a **complete data flow diagram** showing:
- Every vendor that touches patient data
- What data each vendor receives
- Where each vendor processes/stores that data
- What DPAs are in place
- What consent language is required for cross-border transfer
- Martin's legal advisor reviews this before build begins

---

### Multilingual / International Expansion

- Claude API supports multilingual conversation natively — no additional configuration for new languages
- ElevenLabs TTS supports 29+ languages for voice synthesis
- Phase 1: Spanish (Peru) — text-only MVP, voice optional
- Phase 2: English, Portuguese (Brazil)
- Phase 3: Additional markets per Martin's international contacts
- Voice cloning option (future): consistent branded "assistant" voice across all languages

---

## Vendor Strategy & Productization Risk

### The Core Question
If this system is ever sold to multiple physician practices as a white-label product, does ElevenLabs get in the way?

**Short answer: potentially yes — if we don't architect around it from day one.**

---

### ElevenLabs Risks at Scale

#### 1. Vendor Lock-in
ElevenLabs controls the conversation layer entirely. If they change pricing, deprecate WhatsApp support, restrict API access, alter their terms of service, or get acquired, the product is exposed. Building a business on infrastructure you don't control is a meaningful risk — especially in a medical context where reliability is non-negotiable.

#### 2. Margin Compression
ElevenLabs charges per conversation/minute of voice at the API level. At a single-client scale this is manageable. At 10, 50, or 100 physician practices the API costs scale linearly with revenue. The margin math needs to be modeled carefully before committing to a productized pricing model.

| Scale | Est. Monthly Conversations | ElevenLabs Cost (est.) | Notes |
|---|---|---|---|
| 1 practice | ~500 | ~$50–150 | Comfortable |
| 10 practices | ~5,000 | ~$500–1,500 | Still manageable |
| 50 practices | ~25,000 | ~$2,500–7,500 | Needs pricing model review |
| 100 practices | ~50,000 | ~$5,000–15,000 | Requires renegotiated enterprise tier |

> Actual ElevenLabs API pricing to be confirmed during POC phase.

#### 3. White-Label Limitations
Can the ElevenLabs agent be fully white-labeled — meaning zero ElevenLabs branding visible to end users? This needs verification against their terms of service. Some platforms restrict white-labeling at lower tiers or require enterprise agreements.

#### 4. Feature Roadmap Dependency
If the product needs a capability ElevenLabs doesn't support, you're blocked or forced to build workarounds. Triage-specific conditional logic is the most likely friction point.

---

### Mitigation Strategy — The Abstraction Layer

**Design principle: We own the conversation logic. Vendors provide channels and voice only.**

The confirmed architecture uses Twilio as the messaging/channel layer and Claude API for conversation logic. ElevenLabs is used only for voice synthesis — it never touches patient data. Every vendor sits behind an abstraction so it can be swapped without rebuilding.

```
┌─────────────────────────────────────────────────────────┐
│                   CHANNEL LAYER                          │
│            (Twilio — WhatsApp, SMS, Voice)               │
│                                                          │
│  Receives parent messages, delivers AI responses         │
│  Battle-tested, HIPAA-eligible, DPA available            │
│  Swappable: Meta WhatsApp Business API, 360dialog, etc.  │
└──────────────────────────┬──────────────────────────────┘
                           │ Incoming message
                           ▼
┌─────────────────────────────────────────────────────────┐
│            LEARNED GEEK CORE PLATFORM                    │
│                  (We own this fully)                     │
│                                                          │
│  Webhook Receiver → Session Manager → Claude API         │
│  PubMed RAG Layer (symptom keywords only — no PII)       │
│  Triage Classification Engine                            │
│  Data Storage (region-configurable)                      │
│  Physician Dashboard                                     │
│  Admin Interface                                         │
│  Multi-tenant Management                                 │
│  Billing / Subscription Layer                            │
└──────────────────────────┬──────────────────────────────┘
                           │ Response text (no PII)
                           ▼
┌─────────────────────────────────────────────────────────┐
│                   VOICE LAYER (Optional)                  │
│              (ElevenLabs TTS — voice only)                │
│                                                          │
│  Receives AI response text only — never patient data     │
│  Swappable: Azure Cognitive Services, Google TTS, etc.   │
└─────────────────────────────────────────────────────────┘
```

**What this means in practice:**
- We own all conversation logic, triage reasoning, and session management — not a vendor
- ElevenLabs never sees patient symptoms, child names, or parent details — only the AI's response text
- The RAG and medical knowledge layer is entirely ours — not any vendor's built-in knowledge base
- The physician dashboard queries our database, not any vendor API
- Twilio is the channel — swappable for Meta WhatsApp Business API or 360dialog if needed
- Claude API is swappable for Bedrock (regional) or Vertex AI without rebuilding
- Voice synthesis is optional and swappable — text-only WhatsApp is the MVP

---

### Compute Strategy — Azure Functions vs. App Service

Not all components need always-on compute. The triage system handles bursts (parent messages after hours, dashboard queries) — not constant traffic. A hybrid approach optimizes cost while maintaining response time for medical use cases.

#### Hybrid Model

| Component | Compute | Rationale |
|---|---|---|
| Twilio webhook receiver | Azure Functions (Premium, 1 warm instance) | Event-driven, bursty. Premium plan eliminates cold start for first request. Twilio has generous timeout windows but medical context demands fast response. |
| PubMed RAG queries | Azure Functions (Premium) | Triggered per conversation, not continuous |
| Notification dispatch (SMS alerts to physicians) | Azure Functions (Consumption) | Infrequent, latency-tolerant — cold start acceptable |
| Scheduled tasks (backup, key rotation, compliance checks) | Azure Functions (Consumption, timer trigger) | Periodic, no user-facing latency concern |
| Physician Dashboard (Blazor Server) | App Service | Requires persistent SignalR connection for real-time queue updates. Not suitable for serverless. |
| Admin portal | App Service (shared with dashboard) | Low traffic, can share the dashboard App Service plan |

#### Cost Comparison (Early Months — 1-10 Physicians)

| Approach | Est. Monthly Cost | Notes |
|---|---|---|
| All App Service (B1 tier) | $55-70 | Always-on, paying for idle time nights/mornings |
| All App Service (S1 tier) | $70-100 | Staging slots, auto-scale |
| Hybrid (Functions Premium + App Service B1) | $35-55 | Functions scale to zero for unused triggers, 1 warm instance for webhook |
| All Functions (Consumption) | $10-25 | Cheapest but cold starts on dashboard, no SignalR |

**Recommendation:** Start hybrid — Functions Premium (1 warm instance) for event-driven components, App Service B1 for Blazor dashboard. As physician count grows, scale the Functions plan and move dashboard to S1 for staging slots.

#### Premium Plan — Warm Instances

Azure Functions Premium plan allows pre-warmed instances:
- `FUNCTIONS_WORKER_PROCESS_COUNT` — number of worker processes per instance
- `minimumElasticInstanceCount` — minimum warm instances (set to 1 for always-ready)
- Instances beyond the minimum scale dynamically and scale back to zero
- Cost: ~$0.173/vCPU/hour for pre-warmed instance (significantly less than dedicated App Service)

---

### Infrastructure as Code — Terraform

All Azure infrastructure must be defined in Terraform from Phase 1. No portal-only resources — if it's not in Terraform, it doesn't exist in production.

#### Why Terraform (Not Portal Clicks)

The Azure portal hides significant configuration when provisioning resources — role assignments, networking rules, default security settings, managed identity bindings. This creates three problems:
1. **Reproducibility** — can't reliably create dev/staging/production parity
2. **Auditability** — can't show Martin's legal team exactly what infrastructure exists
3. **Data residency** — if we need to replicate in São Paulo, manual setup is error-prone and slow

#### Terraform Workflow

```
1. Define resources in .tf files (version controlled in Git)
2. terraform plan → preview what will be created/changed
3. terraform apply → provision infrastructure
4. State stored in Azure Storage Account (remote backend, encrypted)
```

#### For Existing Resources (TXT-GEEK POC)

The TXT-GEEK POC was provisioned via portal. To bring under Terraform management:

```bash
# 1. List what exists in the resource group
az resource list --resource-group <rg-name> --output table

# 2. Show detailed config for each resource (reveals hidden settings)
az resource show --ids <resource-id> --output json

# 3. Export ARM template (baseline reference)
az group export --resource-group <rg-name> --output json > exported.json

# 4. Write Terraform .tf files based on what you discover
# 5. Import each existing resource into Terraform state
terraform import azurerm_function_app.txtgeek <resource-id>
terraform import azurerm_storage_account.txtgeek <resource-id>
# ... repeat for each resource

# 6. Run terraform plan — diff should show zero changes if .tf matches reality
terraform plan

# 7. From this point forward, all changes go through Terraform
```

#### Resource Map — DrOk Production

| Resource | Terraform Resource Type | Phase |
|---|---|---|
| Resource Group | `azurerm_resource_group` | 1 |
| Azure Functions (Premium) | `azurerm_function_app` + `azurerm_app_service_plan` | 1 |
| App Service (Blazor Dashboard) | `azurerm_app_service` | 1 (provisioned) / 4 (deployed) |
| PostgreSQL Flexible Server | `azurerm_postgresql_flexible_server` | 3 |
| Azure Key Vault | `azurerm_key_vault` | 1 |
| Storage Account (blobs, Function state) | `azurerm_storage_account` | 1 |
| Application Insights | `azurerm_application_insights` | 1 |
| Managed Identity | `azurerm_user_assigned_identity` | 1 |
| Key Vault access policies | `azurerm_key_vault_access_policy` | 1 |
| DNS Zone + records | `azurerm_dns_zone` | 1 |
| TLS Certificate | `azurerm_app_service_certificate` or Let's Encrypt | 1 |

#### Environments

| Environment | Purpose | Terraform Workspace |
|---|---|---|
| dev | Local development + testing | `dev` |
| staging | Pre-production, UAT with Martin | `staging` |
| production | Live physician-facing | `prod` |

Each environment uses the same `.tf` files with environment-specific `.tfvars` for sizing, SKUs, and region. Staging mirrors production config at smaller scale.

#### TXT-GEEK POC — Terraform Learning Exercise

Before building DrOk infrastructure, use the existing TXT-GEEK POC as a practice run:
1. Export current resource state via Azure CLI
2. Write Terraform definitions to match
3. Import into Terraform state
4. Verify `terraform plan` shows no drift
5. Practice a change (e.g., add a tag, adjust a setting) through Terraform
6. Document lessons learned for DrOk Phase 1 provisioning

This validates the workflow on a low-stakes environment before applying it to the production medical platform.

---

### Recommended Path to Productization

#### Phase 1 — First Client (Martin): Twilio + Claude API + ElevenLabs TTS
We own the conversation logic from day one. Twilio handles WhatsApp, Claude handles reasoning, ElevenLabs provides voice quality when needed. The existing Twilio framework reduces build effort — this is extension, not greenfield.

#### Phase 2 — Second Client Onward: Regional Optimization
If data residency becomes a concern for additional markets, move Claude API calls to AWS Bedrock (São Paulo) or Google Vertex AI (São Paulo). Database already in configurable region. No architecture change required.

#### Phase 3 — At Scale: Evaluate Self-Hosted Components
If the product reaches meaningful scale, evaluate:
- **STT:** OpenAI Whisper (self-hosted) or Deepgram — for voice-to-text input
- **LLM:** Continue Claude API / Bedrock, or fine-tuned open source model for cost optimization
- **TTS:** Continue ElevenLabs API (best voice quality) or Azure Cognitive Services for margin improvement
- **WhatsApp:** Direct Meta WhatsApp Business API via 360dialog for lower per-message cost

This gives full control, better margins, and true white-label capability.

---

### What to Build Now That Protects Future Flexibility

| Decision | Why It Matters |
|---|---|
| Own all conversation logic via Claude API — no vendor agent platform | We control triage reasoning, session flow, and guardrails completely |
| ElevenLabs receives response text only — never patient data | Minimizes data exposure, simplifies compliance, makes voice layer swappable |
| Build our own RAG layer (PubMed + physician-curated) | Knowledge is our IP, geographically appropriate, physician-controlled |
| Store all conversation data in our own database (region-configurable) | We own the data, can host in São Paulo if residency required |
| Abstract channel (Twilio), LLM (Claude), and voice (ElevenLabs) behind interfaces | Every vendor is swappable without rebuilding core platform |
| Design physician dashboard against our API only | Dashboard works regardless of channel or LLM provider |
| Build multi-tenant from the start | Productization requires it — retrofitting is painful |
| Use Claude API directly for POC/MVP, Bedrock as regional fallback | Configuration change, not architecture change, if residency becomes hard requirement |
| Hybrid compute — Azure Functions for event-driven, App Service for dashboard | Optimizes cost at low physician counts; scales naturally as usage grows |
| All infrastructure in Terraform from Phase 1 | Reproducible environments, auditable config, enables regional replication for data residency |
| Dual-layer encryption (TLS + field-level AES-256-GCM) | HIPAA safe harbor, Ley 29733 compliance, protects against DBA access and cloud provider breach |

---

## Phasing Summary

| Phase | Deliverable | Timeline | Notes |
|---|---|---|---|
| 0 | POC — ElevenLabs webhook integration | 1–2 weeks | Internal only, not billable to client yet |
| 1 | Discovery & specification | 2–3 weeks | Billable — paid scoping phase |
| 2 | Infanzia Product Chatbot | 4–6 weeks | Lower risk, faster revenue |
| 3 | Physician Triage System — Backend + Webhook | 6–8 weeks | Core data infrastructure |
| 4 | Physician Dashboard | 4–6 weeks | Web interface for physicians |
| 5 | OpenEvidence / RAG Integration | 2–4 weeks | Dependent on API access |
| 6 | Multilingual expansion | 2–3 weeks | Per market demand |
| 7 | Retainer / Maintenance | Ongoing | Monthly recurring |

---

## Pricing Framework (Internal — Do Not Share)

| Item | Est. Range |
|---|---|
| Discovery & Specification | $2,500 – $3,500 |
| Infanzia Product Chatbot (full build) | $8,000 – $10,000 |
| Physician Triage System | $18,000 – $22,000 |
| Physician Dashboard | $6,000 – $8,000 |
| **Total Build Estimate** | **$34,500 – $43,500** |
| Monthly Retainer (maintenance + hosting + KB updates) | $500 – $1,200/mo |

> Note: These are internal estimates only. Final proposal pricing will be determined after discovery phase and Martin's process diagram review.

> **Pricing strategy consideration:** The client-facing proposal currently shows the full $34.5K–$43.5K investment summary upfront. Since Martin's budget has not been discussed (B001 still open), consider whether to present a Discovery-only proposal first ($2,500–$3,500) and defer full build pricing until after discovery — when the client is invested, the spec is concrete, and the value is tangible. Showing the full number before budget alignment risks sticker shock before relationship trust is established.
