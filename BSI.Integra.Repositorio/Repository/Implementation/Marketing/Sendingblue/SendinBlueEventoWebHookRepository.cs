using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue
{
    public class SendinBlueEventoWebHookRepository : GenericRepository<TSendinBlueEventoWebHook>, ISendinBlueEventoWebHookRepository
    {
        private Mapper _mapper;

        public SendinBlueEventoWebHookRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSendinBlueEventoWebHook, SendinBlueEventoWebHook>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSendinBlueEventoWebHook MapeoEntidad(SendinBlueEventoWebHook entidad)
        {
            try
            {
                TSendinBlueEventoWebHook modelo = _mapper.Map<TSendinBlueEventoWebHook>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinBlueEventoWebHook Add(SendinBlueEventoWebHook entidad)
        {
            try
            {
                var AdwordsApiPalabraClave = MapeoEntidad(entidad);
                base.Insert(AdwordsApiPalabraClave);
                return AdwordsApiPalabraClave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinBlueEventoWebHook Update(SendinBlueEventoWebHook entidad)
        {
            try
            {
                var AdwordsApiPalabraClave = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AdwordsApiPalabraClave.RowVersion = entidadExistente.RowVersion;

                base.Update(AdwordsApiPalabraClave);
                return AdwordsApiPalabraClave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TSendinBlueEventoWebHook> Add(IEnumerable<SendinBlueEventoWebHook> listadoEntidad)
        {
            try
            {
                List<TSendinBlueEventoWebHook> listado = new List<TSendinBlueEventoWebHook>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TSendinBlueEventoWebHook> Update(IEnumerable<SendinBlueEventoWebHook> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSendinBlueEventoWebHook> listado = new List<TSendinBlueEventoWebHook>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public List<SendinBlueEventoWebHookDTO> ObtenerTodaLaData()
        {
            try
            {
                List<SendinBlueEventoWebHookDTO> rpta = new List<SendinBlueEventoWebHookDTO>();
                var query = @"SELECT Id,Tipo FROM mkt.T_SendinBlueEventoWebHook WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SendinBlueEventoWebHookDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}