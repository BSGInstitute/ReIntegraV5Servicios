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
    public class TransicionCalificacionFaseService : ITransicionCalificacionFaseService
    {
        private IUnitOfWork _unitOfWork;
        private ITransicionCalificacionFaseRepository _transicionCalificacionFaseRepository;
        private Mapper _mapper;

        public ITransicionCalificacionFaseRepository TransicionCalificacionFaseRepository { get => _transicionCalificacionFaseRepository; set => _transicionCalificacionFaseRepository = value; }

        public TransicionCalificacionFaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            TransicionCalificacionFaseRepository = _unitOfWork.TransicionFaseRepository;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTransicionFaseOportunidad, TransicionCalificacionFase>().ReverseMap();
                cfg.CreateMap<TransicionCalificacionFase, TransicionCalificacionFaseCreateDTO>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);
        }

        public TransicionCalificacionFase Update(TransicionCalificacionFase entidad)
        {


            try
            {
                var modelo = _unitOfWork.TransicionFaseRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TransicionCalificacionFase>(modelo);
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
                _unitOfWork.TransicionFaseRepository.Delete(id, usuario);
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

        public TransicionCalificacionFase ObtenerTransicionCalificacionFasePorId(int idTransicionCalificacionFase)
        {
            try
            {
                return _unitOfWork.TransicionFaseRepository.ObtenerTransicionCalificacionFasePorId(idTransicionCalificacionFase);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransicionCalificacionFase Add(TransicionCalificacionFase entidad)
        {
            try
            {
                var modelo = _unitOfWork.TransicionFaseRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TransicionCalificacionFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}