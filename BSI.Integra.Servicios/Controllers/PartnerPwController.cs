using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PartnerPwController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión de PartnerPw
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PartnerPwController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPartnerPwService _partnerPwService;
        private IUnitOfWork _unitOfWork;

        public PartnerPwController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _partnerPwService = new PartnerPwService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad PartnerPwDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] PartnerPwDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _partnerPwService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] PartnerPwDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _partnerPwService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idPartner}")]
        public IActionResult Eliminar(int idPartner)
        {
            var respuesta = _partnerPwService.Eliminar(idPartner, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id Partner
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idPartner}")]
        public IActionResult ObtenerBeneficioContactoPorId(int idPartner)
        {
            var resultado = _partnerPwService.ObtenerBeneficioContactoPorId(idPartner);
            return Ok(new
            {
                resultado.Beneficios,
                resultado.Contactos
            });
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id Partner
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _partnerPwService.Obtener();
            return Ok(resultado);
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 13/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de T_Partner_PW para combo
        /// </summary>
        /// <returns> </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            var resultado = _partnerPwService.ObtenerCombo();
            return Ok(resultado);
        }

        #region Endpoints para subida de archivos

        /// Tipo Función: POST
        /// Autor: Miguel Valdivia
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Sube el logo de certificación de un partner
        /// </summary>
        /// <param name="idPartner">Id del partner</param>
        /// <param name="archivo">Archivo de imagen</param>
        /// <returns>Retorna 200 con URL del archivo o 400 con mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]/{idPartner}")]
        public IActionResult SubirCertificadoLogo(int idPartner, IFormFile archivo)
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                {
                    return BadRequest("No se ha enviado ningún archivo");
                }

                // Validar que sea una imagen
                var tiposPermitidos = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
                if (!tiposPermitidos.Contains(archivo.ContentType.ToLower()))
                {
                    return BadRequest("Solo se permiten archivos de imagen (jpg, png, gif, webp)");
                }

                // Obtener el partner existente
                var partner = _unitOfWork.PartnerPwRepository.ObtenerPorId(idPartner);
                if (partner == null)
                {
                    return BadRequest($"No se encontró el partner con id {idPartner}");
                }

                // Generar nombre único y subir archivo
                var nombreArchivo = _partnerPwService.GenerarNombreArchivo(archivo.FileName);
                var urlArchivo = _partnerPwService.SubirCertificadoLogo(archivo, nombreArchivo);

                if (string.IsNullOrEmpty(urlArchivo))
                {
                    return BadRequest("Error al subir el archivo al storage");
                }

                // Actualizar el registro en BD (solo el nombre del archivo)
                partner.CertificadoLogo = nombreArchivo;
                partner.UsuarioModificacion = _tokenManager.UserName;
                partner.FechaModificacion = DateTime.Now;
                _unitOfWork.PartnerPwRepository.Update(partner);
                _unitOfWork.Commit();

                return Ok(new
                {
                    NombreArchivo = nombreArchivo,
                    UrlCertificadoLogo = urlArchivo,
                    Mensaje = "Logo de certificación subido correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al subir el logo: {ex.Message}");
            }
        }

        /// Tipo Función: POST
        /// Autor: Miguel Valdivia
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Sube el certificado del partner (PDF)
        /// </summary>
        /// <param name="idPartner">Id del partner</param>
        /// <param name="archivo">Archivo PDF</param>
        /// <returns>Retorna 200 con URL del archivo o 400 con mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]/{idPartner}")]
        public IActionResult SubirCertificadoBSG(int idPartner, IFormFile archivo)
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                {
                    return BadRequest("No se ha enviado ningún archivo");
                }

                // Validar que sea PDF
                if (archivo.ContentType.ToLower() != "application/pdf")
                {
                    return BadRequest("Solo se permiten archivos PDF");
                }

                // Obtener el partner existente
                var partner = _unitOfWork.PartnerPwRepository.ObtenerPorId(idPartner);
                if (partner == null)
                {
                    return BadRequest($"No se encontró el partner con id {idPartner}");
                }

                // Generar nombre único y subir archivo
                var nombreArchivo = _partnerPwService.GenerarNombreArchivo(archivo.FileName);
                var urlArchivo = _partnerPwService.SubirCertificadoBSG(archivo, nombreArchivo);

                if (string.IsNullOrEmpty(urlArchivo))
                {
                    return BadRequest("Error al subir el archivo al storage");
                }

                // Actualizar el registro en BD (solo el nombre del archivo)
                partner.CertificadoBSG = nombreArchivo;
                partner.UsuarioModificacion = _tokenManager.UserName;
                partner.FechaModificacion = DateTime.Now;
                _unitOfWork.PartnerPwRepository.Update(partner);
                _unitOfWork.Commit();

                return Ok(new
                {
                    NombreArchivo = nombreArchivo,
                    UrlCertificadoBSG = urlArchivo,
                    Mensaje = "Certificado del partner subido correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al subir el certificado: {ex.Message}");
            }
        }

        /// Tipo Función: POST
        /// Autor: Miguel Valdivia
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta un partner con archivos usando FormData
        /// </summary>
        /// <param name="dto">Datos del partner con archivos</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarConArchivos([FromForm] PartnerPwEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string nombreLogoArchivo = null;
                string nombreCertificadoArchivo = null;

                // Subir logo de certificación si existe
                if (dto.ArchivoCertificadoLogo != null && dto.ArchivoCertificadoLogo.Length > 0)
                {
                    var tiposPermitidos = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
                    if (!tiposPermitidos.Contains(dto.ArchivoCertificadoLogo.ContentType.ToLower()))
                    {
                        return BadRequest("El logo debe ser una imagen (jpg, png, gif, webp)");
                    }
                    nombreLogoArchivo = _partnerPwService.GenerarNombreArchivo(dto.ArchivoCertificadoLogo.FileName);
                    var urlLogo = _partnerPwService.SubirCertificadoLogo(dto.ArchivoCertificadoLogo, nombreLogoArchivo);
                    if (string.IsNullOrEmpty(urlLogo))
                    {
                        return BadRequest("Error al subir el logo de certificación");
                    }
                }

                // Subir certificado si existe
                if (dto.ArchivoCertificadoBSG != null && dto.ArchivoCertificadoBSG.Length > 0)
                {
                    if (dto.ArchivoCertificadoBSG.ContentType.ToLower() != "application/pdf")
                    {
                        return BadRequest("El certificado debe ser un archivo PDF");
                    }
                    nombreCertificadoArchivo = _partnerPwService.GenerarNombreArchivo(dto.ArchivoCertificadoBSG.FileName);
                    var urlCertificado = _partnerPwService.SubirCertificadoBSG(dto.ArchivoCertificadoBSG, nombreCertificadoArchivo);
                    if (string.IsNullOrEmpty(urlCertificado))
                    {
                        return BadRequest("Error al subir el certificado del partner");
                    }
                }

                // Deserializar beneficios y contactos desde JSON
                List<PartnerBeneficioPwDTO> beneficios = null;
                List<PartnerContactoPwDTO> contactos = null;

                if (!string.IsNullOrEmpty(dto.BeneficiosJson))
                {
                    beneficios = JsonConvert.DeserializeObject<List<PartnerBeneficioPwDTO>>(dto.BeneficiosJson);
                }
                if (!string.IsNullOrEmpty(dto.ContactosJson))
                {
                    contactos = JsonConvert.DeserializeObject<List<PartnerContactoPwDTO>>(dto.ContactosJson);
                }

                // Crear el DTO para insertar
                var partnerDto = new PartnerPwDTO
                {
                    Nombre = dto.Nombre,
                    ImgPrincipal = dto.ImgPrincipal,
                    ImgPrincipalAlf = dto.ImgPrincipalAlf,
                    ImgSecundaria = dto.ImgSecundaria,
                    ImgSecundariaAlf = dto.ImgSecundariaAlf,
                    Descripcion = dto.Descripcion,
                    DescripcionCorta = dto.DescripcionCorta,
                    Preguntas = dto.Preguntas,
                    Posicion = dto.Posicion,
                    IdPartner = dto.IdPartner,
                    EncabezadoCorreoPartner = dto.EncabezadoCorreoPartner,
                    PaginaLink = dto.PaginaLink,
                    CertificadoLogo = nombreLogoArchivo,
                    CertificadoBSG = nombreCertificadoArchivo,
                    Beneficios = beneficios,
                    Contactos = contactos
                };

                var respuesta = _partnerPwService.Insertar(partnerDto, _tokenManager.UserName);

                // Agregar URLs completas a la respuesta
                respuesta.UrlCertificadoLogo = _partnerPwService.ObtenerUrlCertificadoLogo(respuesta.CertificadoLogo);
                respuesta.UrlCertificadoBSG = _partnerPwService.ObtenerUrlCertificadoBSG(respuesta.CertificadoBSG);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al insertar: {ex.Message}");
            }
        }

        /// Tipo Función: PUT
        /// Autor: Miguel Valdivia
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un partner con archivos usando FormData
        /// </summary>
        /// <param name="dto">Datos del partner con archivos</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarConArchivos([FromForm] PartnerPwEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string nombreLogoArchivo = null;
                string nombreCertificadoArchivo = null;

                // Subir logo de certificación si existe
                if (dto.ArchivoCertificadoLogo != null && dto.ArchivoCertificadoLogo.Length > 0)
                {
                    var tiposPermitidos = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
                    if (!tiposPermitidos.Contains(dto.ArchivoCertificadoLogo.ContentType.ToLower()))
                    {
                        return BadRequest("El logo debe ser una imagen (jpg, png, gif, webp)");
                    }
                    nombreLogoArchivo = _partnerPwService.GenerarNombreArchivo(dto.ArchivoCertificadoLogo.FileName);
                    var urlLogo = _partnerPwService.SubirCertificadoLogo(dto.ArchivoCertificadoLogo, nombreLogoArchivo);
                    if (string.IsNullOrEmpty(urlLogo))
                    {
                        return BadRequest("Error al subir el logo de certificación");
                    }
                }

                // Subir certificado si existe
                if (dto.ArchivoCertificadoBSG != null && dto.ArchivoCertificadoBSG.Length > 0)
                {
                    if (dto.ArchivoCertificadoBSG.ContentType.ToLower() != "application/pdf")
                    {
                        return BadRequest("El certificado debe ser un archivo PDF");
                    }
                    nombreCertificadoArchivo = _partnerPwService.GenerarNombreArchivo(dto.ArchivoCertificadoBSG.FileName);
                    var urlCertificado = _partnerPwService.SubirCertificadoBSG(dto.ArchivoCertificadoBSG, nombreCertificadoArchivo);
                    if (string.IsNullOrEmpty(urlCertificado))
                    {
                        return BadRequest("Error al subir el certificado del partner");
                    }
                }

                // Deserializar beneficios y contactos desde JSON
                List<PartnerBeneficioPwDTO> beneficios = null;
                List<PartnerContactoPwDTO> contactos = null;

                if (!string.IsNullOrEmpty(dto.BeneficiosJson))
                {
                    beneficios = JsonConvert.DeserializeObject<List<PartnerBeneficioPwDTO>>(dto.BeneficiosJson);
                }
                if (!string.IsNullOrEmpty(dto.ContactosJson))
                {
                    contactos = JsonConvert.DeserializeObject<List<PartnerContactoPwDTO>>(dto.ContactosJson);
                }

                // Crear el DTO para actualizar
                var partnerDto = new PartnerPwDTO
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    ImgPrincipal = dto.ImgPrincipal,
                    ImgPrincipalAlf = dto.ImgPrincipalAlf,
                    ImgSecundaria = dto.ImgSecundaria,
                    ImgSecundariaAlf = dto.ImgSecundariaAlf,
                    Descripcion = dto.Descripcion,
                    DescripcionCorta = dto.DescripcionCorta,
                    Preguntas = dto.Preguntas,
                    Posicion = dto.Posicion,
                    IdPartner = dto.IdPartner,
                    EncabezadoCorreoPartner = dto.EncabezadoCorreoPartner,
                    PaginaLink = dto.PaginaLink,
                    CertificadoLogo = nombreLogoArchivo,
                    CertificadoBSG = nombreCertificadoArchivo,
                    Beneficios = beneficios,
                    Contactos = contactos
                };

                var respuesta = _partnerPwService.Actualizar(partnerDto, _tokenManager.UserName);

                // Agregar URLs completas a la respuesta
                respuesta.UrlCertificadoLogo = _partnerPwService.ObtenerUrlCertificadoLogo(respuesta.CertificadoLogo);
                respuesta.UrlCertificadoBSG = _partnerPwService.ObtenerUrlCertificadoBSG(respuesta.CertificadoBSG);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar: {ex.Message}");
            }
        }

        #endregion
    }
}
