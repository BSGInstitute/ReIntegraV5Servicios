using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteAccesoTemporalAulaVirtualRepository : GenericRepository<TPostulanteAccesoTemporalAulaVirtual>, IPostulanteAccesoTemporalAulaVirtualRepository
    {
        private Mapper _mapper;
        public PostulanteAccesoTemporalAulaVirtualRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteAccesoTemporalAulaVirtual, PostulanteAccesoTemporalAulaVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteAccesoTemporalAulaVirtual, PostulanteAccesoTemporalAulaVirtualDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteAccesoTemporalAulaVirtual, TPostulanteAccesoTemporalAulaVirtual>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPostulanteAccesoTemporalAulaVirtual MapeoEntidad(PostulanteAccesoTemporalAulaVirtual entidad)
        {
            try
            {
                TPostulanteAccesoTemporalAulaVirtual modelo = _mapper.Map<TPostulanteAccesoTemporalAulaVirtual>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteAccesoTemporalAulaVirtual Add(PostulanteAccesoTemporalAulaVirtual entidad)
        {
            try
            {
                var PostulanteAccesoTemporalAulaVirtual = MapeoEntidad(entidad);
                base.Insert(PostulanteAccesoTemporalAulaVirtual);
                return PostulanteAccesoTemporalAulaVirtual;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteAccesoTemporalAulaVirtual Update(PostulanteAccesoTemporalAulaVirtual entidad)
        {
            try
            {
                var PostulanteAccesoTemporalAulaVirtual = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteAccesoTemporalAulaVirtual.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteAccesoTemporalAulaVirtual);
                return PostulanteAccesoTemporalAulaVirtual;
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
        public IEnumerable<TPostulanteAccesoTemporalAulaVirtual> Add(IEnumerable<PostulanteAccesoTemporalAulaVirtual> listadoEntidad)
        {
            try
            {
                List<TPostulanteAccesoTemporalAulaVirtual> listado = new List<TPostulanteAccesoTemporalAulaVirtual>();
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
        public IEnumerable<TPostulanteAccesoTemporalAulaVirtual> Update(IEnumerable<PostulanteAccesoTemporalAulaVirtual> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteAccesoTemporalAulaVirtual> listado = new List<TPostulanteAccesoTemporalAulaVirtual>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="idPostulante"> (PK) </param> 
        /// <param name="idPespecificoHijo"> (PK) </param> 
        /// <param name="idPespecificoPadre"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_PostulanteAccesoTemporalAulaVirtual por el Primary Key
        /// </summary>
        /// <returns>PostulanteAccesoTemporalAulaVirtual o Nulo</returns>
        public List<PostulanteAccesoTemporalAulaVirtual> ObtenerPorIdPostulantePespecificoHijoPadre(int idPostulante, int idPespecificoHijo, int idPespecificoPadre)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdPostulante,
		                IdPEspecifico_Padre AS IdPespecificoPadre,
		                IdPEspecifico_Hijo AS IdPEspecificoHijo,
		                FechaInicio,
		                FechaFin,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdAlumno,
		                IdPostulanteProcesoSeleccion,
		                IdExamen
	                FROM gp.T_PostulanteAccesoTemporalAulaVirtual
                    WHERE IdPostulante = @IdPostulante
                        AND IdPEspecifico_Padre = @IdPespecificoPadre
                        AND IdPEspecifico_Hijo = @IdPespecificoHijo
                        AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new
                {
                    IdPostulante = idPostulante,
                    IdPespecificoHijo = idPespecificoHijo,
                    IdPespecificoPadre = idPespecificoPadre
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<PostulanteAccesoTemporalAulaVirtual>>(resultado)!;
                }
                return new List<PostulanteAccesoTemporalAulaVirtual>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="idPostulante"> (PK) </param> 
        /// <param name="idPespecificoHijo"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_PostulanteAccesoTemporalAulaVirtual por el Primary Key
        /// </summary>
        /// <returns>PostulanteAccesoTemporalAulaVirtual o Nulo</returns>
        public PostulanteAccesoTemporalAulaVirtual? ObtenerPorIdPostulantePespecificoHijo(int idPostulante, int idPespecificoHijo)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdPostulante,
		                IdPEspecifico_Padre AS IdPespecificoPadre,
		                IdPEspecifico_Hijo AS IdPEspecificoHijo,
		                FechaInicio,
		                FechaFin,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdAlumno,
		                IdPostulanteProcesoSeleccion,
		                IdExamen
	                FROM gp.T_PostulanteAccesoTemporalAulaVirtual
                    WHERE IdPostulante = @IdPostulante
                        AND IdPEspecifico_Hijo = @IdPespecificoHijo
                        AND Estado = 1
                    ORDER BY Id DESC;";
                var resultado = _dapperRepository.FirstOrDefault(query, new
                {
                    IdPostulante = idPostulante,
                    IdPespecificoHijo = idPespecificoHijo
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteAccesoTemporalAulaVirtual>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdPostulantePespecificoHijo(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
		/// Fecha: 24/06/2024
		/// <summary>
		/// Obtiene el información de accesos del portal web por el correo
		/// </summary>
		/// <param name="email">Cadena con el Id del usuario del portal web</param>
		/// <returns> RespuestaAccesosPostulanteDTO </returns>
		public RespuestaAccesosPostulanteDTO ObtenerAccesosPortalWebCorreo(string email)
        {
            try
            {
                RespuestaAccesosPostulanteDTO resultado = new RespuestaAccesosPostulanteDTO();
                var query = "[conf].[SP_ObtenerAccesosPortalWebPorCorreo]";
                var respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { Email = email });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null" && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<RespuestaAccesosPostulanteDTO>(respuestaQuery)!;
                }
                else
                {
                    resultado.IdAlumno = 0;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R.M.F
        /// Fecha: 24/06/2024
        /// <summary>
        /// Actualiza en la tabla de los accesos temporales para el postulante en la DB del portal web
        /// </summary>
        /// <param name="idPostulante">Id del postulante que se le va a otorgar los accesos temporales (PK de la tabla gp.T_Postulante)</param>
        /// <param name="idUsuarioPortal">Id del usuario del portal, (PK de la tabla dbo.AspNetUsers del portal web)</param>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idPespecifico"> Id de Programa Específico Relaciado a componente </param>
        /// <returns>Bool</returns>
        public bool ActualizarAccesosTemporalesPortalWeb(int idPostulante, string idUsuarioPortal, int idAlumno, int idPespecifico)
        {
            try
            {
                var resultado = new BoolDTO();
                var query = "gp.SP_GenerarAccesosTemporalesPostulante";
                var respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new
                {
                    IdPostulante = idPostulante,
                    IdUsuarioPortal = idUsuarioPortal,
                    IdAlumno = idAlumno,
                    IdPespecifico = idPespecifico
                });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Equals("null"))
                {
                    resultado = JsonConvert.DeserializeObject<BoolDTO>(respuestaQuery);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
