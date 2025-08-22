using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ProcesoSeleccionEtapaRepository : GenericRepository<TProcesoSeleccionEtapa>, IProcesoSeleccionEtapaRepository
    {
        private Mapper _mapper;
        public ProcesoSeleccionEtapaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcesoSeleccionEtapa, ProcesoSeleccionEtapa>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProcesoSeleccionEtapa, ProcesoSeleccionEtapaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProcesoSeleccionEtapa, TProcesoSeleccionEtapa>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProcesoSeleccionEtapa MapeoEntidad(ProcesoSeleccionEtapa entidad)
        {
            try
            {
                TProcesoSeleccionEtapa modelo = _mapper.Map<TProcesoSeleccionEtapa>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProcesoSeleccionEtapa Add(ProcesoSeleccionEtapa entidad)
        {
            try
            {
                var ProcesoSeleccionEtapa = MapeoEntidad(entidad);
                Insert(ProcesoSeleccionEtapa);
                return ProcesoSeleccionEtapa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProcesoSeleccionEtapa Update(ProcesoSeleccionEtapa entidad)
        {
            try
            {
                var ProcesoSeleccionEtapa = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProcesoSeleccionEtapa.RowVersion = entidadExistente.RowVersion;

                Update(ProcesoSeleccionEtapa);
                return ProcesoSeleccionEtapa;
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
        public IEnumerable<TProcesoSeleccionEtapa> Add(IEnumerable<ProcesoSeleccionEtapa> listadoEntidad)
        {
            try
            {
                List<TProcesoSeleccionEtapa> listado = new List<TProcesoSeleccionEtapa>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TProcesoSeleccionEtapa> Update(IEnumerable<ProcesoSeleccionEtapa> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProcesoSeleccionEtapa> listado = new List<TProcesoSeleccionEtapa>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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

        public IEnumerable<ProcesoSeleccionEtapaDTO> ObtenerCombo()
        {
            try
            {
                List<ProcesoSeleccionEtapaDTO> rpta = new List<ProcesoSeleccionEtapaDTO>();
                var query = @"SELECT Id, Nombre, IdProcesoSeleccion, NombreProcesoSeleccion, NroOrden FROM gp.V_ObtenerProcesoSeleccionEtapa;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcesoSeleccionEtapaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ProcesoSeleccionEtapaDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ProcesoSeleccionEtapaDTO> rpta = new List<ProcesoSeleccionEtapaDTO>();
                var query = @"SELECT Id, Nombre, IdProcesoSeleccion, NombreProcesoSeleccion, NroOrden FROM gp.V_ObtenerProcesoSeleccionEtapa;";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcesoSeleccionEtapaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcesoSeleccionEtapaDTO> ObtenerEtapaPorIdProcesoSeleccion(int idProcesoSeleccion)
        {
            try
            {
                List<ProcesoSeleccionEtapaDTO> rpta = new List<ProcesoSeleccionEtapaDTO>();
                var query = @"
                    SELECT Id, Nombre, NroOrden FROM gp.T_ProcesoSeleccionEtapa
                    WHERE IdProcesoSeleccion = @IdProcesoSeleccion
                        AND Estado  = 1
                    ORDER BY NroOrden ASC";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcesoSeleccionEtapaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 26/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Etapas del Proceso de Seleccion para combo
        /// </summary>
        /// <returns> IEnumerable<ProcesosSeleccionEtapaComboDTO> </returns>
        public IEnumerable<ProcesosSeleccionEtapaComboDTO> ObtenerComboProcesoSeleccionEtapa()
        {
            try
            {
                var queryDapper = @"SELECT
                                        Id,
                                        Nombre,
                                        IdProcesoSeleccion
                                    FROM
                                        gp.T_ProcesoSeleccionEtapa
                                    WHERE
                                        Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(queryDapper, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var respuesta = JsonConvert.DeserializeObject<IEnumerable<ProcesosSeleccionEtapaComboDTO>>(resultado);
                    return respuesta;
                }   

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProcesoSeleccionEtapa ObtenerEtapaPorId(int id)
        {
            try
            {
                ProcesoSeleccionEtapa rpta = new ProcesoSeleccionEtapa();
                var query = @"
                    SELECT Id,
                        Nombre,
                        IdProcesoSeleccion,
                        NroOrden,
                        Estado,
                        UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_ProcesoSeleccionEtapa
                    WHERE Id = @Id
                        AND Estado  = 1
                    ORDER BY NroOrden ASC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ProcesoSeleccionEtapa>(resultado)!;
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
