using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IBsgTentoPowerUpService
    {
        List<BsgTentoPowerUpDTO> ObtenerPowerUps();
        int InsertarPowerUp(BsgTentoPowerUpInsertarDTO dto, string usuarioCreacion);
        void ActualizarPowerUp(BsgTentoPowerUpActualizarDTO dto, string usuarioModificacion);
        void ActualizarOrdenPowerUps(List<BsgTentoOrdenDTO> ordenList, string usuarioModificacion);
        void EliminarPowerUp(int id, string usuarioModificacion);
        List<CanalDistribucionDTO> ObtenerCanalesDistribucion();
    }
}
