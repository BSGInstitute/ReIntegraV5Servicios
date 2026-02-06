using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteActividadDetalleRepository : GenericRepository<TGestionDocenteActividadDetalle>, IGestionDocenteActividadDetalleRepository
    {
        private Mapper _mapper;

        public GestionDocenteActividadDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteActividadDetalle, TGestionDocenteActividadDetalle>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteActividadDetalle Add(GestionDocenteActividadDetalle entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadDetalle>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteActividadDetalle Update(GestionDocenteActividadDetalle entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadDetalle>(entidad);
                var existing = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                if (existing != null) model.RowVersion = existing.RowVersion;
                base.Update(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteSesionDTO> ObtenerSesiones()
        {
            try
            {
                IEnumerable<GestionDocenteSesionDTO> sesiones = new List<GestionDocenteSesionDTO>();
                string _query = "SELECT Id, Nombre, Descripcion FROM pla.T_GestionDocenteSesion WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    sesiones = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteSesionDTO>>(resultadoDB);
                }
                return sesiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteConfianzaUmbralNivelDTO> ObtenerConfianzaUmbralNiveles()
        {
            try
            {
                IEnumerable<GestionDocenteConfianzaUmbralNivelDTO> niveles = new List<GestionDocenteConfianzaUmbralNivelDTO>();
                string _query = "SELECT Id, Nombre FROM pla.T_GestionDocenteConfianzaUmbralNivel WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    niveles = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteConfianzaUmbralNivelDTO>>(resultadoDB);
                }
                return niveles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteOcurrenciaTipoDTO> ObtenerOcurrenciaTipos()
        {
            try
            {
                IEnumerable<GestionDocenteOcurrenciaTipoDTO> tipos = new List<GestionDocenteOcurrenciaTipoDTO>();
                string _query = "SELECT Id, Nombre, Descripcion FROM pla.T_GestionDocenteOcurrenciaTipo WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    tipos = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteOcurrenciaTipoDTO>>(resultadoDB);
                }
                return tipos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteReferenciaTiempoDTO> ObtenerReferenciasTiempo()
        {
            try
            {
                IEnumerable<GestionDocenteReferenciaTiempoDTO> referencias = new List<GestionDocenteReferenciaTiempoDTO>();
                string _query = "SELECT Id, Nombre, Codigo FROM pla.T_GestionDocenteReferenciaTiempo WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    referencias = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteReferenciaTiempoDTO>>(resultadoDB);
                }
                return referencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteActividadDetalleOutputDTO> ObtenerDetallesPorCabecera(int idCabecera)
        {
            try
            {
                IEnumerable<GestionDocenteActividadDetalleOutputDTO> detalles = new List<GestionDocenteActividadDetalleOutputDTO>();
                string _query = @$"SELECT d.Id, d.IdGestionDocenteActividadCabecera, d.IdGestionDocenteActividadDetalleTipo, t.Nombre AS NombreActividadDetalleTipo, d.IdPlantillaMedioComunicacion, p.Nombre AS NombrePlantilla, d.IdGestionDocenteDisparadorDetalle, d.Nombre
                    FROM pla.T_GestionDocenteActividadDetalle d
                    LEFT JOIN pla.T_GestionDocenteActividadDetalleTipo t ON d.IdGestionDocenteActividadDetalleTipo = t.Id
                    LEFT JOIN mkt.T_PlantillaMedioComunicacion pmc ON d.IdPlantillaMedioComunicacion = pmc.Id
                    LEFT JOIN mkt.T_Plantilla p ON pmc.IdPlantilla = p.Id
                    WHERE d.IdGestionDocenteActividadCabecera = @IdCabecera AND d.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, new { IdCabecera = idCabecera });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    detalles = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteActividadDetalleOutputDTO>>(resultadoDB);
                }
                return detalles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteDisparadorDetalleOutputDTO> ObtenerDisparadoresPorIds(string ids)
        {
            try
            {
                IEnumerable<GestionDocenteDisparadorDetalleOutputDTO> disparadores = new List<GestionDocenteDisparadorDetalleOutputDTO>();
                string _query = $@"SELECT d.Id, d.IdGestionDocenteDisparadorFlujoTipo, t.Nombre AS NombreDisparadorFlujoTipo
                    FROM pla.T_GestionDocenteDisparadorDetalle d
                    LEFT JOIN pla.T_GestionDocenteDisparadorFlujoTipo t ON d.IdGestionDocenteDisparadorFlujoTipo = t.Id
                    WHERE d.Id IN ({ids}) AND d.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    disparadores = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteDisparadorDetalleOutputDTO>>(resultadoDB);
                }
                return disparadores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteDisparadorReglaTiempoFijoOutputDTO> ObtenerReglasTiempoFijoPorDisparadores(string ids)
        {
            try
            {
                IEnumerable<GestionDocenteDisparadorReglaTiempoFijoOutputDTO> reglas = new List<GestionDocenteDisparadorReglaTiempoFijoOutputDTO>();
                string _query = $@"SELECT f.IdGestionDocenteDisparadorReglaTiempo, f.IdGestionDocenteDisparadorDetalle, f.Fecha, rt.TipoRegla
                    FROM pla.T_GestionDocenteDisparadorReglaTiempoFijo f
                    LEFT JOIN pla.T_GestionDocenteDisparadorReglaTiempo rt ON f.IdGestionDocenteDisparadorReglaTiempo = rt.Id
                    WHERE f.IdGestionDocenteDisparadorDetalle IN ({ids}) AND f.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    reglas = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteDisparadorReglaTiempoFijoOutputDTO>>(resultadoDB);
                }
                return reglas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoOutputDTO> ObtenerReglasTiempoRelativoPorDisparadores(string ids)
        {
            try
            {
                IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoOutputDTO> reglas = new List<GestionDocenteDisparadorReglaTiempoRelativoOutputDTO>();
                string _query = $@"SELECT r.Id, r.IdGestionDocenteDisparadorReglaTiempo, r.IdGestionDocenteDisparadorDetalle, r.Cantidad, r.IdGestionDocenteUnidadTiempo, rt.TipoRegla, u.Nombre AS NombreUnidadTiempo
                    FROM pla.T_GestionDocenteDisparadorReglaTiempoRelativo r
                    LEFT JOIN pla.T_GestionDocenteDisparadorReglaTiempo rt ON r.IdGestionDocenteDisparadorReglaTiempo = rt.Id
                    LEFT JOIN pla.T_GestionDocenteUnidadTiempo u ON r.IdGestionDocenteUnidadTiempo = u.Id
                    WHERE r.IdGestionDocenteDisparadorDetalle IN ({ids}) AND r.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    reglas = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoOutputDTO>>(resultadoDB);
                }
                return reglas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO> ObtenerReferenciasRelativasPorReglas(string ids)
        {
            try
            {
                IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO> referencias = new List<GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO>();
                string _query = $@"SELECT r.IdGestionDocenteDisparadorReglaTiempoRelativo, r.IdGestionDocenteReferenciaTiempo, rt.Nombre AS NombreReferenciaTiempo
                    FROM pla.T_GestionDocenteDisparadorReglaTiempoRelativoReferencia r
                    LEFT JOIN pla.T_GestionDocenteReferenciaTiempo rt ON r.IdGestionDocenteReferenciaTiempo = rt.Id
                    WHERE r.IdGestionDocenteDisparadorReglaTiempoRelativo IN ({ids}) AND r.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    referencias = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO>>(resultadoDB);
                }
                return referencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteDisparadorOcurrenciaDetalleOutputDTO> ObtenerDisparadoresOcurrenciaPorIds(string ids)
        {
            try
            {
                IEnumerable<GestionDocenteDisparadorOcurrenciaDetalleOutputDTO> disparadores = new List<GestionDocenteDisparadorOcurrenciaDetalleOutputDTO>();
                string _query = $@"SELECT d.IdGestionDocenteDisparadorDetalle, d.IdGestionDocenteOcurrencia_Previa AS IdGestionDocenteOcurrenciaPrevia, o.Nombre AS NombreOcurrenciaPrevia
                    FROM pla.T_GestionDocenteDisparadorOcurrenciaDetalle d
                    LEFT JOIN pla.T_GestionDocenteOcurrencia o ON d.IdGestionDocenteOcurrencia_Previa = o.Id
                    WHERE d.IdGestionDocenteDisparadorDetalle IN ({ids}) AND d.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    disparadores = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteDisparadorOcurrenciaDetalleOutputDTO>>(resultadoDB);
                }
                return disparadores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionContactoActividadDetalleSesionDTO> ObtenerSesionesPorDetalles(string ids)
        {
            try
            {
                IEnumerable<GestionContactoActividadDetalleSesionDTO> sesiones = new List<GestionContactoActividadDetalleSesionDTO>();
                string _query = $@"SELECT cs.Id, cs.IdGestionDocenteActividadDetalle, cs.IdGestionDocenteSesion, s.Nombre AS NombreSesion
                    FROM pla.T_GestionContactoActividadDetalleSesion cs
                    LEFT JOIN pla.T_GestionDocenteSesion s ON cs.IdGestionDocenteSesion = s.Id
                    WHERE cs.IdGestionDocenteActividadDetalle IN ({ids}) AND cs.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    sesiones = JsonConvert.DeserializeObject<IEnumerable<GestionContactoActividadDetalleSesionDTO>>(resultadoDB);
                }
                return sesiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteOcurrenciaOutputDTO> ObtenerOcurrenciasPorDetalles(string ids)
        {
            try
            {
                IEnumerable<GestionDocenteOcurrenciaOutputDTO> ocurrencias = new List<GestionDocenteOcurrenciaOutputDTO>();
                string _query = $@"SELECT o.Id, o.Nombre, o.Descripcion, o.IdGestionDocenteOcurrenciaTipo, ot.Nombre AS NombreOcurrenciaTipo, o.IdGestionDocenteActividadDetalle, o.IdGestionDocenteModoMarcado, mm.Nombre AS NombreModoMarcado, o.RequiereComentario, o.RequiereFechaHora
                    FROM pla.T_GestionDocenteOcurrencia o
                    LEFT JOIN pla.T_GestionDocenteOcurrenciaTipo ot ON o.IdGestionDocenteOcurrenciaTipo = ot.Id
                    LEFT JOIN pla.T_GestionDocenteModoMarcado mm ON o.IdGestionDocenteModoMarcado = mm.Id
                    WHERE o.IdGestionDocenteActividadDetalle IN ({ids}) AND o.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    ocurrencias = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteOcurrenciaOutputDTO>>(resultadoDB);
                }
                return ocurrencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<OcurrenciaIaConfiguracionCompletaDTO> ObtenerIaConfiguracionesPorOcurrencias(string ids)
        {
            try
            {
                IEnumerable<OcurrenciaIaConfiguracionCompletaDTO> configuraciones = new List<OcurrenciaIaConfiguracionCompletaDTO>();
                string _query = $@"SELECT c.Id, c.Prompt, c.IdGestionDocenteConfianzaUmbralNivel, n.Nombre AS NombreConfianzaUmbralNivel, c.IdGestionDocenteOcurrencia
                    FROM pla.T_GestionDocenteOcurrenciaIaConfiguracion c
                    LEFT JOIN pla.T_GestionDocenteConfianzaUmbralNivel n ON c.IdGestionDocenteConfianzaUmbralNivel = n.Id
                    WHERE c.IdGestionDocenteOcurrencia IN ({ids}) AND c.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    configuraciones = JsonConvert.DeserializeObject<IEnumerable<OcurrenciaIaConfiguracionCompletaDTO>>(resultadoDB);
                }
                return configuraciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO> ObtenerEjemplosEntrenamientoPorConfiguraciones(string ids)
        {
            try
            {
                IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO> ejemplos = new List<GestionDocenteIaEntrenamientoEjemploOutputDTO>();
                string _query = $@"SELECT e.IdGestionDocenteOcurrenciaIaConfiguracion, e.IdGestionDocenteIaEntrenamientoClasificacionTipo, ct.Nombre AS NombreClasificacionTipo, e.TextoEjemplo, e.EsPositivo
                    FROM pla.T_GestionDocenteIaEntrenamientoEjemplo e
                    LEFT JOIN pla.T_GestionDocenteIaEntrenamientoClasificacionTipo ct ON e.IdGestionDocenteIaEntrenamientoClasificacionTipo = ct.Id
                    WHERE e.IdGestionDocenteOcurrenciaIaConfiguracion IN ({ids}) AND e.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    ejemplos = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO>>(resultadoDB);
                }
                return ejemplos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
