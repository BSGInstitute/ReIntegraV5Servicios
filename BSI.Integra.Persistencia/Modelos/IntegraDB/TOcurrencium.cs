using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOcurrencium
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripcion de la ocurrencia
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Columna Nombre en Mayuscula
        /// </summary>
        public string NombreM { get; set; } = null!;
        /// <summary>
        /// Checksum para la columna Nombre
        /// </summary>
        public int? NombreCs { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad
        /// </summary>
        public int IdFaseOportunidad { get; set; }
        /// <summary>
        /// FK [dbo].[TCRM_ActividadesCabecera]
        /// </summary>
        public int? IdActividadCabecera { get; set; }
        /// <summary>
        /// tcrm_plantilla
        /// </summary>
        public int? IdPlantillaSpeech { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_EstadoOcurrencia
        /// </summary>
        public int IdEstadoOcurrencia { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public bool Oportunidad { get; set; }
        public string RequiereLlamada { get; set; } = null!;
        public string Roles { get; set; } = null!;
        public string Color { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
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
        /// FK tabla gp.T_PersonalAreaTrabajo, Permite diferenciar a que area pertenece las ocurrencias
        /// </summary>
        public int IdPersonalAreaTrabajo { get; set; }
    }
}
