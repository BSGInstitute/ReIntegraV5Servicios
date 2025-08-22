using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    public class RegistroCertificadoFisicoGeneradoService : IRegistroCertificadoFisicoGeneradoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RegistroCertificadoFisicoGeneradoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRegistroCertificadoFisicoGenerado, RegistroCertificadoFisicoGenerado>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RegistroCertificadoFisicoGenerado Add(RegistroCertificadoFisicoGenerado entidad)
        {
            try
            {
                var modelo = _unitOfWork.RegistroCertificadoFisicoGeneradoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RegistroCertificadoFisicoGenerado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RegistroCertificadoFisicoGenerado Update(RegistroCertificadoFisicoGenerado entidad)
        {
            try
            {
                var modelo = _unitOfWork.RegistroCertificadoFisicoGeneradoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RegistroCertificadoFisicoGenerado>(modelo);
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
                _unitOfWork.RegistroCertificadoFisicoGeneradoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegistroCertificadoFisicoGenerado> Add(List<RegistroCertificadoFisicoGenerado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RegistroCertificadoFisicoGeneradoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RegistroCertificadoFisicoGenerado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegistroCertificadoFisicoGenerado> Update(List<RegistroCertificadoFisicoGenerado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RegistroCertificadoFisicoGeneradoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RegistroCertificadoFisicoGenerado>>(modelo);
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
                _unitOfWork.RegistroCertificadoFisicoGeneradoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
