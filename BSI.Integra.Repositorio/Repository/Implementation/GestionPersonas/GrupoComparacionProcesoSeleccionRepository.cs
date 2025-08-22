using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class GrupoComparacionProcesoSeleccionRepository : GenericRepository<TGrupoComparacionProcesoSeleccion>, IGrupoComparacionProcesoSeleccionRepository
    {
        private Mapper _mapper;
        public GrupoComparacionProcesoSeleccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccion>(MemberList.None).ReverseMap();
                cfg.CreateMap<GrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<GrupoComparacionProcesoSeleccion, TGrupoComparacionProcesoSeleccion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGrupoComparacionProcesoSeleccion MapeoEntidad(GrupoComparacionProcesoSeleccion entidad)
        {
            try
            {
                TGrupoComparacionProcesoSeleccion modelo = _mapper.Map<TGrupoComparacionProcesoSeleccion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TGrupoComparacionProcesoSeleccion Add(GrupoComparacionProcesoSeleccion entidad)
        {
            try
            {
                var GrupoComparacionProcesoSeleccion = MapeoEntidad(entidad);
                base.Insert(GrupoComparacionProcesoSeleccion);
                return GrupoComparacionProcesoSeleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TGrupoComparacionProcesoSeleccion Update(GrupoComparacionProcesoSeleccion entidad)
        {
            try
            {
                var GrupoComparacionProcesoSeleccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GrupoComparacionProcesoSeleccion.RowVersion = entidadExistente.RowVersion;

                base.Update(GrupoComparacionProcesoSeleccion);
                return GrupoComparacionProcesoSeleccion;
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
        public IEnumerable<TGrupoComparacionProcesoSeleccion> Add(IEnumerable<GrupoComparacionProcesoSeleccion> listadoEntidad)
        {
            try
            {
                List<TGrupoComparacionProcesoSeleccion> listado = new List<TGrupoComparacionProcesoSeleccion>();
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
        public IEnumerable<TGrupoComparacionProcesoSeleccion> Update(IEnumerable<GrupoComparacionProcesoSeleccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGrupoComparacionProcesoSeleccion> listado = new List<TGrupoComparacionProcesoSeleccion>();
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
        public IEnumerable<GrupoComparacionProcesoSeleccionDetalleDTO> ObtenerDetalle()
        {
            try
            {
                List<GrupoComparacionProcesoSeleccionDetalleDTO> rpta = new();
                var query = @"
                    SELECT Id, Nombre, IdPuestoTrabajo, IdSedeTrabajo, IdPostulante
                    FROM gp.V_GrupoComparacionProsesoSeleccion
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<GrupoComparacionProcesoSeleccionDetalleDTO>>(resultado)!;
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
        /// <returns>GrupoComparacionProcesoSeleccion || null</returns>
        public GrupoComparacionProcesoSeleccion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id, Nombre, IdPuestoTrabajo, IdSedeTrabajo, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion
                    FROM gp.T_GrupoComparacionProcesoSeleccion
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<GrupoComparacionProcesoSeleccion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EPS-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene los registros para combos
        /// </summary>
        /// <returns>GrupoComparacionProcesoSeleccion || null</returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre
                    FROM gp.T_GrupoComparacionProcesoSeleccion
                    WHERE Estado=1 ORDER BY Nombre ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(), {ex.Message}");
            }
        }

        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene los registros para combos
        /// </summary>
        /// <returns>GrupoComparacionProcesoSeleccion || null</returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre
                    FROM gp.T_GrupoComparacionProcesoSeleccion
                    WHERE Estado=1 ORDER BY Nombre ASC";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(), {ex.Message}");
            }
        }
    }
}
