using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralProblemaDetalleSolucionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 25/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralProblemaDetalleSolucion
    /// </summary>
    public class ProgramaGeneralProblemaDetalleSolucionRepository : GenericRepository<TProgramaGeneralProblemaDetalleSolucion>, IProgramaGeneralProblemaDetalleSolucionRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaDetalleSolucionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaDetalleSolucion, ProgramaGeneralProblemaDetalleSolucion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaDetalleSolucion MapeoEntidad(ProgramaGeneralProblemaDetalleSolucion entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaDetalleSolucion modelo = _mapper.Map<TProgramaGeneralProblemaDetalleSolucion>(entidad);

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

        public TProgramaGeneralProblemaDetalleSolucion Add(ProgramaGeneralProblemaDetalleSolucion entidad)
        {
            try
            {
                var ProgramaGeneralProblemaDetalleSolucion = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaDetalleSolucion);
                return ProgramaGeneralProblemaDetalleSolucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaDetalleSolucion Update(ProgramaGeneralProblemaDetalleSolucion entidad)
        {
            try
            {
                var ProgramaGeneralProblemaDetalleSolucion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaDetalleSolucion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaDetalleSolucion);
                return ProgramaGeneralProblemaDetalleSolucion;
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


        public IEnumerable<TProgramaGeneralProblemaDetalleSolucion> Add(IEnumerable<ProgramaGeneralProblemaDetalleSolucion> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaDetalleSolucion> listado = new List<TProgramaGeneralProblemaDetalleSolucion>();
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

        public IEnumerable<TProgramaGeneralProblemaDetalleSolucion> Update(IEnumerable<ProgramaGeneralProblemaDetalleSolucion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaDetalleSolucion> listado = new List<TProgramaGeneralProblemaDetalleSolucion>();
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
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralProblemaDetalleSolucion.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionDTO> ObtenerProgramaGeneralProblemaDetalleSolucion()
        {
            try
            {
                List<ProgramaGeneralProblemaDetalleSolucionDTO> rpta = new List<ProgramaGeneralProblemaDetalleSolucionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralProblema,
	                    Detalle,
	                    Solucion,
	                    IdPGeneral,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralProblemaDetalleSolucion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaDetalleSolucionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralProblemaDetalleSolucion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionComboDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionComboDTO> ObtenerCombo()
        {
            try
            {
                List<ProgramaGeneralProblemaDetalleSolucionComboDTO> rpta = new List<ProgramaGeneralProblemaDetalleSolucionComboDTO>();
                var query = @"
                    SELECT Id,IdProgramaGeneralProblema,Detalle,Solucion
                    FROM pla.T_ProgramaGeneralProblemaDetalleSolucion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaDetalleSolucionComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/18/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralProblemaDetalleSolucion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionComboDTO> </returns>
        public ProgramaGeneralProblemaDetalleSolucion ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id	,
                        IdProgramaGeneralProblema,
                        Detalle,
                        Solucion,
                        IdPGeneral AS IdPgeneral,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                        FROM pla.T_ProgramaGeneralProblemaDetalleSolucion
                        WHERE Estado = 1 AND Id=@id
                        ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralProblemaDetalleSolucion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene DetalleSolucion de Problemas basado en Id Problema y Id Oportunidad.
        /// </summary>
        /// <param name="idProgramaGeneralProblema">Id del Problema</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> ObtenerProgramaGeneralProblemaDetalleSolucionParaAgenda(int idProgramaGeneralProblema, int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> rpta = new List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerArgumentoProblemaProgramaGeneral", new { idOportunidad, idProgramaGeneralProblema });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin R.
        /// Fecha: 14/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene DetalleSolucion de Problemas basado en Id Problema y Id Oportunidad para la nueva agenda.
        /// </summary>
        /// <param name="idProgramaGeneralProblema">Id del Problema</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO> ObtenerProgramaGeneralProblemaDetalleSolucionParaAgendaNuevaAgenda(int idProgramaGeneralProblema, int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO> rpta = new List<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerArgumentoProblemaProgramaGeneralNuevaAgenda", new { idOportunidad, idProgramaGeneralProblema });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO>>(resultado);
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
        /// Obtiene registros de T_ProgramaGeneralProblemaDetalleSolucion asociados a un ProgramaGeneralProblema
        /// </summary>
        /// <param name="idProblema">Id del Problema</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionDTO> ObtenerProblemaDetalleSolucionPorIdProblema(int idProblema)
        {
            try
            {
                List<ProgramaGeneralProblemaDetalleSolucionDTO> detalleSolucion = new List<ProgramaGeneralProblemaDetalleSolucionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralProblema,
	                    Detalle,
	                    Solucion,
	                    IdPGeneral,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralProblemaDetalleSolucion
                    WHERE Estado = 1
                        AND IdProgramaGeneralProblema = @idProblema";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idProblema });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    detalleSolucion = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaDetalleSolucionDTO>>(resultadoQuery);
                }
                return detalleSolucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
