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
    /// Repositorio: MonedaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_Moneda
    /// </summary>
    public class MonedaRepository : GenericRepository<TMonedum>, IMonedaRepository
    {
        private Mapper _mapper;

        public MonedaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMonedum, Moneda>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMonedum MapeoEntidad(Moneda entidad)
        {
            try
            {
                //crea la entidad padre
                TMonedum modelo = _mapper.Map<TMonedum>(entidad);

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

        public TMonedum Add(Moneda entidad)
        {
            try
            {
                var Moneda = MapeoEntidad(entidad);
                base.Insert(Moneda);
                return Moneda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMonedum Update(Moneda entidad)
        {
            try
            {
                var Moneda = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Moneda.RowVersion = entidadExistente.RowVersion;

                base.Update(Moneda);
                return Moneda;
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


        public IEnumerable<TMonedum> Add(IEnumerable<Moneda> listadoEntidad)
        {
            try
            {
                List<TMonedum> listado = new List<TMonedum>();
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

        public IEnumerable<TMonedum> Update(IEnumerable<Moneda> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMonedum> listado = new List<TMonedum>();
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
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Moneda.
        /// </summary>
        /// <returns> List<MonedaDTO> </returns>
        public IEnumerable<MonedaDTO> ObtenerMoneda()
        {
            try
            {
                List<MonedaDTO> rpta = new List<MonedaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    NombreCorto,
	                    NombrePlural,
	                    Simbolo,
	                    Codigo,
	                    IdPais,
	                    DigitoFinanzas,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    ValidaProcesoSeleccion,
	                    VisualizarTableroComercial,
	                    VisualizarFinanzas,
	                    PorcentajeMora
                    FROM pla.T_Moneda
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MonedaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Moneda para mostrarse en combo.
        /// </summary>
        /// <returns> List<MonedaComboDTO> </returns>
        public IEnumerable<MonedaComboDTO> ObtenerCombo()
        {
            try
            {
                List<MonedaComboDTO> rpta = new List<MonedaComboDTO>();
                var query = @"SELECT Id,Nombre,IdPais,NombreCorto,NombrePlural,Simbolo FROM pla.T_Moneda WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MonedaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<MonedaComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id,Nombre,IdPais,NombreCorto,NombrePlural,Simbolo FROM pla.T_Moneda WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<MonedaComboDTO>>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Codigo Moneda asociado al Id del Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerCodigoMonedaPorIdAlumno(int idAlumno)
        {
            try
            {
                StringDTO codigoMoneda = new StringDTO();
                var query = @"SELECT Codigo AS Valor FROM com.V_ObtenerMonedaAlumno WHERE Id = @idAlumno";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    codigoMoneda = JsonConvert.DeserializeObject<StringDTO>(resultadoQuery);
                }
                return codigoMoneda;
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
        /// Obtiene Id, Nombre Plural y Simbolo de Moneda.
        /// </summary>
        /// <returns> List<MonedaNombrePluralSimboloDTO> </returns>
        public IEnumerable<MonedaNombrePluralSimboloDTO> ObtenerMonedaNombrePluralSimbolo()
        {
            try
            {
                List<MonedaNombrePluralSimboloDTO> monedas = new List<MonedaNombrePluralSimboloDTO>();
                var query = @"SELECT Id,NombrePlural,Simbolo FROM pla.T_Moneda WHERE Estado = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    monedas = JsonConvert.DeserializeObject<List<MonedaNombrePluralSimboloDTO>>(resultadoQuery);
                }
                return monedas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Codigo y Cambios con Dolar de Monedas
        /// </summary>
        /// <returns> List<MonedaCodigoCambioDTO> </returns>
        public IEnumerable<MonedaCodigoCambioDTO> ObtenerMonedaCodigoCambio()
        {
            try
            {
                List<MonedaCodigoCambioDTO> monedas = new List<MonedaCodigoCambioDTO>();
                var query = @"SELECT Id, Codigo, DolarAMoneda, MonedaADolar FROM pla.V_TMoneda_FiltroCodigoMoneda";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    monedas = JsonConvert.DeserializeObject<List<MonedaCodigoCambioDTO>>(resultadoQuery);
                }
                return monedas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion de Moneda asociados a un Id para DocumentoAgenda
        /// </summary>
        /// <param name="idMoneda">Id de Moneda</param>
        /// <returns> MonedaNombrePluralSimboloDTO </returns>
        public MonedaNombrePluralSimboloDTO ObtenerMonedaParaDocumento(int idMoneda)
        {
            try
            {
                MonedaNombrePluralSimboloDTO moneda = new MonedaNombrePluralSimboloDTO();
                var query = @"SELECT Id, NombrePlural, Simbolo FROM pla.T_Moneda WHERE Estado=1 AND Id=@idMoneda";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMoneda });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    moneda = JsonConvert.DeserializeObject<MonedaNombrePluralSimboloDTO>(resultado);

                return moneda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion de Moneda asociados a un Id para DocumentoAgenda
        /// </summary>
        /// <param name="idMoneda">Id de Moneda</param>
        /// <returns> MonedaNombrePluralSimboloDTO </returns>
        public async Task<MonedaNombrePluralSimboloDTO> ObtenerMonedaParaDocumentoAsync(int idMoneda)
        {
            try
            {
                MonedaNombrePluralSimboloDTO moneda = new MonedaNombrePluralSimboloDTO();
                var query = @"SELECT Id, NombrePlural, Simbolo FROM pla.T_Moneda WHERE Estado=1 AND Id=@idMoneda";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idMoneda });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    moneda = JsonConvert.DeserializeObject<MonedaNombrePluralSimboloDTO>(resultado);

                return moneda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Moneda por Id
        /// </summary>        
        /// <returns> MonedaCostoTotalConDescuentoDTO </returns>
        public List<MonedaNombrePluralSimboloDTO> ObtenerMonedaTodo()
        {
            try
            {
                List<MonedaNombrePluralSimboloDTO> items = new List<MonedaNombrePluralSimboloDTO>();
                string querymoneda = "SELECT Id,NombrePlural,Simbolo FROM pla.V_TMoneda_ObtenercamposDocumento WHERE Estado=1";
                var respuesta = _dapperRepository.QueryDapper(querymoneda, new { });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<MonedaNombrePluralSimboloDTO>>(respuesta)!;
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Moneda por Id
        /// </summary>
        /// <param name="id">Id de moneda </param>
        /// <returns> MonedaCostoTotalConDescuentoDTO </returns>
        public MonedaNombrePluralSimboloDTO ObtenerMonedaPorId(int id)
        {
            try
            {
                string querymoneda = "SELECT Id,NombrePlural,Simbolo FROM pla.V_TMoneda_ObtenercamposDocumento WHERE Id=@Id AND Estado=1";
                var respuesta = _dapperRepository.FirstOrDefault(querymoneda, new { Id = id });
                return JsonConvert.DeserializeObject<MonedaNombrePluralSimboloDTO>(respuesta)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        /// <summary>
        /// Obtiene una lista de los monedas para ser usadas en combobox (obtiene los nombres plurales)
        /// </summary>
        /// <returns></returns>
        public List<FiltroGenericoDTO> ObtenerFiltroMoneda()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.NombrePlural }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
