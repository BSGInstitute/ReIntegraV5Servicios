using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoAgendaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 03/08/2022
    /// <summary>
    /// Gestión general de T_DocumentoAgenda
    /// </summary>
    public class DocumentoAgendaRepository : GenericRepository<TDocumentoAgendum>, IDocumentoAgendaRepository
    {
        private Mapper _mapper;

        public DocumentoAgendaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoAgendum, DocumentoAgenda>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentoAgendum MapeoEntidad(DocumentoAgenda entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoAgendum modelo = _mapper.Map<TDocumentoAgendum>(entidad);

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

        public TDocumentoAgendum Add(DocumentoAgenda entidad)
        {
            try
            {
                var DocumentoAgenda = MapeoEntidad(entidad);
                base.Insert(DocumentoAgenda);
                return DocumentoAgenda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoAgendum Update(DocumentoAgenda entidad)
        {
            try
            {
                var DocumentoAgenda = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoAgenda.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoAgenda);
                return DocumentoAgenda;
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


        public IEnumerable<TDocumentoAgendum> Add(IEnumerable<DocumentoAgenda> listadoEntidad)
        {
            try
            {
                List<TDocumentoAgendum> listado = new List<TDocumentoAgendum>();
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

        public IEnumerable<TDocumentoAgendum> Update(IEnumerable<DocumentoAgenda> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoAgendum> listado = new List<TDocumentoAgendum>();
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
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoAgenda.
        /// </summary>
        /// <returns> List<DocumentoAgendaDTO> </returns>
        public IEnumerable<DocumentoAgendaDTO> ObtenerDocumentoAgenda()
        {
            try
            {
                List<DocumentoAgendaDTO> rpta = new List<DocumentoAgendaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Habilitado,
	                    MensajeDetalle,
	                    Generado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_DocumentoAgenda
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoAgendaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoAgenda para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoAgendaComboDTO> </returns>
        public IEnumerable<DocumentoAgendaComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoAgendaComboDTO> rpta = new List<DocumentoAgendaComboDTO>();
                var query = @"SELECT Id,Nombre FROM com.T_DocumentoAgenda WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoAgendaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_DocumentoAgenda obviando los Campos de Auditoria.
        /// </summary>
        /// <returns> List<DocumentoAgendaSinAuditoriaDTO> </returns>
        public IEnumerable<DocumentoAgendaSinAuditoriaDTO> ObtenerDocumentoAgendaSinAuditoria()
        {
            try
            {
                List<DocumentoAgendaSinAuditoriaDTO> rpta = new List<DocumentoAgendaSinAuditoriaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Habilitado,
	                    MensajeDetalle,
	                    Generado
                    FROM com.T_DocumentoAgenda
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoAgendaSinAuditoriaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener id PGeneral por id centro costo
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro de Costo</param>
        /// <returns>int</returns> 
        public async Task<int> ObtenerIdPGeneralPorIdCentroCostoAsync(int idCentroCosto)
        {
            var query = @"SELECT TOP 1 IdProgramaGeneral 
                  FROM pla.T_PEspecifico 
                  WHERE IdCentroCosto = @idCentroCosto";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idCentroCosto });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return 0;

            try
            {
                var lista = JsonConvert.DeserializeObject<List<dynamic>>(resultado);
                if (lista?.Any() == true)
                {
                    var idPGeneral = lista[0].IdProgramaGeneral;
                    return Convert.ToInt32(idPGeneral);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener IdPGeneral: {ex.Message}");
            }

            return 0;
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener resumen programa por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<ResumenPrograma2DTO></returns> 
        public async Task<List<ResumenPrograma2DTO>> ObtenerResumenProgramaPorIdPGeneralAsync(int idPGeneral)
        {
            try
            {
                var query = @"SELECT EsProgramaOCurso
                                FROM pla.V_TPGeneral_TipoPrograma
                                WHERE IdPGeneral = @idPGeneral;";

                var resultadoJson = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

                return JsonConvert.DeserializeObject<List<ResumenPrograma2DTO>>(resultadoJson ?? "[]");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener prerrequisitos por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns> 
        public async Task<string> ObtenerPrerrequisitosPorIdPGeneralAsync(int idPGeneral)
        {
            var query = @"SELECT id, Nombre, Contenido
                            FROM pla.V_TPGeneralDocumentoPW_Prerrequisitos
                            WHERE IdPGeneral = @idPGeneral
                            ORDER BY id DESC;";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener contenido estructura curricular
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<EstructuraCurricularDTO></returns> 
        public async Task<List<EstructuraCurricularDTO>> ObtenerContenidoEstructuraCurricularAsync(int idPGeneral)
        {
            var query = @"
                        SELECT 
                            Titulo,
                            Contenido,
                            IdSeccionTipoDetalle_PW,
                            NumeroFila,
                            OrdenWeb
                        FROM pla.V_ListaSeccionesPorIdPrograma_Documento
                        WHERE Titulo = 'Estructura Curricular'
                            AND IdSeccionTipoDetalle_PW NOT IN (14, 15, 118, 119)
                            AND IdPGeneral = @idPGeneral 
                        ORDER BY NumeroFila ASC";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return new List<EstructuraCurricularDTO>();

            try
            {
                var lista = JsonConvert.DeserializeObject<List<EstructuraCurricularDTO>>(resultado);
                return lista ?? new List<EstructuraCurricularDTO>();
            }
            catch
            {
                return new List<EstructuraCurricularDTO>();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener presentacion por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns> 
        public string ObtenerPresentacionPorIdPGeneral(int idPGeneral)
        {
            var query = @"SELECT dcs.Contenido
                            FROM pla.T_PGeneralDocumento_PW pdc
                            INNER JOIN pla.T_Documento_PW dc ON pdc.IdDocumento = dc.Id
                            INNER JOIN pla.T_DocumentoSeccion_PW dcs ON dcs.IdDocumentoPW = dc.Id
                            WHERE 
                                pdc.IdPGeneral = @idPGeneral
                                AND dcs.Estado = 1
                                AND dc.IdPlantillaPW IN (10, 11, 33, 36)
                                AND (
                                    dcs.Titulo LIKE '%Presentación%'
                                )
                            ORDER BY dcs.OrdenWeb;";

            var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener presentacion por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns> 
        public async Task<string> ObtenerPresentacionPorIdPGeneralAsync(int idPGeneral)
        {
            var query = @"SELECT Id, Contenido, OrdenWeb
                            FROM pla.V_TPGeneralDocumentoPW_Presentacion
                            WHERE rn = 1 AND IdPGeneral = @idPGeneral
                            ORDER BY Id, OrdenWeb;";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener publico objetivo por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns> 
        public string ObtenerPublicoObjetivoPorIdPGeneral(int idPGeneral)
        {
            var query = @"SELECT dcs.Contenido
                            FROM pla.T_PGeneralDocumento_PW pdc
                            INNER JOIN pla.T_Documento_PW dc ON pdc.IdDocumento = dc.Id
                            INNER JOIN pla.T_DocumentoSeccion_PW dcs ON dcs.IdDocumentoPW = dc.Id
                            WHERE 
                                pdc.IdPGeneral = @idPGeneral
                                AND dcs.Estado = 1
                                AND dc.IdPlantillaPW IN (10, 11, 33, 36)
                                AND (
                                    dcs.Titulo LIKE '%Público Objetivo%'
                                )
                            ORDER BY dcs.OrdenWeb;";

            var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener publico objetivo por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns> 
        public async Task<string> ObtenerPublicoObjetivoPorIdPGeneralAsync(int idPGeneral)
        {
            var query = @"SELECT Contenido
                            FROM pla.V_TPGeneralDocumentoPW_PublicoObjetivo
                            WHERE IdPGeneral = @idPGeneral
                            ORDER BY OrdenWeb;";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener duracion horarios por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns> 
        public string ObtenerDuracionHorariosPorIdPGeneral(int idPGeneral)
        {
            var query = @"
                        SELECT dcs.Contenido
                        FROM pla.T_PGeneralDocumento_PW pdc
                        INNER JOIN pla.T_Documento_PW dc ON pdc.IdDocumento = dc.Id
                        INNER JOIN pla.T_DocumentoSeccion_PW dcs ON dcs.IdDocumentoPW = dc.Id
                        WHERE 
                            pdc.IdPGeneral = @idPGeneral
                            AND dcs.Estado = 1
                            AND dc.IdPlantillaPW IN (10, 11, 33, 36)
                            AND (
                                dcs.Titulo LIKE '%Duracion y Horarios%'
                            )
                        ORDER BY dcs.OrdenWeb;";

            var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener duracion horarios por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns> 
        public async Task<string> ObtenerDuracionHorariosPorIdPGeneralAsync(int idPGeneral)
        {
            var query = @"SELECT id, Contenido
                            FROM pla.V_TPGeneralDocumentoPW_DuracionHorarios
                            WHERE IdPGeneral = @idPGeneral
                            ORDER BY id ASC;";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener prerrequisitos por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns>
        public string ObtenerPrerrequisitosPorIdPGeneral(int idPGeneral)
        {
            var query = @"
		                    SELECT Titulo, Contenido,IdPGeneral, OrdenWeb
                    FROM pla.V_DatoProgramaGeneralContenidoPorIdPrograma
                    WHERE IdPGeneral = @idPGeneral AND Titulo LIKE '%Pre-Requisitos%'";

            var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener expositores por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>string</returns> 
        public string ObtenerExpositoresPorIdPGeneral(int idPGeneral)
        {
            var query = @"SELECT Id,PrimerNombre,SegundoNombre,ApellidoPaterno,ApellidoMaterno,NombrePais,HojaVidaResumidaPerfil,IdPGeneral
                    FROM pla.V_ObtenerExpositorPorIdPrograma
                    WHERE IdPGeneral = @idPGeneral";

            var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return null;

            try
            {
                return JsonConvert.DeserializeObject<string>(resultado);
            }
            catch
            {
                return resultado.Trim('"');
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener expositores por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<ProgramaExpositoresDTO></returns> 
        public async Task<List<ProgramaExpositoresDTO>> ObtenerExpositoresPorIdPGeneralAsync(int idPGeneral)
        {
            var query = @"SELECT Id,PrimerNombre,SegundoNombre,ApellidoPaterno,ApellidoMaterno,NombrePais,HojaVidaResumidaPerfil,IdPGeneral
                  FROM pla.V_ObtenerExpositorPorIdPrograma
                  WHERE IdPGeneral = @idPGeneral AND Estado = 1";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return new List<ProgramaExpositoresDTO>();

            try
            {
                return JsonConvert.DeserializeObject<List<ProgramaExpositoresDTO>>(resultado) ?? new List<ProgramaExpositoresDTO>();
            }
            catch
            {
                return new List<ProgramaExpositoresDTO>();
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener resumen programa por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <param name="idCentroCosto">Id de Centro de Costo</param>
        /// <returns>List<ResumenProgramaDTO></returns> 
        public async Task<List<PEspecificoPorIdPGeneralV2DTO>> ObtenerPorIdPGeneralAsync(int idPGeneral)
        {
            string query = @"SELECT 
                     Id,
                     Nombre,
                     Ciudad,
                     Tipo,
                     Duracion,
                     EstadoPId,
                     v2.FechaHoraInicio,
                     IdCategoria,
                     CentroCosto
                  FROM pla.V_ListaProgramaEspecificoPorIdPrograma AS v1
					 INNER JOIN pla.V_ListaFechaInicioPEspecificoPadrePEspecificoHijoPorIdPadre AS v2 ON v2.IdPEspecifico =v1.Id 
                  WHERE IdPGeneral = @idPGeneral";

            string resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            return string.IsNullOrEmpty(resultado) || resultado.Contains("[]")
                ? new List<PEspecificoPorIdPGeneralV2DTO>()
                : JsonConvert.DeserializeObject<List<PEspecificoPorIdPGeneralV2DTO>>(resultado)!;
        }


        public async Task<PGeneralAtributosPrincipalesv2DTO> ObtenerPGeneralAtributosPrincipalesPorIdAsync(int idPGeneral)
        {
            string query = @"SELECT 
                     Id,
                     Nombre,
                     Pw_duracion AS PwDuracion,
                     (SELECT Categoria FROM pla.T_CategoriaPrograma WHERE Id = pg.IdCategoria) AS EsProgramaOCurso
                  FROM pla.T_PGeneral pg
                  WHERE Estado = 1 AND Id = @idPGeneral";

            string resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idPGeneral });

            return string.IsNullOrEmpty(resultado) || resultado.Contains("[]")
                ? new PGeneralAtributosPrincipalesv2DTO()
                : JsonConvert.DeserializeObject<PGeneralAtributosPrincipalesv2DTO>(resultado)!;
        }
        public List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioPorIdsPEspecificoPadre(List<int> idsPEspecificoPadre)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                     IdPEspecifico,
                     FechaHoraInicio
                 FROM pla.V_ListaFechaInicioPEspecificoPadrePEspecificoHijoPorIdPadre
                 WHERE PEspecificoPadreId IN @idsPEspecificoPadre";
                string repuesta = _dapperRepository.QueryDapper(query, new { idsPEspecificoPadre });
                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(repuesta);
                }
                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioPorIdsPEspecifico(List<int> idsPEspecifico)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                     IdPEspecifico,
                     FechaHoraInicio
                 FROM pla.V_ListaFechaInicioPEspecificoSesionPorIdPEspecifico
                 WHERE IdPEspecifico IN @idsPEspecifico";
                string repuesta = _dapperRepository.QueryDapper(query, new { idsPEspecifico });
                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(repuesta);
                }
                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioSinSesionPorIdsPEspecifico(List<int> idsPEspecifico)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> objeto = new();
                string query = @"SELECT
                     IdPEspecifico,
                     FechaHoraInicio
                 FROM pla.V_ListaFechaInicioPEspecificoSesionSinInicioPorIdPEspecifico
                 WHERE Orden=1 AND IdPEspecifico IN @idsPEspecifico";
                string repuesta = _dapperRepository.QueryDapper(query, new { idsPEspecifico });
                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<PEspecificoSesionFechaHoraInicioDTO>>(repuesta)!;
                }
                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PEspecificoPorIdPGeneralV2DTO> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<PEspecificoPorIdPGeneralV2DTO> rpta = new();
                string query = @"SELECT 
      	                    Id,
                     Nombre,
                     Ciudad,
                     Tipo,
                     Duracion,
                     EstadoPId,
                     FechaCreacion,
                     IdCategoria,
                     CentroCosto
                 FROM pla.V_ListaProgramaEspecificoPorIdPrograma
                 WHERE IdPGeneral = @idPGeneral";
                string resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPorIdPGeneralV2DTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPorIdPGeneral()", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get objetivos raw
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>ObjetivosRawDTO</returns> 
        public async Task<ObjetivosRawDTO> GetObjetivosRawAsync(int idPGeneral)
        {
            try
            {
                // SQL optimizado - moviendo condiciones al WHERE y usando TOP 1
                var sql = @"SELECT 
                                PG.IdPGeneral,
                                CP.Categoria AS EsProgramaOCurso,
                                DS.Contenido AS ObjetivosHtml
                            FROM pla.T_PGeneral AS PG WITH (NOLOCK)
                            INNER JOIN pla.T_CategoriaPrograma AS CP 
                                ON CP.Id = PG.IdCategoria
                            INNER JOIN pla.T_PGeneralDocumento_PW AS PGD 
                                ON PGD.IdPGeneral = PG.IdPGeneral
                                AND PGD.Estado = 1
                            INNER JOIN pla.T_Documento_PW AS D 
                                ON D.Id = PGD.IdDocumento
                                AND D.Estado = 1
                                AND D.IdPlantillaPW IN (10, 11, 33, 36) -- Silabo V2, Perfil Programa V2, Perfil Módulo Ocupacional, Silabo Módulo Ocupacional
                            INNER JOIN pla.T_DocumentoSeccion_PW AS DS 
                                ON DS.IdDocumentoPW = D.Id
                                AND DS.Estado = 1
                            INNER JOIN pla.T_SilaboSeccion AS SS
                                ON LTRIM(RTRIM(SS.Nombre)) LIKE LTRIM(RTRIM(CONCAT('%', ISNULL(DS.TituloComparar, DS.Titulo), '%'))) COLLATE Latin1_General_CI_AI
                                AND SS.Estado = 1
                            WHERE PG.IdPGeneral = @idPGeneral
                              AND SS.Nombre = 'Objetivos';";

                // Timeout más agresivo para forzar optimización
                var resultado = await _dapperRepository.FirstOrDefaultAsync(sql, new { idPGeneral });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<ObjetivosRawDTO>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get beneficios raw
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<BeneficioRawDTO></returns> 
        public async Task<List<BeneficioRawDTO>> GetBeneficiosRawAsync(int idPGeneral)
        {
            try
            {
                var sql = @"SELECT
                                IdPGeneral,
                                EsProgramaOCurso,
                                IdVersion,
                                Version,
                                Beneficio,
                                Nota
                            FROM (
                                SELECT
                                    CP.Categoria AS EsProgramaOCurso,
                                    VP.Nombre AS Version,
                                    pd.IdPGeneral AS IdPGeneral,
                                    ds.Contenido AS Beneficio,
                                    CBPV.IdVersionPrograma AS IdVersion,
                                    ds.PiePagina AS Nota,
                                    ROW_NUMBER() OVER (
                                        PARTITION BY cb.IdPGeneral, cb.OrdenBeneficio 
                                        ORDER BY  
                                            cb.Id DESC
                                    ) AS rn
                                FROM pla.T_PGeneralDocumento_PW pd
                                INNER JOIN pla.T_Documento_PW doc 
                                    ON pd.IdDocumento = doc.Id AND doc.Estado = 1
                                INNER JOIN pla.T_DocumentoSeccion_PW ds 
                                    ON doc.Id = ds.IdDocumentoPw AND ds.Estado = 1
                                INNER JOIN pla.T_Plantilla_PW pla 
                                    ON pla.Id = ds.IdPlantillaPw
                                LEFT JOIN pla.T_ConfiguracionBeneficioProgramaGeneral cb 
                                    ON pd.IdPGeneral = cb.IdPGeneral
                                    AND cb.Tipo = 2
                                    AND cb.IdBeneficio = ds.Id
                                    AND cb.Estado = 1
                                INNER JOIN pla.T_ConfiguracionBeneficioProgramaGeneralVersion CBPV 
                                    ON CBPV.IdConfiguracionBeneficioPGneral = cb.Id
                                INNER JOIN pla.T_PGeneral PG 
                                    ON PG.Id = cb.IdPGeneral
                                INNER JOIN pla.T_CategoriaPrograma CP 
                                    ON CP.Id = PG.IdCategoria
                                INNER JOIN pla.T_VersionPrograma VP 
                                    ON VP.Id = CBPV.IdVersionPrograma
                                WHERE pla.Nombre LIKE '%v2'
                                  AND ds.Titulo = 'Beneficios'
                                  AND pd.Estado = 1
                                  AND CBPV.Estado = 1
                                  AND pd.IdPGeneral = @idPGeneral
                            ) t
                            WHERE rn = 1;";
                var resultado = await _dapperRepository.QueryDapperAsync(sql, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<BeneficioRawDTO>>(resultado);
                }
                return new List<BeneficioRawDTO>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get certificaciones raw
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<CertificacionRawDTO></returns> 
        public async Task<List<CertificacionRawDTO>> GetCertificacionesRawAsync(int idPGeneral)
        {
            try
            {
                var sql = @"
            SELECT 
                PG.Id AS IdPGeneral,
                CP.Categoria AS EsProgramaOCurso,
                DCS.id AS idDocumento,
                DCS.Contenido AS Beneficio,
                DCS.Cabecera AS Cabecera,
                DCS.PiePagina AS Nota
            FROM pla.T_PGeneral PG WITH (NOLOCK)
            INNER JOIN pla.T_CategoriaPrograma CP WITH (NOLOCK)
                ON CP.Id = PG.IdCategoria
            INNER JOIN pla.T_PGeneralDocumento_PW PDC WITH (NOLOCK)
                ON PDC.IdPGeneral = PG.Id AND PDC.Estado = 1
            INNER JOIN pla.T_Documento_PW DC WITH (NOLOCK)
                ON PDC.IdDocumento = DC.Id AND DC.IdPlantillaPW IN (10, 11, 33, 36)
            INNER JOIN pla.T_DocumentoSeccion_PW DCS WITH (NOLOCK)
                ON DCS.IdDocumentoPW = DC.Id 
                AND DCS.Titulo IN ('Certificación','Certificaci&#243;n')
                AND DCS.Estado = 1
            WHERE PG.Id = @idPGeneral;
        ";

                var resultado = await _dapperRepository.QueryDapperAsync(sql, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<CertificacionRawDTO>>(resultado);
                }
                return new List<CertificacionRawDTO>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get metodologia raw
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>MetodologiaRawDTO</returns> 
        public async Task<MetodologiaRawDTO> GetMetodologiaRawAsync(int idPGeneral)
        {
            var sql = @"
        SELECT 
            PG.Id AS IdPGeneral,
            CP.Categoria AS EsProgramaOCurso,
            DCS.id AS idDocumento,
            DCS.Titulo AS Titulo,
            DCS.Contenido AS ContenidoMetodologia
        FROM pla.T_PGeneral PG WITH (NOLOCK)
        INNER JOIN pla.T_CategoriaPrograma CP WITH (NOLOCK)
            ON CP.Id = PG.IdCategoria
        INNER JOIN pla.T_PGeneralDocumento_PW PDC WITH (NOLOCK)
            ON PDC.IdPGeneral = PG.Id AND PDC.Estado = 1
        INNER JOIN pla.T_Documento_PW DC WITH (NOLOCK)
            ON PDC.IdDocumento = DC.Id AND DC.IdPlantillaPW IN (10, 11, 33, 36)
        INNER JOIN pla.T_DocumentoSeccion_PW DCS WITH (NOLOCK)
            ON DCS.IdDocumentoPW = DC.Id 
            AND DCS.Titulo IN ('Metodología Online De Este programa')
            AND DCS.Estado = 1
        WHERE PG.Id = @idPGeneral
    ";

            var resultado = await _dapperRepository.FirstOrDefaultAsync(sql, new { idPGeneral });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                return JsonConvert.DeserializeObject<MetodologiaRawDTO>(resultado);
            }
            return null;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get pautas complementarias raw
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>PautasComplementariasRawDTO</returns> 
        public async Task<PautasComplementariasRawDTO> GetPautasComplementariasRawAsync(int idPGeneral)
        {
            var sql = @"
        SELECT 
            PG.Id AS IdPGeneral,
            CP.Categoria AS EsProgramaOCurso,
            DCS.id AS idDocumento,
            DCS.Titulo AS Titulo,
            DCS.Contenido AS ContenidoMetodologia
        FROM pla.T_PGeneral PG WITH (NOLOCK)
        INNER JOIN pla.T_CategoriaPrograma CP WITH (NOLOCK)
            ON CP.Id = PG.IdCategoria
        INNER JOIN pla.T_PGeneralDocumento_PW PDC WITH (NOLOCK)
            ON PDC.IdPGeneral = PG.Id AND PDC.Estado = 1
        INNER JOIN pla.T_Documento_PW DC WITH (NOLOCK)
            ON PDC.IdDocumento = DC.Id AND DC.IdPlantillaPW IN (10, 11, 33, 36)
        INNER JOIN pla.T_DocumentoSeccion_PW DCS WITH (NOLOCK)
            ON DCS.IdDocumentoPW = DC.Id 
            AND DCS.Titulo IN ('Pautas Complementarias')
            AND DCS.Estado = 1
        WHERE PG.Id = @idPGeneral
    ";

            var resultado = await _dapperRepository.FirstOrDefaultAsync(sql, new { idPGeneral });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                return JsonConvert.DeserializeObject<PautasComplementariasRawDTO>(resultado);
            }
            return null;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener perfil profesional cliente
        /// </summary>
        /// <param name="idAlumno">Id de Alumno</param>
        /// <returns>PerfilProfesionalClienteDTO</returns> 
        public async Task<PerfilProfesionalClienteDTO> ObtenerPerfilProfesionalClienteAsync(int idAlumno)
        {
            var sql = @"
                        SELECT  
                            AF.Nombre AS AreaFormacion,
                            CA.Nombre AS Cargo,
                            A.PrincipalResponsabilidadProfesional,
                            ISNULL(TE.Nombre, 'Sin Experiencia Registrada') AS AniosExperienciaProfesional,
                            I.Nombre AS Industria,
                            ATR.Nombre AS AreaTrabajo,
                            EMP.Nombre AS Empresa,
                            ISNULL(TEA.Nombre, 'Sin Tamaño de Empresa Registrado') AS TamanioEmpresa
                        FROM mkt.T_Alumno A WITH (NOLOCK)
                        LEFT JOIN pla.T_AreaFormacion AF WITH (NOLOCK) ON AF.Id = A.IdAFormacion
                        LEFT JOIN pla.T_Cargo CA WITH (NOLOCK) ON CA.Id = A.IdCargo
                        LEFT JOIN pla.T_AreaTrabajo ATR WITH (NOLOCK) ON ATR.Id = A.IdATrabajo
                        LEFT JOIN pla.T_Industria I WITH (NOLOCK) ON I.Id = A.IdIndustria
                        LEFT JOIN pla.T_Empresa EMP WITH (NOLOCK) ON EMP.Id = A.IdEmpresa
                        LEFT JOIN pla.T_TiempoExperiencia TE WITH (NOLOCK) ON TE.Id = A.IdExperiencia
                        LEFT JOIN pla.T_TamanioEmpresaAgenda TEA WITH (NOLOCK) ON TEA.Id = A.IdTamanioEmpresaAgenda
                        WHERE A.Id = @idAlumno;";

            var resultado = await _dapperRepository.FirstOrDefaultAsync(sql, new { idAlumno });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                return JsonConvert.DeserializeObject<PerfilProfesionalClienteDTO>(resultado);
            }
            return null;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get secciones programa raw
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<SeccionProgramaRawDTO></returns> 
        public List<SeccionDocumentov2DTO> ObtenerDocumentoSeccion(int idPgeneral)
        {
            string query = @"SELECT Id, Titulo, Contenido, IdPGeneral, Orden, NumeroFila, NombreTitulo
             FROM pla.V_ListaSeccionesPorIdPrograma_Silabos 
             WHERE IdPGeneral = @idPgeneral";
            var result = _dapperRepository.QueryDapper(query, new { idPgeneral });
            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<List<SeccionDocumentov2DTO>>(result) : new List<SeccionDocumentov2DTO>();
        }

        // Los demás métodos se quedan igual
        public PgeneralDocumentoSeccionv2DTO ObtenerPgeneralDocumentoPorId(int id)
        {
            string query = @"SELECT Id, Nombre, pw_duracion FROM pla.V_TPgeneral_PorIdBusqueda WHERE Id = @Id";
            var result = _dapperRepository.FirstOrDefault(query, new { Id = id });
            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<PgeneralDocumentoSeccionv2DTO>(result) : null;
        }

        public List<PgeneralHijov2DTO> ObtenerPGeneralHijos(int idPgeneral)
        {
            string query = @"SELECT Id, IdPgeneral, Nombre, Pg_titulo, pw_duracion, Orden
             FROM pla.V_TPgeneral_ObtenerHijos
             WHERE IdPGeneral_Padre = @IdPgeneral AND Estado=1 ORDER BY Orden ASC";
            var result = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPgeneral });
            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<List<PgeneralHijov2DTO>>(result) : new List<PgeneralHijov2DTO>();
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Url del Documento de Agenda asociado a un Id y a un Id de Pais
        /// </summary>
        /// <param name="idDocumentoAgenda">Id de Documento Agenda</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        /// []TODO Mover a T_DocumentoAgendaPaisUrl
        public StringDTO ObtenerDocumentoAgendaUrlPorPais(int idDocumentoAgenda, int idPais)
        {
            try
            {
                StringDTO url = new StringDTO();
                var query = @"SELECT TOP 1 [Url] AS Valor FROM com.T_DocumentoAgendaPaisUrl WHERE IdDocumentoAgenda = @idDocumentoAgenda AND IdPais = @idPais";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idDocumentoAgenda, idPais });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    url = JsonConvert.DeserializeObject<StringDTO>(resultadoQuery);
                }
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jeremy Pacheco.
        /// Fecha: 21/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene encuestas de Alumno por Matricula 
        /// </summary>
        /// <param name="idMatricula">Matricula del Alumno</param>
        /// <returns> List<EncuestaAsignadoMatriculaDTO> </returns>
        public List<EncuestaAsignadoMatriculaDTO> ObtenerEncuestaAlumnoMatriculaCurso(int idMatricula)
        {
            try
            {
                var query = @"SELECT 
                                IdPEspecificoSesionEncuestaAlumno,
                                IdEncuestaSesionPrograma,
                                Titulo,
                                FechaEncuesta,
                                IdPEspecificoSesion,
                                Tipo,
                                Descripcion,
                                IdPGeneral,
                                IdPEspecifico,
                                FechaEncuestaRealizada,
                                Estatus,
                                ComentarioAlumno FROM pw.V_PW_EncuestaAsignadasAlumnoSincronico 
                                WHERE IdMatriculaCabecera = @idMatricula AND AsignadoPara=1";
                var resultado = _dapperRepository.QueryDapper(query, new { idMatricula });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var respuesta = JsonConvert.DeserializeObject<List<EncuestaAsignadoMatriculaDTO>>(resultado);
                    return respuesta;
                }

                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas de Encuesta del IdPEspecifico
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <returns> List<PEspecificoSesionEncuestaPreguntaDTO> </returns>
        public List<PEspecificoSesionEncuestaPreguntaDTO> ObtenerPreguntasSesionEncuestaIdPespecifico(int IdPEspecificoSesion)
        {
            try
            {
                List<PEspecificoSesionEncuestaPreguntaDTO> rpta = new List<PEspecificoSesionEncuestaPreguntaDTO>();
                string _query = "   SELECT Id,IdEncuestaSesionPrograma,IdEncuestaOnline,IdPreguntaEncuestaTipo,Pregunta,Descripcion,NombreTipoPregunta,IdPEspecificoSesion, IdCategoria, NombreCategoria,DescripcionActiva,PreguntaObligatoria,PreguntaActiva FROM pw.V_PW_EncuestaOnlineObtenerPregunta WHERE IdPEspecificoSesion=@IdPEspecificoSesion AND PreguntaActiva=1 ";
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdPEspecificoSesion = IdPEspecificoSesion });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEncuestaPreguntaDTO>>(respuesta);
                    return rpta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Alternativas de las preguntas de Encuesta por Sesion
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <returns> List<PEspecificoSesionEncuestaPreguntaDTO> </returns>
        public List<PEspecificoSesionEncuestaPreguntaAlternativaDTO> ObtenerPEspecificoSesionEncuestaPreguntaAlternativaPorIdSesion(int IdPEspecificoSesion)
        {
            try
            {
                List<PEspecificoSesionEncuestaPreguntaAlternativaDTO> rpta = new List<PEspecificoSesionEncuestaPreguntaAlternativaDTO>();
                string _query = @"SELECT Id,IdEncuestaSesionPrograma,IdEncuestaOnline,IdPreguntaEncuesta,Respuesta,Orden,Puntaje,IdPEspecificoSesion FROM pw.V_PW_EncuestaOnlineObtenerPreguntaAlternativa
                                    WHERE IdPEspecificoSesion=@IdPEspecificoSesion";
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdPEspecificoSesion = IdPEspecificoSesion });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEncuestaPreguntaAlternativaDTO>>(respuesta);
                    return rpta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }

        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene encuesta por IdPEspecificoSesion y alumno
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <param name="IdMatriculaCabecera"> Identificador de la matricula</param>
        /// <returns> List<PEspecificoSesionEncuestaAlumnoDTO> </returns>
        public List<PEspecificoSesionEncuestaAlumnoDTO> ObtenerEncuestaAlumnoPorIdPEspecificoSesion(int IdPEspecificoSesion, int IdMatriculaCabecera)
        {
            try
            {
                List<PEspecificoSesionEncuestaAlumnoDTO> rpta = new List<PEspecificoSesionEncuestaAlumnoDTO>();
                string _query = "SELECT Id,IdPEspecificoSesion,IdEncuestaSesionPrograma,IdMatriculaCabecera,Puntaje,FechaRealizada FROM pw.V_PW_ObtenerEncuestaOnlineAlumno WHERE IdPEspecificoSesion = @IdPEspecificoSesion AND IdMatriculaCabecera = @IdMatriculaCabecera";
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdPEspecificoSesion = IdPEspecificoSesion, IdMatriculaCabecera = IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEncuestaAlumnoDTO>>(respuesta);
                    return rpta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de las encuesta por IdPEspecifico y matricula
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <param name="IdMatriculaCabecera"> Identificador de la matricula</param>
        /// <returns> List<PEspecificoSesionEncuestaAlumnoRespuestaDTO> </returns>
        public List<PEspecificoSesionEncuestaAlumnoRespuestaDTO> ObtenerPEspecificoSesionEncuestaAlumnoRespuestaPorIdSesion(int IdPEspecificoSesion, int IdMatriculaCabecera)
        {
            try
            {
                List<PEspecificoSesionEncuestaAlumnoRespuestaDTO> rpta = new List<PEspecificoSesionEncuestaAlumnoRespuestaDTO>();
                string _query = " SELECT Id,IdPEspecificoSesion,IdPreguntaEncuesta,IdPEspecificoSesionEncuestaAlumno,IdPreguntaRespuestaEncuesta,Valor,Puntos,IdMatriculaCabecera FROM pw.V_PW_ObtenerEncuestaOnlineAlumnoRespuesta WHERE IdPEspecificoSesion=@IdPEspecificoSesion AND IdMatriculaCabecera=@IdMatriculaCabecera";
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdPEspecificoSesion = IdPEspecificoSesion, IdMatriculaCabecera = IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEncuestaAlumnoRespuestaDTO>>(respuesta);
                    return rpta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Agrega encuesta de alumno 
        /// </summary>
        /// <param name="data">Datos para insertar Encuesta</param>
        /// <returns>retorna true o false </returns>
        public bool AgregarPEspecificoSesionEncuestaAlumno(AgregarPEspecificoSesionEncuestaAlumnoDTO data)
        {
            try
            {
                string _queryPrevio = @"SELECT * FROM  pw.T_PW_PEspecificoSesionEncuestaAlumno WHERE IdMatriculaCabecera=@IdMatriculaCabecera and IdEncuestaSesionPrograma=@IdEncuestaSesionPrograma and estado=1";
                string respuesta = _dapperRepository.QueryDapper(_queryPrevio, new { IdMatriculaCabecera = data.IdMatriculaCabecera, IdEncuestaSesionPrograma = data.IdEncuestaSesionPrograma });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                }
                else
                {
                    var asesor = "ASESOR";
                    string Json = JsonConvert.SerializeObject(data.Categorias);
                    string _query = "[pw].[SP_PW_AgregarPEspecificoSesionEncuestaAlumno]";
                    string query = _dapperRepository.QuerySPFirstOrDefault(_query, new
                    {
                        IdEncuestaSesionPrograma = data.IdEncuestaSesionPrograma,
                        IdMatriculaCabecera = data.IdMatriculaCabecera,
                        IdPEspecificoSesion = data.IdPEspecificoSesion,
                        IdPGeneral = data.IdPGeneral,
                        IdPEspecifico = data.IdPEspecifico,
                        Json,
                        Usuario = asesor
                    });

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un comentario a una encuesta
        /// </summary>
        /// <param name="Encuesta">Datos para agregar comentario a un Alumno</param>
        /// <returns>Retorna true o false</returns>
        public bool AgregarComentarioEncuesta(EncuestaComentarioDTO Encuesta)
        {
            try
            {

                var asesor = "ASESOR";

                string _queryPrevio = @"pw.SP_PW_RegistrarComentarioEncuestaAlumno";

                string respuesta = _dapperRepository.QuerySPDapper(_queryPrevio, new { 
                    Encuesta.IdPEspecificoSesionEncuestaAlumno,
                    Encuesta.Comentario,Usuario=asesor });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
