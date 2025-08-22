using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CiudadRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Ciudad
    /// </summary>
    public class CiudadRepository : GenericRepository<TCiudad>, ICiudadRepository
    {
        private Mapper _mapper;

        public CiudadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCiudad, Ciudad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TCiudad MapeoEntidad(Ciudad entidad)
        {
            try
            {
                //crea la entidad padre
                TCiudad modelo = _mapper.Map<TCiudad>(entidad);

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

        public TCiudad Add(Ciudad entidad)
        {
            try
            {
                var Ciudad = MapeoEntidad(entidad);
                base.Insert(Ciudad);
                return Ciudad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCiudad Update(Ciudad entidad)
        {
            try
            {
                var Ciudad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Ciudad.RowVersion = entidadExistente.RowVersion;

                base.Update(Ciudad);
                return Ciudad;
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


        public IEnumerable<TCiudad> Add(IEnumerable<Ciudad> listadoEntidad)
        {
            try
            {
                List<TCiudad> listado = new List<TCiudad>();
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

        public IEnumerable<TCiudad> Update(IEnumerable<Ciudad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCiudad> listado = new List<TCiudad>();
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
        /// Obtiene todos los registros de T_Ciudad
        /// </summary>
        /// <returns> List<CiudadDTO> </returns>
        public IEnumerable<CiudadAlternoDTO> Obtener()
        {
            try
            {
                List<CiudadAlternoDTO> rpta = new List<CiudadAlternoDTO>();
                var query = @"SELECT Id, Codigo, Nombre, IdPais, LongCelular, LongTelefono, LongCelularAlterno
                            FROM conf.T_Ciudad
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CiudadAlternoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Ciudad
        /// </summary>
        /// <returns> List<CiudadDTO> </returns>
        public async Task<IEnumerable<CiudadAlternoDTO>> ObtenerAsync()
        {
            try
            {
                List<CiudadAlternoDTO> rpta = new List<CiudadAlternoDTO>();
                var query = @"SELECT Id, Codigo, Nombre, IdPais, LongCelular, LongTelefono, LongCelularAlterno
                            FROM conf.T_Ciudad
                            WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CiudadAlternoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Ciudad para mostrarse en combo.
        /// </summary>
        /// <returns> List<CiudadComboDTO> </returns>
        public IEnumerable<CiudadComboDTO> ObtenerCombo()
        {
            try
            {
                List<CiudadComboDTO> respuesta = new List<CiudadComboDTO>();

                var query = "SELECT Id, Nombre, IdPais,Codigo FROM conf.T_Ciudad WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<CiudadComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto huaman.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RegionCiudad para mostrarse en combo.
        /// </summary>
        /// <returns> List<RegionCiudadComboDTO> </returns>
        public IEnumerable<RegionCiudadComboDTO> ObtenerComboRegionCiudad()
        {
            try
            {
                List<RegionCiudadComboDTO> rpta = new List<RegionCiudadComboDTO>();

                var query = "SELECT Id, Nombre,IdCiudad,IdPais FROM conf.[T_RegionCiudad] WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegionCiudadComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Ciudad
        /// </summary>
        /// <returns> List<CiudadDTO> </returns>
        public IEnumerable<CiudadDTO> ObtenerCiudad()
        {
            try
            {
                List<CiudadDTO> rpta = new List<CiudadDTO>();
                var query = @"SELECT Id, Codigo, Nombre, IdPais, LongCelular, LongTelefono, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, LongCelularAlterno
                            FROM conf.T_Ciudad
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CiudadDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Nombre de la Ciudad asociado a un Id
        /// </summary>
        /// <param name="idCiudad">Id de la Ciudad</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerNombreCiudadPorId(int idCiudad)
        {
            try
            {
                StringDTO respuesta = new StringDTO();
                var query = @"SELECT Nombre AS Valor FROM conf.T_Ciudad WHERE Id = @IdCiudad AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdCiudad = idCiudad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CiudadEnvioDTO> ObtenerNombreCiudadPorIdPais(int idPais)
        {
            try
            {
                List<CiudadEnvioDTO> rpta = new List<CiudadEnvioDTO>();
                var query = @"SELECT Id, Codigo, Nombre, LongCelular, LongTelefono, LongCelularAlterno, idPais, NombrePais from mkt.V_Ciudad where idPais=" + idPais + "order by id desc";
                var query2 = @"SELECT Id, Codigo, Nombre, LongCelular, LongTelefono, LongCelularAlterno, idPais, NombrePais from mkt.V_Ciudad order by id desc";

                if (idPais != -1)
                {
                    var resultado = _dapperRepository.QueryDapper(query, null);
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    {
                        rpta = JsonConvert.DeserializeObject<List<CiudadEnvioDTO>>(resultado);
                    }
                    return rpta;
                }

                else
                {
                    var resultado = _dapperRepository.QueryDapper(query2, null);
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    {
                        rpta = JsonConvert.DeserializeObject<List<CiudadEnvioDTO>>(resultado);
                    }
                    return rpta;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman 
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Nombre de la Ciudad asociado a un Id, Segun las Sedes Exisentes de BSG
        /// </summary>
        /// <returns> ValorStringDTO </returns>
        public IEnumerable<ComboDTO> ObtenerCiudadesDeSedesExistentes()
        {
            try
            {
                List<ComboDTO> ciudades = new List<ComboDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM fin.V_ObtenerCiudadSedes";
                var ciudadesDB = _dapperRepository.QueryDapper(_query, null);
                ciudades = JsonConvert.DeserializeObject<List<ComboDTO>>(ciudadesDB);
                return ciudades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la longitud del celular correcto según el País
        /// </summary>
        /// <param name="idPais">Id del País</param>
        /// <param name="numeroCelular">Número de celular</param>
        /// <returns> bool </returns>
        public bool LongitudCelularPorPaisCorrecta(int? idPais, string numeroCelular)
        {
            try
            {
                var temp = GetBy(x => x.IdPais == idPais, x => new { x.LongCelular, x.LongCelularAlterno });
                var longitudCorrecta = false;
                var min = temp.Min(x => x.LongCelularAlterno);
                var max = temp.Max(x => x.LongCelular);

                if (numeroCelular.Length >= min && numeroCelular.Length <= max)
                {
                    longitudCorrecta = true;
                }


                return longitudCorrecta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComboDTO> ObtenerCiudadesPorPais(string idPais)
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = @"SELECT Id, Nombre, IdPais, Codigo FROM conf.V_TCiudad_ObtenerDatos  WHERE Estado = 1 and idPais in (select  item from conf.F_Splitstring(@idPais,','))";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idPais });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Christian Quispe
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Ciudad para mostrarse en grilla.
        /// </summary>
        /// <returns> List<CiudadAlternoDTO> </returns>
        public IEnumerable<CiudadAlternoDTO> ObtenerTodoCiudades()
        {
            try
            {
                List<CiudadAlternoDTO> ciudades = new List<CiudadAlternoDTO>();
                var _query = @"
                                SELECT
	                                C.Id AS Id,
	                                C.Codigo AS Codigo,
	                                C.Nombre AS Nombre,
	                                C.IdPais AS IdPais,
	                                P.NombrePais AS NombrePais,
	                                C.LongCelular AS LongCelular,
	                                C.LongTelefono AS LongTelefono
                                FROM conf.T_Ciudad AS C
                                INNER JOIN conf.T_Pais AS P
	                                ON P.Id = C.IdPais
                                WHERE C.Estado = 1 AND P.Estado = 1
                                ORDER BY c.FechaCreacion DESC";
                var ciudadesDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ciudadesDB) && !ciudadesDB.Contains("[]"))
                {
                    ciudades = JsonConvert.DeserializeObject<List<CiudadAlternoDTO>>(ciudadesDB);
                }
                return ciudades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 27/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public Ciudad ObtenerPorId(int id)
        {
            try
            {
                Ciudad rpta = new();
                var query = @"
                        SELECT
                            Id,Codigo,Nombre,IdPais,LongCelular,LongTelefono,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,RowVersion,IdMigracion,LongCelularAlterno
                        FROM conf.T_Ciudad
                        WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Ciudad>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Ciudad para filtros
        /// </summary>
        /// <returns> registros para combo IEnumerable CiudadComboDTO</returns>
        public IEnumerable<ComboDTO> ObtenerCiudadFiltro()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM pla.V_TCiudad_Filtro WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCiudadFiltro(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Ciudad para filtros
        /// </summary>
        /// <returns> registros para combo IEnumerable CiudadComboDTO</returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string query = @"SELECT Id AS Id,
                                       Nombre AS Nombre 
                                FROM [conf].[T_Ciudad]
                                WHERE Estado = 1;";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCiudadFiltro(): {ex.Message}", ex);
            }
        }
        public List<Ciudad> ObtenerPorIds(string ids)
        {
            try
            {
                List<Ciudad> rpta = new();
                var query = @"
                        SELECT
                            Id,Codigo,Nombre,IdPais,LongCelular,LongTelefono,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,RowVersion,IdMigracion,LongCelularAlterno
                        FROM conf.T_Ciudad
                        WHERE Estado = 1 AND Id IN (select Item from conf.F_Splitstring(@ids,','))";
                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<Ciudad>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
		/// Obtiene lista de ciudades con denominacion BS para programa especifico
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerListaCiudadesBs()
        {
            try
            {
                List<FiltroDTO> Listado = new List<FiltroDTO>();
                var query = "SELECT Id, Nombre FROM [conf].[V_TRegionCiudad_ObtenerCiudadBs] WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && res != "null")
                {
                    Listado = JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
                }
                return Listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
		/// Obtiene lista de ciudades con denominacion BS para programa especifico
		/// </summary>
		/// <returns></returns>
		public void insertarColonia(CiudadColoniaDTO dto)
        {
            try
            {
                List<FiltroDTO> Listado = new List<FiltroDTO>();
                var query = "conf.sP_insertarColonia";
                var res = _dapperRepository.QuerySPDapper(query, dto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public ComboDTO? ObtenerMunicipioById(int idMunicipioMexico)
        {
            try
            {
                ComboDTO items = new ComboDTO();
                var _query = @"SELECT Id,Nombre  FROM conf.T_MunicipioMexico WHERE id=@idMunicipioMexico  AND Estado = 1";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idMunicipioMexico });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<ComboDTO>(respuestaDapper);
                    return items;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public ComboDTO? ObtenerAsentammientoById(int idAsentamientoMexico)
        {
            try
            {
                ComboDTO items = new ComboDTO();
                var _query = @"SELECT Id,Nombre  FROM conf.T_AsentamientoMexico WHERE id=@idAsentamientoMexico  AND Estado = 1";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idAsentamientoMexico });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<ComboDTO>(respuestaDapper);
                    return items;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public CodigoPostalAsentammiento? ObtenerCodigoPostalPorIdAsentamiento(int idAsentamientoMexico)
        {
            try
            {
                CodigoPostalAsentammiento items = new CodigoPostalAsentammiento();
                var _query = @"SELECT Id,CodigoPostal  FROM conf.T_AsentamientoMexico WHERE id=@idAsentamientoMexico  AND Estado = 1";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idAsentamientoMexico });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<CodigoPostalAsentammiento>(respuestaDapper);
                    return items;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ComboDTO? ObtenerCiudadMexicoById(int idCiudadMexico)
        {
            try
            {
                ComboDTO items = new ComboDTO();
                var _query = @"SELECT Id,Nombre  FROM conf.T_CiudadMexico WHERE id=@idCiudadMexico  AND Estado = 1";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idCiudadMexico });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<ComboDTO>(respuestaDapper);
                    return items;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComboDTO> ObtenerCiudadMexicoByIdEstadoMexico(int idCiudadRef)
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = @"SELECT IdCiudadMexico AS Id, CiudadMexico AS Nombre FROM conf.V_CodigoPostalMexico
                                WHERE IdCiudad = @idCiudadRef
                                AND IdCiudadMexico IS NOT NULL
                                AND CiudadMexico IS NOT NULL
                                GROUP BY IdCiudadMexico, CiudadMexico";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idCiudadRef });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #region
        //Ficha Datos Postulante
        public List<CiudadDatosDTO> ObtenerCiudadesPorPaisByFichaDato()
        {
            try
            {
                List<CiudadDatosDTO> items = new List<CiudadDatosDTO>();
                var _query = @"SELECT Id, Nombre, IdPais, Codigo FROM conf.V_TCiudad_ObtenerDatos  WHERE Estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<CiudadDatosDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

    }
}
