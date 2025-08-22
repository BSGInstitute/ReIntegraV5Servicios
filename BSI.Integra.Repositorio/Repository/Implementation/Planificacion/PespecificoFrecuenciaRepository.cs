using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PespecificoFrecuenciaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PespecificoFrecuencia
    /// </summary>
    public class PespecificoFrecuenciaRepository : GenericRepository<TPespecificoFrecuencium>, IPespecificoFrecuenciaRepository
    {
        private Mapper _mapper;

        public PespecificoFrecuenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoFrecuencium, PespecificoFrecuencia>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPespecificoFrecuencium MapeoEntidad(PespecificoFrecuencia entidad)
        {
            try
            {
                return _mapper.Map<TPespecificoFrecuencium>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoFrecuencium Add(PespecificoFrecuencia entidad)
        {
            try
            {
                var PespecificoFrecuencia = MapeoEntidad(entidad);
                base.Insert(PespecificoFrecuencia);
                return PespecificoFrecuencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoFrecuencium Update(PespecificoFrecuencia entidad)
        {
            try
            {
                var PespecificoFrecuencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoFrecuencia.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoFrecuencia);
                return PespecificoFrecuencia;
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
        public IEnumerable<TPespecificoFrecuencium> Add(IEnumerable<PespecificoFrecuencia> listadoEntidad)
        {
            try
            {
                List<TPespecificoFrecuencium> listado = new List<TPespecificoFrecuencium>();
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
        public IEnumerable<TPespecificoFrecuencium> Update(IEnumerable<PespecificoFrecuencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoFrecuencium> listado = new List<TPespecificoFrecuencium>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PespecificoFrecuencia por id
        /// </summary>
        /// <returns> PespecificoFrecuencia </returns>
        public PespecificoFrecuencia? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id	,
		                IdPEspecifico AS IdPespecifico,
		                FechaInicio,
		                Frecuencia,
		                NroSesiones,
		                IdFrecuencia,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                FechaFin
	                FROM pla.T_PEspecificoFrecuencia
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoFrecuencia>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PespecificoFrecuencia por id pespecifico
        /// </summary>
        /// <returns> PespecificoFrecuencia </returns>
        public PespecificoFrecuencia? ObtenerPorIdPespecifico(int idPespecifico)
        {
            try
            {
                var query = @"
                    SELECT Id	,
		                IdPEspecifico AS IdPespecifico,
		                FechaInicio,
		                Frecuencia,
		                NroSesiones,
		                IdFrecuencia,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                FechaFin
	                FROM pla.T_PEspecificoFrecuencia
                    WHERE Estado = 1 AND IdPEspecifico=@idPespecifico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoFrecuencia>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani F
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PespecificoFrecuencia.
        /// </summary>
        /// <returns> PespecificoFrecuencia </returns>
        public IEnumerable<PespecificoFrecuencia>? ObtenerPorIds(List<int> id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdPEspecifico AS IdPespecifico,
		                FechaInicio,
		                Frecuencia,
		                NroSesiones,
		                IdFrecuencia,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                FechaFin
	                FROM pla.T_PEspecificoFrecuencia
                    WHERE Estado = 1 AND Id IN @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PespecificoFrecuencia>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIds: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani F
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene pespecifico frecuencia por idpespecifico
        /// </summary>
        /// <returns> PespecificoFrecuencia </returns>
        public IEnumerable<PespecificoFrecuenciaDTO> ObtenerPespecificoFrecuenciaPorIdPespecifico(int idPespecifico)
        {
            try
            {
                string query = "SELECT Id, IdPEspecifico AS IdPespecifico, FechaInicio, Frecuencia, NroSesiones, IdFrecuencia FROM pla.V_TPEspecificoFrecuenciaByIdEspecifico WHERE Estado=1 AND IdPEspecifico=@IdPespecifico;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PespecificoFrecuenciaDTO>>(resultado);
                }
                return new List<PespecificoFrecuenciaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPespecificoFrecuenciaPorIdPespecifico: {ex.Message}", ex);
            }
        }
    }
}



