using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp;
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
    public class CampaniaGeneralDetalleSubAreaService : ICampaniaGeneralDetalleSubAreaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public CampaniaGeneralDetalleSubAreaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCampaniaGeneralDetalleSubArea, CampaniaGeneralDetalleSubArea>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CampaniaGeneralDetalleSubArea Add(CampaniaGeneralDetalleSubArea entidad,string usuario)
        {
            try
            {
                entidad.UsuarioModificacion= usuario;
                entidad.UsuarioCreacion = usuario;
                var modelo = _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampaniaGeneralDetalleSubArea>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampaniaGeneralDetalleSubArea Update(CampaniaGeneralDetalleSubArea entidad,string usuario)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampaniaGeneralDetalleSubArea>(modelo);
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
                _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampaniaGeneralDetalleSubArea> Add(List<CampaniaGeneralDetalleSubArea> listadoEntidad, string usuario)
        {
            try
            {
                listadoEntidad=listadoEntidad.Select(x=> new CampaniaGeneralDetalleSubArea
                {
                    Estado=x.Estado,
                    UsuarioCreacion=usuario,
                    FechaCreacion=x.FechaCreacion,
                    FechaModificacion=x.FechaModificacion,
                    Id=x.Id,
                    IdCampaniaGeneralDetalle=x.IdCampaniaGeneralDetalle,
                    IdMigracion=x.IdMigracion,
                    IdSubAreaCapacitacion=x.IdSubAreaCapacitacion,
                    RowVersion=x.RowVersion,
                    UsuarioModificacion =usuario
                }).ToList();
                var modelo = _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampaniaGeneralDetalleSubArea>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampaniaGeneralDetalleSubArea> Update(List<CampaniaGeneralDetalleSubArea> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampaniaGeneralDetalleSubArea>>(modelo);
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
                _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.Delete(listadoIds, usuario);
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
