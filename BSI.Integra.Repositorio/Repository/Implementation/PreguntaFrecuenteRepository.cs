using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PreguntaFrecuenteRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PreguntaFrecuente
    /// </summary>
    public class PreguntaFrecuenteRepository : GenericRepository<TPreguntaFrecuente>, IPreguntaFrecuenteRepository
    {
        private Mapper _mapper;

        public PreguntaFrecuenteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaFrecuente, PreguntaFrecuente>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntaFrecuentePgeneral, PreguntaFrecuentePGeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntaFrecuenteArea, PreguntaFrecuenteArea>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntaFrecuenteSubArea, PreguntaFrecuenteSubArea>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntaFrecuenteTipo, PreguntaFrecuenteTipo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaFrecuente MapeoEntidad(PreguntaFrecuente entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntaFrecuente modelo = _mapper.Map<TPreguntaFrecuente>(entidad);

                //mapea los hijos
                if (entidad.PreguntaFrecuentePgeneral != null && entidad.PreguntaFrecuentePgeneral.Count > 0)
                    modelo.TPreguntaFrecuentePgenerals = _mapper.Map<List<TPreguntaFrecuentePgeneral>>(entidad.PreguntaFrecuentePgeneral);

                if (entidad.PreguntaFrecuenteArea != null && entidad.PreguntaFrecuenteArea.Count > 0)
                    modelo.TPreguntaFrecuenteAreas = _mapper.Map<List<TPreguntaFrecuenteArea>>(entidad.PreguntaFrecuenteArea);

                if (entidad.PreguntaFrecuenteSubArea != null && entidad.PreguntaFrecuenteSubArea.Count > 0)
                    modelo.TPreguntaFrecuenteSubAreas = _mapper.Map<List<TPreguntaFrecuenteSubArea>>(entidad.PreguntaFrecuenteSubArea);

                if (entidad.PreguntaFrecuenteTipo != null && entidad.PreguntaFrecuenteTipo.Count > 0)
                    modelo.TPreguntaFrecuenteTipos = _mapper.Map<List<TPreguntaFrecuenteTipo>>(entidad.PreguntaFrecuenteTipo);


                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaFrecuente Add(PreguntaFrecuente entidad)
        {
            try
            {
                var PreguntaFrecuente = MapeoEntidad(entidad);
                base.Insert(PreguntaFrecuente);
                return PreguntaFrecuente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaFrecuente Update(PreguntaFrecuente entidad)
        {
            try
            {
                var PreguntaFrecuente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaFrecuente.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaFrecuente);
                return PreguntaFrecuente;
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


        public IEnumerable<TPreguntaFrecuente> Add(IEnumerable<PreguntaFrecuente> listadoEntidad)
        {
            try
            {
                List<TPreguntaFrecuente> listado = new List<TPreguntaFrecuente>();
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

        public IEnumerable<TPreguntaFrecuente> Update(IEnumerable<PreguntaFrecuente> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaFrecuente> listado = new List<TPreguntaFrecuente>();
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
        /// Autor: Gilmer Qm.
        /// Fecha: 20/06/2022
        /// <summary>
        /// Obtiene las preguntas frecuentes con filtros 
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> List<PreguntaFrecuenteFiltroResultadoDTO> </returns>
        public IEnumerable<PreguntaFrecuenteFiltroResultadoDTO> ObtenerPorFiltro(FiltroPreguntaFrecuenteDTO filtro)
        {
            var _query = "pla.SP_ObtenerProgramaEspecificoFiltro";
            var IdArea = string.Join(",", filtro.Areas);
            var IdSubArea = string.Join(",", filtro.Subareas);
            var IdPGeneral = string.Join(",", filtro.PGenerales);

            var preguntaFrecuente = _dapperRepository.QuerySPDapper(_query, new {
                IdPGeneral,
                IdArea,
                IdSubArea
            });
            if (!string.IsNullOrEmpty(preguntaFrecuente) && preguntaFrecuente != "[]")
            {
                return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuenteFiltroResultadoDTO>>(preguntaFrecuente);
            }
            return new List<PreguntaFrecuenteFiltroResultadoDTO>();
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 20/06/2022
        /// <summary>
        /// Obtiene todo los registros con Estado=1 de T_PreguntaFrecuente
        /// </summary>
        /// <returns> List<PreguntaFrecuenteDTO> </returns>
        public IEnumerable<PreguntaFrecuenteDTO> Obtener()
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdSeccionPreguntaFrecuente,
                                   Pregunta,
                                   Respuesta,
                                   Tipo
                            FROM pla.T_PreguntaFrecuente
                            WHERE Estado = 1 ORDER BY Id DESC";
                var preguntaFrecuente = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(preguntaFrecuente) && !preguntaFrecuente.Contains("[]") && !preguntaFrecuente.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaFrecuenteDTO>>(preguntaFrecuente);
                }
                return new List<PreguntaFrecuenteDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 22/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el id
        /// </summary>
        /// <param name="id"> (PK) del registro </param>
        /// <returns> Entidad: PreguntaFrecuente </returns>
        public PreguntaFrecuente ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                IdSeccionPreguntaFrecuente,
                                Pregunta,
                                Respuesta,
                                Tipo,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion 
                    FROM 
                        pla.T_PreguntaFrecuente
                    WHERE
                        Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PreguntaFrecuente>(resultado)!;
                }
                return new PreguntaFrecuente();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPIR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
    }
}
