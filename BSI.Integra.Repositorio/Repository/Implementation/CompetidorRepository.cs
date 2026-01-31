using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CompetidorRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/07/2022
    /// <summary>
    /// Gestión general de T_Competidor
    /// </summary>
    public class CompetidorRepository : GenericRepository<TCompetidor>, ICompetidorRepository
    {
        private Mapper _mapper;

        public CompetidorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCompetidor, Competidor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCompetidor MapeoEntidad(Competidor entidad)
        {
            try
            {
                //crea la entidad padre
                TCompetidor modelo = _mapper.Map<TCompetidor>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCompetidor Add(Competidor entidad)
        {
            try
            {
                var Competidor = MapeoEntidad(entidad);
                base.Insert(Competidor);
                return Competidor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCompetidor Update(Competidor entidad)
        {
            try
            {
                var Competidor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Competidor.RowVersion = entidadExistente.RowVersion;

                base.Update(Competidor);
                return Competidor;
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


        public IEnumerable<TCompetidor> Add(IEnumerable<Competidor> listadoEntidad)
        {
            try
            {
                List<TCompetidor> listado = new List<TCompetidor>();
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

        public IEnumerable<TCompetidor> Update(IEnumerable<Competidor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCompetidor> listado = new List<TCompetidor>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Competidor.
        /// </summary>
        /// <returns> List<CompetidorDTO> </returns>
        public IEnumerable<CompetidorDTO> ObtenerCompetidor()
        {
            try
            {
                List<CompetidorDTO> rpta = new List<CompetidorDTO>();
                var query = @"
                    SELECT Id,Nombre,DuracionCronologica,CostoNeto,Precio,IdMoneda,IdInstitucionCompetidora,IdPais,IdCiudad,IdRegionCiudad,
	                    IdAeaCapacitacion,IdSubAreaCapacitacion,IdCategoria,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM pla.T_Competidor
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CompetidorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Competidor para mostrarse en combo.
        /// </summary>
        /// <returns> List<CompetidorComboDTO> </returns>
        public IEnumerable<CompetidorComboDTO> ObtenerCombo()
        {
            try
            {
                List<CompetidorComboDTO> rpta = new List<CompetidorComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_Competidor WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CompetidorComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Competidores relacionados a una Oportunidad para Agenda.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<CompetidorOportunidadAgendaDTO> </returns>
        public IEnumerable<CompetidorOportunidadAgendaDTO> ObtenerCompetidorParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<CompetidorOportunidadAgendaDTO> competidores = new List<CompetidorOportunidadAgendaDTO>();
                var query = @"
                    SELECT Id, IdOportunidad, Nombre, DuracionCronologica, CostoNeto, Precio, Categoria, Empresa, RegionCiudad,
	                    Moneda, IdCompetidorVentajaDesventaja, ContenidoCompetidorVentajaDesventaja, TipoCompetidorVentajaDesventaja
                    FROM com.V_Oportunidad_Competidores
                    WHERE IdOportunidad = @idOportunidad AND (TipoCompetidorVentajaDesventaja IS NULL OR TipoCompetidorVentajaDesventaja = 0)";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    competidores = JsonConvert.DeserializeObject<List<CompetidorOportunidadAgendaDTO>>(resultadoQuery);
                }
                return competidores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener competidores por id Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns>List<CompetidorRawDTO></returns> 
        public async Task<List<CompetidorRawDTO>> ObtenerCompetidoresPorIdOportunidadAsync(int idOportunidad)
        {
            var query = @"SELECT Id, IdOportunidad, Nombre, DuracionCronologica, CostoNeto, Precio, Categoria, Empresa, RegionCiudad,
                            Moneda, IdCompetidorVentajaDesventaja, ContenidoCompetidorVentajaDesventaja, TipoCompetidorVentajaDesventaja
                        FROM com.V_Oportunidad_Competidores
                        WHERE IdOportunidad = @idOportunidad AND (TipoCompetidorVentajaDesventaja IS NULL OR TipoCompetidorVentajaDesventaja = 0)";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idOportunidad });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return new List<CompetidorRawDTO>();

            try
            {
                return JsonConvert.DeserializeObject<List<CompetidorRawDTO>>(resultado) ?? new List<CompetidorRawDTO>();
            }
            catch
            {
                return new List<CompetidorRawDTO>();
            }
        }
    }
}
