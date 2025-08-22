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
    /// Repositorio: ConjuntoListaRepository
    /// Autor: Adriana Chipana.
    /// Fecha: 18/05/202
    /// <summary>
    /// Gestión general de T_ConjuntoLista
    /// </summary>
    public class ConjuntoListaRepository : GenericRepository<TConjuntoListum>, IConjuntoListaRepository
    {
        private Mapper _mapper;


        public ConjuntoListaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap< TConjuntoListum, ConjuntoLista>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConjuntoListaDetalle, ConjuntoListaDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConjuntoListaDetalleValor, ConjuntoListaDetalleValor>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConjuntoListum MapeoEntidad(ConjuntoLista entidad)
        {
            try
            {
                // Crea la entidad padre
                TConjuntoListum modelo = _mapper.Map<TConjuntoListum>(entidad);

                if (entidad.ListaConjuntoListaDetalle != null && entidad.ListaConjuntoListaDetalle.Count > 0)
                {
                    foreach (var detalleEntidad in entidad.ListaConjuntoListaDetalle)
                    {
                        TConjuntoListaDetalle detalleModelo = _mapper.Map<TConjuntoListaDetalle>(detalleEntidad);

                        //if (detalleEntidad.ListaConjuntoListaDetalleValor != null && detalleEntidad.ListaConjuntoListaDetalleValor.Count > 0)
                        //{   
                        //    foreach (var valorEntidad in detalleEntidad.ListaConjuntoListaDetalleValor)
                        //    {
                        //        TConjuntoListaDetalleValor valorModelo = _mapper.Map<TConjuntoListaDetalleValor>(valorEntidad);
                        //        detalleModelo.TConjuntoListaDetalleValors.Add(valorModelo);
                        //    }
                        //}
                        detalleModelo.RowVersion = detalleEntidad.RowVersion;
                        modelo.TConjuntoListaDetalles.Add(detalleModelo);

                    }
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TConjuntoListum Add(ConjuntoLista entidad)
        {
            try
            {
                var ConjuntoLista = MapeoEntidad(entidad);
                base.Insert(ConjuntoLista);
                return ConjuntoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoListum Update(ConjuntoLista entidad)
        {
            try
            {
                var ConjuntoLista = MapeoEntidad(entidad);


                base.Update(ConjuntoLista);
                return ConjuntoLista;
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


        public IEnumerable<TConjuntoListum> Add(IEnumerable<ConjuntoLista> listadoEntidad)
        {
            try
            {
                List<TConjuntoListum> listado = new List<TConjuntoListum>();
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

        public IEnumerable<TConjuntoListum> Update(IEnumerable<ConjuntoLista> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConjuntoListum> listado = new List<TConjuntoListum>();
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


        public TConjuntoListum Insertar(ConjuntoListaEnvioDTO objeto)
        {
            try
            {
                var ConjuntoLista = new TConjuntoListum();

                ConjuntoLista.Nombre = objeto.Nombre;
                ConjuntoLista.Descripcion = objeto.Descripcion;
                ConjuntoLista.IdCategoriaObjetoFiltro = objeto.IdCategoriaObjetoFiltro;
                ConjuntoLista.IdFiltroSegmento = objeto.IdFiltroSegmento;
                ConjuntoLista.NroListasRepeticionContacto = objeto.NroListasRepeticionContacto;
                ConjuntoLista.Estado = true;
                ConjuntoLista.ConsiderarYaEnviados = objeto.ConsiderarYaEnviados;
                ConjuntoLista.UsuarioCreacion = objeto.UsuarioCreacion;
                ConjuntoLista.UsuarioModificacion = objeto.UsuarioModificacion;
                ConjuntoLista.FechaCreacion = DateTime.Now;
                ConjuntoLista.FechaModificacion =  DateTime.Now;

                base.Insert(ConjuntoLista);
                return ConjuntoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ConjuntoListaGrillaDTO> Obtener()
        {
            try
            {
                List<ConjuntoListaGrillaDTO> conjuntoLista = new List<ConjuntoListaGrillaDTO>();

                var _query = @"
                            SELECT * FROM mkt.V_ObtenerConjuntoListaCategoria ORDER BY fechaCreacion desc
                            ";
                var query = _dapperRepository.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<ConjuntoListaGrillaDTO>>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public ConjuntoListaDTO Obtener(int id)
        {
            try
            {
                ConjuntoListaDTO conjuntoLista = new ConjuntoListaDTO();

                var _query = @"
                            SELECT * FROM mkt.V_ObtenerConjuntoListaCategoria where  Id = @id
                            ";
                var query = _dapperRepository.FirstOrDefault(_query, new { id });

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<ConjuntoListaDTO>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ConjuntoListaCompuestoDTO> ObtenerResultado(int id, int idFiltroSegmentoTipoContacto)
        {
            try
            {
                List<ConjuntoListaCompuestoDTO> listaConjuntoLista = new List<ConjuntoListaCompuestoDTO>();
                var query = "";

                switch (idFiltroSegmentoTipoContacto)
                {
                    case 1:///alumno - exalumno
                        query = "mkt.SP_ObtenerResultadoConjuntoListaTipoAlumno";
                        break;
                    case 2://docente
                        query = "";
                        break;
                    case 6:///prospecto
                        query = "mkt.SP_ObtenerResultadoConjuntoLista";
                        break;
                    default:
                        break;
                }
                var listaConjuntoListaDB = _dapperRepository.QuerySPDapper(query, new { IdConjuntoLista = id });
                if (!string.IsNullOrEmpty(listaConjuntoListaDB) && !listaConjuntoListaDB.Contains("[]"))
                {
                    listaConjuntoLista = JsonConvert.DeserializeObject<List<ConjuntoListaCompuestoDTO>>(listaConjuntoListaDB);
                }
                return listaConjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool SubirLista(ConjuntoListaSubirDTO json, string usuario)
        {
            try
            {
                AlumnoDTO conjuntoLista = new AlumnoDTO();

                var _query = $"exec mkt.SP_InsertarAlumnosConjuntoListaDetalle @IdsAlumnos = '{json.listaIds}', @IdConjuntoLista = {json.idConjuntoLista}, @Usuario = {usuario}";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<AlumnoDTO>(query);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> conjuntoLista = new List<ComboDTO>();

                var _query = @"SELECT Id, Nombre FROM mkt.T_ConjuntoLista where estado = 1 order by id desc";
                var query = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(query))
                {
                    conjuntoLista = JsonConvert.DeserializeObject<List<ComboDTO>>(query);
                }
                return conjuntoLista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public StringDTO GenerarUrlFormulariosLink(GenerarFormularioDTO datos, string usuario)
        {
            try
            {
                StringDTO formulario = new StringDTO();

                var _query = $"exec mkt.SP_GeneradorFormulariosLink @Nombre = '{datos.Nombre}', @IdCentroCosto = {datos.IdCentroCosto}, @UsuarioResponsable = {usuario}";
                var query = _dapperRepository.FirstOrDefault(_query, null);

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query!="null")
                {
                    formulario = JsonConvert.DeserializeObject<StringDTO>(query);
                }
                return formulario;
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
