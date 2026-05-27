using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBsgTentoRepository
    {
        List<BsgTentoAreaDTO> ObtenerAreasConRuta();
        List<BsgTentoUnidadDTO> ObtenerUnidadesPorArea(int idAreaCapacitacion);
        int InsertarUnidad(BsgTentoUnidadInsertarDTO dto, string usuarioCreacion);
        void ActualizarUnidad(BsgTentoUnidadActualizarDTO dto, string usuarioModificacion);
        void ActualizarOrdenUnidad(int id, int orden, string usuarioModificacion);
        void EliminarUnidad(int id, string usuarioModificacion);
        List<BsgTentoPasoDTO> ObtenerPasosPorUnidad(int idBsgTentoUnidad);
        int InsertarPaso(BsgTentoPasoInsertarDTO dto, string usuarioCreacion);
        void ActualizarPaso(BsgTentoPasoActualizarDTO dto, string usuarioModificacion);
        void ActualizarOrdenPaso(int id, int orden, string usuarioModificacion);
        void EliminarPaso(int id, string usuarioModificacion);
        List<BsgTentoComboDTO> ObtenerComboPrograma(int idAreaCapacitacion);
    }
}
