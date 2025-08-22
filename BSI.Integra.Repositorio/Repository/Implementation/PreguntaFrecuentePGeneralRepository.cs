using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PreguntaFrecuentePGeneralRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_PreguntaFrecuentePGeneral
    /// </summary>
    public class PreguntaFrecuentePGeneralRepository : GenericRepository<TPreguntaFrecuentePgeneral>, IPreguntaFrecuentePGeneralRepository
    {
        private Mapper _mapper;

        public PreguntaFrecuentePGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaFrecuentePgeneral, PreguntaFrecuentePGeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPreguntaFrecuentePgeneral MapeoEntidad(PreguntaFrecuentePGeneral entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuentePgeneral modelo = _mapper.Map<TPreguntaFrecuentePgeneral>(entidad);

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

        public TPreguntaFrecuentePgeneral Add(PreguntaFrecuentePGeneral entidad)
        {
            try
            {
                var PreguntaFrecuentePGeneral = MapeoEntidad(entidad);
                base.Insert(PreguntaFrecuentePGeneral);
                return PreguntaFrecuentePGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaFrecuentePgeneral Update(PreguntaFrecuentePGeneral entidad)
        {
            try
            {
                var PreguntaFrecuentePGeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaFrecuentePGeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaFrecuentePGeneral);
                return PreguntaFrecuentePGeneral;
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


        public IEnumerable<TPreguntaFrecuentePgeneral> Add(IEnumerable<PreguntaFrecuentePGeneral> listadoEntidad)
        {
            try
            {
                List<TPreguntaFrecuentePgeneral> listado = new List<TPreguntaFrecuentePgeneral>();
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

        public IEnumerable<TPreguntaFrecuentePgeneral> Update(IEnumerable<PreguntaFrecuentePGeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaFrecuentePgeneral> listado = new List<TPreguntaFrecuentePgeneral>();
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PreguntaFrecuentePGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PreguntaFrecuentePGeneralDTO> </returns>
        public IEnumerable<PreguntaFrecuentePGeneralDTO> Obtener()
        {
            try
            {
                List<PreguntaFrecuentePGeneralDTO> rpta = new List<PreguntaFrecuentePGeneralDTO>();
                var query = @"SELECT Id,IdPreguntaFrecuente,IdPGeneral FROM pla.T_PreguntaFrecuentePGeneral WHERE Estado = 1 ORDER BY Id DESC;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PreguntaFrecuentePGeneralDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de Preguntas Frecuentes asociadas a un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> List<PreguntaFrecuentePorCentroCostoDTO> </returns>
        public IEnumerable<PreguntaFrecuentePorCentroCostoDTO> ObtenerPreguntaFrecuentePorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                List<PreguntaFrecuentePorCentroCostoDTO> preguntas = new List<PreguntaFrecuentePorCentroCostoDTO>();
                var resultadoStoreProcedure = _dapperRepository
                    .QuerySPDapper("pla.SP_ObtenerPreguntaFrecuentePGeneralPorIdCentroCosto", new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    preguntas = JsonConvert.DeserializeObject<List<PreguntaFrecuentePorCentroCostoDTO>>(resultadoStoreProcedure);
                }
                return preguntas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el información de la tabla T_PreguntaFrecuentePGeneral segun los parametros enviados
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="idArea"></param>
        /// <param name="idSubArea"></param>
        /// <param name="idTipo"></param>
        /// <returns>PreguntaFrecuentePGeneralDTO</returns>
        public List<PreguntaFrecuentePGeneralRespuestaDTO> ObtenerPreguntaFrecuenteCambio(int idPGeneral, int idArea, int idSubArea, int idTipo)
        {
            try
            {
                string queryPreguntaFrecuente = @"SELECT Id, Pregunta, Respuesta, IdSeccion, Nombre 
                                                    FROM Pla.V_PreguntaFrecuente 
                                                    WHERE (IdPGeneral = @idPGeneral OR IdPGeneral IS NULL) 
                                                    AND (IdArea = @idArea OR IdArea=1) AND (IdSubArea = @idSubArea OR IdSubArea=1) 
                                                    AND (IdTipo= @idTipo OR IdTipo=3) ORDER BY Nombre";
                var programaPreguntaFrecuente = _dapperRepository.QueryDapper(queryPreguntaFrecuente, new { IdPGeneral = idPGeneral, IdArea = idArea, IdSubArea = idSubArea, IdTipo = idTipo });
                return JsonConvert.DeserializeObject<List<PreguntaFrecuentePGeneralRespuestaDTO>>(programaPreguntaFrecuente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Preguntas Frecuentes por ProgramaCentroCostoDTO data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>List<PreguntaFrecuentePGeneralDTO2></returns>
        public List<PreguntaFrecuentePGeneralRespuestaDTO> ObtenerPreguntaFrecuente(ProgramaCentroCostoDTO data)
        {
            try
            {
                List<PreguntaFrecuentePGeneralRespuestaDTO> lista = new List<PreguntaFrecuentePGeneralRespuestaDTO>();
                string queryPreguntaFrecuente = "select " + data.IdPGeneral + " AS IdPrograma,Id,Pregunta,Respuesta,IdSeccion,Nombre From Pla.V_PreguntaFrecuente where (IdPGeneral = @IdPGeneral or IdPGeneral is Null) and (IdArea = @IdArea or IdArea=1) and (IdSubArea = @IdSubArea or IdSubArea=1) and(IdTipo= @IdTipo or IdTipo=3) Order by Nombre";
                var programaPreguntaFrecuente = _dapperRepository.QueryDapper(queryPreguntaFrecuente, new
                { IdPGeneral = data.IdPGeneral, IdArea = data.IdArea, IdSubArea = data.IdSubArea, IdTipo = data.TipoId });
                if (!string.IsNullOrEmpty(programaPreguntaFrecuente) && !programaPreguntaFrecuente.Contains("null"))
                {
                    lista = JsonConvert.DeserializeObject<List<PreguntaFrecuentePGeneralRespuestaDTO>>(programaPreguntaFrecuente)!;
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros por el IdPreguntaFrecuente
        /// </summary>
        /// <param name="idPreguntaFrecuente"> (PK) de T_PreguntaFrecuente </param>
        /// <returns> IEnumerable<PreguntaFrecuentePGeneral> </returns>
        public IEnumerable<PreguntaFrecuentePGeneral> ObtenerPorIdPreguntaFrecuente(int idPreguntaFrecuente)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPreguntaFrecuente,
                                   IdPGeneral,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PreguntaFrecuentePGeneral
                            WHERE Estado = 1
                                  AND IdPreguntaFrecuente = @IdPreguntaFrecuente;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPreguntaFrecuente = idPreguntaFrecuente });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuentePGeneral>>(resultado);
                }
                return new List<PreguntaFrecuentePGeneral>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros por el IdPreguntaFrecuente y IdPGeneral
        /// </summary>
        /// <param name="idPreguntaFrecuente"> (PK) de T_PreguntaFrecuente </param>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<PreguntaFrecuentePGeneral> </returns>
       public  PreguntaFrecuentePGeneral ObtenerPorIdPreguntaFrecuenteYIdPGeneral(int idPreguntaFrecuente, int idPGeneral)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPreguntaFrecuente,
                                   IdPGeneral,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PreguntaFrecuentePGeneral
                            WHERE Estado = 1
                                  AND IdPreguntaFrecuente = @IdPreguntaFrecuente 
                                  AND IdPGeneral = @IdPGeneral;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPreguntaFrecuente = idPreguntaFrecuente, IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<PreguntaFrecuentePGeneral>(resultado);
                }
                return new PreguntaFrecuentePGeneral();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
