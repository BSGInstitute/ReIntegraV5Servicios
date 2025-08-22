using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RecordatorioWebinarIvrController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de RecordatorioWebinarIvr
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RecordatorioWebinarIvrController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public RecordatorioWebinarIvrController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        ///// Tipo Función: POST
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una insercion basica a la tabla
        ///// </summary>
        ///// <param name="entidad">Entidad a insertar</param>
        ///// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        //[HttpPost("Insertar")]
        //public IActionResult Insertar([FromBody] RecordatorioWebinarIvrRecibidoDTO entidad)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioWebinarIvrService(unitOfWork);
        //            var respuesta = servicio.Add(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }


        //}
        ///// Tipo Función: POST
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una insercion basica a la tabla de una lista
        ///// </summary>
        ///// <param name="listado">Lista de entidades a insertar</param>
        ///// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        //[HttpPost("InsertarLista")]
        //public IActionResult InsertarLista([FromBody] List<RecordatorioWebinarIvr> listado)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioWebinarIvrService(unitOfWork);
        //            var respuesta = servicio.Add(listado);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
        ///// Tipo Función: PUT
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una actualizacion basica a la tabla
        ///// </summary>
        ///// <param name="entidad">Entidad a modificar</param>
        ///// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        //[HttpPut("Actualizar")]
        //public IActionResult Actualizar([FromBody] RecordatorioWebinarIvrRecibidoDTO entidad)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioWebinarIvrService(unitOfWork);
        //            var respuesta = servicio.Update(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
        ///// Tipo Función: PUT
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una actualizacion basica a la tabla de una lista
        ///// </summary>
        ///// <param name="listado">Lista de entidades a actualizar</param>
        ///// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        //[HttpPut("ActualizarLista")]
        //public IActionResult ActualizarLista([FromBody] List<RecordatorioWebinarIvr> listado)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioWebinarIvrService(unitOfWork);
        //            var respuesta = servicio.Update(listado);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
        ///// Tipo Función: DELETE
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una eliminacion logica basica a la tabla
        ///// </summary>
        ///// <param name="id">Id de la entidad a eliminar</param>
        ///// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        ///// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        //[HttpDelete("Eliminar/{id}")]
        //public IActionResult Eliminar(int id)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioWebinarIvrService(unitOfWork);
        //            var respuesta = servicio.Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}
        ///// Tipo Función: DELETE
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Realiza una eliminacion logica basica a la tabla de una lista
        ///// </summary>
        ///// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        ///// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        ///// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        //[HttpDelete("EliminarListado/{listaIds}/{usuario}")]
        //public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        try
        //        {
        //            var servicio = new RecordatorioWebinarIvrService(unitOfWork);
        //            var respuesta = servicio.Delete(listadoIds, _respuestaCorrecta.RegistroClaimToken.UserName);
        //            return Ok(respuesta);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }


        //}
        ///// Tipo Función: GET
        ///// Autor: Griselberto Huaman.
        ///// Fecha: 24/06/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene todos los registros guardados en T_RecordatorioWebinarIvr
        ///// </summary>
        ///// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        //[HttpGet("ObtenerRecordatorioWebinarIvr")]
        //public IActionResult ObtenerRecordatorioWebinarIvr()
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //    if (_respuestaCorrecta.TokenValida)
        //    {
        //        try
        //        {
        //            var servicio = new RecordatorioWebinarIvrService(unitOfWork);
        //            return Ok(servicio.ObtenerRecordatorioWebinarIvr());
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }

        //}

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_RecordatorioWebinarIvr de Id enviado.
        /// </summary>
        /// <returns> RecordatorioWebinarIvr </returns>
        [HttpGet("ObtenerRecordatorioWebinarIvrPorId/{Id}")]
        public IActionResult ObtenerRecordatorioWebinarIvrPorId(int Id)
        {
            try
            {
                var servicio = new RecordatorioWebinarIvrService(unitOfWork);
                return Ok(servicio.ObtenerRecordatorioWebinarIvrPorId(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro para llamada.
        /// </summary>
        /// <returns> List<RecordatorioWebinarIvrDTO> </returns>
        [HttpGet("ObtenerDatoLlamadaRecordatorioWebinar")]
        public IActionResult ObtenerDatoLlamadaRecordatorioWebinar()
        {

            try
            {
                var servicio = new RecordatorioWebinarIvrService(unitOfWork);
                return Ok(servicio.ObtenerDatoLlamadaRecordatorioWebinar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro para llamada.
        /// </summary>
        /// <returns> List<RecordatorioWebinarIvrDTO> </returns>
        [HttpGet("ObtenerDatoLlamadaRecordatorioWebinarPorId/{Id}")]
        public IActionResult ObtenerDatoLlamadaRecordatorioWebinarPorId(int Id)
        {

            try
            {
                var servicio = new RecordatorioWebinarIvrService(unitOfWork);
                return Ok(servicio.ObtenerDatoLlamadaRecordatorioWebinarPorId(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el intento
        /// </summary>
        /// <returns> bool</returns>
        [HttpPut("ActualizarIntento/{Id}")]
        public IActionResult ActualizarIntento(int Id)
        {

            try
            {
                var servicio = new RecordatorioWebinarIvrService(unitOfWork);
                return Ok(servicio.ActualizarIntento(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpPut("ActualizarConcluido/{Id}")]
        public IActionResult ActualizarConcluido(int Id)
        {

            try
            {
                var servicio = new RecordatorioWebinarIvrService(unitOfWork);
                return Ok(servicio.ActualizarConcluido(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpPut("ActualizarIntentoConcluido/{Id}")]
        public IActionResult ActualizarIntentoConcluido(int Id)
        {

            try
            {
                var servicio = new RecordatorioWebinarIvrService(unitOfWork);
                return Ok(servicio.ActualizarIntentoConcluido(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///// Tipo Función: POST
        ///// Fecha: 28/06/2022
        ///// Version: 1.0
        ///// <summary>
        ///// Actualiza el concluido
        ///// </summary>
        ///// <returns> bool</returns>
        [HttpPost("convertirHora")]
        public IActionResult convertirHora(int horas)
        {

            try
            {
                string texto = "tarjeta-123";
                string[] listaDatos = texto.Split("-");
                var objeto = new
                {
                    tarjeta = listaDatos[0],
                    cvv = listaDatos[1]
                };
                return Ok(objeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// tipo función: post
        /// fecha: 28/06/2022
        /// version: 1.0
        /// <summary>
        /// actualiza el concluido
        /// </summary>
        /// <returns> bool</returns>
        [HttpGet("obetneraudio")]
        public IActionResult obetneraudio(string Prueba)
        {
            string nombrelink = string.Empty;
            try
            {
            https://translate.google.com/?sl=es&tl=en&text=
                string apiurl = "https://translate.google.com/translate_tts?ie=utf-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + Prueba + "&tl=es";
                var contador = 0;
                for (int i = 0; i <= 100; i++)
                {
                    using (System.Net.WebClient webclient = new System.Net.WebClient())
                    {
                        try
                        {
                            // realizar solicitud http get y obtener los bytes del archivo
                            byte[] audiobytes = webclient.DownloadData(apiurl);

                            string azurestorageconnectionstring = "defaultendpointsprotocol=https;accountname=repositorioweb;accountkey=jurvlnvfaqg4dcgqcdhej9bkblolv3z/eixa+8qkdtcucwtm1izfgquofuowmdmfnrmrie7nkkho5mpyvtvipa==;endpointsuffix=core.windows.net";
                            string direccionblob = @"repositorioweb/certificados/codigoqr/";

                            //generar entrada al blob storage
                            Microsoft.WindowsAzure.Storage.CloudStorageAccount storageaccount = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(azurestorageconnectionstring);
                            Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient blobclient = storageaccount.CreateCloudBlobClient();
                            Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer container = blobclient.GetContainerReference(direccionblob);
                            Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob blockblob = container.GetBlockBlobReference("audioprueba");
                            blockblob.Properties.ContentType = "audio/mpeg";
                            blockblob.Metadata["filename"] = "audioprueba";
                            Stream stream = new MemoryStream(audiobytes);
                            //asynccallback uploadcompleted = new asynccallback(onuploadcompleted);
                            var objregistrado = blockblob.UploadFromStreamAsync(stream);

                            objregistrado.Wait();
                            var correcto = objregistrado.IsCompletedSuccessfully;

                            if (correcto)
                            {
                                nombrelink = "https://repositorioweb.blob.core.windows.net/" + direccionblob + "audioprueba".Replace(" ", "%20");
                            }
                            else
                            {
                                nombrelink = "";
                            }
                            return Ok(nombrelink);
                        }
                        catch (System.Net.WebException ex)
                        {
                            return Ok($"error al descargar el audio. detalles: {ex.Message}");
                        }
                    }
                    contador = contador + 1;
                }

                return Ok(contador);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
