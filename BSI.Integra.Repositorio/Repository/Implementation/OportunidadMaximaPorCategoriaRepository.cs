using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OportunidadMaximaPorCategoriaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_OportunidadMaximaPorCategoria
    /// </summary>
    public class OportunidadMaximaPorCategoriaRepository : GenericRepository<TOportunidadMaximaPorCategorium>, IOportunidadMaximaPorCategoriaRepository
    {
        private Mapper _mapper;

        public OportunidadMaximaPorCategoriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadMaximaPorCategorium, OportunidadMaximaPorCategoria>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOportunidadMaximaPorCategorium MapeoEntidad(OportunidadMaximaPorCategoria entidad)
        {
            try
            {
                //crea la entidad padre
                TOportunidadMaximaPorCategorium modelo = _mapper.Map<TOportunidadMaximaPorCategorium>(entidad);

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

        public TOportunidadMaximaPorCategorium Add(OportunidadMaximaPorCategoria entidad)
        {
            try
            {
                var OportunidadMaximaPorCategoria = MapeoEntidad(entidad);
                base.Insert(OportunidadMaximaPorCategoria);
                return OportunidadMaximaPorCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidadMaximaPorCategorium Update(OportunidadMaximaPorCategoria entidad)
        {
            try
            {
                var OportunidadMaximaPorCategoria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OportunidadMaximaPorCategoria.RowVersion = entidadExistente.RowVersion;

                base.Update(OportunidadMaximaPorCategoria);
                return OportunidadMaximaPorCategoria;
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


        public IEnumerable<TOportunidadMaximaPorCategorium> Add(IEnumerable<OportunidadMaximaPorCategoria> listadoEntidad)
        {
            try
            {
                List<TOportunidadMaximaPorCategorium> listado = new List<TOportunidadMaximaPorCategorium>();
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

        public IEnumerable<TOportunidadMaximaPorCategorium> Update(IEnumerable<OportunidadMaximaPorCategoria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOportunidadMaximaPorCategorium> listado = new List<TOportunidadMaximaPorCategorium>();
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
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OportunidadMaximaPorCategoria.
        /// </summary>
        /// <returns> List<OportunidadMaximaPorCategoriaDTO> </returns>
        public IEnumerable<OportunidadMaximaPorCategoriaDTO> ObtenerOportunidadMaximaPorCategoria()
        {
            try
            {
                List<OportunidadMaximaPorCategoriaDTO> rpta = new List<OportunidadMaximaPorCategoriaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPersonal,
	                    IdTipoCategoriaOrigen,
	                    IdPais,
	                    OportunidadesMaximas,
	                    OportunidadesSinGenerarIS,
	                    Meta,
	                    Grupo,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_OportunidadMaximaPorCategoria
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadMaximaPorCategoriaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OportunidadMaximaPorCategoria para mostrarse en combo.
        /// </summary>
        /// <returns> List<OportunidadMaximaPorCategoriaComboDTO> </returns>
        public IEnumerable<OportunidadMaximaPorCategoriaComboDTO> ObtenerCombo()
        {
            try
            {
                List<OportunidadMaximaPorCategoriaComboDTO> rpta = new List<OportunidadMaximaPorCategoriaComboDTO>();
                var query = @"SELECT Id,IdPersonal,Grupo FROM com.T_OportunidadMaximaPorCategoria WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadMaximaPorCategoriaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Conteo de Oportunidades Cerradas por el Asesor por Grupos
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idCategoriaOrigen">Id de Categoria Origen</param>
        /// <param name="estadoPantalla">Flag: 0 -> IS, 1 -> Opo. Cerrada, 2-> Solo para mostrar</param>
        /// <returns> List<OportunidadMaximaPorCategoriaComboDTO> </returns>
        public SeguimientoAsesorDTO ObtenerSeguimientoAsesor(int idPersonal, int idCategoriaOrigen, int estadoPantalla)
        {
            try
            {
                SeguimientoAsesorDTO rpta = new SeguimientoAsesorDTO();
                var resultadoStoreProcedure = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerDatosEstaticosPantalla2", new
                {
                    IdAsesor = idPersonal,
                    IdCategoriaOrigen = idCategoriaOrigen,
                    Estado = estadoPantalla
                });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<SeguimientoAsesorDTO>(resultadoStoreProcedure);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Conteo de Oportunidades Cerradas por el Asesor por Grupos (ejemplo: Grupo 1, Grupo 2, etc) 
        /// </summary>
        /// <param name="idAsesor"> id del asesor </param>
        /// oportunidad cerrada y 2 = visualizacion solo obtiene datos para mostrar</param>        
        public void ActualizarDatosEstaticosPantalla2(int idAsesor, int idCategoriaOrigen, int estadoISOM)
        {
            try
            {
                string _querypantalla1 = "com.SP_ObtenerDatosEstaticosPantalla2";
                var querypantalla1 = _dapperRepository.QuerySPDapper(_querypantalla1, new { idAsesor = idAsesor, idCategoriaOrigen = idCategoriaOrigen, estado = estadoISOM });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Conteo de Oportunidades Cerradas por el Asesor por Grupos (ejemplo: Grupo 1, Grupo 2, etc) 
        /// </summary>
        /// <param name="idAsesor"> id del asesor </param>
        /// oportunidad cerrada y 2 = visualizacion solo obtiene datos para mostrar</param>        
        public async Task ActualizarDatosEstaticosPantalla2Async(int idAsesor, int idCategoriaOrigen, int estadoISOM)
        {
            try
            {
                string _querypantalla1 = "com.SP_ObtenerDatosEstaticosPantalla2";
                var querypantalla1 = await _dapperRepository.QuerySPDapperAsync(_querypantalla1, new { idAsesor, idCategoriaOrigen, estado = estadoISOM });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
