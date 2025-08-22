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
    /// Repositorio: CronogramaPagoDetalleModLogFinalRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CronogramaPagoDetalleModLogFinal
    /// </summary>
    public class CronogramaPagoDetalleModLogFinalRepository : GenericRepository<TCronogramaPagoDetalleModLogFinal>, ICronogramaPagoDetalleModLogFinalRepository
    {
        private Mapper _mapper;

        public CronogramaPagoDetalleModLogFinalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCronogramaPagoDetalleModLogFinal, CronogramaPagoDetalleModLogFinal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCronogramaPagoDetalleModLogFinal MapeoEntidad(CronogramaPagoDetalleModLogFinal entidad)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleModLogFinal modelo = _mapper.Map<TCronogramaPagoDetalleModLogFinal>(entidad);

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

        public TCronogramaPagoDetalleModLogFinal Add(CronogramaPagoDetalleModLogFinal entidad)
        {
            try
            {
                var CronogramaPagoDetalleModLogFinal = MapeoEntidad(entidad);
                base.Insert(CronogramaPagoDetalleModLogFinal);
                return CronogramaPagoDetalleModLogFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaPagoDetalleModLogFinal Update(CronogramaPagoDetalleModLogFinal entidad)
        {
            try
            {
                var CronogramaPagoDetalleModLogFinal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CronogramaPagoDetalleModLogFinal.RowVersion = entidadExistente.RowVersion;

                base.Update(CronogramaPagoDetalleModLogFinal);
                return CronogramaPagoDetalleModLogFinal;
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


        public IEnumerable<TCronogramaPagoDetalleModLogFinal> Add(IEnumerable<CronogramaPagoDetalleModLogFinal> listadoEntidad)
        {
            try
            {
                List<TCronogramaPagoDetalleModLogFinal> listado = new List<TCronogramaPagoDetalleModLogFinal>();
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

        public IEnumerable<TCronogramaPagoDetalleModLogFinal> Update(IEnumerable<CronogramaPagoDetalleModLogFinal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCronogramaPagoDetalleModLogFinal> listado = new List<TCronogramaPagoDetalleModLogFinal>();
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
      
    }
}
