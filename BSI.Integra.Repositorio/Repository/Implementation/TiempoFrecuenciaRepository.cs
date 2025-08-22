using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TiempoFrecuenciaRepository
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_TiempoFrecuencia
    /// </summary>
    public class TiempoFrecuenciaRepository : GenericRepository<TTiempoFrecuencium>, ITiempoFrecuenciaRepository
    {
        private Mapper _mapper;

        public TiempoFrecuenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTiempoFrecuencium, TiempoFrecuencia>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TTiempoFrecuencium MapeoEntidad(TiempoFrecuencia entidad)
        {
            try
            {
                //crea la entidad padre
                TTiempoFrecuencium modelo = _mapper.Map<TTiempoFrecuencium>(entidad);

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

        public TTiempoFrecuencium Add(TiempoFrecuencia entidad)
        {
            try
            {
                var TiempoFrecuencia = MapeoEntidad(entidad);
                base.Insert(TiempoFrecuencia);
                return TiempoFrecuencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTiempoFrecuencium Update(TiempoFrecuencia entidad)
        {
            try
            {
                var TiempoFrecuencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TiempoFrecuencia.RowVersion = entidadExistente.RowVersion;

                base.Update(TiempoFrecuencia);
                return TiempoFrecuencia;
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


        public IEnumerable<TTiempoFrecuencium> Add(IEnumerable<TiempoFrecuencia> listadoEntidad)
        {
            try
            {
                List<TTiempoFrecuencium> listado = new List<TTiempoFrecuencium>();
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

        public IEnumerable<TTiempoFrecuencium> Update(IEnumerable<TiempoFrecuencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTiempoFrecuencium> listado = new List<TTiempoFrecuencium>();
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


        public List<ComboDTO> ObtenerListaTiempoFrecuencia()
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT id, nombre FROM mkt.T_TiempoFrecuencia WHERE estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComboDTO> ObtenerListaParaFiltroSegmento()
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT id, nombre FROM mkt.T_TiempoFrecuencia WHERE id IN (2,3,4,5)";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tiempos de frecuencia por Ids para combos.
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public IEnumerable<TiempoFrecuenciaDTO> ObtenerComboPorIds(int[] ids)
        {
            try
            {
                var _query = "SELECT Id, Nombre FROM mkt.T_TiempoFrecuencia WHERE Id IN @ids AND Estado=1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { ids });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<TiempoFrecuenciaDTO>>(respuestaDapper)!;
                return new List<TiempoFrecuenciaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerComboPorIds(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tiempos de frecuencia por Ids para combos.
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public async Task<IEnumerable<TiempoFrecuenciaDTO>> ObtenerComboPorIdsAsync(int[] ids)
        {
            try
            {
                var _query = "SELECT Id, Nombre FROM mkt.T_TiempoFrecuencia WHERE Id IN @ids AND Estado=1";
                var respuestaDapper = await _dapperRepository.QueryDapperAsync(_query, new { ids });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<TiempoFrecuenciaDTO>>(respuestaDapper)!;
                return new List<TiempoFrecuenciaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerComboPorIds(): {ex.Message}");
            }
        }
    }
}
