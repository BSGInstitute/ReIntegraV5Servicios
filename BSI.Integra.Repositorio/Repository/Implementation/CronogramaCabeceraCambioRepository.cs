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
    /// Repositorio: CronogramaCabeceraCambioRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CronogramaCabeceraCambio
    /// </summary>
    public class CronogramaCabeceraCambioRepository : GenericRepository<TCronogramaCabeceraCambio>, ICronogramaCabeceraCambioRepository
    {
        private Mapper _mapper;

        public CronogramaCabeceraCambioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCronogramaCabeceraCambio, CronogramaCabeceraCambio>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCronogramaCabeceraCambio MapeoEntidad(CronogramaCabeceraCambio entidad)
        {
            try
            {
                //crea la entidad padre
                TCronogramaCabeceraCambio modelo = _mapper.Map<TCronogramaCabeceraCambio>(entidad);

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

        public TCronogramaCabeceraCambio Add(CronogramaCabeceraCambio entidad)
        {
            try
            {
                var CronogramaCabeceraCambio = MapeoEntidad(entidad);
                base.Insert(CronogramaCabeceraCambio);
                return CronogramaCabeceraCambio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaCabeceraCambio Update(CronogramaCabeceraCambio entidad)
        {
            try
            {
                var CronogramaCabeceraCambio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CronogramaCabeceraCambio.RowVersion = entidadExistente.RowVersion;

                base.Update(CronogramaCabeceraCambio);
                return CronogramaCabeceraCambio;
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


        public IEnumerable<TCronogramaCabeceraCambio> Add(IEnumerable<CronogramaCabeceraCambio> listadoEntidad)
        {
            try
            {
                List<TCronogramaCabeceraCambio> listado = new List<TCronogramaCabeceraCambio>();
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

        public IEnumerable<TCronogramaCabeceraCambio> Update(IEnumerable<CronogramaCabeceraCambio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCronogramaCabeceraCambio> listado = new List<TCronogramaCabeceraCambio>();
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
        /// Aprueba o rechaza los cambios de un cronograma
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idCambio"></param>
        /// <param name="version"></param>
        /// <param name="aprobado"></param>
        /// <param name="cancelado"></param>
        /// <returns></returns>
        public CambiosCronogramaCabeceraDTO AprobarRechazarCambios(int idMatriculaCabecera, int idCambio, int version, bool aprobado, bool cancelado)
        {
            try
            {
                CambiosCronogramaCabeceraDTO cambiosCronogramaCabecera = new CambiosCronogramaCabeceraDTO();
                var matriculasBD = _dapperRepository.QuerySPFirstOrDefault("fin.Aprobarcancelarcambios", new { idMatriculaCabecera, idCambio, version, aprobado, cancelado });
                if (!matriculasBD.Equals("null") && !string.IsNullOrEmpty(matriculasBD))
                {
                    cambiosCronogramaCabecera = JsonConvert.DeserializeObject<CambiosCronogramaCabeceraDTO>(matriculasBD);
                }
                return cambiosCronogramaCabecera;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
