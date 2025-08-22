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
    public class PuestoTrabajoRelacionRepository : GenericRepository<TPuestoTrabajoRelacion>, IPuestoTrabajoRelacionRepository
    {
        private Mapper _mapper;
        public PuestoTrabajoRelacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoRelacion, PuestoTrabajoRelacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPuestoTrabajoRelacion MapeoEntidad(PuestoTrabajoRelacion entidad)
        {
            try
            {
                TPuestoTrabajoRelacion modelo = _mapper.Map<TPuestoTrabajoRelacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoRelacion Add(PuestoTrabajoRelacion entidad)
        {
            try
            {
                var PuestoTrabajoRelacion = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoRelacion);
                return PuestoTrabajoRelacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoRelacion Update(PuestoTrabajoRelacion entidad)
        {
            try
            {
                var PuestoTrabajoRelacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoRelacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoRelacion);
                return PuestoTrabajoRelacion;
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
        public IEnumerable<TPuestoTrabajoRelacion> Add(IEnumerable<PuestoTrabajoRelacion> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoRelacion> listado = new List<TPuestoTrabajoRelacion>();
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
        public IEnumerable<TPuestoTrabajoRelacion> Update(IEnumerable<PuestoTrabajoRelacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoRelacion> listado = new List<TPuestoTrabajoRelacion>();
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
