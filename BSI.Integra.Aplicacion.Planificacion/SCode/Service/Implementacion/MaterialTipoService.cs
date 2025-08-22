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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Twilio.TwiML.Voice;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: FeedbackTipoService
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 12/05/2023
    /// <summary>
    /// Gestión general de MaterialTipoService
    /// </summary>
    public class MaterialTipoService: IMaterialTipoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MaterialTipoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<IEnumerable<MaterialTipoDetalleDTO>, MaterialTipoAgrupadoDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los MaterialTipo
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public ListaCombosDTO ObtenerCombosModulo()
        {
            ListaCombosDTO listadoCombos = new ListaCombosDTO()
            {
                MaterialAccion = _unitOfWork.MaterialAccionRepository.ObtenerCombo(),
                MaterialVersion = _unitOfWork.MaterialVersionRepository.ObtenerCombo(),
                MaterialCriterio = _unitOfWork.MaterialCriterioVerificacionRepository.ObtenerCombo()
            };
            return listadoCombos;
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los MaterialTipo
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public IEnumerable<MaterialTipoAgrupadoDTO> Obtener()
        {
            try
            {
                IEnumerable<MaterialTipoDetalleDTO>? agrupado = _unitOfWork.MaterialTipoRepository.Obtener();
                IEnumerable<MaterialTipoAgrupadoDTO> resultado = agrupado.GroupBy(x => new { x.Id, x.Nombre, x.Descripcion })
                    .Select(y => new MaterialTipoAgrupadoDTO
                    {
                        Id = y.Key.Id,
                        Nombre = y.Key.Nombre,
                        Descripcion = y.Key.Descripcion,
                        ListaMaterialAccion = y.GroupBy(z => new { z.IdAccion, z.IdMaterialAccion, z.NombreMaterialAccion })
                            .Select(z => new MaterialTipoAccionDTO

                            {
                                IdAccion = z.Key.IdAccion,
                                IdMaterialAccion = z.Key.IdMaterialAccion,
                                NombreMaterialAccion= z.Key.NombreMaterialAccion
                            }).ToList(),
                        ListaMaterialVersion = y.GroupBy(z => new { z.IdVersion, z.IdMaterialVersion, z.NombreMaterialVersion })
                            .Select(z => new MaterialTipoVersionDTO
                            {
                                IdVersion = z.Key.IdVersion,
                                IdMaterialVersion = z.Key.IdMaterialVersion,
                                NombreMaterialVersion = z.Key.NombreMaterialVersion
                            }).ToList(),
                        ListaMaterialCriterioVerificacion = y.GroupBy(z => new { z.IdCriterio, z.IdMaterialCriterioVerificacion, z.NombreMaterialCriterioVerificacion })
                            .Select(z => new MaterialTipoCriterioVerificacionDTO
                            {
                                IdCriterio = z.Key.IdCriterio,
                                IdMaterialCriterioVerificacion = z.Key.IdMaterialCriterioVerificacion,
                                NombreMaterialCriterioVerificacion = z.Key.NombreMaterialCriterioVerificacion
                            }).ToList()
                    }).ToList();
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los MaterialTipo
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public MaterialTipoAgrupadoDTO InsertarMaterialTipo(MaterialTipoAsociacionEntidadDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    List<MaterialAsociacionVersion> listaMaterialAsociacionVersion = new List<MaterialAsociacionVersion>();
                    List<MaterialAsociacionAccion> listaMaterialAsociacionAccion = new List<MaterialAsociacionAccion>();
                    List<MaterialAsociacionCriterioVerificacion> listaMaterialAsociacionCriterio = new List<MaterialAsociacionCriterioVerificacion>();
                    MaterialTipo entidad = new MaterialTipo()
                    {
                        Nombre = dto.Nombre,
                        Descripcion = dto.Descripcion,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    foreach (int idAccion in dto.IdsMaterialAsociacionAccion)
                    {
                        MaterialAsociacionAccion asociacionAccion = new MaterialAsociacionAccion();
                        asociacionAccion.IdMaterialTipo = entidad.Id;
                        asociacionAccion.IdMaterialAccion = idAccion;
                        asociacionAccion.Estado = true;
                        asociacionAccion.UsuarioCreacion = usuario;
                        asociacionAccion.UsuarioModificacion = usuario;
                        asociacionAccion.FechaCreacion = DateTime.Now;
                        asociacionAccion.FechaModificacion = DateTime.Now;
                        listaMaterialAsociacionAccion.Add(asociacionAccion);
                    }

                    foreach (int idVersion in dto.IdsMaterialAsociacionVersion)
                    {
                        MaterialAsociacionVersion asociacionVersion = new MaterialAsociacionVersion();
                        asociacionVersion.IdMaterialTipo = entidad.Id;
                        asociacionVersion.IdMaterialVersion = idVersion;
                        asociacionVersion.Estado = true;
                        asociacionVersion.UsuarioCreacion = usuario;
                        asociacionVersion.UsuarioModificacion = usuario;
                        asociacionVersion.FechaCreacion = DateTime.Now;
                        asociacionVersion.FechaModificacion = DateTime.Now;
                        listaMaterialAsociacionVersion.Add(asociacionVersion);
                    }

                    foreach (int idCriterio in dto.IdsMaterialAsociacionCriterioVerificacion)
                    {
                        MaterialAsociacionCriterioVerificacion asociacionCriterio = new MaterialAsociacionCriterioVerificacion();
                        asociacionCriterio.IdMaterialTipo = entidad.Id;
                        asociacionCriterio.IdMaterialCriterioVerificacion = idCriterio;
                        asociacionCriterio.Estado = true;
                        asociacionCriterio.UsuarioCreacion = usuario;
                        asociacionCriterio.UsuarioModificacion = usuario;
                        asociacionCriterio.FechaCreacion = DateTime.Now;
                        asociacionCriterio.FechaModificacion = DateTime.Now;
                        listaMaterialAsociacionCriterio.Add(asociacionCriterio);
                    }
                    entidad.MaterialAsociacionAccions = listaMaterialAsociacionAccion;
                    entidad.MaterialAsociacionVersions = listaMaterialAsociacionVersion;
                    entidad.MaterialAsociacionCriterioVerificacions = listaMaterialAsociacionCriterio;
                    var resultado = _unitOfWork.MaterialTipoRepository.Add(entidad);
                    _unitOfWork.Commit();

                    var agrupado = AgruparContenidoPorIdMaterialTipo(resultado.Id);
                    return agrupado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los MaterialTipo
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public MaterialTipoAgrupadoDTO ActualizarMaterialTipo(MaterialTipoAsociacionEntidadDTO dto, string usuario)
        {
            try
            {
                if(dto != null)
                {
                    List<MaterialAsociacionAccion> listaMaterialAsociacionAccion = new List<MaterialAsociacionAccion>();
                    List<MaterialAsociacionVersion> listaMaterialAsociacionVersion = new List<MaterialAsociacionVersion>();
                    List<MaterialAsociacionCriterioVerificacion> listaMaterialAsociacionCriterio = new List<MaterialAsociacionCriterioVerificacion>();
                    MaterialTipo entidad = _unitOfWork.MaterialTipoRepository.ObtenerPorId(dto.Id);
                    if (entidad != null && entidad.Id != 0)
                    {
                        entidad.Nombre = dto.Nombre;
                        entidad.Descripcion = dto.Descripcion;
                        entidad.Estado = true;
                        entidad.UsuarioModificacion = usuario;
                        entidad.FechaModificacion = DateTime.Now;
                    }

                   var listaNuevosAsociacionAccion = EliminarSepararNuevaAsociacionAccion(dto, usuario);
                    var listaNuevosAsociacionVersion = EliminarSepararNuevaAsociacionVersion(dto, usuario);
                    var listaNuevosAsociacionCriterioVerificacion = EliminarSepararNuevaAsociacionCriterioVerificacion(dto, usuario);

                    if (listaNuevosAsociacionAccion.Count() > 0)
                    {
                        foreach (int idAccion in listaNuevosAsociacionAccion)
                        {
                            MaterialAsociacionAccion? asociacionAccion = new MaterialAsociacionAccion();
                            asociacionAccion.IdMaterialTipo = entidad.Id;
                            asociacionAccion.IdMaterialAccion = idAccion;
                            asociacionAccion.UsuarioCreacion = usuario;
                            asociacionAccion.UsuarioModificacion = usuario;
                            asociacionAccion.FechaCreacion = DateTime.Now;
                            asociacionAccion.FechaModificacion = DateTime.Now;
                            asociacionAccion.Estado = true;
                            listaMaterialAsociacionAccion.Add(asociacionAccion);
                        }
                    }
                    if (listaNuevosAsociacionVersion.Count() > 0)
                    {
                        foreach (int idVersion in listaNuevosAsociacionVersion)
                        {
                            MaterialAsociacionVersion? asociacionVersion = new MaterialAsociacionVersion();
                            asociacionVersion.IdMaterialTipo = entidad.Id;
                            asociacionVersion.IdMaterialVersion = idVersion;
                            asociacionVersion.UsuarioCreacion = usuario;
                            asociacionVersion.UsuarioModificacion = usuario;
                            asociacionVersion.FechaCreacion = DateTime.Now;
                            asociacionVersion.FechaModificacion = DateTime.Now;
                            asociacionVersion.Estado = true;
                            listaMaterialAsociacionVersion.Add(asociacionVersion);
                        }
                    }
                    if (listaNuevosAsociacionCriterioVerificacion.Count() > 0)
                    {
                        foreach (int idCriterio in listaNuevosAsociacionCriterioVerificacion)
                        {
                            MaterialAsociacionCriterioVerificacion? asociacionCriterio = new MaterialAsociacionCriterioVerificacion();
                            asociacionCriterio.IdMaterialTipo = entidad.Id;
                            asociacionCriterio.IdMaterialCriterioVerificacion = idCriterio;
                            asociacionCriterio.UsuarioCreacion = usuario;
                            asociacionCriterio.UsuarioModificacion = usuario;
                            asociacionCriterio.FechaCreacion = DateTime.Now;
                            asociacionCriterio.FechaModificacion = DateTime.Now;
                            asociacionCriterio.Estado = true;
                            listaMaterialAsociacionCriterio.Add(asociacionCriterio);
                        }
                    }
                    if (listaMaterialAsociacionAccion.Count > 0)
                    {
                        entidad.MaterialAsociacionAccions = listaMaterialAsociacionAccion;
                    }
                    if (listaMaterialAsociacionVersion.Count > 0)
                    {
                        entidad.MaterialAsociacionVersions = listaMaterialAsociacionVersion;
                    }
                    if (listaMaterialAsociacionCriterio.Count > 0)
                    {
                        entidad.MaterialAsociacionCriterioVerificacions = listaMaterialAsociacionCriterio;
                    }
                    if(listaMaterialAsociacionCriterio.Count > 0 || listaMaterialAsociacionVersion.Count > 0 || listaMaterialAsociacionAccion.Count > 0 || !string.IsNullOrEmpty(entidad.Nombre))
                    {
                        _unitOfWork.MaterialTipoRepository.Update(entidad);
                        _unitOfWork.Commit();
                    };

                    var agrupado = AgruparContenidoPorIdMaterialTipo(dto.Id);
                    return agrupado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los MaterialTipo
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public bool EliminarMaterialTipo(int id, string usuario)
        {
            try
            {
                var materialTipo = _unitOfWork.MaterialTipoRepository.ObtenerPorId(id);
                if (materialTipo != null && materialTipo.Id != 0)
                {
                    var listaIdsMaterialAccion = _unitOfWork.MaterialAsociacionAccionRepository.ObtenerPorIdMaterialTipo(id);
                    var listaIdsMaterialVersion = _unitOfWork.MaterialAsociacionVersionRepository.ObtenerPorIdMaterialTipo(id);
                    var listaIdsMaterialCriterio = _unitOfWork.MaterialAsociacionCriterioVerificacionRepository.ObtenerPorIdMaterialTipo(id);
                    var rptaMaterialTipo = _unitOfWork.MaterialTipoRepository.Delete(id, usuario);
                    var rptaMaterialAccion = _unitOfWork.MaterialAsociacionAccionRepository.Delete(listaIdsMaterialAccion.Select(x => x.Id).ToList(), usuario);
                    var rptaMaterialVersion = _unitOfWork.MaterialAsociacionVersionRepository.Delete(listaIdsMaterialVersion.Select(x => x.Id).ToList(), usuario);
                    var rptaMaterialCriterio = _unitOfWork.MaterialAsociacionCriterioVerificacionRepository.Delete(listaIdsMaterialCriterio.Select(x => x.Id).ToList(), usuario);
                    _unitOfWork.Commit();
                    return (rptaMaterialTipo && rptaMaterialAccion && rptaMaterialVersion && rptaMaterialCriterio) ? true : false;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Identifica y separa si se va a eliminar o crear T_TipoDocumentoAlumnoModalidadCurso
        /// </summary>
        private List<int> EliminarSepararNuevaAsociacionAccion(MaterialTipoAsociacionEntidadDTO detalleAsociacion, string usuario)
        {
            var listaBorrarAsociacionAccion =_unitOfWork.MaterialAsociacionAccionRepository.ObtenerPorIdMaterialTipo(detalleAsociacion.Id).ToList();
            var listaCrearAsociacionAccion = listaBorrarAsociacionAccion.ToList();

            listaBorrarAsociacionAccion.RemoveAll(x => detalleAsociacion.IdsMaterialAsociacionAccion.Any(y => y.Equals(x.IdMaterialAccion)));
            detalleAsociacion.IdsMaterialAsociacionAccion.RemoveAll(x => listaCrearAsociacionAccion.Any(y => y.IdMaterialAccion.Equals(x)));

            if (listaBorrarAsociacionAccion.Count() > 0)
            {
                _unitOfWork.MaterialAsociacionAccionRepository.Delete(listaBorrarAsociacionAccion.Select(x => x.Id).ToList(), usuario);
                _unitOfWork.Commit();
            }
            return detalleAsociacion.IdsMaterialAsociacionAccion;
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Identifica y separa si se va a eliminar o crear T_TipoDocumentoAlumnoModalidadCurso
        /// </summary>
        private List<int> EliminarSepararNuevaAsociacionVersion(MaterialTipoAsociacionEntidadDTO detalleAsociacion, string usuario)
        {
            var listaBorrarAsociacionVersion = _unitOfWork.MaterialAsociacionVersionRepository.ObtenerPorIdMaterialTipo(detalleAsociacion.Id).ToList();
            var listaCrearAsociacionVersion = listaBorrarAsociacionVersion.ToList();

            listaBorrarAsociacionVersion.RemoveAll(x => detalleAsociacion.IdsMaterialAsociacionVersion.Any(y => y.Equals(x.IdMaterialVersion)));
            detalleAsociacion.IdsMaterialAsociacionVersion.RemoveAll(x => listaCrearAsociacionVersion.Any(y => y.IdMaterialVersion.Equals(x)));

            if (listaBorrarAsociacionVersion.Count() > 0)
            {
                _unitOfWork.MaterialAsociacionVersionRepository.Delete(listaBorrarAsociacionVersion.Select(x => x.Id).ToList(), usuario);
                _unitOfWork.Commit();
            }
            return detalleAsociacion.IdsMaterialAsociacionVersion;
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Identifica y separa si se va a eliminar o crear T_TipoDocumentoAlumnoModalidadCurso
        /// </summary>
        private List<int> EliminarSepararNuevaAsociacionCriterioVerificacion(MaterialTipoAsociacionEntidadDTO detalleAsociacion, string usuario)
        {
            var listaBorrarAsociacionCriterioVerificacion = _unitOfWork.MaterialAsociacionCriterioVerificacionRepository.ObtenerPorIdMaterialTipo(detalleAsociacion.Id).ToList();
            var listaCrearAsociacionCriterioVerificacion = listaBorrarAsociacionCriterioVerificacion.ToList();

            listaBorrarAsociacionCriterioVerificacion.RemoveAll(x => detalleAsociacion.IdsMaterialAsociacionCriterioVerificacion.Any(y => y.Equals(x.IdMaterialCriterioVerificacion)));
            detalleAsociacion.IdsMaterialAsociacionCriterioVerificacion.RemoveAll(x => listaCrearAsociacionCriterioVerificacion.Any(y => y.IdMaterialCriterioVerificacion.Equals(x)));

            if (listaBorrarAsociacionCriterioVerificacion.Count() > 0)
            {
                _unitOfWork.MaterialAsociacionCriterioVerificacionRepository.Delete(listaBorrarAsociacionCriterioVerificacion.Select(x => x.Id).ToList(), usuario);
                _unitOfWork.Commit();
            }
            return detalleAsociacion.IdsMaterialAsociacionCriterioVerificacion;
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Genera el agrupado para cargar la nueva grilla
        /// </summary>
        private MaterialTipoAgrupadoDTO AgruparContenidoPorIdMaterialTipo(int id)
        {
            var armado = _unitOfWork.MaterialTipoRepository.ObtenerRelacionesPorId(id);
            MaterialTipoAgrupadoDTO? agrupado = armado.GroupBy(x => new { x.Id, x.Nombre, x.Descripcion })
                    .Select(y => new MaterialTipoAgrupadoDTO
                    {
                        Id = y.Key.Id,
                        Nombre = y.Key.Nombre,
                        Descripcion = y.Key.Descripcion,
                        ListaMaterialAccion = y.GroupBy(z => new { z.IdAccion, z.IdMaterialAccion, z.NombreMaterialAccion })
                            .Select(z => new MaterialTipoAccionDTO

                            {
                                IdAccion = z.Key.IdAccion,
                                IdMaterialAccion = z.Key.IdMaterialAccion,
                                NombreMaterialAccion = z.Key.NombreMaterialAccion
                            }).ToList(),
                        ListaMaterialVersion = y.GroupBy(z => new { z.IdVersion, z.IdMaterialVersion, z.NombreMaterialVersion })
                            .Select(z => new MaterialTipoVersionDTO
                            {
                                IdVersion = z.Key.IdVersion,
                                IdMaterialVersion = z.Key.IdMaterialVersion,
                                NombreMaterialVersion = z.Key.NombreMaterialVersion
                            }).ToList(),
                        ListaMaterialCriterioVerificacion = y.GroupBy(z => new { z.IdCriterio, z.IdMaterialCriterioVerificacion, z.NombreMaterialCriterioVerificacion })
                            .Select(z => new MaterialTipoCriterioVerificacionDTO
                            {
                                IdCriterio = z.Key.IdCriterio,
                                IdMaterialCriterioVerificacion = z.Key.IdMaterialCriterioVerificacion,
                                NombreMaterialCriterioVerificacion = z.Key.NombreMaterialCriterioVerificacion
                            }).ToList()
                    }).FirstOrDefault();
            return agrupado;
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-04
        /// <summary>
        /// Genera combo de T_MaterialTipo
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            var combo = _unitOfWork.MaterialTipoRepository.ObtenerCombo();
            return combo;
        }
    }
}
