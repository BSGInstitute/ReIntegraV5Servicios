using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAsignacionRegular
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la tabla GrupoFiltroProgramaCritico
        /// </summary>
        public int IdGrupoFiltroProgramaCritico { get; set; }
        /// <summary>
        /// Id de la tabla GrupoFiltroProgramaCriticoPGeneral
        /// </summary>
        public int IdGrupoFiltroProgramaCriticoPgeneral { get; set; }
        /// <summary>
        /// Id de la tabla GrupoFiltroProgramaCriticoPorAsesor
        /// </summary>
        public int IdGrupoFiltroProgramaCriticoPorAsesor { get; set; }
        /// <summary>
        /// Id de la tabla PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Id de la tabla Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Id de la tabla PersonalJefe
        /// </summary>
        public int IdPersonalJefe { get; set; }
        /// <summary>
        /// prioridad por asesor
        /// </summary>
        public int Prioridad { get; set; }
        /// <summary>
        /// Flag si el asesor recibe datos de calidad
        /// </summary>
        public bool DatoCalidad { get; set; }
        /// <summary>
        /// Aplica proporcion por pais
        /// </summary>
        public bool AplicaProporcionPorPais { get; set; }
        /// <summary>
        /// Flag que indica si el asesor tiene limite de cola
        /// </summary>
        public bool EsLimiteCola { get; set; }
        /// <summary>
        /// Indica el limite de cola del asesor
        /// </summary>
        public int LimiteCola { get; set; }
        /// <summary>
        /// Indica el porcentaje de tolerancia que va tener el asesor
        /// </summary>
        public int PorcentajeTolerancia { get; set; }
        /// <summary>
        /// estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// row version
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id migración
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
