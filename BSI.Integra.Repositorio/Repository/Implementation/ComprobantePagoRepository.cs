using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ComprobantePagoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_ComprobantePago
    /// </summary>
    public class ComprobantePagoRepository : GenericRepository<TComprobantePago>, IComprobantePagoRepository
    {
        private Mapper _mapper;

        public ComprobantePagoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TComprobantePago, ComprobantePago>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TComprobantePago MapeoEntidad(ComprobantePago entidad)
        {
            try
            {
                //crea la entidad padre
                TComprobantePago modelo = _mapper.Map<TComprobantePago>(entidad);

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

        public TComprobantePago Add(ComprobantePago entidad)
        {
            try
            {
                var ComprobantePago = MapeoEntidad(entidad);
                base.Insert(ComprobantePago);
                return ComprobantePago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TComprobantePago Update(ComprobantePago entidad)
        {
            try
            {
                var ComprobantePago = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ComprobantePago.RowVersion = entidadExistente.RowVersion;

                base.Update(ComprobantePago);
                return ComprobantePago;
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


        public IEnumerable<TComprobantePago> Add(IEnumerable<ComprobantePago> listadoEntidad)
        {
            try
            {
                List<TComprobantePago> listado = new List<TComprobantePago>();
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

        public IEnumerable<TComprobantePago> Update(IEnumerable<ComprobantePago> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TComprobantePago> listado = new List<TComprobantePago>();
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

        public IEnumerable<ComprobantePagoDTO> ObtenerComprobanteAutocomplete(string RucComprobanteParcial)
        {
            try
            {
                var _query = "SELECT  Id, IdProveedor,Ruc, Proveedor, Serie,Numero,IdSunatDocumento,SunatDocumento,concat(Proveedor,' ',Comprobante) as Comprobante, MontoBruto,FechaEmision FROM fin.V_ObtenerDatosComprobante WHERE concat(Proveedor,' ',Ruc,' ',Comprobante) like '%" + RucComprobanteParcial + "%' ";
                var comprobantePagoDB = _dapperRepository.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<ComprobantePagoDTO>>(comprobantePagoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SunatDocumentoDTO> ObtenerElementosSunatDocumento()
        {
            try
            {
                List<SunatDocumentoDTO> SunatDocumentos = new List<SunatDocumentoDTO>();
                var _query = "SELECT Id, Nombre FROM  fin.T_SunatDocumento WHERE Estado=1";
                var SunatDocumentoFinanzasDB = _dapperRepository.QueryDapper(_query, null);
                if (!SunatDocumentoFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(SunatDocumentoFinanzasDB))
                {
                    SunatDocumentos = JsonConvert.DeserializeObject<List<SunatDocumentoDTO>>(SunatDocumentoFinanzasDB);
                }
                return SunatDocumentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ComprobantesNoAsociadosDTO> ObtenerComprobantesNoAsociados()
        {
            try
            {
                List<ComprobantesNoAsociadosDTO> rtp = new List<ComprobantesNoAsociadosDTO>();
                var _query = @"SELECT  [Id]
                                      ,[IdProveedor]
                                      ,[RazonSocial]
                                      ,[IdPais]
                                      ,[NombrePais]
                                      ,[idSunatDocumento]
                                      ,[NombreDocumento]
                                      ,[Serie]
                                      ,[NroComprobante]
                                      ,[FechaEmision]
                                      ,[FechaProgramacion]
                                      ,[IdMoneda]
                                      ,[Moneda]
                                      ,[MontoInafecto]
                                      ,[MontoBruto]
                                      ,[OtraTazaContribucion]
                                      ,[MontoNeto]
                                      ,[AjusteMontoBruto]
                                      ,[IdTipoImpuesto]
                                      ,[MontoIgv]
                                      ,[IdRetencion]
                                      ,[valorRetencion]
                                      ,[IdDetraccion]
                                      ,[valorDetraccion]
                                      ,IdSede
                                  FROM [fin].[V_ComprobantesNoAsociados]
                                  ORDER BY [Id] DESC";
                var respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!respuesta.Contains("[]") && !string.IsNullOrEmpty(respuesta))
                {
                    rtp = JsonConvert.DeserializeObject<List<ComprobantesNoAsociadosDTO>>(respuesta);
                }
                return rtp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<ComprobantePagoAsociadoDTO> ObtenerComprobantePagoPorFur(int idFur)
        {
            try
            {
                var _query = @"SELECT  
                                    Id, 
                                    IdDocumentoPago, NombreDocumento, 
                                    SerieComprobante, NumeroComprobante, 
                                    Monto, IdMoneda, NombreMoneda, 
                                    FechaEmision, FechaProgramacion, 
                                    IdPais, NombrePais, 
                                    IdIgv, ValorIGV, 
                                    IdRetencion, ValorRetencion, 
                                    IdDetraccion, ValorDetraccion, IdProveedor,
                                    NombreProveedor,CodigoFur, MontoAfecto,
                                    MontoInafecto,OtraTazaContribucion,
                                    AjusteMontoBruto 
                                FROM fin.V_ObtenerComprobantes WHERE IdFur = @idFur AND Estado = 1";
                var comprobantePagoDB = _dapperRepository.QueryDapper(_query, new { idFur });
                return JsonConvert.DeserializeObject<List<ComprobantePagoAsociadoDTO>>(comprobantePagoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);



            }
        }


        /// <summary>
        /// Obtiene el todos los comprobantes asociados al idfur de T_ComprobantePagoPorFur para registrar el pago
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComprobantePorFurDTO> ObtenerComprobantesPorFurParaPago(int IdFur)
        {
            try
            {
                List<ComprobantePorFurDTO> ComprobantePorFur = new List<ComprobantePorFurDTO>();
                var _query = "SELECT Id, Comprobante, Proveedor, IdAsociacion, NombreAsociacion, IdMoneda, MontoAsociado, MontoAmortizar from fin.V_ObtenerComprobantesPorFurV5 where IdFur = @IdFur AND MontoAmortizar > 0 and (FurCancelado = 0 OR FurCancelado IS NULL) AND EstadoAsociacion = 1";
                var ComprobantePorFurDB = _dapperRepository.QueryDapper(_query, new { IdFur });
                if (!ComprobantePorFurDB.Contains("[]") && !string.IsNullOrEmpty(ComprobantePorFurDB))
                {
                    ComprobantePorFur = JsonConvert.DeserializeObject<List<ComprobantePorFurDTO>>(ComprobantePorFurDB);
                }
                return ComprobantePorFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// <summary>
        /// Obtiene la lista de Comprobantes asociados a un determinado Proveedor
        /// </summary>
        /// <param name="RucParcial">numero de Documneto</param>
        /// <returns></returns>
        public List<RucSerieNumeroComprobanteDTO> ObtenerComprobantePorRuc(string RucParcial)
        {
            try
            {
                List<RucSerieNumeroComprobanteDTO> comprobantes = new List<RucSerieNumeroComprobanteDTO>();
                var _query = "SELECT  Id, Comprobante, MontoNeto, IdMoneda,IdTipoImpuesto,IdRetencion,IdDetraccion,IdPais FROM fin.V_ObtenerRucSerieNumeroComprobante WHERE Comprobante like '%" + RucParcial + "%' ";
                var comprobantePagoDB = _dapperRepository.QueryDapper(_query, null);
                if (!comprobantePagoDB.Contains("[]") && !string.IsNullOrEmpty(comprobantePagoDB))
                {
                    comprobantes = JsonConvert.DeserializeObject<List<RucSerieNumeroComprobanteDTO>>(comprobantePagoDB);
                }
                return comprobantes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// <summary>
        /// Obtiene el monto utilizado de un comprobante basandose en los registros de T_ComprobantePagoPorFur
        /// </summary>
        /// <returns>List<ComprobanteMontoUtilizadoDTO> </returns>
        public List<ComprobanteMontoUtilizadoDTO> ObtenerMontoUtilizadoComprobante(int IdComprobante)
        {
            try
            {
                List<ComprobanteMontoUtilizadoDTO> ComprobanteMontoUtilizado = new List<ComprobanteMontoUtilizadoDTO>();
                var _query = "SELECT IdComprobantePago, MontoUtilizado from ( select IdComprobantePago, sum(Monto) as MontoUtilizado from fin.T_ComprobantePagoPorFur where Estado=1 group by (IdComprobantePago)  ) SumatoriaComprobante where IdComprobantePago=" + IdComprobante;
                var ComprobanteMontoUtilizadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!ComprobanteMontoUtilizadoDB.Contains("[]") && !string.IsNullOrEmpty(ComprobanteMontoUtilizadoDB))
                {
                    ComprobanteMontoUtilizado = JsonConvert.DeserializeObject<List<ComprobanteMontoUtilizadoDTO>>(ComprobanteMontoUtilizadoDB);
                }
                return ComprobanteMontoUtilizado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
