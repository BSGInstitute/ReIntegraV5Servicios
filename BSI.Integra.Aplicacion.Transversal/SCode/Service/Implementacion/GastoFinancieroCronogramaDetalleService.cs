using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: GastoFinancieroCronogramaDetalleService
    /// Autor: Griselberto Huaman.
    /// Fecha: 17/12/2022
    /// <summary>
    /// Gestión general de T_GastoFinancieroCronogramaDetalle
    /// </summary>
    public class GastoFinancieroCronogramaDetalleService : IGastoFinancieroCronogramaDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public GastoFinancieroCronogramaDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGastoFinancieroCronogramaDetalle, GastoFinancieroCronogramaDetalle>(MemberList.None).ReverseMap();
               cfg.CreateMap<GastoFinancieroCronogramaDetalle, CronogramaDetalleEnvioDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public GastoFinancieroCronogramaDetalle Add(CronogramaDetalleEnvioDTO data)
        {
            try
            {
                var entidad = _mapper.Map<GastoFinancieroCronogramaDetalle>(data);
                entidad.Id = 0;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;
                var modelo = _unitOfWork.GastoFinancieroCronogramaDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GastoFinancieroCronogramaDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GastoFinancieroCronogramaDetalle Update(CronogramaDetalleEnvioDTO data)
        {
            try
            {
                var rep = _unitOfWork.GastoFinancieroCronogramaDetalleRepository;
                var anterior = _mapper.Map<GastoFinancieroCronogramaDetalle>(rep.FirstById(data.Id));
                var entidad = _mapper.Map<GastoFinancieroCronogramaDetalle>(data);
                entidad.FechaCreacion = anterior.FechaCreacion;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = anterior.UsuarioCreacion;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = anterior.Estado;
                var modelo = _unitOfWork.GastoFinancieroCronogramaDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GastoFinancieroCronogramaDetalle>(modelo);
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
                _unitOfWork.GastoFinancieroCronogramaDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GastoFinancieroCronogramaDetalle> Add(List<GastoFinancieroCronogramaDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GastoFinancieroCronogramaDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GastoFinancieroCronogramaDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GastoFinancieroCronogramaDetalle> Update(List<GastoFinancieroCronogramaDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GastoFinancieroCronogramaDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GastoFinancieroCronogramaDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.GastoFinancieroCronogramaDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los CronogramaDetalle (Cutas) dado un id de GastoFinanciero (Cronograma)
        /// para ser mostradas en una grilla
        /// </summary>
        /// <returns>  IEnumerable<GastoFinancieroCronogramaDetalleDTO></returns>
        public IEnumerable<GastoFinancieroCronogramaDetalleDTO> ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(int IdCronograma)
        {
            try
            {
                return _unitOfWork.GastoFinancieroCronogramaDetalleRepository.ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(IdCronograma);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

