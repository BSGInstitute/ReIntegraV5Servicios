using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PgeneralAsubPgeneralVersionProgramaRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 25/07/2023
    /// <summary>
    /// Gestión general de T_PgeneralAsubPgeneralVersionPrograma
    /// </summary>
    public class PgeneralAsubPgeneralVersionProgramaRepository : GenericRepository<TPgeneralAsubPgeneralVersionPrograma>, IPgeneralAsubPgeneralVersionProgramaRepository
    {
        private Mapper _mapper;

        public PgeneralAsubPgeneralVersionProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralAsubPgeneralVersionPrograma, PgeneralAsubPgeneralVersionPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralAsubPgeneralVersionPrograma MapeoEntidad(PgeneralAsubPgeneralVersionPrograma entidad)
        {
            try
            {
                TPgeneralAsubPgeneralVersionPrograma modelo = _mapper.Map<TPgeneralAsubPgeneralVersionPrograma>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralAsubPgeneralVersionPrograma Add(PgeneralAsubPgeneralVersionPrograma entidad)
        {
            try
            {
                var PgeneralAsubPgeneralVersionPrograma = MapeoEntidad(entidad);
                base.Insert(PgeneralAsubPgeneralVersionPrograma);
                return PgeneralAsubPgeneralVersionPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralAsubPgeneralVersionPrograma Update(PgeneralAsubPgeneralVersionPrograma entidad)
        {
            try
            {
                var PgeneralAsubPgeneralVersionPrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralAsubPgeneralVersionPrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralAsubPgeneralVersionPrograma);
                return PgeneralAsubPgeneralVersionPrograma;
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

        public IEnumerable<TPgeneralAsubPgeneralVersionPrograma> Add(IEnumerable<PgeneralAsubPgeneralVersionPrograma> listadoEntidad)
        {
            try
            {
                List<TPgeneralAsubPgeneralVersionPrograma> listado = new List<TPgeneralAsubPgeneralVersionPrograma>();
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
        public IEnumerable<TPgeneralAsubPgeneralVersionPrograma> Update(IEnumerable<PgeneralAsubPgeneralVersionPrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralAsubPgeneralVersionPrograma> listado = new List<TPgeneralAsubPgeneralVersionPrograma>();
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
        /// Obtiene el registro T_PgeneralAsubPgeneralVersionPrograma por id
        /// </summary>
        /// <param name="id">Id PgeneralAsubPgeneralVersionPrograma</param>
        /// <returns> PgeneralAsubPgeneralVersionPrograma </returns>
        public PgeneralAsubPgeneralVersionPrograma? ObtenerPorId(int id)
        {
            try
            {
                PgeneralAsubPgeneralVersionPrograma rpta = new();
                var query = @"
                   SELECT 
		                Id,
		                IdPgeneralASubPgeneral AS IdPgeneralAsubPgeneral,
		                IdVersionPrograma,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
		            FROM pla.T_PgeneralASubPgeneralVersionPrograma 
                    WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralAsubPgeneralVersionPrograma>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro T_PgeneralAsubPgeneralVersionPrograma por id
        /// </summary>
        /// <param name="ids">Ids PgeneralAsubPgeneralVersionPrograma</param>
        /// <returns> PgeneralAsubPgeneralVersionPrograma </returns>
        public IEnumerable<PgeneralAsubPgeneralVersionPrograma> ObtenerPorIds(List<int> ids)
        {
            try
            {
                List<PgeneralAsubPgeneralVersionPrograma> rpta = new();
                var query = @"
                    SELECT 
		                Id,
		                IdPgeneralASubPgeneral AS IdPgeneralAsubPgeneral,
		                IdVersionPrograma,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracionS
		            FROM pla.T_PgeneralASubPgeneralVersionPrograma 
                    WHERE Estado=1 AND Id IN @ids";
                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralAsubPgeneralVersionPrograma>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-21
        /// Version: 1.0
        /// Obtiene: valores de tabla T_PgeneralASubPgeneralVersionPrograma
        /// </summary>
        /// <param name="idPgeneralASubPgeneral"></param>
        /// <returns>PgeneralAsubPgeneralVersionPrograma</returns>
        public IEnumerable<PgeneralAsubPgeneralVersionPrograma> ObtenerPoridPgeneralASubPgeneral(int idPgeneralASubPgeneral)
        {
            try
            {
                List<PgeneralAsubPgeneralVersionPrograma> rpta = new();
                var query = @" 
                    select 
	                    Id
	                    ,IdPgeneralASubPgeneral as IdPgeneralAsubPgeneral
	                    ,IdVersionPrograma
	                    ,Estado
	                    ,UsuarioCreacion
	                    ,UsuarioModificacion
	                    ,FechaCreacion
	                    ,FechaModificacion
	                    ,RowVersion
	                    ,IdMigracion
                    FROM [pla].[T_PgeneralASubPgeneralVersionPrograma]
                    WHERE IdPgeneralASubPgeneral = @IdPgeneralASubPgeneral AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPgeneralASubPgeneral = idPgeneralASubPgeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralAsubPgeneralVersionPrograma>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}



