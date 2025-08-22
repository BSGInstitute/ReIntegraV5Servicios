using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Usuario : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public int IdUsuarioRol { get; set; }
        public string CodigoAreaTrabajo { get; set; }
    }
}
