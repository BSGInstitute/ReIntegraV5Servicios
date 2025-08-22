using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ArticuloTagRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_ArticuloTag
    /// </summary>
    public class ArticuloTagRepository : GenericRepository<TArticuloTag>, IArticuloTagRepository
    {
        private Mapper _mapper;

        public ArticuloTagRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TArticuloTag, ArticuloTag>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TArticuloTag MapeoEntidad(ArticuloTag entidad)
        {
            try
            {
                //crea la entidad padre
                TArticuloTag modelo = _mapper.Map<TArticuloTag>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TArticuloTag Add(ArticuloTag entidad)
        {
            try
            {
                var ArticuloTag = MapeoEntidad(entidad);
                base.Insert(ArticuloTag);
                return ArticuloTag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TArticuloTag Update(ArticuloTag entidad)
        {
            try
            {
                var ArticuloTag = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ArticuloTag.RowVersion = entidadExistente.RowVersion;

                base.Update(ArticuloTag);
                return ArticuloTag;
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


        public IEnumerable<TArticuloTag> Add(IEnumerable<ArticuloTag> listadoEntidad)
        {
            try
            {
                List<TArticuloTag> listado = new List<TArticuloTag>();
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

        public IEnumerable<TArticuloTag> Update(IEnumerable<ArticuloTag> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TArticuloTag> listado = new List<TArticuloTag>();
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

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de Tags asociados para T_Articulo
        /// </summary>
        /// <returns> List<ArticuloTag> </returns>
        public List<ArticuloTag> ObtenerArticuloTagsAsociados(int IdArticulo)
        {
            List<ArticuloTag> rpta = new List<ArticuloTag>();
            string query = @"Select Id,IdArticulo, IdTag,UsuarioCreacion,FechaCreacion From pla.T_ArticuloTag Where Estado=1 and IdArticulo=@IdArticulo";
            string resultadoQuery = _dapperRepository.QueryDapper(query, new { IdArticulo = IdArticulo });
            if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
            {
                rpta = JsonConvert.DeserializeObject<List<ArticuloTag>>(resultadoQuery);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de Tags asociados para T_Articulo [Id,Nombre]
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerTagsAsociadosArticulo(int IdArticulo)
        {
            try
            {
                List<FiltroDTO> tags = new List<FiltroDTO>();
                string _query = string.Empty;
                _query = "SELECT Id,Nombre FROM [pla].[V_ArticulosTagsAsociados] WHERE IdArticulo=" + IdArticulo;
                var querySeo = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(querySeo) && !querySeo.Contains("[]"))
                {
                    tags = JsonConvert.DeserializeObject<List<FiltroDTO>>(querySeo);
                }
                return tags;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
