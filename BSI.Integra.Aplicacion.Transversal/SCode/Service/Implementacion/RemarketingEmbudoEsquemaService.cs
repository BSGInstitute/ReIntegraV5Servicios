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
    /// Service: RemarketingEmbudoEsquemaService
    /// Autor: Max Mantilla
    /// Fecha: 06/02/2026
    /// <summary>
    /// Gestión general de T_RemarketingEmbudoEsquema
    /// </summary>
    public class RemarketingEmbudoEsquemaService : IRemarketingEmbudoEsquemaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RemarketingEmbudoEsquemaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRemarketingEmbudoEsquema, RemarketingEmbudoEsquema>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RemarketingEmbudoEsquema Add(RemarketingEmbudoEsquema entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoEsquemaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemarketingEmbudoEsquema>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RemarketingEmbudoEsquema Update(RemarketingEmbudoEsquema entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoEsquemaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemarketingEmbudoEsquema>(modelo);
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
                _unitOfWork.RemarketingEmbudoEsquemaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemarketingEmbudoEsquema> Add(List<RemarketingEmbudoEsquema> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoEsquemaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemarketingEmbudoEsquema>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemarketingEmbudoEsquema> Update(List<RemarketingEmbudoEsquema> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoEsquemaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemarketingEmbudoEsquema>>(modelo);
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
                _unitOfWork.RemarketingEmbudoEsquemaRepository.Delete(listadoIds, usuario);
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
