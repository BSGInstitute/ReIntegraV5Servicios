using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class DescuentoPromocionRepository : GenericRepository<TDescuentoPromocion>, IDescuentoPromocionRepository
    {
        private Mapper _mapper;

        public DescuentoPromocionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDescuentoPromocion, DescuentoPromocion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDescuentoPromocion MapeoEntidad(DescuentoPromocion entidad)
        {
            try
            {
                TDescuentoPromocion modelo = _mapper.Map<TDescuentoPromocion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TDescuentoPromocion Add(DescuentoPromocion entidad)
        {
            try
            {
                var nuevaEntidad = MapeoEntidad(entidad);
                base.Insert(nuevaEntidad);
                return nuevaEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TDescuentoPromocion Update(DescuentoPromocion entidad)
        {
            try
            {
                var nuevaEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.IdTipoDescuento, s => new { s.RowVersion });
                nuevaEntidad.RowVersion = entidadExistente.RowVersion;

                base.Update(nuevaEntidad);
                return nuevaEntidad;
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
        public IEnumerable<TDescuentoPromocion> Add(IEnumerable<DescuentoPromocion> listadoEntidad)
        {
            try
            {
                List<TDescuentoPromocion> listado = new List<TDescuentoPromocion>();
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
        public IEnumerable<TDescuentoPromocion> Update(IEnumerable<DescuentoPromocion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDescuentoPromocion> listado = new List<TDescuentoPromocion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.IdTipoDescuento).Contains(w.Id), s => new { s.Id, s.RowVersion });
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

        /// Autor: Klebert Layme
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DescuentoPromocion.
        /// </summary>
        /// <returns> List<DescuentoPromocionDTO> </returns>
        public IEnumerable<DescuentoPromocionDTO> Obtener()
        {
            try
            {
                IEnumerable<DescuentoPromocionDTO> rpta = new List<DescuentoPromocionDTO>();
                var query = @"SELECT Id,IdTipoDescuento
                                FROM pla.T_DescuentoPromocion
                                WHERE Estado = 1
                                ORDER BY Id DESC;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<DescuentoPromocionDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DescuentoPromocion.
        /// </summary>
        /// <returns> List<DescuentoPromocionDTO> </returns>
        public IEnumerable<DescuentoPromocion> ObtenerPorIdTipoDescuento(int idTipoDescuento)
        {
            try
            {
                IEnumerable<DescuentoPromocion> rpta = new List<DescuentoPromocion>();
                var query = @"
                    SELECT
	                    Id,
				        IdTipoDescuento,
				        Estado,
				        UsuarioCreacion,
				        UsuarioModificacion,
				        FechaCreacion,
				        FechaModificacion,
				        RowVersion,
				        IdMigracion
                    FROM pla.T_DescuentoPromocion
                    WHERE Estado = 1 AND IdTipoDescuento=@idTipoDescuento";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<DescuentoPromocion>>(resultado)!;
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
