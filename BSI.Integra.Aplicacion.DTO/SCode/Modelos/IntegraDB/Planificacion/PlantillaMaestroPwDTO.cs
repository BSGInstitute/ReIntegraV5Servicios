using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
   public class PlantillaMaestroPwDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Repeticion { get; set; }
    }
    public class SeccionPwFiltroPlantillaPwListaSubSeccionesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdPlantilla { get; set; }
        public int IdSeccionMaestraPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public string Titulo { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public string NombreSeccionTipoContenido { get; set; }
        public List<SubSeccionTipoDetallePwDTO> ListaSubSeccionesPw { get; set; }
    }
    public class SeccionPwFiltroPlantillaPwDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdPlantilla { get; set; }
        public int IdSeccionMaestraPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public string Titulo { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public string? NombreSeccionTipoContenido { get; set; }
        public int? IdSeccionTipoDetallePw { get; set; }
        public string? NombreSubSeccion { get; set; }
        public int? IdSubSeccionTipoContenido { get; set; }
        public List<listaGridListaSeccionesDTO> listaGridListaSecciones { get; set; }
    }
}
