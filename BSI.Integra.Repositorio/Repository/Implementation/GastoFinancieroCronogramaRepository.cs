using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: GastoFinancieroCronogramaRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_GastoFinancieroCronograma
    /// </summary>
    public class GastoFinancieroCronogramaRepository : GenericRepository<TGastoFinancieroCronograma>, IGastoFinancieroCronogramaRepository
    {
        private Mapper _mapper;

        public GastoFinancieroCronogramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGastoFinancieroCronograma, GastoFinancieroCronograma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGastoFinancieroCronograma MapeoEntidad(GastoFinancieroCronograma entidad)
        {
            try
            {
                //crea la entidad padre
                TGastoFinancieroCronograma modelo = _mapper.Map<TGastoFinancieroCronograma>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGastoFinancieroCronograma Add(GastoFinancieroCronograma entidad)
        {
            try
            {
                var GastoFinancieroCronograma = MapeoEntidad(entidad);
                base.Insert(GastoFinancieroCronograma);
                return GastoFinancieroCronograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGastoFinancieroCronograma Update(GastoFinancieroCronograma entidad)
        {
            try
            {
                var GastoFinancieroCronograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GastoFinancieroCronograma.RowVersion = entidadExistente.RowVersion;

                base.Update(GastoFinancieroCronograma);
                return GastoFinancieroCronograma;
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


        public IEnumerable<TGastoFinancieroCronograma> Add(IEnumerable<GastoFinancieroCronograma> listadoEntidad)
        {
            try
            {
                List<TGastoFinancieroCronograma> listado = new List<TGastoFinancieroCronograma>();
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

        public IEnumerable<TGastoFinancieroCronograma> Update(IEnumerable<GastoFinancieroCronograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGastoFinancieroCronograma> listado = new List<TGastoFinancieroCronograma>();
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_GastoFinancieroCronograma.
        /// </summary>
        /// <returns> List<GastoFinancieroCronogramaDTO> </returns>
        public IEnumerable<GastoFinancieroCronogramaDatosDTO> ObtenerGastoFinancieroCronograma()
        {
            try
            {
                List<GastoFinancieroCronogramaDatosDTO> resultado = new List<GastoFinancieroCronogramaDatosDTO>();
                var _query = string.Empty;
                _query = @"SELECT 
                            Id, 
                            Nombre, 
                            IdEntidadFinanciera, 
                            NombreEntidadFinanciera, 
                            IdMoneda, 
                            NombreMoneda, 
                            CapitalTotal, 
                            InteresTotal, 
                            FechaInicio 
                        FROM fin.V_ObtenerGastoFinancieroCronograma WHERE Estado = 1 order by id desc";

                var respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<GastoFinancieroCronogramaDatosDTO>>(respuesta);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta los datos del CSV , cargados en la grilla.
        /// </summary>
        /// <returns> boolean </returns>
        public bool InsertarExcelGastoFinancieroCronograma(List<dynamic> datos, DateTime FechaCongelamiento, int IdPeriodo, string User)
        {
            try
            {
                string Json = JsonConvert.SerializeObject(datos);
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("[fin].[SP_InsertarGastoFinancieroCronograma]", new { Json, FechaCongelamiento, IdPeriodo, User });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Margiory Ramirez.
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera el reporte de Prestamos segun los filtros Dados
        /// </summary>
        /// <returns></returns>
        public List<ReporteDePrestamoDTO> ObtenerReportePrestamos(int IdEntidadFinanciera, int IdPrestamo)
        {
            try
            {
                List<ReporteDePrestamoDTO> GastoFinancieroCronogramas = new List<ReporteDePrestamoDTO>();
                var _query = string.Empty;
                _query = "SELECT NumeroCuota, FechaVencimientoCuota, CapitalCuota, InteresCuota, TotalCuota, " +
                    "NombreMoneda FROM fin.V_ObtenerGastoFinancieroParaReportePrestamos WHERE Estado = 1 AND IdEntidadFinanciera="
                    + IdEntidadFinanciera + "  and IdPrestamo=" + IdPrestamo + "  ORDER BY NumeroCuota";
                var GastoFinancieroCronogramasDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(GastoFinancieroCronogramasDB) && !GastoFinancieroCronogramasDB.Contains("[]"))
                {
                    GastoFinancieroCronogramas = JsonConvert.DeserializeObject<List<ReporteDePrestamoDTO>>(GastoFinancieroCronogramasDB);
                }
                return GastoFinancieroCronogramas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Margiory Ramirez.
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Entidades Financieras de las que se tiene un prestamo (utilizado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaEntidadFinancieraPrestamo()
        {
            try
            {
                List<FiltroDTO> EntidadesFinancieras = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM fin.V_ObtenerEntidadFinancieraConPrestamo";
                var EntidadesFinancierasDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(EntidadesFinancierasDB) && !EntidadesFinancierasDB.Contains("[]"))
                {
                    EntidadesFinancieras = JsonConvert.DeserializeObject<List<FiltroDTO>>(EntidadesFinancierasDB);
                }
                return EntidadesFinancieras;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        /// Autor: Margiory Ramirez.
        /// Fecha: 04/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Lista de Prestamos realizados registrados en GastoFinancieroCronograma
        /// </summary>
        /// <returns></returns>
        public List<PrestamoFiltroDTO> ObtenerListaPrestamosFiltro()
        {
            try
            {
                List<PrestamoFiltroDTO> Prestamos = new List<PrestamoFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, IdEntidadFinanciera FROM fin.V_ObtenerPrestamosFiltro";
                var PrestamosDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(PrestamosDB) && !PrestamosDB.Contains("[]"))
                {
                    Prestamos = JsonConvert.DeserializeObject<List<PrestamoFiltroDTO>>(PrestamosDB);
                }
                return Prestamos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
