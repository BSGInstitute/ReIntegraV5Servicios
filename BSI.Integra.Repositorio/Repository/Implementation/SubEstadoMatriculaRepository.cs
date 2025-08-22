using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SubEstadoMatriculaRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_SubEstadoMatricula
    /// </summary>
    public class SubEstadoMatriculaRepository : GenericRepository<TSubEstadoMatricula>, ISubEstadoMatriculaRepository
    {
        private Mapper _mapper;

        public SubEstadoMatriculaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubEstadoMatricula, SubEstadoMatricula>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSubEstadoMatricula MapeoEntidad(SubEstadoMatricula entidad)
        {
            try
            {
                //crea la entidad padre
                TSubEstadoMatricula modelo = _mapper.Map<TSubEstadoMatricula>(entidad);

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

        public TSubEstadoMatricula Add(SubEstadoMatricula entidad)
        {
            try
            {
                var SubEstadoMatricula = MapeoEntidad(entidad);
                base.Insert(SubEstadoMatricula);
                return SubEstadoMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubEstadoMatricula Update(SubEstadoMatricula entidad)
        {
            try
            {
                var SubEstadoMatricula = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SubEstadoMatricula.RowVersion = entidadExistente.RowVersion;

                base.Update(SubEstadoMatricula);
                return SubEstadoMatricula;
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


        public IEnumerable<TSubEstadoMatricula> Add(IEnumerable<SubEstadoMatricula> listadoEntidad)
        {
            try
            {
                List<TSubEstadoMatricula> listado = new List<TSubEstadoMatricula>();
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

        public IEnumerable<TSubEstadoMatricula> Update(IEnumerable<SubEstadoMatricula> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSubEstadoMatricula> listado = new List<TSubEstadoMatricula>();
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
        /// Obtiene todos los registros de T_SubEstadoMatricula.
        /// </summary>
        /// <returns> List<SubEstadoMatriculaDTO> </returns>
        public IEnumerable<TCRM_SubEstadoMatriculaDTO> ObtenerSubEstadoMatricula()
        {
            try
            {
                List<TCRM_SubEstadoMatriculaDTO> rpta = new List<TCRM_SubEstadoMatriculaDTO>();
                var query = @"
                    Select 
                        Id,
                        Nombre,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        Estado,
                        IdOpcionAvanceAcademico,
                        ValorAvanceAcademico1,
                        ValorAvanceAcademico2,
                        IdEstadoPago,
                        IdOpcionNotaPromedio,
                        ValorNotaPromedio1,
                        ValorNotaPromedio2,
                        TieneDeuda,
                        ProyectoFinal,
                        RequiereVerificacionInformacion,
                        EstadoMatricula 
                    From fin.V_ObtenerSubEstadoMatricula 
                    Where Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_SubEstadoMatriculaDTO>>(resultado);
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
        /// Obtiene registros de T_SubEstadoMatricula para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubEstadoMatriculaComboDTO> </returns>
        public IEnumerable<SubEstadoMatriculaComboDTO> ObtenerCombo()
        {
            try
            {
                List<SubEstadoMatriculaComboDTO> rpta = new List<SubEstadoMatriculaComboDTO>();
                var query = @"SELECT Id,Nombre FROM fin.T_SubEstadoMatricula WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SubEstadoMatriculaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion basica de T_SubEstadoMatricula
        /// </summary>
        /// <returns> List<SubEstadoMatriculaFiltroDTO> </returns>
        public IEnumerable<SubEstadoMatriculaFiltroDTO> ObtenerSubEstadoMatriculaFiltro()
        {
            try
            {
                List<SubEstadoMatriculaFiltroDTO> filtros = new List<SubEstadoMatriculaFiltroDTO>();
                var query = @"SELECT Id,Nombre,IdEstadoMatricula FROM fin.T_SubEstadoMatricula WHERE Estado = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    filtros = JsonConvert.DeserializeObject<List<SubEstadoMatriculaFiltroDTO>>(resultadoQuery);
                }
                return filtros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SubEstadoMatriculaFiltroDTO>> ObtenerSubEstadoMatriculaFiltroAsync()
        {
            try
            {
                var query = @"SELECT Id,Nombre,IdEstadoMatricula FROM fin.T_SubEstadoMatricula WHERE Estado = 1";
                var resultadoQuery = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<SubEstadoMatriculaFiltroDTO>>(resultadoQuery);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
