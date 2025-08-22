using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CronogramaController
    /// Autor: Margiory Ramirez
    /// Fecha: 20/12/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class CronogramaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CronogramaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: --
        /// Fecha:20/12/2022
        /// Versión: 1.0
        /// <summary>
        /// obtiene todas las formas de pago
        /// </summary>
        /// <returns>Json/returns>
        
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFormasPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var serFormaPago = new FormaPagoService(unitOfWork);
                return Ok(serFormaPago.ObtenerFormasPago());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
       //// Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene los detos de matricula en base al parametro recibido
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatricula([FromBody] Dictionary<string, string> Valor) //CodigoMatricula
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serMatricula = new CronogramaService(unitOfWork);
                return Ok(_serMatricula.ObtenerCodigoMatricula(Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        //// Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// retorna el nombre completo del alumno y su id , por el valor del parametro
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoPorValor([FromBody] Dictionary<string, string> Valor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serMatricula = new CronogramaService(unitOfWork);
                return Ok(_serMatricula.ObtenerAlumnoPorValor(Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory RamirezActualizarMoraCAdelanto
        //// Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        ///obtiene el codigo de matricula por codigo de alumno
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerCodigoMatriculaPEspecificoPorAlumnos(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serMatricula = new CronogramaService(unitOfWork);
                return Ok(_serMatricula.ObtenerCodigoMatriculaPEspecificoPorAlumnos(idAlumno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        //// Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene todos los estados de matricula 
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoEstadoMatricula()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serMatricula = new CronogramaService(unitOfWork);
                return Ok(_serMatricula.ObtenerTodoEstadoMatricula());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        //// Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene todos los datos necesarios para un pago
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerDatosPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _documentoPago = new CronogramaService(unitOfWork);
                return Ok(_documentoPago.ObtenerDatosPago());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        ///Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del asesor por el apellido
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAsesorPorApellidos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repAsesor = new CronogramaService(unitOfWork);
                return Ok(_repAsesor.ObtenerAsesorPorApellidos());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        ///Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// onbtiene los datos del coordinador por el apellido
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCoordinadorPorApellidos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repPersonal = new CronogramaService(unitOfWork);
                return Ok(_repPersonal.ObtenerCoordinadorPorApellidos());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        ///Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene las ccuotas no pagadas en base a la version final del cronograma del codigo de matricula
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{CodigoMatricula}/{Version}")]
        [HttpGet]
        public ActionResult ObtenerCuotasNoPagadas(string CodigoMatricula, int Version)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _coutas = new CronogramaService(unitOfWork);
                return Ok(_coutas.ObtenerCuotasNoPagadas(CodigoMatricula,Version));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        ///Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene programas especificos por el nombre del centro de costo
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPEspecificoPorCentroCosto([FromBody] Dictionary<string, string> Valor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _coutas = new CronogramaService(unitOfWork);
                return Ok(_coutas.ObtenerPEspecificoPorCentroCosto(Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        ///Fecha:  17/FF1/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del alumno en base al codigo de matricula
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerAlumnoProgramaEspecifico(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _alumno = new CronogramaService(unitOfWork);
                return Ok(_alumno.ObtenerAlumnoProgramaEspecifico(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        ///Fecha:  17/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los datos de la matricula en base a su codigo
        /// </summary>
        /// <returns>Json/returns>
        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerDatosMatriculaPorCodigoMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _datosMatricula = new CronogramaService(unitOfWork);
                return Ok(_datosMatricula.ObtenerDatosMatriculaPorCodigoMatricula(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        ///Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza los datos de una matricula
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarMatricula([FromBody] MatriculaActualizarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _actualizar = new CronogramaService(unitOfWork);
                return Ok(_actualizar.ActualizarMatricula(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        ///Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene el cronograma valido de una matricula en base a su codigo
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCronograma(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _codigoMatricula = new CronogramaService(unitOfWork);
                return Ok(_codigoMatricula.ObtenerCronograma(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        ///Fecha:  16/05/2025
        /// Versión: 1.0
        /// <summary>
        /// obtiene el cronograma valido de una matricula en base a su codigo
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaFacturacion(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _codigoMatricula = new CronogramaService(unitOfWork);
                return Ok(_codigoMatricula.ObtenerCronogramFacturacion(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        /// TipoFuncion: GET
        /// Autor: Margiory Ramirez
        ///Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Costos Administrativos por Codigo de Matricula
        /// </summary>
        /// <returns> Lista de Costos Administrativos </returns>
        /// <returns> Lista de Objeto DTO : List<CostosAdministrativosDTO> </returns>
        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCostosAdministrativosCodigoMatricula(string CodigoMatricula)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _codigoMatricula = new CronogramaService(unitOfWork);
                return Ok(_codigoMatricula.ObtenerCostosAdministrativosCodigoMatricula(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del ObtenerPersonalAprobadoPorApellido
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPersonal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _personsal = new CronogramaService(unitOfWork);
                return Ok(_personsal.ObtenerTodoPersonal());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtieneel personal vcalidado en base al apellido
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPersonalAprobadoPorApellido([FromBody] Dictionary<string, string> Valor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _personal = new CronogramaService(unitOfWork);
                return Ok(_personal.ObtenerPersonalAprobadoPorApellido(Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }



        /// Tipo Función: PUT
        /// Autor: Margiory Ramirez
        /// Fecha:  18/01/2023
        /// <summary>
        /// Actualiza la forma de pago
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]/{IdCuota}/{IdFormaPago}/{Usuario}")]
        [HttpPut]
        public ActionResult ActualizarFormaPago(int IdCuota, int IdFormaPago, string Usuario)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var _formaPago = new CronogramaService(unitOfWork);
                return Ok(_formaPago.ActualizarFormaPago(IdCuota, IdFormaPago, Usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la fecha de deposito
        /// </summary>
        /// <returns>Json/returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarFechaDeposito([FromBody] PagoActualizadoFechaDepositoDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                Json.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var _fechaDeposito = new CronogramaService(unitOfWork);
                return Ok(_fechaDeposito.ActualizarFechaDeposito(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la gestion de cobranza
        /// </summary>
        /// <returns>Json/returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarGestionDeCobranza([FromBody] PagoActualizadoMoraTarifarioDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                Json.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var _fechaDeposito = new CronogramaService(unitOfWork);
                return Ok(_fechaDeposito.ActualizarGestionDeCobranza(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Agrega las tasas academicas con los datos consignados desde la vista.
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult AgregarTasasAcademicas([FromBody] TasasAcademicasDetalleDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _tasas = new CronogramaService(unitOfWork);
                return Ok(_tasas.AgregarTasasAcademicas(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza lod coucmentos de una matricula
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarEntregaControlDocs([FromBody] ListaControlDocumentosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _control = new CronogramaService(unitOfWork);
                return Ok(_control.ActualizarEntregaControlDocs(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 19/01/2023
        /// Versión: 1.0
        /// <summary>
        /// onbtiene los datos de tasas academicas con sus detalles y precios
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerDetalleTasasAcademicas([FromBody] Dictionary<string, string> Valor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _tasas = new CronogramaService(unitOfWork);
                return Ok(_tasas.ObtenerDetalleTasasAcademicas(Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor:Margiory Ramirez
        /// Fecha: 21/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los documentos de una matricula en base a su codigo y su tipo de modalidad
        /// </summary>
        /// <returns>Json/returns>
        /// 
        [Route("[action]/{CodigoMatricula}/{IdPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerDocumentosMatricula(string CodigoMatricula, int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _codumento = new CronogramaService(unitOfWork);
                return Ok(_codumento.ObtenerDocumentosMatricula(CodigoMatricula, IdPEspecifico));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        /// Tipo Función: POST 
        /// Autor: Margiory ramirez
        /// Fecha: 21/01/2023
        /// Versión: 3.0
        /// <summary>
        /// Guarda todos los cambios del cronograma asi como sus detalles y también los log de este
        /// modificacion : se agrego el campo  UsuarioCoordinadorAcademico a la tabla de cronograma de la matricula
        /// </summary>
        /// <returns>Json</returns>

        [Route("[action]")]
        [HttpPost]
        public object GuardarCronograma(CronogramaModificadoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _cronograma = new CronogramaService(unitOfWork);
                return Ok(_cronograma.GuardarCronograma(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 21/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la fecha de pago
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarFechaPago([FromBody] PagoActualizadoFechaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _fechaPago = new CronogramaService(unitOfWork);
                return Ok(_fechaPago.ActualizarFechaPago(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 21/01/2023
        /// Versión: 2.0
        /// <summary>
        /// Guarda el cambio de moneda en todas las tablas respectivas
        /// modificacion : ahora modifica la moneda en la tabla T_CronogramaPago siempre 
        /// </summary>
        /// <returns>Json</returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarMoraCAdelanto([FromBody] MoraActualizadoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _mora = new CronogramaService(unitOfWork);
                return Ok(_mora.ActualizarMoraCAdelanto(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 21/01/2023
        /// <summary>
        /// Guarda una pago , y modifica las tablas de cronograma a pagado
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public object GuardarPago(PagoCuotaCronogramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var _pago = new CronogramaService(unitOfWork);
                Json.usuario = _respuestaCorrecta.RegistroClaimToken.UserName+"-V5";
                return Ok(_pago.GuardarPagoCuota(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }

        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Versión: 2.0
        /// <summary>
        /// Inhabilita la matrocula y guarda todos los cambios del cronograma asi como sus detalles y también los log de este
        /// modificacion : se agrego el campo  UsuarioCoordinadorAcademico a la tabla de cronograma de la matricula
        /// </summary>
        /// <returns>Json</returns>
        [Route("[action]/{CodigoMatricula}/{Modoeliminacion}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarMatricula(string CodigoMatricula, int Modoeliminacion, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _eliminar = new CronogramaService(unitOfWork);
                return Ok(_eliminar.EliminarMatricula(CodigoMatricula, Modoeliminacion, Usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        //// Fecha:  23/01/2023
        /// Versión: 1.0
        /// <summary>
        /// / obtiene la lista de alumnos matriculado por pograma o por codigo de alumno o por ambos filtros
        /// </summary>
        /// <returns>Json/returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerListadoAlumnosMatricula(EscrituraCrepDTO escrituraCrepDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serMatricula = new CronogramaService(unitOfWork);
                return Ok(_serMatricula.ObtenerListadoAlumnosMatricula(escrituraCrepDTO));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiroy Ramirez
        /// Fecha: 23/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene las cuotas de pago en base al codigo de matricula
        /// </summary>
        /// <returns>Json/returns>

        //lectura crep
        [Route("[action]")]
        [HttpPost]
        public ActionResult ProcesarCDPGFinanzas([FromForm] IFormFile files)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _procesar = new CronogramaPagoDetalleFinalService(unitOfWork);
                return Ok(_procesar.ProcesarCDPGFinanzas(files));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ProcesarPagos([FromBody] List<PagoBancoDTO> ListaPagosBanco)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var _procesar = new CronogramaPagoDetalleFinalService(unitOfWork);
                return Ok(_procesar.ProcesarPago(ListaPagosBanco, usuario));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCuotasCrepPorCodigoMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _coutas = new CronogramaService(unitOfWork);
                return Ok(_coutas.ObtenerCuotasCrepPorCodigoMatricula(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor:Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene las cuentas de pago
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCuentasCorrientes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _coutas = new CronogramaService(unitOfWork);
                return Ok(_coutas.ObtenerCuentasCorrientes());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor:Margiory Ramirez
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera un crep de pago
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarCrep([FromBody] ListaCrepsDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var _coutas = new CronogramaService(unitOfWork);
                return Ok(_coutas.GenerarCrep(Json, usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerDocumentosFiltrado([FromBody] FiltroControlDocumentoDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _doc = new CronogramaService(unitOfWork);
                return Ok(_doc.ObtenerDocumentosFiltrado(Filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        /// Tipo Función: POST
        /// Autor:Margiory Ramirez
        /// Fecha: 31/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la mora de adelanto
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarCambioMonedaCronograma([FromBody] CambioMonedaCronogramaModificadoDTO Json)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _moneda = new CronogramaService(unitOfWork);
                return Ok(_moneda.GuardarCambioMonedaCronograma(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerComboCodigoMatricula([FromBody] Dictionary<string, string> Valor) //CodigoMatricula
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _serMatricula = new CronogramaService(unitOfWork);
                return Ok(_serMatricula.ObtenerComboCodigoMatricula(Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor: Margiori
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// descarga un archivo en base al nombre recibido
        /// </summary>
        /// <returns>Json/returns>


        //escritura crep
        [HttpGet("[action]/{file}")]
        public virtual ActionResult Download(string file)
        {
            string destino = @"C:\Temp\Creps\" + file;
            byte[] reporte_facebook = System.IO.File.ReadAllBytes(destino);
            return File(reporte_facebook, "text/plain", file);
        }

    }
}

