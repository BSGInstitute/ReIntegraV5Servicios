using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using PdfSharp.Pdf.Filters;
using BSI.Integra.Aplicacion.DTO;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using Mandrill.Models;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Google.Rpc;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion
{
    /// Service: MaterialPespecificoDetalleService
    /// Autor: Jonathan Caipo
    /// Fecha: 19/09/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_MaterialPespecificoDetalle
    /// </summary>
    public class MaterialPespecificoDetalleService : IMaterialPespecificoDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MaterialPespecificoDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TMaterialPespecificoDetalle, MaterialPespecificoDetalle>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TMaterialPespecificoDetalle, MaterialPespecificoDetalleDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<MaterialPespecificoDetalle, MaterialPespecificoDetalleDTO>(MemberList.None).ReverseMap();
                }
                );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Sube Archivos (Materiales)
        /// </summary>
        /// <param name="files"></param>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool SubirMaterialArchivo(SubirMaterialPEspecificoDetalleDTO dto, string usuario)
        {
            try
            {
                var materialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(dto.Id);
                if (materialPespecificoDetalle != null)
                {
                    string nombreArchivo = "";
                    string contentType = "";
                    var urlArchivoRepositorio = "";
                    var materialPEspecifico = _unitOfWork.MaterialPespecificoRepository.ObtenerPorId(materialPespecificoDetalle.IdMaterialPespecifico);

                    if (dto.Files != null)
                    {
                        foreach (var file in dto.Files)
                        {
                            contentType = file.ContentType;
                            nombreArchivo = file.FileName;
                            nombreArchivo = string.Concat(materialPespecificoDetalle.Id, "-", materialPEspecifico.IdPespecifico, "-", nombreArchivo);
                            urlArchivoRepositorio = SubirArchivoMaterialesRepositorio(file, file.ContentType, nombreArchivo);
                        }
                    }

                    materialPespecificoDetalle.UrlArchivo = urlArchivoRepositorio;
                    materialPespecificoDetalle.IdMaterialEstado = 2; //Editado
                    materialPespecificoDetalle.NombreArchivo = nombreArchivo;
                    materialPespecificoDetalle.FechaSubida = DateTime.Now;
                    materialPespecificoDetalle.ComentarioSubida = dto.ComentarioSubida;
                    materialPespecificoDetalle.UsuarioSubida = usuario;
                    materialPespecificoDetalle.UsuarioModificacion = usuario;
                    materialPespecificoDetalle.FechaModificacion = DateTime.Now;

                    _unitOfWork.MaterialPespecificoDetalleRepository.Update(materialPespecificoDetalle);
                    _unitOfWork.Commit();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/09/2023
        /// Version: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirArchivoMaterialesRepositorio(IFormFile archivoEntrada, string tipo, string nombreArchivo)
        {
            try
            {
                var archivo = ConvertToByte(archivoEntrada);
                string _nombreLink = string.Empty;

                //Elimina los caracteres especiales, mayúsculas y tildes
                var nombreValidado = NormalizarCadena(nombreArchivo);

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/materiales/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }
                    return _nombreLink;
                }
                catch
                {
                    return "";
                }
            }
            catch
            {
                //throw new Exception(e.Message);
                return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/09/2023
        /// Version: 1.0
        /// <summary>
        /// Convierte archivos a bytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public byte[] ConvertToByte(IFormFile file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.OpenReadStream());
            imageByte = rdr.ReadBytes((int)file.Length);
            return imageByte;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/09/2023
        /// Version: 1.0
        /// <summary>
        /// Normaliza la Cadena reemplazando los acentos, mayúsculas y la letra ñ
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

        /// Autor: Margiory Ramirez
        /// Fecha: 15/11/2023
        /// Version: 1.0
        /// <summary>

        public MaterialPEspecificoDetalleFurDTO ObtenerDetalleFur(int idMaterialPEspecificoDetalle)
        {

            try
            {
                return _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerDetalleFur(idMaterialPEspecificoDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
