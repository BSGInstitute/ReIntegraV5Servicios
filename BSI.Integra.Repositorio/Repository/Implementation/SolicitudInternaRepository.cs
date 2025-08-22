using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SolicitudInternaRepository
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2023
    /// <summary>
    /// Gestión general de T_SolicitudInterna
    /// </summary>
    public class SolicitudInternaRepository : GenericRepository<TSolicitudInterna>, ISolicitudInternaRepository
    {
        private Mapper _mapper;

        public SolicitudInternaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolicitudInterna, SolicitudInterna>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSolicitudInterna MapeoEntidad(SolicitudInterna entidad)
        {
            try
            {
                //crea la entidad padre
                TSolicitudInterna modelo = _mapper.Map<TSolicitudInterna>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudInterna Add(SolicitudInterna entidad)
        {
            try
            {
                var SolicitudInterna = MapeoEntidad(entidad);
                base.Insert(SolicitudInterna);
                return SolicitudInterna;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudInterna Update(SolicitudInterna entidad)
        {
            try
            {
                var SolicitudInterna = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudInterna.RowVersion = entidadExistente.RowVersion;

                base.Update(SolicitudInterna);
                return SolicitudInterna;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TSolicitudInterna> Add(IEnumerable<SolicitudInterna> listadoEntidad)
        {
            try
            {
                List<TSolicitudInterna> listado = new List<TSolicitudInterna>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TSolicitudInterna> Update(IEnumerable<SolicitudInterna> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolicitudInterna> listado = new List<TSolicitudInterna>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes en revision
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesPorArea(int idPersonal)
        {
            try
            {
                List<SolicitudInternaFiltradaDTO> rpta = new List<SolicitudInternaFiltradaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudInternaPorPersonalRevision", new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudInternaFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes gestion
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesGestion(int idPersonal)
        {
            try
            {
                List<SolicitudInternaFiltradaDTO> rpta = new List<SolicitudInternaFiltradaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudPorPersonalGestion", new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudInternaFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes del alumno por filtro.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudAlumnoFiltradaDTO </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesPorFiltro(FiltroSolicitudesInternasDTO FiltroSolicitud)
        {
            try
            {
                List<SolicitudInternaFiltradaDTO> rpta = new List<SolicitudInternaFiltradaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudInternaPorFiltro", new { FiltroSolicitud.IdPersonal, FiltroSolicitud.TipoFiltro, FiltroSolicitud.Filtro1, FiltroSolicitud.Filtro2 });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudInternaFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes del alumno por filtro reporte
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudAlumnoFiltradaDTO </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolicitudesInternasDTO FiltroSolicitud)
        {
            try
            {
                List<SolicitudInternaFiltradaDTO> rpta = new List<SolicitudInternaFiltradaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudInternaPorFiltroReporte", new { FiltroSolicitud.IdPersonal, FiltroSolicitud.TipoFiltro, FiltroSolicitud.Filtro1, FiltroSolicitud.Filtro2 });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudInternaFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 23/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del historial de solicitudes Internas.
        /// </summary>
        /// <param name="id"> Id matricula y idPespecifico </param>
        /// <returns> Solicitud </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> obtenerSolicitudesInternas()
        {
            try
            {
                var rpta = new List<SolicitudInternaFiltradaDTO>();
                var query = @" SELECT id,
                                      DetalleSolicitud,
                                      Prioridad,
                                      IdSolicitud,
                                      NombreSolicitud,
                                      IdTipoReporte,
                                      NombreTipoReporte,
                                      IdSolicitudCategoria,
                                      NombreSolicitudCategoria,
                                      IdSubCategoria,
                                      NombreSubCategoria,
                                      IdSolicitante,
                                      NombreSolicitante,
                                      CorreoSolicitante,
                                      IdAreaSolicitante,
                                      AreaSolicitante,
                                      IdAreaRevision,
                                      AreaRevision,
                                      NombreArchivoSolicitante,
                                      IdPersonalRevision,
                                      PersonalRevision,
                                      IdAreaSolucion,
                                      AreaSolucion,
                                      IdPersonalSolucion,
                                      PersonalSolucion,
                                      FechaRegistro,
                                      ComentarioSolucion,
                                      NombreArchivoSolucion,
                                      IdEstadoSolicitud,
                                      EstadoSolicitud FROM ope.V_ObtenerFiltroSolicitudInterna
                                      ORDER BY FechaRegistro desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudInternaFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener por Id.
        /// </summary>
        /// <param name="SolicitudInterna"> Datos actualizar </param>
        /// <returns> RevisarSolicitudAlumnoDTO </returns>
        public SolicitudInterna ObtenerPorId(int id)
        {
            try
            {
                SolicitudInterna respuesta = new SolicitudInterna();
                var query = @"SELECT Id,
                            IdSolicitud,
                            IdEstadoSolicitud,
                            IdPersonal,
                            DetalleSolicitud,
                            ContentTypeSolicitante,
                            NombreArchivoSolicitante,
                            ContentTypeSolucion,
                            NombreArchivoSolucion,
                            ComentarioSolucion,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion,
                            RowVersion,
                            IdMigracion 
                            FROM ope.T_SolicitudInterna
                            WHERE Estado = 1
                                  AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<SolicitudInterna>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
