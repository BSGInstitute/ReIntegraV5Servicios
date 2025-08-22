using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Mandrill.Utilities;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaSendinblueImagenRepository
    /// Autor: Adriana Chipana.
    /// Fecha: 18/05/202
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueImagen
    /// </summary>
    public class PlantillaSendinblueImagenRepository : GenericRepository<TPlantillaSendinblueImagen>, IPlantillaSendinblueImagenRepository
    {
        private Mapper _mapper;


        public PlantillaSendinblueImagenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap< TPlantillaSendinblueImagen, PlantillaSendinblueImagen>(MemberList.None).ReverseMap();
         

            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPlantillaSendinblueImagen MapeoEntidad(PlantillaSendinblueImagen entidad)
        {
            try
            {
                // Crea la entidad padre
                TPlantillaSendinblueImagen modelo = _mapper.Map<TPlantillaSendinblueImagen>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TPlantillaSendinblueImagen Add(PlantillaSendinblueImagen entidad)
        {
            try
            {
                var PlantillaSendinblueImagen = MapeoEntidad(entidad);
                base.Insert(PlantillaSendinblueImagen);
                return PlantillaSendinblueImagen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaSendinblueImagen Update(PlantillaSendinblueImagen entidad)
        {
            try
            {
                var PlantillaSendinblueImagen = MapeoEntidad(entidad);


                base.Update(PlantillaSendinblueImagen);
                return PlantillaSendinblueImagen;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TPlantillaSendinblueImagen> Add(IEnumerable<PlantillaSendinblueImagen> listadoEntidad)
        {
            try
            {
                List<TPlantillaSendinblueImagen> listado = new List<TPlantillaSendinblueImagen>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPlantillaSendinblueImagen> Update(IEnumerable<PlantillaSendinblueImagen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaSendinblueImagen> listado = new List<TPlantillaSendinblueImagen>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        public List<PlantillaSendinblueImagenDTO> ObtenerImagenesPlantilla()
        {
            try
            {
                List<PlantillaSendinblueImagenDTO> conjuntoLista = new List<PlantillaSendinblueImagenDTO>();

                var _query = @"
                            SELECT Id, NombreArchivo, Ruta, Extension FROM  mkt.T_PlantillaSendinblueImagen where estado = 1
                            ";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<PlantillaSendinblueImagenDTO>>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        public string guardarArchivos(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"documentos/integra/marketing/";

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
                return "";
            }
        }


    }
}
