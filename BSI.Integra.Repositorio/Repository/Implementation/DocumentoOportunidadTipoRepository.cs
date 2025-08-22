using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoOportunidadTipoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoOportunidadTipo
    /// </summary>
    public class DocumentoOportunidadTipoRepository : GenericRepository<TDocumentoOportunidadTipo>, IDocumentoOportunidadTipoRepository
    {
        private Mapper _mapper;

        public DocumentoOportunidadTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoOportunidadTipo, DocumentoOportunidadTipo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentoOportunidadTipo MapeoEntidad(DocumentoOportunidadTipo entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoOportunidadTipo modelo = _mapper.Map<TDocumentoOportunidadTipo>(entidad);

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

        public TDocumentoOportunidadTipo Add(DocumentoOportunidadTipo entidad)
        {
            try
            {
                var DocumentoOportunidadTipo = MapeoEntidad(entidad);
                base.Insert(DocumentoOportunidadTipo);
                return DocumentoOportunidadTipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoOportunidadTipo Update(DocumentoOportunidadTipo entidad)
        {
            try
            {
                var DocumentoOportunidadTipo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoOportunidadTipo.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoOportunidadTipo);
                return DocumentoOportunidadTipo;
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


        public IEnumerable<TDocumentoOportunidadTipo> Add(IEnumerable<DocumentoOportunidadTipo> listadoEntidad)
        {
            try
            {
                List<TDocumentoOportunidadTipo> listado = new List<TDocumentoOportunidadTipo>();
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

        public IEnumerable<TDocumentoOportunidadTipo> Update(IEnumerable<DocumentoOportunidadTipo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoOportunidadTipo> listado = new List<TDocumentoOportunidadTipo>();
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
        /// Obtiene todos los registros de T_DocumentoOportunidadTipo.
        /// </summary>
        /// <returns> List<DocumentoOportunidadTipoDTO> </returns>
        public IEnumerable<DocumentoOportunidadTipoDTO> ObtenerDocumentoOportunidadTipo()
        {
            try
            {
                List<DocumentoOportunidadTipoDTO> rpta = new List<DocumentoOportunidadTipoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_DocumentoOportunidadTipo
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoOportunidadTipoDTO>>(resultado);
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
        /// Obtiene registros de T_DocumentoOportunidadTipo para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoOportunidadTipoComboDTO> </returns>
        public IEnumerable<DocumentoOportunidadTipoComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoOportunidadTipoComboDTO> rpta = new List<DocumentoOportunidadTipoComboDTO>();
                var query = @"SELECT Id,Nombre FROM com.T_DocumentoOportunidadTipo WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoOportunidadTipoComboDTO>>(resultado);
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
