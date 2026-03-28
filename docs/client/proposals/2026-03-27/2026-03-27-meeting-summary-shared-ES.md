# Resumen de Reunión — 27 de marzo de 2026
## Plataforma DrOk — Llamada de Alineamiento para Discovery

**Fecha:** 2026-03-27
**Duración:** ~35 minutos
**Asistentes:** Mark McArthey (Learned Geek LLC), Dr. Martín Núñez, Carlos Rojas Klauer (Asesor Legal — Innovación y Tecnología), Karen McArthey (Apoyo en Traducción)
**Formato:** Google Meet

---

## 1. Decisiones Tomadas

| # | Decisión | Responsable | Notas |
|---|---|---|---|
| D001 | Martín (o su entidad clínica) es el **responsable del tratamiento de datos**; Learned Geek es el **encargado del tratamiento** | Todos | Carlos confirmó según Ley 29733 |
| D002 | Se requiere un **Acuerdo de Procesamiento de Datos (DPA)** entre Martín y Learned Geek | Mark + Carlos | Debe definir finalidades, seguridad, sub-encargados, devolución/destrucción de datos |
| D003 | El registro ante la ANPDP es obligación de Martín como responsable del tratamiento | Martín | Learned Geek declarado como encargado; Anthropic/Twilio como sub-encargados |
| D004 | **Un padre/tutor legal** es suficiente en la práctica para el consentimiento de datos de menores | Carlos | Ambos padres mantienen autoridad para solicitar cambios |
| D005 | Los registros médicos **no se pueden eliminar** — solo bloquear; los datos brutos deben preservarse | Todos | Ley peruana; el sistema debe mantener trazabilidad completa |
| D006 | Retención de datos: **5 años electrónicos**, 20 años físicos (nos aplica el estándar electrónico) | Martín | Según NTS N° 139-MINSA y Ley General de Salud |
| D007 | La transferencia internacional de datos requiere **trazabilidad** — registro de auditoría de flujos de datos | Martín + Carlos | Ver "Qué Cambió" abajo |
| D008 | La IA proporciona **solo asesoramiento**; el médico mantiene toda la responsabilidad clínica | Todos | El VoBo valida toda la información de la IA |
| D009 | **La clasificación DIGEMID no bloquea el MVP**, pero el registro voluntario tiene valor estratégico a largo plazo | Todos | Carlos prefiere buscar el registro de forma proactiva |

---

## 2. Qué Cambió

Actualizaciones a posiciones anteriores basadas en la discusión de esta reunión.

| Entendimiento Anterior | Entendimiento Actualizado | Fuente |
|---|---|---|
| Se requieren SCCs para la transferencia Perú→EE.UU. (análisis escrito, 23 de marzo) | Posición en la reunión: solo se requiere trazabilidad. **Estas posiciones necesitan reconciliación formal.** | Carlos + Martín en reunión |
| Ambos padres requeridos para datos sensibles transfronterizos (análisis escrito) | Un padre es suficiente en la práctica; ambos mantienen autoridad para solicitar cambios | Carlos en reunión |
| DIGEMID: zona gris, esperar | Carlos **prefiere el registro proactivo** y preguntó a Martín sobre un contacto en una clínica para confirmar el alcance. La ANPDP es rápida; DIGEMID es más lenta. | Carlos en reunión |
| Ley de telemedicina (Ley 30421) no discutida | Carlos la abordó: SÍ existe una ley de telemedicina con limitaciones sobre notificaciones automatizadas, pero no cree que caigamos bajo clasificación de dispositivo médico. Opinión formal pendiente. | Carlos en reunión |
| Plataforma no es un sistema de registro clínico (analogía del Apple Watch) | Martín también reconoció la obligación de "registrarse como agentes que controlan, registran y almacenan historias clínicas" ante el Ministerio de Salud. Posición más matizada de lo capturado inicialmente. | Martín en reunión |
| Cronograma del MVP desconocido | **Julio 2026** propuesto por Carlos — con retroalimentación de usuarios antes del lanzamiento al mercado | Carlos en reunión |

---

## 3. Resumen de Discusión

### Registro ANPDP y Roles de Datos
Carlos confirmó claramente la separación responsable/encargado. Martín es el responsable (determina la finalidad del tratamiento de datos); Learned Geek es el encargado (proporciona la tecnología). Se requiere un DPA. Todos los sub-encargados (Anthropic, Twilio) deben declararse en el registro ante la ANPDP. Carlos señaló que la ANPDP es una autoridad que responde eficientemente.

### Transferencia Internacional de Datos
Martín y Carlos indicaron que Perú requiere trazabilidad de los flujos de datos pero actualmente no tiene restricciones específicas para la transferencia Perú→EE.UU., siempre que se mantenga la trazabilidad. Mark señaló que existen opciones de infraestructura en la nube en América Latina (Azure Brasil) si la residencia de datos se convierte en una preocupación. Mark confirmó que ya revisó las Políticas de Uso Aceptable de Anthropic y Twilio para uso en salud — sin problemas identificados. **Sin embargo:** esta posición es más simple que el análisis escrito de Martín del 23 de marzo, que indicaba que se requieren SCCs. Esto debe reconciliarse formalmente.

### Consentimiento de Menores
Carlos confirmó que un padre es suficiente en la práctica. Se requiere consentimiento expreso dado que son datos de salud sensibles. Ambos padres mantienen autoridad para solicitar cambios o restricciones. El mecanismo de consentimiento debe ser digital, capturado dentro de la plataforma mediante un flujo de confirmación con verificación de identidad.

### Retención e Inmutabilidad de Datos
Los registros médicos no pueden eliminarse bajo la ley peruana. El sistema debe preservar los datos brutos sin modificación. Martín enfatizó la trazabilidad como el requisito central. La retención es de 5 años para registros electrónicos. La posición de Martín es que la plataforma no es un sistema formal de registro clínico (HCE), pero también reconoció una obligación de registro ante el Ministerio de Salud para la gestión de historias clínicas — una posición más matizada que la analogía del Apple Watch por sí sola.

### Clasificación de Dispositivo Médico (DIGEMID)
Esta fue la discusión más matizada. Martín sostiene que el sistema proporciona asesoramiento, no diagnóstico. Carlos reconoció la zona gris pero dijo que **prefiere buscar el registro de forma proactiva** — preguntó a Martín sobre un contacto en una clínica específica para confirmar el alcance. Carlos señaló que DIGEMID es una autoridad más lenta que la ANPDP, lo cual tiene implicaciones para el cronograma. Carlos también abordó la ley de telemedicina (Ley 30421): existen limitaciones sobre notificaciones de sistemas automatizados, pero no cree que esto constituya un dispositivo médico. No bloquea el MVP, pero Carlos quiere claridad.

### Marca DrOk
"DrOk" no está actualmente registrada como marca. Martín reconoció que necesita hacerlo. Las restricciones del acuerdo de franquicia de Infanzia/Kezer-Lab aún necesitan revisión.

### Cronograma y Alcance
Sin fecha límite externa. Carlos propuso **MVP para julio de 2026** con pruebas de usuario y retroalimentación antes del lanzamiento al mercado. Preguntó específicamente sobre el diseño UX/UI, la confianza de los usuarios en canales de IA, y cuánto tardaría la construcción técnica. Martín dijo que puede proporcionar 50-100 médicos para probar el MVP. Martín hizo referencia a la adopción de IA por médicos desde el COVID (especialmente en imágenes diagnósticas) y plataformas existentes (PubMed, OpenEvidence con Cleveland Clinic / Mayo Clinic) como validación del mercado.

### Autenticidad y Seguridad de la IA
Mark describió su investigación de 6 meses sobre la confabulación de la IA y el enfoque basado en citaciones de PubMed. Carlos planteó independientemente la autenticidad de la IA como una preocupación crítica — haciendo referencia a incidentes reales de seguridad con IA. Carlos usó la palabra "veracidad" para describir el concepto. Tanto Carlos como Martín quieren ver el documento de autenticidad. Mark se comprometió a compartirlo.

### Dinámica del Equipo
Carlos validó la división de expertise: Martín (médica), Mark (técnica, privacidad de datos), Carlos (legal). Dijo: "Me gusta que seas cuidadoso con la data porque este negocio necesita esa óptica." Martín reforzó la visión mundial. Mark respondió: "Este es un problema que muchos intentan resolver, pero nadie lo ha logrado todavía. ¿Por qué no nosotros?"

---

## 4. Tareas Pendientes

| # | Responsable | Acción | Fecha | Estado |
|---|---|---|---|---|
| A001 | Mark | Enviar resumen de reunión por correo | 28 de marzo | 🔴 |
| A002 | Mark | Configurar carpeta compartida en Google Drive con documentos del proyecto | Este fin de semana | 🔴 |
| A003 | Mark | Compartir documento de autenticidad de IA | Este fin de semana | 🔴 |
| A004 | Mark | Compartir referencias técnicas e investigación de IA | Este fin de semana | 🔴 |
| A005 | Mark | Preparar propuesta actualizada con alcance y fases | Antes de la próxima reunión | 🔴 |
| A006 | Carlos | Crear grupo de WhatsApp | Esta semana | 🔴 |
| A007 | Carlos | Contactar clínica para determinar alcance de DIGEMID | Por definir | 🔴 |
| A008 | Martín | Revisar e iniciar registro de marca "DrOk" | En proceso | 🟡 |
| A009 | Martín | Revisar y responder preguntas pendientes (franquicia, presupuesto, tasa de VoBo, mercado EE.UU., documentación de productos) | Antes de la próxima reunión | 🔴 |
| A010 | Todos | Programar reunión de seguimiento | Próxima semana (est.) | 🔴 |

---

## 5. Preguntas Pendientes

### Para Carlos (Legal)

| # | Pregunta | Prioridad | Estado |
|---|---|---|---|
| Q-C001 | **Clasificación DIGEMID:** Se necesita opinión formal. Carlos prefiere registro proactivo; necesita contacto de clínica para confirmar alcance. | Alta | 🟡 Carlos trabajando en ello |
| Q-C002 | **Ley 30421 (Telemedicina):** Carlos reconoció la ley con limitaciones sobre notificaciones automatizadas. Se necesita posición formal escrita. | Alta | 🟡 Parcialmente abordado |
| Q-C003 | **SCCs vs. trazabilidad:** El análisis escrito (23 marzo) dice que se requieren SCCs; la reunión dice solo trazabilidad. ¿Cuál es la posición formal? | Alta | ❓ Necesita reconciliación |
| Q-C004 | **RENHICE / Ley 30024:** ¿El sistema constituye una HCE? Posición matizada — necesita aclaración. | Media | 🟡 Parcialmente abordado |
| Q-C005 | **Cláusula de indemnización:** Revisar lenguaje de responsabilidad clínica para carta de compromiso. | Media | 🔴 No iniciado |

### Para Martín (Clínico / Negocio)

| # | Pregunta | Prioridad | Estado |
|---|---|---|---|
| Q-M001 | **Restricciones de franquicia:** ¿El acuerdo de Infanzia/Kezer-Lab tiene restricciones sobre herramientas de IA? | Alta | 🔴 Planteado — sin respuesta |
| Q-M002 | **Rango de presupuesto:** Rango general de presupuesto para el proyecto. | Alta | 🔴 No discutido |
| Q-M003 | **Tasa de VoBo:** % de casos rutinarios para revisión médica. | Media | 🔴 No discutido |
| Q-M004 | **Mercado EE.UU.:** ¿Interés activo? ¿Se necesita soporte en inglés? Se mencionó brevemente turismo médico. | Media | 🟡 Mencionado brevemente |
| Q-M005 | **Documentación de productos:** Fichas técnicas, tablas de dosificación, ingredientes para base de conocimiento del chatbot. | Media | 🔴 No discutido |
| Q-M006 | **Marca DrOk:** Iniciar registro. | Media | 🟡 Reconocido |
| Q-M007 | **Contacto clínica DIGEMID:** Carlos pidió a Martín un contacto en una clínica para confirmar alcance. | Alta | 🔴 Pendiente Martín |

---

## 6. Estacionamiento

Temas mencionados pero no discutidos completamente. Se registran aquí para no perderlos.

| # | Tema | Planteado Por | Notas |
|---|---|---|---|
| P001 | Registro ante Ministerio de Salud como gestores de historias clínicas | Martín | Necesita aclaración — separado del registro ANPDP |
| P002 | Monetización de datos a médicos e instituciones | Martín | Modelo de ingresos a largo plazo discutido en contexto de beneficio del registro DIGEMID |
| P003 | Caso de uso de turismo médico | Martín | Pacientes con médicos en Perú y EE.UU.; registros médicos transfronterizos |
| P004 | Drive compartido para colaboración continua | Carlos | Carlos sugirió Drive; Mark lo configurará este fin de semana |

---

## 7. Documentos

### Compartidos Previamente
| Documento | Compartido Por | Notas |
|---|---|---|
| Items de Acción Pre-Discovery (EN + ES) | Mark | Carlos los revisó; base para la discusión |
| Análisis Legal de Martín (260323 Proyecto DrOk) | Martín | Referenciado durante discusiones de transferencia y consentimiento |

### Por Compartir Este Fin de Semana
| Documento | Responsable |
|---|---|
| Documento de autenticidad de IA | Mark |
| Especificación técnica | Mark |
| Seguimiento del proyecto (compartido) | Mark |

---

## 8. Próxima Reunión

**Fecha tentativa:** Semana del 30 de marzo al 3 de abril
**Agenda propuesta:**
1. Revisar tareas pendientes de esta reunión
2. Carlos: posición formal sobre clasificación DIGEMID y Ley 30421
3. Carlos: reconciliar posición de SCCs vs. trazabilidad para transferencia internacional
4. Martín: restricciones de franquicia, rango de presupuesto, confirmación de cronograma
5. Mark: presentar documento de autenticidad de IA
6. Mark: presentar propuesta de alcance y fases
7. Discutir enfoque seguro para compartir documentos

---

*Preparado por Mark McArthey, Learned Geek LLC*
