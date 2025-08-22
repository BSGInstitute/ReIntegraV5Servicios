using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ExamenAsignadoRepository : GenericRepository<TExamenAsignado>, IExamenAsignadoRepository
    {
        private Mapper _mapper;
        public ExamenAsignadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExamenAsignado, ExamenAsignado>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenAsignado, ExamenAsignadoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenAsignado, TExamenAsignado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TExamenAsignado MapeoEntidad(ExamenAsignado entidad)
        {
            try
            {
                TExamenAsignado modelo = _mapper.Map<TExamenAsignado>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenAsignado Add(ExamenAsignado entidad)
        {
            try
            {
                var ExamenAsignado = MapeoEntidad(entidad);
                base.Insert(ExamenAsignado);
                return ExamenAsignado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenAsignado Update(ExamenAsignado entidad)
        {
            try
            {
                var ExamenAsignado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ExamenAsignado.RowVersion = entidadExistente.RowVersion;

                base.Update(ExamenAsignado);
                return ExamenAsignado;
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
        public IEnumerable<TExamenAsignado> Add(IEnumerable<ExamenAsignado> listadoEntidad)
        {
            try
            {
                List<TExamenAsignado> listado = new List<TExamenAsignado>();
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
        public IEnumerable<TExamenAsignado> Update(IEnumerable<ExamenAsignado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TExamenAsignado> listado = new List<TExamenAsignado>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_ExamenAsignado por el Primary Key
        /// </summary>
        /// <returns>ExamenAsignado o Nulo</returns>
        public ExamenAsignado? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    Id,
		                IdProcesoSeleccion,
		                IdExamen,
		                IdPostulante,
		                EstadoExamen,
		                FechaInicio,
		                FechaFin,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EstadoAcceso,
		                VersionCentil
                    FROM gp.T_ExamenAsignado
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ExamenAsignado>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="idProcesoSeleccion">Id Proceso de Seleccion </param> 
        /// <summary>
        /// Obtiene un registro de T_ExamenAsignado por el Primary Key
        /// </summary>
        /// <returns>ExamenAsignado o Nulo</returns>
        public List<ExamenAsignado> ObtenerPorIdProcesoSeleccion(int idProcesoSeleccion)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    Id,
		                IdProcesoSeleccion,
		                IdExamen,
		                IdPostulante,
		                EstadoExamen,
		                FechaInicio,
		                FechaFin,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EstadoAcceso,
		                VersionCentil
                    FROM gp.T_ExamenAsignado
                    WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<ExamenAsignado>>(resultado)!;
                }
                return new List<ExamenAsignado>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="idProcesoSeleccion">Id Proceso de Seleccion </param> 
        /// <summary>
        /// Obtiene un registro de T_ExamenAsignado por el Primary Key
        /// </summary>
        /// <returns>ExamenAsignado o Nulo</returns>
        public List<int> ObtenerIdsPostulantesPorIdProcesoSeleccion(int idProcesoSeleccion)
        {
            try
            {
                var query = @"
                    SELECT 
		                IdPostulante AS Valor
                    FROM gp.T_ExamenAsignado
                    WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1
                    GROUP BY IdPostulante
                ";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<List<IntDTO>>(resultado)!;
                    return rpta.Select(x => x.Valor!.Value).ToList();
                }
                return new List<int>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_ExamenAsignado por el Primary Key
        /// </summary>
        /// <returns>ExamenAsignado o Nulo</returns>
        public ExamenAsignado? ObtenerPorIdPostulanteIdExamen(int idPostulante, int idExamen)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    Id,
		                IdProcesoSeleccion,
		                IdExamen,
		                IdPostulante,
		                EstadoExamen,
		                FechaInicio,
		                FechaFin,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EstadoAcceso,
		                VersionCentil
                    FROM gp.T_ExamenAsignado
                    WHERE IdPostulante = @IdPostulante AND IdExamen = @IdExamen AND Estado = 1
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulante = idPostulante, IdExamen = idExamen });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ExamenAsignado>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 05/11/2024
        /// <param name="idProcesoSeleccion">Id Proceso de Seleccion </param> 
        /// <param name="idPostulante">Id de Postulante</param> 
        /// <summary>
        /// Obtiene un registro de T_ExamenAsignado por los ids
        /// </summary>
        /// <returns>ExamenAsignado o Nulo</returns>
        public ExamenAsignado ObtenerPorIdProcesoSeleccionYPorIdPostulante(int idProcesoSeleccion, int idPostulante)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    Id,
		                IdProcesoSeleccion,
		                IdExamen,
		                IdPostulante,
		                EstadoExamen,
		                FechaInicio,
		                FechaFin,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EstadoAcceso,
		                VersionCentil
                    FROM gp.T_ExamenAsignado
                    WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND IdPostulante = @IdPostulante AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProcesoSeleccion = idProcesoSeleccion, IdPostulante = idPostulante });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ExamenAsignado>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdProcesoSeleccionYPorIdPostulante(), {ex.Message}");
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 05/11/2024
        /// <param name="idProcesoSeleccion">Id Proceso de Seleccion </param>
        /// <summary>
        /// Obtiene un registro de de configuracionExamen por el Id de ProcesoSeleccion
        /// </summary>
        /// <returns>Lista<ConfiguracionAsignacionExamenV2DTO> o Nulo</returns>
        public List<ConfiguracionAsignacionExamenV2DTO> ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(int idProcesoSeleccion)
        {
            try
            {
                var query = "SELECT Id, IdProcesoSeleccion, IdEvaluacion, IdExamen, NroOrden FROM [gp].[V_TConfiguracionAsignacionExamenV2] WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1 AND EsCalificadoPorPostulante = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<ConfiguracionAsignacionExamenV2DTO>>(resultado);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
