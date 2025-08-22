using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PersonalArchivoRepository : GenericRepository<TPersonalArchivo>, IPersonalArchivoRepository
    {
        private Mapper _mapper;

        public PersonalArchivoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalArchivo, PersonalArchivo>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalArchivo, TPersonalArchivo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalArchivo MapeoEntidad(PersonalArchivo entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalArchivo modelo = _mapper.Map<TPersonalArchivo>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalArchivo Add(PersonalArchivo entidad)
        {
            try
            {
                var PersonalArchivo = MapeoEntidad(entidad);
                base.Insert(PersonalArchivo);
                return PersonalArchivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalArchivo Update(PersonalArchivo entidad)
        {
            try
            {
                var PersonalArchivo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalArchivo.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalArchivo);
                return PersonalArchivo;
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


        public IEnumerable<TPersonalArchivo> Add(IEnumerable<PersonalArchivo> listadoEntidad)
        {
            try
            {
                List<TPersonalArchivo> listado = new List<TPersonalArchivo>();
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

        public IEnumerable<TPersonalArchivo> Update(IEnumerable<PersonalArchivo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalArchivo> listado = new List<TPersonalArchivo>();
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

        public PersonalArchivo? ObtenerPorId(int idPersonalArchivo)
        {
            try
            {
                var query = @"SELECT
                               Id,
                               NombreArchivo,
                               RutaArchivo,
                               MimeType,
                               EsImagen,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion
                             FROM  gp.T_PersonalArchivo
                    WHERE Id=@idPersonalArchivo AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonalArchivo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalArchivo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OPI-GP-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }



        public string SubirDocumentosPersonal(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string nombreLink = string.Empty;

                try
                {
                    string azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
                    string direccionBlob = @"repositorioweb/archivospersonal/";

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
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
