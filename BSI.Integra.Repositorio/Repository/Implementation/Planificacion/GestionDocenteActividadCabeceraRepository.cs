using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteActividadCabeceraRepository : GenericRepository<TGestionDocenteActividadCabecera>, IGestionDocenteActividadCabeceraRepository
    {
        private Mapper _mapper;

        public GestionDocenteActividadCabeceraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteActividadCabecera, TGestionDocenteActividadCabecera>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteActividadCabecera Add(GestionDocenteActividadCabecera entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadCabecera>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteActividadCabecera Update(GestionDocenteActividadCabecera entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadCabecera>(entidad);
                var existing = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion, s.FechaCreacion, s.UsuarioCreacion });
                if (existing != null)
                {
                    model.RowVersion = existing.RowVersion;
                    model.FechaCreacion = existing.FechaCreacion;
                    model.UsuarioCreacion = existing.UsuarioCreacion;
                }
                base.Update(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GestionDocenteActividadCabeceraOutputDTO ObtenerCabeceraPorId(int id)
        {
            try
            {
                GestionDocenteActividadCabeceraOutputDTO cabecera = null;
                string _query = @"SELECT c.Id, c.Nombre, c.Descripcion, c.IdGestionDocenteEstado, e.Nombre AS NombreEstado, c.IdGestionDocenteCategoria, cat.Nombre AS NombreCategoria
                    FROM pla.T_GestionDocenteActividadCabecera c
                    LEFT JOIN pla.T_GestionDocenteEstado e ON c.IdGestionDocenteEstado = e.Id
                    LEFT JOIN pla.T_GestionDocenteCategoria cat ON c.IdGestionDocenteCategoria = cat.Id
                    WHERE c.Id = @Id AND c.Estado = 1";
                var resultadoDB = _dapperRepository.QueryDapper(_query, new { Id = id });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    var lista = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteActividadCabeceraOutputDTO>>(resultadoDB);
                    cabecera = lista.FirstOrDefault();
                }
                return cabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GestionDocenteActividadCabeceraOutputDTO> ObtenerCabeceraPorIdAsync(int id)
        {
            try
            {
                GestionDocenteActividadCabeceraOutputDTO cabecera = null;
                string _query = @"SELECT c.Id, c.Nombre, c.Descripcion, c.IdGestionDocenteEstado, e.Nombre AS NombreEstado, c.IdGestionDocenteCategoria, cat.Nombre AS NombreCategoria
                    FROM pla.T_GestionDocenteActividadCabecera c
                    LEFT JOIN pla.T_GestionDocenteEstado e ON c.IdGestionDocenteEstado = e.Id
                    LEFT JOIN pla.T_GestionDocenteCategoria cat ON c.IdGestionDocenteCategoria = cat.Id
                    WHERE c.Id = @Id AND c.Estado = 1";
                var resultadoDB = await _dapperRepository.QueryDapperAsync(_query, new { Id = id });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    var lista = JsonConvert.DeserializeObject<IEnumerable<GestionDocenteActividadCabeceraOutputDTO>>(resultadoDB);
                    cabecera = lista.FirstOrDefault();
                }
                return cabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
