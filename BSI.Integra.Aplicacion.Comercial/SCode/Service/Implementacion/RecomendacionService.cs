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
    public class RecomendacionService : IRecomendacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public RecomendacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRecomendacionTranscripcion, RecomendacionTranscripcion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RecomendacionTranscripcion Add(RecomendacionTranscripcion entidad)
        {
            try
            {
                var modelo = _unitOfWork.RecomendacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RecomendacionTranscripcion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecomendacionTranscripcion Update(RecomendacionTranscripcion entidad)
        {
            try
            {
                var modelo = _unitOfWork.RecomendacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RecomendacionTranscripcion>(modelo);
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
                _unitOfWork.RecomendacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecomendacionTranscripcion> Add(List<RecomendacionTranscripcion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RecomendacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RecomendacionTranscripcion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecomendacionTranscripcion> Update(List<RecomendacionTranscripcion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RecomendacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RecomendacionTranscripcion>>(modelo);
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
                _unitOfWork.RecomendacionRepository.Delete(listadoIds, usuario);
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
