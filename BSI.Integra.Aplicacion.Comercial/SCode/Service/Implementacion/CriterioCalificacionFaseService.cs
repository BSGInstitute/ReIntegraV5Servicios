using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.Implementacion
{
    /// Servicio: CriterioCalificacionFaseService
    /// Autor: José Vega
    /// Fecha: 20/09/2023
    /// <summary>
    /// Servicio para gestionar los criterios de calificación de fase
    /// </summary>
    public class CriterioCalificacionFaseService : ICriterioCalificacionFaseService
    {
        private IUnitOfWork _unitOfWork;
        private ICriterioCalificacionFaseRepository _repCriterioCalificacionFase;
        private Mapper _mapper;

        public ICriterioCalificacionFaseRepository CriterioCalificacionFaseRepository { get => _repCriterioCalificacionFase; set => _repCriterioCalificacionFase = value; }

        public CriterioCalificacionFaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CriterioCalificacionFaseRepository = _unitOfWork.CriterioFaseRepository;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioCalificacionFaseOportunidad, CriterioCalificacionFase>().ReverseMap();
                cfg.CreateMap<CriterioCalificacionFase, CriterioCalificacionFaseCreateDTO>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);

        }


        public CriterioCalificacionFase Add(CriterioCalificacionFase entidad)
        {
            try
            {
                var modelo = _unitOfWork.CriterioFaseRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioCalificacionFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza un criterio de calificación de fase existente y sus lineamientos
        /// </summary>
        /// <param name="criterioCalificacionFaseDTO">Datos del criterio a actualizar</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>bool</returns>
        public CriterioCalificacionFase ActualizarCriterio(CriterioCalificacionFase entidad)
   {

            try
            {
                var modelo = _unitOfWork.CriterioFaseRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CriterioCalificacionFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina de manera lógica un criterio de calificación de fase y sus lineamientos
        /// </summary>
        /// <param name="id">ID del criterio a eliminar</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>bool</returns>
        public bool EliminarCriterio(int id, string usuario)
        {
            try
            {
                _unitOfWork.CriterioFaseRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los criterios de calificación de fase
        /// </summary>
        /// <returns>Lista de criterios</returns>
        public List<CriterioCalificacionFaseDTO> ObtenerCriteriosCalificacionFase()
        {
            try
            {
                return _repCriterioCalificacionFase.ObtenerCriteriosCalificacionFase();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los criterios asociados a una transición específica
        /// </summary>
        /// <param name="idTransicionCalificacionFase">ID de la transición</param>
        /// <returns>Lista de criterios</returns>
        public CriterioCalificacionFase ObtenerPorId(int idTransicionCalificacionFase)
        {
            try
            {
                return _repCriterioCalificacionFase.ObtenerPorId(idTransicionCalificacionFase);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}