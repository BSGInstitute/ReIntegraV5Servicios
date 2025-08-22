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
    /// Repositorio: CronogramaPagoDetalleModRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CronogramaPagoDetalleMod
    /// </summary>
    public class CronogramaPagoDetalleModRepository : GenericRepository<TCronogramaPagoDetalleMod>, ICronogramaPagoDetalleModRepository
    {
        private Mapper _mapper;

        public CronogramaPagoDetalleModRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCronogramaPagoDetalleMod, CronogramaPagoDetalleMod>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCronogramaPagoDetalleMod MapeoEntidad(CronogramaPagoDetalleMod entidad)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleMod modelo = _mapper.Map<TCronogramaPagoDetalleMod>(entidad);

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

        public TCronogramaPagoDetalleMod Add(CronogramaPagoDetalleMod entidad)
        {
            try
            {
                var CronogramaPagoDetalleMod = MapeoEntidad(entidad);
                base.Insert(CronogramaPagoDetalleMod);
                return CronogramaPagoDetalleMod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaPagoDetalleMod Update(CronogramaPagoDetalleMod entidad)
        {
            try
            {
                var CronogramaPagoDetalleMod = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CronogramaPagoDetalleMod.RowVersion = entidadExistente.RowVersion;

                base.Update(CronogramaPagoDetalleMod);
                return CronogramaPagoDetalleMod;
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


        public IEnumerable<TCronogramaPagoDetalleMod> Add(IEnumerable<CronogramaPagoDetalleMod> listadoEntidad)
        {
            try
            {
                List<TCronogramaPagoDetalleMod> listado = new List<TCronogramaPagoDetalleMod>();
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

        public IEnumerable<TCronogramaPagoDetalleMod> Update(IEnumerable<CronogramaPagoDetalleMod> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCronogramaPagoDetalleMod> listado = new List<TCronogramaPagoDetalleMod>();
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
