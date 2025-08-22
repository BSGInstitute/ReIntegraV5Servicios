using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CalidadProcesamientoAlternoRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 29/08/2023
    /// <summary>
    /// Gestión general de T_CalidadProcesamientoAlterno
    /// </summary>
    public class CalidadProcesamientoAlternoRepository : GenericRepository<TCalidadProcesamientoAlterno>, ICalidadProcesamientoAlternoRepository
    {
        private Mapper _mapper;

        public CalidadProcesamientoAlternoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCalidadProcesamientoAlterno, CalidadProcesamientoAlterno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCalidadProcesamientoAlterno MapeoEntidad(CalidadProcesamientoAlterno entidad)
        {
            try
            {
                TCalidadProcesamientoAlterno modelo = _mapper.Map<TCalidadProcesamientoAlterno>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCalidadProcesamientoAlterno Add(CalidadProcesamientoAlterno entidad)
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
        public TCalidadProcesamientoAlterno Update(CalidadProcesamientoAlterno entidad)
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
        public IEnumerable<TCalidadProcesamientoAlterno> Add(IEnumerable<CalidadProcesamientoAlterno> listadoEntidad)
        {
            try
            {
                List<TCalidadProcesamientoAlterno> listado = new List<TCalidadProcesamientoAlterno>();
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
        public IEnumerable<TCalidadProcesamientoAlterno> Update(IEnumerable<CalidadProcesamientoAlterno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCalidadProcesamientoAlterno> listado = new List<TCalidadProcesamientoAlterno>();
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
