using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Transversal.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace BSI.Integra.Aplicacion.Transversal.SCode.Service.Implementacion
{
    /// Service: PersonalLogService
    /// Autor: Victor Hinojosa
    /// Fecha: 30/09/2024
    /// <summary>
    /// Gestión general de T_PersonalLog
    /// </summary>
    public class PersonalLogService : IPersonalLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PersonalLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPersonalLog, PersonalLog>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        public PersonalLog Add(PersonalLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalLogRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PersonalLog>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PersonalLog Update(PersonalLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalLogRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PersonalLog>(modelo);
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
                _unitOfWork.PersonalLogRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonalLog> Add(List<PersonalLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalLogRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PersonalLog>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonalLog> Update(List<PersonalLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalLogRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PersonalLog>>(modelo);
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
                _unitOfWork.PersonalLogRepository.Delete(listadoIds, usuario);
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
