using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ChatAsignadoNoAsignadoDTO
    {
        public string NombreArea { get; set; }
        public string NombreSubArea { get; set; }
        public string NombrePGeneral { get; set; }
        public string NombrePais { get; set; }
        public int? IdAsesorChat { get; set; }
        public bool? EsAsignado { get; set; }
        public string NombrePersonal { get; set; }
    }
    public class ChatAsignadoNoAsignadoCompuestoDTO
    {
        public int? Total { get; set; }
        public List<ChatAsignadoNoAsignadoDTO> Registros { get; set; }
    }
    public class ChatListaAsesoresCompuestoDTO
    {
        public int? Total { get; set; }
        public IEnumerable<AsesorChatConsolidadoVisualizarDTO> Registros { get; set; }

    }
    public class ChatListaAsesoresDTO
    {
        public int? Id { get; set; }
        public int? IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public int? IdArea { get; set; }
        public string NombreArea { get; set; }
        public int? IdSubArea { get; set; }
        public string? NombreSubArea { get; set; }
        public int? IdPais { get; set; }
        public string? NombrePais { get; set; }
        public int? IdPGeneral { get; set; }
        public string? NombrePGeneral { get; set; }
    }
    public class AsesorChatConsolidadoVisualizarDTO
    {
        public int? Id { get; set; }
        public int? IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public string ListaPais { get; set; }
        public string ListaArea { get; set; }
        public string ListaSubArea { get; set; }
        public string ListaProgramaGeneral { get; set; }
    }
    public class AsesorChatConsolidadoDTO
    {
        public int? Id { get; set; }
        public int? IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public List<AsesorChatDetalleDTO> ListaPais { get; set; }
        public List<AsesorChatDetalleDTO> ListaArea { get; set; }
        public List<AsesorChatDetalleDTO> ListaSubArea { get; set; }
        public List<AsesorChatDetalleDTO> ListaProgramaGeneral { get; set; }
    }
    public class AsesorChatDetalleDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }
    public class AsesorChatDetalleDetalleDTO
    {
        public List<IdDTO> listadoIdsPais { get; set; }
        public List<IdDTO> listadoIdsProgramaGeneral { get; set; }
        public List<IdDTO> listadoIdsAreaCapacitacion { get; set; }
        public List<IdDTO> listadoIdsSubAreaCapacitacion { get; set; }
    }
    public class CompuestoInsertarAsesorChatDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public List<int> Area { get; set; }
        public List<int> SubArea { get; set; }
        public List<int> Paises { get; set; }
        public List<int> Programas { get; set; }
    }
}
