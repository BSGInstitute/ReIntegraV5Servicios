using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Twilio.TwiML.Voice;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AsignacionManualController
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 02/11/2022.
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AsignacionManualController : Controller


    {
        private IUnitOfWork unitOfWork;

        public AsignacionManualController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// TipoFuncion: GET
        /// Autor:  Margiory Ramirez Neyra
        /// Fecha: 03/11/2022.
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Filtros para interfaz
        /// </summary>
        /// <returns> Obtiene Información para combos : filtroAsignacionManualDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            var _serviTipoDato = new TipoDatoService(unitOfWork);
            var _serviPgeneral = new PGeneralService(unitOfWork);
            var _serviAreaCapacitacion = new AreaCapacitacionService(unitOfWork);
            var _serviSubAreaCapacitacion = new SubAreaCapacitacionService(unitOfWork);
            var _serviCentroCosto = new CentroCostoService(unitOfWork);
            var _serviCategoriaOrigen = new CategoriaOrigenService(unitOfWork);
            var _serviPaisRepositorio = new PaisService(unitOfWork);
            var _serviProbabilidadRegistroPw = new ProbabilidadRegistroPwService(unitOfWork);
            var _serviFaseOportunidad = new FaseOportunidadService(unitOfWork);
            var _serviPersonal = new PersonalService(unitOfWork);
            var _serviOrigen = new OrigenService(unitOfWork);
            var _serviTipoCategoriaOrigen = new TipoCategoriaOrigenService(unitOfWork);
            var _serviOperadorComparacion = new OperadorComparacionService(unitOfWork);

            try
            {
                FiltroAsignacionManualDTO filtroAsignacionManual = new FiltroAsignacionManualDTO()
                {
                    filtroPersonal = _serviPersonal.CargarPersonalParaFiltro(),
                    filtroCentroCosto = _serviCentroCosto.ObtenerCombo().ToList(),
                    filtroFaseOportunidad = _serviFaseOportunidad.ObtenerFaseOportunidadTodoFiltro(),
                    filtroSubAreaCapacitacion = _serviSubAreaCapacitacion.ObtenerCombo(),
                    filtroAreaCapacitacion = _serviAreaCapacitacion.ObtenerCombo(),
                    filtroPgeneral = _serviPgeneral.ObtenerProgramasFiltro(),
                    filtroTipoDato = _serviTipoDato.ObtenerCombo(),
                    filtroCategoriaOrigen = _serviCategoriaOrigen.ObtenerCategoriaFiltro(),
                    filtroPais = _serviPaisRepositorio.ObtenerComboConZonaHoraria(),
                    filtroProbabilidad = _serviProbabilidadRegistroPw.ObtenerTodoFiltro(),
                    filtroOrigen = _serviOrigen.ObtenerTodoFiltro(),
                    filtroTipoCategoriaOrigen = _serviTipoCategoriaOrigen.ObtenerTodoFiltro(),
                    filtroOperadorComparacion = _serviOperadorComparacion.ObtenerCombo()
                };



                return Ok(filtroAsignacionManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor:Margiory Ramirez Neyra.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Oportunidades
        /// </summary>
        /// <returns> Obtiene Lista de Registros de Oportunidades y su información : ResultadoAsignacionManualFiltroTotalDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidades([FromBody] AsignacionAutomaticaManualOportunidadFiltroGrillaDTO obj)
        {
            try
            {
                var _serviOportunidad = new OportunidadService(unitOfWork);
                var _serviOperadorComparacion = new OperadorComparacionService(unitOfWork);

                var oportunidadManual = _serviOportunidad.ObtenerPorFiltroPaginaManual(
                    obj.filtro,
                    obj.filter,
                    _serviOperadorComparacion.ObtenerListado());

                //foreach (var item in oportunidadManual.data)
                //{
                //    item.Email = EncriptarStringCorreo(item.Email);
                //}
                var alumnoService = new AlumnoService(unitOfWork);
                foreach (var item in oportunidadManual.data)
                {
                    if (!string.IsNullOrWhiteSpace(item.Email))
                        item.Email = alumnoService.EncriptarCorreoHash(item.Email);
                }

                return Ok(oportunidadManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 03/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Encripta correos
        /// </summary>
        /// <returns> string </returns>

        [Route("[Action]")]
        [HttpGet]
        public string EncriptarStringCorreo(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int posicion = parametro.IndexOf("@");

                if (posicion > 0)
                {
                    respuesta = new string('x', posicion) + parametro.Remove(0, posicion);
                }
            }
            return respuesta;
        }

        /// Autor:Margiory Ramirez Neyra.
        /// Fecha: 05/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Asignación de oportunidades por Asesor
        /// </summary>
        /// <param name="AsignarAsesor"> Lista de Id de oportunidades asignado a asesor </param>
        /// <param name="Usuario"> Usuario de módulo </param>
        /// <param name="OportunidadLogNueva"> Usuario de módulo </param>
        /// <returns> Confirmación de asignación : Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult AsignarAsesor([FromBody] AsignarAsesorManualParametroDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.AsignarAsesor(data.AsignarAsesor, (data.Usuario + "V5")));

            //IAgendaReprogramacionService servicioPW = new AgendaReprogramacionService(unitOfWork);
            //var resultado = servicio.AsignarAsesor(data.AsignarAsesor, (data.Usuario + "V5"));

            /////Envio de correo  “Correo Información del Curso Completo”
            //servicioPW.EnviarPlantillaAsync(servicio.listaOportunidadesTemp);

            //return Ok(resultado);

        }
        /// Autor:Margiory Ramirez
        /// Fecha: 28/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Asignación de oportunidades por Asesor
        /// </summary>
        /// <param name="AsignarAsesor"> Lista de Id de oportunidades asignado a asesor </param>
        /// <param name="Usuario"> Usuario de módulo </param>
        /// <param name="OportunidadLogNueva"> Usuario de módulo </param>
        /// <returns> Confirmación de asignación : Bool </returns>

        [Route("[Action]")]
        [HttpPost]
        public ActionResult AsignarAsesorFechaProgramacion([FromBody] AsignarAsesorManualParametroWhtasapDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.AsignarAsesorFechaProgramacion(data.AsignarAsesor, (data.Usuario + "V5")));

        }
        /// Autor:Carlos Crispin Riquelme.
        /// Fecha: 05/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Regularizar el envio de whatsapp a los clientes de asesores que tienen chip asignado
        /// <returns> Confirmación de asignación : Bool </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult RegularizacionEnvioWhatsappAsignacion()
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.RegularizacionEnvioWhatsappAsignacion());

        }



        /// TipoFuncion: POST
        /// Autor: Margiory Ramirez  Neyra.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OD
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CerrarOportunidadOD([FromBody] CerrarOportunidadODDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.CerrarOportunidadOD(data.IdOportunidades, data.Usuario));
        }


        /// TipoFuncion: POST
        /// Autor: Margiory Ramirez  Neyra.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OD
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CerrarOportunidadOM([FromBody] CerrarOportunidadODDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.CerrarOportunidadOM(data.IdOportunidades, data.Usuario));
        }


        /// TipoFuncion: POST
        /// Autor: Margiory Ramirez  Neyra.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OD
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CerrarOportunidadRN5([FromBody] CerrarOportunidadODDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.CerrarOportunidadRN5(data.IdOportunidades, data.Usuario));
        }



        /// TipoFuncion: POST
        /// Autor: Margiory Ramirez  Neyra.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OD
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CerrarOportunidadE([FromBody] CerrarOportunidadODDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.CerrarOportunidadE(data.IdOportunidades, data.Usuario));
        }



        /// TipoFuncion: POST
        /// Autor: Margiory Ramirez  Neyra.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OD
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CerrarOportunidadBic([FromBody] CerrarOportunidadODDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.CerrarOportunidadBic(data.IdOportunidades, data.Usuario));
        }


        /// TipoFuncion: POST
        /// Autor: Margiory Ramirez  Neyra.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OD
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CerrarOportunidadBRM1([FromBody] CerrarOportunidadODDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.CerrarOportunidadBRM1(data.IdOportunidades, data.Usuario));
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 06/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Oportunidad en NS
        /// </summary>
        /// <returns> Confirmación de inserción : Bool </returns>


        [Route("[Action]")]
        [HttpPost]
        public ActionResult CerrarOportunidadNS([FromBody] CerrarOportunidadODDTO data)
        {
            var servicio = new AsignacionManualService(unitOfWork);
            return Ok(servicio.CerrarOportunidadNS(data.IdOportunidades, data.Usuario));
        }

    }
}










