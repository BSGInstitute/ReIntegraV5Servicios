using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AsignacionOportunidadRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión general de T_AsignacionOportunidad
    /// </summary>
    public class AsignacionOportunidadRepository : GenericRepository<TAsignacionOportunidad>, IAsignacionOportunidadRepository
    {
        private Mapper _mapper;

        public AsignacionOportunidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionOportunidad, AsignacionOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAsignacionOportunidadLog, AsignacionOportunidadLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TAsignacionOportunidad MapeoEntidad(AsignacionOportunidad entidad)
        {
            try
            {
                //crea la entidad padre
                TAsignacionOportunidad modelo = _mapper.Map<TAsignacionOportunidad>(entidad);
                //mapea los hijos
                if (entidad.AsignacionOportunidadLogs != null && entidad.AsignacionOportunidadLogs.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TAsignacionOportunidadLog>>(entidad.AsignacionOportunidadLogs);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TAsignacionOportunidadLogs.Add(hijoNivel1);
                    }
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsignacionOportunidad Add(AsignacionOportunidad entidad)
        {
            try
            {
                var agregarEntidad = MapeoEntidad(entidad);
                base.Insert(agregarEntidad);
                return agregarEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsignacionOportunidad Update(AsignacionOportunidad entidad)
        {
            try
            {
                var actualizarEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                actualizarEntidad.RowVersion = entidadExistente.RowVersion;

                base.Update(actualizarEntidad);
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
        public IEnumerable<TAsignacionOportunidad> Add(IEnumerable<AsignacionOportunidad> listadoEntidad)
        {
            try
            {
                List<TAsignacionOportunidad> listado = new List<TAsignacionOportunidad>();
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

        public IEnumerable<TAsignacionOportunidad> Update(IEnumerable<AsignacionOportunidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsignacionOportunidad> listado = new List<TAsignacionOportunidad>();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la entidad por el IdOportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <returns> Entidad AsignacionOportunidad </returns>
        public AsignacionOportunidad? ObtenerPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var entidad = new AsignacionOportunidad();
                var query = @"SELECT  Id,
                                       IdOportunidad,
                                       IdPersonal,
                                       IdCentroCosto,
                                       IdAlumno,
                                       FechaAsignacion,
                                       IdTipoDato,
                                       IdFaseOportunidad,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       IdClasificacionPersona
                                FROM mkt.T_AsignacionOportunidad
                                WHERE Estado = 1 AND IdOportunidad = @idOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    entidad = JsonConvert.DeserializeObject<AsignacionOportunidad>(resultado);
                else
                    return null;
                return entidad;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de oportunidades asignadas por una fecha determinada
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <param name="fechaAsignacion"></param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerCantidadOportunidadesAsesor(int idAsesor, DateTime fechaAsignacion)
        {
            try
            {
                var cantidadAsignacion = new ValorIntDTO();
                var _query = @"SELECT Count(Id) AS Valor 
                                FROM mkt.V_TAsignacionOportunidad_ObtenerCantidadOportunidadesAsesor 
                                WHERE IdPersonal = @idAsesor 
                                AND CONVERT(DATE, FechaAsignacion) = Convert(date, @fechaAsignacion) 
                                AND Estado = 1 ";
                var registrosBD = _dapperRepository.FirstOrDefault(_query, new { idAsesor, fechaAsignacion });
                cantidadAsignacion = JsonConvert.DeserializeObject<ValorIntDTO>(registrosBD);
                return cantidadAsignacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Máxima asignacion por el asesor
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerMaximaAsignacionAsesor(int idAsesor)
        {
            try
            {
                var maximaAsignacionAsesorCentroCosto = new ValorIntDTO();
                var _query = "SELECT AsignacionMax AS Valor FROM mkt.V_TAsesorCentroCosto_ObtenerAsignacionMaxima WHERE IdPersonal = @idAsesor AND Estado = 1";
                var maximaAsignacionAsesorCentroCostoDB = _dapperRepository.FirstOrDefault(_query, new { idAsesor });
                maximaAsignacionAsesorCentroCosto = JsonConvert.DeserializeObject<ValorIntDTO>(maximaAsignacionAsesorCentroCostoDB);
                return maximaAsignacionAsesorCentroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
