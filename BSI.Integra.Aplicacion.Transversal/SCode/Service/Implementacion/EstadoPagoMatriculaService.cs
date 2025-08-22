using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EstadoPagoMatriculaService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_EstadoPagoMatricula
    /// </summary>
    public class EstadoPagoMatriculaService : IEstadoPagoMatriculaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstadoPagoMatriculaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoInteracccion, EstadoPagoMatricula>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EstadoPagoMatricula Add(EstadoPagoMatricula entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoPagoMatriculaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoPagoMatricula>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoPagoMatricula Update(EstadoPagoMatricula entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoPagoMatriculaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoPagoMatricula>(modelo);
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
                _unitOfWork.EstadoPagoMatriculaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoPagoMatricula> Add(List<EstadoPagoMatricula> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoPagoMatriculaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoPagoMatricula>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoPagoMatricula> Update(List<EstadoPagoMatricula> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoPagoMatriculaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoPagoMatricula>>(modelo);
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
                _unitOfWork.EstadoPagoMatriculaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


       public List<DTO.ComboDTO> ObtenerTodoFiltro()
        {
            try
            {
                return _unitOfWork.EstadoPagoMatriculaRepository.ObtenerTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto
        /// Fecha: 25/07/2023
        /// Versión: 1.0
        /// <summary>
		/// Obtiene el Id y Nombre del estado pago Matricula (pormatricular y matriculado)
		/// </summary>
		/// <returns></returns>
        public List<ComboDTO> ObtenerEstadoPagoMatriculaDevoluciones()
        {
            try
            {
                return _unitOfWork.EstadoPagoMatriculaRepository.ObtenerEstadoPagoMatriculaDevoluciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
