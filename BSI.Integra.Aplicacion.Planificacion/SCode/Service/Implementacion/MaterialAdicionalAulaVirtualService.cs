
using AutoMapper;

using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

using Microsoft.Win32;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: MaterialAdicionalAulaVirtualService
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 07/06/2023
    /// <summary>
    /// Gestión general de V_TFeedbackTipo_Filtro
    /// </summary>
    public class MaterialAdicionalAulaVirtualService : IMaterialAdicionalAulaVirtualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MaterialAdicionalAulaVirtualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<MaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TMaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        public MaterialAdicionalAulaVirtualDTO InsertarMaterialAdicional(MaterialAdicionalAulaVirtualEntidadDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    MaterialAdicionalAulaVirtual entidad = new MaterialAdicionalAulaVirtual()
                    {
                        NombreConfiguracion = dto.NombreConfiguracion,
                        IdPgeneral = dto.IdPGeneral,
                        EsOnline = dto.EsOnline,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    entidad.MaterialAdicionalAulaVirtualRegistros = new List<MaterialAdicionalAulaVirtualRegistro>();
                    entidad.MaterialAdicionalAulaVirtualPespecificos = new List<MaterialAdicionalAulaVirtualPespecifico>();
                    foreach (var material in dto.MaterialAdicional)
                    {
                        MaterialAdicionalAulaVirtualRegistro registro = new MaterialAdicionalAulaVirtualRegistro();
                        registro.IdMaterialAdicionalAulaVirtual = material.Id;
                        registro.NombreArchivo = material.NombreArchivo;
                        registro.RutaArchivo = material.RutaArchivo;
                        registro.EsEnlace = material.EsEnlace;
                        registro.SoloLectura = material.SoloLectura;
                        registro.Estado = true;
                        registro.UsuarioCreacion = usuario;
                        registro.UsuarioModificacion = usuario;
                        registro.FechaCreacion = DateTime.Now;
                        registro.FechaModificacion = DateTime.Now;
                        entidad.MaterialAdicionalAulaVirtualRegistros.Add(registro);
                    };
                    if (dto.IdsPespecifico != null && dto.IdsPespecifico.Count() != 0)
                    {
                        foreach (int idPespecifico in dto.IdsPespecifico)
                        {
                            MaterialAdicionalAulaVirtualPespecifico pespecifico = new MaterialAdicionalAulaVirtualPespecifico();
                            pespecifico.IdMaterialAdicionalAulaVirtual = entidad.Id;
                            pespecifico.IdPespecifico = idPespecifico;
                            pespecifico.Estado = true;
                            pespecifico.UsuarioCreacion = usuario;
                            pespecifico.UsuarioModificacion = usuario;
                            pespecifico.FechaCreacion = DateTime.Now;
                            pespecifico.FechaModificacion = DateTime.Now;
                            entidad.MaterialAdicionalAulaVirtualPespecificos.Add(pespecifico);
                        }
                    }
                    var respuesta = _unitOfWork.MaterialAdicionalAulaVirtualRepository.Add(entidad);
                    _unitOfWork.Commit();
                    MaterialAdicionalAulaVirtualDTO resultado = _mapper.Map<MaterialAdicionalAulaVirtualDTO>(respuesta);
                    if (dto.IdsPespecifico != null && dto.IdsPespecifico.Count() != 0)
                    {
                        foreach (int idPespecifico in dto.IdsPespecifico)
                        {
                            _unitOfWork.MaterialAdicionalAulaVirtualRepository.NotificacionMaterialAdicional(resultado.Id, idPespecifico, usuario);
                        }
                    }
                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MaterialAdicionalAulaVirtualDTO ActualizarMaterialAdicional(MaterialAdicionalAulaVirtualEntidadDTO dto, string usuario)
        {
            try
            {
                MaterialAdicionalAulaVirtual entidad = _unitOfWork.MaterialAdicionalAulaVirtualRepository.ObtenerPorId(dto.Id);
                if (entidad != null && entidad.Id != 0)
                {
                    var listaParaBorrarPespecificos = _unitOfWork.MaterialAdicionalAulaVirtualPespecificoRepository.ObtenerIdsPorIdMaterialAdicional(dto.Id).ToList();
                    listaParaBorrarPespecificos.RemoveAll(x => dto.IdsPespecifico.Any(y => y == x.Valor));
                    if(listaParaBorrarPespecificos.Count() > 0) _unitOfWork.MaterialAdicionalAulaVirtualPespecificoRepository.Delete(listaParaBorrarPespecificos.Select(x => x.Id), usuario);

                    var listaParaBorrarRegistros = _unitOfWork.MaterialAdicionalAulaVirtualRegistroRepository.ObtenerIdsPorIdMaterialAdicional(dto.Id).ToList();
                    listaParaBorrarRegistros.RemoveAll(x => dto.MaterialAdicional.Select(x => x.Id).Any(y => y == x.Id));
                    if (listaParaBorrarRegistros.Count() > 0) _unitOfWork.MaterialAdicionalAulaVirtualRegistroRepository.Delete(listaParaBorrarRegistros.Select(x => x.Id), usuario);
                    
                    if(listaParaBorrarPespecificos.Count() > 0 || listaParaBorrarRegistros.Count() > 0) _unitOfWork.Commit();

                    entidad.NombreConfiguracion = dto.NombreConfiguracion;
                    entidad.IdPgeneral = dto.IdPGeneral;
                    entidad.EsOnline = dto.EsOnline;
                    entidad.UsuarioModificacion = usuario;
                    entidad.FechaModificacion = DateTime.Now;

                    entidad.MaterialAdicionalAulaVirtualRegistros = new List<MaterialAdicionalAulaVirtualRegistro>();
                    entidad.MaterialAdicionalAulaVirtualPespecificos = new();

                    foreach (var reg in dto.MaterialAdicional)
                    {
                        MaterialAdicionalAulaVirtualRegistro registro;
                        registro = _unitOfWork.MaterialAdicionalAulaVirtualRegistroRepository.ObtenerPorIdYIdMaterialAdicionalAulaVirtual(reg.Id, entidad.Id);
                        if (registro != null && registro.Id != 0)
                        {
                            registro.NombreArchivo = reg.NombreArchivo;
                            registro.RutaArchivo = reg.RutaArchivo;
                            registro.EsEnlace = reg.EsEnlace;
                            registro.SoloLectura = reg.SoloLectura;
                            registro.UsuarioModificacion = usuario;
                            registro.FechaModificacion = DateTime.Now;
                            entidad.MaterialAdicionalAulaVirtualRegistros.Add(registro);
                        }
                        else
                        {
                            registro = new MaterialAdicionalAulaVirtualRegistro();
                            registro.NombreArchivo = reg.NombreArchivo;
                            registro.RutaArchivo = reg.RutaArchivo;
                            registro.EsEnlace = reg.EsEnlace;
                            registro.Estado = true;
                            registro.SoloLectura = reg.SoloLectura;
                            registro.UsuarioCreacion = usuario;
                            registro.UsuarioModificacion = usuario;
                            registro.FechaCreacion = DateTime.Now;
                            registro.FechaModificacion = DateTime.Now;
                            entidad.MaterialAdicionalAulaVirtualRegistros.Add(registro);
                        }
                    }
                    if (dto.IdsPespecifico != null && dto.IdsPespecifico.Count() != 0)
                    {
                        foreach (var pe in dto.IdsPespecifico)
                        {
                            MaterialAdicionalAulaVirtualPespecifico pespecifico;
                            pespecifico = _unitOfWork.MaterialAdicionalAulaVirtualPespecificoRepository.ObtenerPorIdPespecificoIdMaterialAdicional(entidad.Id, pe);
                            if (pespecifico != null && pespecifico.Id != 0)
                            {
                                pespecifico.IdPespecifico = pe;
                                pespecifico.UsuarioModificacion = usuario;
                                pespecifico.FechaModificacion = DateTime.Now;
                                entidad.MaterialAdicionalAulaVirtualPespecificos.Add(pespecifico);
                            }
                            else
                            {
                                pespecifico = new MaterialAdicionalAulaVirtualPespecifico();
                                pespecifico.IdPespecifico = pe;
                                pespecifico.Estado = true;
                                pespecifico.UsuarioCreacion = usuario;
                                pespecifico.UsuarioModificacion = usuario;
                                pespecifico.FechaCreacion = DateTime.Now;
                                pespecifico.FechaModificacion = DateTime.Now;
                                entidad.MaterialAdicionalAulaVirtualPespecificos.Add(pespecifico);
                            }
                        }
                    }
                    _unitOfWork.MaterialAdicionalAulaVirtualRepository.Update(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<MaterialAdicionalAulaVirtualDTO>(entidad);
                }
                else
                    throw new BadRequestException("Entidad no encontrada");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var materialAdicional = _unitOfWork.MaterialAdicionalAulaVirtualRepository.ObtenerPorId(id);
                if (materialAdicional != null && materialAdicional.Id != 0)
                {
                    var respuesta = _unitOfWork.MaterialAdicionalAulaVirtualRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<MaterialAdicionalAulaVirtualDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.MaterialAdicionalAulaVirtualRepository.ObtenerMaterialAdicional();
                return _mapper.Map<List<MaterialAdicionalAulaVirtualDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MaterialAdicionalAulaVirtualDetalleDTO ObtenerDetalleMaterialAdicional(int idMaterialAdicional)
        {
            try
            {
                MaterialAdicionalAulaVirtualDetalleDTO detalleMaterialAdicional = new MaterialAdicionalAulaVirtualDetalleDTO();
                var detalle = _unitOfWork.MaterialAdicionalAulaVirtualRepository.ObtenerMaterialAdicionalDetalle(idMaterialAdicional);
                var detalleRegistro = _unitOfWork.MaterialAdicionalAulaVirtualRegistroRepository.ObtenerMaterialAdicionalDetalleRegistro(idMaterialAdicional);
                var detallePespecifico = _unitOfWork.MaterialAdicionalAulaVirtualPespecificoRepository.ObtenerIdsMaterialAdicionalDetallePespecifico(idMaterialAdicional);
                if (detalle != null)
                {
                    detalleMaterialAdicional.MaterialAdicional = detalle;
                }
                if (detalleRegistro != null && detalleRegistro.Count() > 0)
                {
                    detalleMaterialAdicional.MaterialAdicionalRegistro = detalleRegistro;
                }
                if (detallePespecifico != null && detallePespecifico.Count() > 0)
                {
                    detalleMaterialAdicional.ProgramaEspecifico = detallePespecifico;
                }
                return detalleMaterialAdicional;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
