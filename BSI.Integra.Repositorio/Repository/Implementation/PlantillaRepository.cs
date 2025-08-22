using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Dapper;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Plantilla
    /// </summary>
    public class PlantillaRepository : GenericRepository<TPlantilla>, IPlantillaRepository
    {
        private Mapper _mapper;

        public PlantillaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantilla, Plantilla>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPlantillaClaveValor, PlantillaClaveValor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFaseByPlantilla, FaseByPlantilla>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPlantillaAsociacionModuloSistema, PlantillaAsociacionModuloSistema>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPlantilla MapeoEntidad(Plantilla entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantilla modelo = _mapper.Map<TPlantilla>(entidad);

                if (entidad.PlantillaClaveValor != null && entidad.PlantillaClaveValor.Count > 0)
                {
                    modelo.TPlantillaClaveValors = _mapper.Map<List<TPlantillaClaveValor>>(entidad.PlantillaClaveValor);
                }

                if (entidad.FaseByPlantilla != null && entidad.FaseByPlantilla.Count > 0)
                {
                    modelo.TFaseByPlantillas = (_mapper.Map<List<TFaseByPlantilla>>(entidad.FaseByPlantilla));
                }

                if (entidad.PlantillaAsociacionModuloSistema != null && entidad.PlantillaAsociacionModuloSistema.Count > 0)
                {
                    modelo.TPlantillaAsociacionModuloSistemas = (_mapper.Map<List<TPlantillaAsociacionModuloSistema>>(entidad.PlantillaAsociacionModuloSistema));

                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantilla Add(Plantilla entidad)
        {
            try
            {
                var Plantilla = MapeoEntidad(entidad);
                base.Insert(Plantilla);
                return Plantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantilla Update(Plantilla entidad)
        {
            try
            {
                var Plantilla = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Plantilla.RowVersion = entidadExistente.RowVersion;

                base.Update(Plantilla);
                return Plantilla;
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


        public IEnumerable<TPlantilla> Add(IEnumerable<Plantilla> listadoEntidad)
        {
            try
            {
                List<TPlantilla> listado = new List<TPlantilla>();
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

        public IEnumerable<TPlantilla> Update(IEnumerable<Plantilla> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantilla> listado = new List<TPlantilla>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Plantilla para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                string query = "SELECT Id, Nombre FROM mkt.T_Plantilla WHERE Estado=1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltro(): {ex.Message}");
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Plantilla
        /// </summary>
        /// <returns> List<PlantillaDTO> </returns>
        public IEnumerable<Plantilla> ObtenerPlantilla()
        {
            try
            {
                IEnumerable<Plantilla> rpta = new List<Plantilla>();
                var query = @"SELECT Id, Nombre, Descripcion, IdPlantillaBase, EstadoAgenda, Documento, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, IdPersonalAreaTrabajo
                            FROM mkt.T_Plantilla
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<Plantilla>>(resultado)!;
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
        /// Obtiene el registro de T_Plantilla segun su Id
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> Plantilla </returns>
        public Plantilla? ObtenerPorId(int idPlantilla)
        {
            try
            {
                var query = @"SELECT Id,
                                       Nombre,
                                       Descripcion,
                                       IdPlantillaBase,
                                       EstadoAgenda,
                                       Documento,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       IdPersonalAreaTrabajo,
                                       EstadoPlantillaIntegra
                            FROM mkt.T_Plantilla
                            WHERE Estado=1 AND Id = @idPlantilla";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPlantilla });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Plantilla>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamamni Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Plantilla segun su Id
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public async Task<Plantilla> ObtenerPorIdAsync(int idPlantilla)
        {
            try
            {
                Plantilla rpta = new Plantilla();
                var query = @"SELECT Id,
                                       Nombre,
                                       Descripcion,
                                       IdPlantillaBase,
                                       EstadoAgenda,
                                       Documento,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       IdPersonalAreaTrabajo,
                                       EstadoPlantillaIntegra
                            FROM mkt.T_Plantilla
                            WHERE Estado=1 AND Id = @idPlantilla";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idPlantilla });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Plantilla>(resultado);
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
        /// Obtiene el registro de T_Plantilla segun su Id
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public PlantillaAsuntoCuerpoDTO ObtenerPlantillaCorreo(int idPlantilla)
        {
            try
            {
                var listaPlantillaClaveValor = new List<PlantillaClaveValor>();
                var query = $@"
                        SELECT Id, 
                                Clave, 
                                Valor, 
                                IdPlantilla
                        FROM mkt.T_PlantillaClaveValor
                        WHERE IdPlantilla = @idPlantilla
                                AND Estado = 1";

                var plantillaDB = _dapperRepository.QueryDapper(query, new { idPlantilla });
                if (!string.IsNullOrEmpty(plantillaDB) && !plantillaDB.Contains("[]"))
                {
                    listaPlantillaClaveValor = JsonConvert.DeserializeObject<List<PlantillaClaveValor>>(plantillaDB);
                }

                var asunto = listaPlantillaClaveValor.Where(x => x.Clave == "Asunto").FirstOrDefault();
                string asuntoValor = "";
                if (asunto != null) asuntoValor = asunto.Valor;

                var cuerpo = listaPlantillaClaveValor.Where(x => x.Clave == "Texto").FirstOrDefault();
                string cuerpoValor = "";
                if (cuerpo != null) cuerpoValor = cuerpo.Valor;

                return new PlantillaAsuntoCuerpoDTO()
                {
                    Asunto = asuntoValor,
                    Cuerpo = cuerpoValor
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Plantilla segun su Id
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public async Task<PlantillaAsuntoCuerpoDTO> ObtenerPlantillaCorreoAsync(int idPlantilla)
        {
            try
            {
                var listaPlantillaClaveValor = new List<PlantillaClaveValorDTO>();
                var query = $@"
                    SELECT Id, 
                            Clave, 
                            Valor, 
                            IdPlantilla
                    FROM mkt.T_PlantillaClaveValor
                    WHERE IdPlantilla = @idPlantilla
                            AND Estado = 1";

                var plantillaDB = await _dapperRepository.QueryDapperAsync(query, new { idPlantilla });
                if (!string.IsNullOrEmpty(plantillaDB) && !plantillaDB.Contains("[]"))
                {
                    listaPlantillaClaveValor = JsonConvert.DeserializeObject<List<PlantillaClaveValorDTO>>(plantillaDB);
                }

                var asunto = listaPlantillaClaveValor.Where(x => x.Clave == "Asunto").FirstOrDefault();
                var asuntoValor = "";
                if (asunto != null) asuntoValor = asunto.Valor;

                var cuerpo = listaPlantillaClaveValor.Where(x => x.Clave == "Texto").FirstOrDefault();
                var cuerpoValor = "";
                if (cuerpo != null) cuerpoValor = cuerpo.Valor;

                return new PlantillaAsuntoCuerpoDTO()
                {
                    Asunto = asuntoValor,
                    Cuerpo = cuerpoValor
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene plantillas de mensajes para chat por idPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public IEnumerable<ComboFiltroDTO> ObtenerPlantillaChatIntegraSoporte(int idPlantilla)
        {
            try
            {
                List<ComboFiltroDTO> respuesta = new List<ComboFiltroDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_Plantilla WHERE IdPlantillaBase = 1 AND Estado = 1 ORDER BY Id ASC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPlantilla });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreB"></param>
        /// <param name="nombreP"></param>
        /// <returns></returns>
        public PlantillaDTO? ObtenerPorNombre(string nombreB, string nombreP)
        {
            try
            {
                var query = @"SELECT Id, Nombre, Descripcion, IdPlantillaBase, EstadoAgenda, Documento, Estado, UsuarioCreacion, UsuarioModificacion, 
                              FechaCreacion, FechaModificacion, IdPersonalAreaTrabajo FROM mkt.T_Plantilla
                              WHERE Estado=1 AND Nombre LIKE '%'+@nombreB+'%' AND Nombre LIKE '%'+@nombreP+'%'";
                var resultado = _dapperRepository.FirstOrDefault(query, new { nombreB, nombreP });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PlantillaDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Plantillas de WhatsApp para Operaciones
        /// </summary>
        /// <returns> List<PlantillaDTO> </returns>      
        public List<PlantillaTipoEnvioDTO> ObtenerListaPlantillasConfiguracionEnvio()
        {
            try
            {
                string query = @"SELECT Id, Nombre, IdTipoEnvio FROM mkt.V_TPlantilla_Nombre_ConfiguracionEnvio WHERE Estado = 1 ORDER BY IdPlantillaBase ";
                var responseQuery = _dapperRepository.QueryDapper(query, null);
                List<PlantillaTipoEnvioDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaTipoEnvioDTO>>(responseQuery)!;
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una el registro de Plantilla 
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public DatosPlantillaDTO ObtenerPlantillaPorId(int idPlantilla)
        {
            try
            {
                DatosPlantillaDTO plantilla = new DatosPlantillaDTO();
                var query = string.Empty;
                query = @"
                        SELECT 
                            Descripcion 
                        FROM 
                            mkt.T_Plantilla 
                        WHERE 
                            Estado = 1 AND Id = @IdPlantilla";
                var plantillaDB = _dapperRepository.FirstOrDefault(query, new { IdPlantilla = idPlantilla });
                if (!string.IsNullOrEmpty(plantillaDB) && !plantillaDB.Contains("[]") && plantillaDB != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<DatosPlantillaDTO>(plantillaDB)!;
                }
                return plantilla;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Plantillas que contienen Bienvenidad Y Prescencial
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public Plantilla ObtenerPlantillaBienvenidaPresencial()
        {
            try
            {
                Plantilla plantilla = new Plantilla();
                var query = string.Empty;
                query = @"
                        SELECT 
                            Id,
							Nombre,
							Descripcion,
							IdPlantillaBase,
							EstadoAgenda,
							Documento,
							Estado,
							UsuarioCreacion,
							UsuarioModificacion,
							FechaCreacion,
							FechaModificacion,
							RowVersion,
							IdMigracion,
							IdPersonalAreaTrabajo,
							EstadoPlantillaIntegra 
                        FROM mkt.T_Plantilla 
                        WHERE Estado = 1 AND Nombre LIKE '%Bienvenida%' AND Nombre LIKE '%Presencial%'";
                var plantillaDB = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(plantillaDB) && plantillaDB != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<Plantilla>(plantillaDB)!;
                }
                return plantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de las plantillas para la grilla 
        /// </summary>
        /// <returns> DTO: List<PlantillaDatoDTO> </returns>
        public List<PlantillaDatoDTO> ObtenerListarPlantilla()
        {
            try
            {
                var listaPlantilla = new List<PlantillaDatoDTO>();

                var _query = @"
                    SELECT Id, 
                           Nombre, 
                           Descripcion, 
                           IdPlantillaBase, 
                           NombrePlantillaBase, 
                           EstadoAgenda, 
                           Documento, 
                           IdPersonalAreaTrabajo, 
                           NombrePersonalAreaTrabajo,
                           Estado,
                           EstadoPlantilla
                    FROM mkt.V_ObtenerPlantilla
                    WHERE Estado = 1
                          AND EstadoPlantillaBase = 1
                          AND EstadoPersonalAreaTrabajo = 1
                    ORDER BY FechaCreacionPlantilla DESC
                    ";
                var query = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaPlantilla = JsonConvert.DeserializeObject<List<PlantillaDatoDTO>>(query);
                }
                return listaPlantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la asociacion de las platillas
        /// </summary>
        /// <returns> DTO: List<PlantillaAsociacionModuloSistemaDTO> </returns>

        public List<PlantillaAsociacionModuloSistemaDTO> ObtenerPorPlantlla(List<int> listaIdPlantilla)
        {
            try
            {
                List<PlantillaAsociacionModuloSistemaDTO> rpta = new List<PlantillaAsociacionModuloSistemaDTO>();
                var query = @"SELECT Id, 
                       IdPlantilla, 
                       IdModuloSistema,
                       NombreModulo
                FROM mkt.V_ObtenerPlantillaAsociacionModuloSistema
                WHERE EstadoPlantillaAsociacionModuloSistema = 1
                      AND EstadoPlantilla = 1
                      AND EstadoModuloSistema = 1
                      AND IdPlantilla IN (" + string.Join(",", listaIdPlantilla) + ")";

                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaAsociacionModuloSistemaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene plantilla clave valor
        /// </summary>
        /// <param name="idPlantilla">id Plantilla/param>
        /// <returns> DTO: PlantillaValorDetalleDTO </returns>
        public PlantillaValorDetalleDTO ObtenerPlantillaClaveValor(int idPlantilla)
        {
            try
            {
                PlantillaValorDetalleDTO plantilla = new PlantillaValorDetalleDTO();
                var query = string.Empty;
                query = @"
                        SELECT 
                            *
                        FROM 
                           mkt.V_ObtenerDetallePlantillaIntegra
                        WHERE 
                             IdPlantilla = @IdPlantilla";
                var plantillaDB = _dapperRepository.FirstOrDefault(query, new { IdPlantilla = idPlantilla });
                if (!string.IsNullOrEmpty(plantillaDB) && !plantillaDB.Contains("[]") && plantillaDB != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<PlantillaValorDetalleDTO>(plantillaDB)!;
                }
                return plantilla;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas speech donde la plantilla base sea 1
        /// </summary>
        /// <returns> DTO: List<ComboDTO> </returns>

        public List<ComboDTO> ObtenerPlantillasSpeech()
        {
            try
            {
                List<ComboDTO> plantilla = new List<ComboDTO>();
                var query = string.Empty;
                query = @"SELECT Id, Nombre FROM mkt.T_Plantilla WHERE Estado = 1 AND IdPlantillaBase=1";
                var plantillaDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(plantillaDB) && !plantillaDB.Contains("[]"))
                {
                    plantilla = JsonConvert.DeserializeObject<List<ComboDTO>>(plantillaDB)!;
                }
                return plantilla;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas speech donde el speech sea despedida
        /// </summary>
        /// <returns> DTO: List<ComboDTO> </returns>

        public List<ComboDTO> ObtenerAllPlantillaSpeechDespedida()
        {
            try
            {
                List<ComboDTO> Plantilla = new List<ComboDTO>();
                var _query = string.Empty;
                _query = "select P.Id, P.Nombre from mkt.T_Plantilla P  " +
                    "left join pla.T_PlantillaBase PB on PB.Id=P.IdPlantillaBase  " +
                    "where PB.Nombre like 'speech despedida'";
                var PlantillaDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(PlantillaDB) && !PlantillaDB.Contains("[]"))
                {
                    Plantilla = JsonConvert.DeserializeObject<List<ComboDTO>>(PlantillaDB);
                }
                return Plantilla;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor:Joseph LLanque
        /// Fecha: 24/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Plantillas de certificados
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public List<PlantilaCertificadoConstanciaDTO> ObtenerListaPlantillaCertificadoOperaciones()
        {
            try
            {
                List<PlantilaCertificadoConstanciaDTO> plantilla = new List<PlantilaCertificadoConstanciaDTO>();
                var query = string.Empty;
                query = @"
                        SELECT 
                        Id,
                        IdPlantillaBase,
                        Nombre 
                        FROM mkt.V_TPlantilla_Certificado WHERE Estado = 1";
                var plantillaDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(plantillaDB) && plantillaDB != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<List<PlantilaCertificadoConstanciaDTO>>(plantillaDB)!;
                }
                return plantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas para correo
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public IEnumerable<PlantillaTipoEnvioDTO> ObtenerPlantillaNombreCorreoOperaciones()
        {
            try
            {
                var query = @"SELECT Id, Nombre, 3 AS IdTipoEnvio FROM mkt.V_TPlantilla_Nombre_CorreoOperaciones WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PlantillaTipoEnvioDTO>>(resultado)!;
                return new List<PlantillaTipoEnvioDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPlantillaNombreCorreoOperaciones(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas para correo
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public async Task<IEnumerable<PlantillaTipoEnvioDTO>> ObtenerPlantillaNombreCorreoOperacionesAsync()
        {
            try
            {
                var query = @"SELECT Id, Nombre, 3 AS IdTipoEnvio FROM mkt.V_TPlantilla_Nombre_CorreoOperaciones WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PlantillaTipoEnvioDTO>>(resultado)!;
                return new List<PlantillaTipoEnvioDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPlantillaNombreCorreoOperaciones(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas para Whatsaap
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public IEnumerable<PlantillaTipoEnvioDTO> ObtenerPlantillaNombreWhatsAppOperaciones()
        {
            try
            {
                var query = @"SELECT Id, Nombre, 1 AS IdTipoEnvio FROM mkt.V_TPlantilla_Nombre_WhatsAppOperaciones WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PlantillaTipoEnvioDTO>>(resultado)!;
                return new List<PlantillaTipoEnvioDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPlantillaNombreWhatsAppOperaciones(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas para Whatsaap
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public async Task<IEnumerable<PlantillaTipoEnvioDTO>> ObtenerPlantillaNombreWhatsAppOperacionesAsync()
        {
            try
            {
                var query = @"SELECT Id, Nombre, 1 AS IdTipoEnvio FROM mkt.V_TPlantilla_Nombre_WhatsAppOperaciones WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PlantillaTipoEnvioDTO>>(resultado)!;
                return new List<PlantillaTipoEnvioDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPlantillaNombreWhatsAppOperaciones(): {ex.Message}");
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 08/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista con los registros de la tabla mkt.T_Plantilla
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PlantilaCertificadoConstanciaDTO>> ObtenerPlantillaCertificadoAsync()
        {
            try
            {
                string query = "select Id,IdPlantillaBase,Nombre from mkt.V_TPlantilla_Certificado where Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PlantilaCertificadoConstanciaDTO>>(resultado);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FiltroPlantillaTipadaDTO ObtenerFiltros()
        {
            try
            {
                FiltroPlantillaTipadaDTO data = new();
                List<FiltroPlantillasDTO> dataEmail = new();
                List<FiltroPlantillasDTO> dataWhatssap = new();

                var queryEmail = string.Empty;
                var queryWhatssap = string.Empty;

                queryEmail = @"
                        SELECT Plantilla.Id AS Id,
                               CONCAT(Plantilla.Nombre, ' - ', PlantillaBase.Nombre) AS Nombre
                        FROM mkt.T_Plantilla AS Plantilla
                            INNER JOIN pla.T_PlantillaBase AS PlantillaBase
                                ON Plantilla.IdPlantillaBase = PlantillaBase.Id
		                        AND PlantillaBase.Id = 2
                        WHERE Plantilla.Estado = 1;";
                queryWhatssap = @"
                      SELECT Plantilla.Id AS Id,
                               CONCAT(Plantilla.Nombre, ' - ', PlantillaBase.Nombre) AS Nombre
                        FROM mkt.T_Plantilla AS Plantilla
                            INNER JOIN pla.T_PlantillaBase AS PlantillaBase
                                ON Plantilla.IdPlantillaBase = PlantillaBase.Id
		                        AND PlantillaBase.Id = 8
                        WHERE Plantilla.Estado = 1;";

                var plantillaDBEmail = _dapperRepository.QueryDapper(queryEmail, null);
                var plantillaDBWhatssap = _dapperRepository.QueryDapper(queryWhatssap, null);

                if (!string.IsNullOrEmpty(plantillaDBEmail) && plantillaDBEmail != "null")
                {
                    dataEmail = JsonConvert.DeserializeObject<List<FiltroPlantillasDTO>>(plantillaDBEmail)!;
                    data.Email = dataEmail;
                }
                if (!string.IsNullOrEmpty(plantillaDBWhatssap) && plantillaDBWhatssap != "null")
                {
                    dataWhatssap = JsonConvert.DeserializeObject<List<FiltroPlantillasDTO>>(plantillaDBWhatssap)!;
                    data.Whatsapp = dataWhatssap;
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor:Adriana Chipana Ampuero
        /// Fecha: 12/01/2024
        /// Version: 1.0
        /// <summary>
        /// Inserta el detallePlantilla
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public List<InsertarDetallePlantillaDTO> InsertarDetallePlantilla(InsertarDetallePlantillaDTO plantillaDetalle)
        {
            try
            {
                List<InsertarDetallePlantillaDTO> plantilla = new List<InsertarDetallePlantillaDTO>();

                var Imagen = plantillaDetalle.Imagen;
                var Boton = plantillaDetalle.Boton;
                var IdPlantilla = plantillaDetalle.IdPlantilla;
                var Estado = plantillaDetalle.Estado;
                var UsuarioCreacion = plantillaDetalle.UsuarioCreacion;
                var UsuarioModificacion = plantillaDetalle.UsuarioModificacion;
                var FechaCreacion = plantillaDetalle.FechaCreacion;
                var FechaModificacion = plantillaDetalle.FechaModificacion;

                var query = string.Empty;
                query = @"exec mkt.SP_InsertarDetallePlantilla @Imagen, @Boton, @IdPlantilla, @Estado, @UsuarioCreacion, @UsuarioModificacion, @FechaCreacion, @FechaModificacion";
                var plantillaDB = _dapperRepository.QueryDapper(query, new { Imagen = Imagen,Boton = Boton, IdPlantilla = IdPlantilla, Estado = Estado, UsuarioCreacion = UsuarioCreacion, UsuarioModificacion = UsuarioModificacion, FechaCreacion = FechaCreacion, FechaModificacion = FechaModificacion });
                if (!string.IsNullOrEmpty(plantillaDB) && plantillaDB != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<List<InsertarDetallePlantillaDTO>>(plantillaDB)!;
                }
                return plantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        /// Autor:Eliot Arias F.
        /// Fecha: 25/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de plantillas Email para filtro GP.
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> ObtenerNombrePlantillaBaseEmail()
        {
            try
            {
                string query = "SELECT * FROM gp.V_TPlantilla_PlantillaBaseEmail;";
                string queryRespuesta = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<IEnumerable<ComboPlantillaNombrePlantillaBaseDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor:Eliot Arias F.
        /// Fecha: 25/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de plantillas Watsapp para filtro GP.
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> ObtenerNombrePlantillaBaseWatsApp()
        {
            try
            {
                string query = "SELECT * FROM gp.V_TPlantilla_PlantillaBaseWhatsApp;";
                string queryRespuesta = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<IEnumerable<ComboPlantillaNombrePlantillaBaseDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }








    }
}
