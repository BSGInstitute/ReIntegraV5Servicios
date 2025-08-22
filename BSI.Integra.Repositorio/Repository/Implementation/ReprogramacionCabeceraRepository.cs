using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReprogramacionCabeceraRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_ReprogramacionCabecera
    /// </summary>
    public class ReprogramacionCabeceraRepository : GenericRepository<TReprogramacionCabecera>, IReprogramacionCabeceraRepository
    {
        private Mapper _mapper;

        public ReprogramacionCabeceraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReprogramacionCabecera, ReprogramacionCabecera>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TReprogramacionCabecera MapeoEntidad(ReprogramacionCabecera entidad)
        {
            try
            {
                //crea la entidad padre
                TReprogramacionCabecera modelo = _mapper.Map<TReprogramacionCabecera>(entidad);

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

        public TReprogramacionCabecera Add(ReprogramacionCabecera entidad)
        {
            try
            {
                var ReprogramacionCabecera = MapeoEntidad(entidad);
                base.Insert(ReprogramacionCabecera);
                return ReprogramacionCabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TReprogramacionCabecera Update(ReprogramacionCabecera entidad)
        {
            try
            {
                var ReprogramacionCabecera = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ReprogramacionCabecera.RowVersion = entidadExistente.RowVersion;

                base.Update(ReprogramacionCabecera);
                return ReprogramacionCabecera;
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


        public IEnumerable<TReprogramacionCabecera> Add(IEnumerable<ReprogramacionCabecera> listadoEntidad)
        {
            try
            {
                List<TReprogramacionCabecera> listado = new List<TReprogramacionCabecera>();
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

        public IEnumerable<TReprogramacionCabecera> Update(IEnumerable<ReprogramacionCabecera> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TReprogramacionCabecera> listado = new List<TReprogramacionCabecera>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ReprogramacionCabecera.
        /// </summary>
        /// <returns> List<ReprogramacionCabeceraDTO> </returns>
        public IEnumerable<ReprogramacionCabeceraDTO> ObtenerReprogramacionCabecera()
        {
            try
            {
                List<ReprogramacionCabeceraDTO> rpta = new List<ReprogramacionCabeceraDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdActividadCabecera,
	                    IdCategoriaOrigen,
	                    MaxReproPorDia,
	                    IntervaloSigProgramacionMin,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_ReprogramacionCabecera
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReprogramacionCabeceraDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el intervalo minimo y la maxima cantidad de programaaciones que se puede hacer por dia segun la categoria.
        /// </summary>
        /// <param name="idActividadCabecera"></param>
        /// <param name="idCategoria"></param>
        /// <returns> List<ReprogramacionCabeceraDTO> </returns>
        public ReprogramacionCabeceraRADTO ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(int idActividadCabecera, int idCategoria)
        {
            try
            {
                ReprogramacionCabeceraRADTO rpta = new ReprogramacionCabeceraRADTO();
                var query = @"
                    SELECT IntervaloSigProgramacionMin,MaxReproPorDia
                    FROM com.V_TReprogramacionCabecera_FechaProgramacionAutomatica
                    WHERE IdActividadCabecera = @idActividadCabecera AND IdCategoriaOrigen = @idCategoria";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idActividadCabecera, idCategoria });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    rpta = JsonConvert.DeserializeObject<ReprogramacionCabeceraRADTO>(resultado);
                else
                    return null;
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el intervalo minimo y la maxima cantidad de programaaciones que se puede hacer por dia segun la categoria.
        /// </summary>
        /// <param name="idActividadCabecera">Id de Actividad Cabecera</param>
        /// <param name="idCategoria">Id de Categoria Origen</param>
        /// <returns> ReprogramacionCabeceraPersonalRADTO </returns>
        public ReprogramacionCabeceraPersonalRADTO ObtenerCantidadReprogramacionDelDiaPorAsesor(int idActividadCabecera, int idCategoria, int idPersonal)
        {
            try
            {
                ReprogramacionCabeceraPersonalRADTO rpta = new ReprogramacionCabeceraPersonalRADTO();
                var query = @"
                    SELECT ReproDia
                    FROM com.V_TReprogramacionCabeceraPersonal_FechaProgramacionAutomatica
                    WHERE IdActividadCabecera = @idActividadCabecera
	                    AND IdCategoriaOrigen = @idCategoria
	                    AND IdPersonal = @idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idActividadCabecera, idCategoria, idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ReprogramacionCabeceraPersonalRADTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos Los Datos de la Reprogramacion Por Actividad  y Categoria
        /// </summary>
        /// <param name="idActividadCabecera">Id de Actividad Cabecera</param>
        /// <param name="idCategoriaOrigen">Id de CategoriaOrigen</param>
        /// <returns> ReprogramacionCabeceraSinAuditoriaDTO </returns>
        public ReprogramacionCabecera ObtenerPorIdCabeceraIdCategoriaOrigen(int idActividadCabecera, int idCategoriaOrigen)
        {
            try
            {
                ReprogramacionCabecera rpta = new ReprogramacionCabecera();
                var query = @"
                    SELECT Id,IdActividadCabecera,IdCategoriaOrigen,MaxReproPorDia,IntervaloSigProgramacionMin
                    FROM com.V_TReprogramacionCabecera_ObtenerTodo
                    WHERE Estado = 1
	                    AND IdActividadCabecera = @idActividadCabecera
	                    AND IdCategoriaOrigen = @idCategoriaOrigen";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idActividadCabecera, idCategoriaOrigen });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ReprogramacionCabecera>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos Los Datos de la Reprogramacion Por Actividad  y Categoria
        /// </summary>
        /// <param name="idActividadCabecera">Id de Actividad Cabecera</param>
        /// <param name="idCategoriaOrigen">Id de CategoriaOrigen</param>
        /// <returns> ReprogramacionCabeceraSinAuditoriaDTO </returns>
        public async Task<ReprogramacionCabecera> ObtenerPorIdCabeceraIdCategoriaOrigenAsync(int idActividadCabecera, int idCategoriaOrigen)
        {
            try
            {
                ReprogramacionCabecera rpta = new ReprogramacionCabecera();
                var query = @"
                    SELECT Id,IdActividadCabecera,IdCategoriaOrigen,MaxReproPorDia,IntervaloSigProgramacionMin
                    FROM com.V_TReprogramacionCabecera_ObtenerTodo
                    WHERE Estado = 1
	                    AND IdActividadCabecera = @idActividadCabecera
	                    AND IdCategoriaOrigen = @idCategoriaOrigen";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idActividadCabecera, idCategoriaOrigen });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ReprogramacionCabecera>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Reprogramacion por actividad cabecera
        /// </summary>
        /// <param name="IdActividadCabecera">Id de Actividad Cabecera</param>
        /// <returns> List<ReprogramacionCabeceraDTO> </returns>
        public List<ReprogramacionCabeceraDTO> ObtenerReprogramacionCabPorActividadCab(int IdActividadCabecera)
        {
            try
            {
                List<ReprogramacionCabeceraDTO> ReprogramacionCab = new List<ReprogramacionCabeceraDTO>();
                var query = string.Empty;
                query = "SELECT Id, IdActividadCabecera, IdCategoriaOrigen, MaxReproPorDia, IntervaloSigProgramacionMin, Text_IdCategoriaOrigen FROM com.V_TReprogramacionCabecera_ObtenerTodo WHERE Estado=1 AND IdActividadCabecera=" + IdActividadCabecera;
                var ReprogramacionCabDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(ReprogramacionCabDB) && !ReprogramacionCabDB.Contains("[]"))
                {
                    ReprogramacionCab = JsonConvert.DeserializeObject<List<ReprogramacionCabeceraDTO>>(ReprogramacionCabDB);
                }
                return ReprogramacionCab;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
