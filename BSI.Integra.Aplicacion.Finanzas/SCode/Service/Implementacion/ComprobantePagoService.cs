using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ComprobantePagoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_ComprobantePago
    /// </summary>
    public class ComprobantePagoService : IComprobantePagoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperComprobantePago;

        public ComprobantePagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TComprobantePago, ComprobantePago>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperComprobantePago = new Mapper(config);
        }

        #region Metodos Base
        public ComprobantePago Add(ComprobantePago entidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ComprobantePago>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ComprobantePago Update(ComprobantePago entidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ComprobantePago>(modelo);
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
                _unitOfWork.ComprobantePagoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobantePago> Add(List<ComprobantePago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ComprobantePago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobantePago> Update(List<ComprobantePago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ComprobantePago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.ComprobantePagoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
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
                return _unitOfWork.ComprobantePagoRepository.ObtenerComprobanteAutocomplete(RucComprobanteParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// inserta un nuevo Comprobante de Pago
        /// </summary>
        /// <returns> true </returns>
        public ComprobantePago InsertarComprobante(ComprobantePagoInsercionDTO RequestDTO)
        {
            try
            {
                var repComprobantePago = _unitOfWork.ComprobantePagoRepository;
                using (TransactionScope scope = new TransactionScope())
                {
                    ComprobantePago ComprobantePago = new ComprobantePago();

                    var Comprobante = repComprobantePago.GetBy(x => x.IdProveedor == RequestDTO.IdProveedor
                                                                && x.SerieComprobante == RequestDTO.SerieComprobante
                                                                && x.NumeroComprobante == RequestDTO.NumeroComprobante).FirstOrDefault();

                    if (Comprobante != null)
                    {
                        throw new Exception(" COMPROBANTE-EXISTENTE: Un Comprobante ya existe con el mismo número de serie y número de comprobante ");
                    }

                    ComprobantePago.IdSunatDocumento = RequestDTO.IdSunatDocumento;
                    ComprobantePago.IdPais = RequestDTO.IdPais;
                    ComprobantePago.SerieComprobante = RequestDTO.SerieComprobante;
                    ComprobantePago.NumeroComprobante = RequestDTO.NumeroComprobante;
                    ComprobantePago.IdMoneda = RequestDTO.IdMoneda;
                    ComprobantePago.MontoBruto = RequestDTO.MontoBruto;
                    ComprobantePago.MontoInafecto = RequestDTO.MontoInafecto;
                    ComprobantePago.AjusteMontoBruto = RequestDTO.AjusteMontoBruto;
                    ComprobantePago.MontoNeto = RequestDTO.MontoNeto;
                    ComprobantePago.FechaEmision = RequestDTO.FechaEmision;
                    ComprobantePago.FechaProgramacion = RequestDTO.FechaProgramacion;
                    if (RequestDTO.IdTipoImpuesto != null)
                    {
                        ComprobantePago.IdTipoImpuesto = RequestDTO.IdTipoImpuesto;
                        ComprobantePago.PorcentajeIgv = RequestDTO.PorcentajeIgv;
                        ComprobantePago.MontoIgv = RequestDTO.MontoIgv;

                    }
                    else
                    {
                        ComprobantePago.IdTipoImpuesto = null;
                        ComprobantePago.PorcentajeIgv = null;
                        ComprobantePago.MontoIgv = null;
                    }
                    ComprobantePago.IdRetencion = RequestDTO.IdRetencion;
                    ComprobantePago.IdDetraccion = RequestDTO.IdDetraccion;
                    ComprobantePago.OtraTazaContribucion = RequestDTO.OtraTazaContribucion;
                    ComprobantePago.IdProveedor = RequestDTO.IdProveedor;
                    ComprobantePago.Estado = true;
                    ComprobantePago.UsuarioCreacion = RequestDTO.Usuario;
                    ComprobantePago.UsuarioModificacion = RequestDTO.Usuario;
                    ComprobantePago.FechaCreacion = DateTime.Now;
                    ComprobantePago.FechaModificacion = DateTime.Now;
                    ComprobantePago.IdEmpresa = RequestDTO.IdEmpresa;
                    ComprobantePago.IdCiudad = RequestDTO.IdCiudad;

                    ComprobantePago = this.Add(ComprobantePago);
                    scope.Complete();
                    return ComprobantePago;
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public ComprobantePago ActualizarComprobante(ComprobantePagoInsercionDTO Comprobante)
        {

            try
            {
                var _repComprobantePago = _unitOfWork.ComprobantePagoRepository;
                ComprobantePago comprobantePago = new ComprobantePago();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repComprobantePago.Exist(Comprobante.Id))
                    {
                        comprobantePago = _mapper.Map<ComprobantePago>(_repComprobantePago.FirstById(Comprobante.Id));
                        comprobantePago.IdSunatDocumento = Comprobante.IdSunatDocumento;
                        comprobantePago.IdPais = Comprobante.IdPais;
                        comprobantePago.SerieComprobante = Comprobante.SerieComprobante;
                        comprobantePago.NumeroComprobante = Comprobante.NumeroComprobante;
                        comprobantePago.IdMoneda = Comprobante.IdMoneda;
                        comprobantePago.MontoNeto = Comprobante.MontoNeto;
                        comprobantePago.MontoBruto = Comprobante.MontoBruto;
                        comprobantePago.MontoInafecto = Comprobante.MontoInafecto;
                        comprobantePago.PorcentajeIgv = Comprobante.PorcentajeIgv;
                        comprobantePago.MontoIgv = Comprobante.MontoIgv;
                        comprobantePago.AjusteMontoBruto = Comprobante.AjusteMontoBruto;
                        comprobantePago.FechaEmision = Comprobante.FechaEmision;
                        comprobantePago.FechaProgramacion = Comprobante.FechaProgramacion;
                        comprobantePago.IdTipoImpuesto = Comprobante.IdTipoImpuesto;
                        comprobantePago.IdRetencion = Comprobante.IdRetencion;
                        comprobantePago.IdDetraccion = Comprobante.IdDetraccion;
                        comprobantePago.OtraTazaContribucion = Comprobante.OtraTazaContribucion;
                        comprobantePago.IdProveedor = Comprobante.IdProveedor;
                        comprobantePago.Estado = true;
                        comprobantePago.UsuarioModificacion = Comprobante.Usuario;
                        comprobantePago.FechaModificacion = DateTime.Now;
                        comprobantePago.IdEmpresa = Comprobante.IdEmpresa;
                        comprobantePago.IdCiudad = Comprobante.IdCiudad;

                        comprobantePago = this.Update(comprobantePago);
                        scope.Complete();


                    }
                    else throw new Exception("La entidad no existe");
                }
                return comprobantePago;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// retorna un combo de Documentos de SUNAT
        /// </summary>
        /// <returns> true </returns>
        public IEnumerable<SunatDocumentoDTO> ObtenerElementosSunatDocumento()
        {
            try
            {
                return _unitOfWork.ComprobantePagoRepository.ObtenerElementosSunatDocumento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// <summary>
        /// Obtiene la lista de Comprobantes asociados a un determinado Proveedor
        /// </summary>
        /// <param name="RucParcial">numero de Documneto</param>
        /// <returns> List<RucSerieNumeroComprobanteDTO>  </returns>
        public List<RucSerieNumeroComprobanteDTO> ObtenerComprobantePorRuc(string RucParcial)
        {
            try
            {
                return _unitOfWork.ComprobantePagoRepository.ObtenerComprobantePorRuc(RucParcial);
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _unitOfWork.ComprobantePagoRepository.ObtenerMontoUtilizadoComprobante(IdComprobante);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// retorna un combo de Documentos de SUNAT
        /// </summary>
        /// <returns> true </returns>
        public IEnumerable<ComprobantesNoAsociadosDTO> ObtenerComprobantesNoAsociados()
        {
            try
            {
                return _unitOfWork.ComprobantePagoRepository.ObtenerComprobantesNoAsociados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// retorna comprobantes asociados a un fur
        /// </summary>
        /// <returns> true </returns>
        public IEnumerable<ComprobantePagoAsociadoDTO> ObtenerComprobantePagoPorFur(int idFur)
        {
            try
            {
                return _unitOfWork.ComprobantePagoRepository.ObtenerComprobantePagoPorFur(idFur);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// retorna comprobantes asociados a un fur y montos 
        /// </summary>
        /// <returns> ComprobantePorFurDTO </returns>
        public IEnumerable<ComprobantePorFurDTO> ObtenerComprobantesPorFurParaPago(int IdFur)
        {
            try
            {
                return _unitOfWork.ComprobantePagoRepository.ObtenerComprobantesPorFurParaPago(IdFur);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
