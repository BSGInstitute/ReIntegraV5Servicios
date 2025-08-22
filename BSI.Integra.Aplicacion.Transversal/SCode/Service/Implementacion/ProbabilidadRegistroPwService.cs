using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProbabilidadRegistroPwService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ProbabilidadRegistro_Pw
    /// </summary>
    public class ProbabilidadRegistroPwService : IProbabilidadRegistroPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProbabilidadRegistroPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProbabilidadRegistroPw, ProbabilidadRegistroPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProbabilidadRegistroPw Add(ProbabilidadRegistroPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProbabilidadRegistroPwRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProbabilidadRegistroPw>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProbabilidadRegistroPw Update(ProbabilidadRegistroPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProbabilidadRegistroPwRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProbabilidadRegistroPw>(modelo);
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
                _unitOfWork.ProbabilidadRegistroPwRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProbabilidadRegistroPw> Add(List<ProbabilidadRegistroPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProbabilidadRegistroPwRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProbabilidadRegistroPw>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProbabilidadRegistroPw> Update(List<ProbabilidadRegistroPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProbabilidadRegistroPwRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProbabilidadRegistroPw>>(modelo);
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
                _unitOfWork.ProbabilidadRegistroPwRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProbabilidadRegistro_Pw para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProbabilidadRegistroPwRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProbabilidadRegistro_Pw
        /// </summary>
        /// <returns> List<ProbabilidadRegistroPwDTO> </returns>
        public IEnumerable<ProbabilidadRegistroPwDTO> ObtenerProbabilidadRegistroPw()
        {
            try
            {
                return _unitOfWork.ProbabilidadRegistroPwRepository.ObtenerProbabilidadRegistroPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProbabilidadRegistro_Pw
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerTodoFiltro()
        {
            try
            {
                return _unitOfWork.ProbabilidadRegistroPwRepository.ObtenerTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de T_ProbabilidadRegistro_Pw por el Id
        /// </summary>
        /// <param name="id">Id de T_ProbabilidadRegistro_Pw </param>
        /// <returns> ProbabilidadRegistroPwDTO </returns>
        public ProbabilidadRegistroPwDTO ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.ProbabilidadRegistroPwRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
