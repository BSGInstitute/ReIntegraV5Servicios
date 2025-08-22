using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PespecificoPadreFrecuenciaRepository
    /// Autor: Giancarlo Romero
    /// Fecha: 30/05/2023
    /// <summary>
    /// Gestión general de
    /// </summary>
    public class PespecificoPadreFrecuenciaRepository : GenericRepository<TPespecificoPadreFrecuencium>, IPespecificoPadreFrecuenciaRepository
    {
        private Mapper _mapper;

        public PespecificoPadreFrecuenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoPadreFrecuencium, PespecificoPadreFrecuencia>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPespecificoPadreFrecuenciaSesion, PespecificoPadreFrecuenciaSesion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoPadreFrecuencium MapeoEntidad(PespecificoPadreFrecuencia entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoPadreFrecuencium modelo = _mapper.Map<TPespecificoPadreFrecuencium>(entidad);
                if (entidad.PespecificoPadreFrecuenciaSesions != null && entidad.PespecificoPadreFrecuenciaSesions.Count() > 0)
                {
                    modelo.TPespecificoPadreFrecuenciaSesions = _mapper.Map<ICollection<TPespecificoPadreFrecuenciaSesion>>(entidad.PespecificoPadreFrecuenciaSesions);
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoPadreFrecuencium Add(PespecificoPadreFrecuencia entidad)
        {
            try
            {
                var PespecificoPadreFrecuencia = MapeoEntidad(entidad);
                base.Insert(PespecificoPadreFrecuencia);
                return PespecificoPadreFrecuencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoPadreFrecuencium Update(PespecificoPadreFrecuencia entidad)
        {
            try
            {
                var PespecificoPadreFrecuencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoPadreFrecuencia.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoPadreFrecuencia);
                return PespecificoPadreFrecuencia;
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


        public IEnumerable<TPespecificoPadreFrecuencium> Add(IEnumerable<PespecificoPadreFrecuencia> listadoEntidad)
        {
            try
            {
                List<TPespecificoPadreFrecuencium> listado = new List<TPespecificoPadreFrecuencium>();
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

        public IEnumerable<TPespecificoPadreFrecuencium> Update(IEnumerable<PespecificoPadreFrecuencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoPadreFrecuencium> listado = new List<TPespecificoPadreFrecuencium>();
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
        /// Fecha: 05/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de la tabla por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - PespecificoPadreFrecuencia </returns>
        public PespecificoPadreFrecuencia? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        IdPEspecifico AS IdPespecifico,
                        IdFrecuencia,
                        IdTiempoFrecuencia,
                        Nota,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_PEspecificoPadreFrecuencia
                    WHERE 
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoPadreFrecuencia>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPPFR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de la tabla por medio del Id PEspecifico
        /// </summary>
        /// <returns> Entidad - PespecificoPadreFrecuencia </returns>
        public PespecificoPadreFrecuencia? ObtenerPorIdPespecifico(int idPEspecifico)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        IdPEspecifico AS IdPespecifico,
                        IdFrecuencia,
                        IdTiempoFrecuencia,
                        Nota,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_PEspecificoPadreFrecuencia
                    WHERE 
                        Estado = 1 AND Id = @idPEspecifico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoPadreFrecuencia>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPPFR-OPIPe-001@Error en ObtenerPorIdPespecifico() {ex.Message}", ex);
            }
        }
    }
}
