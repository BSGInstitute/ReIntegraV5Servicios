using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MatriculaCabeceraDatosCertificadoMensajeRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión general de T_MatriculaCabeceraDatosCertificadoMensajes
    /// </summary>
    public class MatriculaCabeceraDatosCertificadoMensajeRepository : GenericRepository<TMatriculaCabeceraDatosCertificadoMensaje>, IMatriculaCabeceraDatosCertificadoMensajeRepository
    {
        private Mapper _mapper;

        public MatriculaCabeceraDatosCertificadoMensajeRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMatriculaCabeceraDatosCertificadoMensaje, MatriculaCabeceraDatosCertificadoMensaje>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMatriculaCabeceraDatosCertificadoMensaje MapeoEntidad(MatriculaCabeceraDatosCertificadoMensaje entidad)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabeceraDatosCertificadoMensaje modelo = _mapper.Map<TMatriculaCabeceraDatosCertificadoMensaje>(entidad);

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

        public TMatriculaCabeceraDatosCertificadoMensaje Add(MatriculaCabeceraDatosCertificadoMensaje entidad)
        {
            try
            {
                var MatriculaCabeceraDatosCertificadoMensaje = MapeoEntidad(entidad);
                base.Insert(MatriculaCabeceraDatosCertificadoMensaje);
                return MatriculaCabeceraDatosCertificadoMensaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMatriculaCabeceraDatosCertificadoMensaje Update(MatriculaCabeceraDatosCertificadoMensaje entidad)
        {
            try
            {
                var MatriculaCabeceraDatosCertificadoMensaje = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MatriculaCabeceraDatosCertificadoMensaje.RowVersion = entidadExistente.RowVersion;

                base.Update(MatriculaCabeceraDatosCertificadoMensaje);
                return MatriculaCabeceraDatosCertificadoMensaje;
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


        public IEnumerable<TMatriculaCabeceraDatosCertificadoMensaje> Add(IEnumerable<MatriculaCabeceraDatosCertificadoMensaje> listadoEntidad)
        {
            try
            {
                List<TMatriculaCabeceraDatosCertificadoMensaje> listado = new List<TMatriculaCabeceraDatosCertificadoMensaje>();
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

        public IEnumerable<TMatriculaCabeceraDatosCertificadoMensaje> Update(IEnumerable<MatriculaCabeceraDatosCertificadoMensaje> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMatriculaCabeceraDatosCertificadoMensaje> listado = new List<TMatriculaCabeceraDatosCertificadoMensaje>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabeceraDatosCertificadoMensajes.
        /// </summary>
        /// <returns> List<MatriculaCabeceraDatosCertificadoMensajeDTO> </returns>
        public IEnumerable<MatriculaCabeceraDatosCertificadoMensajeDTO> ObtenerMatriculaCabeceraDatosCertificadoMensaje()
        {
            try
            {
                List<MatriculaCabeceraDatosCertificadoMensajeDTO> rpta = new List<MatriculaCabeceraDatosCertificadoMensajeDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdMatriculaCabecera,
	                    IdPersonalRemitente,
	                    IdPersonalReceptor,
	                    Mensaje,
	                    ValorAntiguo,
	                    ValorNuevo,
	                    EstadoMensaje,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM ope.T_MatriculaCabeceraDatosCertificadoMensajes
                    WHERE
	                    Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificadoMensajeDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MatriculaCabeceraDatosCertificadoMensajes para mostrarse en combo.
        /// </summary>
        /// <returns> List<MatriculaCabeceraDatosCertificadoMensajeComboDTO> </returns>
        public IEnumerable<MatriculaCabeceraDatosCertificadoMensajeComboDTO> ObtenerCombo()
        {
            try
            {
                List<MatriculaCabeceraDatosCertificadoMensajeComboDTO> rpta = new List<MatriculaCabeceraDatosCertificadoMensajeComboDTO>();
                var query = @"SELECT Id, Mensaje FROM ope.T_MatriculaCabeceraDatosCertificadoMensajes WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificadoMensajeComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cantidad de Registros de T_MatriculaCabeceraDatosCertificadoMensajes basado en un UserName.
        /// </summary>
        /// <param name="userName">Username de AspNetUsers</param>
        /// <returns> List<MatriculaCabeceraDatosCertificadoMensajeComboDTO> </returns>
        public ValorIntDTO ObtenerCantidadMensajesPorUsername(string userName)
        {
            try
            {
                ValorIntDTO cantidadMensajes = new ValorIntDTO();
                var query = @"SELECT Cantidad AS Valor FROM ope.V_CantidadDatosCertificadoMensajesPorUsuario WHERE UserName = @userName";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { userName });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    cantidadMensajes = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoQuery);
                }
                return cantidadMensajes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna los mensajes pendientes
        /// </summary>
        /// <param name="idPersonal">id del personal</param>
        /// <returns> MatriculaCabeceraDatosCertificadoMensajesDTO </returns> 
        public List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesPendientes(int idPersonal)
        {
            try
            {
                List<MatriculaCabeceraDatosCertificadoMensajesDTO> certificado = new List<MatriculaCabeceraDatosCertificadoMensajesDTO>();
                var query = string.Empty;
                query = @"select mccm.*,CONCAT(p.Nombres,' ',p.Apellidos) AS  Remitente ,CONCAT(ptr.Nombres,' ',ptr.Apellidos) AS  Receptor  
                        from ope.T_MatriculaCabeceraDatosCertificadoMensajes as mccm 
                        INNER JOIN gp.T_Personal as p on mccm.IdPersonalRemitente = p.Id 
                        INNER JOIN gp.T_Personal as ptr on mccm.IdPersonalReceptor = ptr.Id  
                        WHERE mccm.EstadoMensaje = 1 AND mccm.Estado=1 AND mccm.IdPersonalReceptor = " + idPersonal + " AND mccm.ValorAntiguo <>'-'";
                var cargosDB = _dapperRepository.QueryDapper(query, new { idPersonal });
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    certificado = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificadoMensajesDTO>>(cargosDB);
                }
                return certificado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Repositorio: MatriculaCabeceraDatosCertificadoRepositorio
        /// Autor: Miguel Mora
        /// Fecha: 10/09/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna el registro del certificado actual
        /// </summary>
        /// <returns> MatriculaCabeceraDatosCertificadoDTO </returns>        
        public List<MatriculaCabeceraDatosCertificadoDTO> ObtenerDatosCertificadoPorMatricula(int IdMatriculaCabecera)
        {
            try
            {

                List<MatriculaCabeceraDatosCertificadoDTO> certificado = new List<MatriculaCabeceraDatosCertificadoDTO>();
                var query = string.Empty;
                query = "SELECT  Id,IdMatriculaCabecera,Duracion,FechaInicio,FechaFinal,NombreCurso,EstadoCambioDatos, UsuarioCreacion AS Usuario,'' AS Mensaje FROM fin.T_MatriculaCabeceraDatosCertificado WHERE  Estado = 1 AND  IdMatriculaCabecera=@IdMatriculaCabecera AND  EstadoCambioDatos=0";
                var cargosDB = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    certificado = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificadoDTO>>(cargosDB);
                }
                return certificado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public int ObtenerCambiosPendientes(int idMatriculaCabecera)
        {
            ValorIntDTO cantidadCambiosPendientes = new ValorIntDTO();

            try
            {
                var query = string.Empty;
                query = @"SELECT COUNT(id) AS VaLor FROM fin.T_MatriculaCabeceraDatosCertificado WHERE  Estado = 1 AND EstadoCambioDatos = 1 AND  IdMatriculaCabecera = @idMatriculaCabecera";
                var cambiosPendientes = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(cambiosPendientes))
                {
                    cantidadCambiosPendientes = JsonConvert.DeserializeObject<ValorIntDTO>(cambiosPendientes);
                    return cantidadCambiosPendientes.Valor.Value;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }



        }


        public List<MatriculaCabeceraDatosCertificado> obtenerListado(int idMatriculaCabecera)
        {
            List<MatriculaCabeceraDatosCertificado> listado = new List<MatriculaCabeceraDatosCertificado>();

            try
            {

                var query = string.Empty;
                query = @"SELECT Id,
                            IdMatriculaCabecera,
                            Duracion,
                            FechaInicio,
                            FechaFinal,
                            NombreCurso,
                            EstadoCambioDatos,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion,
                            RowVersion,
                            IdMigracion,
                            IdCertificadoGeneradoAutomatico FROM fin.T_MatriculaCabeceraDatosCertificado WHERE Estado = 1 AND  EstadoCambioDatos= 0 AND IdMatriculaCabecera = @idMatriculaCabecera";
                var listadoDB = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(listadoDB) && !listadoDB.Contains("[]"))
                {
                    listado = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificado>>(listadoDB);
                }
                return listado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }



        }

        public Personal obtenerIntegraAspNetUser(int Usuario)
        {
            Personal perId = new Personal();

            try
            {
                var query = string.Empty;
                query = @"
                    SELECT Id,
                             Nombres,
                                     Apellidos,
                                     Rol,
                                     TipoPersonal,
                                     Email,
                                     AreaAbrev,
                                     Anexo,
                                     IdJefe,
                                     Central,
                                     Activo,
                                     ApellidoPaterno,
                                     ApellidoMaterno,
                                     IdSexo,
                                     IdEstadocivil,
                                     FechaNacimiento,
                                     IdPaisNacimiento,
                                     IdRegion,
                                     IdCiudad,
                                     IdTipoDocumento,
                                     NumeroDocumento,
                                     AutogeneradoEssalud,
                                     IdTipoSangre,
                                     UrlFirmaCorreos,
                                     IdGrupoProgramasCriticos,
                                     IdCerrador,
                                     EsCerrador,
                                     IdPaisDireccion,
                                     IdRegionDireccion,
                                     CiudadDireccion,
                                     NombreDireccion,
                                     FijoReferencia,
                                     MovilReferencia,
                                     EmailReferencia,
                                     IdSistemaPensionario,
                                     IdEntidadSistemaPensionario,
                                     NombreCUSPP,
                                     DistritoDireccion,
                                     ConEssalud,
                                     IdBusqueda,
                                     AliasEmailAsesor,
                                     Anexo3CX,
                                     Id3CX,
                                     Password3CX,
                                     Dominio,
                                     IdFacebookPersonal,
                                     Estado,
                                     UsuarioCreacion,
                                     UsuarioModificacion,
                                     FechaCreacion,
                                     FechaModificacion,
                                     RowVersion,
                                     IdMigracion,
                                     EsPersonaValida,
                                     UrlFoto,
                                     AplicaFirmaHTML,
                                     FirmaHTML,
                                     CargoFirmaHTML,
                                     IdPostulante,
                                     UsuarioAsterisk,
                                     ContrasenaAsterisk,
                                     IdTableroComercialCategoriaAsesor,
                                     IdPuestoTrabajoNivel,
                                     IdPersonalAreaTrabajo,
                                     IdPersonalArchivo,
                                     IdRolUsuarioTicket,
                                     DiscadorActivo,
                                     DiferenciaHoraria,
                                     EnLlamada,
                         MarcadorPredictivoActivo FROM gp.T_Personal WHERE Estado =1 AND  id =  @Usuario";
                var listadoDB = _dapperRepository.FirstOrDefault(query, new { Usuario });
                if (!string.IsNullOrEmpty(listadoDB) && !listadoDB.Contains("[]"))
                {
                    perId = JsonConvert.DeserializeObject<Personal>(listadoDB);
                }
                return perId;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

        }



        public MatriculaCabeceraDTO obtenerMatricula(int idMatriculaCabecera)
        {
            MatriculaCabeceraDTO matricula = new MatriculaCabeceraDTO();

            try
            {

                var query = string.Empty;
                query = @"	   SELECT Id,
                            CodigoMatricula,
                            IdAlumno,
                            IdPEspecifico,
                            IdEstadoPagoMatricula,
                            EstadoMatricula,
                            FechaMatricula,
                            EmpresaRuc,
                            EmpresaNombre,
                            EmpresaContacto,
                            EmpresaEmail,
                            EmpresaPaga,
                            EmpresaObservaciones,
                            IdDocumentoPago,
                            IdCoordinador,
                            IdAsesor,
                            IdEstado_matricula,
                            FechaSuspendido,
                            UsuarioCoordinadorAcademico,
                            ObservacionGeneralOperaciones,
                            UsuarioCoordinadorSupervision,
                            IdCronograma,
                            IdPeriodo,
                            UsuarioCoordinadorPreAsignacion,
                            VerificacionConforme,
                            FechaMatriculaValidada,
                            FechaPagoValidada,
                            FechaRetiro,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion,
                            RowVersion,
                            IdMigracion,
                            GrupoCurso,
                            IdSubEstadoMatricula,
                            IdPaquete,
                            FechaFinalizacion,
                            IdEstadoMatriculaCertificado,
                            IdSubEstadoMatriculaCertificado,
                            EsInhouse,
                            FechaPorMatricularMatriculado,
                            IdCategoriaAlumno FROM fin.T_MatriculaCabecera WHERE id = @idMatriculaCabecera";
                var matriculaDB = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(matriculaDB) && !matriculaDB.Contains("[]"))
                {
                    matricula = JsonConvert.DeserializeObject<MatriculaCabeceraDTO>(matriculaDB);
                }
                return matricula;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }



        }

        public DetalleOportunidadOperacionesDTO ObtenerDetalleMatricula(int idMatriculaCabecera)
        {
            DetalleOportunidadOperacionesDTO detalleMatricula = new DetalleOportunidadOperacionesDTO();

            try
            {

                var query = string.Empty;
                query = @"SELECT Oportunidad.Id AS IdOportunidad, 
                               CentroCosto.Id AS IdCentroCosto, 
                               CentroCosto.Nombre AS NombreCentroCosto, 
                               PGeneral.Id AS IdProgramaGeneral,
                               PGeneral.Nombre AS NombreProgramaGeneral,
                               PEspecifico.Ciudad AS NombreCiudad,
                               Case When Pespecifico.TipoId=1 then 100 
                                    else Escala.EscalaCalificacion
                               End EscalaCalificacion
                        FROM ope.T_OportunidadClasificacionOperaciones AS OportunidadClasificacionOperaciones
                             INNER JOIN com.T_Oportunidad AS Oportunidad ON OportunidadClasificacionOperaciones.IdOportunidad = Oportunidad.Id
                             INNER JOIN pla.T_CentroCosto AS CentroCosto ON CentroCosto.Id = Oportunidad.IdCentroCosto
                             INNER JOIN pla.T_PEspecifico AS PEspecifico ON PEspecifico.IdCentroCosto = CentroCosto.id
                             INNER JOIN pla.T_PGeneral AS PGeneral ON PGeneral.Id = PEspecifico.IdProgramaGeneral
                             INNER JOIN pla.T_AreaCapacitacion AS AreaCapacitacion ON AreaCapacitacion.Id = PGeneral.IdArea
                             INNER JOIN pla.T_SubAreaCapacitacion AS SubAreaCapacitacion ON SubAreaCapacitacion.Id = PGeneral.IdSubArea
                             LEFT  JOIN pla.V_EscalaCalificacionPespecifico AS Escala ON Escala.CodigoCiudad = PEspecifico.Ciudad
                        WHERE IdMatriculaCabecera = @idMatriculaCabecera;";
                var detalleMatriculaDB = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(detalleMatriculaDB) && !detalleMatriculaDB.Contains("[]"))
                {
                    detalleMatricula = JsonConvert.DeserializeObject<DetalleOportunidadOperacionesDTO>(detalleMatriculaDB);
                }
                return detalleMatricula;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }



        }

        public MatriculaCabeceraDTO obtenerAlumno(int IdAlumno)
        {
            MatriculaCabeceraDTO obtenerAlumno = new MatriculaCabeceraDTO();

            try
            {

                var query = string.Empty;
                query = @"SELECT * FROM mkt.T_Alumno WHERE id = @IdAlumno AND Estado = 1";
                var IdAlumnoDB = _dapperRepository.QueryDapper(query, new { IdAlumno });
                if (!string.IsNullOrEmpty(IdAlumnoDB))
                {
                    obtenerAlumno = JsonConvert.DeserializeObject<MatriculaCabeceraDTO>(IdAlumnoDB);
                }
                return obtenerAlumno;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DetalleOportunidadOperacionesDTO obtenerOportunidad(int IdOportunidad)
        {
            DetalleOportunidadOperacionesDTO obtenerOportunidad = new DetalleOportunidadOperacionesDTO();

            try
            {

                var query = string.Empty;
                query = @" SELECT Oportunidad.Id AS IdOportunidad, 
                               CentroCosto.Id AS IdCentroCosto, 
                               CentroCosto.Nombre AS NombreCentroCosto, 
                               PGeneral.Id AS IdProgramaGeneral,
                               PGeneral.Nombre AS NombreProgramaGeneral,
                               PEspecifico.Ciudad AS NombreCiudad,
                               Case When Pespecifico.TipoId=1 then 100 
                                    else Escala.EscalaCalificacion
                               End EscalaCalificacion
                        FROM ope.T_OportunidadClasificacionOperaciones AS OportunidadClasificacionOperaciones
                             INNER JOIN com.T_Oportunidad AS Oportunidad ON OportunidadClasificacionOperaciones.IdOportunidad = Oportunidad.Id
                             INNER JOIN pla.T_CentroCosto AS CentroCosto ON CentroCosto.Id = Oportunidad.IdCentroCosto
                             INNER JOIN pla.T_PEspecifico AS PEspecifico ON PEspecifico.IdCentroCosto = CentroCosto.id
                             INNER JOIN pla.T_PGeneral AS PGeneral ON PGeneral.Id = PEspecifico.IdProgramaGeneral
                             INNER JOIN pla.T_AreaCapacitacion AS AreaCapacitacion ON AreaCapacitacion.Id = PGeneral.IdArea
                             INNER JOIN pla.T_SubAreaCapacitacion AS SubAreaCapacitacion ON SubAreaCapacitacion.Id = PGeneral.IdSubArea
                             LEFT  JOIN pla.V_EscalaCalificacionPespecifico AS Escala ON Escala.CodigoCiudad = PEspecifico.Ciudad
                        WHERE IdMatriculaCabecera = @Id;";
                var obtenerOportunidadDB = _dapperRepository.FirstOrDefault(query, new { Id = IdOportunidad });
                if (!string.IsNullOrEmpty(obtenerOportunidadDB) && !obtenerOportunidadDB.Contains("[]"))
                {
                    obtenerOportunidad = JsonConvert.DeserializeObject<DetalleOportunidadOperacionesDTO>(obtenerOportunidadDB);
                }
                return obtenerOportunidad;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DatosOportunidadDocumentosCompuestoDTO ObtenerDatosCompuestosPorIdOportunidad(int idOportunidad)
        {
            DatosOportunidadDocumentosCompuestoDTO datosCompuestosPorIdOportunidad = new DatosOportunidadDocumentosCompuestoDTO();

            try
            {

                var query = string.Empty;
                query = @"Select * From com.V_OportunidadCompuesto Where Id=@IdOportunidad";
                var datosCompuestosPorIdOportunidadDB = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(datosCompuestosPorIdOportunidadDB) && !datosCompuestosPorIdOportunidadDB.Contains("[]"))
                {
                    datosCompuestosPorIdOportunidad = JsonConvert.DeserializeObject<DatosOportunidadDocumentosCompuestoDTO>(datosCompuestosPorIdOportunidadDB);
                }
                return datosCompuestosPorIdOportunidad;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool ExisteNuevaAulaVirtual(int idPEspecifico)
        {

            try
            {


                var query = @"SELECT Id FROM [pla].[V_TPEspecificoNuevoAulaVirtual_DataBasica] WHERE Id = @idPEspecifico";
                var NuevaAulaVirtualDB = _dapperRepository.FirstOrDefault(query, new { idPEspecifico });

                return !string.IsNullOrEmpty(NuevaAulaVirtualDB) && !NuevaAulaVirtualDB.Contains("[]"); ;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ObtenerFechaInicioCapacitacion(int IdMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new StringDTO();
                string query = "SELECT FechaInicio AS Valor FROM pla.V_FechaInicioFinCapacitacion Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerFechaFinCapacitacion(int IdMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new StringDTO();
                string _query = "SELECT FechaFin AS Valor FROM pla.V_FechaInicioFinCapacitacion Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerFechaInicioCapacitacionPortalWeb(int IdMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new StringDTO();
                string query = "SELECT FechaInicio AS Valor FROM [pw].[V_PW_FechaInicioFinCapacitacionPortalWeb]   Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                    return FechaInicio.Valor;
                }
                return DateTime.Now.ToString("dd del MMMM de yyyy");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ObtenerFechaFinCapacitacionPortalWeb(int IdMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new StringDTO();
                string _query = "SELECT FechaFin AS Valor FROM [pw].[V_PW_FechaInicioFinCapacitacionPortalWeb]  Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                    return FechaInicio.Valor;
                }
                return DateTime.Now.ToString("dd del MMMM de yyyy");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna los mensajes leidos por el id del personal
        /// </summary>
        /// <param name="idPersonal">id del personal</param>
        /// <returns> MatriculaCabeceraDatosCertificadoMensajesDTO </returns>  
        public List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesLeidos(int idPersonal)
        {
            try
            {
                List<MatriculaCabeceraDatosCertificadoMensajesDTO> certificado = new List<MatriculaCabeceraDatosCertificadoMensajesDTO>();
                var query = string.Empty;
                query = @"select mccm.*,CONCAT(p.Nombres,' ',p.Apellidos) AS  Remitente ,CONCAT(ptr.Nombres,' ',ptr.Apellidos) AS  Receptor  
                        FROM ope.T_MatriculaCabeceraDatosCertificadoMensajes as mccm  
                        INNER JOIN gp.T_Personal as p on mccm.IdPersonalRemitente = p.Id  
                        INNER JOIN gp.T_Personal as ptr on mccm.IdPersonalReceptor = ptr.Id  
                        WHERE  mccm.EstadoMensaje = 0 AND mccm.Estado=1 AND mccm.IdPersonalReceptor = " + idPersonal + " AND mccm.ValorAntiguo <>'-'";
                var cargosDB = _dapperRepository.QueryDapper(query, new { idPersonal });
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    certificado = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificadoMensajesDTO>>(cargosDB);
                }
                return certificado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
