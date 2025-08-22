using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ActividadDetalleRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ActividadDetalle
    /// </summary>
    public class ActividadDetalleRepository : GenericRepository<TActividadDetalle>, IActividadDetalleRepository
    {
        private Mapper _mapper;

        public ActividadDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public bool Detached(ActividadDetalle entidad)
        {
            try
            {
                var ActividadDetalle = MapeoEntidad(entidad);
                return base.SetState(ActividadDetalle, EntityState.Detached);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Metodos Base
        private TActividadDetalle MapeoEntidad(ActividadDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TActividadDetalle modelo = _mapper.Map<TActividadDetalle>(entidad);

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

        public TActividadDetalle Add(ActividadDetalle entidad)
        {
            try
            {
                var ActividadDetalle = MapeoEntidad(entidad);
                base.Insert(ActividadDetalle);
                return ActividadDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadDetalle AddAsync(ActividadDetalle entidad)
        {
            try
            {
                var ActividadDetalle = MapeoEntidad(entidad);
                base.InsertAsync(ActividadDetalle);
                return ActividadDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TActividadDetalle Update(ActividadDetalle entidad)
        {
            try
            {
                var ActividadDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ActividadDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ActividadDetalle);
                return ActividadDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadDetalle UpdateAlterno(ActividadDetalle entidad)
        {
            try
            {
                var ActividadDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ActividadDetalle.RowVersion = entidadExistente.RowVersion;

                base.UpdateAlterno(ActividadDetalle);
                return ActividadDetalle;
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


        public IEnumerable<TActividadDetalle> Add(IEnumerable<ActividadDetalle> listadoEntidad)
        {
            try
            {
                List<TActividadDetalle> listado = new List<TActividadDetalle>();
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

        public IEnumerable<TActividadDetalle> Update(IEnumerable<ActividadDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TActividadDetalle> listado = new List<TActividadDetalle>();
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
        /// Obtiene todos los registros de T_ActividadDetalle.
        /// </summary>
        /// <returns> List<ActividadDetalleDTO> </returns>
        public IEnumerable<ActividadDetalleDTO> ObtenerActividadDetalle()
        {
            try
            {
                List<ActividadDetalleDTO> rpta = new List<ActividadDetalleDTO>();
                var query = @"
                    SELECT Id,IdActividadCabecera,FechaProgramada,FechaReal,DuracionReal,IdOcurrencia,IdEstadoActividadDetalle,Comentario,IdAlumno,Actor,
	                    IdOportunidad,IdCentralLlamada,refLlamada,IdOcurrenciaActividad,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,
	                    IdClasificacionPersona,FechaOcultarWhatsapp,IdOcurrenciaAlterno,IdOcurrenciaActividadAlterno
                    FROM com.T_ActividadDetalle
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de T_ActividadDetalle asociados a un Id.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> ActividadDetalleDTO </returns>
        public ActividadDetalle ObtenerPorId(int idActividadDetalle)
        {
            try
            {
                ActividadDetalle rpta = new ActividadDetalle();
                var query = @"
                        SELECT Id,
                            IdActividadCabecera,
                            FechaProgramada,
                            FechaReal,
                            DuracionReal,
                            IdOcurrencia,
                            IdEstadoActividadDetalle,
                            Comentario,
                            IdAlumno,
                            Actor,
                            IdOportunidad,
                            IdCentralLlamada,
                            refLlamada,
                            IdOcurrenciaActividad,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion,
                            RowVersion,
                            IdMigracion,
                            IdClasificacionPersona,
                            FechaOcultarWhatsapp,
                            IdOcurrenciaAlterno,
                            IdOcurrenciaActividadAlterno
                        FROM com.T_ActividadDetalle
                        WHERE Estado = 1 AND Id = @IdActividadDetalle";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdActividadDetalle = idActividadDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ActividadDetalle>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de T_ActividadDetalle asociados a un Id.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> ActividadDetalleDTO </returns>
        public async Task<ActividadDetalle> ObtenerPorIdAsync(int idActividadDetalle)
        {
            try
            {
                ActividadDetalle rpta = new ActividadDetalle();
                var query = @"
                        SELECT Id,
                            IdActividadCabecera,
                            FechaProgramada,
                            FechaReal,
                            DuracionReal,
                            IdOcurrencia,
                            IdEstadoActividadDetalle,
                            Comentario,
                            IdAlumno,
                            Actor,
                            IdOportunidad,
                            IdCentralLlamada,
                            refLlamada,
                            IdOcurrenciaActividad,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion,
                            RowVersion,
                            IdMigracion,
                            IdClasificacionPersona,
                            FechaOcultarWhatsapp,
                            IdOcurrenciaAlterno,
                            IdOcurrenciaActividadAlterno
                        FROM com.T_ActividadDetalle
                        WHERE Estado = 1 AND Id = @IdActividadDetalle";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { IdActividadDetalle = idActividadDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ActividadDetalle>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna una lista de actividades compuestas filtradas por un idactividad detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> List<CompuestoActividadEjecutadaDTO> </returns>
        public List<CompuestoActividadEjecutadaDTO> ObtenerAgendaActividades(int idActividadDetalle)
        {
            try
            {
                List<CompuestoActividadEjecutadaDTO> compuestosActividadesEjecutadas = new List<CompuestoActividadEjecutadaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_AgendaActividadesEjecutadasRealTimeNuevoModelo", new { idActividadDetalle });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    compuestosActividadesEjecutadas = JsonConvert.DeserializeObject<List<CompuestoActividadEjecutadaDTO>>(resultado)!;
                }
                return compuestosActividadesEjecutadas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna una lista de actividades compuestas filtradas por un idactividad detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> List<CompuestoActividadEjecutadaDTO> </returns>
        public async Task<List<CompuestoActividadEjecutadaDTO>> ObtenerAgendaActividadesAsync(int idActividadDetalle)
        {
            try
            {
                List<CompuestoActividadEjecutadaDTO> compuestosActividadesEjecutadas = new List<CompuestoActividadEjecutadaDTO>();
                var resultado = await _dapperRepository.QuerySPDapperAsync("com.SP_AgendaActividadesEjecutadasRealTimeNuevoModelo", new { idActividadDetalle });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    compuestosActividadesEjecutadas = JsonConvert.DeserializeObject<List<CompuestoActividadEjecutadaDTO>>(resultado)!;
                }
                return compuestosActividadesEjecutadas;
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
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteActividadOcurrenciaDTO> </returns>
        public List<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrenciaAgenda(int idOportunidad)
        {
            try
            {
                List<ReporteActividadOcurrenciaDTO> items = new List<ReporteActividadOcurrenciaDTO>();
                var query = @"
                    SELECT
	                    IdOportunidad,
	                    IdEstadoOcurrencia,
	                    IdFaseOportunidadAnterior,
	                    IdFaseActual,
	                    FechaReal
                    FROM com.V_NumeroActividadesEstadoOcurrencia
                    WHERE IdOportunidad = @idOportunidad";

                var resultado = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteActividadOcurrenciaDTO>>(resultado);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteActividadOcurrenciaDTO> </returns>
        public async Task<List<ReporteActividadOcurrenciaDTO>> ReporteActividadOcurrenciaAgendaAsync(int idOportunidad)
        {
            try
            {
                List<ReporteActividadOcurrenciaDTO> items = new List<ReporteActividadOcurrenciaDTO>();
                var query = @"
                    SELECT
	                    IdOportunidad,
	                    IdEstadoOcurrencia,
	                    IdFaseOportunidadAnterior,
	                    IdFaseActual,
	                    FechaReal
                    FROM com.V_NumeroActividadesEstadoOcurrencia
                    WHERE IdOportunidad = @idOportunidad";

                var resultado = await _dapperRepository.QueryDapperAsync(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteActividadOcurrenciaDTO>>(resultado);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id de la actividad detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Oportunidad</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                ValorIntDTO rpta = new ValorIntDTO();
                var query = @"SELECT Id AS Valor FROM com.T_ActividadDetalle WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idActividadDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
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
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de T_ActividadDetalle asociados a un Id.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> ActividadDetalle Entidad </returns>
        public ActividadDetalle ObtenerEntidadActividadDetallePorId(int idActividadDetalle)
        {
            try
            {
                var rpta = new ActividadDetalle();
                var query = @"
                    SELECT Id,IdActividadCabecera,FechaProgramada,FechaReal,DuracionReal,IdOcurrencia,IdEstadoActividadDetalle,Comentario,IdAlumno,Actor,
	                    IdOportunidad,IdCentralLlamada,refLlamada,IdOcurrenciaActividad,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,
	                    IdClasificacionPersona,FechaOcultarWhatsapp,IdOcurrenciaAlterno,IdOcurrenciaActividadAlterno, Estado
                    FROM com.T_ActividadDetalle
                    WHERE Estado = 1 AND Id = @idActividadDetalle";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idActividadDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ActividadDetalle>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de T_ActividadDetalle asociados a un Id.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> ActividadDetalle Entidad </returns>
        //public ActividadDetalle ObtenerPorId(int idActividadDetalle)
        //{
        //    try
        //    {
        //        var rpta = new ActividadDetalle();
        //        var query = @"SELECT    
        //                            Id,
        //                            IdActividadCabecera,
        //                            FechaProgramada,
        //                            FechaReal,
        //                            DuracionReal,
        //                            IdOcurrencia,
        //                            IdEstadoActividadDetalle,
        //                            Comentario,
        //                            IdAlumno,
        //                            Actor,
        //                            IdOportunidad,
        //                            IdCentralLlamada,
        //                            refLlamada,
        //                            IdOcurrenciaActividad,
        //                            Estado,
        //                            UsuarioCreacion,
        //                            UsuarioModificacion,
        //                            FechaCreacion,
        //                            FechaModificacion,
        //                            RowVersion,
        //                            IdMigracion,
        //                            IdClasificacionPersona,
        //                            FechaOcultarWhatsapp,
        //                            IdOcurrenciaAlterno,
        //                            IdOcurrenciaActividadAlterno
        //                        FROM com.T_ActividadDetalle
        //                        WHERE Estado = 1 AND Id = @idActividadDetalle";
        //        var resultado = _dapperRepository.FirstOrDefault(query, new { idActividadDetalle });
        //        if (!string.IsNullOrEmpty(resultado) && resultado != "null")
        //        {
        //            rpta = JsonConvert.DeserializeObject<ActividadDetalle>(resultado);
        //        }
        //        return rpta;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string ActualizarOportunidadYClasificacionPersona(int idOportunidad, int idClasificacionPersona, int id)
        {
            try
            {
                var rpta = new ActividadDetalle();
                var query = @" ";
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ActualizarOportunidadYClasificacionPersonaDeActividadDetalle", new { IdOportunidad = idOportunidad, IdClasificacionPersona = idClasificacionPersona, Id = id });

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene por el IdOportunidad el Id y la FechaCreacion  
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public ActividadDetalle ObtenerPorIdOportunidad(int idOportunidad)
        {
            try
            {
                ActividadDetalle respuesta = new ActividadDetalle();
                var query = @"
                            SELECT 
                                Id, FechaCreacion 
                            FROM 
                                com.T_ActividadDetalle 
                            WHERE 
                                IdOportunidad = @IdOportunidad AND Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<ActividadDetalle>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
