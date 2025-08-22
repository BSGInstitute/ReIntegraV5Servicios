using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: GastoFinancieroCronogramaDetalleRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_GastoFinancieroCronogramaDetalle
    /// </summary>
    public class GastoFinancieroCronogramaDetalleRepository : GenericRepository<TGastoFinancieroCronogramaDetalle>, IGastoFinancieroCronogramaDetalleRepository
    {
        private Mapper _mapper;

        public GastoFinancieroCronogramaDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGastoFinancieroCronogramaDetalle, GastoFinancieroCronogramaDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGastoFinancieroCronogramaDetalle MapeoEntidad(GastoFinancieroCronogramaDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TGastoFinancieroCronogramaDetalle modelo = _mapper.Map<TGastoFinancieroCronogramaDetalle>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGastoFinancieroCronogramaDetalle Add(GastoFinancieroCronogramaDetalle entidad)
        {
            try
            {
                var GastoFinancieroCronogramaDetalle = MapeoEntidad(entidad);
                base.Insert(GastoFinancieroCronogramaDetalle);
                return GastoFinancieroCronogramaDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGastoFinancieroCronogramaDetalle Update(GastoFinancieroCronogramaDetalle entidad)
        {
            try
            {
                var GastoFinancieroCronogramaDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GastoFinancieroCronogramaDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(GastoFinancieroCronogramaDetalle);
                return GastoFinancieroCronogramaDetalle;
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


        public IEnumerable<TGastoFinancieroCronogramaDetalle> Add(IEnumerable<GastoFinancieroCronogramaDetalle> listadoEntidad)
        {
            try
            {
                List<TGastoFinancieroCronogramaDetalle> listado = new List<TGastoFinancieroCronogramaDetalle>();
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

        public IEnumerable<TGastoFinancieroCronogramaDetalle> Update(IEnumerable<GastoFinancieroCronogramaDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGastoFinancieroCronogramaDetalle> listado = new List<TGastoFinancieroCronogramaDetalle>();
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


        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los CronogramaDetalle (Cutas) dado un id de GastoFinanciero (Cronograma)
        /// para ser mostradas en una grilla
        /// </summary>
        /// <returns>  IEnumerable<GastoFinancieroCronogramaDetalleDTO></returns>
        public IEnumerable<GastoFinancieroCronogramaDetalleDTO> ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(int IdCronograma)
        {
            try
            {
                List<GastoFinancieroCronogramaDetalleDTO> resultado = new List<GastoFinancieroCronogramaDetalleDTO>();
                var _query = string.Empty;
                _query = @"SELECT 
                            Id, 
                            IdGastoFinancieroCronograma, 
                            NumeroCuota, 
                            CapitalCuota, 
                            InteresCuota,
                            FechaVencimientoCuota 
                        FROM fin.T_GastoFinancieroCronogramaDetalle 
                        WHERE Estado = 1 and IdGastoFinancieroCronograma=" + IdCronograma + " order by NumeroCuota asc";
                var respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<GastoFinancieroCronogramaDetalleDTO>>(respuesta);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
