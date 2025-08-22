using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReportePresupuestoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePresupuesto
    /// </summary>
    public class ReportePresupuestoService : IReportePresupuestoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReportePresupuestoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReportePresupuesto, ReportePresupuesto>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReportePresupuestoRecibidoDTO, ReportePresupuesto>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Obtiene la lista de furs que no tengan asociado ningun documento de pago y esten aprobados por jefe de finanzas
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Obtiene toda la data de los campos para el Reporte de Presupuesto 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportePresupuestoDTO> ObtenerReportePresupuestoFinanzas(FiltroPresupuestoDTO filtros)
        {
            try
            {
                var presupuesto  = _unitOfWork.ReportePresupuestoRepository.ObtenerReportePresupuestoFinanzas(filtros);
                if (filtros.idPeridoProgramacionActual!=null)
                {
                    var PresupuestoModificado = presupuesto
                     .GroupBy(p => new
                     {
                         p.IdFur, p.Empresa,  p.Sede, p.Area,  p.TipoPedido, p.CodigoFur, p.ProductoServicio, p.Descripcion,
                         p.CentroCosto, p.Curso,  p.Programa, p.MonedaProyectada, p.CantidadProyectada, p.PresentacionProyectada,
                         p.PrecioUnitarioProyectado, p.PrecioTotalOrigenProyectado, p.PrecioTotalDolaresProyectado, p.MonedaProveedor,
                         p.Cantidad, p.Unidad, p.PrecioUnitarioJF, p.PrecioTotalOrigenJF, p.PrecioTotalDolaresJF, p.Atipico, p.Rubro,
                         p.NroCuenta, p.Cuenta, p.UsuarioCreacion, p.UsuarioAprobacion, p.UsuarioModificacion, p.SubFaseFur, p.FaseAprobacion,
                         p.FechaLimiteFur, p.MesLimiteFur, p.EsDiferido
                     })
                     .Select(g => new ReportePresupuestoDTO
                     {
                         IdFur = g.Key.IdFur,
                         Empresa = g.Key.Empresa,
                         Sede = g.Key.Sede,
                         Area = g.Key.Area,
                         TipoPedido = g.Key.TipoPedido,
                         CodigoFur = g.Key.CodigoFur,
                         ProductoServicio = g.Key.ProductoServicio,
                         Descripcion = g.Key.Descripcion,
                         CentroCosto = g.Key.CentroCosto,
                         Curso = g.Key.Curso,
                         Programa = g.Key.Programa,
                         MonedaProyectada = g.Key.MonedaProyectada,
                         CantidadProyectada = g.Key.CantidadProyectada,
                         PresentacionProyectada = g.Key.PresentacionProyectada,
                         PrecioUnitarioProyectado = g.Key.PrecioUnitarioProyectado,
                         PrecioTotalOrigenProyectado = g.Key.PrecioTotalOrigenProyectado,
                         PrecioTotalDolaresProyectado = g.Key.PrecioTotalDolaresProyectado,
                         MonedaProveedor = g.Key.MonedaProveedor,
                         Cantidad = g.Key.Cantidad,
                         Unidad = g.Key.Unidad,
                         PrecioUnitarioJF = g.Key.PrecioUnitarioJF,
                         PrecioTotalOrigenJF = g.Key.PrecioTotalOrigenJF,
                         PrecioTotalDolaresJF = g.Key.PrecioTotalDolaresJF,
                         Atipico = g.Key.Atipico,
                         Rubro = g.Key.Rubro,
                         NroCuenta = g.Key.NroCuenta,
                         Cuenta = g.Key.Cuenta,
                         UsuarioCreacion = g.Key.UsuarioCreacion,
                         UsuarioAprobacion = g.Key.UsuarioAprobacion,
                         UsuarioModificacion = g.Key.UsuarioModificacion,
                         FaseAprobacion = g.Key.FaseAprobacion,
                         SubFaseFur = g.Key.SubFaseFur,
                         FechaLimiteFur = g.Key.FechaLimiteFur,
                         MesLimiteFur = g.Key.MesLimiteFur,
                         EsDiferido = g.Key.EsDiferido,
                         RUC = string.Join("/", g.Select(x => x.RUC).Distinct().ToList()),
                         Proveedor = string.Join("/", g.Select(x => x.Proveedor).Distinct().ToList()),
                         TipoComprobante = string.Join("/", g.Select(x => x.TipoComprobante).Distinct().ToList()).Replace("/SIN COMPROBANTE", "").Replace("SIN COMPROBANTE/", ""),
                         NumeroComprobante = string.Join("/", g.Select(x => x.NumeroComprobante).Distinct().ToList()).Replace("/SIN COMPROBANTE", "").Replace("SIN COMPROBANTE/", ""),
                         MonedaComprobante = g.Select(x => x.MonedaComprobante).FirstOrDefault(),
                         MontoPorPagar = g.Select(x => x.MontoPorPagar).Sum(),
                         MontoPorPagarDolares = g.Select(x => x.MontoPorPagarDolares).Sum(),
                         FechaEmisionComprobante = g.Where(x => x.FechaEmisionComprobante != null).Select(x => x.FechaEmisionComprobante).FirstOrDefault(),
                         MesEmision = string.Join("/", g.Select(x => x.MesEmision).Distinct().ToList()),
                         FechaVencimientoComprobante = g.Where(x => x.FechaVencimientoComprobante != null).Select(x => x.FechaVencimientoComprobante).FirstOrDefault(),
                         MesVencimiento = string.Join("/", g.Select(x => x.MesVencimiento).Distinct().ToList()),
                         MonedaPago = string.Join("/", g.Select(x => x.MonedaPago).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", ""),
                         FormaPago = string.Join("/", g.Select(x => x.FormaPago).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", ""),
                         MontoRealPagado = g.Select(x => x.MontoRealPagado).Sum(),
                         MontoRealPagadoDolares = g.Select(x => x.MontoRealPagadoDolares).Sum(),
                         NumeroReciboBanco = string.Join("/", g.Select(x => x.NumeroReciboBanco).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", ""),
                         EntidadFinanciera = string.Join("/", g.Select(x => x.EntidadFinanciera).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", ""),
                         CuentaCorriente = string.Join("/", g.Select(x => x.CuentaCorriente).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", ""),
                         FechaPagoBanco = g.Where(x => x.FechaPagoBanco != null).Select(x => x.FechaPagoBanco).FirstOrDefault(),
                         MesPagoBanco = string.Join("/", g.Select(x => x.MesPagoBanco).Distinct().ToList()),
                         FechaProgramacionOriginal = g.Select(x => x.FechaProgramacionOriginal).FirstOrDefault(),
                         MesProgramacionOriginal = string.Join("/", g.Select(x => x.MesProgramacionOriginal).Distinct().ToList()),
                         FechaProgramacionActual = g.Select(x => x.FechaProgramacionActual).FirstOrDefault(),
                         MesProgramacionActual = string.Join("/", g.Select(x => x.MesProgramacionActual).Distinct().ToList()),
                         EstadoComprobante = string.Join("/", g.Select(x => x.EstadoComprobante).Distinct().ToList())
                     });
                     return PresupuestoModificado;
                }
                else
                {
                    return presupuesto;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza Es diferido Del FUR
        /// Obtiene toda la data de los campos para el Reporte de Presupuesto 
        /// </summary>
        /// <returns></returns>
        public bool ActualizarEsDiferidoListaFur(DiferirFurDTO datos)
        {
            try
            {
                return _unitOfWork.ReportePresupuestoRepository.ActualizarEsDiferidoListaFur(datos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
