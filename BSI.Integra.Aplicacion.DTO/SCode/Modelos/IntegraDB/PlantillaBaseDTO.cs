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
}
