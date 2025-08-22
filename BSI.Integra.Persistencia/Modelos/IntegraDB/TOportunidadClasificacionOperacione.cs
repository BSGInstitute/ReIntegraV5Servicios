using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOportunidadClasificacionOperacione
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es clave foránea de T_Oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Es clave foránea de T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Cantidad de dias de atraso de la ultima cuota no pagada
        /// </summary>
        public int DiasAtrasoCuotaPago { get; set; }
        /// <summary>
        /// Cantidad de Cuotas de pago atrasadas.
        /// </summary>
        public int CuotasAtrasoCuotaPago { get; set; }
        /// <summary>
        /// Monto pendiente a pagar respecto a cuota de pago atrasado.
        /// </summary>
        public decimal MontoAtrasoCuotaPago { get; set; }
        /// <summary>
        /// Moneda de la cuota de pago
        /// </summary>
        public string MonedaCuotaPago { get; set; } = null!;
        /// <summary>
        /// Es clave foranea de T_AgendaTab
        /// </summary>
        public int IdAgendaTab { get; set; }
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
        /// Usuario de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
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
        /// Dias que la oportunidad esta en seguimiento
        /// </summary>
        public int? DiasSeguimiento { get; set; }
        /// <summary>
        /// Dias que las Actividades Detalle de la Oportunidad son Ejecutadas
        /// </summary>
        public int? DiasActividadesEjecutadas { get; set; }
        public string? UltimoContacto { get; set; }
        public DateTime? FechaUltimaActividadEjecutada { get; set; }
        /// <summary>
        /// Si hubo seguimiento en los ultimos 7 dias.
        /// </summary>
        public int? DiasSeguimiento7dias { get; set; }
        /// <summary>
        /// Ver si hubo actividades ejecutadas en los ultimos 7 dias.
        /// </summary>
        public int? DiasActividadesEjecutadas7dias { get; set; }
        /// <summary>
        /// Ver si hubo actividades de seguimiento en los ultimos 14 dias
        /// </summary>
        public int? DiasSeguimiento14dias { get; set; }
        /// <summary>
        /// Ver si hubo actividades ejecutadas en los ultimos 14 dias
        /// </summary>
        public int? DiasActividadesEjecutadas14dias { get; set; }
        /// <summary>
        /// Ver si hubo actividades de seguimiento en los ultimos 21 dias.
        /// </summary>
        public int? DiasSeguimiento21dias { get; set; }
        /// <summary>
        /// Ver si hubo actividades ejecutadas en los ultimos 21 dias.
        /// </summary>
        public int? DiasActividadesEjecutadas21dias { get; set; }
        /// <summary>
        /// Conteo de actividades de realizadas de 15 a 30 dias
        /// </summary>
        public int? DiasSeguimiento3014dias { get; set; }
        /// <summary>
        /// Conteo de actividades de ejecutadas de 15 a 30 dias
        /// </summary>
        public int? DiasActividadesEjecutadas3014dias { get; set; }
        /// <summary>
        /// Conteo de actividades de realizadas de 21 a 30 dias
        /// </summary>
        public int? DiasSeguimiento3021dias { get; set; }
        /// <summary>
        /// Conteo de actividades de realizadas de 21 a 30 dias
        /// </summary>
        public int? DiasActividadesEjecutadas3021dias { get; set; }
        /// <summary>
        /// Seguimiento realizado en los ultimos 60 dias.
        /// </summary>
        public int? DiasSeguimiento60dias { get; set; }
        /// <summary>
        /// Actividades ejecutadas en los ultimos 60 dias.
        /// </summary>
        public int? DiasActividadesEjecutadas60dias { get; set; }
        /// <summary>
        /// Seguimiento realizado en los ultimos 90 dias.
        /// </summary>
        public int? DiasSeguimiento90dias { get; set; }
        /// <summary>
        /// Actividades ejecutadas en los ultimos 90 dias.
        /// </summary>
        public int? DiasActividadesEjecutadas90dias { get; set; }
        /// <summary>
        /// Dias de atraso academico
        /// </summary>
        public int? DiasAtrasoAvanceAcademico { get; set; }
        /// <summary>
        /// Estado de autoevaluaciones
        /// </summary>
        public string? EstadoAutoevaluaciones { get; set; }
        /// <summary>
        /// Fecha de pago de la proxima cuota.
        /// </summary>
        public DateTime? FechaProximaCuota { get; set; }
        /// <summary>
        /// Dias de atraso de avance academico sin proyecto.
        /// </summary>
        public int? DiasAtrasoAvanceAcademicoSinProyecto { get; set; }
        /// <summary>
        /// Es foreign key de mkt.T_Tarifario
        /// </summary>
        public int? IdTarifario { get; set; }
        /// <summary>
        /// Porcentaje de avance academico sin proyecto.
        /// </summary>
        public int? PorcentajeAvanceAcademicosinProyecto { get; set; }
        /// <summary>
        /// Nota promedio
        /// </summary>
        public decimal? NotaPromedio { get; set; }
        /// <summary>
        /// Indica si tiene proyecto final.
        /// </summary>
        public bool? TieneProyectoFinal { get; set; }
        /// <summary>
        /// Porcentaje de avance academico.
        /// </summary>
        public int? PorcentajeAvanceAcademico { get; set; }
        /// <summary>
        /// numero de dias desde su ultimo Avance en el aula virtual
        /// </summary>
        public int? DiasDesdeUltimoAvance { get; set; }
        /// <summary>
        /// evaluaciones o sesiones avanzadas
        /// </summary>
        public string? AvanceReal { get; set; }
        /// <summary>
        /// Numero o sesiones realizadas a la fecha Actual
        /// </summary>
        public string? AvanceRealSesion { get; set; }
        /// <summary>
        /// autoevaluacion en la que se encuentra el alumno en moodle
        /// </summary>
        public string? AvanceRealAutoevaluacion { get; set; }
        /// <summary>
        /// Numero de evaluaciones o sesiones avanzadas
        /// </summary>
        public int? ValorAvanceReal { get; set; }
        /// <summary>
        /// tiempo de visualizacion de la sesion actual en el nuevo aula virtual
        /// </summary>
        public int? ReproduccionVideoReal { get; set; }
        /// <summary>
        /// evaluaciones o sesiones Programadas a la fecha Actual
        /// </summary>
        public string? AvanceProgramado { get; set; }
        /// <summary>
        /// Numero de sesiones Programadas a la fecha Actual
        /// </summary>
        public string? AvanceProgramadoSesion { get; set; }
        /// <summary>
        /// autoevaluacion en la que deberia estar el alumno en moodle
        /// </summary>
        public string? AvanceProgramadoAutoevaluacion { get; set; }
        /// <summary>
        /// Numero de evaluaciones o sesiones Programadas a la fecha Actual
        /// </summary>
        public int? ValorAvanceProgramado { get; set; }
        /// <summary>
        /// minutos duracion video aula virtual
        /// </summary>
        public int? ReproduccionVideoProgramado { get; set; }
        /// <summary>
        /// Es Foreign Key de ope.T_EstadoCompromiso
        /// </summary>
        public int? IdEstadoCompromiso { get; set; }

        public virtual TAgendaTab IdAgendaTabNavigation { get; set; } = null!;
        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; } = null!;
        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
    }
}
