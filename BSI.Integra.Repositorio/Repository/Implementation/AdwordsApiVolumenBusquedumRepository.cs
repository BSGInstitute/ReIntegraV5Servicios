using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AdwordsApiVolumenBusquedumRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AdwordsApiVolumenBusquedum
    /// </summary>
    public class AdwordsApiVolumenBusquedumRepository : GenericRepository<TAdwordsApiVolumenBusquedum>, IAdwordsApiVolumenBusquedumRepository
    {
        private Mapper _mapper;

        public AdwordsApiVolumenBusquedumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAdwordsApiVolumenBusquedum, AdwordsApiVolumenBusquedum>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAdwordsApiVolumenBusquedum MapeoEntidad(AdwordsApiVolumenBusquedum entidad)
        {
            try
            {
                //crea la entidad padre
                TAdwordsApiVolumenBusquedum modelo = _mapper.Map<TAdwordsApiVolumenBusquedum>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAdwordsApiVolumenBusquedum Add(AdwordsApiVolumenBusquedum entidad)
        {
            try
            {
                var AdwordsApiVolumenBusquedum = MapeoEntidad(entidad);
                base.Insert(AdwordsApiVolumenBusquedum);
                return AdwordsApiVolumenBusquedum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAdwordsApiVolumenBusquedum Update(AdwordsApiVolumenBusquedum entidad)
        {
            try
            {
                var AdwordsApiVolumenBusquedum = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AdwordsApiVolumenBusquedum.RowVersion = entidadExistente.RowVersion;

                base.Update(AdwordsApiVolumenBusquedum);
                return AdwordsApiVolumenBusquedum;
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


        public IEnumerable<TAdwordsApiVolumenBusquedum> Add(IEnumerable<AdwordsApiVolumenBusquedum> listadoEntidad)
        {
            try
            {
                List<TAdwordsApiVolumenBusquedum> listado = new List<TAdwordsApiVolumenBusquedum>();
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

        public IEnumerable<TAdwordsApiVolumenBusquedum> Update(IEnumerable<AdwordsApiVolumenBusquedum> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAdwordsApiVolumenBusquedum> listado = new List<TAdwordsApiVolumenBusquedum>();
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

        /// Autor: Rodrigo Montesinos
        /// Fecha: 21-03-2023
        /// <summary>
        /// Obtiene datos historicos de adwords palabras clave
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="palabras"></param>
        /// <param name="idPais"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<AdwordsApiVolumenBusquedaHistoricoDTO> ObtenerHistorico(DateTime fechaInicio, DateTime fechaFin, string palabras, int idPais)
        {
            try
            {
                List<AdwordsApiVolumenBusquedaHistoricoDTO> items = new List<AdwordsApiVolumenBusquedaHistoricoDTO>();
                var query = "SELECT DISTINCT PalabraClave, PromedioBusqueda, Mes, Anho, IdPais FROM mkt.V_TAdwordsApiPalabraClave_ObtenerVolumenBusqueda WHERE PalabraClave in ( select item from conf.F_Splitstring(@palabras,',')) " +
                    "AND Anho <= @anhomaximo AND (Mes <= @mesmaximo OR Anho < @anhomaximo)  AND  Anho >= @anhominimo AND (Mes >= @mesminimo OR Anho > @anhominimo) AND IdPais = @idPais order by mes,anho desc";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { palabras, anhomaximo = fechaFin.Year, mesmaximo = fechaFin.Month, anhominimo = fechaInicio.Year, mesminimo = fechaInicio.Month, idPais });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AdwordsApiVolumenBusquedaHistoricoDTO>>(queryRespuesta);
                }
                return items.OrderBy(x=>x.Anho).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 21-03-2023
        /// <summary>
        /// elimina las palabras clave pasadas al momento de hacer una busqueda
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="palabras"></param>
        /// <param name="idPais"></param>
        /// <exception cref="Exception"></exception>
        public void eliminarhistorico(DateTime fechaInicio, DateTime fechaFin, string palabras, int idPais)
        {
            try
            {
                List<AdwordsApiVolumenBusquedaHistoricoDTO> items = new List<AdwordsApiVolumenBusquedaHistoricoDTO>();
                var query = "UPDATE AAVB set AAVB.estado=0 FROM mkt.T_AdwordsApiVolumenBusqueda AAVB INNER JOIN MKT.T_AdwordsApiPalabraClave as AAPC ON AAPC.Id=AAVB.IdAdwordsApiPalabraClave " +
                    "WHERE AAPC.PalabraClave in ( select item from conf.F_Splitstring(@palabras,',')) " +
                    "AND AAVB.Anho <= @anhomaximo AND (AAVB.Mes <= @mesmaximo OR AAVB.Anho < @anhomaximo)  AND AAVB.Anho >= @anhominimo AND (AAVB.Mes >= @mesminimo OR AAVB.Anho > @anhominimo) AND AAVB.IdPais = @idPais ";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { palabras, anhomaximo = fechaFin.Year, mesmaximo = fechaFin.Month, anhominimo = fechaInicio.Year, mesminimo = fechaInicio.Month, idPais });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
