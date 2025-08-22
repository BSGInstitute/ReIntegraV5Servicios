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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: CriterioEvaluacionCategoriumService
    /// Autor: Klebert Layme.
    /// Fecha: 08/05/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacionCategorium
    /// </summary>
    public class CriterioEvaluacionCategoriumService : ICriterioEvaluacionCategoriumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CriterioEvaluacionCategoriumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TCriterioEvaluacionCategorium, CriterioEvaluacionCategorium>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TCriterioEvaluacionCategorium, CriterioEvaluacionCategoriumDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<CriterioEvaluacionCategorium, CriterioEvaluacionCategoriumDTO>(MemberList.None).ReverseMap();
                }
              );
            _mapper = new Mapper(config);

        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CriterioEvaluacionCategoria para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns> 
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CriterioEvaluacionCategoriumRepository.ObtenerCombo().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }
        /// Autor:  Klebert Layme.
        /// Fecha: 08/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CriterioEvaluacionCategorium
        /// </summary>
        /// <returns> List<CriterioEvaluacionCategoriumDTO> </returns>
        public List<CriterioEvaluacionCategoriumDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.CriterioEvaluacionCategoriumRepository.Obtener();
                return _mapper.Map<List<CriterioEvaluacionCategoriumDTO>>(respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 08/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id CriterioEvaluacionCategorium 
        /// </summary>
        /// <param name="idPGeneral">Id del Criterio Evaluacion Categoria</param>

        /// <returns> Cargo </returns>
        public CriterioEvaluacionCategoriumDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.CriterioEvaluacionCategoriumRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<CriterioEvaluacionCategoriumDTO>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme.
        /// Fecha: 08/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Criterio Evaluacion Categoria
        /// </summary>
        /// <param name="dto">CriterioEvaluacion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>CargoDTO</returns>
        public CriterioEvaluacionCategoriumDTO Insertar(CriterioEvaluacionCategoriumDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    CriterioEvaluacionCategorium entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.CriterioEvaluacionCategoriumRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<CriterioEvaluacionCategoriumDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme.
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Criterio Evaluacion Categoria
        /// </summary>
        /// <param name="dto">Criterio Evaluacion Categoria</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>CriterioEvaluacionCategoriumDTO</returns>
        public CriterioEvaluacionCategoriumDTO Actualizar(CriterioEvaluacionCategoriumDTO dto, string usuario)
        {
            try
            {
                CriterioEvaluacionCategorium entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CriterioEvaluacionCategoriumRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.Estado = true;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = usuario;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
                var respuesta = _unitOfWork.CriterioEvaluacionCategoriumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioEvaluacionCategoriumDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme.
        /// Fecha: 08/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de Criterio Evaluacion Categoria
        /// </summary>
        /// <param name="idPGeneral">Id del Criterio Evaluacion Categoria</param>
        /// <returns> CriterioEvaluacionCategoriumDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var criterioEvaluacionCategorium = _unitOfWork.CriterioEvaluacionCategoriumRepository.ObtenerPorId(id);
                if (criterioEvaluacionCategorium != null && criterioEvaluacionCategorium.Id != 0)
                {
                    var respuesta = _unitOfWork.CriterioEvaluacionCategoriumRepository.Delete(id, usuario);
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