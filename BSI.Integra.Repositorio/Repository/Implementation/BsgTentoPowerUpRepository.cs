using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class BsgTentoPowerUpRepository : IBsgTentoPowerUpRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public BsgTentoPowerUpRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<BsgTentoPowerUpDTO> ObtenerPowerUps()
        {
            var res = _dapperRepository.QuerySPDapper("tnt.SP_TPowerUp_Obtener", null);
            if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                return JsonConvert.DeserializeObject<List<BsgTentoPowerUpDTO>>(res).OrderBy(x => x.Orden).ToList();
            return new List<BsgTentoPowerUpDTO>();
        }

        public int InsertarPowerUp(BsgTentoPowerUpInsertarDTO dto, string usuarioCreacion)
        {
            var parametros = new
            {
                dto.Codigo,
                dto.Nombre,
                dto.Descripcion,
                dto.Efecto,
                dto.CostoMonedas,
                dto.CostoPuntos,
                dto.IconoCodigo,
                dto.ColorHexadecimal,
                dto.DisponibleTienda,
                dto.RecompensaDisponible,
                dto.DisponibleRuletaDiaria,
                dto.Orden,
                UsuarioCreacion = usuarioCreacion
            };
            var res = _dapperRepository.QuerySPFirstOrDefault("tnt.SP_TPowerUp_Insertar", parametros);
            if (!string.IsNullOrEmpty(res) && res != "null")
                return Convert.ToInt32(JsonConvert.DeserializeObject<dynamic>(res)?.Id);
            return 0;
        }

        public void ActualizarPowerUp(BsgTentoPowerUpActualizarDTO dto, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPowerUp_Actualizar", new
            {
                IdPowerUp = dto.Id,
                dto.Codigo,
                dto.Nombre,
                dto.Descripcion,
                dto.Efecto,
                dto.CostoMonedas,
                dto.CostoPuntos,
                dto.IconoCodigo,
                dto.ColorHexadecimal,
                dto.DisponibleTienda,
                dto.RecompensaDisponible,
                dto.DisponibleRuletaDiaria,
                dto.Orden,
                UsuarioModificacion = usuarioModificacion
            });
        }

        public void ActualizarOrdenPowerUp(int id, int orden, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPowerUp_ActualizarOrden",
                new { IdPowerUp = id, Orden = orden, UsuarioModificacion = usuarioModificacion });
        }

        public void EliminarPowerUp(int id, string usuarioModificacion)
        {
            _dapperRepository.QuerySPDapper("tnt.SP_TPowerUp_Eliminar",
                new { IdPowerUp = id, UsuarioModificacion = usuarioModificacion });
        }
    }
}
