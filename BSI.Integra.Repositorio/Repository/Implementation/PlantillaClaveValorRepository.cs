using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaClaveValorRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_PlantillaClaveValor
    /// </summary>
    public class PlantillaClaveValorRepository : GenericRepository<TPlantillaClaveValor>, IPlantillaClaveValorRepository
    {
        private Mapper _mapper;

        public PlantillaClaveValorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaClaveValor, PlantillaClaveValor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPlantillaClaveValor MapeoEntidad(PlantillaClaveValor entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaClaveValor modelo = _mapper.Map<TPlantillaClaveValor>(entidad);

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

        public TPlantillaClaveValor Add(PlantillaClaveValor entidad)
        {
            try
            {
                var PlantillaClaveValor = MapeoEntidad(entidad);
                base.Insert(PlantillaClaveValor);
                return PlantillaClaveValor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaClaveValor Update(PlantillaClaveValor entidad)
        {
            try
            {
                var PlantillaClaveValor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaClaveValor.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaClaveValor);
                return PlantillaClaveValor;
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


        public IEnumerable<TPlantillaClaveValor> Add(IEnumerable<PlantillaClaveValor> listadoEntidad)
        {
            try
            {
                List<TPlantillaClaveValor> listado = new List<TPlantillaClaveValor>();
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

        public IEnumerable<TPlantillaClaveValor> Update(IEnumerable<PlantillaClaveValor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaClaveValor> listado = new List<TPlantillaClaveValor>();
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
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PlantillaClaveValor.
        /// </summary>
        /// <returns> List<PlantillaClaveValorDTO> </returns>
        public IEnumerable<PlantillaClaveValorDTO> ObtenerPlantillaClaveValor()
        {
            try
            {
                List<PlantillaClaveValorDTO> rpta = new List<PlantillaClaveValorDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Clave,
	                    Valor,
	                    Etiquetas,
	                    IdPlantilla,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM mkt.T_PlantillaClaveValor
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaClaveValorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_PlantillaClaveValor por IdPlantilla
        /// </summary>
        /// <param name="idPlantilla">Nombre de Plantilla Base</param>
        /// <returns> List<PlantillaClaveValor> </returns>
        public IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantilla2(int idPlantilla)
        {
            try
            {
                List<PlantillaClaveValor> rpta = new List<PlantillaClaveValor>();
                var query = @"
                    SELECT
	                    Id,
                        Clave,
                        Valor,
                        Etiquetas,
                        IdPlantilla,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                    FROM mkt.T_PlantillaClaveValor
                    WHERE Estado = 1 AND IdPlantilla=@IdPlantilla";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPlantilla = idPlantilla });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaClaveValor>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaClaveValor para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlantillaClaveValorComboDTO> </returns>
        public IEnumerable<PlantillaClaveValorComboDTO> ObtenerCombo()
        {
            try
            {
                List<PlantillaClaveValorComboDTO> rpta = new List<PlantillaClaveValorComboDTO>();
                var query = @"
                    SELECT
	                    PCV.Id,
	                    P.Id AS IdPlantilla,
	                    P.Nombre AS Plantilla,
	                    PCV.Clave,
	                    PCV.Valor
                    FROM mkt.T_PlantillaClaveValor AS PCV
                    INNER JOIN mkt.T_Plantilla AS P
	                    ON PCV.IdPlantilla = P.Id
	                    AND P.Estado = 1
                    WHERE PCV.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaClaveValorComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor de plantillas relacionadas a una Plantilla Base en especifico.
        /// </summary>
        /// <param name="nombrePlantillaBase">Nombre de Plantilla Base</param>
        /// <returns> List<PlantillaValorDTO> </returns>
        public IEnumerable<PlantillaValorDTO> ObtenerPlantillaPorNombrePlantillaBase(string nombrePlantillaBase)
        {
            try
            {
                List<PlantillaValorDTO> plantillas = new List<PlantillaValorDTO>();
                var query = @"
                    SELECT Id, Nombre, Valor
                    FROM mkt.V_ObtenerPlantillaValor
                    WHERE EstadoPlantilla = 1 AND EstadoPlantillaClaveValor = 1 AND NombrePlantillaBase = @nombrePlantillaBase";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { nombrePlantillaBase = nombrePlantillaBase });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<List<PlantillaValorDTO>>(resultadoQuery);
                }
                return plantillas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el contenido de plantillas con ciertos filtros por defecto.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        public IEnumerable<PlantillaMailingAgendaDTO> ObtenerPlantillasMailing()
        {
            try
            {
                List<PlantillaMailingAgendaDTO> plantillas = new List<PlantillaMailingAgendaDTO>();
                var query = @"
                    SELECT IdPlantilla, Nombre, Valor, IdPlantillaClaveValor
                    FROM com.V_PlantillasAgendaPantalla2Detalle
                    WHERE Estado=1 AND Clave ='Texto' AND Nombre NOT LIKE '%Lista%' AND IdFaseOrigen IS NOT NULL OR (IdPlantillaBase IN(6))
                    GROUP BY IdPlantilla, Nombre, Valor, IdPlantillaClaveValor";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<List<PlantillaMailingAgendaDTO>>(resultadoQuery);
                }
                return plantillas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Valor de las Plantillas relacionadas a una Fase Oportunidad
        /// </summary>
        /// <param name="idFaseOportunidad">Id de la Fase Oportunidad</param>
        /// <returns> List<PlantillaClaveValorAreaEtiquetaDTO> </returns>
        public IEnumerable<PlantillaClaveValorAreaEtiquetaDTO> ObtenerPlantillasPorIdFaseOportunidad(int idFaseOportunidad)
        {
            try
            {
                List<PlantillaClaveValorAreaEtiquetaDTO> plantillas = new List<PlantillaClaveValorAreaEtiquetaDTO>();
                var query = @"
                    SELECT IdPlantilla,IdPlantillaClaveValor,Clave,Valor,IdAreaEtiqueta
                    FROM com.V_PlantillasAgendaPantalla2Detalle
                    WHERE Estado=1 AND IdFaseOrigen = @idFaseOportunidad OR IdPlantillaBase IN (1,6)";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idFaseOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<List<PlantillaClaveValorAreaEtiquetaDTO>>(resultadoQuery);
                }
                return plantillas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos principales de Plantillas asociadas a WhatsApp para la Agenda.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        public IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsAppAgenda()
        {
            try
            {
                List<PlantillaWhatsAppAgendaDTO> plantillas = new List<PlantillaWhatsAppAgendaDTO>();
                var query = @"
                    SELECT Id,Nombre,Descripcion,TipoPlantilla,Contenido FROM mkt.V_ObtenerPlantillaWhatsAppAgenda ORDER BY Nombre";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<List<PlantillaWhatsAppAgendaDTO>>(resultadoQuery);
                }
                return plantillas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos principales de Plantillas asociadas a WhatsApp para la Agenda.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        public IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsAppAgendaComercial()
        {
            try
            {
                List<PlantillaWhatsAppAgendaDTO> plantillas = new List<PlantillaWhatsAppAgendaDTO>();
                var query = @"
                    SELECT  
                        PL .Id,  
                        PL.Nombre,  
                        PL.Descripcion,  
                        PB.Id AS TipoPlantilla,  
                        PCV.Valor AS Contenido
                    FROM mkt.T_Plantilla AS PL WITH(NOLOCK)  
                    INNER JOIN pla.T_PlantillaBase AS PB WITH(NOLOCK) ON PL.IdPlantillaBase = PB.Id  
                    INNER JOIN mkt.T_PlantillaClaveValor AS PCV WITH(NOLOCK) ON PL.Id = PCV.IdPlantilla  
                    WHERE PB.Nombre LIKE '%WhatsApp%' AND PL.Nombre LIKE 'COMERCIAL |%' AND PL.Estado = 1 AND PB.Estado = 1 AND PCV.Estado = 1;";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<List<PlantillaWhatsAppAgendaDTO>>(resultadoQuery);
                }
                return plantillas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problema y Causa asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> List<ProblemaCausaDTO> </returns>
        public List<ProblemaCausaDTO> ObtenerCausaProblemaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ProblemaCausaDTO> problemas = new List<ProblemaCausaDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("com.GetListaProblemaCausaByOportunidad", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    problemas = JsonConvert.DeserializeObject<List<ProblemaCausaDTO>>(resultadoQuery);
                }
                return problemas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 10/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Problema y Causa asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> List<ProblemaCausaDTO> </returns>
        public async Task<List<ProblemaCausaDTO>> ObtenerCausaProblemaPorIdOportunidadAsync(int idOportunidad)
        {
            try
            {
                List<ProblemaCausaDTO> problemas = new List<ProblemaCausaDTO>();
                var resultadoQuery = await _dapperRepository.QuerySPDapperAsync("com.GetListaProblemaCausaByOportunidad", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    problemas = JsonConvert.DeserializeObject<List<ProblemaCausaDTO>>(resultadoQuery);
                }
                return problemas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los URL de Programas relacionados a un Centro de Costo.
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro Costo</param>
        /// <returns> List<PGeneralCursoRelacionadoDTO> </returns>
        public List<PGeneralCursoRelacionadoDTO> ObtenerCursosRelacionadosPorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                List<PGeneralCursoRelacionadoDTO> cursosRelacionados = new List<PGeneralCursoRelacionadoDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("com.SP_GetUrlCursosRelacionadosForMails", new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    cursosRelacionados = JsonConvert.DeserializeObject<List<PGeneralCursoRelacionadoDTO>>(resultadoQuery);
                }
                return cursosRelacionados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los URL de Programas relacionados a un Centro de Costo.
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro Costo</param>
        /// <returns> List<PGeneralCursoRelacionadoDTO> </returns>
        public async Task<List<PGeneralCursoRelacionadoDTO>> ObtenerCursosRelacionadosPorIdCentroCostoAsync(int idCentroCosto)
        {
            try
            {
                List<PGeneralCursoRelacionadoDTO> cursosRelacionados = new List<PGeneralCursoRelacionadoDTO>();
                var resultadoQuery = await _dapperRepository.QuerySPDapperAsync("com.SP_GetUrlCursosRelacionadosForMails", new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    cursosRelacionados = JsonConvert.DeserializeObject<List<PGeneralCursoRelacionadoDTO>>(resultadoQuery);
                }
                return cursosRelacionados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas disponibles por fase
        /// </summary>
        /// <param name="idFaseOportunidad"> Id de Fase de Oportunidad</param>
        /// <returns>List<FiltroDTO></returns>
        public IEnumerable<FiltroDTO> ObtenerPlantillaGenerarMensaje(int idFaseOportunidad)
        {
            try
            {
                List<FiltroDTO> plantillas = new List<FiltroDTO>();
                var query = @"SELECT Id, Nombre FROM mkt.V_PlantillaPorFaseOportunidad WHERE IdFaseOrigen = @idFaseOportunidad";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idFaseOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultadoQuery);
                }
                return plantillas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el monto de los cursos relacionados
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idEtiqueta">Id de la lista curso area(PK de la tabla mkt.T_ListaCursoAreaEtiqueta)</param>
        /// <returns>List<CursosRelacionadosDTO></returns>
        public List<CursosRelacionadosDTO> ObtenerMontosCursosRelacionados(int idOportunidad, int idEtiqueta)
        {
            try
            {
                List<CursosRelacionadosDTO> cursosRelacionados = new List<CursosRelacionadosDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("pla.SP_ObtenerListaCursosRelacionadosMontos", new { idOportunidad, idEtiqueta });//solo Proyectos
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    cursosRelacionados = JsonConvert.DeserializeObject<List<CursosRelacionadosDTO>>(resultadoQuery);
                }
                return cursosRelacionados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_PlantillaClaveValor relacionado al identificador.
        /// </summary>
        /// <param name="idPlantilla">Id del T_PlantillaClaveValor</param>
        /// <returns> PlantillaClaveValor </returns>
        public IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantilla(int idPlantilla)
        {
            try
            {
                List<PlantillaClaveValor> plantillaClavevValor = new List<PlantillaClaveValor>();
                var query = @"
                             SELECT Clave, Valor, IdPlantilla,Estado FROM mkt.T_PlantillaClaveValor 
                             WHERE IdPlantilla = @idPlantilla AND Estado = 1 AND Clave = 'texto' ";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idPlantilla });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillaClavevValor = JsonConvert.DeserializeObject<List<PlantillaClaveValor>>(resultadoQuery);
                }
                return plantillaClavevValor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 12/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una plantlilla por cada plantilla
        /// </summary>
        /// <param name="nombrePlantillaBase"></param>
        /// <returns></returns>
        /// <exception></exception>
        public List<PlantillaValorDTO> ObtenerPlantillaPorPlantillaBase(string nombrePlantillaBase)
        {
            try
            {
                List<PlantillaValorDTO> complementosDTO = new List<PlantillaValorDTO>();
                var query = "SELECT Id, Nombre, Valor FROM mkt.V_ObtenerPlantillaValor WHERE EstadoPlantilla = 1 and EstadoPlantillaClaveValor = 1 and NombrePlantillaBase = @nombrePlantillaBase";
                var queryRespuesta = _dapperRepository.QueryDapper(query, new { nombrePlantillaBase = nombrePlantillaBase });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    complementosDTO = JsonConvert.DeserializeObject<List<PlantillaValorDTO>>(queryRespuesta)!;
                }
                return complementosDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Todo Plantilla Mailing
        /// </summary>
        /// <returns></returns>
        public List<ContenidoPlantillaDTO> ObtenerTodoPlantillasMailing()
        {
            try
            {
                string query = @"SELECT 
                                    IdPlantilla AS Id, Nombre, Valor, IdPlantillaClaveValor 
                                FROM 
                                    com.V_PlantillasAgendaPantalla2Detalle 
                                WHERE 
                                    Estado = 1 and clave='Texto' and Nombre not  like'%Lista%' and IdFaseOrigen IS NOT NULL 
                                    OR (IdPlantillaBase IN(6))  GROUP BY  IdPlantilla, Nombre, Valor, IdPlantillaClaveValor";
                var queryContenidoPlantilla = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ContenidoPlantillaDTO>>(queryContenidoPlantilla)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene plantilla de whatsapp operaciones
        /// </summary>
        /// <returns> List<PlantillaWhatsAppDTO>  </returns>
        public List<PlantillaWhatsAppDTO> ObtenterPlantillaWhatsAppOperaciones()
        {
            try
            {
                string _queryplantillabyfase = "SELECT Id ,Nombre, Descripcion,TipoPlantilla,Contenido " +
                                               " From ope.V_WhatsAppPlantillaOperaciones";
                var queryplantillabyfase = _dapperRepository.QueryDapper(_queryplantillabyfase, null);
                return JsonConvert.DeserializeObject<List<PlantillaWhatsAppDTO>>(queryplantillabyfase);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas mailing disponibles para operaciones
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPlantillaGenerarMensajeOperaciones()
        {
            try
            {
                string query = @"SELECT 
                                    Id, Nombre 
                                FROM 
                                    mkt.V_ObtenerPlantillaDisponibleMailingOperaciones 
                                WHERE 
                                    Estado = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(resultadoQuery)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<FiltroDTO> ObtenerPlantillasModuloAgenda()
        {
            try
            {
                string query = @"SELECT
                                    Id, Nombre
                                FROM
                                    mkt.V_ObtenerPlantillaDisponibleMailingAgendaOperaciones
                                WHERE
                                    Estado = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(resultadoQuery)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PlantillaDisponiblePlanificacionDTO> ObtenerPlantillasModuloAgendaPlanificacion(int idModuloSistemaV5, int idPlantillaBase, int idPersonalAreaTrabajo)
        {
            try
            {
                List<PlantillaDisponiblePlanificacionDTO> lista = new();
                var resultado = _dapperRepository.QuerySPDapper(
                    "[pla].[SP_PlantillaModuloSistemaObtener]",
                    new { IdModuloSistemaV5 = idModuloSistemaV5, IdPlantillaBase = idPlantillaBase, IdPersonalAreaTrabajo = idPersonalAreaTrabajo }
                );
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    lista = JsonConvert.DeserializeObject<List<PlantillaDisponiblePlanificacionDTO>>(resultado)!;
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Adriana Chipana
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la plantilla clave valor por id plantilla
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlantillaClaveValor> ObtenerPorIdPlantillaTodos(int idPlantilla)
        {
            try
            {
                List<PlantillaClaveValor> plantillaClavevValor = new List<PlantillaClaveValor>();
                var query = @"
                             SELECT Clave, Valor, IdPlantilla,Estado FROM mkt.T_PlantillaClaveValor 
                             WHERE IdPlantilla = @idPlantilla AND Estado = 1 ";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idPlantilla });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillaClavevValor = JsonConvert.DeserializeObject<List<PlantillaClaveValor>>(resultadoQuery);
                }
                return plantillaClavevValor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Adriana Chipana
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Hace la aliminacion logica de la plantilla
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<PlantillaClavesValoresDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantilla == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Clave == x.Clave)); 
                Delete(listaBorrar.Select(x => x.Id), usuario);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Adriana Chipana
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene clave valor por clave y id plantilla
        /// </summary>
        /// <param name="clave">clave/param>
        /// <param name="idPlantilla">id Plantilla/param>
        /// <returns> DTO: PlantillaValorDetalleDTO </returns>
        public PlantillaClaveValor ObtenerPorClaveYPorIdPlantilla(string clave, int idPlantilla)
        {
            try
            {
                PlantillaClaveValor rpta = new PlantillaClaveValor();
                var query = @"SELECT Id,
                                   Clave,
                                   Valor,
                                   Etiquetas,
                                   IdPlantilla,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM mkt.T_PlantillaClaveValor
                            WHERE Estado = 1
                                  AND Clave = @Clave
                                  AND IdPlantilla = @IdPlantilla;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Clave = clave, IdPlantilla = idPlantilla });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PlantillaClaveValor>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
