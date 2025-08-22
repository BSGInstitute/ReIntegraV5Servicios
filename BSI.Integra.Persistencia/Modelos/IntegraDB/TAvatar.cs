using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAvatar
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Cabello del Avatar
        /// </summary>
        public string Top { get; set; } = null!;
        /// <summary>
        /// Accesorio del Avatar
        /// </summary>
        public string Accessories { get; set; } = null!;
        /// <summary>
        /// Color de cabello del Avatar
        /// </summary>
        public string HairColor { get; set; } = null!;
        /// <summary>
        /// Barba del Avatar
        /// </summary>
        public string FacialHair { get; set; } = null!;
        /// <summary>
        /// Color de barba del Avatar
        /// </summary>
        public string FacialHairColor { get; set; } = null!;
        /// <summary>
        /// Ropa del Avatar
        /// </summary>
        public string Clothes { get; set; } = null!;
        /// <summary>
        /// Ojos del Avatar
        /// </summary>
        public string Eyes { get; set; } = null!;
        /// <summary>
        /// Cejas del Avatar
        /// </summary>
        public string Eyesbrow { get; set; } = null!;
        /// <summary>
        /// Boca del Avatar
        /// </summary>
        public string Mouth { get; set; } = null!;
        /// <summary>
        /// Ropa del Avatar
        /// </summary>
        public string Skin { get; set; } = null!;
        /// <summary>
        /// Color de ropa del Avatar
        /// </summary>
        public string ClothesColor { get; set; } = null!;
        /// <summary>
        /// Estado del registro
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
        /// Row version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// IdMigracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
