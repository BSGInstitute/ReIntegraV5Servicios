using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TInteraccionPagina
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key tAlumnos
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Es Foreing Key TMK_InteraccionScore
        /// </summary>
        public int? IdInteraccionScore { get; set; }
        /// <summary>
        /// Llave foranea de T_CentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Llave foranea de T_PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Es Foreing Key tPGeneral
        /// </summary>
        public int? IdPgeneralGenerico { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_SubAreaCapacitacion
        /// </summary>
        public int? IdSubAreaCapacitacion { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_AreaCapacitacion
        /// </summary>
        public int? IdAreaCapacitacion { get; set; }
        /// <summary>
        /// Es Foreing Key tPLA_PGeneral
        /// </summary>
        public int? IdPgeneralGenericoSiguiente { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_SubAreaCapacitacion
        /// </summary>
        public int? IdSubAreaCapcitacionSiguiente { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_AreaCapacitacion
        /// </summary>
        public int? IdAreaCapacitacionSiguiente { get; set; }
        /// <summary>
        /// Es Foreing Key tPLA_PGeneral
        /// </summary>
        public int? IdPgeneralGenericoAnterior { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_SubAreaCapacitacion
        /// </summary>
        public int? IdSubAreaCapcitacionAnterior { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_AreaCapacitacion
        /// </summary>
        public int? IdAreaCapacitacionAnterior { get; set; }
        /// <summary>
        /// Es Foreing Key TMK_CategoriaInteraccion
        /// </summary>
        public int? IdCategoriaInteraccion { get; set; }
        /// <summary>
        /// Es Foreing Key TMK_CategoriaInteraccion
        /// </summary>
        public int? IdCategoriaInteraccionSiguiente { get; set; }
        /// <summary>
        /// Es Foreing Key TMK_CategoriaInteraccion
        /// </summary>
        public int? IdCategoriaInteraccionAnterior { get; set; }
        /// <summary>
        /// Es Foreing Key TMK_SubCategoriaInteraccion
        /// </summary>
        public int? IdSubcategoriaInteraccion { get; set; }
        /// <summary>
        /// Es Foreing Key TMK_SubCategoriaInteraccion
        /// </summary>
        public int? IdSubCategoriaInteraccionSiguiente { get; set; }
        /// <summary>
        /// Es Foreing Key TMK_TipoInteraccion
        /// </summary>
        public int? IdTipoInteraccion { get; set; }
        /// <summary>
        /// Identificador del cookie
        /// </summary>
        public Guid? IpIdCookie { get; set; }
        /// <summary>
        /// Score
        /// </summary>
        public int? IpScore { get; set; }
        /// <summary>
        /// Valor medible
        /// </summary>
        public int? IpValorMedible { get; set; }
        /// <summary>
        /// Direccion Ip
        /// </summary>
        public string? IpIp { get; set; }
        /// <summary>
        /// Fecha de inicio
        /// </summary>
        public DateTime? IpFechaInicio { get; set; }
        /// <summary>
        /// Fecha de fin
        /// </summary>
        public DateTime? IpFechaFin { get; set; }
        /// <summary>
        /// URL actual
        /// </summary>
        public string? UrlActual { get; set; }
        /// <summary>
        /// URL de la que vino
        /// </summary>
        public string? UrlAnterior { get; set; }
        /// <summary>
        /// URL a la que se dirigio
        /// </summary>
        public string? UrlSiguiente { get; set; }
        /// <summary>
        /// Correo electronico
        /// </summary>
        public string? Correo { get; set; }
        /// <summary>
        /// Direccion IP
        /// </summary>
        public string? Ip { get; set; }
        /// <summary>
        /// Es Foreign Key de TFM_ProveedorCampaniaIntegra
        /// </summary>
        public int? IdProveedorCampaniaIntegra { get; set; }
        /// <summary>
        /// Es Foreign Key de TFM_ConjuntoAnuncios
        /// </summary>
        public string? IdConjuntoAnuncio { get; set; }
        /// <summary>
        /// Es Foreign Key de TCRM_categoriaOrigen
        /// </summary>
        public int? IdCategoriaOrigen { get; set; }
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
        public Guid? IdMigracion { get; set; }
    }
}
