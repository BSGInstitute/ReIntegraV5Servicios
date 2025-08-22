using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PreguntaIntentoRepository
    /// Autor: Gilmer qm.
    /// Fecha: 21/07/2023
    /// <summary>
    /// Gestión general de T_PreguntaIntento
    /// </summary>
    public class PreguntaIntentoRepository : GenericRepository<TPreguntaIntento>, IPreguntaIntentoRepository
    {
        private Mapper _mapper;

        public PreguntaIntentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaIntento, PreguntaIntento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntaIntentoDetalle, PreguntaIntentoDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntaProgramaCapacitacion, PreguntaProgramaCapacitacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TRespuestaPreguntaProgramaCapacitacion, RespuestaPreguntaProgramaCapacitacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaIntento MapeoEntidad(PreguntaIntento entidad)
        {
            try
            {
                //Mapea la entidad padre
                TPreguntaIntento modelo = _mapper.Map<TPreguntaIntento>(entidad);

                if (entidad.PreguntaIntentoDetalles != null && entidad.PreguntaIntentoDetalles.Count > 0)
                    modelo.TPreguntaIntentoDetalles = _mapper.Map<List<TPreguntaIntentoDetalle>>(entidad.PreguntaIntentoDetalles);

                if (entidad.PreguntaProgramaCapacitacions != null && entidad.PreguntaProgramaCapacitacions.Count > 0)
                {
                    modelo.TPreguntaProgramaCapacitacions = new List<TPreguntaProgramaCapacitacion>();
                    foreach (var item in entidad.PreguntaProgramaCapacitacions)
                    {
                        TPreguntaProgramaCapacitacion modeloPespecifico = _mapper.Map<TPreguntaProgramaCapacitacion>(item);
                        if (item.RespuestaPreguntaProgramaCapacitacions != null && item.RespuestaPreguntaProgramaCapacitacions.Count() > 0)
                            modeloPespecifico.TRespuestaPreguntaProgramaCapacitacions = _mapper.Map<ICollection<TRespuestaPreguntaProgramaCapacitacion>>(item.RespuestaPreguntaProgramaCapacitacions);
                        modelo.TPreguntaProgramaCapacitacions.Add(modeloPespecifico);
                    }
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaIntento Add(PreguntaIntento entidad)
        {
            try
            {
                var PreguntaIntento = MapeoEntidad(entidad);
                base.Insert(PreguntaIntento);
                return PreguntaIntento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaIntento Update(PreguntaIntento entidad)
        {
            try
            {
                var PreguntaIntento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaIntento.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaIntento);
                return PreguntaIntento;
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


        public IEnumerable<TPreguntaIntento> Add(IEnumerable<PreguntaIntento> listadoEntidad)
        {
            try
            {
                List<TPreguntaIntento> listado = new List<TPreguntaIntento>();
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

        public IEnumerable<TPreguntaIntento> Update(IEnumerable<PreguntaIntento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaIntento> listado = new List<TPreguntaIntento>();
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
        /// Autor : Gilmer Qm.
        /// Fecha: 21/07/2023
        /// Version: 1.0
        /// <param name="id"> (PK) </param>
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        public PreguntaIntento ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                       NumeroMaximoIntento,
                                       ActivarFeedbackMaximoIntento,
                                       MensajeFeedback,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM gp.T_PreguntaIntento
                                WHERE Estado = 1
                                      AND Id = @Id;";
                var res = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<PreguntaIntento>(res);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
