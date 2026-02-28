using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ValidacionRn2Service
    /// Autor: (pendiente)
    /// Fecha: 2026-02-23
    /// <summary>
    /// Implementación de la Regla de Negocio 2: bloqueo de leads/oportunidades por correo o teléfono
    /// </summary>
    public class ValidacionRn2Service : IValidacionRn2Service
    {
        // máximo de IDs similares que se envían al SP para evitar CSV demasiado largo
        private const int MaxAlumnosSimilares = 200;

        // teléfonos que son claramente placeholders (todos el mismo dígito)
        private static readonly HashSet<string> TelefonosPlaceholder = new HashSet<string>
        {
            "000000000", "111111111", "222222222", "333333333", "444444444",
            "555555555", "666666666", "777777777", "888888888", "999999999",
            "0000000000", "1111111111", "9999999999"
        };

        private readonly IUnitOfWork _unitOfWork;

        public ValidacionRn2Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Tipo Función: Business Rule
        /// Autor: (pendiente)
        /// Fecha: 2026-02-23
        /// Versión: 1.0
        /// <summary>
        /// Valida si una oportunidad está bloqueada por RN2.
        /// Retorna true = puede continuar | false = bloqueada.
        /// </summary>
        public bool ValidarLeadRn2Async(int idOportunidad)
        {
            try
            {
                // CASO: ID inválido — no tiene sentido consultar la BD, retornar excepcion
                if (idOportunidad <= 0)
                    throw new ArgumentOutOfRangeException(nameof(idOportunidad),
                        "El idOportunidad debe ser mayor a cero.");
                // ── PASO 1: ¿La oportunidad aplica RN2? ─────────────────────────────
                var datosAlumno = _unitOfWork.OportunidadRepository
                    .ObtenerIdAlumnoPorValidacionRN2(idOportunidad);

                // CASO: oportunidad no pertenece al área Ventas o no está en fases 41/10
                if (datosAlumno?.IdAlumno == null)
                    return true;

                // ── PASO 2: Normalizar datos de contacto ─────────────────────────────
                var telefonoNormalizado = NormalizarTelefono(datosAlumno.Telefono, datosAlumno.IdPais);
                var correoNormalizado   = NormalizarCorreo(datosAlumno.Correo);

                // ── PASO 3: Buscar alumnos con contacto similar (LIKE) ───────────────
                // CASO: sin datos de contacto normalizables → saltar búsqueda LIKE
                if (!string.IsNullOrEmpty(telefonoNormalizado) || !string.IsNullOrEmpty(correoNormalizado))
                {
                    var alumnosSimilares = _unitOfWork.OportunidadRepository
                        .BuscarAlumnosSimilaresPorCelularOCorreo(telefonoNormalizado, correoNormalizado)
                        ?? new List<AlumnoSimilarRn2DTO>();

                    // CASO: excluir al alumno actual + eliminar duplicados + limitar tamaño
                    var idsSimilares = alumnosSimilares
                        .Where(a => a.Id != datosAlumno.IdAlumno.Value)
                        .Select(a => a.Id)
                        .Distinct()
                        .Take(MaxAlumnosSimilares)
                        .ToList();

                    // ── PASO 3b: ¿Alguno de los similares tiene oportunidad activa RN2? ──
                    if (idsSimilares.Any())
                    {
                        var tieneOportunidades = _unitOfWork.OportunidadRepository
                            .ExistenOportunidadesParaAlumnos(idsSimilares);

                        // CASO: duplicado de contacto con oportunidad activa → BLOQUEADO
                        if (tieneOportunidades)
                            return false;
                    }
                }

                // ── PASO 4: ¿El propio alumno tiene más de una oportunidad? ──────────
                var totalOportunidades = _unitOfWork.OportunidadRepository
                    .ContarOportunidadesPorIdAlumno(datosAlumno.IdAlumno.Value);

                // CASO: el alumno ya tiene múltiples oportunidades activas → BLOQUEADO
                if (totalOportunidades > 1)
                    return false;

                // CASO: sin conflictos → PASA
                return true;
            }
            catch (Exception)
            {
                // relanzar preservando el stack trace original
                throw;
            }
        }

        /// <summary>
        /// Normaliza el teléfono extrayendo solo dígitos y quitando el prefijo de país.
        /// Casos:
        ///   1. Trae prefijo de país embebido  → se detecta con IdPais y se quita.
        ///   2. No trae prefijo pero IdPais tiene valor → se usa el número tal cual.
        ///   3. Contiene letras u otros chars  → se extraen solo los dígitos.
        ///   4. Es un placeholder (000..., 999...) → se descarta.
        ///   5. Queda con menos de 6 dígitos   → se descarta.
        /// </summary>
        private static string? NormalizarTelefono(string? telefono, int? idPais)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return null;

            // extraer solo dígitos: elimina +, espacios, guiones, paréntesis, puntos, letras, etc.
            var tel = new string(telefono.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(tel))
                return null;

            // quitar prefijo de país si ya viene incluido en el número
            if (idPais.HasValue)
            {
                var prefijo = idPais.Value.ToString();
                if (tel.StartsWith(prefijo) && tel.Length > prefijo.Length)
                    tel = tel.Substring(prefijo.Length);
            }

            // descartar si es un placeholder conocido (todos iguales o solo ceros)
            if (EsPlaceholder(tel))
                return null;

            // mínimo 6 dígitos para evitar falsos positivos en el LIKE
            if (tel.Length < 6)
                return null;

            return tel;
        }

        /// <summary>
        /// Normaliza el correo: trim + lowercase + validación de formato mínimo.
        /// Descarta si no contiene '@', si es demasiado corto o está vacío.
        /// </summary>
        private static string? NormalizarCorreo(string? correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return null;

            var mail = correo.Trim().ToLower();

            // debe contener '@' y tener al menos 5 caracteres (a@b.c)
            if (!mail.Contains('@') || mail.Length < 5)
                return null;

            // descartar si la parte local o el dominio están vacíos  (ej: "@dominio.com" o "usuario@")
            var partes = mail.Split('@');
            if (partes.Length != 2 || string.IsNullOrEmpty(partes[0]) || string.IsNullOrEmpty(partes[1]))
                return null;

            return mail;
        }

        /// <summary>
        /// Detecta teléfonos que son claramente placeholders:
        /// todos el mismo dígito o todos ceros.
        /// </summary>
        private static bool EsPlaceholder(string tel)
        {
            if (TelefonosPlaceholder.Contains(tel))
                return true;

            // todos los caracteres son el mismo dígito (ej: "99999999" de cualquier longitud)
            return tel.Distinct().Count() == 1;
        }
    }
}
