using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: CiudadDepartamentoPaiService
    /// Autor: Jorge Gamero.
    /// Fecha: 20/09/2024
    /// <summary>
    /// Gestión general de T_CiudadDepartamentoPais
    /// </summary>
    public class CiudadDepartamentoPaiService : ICiudadDepartamentoPaiService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CiudadDepartamentoPaiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCiudadDepartamentoPai, CiudadDepartamentoPai>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 20/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_CiudadDepartamentoPais por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> List<CiudadDepartamentoPai></CiudadDepartamentoPai> </returns>
        public IEnumerable<CiudadDepartamentoPai> ObtenerPorId(int idDepartamentoPais)
        {
            try
            {
                return _unitOfWork.CiudadDepartamentoPaiRepository.ObtenerPorId(idDepartamentoPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 20/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CiudadDepartamentoPaiRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 25/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene código de ciudad por Id
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> CodigoCiudadDTO </returns>
        public CodigoCiudadDTO ObtenerCodigoPorId(int id)
        {
            try
            {
                return _unitOfWork.CiudadDepartamentoPaiRepository.ObtenerCodigoPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
