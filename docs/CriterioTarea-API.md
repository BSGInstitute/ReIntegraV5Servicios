# API CriterioTarea — Guía de integración Frontend

**Controller:** `CriterioTareaController`
**Base URL:** `/api/CriterioTarea`
**Módulo:** Planificación

---

## Modelo de datos

```ts
// TypeScript / modelo de referencia
interface CriterioTarea {
  id: number;          // 0 en creación, requerido en actualización
  nombre: string;
  descripcion: string;
  escala: number;
  activo: boolean;
}
```

| Campo        | Tipo    | Requerido en POST | Requerido en PUT | Notas                        |
|--------------|---------|:-----------------:|:----------------:|------------------------------|
| `id`         | number  | No (ignorado)     | Sí               | PK de la tabla               |
| `nombre`     | string  | Sí                | Sí               |                              |
| `descripcion`| string  | Sí                | Sí               |                              |
| `escala`     | number  | Sí                | Sí               | Valor entero                 |
| `activo`     | boolean | No (ignorado)     | No (ignorado)    | Solo lectura — lo maneja el SP |

---

## Endpoints

### 1. Listar todos los criterios activos

```
GET /api/CriterioTarea/ListarCriterio
```

**Autenticación:** No requerida
**Body:** —

**Respuesta exitosa `200 OK`:**
```json
[
  {
    "id": 1,
    "nombre": "Criterio A",
    "descripcion": "Descripción del criterio A",
    "escala": 5,
    "activo": true
  },
  {
    "id": 2,
    "nombre": "Criterio B",
    "descripcion": "Descripción del criterio B",
    "escala": 10,
    "activo": true
  }
]
```

**Respuesta error `400 Bad Request`:**
```json
"Mensaje de error"
```

---

### 2. Obtener criterio por Id

```
GET /api/CriterioTarea/ObtenerPorIdCriterio/{idCriterio}
```

**Autenticación:** No requerida
**Parámetro de ruta:**

| Param        | Tipo   | Descripción       |
|--------------|--------|-------------------|
| `idCriterio` | number | Id del criterio   |

**Ejemplo:**
```
GET /api/CriterioTarea/ObtenerPorIdCriterio/3
```

**Respuesta exitosa `200 OK`:**
```json
{
  "id": 3,
  "nombre": "Criterio C",
  "descripcion": "Descripción del criterio C",
  "escala": 7,
  "activo": true
}
```

Retorna `null` si el criterio no existe o está inactivo.

---

### 3. Insertar criterio

```
POST /api/CriterioTarea/InsertarCriterio
```

**Autenticación:** `[Authorize]` — pendiente de activar (actualmente comentado)
**Content-Type:** `application/json`

**Body:**
```json
{
  "nombre": "Nuevo criterio",
  "descripcion": "Descripción del nuevo criterio",
  "escala": 5
}
```

**Respuesta exitosa `200 OK`:**
```json
true
```

**Respuesta error `400 Bad Request`:**
```json
"Mensaje de error"
```

---

### 4. Actualizar criterio

```
PUT /api/CriterioTarea/ActualizarCriterio
```

**Autenticación:** `[Authorize]` — pendiente de activar (actualmente comentado)
**Content-Type:** `application/json`

**Body:** (el campo `id` es obligatorio)
```json
{
  "id": 3,
  "nombre": "Criterio actualizado",
  "descripcion": "Nueva descripción",
  "escala": 8
}
```

**Respuesta exitosa `200 OK`:**
```json
true
```

**Respuesta error `400 Bad Request`:**
```json
"Mensaje de error"
```

---

### 5. Eliminar criterio (baja lógica)

```
DELETE /api/CriterioTarea/EliminarCriterio/{id}
```

**Autenticación:** `[Authorize]` — pendiente de activar (actualmente comentado)
**Parámetro de ruta:**

| Param | Tipo   | Descripción     |
|-------|--------|-----------------|
| `id`  | number | Id del criterio |

**Ejemplo:**
```
DELETE /api/CriterioTarea/EliminarCriterio/3
```

**Respuesta exitosa `200 OK`:**
```json
true
```

**Respuesta error `400 Bad Request`:**
```json
"Mensaje de error"
```

> La eliminación es **lógica** — el registro no se borra físicamente, solo se marca como inactivo (`Estado = 0`) mediante el SP `[pla].[SP_EliminarCriterioTarea]`.

---

## Tabla resumen de endpoints

| Acción              | Método   | URL                                              | Auth    | Body            |
|---------------------|----------|--------------------------------------------------|---------|-----------------|
| Listar todos        | `GET`    | `/api/CriterioTarea/ListarCriterio`              | No      | —               |
| Obtener por Id      | `GET`    | `/api/CriterioTarea/ObtenerPorIdCriterio/{id}`   | No      | —               |
| Insertar            | `POST`   | `/api/CriterioTarea/InsertarCriterio`            | Pending | `CriterioTarea` |
| Actualizar          | `PUT`    | `/api/CriterioTarea/ActualizarCriterio`          | Pending | `CriterioTarea` |
| Eliminar            | `DELETE` | `/api/CriterioTarea/EliminarCriterio/{id}`       | Pending | —               |

---

## Ejemplo de servicio Angular (referencia)

```ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CriterioTarea {
  id: number;
  nombre: string;
  descripcion: string;
  escala: number;
  activo: boolean;
}

@Injectable({ providedIn: 'root' })
export class CriterioTareaService {
  private base = '/api/CriterioTarea';

  constructor(private http: HttpClient) {}

  listar(): Observable<CriterioTarea[]> {
    return this.http.get<CriterioTarea[]>(`${this.base}/ListarCriterio`);
  }

  obtenerPorId(id: number): Observable<CriterioTarea> {
    return this.http.get<CriterioTarea>(`${this.base}/ObtenerPorIdCriterio/${id}`);
  }

  insertar(criterio: Partial<CriterioTarea>): Observable<boolean> {
    return this.http.post<boolean>(`${this.base}/InsertarCriterio`, criterio);
  }

  actualizar(criterio: CriterioTarea): Observable<boolean> {
    return this.http.put<boolean>(`${this.base}/ActualizarCriterio`, criterio);
  }

  eliminar(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.base}/EliminarCriterio/${id}`);
  }
}
```

---

## Notas para el frontend

- El campo `activo` **no se envía** en POST ni PUT. Solo se usa al leer (refleja `Estado` en BD).
- En POST, el campo `id` **no es necesario** (el SP genera el Id automáticamente).
- En PUT, el campo `id` **es obligatorio** para identificar el registro a actualizar.
- Los endpoints de escritura (`POST`, `PUT`, `DELETE`) tienen `[Authorize]` **pendiente de activar** — coordinarlo con backend antes de agregar el header `Authorization: Bearer {token}`.
- Todos los errores retornan `400` con el mensaje como `string` plano (no objeto JSON).

---

## Stored Procedures involucrados

| Operación  | SP                                  |
|------------|-------------------------------------|
| Insertar   | `[pla].[SP_InsertarCriterioTarea]`  |
| Actualizar | `[pla].[SP_ActualizarCriterioTarea]`|
| Eliminar   | `[pla].[SP_EliminarCriterioTarea]`  |
| Leer       | Query Dapper directo sobre `pla.T_CriterioTarea` |
