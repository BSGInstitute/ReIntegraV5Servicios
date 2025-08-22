using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EstadoProgramaEspecificoRepository
    /// Autor: Max Mantilla.
    /// Fecha: 20/07/2023
    /// <summary>
    /// Gestión general de T_EstadoPEspecifico
    /// </summary>
    public class EstadoProgramaEspecificoRepository : GenericRepository<TEstadoPespecifico>, IEstadoProgramaEspecificoRepository
    {
        private Mapper _mapper;

        public EstadoProgramaEspecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoPespecifico, EstadoPespecifico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEstadoPespecifico MapeoEntidad(EstadoPespecifico entidad)
        {
            try
            {
                //crea la entidad padre
                TEstadoPespecifico modelo = _mapper.Map<TEstadoPespecifico>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoPespecifico Add(EstadoPespecifico entidad)
        {
            try
            {
                var EstadoPespecifico = MapeoEntidad(entidad);
                base.Insert(EstadoPespecifico);
                return EstadoPespecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoPespecifico Update(EstadoPespecifico entidad)
        {
            try
            {
                var EstadoPespecifico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstadoPespecifico.RowVersion = entidadExistente.RowVersion;

                base.Update(EstadoPespecifico);
                return EstadoPespecifico;
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


        public IEnumerable<TEstadoPespecifico> Add(IEnumerable<EstadoPespecifico> listadoEntidad)
        {
            try
            {
                List<TEstadoPespecifico> listado = new List<TEstadoPespecifico>();
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

        public IEnumerable<TEstadoPespecifico> Update(IEnumerable<EstadoPespecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstadoPespecifico> listado = new List<TEstadoPespecifico>();
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

        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de EstadoPespecifico para combo
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FiltroDTO> ObtenerEstadoPespecificoParaCombo()
        {
            try
            {
                var combo = GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre });
                return combo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<ComboDTO> ObtenerComboEstado()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.T_EstadoPEspecifico WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
