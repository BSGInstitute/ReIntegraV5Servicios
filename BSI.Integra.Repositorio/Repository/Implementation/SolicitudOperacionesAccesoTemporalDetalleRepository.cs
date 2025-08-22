using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SolicitudOperacionesAccesoTemporalDetalleRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 24/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudOperacionesAccesoTemporalDetalle
    /// </summary>
    public class SolicitudOperacionesAccesoTemporalDetalleRepository : GenericRepository<TSolicitudOperacionesAccesoTemporalDetalle>, ISolicitudOperacionesAccesoTemporalDetalleRepository
    {
        private Mapper _mapper;

        public SolicitudOperacionesAccesoTemporalDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSolicitudOperacionesAccesoTemporalDetalle MapeoEntidad(SolicitudOperacionesAccesoTemporalDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TSolicitudOperacionesAccesoTemporalDetalle modelo = _mapper.Map<TSolicitudOperacionesAccesoTemporalDetalle>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudOperacionesAccesoTemporalDetalle Add(SolicitudOperacionesAccesoTemporalDetalle entidad)
        {
            try
            {
                var SolicitudOperacionesAccesoTemporalDetalle = MapeoEntidad(entidad);
                base.Insert(SolicitudOperacionesAccesoTemporalDetalle);
                return SolicitudOperacionesAccesoTemporalDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudOperacionesAccesoTemporalDetalle Update(SolicitudOperacionesAccesoTemporalDetalle entidad)
        {
            try
            {
                var SolicitudOperacionesAccesoTemporalDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudOperacionesAccesoTemporalDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(SolicitudOperacionesAccesoTemporalDetalle);
                return SolicitudOperacionesAccesoTemporalDetalle;
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

        public IEnumerable<TSolicitudOperacionesAccesoTemporalDetalle> Add(IEnumerable<SolicitudOperacionesAccesoTemporalDetalle> listadoEntidad)
        {
            try
            {
                List<TSolicitudOperacionesAccesoTemporalDetalle> listado = new List<TSolicitudOperacionesAccesoTemporalDetalle>();
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

        public IEnumerable<TSolicitudOperacionesAccesoTemporalDetalle> Update(IEnumerable<SolicitudOperacionesAccesoTemporalDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolicitudOperacionesAccesoTemporalDetalle> listado = new List<TSolicitudOperacionesAccesoTemporalDetalle>();
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
