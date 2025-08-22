using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConvocatoriaPersonal
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del registro de Convocatoria
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo de Convocatoria
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// FK T_ProcesoSeleccion
        /// </summary>
        public int IdProcesoSeleccion { get; set; }
        /// <summary>
        /// FK de fin.T_Proveedor
        /// </summary>
        public int IdProveedor { get; set; }
        /// <summary>
        /// Fecha Inicio convocatoria
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// FechaFin convocatoria
        /// </summary>
        public DateTime FechaFin { get; set; }
        /// <summary>
        ///  Cuerpo de Convocatoria
        /// </summary>
        public string CuerpoConvocatoria { get; set; } = null!;
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
        /// FK de T_SedeTrabajo
        /// </summary>
        public int? IdSedeTrabajo { get; set; }
        /// <summary>
        /// FK de T_Area
        /// </summary>
        public int? IdArea { get; set; }
        /// <summary>
        /// Url de Aviso Laboral
        /// </summary>
        public string? UrlAviso { get; set; }
        /// <summary>
        /// FK de T_Personal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Cantidad de vacantes
        /// </summary>
        public int? NroVacantes { get; set; }
        /// <summary>
        /// Id Modalidad de trabajo
        /// </summary>
        public int? IdModalidadTrabajo { get; set; }
        /// <summary>
        /// Id Categoria asignacion
        /// </summary>
        public int? IdCategoriaAsignacion { get; set; }
        /// <summary>
        /// Se muestra en portal 1 si , 0 no
        /// </summary>
        public bool? VerEnPortal { get; set; }
        /// <summary>
        /// Se muestra solo para matriculados 1 si , 0 no
        /// </summary>
        public bool? SoloMatriculado { get; set; }
        /// <summary>
        /// Informacion adicional de la convocatoria
        /// </summary>
        public string? InformacionAdicional { get; set; }
        /// <summary>
        /// Id T_TipoContrato
        /// </summary>
        public int? IdTipoContrato { get; set; }
        /// <summary>
        /// tipo jornada completa/parcial/por Horas
        /// </summary>
        public string? TipoJornada { get; set; }
        /// <summary>
        /// Cantidad de horas Semanales
        /// </summary>
        public int? HoraSemanal { get; set; }
        /// <summary>
        /// id T_Moneada para Remuneració bruta
        /// </summary>
        public int? RemIdMoneda { get; set; }
        /// <summary>
        /// monot para Remuneració bruta
        /// </summary>
        public decimal? MontoRemBruta { get; set; }
        /// <summary>
        /// visualizar remuneración
        /// </summary>
        public bool? VisualizarRem { get; set; }
        /// <summary>
        /// aplica a bono
        /// </summary>
        public bool? AplicaBono { get; set; }
        /// <summary>
        /// id T_Moneda Bono
        /// </summary>
        public int? BonoIdMoneda { get; set; }
        /// <summary>
        /// Monto desde Bono
        /// </summary>
        public decimal? MontoDesdeBono { get; set; }
        /// <summary>
        /// Monto hasta Bono
        /// </summary>
        public decimal? MontoHastaBono { get; set; }
        /// <summary>
        /// Aplica comision
        /// </summary>
        public bool? AplicaComision { get; set; }
        public int? ComisionIdMoneda { get; set; }
        /// <summary>
        /// monto desde comision
        /// </summary>
        public decimal? MontoDesdeComision { get; set; }
        /// <summary>
        /// monto hasta comision
        /// </summary>
        public decimal? MontoHastaComision { get; set; }
        /// <summary>
        ///  id T_EstadoConvocatoria 
        /// </summary>
        public int? IdEstadoConvocatoria { get; set; }

        public virtual TPersonalAreaTrabajo? IdAreaNavigation { get; set; }
        public virtual TPersonal? IdPersonalNavigation { get; set; }
        public virtual TProcesoSeleccion IdProcesoSeleccionNavigation { get; set; } = null!;
        public virtual TProveedor IdProveedorNavigation { get; set; } = null!;
        public virtual TSedeTrabajo? IdSedeTrabajoNavigation { get; set; }
    }
}
