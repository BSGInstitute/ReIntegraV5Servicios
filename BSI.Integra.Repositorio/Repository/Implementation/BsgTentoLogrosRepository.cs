using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class BsgTentoLogrosRepository : IBsgTentoLogrosRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public BsgTentoLogrosRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<BsgTentoTipoCondicionDTO> ObtenerTiposCondicion()
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_TLogroTipoCondicion_Obtener", null);
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoTipoCondicionDTO>>(res);
            return new List<BsgTentoTipoCondicionDTO>();
        }

        public List<BsgTentoLogroDTO> ObtenerLogros(int? tipoLogro)
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_BsgTentoObtenerLogros", new { TipoLogro = tipoLogro });
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoLogroDTO>>(res);
            return new List<BsgTentoLogroDTO>();
        }

        public int InsertarLogro(BsgTentoLogroInsertarDTO dto, string usuarioCreacion)
        {
            var parametros = new
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                TipoLogro = dto.TipoLogro,
                IdLogroTipoCondicion = dto.IdLogroTipoCondicion,
                ValorObjetivo = dto.ValorObjetivo,
                IconoCodigo = dto.IconoCodigo,
                ColorHexadecimal = dto.ColorHexadecimal,
                IdAreaCapacitacion = dto.IdAreaCapacitacion,
                IdPGeneral = dto.IdPGeneral,
                Orden = dto.Orden,
                UsuarioCreacion = usuarioCreacion
            };
            var res = _dapperRepository.QuerySPFirstOrDefault("tnt.SP_TLogro_Insertar", parametros);
            if (!string.IsNullOrEmpty(res) && res != "null")
                return Convert.ToInt32(JsonConvert.DeserializeObject<dynamic>(res)?.Id);
            return 0;
        }

        public void ActualizarLogro(BsgTentoLogroActualizarDTO dto, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TLogro_Actualizar", new
            {
                IdLogro = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                TipoLogro = dto.TipoLogro,
                IdLogroTipoCondicion = dto.IdLogroTipoCondicion,
                ValorObjetivo = dto.ValorObjetivo,
                IconoCodigo = dto.IconoCodigo,
                ColorHexadecimal = dto.ColorHexadecimal,
                IdAreaCapacitacion = dto.IdAreaCapacitacion,
                IdPGeneral = dto.IdPGeneral,
                UsuarioModificacion = usuarioModificacion
            });
        }

        public void ActualizarOrdenLogro(int id, int orden, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TLogro_ActualizarOrden",
                new { IdLogro = id, Orden = orden, UsuarioModificacion = usuarioModificacion });
        }

        public void EliminarLogro(int id, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TLogro_Eliminar",
                new { IdLogro = id, UsuarioModificacion = usuarioModificacion });
        }

        public List<BsgTentoTipoMisionDTO> ObtenerTiposMision()
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_TMisionTipo_Obtener", null);
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoTipoMisionDTO>>(res);
            return new List<BsgTentoTipoMisionDTO>();
        }

        public List<BsgTentoMisionDTO> ObtenerMisiones(int? tipoMision)
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_BsgTentoObtenerMisiones", new { IdTipoMision = tipoMision });
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoMisionDTO>>(res);
            return new List<BsgTentoMisionDTO>();
        }

        public int InsertarMision(BsgTentoMisionInsertarDTO dto, string usuarioCreacion)
        {
            var parametros = new
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                IdMisionTipo = dto.IdMisionTipo,
                IdLogroTipoCondicion = dto.IdLogroTipoCondicion,
                ValorObjetivo = dto.ValorObjetivo,
                RecompensaXP = dto.RecompensaXP,
                RecompensaPuntos = dto.RecompensaPuntos,
                IconoCodigo = dto.IconoCodigo,
                ColorHexadecimal = dto.ColorHexadecimal,
                IdAreaCapacitacion = dto.IdAreaCapacitacion,
                Orden = dto.Orden,
                UsuarioCreacion = usuarioCreacion
            };
            var res = _dapperRepository.QuerySPFirstOrDefault("tnt.SP_TMision_Insertar", parametros);
            if (!string.IsNullOrEmpty(res) && res != "null")
                return Convert.ToInt32(JsonConvert.DeserializeObject<dynamic>(res)?.Id);
            return 0;
        }

        public void ActualizarMision(BsgTentoMisionActualizarDTO dto, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TMision_Actualizar", new
            {
                IdMision = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                IdMisionTipo = dto.IdMisionTipo,
                IdLogroTipoCondicion = dto.IdLogroTipoCondicion,
                ValorObjetivo = dto.ValorObjetivo,
                RecompensaXP = dto.RecompensaXP,
                RecompensaPuntos = dto.RecompensaPuntos,
                IconoCodigo = dto.IconoCodigo,
                ColorHexadecimal = dto.ColorHexadecimal,
                IdAreaCapacitacion = dto.IdAreaCapacitacion,
                Orden = dto.Orden,
                UsuarioModificacion = usuarioModificacion
            });
        }

        public void EliminarMision(int id, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TMision_Eliminar",
                new { IdMision = id, UsuarioModificacion = usuarioModificacion });
        }
    }
}
