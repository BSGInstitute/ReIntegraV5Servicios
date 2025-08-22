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
    /// Repositorio: CategoriaOrigenRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CategoriaOrigen
    /// </summary>
    public class CategoriaOrigenRepository : GenericRepository<TCategoriaOrigen>, ICategoriaOrigenRepository
    {
        private Mapper _mapper;

        public CategoriaOrigenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCategoriaOrigen, CategoriaOrigen>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCategoriaOrigen MapeoEntidad(CategoriaOrigen entidad)
        {
            try
            {
                //crea la entidad padre
                TCategoriaOrigen modelo = _mapper.Map<TCategoriaOrigen>(entidad);

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

        public TCategoriaOrigen Add(CategoriaOrigen entidad)
        {
            try
            {
                var CategoriaOrigen = MapeoEntidad(entidad);
                base.Insert(CategoriaOrigen);
                return CategoriaOrigen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCategoriaOrigen Update(CategoriaOrigen entidad)
        {
            try
            {
                var CategoriaOrigen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CategoriaOrigen.RowVersion = entidadExistente.RowVersion;

                base.Update(CategoriaOrigen);
                return CategoriaOrigen;
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


        public IEnumerable<TCategoriaOrigen> Add(IEnumerable<CategoriaOrigen> listadoEntidad)
        {
            try
            {
                List<TCategoriaOrigen> listado = new List<TCategoriaOrigen>();
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

        public IEnumerable<TCategoriaOrigen> Update(IEnumerable<CategoriaOrigen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCategoriaOrigen> listado = new List<TCategoriaOrigen>();
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
        /// Obtiene registros de T_CategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_CategoriaOrigen WHERE Estado=1";
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
        /// Obtiene todos los registros de T_CategoriaOrigen
        /// </summary>
        /// <returns> List<CategoriaOrigenDTO> </returns>
        public IEnumerable<CategoriaOrigenDTO> ObtenerCategoriaOrigen()
        {
            try
            {
                List<CategoriaOrigenDTO> rpta = new List<CategoriaOrigenDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre,Descripcion,IdTipoDato,IdTipoCategoriaOrigen,Meta,IdProveedorCampaniaIntegra,IdFormularioProcedencia,Considerar,CodigoOrigen,Estado,
	                    UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,CodigoPublicidad
                    FROM mkt.T_CategoriaOrigen
                    WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CategoriaOrigenDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Remirez Neyra.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos de V_ObtenerTipoInteraccionPorProcedenciaFormulario
        /// </summary>
        /// <returns> List<CategoriaOrigenDTO> </returns>

        public IEnumerable<TipoInteraccionFiltroDTO> TiposInteraccionPorProcedenciaFiltro()
        {

            try
            {
                List<TipoInteraccionFiltroDTO> rpta = new List<TipoInteraccionFiltroDTO>();

                var query = @"SELECT Id, Nombre,IdTipoInteraccion FROM mkt.V_ObtenerTipoInteraccionPorProcedenciaFormulario WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoInteraccionFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;


            }

            throw new NotImplementedException();
        }


        /// Autor: Margiory Remirez Neyra.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos de T_TipoCategoriaOrigen
        /// </summary>
        /// <returns> List<CategoriaOrigenDTO> </returns>

        public IEnumerable<TipoCategoriaOrigenFiltroDTO> TipoCategoriaOrigenFiltroTodo()
        {

            try
            {
                List<TipoCategoriaOrigenFiltroDTO> rpta = new List<TipoCategoriaOrigenFiltroDTO>();

                var query = @"SELECT id, Nombre FROM mkt.T_TipoCategoriaOrigen";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCategoriaOrigenFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;


            }

            throw new NotImplementedException();
        }


        /// Autor: Margiory Remirez Neyra.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos de T_TipoCategoriaOrigen
        /// </summary>
        /// <returns> List<CategoriaOrigenDTO> </returns>
        public IEnumerable<ComboFiltroDTO> ObtenerCateoriaOrigenFiltro()
        {
            try
            {
                List<ComboFiltroDTO> rpta = new List<ComboFiltroDTO>();
                var query = @"SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_ObtenerIdNombre WHERE  Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Remirez Neyra.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos de T_TipoCategoriaOrigen
        /// </summary>
        /// <returns> List<CategoriaOrigenDTO> </returns>

        public IEnumerable<ComboFiltroDTO> ObtenerFiltroCategoriaOrigen()
        {
            try
            {
                List<ComboFiltroDTO> rpta = new List<ComboFiltroDTO>();
                var query = @"SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_ObtenerIdNombre WHERE  Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Margiory Remirez Neyra.
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de CategoriaOrigen donde el nombre contenda la palabra remarketing
        /// </summary>
        /// <returns> List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO> </returns>
        public List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO> ObtenerRemarketingCategoriaOrigen()
        {
            try
            {
                var rpta = new List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO>();
                var query = @"SELECT Id, Nombre, IdTipoCategoriaOrigen FROM mkt.T_CategoriaOrigen WHERE estado =1 AND Nombre LIKE '%remarketing%'";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la categoria origen subcategoria dato
        /// </summary>
        /// <param name="idCategoriaOrigen">Id de la categoria origen (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="idTipoFormulario">Id del tipo de formulario (PK de la tabla mkt.T_TipoFormulario)</param>
        /// <returns>Objeto de clase CategoriaOrigenSubCategoriaDatoDTO</returns>
        public CategoriaOrigenSubCategoriaDatoDTO? ObtenerCategoriaOrigenSubCategoriaDato(int idCategoriaOrigen, int idTipoFormulario)
        {
            try
            {
                var query = @"SELECT IdCategoriaOrigen, IdSubCategoriaDato, CodigoOrigen, 
                                IdTipoCategoriaOrigen, NombreCategoriaOrigen 
                            FROM mkt.V_ObtenerCategoriaOrigen_SubCategoriaDato 
                            WHERE IdCategoriaOrigen = @idCategoriaOrigen AND IdTipoFormulario = @idTipoFormulario";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCategoriaOrigen, idTipoFormulario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<CategoriaOrigenSubCategoriaDatoDTO>(resultado);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 12/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id, nombre de un origen filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<ComboFiltroDTO> ObtenerCategoriaOrigenPorNombre(string nombre)
        {
            try
            {
                List<ComboFiltroDTO> categoriaOrigen = new List<ComboFiltroDTO>();
                var query = "SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_ObtenerIdNombre WHERE Estado = 1 AND Nombre = @nombre";//TODO
                var categoriaOrigenDB = _dapperRepository.QueryDapper(query, new { Nombre = nombre });
                if (!string.IsNullOrEmpty(categoriaOrigenDB) && !categoriaOrigenDB.Contains("[]"))
                {
                    categoriaOrigen = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(categoriaOrigenDB)!;
                }
                return categoriaOrigen;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Autor: Margory Ramirez Neyra
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de nombres de las Categoria Origen(activos)  registradas en el sistema 
        /// y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <param></param>
        /// <returns>Id, Nombre</returns> 
        public IEnumerable<ComboFiltroDTO> ObtenerCategoriaFiltro()
        {
            try
            {
                List<ComboFiltroDTO> categoriasOrigen = new List<ComboFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_ObtenerIdNombre WHERE  Estado = 1";
                var categoriasOrigenDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(categoriasOrigenDB) && !categoriasOrigenDB.Contains("[]"))
                {
                    categoriasOrigen = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(categoriasOrigenDB);
                }
                return categoriasOrigen;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de T_CategoriaOrigen por el Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad: CategoriaOrigen </returns>
        public CategoriaOrigen ObtenerPorId(int id)
        {
            try
            {
                CategoriaOrigen categoriaOrigen = new CategoriaOrigen();
                var query = @"SELECT 
                                Id, Nombre, Descripcion, IdTipoDato, IdTipoCategoriaOrigen, Meta, IdProveedorCampaniaIntegra, IdFormularioProcedencia, Considerar, 
                                CodigoOrigen, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion, CodigoPublicidad
                            FROM 
                                mkt.T_CategoriaOrigen 
                            WHERE 
                                Estado = 1 AND Id = @Id";
                var categoriaOrigenDB = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(categoriaOrigenDB) && categoriaOrigenDB != "null")
                {
                    categoriaOrigen = JsonConvert.DeserializeObject<CategoriaOrigen>(categoriaOrigenDB)!;
                }
                return categoriaOrigen;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 22/11/2022
        /// Version: 1.0
        /// <summary>
        /// Extrae el Id y Nombre de categoria origen de Adwords con campañas activas
        /// </summary>
        /// <param></param>
        /// <returns>List<CategoriaOrigenAdwordsDTO></returns> 
        public List<CategoriaOrigenAdwordsDTO> ObtenerCategoriaOrigenAdwords()
        {
            try
            {
                List<CategoriaOrigenAdwordsDTO> categoriasOrigenAdws = new List<CategoriaOrigenAdwordsDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre FROM [mkt].[V_ObtenerCategoriaOrigenAdwordsActivos] WHERE  Estado = 1";
                var categoriasOrigenAdwsDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(categoriasOrigenAdwsDB) && !categoriasOrigenAdwsDB.Contains("[]"))
                {
                    categoriasOrigenAdws = JsonConvert.DeserializeObject<List<CategoriaOrigenAdwordsDTO>>(categoriasOrigenAdwsDB);
                }
                return categoriasOrigenAdws;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ObtenerTipoCategoriaOrigenPorId(int id)
        {
            try
            {
                Dictionary<string, int> respuesta = new Dictionary<string, int>();
                string query = string.Empty;
                query = "SELECT IdTipoCategoriaOrigen FROM mkt.V_TCategoriaOrigen_ObtenerIdTipoCategoriaOrigen WHERE Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<Dictionary<string, int>>(resultado)!;
                }
                //REVISAR CARLOS 17082019
                return respuesta["IdTipoCategoriaOrigen"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CategoriaOrigenFiltroGrupoDTO> ObtenerCategoriaFiltroGrupo()
        {
            try
            {
                List<CategoriaOrigenFiltroGrupoDTO> CategoriaOrigenFiltro = new List<CategoriaOrigenFiltroGrupoDTO>();
                var query = string.Empty;
                query = " SELECT Id, Nombre, IdTipoCategoriaOrigen FROM mkt.T_CategoriaOrigen WHERE  Estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    CategoriaOrigenFiltro = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroGrupoDTO>>(respuestaDapper);
                }
                return CategoriaOrigenFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CategoriaOrigeCombonDTO> ObtenerCategoriaPorTipoCategoria(string TipoDato)
        {
            try
            {
                List<CategoriaOrigeCombonDTO> categoriasOrigen = new List<CategoriaOrigeCombonDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, IdTipoDato, IdTipoCategoriaOrigen FROM mkt.T_CategoriaOrigen WHERE IdTipoCategoriaOrigen in (select  item from conf.F_Splitstring(@TipoDato,','))";
                var categoriasOrigenDB = _dapperRepository.QueryDapper(_query, new { TipoDato = TipoDato });
                if (!string.IsNullOrEmpty(categoriasOrigenDB) && !categoriasOrigenDB.Contains("[]"))
                {
                    categoriasOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigeCombonDTO>>(categoriasOrigenDB);
                }
                return categoriasOrigen;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor:Gilmer Qm
        /// Fecha: 08/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de nombres de las Categoria Origen(activos)  registradas en el sistema 
        /// y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns> IEnumerable<CompuestoCategoriaOrigenConHijosDTO> </returns>
        public async Task<IEnumerable<CompuestoCategoriaOrigenConHijosDTO>> ObtenerCategoriaConHijosAsync()
        {
            try
            {
                List<CategoriaOrigenConHijosDTO> categoriasOrigen = new List<CategoriaOrigenConHijosDTO>();
                List<CompuestoCategoriaOrigenConHijosDTO> compuesto = new List<CompuestoCategoriaOrigenConHijosDTO>();
                var _query = string.Empty;
                _query = @"SELECT IdCategoriaOrigen,
                               Nombre,
                               IdSubCategoria,
                               IdTipoFormulario,
                               NombreTipoFormulario
                        FROM mkt.V_TCategoriaOrigen_ConHijos
                        WHERE EstadoCategoriaOrigen = 1
                              AND EstaSubCategoria = 1
                              AND EstadoTipoFormulario = 1;";
                var categoriasOrigenDB = await _dapperRepository.QueryDapperAsync(_query, null);
                if (!string.IsNullOrEmpty(categoriasOrigenDB) && !categoriasOrigenDB.Contains("[]"))
                {
                    categoriasOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenConHijosDTO>>(categoriasOrigenDB);

                    compuesto = (from p in categoriasOrigen
                                 group p by new
                                 {
                                     p.IdCategoriaOrigen,
                                     p.Nombre,
                                 } into g
                                 select new CompuestoCategoriaOrigenConHijosDTO
                                 {
                                     CategoriaOrigen = new ComboDTO() { Id = g.Key.IdCategoriaOrigen, Nombre = g.Key.Nombre },

                                     SubCategoriaFormulario = g.Select(o => new SubCategoriaFormularioDTO
                                     {
                                         IdSubCategoria = o.IdSubCategoria,
                                         IdTipoFormulario = o.IdTipoFormulario,
                                         NombreTipoFormulario = o.NombreTipoFormulario
                                     }).ToList(),
                                 }).ToList();
                }
                return compuesto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Autor: Margiory ramirez
        /// Fecha: 09/02/2024
        /// Version: 1.0
        /// <summary>
        /// Permite obterner el Centro de Costo por el nombre de Campaña.
        /// </summary>
        /// <param name="Nombre"> Nombre de la Campaña </param>
        /// <returns>Objeto</returns> 
        public CentroCostoCampaniaDTO ObtenerCentroCostoPorCampania(string Nombre)
        {
            try
            {
                string query = "SELECT IdCentroCosto,CentroCosto, Codigo, Campania FROM [mkt].[V_ObtenerCentroCostoPorCampania] WHERE  Codigo LIKE CONCAT('%',@Nombre) AND Estado = 1";
                var centroCostoCampaniaAdwsDB = _dapperRepository.FirstOrDefault(query, new { Nombre = Nombre });
                if (centroCostoCampaniaAdwsDB != "null")
                {
                    return JsonConvert.DeserializeObject<CentroCostoCampaniaDTO>(centroCostoCampaniaAdwsDB);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
