using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Net;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoLegalRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoLegal
    /// </summary>
    public class DocumentoLegalRepository : GenericRepository<TDocumentoLegal>, IDocumentoLegalRepository
    {
        private Mapper _mapper;

        public DocumentoLegalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoLegal, DocumentoLegal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        private byte[] DescargarArchivoBlob(string url)
        {
            byte[] documento = null;
            WebClient myWebClient = new WebClient();
            documento = myWebClient.DownloadData(url);
            return documento;
        }

        #region Metodos Base
        private TDocumentoLegal MapeoEntidad(DocumentoLegal entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoLegal modelo = _mapper.Map<TDocumentoLegal>(entidad);

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

        public TDocumentoLegal Add(DocumentoLegal entidad)
        {
            try
            {
                var DocumentoLegal = MapeoEntidad(entidad);
                base.Insert(DocumentoLegal);
                return DocumentoLegal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoLegal Update(DocumentoLegal entidad)
        {
            try
            {
                var DocumentoLegal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoLegal.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoLegal);
                return DocumentoLegal;
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


        public IEnumerable<TDocumentoLegal> Add(IEnumerable<DocumentoLegal> listadoEntidad)
        {
            try
            {
                List<TDocumentoLegal> listado = new List<TDocumentoLegal>();
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

        public IEnumerable<TDocumentoLegal> Update(IEnumerable<DocumentoLegal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoLegal> listado = new List<TDocumentoLegal>();
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
        /// Obtiene todos los registros de T_DocumentoLegal.
        /// </summary>
        /// <returns> List<DocumentoLegalDTO> </returns>
        public IEnumerable<DocumentoLegalDTO> ObtenerDocumentoLegal()
        {
            try
            {
                List<DocumentoLegalDTO> rpta = new List<DocumentoLegalDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Descripcion,
	                    IdPais,
	                    Url,
	                    VisualizarAgenda,
	                    DescargarAgenda,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    Roles
                    FROM fin.T_DocumentoLegal
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoLegalDTO>>(resultado);
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
        /// Obtiene registros de T_DocumentoLegal para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoLegalComboDTO> </returns>
        public IEnumerable<DocumentoLegalComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoLegalComboDTO> rpta = new List<DocumentoLegalComboDTO>();
                var query = @"SELECT Id,Nombre FROM fin.T_DocumentoLegal WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoLegalComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Documentos Legales para Agenda.
        /// </summary>
        /// <param name="idAreaPersonal">Id de PersonalAreaTrabajo</param>
        /// <param name="rol">Nombre del Rol del Personal</param>
        /// <param name="idPais">Id de Pais</param>
        /// <returns> List<DocumentoLegalComboDTO> </returns>
        public IEnumerable<DocumentoLegalAgendaDTO> ObtenerDocumentoLegalParaAgenda(int idAreaPersonal, string rol, int idPais)
        {
            try
            {
                List<DocumentoLegalAgendaDTO> rpta = new List<DocumentoLegalAgendaDTO>();
                rol = $"%{rol}%";
                var query = @"
                    SELECT Id,Nombre,Descripcion,IdPais,Pais,Url,Area AS IdPersonalAreaTrabajo,VisualizarAgenda,DescargarAgenda,Roles
                    FROM fin.V_ObtenerDocumentoLegal
                    WHERE Area = @idAreaPersonal
                        AND IdPais IN (@idPais, 0)
                        AND Roles LIKE @rol
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idAreaPersonal, rol, idPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoLegalAgendaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los documentos legales y los agrupa para mostrar las areas
        /// </summary>
        /// <returns> List<DocumentoLegalV2DTO> </returns>
        public IEnumerable<DocumentoLegalV2DTO> ObtenerDocumentosLegales()
        {
            try
            {
                List<DocumentoLegalV2DTO> salidaDocumentos = new List<DocumentoLegalV2DTO>();
                List<DocumentoLegalV2DTO> documentosLegales = new List<DocumentoLegalV2DTO>();
                var query = "SELECT * FROM fin.V_ObtenerDocumentoLegal ORDER BY Id desc";
                var documentoBD = _dapperRepository.QueryDapper(query, null);
                if (!documentoBD.Contains("[]") && !string.IsNullOrEmpty(documentoBD))
                {
                    documentosLegales = JsonConvert.DeserializeObject<List<DocumentoLegalV2DTO>>(documentoBD);

                    salidaDocumentos = documentosLegales
                    .GroupBy(x => new { x.Id, x.Nombre, x.Descripcion, x.Url, x.VisualizarAgenda, x.DescargarAgenda, x.Roles })
                    .Select(g =>
                    new DocumentoLegalV2DTO
                    {
                        Id = g.Key.Id,
                        Nombre = g.Key.Nombre,
                        Descripcion = g.Key.Descripcion,
                        Url = g.Key.Url,
                        VisualizarAgenda = g.Key.VisualizarAgenda,
                        DescargarAgenda = g.Key.DescargarAgenda,
                        Roles = g.Key.Roles,
                        Areas = documentosLegales.Where(y => y.Id == g.Key.Id).Select(z => z.Area).ToList()
                    }).ToList();
                    query = "SELECT * FROM fin.T_DocumentoLegalPais WHERE IdDocumentoLegal=@Id AND Estado=1";
                    foreach (var item in salidaDocumentos)
                    {
                        var paisesBD = _dapperRepository.QueryDapper(query, new { Id = item.Id });
                        if (!paisesBD.Contains("[]") && !string.IsNullOrEmpty(paisesBD))
                        {
                            var resultado = JsonConvert.DeserializeObject<List<DocumentoLegalPaisDTO>>(paisesBD);
                            item.PaisesBD = resultado;
                        }
                    }
                }
                else throw new Exception("No existen documentos Legales para mostrar");
                return salidaDocumentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los documentos legales para agenda
        /// </summary>
        /// <returns> List<DocumentoLegalDTO> </returns>
        /// <paramref name="area"/> Identificador del area
        /// <paramref name="rol"/> nomrbe del rol
        /// <paramref name="idpais"/>Identificador del pais
        public IEnumerable<DocumentoLegalV3DTO> ObtenerDocumentoLegalAgenda(int area, string rol, int idpais)
        {
            try
            {
                List<DocumentoLegalV3DTO> documentosLegales = new List<DocumentoLegalV3DTO>();
                var query = "SELECT * FROM fin.V_ObtenerDocumentoLegal WHERE Area=" + area + " AND IdPais IN(" + idpais + ",0) AND Roles LIKE '%" + rol + "%' ORDER BY Id DESC";
                var documentoBD = _dapperRepository.QueryDapper(query, null);
                if (!documentoBD.Contains("[]") && !string.IsNullOrEmpty(documentoBD))
                {
                    documentosLegales = JsonConvert.DeserializeObject<List<DocumentoLegalV3DTO>>(documentoBD);
                    foreach (var item in documentosLegales)
                    {
                        if (item.DescargarAgenda == true)
                        {
                            item.DocumentoByte = DescargarArchivoBlob(item.Url);
                        }
                    }
                }
                // else throw new Exception("No existen Documentos Legales con esos filtros.");
                return documentosLegales;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
