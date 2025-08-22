using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReportePresupuestoRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePresupuesto
    /// </summary>
    public class ReportePresupuestoRepository : IReportePresupuestoRepository
    {
        private IDapperRepository _dapperRepository;
        public ReportePresupuestoRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// Obtiene la lista de furs que no tengan asociado ningun documento de pago y esten aprobados por jefe de finanzas
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <returns></returns>
        /// <summary>
        /// Obtiene toda la data de los campos para el Reporte de Presupuesto 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportePresupuestoDTO> ObtenerReportePresupuestoFinanzas(FiltroPresupuestoDTO filtros)
        {
            try
            {
                IEnumerable<ReportePresupuestoDTO> items = new List<ReportePresupuestoDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePresupuestoV5]", new {
                     IdSede = filtros.idSede,
                     IdCuenta = filtros.idCuenta,
                     IdUsuarioCreacion = filtros.idUsarioCreacion,
                     IdRubro = filtros.idRubro,
                     IdTipoPedido = filtros.idTipoPedido,
                     IdCentroCosto = filtros.idCentroCosto,
                     IdProveedor = filtros.idProveedor,
                     IdFur = filtros.idFur,
                     IdFaseFur = filtros.idEstadoFur,
                     periodoProgramacionOriginal  = filtros.idsPeridoProgramacionOriginal,
                     periodoProgramacionActual = filtros.idPeridoProgramacionActual ,
                     periodoFechaLimiteFur = filtros.idsPeridoFechaLimite
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<IEnumerable<ReportePresupuestoDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
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
                IEnumerable<ReportePresupuestoDTO> items = new List<ReportePresupuestoDTO>();
                var query = _dapperRepository.QuerySPDapper("fin.SP_ActualizarEsDiferidoListaFur", new
                {
                    IdsFur = datos.IdsFur,
                    EsDiferido = datos.EsDiferido,
                    FechaLimiteReprogramacion = datos.FechaLimiteReprogramacion,
                    Usuario = datos.Usuario,
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query!="null")
                {
                    return true;
                }
                else return false;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


    }
}
