using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SentinelSdtPoshisItemRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtPoshisItem
    /// </summary>
    public class SentinelSdtPoshisItemRepository : GenericRepository<TSentinelSdtPoshisItem>, ISentinelSdtPoshisItemRepository
    {
        private Mapper _mapper;

        public SentinelSdtPoshisItemRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSdtPoshisItem, SentinelSdtPoshisItem>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSdtPoshisItem MapeoEntidad(SentinelSdtPoshisItem entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtPoshisItem modelo = _mapper.Map<TSentinelSdtPoshisItem>(entidad);

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

        public TSentinelSdtPoshisItem Add(SentinelSdtPoshisItem entidad)
        {
            try
            {
                var SentinelSdtPoshisItem = MapeoEntidad(entidad);
                base.Insert(SentinelSdtPoshisItem);
                return SentinelSdtPoshisItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSdtPoshisItem Update(SentinelSdtPoshisItem entidad)
        {
            try
            {
                var SentinelSdtPoshisItem = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSdtPoshisItem.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSdtPoshisItem);
                return SentinelSdtPoshisItem;
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


        public IEnumerable<TSentinelSdtPoshisItem> Add(IEnumerable<SentinelSdtPoshisItem> listadoEntidad)
        {
            try
            {
                List<TSentinelSdtPoshisItem> listado = new List<TSentinelSdtPoshisItem>();
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

        public IEnumerable<TSentinelSdtPoshisItem> Update(IEnumerable<SentinelSdtPoshisItem> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSdtPoshisItem> listado = new List<TSentinelSdtPoshisItem>();
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
        /// Obtiene todos los registros de T_SentinelSdtPoshisItem.
        /// </summary>
        /// <returns> List<SentinelSdtPoshisItemDTO> </returns>
        public IEnumerable<SentinelSdtPoshisItemDTO> ObtenerSentinelSdtPoshisItem()
        {
            try
            {
                List<SentinelSdtPoshisItemDTO> rpta = new List<SentinelSdtPoshisItemDTO>();
                var query = @"
                    SELECT
	                    Id,IdSentinel,TipoDocumento,NumeroDocumento,FechaProceso,SemanaActual,DescripcionSemaforo,Score,CodigoVariacion,DescripcionVariacion,NumeroEntidades,
	                    DeudaTotal,PorcentajeCalificacion,PeorCalificacion,PeroCalificacionDescripcion,MontoSBS,ProgresoRegistro,DocImpuesto,DeudaTributaria,AFP,TarjetaCredito,
	                    CuentaCorriente,ReporteNegativo,DeudaDirecta,DeudaIndirecta,LineaCreditoNoUtilizada,DeudaCastigada,UsuarioCreacion,UsuarioModificacion,FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSdtPoshisItem
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtPoshisItemDTO>>(resultado);
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
        /// Obtiene registros de T_SentinelSdtPoshisItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtPoshisItemComboDTO> </returns>
        public IEnumerable<SentinelSdtPoshisItemComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSdtPoshisItemComboDTO> rpta = new List<SentinelSdtPoshisItemComboDTO>();
                var query = @"SELECT Id,NumeroDocumento,FechaProceso FROM com.T_SentinelSdtPoshisItem WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtPoshisItemComboDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene los registros de T_SentinelSdtPoshisItem relacionados al IdSentinel.
        /// </summary>
        /// <param name=idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtPoshisItemDTO> </returns>
        public IEnumerable<SentinelSdtPoshisItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelSdtPoshisItemDTO> rpta = new List<SentinelSdtPoshisItemDTO>();
                var query = @"
                    SELECT
	                    Id,IdSentinel,TipoDocumento,NumeroDocumento,FechaProceso,SemanaActual,DescripcionSemaforo,Score,CodigoVariacion,DescripcionVariacion,NumeroEntidades,
	                    DeudaTotal,PorcentajeCalificacion,PeorCalificacion,PeroCalificacionDescripcion,MontoSBS,ProgresoRegistro,DocImpuesto,DeudaTributaria,AFP,TarjetaCredito,
	                    CuentaCorriente,ReporteNegativo,DeudaDirecta,DeudaIndirecta,LineaCreditoNoUtilizada,DeudaCastigada,UsuarioCreacion,UsuarioModificacion,FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSdtPoshisItem
                    WHERE Estado = 1 AND IdSentinel = @idSentinel";
                var resultado = _dapperRepository.QueryDapper(query, new { idSentinel });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtPoshisItemDTO>>(resultado);
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
        /// Obtiene los registros de T_SentinelSdtPoshisItem relacionados al IdSentinel.
        /// </summary>
        /// <param name=idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtPoshisItemDTO> </returns>
        public List<SentinelSdtPoshisItemPosicionHistoriaDTO> ObtenerPosicionHistoria(int idSentinel)
        {
            try
            {
                string _queryPosicionHitorica = "select FechaProceso,SemanaActual,Score,CodigoVariacion,NumeroEntidades,DeudaTotal,ProgresoRegistro,DocImpuesto,DeudaTributaria,DeudaLab" +
                                                "  ,CuentaCorriente,TarjetaCredito,ReporteNegativo,PorcentajeCalificacion,PeorCalificacion  " +
                                                " from com.V_TSentinelSdtPoshisItem_PosocionHistoria Where IdSentinel=@IdSentinel AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryPosicionHistorica = _dapperRepository.QueryDapper(_queryPosicionHitorica, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtPoshisItemPosicionHistoriaDTO>>(queryPosicionHistorica);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
