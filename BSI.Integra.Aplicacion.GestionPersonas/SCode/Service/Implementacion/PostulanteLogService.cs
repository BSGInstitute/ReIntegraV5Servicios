using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    public class PostulanteLogService : IPostulanteLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PostulanteLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteLog, PostulanteLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPostulanteLog, PostulanteLogDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteLog, PostulanteLogDTO>(MemberList.None).ReverseMap();
                //PostulanteLogv2 alterno
                //cfg.CreateMap<TPostulanteLog, PostulanteLogv2>(MemberList.None).ReverseMap();
                //cfg.CreateMap<PostulanteLogv2, PostulanteLogDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<PostulanteLogv2, TPostulanteLog>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region CRUDBASE
        public TPostulanteLog Add(PostulanteLog entidad)
        {
            try
            {
                return _unitOfWork.PostulanteLogRepository.Add(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPostulanteLog> Add(IEnumerable<PostulanteLog> listadoEntidad)
        {
            try
            {
                return _unitOfWork.PostulanteLogRepository.Add(listadoEntidad);
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
                return _unitOfWork.PostulanteLogRepository.Delete(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                return _unitOfWork.PostulanteLogRepository.Delete(listadoIds, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PostulanteLog? ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.PostulanteLogRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPostulanteLog Update(PostulanteLog entidad)
        {
            try
            {
                return _unitOfWork.PostulanteLogRepository.Update(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPostulanteLog> Update(IEnumerable<PostulanteLog> listadoEntidad)
        {
            try
            {
                return _unitOfWork.PostulanteLogRepository.Update(listadoEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Base


        public Boolean GuardarPostulanteLog(Postulante postulanteRegistroNuevo, IntegraAspNetUser integraUser, bool esNuevoRegistro)
        {
            try
            {
                return false;//_unitOfWork.PostulanteLogRepository.GuardarPostulanteLog(postulanteRegistroNuevo, integraUser, esNuevoRegistro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
