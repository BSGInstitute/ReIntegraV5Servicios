using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue
{
    public class SendinBlueDataDeEventoRepository: GenericRepository<TSendinBlueDataDeEvento>, ISendinBlueDataDeEventoRepository
    {
        private Mapper _mapper;

        public SendinBlueDataDeEventoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSendinBlueDataDeEvento, SendinBlueDataDeEvento>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSendinBlueDataDeEvento MapeoEntidad(SendinBlueDataDeEvento entidad)
        {
            try
            {
                TSendinBlueDataDeEvento modelo = _mapper.Map<TSendinBlueDataDeEvento>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinBlueDataDeEvento Add(SendinBlueDataDeEvento entidad)
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

        public TSendinBlueDataDeEvento Update(SendinBlueDataDeEvento entidad)
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


        public IEnumerable<TSendinBlueDataDeEvento> Add(IEnumerable<SendinBlueDataDeEvento> listadoEntidad)
        {
            try
            {
                List<TSendinBlueDataDeEvento> listado = new List<TSendinBlueDataDeEvento>();
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

        public IEnumerable<TSendinBlueDataDeEvento> Update(IEnumerable<SendinBlueDataDeEvento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSendinBlueDataDeEvento> listado = new List<TSendinBlueDataDeEvento>();
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

        public List<SendinblueReporteMarketingDTO> ObtenerReproteSendinBlue(SendinblueReporteParametrosDTO parametrosConsulta)
        {
            List<SendinblueReporteMarketingDTO> respuesta =new  List<SendinblueReporteMarketingDTO>();
            string query = "MKT.SP_SendinblueReporteClickOpenV2";
            var resultado = _dapperRepository.QuerySPDapper(query, new {
                parametrosConsulta.FechaInicio,
                parametrosConsulta.FechaFin
            });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                respuesta = JsonConvert.DeserializeObject<List<SendinblueReporteMarketingDTO>>(resultado);
            }
            return respuesta;
        }
    }
}
