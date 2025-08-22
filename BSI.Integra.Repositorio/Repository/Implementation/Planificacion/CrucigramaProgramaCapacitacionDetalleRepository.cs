using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CrucigramaProgramaCapacitacionDetalleRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 07/09/2023
    /// <summary>
    /// Gestión general de T_CrucigramaProgramaCapacitacionDetalle
    /// </summary>
    public class CrucigramaProgramaCapacitacionDetalleRepository : GenericRepository<TCrucigramaProgramaCapacitacionDetalle>, ICrucigramaProgramaCapacitacionDetalleRepository
    {
        private Mapper _mapper;

        public CrucigramaProgramaCapacitacionDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCrucigramaProgramaCapacitacionDetalle, CrucigramaProgramaCapacitacionDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCrucigramaProgramaCapacitacionDetalle MapeoEntidad(CrucigramaProgramaCapacitacionDetalle entidad)
        {
            try
            {
                TCrucigramaProgramaCapacitacionDetalle modelo = _mapper.Map<TCrucigramaProgramaCapacitacionDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCrucigramaProgramaCapacitacionDetalle Add(CrucigramaProgramaCapacitacionDetalle entidad)
        {
            try
            {
                var CrucigramaProgramaCapacitacionDetalle = MapeoEntidad(entidad);
                base.Insert(CrucigramaProgramaCapacitacionDetalle);
                return CrucigramaProgramaCapacitacionDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCrucigramaProgramaCapacitacionDetalle Update(CrucigramaProgramaCapacitacionDetalle entidad)
        {
            try
            {
                var CrucigramaProgramaCapacitacionDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CrucigramaProgramaCapacitacionDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(CrucigramaProgramaCapacitacionDetalle);
                return CrucigramaProgramaCapacitacionDetalle;
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


        public IEnumerable<TCrucigramaProgramaCapacitacionDetalle> Add(IEnumerable<CrucigramaProgramaCapacitacionDetalle> listadoEntidad)
        {
            try
            {
                List<TCrucigramaProgramaCapacitacionDetalle> listado = new List<TCrucigramaProgramaCapacitacionDetalle>();
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

        public IEnumerable<TCrucigramaProgramaCapacitacionDetalle> Update(IEnumerable<CrucigramaProgramaCapacitacionDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCrucigramaProgramaCapacitacionDetalle> listado = new List<TCrucigramaProgramaCapacitacionDetalle>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene toda la información de la tabla T_CrucigramaProgramaCapacitacionDetalle por medio del Id
        /// </summary>
        /// <returns> Entidad - CrucigramaProgramaCapacitacionDetalle </returns>
        public CrucigramaProgramaCapacitacionDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT 
                                Id,
                                IdCrucigramaProgramaCapacitacionDetalle,
                                NumeroPalabra,
                                Palabra,
                                Definicion,
                                Tipo,
                                ColumnaInicio,
                                FilaInicio,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion 
                            FROM pla.T_CrucigramaProgramaCapacitacionDetalle
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != null)
                {
                    return JsonConvert.DeserializeObject<CrucigramaProgramaCapacitacionDetalle>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCDR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene toda la información de la tabla T_CrucigramaProgramaCapacitacionDetalle por medio del IdCrucigramaProgramaCapacitacionDetalle
        /// </summary>
        /// <param name="idCrucigramaProgramaCapacitacionDetalle"></param>
        /// <returns> Entidad - CrucigramaProgramaCapacitacionDetalle </returns>
        public List<CrucigramaProgramaCapacitacionDetalle> ObtenerPorIdCrucigramaProgramaCapacitacionDetalle(int idCrucigramaProgramaCapacitacionDetalle)
        {
            try
            {
                var query = @"SELECT 
                                Id,
                                IdCrucigramaProgramaCapacitacionDetalle,
                                NumeroPalabra,
                                Palabra,
                                Definicion,
                                Tipo,
                                ColumnaInicio,
                                FilaInicio,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion FROM pla.T_CrucigramaProgramaCapacitacionDetalle
                            WHERE Estado = 1 AND IdCrucigramaProgramaCapacitacionDetalle = @IdCrucigramaProgramaCapacitacionDetalle";
                var respuesta = _dapperRepository.QueryDapper(query, new { IdCrucigramaProgramaCapacitacionDetalle = idCrucigramaProgramaCapacitacionDetalle });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<CrucigramaProgramaCapacitacionDetalle>>(respuesta)!;
                }
                return new List<CrucigramaProgramaCapacitacionDetalle>();

            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCDR-OPICPCD-002@Error en ObtenerPorIdCrucigramaProgramaCapacitacionDetalle() {ex.Message}", ex);
            }
        }
    }
}
