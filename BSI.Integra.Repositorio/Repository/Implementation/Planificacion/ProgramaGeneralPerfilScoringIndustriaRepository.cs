using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ProgramaGeneralPerfilScoringIndustriaRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPerfilScoringIndustrium
    /// </summary>
    public class ProgramaGeneralPerfilScoringIndustriaRepository : GenericRepository<TProgramaGeneralPerfilScoringIndustrium>, IProgramaGeneralPerfilScoringIndustriaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPerfilScoringIndustriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPerfilScoringIndustrium, ProgramaGeneralPerfilScoringIndustria>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralPerfilScoringIndustrium MapeoEntidad(ProgramaGeneralPerfilScoringIndustria entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringIndustrium perfilScoringIndustria = _mapper.Map<TProgramaGeneralPerfilScoringIndustrium>(entidad);

                return perfilScoringIndustria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilScoringIndustrium Add(ProgramaGeneralPerfilScoringIndustria entidad)
        {
            try
            {
                var perfilScoringIndustria = MapeoEntidad(entidad);
                base.Insert(perfilScoringIndustria);
                return perfilScoringIndustria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilScoringIndustrium Update(ProgramaGeneralPerfilScoringIndustria entidad)
        {
            try
            {
                var perfilScoringIndustria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilScoringIndustria.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilScoringIndustria);
                return perfilScoringIndustria;
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

        public IEnumerable<TProgramaGeneralPerfilScoringIndustrium> Add(IEnumerable<ProgramaGeneralPerfilScoringIndustria> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPerfilScoringIndustrium> listado = new List<TProgramaGeneralPerfilScoringIndustrium>();
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

        public IEnumerable<TProgramaGeneralPerfilScoringIndustrium> Update(IEnumerable<ProgramaGeneralPerfilScoringIndustria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPerfilScoringIndustrium> listado = new List<TProgramaGeneralPerfilScoringIndustrium>();
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
        /// Obtiene toda la información de T_ProgramaGeneralPerfilScoringIndustrium por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPerfilScoringIndustria </returns>
        public ProgramaGeneralPerfilScoringIndustria? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT 
                                    Id,
                                    IdPGeneral IdPgeneral,
                                    Nombre,
                                    IdIndustria,
                                    idSelect,
                                    Valor,
                                    Fila,
                                    Columna,
                                    Validar,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion
                                    FROM 
                                        pla.T_ProgramaGeneralPerfilScoringIndustria
                                    WHERE
                                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPerfilScoringIndustria>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPSIR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ProgramaGeneralPerfilScoringIndustria>() </returns>
        public IEnumerable<ProgramaGeneralPerfilScoringIndustria> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = @"SELECT Id,
                                    IdPGeneral IdPgeneral,
                                    Nombre,
                                    IdIndustria,
                                    idSelect,
                                    Valor,
                                    Fila,
                                    Columna,
                                    Validar,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion
                                    FROM 
                                        pla.T_ProgramaGeneralPerfilScoringIndustria
                                    WHERE
                                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralPerfilScoringIndustria>>(resultado)!;
                }
                return new List<ProgramaGeneralPerfilScoringIndustria>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPSIR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
    }
}
