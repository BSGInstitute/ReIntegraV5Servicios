using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PuestoTrabajoGrupoComparacionRepository : GenericRepository<TPuestoTrabajoGrupoComparacion>, IPuestoTrabajoGrupoComparacionRepository
    {
        private Mapper _mapper;
        public PuestoTrabajoGrupoComparacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoGrupoComparacion, PuestoTrabajoGrupoComparacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoGrupoComparacion, TPuestoTrabajoGrupoComparacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajoGrupoComparacion MapeoEntidad(PuestoTrabajoGrupoComparacion entidad)
        {
            try
            {
                TPuestoTrabajoGrupoComparacion modelo = _mapper.Map<TPuestoTrabajoGrupoComparacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoGrupoComparacion Add(PuestoTrabajoGrupoComparacion entidad)
        {
            try
            {
                var PuestoTrabajoGrupoComparacion = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoGrupoComparacion);
                return PuestoTrabajoGrupoComparacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoGrupoComparacion Update(PuestoTrabajoGrupoComparacion entidad)
        {
            try
            {
                var PuestoTrabajoGrupoComparacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoGrupoComparacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoGrupoComparacion);
                return PuestoTrabajoGrupoComparacion;
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
        public IEnumerable<TPuestoTrabajoGrupoComparacion> Add(IEnumerable<PuestoTrabajoGrupoComparacion> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoGrupoComparacion> listado = new List<TPuestoTrabajoGrupoComparacion>();
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
        public IEnumerable<TPuestoTrabajoGrupoComparacion> Update(IEnumerable<PuestoTrabajoGrupoComparacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoGrupoComparacion> listado = new List<TPuestoTrabajoGrupoComparacion>();
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
        /// <returns>PuestoTrabajoGrupoComparacion || null</returns>
        public PuestoTrabajoGrupoComparacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
		                IdPostulante,
		                IdGrupoComparacionProcesoSeleccion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_PuestoTrabajoGrupoComparacion
                    WHERE Id=@id AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PuestoTrabajoGrupoComparacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PCR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
