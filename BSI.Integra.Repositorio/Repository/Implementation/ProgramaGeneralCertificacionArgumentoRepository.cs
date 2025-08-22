using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralCertificacionArgumentoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralCertificacionArgumento
    /// </summary>
    public class ProgramaGeneralCertificacionArgumentoRepository : GenericRepository<TProgramaGeneralCertificacionArgumento>, IProgramaGeneralCertificacionArgumentoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralCertificacionArgumentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralCertificacionArgumento, ProgramaGeneralCertificacionArgumento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralCertificacionArgumento MapeoEntidad(ProgramaGeneralCertificacionArgumento entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralCertificacionArgumento modelo = _mapper.Map<TProgramaGeneralCertificacionArgumento>(entidad);

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

        public TProgramaGeneralCertificacionArgumento Add(ProgramaGeneralCertificacionArgumento entidad)
        {
            try
            {
                var ProgramaGeneralCertificacionArgumento = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralCertificacionArgumento);
                return ProgramaGeneralCertificacionArgumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralCertificacionArgumento Update(ProgramaGeneralCertificacionArgumento entidad)
        {
            try
            {
                var ProgramaGeneralCertificacionArgumento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralCertificacionArgumento.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralCertificacionArgumento);
                return ProgramaGeneralCertificacionArgumento;
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


        public IEnumerable<TProgramaGeneralCertificacionArgumento> Add(IEnumerable<ProgramaGeneralCertificacionArgumento> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralCertificacionArgumento> listado = new List<TProgramaGeneralCertificacionArgumento>();
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

        public IEnumerable<TProgramaGeneralCertificacionArgumento> Update(IEnumerable<ProgramaGeneralCertificacionArgumento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralCertificacionArgumento> listado = new List<TProgramaGeneralCertificacionArgumento>();
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
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> ProgramaGeneralCertificacionArgumento </returns>
        public ProgramaGeneralCertificacionArgumento? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdProgramaGeneralCertificacion,
		                Nombre,
		                IdPGeneral,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
	                FROM pla.T_ProgramaGeneralCertificacionArgumento
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralCertificacionArgumento>(resultado)!;
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
        /// Obtiene todos los registros de T_ProgramaGeneralCertificacionArgumento.
        /// </summary>
        /// <returns> List<ProgramaGeneralCertificacionArgumentoDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionArgumentoDTO> ObtenerProgramaGeneralCertificacionArgumento()
        {
            try
            {
                List<ProgramaGeneralCertificacionArgumentoDTO> rpta = new List<ProgramaGeneralCertificacionArgumentoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralCertificacion,
	                    Nombre,
	                    IdPGeneral,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralCertificacionArgumento
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralCertificacionArgumentoDTO>>(resultado);
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
        /// Obtiene registros de T_ProgramaGeneralCertificacionArgumento para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralCertificacionArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionArgumentoComboDTO> ObtenerCombo()
        {
            try
            {
                List<ProgramaGeneralCertificacionArgumentoComboDTO> rpta = new List<ProgramaGeneralCertificacionArgumentoComboDTO>();
                var query = @"SELECT Id,IdProgramaGeneralCertificacion,Nombre FROM pla.T_ProgramaGeneralCertificacionArgumento WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralCertificacionArgumentoComboDTO>>(resultado);
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
        /// Obtiene registros de T_ProgramaGeneralCertificacionArgumento para mostrarse en combo.
        /// </summary>
        /// <param name="idCertificacion">Id de Certificacion</param>
        /// <returns> List<ProgramaGeneralCertificacionArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionArgumentoComboDTO> ObtenerProgramaGeneralCertificacionArgumentoAgendaPorIdCertificacion(int idCertificacion)
        {
            try
            {
                List<ProgramaGeneralCertificacionArgumentoComboDTO> argumentos = new List<ProgramaGeneralCertificacionArgumentoComboDTO>();
                var query = @"
                    SELECT Id, IdProgramaGeneralCertificacion, Nombre
                    FROM pla.T_ProgramaGeneralCertificacionArgumento
                    WHERE IdProgramaGeneralCertificacion = @idCertificacion AND Estado = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idCertificacion });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    argumentos = JsonConvert.DeserializeObject<List<ProgramaGeneralCertificacionArgumentoComboDTO>>(resultadoQuery);
                }
                return argumentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
