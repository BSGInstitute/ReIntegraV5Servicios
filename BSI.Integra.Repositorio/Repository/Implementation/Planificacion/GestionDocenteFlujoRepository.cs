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
    public class GestionDocenteFlujoRepository : GenericRepository<TGestionDocenteFlujo>, IGestionDocenteFlujoRepository
    {
        private Mapper _mapper;

        public GestionDocenteFlujoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteFlujo, TGestionDocenteFlujo>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteFlujo Add(GestionDocenteFlujo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteFlujo>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteFlujo Update(GestionDocenteFlujo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteFlujo>(entidad);
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

        public IEnumerable<GestionDocenteEstadoDTO> ObtenerEstadosFlujo()
        {
            try
            {
                IEnumerable<GestionDocenteEstadoDTO> estados = new List<GestionDocenteEstadoDTO>();
                string _query = "SELECT Id, Nombre FROM pla.T_GestionDocenteEstado WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    estados = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteEstadoDTO>>(resultadoDB);
                }
                return estados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionDocenteCategoriaDTO> ObtenerCategorias()
        {
            try
            {
                IEnumerable<GestionDocenteCategoriaDTO> categorias = new List<GestionDocenteCategoriaDTO>();
                string _query = "SELECT Id, Nombre FROM pla.T_GestionDocenteCategoria WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    categorias = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteCategoriaDTO>>(resultadoDB);
                }
                return categorias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
