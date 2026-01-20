using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: RemarketingEmbudoNivelService
    /// Autor: Max Mantilla
    /// Fecha: 06/02/2026
    /// <summary>
    /// Gestión general de T_RemarketingEmbudoNivel
    /// </summary>
    public class RemarketingEmbudoNivelService : IRemarketingEmbudoNivelService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RemarketingEmbudoNivelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRemarketingEmbudoNivel, RemarketingEmbudoNivel>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RemarketingEmbudoNivel Add(RemarketingEmbudoNivel entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoNivelRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemarketingEmbudoNivel>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RemarketingEmbudoNivel Update(RemarketingEmbudoNivel entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoNivelRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemarketingEmbudoNivel>(modelo);
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
                _unitOfWork.RemarketingEmbudoNivelRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemarketingEmbudoNivel> Add(List<RemarketingEmbudoNivel> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoNivelRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemarketingEmbudoNivel>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemarketingEmbudoNivel> Update(List<RemarketingEmbudoNivel> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoNivelRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemarketingEmbudoNivel>>(modelo);
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
                _unitOfWork.RemarketingEmbudoNivelRepository.Delete(listadoIds, usuario);
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
