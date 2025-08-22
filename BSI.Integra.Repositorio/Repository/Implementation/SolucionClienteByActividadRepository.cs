using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SolucionClienteByActividadRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de SolucionClienteByActividad
    /// </summary>
    public class SolucionClienteByActividadRepository : GenericRepository<TSolucionClienteByActividad>, ISolucionClienteByActividadRepository
    {
        private Mapper _mapper;

        public SolucionClienteByActividadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolucionClienteByActividad, SolucionClienteByActividad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSolucionClienteByActividad MapeoEntidad(SolucionClienteByActividad entidad)
        {
            try
            {
                //crea la entidad padre
                TSolucionClienteByActividad modelo = _mapper.Map<TSolucionClienteByActividad>(entidad);
                //mapea los hijos
                //if (entidad.AsignacionOportunidadLogs != null && entidad.AsignacionOportunidadLogs.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<TAsignacionOportunidadLog>>(entidad.AsignacionOportunidadLogs);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.TAsignacionOportunidadLogs.Add(hijoNivel1);
                //    }
                //}
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolucionClienteByActividad Add(SolucionClienteByActividad entidad)
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

        public TSolucionClienteByActividad Update(SolucionClienteByActividad entidad)
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
        public IEnumerable<TSolucionClienteByActividad> Add(IEnumerable<SolucionClienteByActividad> listadoEntidad)
        {
            try
            {
                List<TSolucionClienteByActividad> listado = new List<TSolucionClienteByActividad>();
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

        public IEnumerable<TSolucionClienteByActividad> Update(IEnumerable<SolucionClienteByActividad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolucionClienteByActividad> listado = new List<TSolucionClienteByActividad>();
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
