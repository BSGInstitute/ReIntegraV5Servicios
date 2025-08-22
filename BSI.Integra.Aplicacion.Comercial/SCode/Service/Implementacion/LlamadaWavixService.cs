using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    public class LlamadaWavixService : ILlamadaWavixService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public LlamadaWavixService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLlamadaWavixWebhook, LlamadaWavixWebhook>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public LlamadaWavixWebhook Add(LlamadaWavixWebHookDTO data, string Usuario)
        {
            try
            {
                LlamadaWavixWebhook entidad = _mapper.Map<LlamadaWavixWebhook>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.LlamadaWavixWebhookRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LlamadaWavixWebhook>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LlamadaWavixWebhook Update(LlamadaWavixWebHookDTO data, string Usuario)
        {
            try
            {
                var repositorioLlamadaWavixWebhook = _unitOfWork.LlamadaWavixWebhookRepository;
                var entidad = _mapper.Map<LlamadaWavixWebhook>(repositorioLlamadaWavixWebhook.FirstById(data.Id == null ? 0 : data.Id.Value));
                entidad.Uuid = data.Uuid;
                entidad.AnsweredBy = data.EstadoLlamada;
                entidad.Charge = null;
                entidad.Date = null;
                entidad.Destination = null;
                entidad.Disposition = data.OcurrenciaLlamada;
                entidad.Duration = null;
                entidad.From = data.Origen;
                entidad.PerMinute = null;
                entidad.RecordingUrl = null;
                entidad.SipTrunk = data.TroncalSIP;
                entidad.To = data.Destino;
                entidad.Transcription = null;
                entidad.Tipo = null;

                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.LlamadaWavixWebhookRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LlamadaWavixWebhook>(modelo);
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
                _unitOfWork.LlamadaWavixWebhookRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LlamadaWavixWebhook> Add(List<LlamadaWavixWebhook> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LlamadaWavixWebhookRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LlamadaWavixWebhook>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LlamadaWavixWebhook> Update(List<LlamadaWavixWebhook> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LlamadaWavixWebhookRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LlamadaWavixWebhook>>(modelo);
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
                _unitOfWork.LlamadaWavixWebhookRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public bool? GuardarLlamadaWebhook(LlamadaWavixWebHookDTO llamada)
        {
            try
            {
                return _unitOfWork.LlamadaWavixWebhookRepository.GuardarLlamadaWebhook(llamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool? GuardarLlamadaEntranteWebhook(LlamadaWavixEntranteDTO llamada)
        {
            try
            {
                return _unitOfWork.LlamadaWavixWebhookRepository.GuardarLlamadaEntranteWebhook(llamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}