using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class MaestroCursoComplementarioService : IMaestroCursoComplementarioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MaestroCursoComplementarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoCursoComplementario, CursoComplementario>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPuestoTrabajoCursoComplementario, CursoComplementarioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CursoComplementarioDTO, CursoComplementario>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPuestoTrabajoCursoComplementario, CursoComplementarioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CompetenciaTecnica, CursoComplementarioFiltroDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CursoComplementarioDTO, CursoComplementarioFiltroDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCompetenciaTecnica, CursoComplementarioFiltroDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCompetenciaTecnica, CursoComplementarioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CursoComplementarioFiltroDTO, TPuestoTrabajoCursoComplementario>(MemberList.None).ReverseMap();
                cfg.CreateMap<CursoComplementarioFiltroDTO, CursoComplementario>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 04/04/2024
        /// <summary>
        /// CursoComplementario Servicio
        /// </summary>
        /// <returns> Lista CategoriaPreguntaDTO </returns>
        public IEnumerable<CursoComplementarioDTO> Obtener()
        {
            return _unitOfWork.CompetenciaTecnicaRepository.Obtener();
        }


        public IEnumerable<TipoCursoComplementarioDTO> ObtenerCombos()
        {
            return _unitOfWork.TipoCompetenciaTecnicaRepository.ObtenerCombos();
        }


        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 26/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Curso Complementario
        /// </summary>
        /// <param name="dto">CursoComplementarioFiltroDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>CursoComplementarioFiltroDTO</returns>
        public CursoComplementarioFiltroDTO Insertar(CursoComplementarioFiltroDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    CompetenciaTecnica entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdTipoCompetenciaTecnica = dto.IdTipoCursoComplementario,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.CompetenciaTecnicaRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<CursoComplementarioFiltroDTO>(respuesta);


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


        /// Metodo Actualizar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="CategoriaPreguntaDTO"> parametros de la nueva CategoriaPregunta y sus detalles </param>

        public CursoComplementarioFiltroDTO Actualizar(CursoComplementarioFiltroDTO dto, string usuario)
        {
            try
            {
                CompetenciaTecnica? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CompetenciaTecnicaRepository.ObtenerPorIdCursoComplementario(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {

                            entidad.Nombre = dto.Nombre;
                            entidad.IdTipoCompetenciaTecnica = dto.IdTipoCursoComplementario;
                            entidad.UsuarioModificacion = usuario;     
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.CompetenciaTecnicaRepository.Update(entidad);
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


        /// Metodo Eliminar.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 26/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.CompetenciaTecnicaRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.CompetenciaTecnicaRepository.Delete(id, usuario);

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
