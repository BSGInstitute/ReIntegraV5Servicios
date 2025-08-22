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
    /// Repositorio: AsignacionAutomaticaTempRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 22/11/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class AsignacionAutomaticaTempRepository : GenericRepository<TAsignacionAutomaticaTemp>, IAsignacionAutomaticaTempRepository
    {
        private Mapper _mapper;

        public AsignacionAutomaticaTempRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionAutomaticaTemp, AsignacionAutomaticaTemp>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAsignacionAutomaticaTemp MapeoEntidad(AsignacionAutomaticaTemp entidad)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomaticaTemp modelo = _mapper.Map<TAsignacionAutomaticaTemp>(entidad);

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

        public TAsignacionAutomaticaTemp Add(AsignacionAutomaticaTemp entidad)
        {
            try
            {
                var AsignacionAutomaticaTemp = MapeoEntidad(entidad);
                base.Insert(AsignacionAutomaticaTemp);
                return AsignacionAutomaticaTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsignacionAutomaticaTemp Update(AsignacionAutomaticaTemp entidad)
        {
            try
            {
                var AsignacionAutomaticaTemp = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionAutomaticaTemp.RowVersion = entidadExistente.RowVersion;

                base.Update(AsignacionAutomaticaTemp);
                return AsignacionAutomaticaTemp;
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


        public IEnumerable<TAsignacionAutomaticaTemp> Add(IEnumerable<AsignacionAutomaticaTemp> listadoEntidad)
        {
            try
            {
                List<TAsignacionAutomaticaTemp> listado = new List<TAsignacionAutomaticaTemp>();
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

        public IEnumerable<TAsignacionAutomaticaTemp> Update(IEnumerable<AsignacionAutomaticaTemp> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsignacionAutomaticaTemp> listado = new List<TAsignacionAutomaticaTemp>();
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

        public AsignacionAutomaticaTemp ObtenerPorId(int idAsignacionAutomaticaTemp)
        {
            try
            {
                AsignacionAutomaticaTemp asignacionAutomaticaTemp = new AsignacionAutomaticaTemp();
                var query = @"
                                SELECT
                                    Id,
                                    Nombres,
                                    Apellidos,
                                    Correo,
                                    Fijo,
                                    Movil,
                                    IdPais,
                                    IdCiudad,
                                    IdAreaFormacion,
                                    IdCargo,
                                    IdAreaTrabajo,
                                    IdIndustria,
                                    NombrePrograma,
                                    IdCentroCosto,
                                    CentroCosto,
                                    IdTipoDato,
                                    IdFaseOportunidad,
                                    Origen,
                                    procesado,
                                    IdConjuntoAnuncio,
                                    IdFaseOportunidadPortal,
                                    FechaRegistroCampania,
                                    IdTiempoCapacitacion,
                                    IdCategoriaDato,
                                    IdTipoInteraccion,
                                    IdInteraccionFormulario,
                                    UrlOrigen,
                                    IdPagina,
                                    Estado,
                                    FechaCreacion,
                                    UsuarioCreacion,
                                    FechaModificacion,
                                    UsuarioModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    IdAnuncioFacebook,
                                    IdFacebookFormularioLeadgen,
                                    AptoProcesamiento
                                FROM mkt.T_AsignacionAutomatica_Temp
                                WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAsignacionAutomaticaTemp });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    asignacionAutomaticaTemp = JsonConvert.DeserializeObject<AsignacionAutomaticaTemp>(resultado)!;
                }
                return asignacionAutomaticaTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionAutomaticaTemp ObtenerPorIdFaseOportunidadPortal(string idFaseOportunidadPortal)
        {
            try
            {
                AsignacionAutomaticaTemp asignacionAutomaticaTemp = new AsignacionAutomaticaTemp();
                var query = @"
                        SELECT
                            Id,
                            Nombres,
                            Apellidos,
                            Correo,
                            Fijo,
                            Movil,
                            IdPais,
                            IdCiudad,
                            IdAreaFormacion,
                            IdCargo,
                            IdAreaTrabajo,
                            IdIndustria,
                            NombrePrograma,
                            IdCentroCosto,
                            CentroCosto,
                            IdTipoDato,
                            IdFaseOportunidad,
                            Origen,
                            procesado,
                            IdConjuntoAnuncio,
                            IdFaseOportunidadPortal,
                            FechaRegistroCampania,
                            IdTiempoCapacitacion,
                            IdCategoriaDato,
                            IdTipoInteraccion,
                            IdInteraccionFormulario,
                            UrlOrigen,
                            IdPagina,
                            Estado,
                            FechaCreacion,
                            UsuarioCreacion,
                            FechaModificacion,
                            UsuarioModificacion,
                            RowVersion,
                            IdMigracion,
                            IdAnuncioFacebook,
                            IdFacebookFormularioLeadgen,
                            AptoProcesamiento
                        FROM mkt.T_AsignacionAutomatica_Temp 
                        WHERE Estado = 1 AND 
                            CAST(IdFaseOportunidadPortal AS UNIQUEIDENTIFIER) = CAST(@idFaseOportunidadPortal AS UNIQUEIDENTIFIER)";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idFaseOportunidadPortal });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    asignacionAutomaticaTemp = JsonConvert.DeserializeObject<AsignacionAutomaticaTemp>(resultado)!;
                }
                return asignacionAutomaticaTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 22/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una nuevos registros para asignacionautomaticatemp
        /// </summary>
        /// <returns>Lista de objetos de clase AsignacionAutomaticaTempDTO</returns>
        public List<AsignacionAutomaticaTempDTO> ObtenerNuevosRegistros()
        {
            List<AsignacionAutomaticaTempDTO> Registros = new List<AsignacionAutomaticaTempDTO>();

            var RegistroDB = _dapperRepository.QuerySPDapper("dbo.SP_GetContactoFaseOportunidadPWNuevo", new { Id = 1 });
            Registros = JsonConvert.DeserializeObject<List<AsignacionAutomaticaTempDTO>>(RegistroDB);
            return Registros;
        }

        /// Autor: Margiory Ramirez 
        /// Fecha: 22/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obterner el NombreCampania por IdFase Oportunidad
        /// </summary>
        /// <param name="IdFaseOportunidad"> IdFaseOportunidad </param>
        /// <returns></returns> 
        public NombreCampaniaAsiAsignacionAutomaticaTempDTO ObtenerNombreCampaniaPorIdFaseOportunidad(string IdFaseOportunidad)
        {
            try
            {
                string query = "SELECT IdFaseOportunidadPortal,IdConjuntoAnuncio, NombreCampania FROM [mkt].[V_TFaseOportunidadPortal_ObtenerNombreCampania] WHERE  IdFaseOportunidadPortal=@IdFaseOportunidad AND Estado = 1";
                var nombreCampaniaAdwsDB = _dapperRepository.FirstOrDefault(query, new { IdFaseOportunidad });
                if (nombreCampaniaAdwsDB != "null")
                {
                    return JsonConvert.DeserializeObject<NombreCampaniaAsiAsignacionAutomaticaTempDTO>(nombreCampaniaAdwsDB);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IdDTO InsertarAsignacionAutomatica(InsertarAsignacionAutomaticaTempDTO asignacion)
        {
            try
            {
                var parameters = new
                {
                    Nombres = asignacion.Nombres,
                    Apellidos = asignacion.Apellidos,
                    Correo = asignacion.Correo,
                    Movil = asignacion.Movil,
                    IdPais = asignacion.IdPais,
                    IdCiudad = asignacion.IdCiudad,
                    IdAreaFormacion = asignacion.IdAreaFormacion,
                    IdCargo = asignacion.IdCargo,
                    IdAreaTrabajo = asignacion.IdAreaTrabajo,
                    IdIndustria = asignacion.IdIndustria,
                    IdCentroCosto = asignacion.IdCentroCosto,
                    IdTipoDato = asignacion.IdTipoDato,
                    IdFaseOportunidad = asignacion.IdFaseOportunidad,
                    Origen = asignacion.Origen,
                    Procesado = asignacion.Procesado,
                    FechaRegistroCampania = asignacion.FechaRegistroCampania,
                    IdCategoriaDato = asignacion.IdCategoriaDato,
                    IdTipoInteraccion = asignacion.IdTipoInteraccion,
                    AptoProcesamiento = asignacion.AptoProcesamiento,
                    Estado = asignacion.Estado,
                    UsuarioCreacion = asignacion.UsuarioCreacion,
                    UsuarioModificacion = asignacion.UsuarioModificacion,
                    FechaCreacion = asignacion.FechaCreacion,
                    FechaModificacion = asignacion.FechaModificacion
                };

                var respuesta = _dapperRepository.QuerySPDapper("mkt.SP_InsertarAsignacionAutomaticaTemp", parameters);

                if (respuesta != "null")
                {
                    var dato = JsonConvert.DeserializeObject<List<IdDTO>>(respuesta);

                    return dato[0];
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Margiory Ramirez 
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obterner el NombreCampania por IdFase Oportunidad
        /// </summary>
        /// <param name="IdFaseOportunidad"> IdFaseOportunidad </param>
        /// <returns></returns> 
        public AsignacionAutomaticaTemModeloDTO ObtenerNuevosRegistroById(string idRegistroPortalWeb, int idPagina)
        {
            AsignacionAutomaticaTemModeloDTO Registro = new AsignacionAutomaticaTemModeloDTO();
            var RegistroDB = _dapperRepository.QuerySPFirstOrDefault("dbo.SP_ObtenerContactoFaseOportunidadNuevoPortal", new { idRegistroPortalWeb, idPagina });
            Registro = JsonConvert.DeserializeObject<AsignacionAutomaticaTemModeloDTO>(RegistroDB);
            return Registro;
        }
       

    }

}


