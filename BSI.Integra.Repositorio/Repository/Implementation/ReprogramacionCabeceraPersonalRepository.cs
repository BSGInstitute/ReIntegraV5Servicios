using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReprogramacionCabeceraPersonalRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ReprogramacionCabeceraPersonal
    /// </summary>
    public class ReprogramacionCabeceraPersonalRepository : GenericRepository<TReprogramacionCabeceraPersonal>, IReprogramacionCabeceraPersonalRepository
    {
        private Mapper _mapper;

        public ReprogramacionCabeceraPersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReprogramacionCabeceraPersonal, ReprogramacionCabeceraPersonal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TReprogramacionCabeceraPersonal MapeoEntidad(ReprogramacionCabeceraPersonal entidad)
        {
            try
            {
                //crea la entidad padre
                TReprogramacionCabeceraPersonal modelo = _mapper.Map<TReprogramacionCabeceraPersonal>(entidad);

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

        public TReprogramacionCabeceraPersonal Add(ReprogramacionCabeceraPersonal entidad)
        {
            try
            {
                var ReprogramacionCabeceraPersonal = MapeoEntidad(entidad);
                base.Insert(ReprogramacionCabeceraPersonal);
                return ReprogramacionCabeceraPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TReprogramacionCabeceraPersonal AddAsync(ReprogramacionCabeceraPersonal entidad)
        {
            try
            {
                var ReprogramacionCabeceraPersonal = MapeoEntidad(entidad);
                base.InsertAsync(ReprogramacionCabeceraPersonal);
                return ReprogramacionCabeceraPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TReprogramacionCabeceraPersonal Update(ReprogramacionCabeceraPersonal entidad)
        {
            try
            {
                var ReprogramacionCabeceraPersonal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ReprogramacionCabeceraPersonal.RowVersion = entidadExistente.RowVersion;

                base.Update(ReprogramacionCabeceraPersonal);
                return ReprogramacionCabeceraPersonal;
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


        public IEnumerable<TReprogramacionCabeceraPersonal> Add(IEnumerable<ReprogramacionCabeceraPersonal> listadoEntidad)
        {
            try
            {
                List<TReprogramacionCabeceraPersonal> listado = new List<TReprogramacionCabeceraPersonal>();
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

        public IEnumerable<TReprogramacionCabeceraPersonal> Update(IEnumerable<ReprogramacionCabeceraPersonal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TReprogramacionCabeceraPersonal> listado = new List<TReprogramacionCabeceraPersonal>();
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
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ReprogramacionCabeceraPersonal.
        /// </summary>
        /// <returns> List<ReprogramacionCabeceraPersonalDTO> </returns>
        public IEnumerable<ReprogramacionCabeceraPersonalDTO> ObtenerReprogramacionCabeceraPersonal()
        {
            try
            {
                List<ReprogramacionCabeceraPersonalDTO> rpta = new List<ReprogramacionCabeceraPersonalDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdActividadCabecera,
	                    IdCategoriaOrigen,
	                    ReproDia,
	                    FechaReprogramacion,
	                    IdPersonal,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_ReprogramacionCabeceraPersonal
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReprogramacionCabeceraPersonalDTO>>(resultado);
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
        /// Obtiene el registro de T_ReprogramacionCabeceraPersonal segun los parametros enviados.
        /// </summary>
        /// <param name="idActividadCabecera">Id de Actividad Cabecera</param>
        /// <param name="idCategoriaOrigen">Id de Categoria Origen</param>
        /// <param name="idPersonal">Id de Personal</param>
        /// <returns> ReprogramacionCabeceraPersonalDTO </returns>
        public ReprogramacionCabeceraPersonal? ObtenerPorIdActividadCabeceraIdCategoriaOrigenIdPersonal(
            int idActividadCabecera, int idCategoriaOrigen, int idPersonal
        )
        {
            try
            {
                ReprogramacionCabeceraPersonal rpta = new ReprogramacionCabeceraPersonal();
                var hoy = DateTime.Now.Date;
                var query = @"
                    SELECT
	                    Id,
		                IdActividadCabecera,
		                IdCategoriaOrigen,
		                ReproDia,
		                FechaReprogramacion,
		                IdPersonal,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM com.T_ReprogramacionCabeceraPersonal
                    WHERE Estado = 1
                        AND IdActividadCabecera = @idActividadCabecera
                        AND IdCategoriaOrigen = @idCategoriaOrigen
                        AND IdPersonal = @idPersonal
                        AND FechaReprogramacion = @hoy";
                var resultado = _dapperRepository.FirstOrDefault(query, new
                {
                    idActividadCabecera,
                    idCategoriaOrigen,
                    idPersonal,
                    hoy
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    rpta = JsonConvert.DeserializeObject<ReprogramacionCabeceraPersonal>(resultado);
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
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_ReprogramacionCabeceraPersonal segun los parametros enviados.
        /// </summary>
        /// <param name="idActividadCabecera">Id de Actividad Cabecera</param>
        /// <param name="idCategoriaOrigen">Id de Categoria Origen</param>
        /// <param name="idPersonal">Id de Personal</param>
        /// <returns> ReprogramacionCabeceraPersonalDTO </returns>
        public async Task<ReprogramacionCabeceraPersonal>? ObtenerPorIdActividadCabeceraIdCategoriaOrigenIdPersonalAsync(
            int idActividadCabecera, int idCategoriaOrigen, int idPersonal
        )
        {
            try
            {
                ReprogramacionCabeceraPersonal rpta = new ReprogramacionCabeceraPersonal();
                var hoy = DateTime.Now.Date;
                var query = @"
                    SELECT
	                    Id,
		                IdActividadCabecera,
		                IdCategoriaOrigen,
		                ReproDia,
		                FechaReprogramacion,
		                IdPersonal,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM com.T_ReprogramacionCabeceraPersonal
                    WHERE Estado = 1
                        AND IdActividadCabecera = @idActividadCabecera
                        AND IdCategoriaOrigen = @idCategoriaOrigen
                        AND IdPersonal = @idPersonal
                        AND FechaReprogramacion = @hoy";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new
                {
                    idActividadCabecera,
                    idCategoriaOrigen,
                    idPersonal,
                    hoy
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    rpta = JsonConvert.DeserializeObject<ReprogramacionCabeceraPersonal>(resultado);
                else
                    return null;
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
