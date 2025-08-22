using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OportunidadIsVerificadaRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_OportunidadIsVerificadum
    /// </summary>
    public class OportunidadIsVerificadaRepository : GenericRepository<TOportunidadIsVerificadum>, IOportunidadIsVerificadaRepository
    {
        private Mapper _mapper;

        public OportunidadIsVerificadaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadIsVerificadum, OportunidadIsVerificada>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOportunidadIsVerificadum MapeoEntidad(OportunidadIsVerificada entidad)
        {
            try
            {
                //crea la entidad padre
                TOportunidadIsVerificadum modelo = _mapper.Map<TOportunidadIsVerificadum>(entidad);

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

        public TOportunidadIsVerificadum Add(OportunidadIsVerificada entidad)
        {
            try
            {
                var OportunidadVerificada = MapeoEntidad(entidad);
                base.Insert(OportunidadVerificada);
                return OportunidadVerificada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidadIsVerificadum Update(OportunidadIsVerificada entidad)
        {
            try
            {
                var SentinelSdtEstandarItem = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSdtEstandarItem.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSdtEstandarItem);
                return SentinelSdtEstandarItem;
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


        public IEnumerable<TOportunidadIsVerificadum> Add(IEnumerable<OportunidadIsVerificada> listadoEntidad)
        {
            try
            {
                List<TOportunidadIsVerificadum> listado = new List<TOportunidadIsVerificadum>();
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

        public IEnumerable<TOportunidadIsVerificadum> Update(IEnumerable<OportunidadIsVerificada> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOportunidadIsVerificadum> listado = new List<TOportunidadIsVerificadum>();
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
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene tupla ya sea por idOportunidad o idMatriculaCabecera
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idOportunidadMatriculaCabecera"></param>
        /// <returns></returns>
        public OportunidadIsVerificada ObtenerPorIdOportunidadOIdMatriculaCabecera(int idOportunidad, int idOportunidadMatriculaCabecera)
        {
            try
            {
                var query = @"
                    SELECT 
                        IdOportunidad, IdMatriculaCabecera, Verificado, Estado, UsuarioCreacion, UsuarioModificacion, 
                        FechaCreacion, FechaModificacion, IdMigracion FROM fin.T_OportunidadIsVerificada
                    WHERE IdOportunidad = @IdOportunidad OR IdMatriculaCabecera = @IdMatriculaCabecera AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad, IdMatriculaCabecera = idOportunidadMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<OportunidadIsVerificada>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        //// Fecha: 07/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene oportunidad IS Verificada
        /// </summary>
        /// <param name="idOportunidadIsVerificada"></param>
        /// <returns></returns>
        public List<OportunidadesVerificadasDTO> ObtenerOportunidadesVerificadas()
        {
            try
            {
                var query = "SELECT Coordinador, Alumno, CentroCosto, FaseOportunidad, CodigoMatricula FROM [fin].[V_TOportunidadIsVerificada_ObtenerOportunidadesVerificadas] WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<OportunidadesVerificadasDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 07/02/2023
        /// <summary>
        /// Obtiene todas las oportunidades en fase IS o M con estado de matricula matriculado y sin coordinador academico asignado
        /// </summary>
        /// <returns></returns>
        public List<OportunidadIsVerificadaDTO> ObtenerOportunidadIsVerificadaSinPeriodo()
        {
            try
            {
                var query = "SELECT IdOportunidad, Asesor, Alumno, CentroCosto, CodigoFaseOportunidad, CodigoMatricula, IdMatriculaCabecera, Verificado, UltimaFechaProgramada, FechaCambioIs FROM [fin].[V_ObtenerOportunidadesVerificarISM] WHERE EstadoMatricula = 'matriculado' AND UsuarioCoordinadorAcademico = '0' AND RowNumber = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<OportunidadIsVerificadaDTO>>(res);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 07/02/2023
        /// <summary>
        /// Obtiene todas las oportunidades en fase IS o M con estado de matricula matriculado y sin coordinador academico asignado por FechaInicio y FecheFin seleccionado
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<OportunidadIsVerificadaDTO> ObtenerOportunidadIsVerificadaConPeriodo(DateTime fechaInicio, DateTime fechaFin)

        {
            try
            {
                DateTime newFechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                var query = "SELECT IdOportunidad, Asesor, Alumno, CentroCosto, CodigoFaseOportunidad, CodigoMatricula, IdMatriculaCabecera, Verificado, UltimaFechaProgramada, FechaCambioIs FROM [fin].[V_ObtenerOportunidadesVerificarISM] WHERE FechaMatricula >= @FechaInicio AND FechaMatricula <= @FechaFin AND EstadoMatricula = 'matriculado' AND UsuarioCoordinadorAcademico = '0' AND RowNumber = 1";
                var res = _dapperRepository.QueryDapper(query, new { FechaInicio = fechaInicio, FechaFin = newFechaFin });
                return JsonConvert.DeserializeObject<List<OportunidadIsVerificadaDTO>>(res);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




    }
}