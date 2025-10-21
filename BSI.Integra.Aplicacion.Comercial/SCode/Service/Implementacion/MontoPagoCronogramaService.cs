using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using RestSharp.Extensions;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: MontoPagoCronogramaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/07/2022
    /// <summary>
    /// Gestión general de T_MontoPagoCronograma
    /// </summary>
    public class MontoPagoCronogramaService : IMontoPagoCronogramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MontoPagoCronogramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPagoCronograma, MontoPagoCronograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<MontoPagoCronogramaDTO, MontoPagoCronograma>(MemberList.None);
                cfg.CreateMap<MontoPagoCronogramaInterfazDTO, MontoPagoCronograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<MontoPagoCronogramaInterfazDTO, MontoPagoCronogramaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<MontoPagoCronogramaOportunidadDTO, MontoPagoCronograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<AlumnoDTO, Alumno>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public MontoPagoCronograma Add(MontoPagoCronograma entidad)
        {
            try
            {
                var modelo = _unitOfWork.MontoPagoCronogramaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MontoPagoCronograma>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MontoPagoCronograma Update(MontoPagoCronograma entidad)
        {
            try
            {
                var modelo = _unitOfWork.MontoPagoCronogramaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MontoPagoCronograma>(modelo);
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
                _unitOfWork.MontoPagoCronogramaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MontoPagoCronograma> Add(List<MontoPagoCronograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MontoPagoCronogramaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MontoPagoCronograma>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MontoPagoCronograma> Update(List<MontoPagoCronograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MontoPagoCronogramaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MontoPagoCronograma>>(modelo);
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
                _unitOfWork.MontoPagoCronogramaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MontoPagoCronograma
        /// </summary>
        /// <returns> List<MontoPagoCronogramaDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDTO> ObtenerMontoPagoCronograma()
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerMontoPagoCronograma();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPagoCronograma para mostrarse en combo.
        /// </summary>MontoPagoCronograma
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<MontoPagoCronogramaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPagoCronograma asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoCronogramaOportunidadDTO> </returns>
        public MontoPagoCronograma ObtenerPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Cronograma de Pago asociado a una Oportunidad para Documentos de Agenda
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCronogramaDocumentoDTO </returns>
        public MontoPagoCronogramaDocumentoDTO ObtenerConogramaPagoDocumentoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerConogramaPagoDocumentoPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Cronograma de Pago asociado a una Oportunidad Operaciones para Documentos de Agenda
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCronogramaDocumentoDTO </returns>
        public MontoPagoCronogramaDocumentoDTO ObtenerConogramaPagoDocumentoPorIdOportunidadOperaciones(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerConogramaPagoDocumentoPorIdOportunidadOperaciones(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Costo Total con Descuento asociado a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> string </returns>
        public string ObtenerCostoTotalConDescuento(int idOportunidad)
        {
            var servicioMoneda = new MonedaService(_unitOfWork);

            string result = "";
            var Schedule = ObtenerPorIdOportunidad(idOportunidad);
            if (Schedule != null && Schedule.Id != 0)
            {
                var paymentCurrency = servicioMoneda.ObtenerMonedaParaDocumento(Schedule.IdMoneda);
                result = paymentCurrency.Simbolo + Schedule.PrecioDescuento + " " + paymentCurrency.NombrePlural;
            }
            return result;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna el monto pagado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de Matricula Cabecera</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagadoDTO> </returns>
        public List<MontoPagadoDTO> ObtenerMontoPagado(int idMatriculaCabecera, int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerMontoPagado(idMatriculaCabecera, idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Calcula el Codigo Matricula
        /// </summary>
        /// <param name="idAlumno">Id de Alumno</param>
        /// <returns> CalcularCodigoMatriculaRespuestaDTO </returns>
        public CalcularCodigoMatriculaRespuestaDTO CalcularCodigoMatricula(int idAlumno, MontoPagoCronogramaDTO cronograma)
        {

            string _codigousuario = string.Empty,
                codigobanco = string.Empty;
            string firma = string.Empty, codigoAlumno = string.Empty, mensaje = string.Empty;
            int numero = 0;

            string _query = string.Empty;

            //proceso para guardar
            var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
            var pEspecificoInformacion = _unitOfWork.PEspecificoRepository.ObtenerPespecificoPorOportunidad(cronograma.IdOportunidad.Value);


            if (int.TryParse(pEspecificoInformacion.CodigoBanco, out numero))
            {
                numero = (numero / 100);
                codigobanco = numero.ToString();
                _codigousuario = alumno.Id.ToString() + codigobanco;
                _codigousuario = _codigousuario.PadRight(14, '0');
            }
            else
            {
                codigobanco = pEspecificoInformacion.CodigoBanco.ToUpper();
                _codigousuario = alumno.Id.ToString() + codigobanco;
            }

            cronograma.CodigoMatricula = _codigousuario;
            var matriculaEnProceso = 0;

            if (cronograma.EsAprobado)
            {
                matriculaEnProceso = 1;
            }
            cronograma.MatriculaEnProceso = matriculaEnProceso;
            var alumnoRetorno = _mapper.Map<AlumnoDTO>(alumno);
            return new CalcularCodigoMatriculaRespuestaDTO()
            {
                Cronograma = cronograma,
                Alumno = alumnoRetorno,
                PEspecificoInformacion = pEspecificoInformacion
            };
        }
        private CalcularCodigoMatriculaRespuestaDTO EliminarCronogramaVentas(MontoPagoCronogramaDTO cronogramaDTO, int idAlumno)
        {
            try
            {
                string Firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + cronogramaDTO.Usuario + ".png' />";

                var cronogramaCompleto = CalcularCodigoMatricula(idAlumno, cronogramaDTO);
                var matricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerPorCodigoMatricula(cronogramaCompleto.Cronograma.CodigoMatricula);
                if (matricula.EstadoMatricula == "matriculado")
                {
                    throw new ConflictException("#MPCS-ECV-002@La matricula ya se encuentra en estado matriculado");
                }
                var existeCuota = _unitOfWork.MontoPagoCronogramaRepository.CuotaPagada(cronogramaCompleto.Cronograma.CodigoMatricula);
                int resultado = 0;
                if (existeCuota != null)
                {
                    resultado = existeCuota.Resultado;
                }
                if (int.Parse(resultado.ToString()) == 0)
                {
                    GenerarArchivoCrepCronograma(cronogramaCompleto, "Eliminar", Firma);
                    cronogramaDTO.MatriculaEnProceso = 0;
                    EliminarCronograma(cronogramaCompleto.Cronograma);
                    cronogramaCompleto = null;
                    return cronogramaCompleto;
                }
                else
                {
                    throw new BadRequestException("#MPCS-ECV-001@Existe cuota: " + resultado.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Entidad MontoPagoCronograma desde MontoPagoCronogramaDTO
        /// </summary>
        /// <param name="dto">MontoPagoCronogramaDTO</param>
        /// <returns> MontoPagoCronograma </returns>
        public MontoPagoCronograma MapeoEntidadDesdeDTO(MontoPagoCronogramaDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<MontoPagoCronograma>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar.
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Entidad MontoPagoCronograma desde MontoPagoCronogramaDTO
        /// </summary>
        /// <param name="dto">MontoPagoCronogramaDTO</param>
        /// <returns> MontoPagoCronograma </returns>
        public MontoPagoCronogramaDTO MapeoEntidadEntreDTO(MontoPagoCronogramaInterfazDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<MontoPagoCronogramaDTO>(dto);
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_MontoPagoCronograma asociado al identificador.
        /// </summary>
        /// <param name="idCronograma">Id de MontoPagoCronograma</param>
        /// <returns> MontoPagoCronogramaDTO </returns>
        public MontoPagoCronogramaDTO ObtenerPorId(int idCronograma)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorId(idCronograma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera un Cronograma
        /// </summary>
        /// <param name="idCronograma">Id de Cronograma</param>
        /// <returns> ValorIntDTO> </returns>
        public ResultadoDTO GenerarCronogramaPorCoordinador(int idCronograma)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.GenerarCronogramaPorCoordinador(idCronograma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina un Cronograma
        /// </summary>
        /// <param name="idCronograma">Id de Cronograma</param>
        /// <returns> ValorIntDTO> </returns>
        public ResultadoDTO EliminarCronogramaVentasPorCoordinador(int idCronograma)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.EliminarCronogramaVentasPorCoordinador(idCronograma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Usuario y Clave de PortalWeb
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="email">Email del Alumno</param>
        /// <returns> DatosUsuarioPortalDTO </returns>
        public DatosUsuarioPortalDTO ObtenerUsuarioClavePortalWeb(int idAlumno, string email)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerUsuarioClavePortalWeb(idAlumno, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Usuario y Clave de PortalWeb
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="email">Email del Alumno</param>
        /// <returns> DatosUsuarioPortalDTO </returns>
        public DatosUsuarioPortalDTO CrearUsuarioClavePortalWeb(int idAlumno, string email, string clave, string claveEncriptada, string nombres, string apellidos, string telefono, string celular, int? idCodigoCiudad, int? idCodigoPais, DateTime fecha)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.CrearUsuarioClavePortalWeb(idAlumno, email, clave, claveEncriptada, nombres, apellidos, telefono, celular, idCodigoCiudad, idCodigoPais, fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Enlazar un Alumno
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <returns> void </returns>
        public DatosUsuarioPortalDTO EnlazarAlumno(AlumnoDTO alumno)
        {
            DatosUsuarioPortalDTO datosUsuario = ObtenerUsuarioClavePortalWeb(alumno.Id, alumno.Email1);
            string _passEncrip = string.Empty;
            if (datosUsuario != null && datosUsuario.UserName != null)
            {
                return datosUsuario;
            }
            else
            {
                var password = CrearClave(6);

                //var _passEncrip = Crypto.HashPassword(password); // Posible Libreria: BCrypt


                if (alumno.IdCodigoPais == 51)
                {
                    if (alumno.IdCodigoRegionCiudad.HasValue)
                        alumno.Telefono = (string.IsNullOrEmpty(alumno.Telefono) ? "" : "0" + alumno.IdCodigoRegionCiudad + alumno.Telefono);
                    alumno.Celular = alumno.Celular;
                }
                else
                {
                    if (alumno.IdCodigoRegionCiudad.HasValue)
                    {
                        if (alumno.IdCodigoRegionCiudad.Value == 0)
                        {
                            alumno.Telefono = (string.IsNullOrEmpty(alumno.Telefono) ? "" : "00" + alumno.IdCodigoPais.Value.ToString() + alumno.Telefono);
                            alumno.Celular = "00" + alumno.IdCodigoPais.Value.ToString() + alumno.Celular;
                        }
                        else
                        {
                            alumno.Telefono = (string.IsNullOrEmpty(alumno.Telefono) ? "" : "00" + alumno.IdCodigoPais.Value.ToString() + alumno.IdCodigoRegionCiudad.Value.ToString() + alumno.Telefono);
                            alumno.Celular = "00" + alumno.IdCodigoPais.Value.ToString() + alumno.Celular;
                        }
                    }
                }
                datosUsuario = CrearUsuarioClavePortalWeb(alumno.Id, alumno.Email1, password,
                    _passEncrip, (alumno.Nombre1 + " " + alumno.Nombre2), (alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno),
                    alumno.Telefono, alumno.Celular, alumno.IdCodigoRegionCiudad, alumno.IdCodigoPais, DateTime.Now);
            }
            return datosUsuario;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una Clave
        /// </summary>
        /// <param name="longitud">Longitud de la respuesta</param>
        /// <returns> string </returns>
        private string CrearClave(int longitud)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Envia un correo asociado a Ventas y Finanzas
        /// </summary>
        /// <param name="longitud">Longitud de la respuesta</param>
        /// <returns> bool </returns>
        public MontoPagoCronogramaDTO EnviarCorreosFinanzasVentas(CalcularCodigoMatriculaRespuestaDTO respuestaCodigoMatricula, DatosUsuarioPortalDTO datosUsuario)
        {
            try
            {
                var servicioCorreo = new CorreoService(_unitOfWork);
                var servicioPGeneral = new PGeneralService(_unitOfWork);
                var servicioOportunidad = new OportunidadService(_unitOfWork);
                var servicioCentroCosto = new CentroCostoService(_unitOfWork);

                var pEspecifico = respuestaCodigoMatricula.PEspecificoInformacion;
                var alumno = respuestaCodigoMatricula.Alumno;
                var cronograma = respuestaCodigoMatricula.Cronograma;

                string Mensaje = string.Empty, Firma = string.Empty;

                var Tpgeneral = servicioPGeneral.ObtenerPGeneralAtributosPrincipalesPorId(respuestaCodigoMatricula.PEspecificoInformacion.IdProgramaGeneral);
                Firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + cronograma.UsuarioCreacion + ".png' />";

                cronograma = GenerarArchivoCrepCronograma(respuestaCodigoMatricula, "Nuevo", Firma);

                string _Ciudad = pEspecifico.Ciudad.ToUpper();
                string CodigoAlumno = string.Empty;

                if (alumno.IdCodigoPais.Value != 57)
                {
                    CodigoAlumno = cronograma.CodigoMatricula.ToUpper() + " - " + cronograma.CodigoBancario;
                    if (alumno.IdCodigoPais.Value == 51)
                    {
                        Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "PE", cronograma.SimboloMoneda);
                    }
                    else if (alumno.IdCodigoPais.Value == 591)
                    {
                        CodigoAlumno = cronograma.CodigoMatricula.ToUpper() + " - " + cronograma.CodigoBancario;
                        Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", cronograma.SimboloMoneda);

                    }
                    else if (alumno.IdCodigoPais.Value == 52)
                    {
                        CodigoAlumno = cronograma.CodigoMatricula.ToUpper() + " - " + cronograma.CodigoBancario;
                        Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", cronograma.SimboloMoneda);

                    }
                    else if (alumno.IdCodigoPais.Value == 56)
                    {
                        CodigoAlumno = cronograma.CodigoMatricula.ToUpper() + " - " + cronograma.CodigoBancario;
                        Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", cronograma.SimboloMoneda);
                    }
                    else
                    {
                        CodigoAlumno = cronograma.CodigoMatricula.ToUpper();
                        Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", "US$");
                    }
                }
                else
                {
                    switch (alumno.IdCodigoPais.Value)
                    {
                        case 57://Colombia
                            CodigoAlumno = cronograma.CodigoMatricula.ToUpper();
                            CodigoAlumno = CodigoAlumno.Replace("A", "");
                            Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "CO", cronograma.SimboloMoneda);
                            break;
                        case 591://Bolivia
                            CodigoAlumno = cronograma.CodigoMatricula.ToUpper();
                            CodigoAlumno = CodigoAlumno.Replace("A", "");
                            Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "BO", cronograma.SimboloMoneda);
                            break;
                        case 52://Mexico
                            CodigoAlumno = cronograma.CodigoMatricula.ToUpper();
                            CodigoAlumno = CodigoAlumno.Replace("A", "");
                            Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "MEX", cronograma.SimboloMoneda);
                            break;
                        case 56://Chile
                            CodigoAlumno = cronograma.CodigoMatricula.ToUpper();
                            CodigoAlumno = CodigoAlumno.Replace("A", "");
                            Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "CHI", cronograma.SimboloMoneda);
                            break;
                        default://Otros
                            CodigoAlumno = cronograma.CodigoMatricula.ToUpper() + " - " + cronograma.CodigoBancario;
                            Mensaje = servicioCorreo.ObtenerMensajeCorreoProcesoPagoPeru(datosUsuario.UserName, datosUsuario.Password, "https://bsginstitute.com/AulaVirtual/MisPagos", alumno, cronograma.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", "US$");
                            break;
                    }
                }
                Mensaje += "<br><br>" + Firma;

                // DESCOMENTAR PARA PRODUCCION
                var Correos = servicioPGeneral.ObtenerCorreosIdPersonalAprobacion(cronograma.IdPersonal.Value);

                ////FALTA FUNCION OBTENER CORREOS POR ID /USERNAME
                var listaCorreos = (from x in Correos
                                    select x.Email).ToList();

                var idOportunidad = ObtenerPorId(cronograma.ListaDetalleCuotas[0].IdMontoPagoCronograma.Value).IdOportunidad;
                var idCentroCosto = servicioOportunidad.ObtenerPorId(idOportunidad.Value).IdCentroCosto;
                var centroCosto = _unitOfWork.CentroCostoRepository.ObtenerPorId(idCentroCosto.Value);
                var listaErrores = new List<string>();

                try
                {
                    servicioCorreo.EnvioEmailSinCopia(alumno.Email1, "Matriculas BSG INSTITUTE", "BSG INSTITUTE - Código de Pago " + Tpgeneral.Nombre, Mensaje);
                }
                catch (Exception ex1)
                {
                    servicioCorreo.EnvioEmail("ccrispin@bsginstitute.com", "Matriculas BSG INSTITUTE", "BSG INSTITUTE - Código de Pago - ERROR", "Error al enviar correo al alumno con sus credenciales - " + cronograma.CodigoMatricula.ToUpper(), listaErrores);
                }

                string mensajeArea = servicioCorreo.ObtenerMensajeCorreoFinanzas(alumno, cronograma.ListaDetalleCuotas.ToList(), cronograma.CodigoMatricula.ToUpper(), cronograma.SimboloMoneda, centroCosto);

                try
                {
                    servicioCorreo.EnvioEmail("matriculas@bsginstitute.com", "Matriculas BSG INSTITUTE", "BSG INSTITUTE - Código de Pago " + Tpgeneral.Nombre, mensajeArea, listaCorreos);
                }
                catch (Exception ex2)
                {
                    servicioCorreo.EnvioEmail("ccrispin@bsginstitute.com", "Matriculas BSG INSTITUTE", "BSG INSTITUTE - Código de Pago - ERROR", "Error al enviar correo al alumno con sus credenciales - " + cronograma.CodigoMatricula.ToUpper(), listaErrores);
                }

                //fin nuevo

                return cronograma;
            }
            catch (Exception ex)
            {
                return respuestaCodigoMatricula.Cronograma;
            }
        }

        //
        public int EnviarCorreosValidacionCongelamiento(string respuestaCodigoMatricula)
        {
            try
            {
                var servicioCorreo = new CorreoService(_unitOfWork);
                var servicioPGeneral = new PGeneralService(_unitOfWork);
                var servicioOportunidad = new OportunidadService(_unitOfWork);
                var servicioCentroCosto = new CentroCostoService(_unitOfWork);



                string Mensaje = string.Empty, Firma = string.Empty;


                string CodigoAlumno = string.Empty;


                Mensaje += "<p style='color: red;'><strong>----Urgente, Congelar Matriucula del Alumno <b>" + respuestaCodigoMatricula + " </b> ----</strong></p><p> El congelamiento automatico fallo por problemas con la configuracion del programa, congelar lo antes posible<b>  </b>  </span></strong></p><p><strong>Hora de Comprobacion:</strong></p><p><span style='color: orange;'>" + DateTime.Now + "</span></p> ";

                // Servicio envio correo
                servicioCorreo.EnvioEmailSinCopia("jquinones@bsginstitute.com,dhuaita@bsginstitute.com,jllanque@bsginstitute.com", "Congelamiento BSG INSTITUTE", "CONGELAMIENTO DE ALUMNO " + respuestaCodigoMatricula, Mensaje);

                return 0;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Archivo relacionado al Cronograma
        /// </summary>
        /// <param name="longitud">Longitud de la respuesta</param>
        /// <returns> MontoPagoCronogramaDTO </returns>
        public MontoPagoCronogramaDTO GenerarArchivoCrepCronograma(CalcularCodigoMatriculaRespuestaDTO respuestaCodigoMatricula, string estadoCrep, string firma)
        {
            var servicioCentroCosto = new CentroCostoService(_unitOfWork);
            var servicioCorreo = new CorreoService(_unitOfWork);

            var pEspecifico = respuestaCodigoMatricula.PEspecificoInformacion;
            var alumno = respuestaCodigoMatricula.Alumno;
            var cronograma = respuestaCodigoMatricula.Cronograma;

            string _query = string.Empty;
            string Monto = string.Empty, _Ciudad = string.Empty, _Banco = string.Empty, CodAutoriza = string.Empty, rpta = string.Empty;

            var CentroCostos = _unitOfWork.CentroCostoRepository.ObtenerPorId(pEspecifico.IdCentroCosto.Value);

            if (cronograma.ListaDetalleCuotas != null)
            {
                _Ciudad = pEspecifico.Ciudad.ToUpper();
                if ((_Ciudad == "AREQUIPA" || _Ciudad == "LIMA" || _Ciudad.Contains("ONLINE") || cronograma.NombrePlural == "Soles") && alumno.IdCodigoPais.Value != 57 && alumno.IdCodigoPais.Value != 591 && alumno.IdCodigoPais.Value != 52 && alumno.IdCodigoPais.Value != 56)
                {
                    _Banco = "Banco BCP - PERÚ";
                    string _nombrearchivo = string.Empty;
                    string _monadaNombre = string.Empty;
                    _nombrearchivo = "CREP_X";

                    StringBuilder linea = new StringBuilder();
                    string _tiporegistro = "CC";
                    string _codigosucursal = "215";

                    string _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Soles"; // 215-1863341-0-42 - CCI: 002-215-001863341042-20 - Cuenta Corriente en Soles programas Lima, Online y Aonline
                    string _nrocuentacrep = "1863341"; //seccion del nro de cuenta

                    if (cronograma.NombrePlural == "Dolares")
                    {
                        _monadaNombre = "DOLARES";
                        cronograma.SimboloMoneda = "US$";
                        if (_Ciudad.Contains("AREQUIPA"))
                        {
                            _nrocuenta = "Credipago a BSG Institute a la cuenta Arequipa-Dolares"; //215-2307651-1-32 - CCI: 002-215-002307651132-20 - Cuenta corriente en Dólares programas Arequipa
                            _nrocuentacrep = "2307651";
                        }
                        else//Dolares otros
                        {
                            _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Dolares"; //215-1870934-1-48 - CCI: 002-215-001870934148-23 - Cuenta corriente en Dólares programas Lima, Online y Aonline
                            _nrocuentacrep = "1870934";
                        }
                    }
                    else
                    {
                        _monadaNombre = "SOLES";
                        cronograma.SimboloMoneda = "S/.";
                        if (_Ciudad.Contains("AREQUIPA"))
                        {
                            _nrocuenta = "Credipago a BSG Institute a la cuenta Arequipa-Soles"; //215-1863344-0-72 - CCI: 002-215-001863344072-24 - Cuenta Corriente en Soles programas Arequipa
                            _nrocuentacrep = "1863344";
                        }
                        else//Soles otros
                        {
                            _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Soles"; //215-1863341-0-42 - CCI: 002-215-001863341042-20 - Cuenta corriente en Dólares programas Lima, Online y Aonline
                            _nrocuentacrep = "1863341";
                        }
                    }
                    string _codigomoneda = (_monadaNombre == "SOLES" ? "0" : "1");

                    string _tipovalidacion = "C";

                    cronograma.CodigoBancario = _nrocuenta;

                    string _nombreempresa = ("BSGINSTITUTE" + _Ciudad + _monadaNombre).PadRight(40);
                    string _fecha = String.Format("{0:yyyyMMdd}", DateTime.Now);
                    string _temp;

                    _temp = cronograma.ListaDetalleCuotas.Count.ToString();

                    string _totalregistros = _temp.ToString().PadLeft(9, '0');
                    string _tiporegistrod = "DD";
                    string _libred = "".PadLeft(47);

                    string _codigousuario = string.Empty, _nombreusuario, _fechaemision, _montomora, _montominimo, _tiporegistro2, nombres_b, apellidos_b;
                    string _codigoespecial, _montocuota, _fechavencimiento, _montototal = string.Empty;
                    string _tipoarchivo = string.Empty;

                    if (estadoCrep == "Nuevo")
                    {
                        _tipoarchivo = "A";
                    }
                    else
                    {
                        _tipoarchivo = "E";
                    }

                    string _libre = "000".PadLeft(113);
                    DateTime fechaEmision = new DateTime();

                    string codigobanco = string.Empty;
                    int numero = 0;
                    if (Int32.TryParse(pEspecifico.CodigoBanco, out numero))
                    {
                        numero = (numero / 100);
                        codigobanco = numero.ToString();
                        _codigousuario = alumno.Id.ToString() + codigobanco;
                        _codigousuario = _codigousuario.PadRight(14, '0');
                    }
                    else
                    {
                        codigobanco = pEspecifico.CodigoBanco.ToUpper();
                        _codigousuario = alumno.Id.ToString() + codigobanco;
                    }
                    CodAutoriza = _codigousuario;

                    _codigousuario = _codigousuario.ToString().PadLeft(14);
                    cronograma.CodigoMatricula = _codigousuario;


                    string _auxNombres = string.Empty, _auxApellidos = string.Empty;
                    _auxNombres = alumno.Nombre1.ToLower() + " " + alumno.Nombre2.ToLower();
                    _auxApellidos = alumno.ApellidoPaterno.ToLower() + " " + alumno.ApellidoMaterno.ToLower();

                    nombres_b = NormalizarCadena(_auxNombres.ToUpper());
                    apellidos_b = NormalizarCadena(_auxApellidos.ToUpper());

                    _nombreusuario = apellidos_b + " " + nombres_b;
                    _nombreusuario = _nombreusuario.ToUpper();
                    _nombreusuario = _nombreusuario.PadRight(40);

                    fechaEmision = DateTime.Now.AddDays(1);
                    _fechaemision = String.Format("{0:yyyyMMdd}", fechaEmision);

                    _montomora = "0".PadLeft(15, '0');
                    _montominimo = "0".PadLeft(9, '0');

                    _montototal = cronograma.PrecioDescuento.ToString().PadLeft(15, '0');

                    string nombreArchivoCrep = string.Empty;
                    if (string.IsNullOrEmpty(alumno.Dni))
                    {
                        nombreArchivoCrep = _nombrearchivo + "00000000";
                    }
                    else
                    {
                        nombreArchivoCrep = _nombrearchivo + alumno.Dni;
                    }

                    int contadorpagos = 0;
                    _montototal = cronograma.PrecioDescuento.ToString("0.00");
                    _montototal = _montototal.Replace(".", "").Replace(",", "");
                    byte[] registroCrepByte;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (StreamWriter myStreamWriter = new StreamWriter(ms))
                        {
                            linea.Append(_tiporegistro + _codigosucursal + _codigomoneda + _nrocuentacrep + _tipovalidacion + _nombreempresa + _fecha + _totalregistros + _montototal + _tipoarchivo + _libre);
                            rpta = linea.ToString();
                            myStreamWriter.WriteLine(linea.ToString());
                            linea.Remove(0, linea.Length);

                            foreach (var pagos in cronograma.ListaDetalleCuotas)
                            {
                                contadorpagos++;
                                if (contadorpagos < 10)
                                    _codigoespecial = "10" + contadorpagos.ToString() + "01XXXXXX";
                                else
                                    _codigoespecial = "1" + contadorpagos.ToString() + "01XXXXXX";

                                _codigoespecial = _codigoespecial.PadRight(30);
                                _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(pagos.MontoCuotaDescuento.ToString()) + Convert.ToDouble("0"))).Replace(".", "").Replace(",", "").PadLeft(15, '0');
                                _fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(pagos.FechaPago.ToString()));
                                if (estadoCrep == "Nuevo")
                                {
                                    _tiporegistro2 = "A";
                                }
                                else
                                {
                                    _tiporegistro2 = "E";
                                }

                                linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuentacrep + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                myStreamWriter.WriteLine(linea.ToString());
                                linea.Remove(0, linea.Length);
                            }
                            myStreamWriter.Close();//cerramos al terminar la escritura del archivo crep
                        }
                        registroCrepByte = ms.ToArray();
                    }

                    string mensaje = servicioCorreo.ObtenerMensajeCorreoFinanzas(alumno, cronograma.ListaDetalleCuotas.ToList(), _codigousuario + " - " + _nrocuenta, cronograma.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + firma;

                    List<CorreoNotificacionDTO> correoNotificacions = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.ArchivoCrep).ToList();
                    List<string> correos = new List<string>();
                    correos = correoNotificacions.Where(x => x.IdPais == 51 && x.Principal != true).ToList().Select(y => y.Email).ToList();
                    string destinatarioPrincipal = correoNotificacions.Where(x => x.IdPais == 51 && x.Principal == true).ToList().Select(y => y.Email).ToList().FirstOrDefault();
                    destinatarioPrincipal = string.IsNullOrWhiteSpace(destinatarioPrincipal) ? "dpacheco@bsginstitute.com" : destinatarioPrincipal;

                    servicioCorreo.EnvioEmailBlobAdjunto(destinatarioPrincipal, "Archivo CREP " + estadoCrep + " - Ventas", estadoCrep + " Archivo CREP " + pEspecifico.Nombre.ToString(), mensaje, nombreArchivoCrep + "-" + estadoCrep + ".txt", registroCrepByte, correos);

                }
                else if (alumno.IdCodigoPais.Value == 591)
                {
                    _Banco = "BCP - Bolivia";
                    if (cronograma.NombrePlural == "Dolares")
                    {
                        cronograma.SimboloMoneda = "US$";
                    }
                    else
                    {
                        cronograma.SimboloMoneda = "BS $";
                    }

                    List<CorreoNotificacionDTO> correoNotificacions = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.ArchivoCrep).ToList();
                    List<string> correos = new List<string>();
                    correos = correoNotificacions.Where(x => x.IdPais == 591 && x.Principal != true).ToList().Select(y => y.Email).ToList();
                    string destinatarioPrincipal = correoNotificacions.Where(x => x.IdPais == 591 && x.Principal == true).ToList().Select(y => y.Email).ToList().FirstOrDefault();
                    destinatarioPrincipal = string.IsNullOrWhiteSpace(destinatarioPrincipal) ? "dpacheco@bsginstitute.com" : destinatarioPrincipal;

                    string mensaje = servicioCorreo.ObtenerMensajeCorreoFinanzas(alumno, cronograma.ListaDetalleCuotas.ToList(), cronograma.CodigoMatricula + " " + _Banco, cronograma.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + firma;
                    //helpCorreo.envio_email("ccrispin@bsginstitute.com", "Peticion Bolivia [" + EstadoCrep + "] (" + this.CodigoMatricula + ")", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
                    servicioCorreo.EnvioEmail(destinatarioPrincipal, "Peticion Bolivia [" + estadoCrep + "] (" + cronograma.CodigoMatricula + ")", "Código de Pago " + pEspecifico.Nombre.ToString(), mensaje, correos);

                }
                else if (alumno.IdCodigoPais.Value == 52)
                {
                    _Banco = "Banco Mexico";
                    if (cronograma.NombrePlural == "Dolares")
                    {
                        cronograma.SimboloMoneda = "US$";
                    }
                    else
                    {
                        cronograma.SimboloMoneda = "MXN $";
                    }

                    List<CorreoNotificacionDTO> correoNotificacions = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.ArchivoCrep).ToList();
                    List<string> correos = new List<string>();
                    correos = correoNotificacions.Where(x => x.IdPais == 52 && x.Principal != true).ToList().Select(y => y.Email).ToList();
                    string destinatarioPrincipal = correoNotificacions.Where(x => x.IdPais == 52 && x.Principal == true).ToList().Select(y => y.Email).ToList().FirstOrDefault();
                    destinatarioPrincipal = string.IsNullOrWhiteSpace(destinatarioPrincipal) ? "dpacheco@bsginstitute.com" : destinatarioPrincipal;

                    string mensaje = servicioCorreo.ObtenerMensajeCorreoFinanzas(alumno, cronograma.ListaDetalleCuotas.ToList(), cronograma.CodigoMatricula + " " + _Banco, cronograma.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + firma;
                    //helpCorreo.envio_email("ccrispin@bsginstitute.com", "Peticion Bolivia [" + EstadoCrep + "] (" + this.CodigoMatricula + ")", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
                    servicioCorreo.EnvioEmail(destinatarioPrincipal, "Peticion Mexico [" + estadoCrep + "] (" + cronograma.CodigoMatricula + ")", "Código de Pago " + pEspecifico.Nombre.ToString(), mensaje, correos);

                }
                else if (alumno.IdCodigoPais.Value == 56)
                {
                    _Banco = "Banco Chile";
                    if (cronograma.NombrePlural == "Dolares")
                    {
                        cronograma.SimboloMoneda = "US$";
                    }
                    else
                    {
                        cronograma.SimboloMoneda = "CLP $";
                    }
                    List<CorreoNotificacionDTO> correoNotificacions = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.ArchivoCrep).ToList();
                    List<string> correos = new List<string>();
                    correos = correoNotificacions.Where(x => x.IdPais == 56 && x.Principal != true).ToList().Select(y => y.Email).ToList();
                    string destinatarioPrincipal = correoNotificacions.Where(x => x.IdPais == 56 && x.Principal == true).ToList().Select(y => y.Email).ToList().FirstOrDefault();
                    destinatarioPrincipal = string.IsNullOrWhiteSpace(destinatarioPrincipal) ? "dpacheco@bsginstitute.com" : destinatarioPrincipal;

                    string mensaje = servicioCorreo.ObtenerMensajeCorreoFinanzas(alumno, cronograma.ListaDetalleCuotas.ToList(), cronograma.CodigoMatricula + " " + _Banco, cronograma.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + firma;
                    //helpCorreo.envio_email("ccrispin@bsginstitute.com", "Peticion Bolivia [" + EstadoCrep + "] (" + this.CodigoMatricula + ")", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
                    servicioCorreo.EnvioEmail(destinatarioPrincipal, "Peticion Chile [" + estadoCrep + "] (" + cronograma.CodigoMatricula + ")", "Código de Pago " + pEspecifico.Nombre.ToString(), mensaje, correos);

                }
                else if (alumno.IdCodigoPais.Value == 57)
                {
                    string _codigosistema = string.Empty, _codigousuario = string.Empty;
                    _Banco = "Bancolombia - Colombia";
                    cronograma.SimboloMoneda = "COL $";
                    _codigosistema = alumno.Id.ToString() + pEspecifico.CodigoBanco.ToUpper();
                    _codigousuario = _codigosistema;

                    cronograma.CodigoMatricula = _codigousuario;

                    cronograma.CodigoBancario = "56470";//Nro Conv. Bancolombia: 56470 (lo que identifica nuestra cuenta)


                    List<CorreoNotificacionDTO> correoNotificacions = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.ArchivoCrep).ToList();
                    List<string> correos = new List<string>();
                    correos = correoNotificacions.Where(x => x.IdPais == 57 && x.Principal != true).ToList().Select(y => y.Email).ToList();
                    string destinatarioPrincipal = correoNotificacions.Where(x => x.IdPais == 57 && x.Principal == true).ToList().Select(y => y.Email).ToList().FirstOrDefault();
                    destinatarioPrincipal = string.IsNullOrWhiteSpace(destinatarioPrincipal) ? "dpacheco@bsginstitute.com" : destinatarioPrincipal;

                    string mensaje = servicioCorreo.ObtenerMensajeCorreoFinanzas(alumno, cronograma.ListaDetalleCuotas.ToList(), _codigousuario.ToUpper() + " " + _Banco, cronograma.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + firma;
                    //helpCorreo.envio_email("ccrispin@bsginstitute.com", "Peticion Bancolombia", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
                    servicioCorreo.EnvioEmail(destinatarioPrincipal, "Peticion Bancolombia", "Código de Pago " + pEspecifico.Nombre.ToString(), mensaje, correos);

                }
                else
                {
                    string _codigosistema = string.Empty, _codigousuario = string.Empty;
                    _Banco = "Internacional";
                    cronograma.SimboloMoneda = "US$";
                    _codigosistema = alumno.Id.ToString() + pEspecifico.CodigoBanco.ToUpper();
                    _codigousuario = _codigosistema;

                    cronograma.CodigoMatricula = _codigousuario;

                    List<CorreoNotificacionDTO> correoNotificacions = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.ArchivoCrep).ToList();
                    List<string> correos = new List<string>();
                    correos = correoNotificacions.Where(x => x.IdPais == null && x.Principal != true).ToList().Select(y => y.Email).ToList();
                    string destinatarioPrincipal = correoNotificacions.Where(x => x.IdPais == null && x.Principal == true).ToList().Select(y => y.Email).ToList().FirstOrDefault();
                    destinatarioPrincipal = string.IsNullOrWhiteSpace(destinatarioPrincipal) ? "dpacheco@bsginstitute.com" : destinatarioPrincipal;
                    string mensaje = servicioCorreo.ObtenerMensajeCorreoFinanzas(alumno, cronograma.ListaDetalleCuotas.ToList(), _codigousuario.ToUpper() + " " + _Banco, cronograma.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + firma;
                    //helpCorreo.envio_email("ccrispin@bsginstitute.com", "Internacional", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
                    servicioCorreo.EnvioEmail(destinatarioPrincipal, "Peticion Bancolombia", "Código de Pago " + pEspecifico.Nombre.ToString(), mensaje, correos);
                }
            }
            return cronograma;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Autor Modificación: Jonathan Caipo
        /// Fecha Modificación: 10/04/2023
        /// Version: 1.1
        /// <summary>
        /// Normaliza la Cadena reemplazando los Acentos y la letra ñ
        /// </summary>
        /// <param name="input">Cadena a ser Normalizada</param>
        /// <returns> string </returns>
        public string NormalizarCadena(string input)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            Regex replace_A_Mayuscula = new Regex("[Á]", RegexOptions.Compiled);
            Regex replace_E_Mayuscula = new Regex("[É]", RegexOptions.Compiled);
            Regex replace_I_Mayuscula = new Regex("[Í]", RegexOptions.Compiled);
            Regex replace_O_Mayuscula = new Regex("[Ó]", RegexOptions.Compiled);
            Regex replace_U_Mayuscula = new Regex("[Ú]", RegexOptions.Compiled);
            Regex replace_N_Mayuscula = new Regex("[Ñ]", RegexOptions.Compiled);
            Regex replace_n_Accents = new Regex("[ñ]", RegexOptions.Compiled);

            Regex replace_caracteresEspeciales = new Regex("[]|[|@|~|#|$|%|&|{|}|,|;|°|¿|!|¡|'|^|=|+]", RegexOptions.Compiled);

            input = replace_caracteresEspeciales.Replace(input, "");
            input = replace_a_Accents.Replace(input, "a");
            input = replace_e_Accents.Replace(input, "e");
            input = replace_i_Accents.Replace(input, "i");
            input = replace_o_Accents.Replace(input, "o");
            input = replace_u_Accents.Replace(input, "u");
            input = replace_n_Accents.Replace(input, "n");
            input = replace_A_Mayuscula.Replace(input, "A");
            input = replace_E_Mayuscula.Replace(input, "E");
            input = replace_I_Mayuscula.Replace(input, "I");
            input = replace_O_Mayuscula.Replace(input, "O");
            input = replace_U_Mayuscula.Replace(input, "U");
            input = replace_N_Mayuscula.Replace(input, "N");

            return input;
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Detalle monto Pago
        /// </summary>
        /// <param name="idMontoPago"> Id  Monto Pago </param>        
        /// <returns> Lista Detalle Monto Pago : List<DetalleMontoPagoDTO> </returns>
        public List<DetalleMontoPagoDTO> ObtenerDetalleMontoPago(int idMontoPago)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerDetalleMontoPago(idMontoPago);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/10/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene Cutoas Pagadas por codigoMatricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ResultadoDTO CuotaPagada(string codigoMatricula)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.CuotaPagada(codigoMatricula);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/10/2022
        /// Autor: Jonathan Caipo
        /// Fecha: 28/10/2022
        /// Version 1.0
        /// Descripcion: Se corrige logica de codigo
        /// <summary>
        /// Elimina cronograma
        /// </summary>
        /// <param name="cronograma"></param>
        public void EliminarCronograma(MontoPagoCronogramaDTO cronograma)
        {

            var montoPagoCronogramaDetalleService = new MontoPagoCronogramaDetalleService(_unitOfWork);

            using (TransactionScope scope = new TransactionScope())
            {
                //Delete Orquesta
                int idCronograma = cronograma.Id;
                var lista = montoPagoCronogramaDetalleService.ObtenerPorIdCronograma(idCronograma).ToList();
                foreach (var item in lista)
                {
                    montoPagoCronogramaDetalleService.Delete(item.Id, cronograma.Usuario);
                }
                //Fin Delete Orquesta
                var cronogramas = _unitOfWork.MontoPagoCronogramaRepository.listadoIdsPorOportunidad(cronograma.IdOportunidad.Value).ToList();
                var ids = cronogramas.Select(x => x.Id).ToList();
                Delete(ids, cronograma.Usuario);

                if (cronograma.EsAprobado)
                {
                    GenerarCronogramaPorCoordinador(cronograma.Id);
                }
                else
                {
                    EliminarCronogramaVentasPorCoordinador(cronograma.Id);
                }
                scope.Complete();
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene registros de asociados al IdOportunidad y TipoPersonal
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <param name="tipoPersonal"> Tipo de personal </param>
        /// <returns> List<TipoDescuentoOportunidadDTO> </returns>
        public List<TipoDescuentoOportunidadDTO> ObtenerTipoDescuento(int idOportunidad, string tipoPersonal)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerTipoDescuento(idOportunidad, tipoPersonal);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene montos complementarios filtrado por el IdPGeneral, IdPais, IdMontoPago y IdMatriculaCabecera
        /// </summary>
        /// <param name="idPGeneral"> Id de T_PGeneral </param>
        /// <param name="idPais"> Id de T_Pais </param>
        /// <param name="idMontoPago"> Id T_MontoPago </param>
        /// <param name="idMatriculaCabecera"> Id de T_MatriculaCabecera </param>
        /// <returns> List<DatosMontosComplementariosDTO> </returns>
        public List<DatosMontosComplementariosDTO> ObtenerMontosComplementarios(int idPGeneral, int idPais, int idMontoPago, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerMontosComplementarios(idPGeneral, idPais, idMontoPago, idMatriculaCabecera);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene montos pagos asociados al IdOportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de T_Oportunidad </param>
        /// <returns> List<MontoPagoOportunidadDTO> </returns>
        public List<MontoPagoOportunidadDTO> ObtenerMontosPagos(int idOportunidad)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerMontosPagos(idOportunidad);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion detallada de montos pagos por la opotunidad y el tipo de personal
        /// </summary>
        /// <param name="idOportunidad"> Id de la Oportunidad </param>
        /// <param name="tipoPersonal"> Tipo de personal </param>
        /// <param name="idMatriculaCabecera"> Id de la matricula cabecera </param>
        /// <returns> Objeto DTO: MontoPagoCronogramaV2DTO </returns>
        public MontoPagoCronogramaV2DTO ObtenerOportunidadMontoComplementarios(int idOportunidad, string tipoPersonal, int idMatriculaCabecera)
        {
            try
            {
                var resultadoMontoPagoCronogramaV2Dto = new MontoPagoCronogramaV2DTO();
                var montoPagoService = new MontoPagoService(_unitOfWork);
                var listaMontosComplementarios = new List<DatosMontosComplementariosDTO>();

                var ExisteCronograma = ObtenerPorIdOportunidad(idOportunidad);
                if (ExisteCronograma != null && ExisteCronograma.Id != 0)
                {
                    resultadoMontoPagoCronogramaV2Dto.Id = ExisteCronograma.Id;
                    resultadoMontoPagoCronogramaV2Dto.IdOportunidad = ExisteCronograma.IdOportunidad.Value;
                    resultadoMontoPagoCronogramaV2Dto.IdMontoPago = ExisteCronograma.IdMontoPago.Value;
                    resultadoMontoPagoCronogramaV2Dto.IdPersonal = ExisteCronograma.IdPersonal.Value;
                    resultadoMontoPagoCronogramaV2Dto.Precio = ExisteCronograma.Precio;
                    resultadoMontoPagoCronogramaV2Dto.PrecioDescuento = ExisteCronograma.PrecioDescuento;
                    resultadoMontoPagoCronogramaV2Dto.IdMoneda = ExisteCronograma.IdMoneda;
                    resultadoMontoPagoCronogramaV2Dto.IdTipoDescuento = ExisteCronograma.IdTipoDescuento.Value;
                    resultadoMontoPagoCronogramaV2Dto.EsAprobado = ExisteCronograma.EsAprobado;
                    resultadoMontoPagoCronogramaV2Dto.NombrePlural = ExisteCronograma.NombrePlural;
                    resultadoMontoPagoCronogramaV2Dto.Formula = ExisteCronograma.Formula;
                    resultadoMontoPagoCronogramaV2Dto.MatriculaEnProceso = ExisteCronograma.MatriculaEnProceso;
                    resultadoMontoPagoCronogramaV2Dto.CodigoMatricula = ExisteCronograma.CodigoMatricula;
                    resultadoMontoPagoCronogramaV2Dto.ListaDetalleCuotas = new List<MontoPagoCronogramaDetalleDTO>();

                    var montoPago = montoPagoService.ObtenerPorId(resultadoMontoPagoCronogramaV2Dto.IdMontoPago);
                    resultadoMontoPagoCronogramaV2Dto.ListaTipoDescuento = ObtenerTipoDescuento(idOportunidad, tipoPersonal);
                    resultadoMontoPagoCronogramaV2Dto.ListaMontosPagosVentas = ObtenerMontosPagos(idOportunidad);
                    if (montoPago != null)
                    {
                        resultadoMontoPagoCronogramaV2Dto.MontoPago = montoPago;//complementario
                        resultadoMontoPagoCronogramaV2Dto.ListaMontosComplementarios = ObtenerMontosComplementarios(montoPago.IdPrograma == null ? 0 : montoPago.IdPrograma.Value, montoPago.IdPais == null ? 0 : montoPago.IdPais.Value, montoPago.Id, idMatriculaCabecera);
                    }
                    else
                    {
                        resultadoMontoPagoCronogramaV2Dto.ListaMontosComplementarios = new List<DatosMontosComplementariosDTO>();
                    }
                }
                return (resultadoMontoPagoCronogramaV2Dto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene registros de asociados al IdOportunidad y TipoPersonal
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <param name="tipoPersonal"> Tipo de personal </param>
        /// <returns> List<TipoDescuentoOportunidadDTO> </returns>
        public MontoPagoCronogramaCompletoDTO ObtenerPorIdOportunidadYTipoPersonal(int idOportunidad, string tipoPersonal)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidadYTipoPersonal(idOportunidad, tipoPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Oportunidad Cronograma Pago por idOportunida y tipoPersonal
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="tipoPersonal"></param>
        /// <returns></returns>
        public ResumenCronogramaMontoPagoDTO ObtenerOportunidadCronogramaPago(int idOportunidad, string tipoPersonal)
        {
            try
            {
                var montoPagoCronograma = _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidad(idOportunidad);
                string vistaPortal = "";
                string estadoMatricula = "";
                MontoPagoCronogramaOportunidadDetalleDTO cronogramaCompleto = new MontoPagoCronogramaOportunidadDetalleDTO();
                if (montoPagoCronograma != null && montoPagoCronograma.Id != 0)
                {
                    if (montoPagoCronograma.CodigoMatricula.HasValue())
                    {
                        var matriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerPorCodigoMatricula(montoPagoCronograma.CodigoMatricula);
                        if (matriculaCabecera != null)
                        {
                            estadoMatricula = matriculaCabecera!.EstadoMatricula;
                        }
                        else
                        {
                            throw new BadRequestException($"#MPCS-OOC-001@No existe codigo asociado a una matricula");
                        }
                    }

                    cronogramaCompleto.Id = montoPagoCronograma.Id;
                    cronogramaCompleto.IdOportunidad = montoPagoCronograma.IdOportunidad;
                    cronogramaCompleto.IdMontoPago = montoPagoCronograma.IdMontoPago;
                    cronogramaCompleto.IdPersonal = montoPagoCronograma.IdPersonal;
                    cronogramaCompleto.Precio = montoPagoCronograma.Precio;
                    cronogramaCompleto.PrecioDescuento = montoPagoCronograma.PrecioDescuento;
                    cronogramaCompleto.IdMoneda = montoPagoCronograma.IdMoneda;
                    cronogramaCompleto.IdTipoDescuento = montoPagoCronograma.IdTipoDescuento;
                    cronogramaCompleto.EsAprobado = montoPagoCronograma.EsAprobado;
                    cronogramaCompleto.NombrePlural = montoPagoCronograma.NombrePlural;
                    cronogramaCompleto.Formula = montoPagoCronograma.Formula;
                    cronogramaCompleto.MatriculaEnProceso = montoPagoCronograma.MatriculaEnProceso;
                    cronogramaCompleto.CodigoMatricula = montoPagoCronograma.CodigoMatricula;
                    cronogramaCompleto.TipoPersonal = tipoPersonal;
                    var cronogramaDetalleService = new MontoPagoCronogramaDetalleService(_unitOfWork);
                    cronogramaCompleto.Detalle = cronogramaDetalleService.ObtenerMontoPagoCronogramaDetallePorIdCronograma(montoPagoCronograma.Id).ToList();
                    montoPagoCronograma.Id = montoPagoCronograma.IdMontoPago ?? 0;
                    vistaPortal = cronogramaDetalleService.ObtenerVistaPortalWeb(cronogramaCompleto.Detalle, cronogramaCompleto.NombrePlural);
                }
                cronogramaCompleto.MontosPago = _unitOfWork.MontoPagoRepository.ObtenerMontoPagoPorIdOportunidad(idOportunidad).ToList();
                cronogramaCompleto.TiposDescuento = _unitOfWork.TipoDescuentoRepository.ObtenerTipoDescuentoOportunidad(idOportunidad, tipoPersonal).ToList();
                ResumenCronogramaMontoPagoDTO resumenCronograma = new ResumenCronogramaMontoPagoDTO();
                resumenCronograma.Cronograma = cronogramaCompleto;
                resumenCronograma.MontoPago = _mapper.Map<MontoPagoCronogramaOportunidadDTO>(montoPagoCronograma);
                resumenCronograma.VistaPortalWeb = vistaPortal;
                resumenCronograma.EstadoMatricula = estadoMatricula;
                return resumenCronograma;
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Carlos Crispin R.
        /// Fecha: 24/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtene las sesiones del programa online
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de Matricula Cabecera</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagadoDTO> </returns>
        public List<SesionesDTO> ObtenerSesionesOnline(int idPespecifico)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaRepository.ObtenerSesionesOnline(idPespecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
