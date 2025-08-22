using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEmpresa
    {
        public TEmpresa()
        {
            TFurs = new HashSet<TFur>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Ruc { get; set; }
        public int? IdTipoIdentificador { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? PaginaWeb { get; set; }
        public string? Email { get; set; }
        public int? Trabajadores { get; set; }
        public double? NivelFacturacion { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdIndustria { get; set; }
        public string? IdTipoEmpresa { get; set; }
        public int? IdTamanio { get; set; }
        public int? Ciiu { get; set; }
        public int? IdCodigoCiiuIndustria { get; set; }
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
        /// <summary>
        /// Municipio de la empresa
        /// </summary>
        public string? Municipio { get; set; }
        /// <summary>
        /// Estado del lugar de la empresa
        /// </summary>
        public string? EstadoLugar { get; set; }
        /// <summary>
        /// Código postal de la empresa
        /// </summary>
        public string? CodigoPostal { get; set; }
        /// <summary>
        /// Colonia de la empresa
        /// </summary>
        public string? Colonia { get; set; }
        /// <summary>
        /// Id de Municipio de Mexico
        /// </summary>
        public int? IdMunicipioMexico { get; set; }
        /// <summary>
        /// Id de Asentamiento de Mexico
        /// </summary>
        public int? IdAsentamientoMexico { get; set; }
        /// <summary>
        /// (FK) de T_CiudadMexico
        /// </summary>
        public int? IdCiudadMexico { get; set; }

        public virtual ICollection<TFur> TFurs { get; set; }
    }
}
