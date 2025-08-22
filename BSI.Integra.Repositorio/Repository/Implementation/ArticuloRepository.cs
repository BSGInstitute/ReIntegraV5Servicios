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
    /// Repositorio: ArticuloRepository
    /// Autor: Max Mantilla Rodriguez.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_Articulo
    /// </summary>
    public class ArticuloRepository : GenericRepository<TArticulo>, IArticuloRepository
    {
        private Mapper _mapper;

        public ArticuloRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TArticulo, Articulo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TArticulo MapeoEntidad(Articulo entidad)
        {
            try
            {
                //crea la entidad padre
                TArticulo modelo = _mapper.Map<TArticulo>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TArticulo Add(Articulo entidad)
        {
            try
            {
                var Articulo = MapeoEntidad(entidad);
                base.Insert(Articulo);
                return Articulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TArticulo Update(Articulo entidad)
        {
            try
            {
                var Articulo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Articulo.RowVersion = entidadExistente.RowVersion;

                base.Update(Articulo);
                return Articulo;
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


        public IEnumerable<TArticulo> Add(IEnumerable<Articulo> listadoEntidad)
        {
            try
            {
                List<TArticulo> listado = new List<TArticulo>();
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

        public IEnumerable<TArticulo> Update(IEnumerable<Articulo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TArticulo> listado = new List<TArticulo>();
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
        /// Obtiene todos los registros de T_Articulo
        /// </summary>
        /// <returns> IEnumerable<ArticuloCompuestoDTO> </returns>
        public ArticuloCompuestFiltroTotalDTO ObtenerTodo(filtroPrueba paginador)
        {
            string condicion = string.Empty;
            string nombre = string.Empty;
            string NombreCategoriaPrograma = string.Empty;
            string ImgPortada = string.Empty;
            string Titulo = string.Empty;
            string NombreArea = string.Empty;
            string NombreSubArea = string.Empty;
            string NombreExpositor = string.Empty;
            String NombreTipo = string.Empty;
            string Paginacion = "";

            int tipo = 0;
            string autor = string.Empty;
            var query = string.Empty;
            if (paginador.paginador.take != 0)
            {
                Paginacion = " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            }
            if (paginador.filter != null)
            {

                foreach (var item in paginador.filter)
                {
                    if (item.Field == "nombre" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and Nombre like @nombre ";
                        nombre = item.Value;
                    }
                    if (item.Field == "autor" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and Autor like @autor ";
                        autor = item.Value;
                    }
                    //if (item.Field == "idTipoArticulo" && item.Value.Contains(""))
                    //{
                    //    if ("WEBINAR".Contains(item.Value.ToUpper()))
                    //    {
                    //        tipo = 2;
                    //    }
                    //    else if ("WHITE PAPPER".Contains(item.Value.ToUpper()))
                    //    {
                    //        tipo = 3;
                    //    }
                    //    else if ("BLOG".Contains(item.Value.ToUpper()))
                    //    {
                    //        tipo = 1;
                    //    }
                    //    condicion = condicion + " and IdTipoArticulo =@IdTipoArticulo ";

                    //}
                    if (item.Field == "idCategoria" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and NombreCategoriaPrograma like @NombreCategoriaPrograma ";
                        NombreCategoriaPrograma = item.Value;
                    }

                    if (item.Field == "titulo" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and Titulo like @Titulo ";
                        Titulo = item.Value;
                    }

                    if (item.Field == "imgPortada" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and ImgPortada like @ImgPortada ";
                        ImgPortada = item.Value;
                    }

                    if (item.Field == "idArea" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and NombreArea like @NombreArea ";
                        NombreArea = item.Value;
                    }

                    if (item.Field == "idSubArea" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and NombreSubArea like @NombreSubArea ";
                        NombreSubArea = item.Value;
                    }

                    if (item.Field == "idExpositor" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and NombreExpositor like @NombreExpositor ";
                        NombreExpositor = item.Value;
                    }

                    if (item.Field == "idTipoArticulo" && item.Value.Contains(""))
                    {
                        condicion = condicion + " and NombreTipo like @NombreTipo ";
                        NombreTipo = item.Value;
                    }

                    

                }
            }


            ArticuloCompuestFiltroTotalDTO resultado = new ArticuloCompuestFiltroTotalDTO();

            if (paginador != null && paginador.paginador.take != 0)
            {
                string _queryFormularioArticulo = "Select Id,IdWeb,Nombre,Titulo,ImgPortada,ImgPortadaAlt,ImgSecundaria,ImgSecundariaAlt,Autor,IdTipoArticulo,NombreTipo,Contenido,IdArea,NombreArea,IdSubArea,NombreSubArea," +
                          "IdExpositor,NombreExpositor,IdCategoria,NombreCategoriaPrograma,UrlWeb,UrlDocumento,DescripcionGeneral FROM mkt.V_ObtenerRegistroArticulo WHERE Estado =1 " + condicion + " Order by Id desc " + Paginacion + "";
                var queryOportunidad = _dapperRepository.QueryDapper(_queryFormularioArticulo, new { Nombre = "%" + nombre + "%", Autor = "%" + autor + "%", IdTipoArticulo = tipo, NombreCategoriaPrograma = "%" + NombreCategoriaPrograma + "%", Titulo = "%" + Titulo + "%", ImgPortada = "%" + ImgPortada + "%", NombreArea = "%" + NombreArea + "%", NombreSubArea = "%" + NombreSubArea + "%", NombreExpositor = "%" + NombreExpositor + "%", NombreTipo = "%" + NombreTipo + "%", Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                var rpta = JsonConvert.DeserializeObject<List<ArticuloCompuestoDTO>>(queryOportunidad);
                var cantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault("Select Count(*) From mkt.V_ObtenerRegistroArticulo where Estado = 1"  + condicion + " " , new { Nombre = "%" + nombre + "%", Autor = "%" + autor + "%", IdTipoArticulo = tipo, NombreCategoriaPrograma = "%" + NombreCategoriaPrograma + "%", Titulo = "%" + Titulo + "%", ImgPortada = "%" + ImgPortada + "%", NombreArea = "%" + NombreArea + "%", NombreSubArea = "%" + NombreSubArea + "%", NombreExpositor = "%" + NombreExpositor + "%", NombreTipo = "%" + NombreTipo + "%", Skip = paginador.paginador.skip, Take = paginador.paginador.take }));


                resultado.data = rpta;
                resultado.Total = cantidadRegistros.Select(w => w.Value).FirstOrDefault();
            }
            else
            {
                string _queryFormularioArticuloTotal = "Select count(*) FROM mkt.V_ObtenerRegistroArticulo WHERE Estado =1  " + condicion + "";
                var queryOportunidad = _dapperRepository.FirstOrDefault(_queryFormularioArticuloTotal, new { Nombre = "%" + nombre + "%", Autor = "%" + autor + "%", IdTipoArticulo = tipo, NombreCategoriaPrograma = "%" + NombreCategoriaPrograma + "%", Titulo = "%" + Titulo + "%", ImgPortada = "%" + ImgPortada + "%", NombreArea = "%" + NombreArea + "%", NombreSubArea = "%" + NombreSubArea + "%", NombreExpositor = "%" + NombreExpositor + "%", NombreTipo = "%" + NombreTipo + "%", Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                var rpta = JsonConvert.DeserializeObject<List<ArticuloCompuestoDTO>>(queryOportunidad);
                var cantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapperRepository.FirstOrDefault("Select Count(*) From mkt.V_ObtenerRegistroArticulo where Estado =1 " + condicion + " ", new { Nombre = "%" + nombre + "%", Autor = "%" + autor + "%", IdTipoArticulo = tipo, NombreCategoriaPrograma = "%" + NombreCategoriaPrograma + "%", Titulo = "%" + Titulo + "%", ImgPortada = "%" + ImgPortada + "%", NombreArea = "%" + NombreArea + "%", NombreSubArea = "%" + NombreSubArea + "%", NombreExpositor = "%" + NombreExpositor + "%", NombreTipo = "%" + NombreTipo + "%", Skip = paginador.paginador.skip, Take = paginador.paginador.take }));

                resultado.data = rpta;
                resultado.Total = cantidadRegistros.Select(w => w.Value).FirstOrDefault();
            }

            return resultado;

        
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el último IdWeb para T_Articulo
        /// </summary>
        /// <returns> int </returns>
        public int? ObtenerMaximaIdWeb()
        {
            ValorIntDTO rpta = new ValorIntDTO();
            string query = "Select max(IdWeb) AS Valor From pla.T_Articulo Where Estado=1";
            string resultadoQuery = _dapperRepository.FirstOrDefault(query, null);
            if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
            {
                rpta = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoQuery);
                return rpta.Valor;
            }
            else
            {
                return 1;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PGeneral NO asociados a un determinado articulo [Id,Nombre]
        /// </summary>
        /// <param>IdArticulo</param>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerProgramasNoAsociadosArticulo(int IdArticulo)
        {
            try
            {
                List<FiltroDTO> tags = new List<FiltroDTO>();
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_ObtenerPGeneralesNoAsociadosArticulo]", new { IdArticulo });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    tags = JsonConvert.DeserializeObject<List<FiltroDTO>>(query);
                }
                return tags;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PGeneral asociados a un determinado articulo [Id,Nombre]
        /// </summary>
        /// <param>IdArticulo</param>
        /// <returns>FiltroDTO</returns>
        public List<FiltroDTO> ObtenerProgramasAsociadosArticulo(int IdArticulo)
        {
            try
            {
                List<FiltroDTO> tags = new List<FiltroDTO>();
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_ObtenerPGeneralesAsociadosArticulo]", new { IdArticulo });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    tags = JsonConvert.DeserializeObject<List<FiltroDTO>>(query);
                }
                return tags;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<ArticuloCompuestoDTO> ObtenerTodoArticulo()
        {
            List<ArticuloCompuestoDTO> rpta = new List<ArticuloCompuestoDTO>();
            string query = "Select * From  mkt.V_ObtenerRegistroArticulo Where Estado=1";
            string resultadoQuery = _dapperRepository.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<ArticuloCompuestoDTO>>(resultadoQuery);
            }
            return rpta;
        }

    
    }
}
