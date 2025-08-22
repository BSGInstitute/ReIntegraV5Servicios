using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class SedeTrabajoGrupoComparacionRepository : GenericRepository<TSedeTrabajoGrupoComparacion>, ISedeTrabajoGrupoComparacionRepository
    {
        private Mapper _mapper;
        public SedeTrabajoGrupoComparacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSedeTrabajoGrupoComparacion, SedeTrabajoGrupoComparacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<SedeTrabajoGrupoComparacion, TSedeTrabajoGrupoComparacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSedeTrabajoGrupoComparacion MapeoEntidad(SedeTrabajoGrupoComparacion entidad)
        {
            try
            {
                TSedeTrabajoGrupoComparacion modelo = _mapper.Map<TSedeTrabajoGrupoComparacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSedeTrabajoGrupoComparacion Add(SedeTrabajoGrupoComparacion entidad)
        {
            try
            {
                var SedeTrabajoGrupoComparacion = MapeoEntidad(entidad);
                base.Insert(SedeTrabajoGrupoComparacion);
                return SedeTrabajoGrupoComparacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSedeTrabajoGrupoComparacion Update(SedeTrabajoGrupoComparacion entidad)
        {
            try
            {
                var SedeTrabajoGrupoComparacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SedeTrabajoGrupoComparacion.RowVersion = entidadExistente.RowVersion;

                base.Update(SedeTrabajoGrupoComparacion);
                return SedeTrabajoGrupoComparacion;
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
        public IEnumerable<TSedeTrabajoGrupoComparacion> Add(IEnumerable<SedeTrabajoGrupoComparacion> listadoEntidad)
        {
            try
            {
                List<TSedeTrabajoGrupoComparacion> listado = new List<TSedeTrabajoGrupoComparacion>();
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
        public IEnumerable<TSedeTrabajoGrupoComparacion> Update(IEnumerable<SedeTrabajoGrupoComparacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSedeTrabajoGrupoComparacion> listado = new List<TSedeTrabajoGrupoComparacion>();
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

        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>SedeTrabajoGrupoComparacion || null</returns>
        public SedeTrabajoGrupoComparacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
		                IdSedeTrabajo,
		                IdGrupoComparacionProcesoSeleccion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_SedeTrabajoGrupoComparacion
                    WHERE Id=@id AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<SedeTrabajoGrupoComparacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#STG-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
