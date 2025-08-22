using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: MandrilEnvioCorreoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 24/08/2022
    /// <summary>
    /// Gestión general de T_MandrilEnvioCorreoService
    /// </summary>
    public class MandrilEnvioCorreoService : IMandrilEnvioCorreoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MandrilEnvioCorreoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMandrilEnvioCorreo, MandrilEnvioCorreo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        public MandrilEnvioCorreo Add(MandrilEnvioCorreo entidad)
        {
            try
            {
                var modelo = _unitOfWork.MandrilEnvioCorreoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MandrilEnvioCorreo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MandrilEnvioCorreo> Add(List<MandrilEnvioCorreo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MandrilEnvioCorreoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MandrilEnvioCorreo>>(modelo);
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
                _unitOfWork.MandrilEnvioCorreoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
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
                _unitOfWork.MandrilEnvioCorreoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MandrilEnvioCorreo Update(MandrilEnvioCorreo entidad)
        {
            try
            {
                var modelo = _unitOfWork.MandrilEnvioCorreoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MandrilEnvioCorreo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MandrilEnvioCorreo> Update(List<MandrilEnvioCorreo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MandrilEnvioCorreoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MandrilEnvioCorreo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
