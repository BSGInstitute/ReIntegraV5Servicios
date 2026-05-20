using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBsgTentoLogrosRepository
    {
        List<BsgTentoTipoCondicionDTO> ObtenerTiposCondicion();
        List<BsgTentoLogroDTO> ObtenerLogros(int? tipoLogro);
        int InsertarLogro(BsgTentoLogroInsertarDTO dto, string usuarioCreacion);
        void ActualizarLogro(BsgTentoLogroActualizarDTO dto, string usuarioModificacion);
        void ActualizarOrdenLogro(int id, int orden, string usuarioModificacion);
        void EliminarLogro(int id, string usuarioModificacion);
        List<BsgTentoTipoMisionDTO> ObtenerTiposMision();
        List<BsgTentoMisionDTO> ObtenerMisiones(int? tipoMision);
        int InsertarMision(BsgTentoMisionInsertarDTO dto, string usuarioCreacion);
        void ActualizarMision(BsgTentoMisionActualizarDTO dto, string usuarioModificacion);
        void EliminarMision(int id, string usuarioModificacion);
    }
}
