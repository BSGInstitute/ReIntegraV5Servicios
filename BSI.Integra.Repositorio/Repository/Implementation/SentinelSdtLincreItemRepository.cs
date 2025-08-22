using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SentinelSdtLincreItemRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtLincreItem
    /// </summary>
    public class SentinelSdtLincreItemRepository : GenericRepository<TSentinelSdtLincreItem>, ISentinelSdtLincreItemRepository
    {
        private Mapper _mapper;

        public SentinelSdtLincreItemRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSdtLincreItem, SentinelSdtLincreItem>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSdtLincreItem MapeoEntidad(SentinelSdtLincreItem entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtLincreItem modelo = _mapper.Map<TSentinelSdtLincreItem>(entidad);

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

        public TSentinelSdtLincreItem Add(SentinelSdtLincreItem entidad)
        {
            try
            {
                var SentinelSdtLincreItem = MapeoEntidad(entidad);
                base.Insert(SentinelSdtLincreItem);
                return SentinelSdtLincreItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSdtLincreItem Update(SentinelSdtLincreItem entidad)
        {
            try
            {
                var SentinelSdtLincreItem = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSdtLincreItem.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSdtLincreItem);
                return SentinelSdtLincreItem;
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


        public IEnumerable<TSentinelSdtLincreItem> Add(IEnumerable<SentinelSdtLincreItem> listadoEntidad)
        {
            try
            {
                List<TSentinelSdtLincreItem> listado = new List<TSentinelSdtLincreItem>();
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

        public IEnumerable<TSentinelSdtLincreItem> Update(IEnumerable<SentinelSdtLincreItem> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSdtLincreItem> listado = new List<TSentinelSdtLincreItem>();
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
        /// Fecha: 15/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SentinelSdtLincreItem.
        /// </summary>
        /// <returns> List<SentinelSdtLincreItemDTO> </returns>
        public IEnumerable<SentinelSdtLincreItemDTO> ObtenerSentinelSdtLincreItem()
        {
            try
            {
                List<SentinelSdtLincreItemDTO> rpta = new List<SentinelSdtLincreItemDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdSentinel,
	                    TipoDocumento,
	                    NumeroDocumento,
	                    CnsEntNomRazLN,
	                    TipoCuenta,
	                    LineaCredito,
	                    LineaCreditoNoUtil,
	                    LineaUtil,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSdtLincreItem
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtLincreItemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSdtLincreItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtLincreItemComboDTO> </returns>
        public IEnumerable<SentinelSdtLincreItemComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSdtLincreItemComboDTO> rpta = new List<SentinelSdtLincreItemComboDTO>();
                var query = @"SELECT Id,NumeroDocumento,CnsEntNomRazLN FROM com.T_SentinelSdtLincreItem WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtLincreItemComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSdtLincreItem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelLineaCreditoDatosAlumnoDTO> </returns>
        public IEnumerable<SentinelLineaCreditoDatosAlumnoDTO> ObtenerLineaCreditoPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelLineaCreditoDatosAlumnoDTO> lineaCredito = new List<SentinelLineaCreditoDatosAlumnoDTO>();
                var query = @" Select Id,IdSentinel,TipoDocumento,NumeroDocumento,CnsEntNomRazLn,TipoCuenta,LineaCredito,LineaCreditoNoUtil, 
                     LineaUtil From com.V_TSentinelSdtLincreItem_DatosAlumno Where IdSentinel=@IdSentinel and TipoDocumento='D'  AND Estado = 1 ORDER BY CnsEntNomRazLn, FechaCreacion desc";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { IdSentinel = idSentinel });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    lineaCredito = JsonConvert.DeserializeObject<List<SentinelLineaCreditoDatosAlumnoDTO>>(resultadoQuery)!;
                }
                return lineaCredito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SentinelSdtLincreItem asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtLincreItemDTO> </returns>
        public IEnumerable<SentinelSdtLincreItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelSdtLincreItemDTO> rpta = new List<SentinelSdtLincreItemDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdSentinel,
	                    TipoDocumento,
	                    NumeroDocumento,
	                    CnsEntNomRazLN,
	                    TipoCuenta,
	                    LineaCredito,
	                    LineaCreditoNoUtil,
	                    LineaUtil,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSdtLincreItem
                    WHERE Estado = 1 AND IdSentinel = @idSentinel";
                var resultado = _dapperRepository.QueryDapper(query, new { idSentinel });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtLincreItemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SentinelSdtLincreItem asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtLincreItemDTO> </returns>
        public List<AlumnosSentinelLineasCreditoDTO> ObtenerLineaDeCredito(int idSentinel)
        {
            try
            {
                string _queryLineaCredito = "Select Id,IdSentinel,TipoDocumento,NumeroDocumento,CnsEntNomRazLn,TipoCuenta,LineaCredito,LineaCreditoNoUtil," +
                    "LineaUtil From com.V_TSentinelSdtLincreItem_DatosAlumno Where IdSentinel=@IdSentinel and TipoDocumento='D'  AND Estado = 1 ORDER BY CnsEntNomRazLn, FechaCreacion desc";
                var queryLineaCredito = _dapperRepository.QueryDapper(_queryLineaCredito, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<AlumnosSentinelLineasCreditoDTO>>(queryLineaCredito);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
