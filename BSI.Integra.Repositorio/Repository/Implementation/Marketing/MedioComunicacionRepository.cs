using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    public class MedioComunicacionRepository : GenericRepository<TMedioComunicacion>, IMedioComunicacionRepository
    {
        private Mapper _mapper;
        public MedioComunicacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMedioComunicacion, MedioComunicacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<MedioComunicacion, MedioComunicacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<MedioComunicacion, TMedioComunicacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMedioComunicacion MapeoEntidad(MedioComunicacion entidad)
        {
            try
            {
                TMedioComunicacion modelo = _mapper.Map<TMedioComunicacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMedioComunicacion Add(MedioComunicacion entidad)
        {
            try
            {
                var MedioComunicacion = MapeoEntidad(entidad);
                base.Insert(MedioComunicacion);
                return MedioComunicacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMedioComunicacion Update(MedioComunicacion entidad)
        {
            try
            {
                var MedioComunicacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MedioComunicacion.RowVersion = entidadExistente.RowVersion;

                base.Update(MedioComunicacion);
                return MedioComunicacion;
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


        public IEnumerable<TMedioComunicacion> Add(IEnumerable<MedioComunicacion> listadoEntidad)
        {
            try
            {
                List<TMedioComunicacion> listado = new List<TMedioComunicacion>();
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

        public IEnumerable<TMedioComunicacion> Update(IEnumerable<MedioComunicacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMedioComunicacion> listado = new List<TMedioComunicacion>();
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
