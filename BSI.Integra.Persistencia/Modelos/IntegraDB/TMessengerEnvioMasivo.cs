using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMessengerEnvioMasivo
    {
        /// <summary>
        /// Es clave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Presupuesto Diario para Facebook
        /// </summary>
        public int PresupuestoDiario { get; set; }
        /// <summary>
        /// Es clave foránea de T_Personal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Es clave foránea de T_PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Es clave foranea de T_ActividadCabecera
        /// </summary>
        public int IdActividadCabecera { get; set; }
        /// <summary>
        /// Es clave foranea de T_Plantilla
        /// </summary>
        public int IdPlantilla { get; set; }
        /// <summary>
        /// Es clave foranea de T_ConjuntoListaDetalle
        /// </summary>
        public int IdConjuntoListaDetalle { get; set; }
        /// <summary>
        /// Es clave foranea de T_FacebookPagina
        /// </summary>
        public int IdFacebookPagina { get; set; }
        /// <summary>
        /// Es clave foranea de T_FacebookCuentaPublicitaria
        /// </summary>
        public int IdFacebookCuentaPublicitaria { get; set; }
        /// <summary>
        /// Es clave foranea de T_FacebookAudiencia
        /// </summary>
        public int? IdFacebookAudiencia { get; set; }
        /// <summary>
        /// Es clave foranea de T_ConjuntoAnuncioFacebook
        /// </summary>
        public int? IdConjuntoAnuncioFacebook { get; set; }
        /// <summary>
        /// Es clave foranea de T_FacebookAnuncioCreativo
        /// </summary>
        public int? IdFacebookAnuncioCreativo { get; set; }
        /// <summary>
        /// Es clave foranea de T_FacebookAnuncio
        /// </summary>
        public int? IdFacebookAnuncio { get; set; }
        /// <summary>
        /// Indica la cantidad de contactos calculados para enviar mensaje
        /// </summary>
        public int CantidadContactos { get; set; }
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
    }
}
