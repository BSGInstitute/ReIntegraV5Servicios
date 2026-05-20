using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class BsgTentoSuscripcionRepository : IBsgTentoSuscripcionRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public BsgTentoSuscripcionRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<PlanSuscripcionDTO> ObtenerPlanes()
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_TPlanSuscripcion_Obtener", null);
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<PlanSuscripcionDTO>>(res).OrderBy(x => x.Orden).ToList();
            return new List<PlanSuscripcionDTO>();
        }

        public int InsertarPlan(PlanSuscripcionInsertarDTO dto, string usuarioCreacion)
        {
            var parametros = new
            {
                dto.Nombre,
                dto.Descripcion,
                dto.EsPremium,
                dto.PowerUpsIlimitados,
                dto.IncluyeAnuncio,
                dto.ContenidoExclusivo,
                dto.Orden,
                UsuarioCreacion = usuarioCreacion
            };
            var res = _dapperRepository.QuerySPFirstOrDefault("tnt.SP_TPlanSuscripcion_Insertar", parametros);
            if (!string.IsNullOrEmpty(res) && res != "null")
                return Convert.ToInt32(JsonConvert.DeserializeObject<dynamic>(res)?.Id);
            return 0;
        }

        public void ActualizarPlan(PlanSuscripcionActualizarDTO dto, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPlanSuscripcion_Actualizar", new
            {
                IdPlanSuscripcion = dto.Id,
                dto.Nombre,
                dto.Descripcion,
                dto.EsPremium,
                dto.PowerUpsIlimitados,
                dto.IncluyeAnuncio,
                dto.ContenidoExclusivo,
                dto.Orden,
                UsuarioModificacion = usuarioModificacion
            });
        }

        public void EliminarPlan(int id, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPlanSuscripcion_Eliminar",
                new { IdPlanSuscripcion = id, UsuarioModificacion = usuarioModificacion });
        }
    }
}
