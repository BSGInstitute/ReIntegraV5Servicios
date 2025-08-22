using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteFurPorPagarRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteFurPorPagar
    /// </summary>
    public class ReporteFurPorPagarRepository : IReporteFurPorPagarRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteFurPorPagarRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// Obtiene la lista de furs que no tengan asociado ningun documento de pago y esten aprobados por jefe de finanzas
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <returns></returns>
        public List<FurPorPagarDTO> ObtenerFurPorPagarByFecha(DateTime? FechaInicial, DateTime? FechaFinal)
        {
            try
            {
                var camposTabla = "Empresa,Sede,Area,TipoPedido,CodigoFur,ProductoServicio,Cantidad,Unidades,PrecioUnitario,MonedaProveedor,Descripcion,CentroCosto,Atipico,Rubro,NroCuenta,Cuenta,UsuarioCreacion,UsuarioModificacion,RUC,Proveedor,MonedaReal,MontoAPagar,MontoAPagarDolares,FechaProgramada,MesProgramado,Estado";
                var queryParameters = new { fechaInicial = FechaInicial?.Date, fechaFinal = FechaFinal?.Date ?? DateTime.Now.Date };
                var queryCondition = "";

                if (FechaInicial.HasValue && FechaFinal.HasValue)
                {
                    queryCondition = "Convert(Date,FechaProgramada) >= @fechaInicial AND Convert(Date,FechaProgramada) <= @fechaFinal";
                }
                else if (FechaInicial.HasValue)
                {
                    queryCondition = "Convert(Date,FechaProgramada) >= @fechaInicial";
                }

                var query = $"SELECT {camposTabla} FROM FIN.V_ReporteFursPorPagar";
                if (!string.IsNullOrEmpty(queryCondition))
                {
                    query += $" WHERE {queryCondition}";
                }
                query += " ORDER BY Convert(Date,FechaProgramada) DESC";

                var furDB = _dapperRepository.QueryDapper(query, queryParameters);

                if (!string.IsNullOrEmpty(furDB) && !furDB.Contains("[]"))
                {
                    var listaFur = JsonConvert.DeserializeObject<List<FurPorPagarDTO>>(furDB);
                    return listaFur;
                }

                return new List<FurPorPagarDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
