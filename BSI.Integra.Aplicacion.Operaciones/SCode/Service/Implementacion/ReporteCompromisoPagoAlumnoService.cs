using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Repositorio.UnitOfWork;
using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    public class ReporteCompromisoPagoAlumnoService : IReporteCompromisoPagoAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        public ReporteCompromisoPagoAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ObtenerCombosDTO obtenerCombos(int IdPersonal)
        {
            ObtenerCombosDTO combos = new ObtenerCombosDTO();
            try
            {

                combos.comboPersonal = _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoOperacionesTotalV2(IdPersonal);
                combos.comboCentroCosto = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoParaFiltro();
                return (combos);
            }
            catch
            {
                combos.comboPersonal = null;
                combos.comboCentroCosto = null;
                return (combos);
            }

        }

        public ResultadoFiltroReporteCompromisoDTO ObtenerReporteCompromiso(GenerarReporteCompromisoPagoFiltroGrillaDTO Obj)
        {
            try
            {
                ResultadoFiltroReporteCompromisoDTO compromiso = new ResultadoFiltroReporteCompromisoDTO();
                if (Obj.Filtro.ListaCoordinador.Count() == 0)
                {
                    var asistentesCargo = _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoOperacionesTotalV2(Obj.Filtro.Personal);
                    List<int> ListaCoordinadortmp = new List<int>();
                    foreach (var item in asistentesCargo)
                    {
                        ListaCoordinadortmp.Add(item.Id);
                    }
                    Obj.Filtro.ListaCoordinador = ListaCoordinadortmp;
                    compromiso = _unitOfWork.MontoPagoCronogramaRepository.ObtenerReporteCompromisoPagoFiltrado(Obj.Paginador, Obj.Filtro, Obj.Filter);
                }
                else
                {
                    compromiso = _unitOfWork.MontoPagoCronogramaRepository.ObtenerReporteCompromisoPagoFiltrado(Obj.Paginador, Obj.Filtro, Obj.Filter);
                }
                return (compromiso);
            }
            catch (Exception ex)
            {
                return (null);
            }
            
        }
    }
}
