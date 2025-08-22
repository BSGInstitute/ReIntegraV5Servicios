using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: LogProyeccionFurService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_LogProyeccionFur
    /// </summary>
    public class LogProyeccionFurService : ILogProyeccionFurService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public LogProyeccionFurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLogProyeccionFur, LogProyeccionFur>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public LogProyeccionFur Add(LogProyectadosDTO data,string Usuario)
        {
            try
            {
                var serializerProceso = new JavaScriptSerializer();
                LogProyeccionFur entidad = new LogProyeccionFur();
                entidad.Id = 0;

                entidad.TipoProyectado = data.TipoProyectado;
                entidad.FechaSemilla = data.FechaSemilla;
                entidad.FechaFinProyeccion = data.FechaFinProyeccion;
                entidad.FechaInicioProyeccion = data.FechaInicioProyeccion;
                entidad.ResultadoProceso = serializerProceso.Serialize(
                    new { 
                        TotalProcesado = data.TotalProcesado, 
                        TotalProyectados = data.TotalProyectados, 
                        TotalErrores = data.TotalErrores 
                    });
                entidad.DetalleDeError = data.ConfiguracionesConError;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.LogProyeccionFurRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LogProyeccionFur>(modelo);
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
                _unitOfWork.LogProyeccionFurRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LogProyeccionFur> Add(List<LogProyeccionFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LogProyeccionFurRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LogProyeccionFur>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LogProyeccionFur> Update(List<LogProyeccionFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LogProyeccionFurRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LogProyeccionFur>>(modelo);
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
                _unitOfWork.LogProyeccionFurRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

}
