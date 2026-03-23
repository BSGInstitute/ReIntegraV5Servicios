using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProyectoAplicacionRepository : IProyectoAplicacionRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public ProyectoAplicacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<ProyectoAplicacionPorMatriculaCabeceraDTO> ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper(
                    "ope.SP_ProyectoAplicacionObtenerPorIdMatriculaCabecera",
                    new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ProyectoAplicacionPorMatriculaCabeceraDTO>>(resultado)
                        ?? new List<ProyectoAplicacionPorMatriculaCabeceraDTO>();
                }
                return new List<ProyectoAplicacionPorMatriculaCabeceraDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
