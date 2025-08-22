using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: SolicitudInternaService
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2023
    /// <summary>
    /// Gestión general de T_SolicitudInterna
    /// </summary>
    public class SolicitudInternaService : ISolicitudInterna
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SolicitudInternaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSolicitudInterna, SolicitudInterna>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SolicitudInterna Add(SolicitudInterna entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudInternaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudInterna>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudInterna Update(SolicitudInterna entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudInternaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudInterna>(modelo);
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
                _unitOfWork.SolicitudInternaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudInterna> Add(List<SolicitudInterna> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudInternaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudInterna>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudInterna> Update(List<SolicitudInterna> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudInternaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudInterna>>(modelo);
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
                _unitOfWork.SolicitudInternaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion




        public byte[] ConvertToByte(IFormFile file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.OpenReadStream());
            imageByte = rdr.ReadBytes((int)file.Length);
            return imageByte;
        }
        /// Autor: Joseph llanque
        /// Fecha: 10/03/23
        /// Version: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivoEntrada"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirArchivoSolicitudInternaRepositorio(IFormFile archivoEntrada, string tipo, string nombreArchivo)
        {
            try
            {
                var archivo = ConvertToByte(archivoEntrada);
                string _nombreLink = string.Empty;
                //Elimina los caracteres con tilde
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

                //Elimina las Ñ
                nombreArchivo = nombreArchivo.Replace("ñ", "n");
                nombreArchivo = nombreArchivo.Replace("Ñ", "N");


                //Elimina los caracteres con tilde
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

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/solicitudes/";

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
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }


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



        /// Autor:Joseph Llanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes por revisar 
        /// </summary> 
        /// <returns> IEnumerable<SolicitudAlumnoFiltradaDTO> </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesPorArea(int idPersonal)
        {
            try
            {
                return _unitOfWork.SolicitudInternaRepository.ObtenerSolicitudesPorArea(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes por revisar 
        /// </summary> 
        /// <returns> IEnumerable<SolicitudAlumnoFiltradaDTO> </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesGestion(int idPersonal)
        {
            try
            {
                return _unitOfWork.SolicitudInternaRepository.ObtenerSolicitudesGestion(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor:Joseph Llanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes por filtro
        /// </summary> 
        /// <returns> IEnumerable<SolicitudAlumnoFiltradaDTO> </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesPorFiltro(FiltroSolicitudesInternasDTO FiltroSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudInternaRepository.ObtenerSolicitudesPorFiltro(FiltroSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Joseph Llanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes por filtro reporte
        /// </summary> 
        /// <returns> IEnumerable<SolicitudAlumnoFiltradaDTO> </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolicitudesInternasDTO FiltroSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudInternaRepository.ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 02/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el todas las solicitudes de alumnos
        /// </summary> 
        /// <returns> IEnumerable<SolicitudAlumnoFiltradaDTO> </returns>
        public IEnumerable<SolicitudInternaFiltradaDTO> obtenerSolicitudInterna()
        {
            try
            {
                return _unitOfWork.SolicitudInternaRepository.obtenerSolicitudesInternas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudInterna ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.SolicitudInternaRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
