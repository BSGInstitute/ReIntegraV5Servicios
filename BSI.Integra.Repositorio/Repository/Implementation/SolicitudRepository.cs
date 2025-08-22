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
    /// Repositorio: SolicitudRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 23/12/2022
    /// <summary>
    /// Gestión general de T_Solicitud
    /// </summary>
    public class SolicitudRepository : GenericRepository<TSolicitud>, ISolicitudRepository
    {
        private Mapper _mapper;

        public SolicitudRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolicitud, Solicitud>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSolicitud MapeoEntidad(Solicitud entidad)
        {
            try
            {
                //crea la entidad padre
                TSolicitud modelo = _mapper.Map<TSolicitud>(entidad);

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

        public TSolicitud Add(Solicitud entidad)
        {
            try
            {
                var Solicitud = MapeoEntidad(entidad);
                base.Insert(Solicitud);
                return Solicitud;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitud Update(Solicitud entidad)
        {
            try
            {
                var Solicitud = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Solicitud.RowVersion = entidadExistente.RowVersion;

                base.Update(Solicitud);
                return Solicitud;
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


        public IEnumerable<TSolicitud> Add(IEnumerable<Solicitud> listadoEntidad)
        {
            try
            {
                List<TSolicitud> listado = new List<TSolicitud>();
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

        public IEnumerable<TSolicitud> Update(IEnumerable<Solicitud> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolicitud> listado = new List<TSolicitud>();
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
        /// Autor: Gilmer Quispe
        /// Fecha: 23/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_Solicitud por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> Solicitud </returns>
        public Solicitud ObtenerPorId(int id)
        {
            try
            {
                var rpta = new Solicitud();
                var query = @"SELECT  Id,
                                       Nombre,
                                       Prioridad,
                                       IdSolicitudSubCategoria,
                                       IdPersonalRevision,
                                       IdPersonalSolucion,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM ope.T_Solicitud 
                                WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Solicitud>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 23/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_Solicitud por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> Solicitud </returns>
        public IEnumerable<ReporteSolicitudDTO> ObtenerSolicitudes()
        {
            try
            {
                var rpta = new List<ReporteSolicitudDTO>();
                var query = @" SELECT idCategoria,
                                      nombreCategoria,
                                      idTipoReporte,
                                      nombreReporte,
                                      idSubCategoria,
                                      nombreSubCategoria,
                                      idSolicitud,
                                      nombreSolicitud,
                                      prioridad,
                                      idAreaRevision,
                                      areaRevisión,
                                      idPersonalRevision,
                                      personalRevision,
                                      idAreaSolucion,
                                      areaSolucion,
                                      idPersonalSolucion,
                                      personalSolución FROM ope.V_ObtenerSolicitud ORDER BY idSolicitud desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteSolicitudDTO>>(resultado);
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
        /// Obtiene los datos del historial de solicitudes por alumno.
        /// </summary>
        /// <param name="id"> Id matricula y idPespecifico </param>
        /// <returns> Solicitud </returns>
        public IEnumerable<HistorialSolicitudAlumnoDTO> ObtenerHistorialSolicitudAlumno(int IdMatriculaCabecera, int IdPEspecifico)
        {
            try
            {
                var rpta = new List<HistorialSolicitudAlumnoDTO>();
                var query = @" SELECT Id,
                                      IdMatriculaCabecera,
                                      CodigoMatricula,
                                      NombreAlumno,
                                      IdPEspecifico,
                                      NombrePEspecifico,
                                      IdCentroCosto,
                                      CentroCosto,
                                      IdPGeneral,
                                      PGeneral,
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
	                                  IdAreaSolicitante,
	                                  AreaSolicitante,
                                      NombreArchivoSolicitante,
                                      IdAreaRevision,
                                      AreaRevision,
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
                                      EstadoSolicitud 
                                   FROM [ope].[V_ObtenerListaSolicitudAlumno] 
                                   WHERE IdMatriculaCabecera=@IdMatriculaCabecera AND IdPEspecifico=@IdPEspecifico ORDER BY FechaRegistro desc";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = IdMatriculaCabecera, IdPEspecifico = IdPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<HistorialSolicitudAlumnoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los estados de solicitudes.
        /// </summary>
        /// <param name="id"> Id matricula y idPespecifico </param>
        /// <returns> EstadoSolicitud </returns>
        public IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitud()
        {
            try
            {
                var rpta = new List<EstadoSolicitudDTO>();
                var query = @" SELECT Id,
                                      Nombre
                               FROM ope.V_ObtenerEstadosSolicitud;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoSolicitudDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los estados de solicitudes para revision
        /// </summary>
        /// <param name="id"> Id matricula y idPespecifico </param>
        /// <returns> EstadoSolicitud </returns>
        public IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitudRevision()
        {
            try
            {
                var rpta = new List<EstadoSolicitudDTO>();
                var query = @" SELECT Id,
                                      Nombre
                               FROM ope.V_ObtenerEstadosSolicitud
                               WHERE Id in (1,2) ;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoSolicitudDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los estados de solicitudes gestion
        /// </summary>
        /// <param name="id"> Id matricula y idPespecifico </param>
        /// <returns> EstadoSolicitud </returns>
        public IEnumerable<EstadoSolicitudDTO> ObtenerEstadosSolicitudGestion()
        {
            try
            {
                var rpta = new List<EstadoSolicitudDTO>();
                var query = @" SELECT Id,
                                      Nombre
                               FROM ope.V_ObtenerEstadosSolicitud
                               WHERE Id in (2,3,4,6);";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoSolicitudDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
