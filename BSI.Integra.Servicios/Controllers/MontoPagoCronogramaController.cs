using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MontoPagoCronogramaController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/07/2022
    /// <summary>
    /// Gestión de MontoPagoCronograma
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MontoPagoCronogramaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public MontoPagoCronogramaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] MontoPagoCronograma entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoCronogramaService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<MontoPagoCronograma> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoCronogramaService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] MontoPagoCronograma entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoCronogramaService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<MontoPagoCronograma> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoCronogramaService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoCronogramaService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoCronogramaService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MontoPagoCronograma
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerMontoPagoCronograma")]
        public IActionResult ObtenerMontoPagoCronograma()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoCronogramaService(unitOfWork);
                return Ok(servicio.ObtenerMontoPagoCronograma());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MontoPagoCronograma para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoCronogramaService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Cronograma de Pago y relacionados asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="tipoPersonal">Tipo de Personal</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerOportunidadCronogramaPago/{idOportunidad}/{tipoPersonal}")]
        public IActionResult ObtenerOportunidadCronogramaPago(int idOportunidad, string tipoPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MontoPagoCronogramaService servicioCronograma = new MontoPagoCronogramaService(unitOfWork);
                var resumenCronograma = servicioCronograma.ObtenerOportunidadCronogramaPago(idOportunidad, tipoPersonal);
                return Ok(resumenCronograma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 29/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Cronograma de Pago y relacionados asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="tipoPersonal">Tipo de Personal</param>
        /// <returns> Retorna 200 y objeto con estructura personalizada o 400 y mensaje de error </returns>
        [HttpGet("ObtenerOportunidadCronogramaPagoV2/{idOportunidad}/{tipoPersonal}")]
        public IActionResult ObtenerOportunidadCronogramaPagoV2(int idOportunidad, string tipoPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MontoPagoCronogramaService servicioCronograma = new MontoPagoCronogramaService(unitOfWork);
                var resumenCronograma = servicioCronograma.ObtenerOportunidadCronogramaPago(idOportunidad, tipoPersonal);

                if (resumenCronograma.Cronograma == null)
                {
                    return Ok(new { cronograma = (object)null });
                }

                var cronograma = new
                {
                    precio = resumenCronograma.Cronograma.Precio,
                    precioDescuento = resumenCronograma.Cronograma.PrecioDescuento,
                    idTipoDescuento = resumenCronograma.Cronograma.IdTipoDescuento,
                    nombrePlural = resumenCronograma.Cronograma.NombrePlural,
                    estadoMatricula = resumenCronograma.EstadoMatricula,
                    detalle = resumenCronograma.Cronograma.Detalle.Select(x => new
                    {
                        numeroCuota = x.NumeroCuota,
                        montoCuota = x.MontoCuotaDescuento,
                        fechaPago = x.FechaPago,
                        cuotaDescripcion = x.CuotaDescripcion
                    }).ToList()
                };

                return Ok(new { cronograma });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Guarda el cronograma de ventas segun el idAlumno y la oportunidad, se el registro del metodo de pago y el idMatricula de regreso
        /// </summary>
        /// <param name="cronogramaDTO">MontoPagoCronograma a guardar</param>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <param name="idAlumno">Id de Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("GuardarCronogramaVentas/{idOportunidad}/{idAlumno}")]
        public IActionResult GuardarCronogramaVentas([FromBody] MontoPagoCronogramaInterfazDTO cronogramaDTO, int idOportunidad, int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idOportunidad == 0 && idAlumno == 0)
            {
                return BadRequest("Los Ids IdOportunidad y/o IdAlumno son requeridos");
            }
            if (cronogramaDTO == null)
            {
                return BadRequest("Registros de Montos de pagos no puede ser nulo");
            }
            try
            {
                var servicioMontoPagoCronograma = new MontoPagoCronogramaService(unitOfWork);
                var servicioPasarelaPago = new PasarelaPagoPwService(unitOfWork);
                var servicioMontoCronogramaDetalle = new MontoPagoCronogramaDetalleService(unitOfWork);
                var servicioPGeneral = new PGeneralService(unitOfWork);
                var servicioMontoPago = new MontoPagoService(unitOfWork);


                MontoPagoCronogramaDTO cronograma = new MontoPagoCronogramaDTO();
                cronograma.Id = cronogramaDTO.Id;
                cronograma.IdMontoPago = cronogramaDTO.IdMontoPago;
                cronograma.IdPersonal = cronogramaDTO.IdPersonal;
                cronograma.Precio = cronogramaDTO.Precio;
                cronograma.PrecioDescuento = cronogramaDTO.PrecioDescuento;
                cronograma.IdMoneda = cronogramaDTO.IdMoneda;
                cronograma.IdTipoDescuento = cronogramaDTO.IdTipoDescuento;
                cronograma.EsAprobado = cronogramaDTO.EsAprobado;
                cronograma.NombrePlural = cronogramaDTO.NombrePlural;
                cronograma.Formula = cronogramaDTO.Formula;
                cronograma.MatriculaEnProceso = cronogramaDTO.MatriculaEnProceso;
                cronograma.CodigoMatricula = cronogramaDTO.CodigoMatricula;
                cronograma.SimboloMoneda = cronogramaDTO.SimboloMoneda;
                cronograma.CodigoBancario = cronogramaDTO.CodigoBancario;
                cronograma.IdMedioPago = cronogramaDTO.IdMedioPago;
                cronograma.IdOportunidad = idOportunidad;
                cronograma.FechaCreacion = DateTime.Now;
                cronograma.FechaModificacion = DateTime.Now;
                cronograma.UsuarioCreacion = cronogramaDTO.Usuario;
                cronograma.UsuarioModificacion = cronogramaDTO.Usuario;
                var ListaDetalleCuotas = servicioMontoCronogramaDetalle.MapeoEntidadesEntreDTOsDesdeListaDTO(cronogramaDTO.ListaDetalleCuotas);
                cronograma.ListaDetalleCuotas = ListaDetalleCuotas.ToList();
                //Se calcula el Codigo de Matricula , alumno y programa especifico
                var resultadoCodigoMatricula = servicioMontoPagoCronograma.CalcularCodigoMatricula(idAlumno, cronograma);

                //Impide que un alumno se matricule en un programa que no este en lanzamiento
                if (resultadoCodigoMatricula.PEspecificoInformacion.EstadoP != "Lanzamiento" && resultadoCodigoMatricula.PEspecificoInformacion.EstadoP != "Por Ejecucion")
                {
                    return BadRequest("No se puede matricular en un curso que no esta en Lanzamiento o Por Ejecucion.");
                }


                //Impide que el asesor pueda exceder las 2 cuotas adicionales a la la duracion del curso o programa
                //online valida basado en la fecha fin de su ultima sesion
                //aonline basado en la duracion en meses del programa
                //if (resultadoCodigoMatricula.PEspecificoInformacion.Tipo == "Online Sincronica" || resultadoCodigoMatricula.PEspecificoInformacion.Tipo == "Presencial")
                //{
                //    //obtengo la fecha de la ultima sesion
                //    var sesiones = servicioMontoPagoCronograma.ObtenerSesionesOnline(resultadoCodigoMatricula.PEspecificoInformacion.Id);

                //    if (sesiones.Count > 0)
                //    {
                //        var ultimaSesion = sesiones.OrderByDescending(s => s.FechaInicio).First();

                //        var listamatriculasdivididas = cronogramaDTO.ListaDetalleCuotas.Where(w => w.CuotaDescripcion.Contains("matricula")).ToList();
                        
                //        //si la matricula se partio en mas de 3 se le da 1 mes mas (3)
                //        var fechaLimite = new DateTime();
                //        if (listamatriculasdivididas.Count >= 2)
                //        {
                //            fechaLimite = ultimaSesion.FechaInicio.AddMonths(3);
                //        }
                //        else 
                //        {
                //            fechaLimite = ultimaSesion.FechaInicio.AddMonths(2);
                //        }


                //        var ultimaCuota = cronogramaDTO.ListaDetalleCuotas.OrderByDescending(l => l.FechaPago).First();

                //        if (ultimaCuota.FechaPago.Year == fechaLimite.Year)
                //        {

                //            //SE PASO DE LA FECHA LIMITE LA ULTIMA CUOTA
                //            if (ultimaCuota.FechaPago.Month > fechaLimite.Month)
                //            {
                //                return BadRequest("Excede el limite de fechas de 2 cuotas adicionales a la duracion del curso y/o programa");
                //            }

                //        }
                //        else //son años diferentes
                //        {

                //            //si el año de la ultima cuota es mayor
                //            if (ultimaCuota.FechaPago.Year > fechaLimite.Year)
                //            {
                //                return BadRequest("Excede el limite de fechas de 2 cuotas adicionales a la duracion del curso y/o programa");
                //            }
                //            // el año de la ultima cuota es menor continua sin problemas
                //        }
                //    }



                //}
                //else if (resultadoCodigoMatricula.PEspecificoInformacion.Tipo == "Online Asincronica")
                //{


                //    var montoPago = servicioMontoPago.ObtenerPorId(cronogramaDTO.IdMontoPago.Value);
                //    var duraciones = servicioPGeneral.ObtenerDuracionProgramas(resultadoCodigoMatricula.PEspecificoInformacion.IdProgramaGeneral);

                //    ///obtengo la version seleccionada

                //    if (duraciones.Count() > 0)
                //    {
                //        var seleccionado = duraciones.Where(w => w.IdVersionPrograma == montoPago.Paquete).FirstOrDefault();
                //        if (seleccionado != null)
                //        {
                //            ///basado en la cantidad de horas se calcula la cantidad de meses 12.5 horas cada mes aprox
                //            var duracionMeses = Math.Round(seleccionado.Duracion.Value / 12.5, 0, MidpointRounding.ToEven);
                //            var fechaLimite = DateTime.Now.AddMonths(Convert.ToInt32(duracionMeses));
                //            //fechaLimite = fechaLimite.AddMonths(2);
                //            var listamatriculasdivididas = cronogramaDTO.ListaDetalleCuotas.Where(w => w.CuotaDescripcion.Contains("matricula")).ToList();

                //            //si la matricula se partio en mas de 3 se le da 1 mes mas (3)
                //            if (listamatriculasdivididas.Count >= 2)
                //            {
                //                fechaLimite = fechaLimite.AddMonths(3);
                //            }
                //            else
                //            {
                //                fechaLimite = fechaLimite.AddMonths(2);
                //            }

                //            var ultimaCuota = cronogramaDTO.ListaDetalleCuotas.OrderByDescending(l => l.FechaPago).First();

                //            if (ultimaCuota.FechaPago.Year == fechaLimite.Year)
                //            {

                //                //SE PASO DE LA FECHA LIMITE LA ULTIMA CUOTA
                //                if (ultimaCuota.FechaPago.Month > fechaLimite.Month)
                //                {
                //                    return BadRequest("Excede el limite de fechas de 2 cuotas adicionales a la duracion del curso y/o programa");
                //                }

                //            }
                //            else //son años diferentes
                //            {

                //                //si el año de la ultima cuota es mayor
                //                if (ultimaCuota.FechaPago.Year > fechaLimite.Year)
                //                {
                //                    return BadRequest("Excede el limite de fechas de 2 cuotas adicionales a la duracion del curso y/o programa");
                //                }
                //                // el año de la ultima cuota es menor continua sin problemas
                //            }
                //        }
                //        else
                //        {
                //            return BadRequest("No se puede calcular la fecha limite de AOnline porque no hace match el monto pago seleccionado con la lista de version con duracion");
                //        }

                //    }
                //    else
                //    {
                //        return BadRequest("No se puede calcular la fecha limite de AOnline porque no tiene configurado duraciones");
                //    }
                //}

                GuardarCronograma(idOportunidad, idAlumno, ref cronograma);
                var datosUsuario = new DatosUsuarioPortalDTO();
                if (cronograma.EsAprobado)
                {
                    //enlazar alumno
                    datosUsuario = servicioMontoPagoCronograma.EnlazarAlumno(resultadoCodigoMatricula.Alumno);
                    //envia correos
                    cronograma = servicioMontoPagoCronograma.EnviarCorreosFinanzasVentas(resultadoCodigoMatricula, datosUsuario);// Comentado por pruebas.
                }

                var vistaPortalWeb = "";
                if (cronograma != null && datosUsuario.IdAlumno != 0)
                {
                    vistaPortalWeb = "<div class='card'><div class='card-header cabecera_tab'> <span class='panel-title'>Cronograma de Asesor: </span></div><br> <div class='row'> <div class='col-sm-1'></div><div class='col-sm-9'> <nav class='jumbotron' id='header' style='background-color: #094d82 !important;height: 150px;background: #094d82;margin-bottom: 0;' h_cabecera> <div class='container'> <div> <a href='https://bsgrupo.com/' style='width: 163px;height: 192px;background: #afca0a url(https://bsginstitute.com/repositorioweb/img/logo.png) no-repeat center center;position: absolute;top: 0;text-indent: -9999px;z-index: 100;'></a> </div> </div> </nav> <div style=' margin-top: 80px;margin-bottom: 80px;'></div> <div class='bloque-blanco'> <div style='background: #EEEEEE;'> <div class='container' style='padding: 0px;'> <div class='row'> <div class='col-sm-1'></div><div class='col-sm-9' style='text-align:center;'> <div style='background-color: #094D82;padding: 2px;'> <p st_texto style='color:white;'>Cronograma de pagos</p> </div> <br> <br> <div> #tablacuotas </div><br> <br>  </div> <br> <br>";
                    string tabla = "<table class='table table-striped table table-hover'><thead><tr><th> Nro.Cuota </th><th> Moneda </th><th> Monto </th><th> Fecha de vencimiento</th></tr></thead><tbody>";
                    foreach (var item in cronograma.ListaDetalleCuotas.OrderBy(w => w.FechaPago))
                    {
                        tabla = tabla + "<tr>";
                        tabla = tabla + "<td>" + item.NumeroCuota + "</td>";
                        tabla = tabla + "<td>" + cronograma.NombrePlural + "</td>";
                        tabla = tabla + "<td>" + item.MontoCuotaDescuento + "</td>";
                        tabla = tabla + "<td>" + item.FechaPago.ToShortDateString() + "</td>";
                        tabla = tabla + "</tr>";
                    }
                    tabla = tabla + "</tbody></table>";
                    vistaPortalWeb = vistaPortalWeb.Replace("#tablacuotas", tabla);
                }
                //Registrar el metodo de pago
                var idMatriculaCabecera = servicioPasarelaPago.BuscarIdMatriculaCabeceraPorCodigoMatricula(cronograma.CodigoMatricula);
                try
                {
                    if (idMatriculaCabecera > 0 && cronograma.IdMedioPago > 0)
                    {
                        RegistroMedioPagoMatriculaCronogramaDTO model = new RegistroMedioPagoMatriculaCronogramaDTO();
                        model.IdMatriculaCabecera = idMatriculaCabecera;
                        model.IdMedioPago = cronograma.IdMedioPago;
                        model.Usuario = cronograma.UsuarioModificacion;
                        var resultado = servicioPasarelaPago.RegistroMedioPagoMatriculaCronograma(model);
                    }
                    else
                    {
                        idMatriculaCabecera = 0;
                    }
                }
                catch (Exception e)
                {
                    idMatriculaCabecera = 0;
                }
                try
                {
                    var ValidadacionCongelamiento = servicioPasarelaPago.BuscarIdMatriculaCabecera(cronograma.CodigoMatricula);
                    String CodigoMatriculaAlumno = cronograma.CodigoMatricula;

                    if (ValidadacionCongelamiento == 1)
                    {

                        servicioMontoPagoCronograma.EnviarCorreosValidacionCongelamiento(CodigoMatriculaAlumno);// Comentado por pruebas.
                    }
                }
                catch (Exception e)
                {

                }

                return Ok(new { cronograma, vistaPortalWeb, IdMatriculaCabecera = idMatriculaCabecera });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Guarda el Cronograma
        /// </summary>
        /// <param name="idAlumno">Id de Alumno</param>
        /// <returns> CalcularCodigoMatriculaRespuestaDTO </returns>
        private void GuardarCronograma(int idOportunidad, int idAlumno, ref MontoPagoCronogramaDTO cronograma)
        {

            var servicioMontoPagoCronograma = new MontoPagoCronogramaService(unitOfWork);
            var servicioMontoCronogramaDetalle = new MontoPagoCronogramaDetalleService(unitOfWork);

            //using (TransactionScope scope = new TransactionScope())
            //{

                if (cronograma.Id == 0)
                {
                    var entidad = servicioMontoPagoCronograma.MapeoEntidadDesdeDTO(cronograma);
                    var entidadGuardada = servicioMontoPagoCronograma.Add(entidad);
                    cronograma.Id = entidadGuardada.Id;
                }
                else
                {
                    //Delete Orquesta
                    int idCronograma = cronograma.Id;
                    var lista = servicioMontoCronogramaDetalle.ObtenerMontoPagoCronogramaDetallePorIdCronograma(idCronograma).ToList();
                    servicioMontoCronogramaDetalle.Delete(lista.Select(p => p.Id).ToList(), cronograma.UsuarioModificacion);
                    //Fin Delete Orquesta

                    //Update
                    var montoPagoCronogramaActualizar = servicioMontoPagoCronograma.ObtenerPorId(cronograma.Id);
                    var entidad = servicioMontoPagoCronograma.MapeoEntidadDesdeDTO(montoPagoCronogramaActualizar);
                    //servicioMontoPagoCronograma.Add(entidad);
                    entidad.IdMontoPago = cronograma.IdMontoPago;
                    entidad.IdMoneda = cronograma.IdMoneda;
                    entidad.IdTipoDescuento = cronograma.IdTipoDescuento;
                    entidad.Precio = cronograma.Precio;
                    entidad.PrecioDescuento = cronograma.PrecioDescuento;
                    entidad.IdOportunidad = cronograma.IdOportunidad;
                    entidad.IdPersonal = cronograma.IdPersonal;
                    entidad.EsAprobado = cronograma.EsAprobado;
                    entidad.NombrePlural = cronograma.NombrePlural;
                    entidad.Formula = cronograma.Formula;
                    entidad.MatriculaEnProceso = cronograma.MatriculaEnProceso;
                    entidad.CodigoMatricula = cronograma.CodigoMatricula;
                    entidad.FechaModificacion = DateTime.Now;
                    entidad.UsuarioModificacion = cronograma.UsuarioModificacion;

                    servicioMontoPagoCronograma.Update(entidad);
                    //Fin Update
                }
                if (cronograma.ListaDetalleCuotas != null)
                {

                    var entidades = servicioMontoCronogramaDetalle.MapeoEntidadesDesdeListaDTO(cronograma.ListaDetalleCuotas);
                    if (entidades != null)
                    {
                        var cronogramaId = cronograma.Id;
                        var usuarioCreacion = cronograma.UsuarioCreacion;
                        var usuarioModificacion = cronograma.UsuarioModificacion;

                        //se ajusta para que no tenga valor nulo 
                        foreach (var item in cronograma.ListaDetalleCuotas)
                        {
                            item.IdMontoPagoCronograma = cronogramaId;
                        }

                        entidades.ToList().ForEach(item =>
                        {
                            item.Id = 0;
                            item.IdMontoPagoCronograma = cronogramaId;
                            item.Estado = true;
                            item.FechaCreacion = DateTime.Now;
                            item.UsuarioCreacion = usuarioCreacion;
                            item.FechaModificacion = DateTime.Now;
                            item.UsuarioModificacion = usuarioModificacion;
                        });
                    }
                    servicioMontoCronogramaDetalle.Add(entidades.ToList());
                }
                if (cronograma.EsAprobado)
                {
                    servicioMontoPagoCronograma.GenerarCronogramaPorCoordinador(cronograma.Id);
                }
                else
                {
                    servicioMontoPagoCronograma.EliminarCronogramaVentasPorCoordinador(cronograma.Id);
                }

                //scope.Complete();
            //}
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Retorna Detalle Monto Pago
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [Route("[Action]/{idMonto}")]
        [HttpGet]
        public IActionResult ObtenerDetalleMontoPago(int idMonto)
        {
            try
            {
                IMontoPagoCronogramaService montoPagoCronogramaService = new MontoPagoCronogramaService(unitOfWork);
                return Ok(montoPagoCronogramaService.ObtenerDetalleMontoPago(idMonto));
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Congela el Cronograma de alumno por IdCronograma y Usuario.
        /// </summary>
        /// <param name="idCronograma">Id del cronograma</param>
        /// <param name="usuario">Usuario</param>
        /// <returns> DatoEstructuraCurricularDTO </returns>
        [Route("[Action]/{idCronograma}/{usuario}")]
        [HttpPost]
        public IActionResult CongelarCronogramaAlumno(int idCronograma, string usuario)
        {
            try
            {
                var servicioEstructuraEspecifica = new EstructuraEspecificaService(unitOfWork);
                var estructura = servicioEstructuraEspecifica.CongelarEstructuraEspecifica(idCronograma, usuario);
                return Ok(estructura);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 28/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina cronograma de ventas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idAlumno"></param>
        /// <param name="cronogramaDTO"></param>
        /// <returns></returns>
        [Route("[Action]/{idAlumno}")]
        [HttpPost]
        public IActionResult EliminarCronogramaVentas([FromBody] MontoPagoCronogramaDTO cronogramaDTO, int idAlumno)
        {
            try
            {
                MontoPagoCronogramaService servicioMontoPagoCronograma = new MontoPagoCronogramaService(unitOfWork);
                string Firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + cronogramaDTO.Usuario + ".png' />";

                var cronogramaCompleto = servicioMontoPagoCronograma.CalcularCodigoMatricula(idAlumno, cronogramaDTO);
                var matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                var matricula = matriculaCabeceraService.ObtenerPorCodigoMatricula(cronogramaCompleto.Cronograma.CodigoMatricula);
                if (matricula.EstadoMatricula == "matriculado")
                {
                    return Conflict("La matricula ya se encuentra en estado matriculado");
                }

                var existenCuota = servicioMontoPagoCronograma.CuotaPagada(cronogramaCompleto.Cronograma.CodigoMatricula);
                if (Int32.Parse(existenCuota.Resultado.ToString()) == 0)
                {
                    servicioMontoPagoCronograma.GenerarArchivoCrepCronograma(cronogramaCompleto, "Eliminar", Firma);

                    cronogramaDTO.MatriculaEnProceso = 0;
                    servicioMontoPagoCronograma.EliminarCronograma(cronogramaCompleto.Cronograma);
                    cronogramaCompleto = null;
                    return Ok(new { Cronograma = cronogramaCompleto });
                }
                else
                {
                    return BadRequest(existenCuota);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Tipo Función: GET
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
        [Route("[Action]/{idOportunidad}/{tipoPersonal}/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadMontoComplementarios(int idOportunidad, string tipoPersonal, int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var montoPagoCronogramaService = new MontoPagoCronogramaService(unitOfWork);
                var respuesta = montoPagoCronogramaService.ObtenerOportunidadMontoComplementarios(idOportunidad, tipoPersonal, idMatriculaCabecera);
                return Ok(new { Cronograma = respuesta, Descuentos = respuesta.ListaTipoDescuento, MontosComplementarios = respuesta.ListaMontosComplementarios });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 18/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cronograma de Pago del Alumno
        /// </summary>
        /// <returns> Retorna Lista de Objetos</returns>
        [Route("[Action]/{idOportunidad}/{tipoPersona}/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadCronogramaFinanzas(int idOportunidad, string tipoPersona, int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MontoPagoCronogramaService objeto = new MontoPagoCronogramaService(unitOfWork);

                var capturaObjeto = objeto.ObtenerPorIdOportunidadYTipoPersonal(idOportunidad, tipoPersona);
                capturaObjeto.IdOportunidad = idOportunidad;
                capturaObjeto.TipoPersonal = tipoPersona;
                capturaObjeto.MontoPago = null;
                var ListaDetalleCuotas = new List<MontoPagoCronogramaDetalleDTO>();
                var ListaMontosComplementarios = new List<DatosMontosComplementariosDTO>();

                MontoPagoCronogramaService listaTipoDescuento = new MontoPagoCronogramaService(unitOfWork);
                capturaObjeto.ListaTipoDescuento = listaTipoDescuento.ObtenerTipoDescuento(idOportunidad, tipoPersona);

                MontoPagoCronogramaService listaMontosPagosVentas = new MontoPagoCronogramaService(unitOfWork);
                capturaObjeto.ListaMontosPagosVentas = listaMontosPagosVentas.ObtenerMontosPagos(idOportunidad);

                var cronogramaDetalle = new List<CronogramaPagoDetalleFinalFinanzasDTO>();
                MontoPagoCronogramaCompletoDTO MontoPago;
                MontoPagoCronogramaService servicioMontoPagoCronograma = new MontoPagoCronogramaService(unitOfWork);
                CronogramaPagoDetalleFinalService servicioCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalService(unitOfWork);

                var ExisteCronograma = servicioMontoPagoCronograma.ObtenerPorIdOportunidad(idOportunidad);

                string vistaPortalWeb = "";
                if (ExisteCronograma != null && ExisteCronograma.Id != 0)
                {
                    capturaObjeto.Id = ExisteCronograma.Id;
                    capturaObjeto.IdOportunidad = ExisteCronograma.IdOportunidad.Value;
                    capturaObjeto.IdMontoPago = ExisteCronograma.IdMontoPago.Value;
                    capturaObjeto.IdPersonal = ExisteCronograma.IdPersonal.Value;
                    capturaObjeto.Precio = ExisteCronograma.Precio;
                    capturaObjeto.PrecioDescuento = ExisteCronograma.PrecioDescuento;
                    capturaObjeto.IdMoneda = ExisteCronograma.IdMoneda;
                    capturaObjeto.IdTipoDescuento = ExisteCronograma.IdTipoDescuento.Value;
                    capturaObjeto.EsAprobado = ExisteCronograma.EsAprobado;
                    capturaObjeto.NombrePlural = ExisteCronograma.NombrePlural;
                    capturaObjeto.Formula = ExisteCronograma.Formula;
                    capturaObjeto.MatriculaEnProceso = ExisteCronograma.MatriculaEnProceso;
                    capturaObjeto.CodigoMatricula = ExisteCronograma.CodigoMatricula;
                    capturaObjeto.ListaDetalleCuotas = new List<MontoPagoCronogramaDetalleDTO>();
                    MontoPago = new MontoPagoCronogramaCompletoDTO()
                    {
                        Id = capturaObjeto.IdMontoPago,
                        IdOportunidad = capturaObjeto.IdOportunidad,
                        IdMontoPago = capturaObjeto.IdMontoPago,
                        IdPersonal = capturaObjeto.IdPersonal,
                        Precio = capturaObjeto.Precio,
                        PrecioDescuento = capturaObjeto.PrecioDescuento,
                        IdMoneda = capturaObjeto.IdMoneda,
                        IdTipoDescuento = capturaObjeto.IdTipoDescuento,
                        EsAprobado = capturaObjeto.EsAprobado,
                        NombrePlural = capturaObjeto.NombrePlural,
                        Formula = capturaObjeto.Formula,
                        MatriculaEnProceso = capturaObjeto.MatriculaEnProceso,
                        CodigoMatricula = capturaObjeto.CodigoMatricula
                    };

                    var versionAprobada = servicioCronogramaPagoDetalleFinal.ObtenerCronograma(idMatriculaCabecera).FirstOrDefault();

                    vistaPortalWeb = "";
                    cronogramaDetalle = servicioCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzasPorVersionYMCabecera(versionAprobada.Version.Value, idMatriculaCabecera);
                    //CronogramaDetalle = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matricula.Id && w.Version == versionAprobada.Version).OrderBy(w=> w.NroCuota).ToList();
                }
                else
                {
                    MontoPago = new MontoPagoCronogramaCompletoDTO();//Vacio
                    var versionAprobada = servicioCronogramaPagoDetalleFinal.ObtenerCronograma(idMatriculaCabecera).FirstOrDefault();
                    cronogramaDetalle = servicioCronogramaPagoDetalleFinal.ObtenerCronogramaFinanzasPorVersionYMCabecera(versionAprobada.Version.Value, idMatriculaCabecera);
                }
                return Ok(new { Cronograma = capturaObjeto, MontoPago, ListaCronogramaDetalle = cronogramaDetalle, MontosPagosVentas = capturaObjeto.ListaMontosPagosVentas, Descuentos = capturaObjeto.ListaTipoDescuento, vistaPortalWeb });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 20/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cronograma de Pago del Alumno por Codigo Matricula
        /// </summary>
        /// <returns> Retorna ojeto: List<CronogramaPagoDetalleFinalDTO> </returns>
        [Route("[Action]/{codigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadCronogramaFinanzasPorMatricula(string codigoMatricula)
        {
            List<CronogramaPagoDetalleFinalDTO> cronogramaDetalle = new List<CronogramaPagoDetalleFinalDTO>();
            List<CuotaDataAdicionalDTO> cuotasMorasCalculadas = new List<CuotaDataAdicionalDTO>();
            CronogramaPagoDetalleFinalService cronogramaPagoDetalleFinalService = new CronogramaPagoDetalleFinalService(unitOfWork);
            MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);

            var matricula = matriculaCabeceraService.ObtenerPorCodigoMatricula(codigoMatricula);//, w => new { w.Id });
            var versionAprobada = cronogramaPagoDetalleFinalService.ObtenerCronograma(matricula.Id).FirstOrDefault();
            cronogramaDetalle = cronogramaPagoDetalleFinalService.ObtenerCronogramaFinanzas(versionAprobada.Version.Value, matricula.Id);
            cuotasMorasCalculadas = cronogramaPagoDetalleFinalService.ObtenerMorasCalculadas(matricula.Id);
            cuotasMorasCalculadas.ForEach((x) =>
            {
                var temp = cronogramaDetalle.Find(cd => cd.Id == x.IdCuota);
                if (temp != null)
                {
                    temp.MoraCalculada = x.MoraCalculada;
                    temp.Cuota = x.Cuota;
                }
            });

            return Ok(cronogramaDetalle);
        }
    }
}
