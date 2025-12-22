using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionContactoRepository : GenericRepository<TGestionContacto>, IGestionContactoRepository
    {
        private Mapper _mapper;

        public GestionContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionContacto, TGestionContacto>().ReverseMap();
                cfg.CreateMap<GestionContactoLog, TGestionContactoLog>().ReverseMap();
                cfg.CreateMap<ActividadDetalleGestionContacto, TActividadDetalleGestionContacto>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionContacto AddAsync(GestionContacto entidad)
        {
            try
            {
                var tGestionContacto = MapeoEntidad(entidad);
                base.InsertAsync(tGestionContacto);
                return tGestionContacto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TGestionContacto MapeoEntidad(GestionContacto entidad)
        {
            try
            {
                TGestionContacto modelo = _mapper.Map<TGestionContacto>(entidad);
                if (entidad.ListaGestionContactoLog != null && entidad.ListaGestionContactoLog.Count > 0)
                {
                    foreach (var logBO in entidad.ListaGestionContactoLog)
                    {
                        var logDB = _mapper.Map<TGestionContactoLog>(logBO);
                        modelo.TGestionContactoLogs.Add(logDB);
                    }
                }
                if (entidad.ListaActividadDetalle != null && entidad.ListaActividadDetalle.Count > 0)
                {
                    foreach (var actividadBO in entidad.ListaActividadDetalle)
                    {
                        var actividadDB = _mapper.Map<TActividadDetalleGestionContacto>(actividadBO);
                        modelo.TActividadDetalleGestionContactos.Add(actividadDB);
                    }
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExisteGestionActivaAsync(int idDocente, int idCentroCosto)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM pla.T_GestionContacto WHERE IdClasificacionPersona = @IdDocente AND IdCentroCosto = @IdCentroCosto AND Estado = 1";

                var cantidad = await _dapperRepository.FirstOrDefaultAsync(query, new { IdDocente = idDocente, IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(cantidad) && int.TryParse(cantidad, out int count))
                {
                    return count > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar duplicidad de gestión", ex);
            }
        }
        public async Task<bool> ExisteCentroCostoAsync(int id)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM pla.T_CentroCosto WHERE IdCentroCosto = @Id";

                var cantidad = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                if (!string.IsNullOrEmpty(cantidad) && int.TryParse(cantidad, out int count))
                {
                    return count > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Centro de Costo {id}", ex);
            }
        }

        // 2. Validar Personal (Asesor)
        public async Task<bool> ExistePersonalAsync(int id)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM gp.T_Personal WHERE IdPersonal = @Id";

                var cantidad = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                if (!string.IsNullOrEmpty(cantidad) && int.TryParse(cantidad, out int count))
                {
                    return count > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Personal {id}", ex);
            }
        }

        // 3. Validar Clasificación Persona (Docente)
        public async Task<bool> ExisteClasificacionPersonaAsync(int id)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM conf.T_ClasificacionPersona WHERE IdClasificacionPersona = @Id";

                var cantidad = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                if (!string.IsNullOrEmpty(cantidad) && int.TryParse(cantidad, out int count))
                {
                    return count > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Clasificación Persona {id}", ex);
            }
        }

        // 4. Validar Fase Gestión Contacto
        public async Task<bool> ExisteFaseGestionAsync(int id)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM pla.T_FaseGestionContacto WHERE IdFaseGestionContacto = @Id";

                var cantidad = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                if (!string.IsNullOrEmpty(cantidad) && int.TryParse(cantidad, out int count))
                {
                    return count > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Fase Gestión {id}", ex);
            }
        }

        // 5. Validar Origen
        public async Task<bool> ExisteOrigenAsync(int id)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM mkt.T_Origen WHERE IdOrigen = @Id";

                var cantidad = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                if (!string.IsNullOrEmpty(cantidad) && int.TryParse(cantidad, out int count))
                {
                    return count > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Origen {id}", ex);
            }
        }
    }
}
