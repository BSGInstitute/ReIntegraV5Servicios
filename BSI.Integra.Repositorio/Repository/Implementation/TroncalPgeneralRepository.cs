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
    /// Repositorio: TroncalPgeneralRepository
    /// Autor: Margiory Meiss Ramirez Neyra..
    /// Fecha: 17/10/2022
    /// <summary>
    /// Gestión general de T_TroncalPgeneral
    /// </summary>
    public class TroncalPgeneralRepository : GenericRepository<TTroncalPgeneral>, ITroncalPgeneralRepository
    {
        private Mapper _mapper;
        public TroncalPgeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTroncalPgeneral, TroncalPgeneral>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTroncalPgeneral MapeoEntidad(TroncalPgeneral entidad)
        {
            try
            {
                TTroncalPgeneral modelo = _mapper.Map<TTroncalPgeneral>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTroncalPgeneral Add(TroncalPgeneral entidad)
        {
            try
            {
                var TroncalPgeneral = MapeoEntidad(entidad);
                base.Insert(TroncalPgeneral);
                return TroncalPgeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTroncalPgeneral Update(TroncalPgeneral entidad)
        {
            try
            {
                var TroncalPgeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TroncalPgeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(TroncalPgeneral);
                return TroncalPgeneral;
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
        public IEnumerable<TTroncalPgeneral> Add(IEnumerable<TroncalPgeneral> listadoEntidad)
        {
            try
            {
                List<TTroncalPgeneral> listado = new List<TTroncalPgeneral>();
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
        public IEnumerable<TTroncalPgeneral> Update(IEnumerable<TroncalPgeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTroncalPgeneral> listado = new List<TTroncalPgeneral>();
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

        /// <summary>
        /// Obtiene todos los registros de TroncalPgeneral con los campos de Id, Nombre y IdSubArea.
        /// </summary>
        /// <returns></returns>
        public List<TroncalPgeneralFiltroDTO> ObtenerTroncalPgeneralFiltro()
        {
            try
            {
                var lista = GetBy(x => true, y => new TroncalPgeneralFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdSubArea = y.IdSubArea,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2023
        /// <summary>
        /// Obtiene la lista de troncales con su region
        /// </summary> 
        /// <returns> IEnumerable<LocacionTroncalDTO> </returns>
        public IEnumerable<LocacionTroncalDTO> ObtenerLocacionTroncal()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdCiudad, CodigoBS, DenominacionBS FROM conf.V_ObtenerLocacionTroncal WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<LocacionTroncalDTO>>(resultado)!;
                return new List<LocacionTroncalDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerLocacionTroncal(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2023
        /// <summary>
        /// Obtiene la lista de troncales con su region
        /// </summary> 
        /// <returns> IEnumerable<LocacionTroncalDTO> </returns>
        public async Task<IEnumerable<LocacionTroncalDTO>> ObtenerLocacionTroncalAsync()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdCiudad, CodigoBS, DenominacionBS FROM conf.V_ObtenerLocacionTroncal WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<LocacionTroncalDTO>>(resultado)!;
                return new List<LocacionTroncalDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerLocacionTroncalAsync(): {ex.Message}", ex);
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 19/05/2023
        /// <summary>
        /// Obtiene la lista de troncales con su region
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TroncalPGeneralSubAreaCodigoDTO>> ObtenerPGeneralPorIdSubArea(int idSubArea)
        {
            try
            {
                var query = "Select Id,Nombre,Codigo,IdSubArea From pla.V_TTroncalPgeneral_PorIdSubArea Where Estado=1 and IdSubArea=@idSubArea";
                string resultado = await _dapperRepository.QueryDapperAsync(query, new { idSubArea });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<TroncalPGeneralSubAreaCodigoDTO>>(resultado)!;
                return new List<TroncalPGeneralSubAreaCodigoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerLocacionTroncalAsync(): {ex.Message}", ex);
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 19/05/2023
        /// <summary>
        /// Obtiene la lista de troncales con su region
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TroncalPGeneralSubAreaCodigoDTO>> ObtenerTroncalPGeneral()
        {
            try
            {
                var query = "Select Id,Nombre,Codigo,IdSubArea From pla.V_TTroncalPgeneral_PorIdSubArea Where Estado=1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<TroncalPGeneralSubAreaCodigoDTO>>(resultado)!;
                return new List<TroncalPGeneralSubAreaCodigoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerLocacionTroncalAsync(): {ex.Message}", ex);
            }
        }


        /// Autor: Gretel Canasa
        /// Fecha: 19/05/2023
        /// <summary>
        /// Obtiene la lista de troncales con su region
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerTroncalCiudadAsync()
        {
            try
            {
                var query = "Select Id,Nombre From pla.V_TTroncalCiudad_IdNombre  Where Estado=1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerLocacionTroncalAsync(): {ex.Message}", ex);
            }
        }


        /// Autor: Gilmer Qm
        /// Fecha: 28/06/2023
        /// <summary>
        /// Obtiene el registro por el Id
        /// </summary>
        /// <param name="id"> (PK) de </param>
        /// <returns> TroncalPgeneral </returns>
        public TroncalPgeneral? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre,
                                   Codigo,
                                   IdTroncalPartner,
                                   Duracion,
                                   IdArea,
                                   IdSubArea,
                                   IdBusqueda,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_TroncalPGeneral
                            WHERE Estado = 1
                                  AND Id = @Id;";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                    return JsonConvert.DeserializeObject<TroncalPgeneral>(resultado)!;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en TroncalPGeneral ObtenerPorId(): {ex.Message}", ex);
            }
        }
    }
}