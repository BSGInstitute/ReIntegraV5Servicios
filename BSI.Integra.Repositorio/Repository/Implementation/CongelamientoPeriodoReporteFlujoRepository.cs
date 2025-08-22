using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CongelamientoPeriodoReporteFlujoRepository
    /// Autor: Adriana Chipana
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CongelamientoPeriodoReporteFlujo
    /// </summary>
    public class CongelamientoPeriodoReporteFlujoRepository : ICongelamientoPeriodoReporteFlujoRepository
    {
        private IDapperRepository _dapperRepository;
        public CongelamientoPeriodoReporteFlujoRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoPeriodoDTO> FlujoCongelamientoPeriodo)
        {
            try
            {
                bool items = false;
                foreach (var element in FlujoCongelamientoPeriodo)
                {
                    var query = _dapperRepository.QuerySPDapper("[fin].[SP_GenerarCongelamientoPeriodoReporteFlujo]", new
                    {
                        idPeriodo = element.idPeriodo,
                        periodo = element.periodo,
                        idMatriculaCabecera = element.idMatriculaCabecera,
                        idCoordAcademico = element.idCoordAcademico,
                        coordinadorAcademico = element.coordinadorAcademico,
                        idPespecifico = element.idPespecifico,
                        programa = element.programa,
                        codigoMatricula = element.codigoMatricula,
                        alumno = element.alumno,
                        fechaCuota = element.fechaCuota,
                        montoCuota = element.montoCuota,
                        fechaPago = element.fechaPago,
                        pago = element.pago,
                        saldoPendiente = element.saldoPendiente,
                        mora = element.mora,
                        nroCuota = element.nroCuota,
                        nroSubCuota = element.nroSubCuota,
                        moneda = element.moneda,
                        totalUSD = element.totalUSD,
                        realUSD = element.realUSD,
                        penUSD = element.penUSD,
                        Estado = element.Estado,
                        fechaCreacion = DateTime.Now,
                        fechaModificacion = DateTime.Now,
                        UsuarioCreacion = element.UsuarioCreacion,
                        UsuarioModificacion = element.UsuarioModificacion,
                    });
                    if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                    {
                        items = true;
                    }
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }



    }
}
