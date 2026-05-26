using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class BsgTentoRepository : IBsgTentoRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public BsgTentoRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<BsgTentoAreaDTO> ObtenerAreasConRuta()
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_AreaCapacitacionObtenerHabilitados", null);
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoAreaDTO>>(res);
            return new List<BsgTentoAreaDTO>();
        }

        public List<BsgTentoUnidadDTO> ObtenerUnidadesPorArea(int idAreaCapacitacion)
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_TUnidadEstudio_ObtenerIdPorAreaCapacitacion", new { IdAreaCapacitacion = idAreaCapacitacion });
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoUnidadDTO>>(res)
                    .OrderBy(x => x.Orden).ToList();
            return new List<BsgTentoUnidadDTO>();
        }

        public int InsertarUnidad(BsgTentoUnidadInsertarDTO dto, string usuarioCreacion)
        {
            var parametros = new { IdAreaCapacitacion = dto.IdAreaCapacitacion, Titulo = dto.Titulo, Descripcion = dto.Descripcion, Orden = dto.Orden, UsuarioCreacion = usuarioCreacion };
            var res = _dapperRepository.QuerySPFirstOrDefault("tnt.SP_TUnidadEstudio_Insertar", parametros);
            if (!string.IsNullOrEmpty(res) && res != "null")
                return Convert.ToInt32(JsonConvert.DeserializeObject<dynamic>(res)?.Id);
            return 0;
        }

        public void ActualizarUnidad(BsgTentoUnidadActualizarDTO dto, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TUnidadEstudio_Actualizar",
                new { IdUnidadEstudio = dto.Id, Titulo = dto.Titulo, Descripcion = dto.Descripcion, UsuarioModificacion = usuarioModificacion });
        }

        public void ActualizarOrdenUnidad(int id, int orden, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TUnidadEstudio_ActualizarOrden",
                new { IdUnidadEstudio = id, Orden = orden, UsuarioModificacion = usuarioModificacion });
        }

        public void EliminarUnidad(int id, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TUnidadEstudio_Eliminar",
                new { IdUnidadEstudio = id, UsuarioModificacion = usuarioModificacion });
        }

        public List<BsgTentoPasoDTO> ObtenerPasosPorUnidad(int idBsgTentoUnidad)
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_PasoEstudioObtenerPorIdUnidadEstudio", new { IdUnidadEstudio = idBsgTentoUnidad });
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoPasoDTO>>(res)
                    .OrderBy(x => x.Orden).ToList();
            return new List<BsgTentoPasoDTO>();
        }

        public int InsertarPaso(BsgTentoPasoInsertarDTO dto, string usuarioCreacion)
        {
            var parametros = new { IdUnidadEstudio = dto.IdUnidadEstudio, IdPGeneral = dto.IdPGeneral, Titulo = dto.Titulo, Descripcion = dto.Descripcion, Orden = dto.Orden, UsuarioCreacion = usuarioCreacion };
            var res = _dapperRepository.QuerySPFirstOrDefault("tnt.SP_TPasoEstudio_Insertar", parametros);
            if (!string.IsNullOrEmpty(res) && res != "null")
                return Convert.ToInt32(JsonConvert.DeserializeObject<dynamic>(res)?.Id);
            return 0;
        }

        public void ActualizarPaso(BsgTentoPasoActualizarDTO dto, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPasoEstudio_Actualizar",
                new { IdPasoEstudio = dto.Id, IdPGeneral = dto.IdPGeneral, Titulo = dto.Titulo, Descripcion = dto.Descripcion, UsuarioModificacion = usuarioModificacion });
        }

        public void ActualizarOrdenPaso(int id, int orden, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPasoEstudio_ActualizarOrden",
                new { IdPasoEstudio = id, Orden = orden, UsuarioModificacion = usuarioModificacion });
        }

        public void EliminarPaso(int id, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPasoEstudio_Eliminar",
                new { IdPasoEstudio = id, UsuarioModificacion = usuarioModificacion });
        }

        public List<BsgTentoComboDTO> ObtenerComboPrograma(int idAreaCapacitacion)
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_TPGeneral_ObtenerHabilitadosPorIdAreaCapacitacion", new { IdAreaCapacitacion = idAreaCapacitacion });
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoComboDTO>>(res);
            return new List<BsgTentoComboDTO>();
        }
    }
}
