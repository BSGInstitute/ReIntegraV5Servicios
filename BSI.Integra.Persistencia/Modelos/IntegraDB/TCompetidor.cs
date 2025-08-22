using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCompetidor
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del competidor
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Duracion
        /// </summary>
        public int DuracionCronologica { get; set; }
        /// <summary>
        /// Costo del competidor
        /// </summary>
        public int CostoNeto { get; set; }
        /// <summary>
        /// Precio del competidor
        /// </summary>
        public int Precio { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Empresas
        /// </summary>
        public int IdInstitucionCompetidora { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais por codigo de pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Ciudad
        /// </summary>
        public int IdCiudad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_RegionCiudades
        /// </summary>
        public int? IdRegionCiudad { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_AreaCapacitacion
        /// </summary>
        public int IdAeaCapacitacion { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_SubAreaCapacitacion
        /// </summary>
        public int IdSubAreaCapacitacion { get; set; }
        /// <summary>
        /// Es Foreing Key tCategoriaP
        /// </summary>
        public int IdCategoria { get; set; }
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
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }
}
