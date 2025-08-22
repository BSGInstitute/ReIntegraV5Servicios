using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: DepartamentoPaiService
    /// Autor: Jorge Gamero.
    /// Fecha: 19/09/2024
    /// <summary>
    /// Gestión general de T_DepartamentoPais
    /// </summary>
    public class DepartamentoPaiService : IDepartamentoPaiService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DepartamentoPaiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDepartamentoPai, DepartamentoPai>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 19/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DepartamentoPaiRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 25/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene código de departamento por Id
        /// </summary> 
        /// <returns> CodigoDepartamentoDTO </returns>
        public CodigoDepartamentoDTO ObtenerCodigoPorId(int id)
        {
            try
            {
                return _unitOfWork.DepartamentoPaiRepository.ObtenerCodigoPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
