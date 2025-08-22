using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PuestoTrabajoRelacionDetalleRepository : GenericRepository<TPuestoTrabajoRelacionDetalle>, IPuestoTrabajoRelacionDetalleRepository
    {
        private Mapper _mapper;
        public PuestoTrabajoRelacionDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoRelacionDetalle, PuestoTrabajoRelacionDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPuestoTrabajoRelacionDetalle MapeoEntidad(PuestoTrabajoRelacionDetalle entidad)
        {
            try
            {
                TPuestoTrabajoRelacionDetalle modelo = _mapper.Map<TPuestoTrabajoRelacionDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoRelacionDetalle Add(PuestoTrabajoRelacionDetalle entidad)
        {
            try
            {
                var PuestoTrabajoRelacionDetalle = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoRelacionDetalle);
                return PuestoTrabajoRelacionDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoRelacionDetalle Update(PuestoTrabajoRelacionDetalle entidad)
        {
            try
            {
                var PuestoTrabajoRelacionDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoRelacionDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoRelacionDetalle);
                return PuestoTrabajoRelacionDetalle;
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
        public IEnumerable<TPuestoTrabajoRelacionDetalle> Add(IEnumerable<PuestoTrabajoRelacionDetalle> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoRelacionDetalle> listado = new List<TPuestoTrabajoRelacionDetalle>();
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
        public IEnumerable<TPuestoTrabajoRelacionDetalle> Update(IEnumerable<PuestoTrabajoRelacionDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoRelacionDetalle> listado = new List<TPuestoTrabajoRelacionDetalle>();
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
