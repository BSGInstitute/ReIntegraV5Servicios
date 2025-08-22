using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp.CampaniaMailingWhatsAppFiltradoDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp
{
    public class CampaniaGeneralDetalleProgramaService : ICampaniaGeneralDetalleProgramaService
    {
 
            private IUnitOfWork _unitOfWork;
            private Mapper _mapper;


            public CampaniaGeneralDetalleProgramaService(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

                var config = new MapperConfiguration(cfg => cfg.CreateMap<TCampaniaGeneralDetallePrograma, CampaniaGeneralDetallePrograma>(MemberList.None).ReverseMap());
                _mapper = new Mapper(config);
            }

            #region Metodos Base
            public CampaniaGeneralDetallePrograma Add(CampaniaGeneralDetallePrograma entidad,string usuario)
            {
                try
                {
                    entidad.UsuarioCreacion= usuario;
                    entidad.UsuarioModificacion = usuario;
                    var modelo = _unitOfWork.CampaniaGeneralDetalleProgramaRepositorio.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<CampaniaGeneralDetallePrograma>(modelo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public CampaniaGeneralDetallePrograma Update(CampaniaGeneralDetallePrograma entidad)
            {
                try
                {
                    var modelo = _unitOfWork.CampaniaGeneralDetalleProgramaRepositorio.Update(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<CampaniaGeneralDetallePrograma>(modelo);
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
                    _unitOfWork.CampaniaGeneralDetalleProgramaRepositorio.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public List<CampaniaGeneralDetallePrograma> Add(List<CampaniaGeneralDetallePrograma> listadoEntidad, string usuario)
            {
                try
                {
                listadoEntidad = listadoEntidad.Select(x => new CampaniaGeneralDetallePrograma
                {
                    Estado= x.Estado,
                    FechaCreacion= x.FechaCreacion,
                    FechaModificacion= x.FechaModificacion,
                    Id= x.Id,
                    IdCampaniaGeneralDetalle = x.IdCampaniaGeneralDetalle,
                    IdMigracion= x.IdMigracion,
                    IdPgeneral= x.IdPgeneral,
                    NombreProgramaGeneral= x.NombreProgramaGeneral,
                    UsuarioCreacion=usuario,
                    UsuarioModificacion= usuario,
                }).ToList();
                    var modelo = _unitOfWork.CampaniaGeneralDetalleProgramaRepositorio.Add(listadoEntidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<CampaniaGeneralDetallePrograma>>(modelo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public List<CampaniaGeneralDetallePrograma> Update(List<CampaniaGeneralDetallePrograma> listadoEntidad)
            {
                try
                {
                    var modelo = _unitOfWork.CampaniaGeneralDetalleProgramaRepositorio.Update(listadoEntidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<CampaniaGeneralDetallePrograma>>(modelo);
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
                    _unitOfWork.CampaniaGeneralDetalleProgramaRepositorio.Delete(listadoIds, usuario);
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