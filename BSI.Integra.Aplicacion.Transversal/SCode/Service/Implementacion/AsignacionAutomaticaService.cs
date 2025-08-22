using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Tools;
using BSI.Integra.Aplicacion.Transversal.Validador;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Twilio.TwiML.Voice;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;


namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AsignacionAutomaticaService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AsignacionAutomatica
    /// </summary>
    public class AsignacionAutomaticaService : IAsignacionAutomaticaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private Parser _parser;
       


        private AsignacionAutomatica _asignacionAutomatica;
        private int _idClasificacionPersona;
       public AsignacionAutomatica AsignacionAutomatica { get => _asignacionAutomatica; set => _asignacionAutomatica = value; }
        public int IdClasificacionPersona { get => _idClasificacionPersona; set => _idClasificacionPersona = value; }
        public AsignacionAutomaticaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAsignacionAutomatica, AsignacionAutomatica>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        


   
        }

        #region Metodos Base
        public AsignacionAutomatica Add(AsignacionAutomatica entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionAutomatica>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionAutomatica Update(AsignacionAutomatica entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionAutomatica>(modelo);
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
                _unitOfWork.AsignacionAutomaticaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionAutomatica> Add(List<AsignacionAutomatica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionAutomatica>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionAutomatica> Update(List<AsignacionAutomatica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionAutomatica>>(modelo);
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
                _unitOfWork.AsignacionAutomaticaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AsignacionAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        /// 
        public List<ReporteLandingPagePortalFacebookDTO> ObtenerReporteLandingPagePortalFacebook(FiltroLandingPagePortalFacebookDTO filtros)
        {
            try
            {
               var resultado =  _unitOfWork.AsignacionAutomaticaRepository.ObtenerReporteLandingPagePortalFacebook(filtros);
                var alumnoService = new AlumnoService(_unitOfWork);
                foreach (var item in resultado)
                {
                    if (!string.IsNullOrWhiteSpace(item.Correo))
                        item.Correo = alumnoService.EncriptarCorreoHash(item.Correo);
                    if (!string.IsNullOrWhiteSpace(item.Movil))
                        item.Movil = alumnoService.EncriptarNumeroHash(item.Movil);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionAutomatica ObtenerPorIdFaseOportunidadPortal(string idFaseOportunidadPortal)
        {
            try
            {
                return _unitOfWork.AsignacionAutomaticaRepository.ObtenerPorIdFaseOportunidadPortal(idFaseOportunidadPortal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionAutomatica ObtenerPorId(int idAsignacionAutomatica)
        {
            try
            {
                return _unitOfWork.AsignacionAutomaticaRepository.ObtenerPorId(idAsignacionAutomatica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory ramirez
        /// Fecha: 23/11/2022
        /// <summary>
        /// Valida la lista de asignacionautomatica erroneos
        /// </summary>
        /// <param name="contexto">Objeto de clase integraDBContext</param>
        /// <returns>Lista de objetos de clase AsignacionAutomaticaErrorBO</returns>
        public List<AsignacionAutomaticaError> Validar()
        {

            var listaErrores = new List<AsignacionAutomaticaError>();
            
            //REGLAS DE VALIDACION PARA REGLAS ERRONEAS 
            //-> Validamos el email es correcto
            int idContacto = 0;
            if (_asignacionAutomatica.IdPais == 0)
            {
                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = _asignacionAutomatica.Id,
                    Campo = "Ciudad",
                    Descripcion = "Asigne una ciudad",
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
                return listaErrores;
            }
            string campo = "email";
            try
            {
                Validador.Validador.ValidarEmail(_asignacionAutomatica.Email);
            }
            catch (ValidatorException e)
            {
                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = _asignacionAutomatica.Id,
                    Campo = campo,
                    Descripcion = e.Message,
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
            }
            //Validamos Longitud del Celular & Telefono
            campo = "celular";
            try
            {
                //Realiza la validacion de longitud y en caso no haya error  procede con la validacion de melissa
                Validador.Validador.ValidarLongitudCelular(_asignacionAutomatica.IdPais, _asignacionAutomatica.Celular, _unitOfWork);//validar por pais
            }
            catch (ValidatorException e)
            {
                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = _asignacionAutomatica.Id,
                    Campo = campo,
                    Descripcion = e.Message,
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
            }
            //Validamos Ciudad
            campo = "ciudad";
            try
            {
                if (_asignacionAutomatica.IdCentroCosto == null || _asignacionAutomatica.IdCentroCosto.Value == 0)
                {
                    _asignacionAutomatica.IdCentroCosto = ValorEstatico.IdCentroCostoRegistro2020ILima;
                }
                if (_asignacionAutomatica.IdCentroCosto == null || _asignacionAutomatica.IdCentroCosto.Value == 0)
                {
                    campo = "CentroCosto";
                    throw new ValidatorException("@AAU-V-006#No se encontro centro de costo");
                }
            }
            catch (ValidatorException e)
            {
                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = _asignacionAutomatica.Id,
                    Campo = campo,
                    Descripcion = e.Message,
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
            }
            //Validamos Si el existe el Origen
            campo = "origen";
            try
            {
                if (_asignacionAutomatica.IdOrigen.Equals(0))
                {
                    throw new ValidatorException("#AAS-V-006@No se encontro Origen");
                }
            }
            catch (ValidatorException e)
            {

                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = _asignacionAutomatica.Id,
                    Campo = campo,
                    Descripcion = e.Message,
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
            }
            //REGLAS DE VALIDACION PARA DATOS REPETIDOS->Solo las validamos si no hubo errores tipo erroneo
            if (listaErrores.Count() > 0)
            {
                return listaErrores;
            }
            //Actualizar IdContacto
            var alumnoValidaEmail = _unitOfWork.AlumnoRepository.ObtenerPorEmail(_asignacionAutomatica.Email.ToUpper(), null);
            if (alumnoValidaEmail == null || alumnoValidaEmail.Id == 0)
            {
                alumnoValidaEmail = _unitOfWork.AlumnoRepository.ObtenerPorEmail(null, _asignacionAutomatica.Email.ToUpper());
            }

            if (alumnoValidaEmail != null && alumnoValidaEmail.Id != 0)
            {
                _asignacionAutomatica.IdAlumno = alumnoValidaEmail.Id;
                _asignacionAutomatica.Nombre1 = string.IsNullOrEmpty(_asignacionAutomatica.Nombre1) ? alumnoValidaEmail.Nombre1 : _asignacionAutomatica.Nombre1;
                _asignacionAutomatica.Nombre2 = string.IsNullOrEmpty(_asignacionAutomatica.Nombre2) ? alumnoValidaEmail.Nombre2 : _asignacionAutomatica.Nombre2;
                _asignacionAutomatica.ApellidoPaterno = string.IsNullOrEmpty(_asignacionAutomatica.ApellidoPaterno) ? alumnoValidaEmail.ApellidoPaterno : _asignacionAutomatica.ApellidoPaterno;
                _asignacionAutomatica.ApellidoMaterno = string.IsNullOrEmpty(_asignacionAutomatica.ApellidoMaterno) ? alumnoValidaEmail.ApellidoMaterno : _asignacionAutomatica.ApellidoMaterno;
                _asignacionAutomatica.Telefono = string.IsNullOrEmpty(_asignacionAutomatica.Telefono) ? alumnoValidaEmail.Telefono : _asignacionAutomatica.Telefono;
                _asignacionAutomatica.Celular = string.IsNullOrEmpty(_asignacionAutomatica.Celular) ? alumnoValidaEmail.Celular : _asignacionAutomatica.Celular;
                _asignacionAutomatica.Email = string.IsNullOrEmpty(_asignacionAutomatica.Email) ? alumnoValidaEmail.Email1 : _asignacionAutomatica.Email;
                _asignacionAutomatica.IdPais = _asignacionAutomatica.IdPais == 0 ? alumnoValidaEmail.IdCodigoPais ?? 0 : _asignacionAutomatica.IdPais;
                _asignacionAutomatica.IdCiudad = _asignacionAutomatica.IdCiudad == 0 ? alumnoValidaEmail.IdCodigoRegionCiudad ?? 0 : _asignacionAutomatica.IdCiudad;
                _asignacionAutomatica.IdAreaFormacion = _asignacionAutomatica.IdAreaFormacion.Equals(0) || _asignacionAutomatica.IdAreaFormacion == null ? alumnoValidaEmail.IdAformacion : _asignacionAutomatica.IdAreaFormacion;
                _asignacionAutomatica.IdAreaTrabajo = _asignacionAutomatica.IdAreaTrabajo.Equals(0) || _asignacionAutomatica.IdAreaTrabajo == null ? alumnoValidaEmail.IdAtrabajo : _asignacionAutomatica.IdAreaTrabajo;
                _asignacionAutomatica.IdIndustria = _asignacionAutomatica.IdIndustria.Equals(0) || _asignacionAutomatica.IdIndustria == null ? alumnoValidaEmail.IdIndustria : _asignacionAutomatica.IdIndustria;
                _asignacionAutomatica.IdCargo = _asignacionAutomatica.IdCargo.Equals(0) || _asignacionAutomatica.IdCargo == null ? alumnoValidaEmail.IdCargo : _asignacionAutomatica.IdCargo;

                //Valido la Funcion Calculo Individual
                IPersonaService personaService = new PersonaService(_unitOfWork);

                _idClasificacionPersona = personaService.InsertarPersona(alumnoValidaEmail.Id, TipoPersona.Alumno, "PortalWeb").GetValueOrDefault();
                if (_idClasificacionPersona == 0)
                {
                    throw new Exception("#AAS-V-005@No se creo el persona clasificacion");
                }
            }
            else//CREO EL ALUMNO PARA QUE A CREAR LA OPORTUNIDAD YA TENGA ID
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.Alumno = new Alumno();
                alumnoService.Alumno.Nombre1 = _asignacionAutomatica.Nombre1;
                alumnoService.Alumno.Nombre2 = _asignacionAutomatica.Nombre2;
                alumnoService.Alumno.ApellidoPaterno = _asignacionAutomatica.ApellidoPaterno;
                alumnoService.Alumno.ApellidoMaterno = _asignacionAutomatica.ApellidoMaterno;
                alumnoService.Alumno.Telefono = _asignacionAutomatica.Telefono;
                alumnoService.Alumno.Celular = _asignacionAutomatica.Celular;
                alumnoService.Alumno.Email1 = _asignacionAutomatica.Email;
                alumnoService.Alumno.IdCodigoPais = _asignacionAutomatica.IdPais;
                alumnoService.Alumno.IdCodigoRegionCiudad = _asignacionAutomatica.IdCiudad;
                alumnoService.Alumno.IdCiudad = _asignacionAutomatica.IdCiudad;
                alumnoService.Alumno.IdAformacion = _asignacionAutomatica.IdAreaFormacion;
                alumnoService.Alumno.IdAtrabajo = _asignacionAutomatica.IdAreaTrabajo;
                alumnoService.Alumno.IdIndustria = _asignacionAutomatica.IdIndustria;
                alumnoService.Alumno.IdCargo = _asignacionAutomatica.IdCargo;
                alumnoService.Alumno.IdEmpresa = null;
                alumnoService.Alumno.Estado = true;
                alumnoService.Alumno.UsuarioCreacion = "SYSTEM";
                alumnoService.Alumno.UsuarioModificacion = "SYSTEM";
                alumnoService.Alumno.FechaModificacion = DateTime.Now;
                alumnoService.Alumno.FechaCreacion = DateTime.Now;

                alumnoService.ValidarEstadoContactoWhatsAppTemporal();

                var empresaAlumno = alumnoService.Alumno.IdEmpresa;
                alumnoService.Alumno.IdEmpresa = (empresaAlumno == 0 || empresaAlumno == -1) ? null : empresaAlumno;
                try
                {
                    var respuestaAlumno = _unitOfWork.AlumnoRepository.Add(alumnoService.Alumno);
                    try
                    {
                        _unitOfWork.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw new BadRequestException($"#AAS-V-004@Error en guardar Alumno: {ex.Message}");
                    }
                    alumnoService.Alumno.Id = respuestaAlumno.Id;
                }
                catch (Exception ex)
                {
                    throw new BadRequestException($"#AAS-V-003@Error en crear Alumno: {ex.Message}");
                }
                IPersonaService personaService = new PersonaService(_unitOfWork);
                //Valido la Funcion Calculo Individual
                int? idCreacionCorrecta = personaService.InsertarPersona(alumnoService.Alumno.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Alumno, "PortalWeb");
                //Si boto error en al funcion 
                if (idCreacionCorrecta == null && idCreacionCorrecta != 0)
                {
                    var nombreTablaV3 = "talumnos";
                    var nombreTablaV4 = "mkt.T_Alumno";
                    var resultado = _unitOfWork.AlumnoRepository.EliminarFisicaAlumno(nombreTablaV3, nombreTablaV4, alumnoService.Alumno.Id, null, 0);
                    if (resultado == true)
                    {
                        throw new Exception("#AAS-V-002@Se elimino el alumno");
                    }
                    else
                    {
                        throw new Exception("#AAS-V-001@No se elimino alumno");
                    }
                    //throw new Exception("ocurrio un error NO se pudo Insertar el docente");
                }
                _asignacionAutomatica.IdAlumno = alumnoService.Alumno.Id;
                _idClasificacionPersona = idCreacionCorrecta.Value;
            }
            return listaErrores;
        }
        public IEnumerable<AsignacionAutomaticaCompuestoImportadosDTO> ObtenerRegistrosImportados(FiltroBusquedaAsignacionAutomaticaCompuestoDTO paginador)
        {
            try
            {
                AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                var datos= _unitOfWork.AsignacionAutomaticaRepository.ObtenerRegistrosImportados(paginador);
                foreach (var item in datos) 
                { 
                    if(!string.IsNullOrWhiteSpace(item.Email))
                        item.Email= alumnoService.EncriptarCorreoHash(item.Email);
                    if (!string.IsNullOrWhiteSpace(item.Celular))
                        item.Celular = alumnoService.EncriptarNumeroHash(Regex.Replace(item.Celular, @"[^\d]", ""));
                }

                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<AsignacionAutomaticaCompuestoErroneosDTO> ObtenerRegistrosErroneos(FiltroBusquedaAsignacionAutomaticaCompuestoDTO paginador)
        {
            try
            {
                return _unitOfWork.AsignacionAutomaticaRepository.ObtenerRegistrosErroneos(paginador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ExisteAsignacionAutomatica(int Id)
        {
            try
            {
                return _unitOfWork.AsignacionAutomaticaRepository.ExisteAsignacionAutomatica(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AsignacionAutomatica ObtenerAsignacionAutomaticaPorId(int Id)
        {
            try
            {
                return _unitOfWork.AsignacionAutomaticaRepository.ObtenerAsignacionAutomaticaPorId(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CorregirErroneo(AsignacionAutomaticaCompuestoDTO objeto)
        {
            try
            {
                if (objeto.IdPais == 0)
                {
                    throw new Exception("Se debe seleccionar un país");
                }
                if (objeto.IdCiudad == 0)
                {
                    throw new Exception("Se debe seleccionar un país");
                }
                var ciudadTemp = _unitOfWork.CiudadRepository.FirstBy(x => x.Id == objeto.IdCiudad, x => new { x.LongCelular, x.LongTelefono, x.IdPais });
                objeto.IdPais = ciudadTemp.IdPais;
                objeto.LongTelefono = ciudadTemp.LongTelefono;
                objeto.LongCelular = ciudadTemp.LongCelular;
                if (!objeto.Celular.Substring(0, 1).Equals("0") && objeto.IdPais != 0 && objeto.LongCelular != 0)
                {
                    //Volvemos a validar el registro
                    if (objeto.Celular.Equals(""))
                    {
                        StringBuilder builder = new StringBuilder();
                        for (var i = 0; i < objeto.LongCelular; i++)
                        {
                            builder.Append("0");
                        }
                        objeto.Celular = builder.ToString();
                    }
                    else
                    { //Agregamos Codigos a Movil si existe y es internacional
                        if (objeto.IdPais != 51 && objeto.IdPais != 57 && objeto.IdPais != 591)
                        {
                            objeto.Celular = "00" + objeto.Celular;
                        }
                        else
                        {
                            if (objeto.Celular.StartsWith("51"))
                            {
                                var regex = new Regex(Regex.Escape("51"));
                                objeto.Celular = regex.Replace(objeto.Celular, "", 1);
                            }
                        }
                    }
                }
                // Telefono
                if (objeto.Telefono == "")
                {
                    objeto.Telefono = "0";
                }
                if (!objeto.Telefono.Substring(0, 1).Equals("0") && objeto.IdPais != 0 && objeto.LongTelefono != 0)
                {
                    if (objeto.Telefono.Equals("1"))
                    {
                        StringBuilder builder = new StringBuilder();
                        for (var i = 0; i < objeto.LongTelefono; i++)
                        {
                            builder.Append("0");
                        }
                        objeto.Telefono = builder.ToString();
                    }
                    else
                    { // Agregamos Codigos a Movil si existe y es internacional
                        if (objeto.IdPais != 51)
                        {
                            objeto.Telefono = "00" + objeto.Telefono;
                        }
                        else
                        {
                            objeto.Telefono = "0" + objeto.Telefono;
                        }
                    }
                }
                if (objeto.IdOrigen.Equals("0") || objeto.IdOrigen == 0)
                {
                    int idElSalvador = 503;
                    string elSalvadorIniciales = "SAL";
                    var morigen = new Dictionary<string, int>();
                    var origenes = _unitOfWork.OrigenRepository.GetBy(x => x.Estado == true, x => new { x.Nombre, x.Id });
                    foreach (var item in origenes)
                    {
                        morigen.Add(item.Nombre.Trim().ToUpper(), item.Id);
                    }
                    //Obtenemos los Paises
                    var mpais = new Dictionary<int, string>();
                    var paises = _unitOfWork.PaisRepository.GetBy(x => x.Estado == true, x => new { x.CodigoPais, x.NombrePais });
                    foreach (var pais in paises)
                    {
                        //EL SALVADOR
                        if (pais.CodigoPais == idElSalvador)
                        {
                            mpais.Add(pais.CodigoPais, elSalvadorIniciales);
                        }
                        else
                        {
                            mpais.Add(pais.CodigoPais, pais.NombrePais.Substring(0, 3).ToUpper());
                        }
                    }
                    var categoriadato = _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaOrigenSubCategoriaDato(objeto.IdCategoriaDato.Value, objeto.IdTipoInteraccion.Value);
                    StringBuilder origen = new StringBuilder();
                    origen.Append("LAN").Append(mpais[objeto.IdPais.Value]).Append(categoriadato.CodigoOrigen.ToUpper());
                    var origenNombre = origen.ToString().ToUpper();
                    if (!morigen.ContainsKey(origenNombre))
                    {
                        objeto.IdOrigen = 0;
                    }
                    else
                    {
                        objeto.IdOrigen = morigen[origenNombre];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AsignacionAutomaticaError> ValidarV2(AsignacionAutomaticaCompuestoDTO objeto)
        {
            TPersona _persona = new TPersona();
            var ListaErrores = new List<AsignacionAutomaticaError>();
            var persona = new PersonaService(_unitOfWork);
            var _repAlumno = new AlumnoService(_unitOfWork);
            //REGLAS DE VALIDACION PARA REGLAS ERRONEAS
            //-> Validamos el email es correcto
            int idContacto = 0;
            if (objeto.IdPais == 0)
            {
                AsignacionAutomaticaError error = new AsignacionAutomaticaError();
                error.IdAsignacionAutomatica = objeto.Id;
                error.Campo = "Ciudad";
                error.Descripcion = "Asigne una ciudad";
                error.IdAsignacionAutomaticaTipoError = 1;
                error.IdContacto = idContacto;
                ListaErrores.Add(error);
                return ListaErrores;
            }
            string Campo = "email";
            try
            {
                ValidarEmail(objeto.Email);
            }
            catch (Exception e)
            {
                AsignacionAutomaticaError error = new AsignacionAutomaticaError();
                error.IdAsignacionAutomatica = objeto.Id;
                error.Campo = Campo;
                error.Descripcion = e.Message;
                error.IdAsignacionAutomaticaTipoError = 1;
                error.IdContacto = idContacto;
                ListaErrores.Add(error);
            }
            //Validamos Longitud del Celular & Telefono
            Campo = "celular";
            try
            {
                //validad numero de celular por ciudad
                ValidarLongitudCelular(objeto.IdPais, objeto.Celular);//validar por pais
            }
            catch (Exception e)
            {
                AsignacionAutomaticaError error = new AsignacionAutomaticaError();
                error.IdAsignacionAutomatica = objeto.Id;
                error.Campo = Campo;
                error.Descripcion = e.Message;
                error.IdAsignacionAutomaticaTipoError = 1;
                error.IdContacto = idContacto;
                ListaErrores.Add(error);
            }
            //Validamos Ciudad
            Campo = "ciudad";
            try
            {
                if (objeto.IdCentroCosto == null)
                {
                    objeto.IdCentroCosto = 15907;
                }
                else if (objeto.IdCentroCosto.Value == 0)
                {
                    objeto.IdCentroCosto = 15907;
                }
                if (objeto.IdCentroCosto == 0 || objeto.IdCentroCosto == null)
                {
                    Campo = "CentroCosto";
                    throw new Exception("No se encontro centro de costo");
                }
            }
            catch (Exception e)
            {
                AsignacionAutomaticaError error = new AsignacionAutomaticaError();
                error.IdAsignacionAutomatica = objeto.Id;
                error.Campo = Campo;
                error.Descripcion = e.Message;
                error.IdAsignacionAutomaticaTipoError = 1;
                error.IdContacto = idContacto;
                ListaErrores.Add(error);
            }
            //Validamos Si el existe el Origen
            Campo = "origen";
            try
            {
                if (objeto.IdOrigen.Equals(0))
                {
                    throw new Exception("No se encontro Origen");
                }
            }
            catch (Exception e)
            {
                AsignacionAutomaticaError error = new AsignacionAutomaticaError();
                error.IdAsignacionAutomatica = objeto.Id;
                error.Campo = Campo;
                error.Descripcion = e.Message;
                error.IdAsignacionAutomaticaTipoError = 1;
                error.IdContacto = idContacto;
                ListaErrores.Add(error);
            }
            //REGLAS DE VALIDACION PARA DATOS REPETIDOS->Solo las validamos si no hubo errores tipo erroneo
            if (ListaErrores.Count > 0)
            {
                return ListaErrores;
            }
            //Actualizar IdContacto
            var alumnoValidaEmail = _unitOfWork.AlumnoRepository.ValidarEmailALumno(objeto.Email.ToUpper(), null) ?? _unitOfWork.AlumnoRepository.ValidarEmailALumno(null, objeto.Email.ToUpper());
            //Alumno = Alumno == null ? _repAlumno.ObtenerPor
            if (alumnoValidaEmail != null)
            {
                objeto.IdAlumno = alumnoValidaEmail.Id;
                objeto.Nombre1 = string.IsNullOrEmpty(objeto.Nombre1) ? alumnoValidaEmail.Nombre1 : objeto.Nombre1;
                objeto.Nombre2 = string.IsNullOrEmpty(objeto.Nombre2) ? alumnoValidaEmail.Nombre2 : objeto.Nombre2;
                objeto.ApellidoPaterno = string.IsNullOrEmpty(objeto.ApellidoPaterno) ? alumnoValidaEmail.ApellidoPaterno : objeto.ApellidoPaterno;
                objeto.ApellidoMaterno = string.IsNullOrEmpty(objeto.ApellidoMaterno) ? alumnoValidaEmail.ApellidoMaterno : objeto.ApellidoMaterno;
                objeto.Telefono = string.IsNullOrEmpty(objeto.Telefono) ? alumnoValidaEmail.Telefono : objeto.Telefono;
                objeto.Celular = string.IsNullOrEmpty(objeto.Celular) ? alumnoValidaEmail.Celular : objeto.Celular;
                objeto.Email = string.IsNullOrEmpty(objeto.Email) ? alumnoValidaEmail.Email1 : objeto.Email;
                objeto.IdPais = objeto.IdPais == 0 ? alumnoValidaEmail.IdCodigoPais ?? 0 : objeto.IdPais;
                objeto.IdCiudad = objeto.IdCiudad == 0 ? alumnoValidaEmail.IdCodigoRegionCiudad ?? 0 : objeto.IdCiudad;
                objeto.IdAreaFormacion = objeto.IdAreaFormacion.Equals(0) || objeto.IdAreaFormacion == null ? alumnoValidaEmail.IdAFormacion : objeto.IdAreaFormacion;
                objeto.IdAreaTrabajo = objeto.IdAreaTrabajo.Equals(0) || objeto.IdAreaTrabajo == null ? alumnoValidaEmail.IdATrabajo : objeto.IdAreaTrabajo;
                objeto.IdIndustria = objeto.IdIndustria.Equals(0) || objeto.IdIndustria == null ? alumnoValidaEmail.IdIndustria : objeto.IdIndustria;
                objeto.IdCargo = objeto.IdCargo.Equals(0) || objeto.IdCargo == null ? alumnoValidaEmail.IdCargo : objeto.IdCargo;
                //Valido la Funcion Calculo Individual
                int? idCreacionCorrecta = persona.InsertarPersona(alumnoValidaEmail.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Alumno, "PortalWeb");
                if (idCreacionCorrecta == null)
                {
                    throw new Exception("No se creo el persona clasificacion");
                }
                objeto.IdAlumno = alumnoValidaEmail.Id;
                objeto.idClasificacionPersona = idCreacionCorrecta;
            }
            else//CREO EL ALUMNO PARA QUE A CREAR LA OPORTUNIDAD YA TENGA ID
            {
                var email1 = Regex.Replace(objeto.Email, @"\s", "");
                Alumno alumnoNuevoDTO = new Alumno();
                alumnoNuevoDTO.Nombre1 = objeto.Nombre1;
                alumnoNuevoDTO.Nombre2 = objeto.Nombre2;
                alumnoNuevoDTO.ApellidoPaterno = objeto.ApellidoPaterno;
                alumnoNuevoDTO.ApellidoMaterno = objeto.ApellidoMaterno;
                alumnoNuevoDTO.Telefono = objeto.Telefono;
                alumnoNuevoDTO.Celular = objeto.Celular;
                alumnoNuevoDTO.Email1 = email1;
                alumnoNuevoDTO.IdCodigoPais = objeto.IdPais;
                alumnoNuevoDTO.IdCodigoRegionCiudad = objeto.IdCiudad;
                alumnoNuevoDTO.IdCiudad = objeto.IdCiudad;
                alumnoNuevoDTO.IdAformacion = objeto.IdAreaFormacion;
                alumnoNuevoDTO.IdAtrabajo = objeto.IdAreaTrabajo;
                alumnoNuevoDTO.IdIndustria = objeto.IdIndustria;
                alumnoNuevoDTO.IdCargo = objeto.IdCargo;
                alumnoNuevoDTO.IdEmpresa = null;
                alumnoNuevoDTO.Estado = true;
                alumnoNuevoDTO.UsuarioCreacion = "SYSTEM";
                alumnoNuevoDTO.UsuarioModificacion = "SYSTEM";
                alumnoNuevoDTO.FechaModificacion = DateTime.Now;
                alumnoNuevoDTO.FechaCreacion = DateTime.Now;
                var empresaAlumno = alumnoNuevoDTO.IdEmpresa;
                alumnoNuevoDTO.IdEmpresa = (empresaAlumno == 0 || empresaAlumno == -1) ? null : empresaAlumno;
                var alumnoNuevo = _repAlumno.ValidarEstadoContactoWhatsAppTemporalAlterno(alumnoNuevoDTO);
                //var alumnoNuevo = _repAlumno.MapeoEntidadDesdeDTO(alumnoNuevoDTO);
                var alumnoInsert = _repAlumno.Add(alumnoNuevo);
                objeto.IdAlumno = alumnoInsert.Id;
                //Valido la Funcion Calculo Individual
                int? idCreacionCorrecta = persona.InsertarPersona(alumnoInsert.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Alumno, "PortalWeb");
                //Si boto error en al funcion
                if (idCreacionCorrecta == 0)
                {
                    var nombreTablaV3 = "talumnos";
                    var nombreTablaV4 = "mkt.T_Alumno";
                    var resultado = _repAlumno.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, alumnoInsert.Id, null, 0);
                    if (resultado == true)
                    {
                        throw new Exception("Se elimino el alumno");
                    }
                    else
                    {
                        throw new Exception("No se elimino alumno");
                    }
                    //throw new Exception("ocurrio un error NO se pudo Insertar el docente");
                }
                objeto.idClasificacionPersona = idCreacionCorrecta;
            }
            return ListaErrores;
        }
        public void ValidarEmail(string Email)
        {
            if (!IsEmailValid(Email))
            {
                var Exception = new Exception("Email Inválido");
                throw Exception;
            }
        }
        public bool IsEmailValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public void ValidarLongitudCelular(int? idPais, string celular)
        {
            if (!_unitOfWork.CiudadRepository.LongitudCelularPorPaisCorrecta(idPais, celular))
            {
                var Exception = new Exception("Longitud celular Inválido");
                throw Exception;
            }
        }


        public List<ReporteLandingPagePortalDTO> ObtenerReporteLandingPagePortal(FiltroLandingPagePortalDTO filtros)
        {
            try
            {
                var alumnoService = new AlumnoService(_unitOfWork);
                var resultado = _unitOfWork.AsignacionAutomaticaRepository.ObtenerReporteLandingPagePortal(filtros);

                foreach (var item in resultado)
                {
                    if (!string.IsNullOrWhiteSpace(item.Correo1))
                        item.Correo1 = alumnoService.EncriptarCorreoHash(item.Correo1);

                    if (!string.IsNullOrWhiteSpace(item.Movil))
                        item.Movil = alumnoService.EncriptarNumeroHash(Regex.Replace(item.Movil, @"[^\d]", ""));
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Fecha: 07/05/2021
        /// <summary>
        /// Aplica la configuracion segun la lista de inclusion y exclusion de casos
        /// </summary>
        /// <param name="inclusion">Lista de casos incluidos en el calculo de oportunidades</param>
        /// <param name="exclusion">Lista de casos excluidos en el calculo de oportunidades</param>
        /// <returns>Bool (true)</returns>
        public bool AplicarConfiguracion(List<AsignacionAutomaticaConfiguracionDTO> inclusion, List<AsignacionAutomaticaConfiguracionDTO> exclusion)
        {
            var asigAtomatica = new AsignacionAutomatica();
            foreach (var config in inclusion)
            {
                if (!config.IdFaseOportunidad.Equals(0) && !config.IdFaseOportunidad.Equals(asigAtomatica.IdFaseOportunidad)) return false;
                if (!config.IdOrigen.Equals(0) && !config.IdOrigen.Equals(asigAtomatica.IdOrigen)) return false;
                if (!config.IdTipoDato.Equals(0) && !config.IdTipoDato.Equals(asigAtomatica.IdTipoDato)) return false;
            }
            foreach (var config in exclusion)
            {
                if (!config.IdFaseOportunidad.Equals(0) && config.IdFaseOportunidad.Equals(asigAtomatica.IdFaseOportunidad)) return false;
                if (!config.IdOrigen.Equals(0) && config.IdOrigen.Equals(asigAtomatica.IdOrigen)) return false;
                if (!config.IdTipoDato.Equals(0) && config.IdTipoDato.Equals(asigAtomatica.IdTipoDato)) return false;
            }
            return true;
        }

        /// Versión: 1.0
        /// <summary>
        /// Realiza validaciones al BO para generar el registro en asignacion automatica
        /// </summary>
        /// <param name="idAsignacionAutomaticaTemp">Id de la asignacion automatica temporal (PK de la tabla mkt.T_AsignacionAutomatica_Temp)</param>
        /// <param name="listaOrigenes">Lista de origenes a analizar (PK de la tabla mkt.T_Origen)</param>
        /// <param name="listaPaises">Lista de paises a analizar (PK de la tabla conf.T_Pais)</param>
        public AsignacionAutomatica ValidarRegistroFormularioAsignacionAutomaticaTemp(int idAsignacionAutomaticaTemp, Dictionary<int, string> listaPaises, Dictionary<string, OrigenesCategoriaOrigenDTO> listaOrigenes)
        { 
            AsignacionAutomatica datos=new AsignacionAutomatica();
            
          

            var _repAsignacionAutomaticaTemp = _unitOfWork.AsignacionAutomaticaTempRepository;
            var _repCategoriaOrigen = _unitOfWork.CategoriaOrigenRepository;
            var registro = _repAsignacionAutomaticaTemp.FirstById(idAsignacionAutomaticaTemp);
            if (registro != null)
            {


                Parser _parser = new Parser();
                string preNombres = System.Text.RegularExpressions.Regex.Replace(registro.Nombres, @"\s+", " ");
                var nombres = _parser.ParserCaracteres(preNombres).Split(new char[] { ' ' }).ToList()
                .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
                //Nombres
                if (nombres.Count == 1)
                {
                    datos.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                    datos.Nombre2 = string.Empty;
                }
                else if (nombres.Count == 2)
                {
                    datos.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                    datos.Nombre2 = nombres[1].Length >= 100 ? nombres[1].Substring(0, 100) : nombres[1];
                }
                else if (nombres.Count > 2)
                {
                    datos.Nombre1 = string.Join(" ", nombres.ToArray()).Length >= 100 ? String.Join(" ", nombres.ToArray()).Substring(0, 100) : String.Join(" ", nombres.ToArray());
                    datos.Nombre2 = string.Empty;
                }

                //Apellidos
                registro.Apellidos = registro.Apellidos == null ? "" : registro.Apellidos;
                string preApellidos = System.Text.RegularExpressions.Regex.Replace(registro.Apellidos, @"\s+", " ");
                var apellidos = _parser.ParserCaracteres(preApellidos).Split(new char[] { ' ' }).ToList()
                    .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                if (apellidos.Count == 1)
                {
                    datos.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                    datos.ApellidoMaterno = string.Empty;
                }
                else if (apellidos.Count == 2)
                {
                    datos.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                    datos.ApellidoMaterno = apellidos[1].Length >= 100 ? apellidos[1].Substring(0, 100) : apellidos[1];
                }
                else if (apellidos.Count > 2)
                {
                    datos.ApellidoPaterno = String.Join(" ", apellidos.ToArray()).Length >= 100 ? String.Join(" ", apellidos.ToArray()).Substring(0, 100) : String.Join(" ", apellidos.ToArray());
                    datos.ApellidoMaterno = string.Empty;
                }
                else
                {
                    datos.ApellidoPaterno = string.Empty;
                    datos.ApellidoMaterno = string.Empty;
                }
                //Celular
                datos.Telefono = MapeadorReplace.MapTelefonoCelular(registro.Fijo ?? string.Empty);
                datos.Celular = MapeadorReplace.MapTelefonoCelular(registro.Movil);

                string celularTemporal = string.Empty;

                if (datos.Celular.Length == 0)
                    throw new Exception("Celular no valido");
                //eliminar ceros de adelante del numero si esque los hubiera
                int i = 0;
                for (; i < datos.Celular.Length && datos.Celular[i].Equals('0'); ++i) ;

                try
                {
                    datos.Celular = datos.Celular.Substring(i).Length > 0 ? datos.Celular.Substring(i) : string.Concat("1", new String('0', datos.Celular.Length - 1));
                }
                catch (Exception e)
                {
                    datos.Celular = datos.Celular.Substring(i);
                }

                if (datos.Celular.Length == 12 && registro.IdPais == 52 && datos.Celular.StartsWith("52"))
                    datos.Celular = string.Concat("00", datos.Celular);
                else if (datos.Celular.Length == 10 && registro.IdPais == 52 && !datos.Celular.StartsWith("52"))
                    datos.Celular = string.Concat("0052", datos.Celular);
                else if (datos.Celular.Length == 11 && registro.IdPais == 54 && !datos.Celular.StartsWith("54"))//argentina
                    datos.Celular = string.Concat("0054", datos.Celular);
                else if (datos.Celular.Length == 9 && registro.IdPais == 56 && !datos.Celular.StartsWith("56"))//chile
                    datos.Celular = string.Concat("0056", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 506 && !datos.Celular.StartsWith("506"))//costa rica
                    datos.Celular = string.Concat("00506", datos.Celular);
                else if (datos.Celular.Length == 10 && registro.IdPais == 53 && !datos.Celular.StartsWith("00"))//cuba
                    datos.Celular = string.Concat("00", datos.Celular);
                else if (datos.Celular.Length == 9 && registro.IdPais == 593 && !datos.Celular.StartsWith("593"))//ecuador
                    datos.Celular = string.Concat("00593", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 503 && !datos.Celular.StartsWith("503"))//el salvador
                    datos.Celular = string.Concat("00503", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 502 && !datos.Celular.StartsWith("502"))//guatemala
                    datos.Celular = string.Concat("00502", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 504 && !datos.Celular.StartsWith("504"))//honduras
                    datos.Celular = string.Concat("00504", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 505 && !datos.Celular.StartsWith("505"))//nicaragua
                    datos.Celular = string.Concat("00505", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 507 && !datos.Celular.StartsWith("507"))//panama
                    datos.Celular = string.Concat("00507", datos.Celular);
                else if (datos.Celular.Length == 9 && registro.IdPais == 595 && !datos.Celular.StartsWith("595"))//paraguay
                    datos.Celular = string.Concat("00595", datos.Celular);
                else if (datos.Celular.Length == 7 && registro.IdPais == 1809 && !datos.Celular.StartsWith("1809"))//rep. dominicana
                    datos.Celular = string.Concat("001809", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 598 && !datos.Celular.StartsWith("598"))//uruguay
                    datos.Celular = string.Concat("00598", datos.Celular);
                else if (datos.Celular.Length == 10 && registro.IdPais == 58 && !datos.Celular.StartsWith("58"))//venezuela
                    datos.Celular = string.Concat("58", datos.Celular);

                // asegurar que el dato tenga pais
                if (registro.IdPais == null)
                {
                    if (datos.Celular.Length == 12 && datos.Celular.StartsWith("57")) registro.IdPais = datos.IdPais = 57;
                    else if (datos.Celular.Length == 11 && datos.Celular.StartsWith("591")) registro.IdPais = datos.IdPais = 591;
                    else if (datos.Celular.Length == 11 && datos.Celular.StartsWith("51")) registro.IdPais = datos.IdPais = 51;
                }

                // eliminar el codigo del pais del celular
                if (datos.Celular.Length == 12 && registro.IdPais.Value == 57) datos.Celular = datos.Celular.Substring(2);
                else if (datos.Celular.Length == 11 && registro.IdPais.Value == 591) datos.Celular = datos.Celular.Substring(3);
                else if (datos.Celular.Length == 11 && registro.IdPais.Value == 51) datos.Celular = datos.Celular.Substring(2);


                datos.Email = registro.Correo.Trim();
                datos.IdCentroCosto = registro.IdCentroCosto;

                //asegurarse de que tenga un centro de costo por defecto en caso no se haya podido identificar el centro de costo o el usuario no lo haya registrado
                if (datos.IdCentroCosto == null || datos.IdCentroCosto == 0) datos.IdCentroCosto = 15907;  // CC: "REGISTRO CENTRO DE COSTO 2020 I LIMA"
                else datos.IdCentroCosto = registro.IdCentroCosto;
                datos.NombrePrograma = registro.NombrePrograma;
                datos.IdTipoDato = registro.IdTipoDato;

                //Origen
                StringBuilder origen = new StringBuilder();

                registro.IdCategoriaDato = registro.IdCategoriaDato == null ? 18 : registro.IdCategoriaDato;

                CategoriaOrigenSubCategoriaDatoDTO categoriaDato = _repCategoriaOrigen.ObtenerCategoriaOrigenSubCategoriaDato((registro.IdCategoriaDato == null ? 0 : registro.IdCategoriaDato.Value), registro.IdTipoInteraccion == null ? 0 : registro.IdTipoInteraccion.Value);
                datos.IdSubCategoriaDato = (categoriaDato != null || categoriaDato?.IdSubCategoriaDato != 0) ? categoriaDato?.IdSubCategoriaDato : 0;
                datos.IdCategoriaDato = registro.IdCategoriaDato;
                datos.IdTipoInteraccion = registro.IdTipoInteraccion;
                datos.IdInteraccionFormulario = registro.IdInteraccionFormulario;
                datos.UrlOrigen = registro.UrlOrigen;


                origen.Append("LAN").Append(listaPaises[registro.IdPais ?? default(int)]).Append(categoriaDato?.CodigoOrigen.ToUpper());
                var origenNombre = origen.ToString().ToUpper();
                if (categoriaDato?.IdTipoCategoriaOrigen == 16)
                {
                    if (categoriaDato.NombreCategoriaOrigen.Contains("Offline"))
                        datos.IdOrigen = 132;
                    else
                        datos.IdOrigen = 114;
                }
                else
                {
                    if (!listaOrigenes.ContainsKey(origenNombre))
                    {
                        datos.IdOrigen = 0;
                    }
                    else
                    {
                        datos.IdOrigen = listaOrigenes[origenNombre].Id;
                    }
                }
                datos.OrigenCampania = registro.Origen;
                datos.IdTipoDato = registro.IdTipoDato;
                datos.IdFaseOportunidad = registro.IdFaseOportunidad;
                datos.Email = registro.Correo.Trim();
               
                datos.NombrePrograma = registro.NombrePrograma;
                datos.IdCargo = registro.IdCargo;
                datos.IdIndustria = registro.IdIndustria;
                datos.IdAreaFormacion = registro.IdAreaFormacion;
                datos.IdAreaTrabajo = registro.IdAreaTrabajo;
                datos.IdPais = registro.IdPais;
                datos.IdCiudad = registro.IdCiudad;
                datos.IdConjuntoAnuncio = registro.IdConjuntoAnuncio;
                datos.IdAnuncioFacebook = registro.IdAnuncioFacebook;
                datos.FechaRegistroCampania = registro.FechaRegistroCampania;
                datos.IdFaseOportunidadPortal = registro.IdFaseOportunidadPortal;

                datos.IdTiempoCapacitacion = registro.IdTiempoCapacitacion;
                datos.IdPagina = registro.IdPagina;

                datos.Estado = true;
                datos.FechaCreacion = DateTime.Now;
                datos.FechaModificacion = DateTime.Now;
                datos.UsuarioCreacion = "SYSTEM";
                datos.UsuarioModificacion = "SYSTEM";
                
            }
            return datos;
        }

    }
}
