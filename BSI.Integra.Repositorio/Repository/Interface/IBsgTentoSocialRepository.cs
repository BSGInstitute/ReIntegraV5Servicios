using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBsgTentoSocialRepository
    {
        int InsertarUsuario(UsuarioBsgTentoInsertarDTO dto, string usuarioCreacion);
        UsuarioBsgTentoDTO ObtenerUsuarioPorAspNetUser(string idAspNetUser);
        List<PublicacionAdminDTO> ObtenerPublicaciones(bool? visible, DateTime fechaInicio, DateTime fechaFin);
        void ActualizarVisibilidadPublicacion(int id, bool visible, string usuarioModificacion);
        void EliminarPublicacion(int id, string usuarioModificacion);
        List<TipoReaccionDTO> ObtenerTiposReaccion();
    }
}
