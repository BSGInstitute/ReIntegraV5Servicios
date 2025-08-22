using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoIdentidadRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoIdentidad
    /// </summary>
    public class DocumentoIdentidadRepository : GenericRepository<TDocumentoIdentidad>, IDocumentoIdentidadRepository
    {
        private Mapper _mapper;

        public DocumentoIdentidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoIdentidad, DocumentoIdentidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentoIdentidad MapeoEntidad(DocumentoIdentidad entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoIdentidad modelo = _mapper.Map<TDocumentoIdentidad>(entidad);

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

        public TDocumentoIdentidad Add(DocumentoIdentidad entidad)
        {
            try
            {
                var DocumentoIdentidad = MapeoEntidad(entidad);
                base.Insert(DocumentoIdentidad);
                return DocumentoIdentidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoIdentidad Update(DocumentoIdentidad entidad)
        {
            try
            {
                var DocumentoIdentidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoIdentidad.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoIdentidad);
                return DocumentoIdentidad;
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


        public IEnumerable<TDocumentoIdentidad> Add(IEnumerable<DocumentoIdentidad> listadoEntidad)
        {
            try
            {
                List<TDocumentoIdentidad> listado = new List<TDocumentoIdentidad>();
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

        public IEnumerable<TDocumentoIdentidad> Update(IEnumerable<DocumentoIdentidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoIdentidad> listado = new List<TDocumentoIdentidad>();
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

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoIdentidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoIdentidadComboDTO> </returns>
        public IEnumerable<DocumentoIdentidadComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoIdentidadComboDTO> rpta = new List<DocumentoIdentidadComboDTO>();
                var query = @"SELECT Id,Nombre FROM [fin].[T_DocumentoIdentidad] WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoIdentidadComboDTO>>(resultado);
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
