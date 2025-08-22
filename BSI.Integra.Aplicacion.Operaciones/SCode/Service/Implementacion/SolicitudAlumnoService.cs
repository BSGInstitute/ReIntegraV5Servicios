using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
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
    /// Service: SolicitudAlumnoService
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2023
    /// <summary>
    /// Gestión general de T_SolicitudAlumno
    /// </summary>
    public class SolicitudAlumnoService : ISolicitudAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SolicitudAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSolicitudAlumno, SolicitudAlumno>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SolicitudAlumno Add(SolicitudAlumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudAlumnoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudAlumno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudAlumno Update(SolicitudAlumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudAlumnoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudAlumno>(modelo);
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
                _unitOfWork.SolicitudAlumnoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudAlumno> Add(List<SolicitudAlumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudAlumnoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudAlumno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudAlumno> Update(List<SolicitudAlumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudAlumnoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudAlumno>>(modelo);
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
                _unitOfWork.SolicitudAlumnoRepository.Delete(listadoIds, usuario);
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
        public string SubirArchivoSolicitudAlumnoRepositorio(IFormFile archivoEntrada, string tipo, string nombreArchivo)
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
        /// Obtiene todos las solicitudes por area
        /// </summary> 
        /// <returns> IEnumerable<SolicitudAlumnoFiltradaDTO> </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorArea(int idPersonal)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerSolicitudesPorArea(idPersonal);
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
        /// Obtiene todos las solicitudes por personal
        /// </summary> 
        /// <returns> IEnumerable<SolicitudAlumnoFiltradaDTO> </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorPersonal(int idPersonal)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerSolicitudesPorPersonal(idPersonal);
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
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltro(FiltroSolicitudesDTO FiltroSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerSolicitudesPorFiltro(FiltroSolicitud);
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
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumno(FiltroSolicitudAlumnoDTO FiltroSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerSolicitudesPorFiltroAlumno(FiltroSolicitud);
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
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumnoRevision(FiltroSolicitudAlumnoDTO FiltroSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerSolicitudesPorFiltroAlumnoRevision(FiltroSolicitud);
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
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumnoGestion(FiltroSolicitudAlumnoDTO FiltroSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerSolicitudesPorFiltroAlumnoGestion(FiltroSolicitud);
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
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolicitudesDTO FiltroSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolicitud);
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
        public IEnumerable<SolicitudAlumnoFiltradaDTO> obtenerSolicitudAlumno()
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.obtenerSolicitudesAlumno();
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
        public IEnumerable<SolicitudLogDTO> obtenerLogSolicitudes(int idSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.obtenerLogSolicitudes(idSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudAlumno ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SolicitudPersonalAlumnoDTO> ObtenerPersonalSolicitanteAlumno()
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerPersonalSolicitanteAlumno();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SolicitudPersonalSolucionAlumnoDTO> ObtenerPersonalSolucionSolicitudAlumno(List<int> IdPersonal)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerPersonalSolucionSolicitudAlumno(IdPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Jorge Gamero
        /// Fecha: 15/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las solicitudes de alumno por filtro
        /// </summary> 
        /// <returns> IEnumerable<ReporteSolicitudAlumnoDTO> </returns>
        public IEnumerable<ReporteSolicitudAlumnoDTO> ObtenerReporteSolicitudesPorFiltroAlumno(FiltroReporteSolicitudAlumnoDTO FiltroReporteSolicitud)
        {
            try
            {
                return _unitOfWork.SolicitudAlumnoRepository.ObtenerReporteSolicitudesPorFiltroAlumno(FiltroReporteSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}