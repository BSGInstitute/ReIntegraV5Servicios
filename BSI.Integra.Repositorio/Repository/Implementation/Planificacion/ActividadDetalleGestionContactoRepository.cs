using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ActividadDetalleGestionContactoRepository : GenericRepository<TActividadDetalleGestionContacto>, IActividadDetalleGestionContactoRepository
    {
        private Mapper _mapper;

        public ActividadDetalleGestionContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActividadDetalleGestionContacto, TActividadDetalleGestionContacto>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TActividadDetalleGestionContacto AddAsync(ActividadDetalleGestionContacto entidad)
        {
            try
            {
                var tActividad = MapeoEntidad(entidad);
                base.Insert(tActividad);
                return tActividad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TActividadDetalleGestionContacto MapeoEntidad(ActividadDetalleGestionContacto entidad)
        {
            try
            {
                TActividadDetalleGestionContacto modelo = _mapper.Map<TActividadDetalleGestionContacto>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: JoseVega
        /// Fecha: 27/12/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza la entidad ActividadDetalleGestionContacto con control de concurrencia.
        /// </summary>
        /// <param name="entidad">Objeto con la informacion a actualizar</param>
        /// <returns>Entidad actualizada de tipo TActividadDetalleGestionContacto</returns>
        public TActividadDetalleGestionContacto Update(ActividadDetalleGestionContacto entidad)
        {
            try
            {
                var actividadEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });

                if (entidadExistente != null)
                {
                    actividadEntidad.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(actividadEntidad);
                return actividadEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar ActividadDetalleGestionContacto", ex);
            }
        }
        /// Autor: JoseVega
        /// Fecha: 27/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de ActividadDetalleGestionContacto por su Id
        /// </summary>
        /// <param name="id">Identificador de la actividad</param>
        /// <returns>Objeto ActividadDetalleGestionContacto</returns>
        public async Task<ActividadDetalleGestionContacto> ObtenerPorIdAsync(int id)
        {
            try
            {
                string query = @"
            SELECT 
                Id,
                IdGestionContacto,
                FechaProgramada,
                FechaReal,
                DuracionReal,
                IdActividadCabecera,
                Comentario,
                IdOcurrencia,
                Estado,
                UsuarioCreacion,
                UsuarioModificacion,
                FechaCreacion,
                FechaModificacion,
                RowVersion
            FROM com.T_ActividadDetalleGestionContacto WITH(NOLOCK)
            WHERE Id = @Id AND Estado = 1";


                var resultadoDinamico = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                if (resultadoDinamico == null) return null;
                string json = JsonConvert.SerializeObject(resultadoDinamico);
                var resultadoBO = JsonConvert.DeserializeObject<ActividadDetalleGestionContacto>(json);

                return resultadoBO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ActividadDetalleGestionContacto por ID", ex);
            }
        }
    }
}
