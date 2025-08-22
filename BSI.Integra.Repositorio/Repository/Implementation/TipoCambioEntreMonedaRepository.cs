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
    /// Repositorio: TipoCambioEntreMonedaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoCambioEntreMoneda
    /// </summary>
    public class TipoCambioEntreMonedaRepository : GenericRepository<TTipoCambioEntreMonedum>, ITipoCambioEntreMonedaRepository
    {
        private Mapper _mapper;

        public TipoCambioEntreMonedaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCambioEntreMonedum, TipoCambioEntreMoneda>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TTipoCambioEntreMonedum MapeoEntidad(TipoCambioEntreMoneda entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoCambioEntreMonedum modelo = _mapper.Map<TTipoCambioEntreMonedum>(entidad);

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

        public TTipoCambioEntreMonedum Add(TipoCambioEntreMoneda entidad)
        {
            try
            {
                var TipoCambioEntreMoneda = MapeoEntidad(entidad);
                base.Insert(TipoCambioEntreMoneda);
                return TipoCambioEntreMoneda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCambioEntreMonedum Update(TipoCambioEntreMoneda entidad)
        {
            try
            {
                var TipoCambioEntreMoneda = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoCambioEntreMoneda.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoCambioEntreMoneda);
                return TipoCambioEntreMoneda;
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


        public IEnumerable<TTipoCambioEntreMonedum> Add(IEnumerable<TipoCambioEntreMoneda> listadoEntidad)
        {
            try
            {
                List<TTipoCambioEntreMonedum> listado = new List<TTipoCambioEntreMonedum>();
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

        public IEnumerable<TTipoCambioEntreMonedum> Update(IEnumerable<TipoCambioEntreMoneda> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoCambioEntreMonedum> listado = new List<TTipoCambioEntreMonedum>();
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


        public object ObtenerParaFiltro()
        {
            try
            {
                var _repTipoCambioEntreMoneda = GetBy(x => x.Estado, x => new { x.Id, x.Nombre });


                return _repTipoCambioEntreMoneda;
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }
    }
}
