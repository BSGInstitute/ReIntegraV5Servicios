using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TComprobantePagoOportunidad
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// es foreign key de talumno
        /// </summary>
        public int? IdContacto { get; set; }
        /// <summary>
        /// nombre
        /// </summary>
        public string Nombres { get; set; } = null!;
        /// <summary>
        /// apellidos
        /// </summary>
        public string Apellidos { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string NombrePais { get; set; } = null!;
        /// <summary>
        /// es foreign key de tcrm_pais en el campo codigo
        /// </summary>
        public int IdPais { get; set; }
        public string NombreCiudad { get; set; } = null!;
        public string TipoComprobante { get; set; } = null!;
        public string NroDocumento { get; set; } = null!;
        public string NombreRazonSocial { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public int BitComprobante { get; set; }
        /// <summary>
        /// es foreign key de la tabla TCRM_Ocurrencia
        /// </summary>
        public int? IdOcurrencia { get; set; }
        /// <summary>
        /// es foreign key de tpersonal
        /// </summary>
        public int IdAsesor { get; set; }
        /// <summary>
        /// es foreign key de la tabla TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        public string? Comentario { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
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
        /// Llave foranea con la tabla T_ClasificacionPersona
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// Detalle de observaciones
        /// </summary>
        public string? Observacion { get; set; }

        public virtual TOportunidad? IdOportunidadNavigation { get; set; }
    }
}
