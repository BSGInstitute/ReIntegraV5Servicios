using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FlujoRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_Flujo
    /// </summary>
    public class FlujoRepository : GenericRepository<TFlujo>, IFlujoRepository
    {
        private Mapper _mapper;

        public FlujoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFlujo, Flujo>(MemberList.None).ReverseMap();
                cfg.CreateMap<Flujo, TFlujo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFlujo MapeoEntidad(Flujo entidad)
        {
            try
            {
                TFlujo modelo = _mapper.Map<TFlujo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujo Add(Flujo entidad)
        {
            try
            {
                var Flujo = MapeoEntidad(entidad);
                base.Insert(Flujo);
                return Flujo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujo Update(Flujo entidad)
        {
            try
            {
                var Flujo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Flujo.RowVersion = entidadExistente.RowVersion;

                base.Update(Flujo);
                return Flujo;
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
        public IEnumerable<TFlujo> Add(IEnumerable<Flujo> listadoEntidad)
        {
            try
            {
                List<TFlujo> listado = new List<TFlujo>();
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
        public IEnumerable<TFlujo> Update(IEnumerable<Flujo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFlujo> listado = new List<TFlujo>();
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
        /// Obtiene Flujo por id.
        /// </summary>
        /// <returns>Flujo</returns>
        public Flujo? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdModalidadCurso,
		                IdClasificacionUbicacionDocente,
		                Nombre,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM ope.T_Flujo
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Flujo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Christian Quispe.
        /// Fecha: 4/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Flujo con sus respectivas relaciones
        /// </summary>
        /// <returns> List<FlujoDetalleDTO> </returns>
        public IEnumerable<FlujoDetalleDTO> Obtener()
        {
            try
            {
                List<FlujoDetalleDTO> rpta = new List<FlujoDetalleDTO>();
                var query = @"
                    SELECT Id, IdModalidadCurso, NombreModalidadCurso, IdClasificacionUbicacionDocente, NombreClasificacionUbicacionDocente, NombreFlujo
                    FROM ope.V_ObtenerFlujoDetalleModalidadCursoYClasificacionUbicacionDocente";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<FlujoDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Quispe.
        /// Fecha: 4/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los
        /// </summary>
        /// <returns> List<PartnerPwDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerComboClasificacionUbicacionDocente()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM pla.T_ClasificacionUbicacionDocente WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
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



