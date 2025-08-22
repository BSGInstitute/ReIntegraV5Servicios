using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: RemitenteMailingAsesorService
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 09/11/2022
    /// <summary>
    /// Gestión general de T_RemitenteMailingAsesor
    /// </summary>
    public class RemitenteMailingAsesorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RemitenteMailingAsesorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRemitenteMailingAsesor, RemitenteMailingAsesor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RemitenteMailingAsesor Add(RemitenteMailingAsesor entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemitenteMailingAsesorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemitenteMailingAsesor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RemitenteMailingAsesor Update(RemitenteMailingAsesor entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemitenteMailingAsesorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemitenteMailingAsesor>(modelo);
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
                _unitOfWork.RemitenteMailingAsesorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemitenteMailingAsesor> Add(List<RemitenteMailingAsesor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemitenteMailingAsesorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemitenteMailingAsesor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemitenteMailingAsesor> Update(List<RemitenteMailingAsesor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemitenteMailingAsesorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemitenteMailingAsesor>>(modelo);
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
                _unitOfWork.RemitenteMailingAsesorRepository.Delete(listadoIds, usuario);
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
