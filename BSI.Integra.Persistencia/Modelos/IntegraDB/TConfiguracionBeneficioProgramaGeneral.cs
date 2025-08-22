using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionBeneficioProgramaGeneral
    {
        public TConfiguracionBeneficioProgramaGeneral()
        {
            TConfiguracionBeneficioProgramaGeneralDatoAdicionals = new HashSet<TConfiguracionBeneficioProgramaGeneralDatoAdicional>();
            TConfiguracionBeneficioProgramaGeneralEstadoMatriculas = new HashSet<TConfiguracionBeneficioProgramaGeneralEstadoMatricula>();
            TConfiguracionBeneficioProgramaGeneralPais = new HashSet<TConfiguracionBeneficioProgramaGeneralPai>();
            TConfiguracionBeneficioProgramaGeneralSubEstados = new HashSet<TConfiguracionBeneficioProgramaGeneralSubEstado>();
            TConfiguracionBeneficioProgramaGeneralVersions = new HashSet<TConfiguracionBeneficioProgramaGeneralVersion>();
        }

        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave foranea de la tabla pla.T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Clave foranea de la tabla pla.T_PartnerBeneficio y pla.T_DocumentoSeccion_PW
        /// </summary>
        public int IdBeneficio { get; set; }
        /// <summary>
        /// Identificador de que tabla esta jalando la informacion
        /// </summary>
        public int Tipo { get; set; }
        /// <summary>
        /// Si el registro esta asociado
        /// </summary>
        public bool? Asosiar { get; set; }
        /// <summary>
        /// el periodo de dias en la fecha del beneficio
        /// </summary>
        public int Entrega { get; set; }
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
        /// Porcentaje de avance academico
        /// </summary>
        public int? AvanceAcademico { get; set; }
        /// <summary>
        /// Indica si tiene deuda Pendiente
        /// </summary>
        public bool? DeudaPendiente { get; set; }
        /// <summary>
        /// Orden del Beneficio
        /// </summary>
        public int? OrdenBeneficio { get; set; }
        /// <summary>
        /// Columna que indica si requiere datos adicionales
        /// </summary>
        public bool? DatosAdicionales { get; set; }
        /// <summary>
        /// Requisitos de Beneficio
        /// </summary>
        public string? Requisitos { get; set; }
        /// <summary>
        /// Procesos de Solicitud de Beneficio
        /// </summary>
        public string? ProcesoSolicitud { get; set; }
        /// <summary>
        /// Detalles Adicionales de Beneficio
        /// </summary>
        public string? DetallesAdicionales { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralDatoAdicional> TConfiguracionBeneficioProgramaGeneralDatoAdicionals { get; set; }
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralEstadoMatricula> TConfiguracionBeneficioProgramaGeneralEstadoMatriculas { get; set; }
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralPai> TConfiguracionBeneficioProgramaGeneralPais { get; set; }
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralSubEstado> TConfiguracionBeneficioProgramaGeneralSubEstados { get; set; }
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralVersion> TConfiguracionBeneficioProgramaGeneralVersions { get; set; }
    }
}
