using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ProgramaGeneralPrerequisitoModalidadRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPrerequisitoModalidad
    /// </summary>
    public class ProgramaGeneralPrerequisitoModalidadRepository : GenericRepository<TProgramaGeneralPrerequisitoModalidad>, IProgramaGeneralPrerequisitoModalidadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPrerequisitoModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPrerequisitoModalidad, ProgramaGeneralPrerequisitoModalidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralPrerequisitoModalidad MapeoEntidad(ProgramaGeneralPrerequisitoModalidad entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPrerequisitoModalidad modelo = _mapper.Map<TProgramaGeneralPrerequisitoModalidad>(entidad);

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

        public TProgramaGeneralPrerequisitoModalidad Add(ProgramaGeneralPrerequisitoModalidad entidad)
        {
            try
            {
                var ProgramaGeneralPrerequisitoModalidad = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPrerequisitoModalidad);
                return ProgramaGeneralPrerequisitoModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPrerequisitoModalidad Update(ProgramaGeneralPrerequisitoModalidad entidad)
        {
            try
            {
                var programaGeneralPrerequisitoModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                programaGeneralPrerequisitoModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(programaGeneralPrerequisitoModalidad);
                return programaGeneralPrerequisitoModalidad;
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


        public IEnumerable<TProgramaGeneralPrerequisitoModalidad> Add(IEnumerable<ProgramaGeneralPrerequisitoModalidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPrerequisitoModalidad> listado = new List<TProgramaGeneralPrerequisitoModalidad>();
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

        public IEnumerable<TProgramaGeneralPrerequisitoModalidad> Update(IEnumerable<ProgramaGeneralPrerequisitoModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPrerequisitoModalidad> listado = new List<TProgramaGeneralPrerequisitoModalidad>();
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
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_ProgramaGeneralPrerequisitoModalidad por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPrerequisito </returns>
        public ProgramaGeneralPrerequisitoModalidad? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdProgramaGeneralPrerequisito,
                        IdModalidadCurso,
                        Nombre,
                        IdPGeneral,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPrerequisitoModalidad
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPrerequisitoModalidad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPrMR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ProgramaGeneralPrerequisitoModalidad>() </returns>
        public IEnumerable<ProgramaGeneralPrerequisitoModalidad> ObtenerPorIdPGeneralPreRequisito(int idProgramaGeneralPrerequisito)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdProgramaGeneralPrerequisito,
                        IdModalidadCurso,
                        Nombre,
                        IdPGeneral,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPrerequisitoModalidad
                    WHERE
                        Estado = 1 AND IdProgramaGeneralPrerequisito = @idProgramaGeneralPrerequisito";
                var resultado = _dapperRepository.QueryDapper(query, new { idProgramaGeneralPrerequisito });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralPrerequisitoModalidad>>(resultado)!;
                }
                return new List<ProgramaGeneralPrerequisitoModalidad>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPrMR-OPIPG-002@Error en ObtenerPorIdPGeneralPreRequisito() {ex.Message}", ex);
            }
        }
    }
}
