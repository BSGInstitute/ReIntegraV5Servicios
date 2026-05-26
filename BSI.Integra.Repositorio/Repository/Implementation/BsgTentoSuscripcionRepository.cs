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
                dto.Orden,
                UsuarioModificacion = usuarioModificacion
            });
        }

        public void EliminarPlan(int id, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPlanSuscripcion_Eliminar",
                new { IdPlanSuscripcion = id, UsuarioModificacion = usuarioModificacion });
        }

        public List<BsgTentoBeneficioDTO> ObtenerBeneficios()
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_TBeneficio_Obtener", null);
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoBeneficioDTO>>(res).OrderBy(x => x.Orden).ToList();
            return new List<BsgTentoBeneficioDTO>();
        }

        public List<PlataformaTiendaDTO> ObtenerPlataformasTienda()
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_TPlataformaTienda_Obtener", null);
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<PlataformaTiendaDTO>>(res).OrderBy(x => x.Orden).ToList();
            return new List<PlataformaTiendaDTO>();
        }

        public List<PlanSuscripcionBeneficioDTO> ObtenerTodosPlanSuscripcionBeneficio()
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_PlanSuscripcionBeneficioObtenerTodos", null);
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<PlanSuscripcionBeneficioDTO>>(res);
            return new List<PlanSuscripcionBeneficioDTO>();
        }

        public void ActualizarPlanSuscripcionBeneficio(int idPlanSuscripcion, int idBeneficio, bool activo, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_PlanSuscripcionBeneficioActualizar",
                new { IdPlanSuscripcion = idPlanSuscripcion, IdBeneficio = idBeneficio, Activo = activo, UsuarioModificacion = usuarioModificacion });
        }
    }
}
