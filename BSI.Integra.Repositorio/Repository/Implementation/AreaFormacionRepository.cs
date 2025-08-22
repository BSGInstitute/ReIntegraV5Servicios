using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AreaFormacionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_AreaFormacion
    /// </summary>
    public class AreaFormacionRepository : GenericRepository<TAreaFormacion>, IAreaFormacionRepository
    {
        private Mapper _mapper;

        public AreaFormacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAreaFormacion, AreaFormacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAreaFormacion MapeoEntidad(AreaFormacion entidad)
        {
            try
            {
                //crea la entidad padre
                TAreaFormacion modelo = _mapper.Map<TAreaFormacion>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAreaFormacion Add(AreaFormacion entidad)
        {
            try
            {
                var AreaFormacion = MapeoEntidad(entidad);
                base.Insert(AreaFormacion);
                return AreaFormacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAreaFormacion Update(AreaFormacion entidad)
        {
            try
            {
                var AreaFormacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AreaFormacion.RowVersion = entidadExistente.RowVersion;

                base.Update(AreaFormacion);
                return AreaFormacion;
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


        public IEnumerable<TAreaFormacion> Add(IEnumerable<AreaFormacion> listadoEntidad)
        {
            try
            {
                List<TAreaFormacion> listado = new List<TAreaFormacion>();
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

        public IEnumerable<TAreaFormacion> Update(IEnumerable<AreaFormacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAreaFormacion> listado = new List<TAreaFormacion>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaFormacion.
        /// </summary>
        /// <returns> List<AreaFormacionDTO> </returns>
        public IEnumerable<AreaFormacionDTO> ObtenerAreaFormacion()
        {
            try
            {
                List<AreaFormacionDTO> rpta = new List<AreaFormacionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion
                    FROM pla.T_AreaFormacion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AreaFormacionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaFormacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> respuesta = new List<ComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_AreaFormacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id,Nombre FROM pla.T_AreaFormacion WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null); if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerAreaFormacionFiltro()
        {
            try
            {
                List<ComboDTO> respuesta = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM pla.V_TAreaFormacion_ObtenerIdNombre WHERE  Estado = 1 Order By Id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public AreaFormacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
                           Nombre,
                           Estado,
                           FechaCreacion,
                           FechaModificacion,
                           UsuarioCreacion,
                           UsuarioModificacion,
                           RowVersion,
                           IdMigracion,
                           Peso 
                    FROM pla.T_AreaFormacion 
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<AreaFormacion>(resultado)!;
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
