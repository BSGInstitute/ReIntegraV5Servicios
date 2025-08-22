using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CursoPespecificoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CursoPespecifico
    /// </summary>
    public class CursoPespecificoRepository : GenericRepository<TCursoPespecifico>, ICursoPespecificoRepository
    {
        private Mapper _mapper;

        public CursoPespecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCursoPespecifico, CursoPespecifico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCursoPespecifico MapeoEntidad(CursoPespecifico entidad)
        {
            try
            {
                TCursoPespecifico modelo = _mapper.Map<TCursoPespecifico>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCursoPespecifico Add(CursoPespecifico entidad)
        {
            try
            {
                var CursoPespecifico = MapeoEntidad(entidad);
                base.Insert(CursoPespecifico);
                return CursoPespecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCursoPespecifico Update(CursoPespecifico entidad)
        {
            try
            {
                var CursoPespecifico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CursoPespecifico.RowVersion = entidadExistente.RowVersion;

                base.Update(CursoPespecifico);
                return CursoPespecifico;
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


        public IEnumerable<TCursoPespecifico> Add(IEnumerable<CursoPespecifico> listadoEntidad)
        {
            try
            {
                List<TCursoPespecifico> listado = new List<TCursoPespecifico>();
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

        public IEnumerable<TCursoPespecifico> Update(IEnumerable<CursoPespecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCursoPespecifico> listado = new List<TCursoPespecifico>();
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



