using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PerfilPuestoTrabajoEstadoSolicitudRepository : GenericRepository<TPerfilPuestoTrabajoEstadoSolicitud>, IPerfilPuestoTrabajoEstadoSolicitudRepository
    {
        private Mapper _mapper;
        public PerfilPuestoTrabajoEstadoSolicitudRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitud>(MemberList.None).ReverseMap();
                cfg.CreateMap<PerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitudDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PerfilPuestoTrabajoEstadoSolicitud, TPerfilPuestoTrabajoEstadoSolicitud>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPerfilPuestoTrabajoEstadoSolicitud MapeoEntidad(PerfilPuestoTrabajoEstadoSolicitud entidad)
        {
            try
            {
                TPerfilPuestoTrabajoEstadoSolicitud modelo = _mapper.Map<TPerfilPuestoTrabajoEstadoSolicitud>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPerfilPuestoTrabajoEstadoSolicitud Add(PerfilPuestoTrabajoEstadoSolicitud entidad)
        {
            try
            {
                var PerfilPuestoTrabajoEstadoSolicitud = MapeoEntidad(entidad);
                base.Insert(PerfilPuestoTrabajoEstadoSolicitud);
                return PerfilPuestoTrabajoEstadoSolicitud;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPerfilPuestoTrabajoEstadoSolicitud Update(PerfilPuestoTrabajoEstadoSolicitud entidad)
        {
            try
            {
                var PerfilPuestoTrabajoEstadoSolicitud = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PerfilPuestoTrabajoEstadoSolicitud.RowVersion = entidadExistente.RowVersion;

                base.Update(PerfilPuestoTrabajoEstadoSolicitud);
                return PerfilPuestoTrabajoEstadoSolicitud;
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
        public IEnumerable<TPerfilPuestoTrabajoEstadoSolicitud> Add(IEnumerable<PerfilPuestoTrabajoEstadoSolicitud> listadoEntidad)
        {
            try
            {
                List<TPerfilPuestoTrabajoEstadoSolicitud> listado = new List<TPerfilPuestoTrabajoEstadoSolicitud>();
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
        public IEnumerable<TPerfilPuestoTrabajoEstadoSolicitud> Update(IEnumerable<PerfilPuestoTrabajoEstadoSolicitud> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPerfilPuestoTrabajoEstadoSolicitud> listado = new List<TPerfilPuestoTrabajoEstadoSolicitud>();
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
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<PerfilPuestoTrabajoEstadoSolicitudDTO> Obtener()
        {
            try
            {
                List<PerfilPuestoTrabajoEstadoSolicitudDTO> rpta = new List<PerfilPuestoTrabajoEstadoSolicitudDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_PerfilPuestoTrabajoEstadoSolicitud
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PerfilPuestoTrabajoEstadoSolicitudDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>PerfilPuestoTrabajoEstadoSolicitud || null</returns>
        public PerfilPuestoTrabajoEstadoSolicitud? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PerfilPuestoTrabajoEstadoSolicitud
                    WHERE Id=@id AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PerfilPuestoTrabajoEstadoSolicitud>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PPTES-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
