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
    /// Repositorio: EstadoMatriculaRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_EstadoMatricula
    /// </summary>
    public class EstadoMatriculaRepository : GenericRepository<TEstadoMatricula>, IEstadoMatriculaRepository
    {
        private Mapper _mapper;

        public EstadoMatriculaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoMatricula, EstadoMatricula>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEstadoMatricula MapeoEntidad(EstadoMatricula entidad)
        {
            try
            {
                //crea la entidad padre
                TEstadoMatricula modelo = _mapper.Map<TEstadoMatricula>(entidad);
                modelo.EstadoMatricula = entidad._EstadoMatricula;

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

        public TEstadoMatricula Add(EstadoMatricula entidad)
        {
            try
            {
                var EstadoMatricula = MapeoEntidad(entidad);
                base.Insert(EstadoMatricula);
                return EstadoMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoMatricula Update(EstadoMatricula entidad)
        {
            try
            {
                var EstadoMatricula = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstadoMatricula.RowVersion = entidadExistente.RowVersion;

                base.Update(EstadoMatricula);
                return EstadoMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool Delete(int id, string usuario)
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


        public IEnumerable<TEstadoMatricula> Add(IEnumerable<EstadoMatricula> listadoEntidad)
        {
            try
            {
                List<TEstadoMatricula> listado = new List<TEstadoMatricula>();
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

        public IEnumerable<TEstadoMatricula> Update(IEnumerable<EstadoMatricula> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstadoMatricula> listado = new List<TEstadoMatricula>();
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EstadoMatricula.
        /// </summary>
        /// <returns> List<EstadoMatriculaDTO> </returns>
        public IEnumerable<EstadoMatriculaDTO> ObtenerEstadoMatricula()
        {
            try
            {
                List<EstadoMatriculaDTO> rpta = new List<EstadoMatriculaDTO>();
                var query = @"
                    SELECT
	                    Id, 
                        Estado_matricula as EstadoMatricula, 
                        Estado, 
                        UsuarioCreacion, 
                        FechaCreacion,
                        FechaModificacion,
                        UsuarioModificacion,
                        Activo
                    FROM fin.t_estadomatricula 
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoMatriculaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EstadoMatricula para mostrarse en combo.
        /// </summary>
        /// <returns> List<EstadoMatriculaComboDTO> </returns>
        public IEnumerable<EstadoMatriculaComboDTO> ObtenerEstadoMatriculaCombo()
        {
            try
            {
                List<EstadoMatriculaComboDTO> rpta = new List<EstadoMatriculaComboDTO>();
                var query = @"Select Id,Estado_Matricula AS EstadoMatricula From fin.T_EstadoMatricula WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoMatriculaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta los Estados de Subestado,a traves de un proceso alamacenado
        /// </summary>
        /// <returns> List<EstadoMatriculaDTO> </returns>
        public EstadoMatriculaListDTO InsertarEstadoSubestado(CRUDEstadoMatriculaDTO data)
        {
            EstadoMatriculaListDTO rpta = new EstadoMatriculaListDTO();
            rpta.Estado = new List<TCRM_EstadoMatriculaInsertarDTO>();
            var query = _dapperRepository.QuerySPDapper("[fin].[SP_InsertarEstadosMatriculaV5]", new
            {
                EstadoMatricula = data.EstadoMatricula,
                UsuarioCreacion = data.Usuario,
                IdSubEstados = data.IdSubEstados,
                Activo = data.Activo
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.Estado = JsonConvert.DeserializeObject<List<TCRM_EstadoMatriculaInsertarDTO>>(query);

            }
            return rpta;

        }
        public bool EliminarEstadoSubEstado(int id, string usuario)
        {
            var query = _dapperRepository.QuerySPDapper("fin.SP_EliminarEstadoMatriculaV5", new
            {
                Id = id,
                UsuarioModificacion = usuario
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                return true;

            }
            else return false;
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta los Estados de Subestado,a traves de un proceso alamacenado
        /// </summary>
        /// <returns> List<EstadoMatriculaDTO> </returns>
        public SubEstadoMatriculaDTO ObtenerSubEstadoIndividual(int IdEstadoMatricula)
        {
            SubEstadoMatriculaDTO rpta = new SubEstadoMatriculaDTO();
            rpta.SubEstado = new List<TCRM_SubEstadoMatriculaDTO>();
            var query = _dapperRepository.QuerySPDapper("fin.SP_SubEstadosMatriculaIndividual", new
            {
                IdEstadoMatricula = IdEstadoMatricula,
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.SubEstado = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(query);

            }
            return rpta;
        }

        public EstadoMatriculaListDTO EditarEstado(CRUDEstadoMatriculaDTO data)
        {
            EstadoMatriculaListDTO rpta = new EstadoMatriculaListDTO();
            rpta.Estado = new List<TCRM_EstadoMatriculaInsertarDTO>();
            var query = _dapperRepository.QuerySPDapper("fin.SP_EditarEstadoMatriculaV5", new
            {
                Id = data.Id,
                EstadoMatricula = data.EstadoMatricula,
                UsuarioModificacion = data.Usuario,
                IdSubEstados = data.IdSubEstados,
                Activo = data.Activo
            });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta.Estado = JsonConvert.DeserializeObject<List<TCRM_EstadoMatriculaInsertarDTO>>(query);

            }
            return rpta;
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de la vista V_ObtenerEstadoMatriculado asociados a una Matricula Activa para mostrarse en combo.
        /// </summary>
        /// <returns> List<EstadoMatriculaComboDTO> </returns>
        public IEnumerable<EstadoMatriculaComboDTO> ObtenerEstadoMatriculaParaMatriculados()
        {
            try
            {
                List<EstadoMatriculaComboDTO> estadosMatricula = new List<EstadoMatriculaComboDTO>();
                var query = @"SELECT Id,Nombre AS EstadoMatricula FROM fin.V_ObtenerEstadoMatriculado WHERE Estado=1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    estadosMatricula = JsonConvert.DeserializeObject<List<EstadoMatriculaComboDTO>>(resultadoQuery);

                }
                return estadosMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de la tabla T_EstadoMatricula asociados a una Matricula Activa para mostrarse en combo.
        /// </summary>
        /// <returns> List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO> </returns>
        public List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO> ObtenerTodoFiltroConfiguracionCoordinadora()
        {
            try
            {
                List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO> estadosMatricula = new List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO>();
                var query = @"SELECT Id as IdEstadoMatricula, Estado_matricula as EstadoMatricula FROM fin.T_EstadoMatricula WHERE Estado=1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    estadosMatricula = JsonConvert.DeserializeObject<List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO>>(resultadoQuery);

                }
                return estadosMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de la vista V_ObtenerSubEstadoMatricula asociados a una Matricula Activa para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubEstadoMatriculaFiltroDTO> </returns>
        public List<SubEstadoMatriculaFiltroDTO> ObtenerComboOficialSubEstadoMatricula()
        {
            try
            {
                List<SubEstadoMatriculaFiltroDTO> estadoMatriculado = new List<SubEstadoMatriculaFiltroDTO>();
                var query = "Select Id,Nombre,IdEstadoMatricula From fin.V_ObtenerSubEstadoMatricula Where Estado=1";
                var pEspecificoDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<SubEstadoMatriculaFiltroDTO>>(pEspecificoDB);
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de estados de matricula para ser usados en combo
        /// </summary>
        /// <param></param>
        /// <returns>Lista</returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> comboRepuesta = new List<ComboDTO>();
                var query = "SELECT Id, Estado_matricula AS Nombre FROM fin.T_EstadoMatricula WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    comboRepuesta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return comboRepuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = "SELECT Id, Estado_matricula AS Nombre FROM fin.T_EstadoMatricula WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Estado Matricula
        /// </summary>
        /// <returns></returns>
        public List<ObtenerEstadoMatriculaDTO> ObtenerEstadosMatricula()
        {
            try
            {
                List<ObtenerEstadoMatriculaDTO> estados = new List<ObtenerEstadoMatriculaDTO>();
                var query = string.Empty;
                query = "SELECT Id, EstadoMatricula, Estado, UsuarioCreacion, FechaCreacion FROM fin.V_ObtenerEstadosMatricula where Estado=1";
                var estadoDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(estadoDB) && !estadoDB.Contains("[]"))
                {
                    estados = JsonConvert.DeserializeObject<List<ObtenerEstadoMatriculaDTO>>(estadoDB)!;
                }
                return estados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Funcion referenciada en EstadoMatriculaController
        /// Devuelve los subestados segun el Id del EstadoMatricula
        /// </summary>
        /// <param name="idEstadoMatricula"></param>
        /// <returns> Lista: List<TCRM_SubEstadoMatriculaDTO> </returns>
        public List<TCRM_SubEstadoMatriculaDTO> ObtenerFiltroSubEstadoMatricula(int idEstadoMatricula)          //LPPG
        {
            try
            {
                List<TCRM_SubEstadoMatriculaDTO> estadoMatriculado = new List<TCRM_SubEstadoMatriculaDTO>();
                var query = @"
                            SELECT 
                                Id, Nombre 
                            FROM 
                                fin.V_ObtenerSubEstadoMatricula 
                            WHERE 
                                Estado = 1 AND IdEstadoMatricula = @IdEstadoMatricula";
                var pEspecificoDB = _dapperRepository.QueryDapper(query, new { IdEstadoMatricula = idEstadoMatricula });
                if (!string.IsNullOrEmpty(pEspecificoDB) && !pEspecificoDB.Contains("[]"))
                {
                    estadoMatriculado = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(pEspecificoDB)!;
                }
                return estadoMatriculado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
