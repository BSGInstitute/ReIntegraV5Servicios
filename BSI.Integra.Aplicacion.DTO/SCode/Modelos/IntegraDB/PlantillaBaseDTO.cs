namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaBaseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class PlantillaBaseComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
    public class SpeechBienvenidaDespedidaDTO
    {
        public int IdPlantillaBienvenida { get; set; }
        public int IdPlantillaDespedida { get; set; }
    }
    public class PlantillaWhatsAppDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int TipoPlantilla { get; set; }
    }
    /// <summary>
    /// DTO para almacenar la información de Speech Bienvenida de Convenio de Voz
    /// </summary>
    public class ConvenioDeVozPlantillaDTO
    {
        /// <summary>
        /// ID de la plantilla base
        /// </summary>
        public int IdPlantilla { get; set; }

        /// <summary>
        /// ID de la plantilla clave-valor específica
        /// </summary>
        public int IdPlantillaClaveValor { get; set; }

        /// <summary>
        /// Clave de la plantilla
        /// </summary>
        public string Clave { get; set; }

        /// <summary>
        /// Valor/contenido de la plantilla con etiquetas para reemplazar
        /// Ejemplo: "Hola {tAlumnos.nombre1}, bienvenido al programa {tPLA_PGeneral.Nombre}"
        /// </summary>
        public string Valor { get; set; }

        /// <summary>
        /// ID del área etiqueta (opcional)
        /// </summary>
        public int? IdAreaEtiqueta { get; set; }

        /// <summary>
        /// ID de la fase de oportunidad utilizada
        /// </summary>
        public int IdFaseOportunidad { get; set; }

        /// <summary>
        /// ID de la actividad detalle utilizada
        /// </summary>
        public int IdActividadDetalle { get; set; }
    }

    /// <summary>
    /// DTO para el speech de bienvenida y despedida procesado con etiquetas reemplazadas
    /// Consolida todo el flujo: obtención de datos, filtrado por país, y reemplazo de etiquetas
    /// Autor: Joseph Llanque
    /// Fecha: 05/01/2026
    /// </summary>
    public class SpeechBienvenidaProcesadoDTO
    {
        /// <summary>
        /// Texto del speech de bienvenida con todas las etiquetas reemplazadas
        /// </summary>
        public string SpeechBienvenida { get; set; }

        /// <summary>
        /// Texto del speech de despedida con todas las etiquetas reemplazadas
        /// </summary>
        public string SpeechDespedida { get; set; }

        /// <summary>
        /// ID de la plantilla de bienvenida utilizada
        /// </summary>
        public int IdPlantillaBienvenida { get; set; }

        /// <summary>
        /// ID de la plantilla de despedida utilizada
        /// </summary>
        public int IdPlantillaDespedida { get; set; }

        /// <summary>
        /// ID de la fase de oportunidad utilizada
        /// </summary>
        public int IdFaseOportunidad { get; set; }

        /// <summary>
        /// ID del código de país del alumno (51=Perú, 57=Colombia, etc.)
        /// </summary>
        public int? IdCodigoPais { get; set; }
    }

    /// <summary>
    /// DTO que encapsula todos los datos necesarios para el reemplazo de etiquetas en templates
    /// Utilizado internamente por el servicio de procesamiento de speech
    /// Autor: Joseph Llanque
    /// Fecha: 05/01/2026
    /// </summary>
    public class DatosReemplazoEtiquetaDTO
    {
        // Datos del Alumno
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email1 { get; set; }
        public int? IdCodigoPais { get; set; }

        // Datos del Personal
        public string NombresPersonal { get; set; }
        public string ApellidosPersonal { get; set; }

        // Datos del Programa
        public string ProgramaGeneral { get; set; }

        // Datos adicionales para procesamiento
        public int IdCentroCosto { get; set; }
        public int? IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
    }
}
