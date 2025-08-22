using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MaterialEnvioRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_MaterialEnvio
    /// </summary>
    public class MaterialEnvioRepository : GenericRepository<TMaterialEnvio>, IMaterialEnvioRepository
    {
        private Mapper _mapper;

        public MaterialEnvioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialEnvio, MaterialEnvio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMaterialEnvioDetalle, MaterialEnvioDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialEnvio MapeoEntidad(MaterialEnvio entidad)
        {
            try
            {
                TMaterialEnvio modelo = _mapper.Map<TMaterialEnvio>(entidad);
                if (entidad.MaterialEnvioDetalles != null && entidad.MaterialEnvioDetalles.Count() > 0)
                {
                    modelo.TMaterialEnvioDetalles = _mapper.Map<Collection<TMaterialEnvioDetalle>>(entidad.MaterialEnvioDetalles);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialEnvio Add(MaterialEnvio entidad)
        {
            try
            {
                var MaterialEnvio = MapeoEntidad(entidad);
                base.Insert(MaterialEnvio);
                return MaterialEnvio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialEnvio Update(MaterialEnvio entidad)
        {
            try
            {
                var MaterialEnvio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialEnvio.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialEnvio);
                return MaterialEnvio;
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
        public IEnumerable<TMaterialEnvio> Add(IEnumerable<MaterialEnvio> listadoEntidad)
        {
            try
            {
                List<TMaterialEnvio> listado = new List<TMaterialEnvio>();
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
        public IEnumerable<TMaterialEnvio> Update(IEnumerable<MaterialEnvio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialEnvio> listado = new List<TMaterialEnvio>();
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
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene MaterialEnvio por id.
        /// </summary>
        /// <returns>MaterialEnvio</returns>
        public MaterialEnvio? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdSedeTrabajo,
	                    IdPersonal_Remitente AS IdPersonalRemitente,
	                    IdProveedor_Envio AS IdProveedorEnvio,
	                    FechaEnvio,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_MaterialEnvio
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MaterialEnvio>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}



