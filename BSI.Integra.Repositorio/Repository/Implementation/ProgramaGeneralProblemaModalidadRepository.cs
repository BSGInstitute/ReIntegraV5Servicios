using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralProblemaModalidadRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralProblemaModalidad
    /// </summary>
    public class ProgramaGeneralProblemaModalidadRepository : GenericRepository<TProgramaGeneralProblemaModalidad>, IProgramaGeneralProblemaModalidadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaModalidad, ProgramaGeneralProblemaModalidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaModalidad MapeoEntidad(ProgramaGeneralProblemaModalidad entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaModalidad modelo = _mapper.Map<TProgramaGeneralProblemaModalidad>(entidad);

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

        public TProgramaGeneralProblemaModalidad Add(ProgramaGeneralProblemaModalidad entidad)
        {
            try
            {
                var ProgramaGeneralProblemaModalidad = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaModalidad);
                return ProgramaGeneralProblemaModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaModalidad Update(ProgramaGeneralProblemaModalidad entidad)
        {
            try
            {
                var ProgramaGeneralProblemaModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaModalidad);
                return ProgramaGeneralProblemaModalidad;
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


        public IEnumerable<TProgramaGeneralProblemaModalidad> Add(IEnumerable<ProgramaGeneralProblemaModalidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaModalidad> listado = new List<TProgramaGeneralProblemaModalidad>();
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

        public IEnumerable<TProgramaGeneralProblemaModalidad> Update(IEnumerable<ProgramaGeneralProblemaModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaModalidad> listado = new List<TProgramaGeneralProblemaModalidad>();
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
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralProblemaModalidad.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaModalidadDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaModalidadDTO> ObtenerProgramaGeneralProblemaModalidad()
        {
            try
            {
                List<ProgramaGeneralProblemaModalidadDTO> rpta = new List<ProgramaGeneralProblemaModalidadDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralProblema,
	                    IdModalidadCurso,
	                    Nombre,
	                    IdPGeneral,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralProblemaModalidad
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaModalidadDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_ProgramaGeneralProblemaModalidad asociados a un Problema.
        /// </summary>
        /// <param name="idProblema">Id del ProgramaGeneralProblema</param>
        /// <returns> List<ProgramaGeneralProblemaModalidadDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaModalidadDTO> ObtenerModalidadPorIdProblema(int idProblema)
        {
            try
            {
                List<ProgramaGeneralProblemaModalidadDTO> modalidades = new List<ProgramaGeneralProblemaModalidadDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralProblema,
	                    IdModalidadCurso,
	                    Nombre,
	                    IdPGeneral,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralProblemaModalidad
                    WHERE Estado = 1
                        AND IdProgramaGeneralProblema = @idProblema";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idProblema });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    modalidades = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaModalidadDTO>>(resultadoQuery);
                }
                return modalidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
