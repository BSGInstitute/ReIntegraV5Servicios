using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: RecordatorioClasesOnlineIvrRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_RecordatorioClasesOnlineIvr
    /// </summary>
    public class RecordatorioClasesOnlineIvrRepository : GenericRepository<TRecordatorioClasesOnlineIvr>, IRecordatorioClasesOnlineIvrRepository
    {
        private Mapper _mapper;

        public RecordatorioClasesOnlineIvrRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRecordatorioClasesOnlineIvr, RecordatorioClasesOnlineIvr>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TRecordatorioClasesOnlineIvr MapeoEntidad(RecordatorioClasesOnlineIvr entidad)
        {
            try
            {
                //crea la entidad padre
                TRecordatorioClasesOnlineIvr modelo = _mapper.Map<TRecordatorioClasesOnlineIvr>(entidad);

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

        public TRecordatorioClasesOnlineIvr Add(RecordatorioClasesOnlineIvr entidad)
        {
            try
            {
                var RecordatorioClasesOnlineIvr = MapeoEntidad(entidad);
                base.Insert(RecordatorioClasesOnlineIvr);
                return RecordatorioClasesOnlineIvr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRecordatorioClasesOnlineIvr Update(RecordatorioClasesOnlineIvr entidad)
        {
            try
            {
                var RecordatorioClasesOnlineIvr = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RecordatorioClasesOnlineIvr.RowVersion = entidadExistente.RowVersion;

                base.Update(RecordatorioClasesOnlineIvr);
                return RecordatorioClasesOnlineIvr;
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


        public IEnumerable<TRecordatorioClasesOnlineIvr> Add(IEnumerable<RecordatorioClasesOnlineIvr> listadoEntidad)
        {
            try
            {
                List<TRecordatorioClasesOnlineIvr> listado = new List<TRecordatorioClasesOnlineIvr>();
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

        public IEnumerable<TRecordatorioClasesOnlineIvr> Update(IEnumerable<RecordatorioClasesOnlineIvr> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRecordatorioClasesOnlineIvr> listado = new List<TRecordatorioClasesOnlineIvr>();
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
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_RecordatorioClasesOnlineIvr de Id enviado.
        /// </summary>
        /// <returns> RecordatorioClasesOnlineIvr </returns>
        public RecordatorioClasesOnlineIvr ObtenerRecordatorioClasesOnlineIvrById(int Id)
        {
            RecordatorioClasesOnlineIvr rpta = new RecordatorioClasesOnlineIvr();
            try
            {
                
                var query = @"
                   SELECT [Id]
                          ,[IdMatriculaCabecera]
                          ,[IntentoMaximo]
                          ,[Intento]
                          ,[Concluido]
                          ,[Ejecutado]
                          ,[UsuarioCreacion]
                          ,[UsuarioModificacion]
                          ,[Estado]
                          ,[FechaCreacion]
                          ,[FechaModificacion]
                          ,[RowVersion]
                          ,[IdMigracion]
                    FROM [ope].[T_RecordatorioClasesOnlineIvr]
                    WHERE Estado=1 and Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<RecordatorioClasesOnlineIvr>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message); 
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_RecordatorioClasesOnlineIvr de Id enviado.
        /// </summary>
        /// <returns> RecordatorioClasesOnlineIvr </returns>
        public DatoLLamadaRecordatorioClasesOnlineDTO ObtenerDatoLlamadaRecordatorioClasesOnlineById(int Id)
        {
            DatoLLamadaRecordatorioClasesOnlineDTO rpta = new DatoLLamadaRecordatorioClasesOnlineDTO();
            try
            {

                var query = @"
                  SELECT TOP 1
		                 Id
		                ,IdMatriculaCabecera
		                ,FechaSesion
		                ,NombrePrograma
		                ,Alumno
		                ,CelularAlumno
		                ,AsistenteAcademico 
		                ,Anexo
		                ,IdSexo
		                ,IntentoMaximo
		                ,Intento
		                ,Concluido
		                ,Ejecutado
                        ,Pais
                        ,ZonaHoraria
                        ,IdSesion
	                FROM ope.V_ObtenerDatosRecordatorioClasesOnlineIvr
                    WHERE Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<DatoLLamadaRecordatorioClasesOnlineDTO>(resultado);
                    var DireferenciaHoraria = this.ObtenerHorarioDiferencia(rpta.IdSesion, rpta.ZonaHoraria.Value, rpta.Pais);
                    rpta.FechaSesion = rpta.FechaSesion.AddHours(DireferenciaHoraria);
                    rpta.HoraSesion = rpta.FechaSesion.ToString("h:mmtt", System.Globalization.CultureInfo.InvariantCulture);
                    rpta.EjecutarIvr = true;
                }
            }
            catch (Exception ex)  { }
            return rpta;
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro para llamada.
        /// </summary>
        /// <returns> List<RecordatorioClasesOnlineIvrDTO> </returns>
        public DatoLLamadaRecordatorioClasesOnlineDTO ObtenerDatoLlamadaRecordatorioClasesOnline()
        {
            DatoLLamadaRecordatorioClasesOnlineDTO item = new DatoLLamadaRecordatorioClasesOnlineDTO();
            try
            {
                
                var query = _dapperRepository.QuerySPFirstOrDefault("ope.SP_ObtenerDatoLlamadaRecordatorioClasesOnline",null);
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    item = JsonConvert.DeserializeObject<DatoLLamadaRecordatorioClasesOnlineDTO>(query);
                    item.EjecutarIvr = true;
                }
                else
                {
                    var queryActualizado = _dapperRepository.QuerySPFirstOrDefault("ope.SP_ObtenerDatoLlamadaRecordatorioClasesOnline", null);
                    if (!string.IsNullOrEmpty(queryActualizado) && !query.Contains("[]") && queryActualizado != "null")
                    {
                        item = JsonConvert.DeserializeObject<DatoLLamadaRecordatorioClasesOnlineDTO>(queryActualizado);
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


        public int ObtenerHorarioDiferencia(int IdPespecifico, int ZonaHoraria, string NombrePais)
        {
            try
            {

                CiudadSesionDTO datosSesion = new CiudadSesionDTO();
                var nombreciudadDB = _dapperRepository.QuerySPFirstOrDefault("[ope].[SP_CalculoPaisPespecifico]", new { IdPespecificoSesion = IdPespecifico });
                if (!string.IsNullOrEmpty(nombreciudadDB) && !nombreciudadDB.Contains("[]") && nombreciudadDB != "null")
                {
                    datosSesion = JsonConvert.DeserializeObject<CiudadSesionDTO>(nombreciudadDB);
                    if (ZonaHoraria == 0 || datosSesion.ZonaHoraria == null)
                    {
                        return 0;
                    }
                    if (datosSesion.NombrePais == NombrePais)
                    {
                        return 0;
                    }
                    else if (datosSesion.NombrePais == "Perú")
                    {
                        return ZonaHoraria;
                    }
                    else if (datosSesion.NombrePais == "Mexico")
                    {
                        switch (NombrePais)
                        {
                            case "Sin Pais":
                                return 0;
                                break;
                            case "Internacional":
                                return 0;
                                break;
                            case "Canada":
                                return 1;
                                break;
                            case "Estados Unidos":
                                return 1;
                                break;
                            case "Holanda":
                                return 7;
                                break;
                            case "España":
                                return 7;
                                break;
                            case "Alemania":
                                return 7;
                                break;
                            case "Perú":
                                return 1;
                                break;
                            case "Cuba":
                                return 1;
                                break;
                            case "Argentina":
                                return 3;
                                break;
                            case "Brazil":
                                return 3;
                                break;
                            case "Chile":
                                return 3;
                                break;
                            case "Colombia":
                                return 1;
                                break;
                            case "Venezuela":
                                return 2;
                                break;
                            case "Guatemala":
                                return 0;
                                break;
                            case "El Salvador":
                                return 0;
                                break;
                            case "Honduras":
                                return 0;
                                break;
                            case "Nicaragua":
                                return 0;
                                break;
                            case "Costa Rica":
                                return 0;
                                break;
                            case "Panama":
                                return 1;
                                break;
                            case "Bolivia":
                                return 2;
                                break;
                            case "Ecuador":
                                return 1;
                                break;
                            case "Paraguay":
                                return 3;
                                break;
                            case "Uruguay":
                                return 3;
                                break;
                            case "Republica Dominicana":
                                return 2;
                                break;
                            default:
                                return 0;
                                break;
                        }
                    }
                    else if (datosSesion.NombrePais == "Chile")
                    {
                        switch (NombrePais)
                        {
                            case "Sin Pais":
                                return 0;
                                break;
                            case "Internacional":
                                return 0;
                                break;
                            case "Canada":
                                return -2;
                                break;
                            case "Estados Unidos":
                                return -2;
                                break;
                            case "Holanda":
                                return 4;
                                break;
                            case "España":
                                return 4;
                                break;
                            case "Alemania":
                                return 4;
                                break;
                            case "Perú":
                                return -2;
                                break;
                            case "Mexico":
                                return -3;
                                break;
                            case "Cuba":
                                return -2;
                                break;
                            case "Argentina":
                                return 0;
                                break;
                            case "Brazil":
                                return 0;
                                break;
                            case "Colombia":
                                return 2;
                                break;
                            case "Venezuela":
                                return -1;
                                break;
                            case "Guatemala":
                                return -3;
                                break;
                            case "El Salvador":
                                return -3;
                                break;
                            case "Honduras":
                                return -3;
                                break;
                            case "Nicaragua":
                                return -3;
                                break;
                            case "Costa Rica":
                                return -3;
                                break;
                            case "Panama":
                                return -2;
                                break;
                            case "Bolivia":
                                return -1;
                                break;
                            case "Ecuador":
                                return -2;
                                break;
                            case "Paraguay":
                                return 0;
                                break;
                            case "Uruguay":
                                return 0;
                                break;
                            case "Republica Dominicana":
                                return -1;
                                break;
                            default:
                                return 0;
                                break;
                        }
                    }
                    else if (datosSesion.NombrePais == "Colombia")
                    {
                        switch (NombrePais)
                        {
                            case "Sin Pais":
                                return 0;
                                break;
                            case "Internacional":
                                return 0;
                                break;
                            case "Canada":
                                return 0;
                                break;
                            case "Estados Unidos":
                                return 0;
                                break;
                            case "Holanda":
                                return 6;
                                break;
                            case "España":
                                return 6;
                                break;
                            case "Alemania":
                                return 6;
                                break;
                            case "Perú":
                                return 0;
                                break;
                            case "Mexico":
                                return -1;
                                break;
                            case "Cuba":
                                return 0;
                                break;
                            case "Argentina":
                                return 2;
                                break;
                            case "Brazil":
                                return 2;
                                break;
                            case "Chile":
                                return 2;
                                break;
                            case "Venezuela":
                                return 1;
                                break;
                            case "Guatemala":
                                return -1;
                                break;
                            case "El Salvador":
                                return -1;
                                break;
                            case "Honduras":
                                return -1;
                                break;
                            case "Nicaragua":
                                return -1;
                                break;
                            case "Costa Rica":
                                return -1;
                                break;
                            case "Panama":
                                return 0;
                                break;
                            case "Bolivia":
                                return 1;
                                break;
                            case "Ecuador":
                                return 0;
                                break;
                            case "Paraguay":
                                return 2;
                                break;
                            case "Uruguay":
                                return 2;
                                break;
                            case "Republica Dominicana":
                                return 1;
                                break;
                            default:
                                return 0;
                                break;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

    }
}
