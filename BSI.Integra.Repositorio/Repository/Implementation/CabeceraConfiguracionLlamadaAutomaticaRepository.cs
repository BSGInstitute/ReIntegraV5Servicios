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
    /// Repositorio: CabeceraConfiguracionLlamadaAutomaticaRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CabeceraConfiguracionLlamadaAutomatica
    /// </summary>
    public class CabeceraConfiguracionLlamadaAutomaticaRepository : GenericRepository<TCabeceraConfiguracionLlamadaAutomatica>, ICabeceraConfiguracionLlamadaAutomaticaRepository
    {
        private Mapper _mapper;

        public CabeceraConfiguracionLlamadaAutomaticaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCabeceraConfiguracionLlamadaAutomatica, CabeceraConfiguracionLlamadaAutomatica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCabeceraConfiguracionLlamadaAutomatica MapeoEntidad(CabeceraConfiguracionLlamadaAutomatica entidad)
        {
            try
            {
                //crea la entidad padre
                TCabeceraConfiguracionLlamadaAutomatica modelo = _mapper.Map<TCabeceraConfiguracionLlamadaAutomatica>(entidad);

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

        public TCabeceraConfiguracionLlamadaAutomatica Add(CabeceraConfiguracionLlamadaAutomatica entidad)
        {
            try
            {
                var CabeceraConfiguracionLlamadaAutomatica = MapeoEntidad(entidad);
                base.Insert(CabeceraConfiguracionLlamadaAutomatica);
                return CabeceraConfiguracionLlamadaAutomatica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCabeceraConfiguracionLlamadaAutomatica Update(CabeceraConfiguracionLlamadaAutomatica entidad)
        {
            try
            {
                var CabeceraConfiguracionLlamadaAutomatica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CabeceraConfiguracionLlamadaAutomatica.RowVersion = entidadExistente.RowVersion;

                base.Update(CabeceraConfiguracionLlamadaAutomatica);
                return CabeceraConfiguracionLlamadaAutomatica;
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


        public IEnumerable<TCabeceraConfiguracionLlamadaAutomatica> Add(IEnumerable<CabeceraConfiguracionLlamadaAutomatica> listadoEntidad)
        {
            try
            {
                List<TCabeceraConfiguracionLlamadaAutomatica> listado = new List<TCabeceraConfiguracionLlamadaAutomatica>();
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

        public IEnumerable<TCabeceraConfiguracionLlamadaAutomatica> Update(IEnumerable<CabeceraConfiguracionLlamadaAutomatica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCabeceraConfiguracionLlamadaAutomatica> listado = new List<TCabeceraConfiguracionLlamadaAutomatica>();
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
        /// Obtiene todos los registros de T_CabeceraConfiguracionLlamadaAutomatica.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaDTO> </returns>
        public IEnumerable<LlamadaAutomaticaConfiguracionDTO> ObtenerCabeceraConfiguracionLlamadaAutomatica()
        {
            try
            {
                List<LlamadaAutomaticaConfiguracionDTO> rpta = new List<LlamadaAutomaticaConfiguracionDTO>();
                var query = @"
                        SELECT id,
                               [Nombre]
                              ,[IdIvrPlantilla]
                              ,[IvrPlantilla]
                              ,[PGeneral]
                              ,[PEspecifico]
                              ,[IvrEjecucion]
                              ,[IdIvrEjecucion]
                              ,[HoraInicio]
                              ,[HoraFin]
                              ,[EstadoProceso]
                              ,[IdIvrTipoConfiguracion]
                              ,[CongelamientoConfiguracion]
                          FROM ope.V_LlamadaAutomaticaObtenerCabeceraConfiguracion
                        ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado.Length>=4)
                {
                    rpta = JsonConvert.DeserializeObject<List<LlamadaAutomaticaConfiguracionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CabeceraConfiguracionLlamadaAutomatica.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaDTO> </returns>
        public RangoHoraEjecucionDialerDTO ObtenerRangoHoraEjecucionDialer(int IdIvrEjecucion)
        {
            try
            {
                RangoHoraEjecucionDialerDTO rpta = new RangoHoraEjecucionDialerDTO();
                var query = @"
                        SELECT TOP 1
	                        IdIvrEjecucion,
	                        MIN(HoraInicio)  AS HoraInicio,
	                        MAX(HoraFin) AS HoraFin
                        FROM ope.T_CabeceraConfiguracionLlamadaAutomatica 
                        WHERE ESTADO=1 and IdIvrEjecucion = @IdIvrEjecucion
                        GROUP BY IdIvrEjecucion
                                                ";
                var resultado = _dapperRepository.FirstOrDefault(query,new { IdIvrEjecucion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<RangoHoraEjecucionDialerDTO>(resultado);
                    return rpta;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = string.Empty;
                query = @"
                    SELECT [Id]
                          ,[Nombre]
                    FROM [ope].[T_CabeceraConfiguracionLlamadaAutomatica]
                    WHERE [Estado]=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public bool GenerarRegistrosRecordatorioClases(FiltroGenerarDataLLamdaAutomaticaDTO data)
        {
            try
            {
                var queryResult = _dapperRepository.QuerySPDapper("ope.SP_LlamadaAutomaticaGenerarRegistrosRecordatorioClases", 
                    new {
                        IdsPEspecificoSesion = data.IdsSesiones,
                        IdTipoModalidad = data.IdTipoModalidad.Value,
                        IdCabeceraConfiguracion =data.IdCabeceraConfiguracion.Value,
                        IdPEspecifico =data.IdPEspecifico.Value
                    });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public bool GenerarRegistrosRecordatorioWebinar(FiltroGenerarDataLLamdaAutomaticaDTO data)
        {
            try
            {
                var queryResult = _dapperRepository.QuerySPDapper("ope.SP_LlamadaAutomaticaGenerarRegistrosRecordatorioWebinar",
                    new
                    {
                        IdsPEspecificoSesion = data.IdsSesiones,
                        IdTipoModalidad = data.IdTipoModalidad.Value,
                        IdCabeceraConfiguracion = data.IdCabeceraConfiguracion.Value,
                        IdPEspecifico = data.IdPEspecifico.Value,
                        IsTodosWebinar = data.IsTodosWebinar
                    }) ;
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public bool GenerarRegistrosRecordatorioCuotaCronograma(FiltroGenerarDataLLamdaAutomaticaDTO data)
        {
            try
            {
                DateTime FechaHoy = DateTime.Now;
                FechaHoy  = FechaHoy.AddDays(data.DiasCalculoCuota.Value);
                var queryResult = _dapperRepository.QuerySPDapper("ope.SP_LlamadaAutomaticaGenerarRegistrosRecordatorioCuota",
                    new
                    {
                        IdPEspecifico = data.IdPEspecifico,
                        IsPorPrograma = data.IsPorPrograma,
                        IdCabeceraConfiguracion = data.IdCabeceraConfiguracion.Value,
                        FechaComparacion = FechaHoy,
                    });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public bool GenerarRegistrosRecordatorioAsistencia(FiltroGenerarDataLLamdaAutomaticaDTO data)
        {
            try
            {
                DateTime FechaHoy = DateTime.Now;
                FechaHoy = FechaHoy.AddDays(data.DiasCalculoCuota.Value);
                var queryResult = _dapperRepository.QuerySPDapper("ope.SP_LlamadaAutomaticaGenerarRegistrosRecordatorioAsistencia",
                    new
                    {
                        IdsPEspecificoSesion = data.IdsSesiones,
                        IdCabeceraConfiguracion = data.IdCabeceraConfiguracion,
                        Asistio = data.Asistio,
                        Justifico = data.Justifico,
                        IsTodos = data.IsTodosAsistencia
                    });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public bool GenerarRegistrosRecordatorioAvanceAcademicoAO(FiltroGenerarDataLLamdaAutomaticaDTO data)
        {
            try
            {
                DateTime FechaHoy = DateTime.Now;
                var queryResult = _dapperRepository.QuerySPDapper("ope.SP_LlamadaAutomaticaGenerarRegistrosRecordatorioAvanceAcademicoAO",
                    new
                    {
                        IsSinAvance = data.IsSinAvance,
                        IsMasDe = data.IsMasDe,
                        IsMenosDe = data.IsMenosDe,
                        IsEntre = data.IsEntre,
                        IsPorPrograma = data.IsPorPrograma,
                        IdPEspecifico = data.IdPEspecifico,
                        AjusteDias = data.DiasCalculoCuota,
                        Valor1 = data.Valor1,
                        Valor2 = data.Valor2,
                        IdCabeceraConfiguracion = data.IdCabeceraConfiguracion


                    });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionClases(int IdCabecera, List<int> IdsSesion)
        {
            try
            {
                List<DetalleCabeceraConfiguracionDTO> rpta = new List<DetalleCabeceraConfiguracionDTO>();
                var query = string.Empty;
                var listaSesion = string.Join(",", IdsSesion);
                query = @"
                   SELECT 
                        Id,
	                    IdSesion,
	                    IdCabecera,
	                    Alumno,
	                    AsistenteAcademico,
	                    FechaSesion,
	                    EstadoLLamada
                    FROM  ope.V_LlamadaAutomaticaObtenerDetalleCabeceraConfiguracionClases
                    WHERE IdCabecera = @IdCabecera and IdSesion in (SELECT Item FROM conf.F_Splitstring(@listaSesion, ','))
                    ORDER BY FechaSesion ASC";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    IdCabecera,
                    listaSesion
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleCabeceraConfiguracionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionWebinar(int IdCabecera, List<int> IdsSesion)
        {
            try
            {
                List<DetalleCabeceraConfiguracionDTO> rpta = new List<DetalleCabeceraConfiguracionDTO>();
                var query = string.Empty;
                var listaSesion = string.Join(",", IdsSesion);
                query = @"
                   SELECT 
                        Id,
	                    IdSesion,
	                    IdCabecera,
	                    Alumno,
	                    AsistenteAcademico,
	                    FechaSesion,
	                    EstadoLLamada
                    FROM  ope.V_LlamadaAutomaticaObtenerDetalleCabeceraConfiguracionWebinar
                    WHERE IdCabecera = @IdCabecera and IdSesion in (SELECT Item FROM conf.F_Splitstring(@listaSesion, ','))
                    ORDER BY FechaSesion ASC";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    IdCabecera,
                    listaSesion
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleCabeceraConfiguracionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionCuota(int IdCabecera)
        {
            try
            {
                List<DetalleCabeceraConfiguracionDTO> rpta = new List<DetalleCabeceraConfiguracionDTO>();
                var query = string.Empty;
                query = @"
                   SELECT 
                        Id,
	                    IdSesion,
	                    IdCabecera,
	                    Alumno,
	                    AsistenteAcademico,
	                    FechaSesion,
	                    EstadoLLamada
                    FROM  ope.V_LlamadaAutomaticaObtenerDetalleCabeceraConfiguracionCuota
                    WHERE IdCabecera = @IdCabecera
                    ORDER BY FechaSesion ASC";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    IdCabecera
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleCabeceraConfiguracionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionAsistencia(int IdCabecera, List<int> IdsSesion)
        {
            try
            {
                List<DetalleCabeceraConfiguracionDTO> rpta = new List<DetalleCabeceraConfiguracionDTO>();
                var query = string.Empty;
                var listaSesion = string.Join(",", IdsSesion);
                query = @"
                   SELECT 
                        Id,
	                    IdSesion,
	                    IdCabecera,
	                    Alumno,
	                    AsistenteAcademico,
	                    FechaSesion,
	                    EstadoLLamada
                    FROM  ope.V_LlamadaAutomaticaObtenerDetalleCabeceraConfiguracionAsistencia
                    WHERE IdCabecera = @IdCabecera and IdSesion in (SELECT Item FROM conf.F_Splitstring(@listaSesion, ','))
                    ORDER BY FechaSesion ASC";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    IdCabecera,
                    listaSesion
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleCabeceraConfiguracionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO(int IdCabecera )
        {
            try
            {
                List<DetalleCabeceraConfiguracionDTO> rpta = new List<DetalleCabeceraConfiguracionDTO>();
                var query = string.Empty; 
                query = @"
                   SELECT 
                        Id,
	                    IdSesion,
	                    IdCabecera,
	                    Alumno,
	                    AsistenteAcademico,
	                    FechaSesion,
	                    EstadoLLamada
                    FROM  ope.V_LlamadaAutomaticaObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO
                    WHERE IdCabecera = @IdCabecera 
                    ORDER BY FechaSesion ASC";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    IdCabecera
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleCabeceraConfiguracionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<CabecerasEnProcesoDTO> ObtenerCabecerasEnProceso()
        {
            try
            {
                List<CabecerasEnProcesoDTO> rpta = new List<CabecerasEnProcesoDTO>();
                var query = string.Empty;

                query = @"
                 SELECT 
	                Id,
	                IdIvrTipoConfiguracion,
	                CongelamientoConfiguracion
                FROM [ope].[T_CabeceraConfiguracionLlamadaAutomatica]
                WHERE Estado=1 AND EstadoProceso ='EN PROCESO'";
                var resultado = _dapperRepository.QueryDapper(query,null);
                if (!string.IsNullOrEmpty(resultado) && resultado.Length >= 4)
                {
                    rpta = JsonConvert.DeserializeObject<List<CabecerasEnProcesoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<CabecerasEnProcesoDTO> ObtenerCabecerasSinPendientesALlamar(int IdIvr)
        {
            try
            {
                List<CabecerasEnProcesoDTO> rpta = new List<CabecerasEnProcesoDTO>();
                var query = string.Empty;

                query = @"
                    SELECT 
	                    Id,
	                    IdIvrEjecucion,
	                    IdIvrTipoConfiguracion,
	                    CongelamientoConfiguracion
                    FROM ope.V_LLamadaAutomaticaObtenerPendientesALlamarPorCabeceraConfiguracion
                    WHERE Pendiente=0 AND IdIvrEjecucion = @IdIvr";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    IdIvr
                });
                if (!string.IsNullOrEmpty(resultado) && resultado.Length >= 4)
                {
                    rpta = JsonConvert.DeserializeObject<List<CabecerasEnProcesoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<CabecerasEnProcesoDTO> ObtenerCabecerasEnProcesoPorIdIvrEjecucion(int IvrEjecucion)
        {
            List<CabecerasEnProcesoDTO> rpta = new List<CabecerasEnProcesoDTO>();
            try
            {
                
                var query = string.Empty;

                query = @"
                SELECT 
	                Id,
	                IdIvrTipoConfiguracion,
	                CongelamientoConfiguracion
                FROM [ope].[T_CabeceraConfiguracionLlamadaAutomatica]
                WHERE IdIvrEjecucion=@IvrEjecucion ND Estado=1 AND EstadoProceso ='EN PROCESO'";
                var resultado = _dapperRepository.QueryDapper(query, new { IvrEjecucion });
                if (!string.IsNullOrEmpty(resultado) && resultado.Length >= 4)
                {
                    rpta = JsonConvert.DeserializeObject<List<CabecerasEnProcesoDTO>>(resultado);
                }
            }
            catch (Exception ex)
            {  }
            return rpta;
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public bool ActualizarLlamadaHoy(string IdsDetalle)
        {
            try
            {
                var queryResult = _dapperRepository.QuerySPDapper("ope.SP_LlamadaAutomaticaActualizarLlamadaHoy",
                    new
                    {
                        IdsDetalle = IdsDetalle
                    });
                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro para llamada.
        /// </summary>
        /// <returns> List<RecordatorioClasesOnlineIvrDTO> </returns>
        public DatoLlamadaDTO ObtenerDatoLlamada(int IdIvrEjecucion)
        {
            DatoLlamadaDTO item = new DatoLlamadaDTO();
            try
            {

                var query = _dapperRepository.QuerySPFirstOrDefault("ope.SP_LlamadaAutomaticaObtenerDatoLlamada", new { IdIvrEjecucion = IdIvrEjecucion });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    item = JsonConvert.DeserializeObject<DatoLlamadaDTO>(query);
                    item.EjecutarIvr = true;
                }
                else
                {
                    var queryActualizado = _dapperRepository.QuerySPFirstOrDefault("ope.SP_LlamadaAutomaticaObtenerDatoLlamada", new { IdIvrEjecucion = IdIvrEjecucion });
                    if (!string.IsNullOrEmpty(queryActualizado) && !query.Contains("[]") && queryActualizado != "null")
                    {
                        item = JsonConvert.DeserializeObject<DatoLlamadaDTO>(queryActualizado);
                        item.EjecutarIvr = true;
                    }
                }
                return item;
            }
            catch (Exception Ex)
            {
                return item;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public DetalleIvrDTO ObtenerDetalleParaIvr(string CelularAlumno)
        {
            DetalleIvrDTO item = new DetalleIvrDTO();
            try
            {
                var queryResult = _dapperRepository.QuerySPFirstOrDefault("ope.SP_LlamadaAutomaticaObtenerDetalleParaIvr",  new {  CelularAlumno = CelularAlumno });

                if (!string.IsNullOrEmpty(queryResult) && !queryResult.Contains("[]") && queryResult != "null")
                {
                    item = JsonConvert.DeserializeObject<DetalleIvrDTO>(queryResult);
                    item.EjecutarIvr = true;
                }
                return item;
            }
            catch (Exception Ex)
            {
                return item;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public bool EliminarRegistrosDetalle(string  UsuarioModificacion, int IdCabeceraConfiguracion )
        {
            try
            {
                var queryResult = _dapperRepository.QuerySPDapper("[ope].[SP_TLlamadaAutomaticaDetalleCabeceraConfiguracion_Actualizar]",
                    new
                    {
                        UsuarioModificacion,
                        IdCabeceraConfiguracion
                    });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public bool ActualizarProcesoCompletadoCabeceraConfiguracion(string IdsCabeceraConfiguracion)
        {
            try
            {
                var queryResult = _dapperRepository.QuerySPDapper("[ope].[SP_TCabeceraConfiguracionLLamadaAutomaticaProcesoCompletado_Actualizar]",
                    new
                    {
                        IdsCabeceraConfiguracion
                    });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
