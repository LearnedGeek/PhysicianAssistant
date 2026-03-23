# The Authenticity Boundary — How DrOk Handles AI Uncertainty
## For Dr. Martín Núñez — Discovery Discussion Document

**Prepared by:** Learned Geek LLC
**Date:** March 2026
**Purpose:** To explain clearly and honestly how the system handles the most critical technical risk in AI-assisted clinical triage: the AI generating confident output that is not supported by evidence.

---

## The Problem, In Plain Terms

AI systems — including the most advanced ones — have a failure mode that is well documented and architecturally understood. When the system does not have sufficient evidence to answer a question, rather than saying "I don't know," it will often generate a confident, plausible-sounding response anyway. In a consumer product, this is an inconvenience. In a pediatric triage system, this is unacceptable.

We call this the **authenticity boundary**: the principle that the system must never express more confidence than its evidence actually supports. Crossing that boundary — generating a clinical impression without grounding it in retrieved medical evidence — is the primary failure mode this architecture is designed to prevent.

This is not a theoretical concern. It is the most important engineering problem in this project, and it has shaped every architectural decision in the system.

---

## How the System Prevents It — Two Gates

The architecture uses two independent safeguards. Neither is sufficient alone. Together they make confabulation both rare and catchable.

---

### Gate 1 — The Evidence Gate (before the physician sees anything)

**Step 1 — Retrieval confidence check.**
Before Claude generates any clinical impression, the system searches PubMed for relevant medical evidence. If the retrieved results are not sufficiently relevant to the parent's specific query — or if nothing relevant is retrieved at all — the system does not proceed to impression generation. It flags the case as "Insufficient clinical evidence — physician review required" and queues it without an AI impression.

This is not a failure. It is the correct behavior. A null impression is better than a fabricated one.

**Step 2 — Citation enforcement.**
When evidence IS retrieved and Claude generates a clinical impression, every factual claim in that impression must cite a specific PubMed article (PMID). The system parses Claude's output and verifies that each claim maps to a retrieved source. Claims that cannot be attributed are stripped. If the entire impression cannot be attributed, it is replaced with null.

Claude cannot make a clinical statement it cannot source. This is enforced by code, not by instruction alone.

**Step 3 — Low-temperature generation.**
Clinical impression generation runs at the lowest creative setting. The system trades fluency for factual conservatism. It will say less and say it more carefully.

---

### Gate 2 — The Physician Gate (VoBo — Visto Bueno)

Everything that passes Gate 1 still requires physician review before any clinical decision is made. This is not optional and cannot be bypassed.

For urgent and emergency cases: the case is locked in the queue until the physician completes their review and signs off.

For routine cases: a statistically significant sample is reviewed — following the Pareto principle you identified in your responses.

**The physician override rate is tracked from day one.** If the AI impression is overridden or corrected more than approximately 15–20% of the time, the system is not performing acceptably and goes back to testing. This threshold is a go-live gate, not a post-launch metric.

---

## The Emergency Pathway — Different Architecture, Different Rules

The emergency detection pathway operates under completely different rules from everything else described above.

**The LLM has zero creative latitude in emergency detection.**

When a parent's message contains emergency indicators — specific symptoms, critical lab result language, or explicit distress signals — the system responds with a hardcoded, pre-written message. Claude does not write that response. No retrieval occurs first. No clinical impression is generated. The response is immediate and deterministic:

> *"Lo que describes suena como una emergencia médica. Por favor llame al 112 / vaya a urgencias AHORA. No espere la respuesta del médico."*

Simultaneously, the physician receives an SMS alert.

**The emergency keyword list is designed to be over-inclusive, not precise.**

A false positive — sending a non-emergency to the ER — is an inconvenience. A false negative — failing to flag a real emergency — is the scenario this entire architecture exists to prevent. When in doubt, the system escalates. Every time. There is no "probably not an emergency" path.

---

## What We Need From You — Your Clinical Ownership

The emergency keyword list is the most clinically consequential artifact in the system. It is a medical document, not a technical one. **You must own it.**

Before the system goes live:

1. **You define the emergency keyword list** — in Spanish, reflecting the language parents actually use, not medical terminology. "No puede respirar" not "disnea." "Se puso morado" not "cianosis."

2. **You define the critical lab value thresholds** — the specific numeric boundaries that trigger the emergency pathway for results parents report (hemoglobin, glucose, etc.). These are clinical decisions, not engineering decisions.

3. **You sign off formally on the emergency detection logic** before go-live. This sign-off is documented and kept on file. It establishes that the clinical boundaries of the system were set by a physician, not by software engineers.

4. **You participate in UAT testing** — running realistic scenarios through the system, including ambiguous cases that are close to but not clearly emergencies. The system must be stress-tested by someone who knows what a real emergency looks like.

This is not administrative process. It is the mechanism by which a physician takes clinical ownership of a system operating on their behalf.

---

## The Disclosure Requirement

The system is required by Anthropic's Acceptable Use Policy to disclose AI involvement at the start of every conversation. Every new conversation thread opens with:

> *"Este servicio usa inteligencia artificial para asistir al Dr. [Nombre]. Toda la información será revisada por el médico antes de tomar cualquier decisión clínica."*

Parents always know they are talking to an AI assistant, not directly to you.

---

## Summary — What the System Will and Will Not Do

| The system WILL | The system WILL NOT |
|---|---|
| Search PubMed for evidence before generating any clinical impression | Generate a clinical impression without a citable source |
| Flag cases with insufficient evidence for physician review without an impression | Present a fabricated impression as evidence-based |
| Detect emergency indicators immediately and respond with a hardcoded escalation | Use AI judgment to decide whether something is an emergency |
| Require physician sign-off on every urgent and emergency case | Allow a case to close without physician review |
| Track physician override rates as a live accuracy signal | Continue operating if override rates exceed acceptable thresholds |
| Disclose AI involvement at the start of every conversation | Represent itself as the treating physician |

---

## Discovery Agenda Items — This Topic

The following items require your input during Discovery:

| # | Item | Why It Matters |
|---|---|---|
| AB001 | Review and finalize emergency keyword list in parent-language Spanish | Defines the emergency detection boundary |
| AB002 | Define critical lab value numeric thresholds for emergency routing | Determines when a reported result triggers immediate escalation |
| AB003 | Agree on physician override rate threshold for go-live gate | Sets the accuracy standard the system must meet before launch |
| AB004 | Agree on VoBo sampling rate for routine cases | Balances physician workload with quality assurance |
| AB005 | Formal UAT participation commitment — realistic scenarios including ambiguous cases | Required for go-live sign-off |

---

*Learned Geek LLC — for discussion with Dr. Martín Núñez*
