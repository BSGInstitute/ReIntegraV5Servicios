using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CajaEgresoAprobadoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_CajaEgresoAprobado
    /// </summary>
    public class CajaEgresoAprobadoRepository : GenericRepository<TCajaEgresoAprobado>, ICajaEgresoAprobadoRepository
    {
        private Mapper _mapper;

        public CajaEgresoAprobadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCajaEgresoAprobado, CajaEgresoAprobado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCajaEgresoAprobado MapeoEntidad(CajaEgresoAprobado entidad)
        {
            try
            {
                //crea la entidad padre
                TCajaEgresoAprobado modelo = _mapper.Map<TCajaEgresoAprobado>(entidad);

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

        public TCajaEgresoAprobado Add(CajaEgresoAprobado entidad)
        {
            try
            {
                var CajaEgresoAprobado = MapeoEntidad(entidad);
                base.Insert(CajaEgresoAprobado);
                return CajaEgresoAprobado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCajaEgresoAprobado Update(CajaEgresoAprobado entidad)
        {
            try
            {
                var CajaEgresoAprobado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CajaEgresoAprobado.RowVersion = entidadExistente.RowVersion;

                base.Update(CajaEgresoAprobado);
                return CajaEgresoAprobado;
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


        public IEnumerable<TCajaEgresoAprobado> Add(IEnumerable<CajaEgresoAprobado> listadoEntidad)
        {
            try
            {
                List<TCajaEgresoAprobado> listado = new List<TCajaEgresoAprobado>();
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

        public IEnumerable<TCajaEgresoAprobado> Update(IEnumerable<CajaEgresoAprobado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCajaEgresoAprobado> listado = new List<TCajaEgresoAprobado>();
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
