using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PGeneralConfiguracionPlantillaService
    /// Autor: Jonathan Caipo
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de T_PGeneralConfiguracionPlantilla
    /// </summary>
    public class PGeneralConfiguracionPlantillaService : IPGeneralConfiguracionPlantillaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PGeneralConfiguracionPlantillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralConfiguracionPlantilla, PgeneralConfiguracionPlantilla>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos para la contancia por Matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> DatosGenerarCertificadoDTO </returns>
        public DatosGenerarCertificadoDTO ObtenerDatosParaConstanciasPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PGeneralConfiguracionPlantillaRepository.ObtenerDatosParaConstanciasPorMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
