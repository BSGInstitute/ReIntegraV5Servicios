using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MontoPagoCronogramaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/07/2022
    /// <summary>
    /// Gestión general de T_MontoPagoCronograma
    /// </summary>
    public class MontoPagoCronogramaRepository : GenericRepository<TMontoPagoCronograma>, IMontoPagoCronogramaRepository
    {
        private Mapper _mapper;

        public MontoPagoCronogramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPagoCronograma, MontoPagoCronograma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMontoPagoCronograma MapeoEntidad(MontoPagoCronograma entidad)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoCronograma modelo = _mapper.Map<TMontoPagoCronograma>(entidad);

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

        public TMontoPagoCronograma Add(MontoPagoCronograma entidad)
        {
            try
            {
                var MontoPagoCronograma = MapeoEntidad(entidad);
                base.Insert(MontoPagoCronograma);
                return MontoPagoCronograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPagoCronograma Update(MontoPagoCronograma entidad)
        {
            try
            {
                var MontoPagoCronograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MontoPagoCronograma.RowVersion = entidadExistente.RowVersion;

                base.Update(MontoPagoCronograma);
                return MontoPagoCronograma;
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


        public IEnumerable<TMontoPagoCronograma> Add(IEnumerable<MontoPagoCronograma> listadoEntidad)
        {
            try
            {
                List<TMontoPagoCronograma> listado = new List<TMontoPagoCronograma>();
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

        public IEnumerable<TMontoPagoCronograma> Update(IEnumerable<MontoPagoCronograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMontoPagoCronograma> listado = new List<TMontoPagoCronograma>();
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
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MontoPagoCronograma.
        /// </summary>
        /// <returns> List<MontoPagoCronogramaDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDTO> ObtenerMontoPagoCronograma()
        {
            try
            {
                List<MontoPagoCronogramaDTO> rpta = new List<MontoPagoCronogramaDTO>();
                var query = @"
                    SELECT
	                    Id,IdOportunidad,IdMontoPago,IdPersonal,Precio,PrecioDescuento,IdMoneda,IdTipoDescuento,EsAprobado,NombrePlural,Formula,
	                    MatriculaEnProceso,CodigoMatricula,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_MontoPagoCronograma
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MontoPagoCronogramaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPagoCronograma para mostrarse en combo.
        /// </summary>
        /// <returns> List<MontoPagoCronogramaComboDTO> </returns>
        public IEnumerable<MontoPagoCronogramaComboDTO> ObtenerCombo()
        {
            try
            {
                List<MontoPagoCronogramaComboDTO> rpta = new List<MontoPagoCronogramaComboDTO>();
                var query = @"SELECT Id,IdOportunidad FROM com.T_MontoPagoCronograma WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MontoPagoCronogramaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPagoCronograma asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoCronogramaDTO> </returns>
        public MontoPagoCronograma ObtenerPorIdOportunidad(int idOportunidad)
        {
            try
            {
                MontoPagoCronograma cronograma = new MontoPagoCronograma();
                var query = @"
                        SELECT
	                        Id,
		                    IdOportunidad,
		                    IdMontoPago,
		                    IdPersonal,
		                    Precio,
		                    PrecioDescuento,
		                    IdMoneda,
		                    IdTipoDescuento,
		                    EsAprobado,
		                    NombrePlural,
		                    Formula,
		                    MatriculaEnProceso,
		                    CodigoMatricula,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM com.T_MontoPagoCronograma
                        WHERE Estado=1 AND IdOportunidad = @idOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    cronograma = JsonConvert.DeserializeObject<MontoPagoCronograma>(resultado);
                }
                return cronograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPagoCronograma asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagoCronogramaDTO> </returns>
        public async Task<MontoPagoCronograma> ObtenerPorIdOportunidadAsync(int idOportunidad)
        {
            try
            {
                MontoPagoCronograma cronograma = new MontoPagoCronograma();
                var query = @"
                        SELECT
	                        Id,
		                    IdOportunidad,
		                    IdMontoPago,
		                    IdPersonal,
		                    Precio,
		                    PrecioDescuento,
		                    IdMoneda,
		                    IdTipoDescuento,
		                    EsAprobado,
		                    NombrePlural,
		                    Formula,
		                    MatriculaEnProceso,
		                    CodigoMatricula,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM com.T_MontoPagoCronograma
                        WHERE Estado=1 AND IdOportunidad = @idOportunidad";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    cronograma = JsonConvert.DeserializeObject<MontoPagoCronograma>(resultado);
                }
                return cronograma;
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
        /// Obtiene el Cronograma de Pago asociado a una Oportunidad para Documentos de Agenda
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCronogramaDocumentoDTO </returns>
        public MontoPagoCronogramaDocumentoDTO ObtenerConogramaPagoDocumentoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                MontoPagoCronogramaDocumentoDTO cronograma = new MontoPagoCronogramaDocumentoDTO();
                var query = @"
                            SELECT 
                                IdMoneda, PrecioDescuento, IdMontoPago
                            FROM 
                                com.V_TMontoPagoCronograma_DatosDocumento
                            WHERE 
                                IdOportunidad = @IdOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    cronograma = JsonConvert.DeserializeObject<MontoPagoCronogramaDocumentoDTO>(resultado)!;
                }
                return cronograma;
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
        /// Obtiene el Cronograma de Pago asociado a una Oportunidad Operaciones para Documentos de Agenda
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> MontoPagoCronogramaDocumentoDTO </returns>
        public MontoPagoCronogramaDocumentoDTO ObtenerConogramaPagoDocumentoPorIdOportunidadOperaciones(int idOportunidad)
        {
            try
            {
                MontoPagoCronogramaDocumentoDTO cronograma = new MontoPagoCronogramaDocumentoDTO();
                var query = @"
                    SELECT IdMoneda,PrecioDescuento,IdMontoPago
                    FROM com.V_TMontoPagoCronograma_DatosDocumentoOperaciones
                    WHERE IdOportunidad = @IdOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    cronograma = JsonConvert.DeserializeObject<MontoPagoCronogramaDocumentoDTO>(resultado);
                }
                return cronograma;
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
        /// Retorna el monto pagado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de Matricula Cabecera</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<MontoPagadoDTO> </returns>
        public List<MontoPagadoDTO> ObtenerMontoPagado(int idMatriculaCabecera, int idOportunidad)
        {
            try
            {
                List<MontoPagadoDTO> montos = new List<MontoPagadoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_MontoPagado", new { idOportunidad, idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    montos = JsonConvert.DeserializeObject<List<MontoPagadoDTO>>(resultado);
                }
                return montos;
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
        /// Retorna el monto pagado
        /// </summary>
        /// <param name="idPespecifico">Id del programa especifico</param>
        /// <returns> List<MontoPagadoDTO> </returns>
        public List<SesionesDTO> ObtenerSesionesOnline(int idPespecifico)
        {
            try
            {
                List<SesionesDTO> montos = new List<SesionesDTO>();
                var resultado = _dapperRepository.QuerySPDapper("pla.SP_ObtenerSesionPorPEspecifico", new { idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    montos = JsonConvert.DeserializeObject<List<SesionesDTO>>(resultado);
                }
                return montos;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_MontoPagoCronograma asociado al identificador.
        /// </summary>
        /// <param name="idCronograma">Id de MontoPagoCronograma</param>
        /// <returns> MontoPagoCronogramaDTO </returns>
        public MontoPagoCronogramaDTO ObtenerPorId(int idCronograma)
        {
            try
            {
                MontoPagoCronogramaDTO rpta = new MontoPagoCronogramaDTO();
                var query = @"
                    SELECT
	                    Id,IdOportunidad,IdMontoPago,IdPersonal,Precio,PrecioDescuento,IdMoneda,IdTipoDescuento,EsAprobado,NombrePlural,Formula,
	                    MatriculaEnProceso,CodigoMatricula,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_MontoPagoCronograma
                    WHERE Estado = 1 AND Id = @idCronograma";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCronograma });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<MontoPagoCronogramaDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera un Cronograma
        /// </summary>
        /// <param name="idCronograma">Id de Cronograma</param>
        /// <returns> ValorIntDTO> </returns>
        public ResultadoDTO GenerarCronogramaPorCoordinador(int idCronograma)
        {
            try
            {
                ResultadoDTO respuesta = new ResultadoDTO()
                {
                    Resultado = 0
                };
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_GenerarCronogramaVentasByCordinador", new { idCronograma });

                /*Se agrega congelamiento de curso adicional para cursos IRCA*/
                try
                {
                    _dapperRepository.QuerySPFirstOrDefault("[com].[SP_CongelarCursoAdicional]", new { idCronograma });
                }
                catch
                {
                }

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<ResultadoDTO>(resultado);
                    // respuesta.Valor = Convert.ToInt32(json["Resultado"]);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina un Cronograma
        /// </summary>
        /// <param name="idCronograma">Id de Cronograma</param>
        /// <returns> ValorIntDTO </returns>
        public ResultadoDTO EliminarCronogramaVentasPorCoordinador(int idCronograma)
        {
            try
            {
                ResultadoDTO respuesta = new ResultadoDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_EliminarCronogramaVentasByCordinador", new { idCronograma });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && !resultado.Contains("null"))
                {
                    respuesta = JsonConvert.DeserializeObject<ResultadoDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Usuario y Clave de PortalWeb
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="email">Email del Alumno</param>
        /// <returns> DatosUsuarioPortalDTO </returns>
        public DatosUsuarioPortalDTO ObtenerUsuarioClavePortalWeb(int idAlumno, string email)
        {
            try
            {
                DatosUsuarioPortalDTO datosUsuario = new DatosUsuarioPortalDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("conf.SP_GetUsuarioClavePortalWeb", new { idAlumno, email });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    datosUsuario = JsonConvert.DeserializeObject<DatosUsuarioPortalDTO>(resultado);
                }
                return datosUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        ///	Crea Usuario de Portal Clave
        /// </summary>
        /// <param name="idAlumno">Id de Alumno</param>
        /// <param name="email">Email de Alumno</param>
        /// <param name="clave">Clave</param>
        /// <param name="claveEncriptada">Clave Encriptada</param>
        /// <param name="nombres">Nombre de Alumno</param>
        /// <param name="apellidos">Apellidos</param>
        /// <param name="telefono">Teléfono de Alumno</param>
        /// <param name="celular">Celular</param>
        /// <param name="idCodigoCiudad">Id de Código de Ciudad</param>
        /// <param name="idCodigoPais">Id de Código de Pais</param>
        /// <param name="fecha">Fecha de Creación</param>
        /// <returns> DatosUsuarioPortalDTO </returns>
        public DatosUsuarioPortalDTO CrearUsuarioClavePortalWeb(int idAlumno, string email, string clave, string claveEncriptada, string nombres, string apellidos, string telefono, string celular, int? idCodigoCiudad, int? idCodigoPais, DateTime fecha)
        {
            try
            {
                DatosUsuarioPortalDTO respuesta = new DatosUsuarioPortalDTO();
                string resultado = _dapperRepository.QuerySPFirstOrDefault("conf.SP_CreateUsuarioClavePortalWeb",
                    new
                    {
                        IdAlumno = idAlumno,
                        Email = email,
                        Clave = clave,
                        ClaveEncriptada = claveEncriptada,
                        Nombres = nombres,
                        Apellidos = apellidos,
                        Fijo = telefono,
                        Celular = celular,
                        CodigoPais = idCodigoPais,
                        CodigoCiudad = idCodigoCiudad,
                        Fecha = fecha
                    });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<DatosUsuarioPortalDTO>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 19/08/2022
        /// Version 1.0
        /// <summary>
        /// Obtener Detalle monto Pago
        /// </summary>
        /// <param name="idMontoPago"> Id  Monto Pago </param>        
        /// <returns> Lista Detalle Monto Pago : List<DetalleMontoPagoDTO> </returns>
        public List<DetalleMontoPagoDTO> ObtenerDetalleMontoPago(int idMontoPago)
        {
            try
            {
                List<DetalleMontoPagoDTO> montos = new List<DetalleMontoPagoDTO>();
                string sql = @"
                    SELECT 
                        Titulo,
                        OrdenBeneficio
                    FROM pla.V_DetalleBeneficioPorMontoPago
                    WHERE IdMontoPago = @idMontoPago
                ";
                var resultado = _dapperRepository.QueryDapper(sql, new { idMontoPago });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    montos = JsonConvert.DeserializeObject<List<DetalleMontoPagoDTO>>(resultado);
                }
                return montos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/10/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene Cutoas Pagadas por codigoMatricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public ResultadoDTO? CuotaPagada(string codigoMatricula)
        {
            try
            {
                string resultado = _dapperRepository.QuerySPFirstOrDefault("fin.SP_CuotasPagadas", new { codigoMatricula });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ResultadoDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"Error en CuotaPagada, {e.Message}");
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene registros de asociados al IdOportunidad y TipoPersonal
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <param name="tipoPersonal"> Tipo de personal </param>
        /// <returns> List<TipoDescuentoOportunidadDTO> </returns>
        public List<TipoDescuentoOportunidadDTO> ObtenerTipoDescuento(int idOportunidad, string tipoPersonal)
        {
            try
            {
                var Descuentos = new List<TipoDescuentoOportunidadDTO>();
                var _query = @"SELECT Id,Codigo,Descripcion,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,Tipo 
                                FROM mkt.V_TiposDescuentos 
                                WHERE  IdOportunidad = @idOportunidad and Tipo = @tipoPersonal";
                var TiposDescuentosDB = _dapperRepository.QueryDapper(_query, new { idOportunidad, tipoPersonal });
                if (!string.IsNullOrEmpty(TiposDescuentosDB) && !TiposDescuentosDB.Contains("[]"))
                {
                    Descuentos = JsonConvert.DeserializeObject<List<TipoDescuentoOportunidadDTO>>(TiposDescuentosDB);
                }
                return Descuentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene montos complementarios filtrado por el IdPGeneral, IdPais, IdMontoPago y IdMatriculaCabecera
        /// </summary>
        /// <param name="idPGeneral"> Id de T_PGeneral </param>
        /// <param name="idPais"> Id de T_Pais </param>
        /// <param name="idMontoPago"> Id T_MontoPago </param>
        /// <param name="idMatriculaCabecera"> Id de T_MatriculaCabecera </param>
        /// <returns> List<DatosMontosComplementariosDTO> </returns>
        public List<DatosMontosComplementariosDTO> ObtenerMontosComplementarios(int idPGeneral, int idPais, int idMontoPago, int idMatriculaCabecera)
        {
            try
            {
                var Resultado = new List<DatosMontosComplementariosDTO>();
                string resultado = _dapperRepository.QuerySPDapper("pla.SP_MontosComplementarios",
                    new { IdPGeneral = idPGeneral, IdPPais = idPais, IdMontoPago = idMontoPago, IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    Resultado = JsonConvert.DeserializeObject<List<DatosMontosComplementariosDTO>>(resultado);
                    return Resultado;
                }
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene montos pagos asociados al IdOportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de T_Oportunidad </param>
        /// <returns> List<MontoPagoOportunidadDTO> </returns>
        public List<MontoPagoOportunidadDTO> ObtenerMontosPagos(int idOportunidad)
        {
            try
            {
                var Montos = new List<MontoPagoOportunidadDTO>();
                var _query = @"SELECT Id,Precio,PrecioLetras,IdMoneda,Matricula,Cuotas,NroCuotas,IdPrograma,IdTipoPago,IdPais,Vencimiento,PrimeraCuota,CuotaDoble,IdTipoDescuento,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,NombrePlural,CuotasTipoPago,Paquete,Nombre,VisibleWeb,MontoDescontado FROM mkt.V_MontosPagos WHERE  IdOportunidad = @idOportunidad";
                var MontosDB = _dapperRepository.QueryDapper(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(MontosDB) && !MontosDB.Contains("[]"))
                {
                    Montos = JsonConvert.DeserializeObject<List<MontoPagoOportunidadDTO>>(MontosDB);
                }
                return Montos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version 1.0
        /// <summary>
        /// Obtiene registros de asociados al IdOportunidad y TipoPersonal
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <param name="tipoPersonal"> Tipo de personal </param>
        /// <returns> List<TipoDescuentoOportunidadDTO> </returns>
        public MontoPagoCronogramaCompletoDTO ObtenerPorIdOportunidadYTipoPersonal(int idOportunidad, string tipoPersonal)
        {
            try
            {
                var Descuentos = new MontoPagoCronogramaCompletoDTO();
                var _query = @"SELECT Id,Codigo,Descripcion,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,Tipo 
                                FROM mkt.V_TiposDescuentos 
                                WHERE  IdOportunidad = @idOportunidad and Tipo = @tipoPersonal";
                var TiposDescuentosDB = _dapperRepository.FirstOrDefault(_query, new { idOportunidad, tipoPersonal });
                if (!string.IsNullOrEmpty(TiposDescuentosDB) && TiposDescuentosDB != "null")
                {
                    Descuentos = JsonConvert.DeserializeObject<MontoPagoCronogramaCompletoDTO>(TiposDescuentosDB);
                }
                return Descuentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 17/01/2023
        /// Version 1.0
        /// <summary>
        /// Obtiene todos los Ids de MontoPagoCronograma relacionado al IdOportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param> 
        /// <returns> List<int> </returns>
        public List<ValorIntDTO> listadoIdsPorOportunidad(int idOportunidad)
        {
            try
            {
                var Descuentos = new List<ValorIntDTO>();
                var _query = @"SELECT Id FROM com.T_MontoPagoCronograma WHERE Estado=1 AND IdOportunidad =  @IdOportunidad";
                var TiposDescuentosDB = _dapperRepository.QueryDapper(_query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(TiposDescuentosDB) && TiposDescuentosDB != "[]")
                {
                    Descuentos = JsonConvert.DeserializeObject<List<ValorIntDTO>>(TiposDescuentosDB);
                }
                return Descuentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 24/04/2023
        /// Version: 2.0
        /// <summary>
        /// Obtiene un listado de Oportunidades de Operaciones
        /// </summary>
        /// <param name="Paginador">paginador</param>
        /// <param name="Filtro"> Filtro Modulo </param>
        /// <param name="FilterGrid"> filtros de grilla </param>        
        /// <returns>Lista</returns>
        public ResultadoFiltroReporteCompromisoDTO ObtenerReporteCompromisoPagoFiltrado(PaginadorDTO Paginador, ReporteCompromisoPagoDTO Filtro, GridFiltersDTO FilterGrid)
        {
            try
            {
                ResultadoFiltroReporteCompromisoDTO compromisos = new ResultadoFiltroReporteCompromisoDTO();
                var filtros = new
                {
                    ListaCoordinador = Filtro.ListaCoordinador.Count() == 0 ? "0" : string.Join(",", Filtro.ListaCoordinador.Select(x => x)),
                    ListaAlumno = Filtro.ListaAlumno == null ? "" : string.Join(",", Filtro.ListaAlumno.Select(x => x)),
                    ListaCentroCosto = Filtro.ListaCentroCosto == null ? "" : string.Join(",", Filtro.ListaCentroCosto.Select(x => x)),
                    EstadoCuotas = Filtro.EstadoCuotas,
                    FechaGeneradoInicio = Filtro.FechaGeneradoInicio,
                    FechaGeneradoFin = Filtro.FechaGeneradoFin,
                    FechaCompromisoInicio = Filtro.FechaCompromisoInicio,
                    FechaCompromisoFin = Filtro.FechaCompromisoFin,
                    CodigoMatricula = Filtro.CodigoMatricula,
                    Skip = Paginador.skip,
                    Take = Paginador.take,
                    Cantidad = false,
                };
                var filtrosV2 = new
                {
                    ListaCoordinador = Filtro.ListaCoordinador.Count() == 0 ? "0" : string.Join(",", Filtro.ListaCoordinador.Select(x => x)),
                    ListaAlumno = Filtro.ListaAlumno == null ? "" : string.Join(",", Filtro.ListaAlumno.Select(x => x)),
                    ListaCentroCosto = Filtro.ListaCentroCosto == null ? "" : string.Join(",", Filtro.ListaCentroCosto.Select(x => x)),
                    EstadoCuotas = Filtro.EstadoCuotas,
                    FechaGeneradoInicio = Filtro.FechaGeneradoInicio,
                    FechaGeneradoFin = Filtro.FechaGeneradoFin,
                    FechaCompromisoInicio = Filtro.FechaCompromisoInicio,
                    FechaCompromisoFin = Filtro.FechaCompromisoFin,
                    CodigoMatricula = Filtro.CodigoMatricula,
                    Skip = Paginador.skip,
                    Take = Paginador.take,
                    Cantidad = true,
                };
                string query = "[fin].[SP_ObtenerInformacionCompromisoPagoAlumnoV2]";
                var queryDB = _dapperRepository.QuerySPDapper(query, filtros);
                var queryCantidadDB = _dapperRepository.QuerySPFirstOrDefault(query, filtrosV2);
                var rpta = JsonConvert.DeserializeObject<List<ReporteCompromisoPagoDetalleDTO>>(queryDB);
                var total = JsonConvert.DeserializeObject<TotalReporteCompromisoPagoDTO>(queryCantidadDB);
                compromisos.Lista = rpta;
                compromisos.Total = total.Cantidad;
                return compromisos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
