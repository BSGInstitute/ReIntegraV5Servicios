using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Finanza
{
    public class ReporteEstadoCuentaProveedorService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReporteEstadoCuentaProveedorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<ReporteEstadoCuentaProveedorDTO> VizualizarReporteEstadoCuentaProveedor(ReporteEstadoCuentaProveedorFiltroDTO Filtro)
        {
            try
            {
                if(Filtro.FechaInicio== null)
                {
                    Filtro.FechaInicio = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                }
                if (Filtro.FechaFin == null)
                {
                    Filtro.FechaFin=DateTime.Now.ToString("yyyy-MM-01");
                }
                List<ReporteEstadoCuentaProveedorDTO> listaCrudo = unitOfWork.FurPagoRepository.GenerarReporteEstadoCuentaProveedor(Filtro.Empresa, Filtro.Ciudad, Filtro.Proveedor,Filtro.Comprobante,Filtro.FechaInicio,Filtro.FechaFin,Filtro.CuentaContable);

                listaCrudo = listaCrudo.GroupBy(x => new
                {
                    x.CodigoFur,
                    x.EmpresaSede,
                    x.NombreProveedor,
                    x.NombreMoneda,
                    x.MontoBoleta,
                    x.NumeroComprobante,
                    x.NumeroRecibo,
                    x.DescripcionCuenta,
                    x.NumeroCuenta,
                    x.TipoPago,
                    x.FechaEmisionComprobante,
                    x.FechaPagoBanco,
                    x.FechaVencimientoComprobante,
                    x.MesPagoBanco,
                    x.RucProveedor
                }).Select(g => new ReporteEstadoCuentaProveedorDTO
                {
                    CodigoFur = g.Key.CodigoFur,
                    FechaEmisionComprobante = g.Key.FechaEmisionComprobante,
                    FechaVencimientoComprobante = g.Key.FechaVencimientoComprobante,
                    DescripcionCuenta = g.Key.DescripcionCuenta,
                    EmpresaSede = g.Key.EmpresaSede,
                    FechaPagoBanco = g.Key.FechaPagoBanco,
                    MesPagoBanco = g.Key.MesPagoBanco,
                    MontoBoleta = g.Key.MontoBoleta,
                    MontoPagado = g.Sum(c => c.MontoPagado),
                    MontoPendiente = g.Key.MontoBoleta - g.Sum(c => c.MontoPagado),
                    NumeroRecibo = g.Key.NumeroRecibo,
                    Estado = (g.Key.MontoBoleta-g.Sum(c => c.MontoPagado)) <= 0 ? "PAGADO" : "PENDIENTE",
                    NombreMoneda = g.Key.NombreMoneda,
                    NombreProveedor = g.Key.NombreProveedor,
                    NumeroComprobante = g.Key.NumeroComprobante,
                    NumeroCuenta = g.Key.NumeroCuenta,
                    NumeroCuentaCorriente = g.Select(c => c.NumeroCuentaCorriente).FirstOrDefault(),
                    RucProveedor = g.Key.RucProveedor,
                    TipoPago = g.Key.TipoPago,
                }).ToList();

                bool FiltroPorEstado = false;
                List<ReporteEstadoCuentaProveedorDTO> ListaFiltradaEstado = new List<ReporteEstadoCuentaProveedorDTO>();

                if (Filtro.Estado != null)
                {
                    FiltroPorEstado = Convert.ToBoolean(Filtro.Estado);
                    if (FiltroPorEstado)
                    {
                        ListaFiltradaEstado = listaCrudo.Where(x => x.Estado.ToLower().Equals("pagado")).ToList();
                        return ListaFiltradaEstado;
                    }
                    ListaFiltradaEstado = listaCrudo.Where(x => x.Estado.ToLower().Equals("pendiente")).ToList();
                    return ListaFiltradaEstado;
                }
                else return listaCrudo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
