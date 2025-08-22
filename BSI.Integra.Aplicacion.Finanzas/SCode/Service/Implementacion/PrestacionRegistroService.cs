using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: PrestacionRegistroService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_PrestacionRegistro
    /// </summary>
    public class PrestacionRegistroService : IPrestacionRegistroService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PrestacionRegistroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPrestacionRegistro, PrestacionRegistro>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PrestacionRegistro Add(PrestacionRegistro entidad)
        {
            try
            {
                var modelo = _unitOfWork.PrestacionRegistroRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PrestacionRegistro>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrestacionRegistro Update(PrestacionRegistro entidad)
        {
            try
            {
                var modelo = _unitOfWork.PrestacionRegistroRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PrestacionRegistro>(modelo);
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
                _unitOfWork.PrestacionRegistroRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PrestacionRegistro> Add(List<PrestacionRegistro> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PrestacionRegistroRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PrestacionRegistro>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PrestacionRegistro> Update(List<PrestacionRegistro> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PrestacionRegistroRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PrestacionRegistro>>(modelo);
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
                _unitOfWork.PrestacionRegistroRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PrestacionRegistro para mostrarse en combo.
        /// </summary>
        /// <returns> List<PrestacionRegistroComboDTO> </returns>
        public IEnumerable<PrestacionRegistroComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PrestacionRegistroRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
