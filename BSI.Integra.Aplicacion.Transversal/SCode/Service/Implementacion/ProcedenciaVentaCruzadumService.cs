using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProcedenciaVentaCruzadumService
    /// Autor: Gilmer Quispe.
    /// Fecha: 29/09/2022
    /// <summary>
    /// Gestión general de ProcedenciaVentaCruzada
    /// </summary>
    public class ProcedenciaVentaCruzadumService : IProcedenciaVentaCruzadumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProcedenciaVentaCruzadumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProcedenciaVentaCruzadum, ProcedenciaVentaCruzadum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// inserta un nueva procedencia de venta cruzada
        /// </summary>
        /// <returns> true o false </returns>
        public bool InsertarProcedenciaVentaCruzada(int IdOportunidadActual, int IdOportunidadNueva)
        {
            try
            {
                return _unitOfWork.ProcedenciaVentaCruzadumRepository.InsertarProcedenciaVentaCruzada(IdOportunidadActual, IdOportunidadNueva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
