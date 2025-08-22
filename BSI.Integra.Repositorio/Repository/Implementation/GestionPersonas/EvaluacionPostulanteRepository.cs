using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Service: ReportePostulanteRepository
    /// Autor: Flavio R.M.F.
    /// Fecha: 04/06/2024
    /// <summary>
    /// Repositorio de Reporte de Postulantes
    /// </summary>
    public class EvaluacionPostulanteRepository : IEvaluacionPostulanteRepository
    {
        private IDapperRepository _dapperRepository;
        public EvaluacionPostulanteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
    }
}
