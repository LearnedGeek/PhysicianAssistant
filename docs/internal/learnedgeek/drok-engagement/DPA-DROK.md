# Data Processing Agreement
## DrOk Project — Learned Geek LLC & Dr. Martín Núñez

**Under Master Services Agreement between:**
- **Data Controller:** Dr. Martín Núñez ("Controller")
- **Data Processor:** Learned Geek LLC ("Processor")

**DPA Effective Date:** _______________

---

## 1. Purpose and Scope

1.1 This Data Processing Agreement ("DPA") governs the processing of personal data by Processor on behalf of Controller in connection with the DrOk platform — an AI-powered physician triage and patient communication system.

1.2 This DPA supplements the Master Services Agreement ("MSA") between the Parties. In the event of conflict between this DPA and the MSA regarding data processing, this DPA governs.

---

## 2. Definitions

2.1 **Personal Data:** Any information relating to an identified or identifiable natural person, including but not limited to: name, phone number, health information, demographic data, and communication content.

2.2 **Sensitive Personal Data:** Personal data relating to health, medical conditions, symptoms, clinical impressions, lab results, and any data concerning minors.

2.3 **Processing:** Any operation performed on Personal Data, including collection, storage, retrieval, transmission, analysis, and deletion.

2.4 **Sub-Processor:** Any third party engaged by Processor to process Personal Data on behalf of Controller.

2.5 **APDP:** Autoridad Nacional de Protección de Datos Personales (Peru).

---

## 3. Roles and Responsibilities

3.1 **Controller** determines the purposes and means of processing Personal Data. Controller is responsible for:
- Obtaining valid, informed, and explicit consent from data subjects (or their legal guardians for minors) prior to data collection
- Compliance with Peruvian data protection law (Ley 29733) and healthcare records law (Ley 30024)
- Registration of the data bank with the APDP
- Clinical decisions made using or informed by processed data

3.2 **Processor** processes Personal Data solely on behalf of and under the documented instructions of Controller. Processor is responsible for:
- Implementing appropriate technical and organizational security measures
- Processing data only as instructed by Controller and as necessary to perform the Services
- Assisting Controller with regulatory compliance obligations, including providing technical documentation

---

## 4. Lawful Basis for Processing

4.1 Controller shall ensure that a valid legal basis exists for all processing, including explicit consent from data subjects or their legal guardians.

4.2 For minors under 14 years of age, consent must be provided by a parent or legal guardian as required by Peruvian law.

4.3 For minors aged 14 and above, consent may be provided by the minor if the information and language are age-appropriate, subject to Controller's clinical judgment and applicable law.

---

## 5. Categories of Data and Data Subjects

| Category | Data Types |
|---|---|
| Patient identification | Name, phone number, parent/guardian name |
| Demographic data | Age, date of birth |
| Health data | Symptoms, medical history, lab results, clinical impressions |
| Communication data | WhatsApp messages, timestamps, conversation history |
| Physician data | Name, phone number, practice information, availability |

**Data subjects:** Pediatric patients (via parent/guardian), parents/guardians, physicians.

---

## 6. Sub-Processors

6.1 Controller authorizes Processor to engage the following Sub-Processors:

| Sub-Processor | Purpose | Data Location |
|---|---|---|
| Anthropic | AI language model API (Claude) | United States |
| Twilio | WhatsApp Business API, SMS | United States |
| [Cloud Provider TBD] | Database hosting, application hosting | [Region TBD] |
| NCBI/PubMed | Medical literature retrieval | United States |

6.2 Processor shall notify Controller in writing before adding or replacing a Sub-Processor. Controller may object within fifteen (15) days of notification.

6.3 Processor shall ensure that each Sub-Processor is bound by data protection obligations no less restrictive than those in this DPA.

---

## 7. International Data Transfer

7.1 Personal Data will be transferred from Peru to the United States for processing. The Parties acknowledge that the United States has not been determined by the APDP to provide an adequate level of data protection.

7.2 To ensure adequate protection for cross-border transfers, the Parties agree to the following mechanisms:

**(a) Standard Contractual Clauses (SCCs).** The Parties shall execute Standard Contractual Clauses (Annex A) obligating Processor and each Sub-Processor to maintain security and confidentiality standards equivalent to Peruvian law.

**(b) Explicit Consent.** Controller shall obtain explicit, informed, written consent from each data subject (or legal guardian) for the international transfer of their data, specifying the destination country and purpose.

**(c) APDP Notification.** Controller shall declare the international transfer to the APDP as part of the data bank registration or update process.

**(d) Transfer Impact Assessment.** Processor shall conduct and document a Transfer Impact Assessment ("TIA") evaluating whether U.S. law (including surveillance authorities) permits Processor to comply with the SCCs. The TIA shall be made available to Controller upon request.

---

## 8. Security Measures

Processor shall implement and maintain appropriate technical and organizational measures to protect Personal Data, including:

8.1 Encryption of data in transit (TLS 1.2+) and at rest (AES-256 or equivalent).

8.2 Access controls with role-based permissions and principle of least privilege.

8.3 Audit logging of all access to Personal Data.

8.4 Secure authentication for all system users.

8.5 Regular security assessments and vulnerability remediation.

8.6 Logical separation of data between physicians / practices.

8.7 Secure deletion procedures for data that has exceeded its retention period.

---

## 9. Data Retention and Deletion

9.1 Personal Data shall be retained for the minimum period required by applicable law. Under Peruvian healthcare records law (NTS Nº 139-MINSA/2018/DGAIN), clinical records must be retained for a minimum of five (5) years from the last date of care.

9.2 Non-clinical data (marketing preferences, newsletter subscriptions, non-medical contact information) may be deleted upon request by the data subject.

9.3 Upon termination of the Services or this DPA, Processor shall, at Controller's election, return or securely delete all Personal Data within sixty (60) days, except where retention is required by law.

---

## 10. Data Subject Rights (Derechos ARCO)

10.1 Processor shall assist Controller in fulfilling data subject requests for Access, Rectification, Cancellation, and Opposition ("ARCO rights") as required by Ley 29733.

10.2 If Processor receives a data subject request directly, Processor shall forward the request to Controller within five (5) business days without responding to the data subject.

10.3 **Data Blocking.** Where a data subject requests deletion but applicable law requires retention (Section 9.1), the data shall be blocked — retained for legal compliance only, with no further processing or access except for legally mandated purposes or medical emergencies.

---

## 11. Data Breach Notification

11.1 Processor shall notify Controller of any confirmed data breach involving Personal Data without undue delay and in any event within forty-eight (48) hours of becoming aware of the breach.

11.2 Notification shall include: nature of the breach, categories and approximate number of affected data subjects, likely consequences, and measures taken or proposed to mitigate the breach.

11.3 Controller is responsible for notifying the APDP and affected data subjects as required by Peruvian law.

---

## 12. Audit Rights

12.1 Controller may request, no more than once per year with reasonable notice, that Processor provide documentation or evidence demonstrating compliance with this DPA.

12.2 Processor shall make available relevant security certifications, audit reports, or compliance documentation upon reasonable request.

---

## 13. Term and Termination

13.1 This DPA is effective upon execution and remains in force for as long as Processor processes Personal Data on behalf of Controller.

13.2 This DPA survives termination of the MSA to the extent Processor retains any Personal Data.

---

## 14. Governing Law

14.1 Data protection obligations in this DPA are governed by the applicable data protection law of the Controller's jurisdiction (Ley 29733 and its regulations).

14.2 Contractual disputes between the Parties are governed by the MSA.

---

## Signatures

**Data Controller**

___________________________________
Dr. Martín Núñez
Date: ___________________________

**Data Processor — Learned Geek LLC**

___________________________________
Mark M. McArthey, Member
Date: ___________________________

---

## Annex A — Standard Contractual Clauses

*[To be drafted upon confirmation of regulatory requirements from Carlos Rojas / Rebaza, Alcázar & De Las Casas. SCCs shall be substantially similar to EU Standard Contractual Clauses adapted for Peruvian law.]*

---

*Draft only — review with a licensed attorney before use.*
