using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoOportunidadRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoOportunidad
    /// </summary>
    public class DocumentoOportunidadRepository : GenericRepository<TDocumentoOportunidad>, IDocumentoOportunidadRepository
    {
        private Mapper _mapper;

        public DocumentoOportunidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoOportunidad, DocumentoOportunidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentoOportunidad MapeoEntidad(DocumentoOportunidad entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoOportunidad modelo = _mapper.Map<TDocumentoOportunidad>(entidad);

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

        public TDocumentoOportunidad Add(DocumentoOportunidad entidad)
        {
            try
            {
                var DocumentoOportunidad = MapeoEntidad(entidad);
                base.Insert(DocumentoOportunidad);
                return DocumentoOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoOportunidad Update(DocumentoOportunidad entidad)
        {
            try
            {
                var DocumentoOportunidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoOportunidad.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoOportunidad);
                return DocumentoOportunidad;
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


        public IEnumerable<TDocumentoOportunidad> Add(IEnumerable<DocumentoOportunidad> listadoEntidad)
        {
            try
            {
                List<TDocumentoOportunidad> listado = new List<TDocumentoOportunidad>();
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

        public IEnumerable<TDocumentoOportunidad> Update(IEnumerable<DocumentoOportunidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoOportunidad> listado = new List<TDocumentoOportunidad>();
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
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoOportunidad.
        /// </summary>
        /// <returns> List<DocumentoOportunidadDTO> </returns>
        public IEnumerable<DocumentoOportunidadDTO> ObtenerDocumentoOportunidad()
        {
            try
            {
                List<DocumentoOportunidadDTO> rpta = new List<DocumentoOportunidadDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdAlumno,
	                    IdOportunidad,
	                    NombreArchivo,
	                    Ruta,
	                    Comentario,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    IdClasificacionPersona,
	                    IdDocumentoOportunidadTipo
                    FROM com.T_DocumentoOportunidad
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoOportunidadDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoOportunidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoOportunidadComboDTO> </returns>
        public IEnumerable<DocumentoOportunidadComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoOportunidadComboDTO> rpta = new List<DocumentoOportunidadComboDTO>();
                var query = @"SELECT Id,NombreArchivo FROM com.T_DocumentoOportunidad WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoOportunidadComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de un programa general especifico
        /// </summary>
        /// <returns>Objeto de tipo PgeneralDTO</returns>
        public List<DocumentoOportunidadInsertadoDTO> ObtenerDocumentosPorOportunidad(int idOportunidad)
        {
            try
            {
                List<DocumentoOportunidadInsertadoDTO> documentos = new List<DocumentoOportunidadInsertadoDTO>();
                string query = "SELECT Ruta AS Url,Comentario,IdDocumentoOportunidadTipo AS Tipo FROM  com.T_DocumentoOportunidad WHERE IdOportunidad=@IdOportunidad AND Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    documentos = JsonConvert.DeserializeObject<List<DocumentoOportunidadInsertadoDTO>>(resultado);
                }
                return documentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lel id del DocumentoOportunidad por el IdOportunidad y el tipo
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <param name="idTipo">Id del tipo documento doportunidad</param>
        /// <returns>Objeto de tipo ValorIntDTO</returns>
        public ValorIntDTO ObtenerDocOportunidadPorIdYTipo(int idOportunidad, int idTipo)
        {
            try
            {
                var id = new ValorIntDTO();
                string query = @"SELECT Id,
                               IdAlumno,
                               IdClasificacionPersona,
                               IdDocumentoOportunidadTipo,
                               IdMigracion, IdOportunidad 
                                FROM com.T_DocumentoOportunidad 
                                WHERE Estado=1 AND IdOportunidad= " + idOportunidad + " AND IdDocumentoOportunidadTipo= " + idTipo;
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idTipo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    id = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
