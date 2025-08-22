using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TagsEstiloRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TagsEstilo
    /// </summary>
    public class TagsEstiloRepository : GenericRepository<TTagEstilo>, ITagsEstiloRepository
    {
        private Mapper _mapper;

        public TagsEstiloRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTagEstilo, TagsEstilo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TTagEstilo MapeoEntidad(TagsEstilo entidad)
        {
            try
            {
                //crea la entidad padre
                TTagEstilo modelo = _mapper.Map<TTagEstilo>(entidad);

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

        public TTagEstilo Add(TagsEstilo entidad)
        {
            try
            {
                var TagsEstilo = MapeoEntidad(entidad);
                base.Insert(TagsEstilo);
                return TagsEstilo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTagEstilo Update(TagsEstilo entidad)
        {
            try
            {
                var TagsEstilo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TagsEstilo.RowVersion = entidadExistente.RowVersion;

                base.Update(TagsEstilo);
                return TagsEstilo;
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


        public IEnumerable<TTagEstilo> Add(IEnumerable<TagsEstilo> listadoEntidad)
        {
            try
            {
                List<TTagEstilo> listado = new List<TTagEstilo>();
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

        public IEnumerable<TTagEstilo> Update(IEnumerable<TagsEstilo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTagEstilo> listado = new List<TTagEstilo>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TagsEstilo para mostrarse en combo.
        /// </summary>
        /// <returns> List<TagsEstiloComboDTO> </returns>
        //public IEnumerable<ObtenerFiltro> ObtenerFiltro()
        //{
        //    try
        //    {
        //        List<ObtenerFiltro> rpta = new List<ObtenerFiltro>();

        //        var query = "SELECT Id, Nombre FROM mkt.T_TagsEstilo WHERE Estado=1";

        //        var resultado = _dapperRepository.QueryDapper(query, null);

        //        if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
        //        {
        //            rpta = JsonConvert.DeserializeObject<List<ObtenerFiltro>>(resultado);
        //        }
        //        return rpta;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TagsEstilo
        /// </summary>
        /// <returns> List<TagsEstilo> </returns>
        public IEnumerable<TagsEstilo> ObtenerTagsEstilo()
        {
            try
            {
                List<TagsEstilo> rpta = new List<TagsEstilo>();
                var query = @"select  te.IdTag, ta.Nombre, ta.Texto, es.Nombre as NombreEstilos, te.Valor from mkt.T_Tag ta inner join mkt.T_TagEstilo te on ta.Id = te.IdTag inner join mkt.T_Estilo es on te.IdEstilo = es.Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TagsEstilo>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EstiloValor> ObtenerEstiloValor(int id)
        {
            try
            {
                List<EstiloValor> rpta = new List<EstiloValor>();
                var query = @"select *,nombre as NombreTipo  from mkt.V_EstiloValor where IdTag=" + id;
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstiloValor>>(resultado);
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
