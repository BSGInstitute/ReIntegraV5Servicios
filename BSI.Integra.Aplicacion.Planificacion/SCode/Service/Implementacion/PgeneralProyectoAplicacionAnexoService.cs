using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PgeneralProyectoAplicacionAnexoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/06/2023
    /// <summary>
    /// Gestión general de T_PgeneralProyectoAplicacionAnexo
    /// </summary>
    public class PgeneralProyectoAplicacionAnexoService : IPgeneralProyectoAplicacionAnexoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PgeneralProyectoAplicacionAnexoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralProyectoAplicacionAnexo, PgeneralProyectoAplicacionAnexo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralProyectoAplicacionAnexo, PgeneralProyectoAplicacionAnexoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PgeneralProyectoAplicacionAnexo, PgeneralProyectoAplicacionAnexoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Agrega registro nuevo de PgeneralProyectoAplicacionAnexo
        /// </summary>
        /// <param name="pgeneralProyectoAplicacionAnexoDTO"> Información nueva de PgeneralProyectoAplicacionAnexo </param>
        /// <returns> PgeneralProyectoAplicacionAnexoDTO </returns> 
        public PgeneralProyectoAplicacionAnexoDTO Insertar(PgeneralProyectoAplicacionAnexoDTO pgeneralProyectoAplicacionAnexoDTO, string usuario)
        {
            try
            {
                if (pgeneralProyectoAplicacionAnexoDTO.IdPgeneral != 0)
                {
                    PgeneralProyectoAplicacionAnexo pgeneralProyectoAplicacionAnexo = new PgeneralProyectoAplicacionAnexo()
                    {
                        IdPgeneral = pgeneralProyectoAplicacionAnexoDTO.IdPgeneral,
                        NombreArchivo = pgeneralProyectoAplicacionAnexoDTO.NombreArchivo,
                        RutaArchivo = pgeneralProyectoAplicacionAnexoDTO.RutaArchivo,
                        EsEnlace = pgeneralProyectoAplicacionAnexoDTO.EsEnlace,
                        SoloLectura = pgeneralProyectoAplicacionAnexoDTO.SoloLectura,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    var resultado = _unitOfWork.PgeneralProyectoAplicacionAnexoRepository.Add(pgeneralProyectoAplicacionAnexo);
                    _unitOfWork.Commit();
                    return _mapper.Map<PgeneralProyectoAplicacionAnexoDTO>(resultado);
                }
                else
                {
                    throw new BadRequestException("Id Pgeneral 0 no valido");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Agrega registro nuevo de PgeneralProyectoAplicacionAnexo
        /// </summary>
        /// <param name="pgeneralProyectoAplicacionAnexoDTO"> Información nueva de PgeneralProyectoAplicacionAnexo </param>
        /// <returns> PgeneralProyectoAplicacionAnexoDTO </returns> 
        public PgeneralProyectoAplicacionAnexoDTO Actualizar(PgeneralProyectoAplicacionAnexoDTO pgeneralProyectoAplicacionAnexoDTO, string usuario)
        {
            try
            {
                if (pgeneralProyectoAplicacionAnexoDTO.IdPgeneral != 0)
                {
                    if (pgeneralProyectoAplicacionAnexoDTO.Id == null || pgeneralProyectoAplicacionAnexoDTO.Id == 0)
                    {
                        throw new BadRequestException("Id 0 no valido");
                    }
                    var pgeneralProyectoAplicacionAnexo = _unitOfWork.PgeneralProyectoAplicacionAnexoRepository.ObtenerPorId(pgeneralProyectoAplicacionAnexoDTO.Id);
                    if (pgeneralProyectoAplicacionAnexo != null && pgeneralProyectoAplicacionAnexo.Id != 0)
                    {
                        pgeneralProyectoAplicacionAnexo.IdPgeneral = pgeneralProyectoAplicacionAnexoDTO.IdPgeneral;
                        pgeneralProyectoAplicacionAnexo.NombreArchivo = pgeneralProyectoAplicacionAnexoDTO.NombreArchivo;
                        pgeneralProyectoAplicacionAnexo.RutaArchivo = pgeneralProyectoAplicacionAnexoDTO.RutaArchivo;
                        pgeneralProyectoAplicacionAnexo.EsEnlace = pgeneralProyectoAplicacionAnexoDTO.EsEnlace;
                        pgeneralProyectoAplicacionAnexo.SoloLectura = pgeneralProyectoAplicacionAnexoDTO.SoloLectura;
                        pgeneralProyectoAplicacionAnexo.UsuarioModificacion = usuario;
                        pgeneralProyectoAplicacionAnexo.FechaModificacion = DateTime.Now;
                        _unitOfWork.PgeneralProyectoAplicacionAnexoRepository.Update(pgeneralProyectoAplicacionAnexo);
                        _unitOfWork.Commit();
                        return pgeneralProyectoAplicacionAnexoDTO;
                    }
                    else
                    {
                        throw new BadRequestException("Entidad no existente");
                    }
                }
                else
                {
                    throw new BadRequestException("Id Pgeneral 0 no valido");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Trae los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Lista DTO - PgeneralProyectoAplicacionAnexoDTO </returns>
        public IEnumerable<PgeneralProyectoAplicacionAnexoDTO> ObtenerListaPgeneralProyectoAplicacionAnexo(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PgeneralProyectoAplicacionAnexoRepository.ObtenerListaPgeneralProyectoAplicacionAnexoPorIdPGeneral(idPGeneral).ToList();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: L/ourdes Priscila Pacsi Gamboa
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
        /// </summary>
        /// <returns>Estado eliminacion<returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (_unitOfWork.PgeneralProyectoAplicacionAnexoRepository.Exist(id))
                {
                    _unitOfWork.PgeneralProyectoAplicacionAnexoRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns>url del archivo<returns>
        public string GuardarArchivo(IFormFile file)
        {
            try
            {
                byte[] data;
                using (Stream inputStream = file.OpenReadStream())
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
                var url = GuardarArchivoBlobStorage(data, "application/pdf", file.FileName);
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns>url del archivo<returns>
        private string GuardarArchivoBlobStorage(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string nombreLink = string.Empty;
                try
                {
                    const string azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    const string direccionBlob = @"repositorioweb/aulavirtual/anexosproyectos/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);
                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        nombreLink = "https://repositorioweb.blob.core.windows.net/" + direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        nombreLink = "";
                    }
                    return nombreLink;

                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
