using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SemaforoFinancieroService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_SemaforoFinanciero
    /// </summary>
    public class SemaforoFinancieroService : ISemaforoFinancieroService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SemaforoFinancieroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSemaforoFinanciero, SemaforoFinanciero>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SemaforoFinancieroDTO, ComboDTO>(MemberList.None);
                    cfg.CreateMap<TSemaforoFinancieroDetalle, SemaforoFinancieroDetalle>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SemaforoFinanciero Add(SemaforoFinanciero entidad)
        {
            try
            {
                TSemaforoFinanciero modelo;
                List<SemaforoFinancieroDetalle> semaforoFinancieroDetalle = new List<SemaforoFinancieroDetalle>();
                using (TransactionScope scope = new TransactionScope())
                {
                    modelo = _unitOfWork.SemaforoFinancieroRepository.Add(entidad);
                    _unitOfWork.Commit();
                    if (entidad.Detalle != null && entidad.Detalle.Count > 0)
                    {
                        entidad.Detalle.ForEach(d => d.IdSemaforoFinanciero = modelo.Id);
                        var semaforoFinancieroDetalleService = new SemaforoFinancieroDetalleService(_unitOfWork);
                        semaforoFinancieroDetalle = semaforoFinancieroDetalleService.Add(entidad.Detalle);
                    }
                    scope.Complete();
                }
                SemaforoFinanciero semaforoFinanciero = _mapper.Map<SemaforoFinanciero>(modelo);
                semaforoFinanciero.Detalle = semaforoFinancieroDetalle;
                return semaforoFinanciero;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SemaforoFinanciero Update(SemaforoFinanciero entidad)
        {
            try
            {
                TSemaforoFinanciero modelo;
                SemaforoFinancieroDetalleService? semaforoFinancieroDetalleService = null;
                List<SemaforoFinancieroDetalle>? insertar = null, actualizar = null;
                List<int>? eliminar = null;

                if (entidad.Detalle != null)
                {
                    semaforoFinancieroDetalleService = new SemaforoFinancieroDetalleService(_unitOfWork);
                    var idsDetalleDB = semaforoFinancieroDetalleService
                        .ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(entidad.Id).Select(d => d.Id);

                    eliminar = idsDetalleDB.Except(entidad.Detalle.Select(d => d.Id)).ToList();

                    var segregar = entidad.Detalle.GroupBy(detalle => idsDetalleDB.Any(id => id == detalle.Id));
                    actualizar = segregar.FirstOrDefault(d => d.Key)?.ToList();
                    insertar = segregar.FirstOrDefault(d => !d.Key)?.ToList();
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    modelo = _unitOfWork.SemaforoFinancieroRepository.Update(entidad);
                    _unitOfWork.Commit();
                    if (eliminar != null) semaforoFinancieroDetalleService?.Delete(eliminar, entidad.UsuarioModificacion);

                    if (actualizar != null)
                    {
                        actualizar.ForEach(d => d.IdSemaforoFinanciero = modelo.Id);
                        actualizar = semaforoFinancieroDetalleService?.Update(actualizar);
                    }
                    if (insertar != null)
                    {
                        insertar.ForEach(d => d.IdSemaforoFinanciero = modelo.Id);
                        insertar = semaforoFinancieroDetalleService?.Add(insertar);
                    }
                    scope.Complete();
                }
                SemaforoFinanciero semaforoFinanciero = _mapper.Map<SemaforoFinanciero>(modelo);
                semaforoFinanciero.Detalle = _mapper.Map<List<SemaforoFinancieroDetalle>>(insertar);
                semaforoFinanciero.Detalle.AddRange(_mapper.Map<List<SemaforoFinancieroDetalle>>(actualizar));
                return semaforoFinanciero;
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
                using (TransactionScope scope = new TransactionScope())
                {
                    _unitOfWork.SemaforoFinancieroRepository.Delete(id, usuario);
                    _unitOfWork.Commit();

                    var semaforoFinancieroDetalleService = new SemaforoFinancieroDetalleService(_unitOfWork);
                    var idsDetalleDB = semaforoFinancieroDetalleService
                        .ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(id).Select(d => d.Id).ToList();
                    semaforoFinancieroDetalleService.Delete(idsDetalleDB, usuario);
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SemaforoFinanciero> Add(List<SemaforoFinanciero> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SemaforoFinanciero>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SemaforoFinanciero> Update(List<SemaforoFinanciero> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SemaforoFinanciero>>(modelo);
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
                _unitOfWork.SemaforoFinancieroRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SemaforoFinanciero
        /// </summary>
        /// <returns> List<SemaforoFinancieroDTO> </returns>
        public IEnumerable<SemaforoFinancieroDTO> ObtenerSemaforoFinanciero()
        {
            try
            {
                return _unitOfWork.SemaforoFinancieroRepository.ObtenerSemaforoFinanciero();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SemaforoFinanciero para mostrarse en combo.
        /// </summary>
        /// <returns> List<SemaforoFinancieroComboDTO> </returns>
        public IEnumerable<SemaforoFinancieroComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SemaforoFinancieroRepository.ObtenerCombo().OrderBy(combo => combo.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>InsertarNuevoSemaforo
        /// Inserta nuevo registro a la tabla T_SemaforoFinanciero .
        /// </summary>
        public bool InsertarNuevoSemaforo(SemaforoFinanciero objeto)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroRepository.InsertarNuevoSemaforo(objeto);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="Semaforo">Entidad a modificar</param>
        /// <returns>SemaforoFinanciero</returns>
        public SemaforoFinanciero ActualizarSemaforoFinanciero(SemaforoFinancieroInsertarNuevoDTO Semaforo)
        {
            try
            {
                SemaforoFinanciero semaforoNuevo = new SemaforoFinanciero();

                var servicioSemaforo = new SemaforoFinancieroService(_unitOfWork);
                var servicioSemaforoDetalle = new SemaforoFinancieroDetalleService(_unitOfWork);

                semaforoNuevo = servicioSemaforo.ObtenerSemaforoPorId(Semaforo.Id);
                semaforoNuevo.IdPais = Semaforo.IdPais;
                semaforoNuevo.Activo = Semaforo.Activo;
                semaforoNuevo.UsuarioModificacion = Semaforo.Usuario;
                semaforoNuevo.FechaModificacion = DateTime.Now;
                _unitOfWork.SemaforoFinancieroRepository.Update(semaforoNuevo);
                _unitOfWork.Commit();
                if (Semaforo.Detalle != null)
                {
                    //Borramos los que no estan
                    var detalleBD = servicioSemaforoDetalle.ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(semaforoNuevo.Id);
                    foreach (var item in detalleBD)
                    {
                        if (!Semaforo.Detalle.Any(x => x.Id == item.Id))
                        {
                            servicioSemaforoDetalle.Delete(item.Id, semaforoNuevo.UsuarioModificacion);
                        }
                    }

                    foreach (var item in Semaforo.Detalle)
                    {
                        SemaforoFinancieroDetalle semaforoDetalleNuevo = new SemaforoFinancieroDetalle();
                        if (item.Id > 0)
                        {

                            semaforoDetalleNuevo = servicioSemaforoDetalle.ObtenerSemaforoFinancieroDetallePorId(item.Id);
                            semaforoDetalleNuevo.IdSemaforoFinanciero = semaforoNuevo.Id;
                            semaforoDetalleNuevo.Nombre = item.Nombre;
                            semaforoDetalleNuevo.Mensaje = item.Mensaje;
                            semaforoDetalleNuevo.Color = item.Color;
                            semaforoDetalleNuevo.UsuarioModificacion = Semaforo.Usuario;
                            semaforoDetalleNuevo.FechaModificacion = DateTime.Now;

                            servicioSemaforoDetalle.Update(semaforoDetalleNuevo);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            semaforoDetalleNuevo.IdSemaforoFinanciero = semaforoNuevo.Id;
                            semaforoDetalleNuevo.Nombre = item.Nombre;
                            semaforoDetalleNuevo.Mensaje = item.Mensaje;
                            semaforoDetalleNuevo.Color = item.Color;
                            semaforoDetalleNuevo.Estado = true;
                            semaforoDetalleNuevo.UsuarioCreacion = Semaforo.Usuario;
                            semaforoDetalleNuevo.UsuarioModificacion = Semaforo.Usuario;
                            semaforoDetalleNuevo.FechaCreacion = DateTime.Now;
                            semaforoDetalleNuevo.FechaModificacion = DateTime.Now;
                            servicioSemaforoDetalle.InsertarNuevoSemaforoDetalle(semaforoDetalleNuevo);
                        }
                    }
                }
                return semaforoNuevo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el semaforo por el id
        /// </summary>
        /// <param name="id"> id de la entidad</param>
        /// <returns>SemaforoFinanciero</returns>
        public SemaforoFinanciero ObtenerSemaforoPorId(int id)
        {
            try
            {
                return _unitOfWork.SemaforoFinancieroRepository.ObtenerSemaforoPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
