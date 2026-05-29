using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    public class AlumnoCasoExitoService : IAlumnoCasoExitoService
    {
        private readonly IUnitOfWork _unitOfWork;

        // Azure Blob Storage — repositorioweb/reviews/perfil/
        private const string URL_BASE_BLOB     = "https://repositorioweb.blob.core.windows.net/";
        private const string RUTA_BLOB_FOTOS   = "repositorioweb/reviews/perfil/";
        private const string CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

        public AlumnoCasoExitoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AlumnoCasoExitoDTO> Obtener()
        {
            try
            {
                var lista = _unitOfWork.AlumnoCasoExitoRepository.Obtener().ToList();
                foreach (var item in lista)
                    item.UrlFotoPerfil = ObtenerUrlFotoPerfil(item.FotoPerfil);
                return lista;
            }
            catch (Exception ex) { throw ex; }
        }

        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try { return _unitOfWork.AlumnoCasoExitoRepository.ObtenerCombo(); }
            catch (Exception ex) { throw ex; }
        }

        public AlumnoCasoExitoDTO? ObtenerPorId(int id)
        {
            try
            {
                var item = _unitOfWork.AlumnoCasoExitoRepository.ObtenerPorId(id);
                if (item != null) item.UrlFotoPerfil = ObtenerUrlFotoPerfil(item.FotoPerfil);
                return item;
            }
            catch (Exception ex) { throw ex; }
        }

        public AlumnoCasoExitoDTO Insertar(AlumnoCasoExitoEntradaDTO entrada, string usuario)
        {
            try
            {
                var dto = new AlumnoCasoExitoDTO
                {
                    Nombre            = entrada.Nombre,
                    TituloTestimonio  = entrada.TituloTestimonio,
                    Testimonio        = entrada.Testimonio,
                    IdPais            = entrada.IdPais,
                    Posicion          = entrada.Posicion,
                    Visibilidad       = entrada.Visibilidad,
                    FotoPerfil        = entrada.FotoPerfil
                };

                if (entrada.ArchivoFotoPerfil != null)
                {
                    var nombreGenerado = GenerarNombreArchivo(entrada.ArchivoFotoPerfil.FileName);
                    var nombreSubido   = SubirFotoPerfil(entrada.ArchivoFotoPerfil, nombreGenerado);
                    if (!string.IsNullOrEmpty(nombreSubido))
                        dto.FotoPerfil = nombreSubido;
                }

                var nuevoId = _unitOfWork.AlumnoCasoExitoRepository.Insertar(dto, usuario);
                dto.Id            = nuevoId;
                dto.UrlFotoPerfil = ObtenerUrlFotoPerfil(dto.FotoPerfil);
                return dto;
            }
            catch (Exception ex) { throw ex; }
        }

        public AlumnoCasoExitoDTO Actualizar(AlumnoCasoExitoEntradaDTO entrada, string usuario)
        {
            try
            {
                var dto = new AlumnoCasoExitoDTO
                {
                    Id               = entrada.Id,
                    Nombre           = entrada.Nombre,
                    TituloTestimonio = entrada.TituloTestimonio,
                    Testimonio       = entrada.Testimonio,
                    IdPais           = entrada.IdPais,
                    Posicion         = entrada.Posicion,
                    Visibilidad      = entrada.Visibilidad,
                    FotoPerfil       = entrada.FotoPerfil
                };

                if (entrada.ArchivoFotoPerfil != null)
                {
                    var nombreGenerado = GenerarNombreArchivo(entrada.ArchivoFotoPerfil.FileName);
                    var nombreSubido   = SubirFotoPerfil(entrada.ArchivoFotoPerfil, nombreGenerado);
                    if (!string.IsNullOrEmpty(nombreSubido))
                        dto.FotoPerfil = nombreSubido;
                }

                _unitOfWork.AlumnoCasoExitoRepository.Actualizar(dto, usuario);
                dto.UrlFotoPerfil = ObtenerUrlFotoPerfil(dto.FotoPerfil);
                return dto;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Eliminar(int id, string usuario)
        {
            try { return _unitOfWork.AlumnoCasoExitoRepository.Eliminar(id, usuario); }
            catch (Exception ex) { throw ex; }
        }

        public bool ActualizarVisibilidad(int id, bool estadoVisibilidad, string usuario)
        {
            try { return _unitOfWork.AlumnoCasoExitoRepository.ActualizarVisibilidad(id, estadoVisibilidad, usuario); }
            catch (Exception ex) { throw ex; }
        }

        public bool ActualizarPosiciones(List<AlumnoCasoExitoPosicionDTO> posiciones, string usuario)
        {
            try
            {
                var json = JsonConvert.SerializeObject(posiciones);
                return _unitOfWork.AlumnoCasoExitoRepository.ActualizarPosiciones(json, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public string SubirFotoPerfil(IFormFile archivo, string nombreArchivo)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CONNECTION_STRING);
                CloudBlobClient     blobClient     = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer  container      = blobClient.GetContainerReference("repositorioweb");
                CloudBlockBlob      blockBlob      = container.GetBlockBlobReference("reviews/perfil/" + nombreArchivo);
                blockBlob.UploadFromStreamAsync(archivo.OpenReadStream()).Wait();
                return nombreArchivo;
            }
            catch (Exception) { return string.Empty; }
        }

        public string GenerarNombreArchivo(string nombreOriginal)
        {
            var timestamp    = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var nombreLimpio = SlugNombreArchivo(Path.GetFileNameWithoutExtension(nombreOriginal));
            var extension    = Path.GetExtension(nombreOriginal).ToLower();
            return $"{timestamp}-{nombreLimpio}{extension}";
        }

        public string ObtenerUrlFotoPerfil(string? nombreArchivo)
        {
            if (string.IsNullOrEmpty(nombreArchivo)) return string.Empty;
            return $"{URL_BASE_BLOB}{RUTA_BLOB_FOTOS}{nombreArchivo}";
        }

        private string SlugNombreArchivo(string nombre)
        {
            nombre = nombre.ToLower();
            nombre = Regex.Replace(nombre, @"[áàäâ]", "a");
            nombre = Regex.Replace(nombre, @"[éèëê]", "e");
            nombre = Regex.Replace(nombre, @"[íìïî]", "i");
            nombre = Regex.Replace(nombre, @"[óòöô]", "o");
            nombre = Regex.Replace(nombre, @"[úùüû]", "u");
            nombre = Regex.Replace(nombre, @"[ñ]", "n");
            nombre = Regex.Replace(nombre, @"[^a-z0-9\-]", "-");
            nombre = Regex.Replace(nombre, @"-+", "-").Trim('-');
            return nombre;
        }
    }
}
