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
    /// Repositorio: CronogramaPagoDetalleRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CronogramaPagoDetalle
    /// </summary>
    public class CronogramaPagoDetalleRepository : GenericRepository<TCronogramaPagoDetalle>, ICronogramaPagoDetalleRepository
    {
        private Mapper _mapper;

        public CronogramaPagoDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCronogramaPagoDetalle, CronogramaPagoDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCronogramaPagoDetalle MapeoEntidad(CronogramaPagoDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalle modelo = _mapper.Map<TCronogramaPagoDetalle>(entidad);

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

        public TCronogramaPagoDetalle Add(CronogramaPagoDetalle entidad)
        {
            try
            {
                var CronogramaPagoDetalle = MapeoEntidad(entidad);
                base.Insert(CronogramaPagoDetalle);
                return CronogramaPagoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaPagoDetalle Update(CronogramaPagoDetalle entidad)
        {
            try
            {
                var CronogramaPagoDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CronogramaPagoDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(CronogramaPagoDetalle);
                return CronogramaPagoDetalle;
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


        public IEnumerable<TCronogramaPagoDetalle> Add(IEnumerable<CronogramaPagoDetalle> listadoEntidad)
        {
            try
            {
                List<TCronogramaPagoDetalle> listado = new List<TCronogramaPagoDetalle>();
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

        public IEnumerable<TCronogramaPagoDetalle> Update(IEnumerable<CronogramaPagoDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCronogramaPagoDetalle> listado = new List<TCronogramaPagoDetalle>();
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

        /// Autor: Eliot Arias F.
        /// Fecha: 11/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de la lista del Cronograma detalle de la tabla T_CronogramaPagoDetalle
        /// </summary>
        /// <param name="IdMatriculaCabecera"></param>
        /// <returns>Lista de CronogramaDetalleDTO List<CronogramaDetallePagoDTO></returns>
        public List<CronogramaDetallePagoDTO> ObtenerListaDeCronogramaDetallePagoporIdMatricula(int IdMatriculaCabecera)
        {
            try
            {
                List<CronogramaDetallePagoDTO> matricula = new List<CronogramaDetallePagoDTO>();
                var _query = "SELECT *  From fin.T_CronogramaPagoDetalle WHERE   IdMatriculaCabecera=@IdMatriculaCabecera";
                var subQuery = _dapperRepository.QueryDapper(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]") && subQuery != "null")
                {
                    matricula = JsonConvert.DeserializeObject<List<CronogramaDetallePagoDTO>>(subQuery);
                }
                return matricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<int> ObtenerListaDeIdCronogramaDetallePagoporIdCabecera(int IdMatriculaCabecera)
        {
            try
            {
                List<int> matricula = new List<int>();
                var _query = "SELECT Id From fin.T_CronogramaPagoDetalle WHERE   IdMatriculaCabecera=@IdMatriculaCabecera";
                var subQuery = _dapperRepository.QueryDapper(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(subQuery) && !subQuery.Contains("[]") && subQuery != "null")
                {
                    // Deserializar en una lista de objetos anónimos y luego extraer los Ids
                    var deserializedResult = JsonConvert.DeserializeObject<List<dynamic>>(subQuery);
                    matricula = deserializedResult.Select(x => (int)x.Id).ToList();
                }

                return matricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
