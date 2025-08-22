using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOportunidadMaximaPorCategorium
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Es Foreing Key T_TipoCategoriaOrigen
        /// </summary>
        public int IdTipoCategoriaOrigen { get; set; }
        /// <summary>
        /// Es Foreing Key de la tabla T_Pais en el campo CodigoPais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// numero de oportunidades maximas
        /// </summary>
        public int OportunidadesMaximas { get; set; }
        /// <summary>
        /// numero de oportunidades sin generar IS
        /// </summary>
        public int OportunidadesSinGenerarIs { get; set; }
        /// <summary>
        /// numero de la meta
        /// </summary>
        public int Meta { get; set; }
        /// <summary>
        /// nombre del grupo
        /// </summary>
        public string Grupo { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
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
        public Guid? IdMigracion { get; set; }
    }
}
