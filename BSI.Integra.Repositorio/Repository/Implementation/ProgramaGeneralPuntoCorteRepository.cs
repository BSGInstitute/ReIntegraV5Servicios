using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralPuntoCorteRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPuntoCorte
    /// </summary>
    public class ProgramaGeneralPuntoCorteRepository : GenericRepository<TProgramaGeneralPuntoCorte>, IProgramaGeneralPuntoCorteRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPuntoCorteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPuntoCorte, ProgramaGeneralPuntoCorte>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralPuntoCorte MapeoEntidad(ProgramaGeneralPuntoCorte entidad)
        {
            try
            {
                TProgramaGeneralPuntoCorte modelo = _mapper.Map<TProgramaGeneralPuntoCorte>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPuntoCorte Add(ProgramaGeneralPuntoCorte entidad)
        {
            try
            {
                var ProgramaGeneralPuntoCorte = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPuntoCorte);
                return ProgramaGeneralPuntoCorte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPuntoCorte Update(ProgramaGeneralPuntoCorte entidad)
        {
            try
            {
                var ProgramaGeneralPuntoCorte = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralPuntoCorte.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralPuntoCorte);
                return ProgramaGeneralPuntoCorte;
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


        public IEnumerable<TProgramaGeneralPuntoCorte> Add(IEnumerable<ProgramaGeneralPuntoCorte> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPuntoCorte> listado = new List<TProgramaGeneralPuntoCorte>();
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

        public IEnumerable<TProgramaGeneralPuntoCorte> Update(IEnumerable<ProgramaGeneralPuntoCorte> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPuntoCorte> listado = new List<TProgramaGeneralPuntoCorte>();
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

        /// Fecha: 06/07/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de programas generales con su punto de corte respectivo
        /// </summary>
        /// <param name="filtroDTO">Objeto de clase ProgramaGeneralPuntoCorteFiltroDTO</param>
        /// <returns>Lista de objetos de clase ProgramaGeneralPuntoCorteAreaSubAreaDTO</returns>
        public List<ProgramaGeneralPuntoCorteAreaSubAreaDTO> ObtenerListaProgramaGeneralPuntoCorte(ProgramaGeneralPuntoCorteFiltroDTO filtroDTO)
        {
            try
            {
                List<ProgramaGeneralPuntoCorteAreaSubAreaDTO> listaPuntoCorte = new List<ProgramaGeneralPuntoCorteAreaSubAreaDTO>();
                string query = @"
                    SELECT
	                    IdProgramaGeneralPuntoCorte,
	                    IdProgramaGeneral,
	                    NombreProgramaGeneral,
	                    PuntoCorteMedia,
	                    PuntoCorteAlta,
	                    PuntoCorteMuyAlta,
	                    IdPais,
	                    Usuario,
	                    IdAreaCapacitacion,
	                    IdSubAreaCapacitacion
                    FROM pla.V_ObtenerProgramaGeneralPuntoCorteAreaSubArea";
                string condicion = string.Empty;
                condicion += filtroDTO.ListaIdAreaCapacitacion.Any() ? " AND IdAreaCapacitacion IN @ListaIdAreaCapacitacion" : string.Empty;
                condicion += filtroDTO.ListaIdSubAreaCapacitacion.Any() ? " AND IdSubAreaCapacitacion IN @ListaIdSubAreaCapacitacion" : string.Empty;
                condicion += filtroDTO.ListaIdProgramaGeneral.Any() ? " AND IdProgramaGeneral IN @ListaIdProgramaGeneral" : string.Empty;
                condicion += filtroDTO.ActivoProgramaGeneral != null ? " AND ActivoProgramaGeneral = @ActivoProgramaGeneral" : string.Empty;
                condicion = condicion.Length > 0 ? string.Concat(" WHERE", condicion.Substring(4)) : string.Empty;
                query += condicion;

                var queryRespuesta = _dapperRepository.QueryDapper(query, new
                {
                    filtroDTO.ListaIdAreaCapacitacion,
                    filtroDTO.ListaIdSubAreaCapacitacion,
                    filtroDTO.ListaIdProgramaGeneral,
                    filtroDTO.ActivoProgramaGeneral
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !query.Contains("[]"))
                {
                    listaPuntoCorte = JsonConvert.DeserializeObject<List<ProgramaGeneralPuntoCorteAreaSubAreaDTO>>(queryRespuesta)!;
                }
                return listaPuntoCorte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ProgramaGeneralPuntoCorteDTO> ObtenerPorIdPgeneral(int idProgramaGeneral)
        {
            try
            {
                List<ProgramaGeneralPuntoCorteDTO> rpta = new List<ProgramaGeneralPuntoCorteDTO>();
                string query = @"
                    SELECT Id,
		                IdProgramaGeneral,
		                PuntoCorteMedia,
		                PuntoCorteAlta,
		                PuntoCorteMuyAlta,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                ISNULL(IdPais, 0) AS IdPais
	                FROM pla.T_ProgramaGeneralPuntoCorte 
	                WHERE Estado = 1 AND IdProgramaGeneral = @IdProgramaGeneral";

                var resultado = _dapperRepository.QueryDapper(query, new { IdProgramaGeneral = idProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPuntoCorteDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ProgramaGeneralPuntoCorteDTO? ObtenerPorIdPgeneralIdPais(int idProgramaGeneral, int idPais)
        {
            try
            {
                string query = @"
                    SELECT Id,
		                IdProgramaGeneral,
		                PuntoCorteMedia,
		                PuntoCorteAlta,
		                PuntoCorteMuyAlta,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                ISNULL(IdPais, 0) AS IdPais
	                FROM pla.T_ProgramaGeneralPuntoCorte 
	                WHERE Estado = 1 AND IdProgramaGeneral = @IdProgramaGeneral AND ISNULL(IdPais, 0) = @IdPais";

                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProgramaGeneral = idProgramaGeneral, IdPais = idPais });
                if (!string.IsNullOrEmpty(resultado) && !query.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPuntoCorteDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ProgramaGeneralPuntoCorte? ObtenerPorId(int id)
        {
            try
            {
                ;
                string query = @"
                    SELECT Id,
		                IdProgramaGeneral,
		                PuntoCorteMedia,
		                PuntoCorteAlta,
		                PuntoCorteMuyAlta,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdPais
	                FROM pla.T_ProgramaGeneralPuntoCorte 
	                WHERE Estado = 1 AND Id = @Id";

                var resultado = _dapperRepository.QueryDapper(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !query.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPuntoCorte>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
