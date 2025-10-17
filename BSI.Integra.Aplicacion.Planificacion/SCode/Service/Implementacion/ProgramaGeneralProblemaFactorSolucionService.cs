using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class ProgramaGeneralProblemaFactorSolucionService : IProgramaGeneralProblemaFactorSolucionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorSolucionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactorSolucion, ProgramaGeneralProblemaFactorSolucion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaFactorSolucion, ProgramaGeneralProblemaFactorSolucionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralProblemaFactorSolucion, ProgramaGeneralProblemaFactorSolucionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPartnerBeneficioPw, PartnerBeneficioPw>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro Problema Factor
        /// </summary>
        /// <returns> Lista ProgramaGeneralProblemaFactorSolucionDTO </returns>
        public IEnumerable<ProgramaGeneralProblemaFactorSolucionDTO> Obtener()
        {
            return _unitOfWork.ProgramaGeneralProblemaFactorSolucionRepository.Obtener();
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 17/10/2025
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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo ProgramaGeneralProblemaFactorSolucion
        /// </summary>
        /// <param name="dto">ProgramaGeneralProblemaFactorSolucion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>ProgramaGeneralProblemaFactorSolucionDTO</returns>
        public ProgramaGeneralProblemaFactorSolucionDTO Insertar(ProgramaGeneralProblemaFactorSolucionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    ProgramaGeneralProblemaFactorSolucion entidad = new()
                    {
                        Descripcion = dto.Descripcion,
                        Titulo = dto.Titulo,
                        SubTitulo = dto.SubTitulo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.ProgramaGeneralProblemaFactorSolucionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<ProgramaGeneralProblemaFactorSolucionDTO>(respuesta);


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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Modifica un ProgramaGeneralProblemaFactorSolucion
        /// </summary>
        /// <param name="dto">ProgramaGeneralProblemaFactorSolucion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>ProgramaGeneralProblemaFactorSolucionDTO</returns>
        public ProgramaGeneralProblemaFactorSolucionDTO Actualizar(ProgramaGeneralProblemaFactorSolucionDTO dto, string usuario)
        {
            try
            {
                ProgramaGeneralProblemaFactorSolucion? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.ProgramaGeneralProblemaFactorSolucionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Descripcion = dto.Descripcion;
                            entidad.Titulo = dto.Titulo;
                            entidad.SubTitulo = dto.SubTitulo;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.ProgramaGeneralProblemaFactorSolucionRepository.Update(entidad);
                            _unitOfWork.Commit();


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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 17/10/2025
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
                var entidad = _unitOfWork.ProgramaGeneralProblemaFactorSolucionRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.ProgramaGeneralProblemaFactorSolucionRepository.Delete(id, usuario);

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

    }
}
