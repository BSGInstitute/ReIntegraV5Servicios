using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralCertificacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralCertificacion
    /// </summary>
    public class ProgramaGeneralCertificacionService : IProgramaGeneralCertificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralCertificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralCertificacion, ProgramaGeneralCertificacion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralCertificacionAgendaDTO, ProgramaGeneralCertificacionDetalleAgendaDTO>(MemberList.None);
                    cfg.CreateMap<ProgramaGeneralCertificacionDTO, ProgramaGeneralCertificacion>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene certificaciones y argumentos para Agenda asociados a un Id Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralCertificacionDetalleAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionDetalleAgendaDTO> ObtenerCertificacionesDetalleParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var certificaciones = _unitOfWork.ProgramaGeneralCertificacionRepository.ObtenerCertificacionesParaAgendaPorIdOportunidad(idOportunidad);
                var certificacionesDetalle = _mapper.Map<List<ProgramaGeneralCertificacionDetalleAgendaDTO>>(certificaciones);
                var certificacionArgumentoService = new ProgramaGeneralCertificacionArgumentoService(_unitOfWork);
                certificacionesDetalle.ForEach(
                    c => c.Requisitos = certificacionArgumentoService.ObtenerProgramaGeneralCertificacionArgumentoAgendaPorIdCertificacion(c.IdCertificacion).ToList()
                );
                return certificacionesDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool EliminarCertificacionVenta(int idProgramaGeneralCertificacion, string usuario)
        {
            try
            {
                if (idProgramaGeneralCertificacion <= 0)
                {
                    throw new BadRequestException("Id No valido");
                }
                _unitOfWork.ProgramaGeneralCertificacionRepository.Delete(idProgramaGeneralCertificacion, usuario);

                var eliminarProgramaGeneralCertificacionModalidad = _unitOfWork.ProgramaGeneralCertificacionModalidadRepository.GetBy(x => x.IdProgramaGeneralCertificacion == idProgramaGeneralCertificacion && x.Estado == true);
                if (eliminarProgramaGeneralCertificacionModalidad != null && eliminarProgramaGeneralCertificacionModalidad.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralCertificacionModalidadRepository.Delete(eliminarProgramaGeneralCertificacionModalidad.Select(x => x.Id), usuario);
                }
                var eliminarProgramaGeneralCertificacionArgumento = _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.GetBy(x => x.IdProgramaGeneralCertificacion == idProgramaGeneralCertificacion && x.Estado == true);
                if (eliminarProgramaGeneralCertificacionArgumento != null && eliminarProgramaGeneralCertificacionArgumento.Count() > 0)
                {
                    _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Delete(eliminarProgramaGeneralCertificacionArgumento.Select(x => x.Id), usuario);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProgramaGeneralCertificacionDTO GuardarCertificacionesVentas(CompuestoCertificacionModalidadDTO certificadoDTO, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_unitOfWork.ProgramaGeneralCertificacionRepository.Exist(certificadoDTO.IdCertificacion))
                    {
                        var certificacion = _unitOfWork.ProgramaGeneralCertificacionRepository.ObtenerPorId(certificadoDTO.IdCertificacion)!;
                        certificacion.IdPgeneral = certificadoDTO.IdPGeneral;
                        certificacion.Nombre = certificadoDTO.NombreCertificacion;
                        certificacion.UsuarioModificacion = usuario;
                        certificacion.FechaModificacion = DateTime.Now;

                        var listaBorrar = _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository
                            .GetBy(x => x.IdProgramaGeneralCertificacion == certificadoDTO.IdCertificacion && x.Estado == true)
                            .Where(x => !certificadoDTO.CertificacionesArgumentos.Any(y => y.Id == x.Id)).Select(x => x.Id).ToList();
                        if (listaBorrar != null && listaBorrar.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Delete(listaBorrar, usuario);
                            _unitOfWork.Commit();
                        }

                        var listaBorrar2 = _unitOfWork.ProgramaGeneralCertificacionModalidadRepository
                            .GetBy(x => x.IdProgramaGeneralCertificacion == certificadoDTO.IdCertificacion && x.Estado == true)
                            .Where(x => !certificadoDTO.Modalidades.Any(y => y.Id == x.Id)).Select(x => x.Id).ToList();
                        if (listaBorrar2 != null && listaBorrar2.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralCertificacionModalidadRepository.Delete(listaBorrar2, usuario);
                            _unitOfWork.Commit();
                        }

                        certificacion.ProgramaGeneralCertificacionArgumentos = new List<ProgramaGeneralCertificacionArgumento>();
                        foreach (var subItem in certificadoDTO.CertificacionesArgumentos)
                        {
                            ProgramaGeneralCertificacionArgumento argumento;
                            if (_unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Exist(subItem.Id))
                            {
                                argumento = _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.ObtenerPorId(subItem.Id);
                                argumento.Nombre = subItem.Nombre;
                                argumento.IdPgeneral = certificadoDTO.IdPGeneral;
                                argumento.UsuarioModificacion = usuario;
                                argumento.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                argumento = new ProgramaGeneralCertificacionArgumento()
                                {
                                    Nombre = subItem.Nombre,
                                    IdPgeneral = certificadoDTO.IdPGeneral,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true,
                                };
                            }
                            certificacion.ProgramaGeneralCertificacionArgumentos.Add(argumento);
                        }
                        certificacion.ProgramaGeneralCertificacionModalidads = new List<ProgramaGeneralCertificacionModalidad>();
                        foreach (var subItem in certificadoDTO.Modalidades)
                        {
                            ProgramaGeneralCertificacionModalidad modalidad;
                            if (!_unitOfWork.ProgramaGeneralCertificacionModalidadRepository.Exist(x => x.IdProgramaGeneralCertificacion == certificacion.Id && x.IdModalidadCurso == subItem.IdModalidadCurso))
                            {
                                modalidad = new ProgramaGeneralCertificacionModalidad()
                                {
                                    Nombre = subItem.Nombre,
                                    IdPgeneral = certificadoDTO.IdPGeneral,
                                    IdModalidadCurso = subItem.IdModalidadCurso,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                                certificacion.ProgramaGeneralCertificacionModalidads.Add(modalidad);
                            }
                        }
                        _unitOfWork.ProgramaGeneralCertificacionRepository.Update(certificacion);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        var certificacion = new ProgramaGeneralCertificacion();
                        certificacion.IdPgeneral = certificadoDTO.IdPGeneral;
                        certificacion.Nombre = certificadoDTO.NombreCertificacion;
                        certificacion.UsuarioCreacion = usuario;
                        certificacion.UsuarioModificacion = usuario;
                        certificacion.FechaCreacion = DateTime.Now;
                        certificacion.FechaModificacion = DateTime.Now;
                        certificacion.Estado = true;

                        certificacion.ProgramaGeneralCertificacionArgumentos = certificadoDTO.CertificacionesArgumentos
                            .Select(x => new ProgramaGeneralCertificacionArgumento
                            {
                                Nombre = x.Nombre,
                                IdPgeneral = certificadoDTO.IdPGeneral,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            }).ToList();

                        certificacion.ProgramaGeneralCertificacionModalidads = certificadoDTO.Modalidades.Select(x => new ProgramaGeneralCertificacionModalidad()
                        {
                            Nombre = x.Nombre,
                            IdPgeneral = certificadoDTO.IdPGeneral,
                            IdModalidadCurso = x.IdModalidadCurso,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        }).ToList();
                        var res = _unitOfWork.ProgramaGeneralCertificacionRepository.Add(certificacion);
                        _unitOfWork.Commit();
                        certificadoDTO.IdCertificacion = res.Id;
                    }
                    scope.Complete();
                }
                var resultado = _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.ObtenerPorId(certificadoDTO.IdCertificacion);
                return _mapper.Map<ProgramaGeneralCertificacionDTO>(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
