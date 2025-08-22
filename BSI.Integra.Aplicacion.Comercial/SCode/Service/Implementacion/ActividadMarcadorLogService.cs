using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: ActividadMarcadorLogService
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/09/2022
    /// <summary>
    /// Gestión general de la tabla T_ActividadMarcadorLog
    /// </summary>
    public class ActividadMarcadorLogService : IActividadMarcadorLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ActividadMarcadorLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TActividadMarcadorLog, ActividadMarcadorLog>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TActividadMarcadorLog, ActividadMarcadorLogDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ActividadMarcadorLog, ActividadMarcadorLogDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ActividadMarcadorLog por idActividadDetalle e idOportunidad
        /// </summary>
        /// <param name="idActividadDetalle"> Nombre de usuario </param>
        /// <param name="idOportunidad"> Nombre de usuario </param>
        /// <returns> ActividadMarcadorLogDTO </returns>
        public ActividadMarcadorLogDTO ObtenerPorIdActividadDetalleIdOportunidad(int idActividadDetalle, int idOportunidad)
        {
            try
            {
                var resultado = _unitOfWork.ActividadMarcadorLogRepository.ObtenerPorIdActividadDetalleIdOportunidad(idActividadDetalle, idOportunidad);
                if (resultado != null)
                {
                    return _mapper.Map<ActividadMarcadorLogDTO>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Guarda la actividad marcador log por id actividad detalle idoportunidad
        /// </summary>
        /// <param name="jsonDTO"> </param>
        /// <param name="usuario"> Nombre de usuario </param>
        /// <returns> ActividadMarcadorLogDTO </returns>
        public ActividadMarcadorLogDTO GuardarActividadMarcadorLog(ActividadMarcadorLogDTO jsonDTO, string usuario)
        {
            try
            {
                var actividadMarcadorLog = _unitOfWork.ActividadMarcadorLogRepository.ObtenerPorIdActividadDetalleIdOportunidad(jsonDTO.IdActividadDetalle, jsonDTO.IdOportunidad);
                if (actividadMarcadorLog != null && actividadMarcadorLog.Id != 0)
                {
                    actividadMarcadorLog.IdOportunidad = jsonDTO.IdOportunidad;
                    actividadMarcadorLog.IdActividadDetalle = jsonDTO.IdActividadDetalle;
                    actividadMarcadorLog.FechaProgramada = jsonDTO.FechaProgramada;
                    actividadMarcadorLog.TotalIntento = jsonDTO.TotalIntento ?? actividadMarcadorLog.TotalIntento;
                    actividadMarcadorLog.Contestado = jsonDTO.Contestado ?? actividadMarcadorLog.Contestado;
                    actividadMarcadorLog.NoContestado = jsonDTO.NoContestado ?? actividadMarcadorLog.NoContestado;
                    actividadMarcadorLog.IdAgendaTab = jsonDTO.IdAgendaTab ?? actividadMarcadorLog.IdAgendaTab;
                    actividadMarcadorLog.FechaModificacion = DateTime.Now;
                    actividadMarcadorLog.UsuarioModificacion = usuario;
                    if (actividadMarcadorLog.TotalIntento < actividadMarcadorLog.NoContestado + actividadMarcadorLog.Contestado)
                    {
                        actividadMarcadorLog.TotalIntento = actividadMarcadorLog.NoContestado + actividadMarcadorLog.Contestado;
                    }
                    var resultado = _unitOfWork.ActividadMarcadorLogRepository.Update(actividadMarcadorLog);
                    _unitOfWork.Commit();
                    return _mapper.Map<ActividadMarcadorLogDTO>(resultado);
                }
                else
                {
                    actividadMarcadorLog = new()
                    {
                        IdOportunidad = jsonDTO.IdOportunidad,
                        IdActividadDetalle = jsonDTO.IdActividadDetalle,
                        FechaProgramada = jsonDTO.FechaProgramada,
                        TotalIntento = jsonDTO.TotalIntento ?? 0,
                        Contestado = jsonDTO.Contestado ?? 0,
                        NoContestado = jsonDTO.NoContestado ?? 0,
                        IdAgendaTab = jsonDTO.IdAgendaTab ?? 0,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var resultado = _unitOfWork.ActividadMarcadorLogRepository.Add(actividadMarcadorLog);
                    _unitOfWork.Commit();
                    return _mapper.Map<ActividadMarcadorLogDTO>(resultado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
