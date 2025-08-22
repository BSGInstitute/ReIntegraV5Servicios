using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConjuntoAnuncioRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConjuntoAnuncio
    /// </summary>
    public class ConjuntoAnuncioRepository : GenericRepository<TConjuntoAnuncio>, IConjuntoAnuncioRepository
    {
        private Mapper _mapper;


        public ConjuntoAnuncioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConjuntoAnuncio, ConjuntoAnuncio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public IEnumerable<TConjuntoAnuncio> Add(IEnumerable<ConjuntoAnuncio> listadoEntidad)
        {
            try
            {
                List<TConjuntoAnuncio> listado = new List<TConjuntoAnuncio>();
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

        public IEnumerable<TConjuntoAnuncio> Update(IEnumerable<ConjuntoAnuncio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConjuntoAnuncio> listado = new List<TConjuntoAnuncio>();
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


        #region Metodos Base
        private TConjuntoAnuncio MapeoEntidad(ConjuntoAnuncio entidad)
        {
            try
            {
                //crea la entidad padre
                TConjuntoAnuncio modelo = _mapper.Map<TConjuntoAnuncio>(entidad);

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

        public TConjuntoAnuncio Add(ConjuntoAnuncio entidad)
        {
            try
            {
                var ConjuntoAnuncio = MapeoEntidad(entidad);
                base.Insert(ConjuntoAnuncio);
                return ConjuntoAnuncio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoAnuncio Update(ConjuntoAnuncio entidad)
        {
            try
            {
                var ConjuntoAnuncio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConjuntoAnuncio.RowVersion = entidadExistente.RowVersion;

                base.Update(ConjuntoAnuncio);
                return ConjuntoAnuncio;
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
        /// Obtiene registros de T_ConjuntoAnuncio para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ConjuntoAnuncio WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConjuntoAnuncio
        /// </summary>
        /// <returns> List<ConjuntoAnuncioDTO> </returns>
        public IEnumerable<ConjuntoAnuncioPanelDTO> ObtenerConjuntoAnuncio()
        {
            try
            {
                List<ConjuntoAnuncioPanelDTO> rpta = new List<ConjuntoAnuncioPanelDTO>();
                var query = @"select Id,Nombre,IdConjuntoAnuncio_Facebook,IdCategoriaOrigen, FechaCreacionCampania, NombreCategoria from mkt.V_ReporteCampaña";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConjuntoAnuncioPanelDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        IEnumerable<ConjuntoAnuncioPanelDTO> IConjuntoAnuncioRepository.ListarConjuntoAnuncios(FiltroPaginadorDTO filtro)
        {
            try
            {
                List<ConjuntoAnuncioPanelDTO> items = new List<ConjuntoAnuncioPanelDTO>();

                List<Expression<Func<TConjuntoAnuncio, bool>>> filters = new List<Expression<Func<TConjuntoAnuncio, bool>>>();
                var total = 0;
                List<TConjuntoAnuncio> lista = new List<TConjuntoAnuncio>();
                if (filtro != null && filtro.Take != 0)
                {
                    if ((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
                    {
                        // Creamos la Lista de filtros
                        foreach (var filterGrid in filtro.FiltroKendo.Filters)
                        {
                            switch (filterGrid.Field)
                            {
                                case "nombre":
                                    filters.Add(o => o.Nombre.Contains(filterGrid.Value));
                                    break;
                                case "idConjuntoAnuncio_Facebook":
                                    filters.Add(o => o.IdConjuntoAnuncioFacebook.Contains(filterGrid.Value));
                                    break;
                                case "fechaCreacionCampania":
                                    filters.Add(o => o.FechaCreacionCampania.ToString().Contains(filterGrid.Value));
                                    break;
                                case "id":
                                    filters.Add(o => o.Id.ToString().Contains(filterGrid.Value));
                                    break;
                                default:
                                    filters.Add(o => true);
                                    break;
                            }
                        }

                    }
                    //lista = this.GetFiltered(filters, p => p.Id, false).ToList();
                    var da = this.GetFilteredQuery(filters, p => p.Id, false);
                    total = da.Count();
                    lista = da.Skip(filtro.Skip).Take(filtro.Take).ToList();
                }
                else
                {
                    lista = this.GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                    total = lista.Count();
                }
                items = lista.Select(x => new ConjuntoAnuncioPanelDTO
                {
                    Id = x.Id,
                    IdConjuntoAnuncio_Facebook = x.IdConjuntoAnuncioFacebook,
                    IdProveedor = x.IdCategoriaOrigen,
                    FechaCreacionCampania = x.FechaCreacion,
                    Nombre = x.Nombre,
                    Total = total.ToString()
                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }



        public IEnumerable<CoonjuntoAnuncioUrl> ObtenerConjuntoAnuncioUrl(int IdProgramaGeneral)
        {
            try
            {
                List<CoonjuntoAnuncioUrl> rpta = new List<CoonjuntoAnuncioUrl>();
                var query = @"select * from mkt.V_UrlConjuntoAnuncio where IdProgramaGeneral = @IdProgramaGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProgramaGeneral = IdProgramaGeneral });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CoonjuntoAnuncioUrl>>(resultado);
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
