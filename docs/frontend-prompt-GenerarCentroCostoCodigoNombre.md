# Frontend Integration — GenerarCentroCostoCodigoNombre

## Contexto

El endpoint `POST /api/PEspecifico/GenerarCentroCostoCodigoNombre` fue actualizado.
Ahora retorna 3 campos nuevos además de los existentes. Este documento describe
cómo integrarlos correctamente en el frontend Angular.

---

## Modelo TypeScript — actualizar la interfaz de respuesta

```typescript
interface CentroCostoGeneradoDTO {
  centroCosto: {
    id: number;
    idArea: number | null;
    idSubArea: number | null;
    idPgeneral: string;
    nombre: string;
    codigo: string;
    idAreaCc: string | null;
    ismtotales: number | null;
    icpftotales: number | null;
  };
  codigo: string;
  nombreProgramaEspecifico: string;
  nombreProgramaEspecificoNumerico: string;  // NUEVO: mismo nombre con número arábigo en vez de romano
  nombreProgramaGeneral: string;
  codigoBanco: string;
  gruposAsignados: number | null;    // NUEVO: límite configurado, null si no aplica
  gruposCreados: number;             // NUEVO: cuántos grupos ya existen actualmente
  haAlcanzadoLimiteGrupos: boolean;  // NUEVO: true cuando gruposCreados >= gruposAsignados
}
```

---

## Campos existentes — sin cambios

| Campo | Descripción |
|-------|-------------|
| `nombreProgramaEspecifico` | Nombre con número **romano** (I, II, III…) — sin cambios |
| `codigo` | Código del centro de costo — sin cambios |
| `codigoBanco` | Código banco — sin cambios |
| `centroCosto` | Objeto con datos del centro de costo — sin cambios |
| `nombreProgramaGeneral` | Nombre del programa general — sin cambios |

## Campos nuevos — descripción

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `nombreProgramaEspecificoNumerico` | `string` | Mismo formato que `nombreProgramaEspecifico` pero con número **arábigo** (1, 2, 3…). Usar donde se necesite mostrar el número como dígito. |
| `gruposAsignados` | `number \| null` | Límite máximo de grupos configurado en la versión del programa. `null` = sin restricción. |
| `gruposCreados` | `number` | Cantidad de grupos que ya existen para este programa. |
| `haAlcanzadoLimiteGrupos` | `boolean` | `true` cuando `gruposCreados >= gruposAsignados`. Indica que no se pueden crear más grupos. |

---

## Validación y bloque de advertencia

### Condición para mostrar el aviso

```typescript
response.haAlcanzadoLimiteGrupos === true && response.gruposAsignados !== null
```

> La segunda condición (`gruposAsignados !== null`) cubre el caso donde el programa
> no tiene límite configurado — en ese caso no se muestra ningún aviso.

### Comportamiento esperado

- Mostrar un **bloque de advertencia amarillo inline** en el template, cerca del resultado generado.
- **No** usar toast ni snackbar.
- El bloque debe permanecer visible mientras el resultado esté en pantalla.
- El botón de acción **no** se deshabilita.

### Template HTML (Angular)

```html
<div
  *ngIf="resultado?.haAlcanzadoLimiteGrupos && resultado?.gruposAsignados !== null"
  class="warning-block"
>
  <span class="warning-icon">⚠</span>
  Se ha alcanzado el límite de grupos asignados para este programa.
  No es posible crear más grupos.
</div>
```

### Estilos mínimos

Adaptar al sistema de diseño del proyecto:

```css
.warning-block {
  background-color: #FFF3CD;
  border: 1px solid #FFC107;
  border-left: 4px solid #FFC107;
  color: #856404;
  padding: 12px 16px;
  border-radius: 4px;
  display: flex;
  align-items: center;
  gap: 8px;
  margin-top: 12px;
}
```

---

## Resumen de cambios por campo

```
nombreProgramaEspecifico          → sin cambios (romano)
nombreProgramaEspecificoNumerico  → NUEVO (arábigo, mismo formato)
gruposAsignados                   → NUEVO (límite, null = sin restricción)
gruposCreados                     → NUEVO (grupos existentes)
haAlcanzadoLimiteGrupos           → NUEVO (flag para mostrar aviso)
```
