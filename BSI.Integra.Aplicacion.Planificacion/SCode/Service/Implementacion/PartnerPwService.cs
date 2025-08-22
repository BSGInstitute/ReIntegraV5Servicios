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

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PartnerPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_PartnerPw
    /// </summary>
    public class PartnerPwService : IPartnerPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PartnerPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPartnerPw, PartnerPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPartnerPw, PartnerPwDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerPw, PartnerPwDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPartnerBeneficioPw, PartnerBeneficioPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerBeneficioPwDTO, TPartnerBeneficioPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerBeneficioPwDTO, PartnerBeneficioPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPartnerContactoPw, PartnerContactoPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerContactoPwDTO, TPartnerContactoPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerContactoPwDTO, PartnerContactoPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro Partner PW
        /// </summary>
        /// <returns> Lista PartnerPwDTO </returns>
        public IEnumerable<PartnerPwDTO> Obtener()
        {
            return _unitOfWork.PartnerPwRepository.Obtener();
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id Partner
        /// </summary>
        /// <param name="idPartner">Id Partner</param>
        /// <returns> Lista PartnerBeneficioPwDTO, Lista Contactos </returns>
        public (IEnumerable<PartnerBeneficioPwDTO> Beneficios, IEnumerable<PartnerContactoPwDTO> Contactos) ObtenerBeneficioContactoPorId(int idPartner)
        {
            try
            {
                if (idPartner == 0)
                {
                    throw new BadRequestException("Id 0 no valido");
                }
                var beneficios = _unitOfWork.PartnerBeneficioPwRepository.ObtenerPorIdPartner(idPartner);
                var contactos = _unitOfWork.PartnerContactoPwRepository.ObtenerPorIdPartner(idPartner);
                return (_mapper.Map<IEnumerable<PartnerBeneficioPwDTO>>(beneficios), _mapper.Map<IEnumerable<PartnerContactoPwDTO>>(contactos));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo PartnerPw
        /// </summary>
        /// <param name="dto">PartnerPw</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PartnerPwDTO</returns>
        public PartnerPwDTO Insertar(PartnerPwDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PartnerPw entidad = new()
                    {
                        Nombre = dto.Nombre,
                        ImgPrincipal = dto.ImgPrincipal,
                        ImgPrincipalAlf = dto.ImgPrincipalAlf,
                        ImgSecundaria = dto.ImgSecundaria,
                        ImgSecundariaAlf = dto.ImgSecundariaAlf,
                        Descripcion = dto.Descripcion,
                        DescripcionCorta = dto.DescripcionCorta,
                        Preguntas = dto.Preguntas,
                        Posicion = dto.Posicion,
                        IdPartner = dto.IdPartner,
                        EncabezadoCorreoPartner = dto.EncabezadoCorreoPartner,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.PartnerPwRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<PartnerPwDTO>(respuesta);

                    if (dto.Beneficios != null && dto.Beneficios.Count() > 0)
                    {
                        var partnerBeneficios = dto.Beneficios.Select(x => new PartnerBeneficioPw
                        {
                            IdPartner = entidad.Id,
                            Descripcion = x.Descripcion,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        });
                        var res = _unitOfWork.PartnerBeneficioPwRepository.Add(partnerBeneficios);
                        _unitOfWork.Commit();
                        resultado.Beneficios = _mapper.Map<List<PartnerBeneficioPwDTO>>(res);
                    }
                    if (dto.Contactos != null && dto.Contactos.Count() > 0)
                    {
                        var partnerContactos = dto.Contactos.Select(x => new PartnerContactoPw
                        {
                            IdPartner = entidad.Id,
                            Nombres = x.Nombres,
                            Apellidos = x.Apellidos,
                            Email1 = x.Email1,
                            Email2 = x.Email2,
                            Telefono1 = x.Telefono1,
                            Telefono2 = x.Telefono2,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        });
                        var res = _unitOfWork.PartnerContactoPwRepository.Add(partnerContactos);
                        _unitOfWork.Commit();
                        resultado.Contactos = _mapper.Map<List<PartnerContactoPwDTO>>(res);
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un PartnerPw
        /// </summary>
        /// <param name="dto">PartnerPw</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>PartnerPwDTO</returns>
        public PartnerPwDTO Actualizar(PartnerPwDTO dto, string usuario)
        {
            try
            {
                PartnerPw? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PartnerPwRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.ImgPrincipal = dto.ImgPrincipal;
                            entidad.ImgPrincipalAlf = dto.ImgPrincipalAlf;
                            entidad.ImgSecundaria = dto.ImgSecundaria;
                            entidad.ImgSecundariaAlf = dto.ImgSecundariaAlf;
                            entidad.Descripcion = dto.Descripcion;
                            entidad.DescripcionCorta = dto.DescripcionCorta;
                            entidad.Preguntas = dto.Preguntas;
                            entidad.Posicion = dto.Posicion;
                            entidad.IdPartner = dto.IdPartner;
                            entidad.EncabezadoCorreoPartner = dto.EncabezadoCorreoPartner;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PartnerPwRepository.Update(entidad);
                            _unitOfWork.Commit();

                            var listaBeneficios = _unitOfWork.PartnerBeneficioPwRepository.ObtenerPorIdPartner(entidad.Id).ToList();
                            if (listaBeneficios != null && listaBeneficios.Count() > 0)
                            {
                                if (dto.Beneficios != null && dto.Beneficios.Count() > 0)
                                {
                                    listaBeneficios.RemoveAll(s => dto.Beneficios.Any(x => x.Id == s.Id));
                                }
                                if (listaBeneficios.Count() > 0)
                                {
                                    _unitOfWork.PartnerBeneficioPwRepository.Delete(listaBeneficios.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }
                            }
                            var listaContactos = _unitOfWork.PartnerContactoPwRepository.ObtenerPorIdPartner(entidad.Id).ToList();
                            if (listaContactos != null && listaContactos.Count() > 0)
                            {
                                if (dto.Contactos != null && dto.Contactos.Count() > 0)
                                {
                                    listaContactos.RemoveAll(s => dto.Contactos.Any(x => x.Id == s.Id));
                                }
                                if (listaContactos.Count() > 0)
                                {
                                    _unitOfWork.PartnerContactoPwRepository.Delete(listaContactos.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }
                            }
                            if (dto.Beneficios != null && dto.Beneficios.Count() > 0)
                            {
                                dto.Beneficios.ForEach(beneficio =>
                                {
                                    PartnerBeneficioPw partnerBeneficioPw;
                                    if (beneficio.Id != 0 && _unitOfWork.PartnerBeneficioPwRepository.Exist(beneficio.Id))
                                    {
                                        partnerBeneficioPw = _unitOfWork.PartnerBeneficioPwRepository.ObtenerPorId(beneficio.Id)!;
                                        partnerBeneficioPw.Descripcion = beneficio.Descripcion;
                                        partnerBeneficioPw.UsuarioModificacion = usuario;
                                        partnerBeneficioPw.FechaModificacion = DateTime.Now;
                                        _unitOfWork.PartnerBeneficioPwRepository.Update(partnerBeneficioPw);
                                        _unitOfWork.Commit();
                                    }
                                    else
                                    {
                                        partnerBeneficioPw = new PartnerBeneficioPw()
                                        {
                                            IdPartner = entidad.Id,
                                            Descripcion = beneficio.Descripcion,
                                            Estado = true,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                        };
                                        var resultado = _unitOfWork.PartnerBeneficioPwRepository.Add(partnerBeneficioPw);
                                        _unitOfWork.Commit();
                                        beneficio.Id = resultado.Id;
                                    }
                                });
                            }
                            if (dto.Contactos != null && dto.Contactos.Count() > 0)
                            {
                                dto.Contactos.ForEach(contacto =>
                                {
                                    PartnerContactoPw partnerContactoPw;
                                    if (contacto.Id != 0 && _unitOfWork.PartnerContactoPwRepository.Exist(contacto.Id))
                                    {
                                        partnerContactoPw = _unitOfWork.PartnerContactoPwRepository.ObtenerPorId(contacto.Id)!;
                                        partnerContactoPw.Nombres = contacto.Nombres;
                                        partnerContactoPw.Apellidos = contacto.Apellidos;
                                        partnerContactoPw.Email1 = contacto.Email1;
                                        partnerContactoPw.Email2 = contacto.Email2;
                                        partnerContactoPw.Telefono1 = contacto.Telefono1;
                                        partnerContactoPw.Telefono2 = contacto.Telefono2;
                                        partnerContactoPw.UsuarioModificacion = usuario;
                                        partnerContactoPw.FechaModificacion = DateTime.Now;
                                        _unitOfWork.PartnerContactoPwRepository.Update(partnerContactoPw);
                                        _unitOfWork.Commit();
                                    }
                                    else
                                    {
                                        partnerContactoPw = new PartnerContactoPw()
                                        {
                                            IdPartner = entidad.Id,
                                            Nombres = contacto.Nombres,
                                            Apellidos = contacto.Apellidos,
                                            Email1 = contacto.Email1,
                                            Email2 = contacto.Email2,
                                            Telefono1 = contacto.Telefono1,
                                            Telefono2 = contacto.Telefono2,
                                            Estado = true,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                        };
                                        var resultado = _unitOfWork.PartnerContactoPwRepository.Add(partnerContactoPw);
                                        _unitOfWork.Commit();
                                        contacto.Id = resultado.Id;
                                    }
                                });
                            }
                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina el registro partner por id
        /// </summary>
        /// <param name="id">Id Partner</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.PartnerPwRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PartnerPwRepository.Delete(id, usuario);
                    var idsBeneficios = _unitOfWork.PartnerBeneficioPwRepository.ObtenerPorIdPartner(id).Select(x => x.Id);
                    var idsContactos = _unitOfWork.PartnerContactoPwRepository.ObtenerPorIdPartner(id).Select(x => x.Id);
                    if (idsBeneficios != null && idsBeneficios.Count() > 0)
                    {
                        _unitOfWork.PartnerBeneficioPwRepository.Delete(idsBeneficios, usuario);
                    }
                    if (idsContactos != null && idsContactos.Count() > 0)
                    {
                        _unitOfWork.PartnerContactoPwRepository.Delete(idsContactos, usuario);
                    }
                    _unitOfWork.Commit();
                    return respuesta;
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

        /// Autor: Jorge Gamero
        /// Fecha: 13/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro Partner PW para combo
        /// </summary>
        /// <returns> Lista PartnerPwDTO </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            return _unitOfWork.PartnerPwRepository.ObtenerCombo();
        }

    }
}
