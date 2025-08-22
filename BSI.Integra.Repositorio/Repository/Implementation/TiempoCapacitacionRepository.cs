using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TiempoCapacitacionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_TiempoCapacitacion
    /// </summary>
    public class TiempoCapacitacionRepository : GenericRepository<TTiempoCapacitacion>, ITiempoCapacitacionRepository
    {
        private Mapper _mapper;

        public TiempoCapacitacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTiempoCapacitacion, TiempoCapacitacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTiempoCapacitacion MapeoEntidad(TiempoCapacitacion entidad)
        {
            try
            {
                //crea la entidad padre
                TTiempoCapacitacion modelo = _mapper.Map<TTiempoCapacitacion>(entidad);

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

        public TTiempoCapacitacion Add(TiempoCapacitacion entidad)
        {
            try
            {
                var TiempoCapacitacion = MapeoEntidad(entidad);
                base.Insert(TiempoCapacitacion);
                return TiempoCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTiempoCapacitacion Update(TiempoCapacitacion entidad)
        {
            try
            {
                var TiempoCapacitacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TiempoCapacitacion.RowVersion = entidadExistente.RowVersion;

                base.Update(TiempoCapacitacion);
                return TiempoCapacitacion;
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


        public IEnumerable<TTiempoCapacitacion> Add(IEnumerable<TiempoCapacitacion> listadoEntidad)
        {
            try
            {
                List<TTiempoCapacitacion> listado = new List<TTiempoCapacitacion>();
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

        public IEnumerable<TTiempoCapacitacion> Update(IEnumerable<TiempoCapacitacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTiempoCapacitacion> listado = new List<TTiempoCapacitacion>();
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
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TiempoCapacitacion.
        /// </summary>
        /// <returns> List<TiempoCapacitacionDTO> </returns>
        public IEnumerable<TiempoCapacitacionDTO> ObtenerTiempoCapacitacion()
        {
            try
            {
                List<TiempoCapacitacionDTO> rpta = new List<TiempoCapacitacionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM mkt.T_TiempoCapacitacion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TiempoCapacitacionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TiempoCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<TiempoCapacitacionComboDTO> </returns>
        public List<TiempoCapacitacionComboDTO> ObtenerCombo()
        {
            try
            {
                List<TiempoCapacitacionComboDTO> rpta = new List<TiempoCapacitacionComboDTO>();
                var query = @"SELECT Id, Nombre FROM mkt.T_TiempoCapacitacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TiempoCapacitacionComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TiempoCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<TiempoCapacitacionComboDTO> </returns>
        public async Task<List<TiempoCapacitacionComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<TiempoCapacitacionComboDTO> rpta = new List<TiempoCapacitacionComboDTO>();
                var query = @"SELECT Id, Nombre FROM mkt.T_TiempoCapacitacion WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TiempoCapacitacionComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
