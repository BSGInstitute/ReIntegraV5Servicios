using AutoMapper;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteIaEntrenamientoEjemploRepository : GenericRepository<TGestionDocenteIaEntrenamientoEjemplo>, IGestionDocenteIaEntrenamientoEjemploRepository
    {
        private Mapper _mapper;

        public GestionDocenteIaEntrenamientoEjemploRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteIaEntrenamientoEjemplo, TGestionDocenteIaEntrenamientoEjemplo>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteIaEntrenamientoEjemplo Add(GestionDocenteIaEntrenamientoEjemplo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteIaEntrenamientoEjemplo>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteIaEntrenamientoEjemplo Update(GestionDocenteIaEntrenamientoEjemplo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteIaEntrenamientoEjemplo>(entidad);
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

        public IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO> ObtenerEjemplosEntrenamientoPorConfiguracion(int idIaConfiguracion)
        {
            try
            {
                IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO> ejemplos = new List<GestionDocenteIaEntrenamientoEjemploOutputDTO>();
                string query = "@SELECT e.Id, e.IdGestionDocenteOcurrenciaIaConfiguracion, e.IdGestionDocenteIaEntrenamientoClasificacionTipo, e.TextoEjemplo, e.EsPositivo, ct.Nombre AS NombreClasificacionTipo FROM pla.T_GestionDocenteIaEntrenamientoEjemplo e INNER JOIN pla.T_GestionDocenteIaEntrenamientoClasificacionTipo ct ON e.IdGestionDocenteIaEntrenamientoClasificacionTipo = ct.Id WHERE e.IdGestionDocenteOcurrenciaIaConfiguracion = @IdIaConfiguracion AND e.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdIaConfiguracion = idIaConfiguracion });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                    ejemplos = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO>>(resultadoDB);
                return ejemplos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteIaEntrenamientoClasificacionTipoDTO> ObtenerClasificacionTipos()
        {
            try
            {
                IEnumerable<GestionDocenteIaEntrenamientoClasificacionTipoDTO> tipos = new List<GestionDocenteIaEntrenamientoClasificacionTipoDTO>();
                string query = "SELECT Id, Nombre FROM pla.T_GestionDocenteIaEntrenamientoClasificacionTipo WHERE Estado = 1 ORDER BY Id";
                var resultadoDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                    tipos = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<GestionDocenteIaEntrenamientoClasificacionTipoDTO>>(resultadoDB);
                return tipos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}