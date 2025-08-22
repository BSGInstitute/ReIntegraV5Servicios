using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CajaPorRendirService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_CajaPorRendir
    /// </summary>
    public class CajaPorRendirService : ICajaPorRendirService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CajaPorRendirService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TCajaPorRendir, CajaPorRendir>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CajaPorRendir Add(CajaPorRendir entidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaPorRendirRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CajaPorRendir>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CajaPorRendir Update(CajaPorRendir entidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaPorRendirRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CajaPorRendir>(modelo);
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
                _unitOfWork.CajaPorRendirRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CajaPorRendir> Add(List<CajaPorRendir> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaPorRendirRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CajaPorRendir>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CajaPorRendir> Update(List<CajaPorRendir> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaPorRendirRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CajaPorRendir>>(modelo);
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
                _unitOfWork.CajaPorRendirRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CajaPorRendir
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public IEnumerable<CajaPorRendirDTO> ObtenerCajaPorRendir(CajaPorRendirFiltroDTO filtro)
        {
            try
            {
                return _unitOfWork.CajaPorRendirRepository.ObtenerCajaPorRendir(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los nombres de los solicitantes correspondientes al Encargado de caja
        /// </summary>
        /// <returns> List<CajaPorRendirSolicitanteDTO> </returns>
        public IEnumerable<CajaPorRendirCombosDTO> ObtenerCajaPorRendirSolicitante(int idPersonalResponsable)
        {
            try
            {
                return _unitOfWork.CajaPorRendirRepository.ObtenerCajaPorRendirSolicitante(idPersonalResponsable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina de T_CajaPorRendir , T_Fur
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public bool DevolverSolicitudEnviada(CajaPorRendirDevolerDTO data)
        {
            try
            {
                var repCajaPorRendir = _unitOfWork.CajaPorRendirRepository;
                CajaPorRendir caja = new CajaPorRendir();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (repCajaPorRendir.Exist(data.Id))
                    {
                        caja = _mapper.Map<CajaPorRendir>(repCajaPorRendir.FirstById(data.Id));
                        caja.EsEnviado = false;
                        caja.FechaEnvio = null;
                        caja.FechaModificacion = DateTime.Now;
                        caja.UsuarioModificacion = data.Usuario;

                        this.Update(caja);
                        scope.Complete();

                    }
                    else throw new Exception("Registro no existente");
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina de T_CajaPorRendir , T_Fur
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public bool EliminarCajaPorRendirSolicitudEnviada(int id, int idFur, string usuario)
        {
            try
            {
                var repCajaPorRendir = _unitOfWork.CajaPorRendirRepository;
                var serFur = new FurService(_unitOfWork);
                using (TransactionScope scope = new TransactionScope())
                {
                    if (repCajaPorRendir.Exist(id))
                    {
                        this.Delete(id, usuario);
                        serFur.CambiarEstadoFurSolicitudCajaPR(idFur, false);
                        scope.Complete();

                        return true;
                    }
                    else
                    {
                        throw new Exception("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Crea una solicidtud PR
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public bool InsertarRegistroPorRendirInmediato(CajaPorRendirDTO data, int idPorRendirCabecera)
        {

            try
            {
                CajaPorRendir entidad = new CajaPorRendir();
                entidad.IdCaja = data.IdCaja;
                entidad.IdFur = data.IdFur;
                entidad.IdPersonalSolicitante = data.IdPersonalSolicitante;
                entidad.IdPersonalResponsableCaja = data.IdPersonalResponsable;
                entidad.Descripcion = data.Descripcion;
                entidad.IdMoneda = data.IdMoneda;
                entidad.TotalEfectivo = data.TotalEfectivo;
                entidad.FechaEntregaEfectivo = data.FechaEntregaEfectivo;
                entidad.EsEnviado = true;
                entidad.FechaEnvio = DateTime.Now;
                entidad.FechaAprobacion = DateTime.Now;
                entidad.IdCajaPorRendirCabecera = idPorRendirCabecera;
                entidad.Estado = true;
                entidad.UsuarioCreacion = data.UsuarioModificacion;
                entidad.UsuarioModificacion = data.UsuarioModificacion;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;

                this.Add(entidad);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Crea una solicidtud PR
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public bool InsertarCajaPorRendir(DatosSolicitudDTO ObjetoDTO)
        {
            try
            {
                var  _repoCajaPorRendir = _unitOfWork.CajaPorRendirRepository;
                CajaPorRendir NuevoCajaPorRendir = new CajaPorRendir();
                NuevoCajaPorRendir.IdCaja = null;
                NuevoCajaPorRendir.FechaEnvio = null;
                NuevoCajaPorRendir.IdCajaPorRendirCabecera = null;
                NuevoCajaPorRendir.FechaAprobacion = null;
                NuevoCajaPorRendir.IdPersonalSolicitante = ObjetoDTO.IdPersonalSolicitante;
                NuevoCajaPorRendir.IdFur = ObjetoDTO.IdFur;
                NuevoCajaPorRendir.IdPersonalResponsableCaja = ObjetoDTO.IdPersonalResponsable;
                NuevoCajaPorRendir.Descripcion = ObjetoDTO.Descripcion;
                NuevoCajaPorRendir.IdMoneda = ObjetoDTO.IdMoneda;
                NuevoCajaPorRendir.TotalEfectivo = ObjetoDTO.TotalEfectivo;
                NuevoCajaPorRendir.FechaEntregaEfectivo = ObjetoDTO.FechaEntregaEfectivo;
                NuevoCajaPorRendir.EsEnviado = false;
                NuevoCajaPorRendir.Estado = true;
                NuevoCajaPorRendir.FechaCreacion = DateTime.Now;
                NuevoCajaPorRendir.FechaModificacion = DateTime.Now;
                NuevoCajaPorRendir.UsuarioCreacion = ObjetoDTO.UsuarioModificacion;
                NuevoCajaPorRendir.UsuarioModificacion = ObjetoDTO.UsuarioModificacion;

                this.Add(NuevoCajaPorRendir);


                var _repoFur = _unitOfWork.FurRepository;
                var serFur = new FurService(_unitOfWork);
                Fur Fur = serFur.mapperFur.Map<Fur>(_repoFur.GetBy(x => x.Id == ObjetoDTO.IdFur).FirstOrDefault());
                if (Fur == null)
                    throw new Exception("No se encontro el FUR a actualizar");
                Fur.OcupadoSolicitud = true;
                serFur.Update(Fur);
                return true;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza una solicidtud PR
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public bool ActualizarCajaPorRendir( DatosSolicitudDTO ObjetoDTO)
        {
            try
            {
                var _repoCajaPorRendir = _unitOfWork.CajaPorRendirRepository;
                CajaPorRendir CajaPorRendir = _mapper.Map<CajaPorRendir>(_repoCajaPorRendir.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault());
                if (CajaPorRendir == null) throw new Exception("No se encontro el registro de 'CajaPorRendir' que se quiere actualizar");

                CajaPorRendir.IdCaja = null;
                CajaPorRendir.FechaEnvio = null;
                CajaPorRendir.IdCajaPorRendirCabecera = null;
                CajaPorRendir.FechaAprobacion = null;
                CajaPorRendir.IdPersonalSolicitante = ObjetoDTO.IdPersonalSolicitante;
                CajaPorRendir.IdFur = ObjetoDTO.IdFur;
                CajaPorRendir.IdPersonalResponsableCaja = ObjetoDTO.IdPersonalResponsable;
                CajaPorRendir.Descripcion = ObjetoDTO.Descripcion;
                CajaPorRendir.IdMoneda = ObjetoDTO.IdMoneda;
                CajaPorRendir.TotalEfectivo = ObjetoDTO.TotalEfectivo;
                CajaPorRendir.FechaEntregaEfectivo = ObjetoDTO.FechaEntregaEfectivo;
                CajaPorRendir.EsEnviado = false;
                CajaPorRendir.Estado = true;
                CajaPorRendir.FechaModificacion = DateTime.Now;
                CajaPorRendir.UsuarioModificacion = ObjetoDTO.UsuarioModificacion;

                this.Update(CajaPorRendir);

                var _repoFur = _unitOfWork.FurRepository;
                var serFur = new FurService(_unitOfWork);
                Fur Fur = serFur.mapperFur.Map<Fur>(_repoFur.GetBy(x => x.Id == ObjetoDTO.IdFur).FirstOrDefault());
                if (Fur == null)
                    throw new Exception("No se encontro el FUR a actualizar");

                Fur.OcupadoSolicitud = true;
                _repoFur.Update(Fur);
                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza caja Por rendir a estado enviado
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public bool ActualizarCajaPorRendirPonerEnviado(EsEnviadoSolicitudDTO CajasPorRendir)
        {
            try
            {
                var _repoCajaPorRendir = _unitOfWork.CajaPorRendirRepository;
                using (TransactionScope scope = new TransactionScope())
                {

                    foreach (var idSolicitud in CajasPorRendir.listaIds)
                    {
                        CajaPorRendir _cajaPorRendir = _mapper.Map<CajaPorRendir>(_repoCajaPorRendir.GetBy(x => x.Id == idSolicitud).FirstOrDefault());
                        if (_cajaPorRendir == null) throw new Exception("No se encontro el registro de 'CajaPorRendir' que se quiere actualizar");
                        _cajaPorRendir.EsEnviado = true;
                        _cajaPorRendir.FechaEnvio = DateTime.Now;
                        _cajaPorRendir.Estado = true;
                        _cajaPorRendir.FechaModificacion = DateTime.Now;
                        _cajaPorRendir.UsuarioModificacion = CajasPorRendir.Usuario;
                        this.Update(_cajaPorRendir);

                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza caja Por rendir
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public bool ActualizarCajaPorRendirAprobacion(CajaPorRendirCabeceraDTO objetoPorRendirCab, int id)
        {
            try
            {
                var repCajaPorRendir = _unitOfWork.CajaPorRendirRepository;
                CajaPorRendir entidad = new CajaPorRendir();
                entidad = _mapper.Map<CajaPorRendir>(repCajaPorRendir.FirstById(id));
                if (entidad == null) throw new Exception("No se encontro el registro de 'CajaPorRendir' que se quiere actualizar");

                entidad.FechaAprobacion = DateTime.Now;
                entidad.IdCajaPorRendirCabecera = objetoPorRendirCab.Id;
                entidad.IdCaja = objetoPorRendirCab.IdCaja;
                entidad.UsuarioModificacion = objetoPorRendirCab.UsuarioModificacion;
                entidad.FechaModificacion = DateTime.Now;

                this.Update(entidad);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los montos totales de la Caja
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        public MontoCajaDTO ObtenerMontoTotalCaja(int IdCaja)
        {
            try
            {
                return _unitOfWork.CajaPorRendirRepository.ObtenerMontoTotalCaja(IdCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Acepta la Solicitud para generar por rendir
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        public bool GenerarPorRendir(GenerarPorRendirDTO generacionPorRendirDTO)
        {
            try
            {
                var repPRCabecera = _unitOfWork.CajaPorRendirCabeceraRepository;
                var serPRCabecera = new CajaPorRendirCabeceraService(_unitOfWork);

                int correlativo = 0;
                var listaCodigos = repPRCabecera.GetBy(x => x.Estado == true && x.Codigo.Contains(generacionPorRendirDTO.CajaPRCabecera.Codigo)).ToList();
                if (listaCodigos != null && listaCodigos.Count() != 0)
                {
                    var CodigoMayor = listaCodigos.OrderByDescending(x => x.Id).FirstOrDefault().Codigo;
                    if (Int32.Parse(CodigoMayor.Substring(CodigoMayor.LastIndexOf(".") + 1).Trim()) > correlativo)
                    {
                        correlativo = Int32.Parse(CodigoMayor.Substring(CodigoMayor.LastIndexOf(".") + 1).Trim());
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }

                using (TransactionScope scope = new TransactionScope())
                {

                    generacionPorRendirDTO.CajaPRCabecera.Codigo += correlativo;
                    generacionPorRendirDTO.CajaPRCabecera.Id = (serPRCabecera.Add(generacionPorRendirDTO.CajaPRCabecera)).Id;
                    foreach (var listaId in generacionPorRendirDTO.ListaIdPorRendir)
                    {
                        var Rpt = this.ActualizarCajaPorRendirAprobacion(generacionPorRendirDTO.CajaPRCabecera, listaId);
                        if (Rpt != true) throw new Exception("No se pudo actualizar el Registro Caja Por Rendir");
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GenerarPorRendirInmediato(GenerarPorRendirInmediatoDTO PorRendirInmediatoDTO)
        {
            try
            {
                var repPRCabecera = _unitOfWork.CajaPorRendirCabeceraRepository;
                var serPRCabecera = new CajaPorRendirCabeceraService(_unitOfWork);
                var serFur = new FurService(_unitOfWork);
                int correlativo = 0;
                var listaCodigos = repPRCabecera.GetBy(x => x.Estado == true && x.Codigo.Contains(PorRendirInmediatoDTO.CajaPRCabecera.Codigo)).ToList();
                if (listaCodigos != null && listaCodigos.Count() != 0)
                {
                    var CodigoMayor = listaCodigos.OrderByDescending(x => x.Id).FirstOrDefault().Codigo;
                    if (Int32.Parse(CodigoMayor.Substring(CodigoMayor.LastIndexOf(".") + 1).Trim()) > correlativo)
                    {
                        correlativo = Int32.Parse(CodigoMayor.Substring(CodigoMayor.LastIndexOf(".") + 1).Trim());
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }
                using (TransactionScope scope = new TransactionScope())
                {

                    PorRendirInmediatoDTO.CajaPRCabecera.Codigo += correlativo;
                    int IdPorRendirCabecera = (serPRCabecera.Add(PorRendirInmediatoDTO.CajaPRCabecera)).Id;
                    foreach (var listaPorRendir in PorRendirInmediatoDTO.ListaPorRendir)
                    {
                        this.InsertarRegistroPorRendirInmediato(listaPorRendir, IdPorRendirCabecera);
                        serFur.CambiarEstadoFurSolicitudCajaPR(listaPorRendir.IdFur.Value, true);
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de la vista de CajaPorRendirCabecera con filtros por fechas
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="IdCaja"></param>
        /// <returns> IEnumerable<CajaPorRendirGenerarPdfDTO> </returns>
        public IEnumerable<CajaPorRendirGenerarPdfDTO> ObtenerCajaPorRendirByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            try
            {
                return _unitOfWork.CajaPorRendirRepository.ObtenerCajaPorRendirByFecha(FechaInicial, FechaFinal, IdCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de la vista de CajaPorRendirCabecera 
        /// </summary>
        /// <param name="IdPorRendirCabecera"> Lista de Id</param>
        /// <returns> IEnumerable<CajaPorRendirGenerarPdfDTO> </returns>
        public IEnumerable<CajaPorRendirGenerarPdfDTO> ObtenerDatosCajaPorRendir(int[] IdPorRendirCabecera)
        {
            try
            {
                return _unitOfWork.CajaPorRendirRepository.ObtenerDatosCajaPorRendir(IdPorRendirCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de la vista de CajaPorRendirCabecera 
        /// </summary>
        /// <param name="IdPorRendirCabecera"> Lista de Id</param>
        /// <returns> IEnumerable<CajaPorRendirGenerarPdfDTO> </returns>
        public IEnumerable<CajaPorRendirCabeceraRendicionDTO> ObtenerCajasPorRendirParaRendicion(int IdUsuario)
        {
            try
            {
                return _unitOfWork.CajaPorRendirRepository.ObtenerCajasPorRendirParaRendicion(IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos par el detalle por rendir
        /// </summary>
        /// <param name="IdCajaPorRendirCabecera">ID CabeceraPR</param>
        /// <returns></returns>
        public IEnumerable<CajaPorRendirDTO> ObtenerCajasPorRendirPorIdPorRendirCabecera(int IdCajaPorRendirCabecera)
        {
            try
            {
                return _unitOfWork.CajaPorRendirRepository.ObtenerCajasPorRendirPorIdPorRendirCabecera(IdCajaPorRendirCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de registros con Estado=1 de un Usuario dado un IdPersonal de fin.T_CajasPorRendir (para llenado de grilla en SolicitudEfectivo)
        /// </summary>
        /// <param name="IdUsuario">ID usuario</param>
        /// <returns></returns>
        public List<CajaPorRendirDTO> ObtenerCajasPorRendirFinanzas(int IdUsuario)
        {
            try
            {
                return _unitOfWork.CajaPorRendirRepository.ObtenerCajasPorRendirFinanzas(IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Bytes para la genracion de PDF
        /// </summary>
        /// <param name="listaEnteros"> Lista de Id</param>
        /// <returns> IEnumerable<byte[]> </returns>
        public IEnumerable<byte[]> ObtenerDocumentosCajaPorRendir(List<int> listaEnteros)
        {
            try
            {

                List<byte[]> listaPDFbytes = new List<byte[]>();
                var listaCajaPorRendirPdf = this.ObtenerDatosCajaPorRendir(listaEnteros.ToArray());
                foreach (var datosCajaPorRendir in listaCajaPorRendirPdf)
                {
                    var pdf = this.GenerarPDFReciboCajaPorRendir(datosCajaPorRendir);
                    listaPDFbytes.Add(pdf);
                }

                return listaPDFbytes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// obtiene el monto limite del fur
        /// </summary>
        /// <returns> decimal </returns>
        public decimal ObtenerMontoLimiteSolicitud(int IdFur)
        {
            try
            {
                var repFur = _unitOfWork.FurRepository;
                var _repoFurPago = _unitOfWork.FurPagoRepository;
                var serFur = new FurService(_unitOfWork);

                Fur Fur = serFur.mapperFur.Map<Fur>(repFur.FirstById(IdFur));

                List<TFurPago> FurPagos = _repoFurPago.GetBy(x => x.IdFur == IdFur).ToList();

                if (Fur == null) throw new Exception("Error No se encontro el FUR");

                decimal PagosAcumulado = 0;
                for (int i = 0; i < FurPagos.Count; ++i)
                    PagosAcumulado += FurPagos[i].PrecioTotalMonedaOrigen;
                var MontoLimite = (Fur.PrecioTotalMonedaOrigen - PagosAcumulado);
                return MontoLimite;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }




        /// Autor: Griselberto Huaman
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Byte para la generacion de PDF, solicitados segun el reporte.
        /// </summary>
        /// <param name="datosGenerarPdf">Datos requeridos para llenar los datos de los Pdf</param>
        /// <returns> byte[]</returns>
        public byte[] GenerarPDFReciboCajaPorRendir(CajaPorRendirGenerarPdfDTO datosGenerarPdf)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 72f, 65f, 70f, 65f);
                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, ms);
                doc.AddTitle("Recibo" + datosGenerarPdf.CodigoPorRendir);
                doc.AddCreator("BS grupo");
                doc.Open();
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont_peque = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont_pequeBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font ForLine = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont_normal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPTable TablaTop = new PdfPTable(3);
                TablaTop.WidthPercentage = 100;
                TablaTop.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths = new float[] { 25f, 46.5f, 28.5f };
                TablaTop.SetWidths(widths);
                PdfPCell SeccionEmpresa = new PdfPCell(new Phrase(datosGenerarPdf.RazonSocial, _standardFont));
                SeccionEmpresa.BorderWidth = 1;
                SeccionEmpresa.BackgroundColor = BaseColor.LIGHT_GRAY;
                PdfPCell SeccionTitulo = new PdfPCell(new Phrase("           EGRESOS POR REGULARIZAR", _standardFont));
                SeccionTitulo.BorderWidth = 0;
                PdfPCell SeccioNumeroRecibo = new PdfPCell(new Phrase(datosGenerarPdf.CodigoPorRendir, _standardFont));
                SeccioNumeroRecibo.BorderWidth = 1;
                SeccioNumeroRecibo.BackgroundColor = BaseColor.LIGHT_GRAY;
                TablaTop.AddCell(SeccionEmpresa);
                TablaTop.AddCell(SeccionTitulo);
                TablaTop.AddCell(SeccioNumeroRecibo);
                doc.Add(TablaTop);
                //Segunda 
                Paragraph paDir = new Paragraph("Dirección: " + datosGenerarPdf.Direccion, _standardFont_normal); paDir.SpacingBefore = 10f; doc.Add(paDir);
                doc.Add(new Paragraph("Ruc: " + datosGenerarPdf.Ruc, _standardFont_normal));
                Paragraph paCentral = new Paragraph("Central: " + datosGenerarPdf.Central, _standardFont_normal); paCentral.SpacingAfter = 8f; doc.Add(paCentral);
                //Tercera seccion
                PdfPTable TablaBottom = new PdfPTable(2);
                TablaBottom.DefaultCell.Padding = 10f;
                //TablaBottom.DefaultCell.CellEvent = new CellSpacingEvent(20);
                TablaBottom.WidthPercentage = 80;
                TablaBottom.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths2 = new float[] { 22f, 78f };
                TablaBottom.SetWidths(widths2);
                PdfPCell SeccionCuentaTitulo = new PdfPCell(new Phrase("Cuenta", _standardFont2));
                SeccionCuentaTitulo.PaddingTop = 6f;
                SeccionCuentaTitulo.BorderWidthTop = 1;
                SeccionCuentaTitulo.BorderWidth = 0;
                PdfPCell SeccionCuentaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.CuentaCaja, _standardFont_normal));
                SeccionCuentaValor.BorderWidth = 0;
                SeccionCuentaValor.PaddingTop = 6f;
                SeccionCuentaValor.BorderWidthTop = 1;
                PdfPCell SeccionFechaTitulo = new PdfPCell(new Phrase("Fecha Entrega Efectivo", _standardFont2));
                SeccionFechaTitulo.BorderWidth = 0;
                PdfPCell SeccionFechaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.FechaAprobacion, _standardFont_normal));
                SeccionFechaValor.BorderWidth = 0;
                PdfPCell SeccionNroFurTitulo = new PdfPCell(new Phrase("Nro Furs", _standardFont2));
                SeccionNroFurTitulo.BorderWidth = 0;
                PdfPCell SeccionNroFurValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.CodigoFur, _standardFont_normal));
                SeccionNroFurValor.BorderWidth = 0;
                PdfPCell SeccionSeentregoaTitulo = new PdfPCell(new Phrase("Se entregó a", _standardFont2));
                SeccionSeentregoaTitulo.BorderWidth = 0;
                PdfPCell SeccionSeentregoaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.EntregadoA, _standardFont_normal));
                SeccionSeentregoaValor.BorderWidth = 0;
                PdfPCell SeccionImporteTitulo = new PdfPCell(new Phrase("Importe", _standardFont2));
                SeccionImporteTitulo.BorderWidth = 0;
                PdfPCell SeccionImporteValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.MontoTotal + "   " + datosGenerarPdf.Moneda, _standardFont_normal));
                SeccionImporteValor.BorderWidth = 0;
                PdfPCell SeccionDetalleTitulo = new PdfPCell(new Phrase("Concepto", _standardFont2));
                SeccionDetalleTitulo.PaddingBottom = 43f;
                SeccionDetalleTitulo.BorderWidth = 0;
                PdfPCell SeccionDetalleValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.Detalle, _standardFont_normal));
                SeccionDetalleTitulo.PaddingBottom = 43f;
                SeccionDetalleValor.BorderWidth = 0;
                TablaBottom.AddCell(SeccionCuentaTitulo);
                TablaBottom.AddCell(SeccionCuentaValor);
                TablaBottom.AddCell(SeccionFechaTitulo);
                TablaBottom.AddCell(SeccionFechaValor);
                TablaBottom.AddCell(SeccionNroFurTitulo);
                TablaBottom.AddCell(SeccionNroFurValor);
                TablaBottom.AddCell(SeccionSeentregoaTitulo);
                TablaBottom.AddCell(SeccionSeentregoaValor);
                TablaBottom.AddCell(SeccionImporteTitulo);
                TablaBottom.AddCell(SeccionImporteValor);
                TablaBottom.AddCell(SeccionDetalleTitulo);
                TablaBottom.AddCell(SeccionDetalleValor);
                doc.Add(TablaBottom);
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new iTextSharp.text.Paragraph("  ________________________                             ________________________", _standardFont2));
                doc.Add(new iTextSharp.text.Paragraph("     ENTREGUE CONFORME                                       RECIBI CONFORME", _standardFont2));
                PdfPTable TablaBottom2 = new PdfPTable(2);
                TablaBottom2.WidthPercentage = 100;
                TablaBottom2.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths3 = new float[] { 48, 60 };
                TablaBottom2.SetWidths(widths3);
                PdfPCell SeccionCuentaTitulo2 = new PdfPCell(new Phrase(datosGenerarPdf.PersonalResponsable, _standardFont_normal));
                SeccionCuentaTitulo2.PaddingTop = 10f;
                SeccionCuentaTitulo2.BorderWidth = 0;
                PdfPCell SeccionCuentaValor2 = new PdfPCell(new Phrase("              " + "________________________", _standardFont_normal));
                SeccionCuentaValor2.BorderWidth = 0;
                SeccionCuentaValor2.PaddingTop = 10f;
                PdfPCell SeccionNroFurTitulo2 = new PdfPCell(new Phrase("DNI: ", _standardFont_normal));
                SeccionNroFurTitulo2.BorderWidth = 0;
                PdfPCell SeccionNroFurValor2 = new PdfPCell(new Phrase("              DNI: ", _standardFont_normal));
                SeccionNroFurValor2.BorderWidth = 0;
                TablaBottom2.AddCell(SeccionCuentaTitulo2);
                TablaBottom2.AddCell(SeccionCuentaValor2);
                TablaBottom2.AddCell(SeccionNroFurTitulo2);
                TablaBottom2.AddCell(SeccionNroFurValor2);
                doc.Add(TablaBottom2);
                doc.Add(new Paragraph("\n\n\n"));
                doc.Add(new Paragraph("AUTORIZACIÓN DE DESCUENTO POR PLANILLA DE HABERES", _standardFont2));
                Paragraph condiciones = new Paragraph("Yo _______________________________________________________, " +
                    "por la presente autorizo expresamente que se me descuente, de mi planilla de haberes del mes, la cantidad de " + datosGenerarPdf.MontoTotal + " " + datosGenerarPdf.Moneda + ", en el" +
                    " caso de no entregar la documentación sustentatoria de compra, a los 05 días calendarios de habérseme entregado el importe indicado, el dia de hoy "
                    + DateTime.Now + " , para lo cual he firmado en señal de conformidad el recibo.", _standardFont_normal);
                condiciones.SpacingBefore = 10f;
                condiciones.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(condiciones);

                Paragraph nota = new Paragraph();
                nota.Add(new Chunk("NOTA:", _standardFont_pequeBold));
                nota.Add(new Chunk("  Unicamente se aceptara Comprobantes que hayan sido emitidos dentro del período que se le asigno los gastos. " +
                    "Los comprobantes (Facturas, boletas, etc) deben ser aceptados tributariamente por SUNAT. Por ningún motivo debe ir el término \"POR CONSUMO\", siempre debe ir el detallado de la compra. " +
                    "El dinero entregado por viáticos son gastos específicos de movilidad y Alimentacion.", _standardFont_peque));
                nota.SpacingBefore = 8f;
                nota.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(nota);



                doc.Close();
                writer.Close();
                doc.Dispose();

                return ms.ToArray();
            }
        }

        
    }
}
