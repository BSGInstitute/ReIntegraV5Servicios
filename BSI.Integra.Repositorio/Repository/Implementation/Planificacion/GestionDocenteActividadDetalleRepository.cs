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

        public IEnumerable<GestionDocenteActividadDetalleTipoDTO> ObtenerActividadDetalleTipos()
        {
            try
            {
                IEnumerable<GestionDocenteActividadDetalleTipoDTO> tipos = new List<GestionDocenteActividadDetalleTipoDTO>();
                string _query = "SELECT Id, Nombre FROM pla.T_GestionDocenteActividadDetalleTipo WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    tipos = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteActividadDetalleTipoDTO>>(resultadoDB);
                }
                return tipos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteDisparadorFlujoTipoDTO> ObtenerDisparadorFlujoTipos()
        {
            try
            {
                IEnumerable<GestionDocenteDisparadorFlujoTipoDTO> tipos = new List<GestionDocenteDisparadorFlujoTipoDTO>();
                string _query = "SELECT Id, Nombre FROM pla.T_GestionDocenteDisparadorFlujoTipo WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    tipos = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteDisparadorFlujoTipoDTO>>(resultadoDB);
                }
                return tipos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteDisparadorDetalleOutputDTO> ObtenerDisparadorReglaTiempo()
        {
            try
            {
                IEnumerable<GestionDocenteDisparadorDetalleOutputDTO> disparadores = new List<GestionDocenteDisparadorDetalleOutputDTO>();
                string _query = "pla.SP_GestionDocenteDisparadorDetalleReglaTiempoObtener";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { });
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

        public IEnumerable<GestionDocenteModoMarcadoDTO> ObtenerModosMarcado()
        {
            try
            {
                IEnumerable<GestionDocenteModoMarcadoDTO> modos = new List<GestionDocenteModoMarcadoDTO>();
                string _query = "SELECT Id, Nombre, Descripcion FROM pla.T_GestionDocenteModoMarcado WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    modos = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteModoMarcadoDTO>>(resultadoDB);
                }
                return modos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteMedioComunicacionDTO> ObtenerMediosComunicacion()
        {
            try
            {
                IEnumerable<GestionDocenteMedioComunicacionDTO> medios = new List<GestionDocenteMedioComunicacionDTO>();
                string _query = "SELECT Id, Nombre FROM pla.T_MedioComunicacion WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    medios = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteMedioComunicacionDTO>>(resultadoDB);
                }
                return medios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocentePlantillaMedioComunicacionDTO> ObtenerPlantillasMedioComunicacion()
        {
            try
            {
                IEnumerable<GestionDocentePlantillaMedioComunicacionDTO> plantillas = new List<GestionDocentePlantillaMedioComunicacionDTO>();
                string _query = "pla.SP_GestionDocentePlantillaMedioComunicacionObtener";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<IEnumerable<GestionDocentePlantillaMedioComunicacionDTO>>(resultadoDB);
                }
                return plantillas;
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
                string _query = "pla.SP_GestionDocenteActividadDetalleObtenerPorCabecera";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteActividadCabecera = idCabecera });
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
                string _query = "pla.SP_GestionDocenteDisparadorDetalleObtenerIds";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteDisparadorDetalle_Lista = ids });
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
                string _query = "pla.SP_GestionDocenteReglaTiempoFijoObtenerPorDisparadores";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteDisparadorDetalle_Lista = ids });
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
                string _query = "pla.SP_GestionDocenteReglaTiempoRelativoObtenerPorDisparadores";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteDisparadorDetalle_Lista = ids });
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
                string _query = "pla.SP_GestionDocenteReferenciaRelativaObtenerPorReglas";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteDisparadorReglaTiempoRelativo_Lista = ids });
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
                string _query = "pla.SP_GestionDocenteDisparadorOcurrenciaObtenerIds";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteDisparadorDetalle_Lista = ids });
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
                string _query = "pla.SP_GestionDocenteSesionObtenerPorDetalles";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteActividadDetalle_Lista = ids });
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
                string _query = "pla.SP_GestionDocenteOcurrenciaObtenerPorDetalles";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteActividadDetalle_Lista = ids });
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
                string _query = "pla.SP_GestionDocenteIaConfiguracionObtenerPorOcurrencias";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteOcurrencia_Lista = ids });
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
                string _query = "pla.SP_GestionDocenteEntrenamientoEjemploObtenerPorConfiguraciones";
                var resultadoDB = _dapperRepository.QuerySPDapper(_query, new { IdGestionDocenteOcurrenciaIaConfiguracion_Lista = ids });
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
