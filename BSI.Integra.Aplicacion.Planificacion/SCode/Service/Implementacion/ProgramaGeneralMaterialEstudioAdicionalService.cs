
using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: MaterialAdicionalAulaVirtualService
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 07/06/2023
    /// <summary>
    /// Gestión general de V_TFeedbackTipo_Filtro
    /// </summary>
    public class ProgramaGeneralMaterialEstudioAdicionalService : IProgramaGeneralMaterialEstudioAdicionalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralMaterialEstudioAdicionalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    //cfg.CreateMap<TMaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        public IEnumerable<ComboDTO> ObtenerProgramaGeneralMaterialEstudio()
        {
            try
            {
                var respuesta = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.ObtenerProgramaGeneralMaterialEstudioAdicional();
                return _mapper.Map<List<ComboDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ProgramaGeneralMaterialAgrupadoDTO ObtenerProgramaGeneralMaterialEstudioDetalle(int idPgeneral)
        {
            try
            {

                var programaGeneralMaterialRegistro = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.ObtenerPorIdPgeneral(idPgeneral);
                var programaGeneralMaterialPespecifico = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository.ObtenerIdsPorIdPgeneral(idPgeneral);
                ProgramaGeneralMaterialAgrupadoDTO respuesta = new ProgramaGeneralMaterialAgrupadoDTO();
                if (programaGeneralMaterialRegistro != null)
                {
                    respuesta.MaterialAdicional = programaGeneralMaterialRegistro.Select(x => new ProgramaGeneralMaterialRegistroDTO
                    {
                        Id = x.Id,
                        NombreArchivo = x.NombreArchivo,
                        EnlaceArchivo =x.EnlaceArchivo,
                        EsEnlace =x.EsEnlace,
                        IdPGeneral = x.IdPgeneral
                    }).ToList();
                }
                if (programaGeneralMaterialPespecifico != null && programaGeneralMaterialPespecifico.Count() > 0)
                {
                    respuesta.ListaEspecifico = programaGeneralMaterialPespecifico.Select(x => x.Valor).ToArray();
                }
                return _mapper.Map<ProgramaGeneralMaterialAgrupadoDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<ComboDTO> InsertarActualizarProgramaGeneralMaterialEstudio(ProgramaGeneralMaterialEstudioAdicionalEntidadDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    var listaParaBorrarPespecificos = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository.ObtenerIdsPorIdPgeneral(dto.IdPGeneral).ToList();
                    listaParaBorrarPespecificos.RemoveAll(x => dto.IdsPEspecificos.Any(y => y == x.Valor));
                    if (listaParaBorrarPespecificos.Count() > 0) _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository.Delete(listaParaBorrarPespecificos.Select(x => x.Id), usuario);

                    var listaParaBorrarRegistros = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.ObtenerPorIdPgeneral(dto.IdPGeneral).ToList();
                    listaParaBorrarRegistros.RemoveAll(x => dto.MaterialRegistro.Select(x => x.Id).Any(y => y == x.Id));
                    if (listaParaBorrarRegistros.Count() > 0) _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.Delete(listaParaBorrarRegistros.Select(x => x.Id), usuario);

                    if (listaParaBorrarPespecificos.Count() > 0 || listaParaBorrarRegistros.Count() > 0) _unitOfWork.Commit();

                    List<ProgramaGeneralMaterialEstudioAdicional> listaProgramaGeneralMaterialEstudioAdicional = new List<ProgramaGeneralMaterialEstudioAdicional>();
                    List<ProgramaGeneralMaterialEstudioAdicional> listaProgramaGeneralMaterialEstudioActualizarAdicional = new List<ProgramaGeneralMaterialEstudioAdicional>();

                    var programaGeneralMaterialNuevosRegistros = dto.MaterialRegistro.Where(x => x.Id == 0).ToList();
                    if (programaGeneralMaterialNuevosRegistros != null && programaGeneralMaterialNuevosRegistros.Count() > 0)
                    {
                        foreach (var item in programaGeneralMaterialNuevosRegistros)
                        {
                            ProgramaGeneralMaterialEstudioAdicional pgeneral = new ProgramaGeneralMaterialEstudioAdicional();
                            pgeneral.IdPgeneral = dto.IdPGeneral;
                            pgeneral.NombreArchivo = item.NombreArchivo;
                            pgeneral.EnlaceArchivo = item.EnlaceArchivo;
                            pgeneral.EsEnlace = item.EsEnlace;
                            pgeneral.Estado = true;
                            pgeneral.UsuarioCreacion = usuario;
                            pgeneral.UsuarioModificacion = usuario;
                            pgeneral.FechaCreacion = DateTime.Now;
                            pgeneral.FechaModificacion = DateTime.Now;

                            listaProgramaGeneralMaterialEstudioAdicional.Add(pgeneral);
                        }
                        _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.Add(listaProgramaGeneralMaterialEstudioAdicional);
                    }

                    var programaGeneralMaterialActualizarRegistros = dto.MaterialRegistro.Where(x => x.Id != 0).ToList();
                    if (programaGeneralMaterialActualizarRegistros != null && programaGeneralMaterialActualizarRegistros.Count() > 0)
                    {
                        foreach (var item in programaGeneralMaterialActualizarRegistros)
                        {
                            ProgramaGeneralMaterialEstudioAdicional pgeneral = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.ObtenerPorId(item.Id);
                            pgeneral.IdPgeneral = item.IdPGeneral;
                            pgeneral.NombreArchivo = item.NombreArchivo;
                            pgeneral.EnlaceArchivo = item.EnlaceArchivo;
                            pgeneral.EsEnlace = item.EsEnlace;
                            pgeneral.UsuarioModificacion = usuario;
                            pgeneral.FechaModificacion = DateTime.Now;

                            listaProgramaGeneralMaterialEstudioActualizarAdicional.Add(pgeneral);
                        }
                        _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.Update(listaProgramaGeneralMaterialEstudioActualizarAdicional);
                    }

                    List<ProgramaGeneralMaterialEstudioAdicionalEspecifico> listaProgramaGeneralMaterialEstudioAdicionalPespecifico = new List<ProgramaGeneralMaterialEstudioAdicionalEspecifico>();
                    List<ProgramaGeneralMaterialEstudioAdicionalEspecifico> listaProgramaGeneralMaterialEstudioAdicionalActualizarPespecifico = new List<ProgramaGeneralMaterialEstudioAdicionalEspecifico>();
                    if (dto.IdsPEspecificos != null && dto.IdsPEspecificos.Count > 0)
                    {
                        foreach (var item in dto.IdsPEspecificos)
                        {
                            ProgramaGeneralMaterialEstudioAdicionalEspecifico pespecifico;
                            pespecifico = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository.ObtenerPorIdyIdPgeneral(dto.IdPGeneral, item);
                            if (pespecifico != null && pespecifico.Id != 0)
                            {
                                listaProgramaGeneralMaterialEstudioAdicionalActualizarPespecifico.Add(pespecifico);
                            }
                            else
                            {
                                pespecifico = new ProgramaGeneralMaterialEstudioAdicionalEspecifico();
                                pespecifico.MaterialEstudioAdicionalPorPgeneralId = dto.IdPGeneral;
                                pespecifico.IdPespecifico = item;
                                pespecifico.Estado = true;
                                pespecifico.UsuarioCreacion = usuario;
                                pespecifico.UsuarioModificacion = usuario;
                                pespecifico.FechaCreacion = DateTime.Now;
                                pespecifico.FechaModificacion = DateTime.Now;
                                listaProgramaGeneralMaterialEstudioAdicionalPespecifico.Add(pespecifico);
                            }
                        }
                        if(listaProgramaGeneralMaterialEstudioAdicionalPespecifico.Count() > 0) _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository.Add(listaProgramaGeneralMaterialEstudioAdicionalPespecifico);
                        if(listaProgramaGeneralMaterialEstudioAdicionalActualizarPespecifico.Count() > 0) _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository.Update(listaProgramaGeneralMaterialEstudioAdicionalActualizarPespecifico);
                    }
                    if (listaProgramaGeneralMaterialEstudioAdicionalPespecifico.Count() > 0 || listaProgramaGeneralMaterialEstudioAdicionalActualizarPespecifico.Count() > 0 || listaProgramaGeneralMaterialEstudioAdicional.Count() > 0 || listaProgramaGeneralMaterialEstudioActualizarAdicional.Count() > 0)
                    {
                        _unitOfWork.Commit();
                    }
                    return ObtenerProgramaGeneralMaterialEstudio();
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw new BadRequestException("Error al intentar guardar");
            }
        }
        public bool EliminarProgramaGeneralMaterialEstudio(int idPgeneral, string usuario)
        {
            try
            {
                var entidades = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.ObtenerPorIdPgeneral(idPgeneral);
                if (entidades != null && entidades.Count() > 0)
                {
                    var respuesta = _unitOfWork.ProgramaGeneralMaterialEstudioAdicionalRepository.Delete(entidades.Select(x => x.Id).ToList(), usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                    throw new BadRequestException($"No se encontro la entidad con el id {idPgeneral}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
