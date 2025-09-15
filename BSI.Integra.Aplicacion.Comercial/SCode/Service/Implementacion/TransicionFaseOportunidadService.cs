using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: TransicionCalificacionFaseService
    /// Autor: [Su Nombre]
    /// Fecha: [Fecha Actual]
    /// <summary>
    /// Servicio para la gestión de TransicionCalificacionFase
    /// </summary>
    public class TransicionFaseOportunidadService : ITransicionFaseOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private ITransicionFaseOportunidadRepository _transicionCalificacionFaseRepository;
        private Mapper _mapper;

        public ITransicionFaseOportunidadRepository TransicionCalificacionFaseRepository { get => _transicionCalificacionFaseRepository; set => _transicionCalificacionFaseRepository = value; }

        public TransicionFaseOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            TransicionCalificacionFaseRepository = _unitOfWork.TransicionFaseOportunidadRepository;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTransicionFaseOportunidad, TransicionFaseOportunidad>().ReverseMap();
                cfg.CreateMap<TransicionFaseOportunidad, TransicionCalificacionFaseCreateDTO>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);
        }

        public TransicionFaseOportunidad Update(TransicionFaseOportunidad entidad)
        {


            try
            {
                var modelo = _unitOfWork.TransicionFaseOportunidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TransicionFaseOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.TransicionFaseOportunidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TransicionCalificacionFaseDTO> ObtenerTransicionesCalificacionFase()
        {
            try
            {
                return TransicionCalificacionFaseRepository.ObtenerTransicionesCalificacionFase();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TransicionFaseOportunidad ObtenerTransicionCalificacionFasePorId(int idTransicionCalificacionFase)
        {
            try
            {
                return _unitOfWork.TransicionFaseOportunidadRepository.ObtenerPorId(idTransicionCalificacionFase);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransicionFaseOportunidad Add(TransicionFaseOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.TransicionFaseOportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TransicionFaseOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}