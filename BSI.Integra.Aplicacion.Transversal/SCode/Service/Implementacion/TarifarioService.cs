using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TarifarioService
    /// Autor: Jonathan Caipo
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de Tarifario
    /// </summary>
    public class TarifarioService : ITarifarioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TarifarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTarifario, Tarifario>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza mediante la gestión del Cronograma Pago Tarifario por medio del id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> bool </returns>
        public bool ActualizarGestionadoCronogramaPagoTarifario(int id)
        {
            try
            {
                return _unitOfWork.TarifarioRepository.ActualizarGestionadoCronogramaPagoTarifario(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return _unitOfWork.TarifarioRepository.ObtenerTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
