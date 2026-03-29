# DrOk — Hoja de Ruta del Proyecto
## Qué Estamos Construyendo, Cuándo, y Qué Invierte Cada Socio

Preparado por: Mark McArthey, Learned Geek LLC
Fecha: 29 de marzo de 2026
**Acompaña:** DrOk Term Sheet (documento separado)

---

## Resumen

Este documento detalla lo que sucede en cada fase del proyecto — qué se construye, qué contribuye cada socio y cuánto cuesta. Está diseñado para ser transparente: ambos socios pueden ver exactamente a dónde va la inversión y cuándo comienza el retorno.

---

## Fase 1 — Descubrimiento (4 semanas)

**Objetivo:** Finalizar requisitos, confirmar marco legal, alinear la arquitectura.

| | Inversión de Mark | Inversión de Martin |
|---|---|---|
| **Efectivo** | ~$500 (seguros, infraestructura, cuentas API) | **$2,500** (pago por hito) |
| **Tiempo** | ~60 horas — mapeo de requisitos, arquitectura, configuración de Azure, acuerdos con proveedores, seguros, preparación de pasantes | ~20 horas — revisión de requisitos, documentación de producto, palabras clave de emergencia, revisión de franquicia, marca registrada |
| **Valor de mercado** | ~$9,000 | ~$5,000 |

### Mark Entrega:
- Mapeo completo de requisitos funcionales → técnicos
- Documento de arquitectura del sistema finalizado
- Entorno de nube Azure aprovisionado
- Tablero de proyecto en GitHub con todas las tareas
- Seguro contratado (E&O + Cyber)
- Acuerdos con proveedores revisados (Twilio, Anthropic)
- Documentación de incorporación de pasantes

### Martin Entrega:
- Documentación de productos (catálogo Infanzia, tablas de dosificación, ingredientes, preguntas frecuentes)
- Lista de palabras clave de emergencia en español coloquial de padres
- Decisión sobre tasa de muestreo VoBo
- Confirmación de restricciones de franquicia (Infanzia/Kezer-Lab)
- Registro de marca DrOk iniciado
- Carlos: opinión formal de DIGEMID

### Preguntas Pendientes para Esta Fase:
- Clasificación de DIGEMID (Carlos)
- Aplicabilidad de la ley de telemedicina (Carlos)
- Mecanismo de transferencia transfronteriza — SCC vs. trazabilidad (Carlos)
- Restricciones de franquicia (Martin)
- Políticas de datos de salud de WhatsApp Business + Twilio (Mark)

---

## Fase 2 — Chatbot de Productos Infanzia (4–5 semanas)

**Objetivo:** Chatbot de productos en vivo en WhatsApp — los padres pueden hacer preguntas sobre productos Infanzia las 24 horas del día, los 7 días de la semana.

| | Inversión de Mark | Inversión de Martin |
|---|---|---|
| **Efectivo** | ~$600 (Azure producción, API, Twilio, seguros) | **$2,500** (pago por hito) |
| **Tiempo** | ~80 horas — construcción de base de conocimiento, flujo del chatbot, interfaz de administración, WhatsApp producción, pruebas, mentoría de pasantes | ~15 horas — documentación de productos, pruebas del chatbot, número de WhatsApp, inicio de captación de médicos |
| **Valor de mercado** | ~$12,000 | ~$3,750 |

### Mark Entrega:
- Chatbot de productos funcionando en WhatsApp
- Interfaz de administración para que Martin gestione documentos de productos
- Divulgación de IA al inicio de cada conversación (requisito de Anthropic)
- Despliegue en producción en Azure
- Pasante contribuyendo a documentación y pruebas

### Martin Entrega:
- Documentación completa de productos para la base de conocimiento del chatbot
- Revisión y aprobación de respuestas del chatbot
- Número de WhatsApp Business para producción
- Inicio de pre-venta a la red de médicos

---

## Fase 3 — Sistema de Triaje con IA para Médicos (6–8 semanas)

**Objetivo:** Backend completo de triaje — PubMed RAG, detección de emergencias, cola VoBo, gestión de conversaciones, cumplimiento de datos.

Esta es la fase más compleja y valiosa. Es donde se construye la plataforma central.

| | Inversión de Mark | Inversión de Martin |
|---|---|---|
| **Efectivo** | ~$900 (Azure + PostgreSQL, uso intensivo de API, seguros) | **$2,500** (pago por hito) |
| **Tiempo** | ~120 horas — sistema de evidencia Gate 1, cumplimiento de citaciones, detección de emergencias, cola VoBo, flujo de consentimiento, registro de auditoría, base de datos, revisión de seguridad | ~30 horas — aprobación de palabras clave de emergencia, umbrales de laboratorio, registro ANPDP, plan de incorporación, decisión de precios |
| **Valor de mercado** | ~$18,000 | ~$7,500 |

### Mark Entrega:
- Gate 1: recuperación de evidencia + puntuación de confianza + cumplimiento de citaciones
- Detección determinista de emergencias (no generada por IA — seguridad codificada)
- Cola de revisión médica VoBo con alertas SMS
- Flujo de captura de consentimiento (cumple con Ley 29733)
- Mecanismo de bloqueo de datos (derechos ARCO)
- Registro completo de auditoría / trazabilidad
- Base de datos PostgreSQL con esquema completo
- Revisión de seguridad

### Martin Entrega:
- Lista de palabras clave de emergencia aprobada (documento clínico)
- Umbrales de valores críticos de laboratorio por grupo de edad
- Registro de banco de datos ANPDP presentado
- Carlos: revisión DPA y orientación SCC
- Plan de incorporación de médicos para los primeros 10 médicos
- Precio de suscripción para médicos determinado
- Registro de marca DrOk confirmado como presentado

---

## Fase 4 — Panel de Médicos + Lanzamiento (4–5 semanas)

**Objetivo:** Panel orientado al médico, pruebas UAT, lanzamiento en producción.

| | Inversión de Mark | Inversión de Martin |
|---|---|---|
| **Efectivo** | ~$700 (Azure producción, API, Twilio, seguros) | **$2,500** (pago por hito) |
| **Tiempo** | ~80 horas — construcción del panel, portal de administración, seguimiento de anulaciones, endurecimiento de producción, soporte UAT, monitoreo, lanzamiento | ~40 horas — pruebas UAT, aprobación de lógica de emergencia, aprobación de precisión, incorporación de primeros 5 médicos, materiales de capacitación, anuncio de lanzamiento |
| **Valor de mercado** | ~$12,000 | ~$10,000 |

### Mark Entrega:
- Panel de médicos (cola priorizada, flujo de trabajo VoBo, interruptor de encendido/apagado)
- Portal de administración (monitoreo del sistema, registros de auditoría, reproducción de conversaciones)
- Seguimiento de tasa de anulación médica (señal de precisión)
- Despliegue endurecido para producción
- Pruebas de rendimiento
- Manual de operaciones

### Martin Entrega:
- Pruebas UAT con escenarios realistas (incluyendo emergencias y casos ambiguos)
- **Aprobación formal de la lógica de detección de emergencias** (documentada, archivada)
- **Aprobación formal de la precisión de la IA** (tasa de anulación < 20%)
- Primeros 5 médicos piloto incorporados
- Materiales de capacitación para médicos
- Anuncio de lanzamiento a la red

---

## Inversión Total — Fase de Construcción (Pre-Ingresos)

| | Mark | Martin |
|---|---|---|
| **Efectivo** | ~$2,700 (infraestructura, API, seguros) | **$10,000** (pagos por hitos escalonados) |
| **Tiempo** | ~690 horas (incluyendo 350 hrs pre-proyecto) | ~145 horas |
| **Valor de mercado del tiempo** | ~$103,500 | ~$26,250 |
| **Valor total de inversión** | **~$106,200** | **~$36,250** |

La inversión total de Mark es aproximadamente **3 veces la de Martin** — por eso existe la división de participación (55/45) y la tarifa de licencia de plataforma. Equilibran la ecuación con el tiempo a medida que las contribuciones comerciales de Martin crecen.

---

## Post-Lanzamiento — Cuándo Comienzan los Ingresos

Los ingresos comienzan a fluir cuando los médicos empiezan a pagar suscripciones. Basado en los hitos de rendimiento:

| Línea de Tiempo | Médicos | Ingresos Mensuales (a $100/médico) | Parte Mensual de Mark | Parte Mensual de Martin |
|---|---|---|---|---|
| Lanzamiento | 1 (Martin) | $100 | $60 | $34 |
| +3 meses | 10 | $1,000 | $600 | $340 |
| +6 meses | 25 | $2,500 | $1,500 | $850 |
| +12 meses | 50 | $5,000 | $3,000 | $1,700 |
| Año 2 | 100+ | $10,000+ | $6,000+ | $3,400+ |
| Año 3+ | 500+ | $50,000+ | $30,000+ | $17,000+ |

**El panorama a largo plazo es donde ambos socios ganan.** Los ingresos mensuales son significativos, pero el valor real es lo que estamos construyendo juntos — una plataforma con valor patrimonial compuesto.

### Proyección de Valor a Largo Plazo

| | Año 1 | Año 2 | Año 3 | Año 5 |
|---|---|---|---|---|
| **Médicos** | 50 | 150 | 500 | 2,000 |
| **Ingresos mensuales** | $5,000 | $15,000 | $50,000 | $200,000 |
| **Ingresos anuales (ARR)** | $60,000 | $180,000 | $600,000 | $2,400,000 |
| **Ingreso anual de Mark** | $36,000 | $108,000 | $360,000 | $1,440,000 |
| **Ingreso anual de Martin** | $20,400 | $61,200 | $204,000 | $816,000 |
| **Valoración de la plataforma (6-8x ARR)** | $360K–$480K | $1.1M–$1.4M | $3.6M–$4.8M | $14.4M–$19.2M |
| **Valor patrimonial de Mark (55%)** | $198K–$264K | $594K–$792K | $2.0M–$2.6M | $7.9M–$10.6M |
| **Valor patrimonial de Martin (45%)** | $162K–$216K | $486K–$648K | $1.6M–$2.2M | $6.5M–$8.6M |

Las plataformas SaaS médicas se valoran a 6–8x los ingresos anuales recurrentes debido a los altos costos de cambio, barreras regulatorias y efectos de red de datos. Estas proyecciones asumen $100/médico/mes y crecimiento constante impulsado por el esfuerzo comercial de Martin y la reputación de boca en boca.

**En el Año 5 con 2,000 médicos, la participación de Martin por sí sola vale $6.5M–$8.6M** — a partir de una inversión inicial de $10,000 y su esfuerzo continuo de ventas. Ese es el poder de construir patrimonio en una plataforma en crecimiento en lugar de cobrar un honorario de consultoría único.

### Escenarios de Salida

En cualquier momento, cualquier socio puede realizar su valor patrimonial a través de:

| Escenario | Valor de Mark | Valor de Martin |
|---|---|---|
| **DrOk adquirido con 500 médicos** | $2.0M–$2.6M (55% de DrOk) + plataforma retenida | $1.6M–$2.2M (45% de DrOk) |
| **DrOk adquirido con 1,000 médicos** | $4.0M–$5.3M + plataforma retenida | $3.2M–$4.3M |
| **DrOk adquirido con 2,000 médicos** | $7.9M–$10.6M + plataforma retenida | $6.5M–$8.6M |
| **Plataforma licenciada a mercados adicionales** | Flujos de ingresos adicionales (100% Learned Geek) | DrOk no afectado — datos de Martin aislados |

La capacidad de la plataforma para servir múltiples mercados (otras especialidades, otros países) crea valor adicional para Learned Geek más allá de DrOk. Martin se beneficia del crecimiento de DrOk; Mark se beneficia tanto de DrOk como de la expansión de la plataforma.

**La ventaja de Martin crece más rápido que la de Mark en términos relativos** — porque el esfuerzo comercial de Martin es lo que impulsa la adopción de médicos. Cuantos más médicos traiga, más valiosa se vuelve su participación del 45%. A escala, ambos socios están construyendo riqueza real y duradera.

---

## Precio Mínimo de Suscripción

Martin establece el precio de suscripción para los médicos — él conoce su mercado. Sin embargo, existe un **precio mínimo** para asegurar que los costos operativos estén cubiertos:

| Componente | Monto | Propósito |
|---|---|---|
| **Tarifa de licencia de plataforma** | $15–20/médico/mes | Cubre infraestructura (Azure, Anthropic API, Twilio, seguros, mantenimiento) — va a Learned Geek |
| **Precio mínimo de suscripción** | $50/médico/mes | Por debajo de esto, los costos operativos consumen demasiado margen |
| **Rango recomendado** | $75–150/médico/mes | Basado en plataformas SaaS médicas comparables en América Latina |

**Cómo funciona:** Si Martin cobra $100/médico/mes, los primeros $15–20 cubren costos de plataforma (Learned Geek), y los $80–85 restantes son ingresos netos que se dividen 55/45. Martin no puede establecer un precio por debajo de $50/mes sin acuerdo mutuo — esto asegura que ningún socio subsidie los costos operativos del otro.

Martin es libre de cobrar **por encima** del rango recomendado si su mercado lo soporta. Un pediatra afiliado a Harvard vendiendo una plataforma de triage con IA respaldada por evidencia de PubMed puede comandar un precio premium. Cuanto mayor sea el precio, más ganan ambos socios.

---

## Costos Operativos — Post-Lanzamiento (Compartidos)

Todos los costos continuos se comparten proporcionalmente (55% Mark / 45% Martin):

| Concepto | Estimado Mensual |
|---|---|
| Hospedaje Azure | $100–200 |
| Anthropic API | $100–200 |
| Twilio (WhatsApp) | $50–150 |
| Seguros | $125 |
| Varios | $50 |
| **Total** | **$425–725/mes** |

Estos costos escalan con el número de médicos pero siguen siendo una pequeña fracción de los ingresos. Con 50 médicos ($5,000/mes de ingresos), los costos operativos son ~$725/mes — un margen del 85%.

---

## Cronograma

| Fase | Duración | Inicio | Fin (Estimado) |
|---|---|---|---|
| Fase 1 — Descubrimiento | 4 semanas | Al firmar | +4 semanas |
| Fase 2 — Chatbot de Productos | 4–5 semanas | Fase 1 completada | +9 semanas |
| Fase 3 — Sistema para Médicos | 6–8 semanas | Fase 2 completada | +17 semanas |
| Fase 4 — Panel + Lanzamiento | 4–5 semanas | Fase 3 completada | +22 semanas (~5.5 meses) |

**Si comenzamos en abril, el lanzamiento es aproximadamente en septiembre de 2026.** Esto se alinea con el objetivo de MVP de julio discutido con Carlos, con el lanzamiento completo en producción después de UAT.

---

## Qué Pasa Si No Procedemos

Esto se incluye por transparencia, no como presión.

- Mark retiene toda la propiedad intelectual de la plataforma y puede licenciarla a otra red de médicos o mercado
- Martin retiene el concepto de marca DrOk y sus relaciones con médicos
- Ambos socios pierden su inversión de tiempo pero ninguno pierde sus activos principales
- La prueba de concepto ya funciona — tiene valor independientemente de esta asociación específica

---

## Qué Está Pasando Ahora — El Trabajo No Espera

Esta discusión de términos es importante, pero no bloquea el progreso. Estoy avanzando con el entendimiento de que llegaremos a un acuerdo y que seremos exitosos juntos. Nunca asumo el fracaso, y no voy a permitir que una negociación de buena fe impida que el trabajo se haga.

**En lo que estoy trabajando activamente ahora mismo:**

| # | Tarea | Estado | Notas |
|---|---|---|---|
| 1 | Despliegue del POC en la nube Azure | En progreso | Migrando de local/ngrok a hosting en la nube listo para producción |
| 2 | Seguro (E&O + Cyber) | En progreso | Cotizaciones recibidas, en proceso de contratación |
| 3 | Revisión de políticas de Twilio + WhatsApp Business | En progreso | Verificación de cumplimiento de proveedores |
| 4 | Revisión del DPA de Anthropic | En progreso | Acuerdo de procesamiento de datos para Claude API |
| 5 | Preparación de incorporación de pasante (inicia 8 de junio) | En progreso | Pasante de WCTC — documentación, planificación de tareas, marco de mentoría |
| 6 | Investigación de autenticidad de IA (mitigación de confabulación) | En curso | 6+ meses de investigación aplicada directamente a la arquitectura de seguridad clínica de DrOk |
| 7 | Pipeline de transcripción para grabaciones bilingües de reuniones | Completo | Whisper + diarización de hablantes — ya en uso para nuestras reuniones |
| 8 | Consulta con abogados peruanos y estadounidenses sobre estructura tributaria y protección internacional de PI | En progreso | A mi propio costo — asegurando que la sociedad se construya sobre una base legal sólida para ambas partes |

**Lo que necesito de Martin para seguir avanzando:**

| # | Elemento | Por Qué Se Necesita | Cuándo |
|---|---|---|---|
| 1 | Respuesta a esta propuesta | Para alinearnos y formalizar | Cuando estés listo — sin prisa |
| 2 | Documentación de productos (catálogo Infanzia, dosificación, ingredientes) | Requerida para la base de conocimiento del chatbot de Fase 2 | Antes de que inicie la Fase 2 |
| 3 | Lista de palabras clave de emergencia (en español coloquial de padres) | Requerida para la detección de emergencias de Fase 3 | Antes de que inicie la Fase 3 |
| 4 | Carlos: opinión formal sobre DIGEMID | Afecta la estrategia a largo plazo pero no el MVP | Cuando Carlos esté disponible |
| 5 | Carlos: SCC vs. trazabilidad — confirmación escrita | Necesaria para decisiones de arquitectura de datos | Antes del lanzamiento |
| 6 | Registro de marca DrOk | Protege la marca antes del lanzamiento | Lo antes posible |

**En lo que Carlos está trabajando:**

| # | Elemento | Estado |
|---|---|---|
| 1 | Opinión sobre clasificación DIGEMID | En progreso — buscando contacto en clínica |
| 2 | Posición sobre ley de telemedicina (Ley 30421) | Parcialmente abordada en reunión del 27 de marzo |
| 3 | Reconciliación SCC vs. trazabilidad | Pendiente posición formal escrita |

### El Objetivo de Julio Se Mantiene

La estimación original de **entrega del MVP para julio de 2026 con Martin como usuario piloto único** sigue siendo el plan. La discusión de términos corre en paralelo — no retrasa la construcción. Cuando formalicemos el acuerdo, ya estaremos semanas adelantados porque elegí seguir construyendo en lugar de esperar.

Así es como opero: con confianza en que el acuerdo correcto se logrará, y con la disciplina de mantener el trabajo en movimiento mientras tanto.

---

*Este documento acompaña al DrOk Term Sheet. Ambos documentos juntos forman la base para nuestra discusión de asociación.*

*Preparado por Mark McArthey, Learned Geek LLC*
