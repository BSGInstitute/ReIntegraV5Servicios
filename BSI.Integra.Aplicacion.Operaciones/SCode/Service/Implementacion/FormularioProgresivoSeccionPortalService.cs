using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: FormularioProgresivoSeccionPortalService
    /// Autor: Jorge Gamero
    /// Fecha: 27/02/2025
    /// <summary>
    /// Gestión general de T_FormularioProgresivoSeccionPortal
    /// </summary>
    public class FormularioProgresivoSeccionPortalService : IFormularioProgresivoSeccionPortalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioProgresivoSeccionPortalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormularioProgresivoSeccionPortal, FormularioProgresivoSeccionPortal>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 27/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FormularioProgresivoSeccionPortal> ObtenerRegistros()
        {
            try
            {
                return _unitOfWork.FormularioProgresivoSeccionPortalRepository.ObtenerRegistros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
