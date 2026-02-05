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
    }
}
