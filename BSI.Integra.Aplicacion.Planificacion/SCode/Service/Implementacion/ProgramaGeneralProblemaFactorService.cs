using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class ProgramaGeneralProblemaFactorService : IProgramaGeneralProblemaFactorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactor, ProgramaGeneralProblemaFactor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaFactor, ProgramaGeneralProblemaFactorDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralProblemaFactor, ProgramaGeneralProblemaFactorDTO>(MemberList.None).ReverseMap();
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
        /// <returns> Lista ProgramaGeneralProblemaFactorDTO </returns>
        public IEnumerable<ProgramaGeneralProblemaFactorDTO> Obtener()
        {
            return _unitOfWork.ProgramaGeneralProblemaFactorRepository.Obtener();
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
        /// Registra un nuevo ProgramaGeneralProblemaFactor
        /// </summary>
        /// <param name="dto">ProgramaGeneralProblemaFactor</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>ProgramaGeneralProblemaFactorDTO</returns>
        public ProgramaGeneralProblemaFactorDTO Insertar(ProgramaGeneralProblemaFactorDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    ProgramaGeneralProblemaFactor entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.ProgramaGeneralProblemaFactorRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<ProgramaGeneralProblemaFactorDTO>(respuesta);


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
        /// Modifica un ProgramaGeneralProblemaFactor
        /// </summary>
        /// <param name="dto">ProgramaGeneralProblemaFactor</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>ProgramaGeneralProblemaFactorDTO</returns>
        public ProgramaGeneralProblemaFactorDTO Actualizar(ProgramaGeneralProblemaFactorDTO dto, string usuario)
        {
            try
            {
                ProgramaGeneralProblemaFactor? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.ProgramaGeneralProblemaFactorRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.ProgramaGeneralProblemaFactorRepository.Update(entidad);
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
                var entidad = _unitOfWork.ProgramaGeneralProblemaFactorRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.ProgramaGeneralProblemaFactorRepository.Delete(id, usuario);

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
