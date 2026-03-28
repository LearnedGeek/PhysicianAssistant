# Correo de seguimiento — Reunión del 27 de marzo de 2026

**Para:** Martin Nunez, Carlos Rojas Klauer
**De:** Mark McArthey
**Asunto:** Resumen de reunión + seguimiento del proyecto — DrOk, 27 de marzo

---

Hola Martin, hola Carlos,

Gracias por la reunión de hoy — fue muy productiva. A continuación un resumen de lo que acordamos, las preguntas pendientes, y los próximos pasos. También preparé un documento de seguimiento compartido para que todos tengamos visibilidad sobre el estado del proyecto.

---

## Lo que acordamos hoy

1. **Roles de datos:** Martin es el controlador de datos, Learned Geek es el procesador. Necesitamos un Acuerdo de Procesamiento de Datos (DPA) entre ambos.

2. **Registro ANPDP:** Martin registra el banco de datos. Learned Geek declara los proveedores (Anthropic, Twilio) como sub-procesadores.

3. **Transferencia internacional:** Se requiere trazabilidad de los flujos de datos. Ya revisé las políticas de uso de Anthropic y Twilio — sin problemas para uso en salud.

4. **Consentimiento de menores:** Un padre o tutor legal es suficiente en la práctica. El consentimiento se captura digitalmente en la plataforma.

5. **Retención de datos:** Los registros no se pueden eliminar — solo bloquear. El sistema preserva los datos brutos sin modificación. Trazabilidad completa.

6. **Clasificación DIGEMID:** Estamos en una zona gris. No es un bloqueo para el MVP, pero hay beneficio estratégico a largo plazo en registrar voluntariamente. Lo seguimos evaluando.

7. **Responsabilidad clínica:** La IA genera recomendaciones; el médico mantiene la responsabilidad clínica. El VoBo del médico valida toda la información.

---

## Documentos en Google Drive

Este fin de semana voy a preparar un espacio compartido en Google Drive con los siguientes documentos:

| # | Documento | Descripción |
|---|---|---|
| 1 | Resumen de reunión (27 marzo) | Adjunto a este correo, en inglés y español |
| 2 | Documento de autenticidad de IA | Cómo el sistema previene la confabulación — resultado de 6 meses de investigación |
| 3 | Seguimiento del proyecto | Documento compartido con todas las decisiones, preguntas, y tareas pendientes |
| 4 | Especificación técnica | Arquitectura del sistema, flujos de datos, medidas de seguridad |
| 5 | Items de acción pre-Discovery (original) | El documento que Martin ya revisó, para referencia de Carlos |

Les compartiré el enlace del Drive cuando esté listo.

---

## Preguntas pendientes — Para Carlos

Carlos, estas son preguntas legales que necesitan tu opinión formal. Sin prisa, pero son importantes para avanzar:

| # | Pregunta | Prioridad |
|---|---|---|
| C1 | **Clasificación DIGEMID:** Entiendo que prefieres avanzar con el registro proactivamente. Martin, ¿puedes compartir el contacto de la clínica que Carlos mencionó para confirmar el alcance? | Alta |
| C2 | **Ley 30421 (Telemedicina):** En la reunión mencionaste que hay limitaciones sobre notificaciones de sistemas automatizados, pero que no crees que estemos ante un dispositivo médico. ¿Puedes confirmar esa posición por escrito? | Alta |
| C3 | **Transferencia internacional — aclaración:** En el análisis escrito del 23 de marzo, Martin mencionó que se necesitan Cláusulas Contractuales Tipo (SCCs) para la transferencia Perú→EE.UU. En la reunión, acordamos que solo se requiere trazabilidad. ¿Cuál es la posición formal? Necesitamos reconciliar estas dos posiciones antes de tomar decisiones de arquitectura. | Alta |
| C4 | **RENHICE / Ley 30024:** Martin mencionó la analogía del Apple Watch, pero también reconoció la obligación de registrarse como gestor de historias clínicas ante el Ministerio de Salud. ¿Son estas obligaciones separadas? ¿Cuál aplica? | Media |
| C5 | **Cláusula de indemnización:** Cuando preparemos la carta de compromiso, necesitaremos revisar el lenguaje de responsabilidad clínica. | Media |

## Preguntas pendientes — Para Martin

Martin, estas preguntas son del documento de action items original que todavía necesitan respuesta:

| # | Pregunta | Prioridad |
|---|---|---|
| M1 | **Franquicia:** ¿El acuerdo de Infanzia/Kezer-Lab tiene restricciones sobre herramientas de IA con clientes? | Alta |
| M2 | **Presupuesto:** ¿Cuál es el rango general de presupuesto para el proyecto? Esto me ayuda a preparar una propuesta del tamaño correcto. | Alta |
| M3 | **Tasa de VoBo:** ¿Qué porcentaje de casos rutinarios debe revisar el médico? (¿20%? ¿30%?) | Media |
| M4 | **Mercado EE.UU.:** Mencionaste turismo médico y pacientes con médicos en ambos países. ¿Hay interés activo en el mercado estadounidense? | Media |
| M5 | **Documentación de productos:** Fichas técnicas, tablas de dosificación, ingredientes, FAQs de productos Infanzia. Necesarias para el chatbot. | Media |
| M6 | **Marca DrOk:** Confirmar que se está avanzando con el registro de marca. | Media |

## Tareas pendientes — Mark (para su información)

Para que sepan lo que estoy trabajando de mi lado:

| # | Tarea | Estado |
|---|---|---|
| K1 | Seguro de responsabilidad profesional (E&O + Cyber) | En proceso — cotizaciones solicitadas |
| K2 | Revisión de políticas de WhatsApp Business y Twilio | Pendiente |
| K3 | Acuerdos de procesamiento de datos con Anthropic y Twilio | Pendiente — necesario antes de producción |
| K4 | Configurar Google Drive compartido con documentos | Este fin de semana |
| K5 | Preparar propuesta actualizada con alcance y fases | Antes de la próxima reunión |
| K6 | Compartir documento de autenticidad de IA | Este fin de semana (vía Drive) |

---

## Próximos pasos

| # | Quién | Acción | Cuándo |
|---|---|---|---|
| 1 | Mark | Configurar Google Drive y compartir documentos | Este fin de semana |
| 2 | Carlos | Crear grupo de WhatsApp para los tres | Esta semana |
| 3 | Martin | Revisar preguntas pendientes (M1-M6) | Antes de la próxima reunión |
| 4 | Carlos | Revisar preguntas legales (C1-C5) | Cuando sea posible |
| 5 | Todos | Programar reunión de seguimiento | Próxima semana |

---

## Nota personal

Carlos, fue un placer conocerte. Tu experiencia en innovación y nuevas tecnologías es exactamente lo que este proyecto necesita. Creo que con el equipo que tenemos — medicina, tecnología y derecho — podemos construir algo realmente importante.

Martin, gracias por reunir a este equipo. La visión de llevar esto a nivel mundial es algo que comparto completamente. Como dije en la reunión: este es un problema que muchos intentan resolver, pero nadie lo ha logrado todavía. ¿Por qué no nosotros?

Un fuerte abrazo a los dos,

Mark McArthey
Learned Geek LLC
markm@learnedgeek.com

---

*Borrador — revisar antes de enviar. El documento de seguimiento compartido va en Google Drive junto con este correo.*
