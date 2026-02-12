using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PartnerPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_PartnerPw
    /// </summary>
    public class PartnerPwService : IPartnerPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        private const string URL_BASE_BLOB = "http://repositorioweb.blob.core.windows.net/";
        private const string RUTA_BLOB_LOGOS = "repositorioweb/partners/logos/";
        private const string RUTA_BLOB_CERTIFICADOS = "repositorioweb/partners/certificados/";
        private const string CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

        public PartnerPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPartnerPw, PartnerPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPartnerPw, PartnerPwDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerPw, PartnerPwDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPartnerBeneficioPw, PartnerBeneficioPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerBeneficioPwDTO, TPartnerBeneficioPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerBeneficioPwDTO, PartnerBeneficioPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPartnerContactoPw, PartnerContactoPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerContactoPwDTO, TPartnerContactoPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<PartnerContactoPwDTO, PartnerContactoPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro Partner PW
        /// </summary>
        /// <returns> Lista PartnerPwDTO </returns>
        public IEnumerable<PartnerPwDTO> Obtener()
        {
            var lista = _unitOfWork.PartnerPwRepository.Obtener().ToList();
            // Construir URLs completas para cada registro
            foreach (var item in lista)
            {
                item.UrlCertificadoLogo = ObtenerUrlCertificadoLogo(item.CertificadoLogo);
                item.UrlCertificadoBSG = ObtenerUrlCertificadoBSG(item.CertificadoBSG);
            }
            return lista;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id Partner
        /// </summary>
        /// <param name="idPartner">Id Partner</param>
        /// <returns> Lista PartnerBeneficioPwDTO, Lista Contactos </returns>
        public (IEnumerable<PartnerBeneficioPwDTO> Beneficios, IEnumerable<PartnerContactoPwDTO> Contactos) ObtenerBeneficioContactoPorId(int idPartner)
        {
            try
            {
                if (idPartner == 0)
                {
                    throw new BadRequestException("Id 0 no valido");
                }
                var beneficios = _unitOfWork.PartnerBeneficioPwRepository.ObtenerPorIdPartner(idPartner);
                var contactos = _unitOfWork.PartnerContactoPwRepository.ObtenerPorIdPartner(idPartner);
                return (_mapper.Map<IEnumerable<PartnerBeneficioPwDTO>>(beneficios), _mapper.Map<IEnumerable<PartnerContactoPwDTO>>(contactos));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo PartnerPw
        /// </summary>
        /// <param name="dto">PartnerPw</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PartnerPwDTO</returns>
        public PartnerPwDTO Insertar(PartnerPwDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PartnerPw entidad = new()
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
                        CertificadoLogo = dto.CertificadoLogo,
                        CertificadoBSG = dto.CertificadoBSG,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.PartnerPwRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<PartnerPwDTO>(respuesta);

                    if (dto.Beneficios != null && dto.Beneficios.Count() > 0)
                    {
                        var partnerBeneficios = dto.Beneficios.Select(x => new PartnerBeneficioPw
                        {
                            IdPartner = entidad.Id,
                            Descripcion = x.Descripcion,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        });
                        var res = _unitOfWork.PartnerBeneficioPwRepository.Add(partnerBeneficios);
                        _unitOfWork.Commit();
                        resultado.Beneficios = _mapper.Map<List<PartnerBeneficioPwDTO>>(res);
                    }
                    if (dto.Contactos != null && dto.Contactos.Count() > 0)
                    {
                        var partnerContactos = dto.Contactos.Select(x => new PartnerContactoPw
                        {
                            IdPartner = entidad.Id,
                            Nombres = x.Nombres,
                            Apellidos = x.Apellidos,
                            Email1 = x.Email1,
                            Email2 = x.Email2,
                            Telefono1 = x.Telefono1,
                            Telefono2 = x.Telefono2,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        });
                        var res = _unitOfWork.PartnerContactoPwRepository.Add(partnerContactos);
                        _unitOfWork.Commit();
                        resultado.Contactos = _mapper.Map<List<PartnerContactoPwDTO>>(res);
                    }
                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un PartnerPw
        /// </summary>
        /// <param name="dto">PartnerPw</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>PartnerPwDTO</returns>
        public PartnerPwDTO Actualizar(PartnerPwDTO dto, string usuario)
        {
            try
            {
                PartnerPw? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PartnerPwRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.ImgPrincipal = dto.ImgPrincipal;
                            entidad.ImgPrincipalAlf = dto.ImgPrincipalAlf;
                            entidad.ImgSecundaria = dto.ImgSecundaria;
                            entidad.ImgSecundariaAlf = dto.ImgSecundariaAlf;
                            entidad.Descripcion = dto.Descripcion;
                            entidad.DescripcionCorta = dto.DescripcionCorta;
                            entidad.Preguntas = dto.Preguntas;
                            entidad.Posicion = dto.Posicion;
                            entidad.IdPartner = dto.IdPartner;
                            entidad.EncabezadoCorreoPartner = dto.EncabezadoCorreoPartner;
                            // Nuevos campos
                            entidad.PaginaLink = dto.PaginaLink;
                            // Solo actualizar si se envió un nuevo valor (no sobrescribir con null si no se envió archivo)
                            if (!string.IsNullOrEmpty(dto.CertificadoLogo))
                                entidad.CertificadoLogo = dto.CertificadoLogo;
                            if (!string.IsNullOrEmpty(dto.CertificadoBSG))
                                entidad.CertificadoBSG = dto.CertificadoBSG;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PartnerPwRepository.Update(entidad);
                            _unitOfWork.Commit();

                            var listaBeneficios = _unitOfWork.PartnerBeneficioPwRepository.ObtenerPorIdPartner(entidad.Id).ToList();
                            if (listaBeneficios != null && listaBeneficios.Count() > 0)
                            {
                                if (dto.Beneficios != null && dto.Beneficios.Count() > 0)
                                {
                                    listaBeneficios.RemoveAll(s => dto.Beneficios.Any(x => x.Id == s.Id));
                                }
                                if (listaBeneficios.Count() > 0)
                                {
                                    _unitOfWork.PartnerBeneficioPwRepository.Delete(listaBeneficios.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }
                            }
                            var listaContactos = _unitOfWork.PartnerContactoPwRepository.ObtenerPorIdPartner(entidad.Id).ToList();
                            if (listaContactos != null && listaContactos.Count() > 0)
                            {
                                if (dto.Contactos != null && dto.Contactos.Count() > 0)
                                {
                                    listaContactos.RemoveAll(s => dto.Contactos.Any(x => x.Id == s.Id));
                                }
                                if (listaContactos.Count() > 0)
                                {
                                    _unitOfWork.PartnerContactoPwRepository.Delete(listaContactos.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }
                            }
                            if (dto.Beneficios != null && dto.Beneficios.Count() > 0)
                            {
                                dto.Beneficios.ForEach(beneficio =>
                                {
                                    PartnerBeneficioPw partnerBeneficioPw;
                                    if (beneficio.Id != 0 && _unitOfWork.PartnerBeneficioPwRepository.Exist(beneficio.Id))
                                    {
                                        partnerBeneficioPw = _unitOfWork.PartnerBeneficioPwRepository.ObtenerPorId(beneficio.Id)!;
                                        partnerBeneficioPw.Descripcion = beneficio.Descripcion;
                                        partnerBeneficioPw.UsuarioModificacion = usuario;
                                        partnerBeneficioPw.FechaModificacion = DateTime.Now;
                                        _unitOfWork.PartnerBeneficioPwRepository.Update(partnerBeneficioPw);
                                        _unitOfWork.Commit();
                                    }
                                    else
                                    {
                                        partnerBeneficioPw = new PartnerBeneficioPw()
                                        {
                                            IdPartner = entidad.Id,
                                            Descripcion = beneficio.Descripcion,
                                            Estado = true,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                        };
                                        var resultado = _unitOfWork.PartnerBeneficioPwRepository.Add(partnerBeneficioPw);
                                        _unitOfWork.Commit();
                                        beneficio.Id = resultado.Id;
                                    }
                                });
                            }
                            if (dto.Contactos != null && dto.Contactos.Count() > 0)
                            {
                                dto.Contactos.ForEach(contacto =>
                                {
                                    PartnerContactoPw partnerContactoPw;
                                    if (contacto.Id != 0 && _unitOfWork.PartnerContactoPwRepository.Exist(contacto.Id))
                                    {
                                        partnerContactoPw = _unitOfWork.PartnerContactoPwRepository.ObtenerPorId(contacto.Id)!;
                                        partnerContactoPw.Nombres = contacto.Nombres;
                                        partnerContactoPw.Apellidos = contacto.Apellidos;
                                        partnerContactoPw.Email1 = contacto.Email1;
                                        partnerContactoPw.Email2 = contacto.Email2;
                                        partnerContactoPw.Telefono1 = contacto.Telefono1;
                                        partnerContactoPw.Telefono2 = contacto.Telefono2;
                                        partnerContactoPw.UsuarioModificacion = usuario;
                                        partnerContactoPw.FechaModificacion = DateTime.Now;
                                        _unitOfWork.PartnerContactoPwRepository.Update(partnerContactoPw);
                                        _unitOfWork.Commit();
                                    }
                                    else
                                    {
                                        partnerContactoPw = new PartnerContactoPw()
                                        {
                                            IdPartner = entidad.Id,
                                            Nombres = contacto.Nombres,
                                            Apellidos = contacto.Apellidos,
                                            Email1 = contacto.Email1,
                                            Email2 = contacto.Email2,
                                            Telefono1 = contacto.Telefono1,
                                            Telefono2 = contacto.Telefono2,
                                            Estado = true,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                        };
                                        var resultado = _unitOfWork.PartnerContactoPwRepository.Add(partnerContactoPw);
                                        _unitOfWork.Commit();
                                        contacto.Id = resultado.Id;
                                    }
                                });
                            }
                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina el registro partner por id
        /// </summary>
        /// <param name="id">Id Partner</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.PartnerPwRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PartnerPwRepository.Delete(id, usuario);
                    var idsBeneficios = _unitOfWork.PartnerBeneficioPwRepository.ObtenerPorIdPartner(id).Select(x => x.Id);
                    var idsContactos = _unitOfWork.PartnerContactoPwRepository.ObtenerPorIdPartner(id).Select(x => x.Id);
                    if (idsBeneficios != null && idsBeneficios.Count() > 0)
                    {
                        _unitOfWork.PartnerBeneficioPwRepository.Delete(idsBeneficios, usuario);
                    }
                    if (idsContactos != null && idsContactos.Count() > 0)
                    {
                        _unitOfWork.PartnerContactoPwRepository.Delete(idsContactos, usuario);
                    }
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 13/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro Partner PW para combo
        /// </summary>
        /// <returns> Lista PartnerPwDTO </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            return _unitOfWork.PartnerPwRepository.ObtenerCombo();
        }

        #region Métodos de Blob Storage

        /// <summary>
        /// Convierte IFormFile a byte[]
        /// Copiado de SolicitudAlumnoService.cs
        /// </summary>
        public byte[] ConvertToByte(IFormFile file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.OpenReadStream());
            imageByte = rdr.ReadBytes((int)file.Length);
            return imageByte;
        }

        /// <summary>
        /// Limpia el nombre del archivo quitando caracteres inválidos
        /// Copiado de SolicitudAlumnoService.cs
        /// </summary>
        public string SlugNombreArchivo(string textoOriginal)
        {
            string extension = textoOriginal.Substring(textoOriginal.LastIndexOf("."));
            string texto = textoOriginal;

            // Caracteres inválidos
            texto = Regex.Replace(texto, @"[^a-zA-Z0-9\s-]", "");
            texto = texto.Replace("+", " ");
            texto = texto.Replace("-", " ");

            // Convierte múltiples espacios
            texto = Regex.Replace(texto, @"\s+", " ").Trim();
            texto = texto.Trim();
            texto = texto + extension;

            return texto;
        }

        /// <summary>
        /// Genera un nombre único para el archivo: timestamp + nombre original limpio
        /// Siguiendo el patrón de SolicitudAlumnoController.cs
        /// </summary>
        public string GenerarNombreArchivo(string nombreOriginal)
        {
            // Formato: yyyyMMdd-HHmmss-nombrearchivo.extension
            return string.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-", SlugNombreArchivo(nombreOriginal));
        }

        /// <summary>
        /// Sube el logo de certificación al Blob Storage
        /// Copiado de SolicitudAlumnoService.SubirArchivoSolicitudAlumnoRepositorio()
        /// </summary>
        /// <param name="archivoEntrada">Archivo de imagen</param>
        /// <param name="nombreArchivo">Nombre del archivo a guardar (ya procesado con GenerarNombreArchivo)</param>
        /// <returns>URL del archivo subido o string vacío si falla</returns>
        public string SubirCertificadoLogo(IFormFile archivoEntrada, string nombreArchivo)
        {
            try
            {
                var archivo = ConvertToByte(archivoEntrada);
                string _nombreLink = string.Empty;

                // Elimina los caracteres con tilde
                nombreArchivo = nombreArchivo.Replace("á", "a");
                nombreArchivo = nombreArchivo.Replace("é", "e");
                nombreArchivo = nombreArchivo.Replace("í", "i");
                nombreArchivo = nombreArchivo.Replace("ó", "o");
                nombreArchivo = nombreArchivo.Replace("ú", "u");

                nombreArchivo = nombreArchivo.Replace("Á", "A");
                nombreArchivo = nombreArchivo.Replace("É", "E");
                nombreArchivo = nombreArchivo.Replace("Í", "I");
                nombreArchivo = nombreArchivo.Replace("Ó", "O");
                nombreArchivo = nombreArchivo.Replace("Ú", "U");

                // Elimina las Ñ
                nombreArchivo = nombreArchivo.Replace("ñ", "n");
                nombreArchivo = nombreArchivo.Replace("Ñ", "N");

                try
                {
                    // TODO: Reemplazar por el connection string real (idealmente desde appsettings.json)
                    string _azureStorageConnectionString = CONNECTION_STRING;
                    string _direccionBlob = RUTA_BLOB_LOGOS;

                    // Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = archivoEntrada.ContentType;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = archivoEntrada.ContentType;
                    Stream stream = new MemoryStream(archivo);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = URL_BASE_BLOB + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }
                    return _nombreLink;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }

        /// <summary>
        /// Sube el certificado del partner (PDF) al Blob Storage
        /// Copiado de SolicitudAlumnoService.SubirArchivoSolicitudAlumnoRepositorio()
        /// </summary>
        /// <param name="archivoEntrada">Archivo PDF</param>
        /// <param name="nombreArchivo">Nombre del archivo a guardar (ya procesado con GenerarNombreArchivo)</param>
        /// <returns>URL del archivo subido o string vacío si falla</returns>
        public string SubirCertificadoBSG(IFormFile archivoEntrada, string nombreArchivo)
        {
            try
            {
                var archivo = ConvertToByte(archivoEntrada);
                string _nombreLink = string.Empty;

                // Elimina los caracteres con tilde
                nombreArchivo = nombreArchivo.Replace("á", "a");
                nombreArchivo = nombreArchivo.Replace("é", "e");
                nombreArchivo = nombreArchivo.Replace("í", "i");
                nombreArchivo = nombreArchivo.Replace("ó", "o");
                nombreArchivo = nombreArchivo.Replace("ú", "u");

                nombreArchivo = nombreArchivo.Replace("Á", "A");
                nombreArchivo = nombreArchivo.Replace("É", "E");
                nombreArchivo = nombreArchivo.Replace("Í", "I");
                nombreArchivo = nombreArchivo.Replace("Ó", "O");
                nombreArchivo = nombreArchivo.Replace("Ú", "U");

                // Elimina las Ñ
                nombreArchivo = nombreArchivo.Replace("ñ", "n");
                nombreArchivo = nombreArchivo.Replace("Ñ", "N");

                try
                {
                    // TODO: Reemplazar por el connection string real (idealmente desde appsettings.json)
                    string _azureStorageConnectionString = CONNECTION_STRING;
                    string _direccionBlob = RUTA_BLOB_CERTIFICADOS;

                    // Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = "application/pdf";
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = "application/pdf";
                    Stream stream = new MemoryStream(archivo);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = URL_BASE_BLOB + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }
                    return _nombreLink;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }

        /// <summary>
        /// Obtiene la URL completa del logo de certificación
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo guardado en BD</param>
        /// <returns>URL completa o string vacío si no hay archivo</returns>
        public string ObtenerUrlCertificadoLogo(string? nombreArchivo)
        {
            if (string.IsNullOrEmpty(nombreArchivo))
                return string.Empty;

            return URL_BASE_BLOB + RUTA_BLOB_LOGOS + nombreArchivo.Replace(" ", "%20");
        }

        /// <summary>
        /// Obtiene la URL completa del certificado del partner
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo guardado en BD</param>
        /// <returns>URL completa o string vacío si no hay archivo</returns>
        public string ObtenerUrlCertificadoBSG(string? nombreArchivo)
        {
            if (string.IsNullOrEmpty(nombreArchivo))
                return string.Empty;

            return URL_BASE_BLOB + RUTA_BLOB_CERTIFICADOS + nombreArchivo.Replace(" ", "%20");
        }

        #endregion

    }
}
