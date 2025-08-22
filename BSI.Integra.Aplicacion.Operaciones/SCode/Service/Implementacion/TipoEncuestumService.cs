using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{

    /// Service: TipoEncuestumService
    /// Autor: Jorge Gamero
    /// Fecha: 26/03/2025
    /// <summary>
    /// Gestión general de T_TipoEncuesta
    /// </summary>
    public class TipoEncuestumService : ITipoEncuestumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoEncuestumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoEncuestum, TipoEncuestum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla T_TipoEncuesta
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoEncuestumRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 24/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la vista V_TModalidadCurso_Filtro
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        /// public async Task<CriterioEvaluacionComboDTO> ObtenerCombosModulo()
        public IEnumerable<ComboDTO> ObtenerComboTipoModalidad()
        {
            try
            {
                return _unitOfWork.TipoEncuestumRepository.ObtenerComboTipoModalidad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
