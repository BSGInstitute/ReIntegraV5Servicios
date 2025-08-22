using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadRemarketingAgendaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_OportunidadRemarketingAgenda
    /// </summary>
    public class OportunidadRemarketingAgendaService : IOportunidadRemarketingAgendaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadRemarketingAgendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOportunidadRemarketingAgendum, OportunidadRemarketingAgenda>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OportunidadRemarketingAgenda Add(OportunidadRemarketingAgenda entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadRemarketingAgendaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadRemarketingAgenda>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadRemarketingAgenda Update(OportunidadRemarketingAgenda entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadRemarketingAgendaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadRemarketingAgenda>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.OportunidadRemarketingAgendaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadRemarketingAgenda> Add(List<OportunidadRemarketingAgenda> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadRemarketingAgendaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadRemarketingAgenda>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadRemarketingAgenda> Update(List<OportunidadRemarketingAgenda> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadRemarketingAgendaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadRemarketingAgenda>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.OportunidadRemarketingAgendaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OportunidadRemarketingAgenda
        /// </summary>
        /// <returns> List<OportunidadRemarketingAgendaDTO> </returns>
        public IEnumerable<OportunidadRemarketingAgendaDTO> ObtenerOportunidadRemarketingAgenda()
        {
            try
            {
                return _unitOfWork.OportunidadRemarketingAgendaRepository.ObtenerOportunidadRemarketingAgenda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Desactivar la redireccion de remarketing
        /// </summary>
        /// <returns>bool</returns>
        public bool DesactivarRedireccionRemarketingAnterior(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnterior(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
