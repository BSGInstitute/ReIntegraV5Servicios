using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IBsgTentoSocialService
    {
        int InsertarUsuario(UsuarioBsgTentoInsertarDTO dto, string usuarioCreacion);
        UsuarioBsgTentoDTO ObtenerUsuarioPorAspNetUser(string idAspNetUser);
        List<PublicacionAdminDTO> ObtenerPublicaciones(bool? visible);
        void ActualizarVisibilidadPublicacion(int id, bool visible, string usuarioModificacion);
        void EliminarPublicacion(int id, string usuarioModificacion);
    }
}
