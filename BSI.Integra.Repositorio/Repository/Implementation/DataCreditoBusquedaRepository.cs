using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DataCreditoBusquedaRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 08/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoBusqueda
    /// </summary>
    public class DataCreditoBusquedumRepository : GenericRepository<TDataCreditoBusquedum>, IDataCreditoBusquedumRepository
    {
        private Mapper _mapper;
        public DataCreditoBusquedumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDataCreditoBusquedum, DataCreditoBusquedum>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDataCreditoBusquedum MapeoEntidad(DataCreditoBusquedum entidad)
        {
            try
            {
                //crea la entidad padre
                TDataCreditoBusquedum modelo = _mapper.Map<TDataCreditoBusquedum>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoBusquedum Add(DataCreditoBusquedum entidad)
        {
            try
            {
                var agregarEntidad = MapeoEntidad(entidad);
                base.Insert(agregarEntidad);
                return agregarEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoBusquedum Update(DataCreditoBusquedum entidad)
        {
            try
            {
                var actualizarEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                actualizarEntidad.RowVersion = entidadExistente.RowVersion;

                base.Update(actualizarEntidad);
                return actualizarEntidad;
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
        public IEnumerable<TDataCreditoBusquedum> Add(IEnumerable<DataCreditoBusquedum> listadoEntidad)
        {
            try
            {
                List<TDataCreditoBusquedum> listado = new List<TDataCreditoBusquedum>();
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

        public IEnumerable<TDataCreditoBusquedum> Update(IEnumerable<DataCreditoBusquedum> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDataCreditoBusquedum> listado = new List<TDataCreditoBusquedum>();
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
        /// Autor: Gilmer Quispe
        /// Fecha: 09/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdDataCredito de un alumno por su id.
        /// </summary>
        /// <param name="idAlumno"> Numero de documento del alumno </param>
        /// <returns> ObjetoDTO: DataCreditoDTO </returns>
        public DataCreditoDataDTO ObtenerIdDataCreditoDeAlumnoPorId(int idAlumno)
        {
            try
            {
                string query = "com.SP_DataCreditoObtenerIdPorAlumno";
                string respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno = idAlumno });
                return JsonConvert.DeserializeObject<DataCreditoDataDTO>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion de DataCredito por el Id
        /// </summary>
        /// <param name="idDataCredito"> Id de data credito en la BD </param>
        /// <returns>Lista de ObjetosDTO: List(DataCreditoInformacionDTO)</returns>
        public DataCreditoInformacionDTO ObtenerInformacionDataCreditoPorId(int idDataCredito)
        {
            try
            {
                string query = "com.SP_DataCreditoObtenerInformacionGeneral";
                string respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { IdDataCredito = idDataCredito });
                return JsonConvert.DeserializeObject<DataCreditoInformacionDTO>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de Deudas vigentes de credito de DataCredito
        /// </summary>
        /// <param name="idDataCredito"> Id de data credito en la BD </param>
        /// <returns>Lista de ObjetosDTO: List(DataCreditoCreditoVigenteDTO)</returns>
        public List<DataCreditoCreditoVigenteDTO> ObtenerHistorialDeudasDataCreditoPorId(int idDataCredito)
        {
            try
            {
                string query = "com.SP_DataCreditoObtenerCreditoVigente";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdDataCredito = idDataCredito });
                return JsonConvert.DeserializeObject<List<DataCreditoCreditoVigenteDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de tarjetas de credito de DataCredito
        /// </summary>
        /// <param name="idDataCredito"> Id de data credito en la BD </param>
        /// <returns>Lista de ObjetosDTO: List(DataCreditoTarjetaCreditoDTO)</returns>
        public List<DataCreditoTarjetaCreditoDTO> ObtenerHistorialTarjetasDataCreditoPorId(int idDataCredito)
        {
            try
            {
                string query = "com.SP_DataCreditoObtenerTarjetasCredito";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdDataCredito = idDataCredito });
                return JsonConvert.DeserializeObject<List<DataCreditoTarjetaCreditoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
