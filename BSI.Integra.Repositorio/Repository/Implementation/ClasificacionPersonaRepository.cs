using AutoMapper;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ClasificacionPersonaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 23/07/2022
    /// <summary>
    /// Gestión general de T_ClasificacionPersona
    /// </summary>
    public class ClasificacionPersonaRepository : GenericRepository<TClasificacionPersona>, IClasificacionPersonaRepository
    {
        private Mapper _mapper;

        public ClasificacionPersonaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TClasificacionPersona, ClasificacionPersona>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TClasificacionPersona MapeoEntidad(ClasificacionPersona entidad)
        {
            try
            {
                //crea la entidad padre
                TClasificacionPersona modelo = _mapper.Map<TClasificacionPersona>(entidad);

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

        public TClasificacionPersona Add(ClasificacionPersona entidad)
        {
            try
            {
                var ClasificacionPersona = MapeoEntidad(entidad);
                base.Insert(ClasificacionPersona);
                return ClasificacionPersona;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TClasificacionPersona Update(ClasificacionPersona entidad)
        {
            try
            {
                var ClasificacionPersona = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ClasificacionPersona.RowVersion = entidadExistente.RowVersion;

                base.Update(ClasificacionPersona);
                return ClasificacionPersona;
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


        public IEnumerable<TClasificacionPersona> Add(IEnumerable<ClasificacionPersona> listadoEntidad)
        {
            try
            {
                List<TClasificacionPersona> listado = new List<TClasificacionPersona>();
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

        public IEnumerable<TClasificacionPersona> Update(IEnumerable<ClasificacionPersona> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TClasificacionPersona> listado = new List<TClasificacionPersona>();
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
        /// Fecha: 23/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ClasificacionPersona.
        /// </summary>
        /// <returns> List<ClasificacionPersonaDTO> </returns>
        public IEnumerable<ClasificacionPersonaDTO> ObtenerClasificacionPersona()
        {
            try
            {
                List<ClasificacionPersonaDTO> rpta = new List<ClasificacionPersonaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPersona,
	                    IdTipoPersona,
	                    IdTablaOriginal,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM conf.T_ClasificacionPersona
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ClasificacionPersonaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 23/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ClasificacionPersona para mostrarse en combo.
        /// </summary>
        /// <returns> List<ClasificacionPersonaComboDTO> </returns>
        public IEnumerable<ClasificacionPersonaComboDTO> ObtenerCombo()
        {
            try
            {
                List<ClasificacionPersonaComboDTO> rpta = new List<ClasificacionPersonaComboDTO>();
                var query = @"SELECT Id,IdPersona,IdTipoPersona,IdTablaOriginal FROM conf.T_ClasificacionPersona WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ClasificacionPersonaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id de la taba CalisifacionPersona por el tipo de persona y el IdAlumno.
        /// </summary>
        /// <param name="tipo">Tipo persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> ClasificacionPersonaIdDTO </returns>
        public ValorIntDTO ObtenerIdClasificacionPersonaPorTipoYIdAlumno(int tipo, int idAlumno)
        {
            try
            {
                var rpta = new ValorIntDTO();
                var query = @"SELECT IdPersona as Id 
                            FROM conf.T_ClasificacionPersona 
                            WHERE Estado =1 AND IdTipoPersona= " + tipo + " AND IdTablaOriginal= " + idAlumno;
                var resultado = _dapperRepository.FirstOrDefault(query, new { tipo, idAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id de la taba CalisifacionPersona por el tipo de persona y el IdAlumno.
        /// </summary>
        /// <param name="tipo">Tipo persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> ClasificacionPersonaIdDTO </returns>
        public ClasificacionPersona ObtenerPorIdAlumno(int idAlumno)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdPersona,
	                    IdTipoPersona,
	                    IdTablaOriginal,
	                    IdMigracion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM conf.T_ClasificacionPersona
                    WHERE
	                    Estado = 1
	                    AND IdTipoPersona = 1
	                    AND IdTablaOriginal = @idAlumno;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ClasificacionPersona>(resultado)!;
                }
                return new ClasificacionPersona();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdAlumno: {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza una busqueda de ClasifiacionPersona por su id y tipo de persona
        /// </summary>
        /// <param name="idPersona"> id de la persona </param>
        /// <param name="tipoPersona"> tipo de persona </param>
        /// <returns> 1 o 0 </returns>
        public bool ExistePorIdPersonaTipoPersona(int idPersona, TipoPersona tipoPersona)
        {
            try
            {
                var query = @"SELECT TOP 1 Id AS Valor FROM conf.T_ClasificacionPersona WHERE Estado = 1 AND IdPersona = @IdPersona AND IdTipoPersona = @TipoPersona";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPersona = idPersona, TipoPersona = (int)tipoPersona });
                return (resultado != "null");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza una busqueda de ClasifiacionPersona por su id y tipo de persona
        /// </summary>
        /// <param name="idPersona"> id de la persona </param>
        /// <param name="tipoPersona"> tipo de persona </param>
        /// <returns> 1 o 0 </returns>
        public ClasificacionPersona ObtenerPorIdPersonaTipoPersona(int idPersona, TipoPersona tipoPersona)
        {
            try
            {
                var query = @"SELECT Id, IdPersona, IdTipoPersona, IdTablaOriginal, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion FROM conf.T_ClasificacionPersona WHERE Estado = 1 AND IdPersona = @IdPersona AND IdTipoPersona = @TipoPersona";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPersona = idPersona, TipoPersona = (int)tipoPersona });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ClasificacionPersona>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 24/06/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una busqueda de ClasifiacionPersona por su id y tipo de persona ALUMNO
        /// </summary>
        /// <param name="idPersona"> id de la persona </param>
        /// <returns> 1 o 0 </returns>
        public ClasificacionPersona? ObtenerPorIdPersonaTipoAlumno(int idPersona)
        {
            try
            {
                var query = @"SELECT 
			                Id	,
			                IdPersona,
			                IdTipoPersona,
			                IdTablaOriginal,
			                Estado,
			                UsuarioCreacion,
			                UsuarioModificacion,
			                FechaCreacion,
			                FechaModificacion,
			                RowVersion,
			                IdMigracion
		                FROM conf.T_ClasificacionPersona 
                        WHERE Estado = 1 AND IdPersona = @IdPersona AND IdTipoPersona = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPersona = idPersona });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ClasificacionPersona>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClasificacionPersona ObtenerPorIdAlumnoYTipoPersona(int idTablaOriginal, TipoPersona tipoPersona)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPersona,
                                   IdTipoPersona,
                                   IdTablaOriginal,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM conf.T_ClasificacionPersona
                            WHERE IdTablaOriginal = @IdTablaOriginal
                                  AND IdTipoPersona = @IdTipoPersona
                                  AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdTablaOriginal = idTablaOriginal, IdTipoPersona = (int)tipoPersona });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ClasificacionPersona>(resultado)!;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza una busqueda de ClasifiacionPersona por su id y tipo de persona
        /// </summary>
        /// <param name="idTablaOriginal"> id de la tabla original </param>
        /// <param name="tipoPersona"> tipo de persona </param>
        /// <param name="idPersona"> id de la persona </param>
        /// <returns> 1 o 0 </returns>
        public ValorIntDTO ObtenerPorTablaOriginalPorTipoPersonaPorPersona(int idTablaOriginal, Enums.TipoPersona tipoPersona, int idPersona)
        {
            try
            {
                var rpta = new ValorIntDTO();
                var query = @"SELECT Id  
                                FROM conf.T_ClasificacionPersona 
                                WHERE Estado =1 AND IdTablaOriginal = @IdTablaOriginal 
                                AND IdTipoPersona = @IdTipoPersona 
                                AND IdPersona = @IdPersona";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdTablaOriginal = idTablaOriginal, IdTipoPersona = tipoPersona, IdPersona = idPersona });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != null && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza una busqueda de ClasifiacionPersona por su id y tipo de persona
        /// </summary>
        /// <param name="idTablaOriginal"> id de la tabla original </param>
        /// <param name="tipoPersona"> tipo de persona </param>
        /// <param name="idPersona"> id de la persona </param>
        /// <returns> 1 o 0 </returns>
        public ClasificacionPersona ObtenerPorIdTablaOriginalIdPersonaTipoPersona(int idTablaOriginal, TipoPersona tipoPersona, int idPersona)
        {
            try
            {
                var rpta = new ClasificacionPersona();
                var query = @"SELECT Id,
                                IdPersona,
                                IdTipoPersona,
                                IdTablaOriginal, 
                                Estado,
                                FechaCreacion,
                                FechaModificacion,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                RowVersion, 
                                IdMigracion
                            FROM conf.T_ClasificacionPersona 
                            WHERE  Estado =1 AND IdTablaOriginal = @IdTablaOriginal 
                                AND IdTipoPersona = @IdTipoPersona 
                                AND IdPersona = @IdPersona";

                var resultado = _dapperRepository.FirstOrDefault(query, new { IdTablaOriginal = idTablaOriginal, IdTipoPersona = (int)tipoPersona, IdPersona = idPersona });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ClasificacionPersona>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ClasificacionPersona? ObtenerPorPersonalYTipoPersona(int idPersona, int tipoPersona)
        {
            try
            {
                ClasificacionPersona? final = null;
                var query = @"SELECT Id,
                             IdPersona,
                             IdTipoPersona,
                             IdTablaOriginal,
                             Estado,
                             UsuarioCreacion,
                             UsuarioModificacion,
                             FechaCreacion,
                             FechaModificacion,
                             RowVersion,
                             IdMigracion
                      FROM conf.T_ClasificacionPersona
                      WHERE IdPersona = @idPersona
                            AND IdTipoPersona = @tipoPersona
                            AND Estado = 1;"; 
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersona, tipoPersona });
                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    final = JsonConvert.DeserializeObject<ClasificacionPersona>(resultado);
                }

                return final;
            }
            catch (Exception ex)
            {
                throw; // Mantiene la traza original de la excepción sin sobreescribirla
            }
        }
        public ClasificacionPersona ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPersona,
                                   IdTipoPersona,
                                   IdTablaOriginal,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM conf.T_ClasificacionPersona
                            WHERE Id = @id
                                  AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ClasificacionPersona>(resultado)!;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
