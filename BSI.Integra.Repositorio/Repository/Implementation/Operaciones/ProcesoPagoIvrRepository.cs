using AutoMapper;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Operaciones;
using BSI.Integra.Repositorio.Repository.Interface.Operaciones;

namespace BSI.Integra.Repositorio.Repository.Implementation.Operaciones
{

    /// Repositorio: ProcesoPagoIvrRepository
    /// Autor: Gilmer Qm.
    /// Fecha: 25/08/2023
    /// <summary>
    /// Gestión general de T_ProcesoPagoIvr
    /// </summary>
    public class ProcesoPagoIvrRepository : GenericRepository<TProcesoPagoIvr>, IProcesoPagoIvrRepository
    {
        private Mapper _mapper;
        public ProcesoPagoIvrRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcesoPagoIvr, ProcesoPagoIvr>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProcesoPagoIvr MapeoEntidad(ProcesoPagoIvr entidad)
        {
            try
            {
                //crea la entidad padre
                TProcesoPagoIvr modelo = _mapper.Map<TProcesoPagoIvr>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProcesoPagoIvr Add(ProcesoPagoIvr entidad)
        {
            try
            {
                var ProcesoPagoIvr = MapeoEntidad(entidad);
                base.Insert(ProcesoPagoIvr);
                return ProcesoPagoIvr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProcesoPagoIvr Update(ProcesoPagoIvr entidad)
        {
            try
            {
                var ProcesoPagoIvr = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProcesoPagoIvr.RowVersion = entidadExistente.RowVersion;

                base.Update(ProcesoPagoIvr);
                return ProcesoPagoIvr;
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


        public IEnumerable<TProcesoPagoIvr> Add(IEnumerable<ProcesoPagoIvr> listadoEntidad)
        {
            try
            {
                List<TProcesoPagoIvr> listado = new List<TProcesoPagoIvr>();
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

        public IEnumerable<TProcesoPagoIvr> Update(IEnumerable<ProcesoPagoIvr> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProcesoPagoIvr> listado = new List<TProcesoPagoIvr>();
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
