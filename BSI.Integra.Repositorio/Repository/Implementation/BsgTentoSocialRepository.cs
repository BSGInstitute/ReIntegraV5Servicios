using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class BsgTentoSocialRepository : IBsgTentoSocialRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public BsgTentoSocialRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public int InsertarUsuario(UsuarioBsgTentoInsertarDTO dto, string usuarioCreacion)
        {
            var res = _dapperRepository.QuerySPFirstOrDefault("tnt.SP_TUsuario_Insertar",
                new { dto.IdAspNetUser, Nombre = dto.Nombre, UsuarioCreacion = usuarioCreacion });
            if (!string.IsNullOrEmpty(res) && res != "null")
                return Convert.ToInt32(JsonConvert.DeserializeObject<dynamic>(res)?.Id);
            return 0;
        }

        public UsuarioBsgTentoDTO ObtenerUsuarioPorAspNetUser(string idAspNetUser)
        {
            var res = _dapperRepository.QuerySPFirstOrDefault("tnt.SP_TUsuario_ObtenerPorAspNetUser",
                new { IdAspNetUser = idAspNetUser });
            if (!string.IsNullOrEmpty(res) && res != "null")
                return JsonConvert.DeserializeObject<UsuarioBsgTentoDTO>(res);
            return null;
        }

        public List<PublicacionAdminDTO> ObtenerPublicaciones(bool? visible)
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_BsgTentoObtenerPublicacionesAdmin",
                new { Visible = visible });
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<PublicacionAdminDTO>>(res)
                    .OrderByDescending(x => x.FechaCreacion).ToList();
            return new List<PublicacionAdminDTO>();
        }

        public void ActualizarVisibilidadPublicacion(int id, bool visible, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPublicacion_ActualizarVisibilidad",
                new { IdPublicacion = id, Visible = visible, UsuarioModificacion = usuarioModificacion });
        }

        public void EliminarPublicacion(int id, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPublicacion_Eliminar",
                new { IdPublicacion = id, UsuarioModificacion = usuarioModificacion });
        }
    }
}
