using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecifico
    {
        public TPespecifico()
        {
            TCarreraPreRequisitoPespecificos = new HashSet<TCarreraPreRequisitoPespecifico>();
            TConfigurarWebinars = new HashSet<TConfigurarWebinar>();
            TCursoPespecificos = new HashSet<TCursoPespecifico>();
            TFeedbackGrupoPreguntaProgramaEspecificos = new HashSet<TFeedbackGrupoPreguntaProgramaEspecifico>();
            TMaterialAdicionalAulaVirtualPespecificos = new HashSet<TMaterialAdicionalAulaVirtualPespecifico>();
            TPespecificoCodigoPartners = new HashSet<TPespecificoCodigoPartner>();
            TPreguntaProgramaCapacitacions = new HashSet<TPreguntaProgramaCapacitacion>();
            TProgramaGeneralMaterialEstudioAdicionalEspecificos = new HashSet<TProgramaGeneralMaterialEstudioAdicionalEspecifico>();
            TSolicitudAlumnos = new HashSet<TSolicitudAlumno>();
            TSolicitudOperacionesAccesoTemporalDetalles = new HashSet<TSolicitudOperacionesAccesoTemporalDetalle>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del programa 
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo del programa
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Es foreing key tCentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// frecuencia en la que se dan las clases (mensual, quincenal, diaria)
        /// </summary>
        public string? Frecuencia { get; set; }
        /// <summary>
        /// Estado del programa (Ejecucion, Lanzamiento, Cancelado, Concluido)
        /// </summary>
        public string EstadoP { get; set; } = null!;
        /// <summary>
        /// Modalidad en la que se dan las clases (Online, Presencial, Aonline, etc)
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Es foreing key tAmbiente
        /// </summary>
        public string? TipoAmbiente { get; set; }
        /// <summary>
        /// Categoria a la que pertenece el programa (Diplomado, Curso, Programa, etc)
        /// </summary>
        public string Categoria { get; set; } = null!;
        /// <summary>
        /// Es foreing key tPGeneral
        /// </summary>
        public int? IdProgramaGeneral { get; set; }
        /// <summary>
        /// Ciudad en donde se ejecuta el programa
        /// </summary>
        public string Ciudad { get; set; } = null!;
        /// <summary>
        /// Fecha de inicio del programa
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha de fin del programa
        /// </summary>
        public DateTime? FechaTermino { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public string? FechaInicioV { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public string? FechaTerminoV { get; set; }
        /// <summary>
        /// Codigo de pago en el banco
        /// </summary>
        public string? CodigoBanco { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public string? FechaInicioP { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public string? FechaTerminoP { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int? FrecuenciaId { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int? EstadoPid { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int? TipoId { get; set; }
        /// <summary>
        /// Es foreing key tCategoriaP
        /// </summary>
        public int? CategoriaId { get; set; }
        /// <summary>
        /// Es foreing key tOrigenPrograma
        /// </summary>
        public short? OrigenPrograma { get; set; }
        /// <summary>
        /// Es foreing key tCiudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Usuario de la coordinadora academica asignada al programa
        /// </summary>
        public string? CoordinadoraAcademica { get; set; }
        /// <summary>
        /// Usuario de la coordinadora academica asignada para hacer las cobranzas
        /// </summary>
        public string? CoordinadoraCobranza { get; set; }
        /// <summary>
        /// Duracion del programa
        /// </summary>
        public string? Duracion { get; set; }
        /// <summary>
        /// Indica si el registro fue actualizado en un proceso automatico o no (0, 1)
        /// </summary>
        public string? ActualizacionAutomatica { get; set; }
        /// <summary>
        /// Identificador del campo IdCursoM de la tabla T_MoodleCurso
        /// </summary>
        public int? IdCursoMoodle { get; set; }
        /// <summary>
        /// Indica si es un curso individual o no (1, 0)
        /// </summary>
        public bool? CursoIndividual { get; set; }
        /// <summary>
        /// Es foreing key tPLA_PEspecificoSesion 
        /// </summary>
        public int? IdSesionInicio { get; set; }
        /// <summary>
        /// Es foreing key tPLA_Expositor
        /// </summary>
        public int? IdExpositorReferencia { get; set; }
        /// <summary>
        /// Es foreing key TPLA_Ambiente
        /// </summary>
        public int? IdAmbiente { get; set; }
        /// <summary>
        /// Url del documento del cronograma de pagos 
        /// </summary>
        public string? UrlDocumentoCronograma { get; set; }
        /// <summary>
        /// FK con T_EstadoPEspecifico
        /// </summary>
        public int? IdEstadoPespecifico { get; set; }
        /// <summary>
        /// Estado del registro (creado, eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que hizo la ultima modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de la ultima modificacion del registro
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
        /// Url del documento del cronograma de alumnos con todos los grupos
        /// </summary>
        public string? UrlDocumentoCronogramaGrupos { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_TroncalPartner
        /// </summary>
        public int? IdTroncalPartner { get; set; }
        /// <summary>
        /// Id de Moodle del aula virtual de prueba
        /// </summary>
        public int? IdCursoMoodlePrueba { get; set; }
        /// <summary>
        /// Id en v3 con la tabla ra_Curso
        /// </summary>
        public int? IdCursoRa { get; set; }
        /// <summary>
        /// clave foranea de la tabla fin.T_Proveedor
        /// </summary>
        public int? IdProveedor { get; set; }
        /// <summary>
        /// clave foranea de la tabla fin.T_Proveedor
        /// </summary>
        public int? IdProveedorCalificaProyecto { get; set; }
        public string? ObservacionCursoFinalizado { get; set; }
        /// <summary>
        /// Identificador de sesión especial
        /// </summary>
        public bool? EsEspecial { get; set; }
        /// <summary>
        /// Cantidad de creditos teoria
        /// </summary>
        public double? CreditosTeoricos { get; set; }
        /// <summary>
        /// Cantidad de creditos practica
        /// </summary>
        public double? CreditosPracticos { get; set; }
        /// <summary>
        /// Cantidad de creditos totales
        /// </summary>
        public double? CreditosTotales { get; set; }
        /// <summary>
        /// Cantidad de horas teoricas
        /// </summary>
        public int? HorasTeoricas { get; set; }
        /// <summary>
        /// Cantidad de horas practicas
        /// </summary>
        public int? HorasPracticas { get; set; }
        /// <summary>
        /// Cantidad de horas totales
        /// </summary>
        public int? HorasTotales { get; set; }
        /// <summary>
        /// Periodo lectivo
        /// </summary>
        public int? IdPeriodoLectivo { get; set; }
        /// <summary>
        /// URL del cronograma general
        /// </summary>
        public string? UrlCronogramaSemanal { get; set; }
        /// <summary>
        /// (1: Formacion, 2: Carrera)
        /// </summary>
        public int? IdTipoProgramaCarrera { get; set; }
        /// <summary>
        /// Foreign Key de id tabla Ciclo
        /// </summary>
        public int? IdCiclo { get; set; }
        /// <summary>
        /// Indica si el registro se filtrará en resumen de sesiones
        /// </summary>
        public bool? ResumenClaseActivo { get; set; }
        /// <summary>
        /// Indica si el registro tendrá Tutor Virtual habilitado
        /// </summary>
        public bool? TutorVirtualActivo { get; set; }

        public virtual TCentroCosto? IdCentroCostoNavigation { get; set; }
        public virtual ICollection<TCarreraPreRequisitoPespecifico> TCarreraPreRequisitoPespecificos { get; set; }
        public virtual ICollection<TConfigurarWebinar> TConfigurarWebinars { get; set; }
        public virtual ICollection<TCursoPespecifico> TCursoPespecificos { get; set; }
        public virtual ICollection<TFeedbackGrupoPreguntaProgramaEspecifico> TFeedbackGrupoPreguntaProgramaEspecificos { get; set; }
        public virtual ICollection<TMaterialAdicionalAulaVirtualPespecifico> TMaterialAdicionalAulaVirtualPespecificos { get; set; }
        public virtual ICollection<TPespecificoCodigoPartner> TPespecificoCodigoPartners { get; set; }
        public virtual ICollection<TPreguntaProgramaCapacitacion> TPreguntaProgramaCapacitacions { get; set; }
        public virtual ICollection<TProgramaGeneralMaterialEstudioAdicionalEspecifico> TProgramaGeneralMaterialEstudioAdicionalEspecificos { get; set; }
        public virtual ICollection<TSolicitudAlumno> TSolicitudAlumnos { get; set; }
        public virtual ICollection<TSolicitudOperacionesAccesoTemporalDetalle> TSolicitudOperacionesAccesoTemporalDetalles { get; set; }
    }
}
