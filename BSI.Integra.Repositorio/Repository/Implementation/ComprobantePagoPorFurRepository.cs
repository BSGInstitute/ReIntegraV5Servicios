using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ComprobantePagoPorFurRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_ComprobantePagoPorFur
    /// </summary>
    public class ComprobantePagoPorFurRepository : GenericRepository<TComprobantePagoPorFur>, IComprobantePagoPorFurRepository
    {
        private Mapper _mapper;

        public ComprobantePagoPorFurRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TComprobantePagoPorFur, ComprobantePagoPorFur>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TComprobantePagoPorFur MapeoEntidad(ComprobantePagoPorFur entidad)
        {
            try
            {
                //crea la entidad padre
                TComprobantePagoPorFur modelo = _mapper.Map<TComprobantePagoPorFur>(entidad);

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

        public TComprobantePagoPorFur Add(ComprobantePagoPorFur entidad)
        {
            try
            {
                var ComprobantePagoPorFur = MapeoEntidad(entidad);
                base.Insert(ComprobantePagoPorFur);
                return ComprobantePagoPorFur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TComprobantePagoPorFur Update(ComprobantePagoPorFur entidad)
        {
            try
            {
                var ComprobantePagoPorFur = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ComprobantePagoPorFur.RowVersion = entidadExistente.RowVersion;

                base.Update(ComprobantePagoPorFur);
                return ComprobantePagoPorFur;
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


        public IEnumerable<TComprobantePagoPorFur> Add(IEnumerable<ComprobantePagoPorFur> listadoEntidad)
        {
            try
            {
                List<TComprobantePagoPorFur> listado = new List<TComprobantePagoPorFur>();
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

        public IEnumerable<TComprobantePagoPorFur> Update(IEnumerable<ComprobantePagoPorFur> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TComprobantePagoPorFur> listado = new List<TComprobantePagoPorFur>();
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
        /// <summary>
        /// Genera el Reporte de Egreso por Rubro
        /// </summary>
        /// <param name="IdEmpresa"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<ReporteEgresoPorRubroDTO> ObtenerDatosReporteEgresosPorRubro(string IdEmpresa, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ReporteEgresoPorRubroDTO> gastosRubro = new List<ReporteEgresoPorRubroDTO>();
                var _query = "[fin].[SP_GenerarReporteEgresoPorRubroV5]";
                var gastosRubroDB = _dapperRepository.QuerySPDapper(_query, new
                {
                    IdEmpresa,
                    FechaInicio,
                    FechaFin
                });
                if (!gastosRubroDB.Contains("[]") && !string.IsNullOrEmpty(gastosRubroDB))
                {
                    gastosRubro = JsonConvert.DeserializeObject<List<ReporteEgresoPorRubroDTO>>(gastosRubroDB);
                }
                return gastosRubro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Genera el desgloce de ReporteEgresoPorRubro
        /// </summary>
        /// <param name="IdEmpresa"></param>
        /// <param name="FechaFin"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="IdRubro"></param>
        /// <returns></returns>
        public List<DesgloseReporteEgresoPorRubroDTO> ObtenerDesgloceReporteEgresosPorRubro(string IdEmpresa, DateTime FechaInicio, DateTime @FechaFin,int IdRubro)
        {
            try
            {
                List<DesgloseReporteEgresoPorRubroDTO> gastosRubro = new List<DesgloseReporteEgresoPorRubroDTO>();
                var _query = "[fin].[SP_GenerarDesgloseReporteEgresoPorRubroV5]";
                var gastosRubroDB = _dapperRepository.QuerySPDapper(_query, new
                {
                    IdEmpresa,
                    FechaInicio,
                    FechaFin, 
                    IdRubro
                });
                if (!gastosRubroDB.Contains("[]") && !string.IsNullOrEmpty(gastosRubroDB))
                {
                    gastosRubro = JsonConvert.DeserializeObject<List<DesgloseReporteEgresoPorRubroDTO>>(gastosRubroDB);
                }
                return gastosRubro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
