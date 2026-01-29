using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    public class BloqueHorarioDetalleRepository : GenericRepository<TBloqueHorarioDetalle>, IBloqueHorarioDetalleRepository
    {
        private Mapper _mapper;
        public BloqueHorarioDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TBloqueHorarioDetalle, BloqueHorarioDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<BloqueHorarioDetalle, BloqueHorarioDetalleDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<BloqueHorarioDetalle, TBloqueHorarioDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TBloqueHorarioDetalle MapeoEntidad(BloqueHorarioDetalle entidad)
        {
            try
            {
                TBloqueHorarioDetalle modelo = _mapper.Map<TBloqueHorarioDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBloqueHorarioDetalle Add(BloqueHorarioDetalle entidad)
        {
            try
            {
                var BloqueHorarioDetalle = MapeoEntidad(entidad);
                base.Insert(BloqueHorarioDetalle);
                return BloqueHorarioDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBloqueHorarioDetalle Update(BloqueHorarioDetalle entidad)
        {
            try
            {
                var BloqueHorarioDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                BloqueHorarioDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(BloqueHorarioDetalle);
                return BloqueHorarioDetalle;
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


        public IEnumerable<TBloqueHorarioDetalle> Add(IEnumerable<BloqueHorarioDetalle> listadoEntidad)
        {
            try
            {
                List<TBloqueHorarioDetalle> listado = new List<TBloqueHorarioDetalle>();
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

        public IEnumerable<TBloqueHorarioDetalle> Update(IEnumerable<BloqueHorarioDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TBloqueHorarioDetalle> listado = new List<TBloqueHorarioDetalle>();
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
