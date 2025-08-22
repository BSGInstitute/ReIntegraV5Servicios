using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MaterialVersionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_MaterialVersion
    /// </summary>
    public class MaterialVersionRepository : GenericRepository<TMaterialVersion>, IMaterialVersionRepository
    {
        private Mapper _mapper;

        public MaterialVersionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialVersion, MaterialVersion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMaterialVersion MapeoEntidad(MaterialVersion entidad)
        {
            try
            {
                //crea la entidad padre
                TMaterialVersion modelo = _mapper.Map<TMaterialVersion>(entidad);

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

        public TMaterialVersion Add(MaterialVersion entidad)
        {
            try
            {
                var MaterialVersion = MapeoEntidad(entidad);
                base.Insert(MaterialVersion);
                return MaterialVersion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialVersion Update(MaterialVersion entidad)
        {
            try
            {
                var MaterialVersion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialVersion.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialVersion);
                return MaterialVersion;
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


        public IEnumerable<TMaterialVersion> Add(IEnumerable<MaterialVersion> listadoEntidad)
        {
            try
            {
                List<TMaterialVersion> listado = new List<TMaterialVersion>();
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

        public IEnumerable<TMaterialVersion> Update(IEnumerable<MaterialVersion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialVersion> listado = new List<TMaterialVersion>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialVersion.
        /// </summary>
        /// <returns> List<MaterialVersionDTO> </returns>
        public MaterialVersion ObtenerPorId(int id)
        {
            try
            {
                MaterialVersion rpta = new MaterialVersion();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Descripcion,
                        Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
                        RowVersion,
                        IdMigracion
                    FROM ope.T_MaterialVersion
                    WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<MaterialVersion>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public List<MaterialVersion> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<MaterialVersion> rpta = new();
                var query = @"
                        SELECT
	                        Id,
	                        Nombre,
	                        Descripcion,
                            Estado,
	                        UsuarioCreacion,
	                        UsuarioModificacion,
	                        FechaCreacion,
	                        FechaModificacion,
                            RowVersion,
                            IdMigracion
                        FROM ope.T_MaterialVersion
                        WHERE Estado = 1 AND Id IN @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<MaterialVersion>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MaterialVersion para mostrarse en combo.
        /// </summary>
        /// <returns> List<MaterialVersionComboDTO> </returns>
        public IEnumerable<MaterialVersionDTO> ObtenerMaterialVersion()
        {
            try
            {
                IEnumerable<MaterialVersionDTO> rpta = new List<MaterialVersionDTO>();
                var query = "SELECT Id,Nombre,Descripcion FROM ope.T_MaterialVersion WHERE Estado = 1 ORDER BY FechaModificacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<MaterialVersionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin Riquelme.
        /// Fecha: 13/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_GrabacionCelularCorporativo para mostrarse.
        /// </summary>
        /// <returns> List<MaterialVersionComboDTO> </returns>
        public IEnumerable<GrabacionCelularCorporativoDTO> ObtenerGrabacionesCelularCorporativo()
        {
            try
            {
                IEnumerable<GrabacionCelularCorporativoDTO> rpta = new List<GrabacionCelularCorporativoDTO>();
                var query = "SELECT Id,NombreArchivo,NumeroDestino,FechaGrabacion,Url,case when IdPersonalAreaTrabajo =3 then 'Atencion al Cliente' when IdPersonalAreaTrabajo=8 then 'Comercial' else 'Ninguno' end as Area FROM ope.T_GrabacionCelularCorporativo WHERE Estado = 1 ORDER BY FechaModificacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<GrabacionCelularCorporativoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin Riquelme.
        /// Fecha: 13/07/2025
        /// Version: 1.0
        /// <summary>
        /// Inserta registros en T_GrabacionCelularCorporativo.
        /// </summary>
        /// <returns> bool </returns>
        public bool InsertarGrabacionesCelularCorporativo(GrabacionCelularCorporativoDTO datos, string usuario)
        {
            try
            {
                string DuracionContestotemp = datos.DuracionContesto.Value.ToString();

                var query = @"EXEC ope.SP_InsertaGrabacionesCelular
                      @NumeroDestino,
                      @FechaGrabacion,
                      @Url,
                      @NombreArchivo,
                      @DuracionContesto,
                      @NroBytes,
                      @Usuario,
                      @IdArea";
                      
                      

                var parametros = new
                {
                    NumeroDestino = datos.NumeroDestino,
                    FechaGrabacion = datos.FechaGrabacion,
                    Url = datos.Url,
                    NombreArchivo = datos.NombreArchivo,
                    DuracionContesto = datos.DuracionContesto.Value.ToString(),
                    NroBytes = datos.NroBytes.ToString(),
                    Usuario = usuario,
                    IdArea = datos.IdArea,

                };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MaterialVersion para mostrarse en combo.
        /// </summary>
        /// <returns> List<MaterialVersionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id,Nombre FROM ope.T_MaterialVersion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirModeloCertificadoRepositorio(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
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

                    string _direccionBlob = @"repositorioweb/modeloscertificado/";

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
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
