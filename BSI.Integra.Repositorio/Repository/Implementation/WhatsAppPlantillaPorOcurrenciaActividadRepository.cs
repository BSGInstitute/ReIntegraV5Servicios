using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppPlantillaPorOcurrenciaActividadRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/07/2022
    /// <summary>
    /// Gestión general de T_WhatsAppPlantillaPorOcurrenciaActividad
    /// </summary>
    public class WhatsAppPlantillaPorOcurrenciaActividadRepository : GenericRepository<TWhatsAppPlantillaPorOcurrenciaActividad>, IWhatsAppPlantillaPorOcurrenciaActividadRepository
    {
        private Mapper _mapper;

        public WhatsAppPlantillaPorOcurrenciaActividadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppPlantillaPorOcurrenciaActividad, WhatsAppPlantillaPorOcurrenciaActividad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppPlantillaPorOcurrenciaActividad MapeoEntidad(WhatsAppPlantillaPorOcurrenciaActividad entidad)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppPlantillaPorOcurrenciaActividad modelo = _mapper.Map<TWhatsAppPlantillaPorOcurrenciaActividad>(entidad);

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

        public TWhatsAppPlantillaPorOcurrenciaActividad Add(WhatsAppPlantillaPorOcurrenciaActividad entidad)
        {
            try
            {
                var WhatsAppPlantillaPorOcurrenciaActividad = MapeoEntidad(entidad);
                base.Insert(WhatsAppPlantillaPorOcurrenciaActividad);
                return WhatsAppPlantillaPorOcurrenciaActividad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppPlantillaPorOcurrenciaActividad Update(WhatsAppPlantillaPorOcurrenciaActividad entidad)
        {
            try
            {
                var WhatsAppPlantillaPorOcurrenciaActividad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppPlantillaPorOcurrenciaActividad.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppPlantillaPorOcurrenciaActividad);
                return WhatsAppPlantillaPorOcurrenciaActividad;
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


        public IEnumerable<TWhatsAppPlantillaPorOcurrenciaActividad> Add(IEnumerable<WhatsAppPlantillaPorOcurrenciaActividad> listadoEntidad)
        {
            try
            {
                List<TWhatsAppPlantillaPorOcurrenciaActividad> listado = new List<TWhatsAppPlantillaPorOcurrenciaActividad>();
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

        public IEnumerable<TWhatsAppPlantillaPorOcurrenciaActividad> Update(IEnumerable<WhatsAppPlantillaPorOcurrenciaActividad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppPlantillaPorOcurrenciaActividad> listado = new List<TWhatsAppPlantillaPorOcurrenciaActividad>();
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
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_WhatsAppPlantillaPorOcurrenciaActividad.
        /// </summary>
        /// <returns> List<WhatsAppPlantillaPorOcurrenciaActividadDTO> </returns>
        public IEnumerable<WhatsAppPlantillaPorOcurrenciaActividad> ObtenerWhatsAppPlantillaPorOcurrenciaActividad()
        {
            try
            {
                List<WhatsAppPlantillaPorOcurrenciaActividad> rpta = new List<WhatsAppPlantillaPorOcurrenciaActividad>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOcurrenciaActividad,
	                    IdPlantilla,
	                    NumeroDiasSinContacto,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_WhatsAppPlantillaPorOcurrenciaActividad
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<WhatsAppPlantillaPorOcurrenciaActividad>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_WhatsAppPlantillaPorOcurrenciaActividad para mostrarse en combo.
        /// </summary>
        /// <returns> List<WhatsAppPlantillaPorOcurrenciaActividadComboDTO> </returns>
        public IEnumerable<WhatsAppPlantillaPorOcurrenciaActividadComboDTO> ObtenerCombo()
        {
            try
            {
                List<WhatsAppPlantillaPorOcurrenciaActividadComboDTO> rpta = new List<WhatsAppPlantillaPorOcurrenciaActividadComboDTO>();
                var query = @"
                    SELECT
	                    WPOA.Id,
	                    WPOA.IdOcurrenciaActividad,
	                    P.Nombre AS Plantilla
                    FROM com.T_WhatsAppPlantillaPorOcurrenciaActividad AS WPOA
                    INNER JOIN mkt.T_Plantilla AS P
	                    ON WPOA.IdPlantilla = P.Id
	                    AND P.Estado = 1
                    WHERE WPOA.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<WhatsAppPlantillaPorOcurrenciaActividadComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Asociacion basado en la Actividad Ocurrencia.
        /// </summary>
        /// <param name="idActividadOcurrencia">Id Contacto</param>
        /// <returns> List<WhatsAppPlantillaPorOcurrenciaActividadSinAuditoriaDTO> </returns>
        public List<WhatsAppPlantillaPorOcurrenciaActividadDTO> ObtenerPorIdOcurrenciaActividad(int idActividadOcurrencia)
        {
            try
            {
                List<WhatsAppPlantillaPorOcurrenciaActividadDTO> rpta = new List<WhatsAppPlantillaPorOcurrenciaActividadDTO>();
                var query = @"
                    SELECT IdOcurrenciaActividad, IdPlantilla, NumeroDiasSinContacto
                    FROM com.V_TWhatsAppPlantillaPorOcurrenciaActividad_PorIdOcurrenciaActividad
                    WHERE Estado = 1 AND IdOcurrenciaActividad = @IdActividadOcurrencia";
                var resultado = _dapperRepository.QueryDapper(query, new { IdActividadOcurrencia = idActividadOcurrencia });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<WhatsAppPlantillaPorOcurrenciaActividadDTO>>(resultado);
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
