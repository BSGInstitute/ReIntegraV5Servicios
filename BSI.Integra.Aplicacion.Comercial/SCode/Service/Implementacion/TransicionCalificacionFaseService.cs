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
                cfg.CreateMap<TTransicionFase, TransicionCalificacionFase>().ReverseMap();
                cfg.CreateMap<TransicionCalificacionFase, TransicionCalificacionFaseCreateDTO>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);
        }

        public TransicionCalificacionFaseCreateDTO InsertarTransicionCalificacionFase(TransicionCalificacionFaseCreateDTO transicionCalificacionFaseCreateDTO, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {

                    TransicionCalificacionFase TransicionCalificacionFase = new TransicionCalificacionFase();
                    // Mapear el DTO a la entidad del modelo (TransicionCalificacionFase)


                    // Establecer propiedades adicionales
                    TransicionCalificacionFase.IdFaseOportunidadOrigen = transicionCalificacionFaseCreateDTO.IdFaseOportunidadOrigen;
                    TransicionCalificacionFase.IdFaseOportunidadDestino = transicionCalificacionFaseCreateDTO.IdFaseOportunidadDestino;
                    TransicionCalificacionFase.Estado = true;
                    TransicionCalificacionFase.UsuarioCreacion = usuario;
                    TransicionCalificacionFase.UsuarioModificacion = usuario;
                    TransicionCalificacionFase.FechaCreacion = DateTime.Now;
                    TransicionCalificacionFase.FechaModificacion = DateTime.Now;

                    // Insertar la entidad en la base de datos usando el método Add que acepta una entidad única
                    //var entidadInsertada = _transicionCalificacionFaseRepository.Add(TransicionCalificacionFase);
                    //var retorno = _mapper.Map<TransicionCalificacionFaseCreateDTO>(TransicionCalificacionFase);
                    //var retorno = _mapper.Map<TransicionCalificacionFaseCreateDTO>(_transicionCalificacionFaseRepository.Add(TransicionCalificacionFase));
                    var retorno = _unitOfWork.TransicionFaseRepository.Add(TransicionCalificacionFase);
                    _unitOfWork.Commit();
                    scope.Complete();

                    // Mapear la entidad insertada de vuelta al DTO

                    return _mapper.Map<TransicionCalificacionFaseCreateDTO>(retorno);
                    /*var retorno = _mapper.Map<CriterioEvaluacionDTO>(_unitOfWork.CriterioEvaluacionRepository.Add(criterioEvaluacion));
                    _unitOfWork.Commit();
                    scope.Complete();
                    return (retorno);*/
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar transición de fase: {ex.Message}");
            }
        }

        public TransicionCalificacionFase ActualizarTransicionCalificacionFase(TransicionCalificacionFase entidad)
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

        public async Task<Dictionary<string, object>> ObtenerFasesOportunidad()
        {
            try
            {
                // Implementar la lógica para obtener los combos necesarios para el módulo
                // Por ejemplo: fases origen, fases destino, criticidades, etc.
                var combos = new Dictionary<string, object>();

                // Ejemplo:
                var fasesRepositorio = _unitOfWork.FaseOportunidadRepository;
                var fasesOportunidad = fasesRepositorio.GetAll().Where(x => x.Estado).Select(x => new ComboDTO { Id = x.Id, Nombre = x.Nombre }).ToList();

                combos.Add("fasesOportunidad", fasesOportunidad);

                // Aquí se pueden agregar más combos según se necesite

                return combos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TransicionCalificacionFaseDTO> ObtenerCombo()
        {
            try
            {
                return TransicionCalificacionFaseRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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