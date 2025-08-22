using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class PersonalEncargadoDeEnvioDeConsultumService : IPersonalEncargadoDeEnvioDeConsultumService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PersonalEncargadoDeEnvioDeConsultumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TPersonalEncargadoDeEnvioDeConsultum, PersonalEncargadoDeEnvioDeConsultum>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalEncargadoDeEnvioDeConsultum, PersonalEncargadoDeEnvioDeConsultumDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalEncargadoDeEnvioDeConsultumDTO, TPersonalEncargadoDeEnvioDeConsultum>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TPersonalEncargadoDeEnvioDeConsultum Add(PersonalEncargadoDeEnvioDeConsultumDTO entidad1,string usuario)
        {
            try
            {
                var entidad = _mapper.Map<PersonalEncargadoDeEnvioDeConsultum>(entidad1);
                entidad.FechaCreacion=DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion= usuario;
                entidad.UsuarioModificacion = usuario;
                entidad.Estado = true;
                var modelo = _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TPersonalEncargadoDeEnvioDeConsultum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalEncargadoDeEnvioDeConsultum Update(PersonalEncargadoDeEnvioDeConsultumDTO entidad, string usuario)
        {
            try
            {
                var entidad1 = _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.GetBy(x => x.Id == entidad.Id).FirstOrDefault();
                PersonalEncargadoDeEnvioDeConsultum personalEncargado = new PersonalEncargadoDeEnvioDeConsultum
                {
                    Dia1 = entidad.Dia1,
                    Dia3 = entidad.Dia3,
                    Dia2 = entidad.Dia2,
                    Dia4 = entidad.Dia4,
                    Dia5 = entidad.Dia5,
                    FechaDia1 = entidad.FechaDia1,
                    FechaDia2 = entidad.FechaDia2,
                    FechaDia3 = entidad.FechaDia3,
                    FechaDia4 = entidad.FechaDia4,
                    FechaDia5 = entidad.FechaDia5,
                    IdPersonal = entidad.IdPersonal,
                    IdConfiguracionDeEnvioParaWhatsApp = entidad.IdConfiguracionDeEnvioParaWhatsApp,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    FechaCreacion = entidad1.FechaCreacion,
                    UsuarioModificacion = usuario,
                    Estado = true,
                    Id = entidad1.Id,
                };
                var modelo = _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.Update(personalEncargado);
                _unitOfWork.Commit();
                return _mapper.Map<TPersonalEncargadoDeEnvioDeConsultum>(entidad);
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
                _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TPersonalEncargadoDeEnvioDeConsultum> Add(List<PersonalEncargadoDeEnvioDeConsultumDTO> listadoEntidad1, string usuario)
        {
            try
            {
                var listadoEntidad = listadoEntidad1.Select(x => new PersonalEncargadoDeEnvioDeConsultum
                {
                    Dia1= x.Dia1,
                    Dia2= x.Dia2,
                    Dia3= x.Dia3,
                    Dia4= x.Dia4,
                    Dia5= x.Dia5,
                    FechaDia1= x.FechaDia1,
                    FechaDia2= x.FechaDia2,
                    FechaDia3= x.FechaDia3,
                    FechaDia4= x.FechaDia4,
                    FechaDia5= x.FechaDia5,
                    Id= x.Id,
                    IdConfiguracionDeEnvioParaWhatsApp = x.IdConfiguracionDeEnvioParaWhatsApp,
                    IdPersonal=x.IdPersonal,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    Estado = true,
                }).ToList();
                var modelo = _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TPersonalEncargadoDeEnvioDeConsultum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TPersonalEncargadoDeEnvioDeConsultum> AddDelete(List<PersonalEncargadoDeEnvioDeConsultumDTO> listadoEntidad1,int IdConfiguracionDeEnvioParaWhatsApp, string usuario)
        {
            try
            {
                var listas = _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository
                               .GetBy(x => x.IdConfiguracionDeEnvioParaWhatsApp == IdConfiguracionDeEnvioParaWhatsApp)
                               .Select(x => x.Id)
                               .ToList();
                _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.Delete(listas,usuario);

                var listadoEntidad = listadoEntidad1.Select(x => new PersonalEncargadoDeEnvioDeConsultum
                {
                    Dia1 = x.Dia1,
                    Dia2 = x.Dia2,
                    Dia3 = x.Dia3,
                    Dia4 = x.Dia4,
                    Dia5 = x.Dia5,
                    FechaDia1 = x.FechaDia1,
                    FechaDia2 = x.FechaDia2,
                    FechaDia3 = x.FechaDia3,
                    FechaDia4 = x.FechaDia4,
                    FechaDia5 = x.FechaDia5,
                    Id = x.Id,
                    IdConfiguracionDeEnvioParaWhatsApp = x.IdConfiguracionDeEnvioParaWhatsApp,
                    IdPersonal = x.IdPersonal,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    Estado = true,
                }).ToList();
                var modelo = _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TPersonalEncargadoDeEnvioDeConsultum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TPersonalEncargadoDeEnvioDeConsultum> Update(List<PersonalEncargadoDeEnvioDeConsultum> listadoEntidad, string usuario)
        {
            try
            {

                listadoEntidad = listadoEntidad.Select(x => new PersonalEncargadoDeEnvioDeConsultum
                {
                    Dia1 = x.Dia1,
                    Dia2 = x.Dia2,
                    Dia3 = x.Dia3,
                    Dia4 = x.Dia4,
                    Dia5 = x.Dia5,
                    FechaDia1 = x.FechaDia1,
                    FechaDia2 = x.FechaDia2,
                    FechaDia3 = x.FechaDia3,
                    FechaDia4 = x.FechaDia4,
                    FechaDia5 = x.FechaDia5,
                    Id = x.Id,
                    IdConfiguracionDeEnvioParaWhatsApp = x.IdConfiguracionDeEnvioParaWhatsApp,
                    IdPersonal = x.IdPersonal,
                    FechaCreacion = x.FechaCreacion,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = x.UsuarioCreacion,
                    UsuarioModificacion = usuario,
                    Estado = true,
                }).ToList();
                var modelo = _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TPersonalEncargadoDeEnvioDeConsultum>>(modelo);
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
                _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<PersonalEncargadoDeEnvioDeConsultumObtenerDTO> GetAllByConfiguracionEnvio(int IdConfiguracionDeEnvioParaWhatsApp)
        {
            try
            {
                var datos =  _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.ObtenerDias(IdConfiguracionDeEnvioParaWhatsApp);
                return datos;
                //return _unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.GetBy(x=>x.IdConfiguracionDeEnvioParaWhatsApp == IdConfiguracionDeEnvioParaWhatsApp).ToList();
            }  
            catch(Exception e)
            {
                throw e;
            }
        }
        public PersonalEncargadoDeEnvioDeConsultum GetbyId(int id)
        {
            try
            {
                return _mapper.Map<PersonalEncargadoDeEnvioDeConsultum>(_unitOfWork.PersonalEncargadoDeEnvioDeConsultumRepository.FirstById(id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
