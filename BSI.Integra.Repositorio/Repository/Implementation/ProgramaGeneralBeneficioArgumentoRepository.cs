using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralBeneficioArgumentoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralBeneficioArgumento
    /// </summary>
    public class ProgramaGeneralBeneficioArgumentoRepository : GenericRepository<TProgramaGeneralBeneficioArgumento>, IProgramaGeneralBeneficioArgumentoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralBeneficioArgumentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralBeneficioArgumento, ProgramaGeneralBeneficioArgumento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralBeneficioArgumento MapeoEntidad(ProgramaGeneralBeneficioArgumento entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralBeneficioArgumento modelo = _mapper.Map<TProgramaGeneralBeneficioArgumento>(entidad);

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

        public TProgramaGeneralBeneficioArgumento Add(ProgramaGeneralBeneficioArgumento entidad)
        {
            try
            {
                var ProgramaGeneralBeneficioArgumento = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralBeneficioArgumento);
                return ProgramaGeneralBeneficioArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralBeneficioArgumento Update(ProgramaGeneralBeneficioArgumento entidad)
        {
            try
            {
                var ProgramaGeneralBeneficioArgumento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralBeneficioArgumento.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralBeneficioArgumento);
                return ProgramaGeneralBeneficioArgumento;
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


        public IEnumerable<TProgramaGeneralBeneficioArgumento> Add(IEnumerable<ProgramaGeneralBeneficioArgumento> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralBeneficioArgumento> listado = new List<TProgramaGeneralBeneficioArgumento>();
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

        public IEnumerable<TProgramaGeneralBeneficioArgumento> Update(IEnumerable<ProgramaGeneralBeneficioArgumento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralBeneficioArgumento> listado = new List<TProgramaGeneralBeneficioArgumento>();
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
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficioArgumento por id
        /// </summary>
        /// <returns> ProgramaGeneralBeneficioArgumento </returns>
        public ProgramaGeneralBeneficioArgumento? ObtenerPorId(int id)
        {
            try
            {
                var query = @"      
                    SELECT 
                        Id,
		                IdProgramaGeneralBeneficio,
		                Nombre,
		                IdPGeneral,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
		            FROM com.T_ProgramaGeneralBeneficioArgumento
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralBeneficioArgumento>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficioArgumento.
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioArgumentoDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioArgumentoDTO> ObtenerProgramaGeneralBeneficioArgumento()
        {
            try
            {
                List<ProgramaGeneralBeneficioArgumentoDTO> rpta = new List<ProgramaGeneralBeneficioArgumentoDTO>();
                var query = @"      
                    SELECT
	                    Id,
	                    IdProgramaGeneralBeneficio,
	                    Nombre,
	                    IdPGeneral,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_ProgramaGeneralBeneficioArgumento
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioArgumentoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralBeneficioArgumento para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioArgumentoComboDTO> ObtenerCombo()
        {
            try
            {
                List<ProgramaGeneralBeneficioArgumentoComboDTO> rpta = new List<ProgramaGeneralBeneficioArgumentoComboDTO>();
                var query = @"SELECT Id,Nombre FROM com.T_ProgramaGeneralBeneficioArgumento WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioArgumentoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralBeneficioArgumento asociados a un ProgramaGeneralBeneficio.
        /// </summary>
        /// <param name="idBeneficio">Id de ProgramaGeneralBeneficio</param>
        /// <returns> List<ProgramaGeneralBeneficioArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioArgumentoAgendaDTO> ObtenerProgramaGeneralBeneficioArgumentoPorIdBeneficio(int idBeneficio)
        {
            try
            {
                List<ProgramaGeneralBeneficioArgumentoAgendaDTO> argumentosBeneficio = new List<ProgramaGeneralBeneficioArgumentoAgendaDTO>();
                var query = @"
                    SELECT Id,IdProgramaGeneralBeneficio,IdPGeneral,Nombre
                    FROM com.T_ProgramaGeneralBeneficioArgumento
                    WHERE IdProgramaGeneralBeneficio = @idBeneficio AND Estado = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idBeneficio });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    argumentosBeneficio = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioArgumentoAgendaDTO>>(resultadoQuery);
                }
                return argumentosBeneficio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
