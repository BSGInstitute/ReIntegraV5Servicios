using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PGeneralDescripcionRepository
    /// Autor: GIlmer Quispe.
    /// Fecha: 19/06/2023
    /// <summary>
    /// Gestión general de T_PGeneralDescripcion
    /// </summary>
    public class PGeneralDescripcionRepository : GenericRepository<TPgeneralDescripcion>, IPGeneralDescripcionRepository
    {
        public PGeneralDescripcionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 19/06/2023
        /// <summary>
        ///  Obtiene la lista Descripción web General para un programa general registradas en el sistema(activos)
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral</param>
        /// <returns> IEnumerable<PGeneralDescripcionDTO> </returns>
        public IEnumerable<PGeneralDescripcionDTO> ObtenerPGeneralDescripcionPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = "SELECT Id,IdPGeneral AS IdPgeneral,Texto FROM pla.V_TPGeneralDescripcion WHERE Estado = 1 AND IdPGeneral = @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralDescripcionDTO>>(resultado)!;
                }
                return new List<PGeneralDescripcionDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
