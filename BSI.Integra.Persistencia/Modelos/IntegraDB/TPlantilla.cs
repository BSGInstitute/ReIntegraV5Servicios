using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPlantilla
    {
        public TPlantilla()
        {
            TConfiguracionDeEnvioParaWhatsApps = new HashSet<TConfiguracionDeEnvioParaWhatsApp>();
            TFaseByPlantillas = new HashSet<TFaseByPlantilla>();
            TPlantillaAsociacionModuloSistemas = new HashSet<TPlantillaAsociacionModuloSistema>();
            TPlantillaClaveValors = new HashSet<TPlantillaClaveValor>();
            TSmsConfiguracionEnvios = new HashSet<TSmsConfiguracionEnvio>();
            TWhatsAppConfiguracionEnvios = new HashSet<TWhatsAppConfiguracionEnvio>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la plantilla
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de la plantilla
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Es Foreing Key T_PlantillaBase
        /// </summary>
        public int IdPlantillaBase { get; set; }
        /// <summary>
        /// estado de la agenda
        /// </summary>
        public bool EstadoAgenda { get; set; }
        /// <summary>
        /// documento
        /// </summary>
        public int Documento { get; set; }
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
        /// <summary>
        /// Llave foranea a la tabla gp.T_PersonalAreaTrabajo
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// Estado de la plantilla en integra
        /// </summary>
        public bool? EstadoPlantillaIntegra { get; set; }

        public virtual ICollection<TConfiguracionDeEnvioParaWhatsApp> TConfiguracionDeEnvioParaWhatsApps { get; set; }
        public virtual ICollection<TFaseByPlantilla> TFaseByPlantillas { get; set; }
        public virtual ICollection<TPlantillaAsociacionModuloSistema> TPlantillaAsociacionModuloSistemas { get; set; }
        public virtual ICollection<TPlantillaClaveValor> TPlantillaClaveValors { get; set; }
        public virtual ICollection<TSmsConfiguracionEnvio> TSmsConfiguracionEnvios { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvio> TWhatsAppConfiguracionEnvios { get; set; }
    }
}
