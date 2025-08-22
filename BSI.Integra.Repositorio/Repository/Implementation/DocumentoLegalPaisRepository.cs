using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoLegalPaisRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoLegalPais
    /// </summary>
    public class DocumentoLegalPaisRepository : GenericRepository<TDocumentoLegalPai>, IDocumentoLegalPaisRepository
    {
        private Mapper _mapper;

        public DocumentoLegalPaisRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoLegalPai, DocumentoLegalPais>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentoLegalPai MapeoEntidad(DocumentoLegalPais entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoLegalPai modelo = _mapper.Map<TDocumentoLegalPai>(entidad);

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

        public TDocumentoLegalPai Add(DocumentoLegalPais entidad)
        {
            try
            {
                var DocumentoLegalPais = MapeoEntidad(entidad);
                base.Insert(DocumentoLegalPais);
                return DocumentoLegalPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoLegalPai Update(DocumentoLegalPais entidad)
        {
            try
            {
                var DocumentoLegalPais = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoLegalPais.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoLegalPais);
                return DocumentoLegalPais;
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


        public IEnumerable<TDocumentoLegalPai> Add(IEnumerable<DocumentoLegalPais> listadoEntidad)
        {
            try
            {
                List<TDocumentoLegalPai> listado = new List<TDocumentoLegalPai>();
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

        public IEnumerable<TDocumentoLegalPai> Update(IEnumerable<DocumentoLegalPais> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoLegalPai> listado = new List<TDocumentoLegalPai>();
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
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoLegalPais.
        /// </summary>
        /// <returns> List<DocumentoLegalPaisDTO> </returns>
        public IEnumerable<DocumentoLegalPaisDTO> ObtenerDocumentoLegalPais()
        {
            try
            {
                List<DocumentoLegalPaisDTO> rpta = new List<DocumentoLegalPaisDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdDocumentoLegal,
	                    IdPais,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM fin.T_DocumentoLegalPais
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoLegalPaisDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoLegalPais para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoLegalPaisComboDTO> </returns>
        public IEnumerable<DocumentoLegalPaisComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoLegalPaisComboDTO> rpta = new List<DocumentoLegalPaisComboDTO>();
                var query = @"
                    SELECT
	                    DLP.Id,
	                    DL.Nombre AS DocumentoLegal,
	                    P.NombrePais AS Pais
                    FROM fin.T_DocumentoLegalPais AS DLP
                    INNER JOIN fin.T_DocumentoLegal AS DL
	                    ON DLP.IdDocumentoLegal = DL.Id
	                    AND DL.Estado = 1
                    INNER JOIN conf.T_Pais AS P
	                    ON DLP.IdPais = P.Id
	                    AND P.Estado = 1
                    WHERE DLP.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoLegalPaisComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
