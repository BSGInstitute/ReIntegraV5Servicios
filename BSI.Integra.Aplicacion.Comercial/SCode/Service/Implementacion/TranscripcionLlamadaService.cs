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
    /// Service: TranscripcionLlamadaService
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2023
    /// <summary>
    /// Gestión general de T_SolicitudAlumno
    /// </summary>
    public class TranscripcionLlamadaService : ITranscripcionLlamadaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TranscripcionLlamadaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTranscripcionLlamadum, TranscripcionLlamada>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public TranscripcionLlamada Add(TranscripcionLlamada entidad)
        {
            try
            {
                var modelo = _unitOfWork.TranscripcionLlamadaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TranscripcionLlamada>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TranscripcionLlamada Update(TranscripcionLlamada entidad)
        {
            try
            {
                var modelo = _unitOfWork.TranscripcionLlamadaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TranscripcionLlamada>(modelo);
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
                _unitOfWork.TranscripcionLlamadaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TranscripcionLlamada> Add(List<TranscripcionLlamada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TranscripcionLlamadaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TranscripcionLlamada>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TranscripcionLlamada> Update(List<TranscripcionLlamada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TranscripcionLlamadaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TranscripcionLlamada>>(modelo);
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
                _unitOfWork.TranscripcionLlamadaRepository.Delete(listadoIds, usuario);
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
