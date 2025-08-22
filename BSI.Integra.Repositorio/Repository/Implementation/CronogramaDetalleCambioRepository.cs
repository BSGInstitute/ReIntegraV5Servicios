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
    /// Repositorio: CronogramaDetalleCambioRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CronogramaDetalleCambio
    /// </summary>
    public class CronogramaDetalleCambioRepository : GenericRepository<TCronogramaDetalleCambio>, ICronogramaDetalleCambioRepository
    {
        private Mapper _mapper;

        public CronogramaDetalleCambioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCronogramaDetalleCambio, CronogramaDetalleCambio>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCronogramaDetalleCambio MapeoEntidad(CronogramaDetalleCambio entidad)
        {
            try
            {
                //crea la entidad padre
                TCronogramaDetalleCambio modelo = _mapper.Map<TCronogramaDetalleCambio>(entidad);

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

        public TCronogramaDetalleCambio Add(CronogramaDetalleCambio entidad)
        {
            try
            {
                var CronogramaDetalleCambio = MapeoEntidad(entidad);
                base.Insert(CronogramaDetalleCambio);
                return CronogramaDetalleCambio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaDetalleCambio Update(CronogramaDetalleCambio entidad)
        {
            try
            {
                var CronogramaDetalleCambio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CronogramaDetalleCambio.RowVersion = entidadExistente.RowVersion;

                base.Update(CronogramaDetalleCambio);
                return CronogramaDetalleCambio;
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


        public IEnumerable<TCronogramaDetalleCambio> Add(IEnumerable<CronogramaDetalleCambio> listadoEntidad)
        {
            try
            {
                List<TCronogramaDetalleCambio> listado = new List<TCronogramaDetalleCambio>();
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

        public IEnumerable<TCronogramaDetalleCambio> Update(IEnumerable<CronogramaDetalleCambio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCronogramaDetalleCambio> listado = new List<TCronogramaDetalleCambio>();
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
        /// Obtiene los cambios pendientes por matricula y version
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="version"></param>
        public List<CambioCronogramaDTO> ObtenerCambiosPendientes(int idMatriculaCabecera, int version)
        {
            try
            {
                List<CambioCronogramaDTO> cambiosCronograma = new List<CambioCronogramaDTO>();
                var query = "SELECT TipoModificacion, SubTipo, EmailAprueba, EmailSolicita FROM fin.ObtenerCambiosPendientesPorMatricula WHERE  IdMatriculaCabecera = @idMatriculaCabecera AND Version = @version AND EstadoCronogramaDetalleCambio = 1 AND EstadoCronogramaDetalleCambio = 1 AND EstadoCronogramaCabeceraCambio = 1 AND EstadoCronogramaTipoModificacion = 1 AND EstadoCronogramaSubTipoModificacion = 1 AND EstadoPersonalAprobado = 1 AND EstadoPersonalSolicitante = 1";
                var cambiosCronogramasDB = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera, version });
                if (!string.IsNullOrEmpty(cambiosCronogramasDB) && !cambiosCronogramasDB.Contains("[]"))
                {
                    cambiosCronograma = JsonConvert.DeserializeObject<List<CambioCronogramaDTO>>(cambiosCronogramasDB);
                }
                return cambiosCronograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
