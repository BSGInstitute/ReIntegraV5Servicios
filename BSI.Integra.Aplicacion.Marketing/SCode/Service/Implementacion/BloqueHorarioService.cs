using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service:BloqueHorarioService
    /// Autor:  Gilmer Quispe.
    /// Fecha:  21/06/2022
    /// <summary>
    /// Gestión general de T_BloqueHorario
    /// </summary>
    public class BloqueHorarioService : IBloqueHorarioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public BloqueHorarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TBloqueHorario, BloqueHorario>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de TBloqueHorario.
        /// </summary>
        /// <returns> List<BloqueHorarioProcesarBicDTO> </returns>
        public IEnumerable<BloqueHorarioProcesarBicDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.BloqueHorarioRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
