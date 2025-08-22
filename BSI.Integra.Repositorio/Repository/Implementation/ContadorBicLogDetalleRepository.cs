using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ContadorBicLogDetalleRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 29/08/2023
    /// <summary>
    /// Gestión general de T_ContadorBicLogDetalle
    /// </summary>
    public class ContadorBicLogDetalleRepository : GenericRepository<TContadorBicLogDetalle>, IContadorBicLogDetalleRepository
    {
        private Mapper _mapper;

        public ContadorBicLogDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TContadorBicLogDetalle, ContadorBicLogDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TContadorBicLogDetalle MapeoEntidad(ContadorBicLogDetalle entidad)
        {
            try
            {
                TContadorBicLogDetalle modelo = _mapper.Map<TContadorBicLogDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IEnumerable<TContadorBicLogDetalle> MapeoEntidad(IEnumerable<ContadorBicLogDetalle> entidad)
        {
            try
            {
                IEnumerable<TContadorBicLogDetalle> modelo = _mapper.Map<IEnumerable<TContadorBicLogDetalle>>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TContadorBicLogDetalle Add(ContadorBicLogDetalle entidad)
        {
            try
            {
                var agregarEntidad = MapeoEntidad(entidad);
                Insert(agregarEntidad);
                return agregarEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TContadorBicLogDetalle Update(ContadorBicLogDetalle entidad)
        {
            try
            {
                var actualizarEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                actualizarEntidad.RowVersion = entidadExistente.RowVersion;
                Update(actualizarEntidad);
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
        public IEnumerable<TContadorBicLogDetalle> Add(IEnumerable<ContadorBicLogDetalle> listadoEntidad)
        {
            try
            {
                IEnumerable<TContadorBicLogDetalle> listado = MapeoEntidad(listadoEntidad);
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TContadorBicLogDetalle> Update(IEnumerable<ContadorBicLogDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                IEnumerable<TContadorBicLogDetalle> listado = MapeoEntidad(listadoEntidad);
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<ContadorBicLogDetalle> ObtenerPorIdContadorLog(int idContadorLog)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdContadorBicLog,
		                EstadoContactoManhana,
		                EstadoContactoTarde,
		                FechaLogContacto,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
	                FROM com.T_ContadorBicLogDetalle WHERE IdContadorBicLog=@idContadorLog AND Estado=1
                    ";
                var resultado = _dapperRepository.QueryDapper(query, new { idContadorLog });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ContadorBicLogDetalle>>(resultado)!;
                }
                return new List<ContadorBicLogDetalle>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBLR-OPIO-001@Error en ObtenerPorIdContadorLog(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<ContadorBicLogDetalle> ObtenerPorIdsContadorLog(List<int> idsContadorLog)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdContadorBicLog,
		                EstadoContactoManhana,
		                EstadoContactoTarde,
		                FechaLogContacto,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
	                FROM com.T_ContadorBicLogDetalle WHERE IdContadorBicLog IN @idsContadorLog AND Estado=1
                    ";
                var resultado = _dapperRepository.QueryDapper(query, new { idsContadorLog });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ContadorBicLogDetalle>>(resultado)!;
                }
                return new List<ContadorBicLogDetalle>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBLR-OPIO-001@Error en ObtenerPorIdContadorLog(), {ex.Message}");
            }
        }
    }
}
