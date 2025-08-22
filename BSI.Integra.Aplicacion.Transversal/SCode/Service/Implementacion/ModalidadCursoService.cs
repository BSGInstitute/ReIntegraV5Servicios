using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ModalidadCursoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 05/08/2022
    /// <summary>
    /// Gestión general de T_ModalidadCurso
    /// </summary>
    public class ModalidadCursoService : IModalidadCursoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ModalidadCursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TModalidadCurso, ModalidadCurso>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ModalidadCurso para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            return _unitOfWork.ModalidadCursoRepository.ObtenerCombo();
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ModalidadCurso
        /// </summary>
        /// <returns> Lista ModalidadCursoDTO</returns>
        public IEnumerable<ModalidadCursoDTO> Obtener()
        {
            return _unitOfWork.ModalidadCursoRepository.Obtener();
        }
    }
}
