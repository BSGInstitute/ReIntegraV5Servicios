using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la configuracion de acciones del botón de progressive profiling
    /// </summary>
    public partial class TFormularioProgresivoConfiguracionBoton
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es fk T_FormularioProgresivo
        /// </summary>
        public int IdFormularioProgresivo { get; set; }
        /// <summary>
        /// Es fk T_FormularioProgresivoSeccionPortal
        /// </summary>
        public int IdFormularioProgresivoSeccionPortal { get; set; }
        /// <summary>
        /// Es fk T_FormularioProgresivoAccionBoton
        /// </summary>
        public int IdFormularioProgresivoAccionBoton { get; set; }
        /// <summary>
        /// Es fk T_RegistroArchivoStorage
        /// </summary>
        public int? IdRegistroArchivoStorage { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TFormularioProgresivoAccionBoton IdFormularioProgresivoAccionBotonNavigation { get; set; } = null!;
        public virtual TFormularioProgresivo IdFormularioProgresivoNavigation { get; set; } = null!;
        public virtual TFormularioProgresivoSeccionPortal IdFormularioProgresivoSeccionPortalNavigation { get; set; } = null!;
    }
}
