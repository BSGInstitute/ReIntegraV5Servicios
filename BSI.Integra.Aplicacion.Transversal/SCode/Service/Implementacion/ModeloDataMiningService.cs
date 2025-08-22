using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ModeloDataMiningService
    /// Autor: Gilmer Quispe.
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión general de ModeloDataMining
    /// </summary>
    public class ModeloDataMiningService : IModeloDataMiningService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ModeloDataMiningService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TModeloDataMining, ModeloDataMining>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ModeloDataMining Add(ModeloDataMining entidad)
        {
            try
            {
                var modelo = _unitOfWork.ModeloDataMiningRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ModeloDataMining>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ModeloDataMining Update(ModeloDataMining entidad)
        {
            try
            {
                var modelo = _unitOfWork.ModeloDataMiningRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ModeloDataMining>(modelo);
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
                _unitOfWork.ModeloDataMiningRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ModeloDataMining> Add(List<ModeloDataMining> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ModeloDataMiningRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ModeloDataMining>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ModeloDataMining> Update(List<ModeloDataMining> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ModeloDataMiningRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ModeloDataMining>>(modelo);
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
                _unitOfWork.ModeloDataMiningRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lis de modeloDataMining por la oportunidad
        /// </summary>
        /// <param name="idOportunidad"> id de la oportunidad </param>
        /// <returns> Entidad List<ModeloDataMining> </returns> 
        public List<ValorIntDTO> ListaPorOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ModeloDataMiningRepository.ObtenerListaPorOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el modeloDataMining por la oportunidad
        /// </summary>
        /// <param name="idOportunidad"> id de la oportunidad </param>
        /// <returns> Entidad ModeloDataMining </returns>
        public ModeloDataMining ObtenerPorOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ModeloDataMiningRepository.ObtenerPorOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la probabilidad de la oportunidad
        /// </summary>
        /// <param name="idOportunidad"> id de la oportunidad </param>
        /// <returns> Entidad ProbabilidadModeloDataMiningDTO </returns>
        public void ObtenerProbabilidad(int idOportunidad, ref ModeloDataMining modeloDataMining)
        {
            try
            {
                var probabilidad = _unitOfWork.ModeloDataMiningRepository.ObtenerProbabilidad(idOportunidad);
                modeloDataMining.ProbabilidadInicial = probabilidad.Probabilidad ?? 0;
                modeloDataMining.ProbabilidadActual = probabilidad.Probabilidad ?? 0;
                modeloDataMining.IdProbabilidadRegistroPwInicial = probabilidad.IdProbabilidaRegistroPW ?? ValorEstatico.IdProbabilidadRegistroSinProbabilidad;
                modeloDataMining.IdProbabilidadRegistroPwActual = probabilidad.IdProbabilidaRegistroPW ?? ValorEstatico.IdProbabilidadRegistroSinProbabilidad;
                modeloDataMining.IdAreaFormacion = probabilidad.IdareaFormacion ?? 0;
                modeloDataMining.IdCargo = probabilidad.IdCargo ?? 0;
                modeloDataMining.IdIndustria = probabilidad.IdIndustria ?? 0;
                modeloDataMining.IdAreaTrabajo = probabilidad.IdAreaTrabajo ?? 0;
                modeloDataMining.IdCategoriaOrigen = probabilidad.IdCategoriaDato ?? 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la probabilidad de la oportunidad
        /// </summary>
        /// <param name="idOportunidad"> id de la oportunidad </param>
        /// <returns> Entidad ProbabilidadModeloDataMiningDTO </returns>
        public ProbabilidadModeloDTO ObtenerProbabilidadCalculada(int idOportunidad)
        {
            try
            {
                var probabilidad = _unitOfWork.ModeloDataMiningRepository.ObtenerProbabilidad(idOportunidad);
                var prob = new ProbabilidadModeloDTO();

                prob.ProbabilidadInicial = probabilidad.Probabilidad == null ? 0 : probabilidad.Probabilidad;
                prob.ProbabilidadActual = probabilidad.Probabilidad == null ? 0 : probabilidad.Probabilidad;
                prob.IdProbabilidadRegistroPwInicial = probabilidad.IdProbabilidaRegistroPW == null ? /* ValorEstatico.IdProbabilidadRegistroSinProbabilidad */ 1 : probabilidad.IdProbabilidaRegistroPW;
                prob.IdProbabilidadRegistroPwActual = probabilidad.IdProbabilidaRegistroPW == null ?  /* ValorEstatico.IdProbabilidadRegistroSinProbabilidad */ 1 : probabilidad.IdProbabilidaRegistroPW;
                prob.IdAreaFormacion = probabilidad.IdareaFormacion == null ? 0 : probabilidad.IdareaFormacion;
                prob.IdCargo = probabilidad.IdCargo == null ? 0 : probabilidad.IdCargo;
                prob.IdIndustria = probabilidad.IdIndustria == null ? 0 : probabilidad.IdIndustria;
                prob.IdAreaTrabajo = probabilidad.IdAreaTrabajo == null ? 0 : probabilidad.IdAreaTrabajo;
                prob.IdCategoriaOrigen = probabilidad.IdCategoriaDato == null ? 0 : probabilidad.IdCategoriaDato;
                return prob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ModeloDataMining ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.ModeloDataMiningRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
