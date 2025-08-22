using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMatriculaCabecera
    {
        public TMatriculaCabecera()
        {
            TMatriculaFormularioProgresivos = new HashSet<TMatriculaFormularioProgresivo>();
            TOportunidadClasificacionOperaciones = new HashSet<TOportunidadClasificacionOperacione>();
            TReclamos = new HashSet<TReclamo>();
            TRecuperacionSesions = new HashSet<TRecuperacionSesion>();
            TSolicitudAlumnos = new HashSet<TSolicitudAlumno>();
            TSolicitudCertificadoFisicos = new HashSet<TSolicitudCertificadoFisico>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// codigo de matricula
        /// </summary>
        public string CodigoMatricula { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key tAlumnos
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Es Foreing Key tPEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Estado del pago de la matricula, Fk con la tabla T_EstadoPagoMatricula
        /// </summary>
        public int IdEstadoPagoMatricula { get; set; }
        /// <summary>
        /// Estado del pago de la matricula, Fk con la tabla T_EstadoPagoMatricula, el valor es legacy para reportes
        /// </summary>
        public string? EstadoMatricula { get; set; }
        /// <summary>
        /// Fecha de la matricula
        /// </summary>
        public DateTime? FechaMatricula { get; set; }
        /// <summary>
        /// RUC de la empresa
        /// </summary>
        public string? EmpresaRuc { get; set; }
        /// <summary>
        /// Nombre de la empresa
        /// </summary>
        public string? EmpresaNombre { get; set; }
        /// <summary>
        /// Nombre empresa contacto
        /// </summary>
        public string? EmpresaContacto { get; set; }
        /// <summary>
        /// Email empresa
        /// </summary>
        public string? EmpresaEmail { get; set; }
        /// <summary>
        /// Si empresa corre con los gastos
        /// </summary>
        public string? EmpresaPaga { get; set; }
        /// <summary>
        /// Observaciones de la empresa
        /// </summary>
        public string? EmpresaObservaciones { get; set; }
        /// <summary>
        /// Es Foreing Key tDocumentoPago
        /// </summary>
        public int? IdDocumentoPago { get; set; }
        /// <summary>
        /// Es Foreing Key tPersonal
        /// </summary>
        public int? IdCoordinador { get; set; }
        /// <summary>
        /// Es Foreing Key tPersonal
        /// </summary>
        public int? IdAsesor { get; set; }
        /// <summary>
        /// Es Foreing Key tEstados_Matricula
        /// </summary>
        public int? IdEstadoMatricula { get; set; }
        /// <summary>
        /// Fecha de suspension
        /// </summary>
        public string? FechaSuspendido { get; set; }
        /// <summary>
        /// Usuario del coordinador
        /// </summary>
        public string? UsuarioCoordinadorAcademico { get; set; }
        /// <summary>
        /// Observaciones de la matricula
        /// </summary>
        public string? ObservacionGeneralOperaciones { get; set; }
        /// <summary>
        /// Si es un usuario en supervision
        /// </summary>
        public string? UsuarioCoordinadorSupervision { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_MontoPagoCronogramas
        /// </summary>
        public int? IdCronograma { get; set; }
        /// <summary>
        /// TCRM_Periodo
        /// </summary>
        public int? IdPeriodo { get; set; }
        /// <summary>
        /// Columna para la preasignacion de coordinadores de operaciones
        /// </summary>
        public string? UsuarioCoordinadorPreAsignacion { get; set; }
        /// <summary>
        /// Campo para la validación de los datos de la matricula está conforme - utilizada por operaciones
        /// </summary>
        public bool? VerificacionConforme { get; set; }
        /// <summary>
        /// Indica si la Fecha Matricula esta validada
        /// </summary>
        public bool? FechaMatriculaValidada { get; set; }
        /// <summary>
        /// Indica si la Fecha de Pago esta validada
        /// </summary>
        public bool? FechaPagoValidada { get; set; }
        /// <summary>
        /// Fecha en la que se devolvio el dinero al alumno
        /// </summary>
        public DateTime? FechaRetiro { get; set; }
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
        public string? IdMigracion { get; set; }
        /// <summary>
        /// Indica el grupo al que pertenece el alumno
        /// </summary>
        public int? GrupoCurso { get; set; }
        /// <summary>
        /// Es clave foránea de T_SubEstadoMatricula
        /// </summary>
        public int? IdSubEstadoMatricula { get; set; }
        /// <summary>
        /// paquete elegido
        /// </summary>
        public int? IdPaquete { get; set; }
        /// <summary>
        /// Fecha finalizacion del programa del alumno
        /// </summary>
        public DateTime? FechaFinalizacion { get; set; }
        /// <summary>
        /// Es Foreing Key TEstadoMatricula cuando se puede certificar
        /// </summary>
        public int? IdEstadoMatriculaCertificado { get; set; }
        /// <summary>
        /// Es Foreing Key TSubEstadoMatricula cuando se puede certificar
        /// </summary>
        public int? IdSubEstadoMatriculaCertificado { get; set; }
        /// <summary>
        /// Verifica si una matricula es inhouse o no
        /// </summary>
        public bool? EsInhouse { get; set; }
        /// <summary>
        /// Fecha paso pormatricular a matriculado
        /// </summary>
        public DateTime? FechaPorMatricularMatriculado { get; set; }

        public virtual ICollection<TMatriculaFormularioProgresivo> TMatriculaFormularioProgresivos { get; set; }
        public virtual ICollection<TOportunidadClasificacionOperacione> TOportunidadClasificacionOperaciones { get; set; }
        public virtual ICollection<TReclamo> TReclamos { get; set; }
        public virtual ICollection<TRecuperacionSesion> TRecuperacionSesions { get; set; }
        public virtual ICollection<TSolicitudAlumno> TSolicitudAlumnos { get; set; }
        public virtual ICollection<TSolicitudCertificadoFisico> TSolicitudCertificadoFisicos { get; set; }
    }
}
