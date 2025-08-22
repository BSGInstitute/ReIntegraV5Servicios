using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OrigenDatoCalidadRepository
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenDatoCalidad
    /// </summary>
    public class OrigenDatoCalidadRepository : GenericRepository<TOrigenDatoCalidad>, IOrigenDatoCalidadRepository
    {
        private Mapper _mapper;

        public OrigenDatoCalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigenDatoCalidad, OrigenDatoCalidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TOrigenDatoCalidad MapeoEntidad(OrigenDatoCalidad entidad)
        {
            try
            {
                //crea la entidad padre
                TOrigenDatoCalidad modelo = _mapper.Map<TOrigenDatoCalidad>(entidad);

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

        public TOrigenDatoCalidad Add(OrigenDatoCalidad entidad)
        {
            try
            {
                var OrigenDatoCalidad = MapeoEntidad(entidad);
                base.Insert(OrigenDatoCalidad);
                return OrigenDatoCalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOrigenDatoCalidad Update(OrigenDatoCalidad entidad)
        {
            try
            {
                var OrigenDatoCalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OrigenDatoCalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(OrigenDatoCalidad);
                return OrigenDatoCalidad;
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


        public IEnumerable<TOrigenDatoCalidad> Add(IEnumerable<OrigenDatoCalidad> listadoEntidad)
        {
            try
            {
                List<TOrigenDatoCalidad> listado = new List<TOrigenDatoCalidad>();
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

        public IEnumerable<TOrigenDatoCalidad> Update(IEnumerable<OrigenDatoCalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOrigenDatoCalidad> listado = new List<TOrigenDatoCalidad>();
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
