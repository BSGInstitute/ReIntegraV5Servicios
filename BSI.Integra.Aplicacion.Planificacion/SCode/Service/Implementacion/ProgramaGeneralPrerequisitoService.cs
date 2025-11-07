using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralPrerequisitoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPrerequisito
    /// </summary>
    public class ProgramaGeneralPrerequisitoService : IProgramaGeneralPrerequisitoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralPrerequisitoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPrerequisito, ProgramaGeneralPrerequisito>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPrerequisito, ProgramaGeneralPrerequisitoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPrerequisito, ProgramaGeneralPrerequisitoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPrerequisitoModalidad, ProgramaGeneralPrerequisito>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPrerequisitoModalidad, ProgramaGeneralPrerequisitoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPrerequisito, ProgramaGeneralPrerequisitoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Prerequisitos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralPrerequisitoOportunidadDTO> </returns>
        public IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 07/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene Prerequisitos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralPrerequisitoOportunidadDTO> </returns>
        public IEnumerable<ProgramaGeneralPrerequisitoOportunidadV2DTO> ObtenerProgramaGeneralPrerequisitoPorIdOportunidadV2(int idOportunidad)
        {
            IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> datosRepo;
            try
            {
                datosRepo = _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la capa de repositorio: {ex.Message}", ex);
            }

            if (datosRepo == null)
            {
                return Enumerable.Empty<ProgramaGeneralPrerequisitoOportunidadV2DTO>();
            }

            string ObtenerTextoRespuesta(int idRespuesta)
            {
                if (idRespuesta == 0) return null;
                _mapeoRespuestas.TryGetValue(idRespuesta, out string txt);
                return txt;
            }
            var resultadoTransformado = datosRepo.Select(prereq => new ProgramaGeneralPrerequisitoOportunidadV2DTO
            {
                IdPrerequisito = prereq.IdPrerequisito,
                PRNombre = prereq.PRNombre,
                Completado = prereq.Completado,
                Respuesta = ObtenerTextoRespuesta(prereq.Respuesta)
            });

            return resultadoTransformado;
        }
        private static readonly Dictionary<int, string> _mapeoRespuestas = new Dictionary<int, string>
        {
            { 1, "Cumple al 100%" },
            { 2, "Cumple al 75%" },
            { 3, "Cumple al 50%" },
            { 4, "Cumple al 25%" },
            { 5, "No cumple" }
        };

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Prerequisitos Especificos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralPrerequisitoOportunidadDTO> </returns>
        public IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerProgramaGeneralPrerequisitoEspecificoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerProgramaGeneralPrerequisitoEspecificoPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idPrerequisito"></param>
        /// <param name="usuario"></param>
        /// <param name="listaModalidades"></param>
        /// <returns> bool </returns>
        public bool EliminarPreRequisitos(int idPrerequisito, string usuario)
        {

            try
            {
                if (_unitOfWork.ProgramaGeneralPrerequisitoRepository.Exist(idPrerequisito))
                {
                    _unitOfWork.ProgramaGeneralPrerequisitoRepository.Delete(idPrerequisito, usuario);
                    var listaBorrar = _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.GetBy(x => x.IdProgramaGeneralPrerequisito == idPrerequisito && x.Estado == true).ToList();
                    _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="jsonDTO"></param>
        /// <param name="usuario"></param>
        /// <returns> ProgramaGeneralPrerequisitoDTO </returns>
        public ProgramaGeneralPrerequisitoDTO GuardarPreRequisitos(CompuestoPreRequisitoModalidadAlternaDTO jsonDTO, string usuario)
        {
            try
            {
                bool isNew = false;
                List<ProgramaGeneralPrerequisitoModalidad> modalidadPreRequisito = new();


                ProgramaGeneralPrerequisito preRequisito = new();
                if (_unitOfWork.ProgramaGeneralPrerequisitoRepository.Exist(jsonDTO.IdPreRequisito))
                {
                    preRequisito = _unitOfWork.ProgramaGeneralPrerequisitoRepository.ObtenerPorId(jsonDTO.IdPreRequisito)!;
                    preRequisito.Nombre = jsonDTO.NombrePreRequisito ?? "";
                    preRequisito.Tipo = jsonDTO.Tipo;
                    preRequisito.UsuarioModificacion = usuario;
                    preRequisito.FechaModificacion = DateTime.Now;

                    var listaBorrar = _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository
                        .GetBy(x => x.IdProgramaGeneralPrerequisito == jsonDTO.IdPreRequisito)
                        .Where(x => !jsonDTO.Modalidades.Any(y => y.IdModalidad == x.IdModalidadCurso)).ToList();
                    if (listaBorrar != null && listaBorrar.Count() > 0)
                    {
                        _unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                        _unitOfWork.Commit();
                    }
                    preRequisito.ProgramaGeneralPrerequisitoModalidads = new();
                    foreach (var modalidad in jsonDTO.Modalidades)
                    {
                        ProgramaGeneralPrerequisitoModalidad preRequisitoModalidad;
                        if (!_unitOfWork.ProgramaGeneralPrerequisitoModalidadRepository.Exist(x => x.IdProgramaGeneralPrerequisito == jsonDTO.IdPreRequisito
                            && x.IdModalidadCurso == modalidad.IdModalidad))
                        {
                            preRequisitoModalidad = new ProgramaGeneralPrerequisitoModalidad()
                            {
                                Nombre = modalidad.Nombre ?? "",
                                IdModalidadCurso = modalidad.IdModalidad,
                                IdPgeneral = jsonDTO.IdPGeneral,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                            preRequisito.ProgramaGeneralPrerequisitoModalidads.Add(preRequisitoModalidad);
                        }
                    }
                    _unitOfWork.ProgramaGeneralPrerequisitoRepository.Update(preRequisito);
                    _unitOfWork.Commit();
                }
                else
                {
                    preRequisito = new ProgramaGeneralPrerequisito()
                    {
                        Nombre = jsonDTO.NombrePreRequisito ?? "",
                        IdPgeneral = jsonDTO.IdPGeneral,
                        Tipo = jsonDTO.Tipo,
                        Orden = jsonDTO.Orden,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                    };
                    preRequisito.ProgramaGeneralPrerequisitoModalidads = jsonDTO.Modalidades.Select(x => new ProgramaGeneralPrerequisitoModalidad
                    {
                        Nombre = x.Nombre ?? "",
                        IdModalidadCurso = x.IdModalidad,
                        IdPgeneral = jsonDTO.IdPGeneral,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    }).ToList();

                    var resultado = _unitOfWork.ProgramaGeneralPrerequisitoRepository.Add(preRequisito);
                    _unitOfWork.Commit();
                    preRequisito.Id = resultado.Id;
                }
                return _mapper.Map<ProgramaGeneralPrerequisitoDTO>(preRequisito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
