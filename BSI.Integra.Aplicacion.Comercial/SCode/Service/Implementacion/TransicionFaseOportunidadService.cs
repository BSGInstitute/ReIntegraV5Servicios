using AutoMapper;
using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: TransicionCalificacionFaseService
    /// Autor: Jose Vega.
    /// Fecha: 15/09/2025
    /// <summary>
    /// Servicio para la gestión de TransicionFaseOportunidad
    /// </summary>
    public class TransicionFaseOportunidadService : ITransicionFaseOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public TransicionFaseOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTransicionFaseOportunidad, TransicionFaseOportunidad>().ReverseMap();
                cfg.CreateMap<TCriterioCalificacionFaseOportunidad, TransicionFaseCriterioOportunidad>().ReverseMap();
                cfg.CreateMap<TransicionFaseOportunidadDTO, TransicionFaseOportunidad>().ReverseMap();
                cfg.CreateMap<TransicionFaseCriterioOportunidadDTO, TransicionFaseCriterioOportunidad>().ReverseMap();
            });
            _mapper = new Mapper(mapperConfig);
        }

        #region
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
                var entidad = _unitOfWork.TransicionFaseOportunidadRepository.ObtenerPorId(id);
                if (entidad == null)
                    throw new Exception("La transición a eliminar no existe.");

                var criterios = entidad.TransicionFaseCriterioOportunidad?.ToList() ?? new List<TransicionFaseCriterioOportunidad>();

                if (criterios.Count == 0)
                {

                    criterios = _unitOfWork
                        .TransicionFaseOportunidadRepository
                        .ObtenerPorIdTransicion(id);
                }

                foreach (var criterio in criterios)
                {
                    _unitOfWork.TransicionFaseCriterioOportunidadRepository.DeleteCriterios(criterio.Id, usuario);
                }

                _unitOfWork.TransicionFaseOportunidadRepository.Delete(id, usuario);

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar en cascada la transición (Id={id}): {ex.Message}", ex);
            }
        }

        private void AsignarValoresComunes(dynamic e)
        {
            e.FechaCreacion = DateTime.Now;
            e.FechaModificacion = DateTime.Now;
            e.Estado = true;
        }
        public async Task InsertTransicionAsync(TransicionFaseOportunidadDTO transicionFaseOportunidadDTO)
        {
            try
            {
                if (transicionFaseOportunidadDTO.TransicionFaseCriterioOportunidad == null)
                    throw new ArgumentNullException("La propiedad TransicionFaseCriterioOportunidad es nula.");

                // Mapear el padre
                var transicionEntity = _mapper.Map<TransicionFaseOportunidad>(transicionFaseOportunidadDTO);
                transicionEntity.UsuarioCreacion = transicionFaseOportunidadDTO.Usuario;
                transicionEntity.UsuarioModificacion = transicionFaseOportunidadDTO.Usuario;
                AsignarValoresComunes(transicionEntity);

                // Mapear la lista de hijos
                if (transicionFaseOportunidadDTO.TransicionFaseCriterioOportunidad != null)
                {
                    transicionEntity.TransicionFaseCriterioOportunidad = new List<TransicionFaseCriterioOportunidad>();
                    var criterioEntities = _mapper.Map<List<TransicionFaseCriterioOportunidad>>(transicionFaseOportunidadDTO.TransicionFaseCriterioOportunidad);
                    foreach (var criterio in criterioEntities)
                    {
                        criterio.IdTransicionFaseOportunidad = transicionEntity.Id;
                        criterio.UsuarioCreacion = transicionFaseOportunidadDTO.Usuario;
                        criterio.UsuarioModificacion = transicionFaseOportunidadDTO.Usuario;
                        AsignarValoresComunes(criterio);
                        transicionEntity.TransicionFaseCriterioOportunidad.Add(criterio);

                    }
                }

                _unitOfWork.TransicionFaseOportunidadRepository.Add(transicionEntity);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateTransicionAsync(TransicionFaseOportunidadDTO dto)
        {
            try
            {
                // 1. Obtener la entidad actual desde DB asegurando que se carga con sus relaciones
                var entidadActual = _unitOfWork.TransicionFaseOportunidadRepository.ObtenerPorId(dto.Id.Value);
                if (entidadActual == null)
                    throw new Exception("Transición no encontrada.");

                // Inicializa colección de hijos si es null
                if (entidadActual.TransicionFaseCriterioOportunidad == null)
                    entidadActual.TransicionFaseCriterioOportunidad = new List<TransicionFaseCriterioOportunidad>();

                // Actualizar campos del padre
                entidadActual.IdFaseOportunidadOrigen = dto.IdFaseOportunidadOrigen;
                entidadActual.IdFaseOportunidadDestino = dto.IdFaseOportunidadDestino;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.UsuarioModificacion = dto.Usuario;

                // Sincronizar hijos - primero obtener los IDs existentes
                var criteriosExistentes = entidadActual.TransicionFaseCriterioOportunidad.ToList();
                var idsDto = dto.TransicionFaseCriterioOportunidad?.Select(x => x.Id).Where(id => id > 0).ToList() ?? new List<int>();

                // Eliminar hijos que ya no están en DTO
                var criteriosParaEliminar = criteriosExistentes.Where(x => x.Id > 0 && !idsDto.Contains(x.Id)).ToList();
                foreach (var criterioEliminar in criteriosParaEliminar)
                {
                    entidadActual.TransicionFaseCriterioOportunidad.Remove(criterioEliminar);
                    _unitOfWork.TransicionFaseCriterioOportunidadRepository.DeleteCriterios(criterioEliminar.Id, "Prueba");
                }

                // Agregar/Actualizar hijos
                if (dto.TransicionFaseCriterioOportunidad != null)
                {
                    foreach (var itemDto in dto.TransicionFaseCriterioOportunidad)
                    {
                        if (itemDto.Id > 0)
                        {
                            var criterio = _unitOfWork.TransicionFaseCriterioOportunidadRepository.ObtenerPorId(itemDto.Id);
                            if (criterio != null)
                            {
                                criterio.IdCriterioCalificacionFaseOportunidad = itemDto.IdCriterioCalificacionFaseOportunidad;
                                if (itemDto.Estado.HasValue)
                                criterio.Estado = itemDto.Estado.Value;
                                criterio.FechaModificacion = DateTime.Now;
                                criterio.UsuarioModificacion = dto.Usuario;

                                _unitOfWork.TransicionFaseCriterioOportunidadRepository.Update(criterio);
                            }
                        }
                        else
                        {
                            var nuevoHijo = new TransicionFaseCriterioOportunidad
                            {
                                IdTransicionFaseOportunidad = entidadActual.Id,
                                IdCriterioCalificacionFaseOportunidad = itemDto.IdCriterioCalificacionFaseOportunidad,
                                Estado = itemDto.Estado ?? true,
                                UsuarioModificacion = dto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };

                            _unitOfWork.TransicionFaseCriterioOportunidadRepository.Add(nuevoHijo);
                        }
                    }
                }

                _unitOfWork.TransicionFaseOportunidadRepository.Update(entidadActual);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en UpdateTransicionAsync: {ex.Message}", ex);
            }
        }
        #endregion

        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> List<TransicionFaseOportunidadDTO> </returns>
        public List<TransicionFaseOportunidadPlanoDto> Obtener()
        {
            try
            {
                return _unitOfWork.TransicionFaseOportunidadRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_TransicionFaseOportunidad por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> TransicionFaseOportunidad </returns>
        public TransicionFaseOportunidad ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.TransicionFaseOportunidadRepository.ObtenerPorId(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}