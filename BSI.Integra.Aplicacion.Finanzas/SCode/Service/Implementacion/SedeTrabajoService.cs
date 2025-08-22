using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: SedeTrabajoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_SedeTrabajo
    /// </summary>
    public class SedeTrabajoService : ISedeTrabajoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SedeTrabajoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSedeTrabajo, SedeTrabajo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SedeTrabajo
        /// </summary>
        /// <returns> List<SedeTrabajoDTO> </returns>
        public IEnumerable<SedeTrabajoComboDTO> ObtenerSedeTrabajoCombo()
        {
            try
            {
                return _unitOfWork.SedeTrabajoRepository.ObtenerSedeTrabajoCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
