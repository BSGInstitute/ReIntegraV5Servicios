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

                var transicionesExistentes = _unitOfWork.TransicionFaseOportunidadRepository.Obtener()
                    .Where(t => t.IdFaseOportunidadOrigen == transicionFaseOportunidadDTO.IdFaseOportunidadOrigen
                        && t.IdFaseOportunidadDestino == transicionFaseOportunidadDTO.IdFaseOportunidadDestino)
                    .ToList();

                if (transicionesExistentes.Any())
                {
                    throw new InvalidOperationException(
                        $"Ya existe una transición con origen {transicionFaseOportunidadDTO.IdFaseOportunidadOrigen} y " +
                        $"destino {transicionFaseOportunidadDTO.IdFaseOportunidadDestino}. Use la función de actualización en su lugar.");
                }

                var transicionEntity = _mapper.Map<TransicionFaseOportunidad>(transicionFaseOportunidadDTO);
                transicionEntity.UsuarioCreacion = transicionFaseOportunidadDTO.Usuario;
                transicionEntity.UsuarioModificacion = transicionFaseOportunidadDTO.Usuario;
                AsignarValoresComunes(transicionEntity);

                if (transicionFaseOportunidadDTO.TransicionFaseCriterioOportunidad != null)
                {
                    var criteriosSinDuplicados = transicionFaseOportunidadDTO.TransicionFaseCriterioOportunidad
                        .GroupBy(c => c.IdCriterioCalificacionFaseOportunidad)
                        .Select(g => g.First())
                        .ToList();

                    transicionEntity.TransicionFaseCriterioOportunidad = new List<TransicionFaseCriterioOportunidad>();
                    var criterioEntities = _mapper.Map<List<TransicionFaseCriterioOportunidad>>(criteriosSinDuplicados);

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
                if (ex is InvalidOperationException)
                    throw;

                throw new Exception($"Error al insertar la transición: {ex.Message}", ex);
            }
        }

        public async Task UpdateTransicionAsync(TransicionFaseOportunidadDTO dto)
        {
            try
            {
                var entidadActual = _unitOfWork.TransicionFaseOportunidadRepository.ObtenerPorId(dto.Id.Value);
                if (entidadActual == null)
                    throw new Exception("Transición no encontrada.");

                bool hayCambiosEnPadre =
                    entidadActual.IdFaseOportunidadOrigen != dto.IdFaseOportunidadOrigen ||
                    entidadActual.IdFaseOportunidadDestino != dto.IdFaseOportunidadDestino;

                if (hayCambiosEnPadre)
                {
                    var transicionesExistentes = _unitOfWork.TransicionFaseOportunidadRepository.Obtener()
                        .Where(t => t.IdFaseOportunidadOrigen == dto.IdFaseOportunidadOrigen
                            && t.IdFaseOportunidadDestino == dto.IdFaseOportunidadDestino
                            && t.Id != entidadActual.Id)
                        .ToList();

                    if (transicionesExistentes.Any())
                    {
                        throw new InvalidOperationException(
                            $"Ya existe una transición con origen {dto.IdFaseOportunidadOrigen} y destino {dto.IdFaseOportunidadDestino}. En su lugar, actualice la transición existente.");
                    }

                    entidadActual.IdFaseOportunidadOrigen = dto.IdFaseOportunidadOrigen;
                    entidadActual.IdFaseOportunidadDestino = dto.IdFaseOportunidadDestino;
                    entidadActual.FechaModificacion = DateTime.Now;
                    entidadActual.UsuarioModificacion = dto.Usuario;
                    _unitOfWork.TransicionFaseOportunidadRepository.Update(entidadActual);
                }

                var hijosDTO = dto.TransicionFaseCriterioOportunidad ?? new List<TransicionFaseCriterioOportunidadDTO>();
                if (hijosDTO.Count == 0)
                {
                    await _unitOfWork.CommitAsync();
                    return;
                }

                var hijosBD = _unitOfWork.TransicionFaseOportunidadRepository
                    .ObtenerPorIdTransicion(entidadActual.Id);

                var hijosDTOAgrupados = hijosDTO
                    .GroupBy(x => x.IdCriterioCalificacionFaseOportunidad)
                    .Select(g => g.First())
                    .ToList();

                foreach (var itemDto in hijosDTOAgrupados)
                {
                    var hijoExistente = hijosBD.FirstOrDefault(x =>
                        x.IdCriterioCalificacionFaseOportunidad == itemDto.IdCriterioCalificacionFaseOportunidad);

                    if (itemDto.Estado.HasValue && itemDto.Estado.Value == false)
                    {
                        if (hijoExistente != null)
                        {
                            _unitOfWork.TransicionFaseCriterioOportunidadRepository.DeleteCriterios(hijoExistente.Id, dto.Usuario);
                        }
                        continue;
                    }

                    if (hijoExistente != null)
                    {
                        if (hijoExistente.Estado != true)
                        {
                            hijoExistente.Estado = true;
                            hijoExistente.FechaModificacion = DateTime.Now;
                            hijoExistente.UsuarioModificacion = dto.Usuario;
                            _unitOfWork.TransicionFaseCriterioOportunidadRepository.Update(hijoExistente);
                        }
                    }
                    else
                    {
                        var nuevoHijo = new TransicionFaseCriterioOportunidad
                        {
                            IdTransicionFaseOportunidad = entidadActual.Id,
                            IdCriterioCalificacionFaseOportunidad = itemDto.IdCriterioCalificacionFaseOportunidad,
                            Estado = true,
                            UsuarioCreacion = dto.Usuario,
                            UsuarioModificacion = dto.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _unitOfWork.TransicionFaseCriterioOportunidadRepository.Add(nuevoHijo);
                    }
                }

                var criteriosParaEliminar = hijosBD.Where(hijo =>
                    !hijosDTOAgrupados.Any(dto =>
                        dto.IdCriterioCalificacionFaseOportunidad == hijo.IdCriterioCalificacionFaseOportunidad)
                ).ToList();

                foreach (var criterioEliminar in criteriosParaEliminar)
                {
                    _unitOfWork.TransicionFaseCriterioOportunidadRepository.DeleteCriterios(criterioEliminar.Id, dto.Usuario);
                }

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