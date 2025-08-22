using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: MatriculaCabeceraBeneficiosService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_MatriculaCabeceraBeneficios
    /// </summary>
    public class MatriculaCabeceraBeneficiosService : IMatriculaCabeceraBeneficiosService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MatriculaCabeceraBeneficiosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMatriculaCabeceraBeneficio, MatriculaCabeceraBeneficios>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public MatriculaCabeceraBeneficios Add(MatriculaCabeceraBeneficios entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraBeneficiosRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MatriculaCabeceraBeneficios>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MatriculaCabeceraBeneficios Update(MatriculaCabeceraBeneficios entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraBeneficiosRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MatriculaCabeceraBeneficios>(modelo);
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
                _unitOfWork.MatriculaCabeceraBeneficiosRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaCabeceraBeneficios> Add(List<MatriculaCabeceraBeneficios> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraBeneficiosRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaCabeceraBeneficios>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaCabeceraBeneficios> Update(List<MatriculaCabeceraBeneficios> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraBeneficiosRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaCabeceraBeneficios>>(modelo);
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
                _unitOfWork.MatriculaCabeceraBeneficiosRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabeceraBeneficios
        /// </summary>
        /// <returns> List<MatriculaCabeceraBeneficiosDTO> </returns>
        public IEnumerable<MatriculaCabeceraBeneficiosDTO> ObtenerMatriculaCabeceraBeneficios()
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraBeneficiosRepository.ObtenerMatriculaCabeceraBeneficios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MatriculaCabeceraBeneficios para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<MatriculaCabeceraBeneficiosComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraBeneficiosRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Nombre de los Beneficios asociados a una Matricula Cabecera.
        /// </summary>
        /// <param name="version">Version de matricula alumno</param>
        /// <returns> List<string> </returns>
        public IEnumerable<StringDTO> ObtenerBeneficiosPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraBeneficiosRepository.ObtenerBeneficiosPorMatriculaCabecera(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BeneficiosSolicitadosDTO> ObtenerBeneficiosSolicitadosSinRepetir()
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraBeneficiosRepository.ObtenerBeneficiosSolicitadosSinRepetir();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
