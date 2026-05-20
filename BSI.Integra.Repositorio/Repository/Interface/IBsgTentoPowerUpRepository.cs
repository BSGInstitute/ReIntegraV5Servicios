using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBsgTentoPowerUpRepository
    {
        List<BsgTentoPowerUpDTO> ObtenerPowerUps();
        int InsertarPowerUp(BsgTentoPowerUpInsertarDTO dto, string usuarioCreacion);
        void ActualizarPowerUp(BsgTentoPowerUpActualizarDTO dto, string usuarioModificacion);
        void ActualizarOrdenPowerUp(int id, int orden, string usuarioModificacion);
        void EliminarPowerUp(int id, string usuarioModificacion);
    }
}
