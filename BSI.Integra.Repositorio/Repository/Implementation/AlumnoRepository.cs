using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AlumnoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Alumno
    /// </summary>
    public class AlumnoRepository : GenericRepository<TAlumno>, IAlumnoRepository
    {
        private Mapper _mapper;

        public AlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAlumno, Alumno>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAlumno MapeoEntidad(Alumno entidad)
        {
            try
            {
                //crea la entidad padre
                TAlumno modelo = _mapper.Map<TAlumno>(entidad);

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

        public TAlumno Add(Alumno entidad)
        {
            try
            {
                var Alumno = MapeoEntidad(entidad);
                base.Insert(Alumno);
                return Alumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAlumno Update(Alumno entidad)
        {
            try
            {
                var Alumno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Alumno.RowVersion = entidadExistente.RowVersion;

                base.Update(Alumno);
                return Alumno;
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


        public IEnumerable<TAlumno> Add(IEnumerable<Alumno> listadoEntidad)
        {
            try
            {
                List<TAlumno> listado = new List<TAlumno>();
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

        public IEnumerable<TAlumno> Update(IEnumerable<Alumno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAlumno> listado = new List<TAlumno>();
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
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Alumno.
        /// </summary>
        /// <returns> List<AlumnoDTO> </returns>
        public IEnumerable<AlumnoDTO> ObtenerAlumno()
        {
            try
            {
                List<AlumnoDTO> rpta = new List<AlumnoDTO>();
                var query = @"
                    SELECT Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,DNI,Direccion,FechaNacimiento,Pais,Ciudad,Telefono,Celular,
	                    Email1,Email2,NivelFormacion,Profesion,Empresa,EstadoCivil,TelefonoFamiliar,NombreFamiliar,Parentesco,TelefonoTrabajo,
	                    TelefonoTrabajoAnexo,Genero,Skype,Fax,IdPais,UbigeoPais,UbigeoDepartamento,UbigeoProvincia,UbigeoCiudad,
	                    UbigeoDistrito,DireccionCalle,DireccionAv,DireccionZona,DireccionComp,DireccionTorre,DireccionEdificio,
	                    DireccionDpto,DireccionUrb,DireccionMz,DireccionLt,ReferenciaDetallada,HoraMaxima,Puesto,AniversarioBodas,NroHijo,
	                    ValidacionTelefonica,FaseContacto,IdCargo,Cargo,IdAFormacion,AFormacion,IdATrabajo,ATrabajo,IdIndustria,Industria,
	                    IdReferido,Referido,IdCodigoPais,NombrePais,IdCiudad,NombreCiudad,HoraContacto,HoraPeru,IdCodigoRegionCiudad,Telefono2,
	                    Celular2,IdEmpresa,IdOportunidad_Inicial,UsClave,IdTipoDocumento,NroDocumento,DescripcionCargo,Asociado,DeSuscrito,
	                    UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,NroOportunidades,EsPersonaValida,
	                    EsEliminadoPorRegularizacion,TieneOportunidad,TieneMatricula,EsRepetido,IdEstadoContactoWhatsApp,IdEstadoContactoMailing,
	                    DireccionEnvioCertificado,UsarNuevaDireccionParaEnvio,CiudadEnvioCertificado,IdEstadoContactoWhatsApp_Secundario,CodigoPortal,
	                    IdNumeroTipoDocumento,IdGenero,Municipio,IdMunicipioMexico,EstadoLugar,CodigoPostal,Colonia,IdAsentamientoMexico,IdCiudadMexico,Curp,Rfc
                    FROM mkt.V_TAlumno_Obtener
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
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
        /// Obtiene toda la informacion de T_Alumno asociado a un Id.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoDTO </returns>
        public Alumno? ObtenerPorId(int idAlumno)
        {
            try
            {
                var query = @"
                    SELECT Id,
                        Nombre1,
                        Nombre2,
                        ApellidoPaterno,
                        ApellidoMaterno,
                        COALESCE(DNI, NroDocumento) AS Dni,
                        Direccion,
                        FechaNacimiento,
                        Pais,
                        Ciudad,
                        Telefono,
                        Celular,
                        Email1,
                        Email2,
                        NivelFormacion,
                        Profesion,
                        Empresa,
                        EstadoCivil,
                        TelefonoFamiliar,
                        NombreFamiliar,
                        Parentesco,
                        TelefonoTrabajo,
                        TelefonoTrabajoAnexo,
                        Genero,
                        Skype,
                        Fax,
                        IdPais,
                        UbigeoPais,
                        UbigeoDepartamento,
                        UbigeoProvincia,
                        UbigeoCiudad,
                        UbigeoDistrito,
                        DireccionCalle,
                        DireccionAv,
                        DireccionZona,
                        DireccionComp,
                        DireccionTorre,
                        DireccionEdificio,
                        DireccionDpto,
                        DireccionUrb,
                        DireccionMz,
                        DireccionLt,
                        ReferenciaDetallada,
                        HoraMaxima,
                        Puesto,
                        AniversarioBodas,
                        NroHijo,
                        ValidacionTelefonica,
                        FaseContacto,
                        IdCargo,
                        Cargo,
                        IdAFormacion,
                        AFormacion,
                        IdATrabajo,
                        ATrabajo,
                        IdIndustria,
                        Industria,
                        IdReferido,
                        Referido,
                        IdCodigoPais,
                        NombrePais,
                        IdCiudad,
                        NombreCiudad,
                        HoraContacto,
                        HoraPeru,
                        IdCodigoRegionCiudad,
                        Telefono2,
                        Celular2,
                        IdEmpresa,
                        IdOportunidad_Inicial as  IdOportunidadInicial,
                        UsClave,
                        IdTipoDocumento,
                        NroDocumento,
                        DescripcionCargo,
                        Asociado,
                        DeSuscrito,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        NroOportunidades,
                        EsPersonaValida,
                        EsEliminadoPorRegularizacion,
                        TieneOportunidad,
                        TieneMatricula,
                        EsRepetido,
                        IdEstadoContactoWhatsApp,
                        IdEstadoContactoMailing,
                        DireccionEnvioCertificado,
                        UsarNuevaDireccionParaEnvio,
                        CiudadEnvioCertificado,
                        IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
                        CodigoPortal,
                        IdNumeroTipoDocumento,
                        IdGenero,
                        Comentario,
                        Municipio,
                        IdMunicipioMexico,
                        EstadoLugar,
                        CodigoPostal,
                        Colonia,
                        IdAsentamientoMexico,
                        IdCiudadMexico,
                        Curp,
                        Rfc,
                        PrincipalResponsabilidadProfesional,
                        IdExperiencia,
                        IdTamanioEmpresaAgenda
                    FROM mkt.V_TAlumno_Obtener
                    WHERE Estado = 1 AND Id = @idAlumno";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Alumno>(resultado)!;
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
        /// Obtiene el Email Principal por IdAlumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoDTO </returns>
        public StringDTO ObtenerEmailPrincipalPorId(int idAlumno)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"
                    SELECT 
                        Email1 AS Valor
                    FROM mkt.V_TAlumno_Obtener
                    WHERE Estado = 1 AND Id = @idAlumno";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StringDTO RegistrarLoginPortal(int idAlumno, string usuario)
        {
            try
            {
                List<StringDTO> rpta = new List<StringDTO>();
                var query = "ope.SP_TLoginPortalWeb_Insertar";
                var resultado = _dapperRepository.QuerySPDapper(query, new { idAlumno, user = usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<StringDTO>>(resultado)!;
                }
                if (rpta.Count == 0)
                    throw new Exception("No se pudo registrar un login");

                return rpta[0];
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
        /// Obtiene el Celular del Alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoDTO </returns>
        public StringDTO ObtenerCelularPrincipalPorId(int idAlumno)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"
                    SELECT Id,
                        Celular AS Valor
                    FROM mkt.V_TAlumno_Obtener
                    WHERE Estado = 1 AND Id = @idAlumno";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
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
        /// Obtiene toda la informacion de T_Alumno asociado a un Id.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoDTO </returns>
        /// 
        /// Modificación
        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 24/04/2024
        /// Se incluyeron los nuevos campos 'EstadoLugar', 'CodigoPostal', 'Colonia' en el query.
        public async Task<Alumno> ObtenerPorIdAsync(int idAlumno)
        {
            try
            {
                Alumno rpta = new Alumno();
                var query = @"
                    SELECT Id,
                        Nombre1,
                        Nombre2,
                        ApellidoPaterno,
                        ApellidoMaterno,
                        DNI AS Dni,
                        Direccion,
                        FechaNacimiento,
                        Pais,
                        Ciudad,
                        Telefono,
                        Celular,
                        Email1,
                        Email2,
                        NivelFormacion,
                        Profesion,
                        Empresa,
                        EstadoCivil,
                        TelefonoFamiliar,
                        NombreFamiliar,
                        Parentesco,
                        TelefonoTrabajo,
                        TelefonoTrabajoAnexo,
                        Genero,
                        Skype,
                        Fax,
                        IdPais,
                        UbigeoPais,
                        UbigeoDepartamento,
                        UbigeoProvincia,
                        UbigeoCiudad,
                        UbigeoDistrito,
                        DireccionCalle,
                        DireccionAv,
                        DireccionZona,
                        DireccionComp,
                        DireccionTorre,
                        DireccionEdificio,
                        DireccionDpto,
                        DireccionUrb,
                        DireccionMz,
                        DireccionLt,
                        ReferenciaDetallada,
                        HoraMaxima,
                        Puesto,
                        AniversarioBodas,
                        NroHijo,
                        ValidacionTelefonica,
                        FaseContacto,
                        IdCargo,
                        Cargo,
                        IdAFormacion,
                        AFormacion,
                        IdATrabajo,
                        ATrabajo,
                        IdIndustria,
                        Industria,
                        IdReferido,
                        Referido,
                        IdCodigoPais,
                        NombrePais,
                        IdCiudad,
                        NombreCiudad,
                        HoraContacto,
                        HoraPeru,
                        IdCodigoRegionCiudad,
                        Telefono2,
                        Celular2,
                        IdEmpresa,
                        IdOportunidad_Inicial as  IdOportunidadInicial,
                        UsClave,
                        IdTipoDocumento,
                        NroDocumento,
                        DescripcionCargo,
                        Asociado,
                        DeSuscrito,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        NroOportunidades,
                        EsPersonaValida,
                        EsEliminadoPorRegularizacion,
                        TieneOportunidad,
                        TieneMatricula,
                        EsRepetido,
                        IdEstadoContactoWhatsApp,
                        IdEstadoContactoMailing,
                        DireccionEnvioCertificado,
                        UsarNuevaDireccionParaEnvio,
                        CiudadEnvioCertificado,
                        IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
                        CodigoPortal,
                        IdNumeroTipoDocumento,
                        IdGenero,
                        Comentario,
                        Municipio,
                        IdMunicipioMexico,
                        EstadoLugar,
                        CodigoPostal,
                        Colonia,
                        IdAsentamientoMexico,
                        IdCiudadMexico,
                        Curp,
                        Rfc
                    FROM mkt.V_TAlumno_Obtener
                    WHERE Estado = 1 AND Id = @idAlumno";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Alumno>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Alumno para mostrarse en combo.
        /// </summary>
        /// <returns> List<AlumnoComboDTO> </returns>
        public IEnumerable<AlumnoComboDTO> ObtenerCombo()
        {
            try
            {
                List<AlumnoComboDTO> rpta = new List<AlumnoComboDTO>();
                var query = @"
                    SELECT TOP 100 Id,NombreCompleto
                    FROM mkt.V_TAlumno_NombreCompleto
                    WHERE Estado = 1
                    ORDER By NombreCompleto ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AlumnoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de Alumnos basado en un Nombre Parcial.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Alumno</param>
        /// <returns> List<AlumnoComboDTO> </returns>
        public IEnumerable<AlumnoComboDTO> ObtenerAutocomplete(string nombreParcial)
        {
            try
            {
                List<AlumnoComboDTO> alumnos = new List<AlumnoComboDTO>();
                var query = @"
                    SELECT Id, NombreCompleto
                    FROM mkt.V_TAlumno_NombreCompleto
                    WHERE NombreCompleto LIKE CONCAT('%',@nombreParcial,'%') AND Estado = 1
                    ORDER By NombreCompleto ASC";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { nombreParcial });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    alumnos = JsonConvert.DeserializeObject<List<AlumnoComboDTO>>(resultadoQuery);
                }
                return alumnos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de Alumnos basado en un Nombre Parcial.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Alumno</param>
        /// <returns> List<AlumnoComboDTO> </returns>
        public IEnumerable<AlumnoComboDTO> ObtenerAlumnoMatriculadoAutocomplete(string nombreParcial)
        {
            try
            {
                List<AlumnoComboDTO> alumnos = new List<AlumnoComboDTO>();
                var query = @"
                    SELECT Id, NombreCompleto
                    FROM ope.V_NombreCompletoAlumnoMatriculado
                    WHERE NombreCompleto LIKE CONCAT('%',@nombreParcial,'%') AND Estado = 1
                    ORDER By NombreCompleto ASC";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { nombreParcial });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    alumnos = JsonConvert.DeserializeObject<List<AlumnoComboDTO>>(resultadoQuery);
                }
                return alumnos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdPais de un Alumno basado en el CodigoPais
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> IntDTO </returns>
        public IntDTO ObtenerIdPaisPorIdAlumno(int idAlumno)
        {
            try
            {
                IntDTO idPais = new IntDTO();
                var resultadoStoreProcedure = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ObtenerIdPaisPorIdAlumno", new { idAlumno });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    idPais = JsonConvert.DeserializeObject<IntDTO>(resultadoStoreProcedure);
                }
                return idPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdPais de un Alumno basado en el CodigoPais
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> IntDTO </returns>
        public async Task<IntDTO> ObtenerIdPaisPorIdAlumnoAsync(int idAlumno)
        {
            try
            {
                IntDTO idPais = new IntDTO();
                var resultadoStoreProcedure = await _dapperRepository.QuerySPFirstOrDefaultAsync("mkt.SP_ObtenerIdPaisPorIdAlumno", new { idAlumno });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    idPais = JsonConvert.DeserializeObject<IntDTO>(resultadoStoreProcedure);
                }
                return idPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener IdCiudad y IdPais de un Alumno segun su Id
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoCiudadPaisDTO </returns>
        public AlumnoCiudadPaisDTO ObtenerCiudadPaisPorIdAlumno(int idAlumno)
        {
            try
            {
                AlumnoCiudadPaisDTO ubicacion = new AlumnoCiudadPaisDTO();
                var query = @"SELECT 
                                    IdCiudad, IdCodigoPais 
                              FROM 
                                    mkt.V_TAlumno_Obtener 
                              WHERE 
                                    Estado = 1 AND Id = @IdAlumno";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    ubicacion = JsonConvert.DeserializeObject<AlumnoCiudadPaisDTO>(resultadoQuery)!;
                }
                return ubicacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Datos de Alumno para Documento
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoDatosDocumentoDTO </returns>
        public AlumnoDatosDocumentoDTO ObtenerDatosDocumentoPorIdAlumno(int idAlumno)
        {
            try
            {
                AlumnoDatosDocumentoDTO datosDocumento = new AlumnoDatosDocumentoDTO();
                var query = @"
                    SELECT Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Direccion,DNI,Celular,Telefono,IdCiudad,NombreCiudad,
	                    IdCodigoPais,NombrePais,Correo
                    FROM mkt.V_TAlumno_DatosAlumnoParaDocumento
                    WHERE Estado = 1 AND Id = @idAlumno";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    datosDocumento = JsonConvert.DeserializeObject<AlumnoDatosDocumentoDTO>(resultadoQuery);
                }
                return datosDocumento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion del Alumno asociados a una Clasificacion Persona
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <returns> AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                AlumnoInformacionDTO informacionAlumno = new AlumnoInformacionDTO();
                var query = @"
                    SELECT Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,DNI,Direccion,FechaNacimiento,Telefono,Celular,Email1,Email2,
	                    Genero,Parentesco,NombreFamiliar,TelefonoFamiliar,Empresa,IdCargo,Cargo,IdAFormacion,AFormacion,IdATrabajo,ATrabajo,
	                    IdIndustria,Industria,IdReferido,Referido,IdCodigoPais,NombrePais,IdCiudad,NombreCiudad,HoraContacto,HoraPeru,
	                    Telefono2,Celular2,IdEmpresa,IdEstadoContactoWhatsApp,IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
	                    IdOportunidad_Inicial,IdTipoDocumento,NroDocumento,DescripcionCargo,Asociado, RutaBandera,Municipio,IdMunicipioMexico,IdAsentamientoMexico,IdCiudadMexico,Colonia,EstadoLugar,CodigoPostal,Curp,Rfc,
                        PrincipalResponsabilidadProfesional,IdTiempoExperiencia,TiempoExperiencia,IdTamanioEmpresa,TamanioEmpresa
                    FROM com.V_InformacionAlumno
                    WHERE IdClasificacionPersona = @idClasificacionPersona AND Estado = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    informacionAlumno = JsonConvert.DeserializeObject<AlumnoInformacionDTO>(resultadoQuery);
                }
                return informacionAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion del Alumno asociados a una Clasificacion Persona
        /// </summary>
        /// <param name="idAlumno">Id de Clasificacion Persona</param>
        /// <returns> AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdAlumno(int idAlumno)
        {
            try
            {
                AlumnoInformacionDTO informacionAlumno = new AlumnoInformacionDTO();
                var query = @"
                    SELECT Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,DNI,Direccion,FechaNacimiento,Telefono,Celular,Email1,Email2,
	                    Genero,Parentesco,NombreFamiliar,TelefonoFamiliar,Empresa,IdCargo,Cargo,IdAFormacion,AFormacion,IdATrabajo,ATrabajo,
	                    IdIndustria,Industria,IdReferido,Referido,IdCodigoPais,NombrePais,IdCiudad,NombreCiudad,HoraContacto,HoraPeru,
	                    Telefono2,Celular2,IdEmpresa,IdEstadoContactoWhatsApp,IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
	                    IdOportunidad_Inicial,IdTipoDocumento,NroDocumento,DescripcionCargo,Asociado, RutaBandera
                    FROM com.V_InformacionAlumno
                    WHERE Id = @idAlumno AND Estado = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    informacionAlumno = JsonConvert.DeserializeObject<AlumnoInformacionDTO>(resultadoQuery);
                }
                return informacionAlumno;
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
        /// Obtiene Datos del Alumno asociados a un numero de celular
        /// </summary>
        /// <param name="celular">Celular del Alumno</param>
        /// <returns> AlumnoPorCelularDTO </returns>
        public AlumnoPorCelularDTO? ObtenerAlumnoPorCelular(string celular)
        {
            try
            {
                var query = @"
                    SELECT Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Celular,Email1
                    FROM mkt.V_TAlumno_Obtener
                    WHERE Estado = 1 AND Celular = @celular";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { celular });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    return JsonConvert.DeserializeObject<AlumnoPorCelularDTO>(resultadoQuery)!;
                }
                return null;
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
        /// Obtiene el Area de Ventas asociado a un Envio Masivo donde se incluyo al Alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO? ObtenerEnvioMasivoSMSPorIdAlumno(int idAlumno)
        {
            try
            {
                var query = @"
                    SELECT AreaVentas AS Valor
                    FROM mkt.T_AlumnoCuponRegistro
                    WHERE IdPersonal = 4363
	                    AND AreaVentas = '(A) Gestión Ambiental'
	                    AND Estado = 1
	                    AND IdAlumno = @IdAlumno";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultadoQuery)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la ciudad de origen del alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> string </returns>
        public string ObtenerCiudadOrigen(int idAlumno)
        {
            try
            {
                var _resultado = new StringDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ObtenerNombreCiudadOrigenAlumno", new { idAlumno });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<StringDTO>(resultado);
                    return _resultado.Valor;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la ciudad de origen del alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> string </returns>
        public async Task<string> ObtenerCiudadOrigenAsync(int idAlumno)
        {
            try
            {
                var _resultado = new StringDTO();
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync("mkt.SP_ObtenerNombreCiudadOrigenAlumno", new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<StringDTO>(resultado);
                    return _resultado.Valor;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el pais de origen del alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> string </returns>
        public string ObtenerPaisOrigen(int idAlumno)
        {
            try
            {
                var _resultado = new StringDTO();
                var query = $@"mkt.SP_ObtenerNombrePaisOrigenAlumno";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { idAlumno });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el pais de origen del alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> string </returns>
        public async Task<string> ObtenerPaisOrigenAsync(int idAlumno)
        {
            try
            {
                var _resultado = new StringDTO();
                var query = $@"mkt.SP_ObtenerNombrePaisOrigenAlumno";
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync(query, new { idAlumno });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene si ya se ha enviado en el mismo dia un mensaje
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="fecha">Fecha para Busqueda</param>
        /// <returns> EnvioSMSOportunidad </returns>
        public EnvioSMSOportunidad Obtener_EnvioSMSPorDiaOportunidad(int idOportunidad, DateTime fecha)
        {
            try
            {
                string queryAlumno = @"
                    SELECT Id, IdOportunidad
                    FROM mkt.T_EnvioSMSOportunidad WITH (NOLOCK)
                    WHERE idOportunidad = @IdOportunidad AND CONVERT(DATE, Fecha) = CONVERT(DATE, @fecha);";
                var envio = _dapperRepository.FirstOrDefault(queryAlumno, new { idOportunidad, fecha });

                return JsonConvert.DeserializeObject<EnvioSMSOportunidad>(envio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta el envio de SMS por oportunidad y dia en la tabla mkt.T_SmsMensajeEnviado
        /// </summary>
        /// <param name="celular">Celular al que se envia el mensaje</param>
        /// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="mensaje">Mensaje a enviar</param>
        /// <param name="parteMensaje">Parte del mensaje seccionado</param>
        /// <param name="idPais">Id del pais (PK de la tabla gp.T_Pais)</param>
        /// <returns> bool </returns>
        public bool InsertaSMSOportunidadUsuario(string celular, int idPersonal, int idAlumno, string mensaje, int parteMensaje, int idPais, string usuario)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarMensajeEnviado",
                    new
                    {
                        celular,
                        idPersonal,
                        idAlumno,
                        mensaje,
                        parteMensaje,
                        idPais,
                        usuario
                    });
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta el envio de SMS por oportunidad y dia
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="fechaEnvio">Fecha en Envio del Mensaje</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO InsertaSMSOportunidad(int idOportunidad, DateTime fechaEnvio)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_insertarSMSOportunidad", new { idOportunidad, fechaEnvio });

                if (!string.IsNullOrEmpty(resultado))
                {
                    var resultadoDB = JsonConvert.DeserializeObject<JToken>(resultado);
                    _resultado.Valor = Convert.ToInt32(resultadoDB["Resultado"]);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Validar Email 1 - Alumno
        /// </summary>
        /// <param name="email">Email a Validar</param>
        /// <returns> AlumnoEmailDTO </returns>
        public AlumnoEmailDTO? ValidarEmail1Alumno(string email)
        {
            try
            {
                var validacion = new AlumnoEmailDTO();
                string queryAlumno = @"
                    SELECT Id, Email1, Email2
                    FROM mkt.V_TAlumno_ValidarEmail
                    WHERE Email1 = @email";
                var resultado = _dapperRepository.FirstOrDefault(queryAlumno, new { email });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<AlumnoEmailDTO>(resultado);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Validar Email 1 - Alumno
        /// </summary>
        /// <param name="email">Email a Validar</param>
        /// <returns> AlumnoEmailDTO </returns>
        public AlumnoEmailPrincipalDTO ValidarEmailPrincipal(string email)
        {
            try
            {
                string queryAlumno = @"
                    SELECT TOP 1 Id,
	                    Email1,
	                    Email2,
	                    Estado,
	                    IdClasificacionPersona,
	                    EstadoCP,
	                    IdPersona,
	                    EstadoPer
                    FROM [mkt].[V_AlumnoValidarEmailPrincipal]
                    WHERE Email1 = @email ORDER BY Estado DESC, EstadoCP DESC, EstadoPer DESC";
                var resultado = _dapperRepository.FirstOrDefault(queryAlumno, new { email });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<AlumnoEmailPrincipalDTO>(resultado)!;
                return new AlumnoEmailPrincipalDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ValidarEmailPrincipal: {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Validar Email 1 - Alumno
        /// </summary>
        /// <param name="email">Email a Validar</param>
        /// <returns> AlumnoEmailDTO </returns>
        public AlumnoEmailPrincipalDTO ValidarEmailSecundario(string email)
        {
            try
            {
                string queryAlumno = @"
                    SELECT TOP 1 Id,
	                    Email1,
	                    Email2,
	                    Estado,
	                    IdClasificacionPersona,
	                    EstadoCP,
	                    IdPersona,
	                    EstadoPer
                    FROM [mkt].[V_AlumnoValidarEmailPrincipal]
                    WHERE Email2 = @email ORDER BY Estado DESC, EstadoCP DESC, EstadoPer";
                var resultado = _dapperRepository.FirstOrDefault(queryAlumno, new { email });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<AlumnoEmailPrincipalDTO>(resultado)!;
                return new AlumnoEmailPrincipalDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ValidarEmailSecundario: {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Validar Email 2 - Alumno
        /// </summary>
        /// <param name="email">Email a Validar</param>
        /// <returns> AlumnoEmailDTO </returns>
        public AlumnoEmailDTO ValidarEmail2Alumno(string email)
        {
            try
            {
                var validacion = new AlumnoEmailDTO();
                string queryAlumno = @"
                    SELECT Id,Email1,Email2
                    FROM mkt.V_TAlumno_ValidarEmail
                    WHERE Email2 = @email AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(queryAlumno, new { email });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    validacion = JsonConvert.DeserializeObject<AlumnoEmailDTO>(resultado);
                }
                return validacion;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 07/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene cupon por IdAlumno  
        /// </summary>
        /// <param name="idAlumno">Id de alumno</param>
        /// <returns>ObjetoDTO: AlumnoCuponDTO</returns>
        public AlumnoCuponDTO ObtenerCuponPorIdAlumno(int idAlumno)
        {
            try
            {
                string queryAlumno = "SELECT Id,IdAlumno,CodigoCupon FROM mkt.T_AlumnoCuponRegistro WHERE IdAlumno=@IdAlumno";
                var cupon = _dapperRepository.FirstOrDefault(queryAlumno, new { IdAlumno = idAlumno });
                return JsonConvert.DeserializeObject<AlumnoCuponDTO>(cupon);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id de SolicitudVisualizacionOportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idPersonal"></param>
        /// <returns>ValorIntDTO</returns>
        public ValorIntDTO InsertarSolicitudVisualizarDatosOportunidad(int idOportunidad, int idPersonal)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"com.SP_InsertarVisualizacionOportunidad";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { idOportunidad, idPersonal });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de usuarios con un mismo Email1 o Email2
        /// </summary>
        /// <param name="email1"></param>
        /// <param name="email2"></param>
        /// <returns> List<AlumnoEmailDTO> </returns>
        public List<AlumnoEmailDTO> ObtenerAlumnoPorEmail(string email1, string email2)
        {
            try
            {
                string _queryAlumno = "com.SP_ExisteContacto";
                var queryAlumno = _dapperRepository.QuerySPDapper(_queryAlumno, new { Email1 = email1, Email2 = email2 });
                List<AlumnoEmailDTO> listaAlumno = JsonConvert.DeserializeObject<List<AlumnoEmailDTO>>(queryAlumno);
                return listaAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el correo del Alumno a  traves del Id
        /// </summary>
        /// <param name="idAlumno"> Id del alumno </param>
        /// <returns> AlumnoEmailDTO </returns>
        public AlumnoEmailDTO? ObtenerEmailAlumno(int idAlumno)
        {
            try
            {
                string query = "SELECT Id, Email1, Email2 FROM mkt.V_TAlumno_Obtener WHERE Id = @IdAlumno";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<AlumnoEmailDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string ObtenerEmail(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).FirstOrDefault().Email1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public string ObtenerEmail(int id)
        //{
        //    try
        //    {
        //        return this.GetBy(x => x.Id == id).FirstOrDefault().Email1;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        /// Autor: Jonathan Caipo
        /// Fecha: 11/10/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si existe el alumno con email1 o email2
        /// </summary>
        /// <param name="email1">Correo principal del alumno a buscar</param>
        /// <param name="email2">Correo secundario del alumno a buscar</param>
        /// <param name="id">Id del contacto</param>
        /// <returns>Booleano</returns>
        public bool ExisteContacto(string email1, string email2, int id = 0)
        {
            try
            {
                bool existe = true;
                var alumnos = this.ObtenerAlumnoPorEmail(email1, email2).ToList();
                if (alumnos.Count() == 0)
                {
                    existe = false;
                }
                else if (alumnos.Count() == 1)
                {
                    // Si es el registro que se esta editando, retorna false por que no existe duplicados, si podria admitirlo en cualquiera: email 1 o email2
                    existe = !(alumnos.FirstOrDefault().Id == id);
                }
                else
                {
                    //Verificar el caso, que hay varias filas, pero en el row que se esta editando se quiere pasar el email2 y duplicarlo en email1
                    bool CumpleCondiciones = false;
                    var alumnoDB = ObtenerEmailAlumno(id);
                    foreach (var alumno in alumnos)
                    {
                        if (id == alumno.Id && string.IsNullOrEmpty(alumnoDB.Email1) && alumno.Email2.Equals(email1))
                        {
                            CumpleCondiciones = true;
                        }
                    }
                    if (CumpleCondiciones)
                    {
                        existe = false;
                    }
                }
                return existe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre del programa general del ultimo envio masivo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerNombreProgramaGeneralUltimoEnvioMasivo(int id)
        {
            try
            {
                var respuesta = new StringDTO();
                var query = $@"mkt.SP_ObtenerNombreProgramaGeneralUltimoEnvioMasivo";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return respuesta.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre del programa general de la ultima solicitud de información
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(int id)
        {
            try
            {
                var respuesta = new StringDTO();
                var query = $@"mkt.SP_ObtenerNombreProgramaGeneralUltimaSolicitudInformacion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return respuesta.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Alumno por número de celular y número de celular alterno
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="numeroAlterno"></param>
        /// <returns></returns>
        /// <exception></exception>
        public AlumnoPorCelularDTO ObtenerPorCelular(string numero, string numeroAlterno)
        {
            try
            {
                string queryAlumno = @"SELECT Id, Nombre1, Nombre2, ApellidoMaterno, ApellidoPaterno, Email1 FROM mkt.V_TAlumno_Obtener   WITH (NOLOCK) 
                                       WHERE Celular LIKE '%'+@numero+'%' OR Celular LIKE '%'+@numeroAlterno+'%'";
                var resultado = _dapperRepository.FirstOrDefault(queryAlumno, new { numero = numero, numeroAlterno = numeroAlterno });
                return JsonConvert.DeserializeObject<AlumnoPorCelularDTO>(resultado)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 15/10/2022
        /// Version: 1.0
        /// <summary>
        /// ELimina de forma fisica de la base de datos de Alumno
        /// </summary>
        /// <returns> true o false </returns>
        public bool EliminarFisicaAlumno(string NombreTablaV3, string NombreTablaV4, int IdV4, string Idv3, int? Idv3Int)
        {
            try
            {
                bool expositor = new bool();
                string queryExpositor = _dapperRepository.QuerySPDapper("conf.SP_EliminarRegistroTablaMaestro", new { NombreTablaV3, NombreTablaV4, IdV4, Idv3, Idv3Int = Idv3Int == 0 ? null : Idv3Int });
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
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de inicio de capacitacion
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de inicio de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaInicioCapacitacion(int idMatriculaCabecera)
        {
            try
            {
                var fechaInicio = new StringDTO();
                string query = "SELECT FechaInicio AS Valor FROM pla.V_FechaInicioFinCapacitacion Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    fechaInicio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return fechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de fin de capacitacion
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de fin de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaFinCapacitacion(int idMatriculaCabecera)
        {
            try
            {
                var fechaInicio = new StringDTO();
                string query = "SELECT FechaFin AS Valor FROM pla.V_FechaInicioFinCapacitacion Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    fechaInicio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return fechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la nota promedio del alumno coincidente con la matricula cabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Es el Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabeceraa)</param>
        /// <returns>Cadena con nota promedio del alumno</returns>
        public string ObtenerNotaPromedio(int idMatriculaCabecera)
        {
            try
            {
                var notaPromedio = new StringDTO();
                string query = "SELECT Nota AS Valor FROM ope.V_Alumno_NotaPromedio Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    notaPromedio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return notaPromedio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene fecha de emision
        /// </summary>
        /// <returns>Cadena con la fecha de emision formateada en texto comprensible</returns>
        public string ObtenerFechaEmision()
        {
            try
            {
                var fechaEmision = new StringDTO();
                string query = "SELECT FechaEmision AS Valor FROM ope.V_ObtenerFechaEmision ";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    fechaEmision = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return fechaEmision.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene codigo del certificado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con la el codigo del certificado</returns>
        public string ObtenerCodigoCertificado(int idMatriculaCabecera)
        {
            try
            {
                var CodigoCertificado = new StringDTO();
                string query = "SELECT CodigoCertificado AS Valor FROM ope.V_ObtenerCodigoCertificado Where IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    CodigoCertificado = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return CodigoCertificado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la URL de feliz cumpleaños
        /// </summary>
        /// <returns>Cadena con la URL de la imagen de feliz cumpleaños</returns>
        public string ObtenerUrlImagenFelizCumpleanios()
        {
            try
            {
                var resultadoFinal = new StringDTO();
                var query = $@"mkt.SP_ObtenerUrlFelizCumpleanios";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener información de Id, Alumnos por nombre AutoComplete
        /// </summary>
        /// <param name="valor"> valor de búsqueda </param>
        /// <returns> Lista de Alumnos por nombre Registrados </returns>
        /// <returns> Objeto DTO: List<AlumnoFiltroAutocompleteDTO> </returns>	
        public List<AlumnoComboDTO> ObtenerTodoComboAutoComplete(string valor)
        {
            try
            {
                List<AlumnoComboDTO> alumnosAutocompleteFiltro = new List<AlumnoComboDTO>();
                string queryAlumnoFiltro = string.Empty;
                queryAlumnoFiltro = "SELECT Id,NombreCompleto FROM mkt.V_TAlumno_NombreCompleto WHERE NombreCompleto LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By NombreCompleto ASC";
                var alumnoDB = _dapperRepository.QueryDapper(queryAlumnoFiltro, new { valor });
                if (!string.IsNullOrEmpty(alumnoDB) && !alumnoDB.Contains("[]"))
                {
                    alumnosAutocompleteFiltro = JsonConvert.DeserializeObject<List<AlumnoComboDTO>>(alumnoDB)!;
                }
                return alumnosAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del alumno mediante su email
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<AlumnoComboDTO> AlumnnosTodoComboAutoCompletePorEmail(string valor)
        {
            try
            {
                List<AlumnoComboDTO> alumnosEmail = new List<AlumnoComboDTO>();
                string queryAlumno = string.Empty;
                queryAlumno = "SELECT Id, NombreCompleto FROM mkt.V_TAlumno_NombreCompletoEmail WHERE " +
                    "(ltrim(rtrim(Email1)) like '%'+ltrim(rtrim(@val))+'%' or ltrim(rtrim(Email2)) like '%'+ltrim(rtrim(@val))+'%') and Estado='1'";
                var alumno = _dapperRepository.QueryDapper(queryAlumno, new { val = valor });
                if (!string.IsNullOrEmpty(alumno) && !alumno.Contains("[]"))
                {
                    alumnosEmail = JsonConvert.DeserializeObject<List<AlumnoComboDTO>>(alumno)!;
                }
                return alumnosEmail;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del alumno para messenger chat mediante IdAlumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> AlumnoInformacionMessengerDTO </returns>
        public AlumnoInformacionMessengerDTO ObtenerAlumnoInformacionMessengerChatPorId(int idAlumno)
        {
            try
            {
                string query = string.Empty;
                query = @"Select Id, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno,
                                    Direccion, Telefono, Celular, Email1, Email2, IdReferido, IdCodigoPais, IdCiudad, HoraContacto,
                                    HoraPeru, IdCargo, IdAFormacion, IdATrabajo, IdIndustria, IdEmpresa, Asociado 
                        FROM com.V_DatosAlumno_MessengerChat 
                        WHERE Estado = 1 and Id=@Id";
                var alumno = _dapperRepository.FirstOrDefault(query, new { Id = idAlumno });
                if (alumno == "null" || string.IsNullOrEmpty(alumno)) { throw new Exception("No existe alumno con ese Id."); }
                else
                {
                    return JsonConvert.DeserializeObject<AlumnoInformacionMessengerDTO>(alumno);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y NombreCompleto del Alumno mediante el IdReferido
        /// </summary>
        /// <param name="idR"></param>
        /// <returns></returns>
        public List<AlumnoComboDTO> ObtenerTodoFiltroAutoCompleteReferido(int idR)
        {
            try
            {
                List<AlumnoComboDTO> alumnoAutocompleteFiltro = new List<AlumnoComboDTO>();
                string queryAlumnoFiltro = string.Empty;
                queryAlumnoFiltro = @"SELECT 
                                        Id,NombreCompleto 
                                    FROM 
                                        mkt.V_TAlumno_NombreCompleto 
                                    WHERE 
                                        Id = @IdR AND Estado = 1 ORDER BY NombreCompleto ASC";
                var AlumnoDB = _dapperRepository.QueryDapper(queryAlumnoFiltro, new { IdR = idR });
                if (!string.IsNullOrEmpty(AlumnoDB) && !AlumnoDB.Contains("[]"))
                {
                    alumnoAutocompleteFiltro = JsonConvert.DeserializeObject<List<AlumnoComboDTO>>(AlumnoDB)!;
                }
                return alumnoAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retorna un alumno con todos sus datos, podra ser buscado por Email1 o Email2
        /// </summary>
        /// <param name="email1">Email 1 del alumno</param>
        /// <param name="email2">Email 2 del alumno</param>
        /// <returns>Objeto de clase AlumnoBO</returns>
        public Alumno ObtenerPorEmail(string email1, string email2)
        {
            Alumno alumno = new Alumno();
            var query = @"
                            SELECT
	                            TOP 1
                                Id,
                                Nombre1,
                                Nombre2,
                                ApellidoPaterno,
                                ApellidoMaterno,
                                DNI AS Dni,
                                Direccion,
                                FechaNacimiento,
                                Pais,
                                Ciudad,
                                Telefono,
                                Celular,
                                Email1,
                                Email2,
                                NivelFormacion,
                                Profesion,
                                Empresa,
                                EstadoCivil,
                                TelefonoFamiliar,
                                NombreFamiliar,
                                Parentesco,
                                TelefonoTrabajo,
                                TelefonoTrabajoAnexo,
                                Genero,
                                Skype,
                                Fax,
                                IdPais,
                                UbigeoPais,
                                UbigeoDepartamento,
                                UbigeoProvincia,
                                UbigeoCiudad,
                                UbigeoDistrito,
                                DireccionCalle,
                                DireccionAv,
                                DireccionZona,
                                DireccionComp,
                                DireccionTorre,
                                DireccionEdificio,
                                DireccionDpto,
                                DireccionUrb,
                                DireccionMz,
                                DireccionLt,
                                ReferenciaDetallada,
                                HoraMaxima,
                                Puesto,
                                AniversarioBodas,
                                NroHijo,
                                ValidacionTelefonica,
                                FaseContacto,
                                IdCargo,
                                Cargo,
                                IdAFormacion,
                                AFormacion,
                                IdATrabajo,
                                ATrabajo,
                                IdIndustria,
                                Industria,
                                IdReferido,
                                Referido,
                                IdCodigoPais,
                                NombrePais,
                                IdCiudad,
                                NombreCiudad,
                                HoraContacto,
                                HoraPeru,
                                IdCodigoRegionCiudad,
                                Telefono2,
                                Celular2,
                                IdEmpresa,
                                IdOportunidad_Inicial,
                                UsClave,
                                IdTipoDocumento,
                                NroDocumento,
                                DescripcionCargo,
                                Asociado,
                                DeSuscrito,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion,
                                NroOportunidades,
                                EsPersonaValida,
                                EsEliminadoPorRegularizacion,
                                TieneOportunidad,
                                TieneMatricula,
                                EsRepetido,
                                IdEstadoContactoWhatsApp,
                                IdEstadoContactoMailing,
                                DireccionEnvioCertificado,
                                UsarNuevaDireccionParaEnvio,
                                CiudadEnvioCertificado,
                                IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
                                CodigoPortal,
                                IdNumeroTipoDocumento,
                                IdGenero,
                                Comentario
                            FROM mkt.V_TAlumno_Obtener
                            WHERE Estado = 1";
            string email = string.Empty;

            if (email1 != null && email2 == null)
            {
                query += " AND Email1 = @Email";
                email = email1;
            }
            else if (email2 != null && email1 == null)
            {
                query += " AND Email2 = @Email";
                email = email2;
            }

            var alumnoDB = _dapperRepository.FirstOrDefault(query, new { Email = email });
            if (!string.IsNullOrEmpty(alumnoDB) && alumnoDB != "null")
            {
                alumno = JsonConvert.DeserializeObject<Alumno>(alumnoDB)!;
            }
            return alumno;
        }
        /// <summary>
        /// Retorna un alumno con todos sus datos, podra ser buscado por Email1 o Email2
        /// </summary>
        /// <param name="email1">Email 1 del alumno</param>
        /// <returns>Objeto de clase AlumnoBO</returns>
        public Alumno? ObtenerPorEmail1(string email1)
        {
            var query = @"	SELECT Id,
		                Nombre1,
		                Nombre2,
		                ApellidoPaterno,
		                ApellidoMaterno,
		                DNI,
		                Direccion,
		                FechaNacimiento,
		                Pais,
		                Ciudad,
		                Telefono,
		                Celular,
		                Email1,
		                Email2,
		                NivelFormacion,
		                Profesion,
		                Empresa,
		                EstadoCivil,
		                TelefonoFamiliar,
		                NombreFamiliar,
		                Parentesco,
		                TelefonoTrabajo,
		                TelefonoTrabajoAnexo,
		                Genero,
		                Skype,
		                Fax,
		                IdPais,
		                UbigeoPais,
		                UbigeoDepartamento,
		                UbigeoProvincia,
		                UbigeoCiudad,
		                UbigeoDistrito,
		                DireccionCalle,
		                DireccionAv,
		                DireccionZona,
		                DireccionComp,
		                DireccionTorre,
		                DireccionEdificio,
		                DireccionDpto,
		                DireccionUrb,
		                DireccionMz,
		                DireccionLt,
		                ReferenciaDetallada,
		                HoraMaxima,
		                Puesto,
		                AniversarioBodas,
		                NroHijo,
		                ValidacionTelefonica,
		                FaseContacto,
		                IdCargo,
		                Cargo,
		                IdAFormacion,
		                AFormacion,
		                IdATrabajo,
		                ATrabajo,
		                IdIndustria,
		                Industria,
		                IdReferido,
		                Referido,
		                IdCodigoPais,
		                NombrePais,
		                IdCiudad,
		                NombreCiudad,
		                HoraContacto,
		                HoraPeru,
		                IdCodigoRegionCiudad,
		                Telefono2,
		                Celular2,
		                IdEmpresa,
		                IdOportunidad_Inicial AS IdOportunidadInicial,
		                UsClave,
		                IdTipoDocumento,
		                NroDocumento,
		                DescripcionCargo,
		                Asociado,
		                DeSuscrito,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                NroOportunidades,
		                EsPersonaValida,
		                EsEliminadoPorRegularizacion,
		                TieneOportunidad,
		                TieneMatricula,
		                EsRepetido,
		                IdEstadoContactoWhatsApp,
		                IdEstadoContactoMailing,
		                DireccionEnvioCertificado,
		                UsarNuevaDireccionParaEnvio,
		                CiudadEnvioCertificado,
		                IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
		                CodigoPortal,
		                IdNumeroTipoDocumento,
		                IdGenero,
		                Comentario,
		                UrlCvAlumno,
		                NombreArchivoCvAlumno,
		                Municipio,
		                EstadoLugar,
		                CodigoPostal,
		                Colonia,
		                RFC,
		                IdAsentamientoMexico,
		                IdMunicipioMexico,
		                Curp,
                        Rfc,
		                IdCiudadMexico
		            FROM mkt.V_TAlumno_Obtener
                    WHERE Email1 = @Email1
                            AND Estado = 1";
            string email = string.Empty;

            var alumnoDB = _dapperRepository.FirstOrDefault(query, new { Email1 = email1 });
            if (!string.IsNullOrEmpty(alumnoDB) && alumnoDB != "null")
            {
                return JsonConvert.DeserializeObject<Alumno>(alumnoDB)!;
            }
            return null;
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de T_Alumno asociado a email1 o email2.
        /// </summary>
        /// <param name="Email1">Email 1</param>
        /// <param name="Email2">Email 2</param>
        /// <returns> AlumnoValidarEmailDTO </returns>
        public AlumnoValidarEmailDTO ValidarEmailALumno(string Email1, string Email2)
        {
            try
            {
                AlumnoValidarEmailDTO rpta = new AlumnoValidarEmailDTO();
                if (Email1 != null && Email2 == null)
                {
                    var query = @"SELECT Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Telefono,Celular,Email1,Email2, " +
                            "IdCodigoPais,IdCodigoRegionCiudad,IdAFormacion,IdATrabajo,IdIndustria,IdCargo FROM mkt.V_TAlumno_Obtener " +
                            " WHERE Estado = 1 AND Email1 = @Email1";
                    var resultado = _dapperRepository.FirstOrDefault(query, new { Email1 });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != null && resultado != "null")
                    {
                        rpta = JsonConvert.DeserializeObject<AlumnoValidarEmailDTO>(resultado);
                        return rpta;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (Email1 == null && Email2 != null)
                {
                    var query = @"SELECT Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Telefono,Celular,Email1,Email2, " +
                            "IdCodigoPais,IdCodigoRegionCiudad,IdAFormacion,IdATrabajo,IdIndustria,IdCargo FROM mkt.V_TAlumno_Obtener " +
                            " WHERE Estado = 1 AND Email2 = @Email2";
                    var resultado = _dapperRepository.FirstOrDefault(query, new { Email2 });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != null && resultado != "null")
                    {
                        rpta = JsonConvert.DeserializeObject<AlumnoValidarEmailDTO>(resultado);
                        rpta.Email1 = rpta.Email2;
                        return rpta;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de inicio de capacitacion
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de inicio de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaInicioCapacitacionPortalWeb(int idMatriculaCabecera)
        {
            try
            {
                var FechaInicio = new StringDTO();
                string query = @"SELECT 
                                    FechaInicio AS Valor 
                                FROM 
                                    pla.V_FechaInicioFinCapacitacionPortalWeb     
                                WHERE 
                                    IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    FechaInicio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                else
                {
                    FechaInicio.Valor = ObtenerFechaInicioCapacitacion(idMatriculaCabecera);
                }
                if (FechaInicio.Valor == " de  del ")
                {
                    FechaInicio.Valor = ObtenerFechaInicioCapacitacion(idMatriculaCabecera);
                }
                return FechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de fin de capacitacion
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de fin de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaFinCapacitacionPortalWeb(int idMatriculaCabecera)
        {
            try
            {
                var fechaInicio = new StringDTO();
                string query = @"SELECT 
                                    FechaFin AS Valor 
                                FROM 
                                    pla.V_FechaInicioFinCapacitacionPortalWeb     
                                WHERE 
                                    IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    fechaInicio = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                else
                {
                    fechaInicio.Valor = ObtenerFechaFinCapacitacion(idMatriculaCabecera);
                }
                if (fechaInicio.Valor == " de  del ")
                {
                    fechaInicio.Valor = ObtenerFechaFinCapacitacion(idMatriculaCabecera);
                }
                return fechaInicio.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cronograma de Nota por medio de idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<CronogramaNotaDTO> </returns>
        public List<CronogramaNotaDTO> ObtenerCronogramaNota(int idMatriculaCabecera)
        {
            try
            {
                List<CronogramaNotaDTO> cronogramaNota = new List<CronogramaNotaDTO>();
                string query = @"SELECT 
                                    Curso, Nota, Estado  
                                FROM 
                                    ope.V_ObtenerCronogramaNota 
                                WHERE 
                                    IdMatriculaCabecera = @IdMatriculaCabecera ORDER BY Orden ";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    cronogramaNota = JsonConvert.DeserializeObject<List<CronogramaNotaDTO>>(resultado)!;
                }
                return cronogramaNota;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cronograma de Asistencia por medio de idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<CronogramaAsistenciaDTO> </returns>
        public List<CronogramaAsistenciaDTO> ObtenerCronogramaAsistencia(int idMatriculaCabecera)
        {
            try
            {
                List<CronogramaAsistenciaDTO> cronogramaNota = new List<CronogramaAsistenciaDTO>();
                string query = @"SELECT 
                                    Curso,PorcentajeAsistencia 
                                FROM 
                                    ope.V_ObtenerCronogramaPorcentajeAsistencia 
                                WHERE 
                                    IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    cronogramaNota = JsonConvert.DeserializeObject<List<CronogramaAsistenciaDTO>>(resultado)!;
                }
                return cronogramaNota;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado de WhatsApp por idAlumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerEstadoWhatsapp(int idAlumno)
        {
            try
            {
                string query = "SELECT TOP 1 IdEstadoContactoWhatsApp FROM mkt.V_TAlumno_Obtener WHERE id = @IdAlumno";
                var queryDatosAlumno = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno });
                return JsonConvert.DeserializeObject<AlumnoInformacionDTO>(queryDatosAlumno)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene documentos para alumno por ID   
        /// </summary>
        /// <param name="id"></param>
        /// <returns> ObjetoDTO: AlumnoCompuestoDocumentoDTO </returns>
        public AlumnoDatosDocumentoDTO ObtenerDatosAlumnoDocumentoPorId(int id)
        {
            try
            {
                AlumnoDatosDocumentoDTO alumnoDatosDocumentoDTO = new AlumnoDatosDocumentoDTO();
                string queryAlumno = @"SELECT 
                                            Id, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno, Direccion, DNI, 
                                            Celular, Telefono, IdCiudad, NombreCiudad, IdCodigoPais, NombrePais, Correo 
                                        FROM 
                                            mkt.V_TAlumno_DatosAlumnoParaDocumento 
                                        WHERE 
                                            Id = @Id and Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(queryAlumno, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    alumnoDatosDocumentoDTO = JsonConvert.DeserializeObject<AlumnoDatosDocumentoDTO>(resultado)!;
                }
                return alumnoDatosDocumentoDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// lisbeth
        /// </summary>
        /// <param name="id"></param>
        /// <returns> DTO: AlumnoComprobanteDTO </returns>
        public AlumnoComprobanteDTO ObtenerDatosAlumnoPorId(int id)
        {
            try
            {
                AlumnoComprobanteDTO alumnoComprobanteDTO = new AlumnoComprobanteDTO();
                string queryAlumno = @"
                                     SELECT 
                                        Id, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno, Direccion, DNI, Celular, Telefono, IdCiudad, NombreCiudad, IdCodigoPais, NombrePais, Email1 
                                     FROM 
                                        mkt.V_TAlumno_Obtener 
                                     WHERE 
                                        Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(queryAlumno, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    alumnoComprobanteDTO = JsonConvert.DeserializeObject<AlumnoComprobanteDTO>(resultado)!;
                }
                return alumnoComprobanteDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: AlumnoRepositorio
        ///Autor: Jonathan Caipo
        ///Fecha: 03/05/2023
        /// <summary>
        /// Obtener información de Id, Alumnos por nombre AutoComplete
        /// </summary>
        /// <param name="valor"> valor de búsqueda </param>
        /// <returns> Lista de Alumnos por nombre Registrados </returns>
        /// <returns> Objeto DTO: List<AlumnoFiltroAutocompleteDTO> </returns>	
        public IEnumerable<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor)
        {
            try
            {
                IEnumerable<AlumnoFiltroAutocompleteDTO> rpta = new List<AlumnoFiltroAutocompleteDTO>();
                string query = @"
                                SELECT 
                                    Id, NombreCompleto
                                FROM 
                                    mkt.V_TAlumno_NombreCompleto 
                                WHERE 
                                    NombreCompleto LIKE @Valor AND Estado = 1 ORDER BY NombreCompleto ASC";
                var resultado = _dapperRepository.QueryDapper(query, new { Valor = $"%{valor}%" });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<AlumnoFiltroAutocompleteDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerTodoFiltroAutoComplete()", ex);
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del alumno por el IdClasificacionPersona
        /// </summary>
        /// <param name="idClasificacionPersona"> Id de T_ClasificacionPersona </param>
        /// <returns> ObjetoDTO: AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerPorIdClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                string _queryDatosAlumno = @"Select Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,DNI,Direccion,FechaNacimiento,Telefono,Celular,Email1
                                            ,Email2,Genero,Parentesco,NombreFamiliar,TelefonoFamiliar,Empresa,IdCargo,Cargo,IdAFormacion,AFormacion,IdATrabajo,ATrabajo,IdIndustria,Industria,IdReferido,
                                            Referido,IdCodigoPais,NombrePais,IdCiudad,NombreCiudad,HoraContacto,HoraPeru,Telefono2,Celular2,IdEmpresa,IdEstadoContactoWhatsApp,IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
                                            IdOportunidad_Inicial,IdTipoDocumento,NroDocumento,DescripcionCargo,Asociado,RutaBandera,IdClasificacionPersona,PrincipalResponsabilidadProfesional,IdTiempoExperiencia,TiempoExperiencia,IdTamanioEmpresa,TamanioEmpresa From com.V_InformacionAlumno where IdClasificacionPersona=@IdClasificacionPersona and Estado=1";
                var queryDatosAlumno = _dapperRepository.FirstOrDefault(_queryDatosAlumno, new { IdClasificacionPersona = idClasificacionPersona });
                return JsonConvert.DeserializeObject<AlumnoInformacionDTO>(queryDatosAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del alumno por el IdClasificacionPersona
        /// </summary>
        /// <param name="idClasificacionPersona"> Id de T_ClasificacionPersona </param>
        /// <returns> ObjetoDTO: AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerPorIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                string _queryDatosAlumno = @"Select Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,DNI,Direccion,FechaNacimiento,Telefono,Celular,Email1
                                            ,Email2,Genero,Parentesco,NombreFamiliar,TelefonoFamiliar,Empresa,IdCargo,Cargo,IdAFormacion,AFormacion,IdATrabajo,ATrabajo,IdIndustria,Industria,IdReferido,
                                            Referido,IdCodigoPais,NombrePais,IdCiudad,NombreCiudad,HoraContacto,HoraPeru,Telefono2,Celular2,IdEmpresa,IdEstadoContactoWhatsApp,IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario,
                                            IdOportunidad_Inicial,IdTipoDocumento,NroDocumento,DescripcionCargo,Asociado,RutaBandera,IdClasificacionPersona,Modalidad From com.V_InformacionAlumnoOportunidad where IdActividadDetalle=@IdActividadDetalle and Estado=1";
                var queryDatosAlumno = _dapperRepository.FirstOrDefault(_queryDatosAlumno, new { IdActividadDetalle = idActividadDetalle });
                return JsonConvert.DeserializeObject<AlumnoInformacionDTO>(queryDatosAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del alumno acceso portal
        /// </summary>
        /// <param name="idClasificacionPersona"> Id de T_ClasificacionPersona </param>
        /// <returns> ObjetoDTO: AlumnoInformacionDTO </returns>
        /// 
        /// Modificacion: Juan D. Huanaco Quispe
        /// Fecha: 08/05/2024
        /// Descripcion: Se alteró la query para que ahora incluye el Id (guid)
        public AlumnoAccesosDTO ObtenerAccesosAlumno(int idAlumno)
        {
            try
            {
                string _queryDatosAlumno = @"SELECT Id, UserName as usuario,Clave as contrasenia FROM [192.168.2.5].integraDB_PortalWeb.dbo.AspNetUsers WHERE IdAlumno=@idAlumno";
                var queryDatosAlumno = _dapperRepository.FirstOrDefault(_queryDatosAlumno, new { idAlumno = idAlumno });
                return JsonConvert.DeserializeObject<AlumnoAccesosDTO>(queryDatosAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del alumno sobre cobranza
        /// </summary>
        /// <param name="idClasificacionPersona"> Id de T_ClasificacionPersona </param>
        /// <returns> ObjetoDTO: AlumnoInformacionDTO </returns>
        public DatosCorbranzaAlumnoDTO obtenerDatosCobranzaAlumno(int IdMatriculaCabecera)
        {
            try
            {
                DatosCorbranzaAlumnoDTO respuesta = new DatosCorbranzaAlumnoDTO();
                string _query = $@"ope.SP_DatosCobranzaAlumno";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(_query, new { id = IdMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<DatosCorbranzaAlumnoDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del avance AOnline del ALumno
        /// </summary>
        /// <param name="idClasificacionPersona"> Id de T_ClasificacionPersona </param>
        /// <returns> ObjetoDTO: AlumnoInformacionDTO </returns>
        public AvanceAonlineAlumnoDTO obtenerDatosAvanceAonline(int idMatriculaCabecera)
        {
            try
            {
                AvanceAonlineAlumnoDTO respuesta = new AvanceAonlineAlumnoDTO();
                string _query = $@"[ope].[SP_AvanceAOnlineAlumno]";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(_query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<AvanceAonlineAlumnoDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del avance Online del ALumno
        /// </summary>
        /// <param name="idClasificacionPersona"> Id de T_ClasificacionPersona </param>
        /// <returns> ObjetoDTO: AlumnoInformacionDTO </returns>
        public AvanceOnlineAlumnoDTO obtenerDatosAvanceOnline(int idMatriculaCabecera)
        {
            try
            {
                AvanceOnlineAlumnoDTO respuesta = new AvanceOnlineAlumnoDTO();
                string _query = $@"[ope].[SP_AvanceOnlineAlumno]";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(_query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    respuesta = JsonConvert.DeserializeObject<AvanceOnlineAlumnoDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los alumnos de contactos a validarWhatsapp
        /// </summary>
        /// <returns></returns>
        public List<AlumnoWhatsappDTO> ObtenerALumnosaValidarWhatsapp()
        {
            try
            {
                var listaIdsAlumnoPorValidar = new List<AlumnoWhatsappDTO>();
                string _query = "Select  Celular,IdCodigoPais, IdAlumno  From mkt.V_ListaAlumnosValidacionWhatsapp where  celular !='1'";
                var resultado = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaIdsAlumnoPorValidar = JsonConvert.DeserializeObject<List<AlumnoWhatsappDTO>>(resultado);
                }

                return listaIdsAlumnoPorValidar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AccesosMoodleDTO ObtenerAccesosMoodle(string usuarioMoodle)
        {
            try
            {
                try
                {
                    var accesosMoodle = new AccesosMoodleDTO();
                    string _query = "Select  Celular,IdCodigoPais, IdAlumno  From mkt.V_ListaAlumnosValidacionWhatsapp where  celular !='1'";
                    var resultado = _dapperRepository.QueryDapper(_query, new { usuarioMoodle = usuarioMoodle });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    {
                        accesosMoodle = JsonConvert.DeserializeObject<AccesosMoodleDTO>(resultado);
                    }

                    return accesosMoodle;
                }
                catch (Exception e)
                {
                    throw e;
                }



            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ObtenerNumeroWhatsApp(int codigoPais, string celular)
        {
            try
            {
                if (codigoPais == 51)
                {
                    if (celular.Length == 9)
                    {
                        celular = "51" + celular;
                    }
                }
                else if (codigoPais == 57)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 12)
                    {
                        celular = "57" + celular;
                    }
                }
                else if (codigoPais == 591)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 11)
                    {
                        celular = "591" + celular;
                    }
                }
                return celular;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaValidarWhatsappPeru(int cantidad, int iterador)
        {
            try
            {
                var listaAlumnos = new List<AlumnoDTO>();
                var saltar = cantidad * iterador;
                string query = "SELECT * FROM mkt.V_TAlumno_Obtener  WITH (NOLOCK) WHERE IdCodigoPais=51 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 ORDER BY Id ASC OFFSET @Saltar ROWS FETCH NEXT @Cantidad ROWS ONLY ";
                var resultado = _dapperRepository.QueryDapper(query, new { Saltar = saltar, Cantidad = cantidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="alumnos"> Alumnos a actualizar </param>
        /// <param name="estadoWhatsApp"> Estado a modificar a los alumnos </param>
        /// <returns> IntDTO </returns>
        public IntDTO ActualizarValidos(string alumnos, int estadoWhatsApp)
        {
            try
            {
                var respuesta = new IntDTO();
                string query = "mkt.SP_ActualizarIdEstadoContactoWhatsApp";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { Id = alumnos, Estado = estadoWhatsApp });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<IntDTO>(resultado);
                }

                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="alumnos"> Alumnos a actualizar </param>
        /// <param name="estadoWhatsApp"> Estado a modificar a los alumnos </param>
        /// <returns> IntDTO </returns>
        public IntDTO ActualizarValidosSecundario(string alumnos, int estadoWhatsApp)
        {
            try
            {
                var respuesta = new IntDTO();
                string query = "mkt.SP_ActualizarIdEstadoContactoWhatsAppSecundario";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { Id = alumnos, Estado = estadoWhatsApp });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<IntDTO>(resultado);
                }

                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaValidarWhatsappColombia(int cantidad, int iterador)
        {
            try
            {
                var listaAlumnos = new List<AlumnoDTO>();
                var saltar = cantidad * iterador;
                string query = "SELECT * FROM mkt.V_TAlumno_Obtener WITH (NOLOCK)  WHERE IdCodigoPais=57 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 ORDER BY Id ASC OFFSET @Saltar ROWS FETCH NEXT @Cantidad ROWS ONLY ";
                var resultado = _dapperRepository.QueryDapper(query, new { Saltar = saltar, Cantidad = cantidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaValidarWhatsappBolivia(int cantidad, int iterador)
        {
            try
            {
                var listaAlumnos = new List<AlumnoDTO>();
                var saltar = cantidad * iterador;
                string query = "SELECT * FROM mkt.V_TAlumno_Obtener  WITH (NOLOCK) WHERE IdCodigoPais=591 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 ORDER BY Id ASC OFFSET @Saltar ROWS FETCH NEXT @Cantidad ROWS ONLY ";
                var resultado = _dapperRepository.QueryDapper(query, new { Saltar = saltar, Cantidad = cantidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaValidarWhatsappInternacional(int cantidad, int iterador)
        {
            try
            {
                var listaAlumnos = new List<AlumnoDTO>();
                var saltar = cantidad * iterador;
                string query = "SELECT * FROM mkt.V_TAlumno_Obtener WITH (NOLOCK) WHERE IdCodigoPais NOT IN (591,57,51) AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 ORDER BY Id ASC OFFSET @Saltar ROWS FETCH NEXT @Cantidad ROWS ONLY ";
                var resultado = _dapperRepository.QueryDapper(query, new { Saltar = saltar, Cantidad = cantidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaRegularizarWhatsappPeru()
        {
            try
            {
                var listaAlumnos = new List<AlumnoDTO>();
                string query = "SELECT * FROM mkt.V_TAlumno_Obtener WITH (NOLOCK)  WHERE IdCodigoPais=51 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 AND IdEstadoContactoWhatsApp IS NULL ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaRegularizarWhatsappColombia()
        {
            try
            {
                var listaAlumnos = new List<AlumnoDTO>();
                string query = "SELECT * FROM mkt.V_TAlumno_Obtener WITH (NOLOCK)  WHERE IdCodigoPais=57 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 AND IdEstadoContactoWhatsApp IS NULL";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaRegularizarWhatsappBolivia()
        {
            try
            {
                var listaAlumnos = new List<AlumnoDTO>();
                string query = "SELECT * FROM mkt.V_TAlumno_Obtener  WITH (NOLOCK) WHERE IdCodigoPais=591 AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 AND IdEstadoContactoWhatsApp IS NULL ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaRegularizarWhatsappInternacional()
        {
            try
            {
                var listaAlumnos = new List<AlumnoDTO>();
                string query = "SELECT * FROM mkt.V_TAlumno_Obtener WITH (NOLOCK) WHERE IdCodigoPais NOT IN (591,57,51) AND Celular!='' AND Celular!='-' AND Celular IS NOT NULL  AND Estado=1 AND IdEstadoContactoWhatsApp IS NULL ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                }

                return listaAlumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los alumnos por el IdReferido
        /// </summary>
        /// <param name="idReferido"> Id del referido</param>
        /// <returns> Objeto lista DTO: List<AlumnoReferidoDTO> </returns>
        public List<AlumnoReferidosDTO> ObtenerReferidos(int idReferido)
        {
            try
            {
                List<AlumnoReferidosDTO> cronogramaNota = new List<AlumnoReferidosDTO>();
                string query = @"SELECT IdReferido,Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Telefono,Celular,Email1,Email2,HoraPeru 
                                FROM mkt.V_TAlumno_Obtener WHERE IdReferido = @IdReferido";
                var resultado = _dapperRepository.QueryDapper(query, new { IdReferido = idReferido });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    cronogramaNota = JsonConvert.DeserializeObject<List<AlumnoReferidosDTO>>(resultado)!;
                }
                return cronogramaNota;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        ///Autor: Margiory Ramirez.
        ///Fecha: 17/01/2023
        /// <summary>
        /// Obtener información de Id, Alumnos por nombre AutoComplete
        /// </summary>
        /// <param name="valor"> valor de búsqueda </param>
        /// <returns> Lista de Alumnos por nombre Registrados </returns>
        /// <returns> Objeto DTO: List<AlumnoFiltroAutocompleteDTO> </returns>	
        public List<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltrosAutoComplete(string valor)
        {
            try
            {
                List<AlumnoFiltroAutocompleteDTO> alumnosAutocompleteFiltro = new List<AlumnoFiltroAutocompleteDTO>();
                string queryAlumnoFiltro = string.Empty;
                queryAlumnoFiltro = "SELECT Id,NombreCompleto FROM mkt.V_TAlumno_NombreCompleto WHERE CONCAT(NombreCompleto,Id) LIKE CONCAT('%','" + valor + "','%') AND Estado = 1 ORDER By NombreCompleto ASC";
                var alumnoDB = _dapperRepository.QueryDapper(queryAlumnoFiltro, null);
                if (!string.IsNullOrEmpty(alumnoDB) && !alumnoDB.Contains("[]"))
                {
                    alumnosAutocompleteFiltro = JsonConvert.DeserializeObject<List<AlumnoFiltroAutocompleteDTO>>(alumnoDB);
                }
                return alumnosAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos del Alumno
        /// </summary>
        /// <param name="idClasificacionPersona"></param>
        /// <returns></returns>
        public AlumnoInformacionDTO ObtenerDatosAlumno(int idClasificacionPersona)
        {
            try
            {
                AlumnoInformacionDTO resultado = new AlumnoInformacionDTO();
                string _queryDatosAlumno = @"SELECT 
                                                Id, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno, DNI, Direccion, FechaNacimiento, Telefono, Celular, Email1, Email2, Genero, Parentesco, 
                                                NombreFamiliar,TelefonoFamiliar,Empresa,IdCargo,Cargo,IdAFormacion,AFormacion,IdATrabajo,ATrabajo,IdIndustria,Industria,IdReferido, Referido, 
                                                IdCodigoPais, NombrePais, IdCiudad, NombreCiudad, HoraContacto, HoraPeru, Telefono2, Celular2, IdEmpresa, IdEstadoContactoWhatsApp, Asociado, 
                                                IdEstadoContactoWhatsApp_Secundario AS IdEstadoContactoWhatsAppSecundario, IdOportunidad_Inicial, IdTipoDocumento, NroDocumento, DescripcionCargo, 
                                                RutaBandera 
                                            FROM 
                                                com.V_InformacionAlumno 
                                            WHERE 
                                                IdClasificacionPersona = @IdClasificacionPersona AND Estado = 1";
                var queryDatosAlumno = _dapperRepository.FirstOrDefault(_queryDatosAlumno, new { IdClasificacionPersona = idClasificacionPersona });
                if (!string.IsNullOrEmpty(queryDatosAlumno) && !queryDatosAlumno.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<AlumnoInformacionDTO>(queryDatosAlumno)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Autor: Jonathan Caipo
        ///Fecha: 28/03/2023
        /// <summary>
        /// Obtiene el nombre completa del Alumno por medio del email1
        /// </summary>
        /// <param name="id"> email1 del Alumno</param>
        /// <returns> DTO: NombreCompletoAlumnoDTO </returns>
        public NombreCompletoAlumnoDTO ObtenerNombreCompletoAlumnoPorId(int id)
        {
            try
            {
                NombreCompletoAlumnoDTO rpta = new();
                string query = @"SELECT Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno FROM mkt.V_TAlumno_Obtener WHERE id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<NombreCompletoAlumnoDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerNombreCompletoAlumnoPorId: {ex.Message}", ex);
            }
        }

        ///Autor: Carlos Crispin
        ///Fecha: 09/11/2023
        /// <summary>
        /// Actualiza el valor contesto en la tabla de pruebas predictivo
        /// </summary>
        /// <param name="id"> id alumno</param>
        /// <returns> DTO: NombreCompletoAlumnoDTO </returns>
        public ResultadoFinalDTO ActualizarContestoPredictivo(int id)
        {
            try
            {
                var respuesta = new ResultadoFinalDTO();
                string query = "com.SP_ActualizarContestado_DatosPredictivo";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(resultado);
                }

                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        ///Autor: Carlos Crispin
        ///Fecha: 09/11/2023
        /// <summary>
        /// Actualiza el valor contesto en la tabla de pruebas predictivo
        /// </summary>
        /// <param name="id"> id alumno</param>
        /// <returns> DTO: NombreCompletoAlumnoDTO </returns>
        public ResultadoFinalDTO ActualizarCreoOportunidadPredictivo(int id, int idOportunidadCreada)
        {
            try
            {
                var respuesta = new ResultadoFinalDTO();
                string query = "com.SP_ActualizarCreoOportunidad_DatosPredictivo";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno = id, IdOportunidadCreada = idOportunidadCreada });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(resultado);
                }

                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 21/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista TipoCategoriaError
        /// </summary>
        /// <returns> Lista DTO - List<AlumnoFiltroAutocompleteDTO> - alumnos </returns>
        public IEnumerable<ComboDTO> ObtenerTodoFiltroTipoCategoriaError()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                string query = @"
                                SELECT 
                                    Id, Nombre
                                FROM 
                                    pla.V_TipoCategoriaErrorFiltro 
                                WHERE 
                                    Estado = 1 ORDER By Nombre ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerTodoFiltroTipoCategoriaError()", ex);
            }
        }

        public string guardarArchivosQR(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/certificados/CodigoQR/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }


                    return _nombreLink;

                }
                catch (Exception ex)
                {
                    return "";
                }

            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }
        public bool ObtenerAlumnoPorDNI(StringDTO valor)
        {

            string query = @"SELECT Id, Nombre1 as Nombre
                            FROM mkt.V_TAlumno_Obtener
                            WHERE DNI = @DNI;";
            var resultado = _dapperRepository.FirstOrDefault(query, new { DNI = valor.Valor });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
            {
                return true;
            }
            return false;
        }
        public PruebaCFD ObtenerAlumnoPorDNIV2(StringDTO valor)
        {

            string query = @"SELECT CASE
                                        WHEN Nombre2 !='' THEN
                                           'Si'
                                       ELSE
                                           'No'
                                   END AS Existe,
                                   Nombre1 AS Nombre
                            FROM mkt.V_TAlumno_Obtener
                            WHERE DNI = @DNI;";
            var resultado = _dapperRepository.FirstOrDefault(query, new { DNI = valor.Valor });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
            {
                return JsonConvert.DeserializeObject<PruebaCFD>(resultado)!;
            }
            return null;
        }
        public InformacionAlumnoDTO ObtenerInformacionAlumno(int idAlumno)
        {

            string query = @"conf.SP_ObtenerInformacionActualAlumno";
            var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { idAlumno });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                return JsonConvert.DeserializeObject<InformacionAlumnoDTO>(resultado)!;
            }
            return null;
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 12/12/2023
        /// Version/: 1.0
        /// <summary>/
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="AlumnoActualizar"> Alumnos a actualizar </param>
        /// <returns> ResultadoFinalDTO </returns>
        public bool ActualizarAlumnoWhatsapp(DatosAlumnoWhatsappDTO valor)
        {
            try
            {
                string query = "mkt.SP_ActualizarAlumnoWhatsapp";


                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, valor);

                return !string.IsNullOrEmpty(resultado) && resultado != "[]";
            }
            catch (Exception e)
            {

                Console.WriteLine($"Error en ActualizarAlumnoWhatsapp: {e.Message}");
                return false;
            }

        }
        public AvatarAlumnoDTO ObtenerAvatar(int IdAlumno, string Genero)
        {
            try
            {
                var _query = string.Empty;
                AvatarAlumnoDTO usuarioAvatar = new AvatarAlumnoDTO();
                _query = "SELECT IdAvatar,IdALumno,TopC, Accessories, Hair_Color, Facial_Hair, Facial_Hair_Color, Clothes, Clothes_Color,Eyes,Eyesbrow,Mouth,Skin FROM [192.168.2.5].integraDB_PortalWeb.dbo.V_PW_ObtenerAvatar WHERE IdAlumno = @IdAlumno";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdAlumno });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    usuarioAvatar = JsonConvert.DeserializeObject<AvatarAlumnoDTO>(respuesta);
                }
                else
                {
                    usuarioAvatar = new AvatarAlumnoDTO();
                    if (Genero == "F")
                    {
                        usuarioAvatar.IdAlumno = IdAlumno;
                        usuarioAvatar.TopC = "LongHairStraight";
                        usuarioAvatar.Accessories = "Blank";
                        usuarioAvatar.Hair_Color = "Brown";
                        usuarioAvatar.Facial_Hair = "Blank";
                        usuarioAvatar.Facial_Hair_Color = "Brown";
                        usuarioAvatar.Clothes = "ShirtScoopNeck";
                        usuarioAvatar.Clothes_Color = "Pink";
                        usuarioAvatar.Eyes = "Default";
                        usuarioAvatar.Eyesbrow = "Default";
                        usuarioAvatar.Mouth = "Default";
                        usuarioAvatar.Skin = "Light";
                    }
                    else
                    {
                        usuarioAvatar = new AvatarAlumnoDTO();
                        usuarioAvatar.IdAlumno = IdAlumno;
                        usuarioAvatar.TopC = "ShortHairTheCaesar";
                        usuarioAvatar.Accessories = "Blank";
                        usuarioAvatar.Hair_Color = "Auburn";
                        usuarioAvatar.Facial_Hair = "Blank";
                        usuarioAvatar.Facial_Hair_Color = "Auburn";
                        usuarioAvatar.Clothes = "CollarSweater";
                        usuarioAvatar.Clothes_Color = "Blue02";
                        usuarioAvatar.Eyes = "Default";
                        usuarioAvatar.Eyesbrow = "Default";
                        usuarioAvatar.Mouth = "Default";
                        usuarioAvatar.Skin = "Tanned";
                    }
                }
                return usuarioAvatar;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CredencialesPortalWebAlumnoDTO ObtenerCredencialesPortalWebPorIdAlumno(int idAlumno)
        {
            try
            {
                CredencialesPortalWebAlumnoDTO credencialesObtenidas = new CredencialesPortalWebAlumnoDTO();

                string query = "SELECT IdAlumno, PortalWebUsuario, PortalWebClave FROM conf.V_ObtenerCredencialesPortalWebPorIdAlumno WHERE IdAlumno = @IdAlumno";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    credencialesObtenidas = JsonConvert.DeserializeObject<CredencialesPortalWebAlumnoDTO>(resultado);
                }

                return credencialesObtenidas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
    }
}



