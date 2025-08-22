using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: MatriculaCabeceraDatosCertificadoMensajeService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión general de T_MatriculaCabeceraDatosCertificadoMensajes
    /// </summary>
    public class MatriculaCabeceraDatosCertificadoMensajeService : IMatriculaCabeceraDatosCertificadoMensajeService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MatriculaCabeceraDatosCertificadoMensajeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMatriculaCabeceraDatosCertificadoMensaje, MatriculaCabeceraDatosCertificadoMensaje>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public MatriculaCabeceraDatosCertificadoMensaje Add(MatriculaCabeceraDatosCertificadoMensaje entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MatriculaCabeceraDatosCertificadoMensaje>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MatriculaCabeceraDatosCertificadoMensaje Update(MatriculaCabeceraDatosCertificadoMensaje entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MatriculaCabeceraDatosCertificadoMensaje>(modelo);
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
                _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ObtenerCambiosPendientes(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerCambiosPendientes(idMatriculaCabecera);
                //                return _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerMatriculaCabeceraDatosCertificadoMensaje();

            }
            catch (Exception ex)
            {
                throw ex;
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
        public bool ObtenerDatosCertificadoPorMatricula(MatriculaCabeceraDatosCertificadoDTO ObjetoDTO, int idpersonal)
        {
            try
            {

                var matriculaCabeceraDatosCertificadoService = new MatriculaCabeceraDatosCertificadoService(_unitOfWork);
                var certificadoActual = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerDatosCertificadoPorMatricula(ObjetoDTO.IdMatriculaCabecera).First();

               
                var personal = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.obtenerIntegraAspNetUser(idpersonal);

                if(personal.IdPersonalAreaTrabajo == 3 && personal.TipoPersonal != null && personal.TipoPersonal.ToLower() == "coordinador")
                {

                    var nuevocertificado = new MatriculaCabeceraDatosCertificado()
                    {
                        IdMatriculaCabecera = certificadoActual.IdMatriculaCabecera,
                        IdCertificadoGeneradoAutomatico = ObjetoDTO.IdCertificadoGeneradoAutomatico,
                        Duracion = ObjetoDTO.Duracion,
                        FechaInicio = (DateTime)ObjetoDTO.FechaInicio,
                        FechaFinal = (DateTime)ObjetoDTO.FechaFinal,
                        NombreCurso = ObjetoDTO.NombreCurso,
                        EstadoCambioDatos = false,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };


                    var nuevocertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensaje()
                    {
                        IdMatriculaCabecera = certificadoActual.IdMatriculaCabecera,
                        IdPersonalRemitente = personal.Id,
                        IdPersonalReceptor = personal.Id,
                        Mensaje = ObjetoDTO.Mensaje,
                        ValorAntiguo = JsonConvert.SerializeObject(certificadoActual),
                        ValorNuevo = JsonConvert.SerializeObject(nuevocertificado),
                        EstadoMensaje = false,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now

                    };

                    _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Delete(certificadoActual.Id, ObjetoDTO.Usuario);
                    _unitOfWork.Commit();

                    var tnuevocertificado = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Add(nuevocertificado);
                    _unitOfWork.Commit();
                    Add(nuevocertificadoMensaje);

                    var datoOportunidad = _unitOfWork.PGeneralConfiguracionPlantillaRepository.ObtenerDatoAlumno(certificadoActual.IdMatriculaCabecera);
                    var datos = _unitOfWork.CertificadoGeneradoAutomaticoRepository.FirstById(tnuevocertificado.IdCertificadoGeneradoAutomatico.Value);
                    var matricula = _unitOfWork.MatriculaCabeceraRepository.FirstById(certificadoActual.IdMatriculaCabecera);
                    bool existe = _unitOfWork.PEspecificoMatriculaAlumnoRepository.ExisteNuevaAulaVirtual(matricula.IdPespecifico);
                    DocumentoService documentos = new DocumentoService(_unitOfWork);
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    byte[] documentoByte;

                    documentoByte = documentos.RepublicacionVistaPreviaCertificado(
                        datos.IdPlantilla,
                        1235,
                        datoOportunidad.IdOportunidad,
                        tnuevocertificado,
                        ref Idplantillabase,
                        ref codigoCertificado
                    );

                    CertificadoGeneradoAutomatico certificadoGenerado = new CertificadoGeneradoAutomatico();

                    var nuevoCodigo = Idplantillabase == 12 ? codigoCertificado : documentos.ContenidoCertificado.CorrelativoConstancia.ToString();
                    _unitOfWork.CertificadoGeneradoAutomaticoRepository.ActualizarNombreArchivo(codigoCertificado, datos.Id);
                    CertificadoGeneradoAutomaticoContenido contenidoCertificadoBO = documentos.ContenidoCertificado;
                    contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                    contenidoCertificadoBO.FechaFinCapacitacion = contenidoCertificadoBO.FechaFinCapacitacion == null ? "" : contenidoCertificadoBO.FechaFinCapacitacion;
                    contenidoCertificadoBO.Estado = true;
                    contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                    contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                    contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                    contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";
                    CertificadoDetalleService data = new CertificadoDetalleService(_unitOfWork);
                    var Url = data.GuardarArchivoCertificado(documentoByte, "application/pdf", codigoCertificado);

                }
                else
                {

                    var nuevocertificado = new MatriculaCabeceraDatosCertificado()
                    {
                        IdMatriculaCabecera = certificadoActual.IdMatriculaCabecera,
                        IdCertificadoGeneradoAutomatico = ObjetoDTO.IdCertificadoGeneradoAutomatico,
                        Duracion = ObjetoDTO.Duracion,
                        FechaInicio = (DateTime)ObjetoDTO.FechaInicio,
                        FechaFinal = (DateTime)ObjetoDTO.FechaFinal,
                        NombreCurso = ObjetoDTO.NombreCurso,
                        EstadoCambioDatos = true,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    var nuevocertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensaje()
                    {
                        IdMatriculaCabecera = certificadoActual.IdMatriculaCabecera,
                        IdPersonalRemitente = personal.Id,
                        IdPersonalReceptor = (int)personal.IdJefe,
                        Mensaje = ObjetoDTO.Mensaje,
                        ValorAntiguo = JsonConvert.SerializeObject(certificadoActual),
                        ValorNuevo = JsonConvert.SerializeObject(nuevocertificado),
                        EstadoMensaje = true,
                        Estado = true,
                        UsuarioCreacion = ObjetoDTO.Usuario,
                        UsuarioModificacion = ObjetoDTO.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now

                    };


                    Add(nuevocertificadoMensaje);

                    var rpta = matriculaCabeceraDatosCertificadoService.Add(nuevocertificado);
                }

                

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MatriculaCabeceraDatosCertificado> ObtenerListado(int idMatriculaCabecera)
        {
            try
            {
                var listado = new List<MatriculaCabeceraDatosCertificado>();
                var serviceMatriculaCabeceraDatosCertificado = new MatriculaCabeceraDatosCertificadoService(_unitOfWork);
                //var listado = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.obtenerListado(idMatriculaCabecera);
                //if (listado.Count() == 102)
                //{

                var matriculaCabecera = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.obtenerMatricula(idMatriculaCabecera);
                var detalleMatriculaCabecera = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerDetalleMatricula(idMatriculaCabecera);
                //solo esos 2 de arriba lo estan usando
                DateTime fechaInicioCapacitacion = new DateTime();
                DateTime fechaFinCapacitacion = new DateTime();
                bool existe = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ExisteNuevaAulaVirtual(matriculaCabecera.IdPespecifico);
                if (existe == false)
                {
                    fechaInicioCapacitacion = TranformarCadenaEnFecha(_unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerFechaInicioCapacitacion(matriculaCabecera.Id));
                    fechaFinCapacitacion = TranformarCadenaEnFecha(_unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerFechaFinCapacitacion(matriculaCabecera.Id));
                }
                else
                {
                    fechaInicioCapacitacion = TranformarCadenaEnFecha(_unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerFechaInicioCapacitacionPortalWeb(matriculaCabecera.Id));
                    fechaFinCapacitacion = TranformarCadenaEnFecha(_unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerFechaFinCapacitacionPortalWeb(matriculaCabecera.Id));
                }
                //MatriculaCabeceraDatosCertificado nuevocertificado = new MatriculaCabeceraDatosCertificado
                //{
                //    IdMatriculaCabecera = idMatriculaCabecera,
                //    Duracion = _unitOfWork.EstructuraEspecificaRepository.ObtenerDuracionTotalPorIdMatriculaCabecera(idMatriculaCabecera),
                //    FechaInicio = fechaInicioCapacitacion,
                //    FechaFinal = fechaFinCapacitacion,
                //    NombreCurso = detalleMatriculaCabecera.NombreProgramaGeneral.ToUpper(),
                //    EstadoCambioDatos = false,
                //    Estado = true,
                //    UsuarioCreacion = "SYSTEM",
                //    UsuarioModificacion = "SYSTEM",
                //    FechaCreacion = DateTime.Now,
                //    FechaModificacion = DateTime.Now
                //};
                //serviceMatriculaCabeceraDatosCertificado.Add(nuevocertificado);
                listado = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.obtenerListado(idMatriculaCabecera);
                //}
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DateTime TranformarCadenaEnFecha(string fecha)
        {
            try
            {
                DateTime FechaMod = new DateTime();
                string[] dates = fecha.Split(' ');
                string mes = "00";
                switch (dates[2].ToUpper())
                {
                    case "ENERO":
                        mes = "01";
                        break;
                    case "FEBRERO":
                        mes = "02";
                        break;
                    case "MARZO":
                        mes = "03";
                        break;
                    case "ABRIL":
                        mes = "04";
                        break;
                    case "MAYO":
                        mes = "05";
                        break;
                    case "JUNIO":
                        mes = "06";
                        break;
                    case "JULIO":
                        mes = "07";
                        break;
                    case "AGOSTO":
                        mes = "08";
                        break;
                    case "SEPTIEMBRE":
                        mes = "09";
                        break;
                    case "SETIEMBRE":
                        mes = "09";
                        break;
                    case "OCTUBRE":
                        mes = "10";
                        break;
                    case "NOVIEMBRE":
                        mes = "11";
                        break;
                    case "DICIEMBRE":
                        mes = "12";
                        break;
                }
                FechaMod = DateTime.Parse(dates[4] + "-" + mes + "-" + dates[0]);
                return FechaMod;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MatriculaCabeceraDatosCertificadoMensaje> Add(List<MatriculaCabeceraDatosCertificadoMensaje> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaCabeceraDatosCertificadoMensaje>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaCabeceraDatosCertificadoMensaje> Update(List<MatriculaCabeceraDatosCertificadoMensaje> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaCabeceraDatosCertificadoMensaje>>(modelo);
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
                _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabeceraDatosCertificadoMensajes
        /// </summary>
        /// <returns> List<MatriculaCabeceraDatosCertificadoMensajeDTO> </returns>
        public IEnumerable<MatriculaCabeceraDatosCertificadoMensajeDTO> ObtenerMatriculaCabeceraDatosCertificadoMensaje()
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerMatriculaCabeceraDatosCertificadoMensaje();
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
                return _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerCombo();
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
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerCantidadMensajesPorUsername(string userName)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerCantidadMensajesPorUsername(userName);
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
                return _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerMensajesPendientes(idPersonal);
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
        /// Retorna los mensajes leidos por el id del personal
        /// </summary>
        /// <param name="idPersonal">id del personal</param>
        /// <returns> MatriculaCabeceraDatosCertificadoMensajesDTO </returns>  
        public List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesLeidos(int idPersonal)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.ObtenerMensajesLeidos(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public Boolean ModificarCertificadoMensaje(MatriculaCabeceraDatosCertificadoMensajesDTO ObjetoDTO)
        {
            try
            {
                var mensaje = _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.FirstById(ObjetoDTO.Id);
                MatriculaCabeceraDatosCertificadoDTO certificadoActual = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.ObtenerDatosCertificadoPorMatricula(mensaje.IdMatriculaCabecera).First();
                //var solicitudCertificado = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.ObtenerMatriculaCabceraDatosCertificado(mensaje.IdMatriculaCabecera);
                var solicitudCertificado = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.FirstBy(
                   w =>
                   w.Estado == true &&
                   w.IdMatriculaCabecera == mensaje.IdMatriculaCabecera &&
                   w.EstadoCambioDatos == true
                );
                //MatriculaCabeceraDatosCertificado solicitudCertificado = _mapper.Map<MatriculaCabeceraDatosCertificadoDTO, MatriculaCabeceraDatosCertificado>(aux);
                var valorMensaje = string.Empty;
                if (ObjetoDTO.solicitud == false)
                {
                    valorMensaje = "NO se aprobó tu solicitud -";
                    solicitudCertificado.Estado = false;
                }
                else
                {
                    valorMensaje = "Se aprobó tu solicitud -";
                    _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Delete(certificadoActual.Id, ObjetoDTO.Usuario);
                    _unitOfWork.Commit();
                }
                solicitudCertificado.EstadoCambioDatos = false;
                _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Update(solicitudCertificado);
                _unitOfWork.Commit();
                if (ObjetoDTO.solicitud == true)
                {
                    var datoOportunidad = _unitOfWork.PGeneralConfiguracionPlantillaRepository.ObtenerDatoAlumno(certificadoActual.IdMatriculaCabecera);
                    var datos = _unitOfWork.CertificadoGeneradoAutomaticoRepository.FirstById(solicitudCertificado.IdCertificadoGeneradoAutomatico.Value);
                    var matricula = _unitOfWork.MatriculaCabeceraRepository.FirstById(certificadoActual.IdMatriculaCabecera);
                    bool existe = _unitOfWork.PEspecificoMatriculaAlumnoRepository.ExisteNuevaAulaVirtual(matricula.IdPespecifico);
                    DocumentoService documentos = new DocumentoService(_unitOfWork);
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    byte[] documentoByte;

                    documentoByte = documentos.RepublicacionVistaPreviaCertificado(
                        datos.IdPlantilla,
                        1235,
                        datoOportunidad.IdOportunidad,
                        solicitudCertificado,
                        ref Idplantillabase,
                        ref codigoCertificado
                    );


                    CertificadoGeneradoAutomatico certificadoGenerado = new CertificadoGeneradoAutomatico();

                    var nuevoCodigo = Idplantillabase == 12 ? codigoCertificado : documentos.ContenidoCertificado.CorrelativoConstancia.ToString();
                    _unitOfWork.CertificadoGeneradoAutomaticoRepository.ActualizarNombreArchivo(codigoCertificado, datos.Id);
                    CertificadoGeneradoAutomaticoContenido contenidoCertificadoBO = documentos.ContenidoCertificado;
                    contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                    contenidoCertificadoBO.FechaFinCapacitacion = contenidoCertificadoBO.FechaFinCapacitacion == null ? "" : contenidoCertificadoBO.FechaFinCapacitacion;
                    contenidoCertificadoBO.Estado = true;
                    contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                    contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                    contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                    contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";
                    CertificadoDetalleService data = new CertificadoDetalleService(_unitOfWork);
                    var Url = data.GuardarArchivoCertificado(documentoByte, "application/pdf", codigoCertificado);

                }
                mensaje.EstadoMensaje = false;
                _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.Update(mensaje);
                _unitOfWork.Commit();
                valorMensaje += ObjetoDTO.MensajeRespuesta;
                TMatriculaCabeceraDatosCertificadoMensaje nuevocertificadoMensaje = new TMatriculaCabeceraDatosCertificadoMensaje
                {
                    IdMatriculaCabecera = mensaje.IdMatriculaCabecera,
                    IdPersonalRemitente = mensaje.IdPersonalReceptor,
                    IdPersonalReceptor = mensaje.IdPersonalRemitente,
                    Mensaje = valorMensaje,
                    ValorAntiguo = "-",
                    ValorNuevo = "-",
                    EstadoMensaje = true,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _unitOfWork.MatriculaCabeceraDatosCertificadoMensajeRepository.Insert(nuevocertificadoMensaje);
                _unitOfWork.Commit();

                return (true);
            }

            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
