using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SolicitudCertificadoFisicoRepository
    /// Autor: Jonathan Caipo 
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de Solicitud de certificado físico
    /// </summary>
    public class SolicitudCertificadoFisicoRepository : GenericRepository<TSolicitudCertificadoFisico>, ISolicitudCertificadoFisicoRepository
    {
        private Mapper _mapper;

        public SolicitudCertificadoFisicoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolicitudCertificadoFisico, SolicitudCertificadoFisico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSolicitudCertificadoFisico MapeoEntidad(SolicitudCertificadoFisico entidad)
        {
            try
            {
                //crea la entidad padre
                TSolicitudCertificadoFisico modelo = _mapper.Map<TSolicitudCertificadoFisico>(entidad);

                //mapea los hijos
               //Listo mi king

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudCertificadoFisico Add(SolicitudCertificadoFisico entidad)
        {
            try
            {
                var SolicitudCertificadoFisico = MapeoEntidad(entidad);
                base.Insert(SolicitudCertificadoFisico);
                return SolicitudCertificadoFisico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudCertificadoFisico Update(SolicitudCertificadoFisico entidad)
        {
            try
            {
                var SolicitudCertificadoFisico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudCertificadoFisico.RowVersion = entidadExistente.RowVersion;

                base.Update(SolicitudCertificadoFisico);
                return SolicitudCertificadoFisico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudCertificadoFisico UpdateAlterno(SolicitudCertificadoFisico entidad)
        {
            try
            {
                var SolicitudCertificadoFisico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudCertificadoFisico.RowVersion = entidadExistente.RowVersion;

                base.UpdateAlterno(SolicitudCertificadoFisico);
                return SolicitudCertificadoFisico;
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


        public IEnumerable<TSolicitudCertificadoFisico> Add(IEnumerable<SolicitudCertificadoFisico> listadoEntidad)
        {
            try
            {
                List<TSolicitudCertificadoFisico> listado = new List<TSolicitudCertificadoFisico>();
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

        public IEnumerable<TSolicitudCertificadoFisico> Update(IEnumerable<SolicitudCertificadoFisico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolicitudCertificadoFisico> listado = new List<TSolicitudCertificadoFisico>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el nombre del courier de una solicitud
        /// </summary>
        /// <returns>L></returns>
        public string ObtenerCourierPorNombre(int idSolicitudCertificado)
        {
            try
            {
                ComboFiltroDTO courier = new ComboFiltroDTO();
                var queryText = string.Empty;
                queryText = "SELECT c.Id,c.Nombre FROM mkt.T_SolicitudCertificadoFisico sol " +
                            "INNER JOIN pla.T_Courier c on c.Id = sol.IdCourier WHERE sol.id=" + idSolicitudCertificado;
                var resultado = _dapperRepository.FirstOrDefault(queryText, new { idSolicitudCertificado });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    courier = JsonConvert.DeserializeObject<ComboFiltroDTO>(resultado)!;
                }
                else
                {
                    return null;
                }

                return courier.Nombre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tupla por idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public SolicitudCertificadoFisico ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                SolicitudCertificadoFisico tiempoCapacitacion = new SolicitudCertificadoFisico();
                var query = @"
                    SELECT Id,
                        IdMatriculaCabecera, IdPersonal, IdFur, IdProveedor,FechaSolicitud, FechaEntregaEstimada, FechaEntregaReal, CodigoSeguimientoEnvio, 
                        IdEstadoCertificadoFisico, IdCertificadoGeneradoAutomatico, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, 
                        IdCourier, FechaEntregaCourier, EstadoCourier, IdPaisConsignado, IdCiudadConsignada, CodigoSeguimiento 
                    FROM mkt.T_SolicitudCertificadoFisico WHERE Estado = 1 AND IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    tiempoCapacitacion = JsonConvert.DeserializeObject<SolicitudCertificadoFisico>(resultadoQuery)!;
                }
                return tiempoCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del reporte de Certificado Físico según el CodigoMatricula
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<DatosReporteEnvioCertificadoFisicoDTO> DatosReporteCertificadoEnvioFisicoPorId(string codigoMatricula)
        {
            try
            {
                List<DatosReporteEnvioCertificadoFisicoDTO> respuesta = new List<DatosReporteEnvioCertificadoFisicoDTO>();
                string query = @"SELECT 
                                    Id, IdMatriculaCabecera, CodigoMatricula, FechaSolicitud, Courier, Pais, Ciudad, 
                                    CodigoSeguimiento, Telefono, Direccion, Url, FechaEntregaCourier, FechaEntregaEstimada, EstadoCourier 
                                FROM 
                                    mkt.V_ReporteSolicitudCertificadoFisico 
                                WHERE 
                                    CodigoMatricula=@CodigoMatricula"; 
                var datosCertificadoEnvioFisico = _dapperRepository.QueryDapper(query, new { CodigoMatricula = codigoMatricula });
                if (!string.IsNullOrEmpty(datosCertificadoEnvioFisico) && !datosCertificadoEnvioFisico.Contains("[]") && datosCertificadoEnvioFisico != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<DatosReporteEnvioCertificadoFisicoDTO>>(datosCertificadoEnvioFisico)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DatosRegistroEnvioFisicoDTO DatosRegistroEnvioFisico(int IdSolicitudCertificadoFisico)
        {
            try
            {
                var rpta = new DatosRegistroEnvioFisicoDTO();
                string _query = "Select * From [ope].[V_ObtenerRegistroEnvioFisico] Where IdSolicitudCertificadoFisico = @IdSolicitudCertificadoFisico";
                string query = _dapperRepository.FirstOrDefault(_query, new { IdSolicitudCertificadoFisico });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<DatosRegistroEnvioFisicoDTO>(query);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<DatosEnvioAlumnoDTO> obtenerDatosEnvio(int idmatricula)
        {
            try
            {
                List<DatosEnvioAlumnoDTO> rpta = new List<DatosEnvioAlumnoDTO>();
                string query = "SELECT * FROM pla.V_obtenerDatosEnvioCertificado WHERE IdMatriculaCabecera=@idmatricula";
                var datosCertificadoEnvioFisico = _dapperRepository.QueryDapper(query, new { idmatricula = idmatricula });
                if (!string.IsNullOrEmpty(datosCertificadoEnvioFisico) && !datosCertificadoEnvioFisico.Contains("[]") && datosCertificadoEnvioFisico != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<DatosEnvioAlumnoDTO>>(datosCertificadoEnvioFisico);
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public SolicitudCertificadoFisico obtenerSolicitudCertificado(int IdMatriculaCabecera, int IdCertificadoGeneradoAutomatico)
        {
            try
            {
                SolicitudCertificadoFisico tiempoCapacitacion = new SolicitudCertificadoFisico();
                var query = @"
                        SELECT Id,
                               IdMatriculaCabecera,
                               IdPersonal,
                               IdFur,
                               IdProveedor,
                               FechaSolicitud,
                               FechaEntregaEstimada,
                               FechaEntregaReal,
                               CodigoSeguimientoEnvio,
                               IdEstadoCertificadoFisico,
                               IdCertificadoGeneradoAutomatico,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion,
                               RowVersion,
                               IdMigracion,
                               IdCourier,
                               FechaEntregaCourier,
                               EstadoCourier,
                               IdPaisConsignado,
                               IdCiudadConsignada,
                               CodigoSeguimiento FROM mkt.T_SolicitudCertificadoFisico WHERE IdMatriculaCabecera=@IdMatriculaCabecera AND IdCertificadoGeneradoAutomatico = @IdCertificadoGeneradoAutomatico              
                            ";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = IdMatriculaCabecera, IdCertificadoGeneradoAutomatico = IdCertificadoGeneradoAutomatico });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    tiempoCapacitacion = JsonConvert.DeserializeObject<SolicitudCertificadoFisico>(resultadoQuery)!;
                }
                return tiempoCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DatosEnvioAlumnoDTO InsertarDatosEnviosOperaciones(DatosEnvioAlumnoDTO filtro)
        {
            try
            {
              
                DatosEnvioAlumnoDTO rpta = new DatosEnvioAlumnoDTO();
                var idAlumno = filtro.IdAlumno;
                var idMatriculaCabecera = filtro.IdMatriculaCabecera;
                var codigoMatricula = filtro.CodigoMatricula;
                var region = filtro.Region;
                var ciudad = filtro.Ciudad;
                var distrito = filtro.Distrito;
                var referencia = filtro.Referencia;
                var direccion = filtro.Direccion;
                var nombre = filtro.Nombre;
                var telefono = filtro.Telefono;
                var codigoPostal = filtro.CodigoPostal;
                var idSolicitud = filtro.IdSolicitudCertificadoFisico;
                var dni = filtro.DNI;
                var usuario = filtro.Usuario;
                var query = "fin.SP_InsertarDatosEnvioAgenda";
                var res = _dapperRepository.QuerySPDapper(query, new { IdAlumno = idAlumno, IdMatriculaCabecera = idMatriculaCabecera, CodigoMatricula = codigoMatricula, Region = region, Ciudad = ciudad, Distrito = distrito, Referencia = referencia, Direccion = direccion, Nombre = nombre, Telefono = telefono, IdSolicitudCertificadoFisico = idSolicitud, CodigoPostal = codigoPostal, DNI = dni, Usuario = usuario });
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DataSolicitudCertificadoFisicoDTO> ObtenerSolicitudesCertificadoFisico (filtroSolicitudCertificadoFisicoDTO filtro)
        {
            try
            {
                var rpta = new List<DataSolicitudCertificadoFisicoDTO>();

                var filtros = new
                {
                    ConFiltros = filtro.ConFiltros,
                    IdPersonal = (filtro.ListaCoordinador == null || filtro.ListaCoordinador.Count() == 0) ? null : string.Join(",", filtro.ListaCoordinador),
                    CodigoSeguimiento = filtro.CodigoSeguimiento,   
                    IdMatriculaCabecera = filtro.IdMatriculaCabecera,
                    IdEstadoCertificadoFisico = filtro.IdEstadoCertificadoFisico,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin
                };

                var query = "ope.SP_ObtenerSolicitudCertificadoFisico";
                var res = _dapperRepository.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<DataSolicitudCertificadoFisicoDTO>>(res);
                
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
