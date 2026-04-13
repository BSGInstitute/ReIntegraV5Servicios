using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Newtonsoft.Json.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{

    /// Repositorio: GrabacionesClasesOnlineRepository
    /// Autor: Jorge Gamero
    /// Fecha: 14/01/2025
    /// <summary>
    /// </summary>
    public class GrabacionesClasesOnlineRepository : GenericRepository<TSubAreaCapacitacion>, IGrabacionesClasesOnlineRepository
    {
        private Mapper _mapper;

        public GrabacionesClasesOnlineRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            
        }

        /// Autor: Jorge Gamero
        /// Fecha: 14/01/2025
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data para la grilla principal.
        /// </summary>
        /// <returns> List<GrabacionesClasesOnlineDTO> </returns>
        public List<GrabacionesClasesOnlineDTO> GenerarVistaProgramasOnline(GrabacionesClasesOnlineFiltroDTO filtro)
        {
            try
            {
                string IdArea = null, IdSubArea = null, IdPGeneral = null, IdPEspecifico = null, IdPartner = null;
                if (filtro.Area != null && filtro.Area.Count() > 0) IdArea = String.Join(",", filtro.Area);
                if (filtro.SubArea != null && filtro.SubArea.Count() > 0) IdSubArea = String.Join(",", filtro.SubArea);
                if (filtro.PGeneral != null && filtro.PGeneral.Count() > 0) IdPGeneral = String.Join(",", filtro.PGeneral);
                if (filtro.PEspecifico != null && filtro.PEspecifico.Count() > 0) IdPEspecifico = String.Join(",", filtro.PEspecifico);
                if (filtro.Partner != null && filtro.Partner.Count() > 0) IdPartner = String.Join(",", filtro.Partner);

                List<GrabacionesClasesOnlineDTO> reporteProgramasOnline = new List<GrabacionesClasesOnlineDTO>();
                var query = _dapperRepository.QuerySPDapper("pla.SP_ConfigurarVideoProgramaSincronico", new { IdPGeneral, IdPEspecifico, IdArea, IdSubArea, IdPartner });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteProgramasOnline = JsonConvert.DeserializeObject<List<GrabacionesClasesOnlineDTO>>(query);
                }
                return reporteProgramasOnline;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data para la grilla de modal de sesiones.
        /// </summary>
        /// <returns> List<SesionesClasesOnlineResumenDTO> </returns>
        public List<SesionesClasesOnlineResumenDTO> ObtenerSesiones(SesionesFiltroDTO filtro)
        {
            try
            {
                string IdPEspecifico = null;
                if (filtro.IdPEspecifico != null) IdPEspecifico = String.Join(",", filtro.IdPEspecifico);

                List<SesionesClasesOnlineResumenDTO> reporteSesiones = new List<SesionesClasesOnlineResumenDTO>();
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_ConfigurarVideoProgramaSesionResumen]", new { IdPEspecifico });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteSesiones = JsonConvert.DeserializeObject<List<SesionesClasesOnlineResumenDTO>>(query);
                }
                return reporteSesiones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data para la grilla de modal de detalle de resumen de sesiones
        /// </summary>
        /// <returns> List<SesionesClasesOnlineDetalleResumenDTO> </returns>
        public List<SesionesClasesOnlineDetalleResumenDTO> ObtenerDetalleResumenGrabacionSesion(SesionesFiltroDTO filtro)
        {
            try
            {
                string IdPEspecifico = null;
                if (filtro.IdPEspecifico != null) IdPEspecifico = String.Join(",", filtro.IdPEspecifico);

                List<SesionesClasesOnlineDetalleResumenDTO> reporteSesiones = new List<SesionesClasesOnlineDetalleResumenDTO>();
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_ObtenerDetalleResumenGrabacionSesion]", new { IdPEspecifico });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteSesiones = JsonConvert.DeserializeObject<List<SesionesClasesOnlineDetalleResumenDTO>>(query);
                }
                return reporteSesiones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza sesiones
        /// </summary>
        /// <returns> bool </returns>
        public bool ActualizarSesiones(SesionesClasesOnlineModificarFiltroDTO filtro)
        {
            try
            {
                foreach (var item in filtro.Data)
                {
                    string IdPEspecifico = null, IdPEspecificoSesion = null, NombreSesion = null, IdTipoProveedorVideo = null, Video = null;
                    string Habilitado = "false";
                    DateTime? fechainicio = null;
                    DateTime? fechafin = null;

                    if (item.IdPEspecifico != null) IdPEspecifico = item.IdPEspecifico;
                    if (item.IdPEspecificoSesion != null) IdPEspecificoSesion = item.IdPEspecificoSesion;
                    if (item.NombreSesion != null) NombreSesion = item.NombreSesion;
                    if (item.IdTipoProveedorVideo != null) IdTipoProveedorVideo = item.IdTipoProveedorVideo;
                    if (item.Video != null) Video = item.Video;
                    if (item.Habilitado != null) Habilitado = item.Habilitado;
                    if (item.FechaInicio != null) fechainicio = item.FechaInicio.Value.AddHours(-5);
                    if (item.FechaFin != null) fechafin = item.FechaFin.Value.AddHours(-5);

                    var Estado = 1;
                    var FechaCreacion = DateTime.Now;
                    var FechaModificacion = DateTime.Now;
                    var UsuarioCreacion = "SYSTEM-PRUEBA";
                    var UsuarioModificacion = "SYSTEM-PRUEBA";

                    var _consultaDataTabla = _dapperRepository.QueryDapper("Select * from pla.T_ConfigurarVideoSesionProgramaSincronico where IdPEspecificoSesion = @IdPEspecificoSesion ", new { IdPEspecificoSesion });
                    var resultadoDataTabla = JArray.Parse(_consultaDataTabla);
                    var ItemHabilitadoAnterior = resultadoDataTabla.FirstOrDefault()?["Habilitado"]?.ToString();
                    if (String.Equals(_consultaDataTabla, "[]"))
                    {
                        var _query = "INSERT INTO pla.T_ConfigurarVideoSesionProgramaSincronico (IdPEspecifico,IdPEspecificoSesion,NombreSesion,IdTipoProveedorVideo,Video, fechainicio, fechafin, Habilitado, Estado ,UsuarioCreacion ,UsuarioModificacion ,FechaCreacion ,FechaModificacion )" +
                        " VALUES(@IdPEspecifico,@IdPEspecificoSesion, @NombreSesion, @IdTipoProveedorVideo, @Video, @fechainicio, @fechafin, @Habilitado , @Estado ,@UsuarioCreacion ,@UsuarioModificacion ,@FechaCreacion ,@FechaModificacion)";
                        var query = _dapperRepository.QueryDapper(_query, new
                        {
                            IdPEspecifico,
                            IdPEspecificoSesion,
                            NombreSesion,
                            IdTipoProveedorVideo,
                            Video,
                            fechainicio,
                            fechafin,
                            Habilitado,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion
                        });
                        if (ItemHabilitadoAnterior == "False" && Habilitado == "true")
                        {
                            NotificacionGrabacionPublicada(int.Parse(IdPEspecifico),
                                                            int.Parse(IdPEspecificoSesion),
                                                            UsuarioCreacion,
                                                            true);

                        }
                    }
                    else
                    {
                        var _query = "UPDATE pla.T_ConfigurarVideoSesionProgramaSincronico SET " +
                        " NombreSesion = @NombreSesion,IdTipoProveedorVideo= @IdTipoProveedorVideo,Video = @Video, fechainicio= @fechainicio, fechafin = @fechafin, Habilitado = @Habilitado  WHERE IdPEspecificoSesion = @IdPEspecificoSesion";
                        var query = _dapperRepository.QueryDapper(_query, new { IdPEspecifico, IdPEspecificoSesion, NombreSesion, IdTipoProveedorVideo, Video, fechainicio, fechafin, Habilitado });
                        if (ItemHabilitadoAnterior == "False" && Habilitado == "true")
                        {
                            NotificacionGrabacionPublicada(int.Parse(IdPEspecifico),
                                                            int.Parse(IdPEspecificoSesion),
                                                            UsuarioCreacion,
                                                            true);

                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Modifica numeroDia de T_DisponibilidadProgramaSincronicoDefecto
        /// </summary>
        /// <returns> bool </returns>
        public bool ModificarDisponibilidadProgramaDefecto(DataDisponibilidadProgramaDefectoDTO filtro)
        {
            try
            {
                var Id = filtro.Id;
                var NumeroDia = filtro.NumeroDia;
                var Estado = 1;
                var FechaCreacion = DateTime.Now;
                var FechaModificacion = DateTime.Now;
                var UsuarioCreacion = "SYSTEM-PRUEBA";
                var UsuarioModificacion = "SYSTEM-PRUEBA";
                var _consultaDataTabla = _dapperRepository.QueryDapper("Select * from pla.T_DisponibilidadProgramaSincronicoDefecto", new { });
                if (String.Equals(_consultaDataTabla, "[]"))
                {
                    var _query = "INSERT INTO pla.T_DisponibilidadProgramaSincronicoDefecto (NumeroDia, Estado ,UsuarioCreacion ,UsuarioModificacion ,FechaCreacion ,FechaModificacion )" +
                    " VALUES(@NumeroDia, @Estado ,@UsuarioCreacion ,@UsuarioModificacion ,@FechaCreacion ,@FechaModificacion)";
                    var query = _dapperRepository.QueryDapper(_query, new
                    {
                        NumeroDia,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    });
                }
                else
                {
                    var _query = "UPDATE pla.T_DisponibilidadProgramaSincronicoDefecto SET " +
                    " NumeroDia = @NumeroDia, UsuarioModificacion = @UsuarioModificacion, FechaModificacion = @FechaModificacion WHERE Id = @Id";
                    var query = _dapperRepository.QueryDapper(_query, new { NumeroDia, UsuarioModificacion, FechaModificacion, Id });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene disponibilidad de programa
        /// </summary>
        /// <returns> List<DataDisponibilidadProgramaDefectoDTO> </returns>
        public List<DataDisponibilidadProgramaDefectoDTO> ObtenerDisponibilidadPrograma()
        {
            List<DataDisponibilidadProgramaDefectoDTO> DisponibilidadPrograma = new List<DataDisponibilidadProgramaDefectoDTO>();
            var query = _dapperRepository.QueryDapper("select * from pla.T_DisponibilidadProgramaSincronicoDefecto", new { });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                DisponibilidadPrograma = JsonConvert.DeserializeObject<List<DataDisponibilidadProgramaDefectoDTO>>(query);
            }
            return DisponibilidadPrograma;
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 29/05/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza sesiones
        /// </summary>
        /// <returns> bool </returns>
        public bool NotificacionGrabacionPublicada(int IdPEspecifico, int IdPEspecificoSesion, string UsuarioCreacion, bool Habilitado)
        {
            try
            {
                var query = "pla.SP_CrearNotificacionGrabacionPublicada";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdPEspecifico, IdPEspecificoSesion, UsuarioCreacion, Habilitado });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 2026-04-09
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de la ultima sesion asignada a PEspecifico Padre
        /// </summary>
        /// <param name="idPEspecifico">Id del programa especifico</param>
        /// <returns> PEspecificoUltimaSesionDTO </returns>
        public PEspecificoUltimaSesionDTO ObtenerUltimaSesionPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                PEspecificoUltimaSesionDTO resultado = null;
                var query = _dapperRepository.QuerySPFirstOrDefault("[pla].[SP_PEspecificoUltimaSesionPorIdPEspecifico]", new { PEspecifico = idPEspecifico });

                if (!string.IsNullOrEmpty(query) && query != "null")
                {
                    resultado = JsonConvert.DeserializeObject<PEspecificoUltimaSesionDTO>(query);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
