using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PersonalRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 08/06/2022
    /// <summary>
    /// Gestión general de T_Personal
    /// </summary>
    public class PersonalRepository : GenericRepository<TPersonal>, IPersonalRepository
    {
        private Mapper _mapper;

        public PersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonal, Personal>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalDTO, Personal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPersonal MapeoEntidad(Personal entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonal modelo = _mapper.Map<TPersonal>(entidad);

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

        public TPersonal Add(Personal entidad)
        {
            try
            {
                var Personal = MapeoEntidad(entidad);
                base.Insert(Personal);
                return Personal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonal Update(Personal entidad)
        {
            try
            {
                var Personal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Personal.RowVersion = entidadExistente.RowVersion;

                base.Update(Personal);
                return Personal;
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

        public IEnumerable<TPersonal> Add(IEnumerable<Personal> listadoEntidad)
        {
            try
            {
                List<TPersonal> listado = new List<TPersonal>();
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

        public IEnumerable<TPersonal> Update(IEnumerable<Personal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonal> listado = new List<TPersonal>();
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

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Personal para mostrarse en combo.
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public IEnumerable<PersonalComboDTO> ObtenerCombo()
        {
            try
            {
                List<PersonalComboDTO> rpta = new List<PersonalComboDTO>();

                var query = "SELECT Id,CONCAT(Nombres,' ',Apellidos) AS Nombres FROM gp.T_Personal WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Personal
        /// </summary>
        /// <returns> List<PersonalDTO> </returns>
        public IEnumerable<PersonalDTO> ObtenerPersonal()
        {
            try
            {
                List<PersonalDTO> rpta = new List<PersonalDTO>();
                var query = @"SELECT Id, Nombres, Apellidos, Rol, TipoPersonal, Email, AreaAbrev, Anexo, IdJefe, Central, Activo, ApellidoPaterno, ApellidoMaterno, IdSexo, IdEstadocivil, FechaNacimiento, IdPaisNacimiento, IdRegion, IdCiudad, 
                            IdTipoDocumento, NumeroDocumento, AutogeneradoEssalud, IdTipoSangre, UrlFirmaCorreos, IdGrupoProgramasCriticos, IdCerrador, EsCerrador, IdPaisDireccion, IdRegionDireccion, CiudadDireccion, NombreDireccion, 
                            FijoReferencia, MovilReferencia, EmailReferencia, IdSistemaPensionario, IdEntidadSistemaPensionario, NombreCUSPP, DistritoDireccion, ConEssalud, IdBusqueda, AliasEmailAsesor, Anexo3CX, Id3CX, Password3CX, 
                            Dominio, IdFacebookPersonal, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion,  EsPersonaValida, UrlFoto, AplicaFirmaHTML, FirmaHTML, CargoFirmaHTML, 
                            IdPostulante, UsuarioAsterisk, ContrasenaAsterisk, IdTableroComercialCategoriaAsesor, IdPuestoTrabajoNivel, IdPersonalAreaTrabajo, IdPersonalArchivo, IdRolUsuarioTicket, DiscadorActivo, DiferenciaHoraria
                            FROM gp.T_Personal
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerPersonalActivo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT id , CONCAT(Nombres , ' ' ,  Apellidos) AS Nombre FROM gp.T_Personal  WHERE Estado=1 AND Activo = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Victor Hinojosa
        /// Fecha: 20/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal
        /// </summary>
        /// <returns> List<PersonalDetalleDTO> </returns>
        public IEnumerable<PersonalDetalleDTO> ObtenerTodoPersonal()
        {
            try
            {
                List<PersonalDetalleDTO> gestionPersonalDTO = new List<PersonalDetalleDTO>();
                var query = "SELECT Id, Nombres, Apellidos,Area,AsesorCoordinador,AreaAbrev,email,UsuarioModificacion,FechaModificacion,Anexo,Jefe,Dominio,IdDominioPbx, CodigoPaisDiferenciaHoraria IdPais,IdCentral,IdJefe,IdArea,Activo,Estado, Id3CX, Password3CX,UsuarioAsterisk,ContrasenaAsterisk, IdGmailCliente, PasswordCorreo, Ip1 , Ip2 FROM gp.V_TPersonal_ObtenerDatos WHERE Estado = 1  AND RowNumber = 1 ORDER BY FechaModificacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    gestionPersonalDTO = JsonConvert.DeserializeObject<List<PersonalDetalleDTO>>(resultado);
                }
                return gestionPersonalDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Anexo por IdPersonal
        /// </summary>
        /// <returns> List<PersonalDTO> </returns>
        public StringDTO ObtenerAnexoPersonal(int idPersonal)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"SELECT Anexo as Valor FROM gp.T_Personal
                            WHERE Estado=1 AND Id=@idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.Mamani Fabian
        /// Fecha: 22/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Anexo3CX por IdPersonal
        /// </summary>
        /// <returns> List<PersonalDTO> </returns>
        public StringDTO ObtenerAnexo3CXPersonal(int idPersonal)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"SELECT Anexo3CX as Valor FROM gp.T_Personal
                            WHERE Estado=1 AND Id=@idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R.Mamani Fabian
        /// Fecha: 22/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Central por IdPersonal
        /// </summary>
        /// <returns> List<PersonalDTO> </returns>
        public StringDTO ObtenerCentralPersonal(int idPersonal)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"SELECT Central as Valor FROM gp.T_Personal
                            WHERE Estado=1 AND Id=@idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene primer nombre y apellido paterno por el nombre de usuario
        /// </summary>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerPrimerNombreApellidoPaternoPorUserName(string usuario)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"SELECT CONCAT(Nombre,' ',ApellidoPaterno) AS Valor FROM gp.V_TPersonal_ObtenerNombreApellidoPaterno WHERE Usuario=@usuario ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { usuario });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Determina si existe algun registro de T_Personal asociado al correo
        /// </summary>
        /// <param name="email">Correo del Personal</param>
        /// <returns> BoolDTO </returns>
        public BoolDTO ExistePersonalPorCorreo(string email)
        {
            try
            {
                BoolDTO rpta = new BoolDTO()
                {
                    Valor = false
                };
                var query = @"SELECT 1 WHERE EXISTS (SELECT * FROM gp.T_Personal WHERE Email = @email AND Estado = 1)";
                var resultado = _dapperRepository.FirstOrDefault(query, new { email });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta.Valor = true;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la diferencia horaria del asesor
        /// </summary>
        /// <param name="idPersonal">Id del asesor</param>
        /// <returns> ValorIntDTO </returns>
        public IntDTO? ObtenerDiferenciaHoraria(int idPersonal)
        {
            try
            {
                var query = @"SELECT ISNULL(DiferenciaHoraria, 0) AS Valor FROM gp.T_Personal WHERE Id = @idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPersonal = idPersonal });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return new IntDTO()
                {
                    Valor = 0
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerDiferenciaHoraria()", ex);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la diferencia horaria del asesor
        /// </summary>
        /// <param name="idPersonal">Id del asesor</param>
        /// <returns> ValorIntDTO </returns>
        public async Task<IntDTO> ObtenerDiferenciaHorariaAsync(int idPersonal)
        {
            try
            {
                IntDTO rpta = new();
                var query = @"SELECT ISNULL(DiferenciaHoraria, 0) AS Valor FROM gp.T_Personal WHERE Id = @idPersonal";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { IdPersonal = idPersonal });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Personal asociado al identificador.
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> List<PersonalDTO> </returns>
        public Personal? ObtenerPorId(int idPersonal)
        {
            try
            {
                var query = @"SELECT Id,
	                            Nombres,
                                Nombre1,
                                Nombre2,
	                            Apellidos,
	                            Rol,
	                            TipoPersonal,
	                            Email,
	                            AreaAbrev,
	                            Anexo,
	                            IdJefe,
	                            Central,
	                            Activo,
	                            ApellidoPaterno,
	                            ApellidoMaterno,
                                Nombre1,
                                Nombre2,
	                            IdSexo,
	                            IdEstadocivil,
	                            FechaNacimiento,
	                            IdPaisNacimiento,
	                            IdRegion,
	                            IdCiudad,
	                            IdTipoDocumento,
	                            NumeroDocumento,
	                            AutogeneradoEssalud,
	                            IdTipoSangre,
	                            UrlFirmaCorreos,
	                            IdGrupoProgramasCriticos,
	                            IdCerrador,
	                            EsCerrador,
	                            IdPaisDireccion,
	                            IdRegionDireccion,
	                            CiudadDireccion,
	                            NombreDireccion,
	                            FijoReferencia,
	                            MovilReferencia,
	                            EmailReferencia,
	                            IdSistemaPensionario,
	                            IdEntidadSistemaPensionario,
	                            NombreCUSPP,
	                            DistritoDireccion,
	                            ConEssalud,
	                            IdBusqueda,
	                            AliasEmailAsesor,
	                            Anexo3CX,
	                            Id3CX,
	                            Password3CX,
	                            Dominio,
	                            IdFacebookPersonal,
	                            Estado,
	                            UsuarioCreacion,
	                            UsuarioModificacion,
	                            FechaCreacion,
	                            FechaModificacion,
	                            RowVersion,
	                            IdMigracion,
	                            EsPersonaValida,
	                            UrlFoto,
	                            AplicaFirmaHTML,
	                            FirmaHTML,
	                            CargoFirmaHTML,
	                            IdPostulante,
	                            UsuarioAsterisk,
	                            ContrasenaAsterisk,
	                            IdTableroComercialCategoriaAsesor,
	                            IdPuestoTrabajoNivel,
	                            IdPersonalAreaTrabajo,
	                            IdPersonalArchivo,
	                            IdRolUsuarioTicket,
	                            DiscadorActivo,
	                            DiferenciaHoraria,
	                            EnLlamada,
	                            MarcadorPredictivoActivo,
                                CodigoPaisDiferenciaHoraria,
                                IdDominioPbx,
                                Ip1,
                                Ip2
                            FROM gp.T_Personal
                            WHERE Estado = 1 AND Id = @idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Personal>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Email del Personal
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> List<PersonalDTO> </returns>
        public StringDTO ObtenerEmailPorId(int idPersonal)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"SELECT
	                            Email AS Valor
                            FROM gp.T_Personal
                            WHERE Estado = 1 AND Id = @idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 24/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id, Nombre y Apellido a trav�s del email del Asesor.
        /// </summary>
        /// <param name="email"></param>
        /// <returns> Información de nombres, apellidos por email de Asesor : PersonalInformacionCorreoDTO </returns>
        public PersonalInformacionCorreoDTO ObtenerNombreApellido(string email)
        {
            try
            {
                string _query = @"SELECT Id, Nombres, Apellidos, Email 
                                FROM gp.V_TPersonal_ObtenerNombreEmail 
                                WHERE Email=@Email AND Estado=1";
                string queryRespuesta = _dapperRepository.FirstOrDefault(_query, new { @Email = email });
                if (!string.IsNullOrEmpty(queryRespuesta) && queryRespuesta != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalInformacionCorreoDTO>(queryRespuesta);
                }
                throw new Exception("No se encontro correo");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el Id, Nombre y Apellido a trav�s del email del Asesor.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Información de nombres, apellidos por email de Asesor : PersonalInformacionCorreoDTO </returns>
        public PersonalInformacionCorreoDTO ObtenerNombreApellidoPorId(int id)
        {
            try
            {
                string _query = @"SELECT Id, Nombres, Apellidos, Email 
                                FROM gp.V_TPersonal_ObtenerNombreEmail 
                                WHERE Id=@Id AND Estado=1";
                string queryRespuesta = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(queryRespuesta) && queryRespuesta != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalInformacionCorreoDTO>(queryRespuesta);
                }
                throw new Exception("No se encontro correo");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignado(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinados";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el personal por el Id
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns>Personal</returns>
        public Personal ObtenerPersonalPorId(int idPersonal)
        {
            try
            {
                Personal rpta = new Personal();
                var query = @"SELECT Id,AreaAbrev FROM gp.T_Personal  WHERE Id = " + idPersonal + " AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<Personal>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public Personal ObtenerListaPersonalPorEmail(string email, int id)
        {
            try
            {
                Personal rpta = new();
                var query = @"SELECT Id,Email FROM gp.T_Personal  WHERE email = @email AND Estado = 1 AND Id != @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { email, id});
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Personal>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores que no sean coordinadores
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<AsesorNombreFiltroDTO> </returns>
        public List<ReportePersonalDTO> ObtenerAsesoresVentasOficial()
        {
            try
            {
                var asesores = new List<ReportePersonalDTO>();
                var query = "SELECT Id, NombreCompleto, Activo, Estado, IdJefe FROM gp.V_TPersonal_Ventas WHERE Estado=1 AND TipoPersonal <> @TipoPersonal";
                var personalDB = _dapperRepository.QueryDapper(query, new { TipoPersonal = "Coordinador" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    asesores = JsonConvert.DeserializeObject<List<ReportePersonalDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Victor Hinojosa.
        /// Fecha: 18/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores que no sean coordinadores para el reporte de Cambio de Fase
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<AsesorNombreFiltroDTO> </returns>
        public List<ReportePersonalDTO> ObtenerAsesoresVentasOficial_CF(int idPersonal)
        {
            try
            {
                var asesores = new List<ReportePersonalDTO>();
                var query = "com.SP_TPersonal_GetSubordinadosVentas_TCC";
                var personalDB = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    asesores = JsonConvert.DeserializeObject<List<ReportePersonalDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Victor Hinojosa.
        /// Fecha: 18/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores que no sean coordinadores para el reporte de Contactabilidad.
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<AsesorNombreFiltroDTO> </returns>
        public List<ReportePersonalDTO> ObtenerAsesoresVentasOficial_CONT(int idPersonal)
        {
            try
            {
                var asesores = new List<ReportePersonalDTO>();
                var query = "com.SP_TPersonal_GetSubordinadosVentas_TCC";
                var personalDB = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    asesores = JsonConvert.DeserializeObject<List<ReportePersonalDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Carlos Crispin.
        /// Fecha: 17/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores que no sean coordinadores para el rep de ingresos por asesor
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<AsesorNombreFiltroDTO> </returns>
        public List<ReportePersonalDTO> ObtenerAsesoresVentasOficialRI(int idPersonal)
        {
            try
            {
                var asesores = new List<ReportePersonalDTO>();
                var query = "com.SP_TPersonal_GetSubordinadosVentas_TCC";
                var personalDB = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    asesores = JsonConvert.DeserializeObject<List<ReportePersonalDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal de tipo coordinador 
        /// </summary>
        /// <returns> List<ReportePersonalDTO> </returns>
        public List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficial()
        {
            try
            {
                var coordinadores = new List<ReportePersonalDTO>();
                var query = "SELECT Id, NombreCompleto, Activo, Estado, IdJefe FROM gp.V_TPersonal_Ventas WHERE Estado=1 AND TipoPersonal = @TipoPersonal AND TipoPersonal IS NOT NULL";
                var personalDB = _dapperRepository.QueryDapper(query, new { TipoPersonal = "Coordinador" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<ReportePersonalDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Carlos Crispin.
        /// Fecha: 17/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal de tipo coordinador para rep de ingresos por asesor
        /// </summary>
        /// <returns> List<ReportePersonalDTO> </returns>
        public List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficialRI(int idPersonal)
        {
            try
            {
                var coordinadores = new List<ReportePersonalDTO>();
                string query = "com.SP_TPersonal_GetSubordinadosVentas_TCC";
                var personalDB = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<ReportePersonalDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public List<ReportePersonalDTO> ObtenerCoordinadoresVentasOficialTCC(int idPersonal)
        {

            try
            {
                var coordinadores = new List<ReportePersonalDTO>();
                var query = "com.SP_TPersonal_GetSubordinadosVentas_TCC";
                var personalDB = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<ReportePersonalDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PersonalAutocompleteDTO> CargarPersonalParaFiltro()
        {
            try
            {
                string query = "SELECT Id, Nombre from gp.V_TPersonal_NombreCompleto WHERE  Estado = 1 AND Rol = 'VENTAS' AND Activo = 1 ORDER By Nombre ASC";
                string queryRespuesta = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns>List<PersonalAsignadoDTO></returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotal(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosOperacionesTodos";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores de ventas para el reporte de seguimiento por el tipoPersonal=coordinador
        /// </summary>
        /// <returns>Lista de objetos de clase PersonalAsignadoDTO</returns>
        public List<PersonalAsignadoDTO> ObtenerAsesoresVentasOficialReporteSeguimiento()
        {
            try
            {
                List<PersonalAsignadoDTO> asesores = new List<PersonalAsignadoDTO>();
                var query = @"SELECT 
                                Id,
                                NombreCompleto AS Nombres,
                                Email,
                                Activo
                            FROM gp.V_TPersonal_Ventas
                            WHERE TipoPersonal <> @TipoPersonal";
                var personalDB = _dapperRepository.QueryDapper(query, new { TipoPersonal = "Coordinador" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    asesores = JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns>Lista de ObjetosDTO: List(PersonalAsignadoDTO)</returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoVentas(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosVentas";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Carlos Crispin.
        /// Fecha: 17/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores asignados a un Personal para el reporte seg oportunidades.
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns>Lista de ObjetosDTO: List(PersonalAsignadoDTO)</returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoVentasRS(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosVentas_RS";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns>Lista de Persoal asignado</returns>
        public async Task<List<PersonalAsignadoDTO>> ObtenerPersonalAsignadoVentasAsync(int idPersonal)
        {
            try
            {
                string query = "com.SP_TPersonal_GetSubordinadosVentas";
                string resultado = await _dapperRepository.QuerySPDapperAsync(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(resultado)!;
                }
                return new List<PersonalAsignadoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPersonalAsignadoVentasAsync(), {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Personal (activos) del Área de Ventas
        /// </summary>
        /// <returns></returns>
        public List<AsesorFiltroDTO> ObtenerPersonalAsesoresFiltro()
        {
            try
            {
                List<AsesorFiltroDTO> personalMinReasignacion = new List<AsesorFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto, Asignado FROM com.V_TPersonal_ObtenerPersonalVentas WHERE Estado = 1 and  Rol = 'VENTAS'";
                var personalDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    personalMinReasignacion = JsonConvert.DeserializeObject<List<AsesorFiltroDTO>>(personalDB);
                }
                return personalMinReasignacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Coordinadores (activos) del Área de Ventas
        /// </summary>
        /// <returns></returns>
        public List<CoordinadorFiltroDTO> ObtenerPersonalCoordinadoresFiltro()
        {
            try
            {
                List<CoordinadorFiltroDTO> coordinadores = new List<CoordinadorFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto FROM com.V_TPersonal_ObtenerCoordinadorVentas";
                var personalDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<CoordinadorFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del personal
        /// </summary>
        /// <param name="idAsesor"> Id de Personal </param>
        /// <returns> PersonalMinReasignacionDTO </returns>
        public PersonalMinReasignacionDTO ObtenerPersonalReasignacion(int idAsesor)
        {
            try
            {
                PersonalMinReasignacionDTO personalMinReasignacion = new PersonalMinReasignacionDTO();
                var _query = @"SELECT IdAsesor, NombreCompletoAsesor, EmailAsesor, 
                                      IdJefe,NombreCompletoJefe, EmailJefe 
                                FROM com.V_TPersonal_ObtenerAsesorCorreoReasignacion 
                                WHERE EstadoPersonal = 1 AND EstadoJefe = 1 AND IdAsesor = @idAsesor";
                var personalDB = _dapperRepository.FirstOrDefault(_query, new { idAsesor });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    personalMinReasignacion = JsonConvert.DeserializeObject<PersonalMinReasignacionDTO>(personalDB);
                }
                return personalMinReasignacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion necesaria del Personal para la  Agenda.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns>retorna un objeto tipo PersonalDatosAgendaDTO</returns>
        public PersonalDatosAgendaDTO ObtenerDatosPersonalAgenda(int idPersonal)
        {
            try
            {
                string query = @"SELECT Id, Nombres, Apellidos, Rol, TipoPersonal, Email, AreaAbrev,Anexo, IdJefe,
                                Central, Anexo3Cx, Id3cx, Password3Cx, Dominio, UsuarioAsterisk, ContrasenaAsterisk, IdAsterisk 
                                FROM gp.V_TPersonal_DatosAgenda 
                                WHERE Estado=1 AND Activo=1 AND Id=@IdPersonal";
                var respuestaQuery = _dapperRepository.FirstOrDefault(query, new { IdPersonal = idPersonal });
                if (respuestaQuery != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalDatosAgendaDTO>(respuestaQuery);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el personal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DatoCompletoPersonalDTO ObtenerDatoPersonal(int id)
        {
            try
            {
                var personal = new DatoCompletoPersonalDTO();
                var query = $@"
                            SELECT Id, 
                                   Nombres, 
                                   Apellidos, 
                                   Anexo3Cx, 
                                   Central, 
                                   Email,
                                   MovilReferencia, 
                                   PrimerNombreApellidoPaterno,
                                   PrimerNombre AS Nombre1,
                                   CodigoPaisDiferenciaHoraria,
                                   DiferenciaHoraria
                            FROM gp.V_ObtenerPersonalNombreCompleto
                            WHERE Estado = 1
                                  AND Activo = 1
                                  AND Id = @id
                            ";
                var personalAsesor = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(personalAsesor))
                {
                    personal = JsonConvert.DeserializeObject<DatoCompletoPersonalDTO>(personalAsesor);
                }
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el horario de trabajo del personal
        /// </summary>
        /// <param name="Id">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <returns>Obtener el horario de trabajo de un personal en formato HTML</returns>
        public string ObtenerHorarioTrabajo(int id)
        {
            try
            {
                var resultadoFinal = new StringDTO();
                var query = "gp.SP_ObtenerPersonalHorarioAtencion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPersonal = id });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    resultadoFinal = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2021
        /// <summary>
        /// Obtener información de Personal por nombre AutoComplete
        /// </summary>
        /// <param name="nombre"> nombre de búsqueda </param>
        /// <returns> Lista de Personal por nombre Registrados : List<PersonalAutocompleteDTO> </returns>
        public List<PersonalAutocompleteDTO> CargarPersonalAutoComplete(string nombre)
        {
            try
            {
                string query = @"SELECT Id, Nombre from gp.V_TPersonal_NombreCompleto WHERE Nombre LIKE CONCAT('%',@nombre,'%') 
                                 AND Estado = 1 AND Rol = 'VENTAS' AND Activo = 1 ORDER By Nombre ASC";
                string queryRespuesta = _dapperRepository.QueryDapper(query, new { nombre });
                return JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(queryRespuesta)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor:Margiory Ramirez Neyra.
        /// Fecha: 09/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene  todo el personal que son asesores por id de grupo
        /// </summary>
        /// <param name="idGrupo">Id del grupo de filtro programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <returns>Lista de objetos de clase DatosPersonalAsesorPorGrupoIdDTO</returns>
        public List<DatosPersonalAsesorPorGrupoIdDTO> ObtenerAsesoresPorGrupoId(int idGrupo)
        {
            try
            {
                List<DatosPersonalAsesorPorGrupoIdDTO> personalAsesores = new List<DatosPersonalAsesorPorGrupoIdDTO>();
                var query = string.Empty;
                query = "SELECT Id,Nombres,Apellidos,Email,NombreCompleto,asignado,IdAsesor FROM gp.V_TPersonal_ObtenerAsesoresPorGrupoId WHERE Rol = 'VENTAS' and IdGrupo = @IdGrupo and Estado = 1 ";
                var personalAsesor = _dapperRepository.QueryDapper(query, new { IdGrupo = idGrupo });
                personalAsesores = JsonConvert.DeserializeObject<List<DatosPersonalAsesorPorGrupoIdDTO>>(personalAsesor);

                return personalAsesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Coordinadores Para Filtro
        /// </summary>
        /// <param></param>
        /// <returns>Lista Objeto: List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerCoordinadoresParaFiltro()
        {
            try
            {
                var query = @"SELECT 
                                Id, Nombre,TipoPersonal 
                            FROM 
                                [ope].[V_ObtenerCoordinadoresOperaciones] 
                            WHERE 
                                Estado = 1 AND Activo = 1 AND (Rol = 'OPERACIONES' or Rol = 'Atención al cliente') AND IdRol = 17";
                var resultado = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 08/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener configuracion de open vox por el id del Personal
        /// </summary>
        /// <param name="idPersonal"> id del personal </param>
        /// <returns> Lista Objeto: List<PersonalConfiguracionOpenVoxDTO> </returns>
        public List<PersonalConfiguracionOpenVoxDTO> ObtenerConfiguracionOpenVoxPorIdPersonal(int idPersonal)
        {
            try
            {
                var resultadoLista = new List<PersonalConfiguracionOpenVoxDTO>();
                var query = "SELECT IdPais, Prefijo, Anexo FROM COM.V_ObtenerConfiguracionOpenVoxPersonal WHERE IdPersonal = @IdPersonal";
                var resultadoPlano = _dapperRepository.QueryDapper(query, new { IdPersonal = idPersonal });

                if (!string.IsNullOrEmpty(resultadoPlano))
                    resultadoLista = JsonConvert.DeserializeObject<List<PersonalConfiguracionOpenVoxDTO>>(resultadoPlano);

                return resultadoLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Id, Nombre, Apellido, CorreoElectronico de todo Personal Activo y con Estado=1
        /// </summary>
        /// <param></param>
        /// <returns>List<PersonalActivoEmailDTO></returns> 
        public List<PersonalActivoEmailDTO> ObtenerTodoPersonalActivoParaFiltro()
        {
            try
            {
                List<PersonalActivoEmailDTO> personalAsesores = new List<PersonalActivoEmailDTO>();
                var _query = "SELECT Id, Nombres, Apellidos, Email FROM com.V_TPersonal_ObtenerAsesores where Rol = 'VENTAS' AND (TipoPersonal IN ('ASESOR','Coordinador')) AND Activo = 1 and Estado = 1";
                var personalAsesor = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(personalAsesor) && !personalAsesor.Contains("[]") && personalAsesor != null && personalAsesor != "null")
                {
                    personalAsesores = JsonConvert.DeserializeObject<List<PersonalActivoEmailDTO>>(personalAsesor);
                }
                return personalAsesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores y sus coordinadores para filtros (id, nombres)
        /// </summary>
        /// <returns>Lista de objetos de clase AsesorNombreFiltroDTO</returns>
        public List<AsesorNombreFiltroDTO> ObtenerTodoAsesorCoordinadorVentas()
        {
            try
            {
                List<AsesorNombreFiltroDTO> Coordinadores = new List<AsesorNombreFiltroDTO>();
                var query = @"SELECT Id,Concat(Nombres, ' ',Apellidos) AS NombreCompleto FROM com.V_TPersonal_ObtenerAsesores
                                WHERE Rol = 'VENTAS' AND (TipoPersonal IN ('ASESOR','Coordinador')) AND estado = 1";
                var personalAsesor = _dapperRepository.QueryDapper(query, new { TipoPersonal = "Asesor", Rol = "VENTAS" });
                if (!string.IsNullOrEmpty(personalAsesor) && !personalAsesor.Contains("[]") && personalAsesor != null && personalAsesor != "null")
                {
                    Coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalAsesor);
                }
                return Coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public IEnumerable<PersonalComboDTO> ObtenerPersonalPorMarketing()
        {
            try
            {
                List<PersonalComboDTO> rpta = new List<PersonalComboDTO>();

                var query = "SELECT Id,concat(Nombres,' ',Apellidos) as Nombres FROM gp.T_Personal WHERE Activo = 1 AND Rol = 'Marketing'";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Personal Asignado Operaciones
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> List<PersonalAsignadoDTO> </returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesTotalV2(int idPersonal)
        {
            try
            {
                List<PersonalAsignadoDTO> resultado = new List<PersonalAsignadoDTO>();
                string query = "com.SP_TPersonal_GetSubordinadosOperacionesTodosV2";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(respuestaQuery) && respuestaQuery != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> Lista de DTO: List<PersonalAsignadoDTO> </returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperacionesUsuarioTotal(int idPersonal)
        {
            try
            {
                List<PersonalAsignadoDTO> listaResultado = new List<PersonalAsignadoDTO>();
                string query = "com.SP_TPersonal_GetSubordinadosOperacionesTodosUsuario";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    listaResultado = JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery)!;
                }
                return listaResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> DTO: PersonalAsignadoReportePendienteDTO </returns>
        public PersonalAsignadoReportePendienteDTO ObtenerDatosUsuariosReportePendiente(string usuario)
        {
            try
            {
                PersonalAsignadoReportePendienteDTO personal = new PersonalAsignadoReportePendienteDTO();
                var query = string.Empty;
                query = @"
                        SELECT 
                            Id, Nombres, Activo, Email, TipoPersonal, Usuario 
                        FROM 
                            gp.V_ObtenerDatosPersonalPorUsuario 
                        WHERE 
                            Usuario = @Usuario";
                var personalDB = _dapperRepository.FirstOrDefault(query, new { Usuario = usuario });
                personal = JsonConvert.DeserializeObject<PersonalAsignadoReportePendienteDTO>(personalDB)!;
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Autor: Margiory Ramirez.
        /// Fecha: 18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del asesor por el apellido
        /// </summary>
        /// <returns>Json/returns>


        public object ObtenerAsesorPorApellidos()
        {
            try
            {

                var R = GetBy(x => x.Rol == "Ventas" || x.TipoPersonal == "otro" && x.Estado == true, x => new { x.Id, x.Nombres, x.Apellidos, x.Email, NombreCompleto = x.Nombres + " " + x.Apellidos });
                return R;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }





        /// Autor: Margiory Ramirez
        /// Fecha:  18/01/2023
        /// Versión: 1.0
        /// <summary>
        /// onbtiene los datos del coordinador por el apellido
        /// </summary>
        /// <returns>Json/returns>



        public object ObtenerCoordinadorPorApellidos()
        {
            try
            {
                return (GetBy(x => x.TipoPersonal.Contains("coor") && x.Estado == true, x => new { x.Id, NombreCompleto = x.Nombres + " " + x.Apellidos }));

            }
            catch (Exception e)

            {

                throw new Exception(e.Message);
            }
        }



        /// Autor: Margiory Ramirez.
        /// Fecha: 18/06/2023
        /// Versión: 1.0
        /// <summary>
        /// obtieneel personal vcalidado en base al apellido
        /// </summary>
        /// <returns>Json/returns>


        public List<PersonalComboAprobadoDTO> ObtenerPersonalAprobadoPorApellido(Dictionary<string, string> Valor)
        {
            try
            {
                List<PersonalComboAprobadoDTO> respuesta = new List<PersonalComboAprobadoDTO>();

                if (Valor != null && Valor.Count > 0)
                {
                    //PersonalRepositorio _repPersonal = new PersonalRepositorio();
                    //var tempAprobados = new int[] { 13, 24, 213, 10, 74 };
                    var query = @"SELECT Id,NombreCompleto FROM fin.V_ObetenerPersonalAprobacionCambioMatricula WHERE NombreCompleto LIKE '%" + Valor["valor"] + "%'";
                    string respuestaQuery = _dapperRepository.QueryDapper(query, null);
                    if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                    {
                        respuesta = JsonConvert.DeserializeObject<List<PersonalComboAprobadoDTO>>(respuestaQuery)!;
                    }
                    //var repPersonal = GetBy(x => x.Activo == true && x.Apellidos.Contains(Valor["valor"]) && tempAprobados.Contains(x.Id), x => new { x.Id, NombreCompleto = string.Concat(x.Apellidos, " ", x.Nombres) });
                    return respuesta;
                }
                else
                {
                    return respuesta;
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Autor: Jonathan Caipo
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>Original solo activos
        /// Obtiene los asesores asignados a un Personal.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> Lista DTO: List<PersonalAsignadoDTO> </returns>
        public List<PersonalAsignadoDTO> ObtenerPersonalAsignadoOperaciones(int idPersonal)
        {
            try
            {
                List<PersonalAsignadoDTO> respuesta = new List<PersonalAsignadoDTO>();
                string query = "com.SP_TPersonal_GetSubordinadosOperaciones";
                string respuestaQuery = _dapperRepository.QuerySPDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<PersonalAsignadoDTO>>(respuestaQuery)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los asesores de operaciones activos
        /// </summary>
        /// <returns> Lista DTO: List<AsesorFiltroDTO> - listaPersonal </returns>
        public List<AsesorFiltroDTO> ObtenerPersonalAsesoresOperacionesActivos()
        {
            try
            {
                List<AsesorFiltroDTO> listaPersonal = new List<AsesorFiltroDTO>();
                var query = "com.SP_TPersonalObtenerAsistenteOperaciones";
                var respuestaQuery = _dapperRepository.QuerySPDapper(query, new { });
                if (!string.IsNullOrEmpty(respuestaQuery))
                {
                    listaPersonal = JsonConvert.DeserializeObject<List<AsesorFiltroDTO>>(respuestaQuery)!;
                }
                return listaPersonal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal tipo asesor y pertenescan solo al area de ventas
        /// </summary>
        /// <param></param>
        /// <returns> Lista DTO - List<AsesorNombreFiltroDTO> - coordinadores </returns>
        public IEnumerable<PersonalComboDTO> ObtenerCoordinadorasOperaciones()
        {
            try
            {
                IEnumerable<PersonalComboDTO> rpta = new List<PersonalComboDTO>();
                string query = @"
                                SELECT 
                                    Id, NombreCompleto as Nombres
                                FROM 
                                    gp.V_TPersonal_ObtenerSubordinado 
                                WHERE 
                                    estado = 1 AND activo = 1 AND  TipoPersonal = @TipoPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { TipoPersonal = "Asesor" });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && !resultado.Contains("null"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<PersonalComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerCoordinadorasOperaciones()", ex);
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Se obtiene Información de Personal del Área de Gestión de Personas Activos para Filtro
        /// </summary>
        /// <returns> List<DatoPersonalDTO> </returns>
        public List<FiltroCombosDTO> ObtenerComboPersonalGestionPersonas()
        {
            try
            {
                List<FiltroCombosDTO> personal = new List<FiltroCombosDTO>();
                var query = string.Empty;
                query = "SELECT Id,NombreCompleto AS Nombre FROM gp.V_TPersonalDatosPorArea WHERE AreaAbrev ='GP' AND Activo = 1 AND Estado = 1 ";
                var respuesta = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && !respuesta.Contains("null"))
                {
                    personal = JsonConvert.DeserializeObject<List<FiltroCombosDTO>>(respuesta);
                }
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Eliot Arias F.
        ///Fecha: 06/11/2024
        /// <summary>
        /// Se obtiene Información de Personal del Área de Gestión de Personas Activos
        /// </summary>
        /// <param>idPersonal</param>
        /// <returns> DatoPersonalDTO </returns>
        public FiltroCombosDTO ObtenerPersonalGestionPersonasPorId(int idPersonal)
        {
            try
            {
                FiltroCombosDTO personal = new FiltroCombosDTO();
                var query = string.Empty;
                query = "SELECT Id,NombreCompleto AS Nombre FROM gp.V_TPersonalDatosPorArea WHERE AreaAbrev ='GP' AND Id = @Id AND Activo = 1 AND Estado = 1";
                var respuesta = _dapperRepository.FirstOrDefault(query, new { Id = idPersonal });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && !respuesta.Contains("null"))
                {
                    personal = JsonConvert.DeserializeObject<FiltroCombosDTO>(respuesta);
                }
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }



        ///Repositorio: PersonalRepositorio
        ///Autor: Griselberto.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Retrna todo el personal de ventas, Coordinador y asesores
        /// </summary>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerPersonalVentasV4()
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto FROM gp.V_TPersonal_ObtenerSubordinado where estado = 1 and activo = 1  and Rol = @Rol";
                var personalDB = _dapperRepository.QueryDapper(_query, new { Rol = "VENTAS" });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Autor: Flavio R. Mamani Fabian
        ///Fecha: 14/09/2023
        /// <summary>
        /// Retrna todo el personal de ventas, Coordinador y asesores
        /// </summary>
        /// <returns></returns>
        public int? ObtenerPaisSedePersonal(int idPersonal)
        {
            try
            {
                var query = "SELECT IdPais_Sede AS Valor FROM gp.T_Personal WHERE Id=@idPersonal AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                    return rpta.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PR-OPSP-001@Error en ObtenerPaisSedePersonal, {ex.Message}");
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Grisleberto.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Obtiene personal para autocomplete
        /// </summary>
        /// <returns></returns>
        public List<PersonalAutocompleteDTO> ObtenerNombresFiltroAutoComplete(string valor)
        {
            try
            {
                List<PersonalAutocompleteDTO> PersonalAutocompleteFiltro = new List<PersonalAutocompleteDTO>();
                string queryPersonalNombresFiltro = "SELECT Id,Nombre FROM gp.V_TPersonal_NombreCompleto WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By Id ASC";
                var PersonalDB = _dapperRepository.QueryDapper(queryPersonalNombresFiltro, new { valor });
                if (!string.IsNullOrEmpty(PersonalDB) && !PersonalDB.Contains("[]"))
                {
                    PersonalAutocompleteFiltro = JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(PersonalDB);
                }
                return PersonalAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Grisleberto.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Obtiene personal Asistente Coordinador/Cobranza Matricula.
        /// </summary>
        /// <returns></returns>
        public List<PersonalAutocompleteDTO> ObtenerAsistenteAcademicoMatricula(string valor)
        {
            try
            {
                List<PersonalAutocompleteDTO> PersonalAutocompleteFiltro = new List<PersonalAutocompleteDTO>();
                string queryPersonalNombresFiltro = "SELECT Id,Nombre FROM fin.V_ObtenerAsistenteAcademicoMatricula  WHERE Nombre LIKE CONCAT('%',@valor,'%') ORDER By Id ASC";
                var PersonalDB = _dapperRepository.QueryDapper(queryPersonalNombresFiltro, new { valor });
                if (!string.IsNullOrEmpty(PersonalDB) && !PersonalDB.Contains("[]"))
                {
                    PersonalAutocompleteFiltro = JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(PersonalDB);
                }
                return PersonalAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: PersonalRepositorio
        ///Autor: Edmundo Llaza
        ///Fecha: 2023-07-27
        /// <summary>
        /// Obtiene lista de coordinadoras de los docentes
        /// </summary>
        /// <param name="coordinador"></param>
        /// <returns></returns>
        public List<ComboDTO> ObtenerCoordinadorasDocente()
        {
            try
            {
                var query = "SELECT DISTINCT PER.Id, CONCAT(Nombres, ' ', Apellidos) AS Nombre FROM gp.T_Personal AS PER INNER JOIN fin.T_Proveedor AS PRO ON PRO.IdPersonal_Asignado = PER.Id WHERE PER.Estado = 1 AND PRO.Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ComboDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 27/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del usuario
        /// </summary>
        /// <returns> Entidad: usuarioEntidad </returns>
        public IEnumerable<ComboDTO> ObtenerPersonalAutocomplete(string valor)
        {
            try
            {
                List<ComboDTO> personal = new List<ComboDTO>();
                string _query = @"SELECT   
                                  Id, CONCAT(Nombres, ' ', Apellidos) As Nombre   
                                FROM gp.T_Personal   
                                WHERE Activo = 1 AND CONCAT(Nombres, ' ', Apellidos) LIKE CONCAT('%',@valor,'%')";
                var res = _dapperRepository.QueryDapper(_query, new { valor });
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    personal = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return personal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Junior Llerena
        ///Fecha: 16/07/2025
        /// <summary>
        /// Obtiene lista de las areas de trabajo del pesonal
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public IEnumerable<PersonalComboAreaDTO> ObtenerPersonalAreaTrabajo()
        {
            try
            {
                List<PersonalComboAreaDTO> area = new List<PersonalComboAreaDTO>();
                string _query = @"SELECT 
                            pat.Id AS Id, 
                            pat.Nombre AS Nombre, 
                            pat.Codigo AS Codigo 
                            FROM gp.T_PersonalAreaTrabajo pat WHERE pat.Estado = 1";
                var res = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    area = JsonConvert.DeserializeObject<List<PersonalComboAreaDTO>>(res);
                }
                return area;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///Repositorio: PersonalRepositorio
        ///Autor: Edmundo Llaza
        ///Fecha: 2023-07-27
        /// <summary>
        /// Obtiene lista de coordinadoras de los docentes
        /// </summary>
        /// <param name="coordinador"></param>
        /// <returns></returns>
        public bool InsertarNuevaContrasena(PersonalNuevaContraseniaDTO dto)
        {
            try
            {
                var query = "com.SP_InsertarCorreoNuevaContraseña";
                var res = _dapperRepository.QuerySPDapper(query, new
                {
                    dto.Usuario,
                    dto.NuevaContrasena
                });
                if (!string.IsNullOrEmpty(res) && res != "[]") return true;
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Autor: Joseph LLanque.
        ///Fecha: 13/12/2023
        /// <summary>
        /// Obtiene personal con agenda liberada operaciones
        /// </summary>
        /// <returns></returns>
        public List<PersonalAutocompleteDTO> ObtenerPersonalAgendaLiberadaOperaciones()
        {
            try
            {
                List<PersonalAutocompleteDTO> PersonalAutocompleteFiltro = new List<PersonalAutocompleteDTO>();
                string queryPersonalNombresFiltro = "SELECT Id,Nombre FROM [ope].[V_ObtenerPersonalAgendaLiberadaOperaciones] ";
                var PersonalDB = _dapperRepository.QueryDapper(queryPersonalNombresFiltro, null);
                if (!string.IsNullOrEmpty(PersonalDB) && !PersonalDB.Contains("[]"))
                {
                    PersonalAutocompleteFiltro = JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(PersonalDB);
                }
                return PersonalAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta la Marcacion del Personal
        /// </summary>
        /// <returns> true or False </returns>
        public bool InsertarMarcacionPersonal(string data, string usuario)
        {
            try
            {
                var query = @"[gp].[SP_TRegistroMarcadorFecha_Insertar]";

                var resultado = _dapperRepository.QuerySPDapper(query, new { JsonString = data, Usuario = usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null") return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 03/06/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id de personal por su username
        /// </summary>
        /// <returns> int </returns>
        public int ObtenerIdPersonalPorUserName(string UserName)
        {
            try
            {
                var queryPersonal = @"SELECT TOP 1 PerId AS Id FROM conf.T_Integra_AspNetUsers WHERE Estado=1 and UserName=@UserName";
                var resultado = _dapperRepository.FirstOrDefault(queryPersonal, new { UserName = UserName });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null" && resultado != null)
                {
                    var rpta = JsonConvert.DeserializeObject<IdDTO>(resultado);
                    return rpta.Id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }    
        
        ///Repositorio: PersonalRepositorio
        ///Autor: Edgar S.
        ///Fecha: 16/03/2021
        /// <summary>
        /// Se obtiene Información de Personal del Área de Gestión de Personas Activos para Filtro
        /// </summary>
        /// <returns> List<DatoPersonalDTO> </returns>
        public List<DatoPersonalPersonalAprobacionDTO> ObtenerDatosPersonal()
        {
            try
            {
                List<DatoPersonalPersonalAprobacionDTO> personal = new List<DatoPersonalPersonalAprobacionDTO>();
                var query = string.Empty;
                query = "SELECT Id,NombreCompleto  FROM gp.V_TPersonalDatos WHERE Activo = 1 AND Estado = 1 ";
                var respuesta = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && !respuesta.Contains("null"))
                {
                    personal = JsonConvert.DeserializeObject<List<DatoPersonalPersonalAprobacionDTO>>(respuesta);
                }
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public IEnumerable<PersonalFichaDatosDTO> ObtenerFichaDatosPersonal()
        {
            try
            {
                var query = @"
                    SELECT
                    Id,
                    Nombres,
                    Apellidos,
                    Rol,
                    Email,
                    Activo
                    FROM gp.T_Personal where Estado=1
                    ORDER BY Id DESC ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PersonalFichaDatosDTO>>(resultado)!;
                }
                return new List<PersonalFichaDatosDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCR-OT-002@Error en ObtenerFichaDatosPersonal() {ex.Message}", ex);
            }
        }
        public IEnumerable<ComboDTO> ObtenerComboNombre()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id,CONCAT(Nombres,' ',Apellidos) AS Nombre FROM gp.T_Personal WHERE Estado = 1 AND Activo = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ComboDTO> ObtenerAsesorCerrador()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id,CONCAT(Nombres,' ',Apellidos) AS Nombre FROM gp.T_Personal WHERE Estado = 1 AND Activo = 1 AND EsCerrador=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MaestroPersonalPuestoSedeDTO ObtenerInformacionPersonalPuestoSede(int idPersonal)
        {
            try
            {
                MaestroPersonalPuestoSedeDTO personal = new MaestroPersonalPuestoSedeDTO();
                var query = @"
					SELECT Id, 
						   Apellidos, 
						   Nombres, 
                           IdPersonalAreaTrabajo,
						   FijoReferencia, 
						   MovilReferencia, 
						   EmailReferencia, 
						   IdPaisNacimiento, 
						   IdCiudad, 
						   FechaNacimiento, 
						   IdPaisDireccion, 
						   IdRegionDireccion, 
						   DistritoDireccion, 
						   NombreDireccion, 
						   IdTipoDocumento, 
						   NumeroDocumento, 
						   IdEstadoCivil, 
						   IdSexo, 
                           IdPuestoTrabajo,
                           IdSedeTrabajo,
						   IdSistemaPensionario, 
						   IdEntidadSistemaPensionario, 
						   CodigoAfiliado,
						   IdEntidadSeguroSalud,
						   Email,
						   TipoPersonal,
						   IdJefe,
						   Central,
						   Anexo3CX,
						   UrlFirmaCorreos,
						   Activo, 
						   IdTipoSangre,
                           EsCerrador,
                           IdCerrador,
                           IdPuestoTrabajoNivel,
						   Estado,
                           IdTableroComercialCategoriaAsesor,
                           IdPersonalArchivo
					FROM gp.V_TPersonal_InformacionPersonalRegistradoPuestoSede
					WHERE Id = @IdPersonal";
                var res = _dapperRepository.FirstOrDefault(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(res))
                {
                    personal = JsonConvert.DeserializeObject<MaestroPersonalPuestoSedeDTO>(res);
                }
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<PersonalDireccionVistaDTO> ObtenerPersonalDireccionDomiciliaria(int idPersonal)
        {
            try
            {
                List<PersonalDireccionVistaDTO> lista = new List<PersonalDireccionVistaDTO>();
                var query = "SELECT Id, IdPersonal, IdPais, IdCiudad, Distrito, TipoVia, NombreVia, Manzana, Lote, TipoZonaUrbana, NombreZonaUrbana, Activo, UsuarioModificacion, FechaModificacion FROM gp.t_personaldireccion WHERE IdPersonal = @IdPersonal AND Estado = 1";
                var respuesta = _dapperRepository.QueryDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PersonalDireccionVistaDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public int? ObtenerPersonalEliminadoEmailRepetido(string email)
        {
            try
            {
                Dictionary<string, int> personal = new Dictionary<string, int>();
                var query = "SELECT Id FROM gp.T_Personal where estado = 0 and Email=@Email";
                var personalDB = _dapperRepository.FirstOrDefault(query, new { Email = email });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    personal = JsonConvert.DeserializeObject<Dictionary<string, int>>(personalDB);
                }
                return personal.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool ActivarPersonal(int Id)
        {
            try
            {
                var query = "gp.SP_Personal_ActualizarEstado";
                var parametros = new
                {
                    Id = Id
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarEstadoEnviado() {ex.Message}", ex);
            }
        }



        public List<FiltroPersonalJefaturaFiltroDTO> ObtenerPersonalJefaturaFiltro(string condiciones)
        {
            try
            {
                List<FiltroPersonalJefaturaFiltroDTO> personal = new List<FiltroPersonalJefaturaFiltroDTO>();
                var query = string.Empty;
                if (condiciones.Length > 0)
                {
                    query = "SELECT PersonalAreaTrabajo,Personal,PersonalPuestoTrabajo,PersonasACargo, Estado, FechaInicioPuesto, FechaIngreso, FechaCese, JefeInmediato, PuestoJefeInmediato FROM gp.V_TPersonal_ObtieneJefePersonalFiltro WHERE " + condiciones;
                }
                else
                {
                    query = "SELECT PersonalAreaTrabajo,Personal,PersonalPuestoTrabajo,PersonasACargo, Estado, FechaInicioPuesto, FechaIngreso, FechaCese, JefeInmediato, PuestoJefeInmediato FROM gp.V_TPersonal_ObtieneJefePersonalFiltro";
                }
                var respuesta = _dapperRepository.QueryDapper(query, null);
                personal = JsonConvert.DeserializeObject<List<FiltroPersonalJefaturaFiltroDTO>>(respuesta);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<PersonalJefaturaDTO> ObtenerPersonalJefatura()
        {
            try
            {
                List<PersonalJefaturaDTO> personal = new List<PersonalJefaturaDTO>();
                var query = string.Empty;
                query = "SELECT IdPersonal, Personal, PuestoTrabajo, IdJefeInmediato FROM gp.V_TPersonal_ObtieneJefePersonal";
                var respuesta = _dapperRepository.QueryDapper(query, null);
                personal = JsonConvert.DeserializeObject<List<PersonalJefaturaDTO>>(respuesta);
                return personal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 10/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del personal de GP para contacto de whatsapp
        /// </summary>
        /// <returns> int </returns>
        public PersonalWhatsAppDTO ObtenerDatosPersonalPorID(int IdPersonal)
        {
            try
            {
                var queryPersonal = @"SELECT * FROM gp.V_TPersonal_DatosPersonalGP WHERE Id = @IdPersonal";
                var resultado = _dapperRepository.FirstOrDefault(queryPersonal, new { IdPersonal = IdPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null" && resultado != null)
                {
                    var rpta = JsonConvert.DeserializeObject<PersonalWhatsAppDTO>(resultado);
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Repositorio: PersonalRespository 
		/// Autor: Eliot Arias F
		/// Fecha: 26/12/2024
		/// <summary>
		/// Obtiene todos los registros de InduccionPersonal
		/// </summary>
		/// <returns>List<InduccionPersonalDTO></returns>
		public List<InduccionPersonalDTO> ObtenerReportePersonal()
        {
            try
            {
                List<InduccionPersonalDTO> registros = new List<InduccionPersonalDTO>();
                var query = @"SELECT FechaIncoorporacion,FechaRealizado, IdSede, NombreSede, IdArea, NombreArea, IdPuestoTrabajo, NombrePuestoTrabajo, IdProcesoSeleccion, NroDocumento, IdPostulante, NombrePostulante, OrdenFilaSesion, Calificacion
									FROM gp.V_ReporteInduccionPersonalCursos
									ORDER BY FechaIncoorporacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    registros = JsonConvert.DeserializeObject<List<InduccionPersonalDTO>>(resultado);
                }
                return registros;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F
        /// Fecha: 26/12/2024
        /// <summary>
        /// Obtiene todos los registros de InduccionPersonal segun Filtro 
        /// </summary>
        /// <returns>List<InduccionPersonalDTO></returns>
        public List<InduccionPersonalDTO> ObtenerReportePersonalFiltro(FiltroInduccionPersonalDTO filtro)
        {
            try
            {
                var filtros = new
                {
                    IdArea = filtro.IdArea == null ? "" : string.Join(",", filtro.IdArea.Select(x => x)),
                    IdSede = filtro.IdSede == null ? "" : string.Join(",", filtro.IdSede.Select(x => x)),
                    IdProceso = filtro.IdProceso == null ? "" : string.Join(",", filtro.IdProceso.Select(x => x)),
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin
                };
                List<InduccionPersonalDTO> PersonalExamenResultados = new List<InduccionPersonalDTO>();
                string query = "gp.SP_FiltroInduccionPersonal";
                var PersonalExamen = _dapperRepository.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(PersonalExamen) && !PersonalExamen.Contains("[]"))
                {
                    PersonalExamenResultados = JsonConvert.DeserializeObject<List<InduccionPersonalDTO>>(PersonalExamen);
                }
                return PersonalExamenResultados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRespository 
		/// Autor: Eliot Arias F
		/// Fecha: 30/12/2024
		/// <summary>
		/// Obtiene los datos para 
		/// </summary>
		/// <returns>List<></returns>
        public List<PersonalFormularioDTO> ObtenerInfoContrato(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonal_ObtenerPersonalFormulario] WHERE Id = @IdPersonal AND Estado = 1";
                string queryRespuesta = _dapperRepository.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRespository 
		/// Autor: Eliot Arias F
		/// Fecha: 30/12/2024
        /// <summary>
        /// Obtiene lista de Experiencia por personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalExperienciaFormularioDTO> ObtenerPersonalExperiencia(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalExperiencia_ObtenerPersonalExperiencia] WHERE IdPersonal = @IdPersonal AND Estado = 1 ORDER BY FechaIngreso";
                string queryRespuesta = _dapperRepository.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalExperienciaFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRespository 
		/// Autor: Eliot Arias F
		/// Fecha: 30/12/2024
        /// <summary>
		/// Obtiene lista de Formacion profesional del Personal
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <returns></returns>
		public List<PersonalFormacionFormularioDTO> ObtenerPersonalFormacion(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalFormacion_ObtenerPersonalFormacion] WHERE IdPersonal = @IdPersonal AND Estado = 1 ORDER BY FechaInicio DESC";
                string queryRespuesta = _dapperRepository.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalFormacionFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRespository 
		/// Autor: Eliot Arias F
		/// Fecha: 30/12/2024
        /// <summary>
        /// Obtiene lista de Idioma por personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<PersonalIdiomaFormularioDTO> ObtenerPersonalIdioma(int IdPersonal)
        {
            try
            {
                string query = "SELECT * from [gp].[TPersonalIdioma_ObtenerPersonalIdioma] WHERE IdPersonal = @IdPersonal AND Estado = 1";
                string queryRespuesta = _dapperRepository.QueryDapper(query, new { IdPersonal });
                return JsonConvert.DeserializeObject<List<PersonalIdiomaFormularioDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRespository 
		/// Autor: Eliot Arias F
		/// Fecha: 30/12/2024
        /// <summary>
        /// Este método obtiene la lista de contratos historicos de determinado personal
        /// </summary>
        /// <returns></returns>
        public List<ContratoHistoricoRegistroDTO> ObtenerContratoHistorico(int IdPersonal)
        {
            try
            {
                List<ContratoHistoricoRegistroDTO> listaContratoHistorico = new List<ContratoHistoricoRegistroDTO>();
                string _query = string.Empty;
                _query = "SELECT * from [gp].[V_TDatoContratoPersonal_ObtenerHistorico] WHERE IdPersonal = @IdPersonal AND Estado = 1 ORDER BY FechaFin DESC";
                var ContratoDB = _dapperRepository.QueryDapper(_query, new { IdPersonal });
                if (!string.IsNullOrEmpty(ContratoDB) && !ContratoDB.Contains("[]"))
                {
                    listaContratoHistorico = JsonConvert.DeserializeObject<List<ContratoHistoricoRegistroDTO>>(ContratoDB);
                }
                return listaContratoHistorico;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRespository 
		/// Autor: Eliot Arias F
        /// <summary>
        /// Obtiene el Id y Nombre del Personal a trav�s del nombre del personal.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PersonalAutocompleteDTO> CargarPersonalAutoCompleteContrato(string nombre)
        {
            try
            {
                string query = "SELECT Id, Nombre from gp.V_TPersonal_NombreCompleto WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1 ORDER By Nombre ASC";
                string queryRespuesta = _dapperRepository.QueryDapper(query, new { nombre });
                return JsonConvert.DeserializeObject<List<PersonalAutocompleteDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PersonalRespository 
		/// Autor: Eliot Arias F
        /// <summary>
        /// Obtiene el tipo de personal por el idPersonal
        /// </summary>
        /// <param name="IdPersonal"></param>
        /// <returns>bool</returns>
        public string EsPersonalCoordinador(int IdPersonal)
        {
            try
            {
                NombreDTO tipoPersonal = new NombreDTO();
                string _query = @"SELECT TOP 1 TipoPersonal AS Nombre FROM gp.T_Personal WHERE Id = @idPersonal";
                var queryTipoPersonal = _dapperRepository.FirstOrDefault(_query, new { idPersonal = IdPersonal });
                var TipoPersonal = JsonConvert.DeserializeObject<NombreDTO>(queryTipoPersonal);
                return TipoPersonal.Nombre;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener el tipo de Personal \n" + ex.Message);
            }
        }

        //victor hinojosa






        /////Repositorio: PersonalRepositorio
        /////Autor: Edgar S.
        /////Fecha: 25/01/2021
        ///// <summary>
        ///// Obtiene email de Personal Repetido y desactivado
        ///// </summary>        
        ///// <param name="email"> Email del Personal </param>
        ///// <returns> Registro email de Personal Repetido y desactivado : Dictionary<string, int> </returns>
        //public int? ObtenerPersonalEliminadoEmailRepetido(string email)
        //{
        //    try
        //    {
        //        Dictionary<string, int> personal = new Dictionary<string, int>();
        //        var query = "SELECT Id FROM gp.T_Personal where estado = 0 and Email=@Email";
        //        var personalDB = _dapperRepository.QueryDapper(query, new { Email = email });
        //        if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
        //        {
        //            personal = JsonConvert.DeserializeObject<Dictionary<string, int>>(personalDB);
        //        }
        //        return personal.Select(x => x.Value).FirstOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        /////Repositorio: PersonalRepositorio
        /////Autor: Britsel C., Luis H., Edgar S.
        /////Fecha: 25/01/2021
        ///// <summary>
        ///// Activa personal por Id
        ///// </summary>        
        ///// <param name="id"> Id del Personal </param>
        ///// <returns> Confirmación de Activación de personal </returns>
        ///// <returns> Bool </returns>
        //public bool ActivarPersonal(int id)
        //{
        //    try
        //    {
        //        _dapperRepository.QueryDapper("UPDATE gp.T_Personal set Estado=1 where Id=@Id", new { id = id });
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        public List<PersonalDTO> ObtenerValidacionAnexo(int id, string anexo)
        {
            try
            {
                List<PersonalDTO> ValidacionAnexo = new List<PersonalDTO>();
                string queryValidacionAnexo = "SELECT id, anexo FROM gp.V_TPersonal_ObtenerActivos WHERE Id !=@id AND Anexo = @Anexo";
                var resultado = _dapperRepository.QueryDapper(queryValidacionAnexo, new { Id = id, Anexo = anexo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ValidacionAnexo = JsonConvert.DeserializeObject<List<PersonalDTO>>(resultado);
                }
                return ValidacionAnexo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

    }
}