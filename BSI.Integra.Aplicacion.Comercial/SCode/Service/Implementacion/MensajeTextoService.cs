using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: MensajeTextoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 29/09/2022
    /// <summary>
    /// Gestión general de Informacion de MensajeTexto
    /// </summary>
    public class MensajeTextoService : IMensajeTextoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MensajeTextoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMensajeTexto, MensajeTexto>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public MensajeTexto Add(MensajeTexto entidad)
        {
            try
            {
                var modelo = _unitOfWork.MensajeTextoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MensajeTexto>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MensajeTexto Update(MensajeTexto entidad)
        {
            try
            {
                var modelo = _unitOfWork.MensajeTextoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MensajeTexto>(modelo);
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
                _unitOfWork.MensajeTextoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MensajeTexto> Add(List<MensajeTexto> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MensajeTextoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MensajeTexto>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MensajeTexto> Update(List<MensajeTexto> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MensajeTextoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MensajeTexto>>(modelo);
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
                _unitOfWork.MensajeTextoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe.
        /// Fecha: 29/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtener Codigo Matricula segun el Id de una Oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> MatriculaCabeceraCodigoFechaDTO </returns>
        public MatriculaCabeceraCodigoFechaDTO CodigoMatriculaPorOportunidad(int CodigoMatricula)
        {
            try
            {
                return _unitOfWork.MensajeTextoRepository.ObtenerCodigoMatriculaPorOportunidad(CodigoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 29/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Accesos del Portal para mandar por mensaje
        /// </summary>
        /// <param name="IdAlumno"> Id del alumno </param>
        /// <returns> AccesoPortalWebDTO </returns>
        public AccesoPortalWebDTO AccesoPorIdAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.MensajeTextoRepository.ObtenerAccesoPorIdAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Envía mensaje de texto a Alumno
        /// </summary>
        /// <returns> Confirmación de envío </returns>
        /// <returns> Bool </returns>
        public bool EnviarMensajeTexto(AgendaMensajeTextoDTO mensajeParametros)
        {
            try
            {
                var servicioAlumno = new AlumnoService(_unitOfWork);
                var servicioTMK_Twilio = new TMK_TwilioService();
                var mensajeTextoDTO = new MensajeTextoDTO();
                int idAlumno = mensajeParametros.IdAlumno;

                var datosAlumno = servicioAlumno.ObtenerPorId(mensajeParametros.IdAlumno);
                var codigoMatricula = _unitOfWork.MensajeTextoRepository.ObtenerCodigoMatriculaPorOportunidad(mensajeParametros.IdOportunidad);
                var accesos = _unitOfWork.MensajeTextoRepository.ObtenerAccesoPorIdAlumno(mensajeParametros.IdAlumno);

                mensajeTextoDTO.IdOportunidad = mensajeParametros.IdOportunidad;
                mensajeTextoDTO.CodigoPais = datosAlumno.IdCodigoPais.Value;
                mensajeTextoDTO.IdMatriculaCabecera = codigoMatricula.CodigoMatricula;
                mensajeTextoDTO.Numero = datosAlumno.Celular;
                mensajeTextoDTO.UserName = accesos.UserName;
                mensajeTextoDTO.Clave = accesos.Clave;

                //Se arma el mensaje
                string msjCodigMatricula = string.Empty;
                if (datosAlumno.IdCodigoPais.Value == 57)
                {
                    msjCodigMatricula = mensajeTextoDTO.IdMatriculaCabecera.Replace("A", "");
                    mensajeTextoDTO.Mensaje = "BSG Institute: \n Codigo de referencia " + msjCodigMatricula + "\n Usuario: " + mensajeTextoDTO.UserName + "\n Clave: " + mensajeTextoDTO.Clave;
                    //Encoding encode = System.Text.Encoding.GetEncoding(mensaje);
                }
                else
                {
                    msjCodigMatricula = mensajeTextoDTO.IdMatriculaCabecera;
                    mensajeTextoDTO.Mensaje = "BSG Institute: \n Codigo Matricula " + msjCodigMatricula + "\n Usuario: " + mensajeTextoDTO.UserName + "\n Clave: " + mensajeTextoDTO.Clave;
                }
                //Se arma el número de destino
                if (datosAlumno.Celular == null)
                {
                    throw new Exception("El alumno no tiene un número de telefono registrado.");
                    return false;
                }
                else
                {
                    bool banderaNumero = false;
                    string aux_numero = string.Empty;
                    if (datosAlumno.IdCodigoPais.Value == 51)
                    {
                        mensajeTextoDTO.Numero = "+51" + datosAlumno.Celular;
                    }
                    else
                    {
                        var codigoPais = datosAlumno.Celular.Substring(0, 2);
                        try
                        {
                            int codigoNumero = Convert.ToInt32(codigoPais);
                            if (codigoNumero == 0)
                            {
                                banderaNumero = true; //si los digito iniciales son 00
                            }
                            else
                            {
                                banderaNumero = false; //si los digitos iniciales no son 00 y es otro numero
                            }
                        }
                        catch (Exception)
                        {
                            banderaNumero = false;
                        }
                        if (banderaNumero)
                        {
                            aux_numero = datosAlumno.Celular.Remove(0, 2);
                        }
                        else
                        {
                            aux_numero = datosAlumno.Celular;
                        }
                        mensajeTextoDTO.Numero = "+" + aux_numero;
                    }

                    var idEnvio = servicioTMK_Twilio.EnviarMensajeTexto(mensajeTextoDTO.Mensaje, mensajeTextoDTO.Numero);
                    var mensajeTextoEntidad = new MensajeTexto();
                    mensajeTextoEntidad.IdSeguimientoTwilio = idEnvio;
                    mensajeTextoEntidad.FechaCreacion = DateTime.Now;
                    mensajeTextoEntidad.FechaModificacion = DateTime.Now;
                    mensajeTextoEntidad.UsuarioCreacion = mensajeParametros.Usuario;
                    mensajeTextoEntidad.UsuarioModificacion = mensajeParametros.Usuario;
                    mensajeTextoEntidad.Estado = true;
                    _unitOfWork.MensajeTextoRepository.Add(mensajeTextoEntidad);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
