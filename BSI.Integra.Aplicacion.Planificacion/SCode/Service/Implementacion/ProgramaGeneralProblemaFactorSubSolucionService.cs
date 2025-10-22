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
    public class ProgramaGeneralProblemaFactorSubSolucionService : IProgramaGeneralProblemaFactorSubSolucionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorSubSolucionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactorSubSolucion, ProgramaGeneralProblemaFactorSubSolucion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaFactorSubSolucion, ProgramaGeneralProblemaFactorSubSolucionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralProblemaFactorSubSolucion, ProgramaGeneralProblemaFactorSubSolucionDTO>(MemberList.None).ReverseMap();
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
        /// <returns> Lista ProgramaGeneralProblemaFactorSubSolucionDTO </returns>
        public IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> Obtener()
        {
            return _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionRepository.Obtener();
        }
        public IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> ObtenerPorIdProgramaGeneralProblemaFactorSolucion(int idProgramaGeneralProblemaFactorSolucion)
        {
            return _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionRepository.ObtenerPorIdProgramaGeneralProblemaFactorSolucion(idProgramaGeneralProblemaFactorSolucion);
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo ProgramaGeneralProblemaFactorSubSolucion
        /// </summary>
        /// <param name="dto">ProgramaGeneralProblemaFactorSubSolucion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>ProgramaGeneralProblemaFactorSubSolucionDTO</returns>
        public IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> Insertar(List<ProgramaGeneralProblemaFactorSubSolucionDTO> dtos,
     string usuario)
        {
            if (dtos == null || dtos.Count == 0)
                throw new BadRequestException("La lista de sub soluciones está vacía.");

            try
            {
                var ahora = DateTime.Now;
                var entidades = dtos.Select(dto => new ProgramaGeneralProblemaFactorSubSolucion
                {
                    IdProgramaGeneralProblemaFactorSolucion = dto.IdProgramaGeneralProblemaFactorSolucion,
                    Solucion = dto.Solucion?.Trim(),
                    Orden = dto.Orden,
                    Nivel = dto.Nivel,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = ahora,
                    FechaModificacion = ahora,
                }).ToList();

             
                foreach (var e in entidades)
                    _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionRepository.Add(e);

                _unitOfWork.Commit(); 

            
                var result = entidades.Select(e => new ProgramaGeneralProblemaFactorSubSolucionDTO
                {
                    Id = e.Id,
                    IdProgramaGeneralProblemaFactorSolucion = e.IdProgramaGeneralProblemaFactorSolucion,
                    Solucion = e.Solucion,
                    Orden = e.Orden,
                    Nivel = e.Nivel
                }).ToList();

                return result;
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
        /// Modifica un ProgramaGeneralProblemaFactorSubSolucion
        /// </summary>
        /// <param name="dto">ProgramaGeneralProblemaFactorSubSolucion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>ProgramaGeneralProblemaFactorSubSolucionDTO</returns>
        public IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> Actualizar(List<ProgramaGeneralProblemaFactorSubSolucionDTO> dtos, string usuario)
        {
            if (dtos == null || dtos.Count == 0)
                throw new BadRequestException("La lista está vacía.");

            var ahora = DateTime.Now;
            var resultado = new List<ProgramaGeneralProblemaFactorSubSolucionDTO>();

            foreach (var dto in dtos)
            {
                var entidad = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionRepository
                                 .FirstById(dto.Id);

                if (entidad == null)
                    throw new BadRequestException($"No existe Id={dto.Id}");

                entidad.IdProgramaGeneralProblemaFactorSolucion = dto.IdProgramaGeneralProblemaFactorSolucion;
                entidad.Solucion = dto.Solucion?.Trim();
                entidad.Orden = dto.Orden;
                entidad.Nivel = dto.Nivel;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaModificacion = ahora;

                _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionRepository.Update(entidad);

                resultado.Add(new ProgramaGeneralProblemaFactorSubSolucionDTO
                {
                    Id = entidad.Id,
                    IdProgramaGeneralProblemaFactorSolucion = entidad.IdProgramaGeneralProblemaFactorSolucion,
                    Solucion = entidad.Solucion,
                    Orden = entidad.Orden,
                    Nivel = entidad.Nivel
                });
            }

            _unitOfWork.Commit();
            return resultado;
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
                if (id <= 0) throw new BadRequestException("Id inválido");

                var repo = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionRepository;

                var padre = repo.ObtenerPorId(id);
                if (padre == null || padre.Id == 0)
                    throw new BadRequestException($"No se encontró el id {id}");

                // Traer todos los del mismo grupo (padre + hijos)
                var grupo = repo.GetBy(x =>
                    x.IdProgramaGeneralProblemaFactorSolucion == padre.IdProgramaGeneralProblemaFactorSolucion &&
                    x.Orden == padre.Orden
                ).ToList();

                foreach (var item in grupo)
                    repo.Delete(item.Id, usuario);

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
