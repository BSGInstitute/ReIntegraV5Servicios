using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonal
    {
        public TPersonal()
        {
            TBandejaPendientePws = new HashSet<TBandejaPendientePw>();
            TCampaniaGeneralDetalles = new HashSet<TCampaniaGeneralDetalle>();
            TConfiguracionAccesoPersonalIdPersonalAccesoNavigations = new HashSet<TConfiguracionAccesoPersonal>();
            TConfiguracionAccesoPersonalIdPersonalNavigations = new HashSet<TConfiguracionAccesoPersonal>();
            TConvocatoriaPersonals = new HashSet<TConvocatoriaPersonal>();
            TExamenAsignadoEvaluadors = new HashSet<TExamenAsignadoEvaluador>();
            THorarioGrupoPersonals = new HashSet<THorarioGrupoPersonal>();
            TModoPersonalFurs = new HashSet<TModoPersonalFur>();
            TPerfilPuestoTrabajoPersonalAprobacions = new HashSet<TPerfilPuestoTrabajoPersonalAprobacion>();
            TPersonalCertificacions = new HashSet<TPersonalCertificacion>();
            TPersonalEncargadoDeEnvioDeConsulta = new HashSet<TPersonalEncargadoDeEnvioDeConsultum>();
            TPersonalLogs = new HashSet<TPersonalLog>();
            TPersonalMotivoTiempoInactividads = new HashSet<TPersonalMotivoTiempoInactividad>();
            TPersonalPuestoSedeHistoricos = new HashSet<TPersonalPuestoSedeHistorico>();
            TPlantillaRevisionPws = new HashSet<TPlantillaRevisionPw>();
            TPrioridadMailChimpLista = new HashSet<TPrioridadMailChimpListum>();
            TSmsConfiguracionEnvios = new HashSet<TSmsConfiguracionEnvio>();
            TSolicitudAlumnos = new HashSet<TSolicitudAlumno>();
            TSolicitudCertificadoFisicos = new HashSet<TSolicitudCertificadoFisico>();
            TSolicitudIdPersonalRevisionNavigations = new HashSet<TSolicitud>();
            TSolicitudIdPersonalSolucionNavigations = new HashSet<TSolicitud>();
            TSolicitudInternas = new HashSet<TSolicitudInterna>();
            TSolicitudTis = new HashSet<TSolicitudTi>();
            TWhatsAppConfiguracionEnvios = new HashSet<TWhatsAppConfiguracionEnvio>();
            TWhatsAppUsuarios = new HashSet<TWhatsAppUsuario>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombres 
        /// </summary>
        public string? Nombres { get; set; }
        /// <summary>
        /// Apellidos 
        /// </summary>
        public string? Apellidos { get; set; }
        /// <summary>
        /// Area a la que pertenece
        /// </summary>
        public string? Rol { get; set; }
        /// <summary>
        /// Rol que ejerce en su area de trabajo
        /// </summary>
        public string? TipoPersonal { get; set; }
        /// <summary>
        /// Email de contacto 
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Abreviacion del area a la que pertenece
        /// </summary>
        public string? AreaAbrev { get; set; }
        /// <summary>
        /// Numero de anexo
        /// </summary>
        public string? Anexo { get; set; }
        /// <summary>
        /// Identificador del jefe 
        /// </summary>
        public int? IdJefe { get; set; }
        /// <summary>
        /// Direccion Ip de la central de llamadas 
        /// </summary>
        public string? Central { get; set; }
        /// <summary>
        /// Si el personal se encuentra activo o cesado
        /// </summary>
        public bool? Activo { get; set; }
        /// <summary>
        /// Apellido paterno del personal
        /// </summary>
        public string? ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido materno del personal
        /// </summary>
        public string? ApellidoMaterno { get; set; }
        /// <summary>
        /// FK de T_Sexo
        /// </summary>
        public int? IdSexo { get; set; }
        /// <summary>
        /// FK de T_EstadoCivil
        /// </summary>
        public int? IdEstadocivil { get; set; }
        /// <summary>
        /// Fecha de nacimiento del personal
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }
        /// <summary>
        /// FK de T_Pais
        /// </summary>
        public int? IdPaisNacimiento { get; set; }
        /// <summary>
        /// FK de T_Region
        /// </summary>
        public int? IdRegion { get; set; }
        /// <summary>
        /// FK de T_Ciudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// FK de T_TipoDocumento
        /// </summary>
        public int? IdTipoDocumento { get; set; }
        /// <summary>
        /// Numero de documento del personal
        /// </summary>
        public string? NumeroDocumento { get; set; }
        /// <summary>
        /// EsSalud
        /// </summary>
        public string? AutogeneradoEssalud { get; set; }
        /// <summary>
        /// FK de T_TipoSangre
        /// </summary>
        public int? IdTipoSangre { get; set; }
        /// <summary>
        /// URL de la firma de correos
        /// </summary>
        public string? UrlFirmaCorreos { get; set; }
        /// <summary>
        /// FK de T_GrupoProgramasCriticos
        /// </summary>
        public int? IdGrupoProgramasCriticos { get; set; }
        /// <summary>
        /// FK de T_Personal para personal cerrador
        /// </summary>
        public int? IdCerrador { get; set; }
        /// <summary>
        /// Indica si es cerrador o no
        /// </summary>
        public bool? EsCerrador { get; set; }
        /// <summary>
        /// FK de T_Pais para direccion
        /// </summary>
        public int? IdPaisDireccion { get; set; }
        /// <summary>
        /// FK T_Region para direccion
        /// </summary>
        public int? IdRegionDireccion { get; set; }
        /// <summary>
        /// Ciudad para la direccion
        /// </summary>
        public string? CiudadDireccion { get; set; }
        /// <summary>
        /// Nombre de la direccion
        /// </summary>
        public string? NombreDireccion { get; set; }
        /// <summary>
        /// Telefono fijo de referencia
        /// </summary>
        public string? FijoReferencia { get; set; }
        /// <summary>
        /// Celular de referencia
        /// </summary>
        public string? MovilReferencia { get; set; }
        /// <summary>
        /// Email de referencia
        /// </summary>
        public string? EmailReferencia { get; set; }
        /// <summary>
        /// FK de T_SistemaPensionario
        /// </summary>
        public int? IdSistemaPensionario { get; set; }
        /// <summary>
        /// FK de T_EntidadSistemaPensionario
        /// </summary>
        public int? IdEntidadSistemaPensionario { get; set; }
        /// <summary>
        /// Nombre del CUSPP
        /// </summary>
        public string? NombreCuspp { get; set; }
        /// <summary>
        /// Distrito de la direccion
        /// </summary>
        public string? DistritoDireccion { get; set; }
        /// <summary>
        /// Si cuenta con EsSalud o no
        /// </summary>
        public bool? ConEssalud { get; set; }
        /// <summary>
        /// Identificador de Busqueda
        /// </summary>
        public int? IdBusqueda { get; set; }
        /// <summary>
        /// alias del email del asesor
        /// </summary>
        public string? AliasEmailAsesor { get; set; }
        /// <summary>
        /// Anexo del 3cx
        /// </summary>
        public string? Anexo3Cx { get; set; }
        /// <summary>
        /// Id del 3cx
        /// </summary>
        public string? Id3Cx { get; set; }
        /// <summary>
        /// Password del 3 CX
        /// </summary>
        public string? Password3Cx { get; set; }
        /// <summary>
        /// Dominio
        /// </summary>
        public string? Dominio { get; set; }
        /// <summary>
        /// Id del Usuario en Facebook
        /// </summary>
        public long? IdFacebookPersonal { get; set; }
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
        /// Indica si es persona valida o no
        /// </summary>
        public bool? EsPersonaValida { get; set; }
        /// <summary>
        /// Almacena la url de la foto en el repositorio web
        /// </summary>
        public string? UrlFoto { get; set; }
        /// <summary>
        /// Almacena la firma en formato html
        /// </summary>
        public bool? AplicaFirmaHtml { get; set; }
        /// <summary>
        /// Indica si el personal aplica firma html
        /// </summary>
        public string? FirmaHtml { get; set; }
        /// <summary>
        /// Almacena el cargo con el que aparecera en la firma
        /// </summary>
        public string? CargoFirmaHtml { get; set; }
        /// <summary>
        /// FK T_Postulante
        /// </summary>
        public int? IdPostulante { get; set; }
        /// <summary>
        /// Usuario para loguearse en Asterisk
        /// </summary>
        public int? UsuarioAsterisk { get; set; }
        /// <summary>
        /// Constraseña para loguearse en Asterisk
        /// </summary>
        public string? ContrasenaAsterisk { get; set; }
        /// <summary>
        /// Es Foreign Key de T_TableroComercialCategoriaAsesor
        /// </summary>
        public int? IdTableroComercialCategoriaAsesor { get; set; }
        /// <summary>
        /// FK de T_PuestoTrabajoNivel
        /// </summary>
        public int? IdPuestoTrabajoNivel { get; set; }
        /// <summary>
        /// FK de T_PersonalAreaTrabajo
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// FK de T_PersonalArchivo
        /// </summary>
        public int? IdPersonalArchivo { get; set; }
        /// <summary>
        /// FK de T_RolUsuarioTicket
        /// </summary>
        public int? IdRolUsuarioTicket { get; set; }
        /// <summary>
        /// Campo que indica si esta el discador activo para empezar a llamar
        /// </summary>
        public bool? DiscadorActivo { get; set; }
        /// <summary>
        /// Diferencia horaria para agenda
        /// </summary>
        public int? DiferenciaHoraria { get; set; }
        /// <summary>
        /// Indica si el personal esta en llamada o no.
        /// </summary>
        public bool? EnLlamada { get; set; }
        /// <summary>
        /// Indica si esta en la cola de marcador predictivo
        /// </summary>
        public bool? MarcadorPredictivoActivo { get; set; }
        /// <summary>
        /// (FK) de conf.T_Pais
        /// </summary>
        public int? IdPaisSede { get; set; }
        /// <summary>
        /// Almacena el Id del pais solo en caso que se modifique su zona horaria
        /// </summary>
        public int? CodigoPaisDiferenciaHoraria { get; set; }
        /// <summary>
        /// Columna encargada de relacionar el Dominio con el asesor
        /// </summary>
        public int? IdDominioPbx { get; set; }
        /// <summary>
        /// Direccion IP 1 de conexion
        /// </summary>
        public string? Ip1 { get; set; }
        /// <summary>
        /// Direccion IP 2 de conexion
        /// </summary>
        public string? Ip2 { get; set; }
        /// <summary>
        /// Primer Nombre
        /// </summary>
        public string? Nombre1 { get; set; }
        /// <summary>
        /// Segundo Nombre
        /// </summary>
        public string? Nombre2 { get; set; }

        public virtual ICollection<TBandejaPendientePw> TBandejaPendientePws { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalle> TCampaniaGeneralDetalles { get; set; }
        public virtual ICollection<TConfiguracionAccesoPersonal> TConfiguracionAccesoPersonalIdPersonalAccesoNavigations { get; set; }
        public virtual ICollection<TConfiguracionAccesoPersonal> TConfiguracionAccesoPersonalIdPersonalNavigations { get; set; }
        public virtual ICollection<TConvocatoriaPersonal> TConvocatoriaPersonals { get; set; }
        public virtual ICollection<TExamenAsignadoEvaluador> TExamenAsignadoEvaluadors { get; set; }
        public virtual ICollection<THorarioGrupoPersonal> THorarioGrupoPersonals { get; set; }
        public virtual ICollection<TModoPersonalFur> TModoPersonalFurs { get; set; }
        public virtual ICollection<TPerfilPuestoTrabajoPersonalAprobacion> TPerfilPuestoTrabajoPersonalAprobacions { get; set; }
        public virtual ICollection<TPersonalCertificacion> TPersonalCertificacions { get; set; }
        public virtual ICollection<TPersonalEncargadoDeEnvioDeConsultum> TPersonalEncargadoDeEnvioDeConsulta { get; set; }
        public virtual ICollection<TPersonalLog> TPersonalLogs { get; set; }
        public virtual ICollection<TPersonalMotivoTiempoInactividad> TPersonalMotivoTiempoInactividads { get; set; }
        public virtual ICollection<TPersonalPuestoSedeHistorico> TPersonalPuestoSedeHistoricos { get; set; }

        public virtual ICollection<TPlantillaRevisionPw> TPlantillaRevisionPws { get; set; }
        public virtual ICollection<TPrioridadMailChimpListum> TPrioridadMailChimpLista { get; set; }
        public virtual ICollection<TSmsConfiguracionEnvio> TSmsConfiguracionEnvios { get; set; }
        public virtual ICollection<TSolicitudAlumno> TSolicitudAlumnos { get; set; }
        public virtual ICollection<TSolicitudCertificadoFisico> TSolicitudCertificadoFisicos { get; set; }
        public virtual ICollection<TSolicitud> TSolicitudIdPersonalRevisionNavigations { get; set; }
        public virtual ICollection<TSolicitud> TSolicitudIdPersonalSolucionNavigations { get; set; }
        public virtual ICollection<TSolicitudInterna> TSolicitudInternas { get; set; }
        public virtual ICollection<TSolicitudTi> TSolicitudTis { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvio> TWhatsAppConfiguracionEnvios { get; set; }
        public virtual ICollection<TWhatsAppUsuario> TWhatsAppUsuarios { get; set; }
    }
}
