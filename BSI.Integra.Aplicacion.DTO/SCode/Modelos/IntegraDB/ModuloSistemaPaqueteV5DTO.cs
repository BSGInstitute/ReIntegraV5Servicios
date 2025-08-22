using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ModuloSistemaPaqueteV5DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? IdModuloSistema { get; set; }
        public string Descripcion { get; set; }
    }
    public class ModuloSistemaPaqueteModulosV5DTO
    {
        public int Id { get; set; }
        public string NombreGrupo { get; set;}
        public string NombreModulo { get; set; }
        public string Url { get; set; }
    }
    public class AsignarModuloV5DTO
    {
        public int IdUsuario { get; set; }
        public string IdsModulo { get; set; }
    }
}
