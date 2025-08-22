using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: GastoFinancieroCronogramaService
    /// Autor: Griselberto Huaman.
    /// Fecha: 17/12/2022
    /// <summary>
    /// Gestión general de T_GastoFinancieroCronograma
    /// </summary>
    public class GastoFinancieroCronogramaService : IGastoFinancieroCronogramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public GastoFinancieroCronogramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGastoFinancieroCronograma, GastoFinancieroCronograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<GastoFinancieroCronograma, CronogramaEnvioDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public GastoFinancieroCronograma Add(CronogramaEnvioDTO data)
        {
            try
            {
                var entidad = _mapper.Map<GastoFinancieroCronograma>(data);
                entidad.Id = 0;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.FechaInicio = data.FechaInicioNueva;
                entidad.Estado = true;
                var modelo = _unitOfWork.GastoFinancieroCronogramaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GastoFinancieroCronograma>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GastoFinancieroCronograma Update(CronogramaEnvioDTO data)
        {
            try
            {
                var rep = _unitOfWork.GastoFinancieroCronogramaRepository;
                var anterior = _mapper.Map<GastoFinancieroCronograma>(rep.FirstById(data.Id));
                var entidad = _mapper.Map<GastoFinancieroCronograma>(data);
                entidad.FechaCreacion = anterior.FechaCreacion;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = anterior.UsuarioCreacion;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.FechaInicio = data.FechaInicioNueva;
                entidad.Estado = anterior.Estado;
                var modelo = _unitOfWork.GastoFinancieroCronogramaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GastoFinancieroCronograma>(modelo);
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
                _unitOfWork.GastoFinancieroCronogramaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GastoFinancieroCronograma> Add(List<GastoFinancieroCronograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GastoFinancieroCronogramaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GastoFinancieroCronograma>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GastoFinancieroCronograma> Update(List<GastoFinancieroCronograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GastoFinancieroCronogramaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GastoFinancieroCronograma>>(modelo);
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
                _unitOfWork.GastoFinancieroCronogramaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_GastoFinancieroCronograma.
        /// </summary>
        /// <returns> IEnumerable<GastoFinancieroCronogramaDatosDTO> </returns>
        public IEnumerable<GastoFinancieroCronogramaDatosDTO> ObtenerGastoFinancieroCronograma()
        {
            try
            {
                return _unitOfWork.GastoFinancieroCronogramaRepository.ObtenerGastoFinancieroCronograma();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina el cronograma y el detalla.
        /// </summary>
        /// <returns> IEnumerable<GastoFinancieroCronogramaDatosDTO> </returns>
        public bool EliminarCrogramayDetalle(int IdCronograma, string usuario)
        {
            try
            {

                var gastoFinancieroCronogramaRepositorio = _unitOfWork.GastoFinancieroCronogramaRepository;
                var gastoFinancieroCronogramaDetalleRepositorio = _unitOfWork.GastoFinancieroCronogramaDetalleRepository;
                var serviceCronogramaDetalle = new GastoFinancieroCronogramaDetalleService(_unitOfWork);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (gastoFinancieroCronogramaRepositorio.Exist(IdCronograma))
                    {
                        this.Delete(IdCronograma, usuario);
                        var detalleCronograma = gastoFinancieroCronogramaDetalleRepositorio.GetBy(x => x.IdGastoFinancieroCronograma == IdCronograma);
                        foreach (var detalle in detalleCronograma)
                        {
                            serviceCronogramaDetalle.Delete(detalle.Id, usuario);
                        }
                    }

                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// inserta un nuevo cronograma y el detalla.
        /// </summary>
        /// <returns> bool</returns>
        public bool InsertarCronogramaYDetalle(CronogramaYDetalleDTO Json)
        {
            try
            {
                var serviceCronogramaDetalle = new GastoFinancieroCronogramaDetalleService(_unitOfWork);

                using (TransactionScope scope = new TransactionScope())
                {
                    var respuestaCrongrama = this.Add(Json.Cronograma);

                    foreach (var item in Json.Detalle)
                    {
                        item.IdGastoFinancieroCronograma = respuestaCrongrama.Id;
                        item.Usuario = Json.Cronograma.Usuario;

                        var detalle = serviceCronogramaDetalle.Add(item);
                    }
                    scope.Complete();
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualizar un nuevo cronograma y el detalla.
        /// </summary>
        /// <returns> bool</returns>
        public bool ActualizarCronogramaYDetalle(CronogramaYDetalleDTO Json)
        {
            try
            {
                var serviceCronogramaDetalle = new GastoFinancieroCronogramaDetalleService(_unitOfWork);
                var gastoFinancieroCronogramaDetalleRepositorio = _unitOfWork.GastoFinancieroCronogramaDetalleRepository;

                using (TransactionScope scope = new TransactionScope())
                {
                    var respuestaCrongrama = this.Update(Json.Cronograma);
                    var detalleAntiguo = gastoFinancieroCronogramaDetalleRepositorio.GetBy(x => x.IdGastoFinancieroCronograma == respuestaCrongrama.Id);
                    foreach (var IdDetalle in Json.DetalleEliminado)
                    {
                        serviceCronogramaDetalle.Delete(IdDetalle, Json.Cronograma.Usuario);
                    }
                    foreach (var item in Json.Detalle)
                    {
                        if(item.Id==0 && item.IdGastoFinancieroCronograma==0)
                        {
                            item.IdGastoFinancieroCronograma = respuestaCrongrama.Id;
                            item.Usuario = Json.Cronograma.Usuario;
                            var detalle = serviceCronogramaDetalle.Add(item);
                        }
                        else
                        {
                            item.Usuario = Json.Cronograma.Usuario;
                            var detalle = serviceCronogramaDetalle.Update(item);
                        }
                    }
                    scope.Complete();
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: margiory Ramirez.
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Generera  filtro para Reporte Detallado
        /// </summary>
        /// <returns> bool</returns>


        public object GenerarReportePrestamos ( FiltroReportePrestamoDTO Filtro)
        {
           
            try
            {
                var _repoGastoFinancieroCronograma = _unitOfWork.GastoFinancieroCronogramaRepository;

                var Lista = _repoGastoFinancieroCronograma.ObtenerReportePrestamos(Filtro.IdEntidadFinanciera, Filtro.IdGastoFinancieroCronograma);

                return (Lista);

            }
            catch (Exception ex)
            {
                return (ex);
            }
        }
        /// Autor: margiory Ramirez.
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        ///Obtiene Lista de Entidades Financieras Con Prestamo
        /// </summary>
        /// <returns> bool</returns>

        public object ObtenerListaEntidadesFinancierasConPrestamo()
        {
            try
            {
                var _repoEntidadFinanciera = _unitOfWork.GastoFinancieroCronogramaRepository;
                return (_repoEntidadFinanciera.ObtenerListaEntidadFinancieraPrestamo());
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }

        /// Autor: margiory Ramirez.
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        ///Obtiene Lista de Prestamos
        /// </summary>
        /// <returns> bool</returns>

        public Object ObtenerListaPrestamos()
        {
            try
            {
                var _repoGastoFinancieroCronograma =_unitOfWork.GastoFinancieroCronogramaRepository;
                return (_repoGastoFinancieroCronograma.ObtenerListaPrestamosFiltro());
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }



    }
}

