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
    public class GestionDocenteOcurrenciaRepository : GenericRepository<TGestionDocenteOcurrencium>, IGestionDocenteOcurrenciaRepository
    {
        private Mapper _mapper;

        public GestionDocenteOcurrenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteOcurrencia, TGestionDocenteOcurrencium>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteOcurrencium Add(GestionDocenteOcurrencia entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteOcurrencium>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteOcurrencium Update(GestionDocenteOcurrencia entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteOcurrencium>(entidad);
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

        public IEnumerable<GestionDocenteOcurrenciaDTO> ObtenerOcurrencias()
        {
            try
            {
                IEnumerable<GestionDocenteOcurrenciaDTO> ocurrencias = new List<GestionDocenteOcurrenciaDTO>();
                string _query = @"SELECT Id, Nombre, Descripcion, IdGestionDocenteOcurrenciaTipo,
                                  IdGestionDocenteActividadDetalle, IdGestionDocenteModoMarcado,
                                  RequiereComentario, RequiereFechaHora
                                  FROM pla.T_GestionDocenteOcurrencia WHERE Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    ocurrencias = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteOcurrenciaDTO>>(resultadoDB);
                }
                return ocurrencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
