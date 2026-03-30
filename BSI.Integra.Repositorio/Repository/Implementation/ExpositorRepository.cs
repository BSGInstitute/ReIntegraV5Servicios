using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ExpositorRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_Expositor
    /// </summary>
    public class ExpositorRepository : GenericRepository<TExpositor>, IExpositorRepository
    {
        private Mapper _mapper;

        public ExpositorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExpositor, Expositor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TExpositor MapeoEntidad(Expositor entidad)
        {
            try
            {
                //crea la entidad padre
                TExpositor modelo = _mapper.Map<TExpositor>(entidad);

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

        public TExpositor Add(Expositor entidad)
        {
            try
            {
                var Expositor = MapeoEntidad(entidad);
                base.Insert(Expositor);
                return Expositor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TExpositor Update(Expositor entidad)
        {
            try
            {
                var Expositor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Expositor.RowVersion = entidadExistente.RowVersion;

                base.Update(Expositor);
                return Expositor;
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


        public IEnumerable<TExpositor> Add(IEnumerable<Expositor> listadoEntidad)
        {
            try
            {
                List<TExpositor> listado = new List<TExpositor>();
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

        public IEnumerable<TExpositor> Update(IEnumerable<Expositor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TExpositor> listado = new List<TExpositor>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de Datos Expositor para mostrarse en combo.
        /// </summary>
        /// <returns> Lista ExpositorDTO </returns>
        public IEnumerable<ExpositorDTO> Obtener()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
		                IdTipoDocumento,
		                NroDocumento,
		                PrimerNombre,
		                SegundoNombre,
		                ApellidoPaterno,
		                ApellidoMaterno,
		                FechaNacimiento,
		                IdPais_Procedencia AS IdPaisProcedencia,
		                IdCiudad_Procedencia AS IdCiudadProcedencia,
		                IdReferidoPor,
		                TelfCelular1,
		                TelfCelular2,
		                TelfCelular3,
		                Email1,
		                Email2,
		                Email3,
		                Domicilio,
		                IdPais_Domicilio AS IdPaisDomicilio,
		                IdCiudad_Domicilio AS IdCiudadDomicilio,
		                LugarTrabajo,
		                IdPais_LugarTrabajo AS IdPaisLugarTrabajo,
		                IdCiudad_LugarTrabajo AS IdCiudadLugarTrabajo,
		                AsistenteNombre,
		                AsistenteTelefono,
		                AsistenteCelular,
		                HojaVidaResumidaPerfil,
		                HojaVidaResumidaSpeech,
		                FormacionAcademica,
		                ExperienciaProfesional,
		                Publicaciones,
		                PremiosDistinciones,
		                OtraInformacion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EsPersonaValida,
		                IdPersonal_Asignado AS IdPersonalAsignado,
		                FotoDocente,
		                UrlFotoDocente,
                        DocenteInstituto
	                FROM pla.T_Expositor WHERE Estado = 1 
	                ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ExpositorDTO>>(resultado)!;
                return new List<ExpositorDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Obtener(): {ex.Message}", ex);
            }
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 2026-03-30
        /// Version: 1.0
        /// <summary>
        /// Busca expositores por email, celular o nro de documento para vincular con proveedores.
        /// </summary>
        public IEnumerable<ExpositorDTO> BuscarPorContacto(string? email, string? celular, string? nroDocumento)
        {
            try
            {
                var parametros = new
                {
                    Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
                    Celular = string.IsNullOrWhiteSpace(celular) ? null : celular.Trim(),
                    NroDocumento = string.IsNullOrWhiteSpace(nroDocumento) ? null : nroDocumento.Trim()
                };

                var resultado = _dapperRepository.QuerySPDapper(
                    "pla.SP_ExpositorBuscarPorContacto",
                    parametros
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ExpositorDTO>>(resultado)!;

                return new List<ExpositorDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en BuscarPorContacto(): {ex.Message}", ex);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de Datos Expositor para mostrarse en combo.
        /// </summary>
        /// <returns> Lista ComboDTO DatosExpositor </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"SELECT Id, Nombre FROM pla.V_DatosExpositor WHERE Estado = 1 ORDER BY Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de Datos Expositor para mostrarse en combo.
        /// </summary>
        /// <returns> Lista ComboDTO DatosExpositor </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id, Nombre FROM pla.V_DatosExpositor WHERE Estado = 1 ORDER BY Nombre";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los expositores por programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<AgendaExpositorDTO> </returns>
        public List<AgendaExpositorDTO> ObtenerExpositoresPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                List<AgendaExpositorDTO> rpta = new List<AgendaExpositorDTO>();
                var query = @"
                    SELECT Nombres,HojaVida
                    FROM pla.V_Expositores
                    WHERE EstadoExpositor = 1 AND IdProgramaGeneral = @idPGeneral
                    GROUP BY Nombres,HojaVida";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaExpositorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el expositor por su Id
        /// </summary>
        /// <param name="idExpositor"> Id de Expositor </param>
        /// <returns> ExpositorDTO </returns>
        public Expositor? ObtenerPorId(int idExpositor)
        {
            try
            {
                var query = @"SELECT
                    Id,
	                IdTipoDocumento,
	                NroDocumento,
	                PrimerNombre,
	                SegundoNombre,
	                ApellidoPaterno,
	                ApellidoMaterno,
	                FechaNacimiento,
	                IdPais_Procedencia AS IdPaisProcedencia,
	                IdCiudad_Procedencia AS IdCiudadProcedencia,
	                IdReferidoPor,
	                TelfCelular1,
	                TelfCelular2,
	                TelfCelular3,
	                Email1,
	                Email2,
	                Email3,
	                Domicilio,
	                IdPais_Domicilio AS IdPaisDomicilio,
	                IdCiudad_Domicilio AS IdCiudadDomicilio,
	                LugarTrabajo,
	                IdPais_LugarTrabajo AS IdPaisLugarTrabajo,
	                IdCiudad_LugarTrabajo AS IdCiudadLugarTrabajo,
	                AsistenteNombre,
	                AsistenteTelefono,
	                AsistenteCelular,
	                HojaVidaResumidaPerfil,
	                HojaVidaResumidaSpeech,
	                FormacionAcademica,
	                ExperienciaProfesional,
	                Publicaciones,
	                PremiosDistinciones,
	                OtraInformacion,
	                Estado,
	                UsuarioCreacion,
	                UsuarioModificacion,
	                FechaCreacion,
	                FechaModificacion,
	                RowVersion,
	                IdMigracion,
	                EsPersonaValida,
	                IdPersonal_Asignado AS IdPersonalAsignado,
	                FotoDocente,
	                UrlFotoDocente
	            FROM pla.T_Expositor 
	            WHERE Estado = 1 AND Id=@idExpositor";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idExpositor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Expositor>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si existe un expositor con el email
        /// </summary>
        /// <param name="email1"></param>
        /// <returns> true o false </returns>
        public bool ExisteExpositorPorEmail(string email1)
        {
            try
            {
                var query = @"SELECT Id 
	                           FROM pla.T_Expositor 
	                           WHERE Estado = 1 and Email1= @Email1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Email1 = email1 });
                return (!string.IsNullOrEmpty(resultado) && resultado != "null");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ExisteExpositorPorEmail: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/08/2023
        /// Version: 1.0
        /// <summary>
        /// Verifica que el correo no sea repetido
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int? ObtenerExpositorEliminadoEmailRepetido(string email)
        {
            try
            {
                var query = "SELECT Id AS Valor FROM pla.T_Expositor WHERE Estado=0 AND Email1=@Email";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Email = email });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    var rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                    return rpta.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#ER-OEEER-001@Error en ObtenerExpositorEliminadoEmailRepetido(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/08/2023
        /// Version: 1.0
        /// <summary>
        /// ELimina de forma fisica de la base de datos de Expositor
        /// </summary>
        /// <returns></returns>
        public bool EliminarFisicaExpositor(string tablaV3, string tablaV4, int idV4, Guid? idv3, int? id_v3)
        {
            try
            {
                bool expositor = new bool();
                string queryExpositor = _dapperRepository.QuerySPDapper("conf.SP_EliminarRegistroTablaMaestro", new { NombreTablaV3 = tablaV3, NombreTablaV4 = tablaV4, IdV4 = idV4, IdV3 = idv3, IdV3Int = id_v3 });
                if (!string.IsNullOrEmpty(queryExpositor) && !queryExpositor.Contains("[]"))
                {
                    expositor = JsonConvert.DeserializeObject<bool>(queryExpositor);
                }
                return expositor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}