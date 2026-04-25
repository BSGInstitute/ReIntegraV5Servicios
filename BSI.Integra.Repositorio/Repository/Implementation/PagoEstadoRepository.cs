using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using EntidadPagoEstado = BSI.Integra.Persistencia.Entidades.IntegraDB.PagoEstado;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// <summary>
    /// Repositorio para gestión de catálogo T_PagoEstado
    /// </summary>
    public class PagoEstadoRepository : GenericRepository<TPagoEstado>, IPagoEstadoRepository
    {
        private Mapper _mapper;

        public PagoEstadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPagoEstado, EntidadPagoEstado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPagoEstado MapeoEntidad(EntidadPagoEstado entidad)
        {
            try
            {
                TPagoEstado modelo = _mapper.Map<TPagoEstado>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPagoEstado Add(EntidadPagoEstado entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                base.Insert(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPagoEstado Update(EntidadPagoEstado entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modelo.RowVersion = entidadExistente.RowVersion;
                base.Update(modelo);
                return modelo;
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

        public IEnumerable<TPagoEstado> Add(IEnumerable<EntidadPagoEstado> listadoEntidad)
        {
            try
            {
                List<TPagoEstado> listado = new List<TPagoEstado>();
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

        public IEnumerable<TPagoEstado> Update(IEnumerable<EntidadPagoEstado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPagoEstado> listado = new List<TPagoEstado>();
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

        public IEnumerable<PagoEstadoDTO> ObtenerPagoEstados()
        {
            try
            {
                List<PagoEstadoDTO> listaEstados = new List<PagoEstadoDTO>();

                var _query = @"SELECT Id, Nombre FROM fin.T_PagoEstado WHERE Estado = 1";

                var queryResultado = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaEstados = JsonConvert.DeserializeObject<List<PagoEstadoDTO>>(queryResultado);
                }
                return listaEstados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
