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
    public class PersonalAccesoTemporalAulaVirtualRepository : GenericRepository<TPersonalAccesoTemporalAulaVirtual>, IPersonalAccesoTemporalAulaVirtualRepository
    {
        private Mapper _mapper;
        public PersonalAccesoTemporalAulaVirtualRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalAccesoTemporalAulaVirtual, PersonalAccesoTemporalAulaVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalAccesoTemporalAulaVirtual, PersonalAccesoTemporalAulaVirtualDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalAccesoTemporalAulaVirtual, TPersonalAccesoTemporalAulaVirtual>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalAccesoTemporalAulaVirtual MapeoEntidad(PersonalAccesoTemporalAulaVirtual entidad)
        {
            try
            {
                TPersonalAccesoTemporalAulaVirtual modelo = _mapper.Map<TPersonalAccesoTemporalAulaVirtual>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPersonalAccesoTemporalAulaVirtual Add(PersonalAccesoTemporalAulaVirtual entidad)
        {
            try
            {
                var PersonalAccesoTemporalAulaVirtual = MapeoEntidad(entidad);
                base.Insert(PersonalAccesoTemporalAulaVirtual);
                return PersonalAccesoTemporalAulaVirtual;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPersonalAccesoTemporalAulaVirtual Update(PersonalAccesoTemporalAulaVirtual entidad)
        {
            try
            {
                var PersonalAccesoTemporalAulaVirtual = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalAccesoTemporalAulaVirtual.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalAccesoTemporalAulaVirtual);
                return PersonalAccesoTemporalAulaVirtual;
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
        public IEnumerable<TPersonalAccesoTemporalAulaVirtual> Add(IEnumerable<PersonalAccesoTemporalAulaVirtual> listadoEntidad)
        {
            try
            {
                List<TPersonalAccesoTemporalAulaVirtual> listado = new List<TPersonalAccesoTemporalAulaVirtual>();
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
        public IEnumerable<TPersonalAccesoTemporalAulaVirtual> Update(IEnumerable<PersonalAccesoTemporalAulaVirtual> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalAccesoTemporalAulaVirtual> listado = new List<TPersonalAccesoTemporalAulaVirtual>();
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
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_PersonalAccesoTemporalAulaVirtual por el Primary Key
        /// </summary>
        /// <returns>PersonalAccesoTemporalAulaVirtual o Nulo</returns>
        public PersonalAccesoTemporalAulaVirtual? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT 
	                    Id,
	                    IdPersonal,
	                    IdPEspecifico_Padre AS IdPespecificoPadre,
	                    IdPEspecifico_Hijo AS IdPespecificoHijo,
	                    FechaInicio,
	                    FechaFin,
	                    EvaluacionHabilitada,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PersonalAccesoTemporalAulaVirtual
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalAccesoTemporalAulaVirtual>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 24/06/2024
        /// Versio/n/ 1.0
        /// <summary>
        /// Obtiene el id usuario del portal web por el correo
        /// </summary>
        /// <param name="email">Cadena con el Id del usuario del portal web</param>
        /// <returns>String</returns>
        public string? ObtenerIdUsuarioPortalWebCorreo(string email)
        {
            try
            {
                var query = "[conf].[SP_ObtenerIdUsuarioPortalWebPorCorreo]";
                var respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { Email = email });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<StringDTO>(respuestaQuery)!;
                    return rpta.Valor;
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MaestroPersonalAccesoTemporalDTO> ObtenerListaAccesoTemporal(int idPersonal)
        {
            try
            {
                var listaAccesoTemporal = new List<MaestroPersonalAccesoTemporalDTO>();
                var query = "[gp].[SP_ObtenerDataAccesoTemporalPersonal]";
                var respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    listaAccesoTemporal = JsonConvert.DeserializeObject<List<MaestroPersonalAccesoTemporalDTO>>(respuestaQuery);
                }

                return listaAccesoTemporal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool? EliminarAccesoTemporalPorIdPersonal(int idPersonal, string usuario)
        {
            try
            {
                var resultado = new BoolDTO();
                var query = "[gp].[SP_EliminarAccesoTemporalPorIdPersonal]";
                var respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPersonal = idPersonal, Usuario = usuario });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<BoolDTO>(respuestaQuery);
                }

                return resultado.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarAccesosTemporalesIntegra(ActualizarAccesoTemporalDTO datosAccesoTemporal)
        {
            try
            {
                var resultado = new BoolDTO();
                var query = "[gp].[SP_ActualizarAccesoTemporalPersonal]";
                var respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPersonal = datosAccesoTemporal.IdPersonal, IdPEspecificoPadre = datosAccesoTemporal.IdPEspecificoPadre, IdPEspecificoPadreAnterior = datosAccesoTemporal.IdPEspecificoPadreAnterior, ListaPEspecificoHijo = String.Join(",", datosAccesoTemporal.ListaPEspecificoHijo), FechaInicio = datosAccesoTemporal.FechaInicio, FechaFin = datosAccesoTemporal.FechaFin, FechaInicioAnterior = datosAccesoTemporal.FechaInicioAnterior, FechaFinAnterior = datosAccesoTemporal.FechaFinAnterior, EvaluacionHabilitada = datosAccesoTemporal.EvaluacionHabilitada, Usuario = datosAccesoTemporal.Usuario });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<BoolDTO>(respuestaQuery);
                }

                return resultado.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DatosBasicosPortalContactoDTO ObtenerDatosBasicosPortalWebUsername(string username)
        {
            try
            {
                var resultado = new DatosBasicosPortalContactoDTO();
                var query = "[conf].[SP_ObtenerDatosPortalWebPorUsername]";
                var respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { Username = username });

                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null" && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<DatosBasicosPortalContactoDTO>(respuestaQuery);
                }
                else
                {
                    resultado = null;
                }

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActualizarIdAlumnoUsuarioPortalWeb(string idUsuarioPortalWeb, int idAlumno)
        {
            try
            {
                var query = "[conf].[SP_ActualizarIdAlumnoUsuarioPortalWeb]";
                _dapperRepository.QuerySPFirstOrDefault(query, new { IdUsuarioPortalWeb = idUsuarioPortalWeb, IdAlumno = idAlumno });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool ActualizarAccesosTemporalesPortalWeb(int idPersonal, string idUsuarioPortal, int idAlumno)
        {
            try
            {
                var resultado = new BoolDTO();

                var query = "[gp].[SP_GenerarAccesosTemporalesPersonal]";
                var respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPersonal = idPersonal, IdUsuarioPortal = idUsuarioPortal, IdAlumno = idAlumno });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
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


        public bool EliminarAccesoTemporalPorIdPEspecificoPadre(int idPersonal, int idPEspecificoPadre, DateTime fechaInicio, DateTime fechaFin, string usuario)
        {
            try
            {
                var resultado = new BoolDTO();
                var query = "[gp].[SP_EliminarAccesoTemporalPorIdPEspecificoPadre]";
                var respuestaQuery = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPersonal = idPersonal, IdPEspecificoPadre = idPEspecificoPadre, FechaInicio = fechaInicio, FechaFin = fechaFin, Usuario = usuario });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<BoolDTO>(respuestaQuery);
                }

                return resultado.Valor.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
