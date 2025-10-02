using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: MontoPagoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión general de T_MontoPago
    /// </summary>
    public class MontoPagoService : IMontoPagoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MontoPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPago, MontoPago>(MemberList.None).ReverseMap();
                cfg.CreateMap<MontoPagoDTO, MontoPago>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Probabilidad de Sueldo asociado a la Oportunidad y Pais.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerProbabilidadSueldoOportunidad(int idOportunidad, int idPais)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerProbabilidadSueldoOportunidad(idOportunidad, idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Versiones de Monto Pago asociado a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoVersionDTO> </returns>
        public IEnumerable<MontoPagoVersionDTO> ObtenerVersionMontoPagoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerVersionMontoPagoPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Versiones de Monto Pago asociado a una Oportunidad junto a los Beneficios
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoVersionBeneficiosDTO> </returns>
        public IEnumerable<MontoPagoVersionBeneficiosDTO> ObtenerVersionMontoPagoBeneficiosPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerVersionMontoPagoBeneficiosPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Versiones de Monto Pago asociado a una Oportunidad junto a los Beneficios
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoVersionBeneficiosDTO> </returns>
        public StringDTO ObtenerTablaHTMLVersionMontoPagoBeneficios(int idOportunidad)
        {
            try
            {
                var versionesBeneficios = ObtenerVersionMontoPagoBeneficiosPorIdOportunidad(idOportunidad);
                if (versionesBeneficios == null)
                {
                    return new StringDTO() { Valor = null };
                }
                var beneficiosDetalle = (
                    from x in versionesBeneficios
                    orderby x.OrdenBeneficio
                    group x by new { x.Paquete, x.tp_nombre, x.tp_cuotas, x.mp_precio, x.Simbolo, x.mp_matricula, x.mp_nro_cuotas, x.mp_cuotas } into gj
                    select new MontoPagoVersionBeneficiosDetalleDTO
                    {
                        Paquete = gj.Key.Paquete,
                        tp_nombre = gj.Key.tp_nombre,
                        tp_cuotas = gj.Key.tp_cuotas,
                        mp_precio = gj.Key.mp_precio,
                        Simbolo = gj.Key.Simbolo,
                        mp_matricula = gj.Key.mp_matricula,
                        mp_nro_cuotas = gj.Key.mp_nro_cuotas,
                        mp_cuotas = gj.Key.mp_cuotas,
                        Beneficios = gj.Select(x => x.Titulo).ToList()
                    }).ToList();

                if (beneficiosDetalle.Count() == 0)
                {
                    return new StringDTO() { Valor = null };
                }
                else
                {
                    string tabla = "";
                    tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                    tabla += "<tr>";
                    tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
                    tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
                    tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
                    tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
                    tabla += "</tr>";
                    foreach (var re in beneficiosDetalle)
                    {
                        var credito = beneficiosDetalle.Where(s => s.Paquete == re.Paquete && s.tp_cuotas != 2).FirstOrDefault(); //add roy
                        tabla += "<tr>";
                        tabla += "<td style='border: 1px solid #E6E6E6' >" + ObtenerPaquete(re.Paquete == null ? "" : re.Paquete.ToString()) + "</td>";
                        tabla += "<td style='border: 1px solid #E6E6E6'> " + string.Join(", ", re.Beneficios.Distinct()) + "</td>";
                        tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.tp_nombre + "</td>";
                        tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + (re.tp_cuotas == 2 ? re.Simbolo.Replace(".", " ") + re.mp_precio.ToString() /*+ (credito == null? "<br /><span style='color:red;'>" + "Con 25% de descuento:" + re.Simbolo.Replace(".", " ") + Math.Round(((re.mp_precio * 75) / 100), MidpointRounding.AwayFromZero).ToString() + "</span>" : "<br /><span style='color:red;'>" + "Con 25% de descuento:" + credito.Simbolo.Replace(".", " ") + Math.Round(((credito.mp_precio * 75) / 100), MidpointRounding.AwayFromZero).ToString() + "</span>")*///decomnetar para 25 % descuento
                                : "1 matricula de: " + re.Simbolo.Replace(".", "") + re.mp_matricula + " y " + re.mp_nro_cuotas + " cuotas de " + re.Simbolo.Replace(".", "") + re.mp_cuotas)
                                + "</td>";
                        tabla += "</tr>";

                    }
                    tabla += "</TABLE>";
                    return new StringDTO() { Valor = tabla };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Paquete ascociado a un Valor.
        /// </summary>
        /// <param name="valor">Valor al que se asocia un Paquete</param>
        /// <returns> ValorStringDTO </returns>
        private StringDTO ObtenerPaquete(string valor)
        {
            string? paquete = null;
            switch (valor)
            {
                case "1":
                    paquete = "Versión Basica";
                    break;
                case "2":
                    paquete = "Versión Profesional";
                    break;
                case "3":
                    paquete = "Versión Gerencial";
                    break;
                default:
                    paquete = "";
                    break;
            }
            return new StringDTO() { Valor = paquete };
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Monto Pago Contado por Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public MontoPagoCompuestoDTO ObtenerMontoPagoContadoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerMontoPagoContadoPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una tabla para Precio en Cuotas de un Programa
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidadParaTabla(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerMontoPagoPorIdOportunidadParaTabla(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Monto Pago por Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoCronogramaCompuestoDTO> </returns>
        public IEnumerable<MontoPagoCronogramaCompuestoDTO> ObtenerMontoPagoPorIdOportunidadV2(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerPorIdOportunidadV2(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna los paquetes, nombre paquete,precio y pais de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> List<MontoPagoModalidadDTO> </returns>
        public List<MontoPagoModalidadDTO> ObtenerMontosPorId(int idPGeneral)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerMontosPorId(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/09/2025
        /// Version: 1.0
        /// <summary>
        /// Retorna los paquetes, nombre paquete,precio y pais de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> List<MontoPagoModalidadDTO> </returns>
        public async Task<List<MontoPagoModalidadDTO>> ObtenerMontosPorIdAsync(int idPGeneral)
        {
            try
            {
                return await _unitOfWork.MontoPagoRepository.ObtenerMontosPorIdAsync(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna los datos de la tabla MontoPago asociado al Id
        /// </summary>
        /// <param name="idMontoPago">Id de T_MontoPago </param>
        /// <returns> MontoPagoDTO </returns>
        public MontoPagoDTO ObtenerPorId(int idMontoPago)
        {
            try
            {
                return _mapper.Map<MontoPagoDTO>(_unitOfWork.MontoPagoRepository.ObtenerPorId(idMontoPago));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Versiones de Monto Pago v2
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<MontoPagoEtiquetaDTO> </returns>
        public List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPagoV2(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerVersionesMontoPagoV2(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Versiones por monto de Pago
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> List<MontoPagoEtiquetaDTO> </returns>
        public List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPago(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerVersionesMontoPago(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Monto de Pago Por Id de Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> MontoPagoCompuestoDTO </returns>
        public MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidadSP(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerMontoPagoPorIdOportunidadSP(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene paquete por medio del id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> MontoPagoPaqueteDTO </returns>
        public MontoPagoPaqueteDTO ObtenerPaquete(int id)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerPaquete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los paquetes completos por IdCentroCosto
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Lista: List<PaqueteCentroCostoDTO </returns>
        public List<PaqueteCentroCostoDTO> ObtenerPaquetesIdCentroCosto(int id)
        {
            try
            {
                return _unitOfWork.MontoPagoRepository.ObtenerPaquetesIdCentroCosto(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle del monto pago
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <param name="idCategoria"></param>
        /// <returns> DTO - MontoPagoDetalleDTO - detalleMontoPago </returns>
        public PgeneralMontoPagoDetalleDTO ObtenerPgeneralMontoPagoDetalle(int idPrograma, int idCategoria)
        {
            try
            {
                var montosPago = _unitOfWork.MontoPagoRepository.ObtenerPorIdPrograma(idPrograma);
                if (montosPago.Count() > 0)
                {
                    var plataformasPagos = _unitOfWork.MontoPagoPlataformaRepository.ObtenerPorIdsMontoPago(montosPago.Select(x => x.Id));
                    var suscripcionesPagos = _unitOfWork.MontoPagoSuscripcionRepository.ObtenerPorIdsMontoPago(montosPago.Select(x => x.Id));
                    foreach (var item in montosPago)
                    {
                        item.PlataformasPagos = plataformasPagos.Where(x => x.Id == item.Id).Select(x => x.Valor!.Value).ToList();
                        item.SuscripcionesPagos = suscripcionesPagos.Where(x => x.Id == item.Id).Select(x => x.Valor!.Value).ToList();
                    }
                }
                PgeneralMontoPagoDetalleDTO detalleMontoPago = new();
                detalleMontoPago.MontoPagos = montosPago;
                detalleMontoPago.Suscripciones = _unitOfWork.SuscripcionProgramaGeneralRepository.ObtenerComboPorIdPgeneral(idPrograma);
                detalleMontoPago.TipoCategoria = _unitOfWork.TipoPagoCategoriaRepository.ObtenerComboPorIdCategoriaPrograma(idCategoria);

                return detalleMontoPago;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 10/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle del monto pago
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <param name="idCategoria"></param>
        /// <returns> DTO - MontoPagoDetalleDTO - detalleMontoPago </returns>
        public List<PGeneralMontoPagoPanelDTO> ListarProgramaGeneralParaMontoPago()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ListarProgramaGeneralParaMontoPago();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/08/2023
        /// Version: 1.0
        /// <summary>
        /// Eliminar Monto Pago
        /// </summary>
        /// <param name="idMontoPago"></param>
        /// <param name="usuario"></param>
        /// <returns>Estado de la eliminacion</returns>
        public bool EliminarMontoPago(int idMontoPago, string usuario)
        {
            try
            {
                if (_unitOfWork.MontoPagoRepository.Exist(idMontoPago))
                {
                    _unitOfWork.MontoPagoRepository.Delete(idMontoPago, usuario);
                    var idsMontoPagoPlataforma = _unitOfWork.MontoPagoPlataformaRepository.GetBy(x => x.IdMontoPago == idMontoPago).Select(x => x.Id).ToList();
                    if (idsMontoPagoPlataforma != null && idsMontoPagoPlataforma.Count() > 0)
                    {
                        _unitOfWork.MontoPagoPlataformaRepository.Delete(idsMontoPagoPlataforma, usuario);
                    }
                    var idsMontoPagoSuscripcion = _unitOfWork.MontoPagoSuscripcionRepository.GetBy(x => x.IdMontoPago == idMontoPago).Select(x => x.Id).ToList();
                    if (idsMontoPagoPlataforma != null && idsMontoPagoPlataforma.Count() > 0)
                    {
                        _unitOfWork.MontoPagoSuscripcionRepository.Delete(idsMontoPagoPlataforma, usuario);
                    }
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene pGeneralComboModuloDTO para el moduloo de Programa General
        /// </summary>
        /// <returns> PGeneralComboMontoPagoModuloDTO </returns>
        public async Task<PGeneralComboMontoPagoModuloDTO> ObtenerCombosModuloAsync()
        {
            try
            {
                var task_subAreaCapacitacion = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltroAsync();
                var task_areaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerFiltroAsync();
                var task_tipoDescuento = _unitOfWork.TipoDescuentoRepository.ObtenerComboAsync();
                var task_pais = _unitOfWork.PaisRepository.ObtenerComboConMonedaAsync();
                var task_moneda = _unitOfWork.MonedaRepository.ObtenerComboAsync();
                var task_categoriaPrograma = _unitOfWork.CategoriaProgramaRepository.ObtenerComboAsync();
                var task_suscripcionProgramaGeneral = _unitOfWork.SuscripcionProgramaGeneralRepository.ObtenerComboAsync();
                var task_tipoPago = _unitOfWork.TipoPagoRepository.ObtenerComboAsync();
                var task_plataformaPago = _unitOfWork.PlataformaPagoRepository.ObtenerComboAsync();

                PGeneralComboMontoPagoModuloDTO pGeneralComboModuloDTO = new PGeneralComboMontoPagoModuloDTO();
                pGeneralComboModuloDTO.SubAreaCapacitacion = await task_subAreaCapacitacion;
                pGeneralComboModuloDTO.AreaCapacitacion = await task_areaCapacitacion;
                pGeneralComboModuloDTO.TipoDescuento = await task_tipoDescuento;
                pGeneralComboModuloDTO.Pais = await task_pais;
                pGeneralComboModuloDTO.Moneda = await task_moneda;
                pGeneralComboModuloDTO.CategoriaPrograma = await task_categoriaPrograma;
                pGeneralComboModuloDTO.SuscripcionProgramaGeneral = await task_suscripcionProgramaGeneral;
                pGeneralComboModuloDTO.TipoPago = await task_tipoPago;
                pGeneralComboModuloDTO.PlataformaPago = await task_plataformaPago;
                return (pGeneralComboModuloDTO);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
