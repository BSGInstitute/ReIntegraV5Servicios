using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion
{
    public class DetalleFraseReconocidaService : IDetalleFraseReconocidaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public DetalleFraseReconocidaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDetalleFraseReconocidum, DetalleFraseReconocida>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DetalleFraseReconocida Add(DetalleFraseReconocida entidad)
        {
            try
            {
                var modelo = _unitOfWork.DetalleFraseReconocidaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DetalleFraseReconocida>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DetalleFraseReconocida Update(DetalleFraseReconocida entidad)
        {
            try
            {
                var modelo = _unitOfWork.DetalleFraseReconocidaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DetalleFraseReconocida>(modelo);
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
                _unitOfWork.DetalleFraseReconocidaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleFraseReconocida> Add(List<DetalleFraseReconocida> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DetalleFraseReconocidaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DetalleFraseReconocida>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleFraseReconocida> Update(List<DetalleFraseReconocida> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DetalleFraseReconocidaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DetalleFraseReconocida>>(modelo);
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
                _unitOfWork.DetalleFraseReconocidaRepository.Delete(listadoIds, usuario);
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
