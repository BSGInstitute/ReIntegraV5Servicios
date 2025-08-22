using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoLegalAreaTrabajoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoLegalAreaTrabajo
    /// </summary>
    public class DocumentoLegalAreaTrabajoRepository : GenericRepository<TDocumentoLegalAreaTrabajo>, IDocumentoLegalAreaTrabajoRepository
    {
        private Mapper _mapper;

        public DocumentoLegalAreaTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoLegalAreaTrabajo, DocumentoLegalAreaTrabajo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentoLegalAreaTrabajo MapeoEntidad(DocumentoLegalAreaTrabajo entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoLegalAreaTrabajo modelo = _mapper.Map<TDocumentoLegalAreaTrabajo>(entidad);

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

        public TDocumentoLegalAreaTrabajo Add(DocumentoLegalAreaTrabajo entidad)
        {
            try
            {
                var DocumentoLegalAreaTrabajo = MapeoEntidad(entidad);
                base.Insert(DocumentoLegalAreaTrabajo);
                return DocumentoLegalAreaTrabajo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoLegalAreaTrabajo Update(DocumentoLegalAreaTrabajo entidad)
        {
            try
            {
                var DocumentoLegalAreaTrabajo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoLegalAreaTrabajo.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoLegalAreaTrabajo);
                return DocumentoLegalAreaTrabajo;
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


        public IEnumerable<TDocumentoLegalAreaTrabajo> Add(IEnumerable<DocumentoLegalAreaTrabajo> listadoEntidad)
        {
            try
            {
                List<TDocumentoLegalAreaTrabajo> listado = new List<TDocumentoLegalAreaTrabajo>();
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

        public IEnumerable<TDocumentoLegalAreaTrabajo> Update(IEnumerable<DocumentoLegalAreaTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoLegalAreaTrabajo> listado = new List<TDocumentoLegalAreaTrabajo>();
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
        /// Obtiene todos los registros de T_DocumentoLegalAreaTrabajo.
        /// </summary>
        /// <returns> List<DocumentoLegalAreaTrabajoDTO> </returns>
        public IEnumerable<DocumentoLegalAreaTrabajoDTO> ObtenerDocumentoLegalAreaTrabajo()
        {
            try
            {
                List<DocumentoLegalAreaTrabajoDTO> rpta = new List<DocumentoLegalAreaTrabajoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdDocumentoLegal,
	                    IdPersonalAreaTrabajo,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM fin.T_DocumentoLegalAreaTrabajo
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoLegalAreaTrabajoDTO>>(resultado);
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
        /// Obtiene registros de T_DocumentoLegalAreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoLegalAreaTrabajoComboDTO> </returns>
        public IEnumerable<DocumentoLegalAreaTrabajoComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoLegalAreaTrabajoComboDTO> rpta = new List<DocumentoLegalAreaTrabajoComboDTO>();
                var query = @"
                    SELECT
	                    DLAT.Id,
	                    DL.Nombre AS DocumentoLegal,
	                    PAT.Nombre AS PersonalAreaTrabajo
                    FROM fin.T_DocumentoLegalAreaTrabajo AS DLAT
                    INNER JOIN fin.T_DocumentoLegal AS DL
	                    ON DLAT.IdDocumentoLegal = DL.Id
	                    AND DL.Estado = 1
                    INNER JOIN gp.T_PersonalAreaTrabajo AS PAT
	                    ON DLAT.IdPersonalAreaTrabajo = PAT.Id
	                    AND PAT.Estado = 1
                    WHERE DLAT.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoLegalAreaTrabajoComboDTO>>(resultado);
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
