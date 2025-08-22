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
    /// Repositorio: TipoDatoRepository
    /// Autor: Margiory Ramirez.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de TAnuncioFacebookMetrica
    /// </summary>
    public class AnuncioFacebookMetricaRepository : GenericRepository<TAnuncioFacebookMetrica>, IAnuncioFacebookMetricaRepository
    {
        private Mapper _mapper;

        public AnuncioFacebookMetricaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAnuncioFacebookMetrica, AnuncioFacebookMetrica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAnuncioFacebookMetrica MapeoEntidad(AnuncioFacebookMetrica entidad)
        {
            try
            {
                //crea la entidad padre
                TAnuncioFacebookMetrica modelo = _mapper.Map<TAnuncioFacebookMetrica>(entidad);

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

        public TAnuncioFacebookMetrica Add(AnuncioFacebookMetrica entidad)
        {
            try
            {
                var AnuncioFacebookMetrica = MapeoEntidad(entidad);
                base.Insert(AnuncioFacebookMetrica);
                return AnuncioFacebookMetrica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAnuncioFacebookMetrica Update(AnuncioFacebookMetrica entidad)
        {
            try
            {
                var AnuncioFacebookMetrica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AnuncioFacebookMetrica.RowVersion = entidadExistente.RowVersion;

                base.Update(AnuncioFacebookMetrica);
                return AnuncioFacebookMetrica;
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


        public IEnumerable<TAnuncioFacebookMetrica> Add(IEnumerable<AnuncioFacebookMetrica> listadoEntidad)
        {
            try
            {
                List<TAnuncioFacebookMetrica> listado = new List<TAnuncioFacebookMetrica>();
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

        public IEnumerable<TAnuncioFacebookMetrica> Update(IEnumerable<AnuncioFacebookMetrica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAnuncioFacebookMetrica> listado = new List<TAnuncioFacebookMetrica>();
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

        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener reporte de anuncio Facebook metrica
        /// </summary>
        /// <param name="idAreaCapacitacion">Objeto de clase ParametroReporteAnuncioFacebookMetricaDTO</param>
        /// <returns>Lista de objetos de clase ReporteAnuncioFacebookMetricaDTO</returns>
        public List<ReporteAnuncioFacebookMetricaDTO> ObtenerReporteAnuncioFacebookMetrica(int? idAreaCapacitacion)
        {
            try
            {
                List<ReporteAnuncioFacebookMetricaDTO> listaResultadoReporte = new List<ReporteAnuncioFacebookMetricaDTO>();

                string spReporte = "[mkt].[SP_ObtenerReporteAnuncioFacebookMetrica]";
                string resultadoReporte = _dapperRepository.QuerySPDapper(spReporte, new { IdAreaCapacitacion = idAreaCapacitacion });


                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]"))
                {
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<ReporteAnuncioFacebookMetricaDTO>>(resultadoReporte);
                }

                return listaResultadoReporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la ultima modificacion
        /// </summary>
        /// <returns>String</returns>
        public string ObtenerUltimaModificacion()
        {
            try
            {
                StringDTO resultadoCadena = new StringDTO();

                string consultaFechaModificacion = "SELECT Valor FROM mkt.V_ObtenerUltimaModificacionMetricaFacebook";
                string resultadoFechaModificacion = _dapperRepository.FirstOrDefault(consultaFechaModificacion, null);

                if (!string.IsNullOrEmpty(resultadoFechaModificacion) && !resultadoFechaModificacion.Contains("[]"))
                {
                    resultadoCadena = JsonConvert.DeserializeObject<StringDTO>(resultadoFechaModificacion);
                }

                return resultadoCadena.Valor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Margiory Ramirez .
        /// Fecha: 15/06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener las areas para el reporte de anuncio Facebook metrica basado en grupo de filtro de programas criticos
        /// </summary>
        /// <returns>Lista de objetos de clase AreaAnuncioFacebookMetricaDTO</returns>
        public List<AreaAnuncioFacebookMetricaDTO> ObtenerComboAreaAnuncioFacebookMetrica()
        {
            try
            {
                List<AreaAnuncioFacebookMetricaDTO> listaArea = new List<AreaAnuncioFacebookMetricaDTO>();

                string consultaArea = "SELECT IdGrupoFiltroProgramaCritico, NombreGrupoFiltroProgramaCritico FROM mkt.V_TGrupoFiltroProgramaCritico_ComboFacebook";
                string resultadoArea = _dapperRepository.QueryDapper(consultaArea, null);

                if (!string.IsNullOrEmpty(resultadoArea) && !resultadoArea.Contains("[]"))
                {
                    listaArea = JsonConvert.DeserializeObject<List<AreaAnuncioFacebookMetricaDTO>>(resultadoArea);
                }

                return listaArea;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// Autor: Margiory Ramirez
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado a 0 de una fecha especifica
        /// </summary>
        /// <returns>Bool</returns>
        public bool EliminarDatosPorFecha(DateTime fechaConsulta, string usuario)
        {
            try
            {
                string spReporte = "[mkt].[SP_EliminarFacebookMetricaPorFecha]";
                string resultadoReporte = _dapperRepository.QuerySPDapper(spReporte, new { FechaConsulta = fechaConsulta, Usuario = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}

