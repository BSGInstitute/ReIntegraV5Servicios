# 📞 Documentación API de Integración Wavix

## Tabla de Contenidos

- [Introducción](#introducción)
- [Arquitectura](#arquitectura)
- [Endpoints Disponibles](#endpoints-disponibles)
  - [1. Obtener Configuración Completa Wavix](#1-obtener-configuración-completa-wavix-)
  - [2. Obtener Acceso de Usuario](#2-obtener-acceso-de-usuario)
  - [3. Obtener Números por Usuario](#3-obtener-números-por-usuario)
  - [4. Listar SIP Trunks](#4-listar-sip-trunks)
  - [5. Obtener SIP Trunk por ID](#5-obtener-sip-trunk-por-id)
  - [6. Generar Token Widget](#6-generar-token-widget)
  - [7. Obtener Estado de Llamada](#7-obtener-estado-de-llamada)
- [Modelos de Datos](#modelos-de-datos)
- [Códigos de Error](#códigos-de-error)
- [Guía de Migración Frontend](#guía-de-migración-frontend)
- [Mejores Prácticas](#mejores-prácticas)

---

## Introducción

Esta documentación describe los endpoints de la API de integración con Wavix para el sistema IntegraV5. La integración permite gestionar llamadas VoIP, configuración de SIP trunks y generación de tokens para widgets embebibles.

### Características Principales

- ✅ Gestión segura de credenciales (API Keys en BD)
- ✅ Generación automática de tokens con TTL de 12 horas
- ✅ Tracking de tokens en base de datos
- ✅ Configuración por personal/asesor
- ✅ Múltiples números de salida por país
- ✅ Integración con Wavix API v1 y v2

### URL Base

```
http://localhost:{puerto}/api/Wavix
https://{servidor}/api/Wavix
```

---

## Arquitectura

```
┌─────────────┐
│   Frontend  │
│  (Angular)  │
└──────┬──────┘
       │
       │ HTTP Request
       ▼
┌─────────────────────────────────────┐
│      Backend API (IntegraV5)       │
├─────────────────────────────────────┤
│  ┌────────────────────────────┐    │
│  │   WavixController.cs       │    │
│  └────────┬───────────────────┘    │
│           │                         │
│  ┌────────▼───────────────────┐    │
│  │   WavixService.cs          │    │
│  │  (Lógica de negocio)       │    │
│  └────────┬───────────────────┘    │
│           │                         │
│  ┌────────▼───────────────────┐    │
│  │  WavixRepository.cs        │    │
│  │  (Acceso a datos)          │    │
│  └────────┬───────────────────┘    │
│           │                         │
└───────────┼─────────────────────────┘
            │
    ┌───────┴────────┐
    │                │
┌───▼────┐    ┌─────▼──────┐
│   BD   │    │  Wavix API │
│ SQL    │    │  (Externa) │
└────────┘    └────────────┘
```

---

## Endpoints Disponibles

### 1. Obtener Configuración Completa Wavix ⭐

**El endpoint más importante y recomendado para el frontend.**

Endpoint unificado que obtiene toda la configuración necesaria para inicializar el widget de Wavix en una sola llamada. Incluye: configuración del SIP trunk, generación de token y guardado en BD.

#### Request

```http
GET /api/Wavix/ObtenerConfiguracionCompletaWavix/{idPersonal}
```

#### Parámetros

| Parámetro | Tipo | Ubicación | Requerido | Descripción |
|-----------|------|-----------|-----------|-------------|
| `idPersonal` | integer | Path | ✅ Sí | ID del personal/asesor |

#### Response (200 OK)

```json
{
  "id": "3107",
  "name": "trunk-username-123",
  "urlServer": "sg.wavix.net",
  "callerid": "+51987654321",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
  "uuid": "550e8400-e29b-41d4-a716-446655440000",
  "ttl": 43200
}
```

#### Campos de Respuesta

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `id` | string | ID del SIP trunk en Wavix |
| `name` | string | Nombre de usuario del trunk |
| `urlServer` | string | URL del servidor Wavix (ej: "sg.wavix.net") |
| `callerid` | string | Número de llamante predeterminado |
| `token` | string | Token JWT para autenticación del widget (válido 12 horas) |
| `uuid` | string | UUID único del token generado |
| `ttl` | integer | Tiempo de vida del token en segundos (43200 = 12 horas) |

#### Ejemplo de Uso

**JavaScript/TypeScript:**
```typescript
async obtenerConfiguracionWavix(idPersonal: number): Promise<ConfigWavix> {
  const url = `/api/Wavix/ObtenerConfiguracionCompletaWavix/${idPersonal}`;
  const response = await this.http.get<ConfigWavix>(url).toPromise();
  return response;
}
```

**cURL:**
```bash
curl -X GET "http://localhost:5000/api/Wavix/ObtenerConfiguracionCompletaWavix/123"
```

#### Flujo Interno

1. ✅ Consulta BD: `T_PersonalWavix` → Obtiene IdSipTrunk, UrlServer, IdWavixCredencial
2. ✅ Consulta BD: `T_WavixCredencial` → Obtiene ApiKey
3. ✅ Llama a Wavix API: `GET /trunks/{id}` → Obtiene config del trunk
4. ✅ Llama a Wavix API: `POST /webrtc/tokens` → Genera token (TTL 12h)
5. ✅ Guarda en BD: `T_WavixTokenDiario` → Registra token
6. ✅ Retorna configuración completa

#### Códigos de Error

| Código | Descripción |
|--------|-------------|
| `200` | ✅ Configuración obtenida exitosamente |
| `400` | ❌ Personal no encontrado o sin configuración Wavix |
| `500` | ❌ Error al consultar API de Wavix o BD |

---

### 2. Obtener Acceso de Usuario

Obtiene la configuración básica de Wavix asociada a un personal.

#### Request

```http
GET /api/Wavix/GetUserAccess/{idPersonal}
```

#### Parámetros

| Parámetro | Tipo | Ubicación | Requerido | Descripción |
|-----------|------|-----------|-----------|-------------|
| `idPersonal` | integer | Path | ✅ Sí | ID del personal |

#### Response (200 OK)

```json
{
  "id": 1,
  "idPersonal": 123,
  "idCredencial": 5,
  "idSipTrunk": "trunk-user-123",
  "urlServer": "sg.wavix.net"
}
```

#### Ejemplo

```bash
curl -X GET "http://localhost:5000/api/Wavix/GetUserAccess/123"
```

---

### 3. Obtener Números por Usuario

Obtiene todos los números de teléfono configurados para un personal, organizados por país.

#### Request

```http
GET /api/Wavix/GetNumberByUser/{idPersonal}
```

#### Parámetros

| Parámetro | Tipo | Ubicación | Requerido | Descripción |
|-----------|------|-----------|-----------|-------------|
| `idPersonal` | integer | Path | ✅ Sí | ID del personal |

#### Response (200 OK)

```json
[
  {
    "idPersonal": "123",
    "nombreAsesor": "Juan Pérez",
    "idSipTrunk": "trunk-user-123",
    "urlServer": "sg.wavix.net",
    "idPais": 1,
    "numero": "+51987654321",
    "predeterminado": true
  },
  {
    "idPersonal": "123",
    "nombreAsesor": "Juan Pérez",
    "idSipTrunk": "trunk-user-123",
    "urlServer": "sg.wavix.net",
    "idPais": 2,
    "numero": "+1234567890",
    "predeterminado": false
  }
]
```

#### Uso

Este endpoint es útil para implementar **Caller ID dinámico** según el país del contacto.

```typescript
// Seleccionar número según país del contacto
const numeros = await getNumberByUser(idPersonal);
const numeroMexico = numeros.find(n => n.idPais === 2); // México
```

---

### 4. Listar SIP Trunks

Lista todos los SIP trunks disponibles en la cuenta de Wavix (con paginación).

#### Request

```http
GET /api/Wavix/ListarSipTrunks?apiKey={apiKey}&page={page}&perPage={perPage}
```

#### Parámetros

| Parámetro | Tipo | Ubicación | Requerido | Descripción |
|-----------|------|-----------|-----------|-------------|
| `apiKey` | string | Query | ✅ Sí | API Key de Wavix |
| `page` | integer | Query | ❌ No | Número de página (default: 1) |
| `perPage` | integer | Query | ❌ No | Registros por página (default: 25, max: 100) |

#### Response (200 OK)

```json
{
  "sip_trunks": [
    {
      "id": "3107",
      "access_token": "token123",
      "urlServer": "sg.wavix.net",
      "label": "Trunk Principal",
      "name": "trunk-user-main",
      "auth_method": "ip",
      "callerid": "+51987654321",
      "host_request": {
        "host": "sip.wavix.com",
        "status": "active"
      },
      "encrypted_media": true,
      "passthrough": false,
      "multiple_numbers": true,
      "status": "active",
      "charge": 0.05,
      "talk_time": 3600,
      "machine_detection_enabled": true,
      "call_recording_enabled": true,
      "transcription_enabled": false,
      "transcription_threshold": 60
    }
  ],
  "pagination": {
    "current_page": 1,
    "total": 50,
    "per_page": 25,
    "total_pages": 2
  }
}
```

#### Ejemplo

```bash
curl -X GET "http://localhost:5000/api/Wavix/ListarSipTrunks?apiKey=abc123&page=1&perPage=25"
```

---

### 5. Obtener SIP Trunk por ID

Obtiene la configuración detallada de un SIP trunk específico.

#### Request

```http
GET /api/Wavix/ObtenerSipTrunkPorId/{idSipTrunk}?apiKey={apiKey}
```

#### Parámetros

| Parámetro | Tipo | Ubicación | Requerido | Descripción |
|-----------|------|-----------|-----------|-------------|
| `idSipTrunk` | string | Query | ✅ Sí | ID del SIP trunk |
| `apiKey` | string | Query | ✅ Sí | API Key de Wavix |

#### Response (200 OK)

```json
{
  "id": "3107",
  "name": "trunk-user-main",
  "callerid": "+51987654321",
  "label": "Trunk Principal",
  "created_at": "2024-01-15T10:30:00Z",
  "ip_restrict": true,
  "channels_restrict": true,
  "max_channels": 10,
  "cost_limit": true,
  "max_call_cost": "5.00",
  "call_restrict": true,
  "call_limit": 3600,
  "didinfo_enabled": true,
  "call_recording_enabled": true,
  "machine_detection_enabled": true,
  "transcription_enabled": false,
  "rewrite_enabled": false,
  "encrypted_media": true,
  "multiple_numbers": true,
  "allowed_ips": [
    {
      "id": 1,
      "ip": "192.168.1.100"
    }
  ],
  "host": "sip.wavix.com",
  "access_token": "token_abc123",
  "transcription_threshold": 60
}
```

#### Ejemplo

```bash
curl -X GET "http://localhost:5000/api/Wavix/ObtenerSipTrunkPorId/3107?apiKey=abc123"
```

---

### 6. Generar Token Widget

Genera un nuevo token JWT para autenticar el widget embebible de Wavix.

> **⚠️ Nota:** Normalmente NO necesitas llamar este endpoint directamente. Usa `ObtenerConfiguracionCompletaWavix` que ya incluye la generación de token.

#### Request

```http
POST /api/Wavix/GenerarTokenWidget?apiKey={apiKey}
Content-Type: application/json
```

#### Parámetros Query

| Parámetro | Tipo | Ubicación | Requerido | Descripción |
|-----------|------|-----------|-----------|-------------|
| `apiKey` | string | Query | ✅ Sí | API Key de Wavix |

#### Body

```json
{
  "sip_trunk": "trunk-user-main",
  "payload": {
    "user_id": "123",
    "custom_data": "cualquier dato"
  },
  "ttl": 43200
}
```

#### Campos del Body

| Campo | Tipo | Requerido | Descripción |
|-------|------|-----------|-------------|
| `sip_trunk` | string | ✅ Sí | Nombre del SIP trunk |
| `payload` | object | ❌ No | Datos arbitrarios asociados al token |
| `ttl` | integer | ❌ No | Tiempo de vida en segundos (default: 43200) |

#### Response (200 OK)

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "uuid": "550e8400-e29b-41d4-a716-446655440000",
  "sip_trunk": "trunk-user-main",
  "payload": {
    "user_id": "123",
    "custom_data": "cualquier dato"
  },
  "ttl": 43200
}
```

#### Ejemplo

```bash
curl -X POST "http://localhost:5000/api/Wavix/GenerarTokenWidget?apiKey=abc123" \
  -H "Content-Type: application/json" \
  -d '{
    "sip_trunk": "trunk-user-main",
    "payload": {},
    "ttl": 43200
  }'
```

---

### 7. Obtener Estado de Llamada

Obtiene el estado de la última llamada realizada por un asesor en una actividad específica.

#### Request

```http
GET /api/Wavix/ObtenerEstadoLlamadaWavix/{idPersonal}/{idOportunidad}/{idActividadDetalle}/{idAlumno}/{nroIntentoLlamada}
```

#### Parámetros

| Parámetro | Tipo | Ubicación | Requerido | Descripción |
|-----------|------|-----------|-----------|-------------|
| `idPersonal` | integer | Path | ✅ Sí | ID del personal |
| `idOportunidad` | integer | Path | ✅ Sí | ID de la oportunidad |
| `idActividadDetalle` | integer | Path | ✅ Sí | ID del detalle de actividad |
| `idAlumno` | integer | Path | ✅ Sí | ID del alumno/contacto |
| `nroIntentoLlamada` | integer | Path | ✅ Sí | Número de intento de llamada |

#### Response (200 OK)

```json
{
  "uuid": "550e8400-e29b-41d4-a716-446655440000",
  "idOportunidad": 456,
  "idActividadDetalle": 789,
  "disposition": "answered",
  "answered_by": "human"
}
```

#### Valores de `disposition`

| Valor | Descripción |
|-------|-------------|
| `answered` | Llamada contestada |
| `no-answer` | No contestó |
| `busy` | Ocupado |
| `failed` | Fallo en la llamada |
| `canceled` | Cancelada |

#### Valores de `answered_by`

| Valor | Descripción |
|-------|-------------|
| `human` | Contestó una persona |
| `machine` | Contestó contestadora/buzón |
| `unknown` | Desconocido |

---

## Modelos de Datos

### ConfiguracionCompletaWavixDTO

```typescript
interface ConfiguracionCompletaWavixDTO {
  id: string;                // ID del SIP trunk
  name: string;              // Nombre de usuario del trunk
  urlServer: string;         // Servidor Wavix (ej: "sg.wavix.net")
  callerid: string;          // Número de llamante
  token: string;             // Token JWT (válido 12h)
  uuid: string;              // UUID del token
  ttl: number;               // Tiempo de vida en segundos
}
```

### WavixPersonalDTO

```typescript
interface WavixPersonalDTO {
  id: number;                // ID registro T_PersonalWavix
  idPersonal: number;        // ID del personal
  idCredencial: number;      // ID credencial asociada
  idSipTrunk: string;        // ID del SIP trunk
  urlServer: string;         // URL del servidor
}
```

### NumeroAsesorWavixDTO

```typescript
interface NumeroAsesorWavixDTO {
  idPersonal: string;        // ID del personal
  nombreAsesor: string;      // Nombre del asesor
  idSipTrunk: string;        // ID del SIP trunk
  urlServer: string;         // URL del servidor
  idPais: number;            // ID del país
  numero: string;            // Número de teléfono
  predeterminado: boolean;   // Es número predeterminado
}
```

### SipTrunkDTO

```typescript
interface SipTrunkDTO {
  id: string;
  access_token: string;
  urlServer: string;
  label: string;
  name: string;
  auth_method: string;
  callerid: string;
  host_request: {
    host: string;
    status: string;
  };
  encrypted_media: boolean;
  passthrough: boolean;
  multiple_numbers: boolean;
  status: string;
  charge: number;
  talk_time: number;
  machine_detection_enabled: boolean;
  call_recording_enabled: boolean;
  transcription_enabled: boolean;
  transcription_threshold: number;
}
```

---

## Códigos de Error

### Códigos HTTP Estándar

| Código | Significado | Descripción |
|--------|-------------|-------------|
| `200` | OK | ✅ Petición exitosa |
| `201` | Created | ✅ Recurso creado exitosamente |
| `400` | Bad Request | ❌ Parámetros inválidos o faltantes |
| `401` | Unauthorized | ❌ API Key inválida o faltante |
| `404` | Not Found | ❌ Recurso no encontrado |
| `500` | Internal Server Error | ❌ Error en el servidor |

### Mensajes de Error Comunes

```json
{
  "error": "No se encontró configuración de Wavix para el personal 123"
}
```

```json
{
  "error": "No se encontró API Key activa para el personal: 123"
}
```

```json
{
  "error": "Error al generar token del widget Wavix: 401 - Invalid API Key"
}
```

---

## Guía de Migración Frontend

### ❌ ANTES (Código Antiguo - No Usar)

```typescript
// ❌ API Key expuesta
private apiKey = 'be27089f5daf192d7fe0f7552bea8400...';

// ❌ Múltiples llamadas HTTP
async getSipTrunks(): Promise<any[]> {
  for (let page = 1; page <= 3; page++) {
    const response = await this.http.get(`${this.baseUrl}/trunks?appid=${this.apiKey}...`);
    // ...
  }
}

// ❌ Llama directamente a Wavix API
async getWavixConfigByCallerId(idSiptrunk: string) {
  const url = `${this.baseUrlv2}/webrtc/tokens?appid=${this.apiKey}`;
  const response = await this.http.post(url, body);
}
```

### ✅ DESPUÉS (Código Nuevo - Recomendado)

```typescript
// ✅ Sin API Key expuesta
// ✅ Una sola llamada HTTP
// ✅ Todo se maneja en el backend

import { Injectable, inject } from '@angular/core';
import { Integra } from '@core/services/integra.service';

@Injectable({
  providedIn: 'root'
})
export class WavixService {
  private integra = inject(Integra);

  async obtenerConfiguracionCompleta(idPersonal: number): Promise<ConfigWavix> {
    const url = `/api/Wavix/ObtenerConfiguracionCompletaWavix/${idPersonal}`;
    const response = await this.integra.get<ConfigWavix>(url).toPromise();
    return response.body;
  }

  async obtenerNumerosPorUsuario(idPersonal: number): Promise<NumeroAsesor[]> {
    const url = `/api/Wavix/GetNumberByUser/${idPersonal}`;
    const response = await this.integra.get<NumeroAsesor[]>(url).toPromise();
    return response.body;
  }
}
```

### Uso en Componentes

```typescript
export class TopBarComponent implements OnInit {
  private wavixService = inject(WavixService);
  private wavixSdkService = inject(WavixSdkService);

  async inicializarWavix(idPersonal: number, autoInit: boolean = false) {
    try {
      // 1. Obtener configuración completa (incluye token)
      const config = await this.wavixService.obtenerConfiguracionCompleta(idPersonal);

      // 2. Configurar SDK
      this.wavixSdkService.setCredentials(config);

      // 3. Inicializar si es necesario
      if (autoInit) {
        await this.wavixSdkService.iniciarWavix2();
      }

      console.log('✅ Wavix configurado exitosamente');
    } catch (error) {
      console.error('❌ Error al configurar Wavix:', error);
    }
  }

  async cargarNumeros(idPersonal: number) {
    const numeros = await this.wavixService.obtenerNumerosPorUsuario(idPersonal);
    this.wavixSdkService.setNumber(numeros);
  }
}
```

---

## Mejores Prácticas

### 1. **Usa el Endpoint Unificado**

✅ **Recomendado:**
```typescript
// Una sola llamada obtiene todo
const config = await obtenerConfiguracionCompleta(idPersonal);
```

❌ **No recomendado:**
```typescript
// Múltiples llamadas innecesarias
const personal = await getUserAccess(idPersonal);
const apiKey = await getApiKey();
const trunk = await getSipTrunk(apiKey, personal.idSipTrunk);
const token = await generateToken(apiKey, trunk.name);
```

### 2. **Manejo de Errores**

```typescript
async inicializarWavix(idPersonal: number) {
  try {
    const config = await this.wavixService.obtenerConfiguracionCompleta(idPersonal);
    this.wavixSdkService.setCredentials(config);
  } catch (error) {
    if (error.status === 404) {
      this.notificationService.error('No tienes configuración de Wavix');
    } else if (error.status === 500) {
      this.notificationService.error('Error al conectar con el servidor');
    } else {
      this.notificationService.error('Error desconocido');
    }
  }
}
```

### 3. **Caché de Configuración**

```typescript
private wavixConfig: ConfigWavix | null = null;
private configExpiration: Date | null = null;

async obtenerConfiguracionConCache(idPersonal: number): Promise<ConfigWavix> {
  // Si hay config y no ha expirado, reutilizar
  if (this.wavixConfig && this.configExpiration && new Date() < this.configExpiration) {
    return this.wavixConfig;
  }

  // Obtener nueva configuración
  this.wavixConfig = await this.wavixService.obtenerConfiguracionCompleta(idPersonal);

  // Establecer expiración (11 horas, antes de las 12h del token)
  this.configExpiration = new Date(Date.now() + 11 * 60 * 60 * 1000);

  return this.wavixConfig;
}
```

### 4. **Caller ID Dinámico**

```typescript
async realizarLlamada(numeroDestino: string, idPaisContacto: number) {
  // Obtener números configurados
  const numeros = await this.wavixService.obtenerNumerosPorUsuario(idPersonal);

  // Buscar número del país del contacto
  let callerIdSeleccionado = numeros.find(n => n.idPais === idPaisContacto);

  // Si no hay número para ese país, usar predeterminado
  if (!callerIdSeleccionado) {
    callerIdSeleccionado = numeros.find(n => n.predeterminado);
  }

  // Realizar llamada con el caller ID apropiado
  await this.wavixSdkService.realizarLlamada(numeroDestino, callerIdSeleccionado?.numero);
}
```

### 5. **Limpieza de Sesión**

```typescript
async cerrarSesionWavix() {
  try {
    // Limpiar token de sessionStorage
    sessionStorage.removeItem('wavix_token');
    sessionStorage.removeItem('wavix_uuid');

    // Destruir instancia del SDK
    await this.wavixSdkService.onDestroy();

    // Limpiar configuración en memoria
    this.wavixConfig = null;
    this.configExpiration = null;

    console.log('✅ Sesión Wavix cerrada correctamente');
  } catch (error) {
    console.error('❌ Error al cerrar sesión Wavix:', error);
  }
}
```

---

## Seguridad

### ⚠️ Nunca expongas el API Key en el frontend

❌ **INCORRECTO:**
```typescript
// NO HACER ESTO
const apiKey = 'be27089f5daf192d7fe0f7552bea8400...';
const response = await fetch(`https://api.wavix.com/trunks?appid=${apiKey}`);
```

✅ **CORRECTO:**
```typescript
// Siempre usar el backend
const response = await this.http.get('/api/Wavix/ObtenerConfiguracionCompletaWavix/123');
```

### Validación de Permisos

El backend debe validar que el usuario tiene permiso para acceder a la configuración del personal solicitado:

```csharp
// En el controlador
var usuarioActual = ObtenerUsuarioActual(); // Desde contexto/sesión

if (!TienePermisoAcceso(usuarioActual, idPersonal)) {
    return Unauthorized("No tienes permiso para acceder a esta configuración");
}
```

---

## Changelog

### Versión 1.0.0 (Diciembre 2024)

- ✅ Endpoint unificado `ObtenerConfiguracionCompletaWavix`
- ✅ Seguridad: API Keys en base de datos
- ✅ Generación automática de tokens con TTL de 12 horas
- ✅ Guardado de tokens en `T_WavixTokenDiario`
- ✅ Soporte para múltiples números por país
- ✅ Integración con Wavix API v2

---

## Soporte y Contacto

Para preguntas o soporte técnico sobre esta API:

- **Documentación Wavix:** https://docs.wavix.com/
- **Issues:** Reportar en el repositorio del proyecto

---

**Última actualización:** Diciembre 2024
**Versión API:** 1.0.0
**Wavix API Version:** v1 y v2
