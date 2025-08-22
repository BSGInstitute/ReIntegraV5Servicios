using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFiltradoDeDatosPorPrioridadWhatsApp
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero movil de alumno
        /// </summary>
        public string Movil { get; set; } = null!;
        /// <summary>
        /// id de refrencia a la tabla mkt.T_Alumno
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// id del pais referencia a la tabla conf.T_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Id area capacitacion referenecia a la tabla pla.T_AreaCapacitacion
        /// </summary>
        public int? IdAreaCapacitacion { get; set; }
        /// <summary>
        /// id de programa general referenecia a la tabla pla.T_PGenera
        /// </summary>
        public int? IdProgramaGeneral { get; set; }
        /// <summary>
        /// Id de area formacion referencia a la tabla pla.T_AreaFormacion
        /// </summary>
        public int? IdAreaFormacion { get; set; }
        /// <summary>
        /// Id de area de tabala referncia  a la tabla pla.T_AreaTrabajo
        /// </summary>
        public int? IdAreaTrabajo { get; set; }
        /// <summary>
        /// Id del cargo referencia a la tabla pla.T_Cargo
        /// </summary>
        public int? IdCargo { get; set; }
        /// <summary>
        /// Id de la indutria referencia a la tabla pla.T_Industria
        /// </summary>
        public int? IdIndustria { get; set; }
        /// <summary>
        /// Id de la campania general referencia a la tabla mkt.T_CampaniaGeneral
        /// </summary>
        public int? IdCampaniaGeneral { get; set; }
        /// <summary>
        /// Prioridad de filtro
        /// </summary>
        public int? Prioridad { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// nivel de probabilidad
        /// </summary>
        public string? Probabilidad { get; set; }
        /// <summary>
        /// Indica si el mensaje fue o no enviado
        /// </summary>
        public bool? EstadoEnviado { get; set; }
        /// <summary>
        /// identificador unico de campania gneral detalle
        /// </summary>
        public int IdCampaniaGeneralDetalle { get; set; }
        /// <summary>
        /// Indica si el numero de contacto es valido para envio por WhatsApp
        /// </summary>
        public bool EsValidoParaWhatsApp { get; set; }

        public virtual TCampaniaGeneralDetalle IdCampaniaGeneralDetalleNavigation { get; set; } = null!;
    }
}
