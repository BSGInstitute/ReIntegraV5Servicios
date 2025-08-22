using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPreguntaFrecuenteService
    { 
        List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> Obtener();
        List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> ObtenerPorFiltro(FiltroPreguntaFrecuenteDTO filtro);
        PreguntaFrecuenteComboModuloDTO ObtenerCombosModulo();
        List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> Insertar(PreguntaFrecuenteParametrosDTO preguntaFrecuenteParametrosDTO, string usuario);
        List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> Actualizar(PreguntaFrecuenteParametrosDTO preguntaFrecuenteParametrosDTO, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
