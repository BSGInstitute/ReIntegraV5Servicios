using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralMotivacionArgumentoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralMotivacionArgumento
    /// </summary>
    public class ProgramaGeneralMotivacionArgumentoRepository : GenericRepository<TProgramaGeneralMotivacionArgumento>, IProgramaGeneralMotivacionArgumentoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralMotivacionArgumentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralMotivacionArgumento, ProgramaGeneralMotivacionArgumento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralMotivacionArgumento MapeoEntidad(ProgramaGeneralMotivacionArgumento entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralMotivacionArgumento modelo = _mapper.Map<TProgramaGeneralMotivacionArgumento>(entidad);

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

        public TProgramaGeneralMotivacionArgumento Add(ProgramaGeneralMotivacionArgumento entidad)
        {
            try
            {
                var ProgramaGeneralMotivacionArgumento = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralMotivacionArgumento);
                return ProgramaGeneralMotivacionArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralMotivacionArgumento Update(ProgramaGeneralMotivacionArgumento entidad)
        {
            try
            {
                var ProgramaGeneralMotivacionArgumento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralMotivacionArgumento.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralMotivacionArgumento);
                return ProgramaGeneralMotivacionArgumento;
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


        public IEnumerable<TProgramaGeneralMotivacionArgumento> Add(IEnumerable<ProgramaGeneralMotivacionArgumento> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralMotivacionArgumento> listado = new List<TProgramaGeneralMotivacionArgumento>();
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

        public IEnumerable<TProgramaGeneralMotivacionArgumento> Update(IEnumerable<ProgramaGeneralMotivacionArgumento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralMotivacionArgumento> listado = new List<TProgramaGeneralMotivacionArgumento>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ProgramaGeneralMotivacionArgumento? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id	,
		                IdProgramaGeneralMotivacion,
		                Nombre,
		                IdPGeneral AS IdPgeneral,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
	                FROM pla.T_ProgramaGeneralMotivacionArgumento
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralMotivacionArgumento>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralMotivacionArgumento.
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionArgumentoDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionArgumentoDTO> ObtenerProgramaGeneralMotivacionArgumento()
        {
            try
            {
                List<ProgramaGeneralMotivacionArgumentoDTO> rpta = new List<ProgramaGeneralMotivacionArgumentoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralMotivacion,
	                    Nombre,
	                    IdPGeneral,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralMotivacionArgumento
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralMotivacionArgumentoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralMotivacionArgumento para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionArgumentoComboDTO> ObtenerCombo()
        {
            try
            {
                List<ProgramaGeneralMotivacionArgumentoComboDTO> rpta = new List<ProgramaGeneralMotivacionArgumentoComboDTO>();
                var query = @"SELECT Id,IdProgramaGeneralMotivacion,Nombre FROM pla.T_ProgramaGeneralMotivacionArgumento WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralMotivacionArgumentoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene MotivacionArgumentos asociados a un Id Motivacion.
        /// </summary>
        /// <param name="idMotivacion">Id de Motivacion</param>
        /// <returns> List<ProgramaGeneralMotivacionArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionArgumentoComboDTO> ObtenerProgramaGeneralMotivacionArgumentoAgendaPorIdMotivacion(int idMotivacion)
        {
            try
            {
                List<ProgramaGeneralMotivacionArgumentoComboDTO> argumentosMotivacion = new List<ProgramaGeneralMotivacionArgumentoComboDTO>();
                var query = @"
                    SELECT Id,IdProgramaGeneralMotivacion,Nombre
                    FROM pla.T_ProgramaGeneralMotivacionArgumento
                    WHERE IdProgramaGeneralMotivacion = @idMotivacion and Estado = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idMotivacion });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    argumentosMotivacion = JsonConvert.DeserializeObject<List<ProgramaGeneralMotivacionArgumentoComboDTO>>(resultadoQuery);
                }
                return argumentosMotivacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
