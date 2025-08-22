using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PespecificoFrecuenciaDetalleRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PespecificoFrecuenciaDetalle
    /// </summary>
    public class PespecificoFrecuenciaDetalleRepository : GenericRepository<TPespecificoFrecuenciaDetalle>, IPespecificoFrecuenciaDetalleRepository
    {
        private Mapper _mapper;

        public PespecificoFrecuenciaDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoFrecuenciaDetalle, PespecificoFrecuenciaDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPespecificoFrecuenciaDetalle MapeoEntidad(PespecificoFrecuenciaDetalle entidad)
        {
            try
            {
                return _mapper.Map<TPespecificoFrecuenciaDetalle>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPespecificoFrecuenciaDetalle Add(PespecificoFrecuenciaDetalle entidad)
        {
            try
            {
                var PespecificoFrecuenciaDetalle = MapeoEntidad(entidad);
                base.Insert(PespecificoFrecuenciaDetalle);
                return PespecificoFrecuenciaDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPespecificoFrecuenciaDetalle Update(PespecificoFrecuenciaDetalle entidad)
        {
            try
            {
                var PespecificoFrecuenciaDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoFrecuenciaDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoFrecuenciaDetalle);
                return PespecificoFrecuenciaDetalle;
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
        public IEnumerable<TPespecificoFrecuenciaDetalle> Add(IEnumerable<PespecificoFrecuenciaDetalle> listadoEntidad)
        {
            try
            {
                List<TPespecificoFrecuenciaDetalle> listado = new List<TPespecificoFrecuenciaDetalle>();
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
        public IEnumerable<TPespecificoFrecuenciaDetalle> Update(IEnumerable<PespecificoFrecuenciaDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoFrecuenciaDetalle> listado = new List<TPespecificoFrecuenciaDetalle>();
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
        /// Obtiene PespecificoFrecuenciaDetalle por id
        /// </summary>
        /// <returns> PespecificoFrecuenciaDetalle </returns>
        public PespecificoFrecuenciaDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id, IdPEspecificoFrecuencia AS IdPespecificoFrecuencia, DiaSemana, HoraDia, Duracion, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion FROM pla.T_PEspecificoFrecuenciaDetalle
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoFrecuenciaDetalle>(resultado)!;
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
        /// Obtiene T_PespecificoFrecuenciaDetalle por Ids
        /// </summary>
        /// <returns> PespecificoFrecuenciaDetalle </returns>
        public IEnumerable<PespecificoFrecuenciaDetalle>? ObtenerPorIds(List<int> id)
        {
            try
            {
                var query = @"
                    SELECT Id, IdPEspecificoFrecuencia AS IdPespecificoFrecuencia, DiaSemana, HoraDia, Duracion, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion FROM pla.T_PEspecificoFrecuenciaDetalle
                    WHERE Estado = 1 AND Id IN @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PespecificoFrecuenciaDetalle>>(resultado)!;
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
        /// Obtiene los registros por idpespecificofrecuencia.
        /// </summary>
        /// <param name="idPespecificoFrecuencia">Id Pespecifico Frecuencia</param>
        /// <returns> PespecificoFrecuenciaDetalle </returns>
        public IEnumerable<PespecificoFrecuenciaDetalleDTO> ObtenerPorIdPespecificoFrecuencia(int idPespecificoFrecuencia)
        {
            try
            {
                string query = "SELECT Id, IdPEspecificoFrecuencia AS IdPespecificoFrecuencia, DiaSemana, HoraDia, Duracion FROM pla.V_TPEspecificoFrecuenciadetallePorIdPespecificoFrecuencia WHERE Estado=1 AND IdPEspecificoFrecuencia=@IdPespecificoFrecuencia;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPespecificoFrecuencia = idPespecificoFrecuencia });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PespecificoFrecuenciaDetalleDTO>>(resultado)!;
                }
                return new List<PespecificoFrecuenciaDetalleDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdPespecificoFrecuencia: {ex.Message}", ex);
            }
        }
    }
}



