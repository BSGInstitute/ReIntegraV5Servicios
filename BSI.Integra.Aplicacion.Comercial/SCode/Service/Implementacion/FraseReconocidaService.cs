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
    public class FraseReconocidaService : IFraseReconocidaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public FraseReconocidaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFraseReconocidum, FraseReconocida>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FraseReconocida Add(FraseReconocida entidad)
        {
            try
            {
                var modelo = _unitOfWork.FraseReconocidaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FraseReconocida>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FraseReconocida Update(FraseReconocida entidad)
        {
            try
            {
                var modelo = _unitOfWork.FraseReconocidaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FraseReconocida>(modelo);
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
                _unitOfWork.FraseReconocidaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FraseReconocida> Add(List<FraseReconocida> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FraseReconocidaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FraseReconocida>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FraseReconocida> Update(List<FraseReconocida> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FraseReconocidaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FraseReconocida>>(modelo);
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
                _unitOfWork.FraseReconocidaRepository.Delete(listadoIds, usuario);
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
