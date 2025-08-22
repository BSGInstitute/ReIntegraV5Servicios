using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class MensajeTiempoInactivoRepository : GenericRepository<TMensajeTiempoInactivo>, IMensajeTiempoInactivoRepository
    {
        private Mapper _mapper;
        public MensajeTiempoInactivoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMensajeTiempoInactivo, MensajeTiempoInactivo>(MemberList.None).ReverseMap();
                cfg.CreateMap<MensajeTiempoInactivo, MensajeTiempoInactivoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<MensajeTiempoInactivo, TMensajeTiempoInactivo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMensajeTiempoInactivo MapeoEntidad(MensajeTiempoInactivo entidad)
        {
            try
            {
                TMensajeTiempoInactivo modelo = _mapper.Map<TMensajeTiempoInactivo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMensajeTiempoInactivo Add(MensajeTiempoInactivo entidad)
        {
            try
            {
                var MensajeTiempoInactivo = MapeoEntidad(entidad);
                base.Insert(MensajeTiempoInactivo);
                return MensajeTiempoInactivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMensajeTiempoInactivo Update(MensajeTiempoInactivo entidad)
        {
            try
            {
                var MensajeTiempoInactivo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MensajeTiempoInactivo.RowVersion = entidadExistente.RowVersion;

                base.Update(MensajeTiempoInactivo);
                return MensajeTiempoInactivo;
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
        public IEnumerable<TMensajeTiempoInactivo> Add(IEnumerable<MensajeTiempoInactivo> listadoEntidad)
        {
            try
            {
                List<TMensajeTiempoInactivo> listado = new List<TMensajeTiempoInactivo>();
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
        public IEnumerable<TMensajeTiempoInactivo> Update(IEnumerable<MensajeTiempoInactivo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMensajeTiempoInactivo> listado = new List<TMensajeTiempoInactivo>();
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
        public IEnumerable<MensajeTiempoInactivoDTO> Obtener()
        {
            try
            {
                List<MensajeTiempoInactivoDTO> rpta = new List<MensajeTiempoInactivoDTO>();
                var query = @"
                    SELECT
	                    Id,Mensaje,MinutoInactivo
                    FROM gp.T_MensajeTiempoInactivo
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MensajeTiempoInactivoDTO>>(resultado);

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
        /// <returns>MensajeTiempoInactivo || null</returns>
        public MensajeTiempoInactivo? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Mensaje,
                        MinutoInactivo,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_MensajeTiempoInactivo
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MensajeTiempoInactivo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EPS-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
