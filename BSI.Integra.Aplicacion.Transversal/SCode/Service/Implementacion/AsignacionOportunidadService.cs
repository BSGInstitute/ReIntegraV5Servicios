using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AsignacionOportunidadService
    /// Autor: Gilmer Quispe.
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión general de T_AsignacionOportunidad
    /// </summary>
    public class AsignacionOportunidadService : IAsignacionOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AsignacionOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionOportunidad, AsignacionOportunidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public AsignacionOportunidad Add(AsignacionOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionOportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionOportunidad Update(AsignacionOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionOportunidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionOportunidad>(modelo);
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
                _unitOfWork.AsignacionOportunidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionOportunidad> Add(List<AsignacionOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionOportunidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionOportunidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionOportunidad> Update(List<AsignacionOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionOportunidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionOportunidad>>(modelo);
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
                _unitOfWork.AsignacionOportunidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la entidad por el IdOportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <returns> Entidad AsignacionOportunidad </returns>
        public AsignacionOportunidad AsignacionPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.AsignacionOportunidadRepository.ObtenerPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de oportunidades asignadas por una fecha determinada
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <param name="fechaAsignacion"></param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerCantidadOportunidadesAsesor(int idAsesor, DateTime fechaAsignacion)
        {
            try
            {
                return _unitOfWork.AsignacionOportunidadRepository.ObtenerCantidadOportunidadesAsesor(idAsesor, fechaAsignacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Máxima asignacion por el asesor
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerMaximaAsignacionAsesor(int idAsesor)
        {
            try
            {
                return _unitOfWork.AsignacionOportunidadRepository.ObtenerMaximaAsignacionAsesor(idAsesor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///Autor:Margiory Ramirez Neyra
        ///Fecha: 03/11/2022
        /// <summary>
        /// Obtiene una AsignacionOportunidaBO por idOportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> ObjetoBO: AsignacionOportunidadBO </returns>
        public AsignacionOportunidad ObtenerPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.AsignacionOportunidadRepository.ObtenerPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
