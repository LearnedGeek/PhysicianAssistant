# IP Registration & AI Ownership — Peru/US Analysis
## Prepared by Erika Medina Valencia (Estudio Muñiz)
**Date:** March 29, 2026
**Source:** IP.docx (original in Spanish)

---

## Key Findings

### 1. AI Cannot Own IP — Either Country

Neither Peru nor the US recognizes AI as an author or inventor. Both require **human authorship** as a prerequisite for IP protection. This is settled law in both jurisdictions.

- **Peru:** Decreto Legislativo 822 defines "Author" as a "natural person who carries out the intellectual creation." Ley 32314 (2025) reinforces human authorship in the Penal Code.
- **US:** USPTO requires natural persons as named inventors. USCO rejects copyright registration for AI-generated works without "significant human authorship."
- **TLC (Peru-US Trade Agreement):** Chapter 16 covers IP rights but contains no provisions recognizing AI systems as subjects of law.

**Implication for DrOk:** Mark (as the human developer) is the IP owner. The platform code, architecture, and technical design are Mark's intellectual property. AI-generated clinical impressions (Claude's output) are NOT copyrightable in either country.

### 2. Three Ways to Register AI/Software IP in Peru

**A. Software Registration (Copyright) — Recommended**
- Register source code and object code as a "literary work" with Indecopi (Dirección de Derecho de Autor)
- **Base:** Article 69, Decreto Legislativo 822
- Protects: the code expression, technical documentation, user manuals
- Does NOT protect: the underlying algorithm or mathematical method
- Registration is not required (protection exists automatically) but recommended for proof of authorship

**B. Patent of Invention (Industrial Property)**
- Possible if the AI solves a technical problem through novel software-hardware interaction
- Software "as such" is NOT patentable (Decreto Legislativo 1075, CAN Decision 486)
- Must demonstrate: novelty, inventive step, and industrial application
- Example: AI system that optimizes medical image processing could qualify
- DrOk's triage routing and confidence scoring MAY qualify as a "technical effect" — worth exploring

**C. Works "Created With" AI**
- AI outputs (text, images, clinical impressions) cannot be registered with AI as author
- Can ONLY be registered if a human used the AI as a tool and maintained creative control
- Works generated entirely by AI have no copyright protection in Peru — they enter public domain

### 3. New Obligations Under Ley 31814 (AI Law) and Reglamento D.S. 115-2025-PCM

- Developers must **respect third-party copyrights** in AI training data
- **Algorithmic transparency** required for "high-risk" systems — must inform users about AI capabilities and decision types
- Trade secrets are still protected — transparency does not require disclosing proprietary algorithms
- **Indecopi** is the authority for IP-related AI complaints

### 4. Jurisprudence

Indecopi evaluates patents based on the **inventive step contributed by human inventors**. AI/computational systems are treated as technical support, not as claimants of rights (reference: "Burbuja Artificial Neonatal" case).

---

## Action Items for DrOk

| # | Action | Priority | Owner |
|---|---|---|---|
| 1 | Register platform source code with Indecopi as software copyright | High | Mark |
| 2 | Evaluate whether triage routing/confidence scoring qualifies for patent as "invention implemented by computer" | Medium | Mark + Erika |
| 3 | Ensure AI training data does not infringe third-party copyrights | High | Mark |
| 4 | Document human creative control over all platform design decisions | Medium | Mark |
| 5 | Assess whether DrOk qualifies as "high-risk" AI under Ley 31814 — if so, prepare transparency documentation | Medium | Mark + Carlos |

---

*Translated from Spanish original (IP.docx) prepared by Erika Medina Valencia, Estudio Muñiz, Lima.*
