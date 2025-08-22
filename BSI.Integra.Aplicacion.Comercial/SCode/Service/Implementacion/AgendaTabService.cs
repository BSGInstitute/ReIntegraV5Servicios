using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: AgendaTabService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_AgendaTab
    /// </summary>
    public class AgendaTabService : IAgendaTabService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public AgendaTabService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TAgendaTab, AgendaTab>(MemberList.None).ReverseMap();
                    cfg.CreateMap<AgendaTabDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista AgendaTabDTO </returns>
        public IEnumerable<AgendaTabDTO> Obtener()
        {
            return _unitOfWork.AgendaTabRepository.Obtener();
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>AgendaTabDTO</returns>
        public AgendaTabDTO Insertar(AgendaTabAlternoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    AgendaTab entidad = new()
                    {
                        Nombre = dto.Nombre,
                        CodigoAreaTrabajo = dto.CodigoAreaTrabajo,
                        VisualizarActividad = true,
                        CargarInformacionInicial = true,
                        Numeracion = 1,
                        ValidarFecha = true,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.AgendaTabRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<AgendaTabDTO>(respuesta);
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
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>AgendaTabDTO</returns>
        public AgendaTabDTO Actualizar(AgendaTabAlternoDTO dto, string usuario)
        {
            try
            {
                AgendaTab entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.AgendaTabRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.CodigoAreaTrabajo = dto.CodigoAreaTrabajo;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
                var respuesta = _unitOfWork.AgendaTabRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AgendaTabDTO>(respuesta);
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
                var materialAccion = _unitOfWork.AgendaTabRepository.ObtenerPorId(id);
                if (materialAccion != null && materialAccion.Id != 0)
                {
                    var respuesta = _unitOfWork.AgendaTabRepository.Delete(id, usuario);
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
