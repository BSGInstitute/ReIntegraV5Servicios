using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
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
    public class FurTipoSolicitudService : IFurTipoSolicitudService
    {
        private readonly IUnitOfWork unitOfWork;
        private Mapper _mapper;
        public FurTipoSolicitudService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFurTipoSolicitudDTO, FurTipoSolicitudDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFurTipoSolicitud, TFurTipoSolicitudDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public TFurTipoSolicitud Add(FurTipoSolicitudDTO entidad,string usuario)
        {
            try
            {
                var dato = _mapper.Map<TFurTipoSolicitudDTO>(entidad);
                dato.FechaModificacion=DateTime.Now;
                dato.FechaCreacion=DateTime.Now;
                dato.UsuarioModificacion = usuario;
                dato.Estado = true;
                dato.UsuarioCreacion= usuario;
                var resp = unitOfWork.FurTipoSolicitudRepository.Add(dato);
                unitOfWork.Commit();
                return resp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurTipoSolicitud Update(TFurTipoSolicitudDTOV2 entidad,string usuario)
        {
            try
            {
                var entidadActual = unitOfWork.FurTipoSolicitudRepository.ObtenerPorId(entidad.Id);
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Descripcion = entidad.Descripcion;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.UsuarioModificacion = usuario;
                var entidadEnvio = _mapper.Map<TFurTipoSolicitudDTO>(entidadActual);
                var resp = unitOfWork.FurTipoSolicitudRepository.Update(entidadEnvio);
                unitOfWork.Commit();
                return resp;
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
                var resp= unitOfWork.FurTipoSolicitudRepository.Delete(id, usuario);
                unitOfWork.Commit();
                return resp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TFurTipoSolicitud> Add(IEnumerable<FurTipoSolicitudDTO> listadoEntidad,string usuario)
        {
            try
            {
                var dato = _mapper.Map<List<TFurTipoSolicitudDTO>>(listadoEntidad);
                dato = dato.Select(x => new TFurTipoSolicitudDTO
                {
                    Descripcion = x.Descripcion,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Nombre = x.Nombre,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion=usuario
                }).ToList();
                var resp = unitOfWork.FurTipoSolicitudRepository.Add(dato);
                unitOfWork.Commit();
                return resp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TFurTipoSolicitud> Update(IEnumerable<TFurTipoSolicitudDTO> listadoEntidad, string usuario)
        {
            try
            {
                var dato = listadoEntidad.Select(x => new TFurTipoSolicitudDTO
                {
                    FechaCreacion=x.FechaCreacion,
                    UsuarioCreacion=x.UsuarioCreacion,
                    Id= x.Id,
                    Descripcion = x.Descripcion,
                    Estado = true,
                    FechaModificacion = DateTime.Now,
                    Nombre = x.Nombre,
                    UsuarioModificacion = usuario
                }).ToList();
                var resp= unitOfWork.FurTipoSolicitudRepository.Update(dato);
                unitOfWork.Commit();
                return resp;
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
                var resp= unitOfWork.FurTipoSolicitudRepository.Delete(listadoIds,usuario);
                unitOfWork.Commit();
                return resp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurTipoSolicitud ObtenerPorId(int id)
        {
            try
            {
                var resp= unitOfWork.FurTipoSolicitudRepository.ObtenerPorId(id);
                unitOfWork.Commit();
                return resp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFurTipoSolicitud> ObtenerTodos()
        {
            try
            {
                return unitOfWork.FurTipoSolicitudRepository.ObtenerTodos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFurTipoSolicitud> ObtenerPorTexto(string texto)
        {
            try
            {
                return unitOfWork.FurTipoSolicitudRepository.ObtenerPorTexto(texto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
