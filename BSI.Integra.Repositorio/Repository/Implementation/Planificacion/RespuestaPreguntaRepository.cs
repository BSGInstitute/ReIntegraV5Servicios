using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{    /// Repositorio: RespuestaPreguntumRepository
     /// Autor: Erick Marcelo Quispe.
     /// Fecha: 10/06/2022
     /// <summary>
     /// Gestión general de T_RespuestaPreguntum
     /// </summary>
    public class RespuestaPreguntaRepository : GenericRepository<TRespuestaPreguntum>, IRespuestaPreguntaRepository
    {
        private Mapper _mapper;

        public RespuestaPreguntaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRespuestaPreguntum, RespuestaPregunta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TRespuestaPreguntum MapeoEntidad(RespuestaPregunta entidad)
        {
            try
            {
                TRespuestaPreguntum modelo = _mapper.Map<TRespuestaPreguntum>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRespuestaPreguntum Add(RespuestaPregunta entidad)
        {
            try
            {
                var RespuestaPreguntum = MapeoEntidad(entidad);
                base.Insert(RespuestaPreguntum);
                return RespuestaPreguntum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRespuestaPreguntum Update(RespuestaPregunta entidad)
        {
            try
            {
                var RespuestaPreguntum = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RespuestaPreguntum.RowVersion = entidadExistente.RowVersion;

                base.Update(RespuestaPreguntum);
                return RespuestaPreguntum;
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


        public IEnumerable<TRespuestaPreguntum> Add(IEnumerable<RespuestaPregunta> listadoEntidad)
        {
            try
            {
                List<TRespuestaPreguntum> listado = new List<TRespuestaPreguntum>();
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

        public IEnumerable<TRespuestaPreguntum> Update(IEnumerable<RespuestaPregunta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRespuestaPreguntum> listado = new List<TRespuestaPreguntum>();
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

        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_RespuestaPregunta por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public RespuestaPregunta ObtenerPorId(int id)
        {
            try
            {
                var rpta = new RespuestaPregunta();
                var query = @"SELECT Id
		                        ,IdPregunta
		                        ,EnunciadoRespuesta
		                        ,NroOrden
		                        ,Puntaje
		                        ,Estado
		                        ,UsuarioCreacion
		                        ,UsuarioModificacion
		                        ,FechaCreacion
		                        ,FechaModificacion
		                        ,RowVersion FROM pla.T_RespuestaPregunta
                                WHERE
                                Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<RespuestaPregunta>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 26/10/2024
        /// <summary>
        /// Obtiene la lista de facgor desaprovatorio
        /// </summary>
        /// <returns>IEnumerable<RespuestaPreguntaFactorDesaprovatorioComboDTO></returns>
        public IEnumerable<RespuestaPreguntaFactorDesaprovatorioComboDTO> ObtenerFactorDesaprovatorio()
        {
            try
            {
                var respuesta = new List<RespuestaPreguntaFactorDesaprovatorioComboDTO>();
                string queryDapper = @"SELECT
                                            Id                 AS IdRespuestaDesaprovatoria,
                                            EnunciadoRespuesta AS Nombre
                                        FROM
                                            gp.T_RespuestaPregunta
                                        WHERE
                                            IdPregunta = 761
                                            AND Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(queryDapper, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<RespuestaPreguntaFactorDesaprovatorioComboDTO>>(resultado);
                    return respuesta;
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 13/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las respuestas asociadas a una pregunta
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> PreguntaRespuestaAsincronicaDTO </returns>
        public List<PreguntaRespuestaAsincronicaDTO> ObtenerRespuestaPregunta(int idPregunta)
        {
            try
            {
                var rpta = new List<PreguntaRespuestaAsincronicaDTO>();
                var query = @"SELECT 
                                    IdRespuestaPregunta, 
                                    IdPregunta, 
                                    NroOrden, 
                                    EnunciadoRespuesta, 
                                    Puntaje 
                             FROM [gp].[V_PreguntaRespuestaAOnline]
                             WHERE IdPregunta = @idPregunta
                             ORDER BY IdRespuestaPregunta DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPregunta });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<PreguntaRespuestaAsincronicaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
