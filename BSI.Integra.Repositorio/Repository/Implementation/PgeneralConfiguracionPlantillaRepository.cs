using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PGeneralConfiguracionPlantillaRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 29/11/2022
    /// <summary>
    /// Gestión general de T_PGeneralConfiguracionPlantilla
    /// </summary>
    public class PGeneralConfiguracionPlantillaRepository : GenericRepository<TPgeneralConfiguracionPlantilla>, IPGeneralConfiguracionPlantillaRepository
    {
        private Mapper _mapper;

        public PGeneralConfiguracionPlantillaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralConfiguracionPlantilla, PgeneralConfiguracionPlantilla>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralConfiguracionPlantillaDetalle, PGeneralConfiguracionPlantillaDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralConfiguracionPlantilla MapeoEntidad(PgeneralConfiguracionPlantilla entidad)
        {
            try
            {
                TPgeneralConfiguracionPlantilla modelo = _mapper.Map<TPgeneralConfiguracionPlantilla>(entidad);

                if (entidad.PGeneralConfiguracionPlantillaDetalle != null && entidad.PGeneralConfiguracionPlantillaDetalle.Count > 0)
                    modelo.TPgeneralConfiguracionPlantillaDetalles = _mapper.Map<List<TPgeneralConfiguracionPlantillaDetalle>>(entidad.PGeneralConfiguracionPlantillaDetalle);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralConfiguracionPlantilla Add(PgeneralConfiguracionPlantilla entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                base.Insert(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralConfiguracionPlantilla Update(PgeneralConfiguracionPlantilla entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralConfiguracionPlantilla.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
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


        public IEnumerable<TPgeneralConfiguracionPlantilla> Add(IEnumerable<PgeneralConfiguracionPlantilla> listadoEntidad)
        {
            try
            {
                List<TPgeneralConfiguracionPlantilla> listado = new List<TPgeneralConfiguracionPlantilla>();
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

        public IEnumerable<TPgeneralConfiguracionPlantilla> Update(IEnumerable<PgeneralConfiguracionPlantilla> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralConfiguracionPlantilla> listado = new List<TPgeneralConfiguracionPlantilla>();
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
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos pra contancias por matricula por medio de idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> DatosGenerarCertificadoDTO </returns>
        public DatosGenerarCertificadoDTO ObtenerDatosParaConstanciasPorMatricula(int idMatriculaCabecera)
        {
            var respuesta = new DatosGenerarCertificadoDTO();
            string query = "pla.SP_ObtenerDatosConstanciaPorMatricula";
            string resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculacabecera = idMatriculaCabecera });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                respuesta = JsonConvert.DeserializeObject<DatosGenerarCertificadoDTO>(resultado);
            }
            return respuesta;
        }
        public DatosAlumnosOperacionesDTO ObtenerDatoAlumno(int IdMatriculaCabecera)
        {
            try
            {
                DatosAlumnosOperacionesDTO rpta = new DatosAlumnosOperacionesDTO();
                string _query = "SELECT * FROM [fin].[V_ObtenerOportunidadOperaciones] WHERE IdMatricula=@IdMatriculaCabecera ";
                var resultado = _dapperRepository.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<DatosAlumnosOperacionesDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 19/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos pra contancias por matricula por medio de idMatriculaCabecesra
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> List<ConfiguracionPlantillaDTO> </returns>
        public List<PgeneralConfiguracionPlantillaDTO> ObtenerPGeneralConfiguracionPlantillaPorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<PgeneralConfiguracionPlantillaDTO> rpta = new List<PgeneralConfiguracionPlantillaDTO>();
                var query = "SELECT Id,IdPlantillaBase, IdPlantillaFrontal,IdPlantillaPosterior,UltimaFechaRemplazarCertificado FROM pla.V_PGeneral_ConfiguracionPlantilla WHERE Estado = 1 and IdPgeneral=@IdPgeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralConfiguracionPlantillaDTO>>(resultado)!;
                    foreach (var item in rpta)
                    {
                        item.Detalle = new List<PgeneralConfiguracionPlantillaDetalleDTO>();
                        string queryDetalle = "SELECT Id, IdPgeneralConfiguracionPlantilla,IdModalidadCurso,IdOperadorComparacion,NotaAprobatoria,DeudaPendiente FROM pla.V_PGeneral_ConfiguracionPlantillaDetalle WHERE Estado = 1 and IdPgeneralConfiguracionPlantilla=@IdPgeneralConfiguracionPlantilla";
                        var resultadoDetalle = _dapperRepository.QueryDapper(queryDetalle, new { IdPgeneralConfiguracionPlantilla = item.Id });
                        if (!string.IsNullOrEmpty(resultadoDetalle) && !resultadoDetalle.Contains("[]"))
                        {
                            item.Detalle = JsonConvert.DeserializeObject<List<PgeneralConfiguracionPlantillaDetalleDTO>>(resultadoDetalle)!;
                            foreach (var estados in item.Detalle)
                            {
                                estados.EstadosMatricula = new();
                                estados.SubEstadosMatricula = new();
                                string queryEstado = "SELECT IdEstadoMatricula as Id,IdEstadoMatricula as Valor FROM pla.T_PgeneralConfiguracionPlantillaEstadoMatricula WHERE Estado = 1 and IdPgeneralConfiguracionPlantillaDetalle=@IdPgeneralConfiguracionPlantillaDetalle";
                                var resultadoEstado = _dapperRepository.QueryDapper(queryEstado, new { IdPgeneralConfiguracionPlantillaDetalle = estados.Id });
                                if (!string.IsNullOrEmpty(resultadoEstado) && !resultadoEstado.Contains("[]"))
                                {
                                    estados.EstadosMatricula = JsonConvert.DeserializeObject<List<IntDTO>>(resultadoEstado)!.Select(x => x.Valor.GetValueOrDefault()).ToList();
                                }
                                string querySubEstado = "SELECT IdSubEstadoMatricula AS Id,IdSubEstadoMatricula as Valor  FROM pla.T_PgeneralConfiguracionPlantillaSubEstadoMatricula WHERE Estado = 1 and IdPgeneralConfiguracionPlantillaDetalle=@IdPgeneralConfiguracionPlantillaDetalle";
                                var resultadoSubEstado = _dapperRepository.QueryDapper(querySubEstado, new { IdPgeneralConfiguracionPlantillaDetalle = estados.Id });
                                if (!string.IsNullOrEmpty(resultadoSubEstado) && !resultadoSubEstado.Contains("[]"))
                                {
                                    estados.SubEstadosMatricula = JsonConvert.DeserializeObject<List<IntDTO>>(resultadoSubEstado)!.Select(x => x.Valor.GetValueOrDefault()).ToList();
                                }
                            }
                        }
                    }
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene los registros asociados al IdPGeneral y IdPlantillaBase
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<ProgramaAreaRelacionadum> </returns> 
        public IEnumerable<PgeneralConfiguracionPlantilla> ObtenerPorIdPGeneralYIdPlantillaBase(int idPGeneral, int idPlantillaBase)
        {
            try
            {
                var _query = @"SELECT conf.Id,
                                   conf.IdPlantillaFrontal,
                                   conf.IdPlantillaPosterior,
                                   conf.IdPgeneral,
                                   conf.UltimaFechaRemplazarCertificado,
                                   conf.Estado,
                                   conf.UsuarioCreacion,
                                   conf.UsuarioModificacion,
                                   conf.FechaCreacion,
                                   conf.FechaModificacion,
                                   conf.RowVersion,
                                   conf.IdMigracion,
	                               pla.IdPlantillaBase
                            FROM pla.T_PGeneralConfiguracionPlantilla conf
                                INNER JOIN mkt.T_Plantilla pla
                                    ON pla.Id = conf.IdPlantillaFrontal
                            WHERE conf.Estado = 1
                                  AND IdPgeneral = @IdPgeneral
                                  AND pla.IdPlantillaBase = @IdPlantillaBase;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral, IdPlantillaBase = idPlantillaBase });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralConfiguracionPlantilla>>(respuestaDapper);
                }
                return new List<PgeneralConfiguracionPlantilla>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> IEnumerable<ProgramaAreaRelacionadum> </returns> 
        public PgeneralConfiguracionPlantilla ObtenerPorId(int id)
        {
            try
            {
                string _query = @"SELECT Id,
                                           IdPlantillaFrontal,
                                           IdPlantillaPosterior,
                                           IdPgeneral,
                                           UltimaFechaRemplazarCertificado,
                                           Estado,
                                           UsuarioCreacion,
                                           UsuarioModificacion,
                                           FechaCreacion,
                                           FechaModificacion,
                                           RowVersion,
                                           IdMigracion
                                    FROM pla.T_PGeneralConfiguracionPlantilla
                                    WHERE Estado = 1
                                          AND Id = @Id;";
                var resultado = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                    return JsonConvert.DeserializeObject<PgeneralConfiguracionPlantilla>(resultado);
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}