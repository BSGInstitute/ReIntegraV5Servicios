using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class CriterioEvaluacionProcesoService : ICriterioEvaluacionProcesoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CriterioEvaluacionProcesoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, CriterioEvaluacionProceso>(MemberList.None).ReverseMap();
                cfg.CreateMap<CriterioEvaluacionProceso, CriterioEvaluacionProcesoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionProceso, CriterioEvaluacionProcesoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CriterioEvaluacionProceso, TCriterioEvaluacionProceso>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 16/04/2024
        /// <summary>
        /// Criterio Evaluacion Proceso
        /// </summary>
        /// <returns> Lista CriterioEvaluacionProcesoExamenDTO </returns>
        public IEnumerable<CriterioEvaluacionProcesoExamenDTO> Obtener()
        {
            var lista = _unitOfWork.CriterioEvaluacionProcesoRepository.Obtener();
            List<CriterioEvaluacionProcesoExamenDTO> listaCompleta = new List<CriterioEvaluacionProcesoExamenDTO>();
            foreach (var item in lista)
            {
                var count = _unitOfWork.ExamenRepository.ObtenerPorIdCriterioEvaluacionProceso(item.Id).ToList().Count();
                listaCompleta.Add(new CriterioEvaluacionProcesoExamenDTO { Id = item.Id, Nombre = item.Nombre, Relacionado = count > 0 ? true : false });
            }
            return listaCompleta;
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaPregunta
        /// </summary>
        /// <param name="dto">CategoriaPreguntaDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>CategoriaPreguntaDTO</returns>
        public CriterioEvaluacionProcesoDTO Insertar(CriterioEvaluacionProcesoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    CriterioEvaluacionProceso entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.CriterioEvaluacionProcesoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<CriterioEvaluacionProcesoDTO>(respuesta);


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
        /// Fecha: 16/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="CriterioEvaluacionProcesoDTO"> parametros de la nueva CriterioEvaluacionProceso y sus detalles </param>

        public CriterioEvaluacionProcesoDTO Actualizar(CriterioEvaluacionProcesoDTO dto, string usuario)
        {
            try
            {
                CriterioEvaluacionProceso? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CriterioEvaluacionProcesoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.CriterioEvaluacionProcesoRepository.Update(entidad);
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
        /// Fecha: 16/04/2024
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
                var entidad = _unitOfWork.CriterioEvaluacionProcesoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.CriterioEvaluacionProcesoRepository.Delete(id, usuario);

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
