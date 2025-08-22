using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: InformacionWebinarService
    /// Autor: Gilmer Quispe.
    /// Fecha: 08/08/2022
    /// <summary>
    /// Gestión general de Informacion Webinar
    /// </summary>
    public class InformacionWebinarService : IInformacionWebinarService
    {
        private IUnitOfWork _unitOfWork;

        public InformacionWebinarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Giancarlo Romero
        /// Fecha: 22/05/2023
        /// Versión: 2.0
        /// <summary>
        /// Obtiene los combos para el modulo de informacion webinar
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        public (IEnumerable<PgeneralWebinarDTO> PGenerals, List<ComboDTO> Pespecificos, List<ComboDTO> CentroCostos) ObtenerCombosModulo()
        {
            var comboPgeneral = _unitOfWork.PGeneralRepository.ObtenerPGeneralWebinar();
            var comboPespecifico = _unitOfWork.PEspecificoRepository.ObtenerPEspecificoWebinar();
            var comboCentroCosto = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoWebinar();
            return (comboPgeneral, comboPespecifico, comboCentroCosto);
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 19/07/2023
        /// <summary>
        /// Obtiene los registros de Fecha, hora y estado sesion (Modulo Informacion Webinar)
        /// </summary>
        /// <param name="filtro">Objeto de clase WebinarReporteFiltroDTO</param>
        /// <returns>Lista de objetos de clase WebinarDDetalleSesionDTO</returns>
        public List<WebinarDetalleSesionDTO> ObtenerWebinarPorFiltro(WebinarReporteFiltroDTO filtro)
        {
            return _unitOfWork.PGeneralRepository.ObtenerWebinarPorFiltro(filtro).ToList();
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 19/07/2023
        /// <summary>
        /// Obtiene los registros de Fecha, hora y estado sesion (Modulo Informacion Webinar)
        /// </summary>
        /// <param name="idPGeneral"> (PK) De T_PGeneral </param>
        /// <returns>Lista de objetos de clase WebinarDDetalleSesionDTO</returns>
        public List<ComboDTO> ObtenerPEspecificoWebinar(int idPGeneral)
        {
            return _unitOfWork.PEspecificoRepository.ObtenerPEspecificoWebinarPorIdPGeneral(idPGeneral).ToList();
        }
    }
}
