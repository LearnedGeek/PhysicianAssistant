# Pre-Discovery Action Items — Dr. Núñez

**From:** Mark McArthey, Learned Geek LLC
**Date:** March 18, 2026
**Purpose:** Items that need your input or action before or during our Discovery phase. These will help us move efficiently once we begin.

---

Martin,

Thank you for the detailed responses to our initial questions — they were thorough and precise, and they've shaped the technical direction significantly. We've been working through the architecture, compliance requirements, and project planning on our end, and we're at a point where we need your help on several items that only you can drive from Peru.

There's no rush on all of these at once, but I wanted to get them in front of you so you can start thinking through them — and so we can prioritize the most critical ones for our Discovery call.

---

## Priority 1 — Need Answers Before We Build

These items could change the scope, architecture, or feasibility of the project. We need clarity on them before committing to a build plan.

### 1.1 — Legal Counsel (Peruvian Data Privacy)

We need a Peruvian attorney who specializes in data privacy (Ley 29733) and ideally health data regulation. The system will store longitudinal pediatric health records and transfer data to US-based cloud services (Anthropic, Twilio). Specific questions for counsel:

- **APDP Registration:** Does the system require registration with the Autoridad Nacional de Protección de Datos Personales? Who registers — you as the physician (data controller) or us as the technology provider (data processor)?
- **Cross-border data transfer:** What legal mechanism is required for transferring pediatric health data to the US? Is explicit consent sufficient, or is a formal adequacy determination needed?
- **Consent for minors:** Who can legally consent to a child's health data being processed? Legal guardian only? Both parents? Are there age thresholds?
- **Data retention vs. right of erasure:** If a parent requests their child's data be deleted, does that conflict with medical record retention requirements?

**What I need from you:** Can you recommend a Peruvian attorney who handles this? If not, I can research firms, but someone in your professional network who understands both health data and Ley 29733 would be ideal. This is the single most important item on this list.

### 1.2 — Medical Device / Regulatory Classification

This is a question we need answered by legal counsel, but I want you aware of it: **could DIGEMID classify this system as a medical device?** The AI generates a clinical impression (impresión diagnóstica) based on symptoms and lab values. Some jurisdictions classify AI clinical decision support tools as medical devices, which would require registration and regulatory approval.

Similarly: **does this system fall under Peru's telemedicine law (Ley 30421)?** An AI communicating with patients on behalf of a physician during off-hours may be considered telemedicine.

**What I need from you:** Raise both questions with the legal counsel. If the answer to either is "yes" or "maybe," we need to understand the requirements before building — not after.

### 1.3 — Franchise Agreement Restrictions

Does your Infanzia / Kezer-Lab franchise agreement restrict or require approval for deploying AI tools that interact with customers on behalf of the brand?

**What I need from you:** Review your franchise agreement for any technology deployment clauses, or check with your franchise contact.

### 1.4 — Budget Range

We haven't discussed budget yet, and I want to be respectful of your time by preparing the right-sized proposal. The full system (product chatbot + physician triage system + dashboard) represents a significant investment. I can prepare tiered options — from a lean MVP to the full vision — but knowing your range helps me focus.

**What I need from you:** A general budget range or comfort level. This doesn't commit you to anything — it helps me structure the proposal so we're not wasting time on options that don't fit.

### 1.5 — Timeline Expectations

What is your target for having the first system operational? Are there any external deadlines driving this (conference, franchise review, patient volume milestone)?

---

## Priority 2 — Need During Discovery Phase

These are items we'll work through together during Discovery, but thinking about them now will make the call more productive.

### 2.1 — Critical Lab Value Thresholds

The system needs to detect critical lab results and trigger the emergency pathway. You mentioned this in your response to Q6. We need specific numeric thresholds for the pediatric values you consider critical — for example:

- Hemoglobin: below what value is critical for a child of age X?
- Glucose: what range triggers emergency?
- Any other lab values you want the system to flag?

**What I need from you:** A list of lab values with critical thresholds, ideally by age group. This can be a simple table — it doesn't need to be formal. We'll build the detection logic from your clinical expertise.

### 2.2 — VoBo Sampling Rate

You mentioned that routine (non-urgent) cases don't need 100% physician review — a statistically significant sample is sufficient. What sampling rate do you want to start with? 20%? 30%? Should it vary by category (e.g., higher sampling for cases involving images)?

### 2.3 — Physician Network

You've mentioned other physicians beyond yourself. Is the network of potential users identified and in contact with you, or is it aspirational at this stage? This affects whether we build multi-tenant from day one or add it later.

### 2.4 — US Market Status

You were at Expo West in Anaheim when we first connected. Is there active US market interest for Infanzia products, and would the product chatbot need to support English and US-based parents?

### 2.5 — Product Documentation

For the Infanzia Product Chatbot (Workstream A), we'll need the complete product catalog to build the knowledge base:

- Product sheets for Biomilk, Infabiotix, Infavit, and all other products
- Dosing tables / recommended usage
- Ingredient lists and certifications (EU certifications, manufacturing details)
- FAQs you commonly answer
- Any clinical references you want the chatbot to cite

**What I need from you:** Start gathering these. Digital formats (PDF, Word) are fine. The more complete the documentation, the more accurate the chatbot.

---

## Priority 3 — For Your Awareness

Items we're handling on our end, but you should know about.

### 3.1 — Vendor Compliance Review

We're reviewing the acceptable use policies of Anthropic (the AI provider) and Meta/WhatsApp to confirm that medical/clinical use is permitted. We don't anticipate issues, but we're verifying before building on these platforms.

### 3.2 — Data Processing Agreements

We'll execute Data Processing Agreements (DPAs) with Twilio and Anthropic before any production deployment. Your legal counsel may want copies of these for their compliance review.

### 3.3 — Insurance Review

We're reviewing our professional liability and E&O insurance coverage to ensure it extends to AI-assisted medical systems and international clients. We'll share relevant certificates if your counsel requests them.

### 3.4 — WhatsApp Business Approval

WhatsApp Business API approval can take 1–4 weeks. We'll begin this process at the start of Discovery so it doesn't become a bottleneck.

---

## Discovery Call

I'd like to schedule our Discovery call at your convenience. I know I said "sin prisa, a tu ritmo" — and that still applies to the overall pace — but getting the Discovery conversation started soon will let us address the legal and regulatory questions early, which is where the real timeline risk lives. The build is straightforward; the compliance groundwork takes longer.

**Suggested agenda:**
1. Review these action items — confirm priorities, clarify any questions
2. Budget and timeline alignment
3. Legal counsel — next steps and timeline
4. Critical lab value thresholds — initial discussion
5. Product documentation — what's available now vs. what needs to be created
6. Physician network — current status and expansion timeline
7. Next steps and cadence going forward

Please suggest 2–3 times that work for you. I'm flexible across time zones.

---

Looking forward to the next step.

Mark McArthey
Learned Geek LLC
markm@learnedgeek.com
