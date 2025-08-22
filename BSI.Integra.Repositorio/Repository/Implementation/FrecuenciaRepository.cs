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
    /// Repositorio: FrecuenciaRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_Frecuencia
    /// </summary>
    public class FrecuenciaRepository : GenericRepository<TFrecuencium>, IFrecuenciaRepository
    {
        private Mapper _mapper;

        public FrecuenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFrecuencium, Frecuencia>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFrecuencium MapeoEntidad(Frecuencia entidad)
        {
            try
            {
                //crea la entidad padre
                TFrecuencium modelo = _mapper.Map<TFrecuencium>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFrecuencium Add(Frecuencia entidad)
        {
            try
            {
                var Frecuencia = MapeoEntidad(entidad);
                base.Insert(Frecuencia);
                return Frecuencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFrecuencium Update(Frecuencia entidad)
        {
            try
            {
                var Frecuencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Frecuencia.RowVersion = entidadExistente.RowVersion;
                base.Update(Frecuencia);
                return Frecuencia;
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


        public IEnumerable<TFrecuencium> Add(IEnumerable<Frecuencia> listadoEntidad)
        {
            try
            {
                List<TFrecuencium> listado = new List<TFrecuencium>();
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

        public IEnumerable<TFrecuencium> Update(IEnumerable<Frecuencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFrecuencium> listado = new List<TFrecuencium>();
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
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de T_Frecuencia por id.
        /// </summary>
        /// <returns> Frecuencia </returns>
        public Frecuencia? ObtenerPorId(int id)
        {
            try
            {
                Frecuencia rpta = new Frecuencia();
                var query = @"SELECT Id, Nombre, NumDias, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion FROM pla.T_Frecuencia WHERE Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Frecuencia>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Frecuencia.
        /// </summary>
        /// <returns> List<FrecuenciaDTO> </returns>
        public IEnumerable<FrecuenciaDTO> ObtenerFrecuencia()
        {
            try
            {
                var query = @"SELECT Id,Nombre,NumDias FROM pla.T_Frecuencia where Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<FrecuenciaDTO>>(resultado);
                }
                return new List<FrecuenciaDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Frecuencia para combo
        /// </summary>
        /// <returns> Lista de Frecuencias </returns>
        public FrecuenciaDTO? ObtenerFrecuenciaPorId(int idFrecuencia)
        {
            try
            {
                var query = @"SELECT Id, Nombre, NumDias FROM pla.V_TFrecuenciaPorId WHERE Estado=1 AND Id=@IdFrecuencia;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdFrecuencia = idFrecuencia });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<FrecuenciaDTO>(resultado)!;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Frecuencia para combo
        /// </summary>
        /// <returns> Lista de Frecuencias </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM pla.V_TFrecuenciaPorId WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Frecuencia para combo
        /// </summary>
        /// <returns> Lista de Frecuencias </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM pla.V_TFrecuenciaPorId WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerComboAsync(): {ex.Message}", ex);
            }
        }

        public List<DatosFrecuenciaGeneralDTO> ObtenerListaFrecuenciaActividad()
        {
            try
            {
                string _queryFrecuencia = "Select Id,Nombre From pla.V_TFrecuenciaPorId Where Estado=1 and Id in (1,3,5)";
                var queryFrecuencia = _dapperRepository.QueryDapper(_queryFrecuencia, null);
                return JsonConvert.DeserializeObject<List<DatosFrecuenciaGeneralDTO>>(queryFrecuencia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<DatosFrecuenciaGeneralDTO> ObtenerFrecuenciaReporteDocumentos()
        {
            try
            {
                string _queryFrecuencia = "Select Id,Nombre From pla.V_TFrecuenciaPorId Where Estado=1 and Id in (1,3)";
                var queryFrecuencia = _dapperRepository.QueryDapper(_queryFrecuencia, null);
                return JsonConvert.DeserializeObject<List<DatosFrecuenciaGeneralDTO>>(queryFrecuencia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
