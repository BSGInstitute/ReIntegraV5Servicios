using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ArticuloSeoRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_ArticuloSeo
    /// </summary>
    public class ArticuloSeoRepository : GenericRepository<TArticuloSeo>, IArticuloSeoRepository
    {
        private Mapper _mapper;

        public ArticuloSeoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TArticuloSeo, ArticuloSeo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TArticuloSeo MapeoEntidad(ArticuloSeo entidad)
        {
            try
            {
                //crea la entidad padre
                TArticuloSeo modelo = _mapper.Map<TArticuloSeo>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TArticuloSeo Add(ArticuloSeo entidad)
        {
            try
            {
                var ArticuloSeo = MapeoEntidad(entidad);
                base.Insert(ArticuloSeo);
                return ArticuloSeo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TArticuloSeo Update(ArticuloSeo entidad)
        {
            try
            {
                var ArticuloSeo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ArticuloSeo.RowVersion = entidadExistente.RowVersion;

                base.Update(ArticuloSeo);
                return ArticuloSeo;
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


        public IEnumerable<TArticuloSeo> Add(IEnumerable<ArticuloSeo> listadoEntidad)
        {
            try
            {
                List<TArticuloSeo> listado = new List<TArticuloSeo>();
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

        public IEnumerable<TArticuloSeo> Update(IEnumerable<ArticuloSeo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TArticuloSeo> listado = new List<TArticuloSeo>();
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
        /// Autor: Max Mantilla Rodríguez
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiena Articulo Seo Parametro
        /// </summary>
        /// <param>IdArticulo</param>
        /// <returns>List<ParametroSeoContenidoArticuloDTO></returns>
        public List<ParametroSeoContenidoArticuloDTO> ObtenerArticuloSeoParametro(int IdArticulo)
        {
            List<ParametroSeoContenidoArticuloDTO> rpta = new List<ParametroSeoContenidoArticuloDTO>();
            string queryArticuloSeo = "Select Id,IdArticulo,Nombre,NumeroCaracteres,Descripcion From mkt.V_ObtenerParamentroArticulo where IdArticulo=@IdArticulo and EstadoParametroSeo=1 and EstadoArticuloSeo=1";
            string resultadoQueryArticuloSeo = _dapperRepository.QueryDapper(queryArticuloSeo, new { IdArticulo });
            if (!string.IsNullOrEmpty(resultadoQueryArticuloSeo) && !resultadoQueryArticuloSeo.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<ParametroSeoContenidoArticuloDTO>>(resultadoQueryArticuloSeo);
            }

            return rpta;
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ParametroSeo para los combos de T_ArticuloSeo
        /// </summary>
        /// <returns> IEnumerable<ParametroSeoComboDTO> </returns>
        public IEnumerable<ParametroSeoComboDTO> ObtenerCombo()
        {
            try
            {
                List<ParametroSeoComboDTO> rpta = new List<ParametroSeoComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.V_TParametroSEOPW_Filtro WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ParametroSeoComboDTO>>(resultado);
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
