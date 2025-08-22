using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulante
    {
        public TPostulante()
        {
            TEtapaProcesoSeleccionCalificados = new HashSet<TEtapaProcesoSeleccionCalificado>();
            TExamenAsignadoEvaluadors = new HashSet<TExamenAsignadoEvaluador>();
            TPostulanteComparacions = new HashSet<TPostulanteComparacion>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del postulante
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Apellido Paterno del postulante
        /// </summary>
        public string ApellidoPaterno { get; set; } = null!;
        /// <summary>
        /// Apellido Materno del postulante
        /// </summary>
        public string? ApellidoMaterno { get; set; }
        /// <summary>
        /// Nro. de Documento  del postulante
        /// </summary>
        public string? NroDocumento { get; set; }
        /// <summary>
        /// Telefono del postulante
        /// </summary>
        public string? Telefono { get; set; }
        /// <summary>
        /// Celular del postulante
        /// </summary>
        public string? Celular { get; set; }
        /// <summary>
        /// Email del postulante
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Telefono del postulante
        /// </summary>
        public string? Telefono2 { get; set; }
        /// <summary>
        /// Celular del postulante
        /// </summary>
        public string? Celular2 { get; set; }
        /// <summary>
        /// Celular del postulante
        /// </summary>
        public string? Celular3 { get; set; }
        /// <summary>
        /// Email del postulante
        /// </summary>
        public string? Email2 { get; set; }
        /// <summary>
        /// Email del postulante
        /// </summary>
        public string? Email3 { get; set; }
        /// <summary>
        /// Fecha de nacimiento del postulante
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }
        /// <summary>
        /// Fk T_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Fk T_Ciudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Fk T_TipoDocumentoPersonal
        /// </summary>
        public int? IdTipoDocumento { get; set; }
        /// <summary>
        /// Fk T_Sexo
        /// </summary>
        public int? IdSexo { get; set; }
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
        /// Url perfil facebook
        /// </summary>
        public string? UrlPerfilFacebook { get; set; }
        /// <summary>
        /// Url Perfil Linkedin
        /// </summary>
        public string? UrlPerfilLinkedin { get; set; }
        /// <summary>
        /// True: si pertenece al proceso de seleccion anterior
        /// </summary>
        public bool? EsProcesoAnterior { get; set; }
        /// <summary>
        /// Edad del postulante
        /// </summary>
        public int? Edad { get; set; }
        /// <summary>
        /// Valida si el postulante tiene hijos
        /// </summary>
        public bool? TieneHijo { get; set; }
        /// <summary>
        /// Cantidad de hijos que tiene el postulante
        /// </summary>
        public int? CantidadHijo { get; set; }
        /// <summary>
        /// Foreign key de convocatoria personal
        /// </summary>
        public int? IdConvocatoriaPersonal { get; set; }
        /// <summary>
        /// Foreign key de Personal OperadorProceso
        /// </summary>
        public int? IdPersonalOperadorProceso { get; set; }
        /// <summary>
        /// Foreign key de PaginaReclutadoraPersona
        /// </summary>
        public int? IdPaginaReclutadoraPersonal { get; set; }
        /// <summary>
        /// Foreign key de PostulanteNivelPotencial
        /// </summary>
        public int? IdPostulanteNivelPotencial { get; set; }

        public virtual ICollection<TEtapaProcesoSeleccionCalificado> TEtapaProcesoSeleccionCalificados { get; set; }
        public virtual ICollection<TExamenAsignadoEvaluador> TExamenAsignadoEvaluadors { get; set; }
        public virtual ICollection<TPostulanteComparacion> TPostulanteComparacions { get; set; }
    }
}
