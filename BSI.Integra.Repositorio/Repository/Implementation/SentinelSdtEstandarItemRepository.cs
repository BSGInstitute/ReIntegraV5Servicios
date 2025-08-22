using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SentinelSdtEstandarItemRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtEstandarItem
    /// </summary>
    public class SentinelSdtEstandarItemRepository : GenericRepository<TSentinelSdtEstandarItem>, ISentinelSdtEstandarItemRepository
    {
        private Mapper _mapper;

        public SentinelSdtEstandarItemRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSdtEstandarItem, SentinelSdtEstandarItem>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSdtEstandarItem MapeoEntidad(SentinelSdtEstandarItem entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtEstandarItem modelo = _mapper.Map<TSentinelSdtEstandarItem>(entidad);

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

        public TSentinelSdtEstandarItem Add(SentinelSdtEstandarItem entidad)
        {
            try
            {
                var SentinelSdtEstandarItem = MapeoEntidad(entidad);
                base.Insert(SentinelSdtEstandarItem);
                return SentinelSdtEstandarItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSdtEstandarItem Update(SentinelSdtEstandarItem entidad)
        {
            try
            {
                var SentinelSdtEstandarItem = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSdtEstandarItem.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSdtEstandarItem);
                return SentinelSdtEstandarItem;
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


        public IEnumerable<TSentinelSdtEstandarItem> Add(IEnumerable<SentinelSdtEstandarItem> listadoEntidad)
        {
            try
            {
                List<TSentinelSdtEstandarItem> listado = new List<TSentinelSdtEstandarItem>();
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

        public IEnumerable<TSentinelSdtEstandarItem> Update(IEnumerable<SentinelSdtEstandarItem> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSdtEstandarItem> listado = new List<TSentinelSdtEstandarItem>();
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
        /// Obtiene todos los registros de T_SentinelSdtEstandarItem.
        /// </summary>
        /// <returns> List<SentinelSdtEstandarItemDTO> </returns>
        public IEnumerable<SentinelSdtEstandarItemDTO> ObtenerSentinelSdtEstandarItem()
        {
            try
            {
                List<SentinelSdtEstandarItemDTO> rpta = new List<SentinelSdtEstandarItemDTO>();
                var query = @"
                    SELECT
	                    Id,IdSentinel,TipoDocumento,Documento,RazonSocial,FechaProceso,Semaforos,Score,NroBancos,DeudaTotal,VencidoBanco,Calificativo,Veces24m,SemanaActual,
	                    SemanaPrevio,SemanaPeorMejor,Documento2,EstadoDomicilio,CondicionDomicilio,DeudaTributaria,DeudaLaboral,DeudaImpagable,DeudaProtestos,DeudaSBS,
	                    CuentasTarjetas,ReporteNegativo,TipoActividad,FechaInicioActividad,DeudaDirecta,DeudaIndirecta,DeudaCastigada,LineaCreditoNoUtilizada,TotalRiesgo,
	                    PeorCalificacion,PorcentajeCalificacionNormal,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_SentinelSdtEstandarItem
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtEstandarItemDTO>>(resultado);
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
        /// Obtiene registros de T_SentinelSdtEstandarItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtEstandarItemComboDTO> </returns>
        public IEnumerable<SentinelSdtEstandarItemComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSdtEstandarItemComboDTO> rpta = new List<SentinelSdtEstandarItemComboDTO>();
                var query = @"SELECT Id,Documento,RazonSocial FROM com.T_SentinelSdtEstandarItem WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtEstandarItemComboDTO>>(resultado);
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
        /// Obtiene los registros de T_SentinelSdtEstandarItem asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id del centro de costo</param>
        /// <returns> List<SentinelSdtEstandarItemDTO> </returns>
        public IEnumerable<SentinelSdtEstandarItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelSdtEstandarItemDTO> rpta = new List<SentinelSdtEstandarItemDTO>();
                var query = @"
                    SELECT
	                    Id,IdSentinel,TipoDocumento,Documento,RazonSocial,FechaProceso,Semaforos,Score,NroBancos,DeudaTotal,VencidoBanco,Calificativo,Veces24m,SemanaActual,
	                    SemanaPrevio,SemanaPeorMejor,Documento2,EstadoDomicilio,CondicionDomicilio,DeudaTributaria,DeudaLaboral,DeudaImpagable,DeudaProtestos,DeudaSBS,
	                    CuentasTarjetas,ReporteNegativo,TipoActividad,FechaInicioActividad,DeudaDirecta,DeudaIndirecta,DeudaCastigada,LineaCreditoNoUtilizada,TotalRiesgo,
	                    PeorCalificacion,PorcentajeCalificacionNormal,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_SentinelSdtEstandarItem
                    WHERE Estado = 1 AND IdSentinel = @idSentinel";
                var resultado = _dapperRepository.QueryDapper(query, new { idSentinel });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtEstandarItemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SentinelSdtEstandarItem asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id del centro de costo</param>
        /// <returns> List<SentinelSdtEstandarItemDTO> </returns>
        public List<SentinelSdtEstandarItemDniRucDTO> ObtenerDniRucSentinel(int idSentinel)
        {
            try
            {
                string _queryDniRuc = "Select FechaProceso,TipoDocumento, Documento,RazonSocial,Score,DeudaTotal, SemanaActual,SemanaPrevio,Semaforos From com.V_TSentinelSdtEstandarItem_DniRuc Where IdSentinel=@IdSentinel and Estado = 1 ORDER BY FechaCreacion desc";
                var queryDniRuc = _dapperRepository.QueryDapper(_queryDniRuc, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtEstandarItemDniRucDTO>>(queryDniRuc);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
