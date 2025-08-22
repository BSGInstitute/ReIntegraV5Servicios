using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralProblemaDetalleSolucionRespuestaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralProblemaDetalleSolucionRespuesta
    /// </summary>
    public class ProgramaGeneralProblemaDetalleSolucionRespuestaRepository : GenericRepository<TProgramaGeneralProblemaDetalleSolucionRespuestum>, IProgramaGeneralProblemaDetalleSolucionRespuestaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaDetalleSolucionRespuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaDetalleSolucionRespuestum, ProgramaGeneralProblemaDetalleSolucionRespuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaDetalleSolucionRespuestum MapeoEntidad(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad)
        {
            try
            {
                var modelo = _mapper.Map<TProgramaGeneralProblemaDetalleSolucionRespuestum>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaDetalleSolucionRespuestum Add(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                base.Insert(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaDetalleSolucionRespuestum Update(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modelo.RowVersion = entidadExistente.RowVersion;

                base.Update(modelo);
                return modelo;
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


        public IEnumerable<TProgramaGeneralProblemaDetalleSolucionRespuestum> Add(IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuesta> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaDetalleSolucionRespuestum> listado = new List<TProgramaGeneralProblemaDetalleSolucionRespuestum>();
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

        public IEnumerable<TProgramaGeneralProblemaDetalleSolucionRespuestum> Update(IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaDetalleSolucionRespuestum> listado = new List<TProgramaGeneralProblemaDetalleSolucionRespuestum>();
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
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralProblemaDetalleSolucionRespuesta.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuesta> ObtenerTodo()
        {
            try
            {
                List<ProgramaGeneralProblemaDetalleSolucionRespuesta> rpta = new List<ProgramaGeneralProblemaDetalleSolucionRespuesta>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    IdProgramaGeneralProblemaDetalleSolucion,
	                    EsSeleccionado,
	                    EsSolucionado,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_ProgramaGeneralProblemaDetalleSolucionRespuesta
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaDetalleSolucionRespuesta>>(resultado);

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_ProgramaGeneralProblemaDetalleSolucionRespuesta asociado a una Oportunidad y un Detalle.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idProblemaSolucion">Id del Detalle Solucion de un Problema</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionRespuestaDTO> </returns>
        public ProgramaGeneralProblemaDetalleSolucionRespuesta ObtenerPorIdOportunidadIdProblemaSolucion(int idOportunidad, int idProblemaSolucion)
        {
            try
            {
                ProgramaGeneralProblemaDetalleSolucionRespuesta rpta = new ProgramaGeneralProblemaDetalleSolucionRespuesta();
                var query = @"
                            SELECT
	                            Id,
	                            IdOportunidad,
	                            IdProgramaGeneralProblemaDetalleSolucion,
	                            EsSeleccionado,
	                            EsSolucionado,
	                            Estado,
	                            UsuarioCreacion,
	                            UsuarioModificacion,
	                            FechaCreacion,
	                            FechaModificacion,
	                            RowVersion,
	                            IdMigracion
                            FROM pla.T_ProgramaGeneralProblemaDetalleSolucionRespuesta
                            WHERE Estado = 1
                                AND IdOportunidad = @idOportunidad
                                AND IdProgramaGeneralProblemaDetalleSolucion = @idProblemaSolucion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idProblemaSolucion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralProblemaDetalleSolucionRespuesta>(resultado)!;

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
