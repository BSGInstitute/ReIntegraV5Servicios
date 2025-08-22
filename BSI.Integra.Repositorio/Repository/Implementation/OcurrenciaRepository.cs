using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OcurrenciaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_Ocurrencia
    /// </summary>
    public class OcurrenciaRepository : GenericRepository<TOcurrencium>, IOcurrenciaRepository
    {
        private Mapper _mapper;

        public OcurrenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOcurrencium, Ocurrencia>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOcurrencium MapeoEntidad(Ocurrencia entidad)
        {
            try
            {
                //crea la entidad padre
                TOcurrencium modelo = _mapper.Map<TOcurrencium>(entidad);

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

        public TOcurrencium Add(Ocurrencia entidad)
        {
            try
            {
                var Ocurrencia = MapeoEntidad(entidad);
                base.Insert(Ocurrencia);
                return Ocurrencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOcurrencium Update(Ocurrencia entidad)
        {
            try
            {
                var Ocurrencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Ocurrencia.RowVersion = entidadExistente.RowVersion;

                base.Update(Ocurrencia);
                return Ocurrencia;
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


        public IEnumerable<TOcurrencium> Add(IEnumerable<Ocurrencia> listadoEntidad)
        {
            try
            {
                List<TOcurrencium> listado = new List<TOcurrencium>();
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

        public IEnumerable<TOcurrencium> Update(IEnumerable<Ocurrencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOcurrencium> listado = new List<TOcurrencium>();
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
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Ocurrencia.
        /// </summary>
        /// <returns> List<OcurrenciaDTO> </returns>
        public IEnumerable<OcurrenciaDTO> ObtenerOcurrencia()
        {
            try
            {
                List<OcurrenciaDTO> rpta = new List<OcurrenciaDTO>();
                var query = @"
                    SELECT Id,Nombre,NombreM,NombreCS,IdFaseOportunidad,IdActividadCabecera,IdPlantilla_Speech,IdEstadoOcurrencia,Oportunidad,RequiereLlamada,
	                    Roles,Color,IdPersonalAreaTrabajo,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_Ocurrencia
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OcurrenciaDTO>>(resultado);
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
        /// Obtiene el registro de T_Ocurrencia asociado al Identificador.
        /// </summary>
        /// <param name="idOcurrencia"> Id de la Ocurrencia </param>
        /// <returns> OcurrenciaDTO </returns>
        public OcurrenciaDTO ObtenerPorId(int idOcurrencia)
        {
            try
            {
                OcurrenciaDTO rpta = new OcurrenciaDTO();
                var query = @"
                    SELECT Id,Nombre,NombreM,NombreCS,IdFaseOportunidad,IdActividadCabecera,IdPlantilla_Speech,IdEstadoOcurrencia,Oportunidad,RequiereLlamada,
	                    Roles,Color,IdPersonalAreaTrabajo,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_Ocurrencia
                    WHERE Estado = 1 AND Id = @idOcurrencia";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOcurrencia });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<OcurrenciaDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Ocurrencia para mostrarse en combo.
        /// </summary>
        /// <returns> List<OcurrenciaComboDTO> </returns>
        public IEnumerable<OcurrenciaComboDTO> ObtenerCombo()
        {
            try
            {
                List<OcurrenciaComboDTO> rpta = new List<OcurrenciaComboDTO>();
                var query = @"SELECT Id,Nombre FROM com.T_Ocurrencia WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OcurrenciaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Ocurrencias de Actividad por Ocurrencia Alterno
        /// </summary>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> List<HojaActividadesDTO> </returns>
        public List<HojaActividadesDTO> ObtenerHojaActividadesPorIdOcurrenciaAlterno(int idOcurrencia)
        {
            try
            {
                List<HojaActividadesDTO> rpta = new List<HojaActividadesDTO>();
                var query = @"
                    SELECT Id,TipoActividad,Actividad,FechaProgramada,IdOcurrencia,OcurrenciaPadre
                    FROM com.V_HojaGetActividadesByOcurrenciaAlterno
                    WHERE IdOcurrencia = @idOcurrencia AND OcurrenciaPadre = -1";
                var resultado = _dapperRepository.QueryDapper(query, new { idOcurrencia });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<HojaActividadesDTO>>(resultado);
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
        /// Devuelve 1 o 0 si la Ocurrencia debe cambiar su estado.
        /// </summary>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> bool </returns>
        public bool ValidarEstadoOcurrencia(int idOcurrencia)
        {
            try
            {
                string consultaOcurrencia = "Select Id From com.V_TOcurrencia_ValidarCambiarEstado Where Id = @IdOcurrencia and Estado = 1";
                var queryOcurrencia = _dapperRepository.QueryDapper(consultaOcurrencia, new { IdOcurrencia = idOcurrencia });
                if (!string.IsNullOrEmpty(queryOcurrencia) && !queryOcurrencia.Contains("[]"))
                    return true;
                else
                    return false;
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
        /// Devuelve 1 o 0 si la Ocurrencia debe cambiar su estado.
        /// </summary>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> bool </returns>
        public async Task<bool> ValidarEstadoOcurrenciaAsync(int idOcurrencia)
        {
            try
            {
                string consultaOcurrencia = "SELECT Id FROM com.V_TOcurrencia_ValidarCambiarEstado WHERE Id = @IdOcurrencia AND Estado = 1";
                var queryOcurrencia = await _dapperRepository.QueryDapperAsync(consultaOcurrencia, new { IdOcurrencia = idOcurrencia });
                if (!string.IsNullOrEmpty(queryOcurrencia) && !queryOcurrencia.Contains("[]"))
                    return true;
                else
                    return false;
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
        /// Valida si pertenece a Workshop o Prelanzamiento
        /// </summary>
        /// <param name="idCategoria"> Id de Categoría </param>
        /// <returns> int </returns>
        public int ValidarGrupoPreLanzamiento(int idCategoria)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerGrupoAgenda", new { idCategoria });
                var ocurrencias = JsonConvert.DeserializeObject<Dictionary<string, int>>(resultado);
                return ocurrencias.Select(x => x.Value).FirstOrDefault();
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
        /// Valida si pertenece a Workshop o Prelanzamiento
        /// </summary>
        /// <param name="idCategoria"> Id de Categoría </param>
        /// <returns> int </returns>
        public async Task<int> ValidarGrupoPreLanzamientoAsync(int idCategoria)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync("com.SP_ObtenerGrupoAgenda", new { idCategoria });
                var ocurrencias = JsonConvert.DeserializeObject<Dictionary<string, int>>(resultado);
                return ocurrencias.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Información de Ocurrencia por Id
        /// </summary>
        /// <param name="idOcurrencia"> Id de Ocurrencia </param>
        /// <returns> Ocurrencia </returns>
        public Ocurrencia ObtenerOcurrenciaPorActividad(int idOcurrencia)
        {
            try
            {
                string queryOcurrencia = "Select * From com.T_OcurrenciaReporte Where Id=@IdOcurrencia";
                var resultado = _dapperRepository.FirstOrDefault(queryOcurrencia, new { IdOcurrencia = idOcurrencia });
                return JsonConvert.DeserializeObject<Ocurrencia>(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: margiory Ramirez.
        ///Fecha: 06/02/2021
        /// <summary>
        /// Obtener IdOcurrencia por nombre
        /// </summary>
        /// <param name="nombre"> Nombre de Ocurrencia </param>
        /// <returns> Id Ocurrencia : OcurrenciaBO </returns>
        public int ObtenerOcurrenciaPorNombre(string nombre)
        {
            try
            {
                string query = "Select top 1 Id From com.T_Ocurrencia Where Nombre= @nombre";
                var queryOcurrenciaNombre = _dapperRepository.FirstOrDefault(query, new { nombre });
                var queryOcurrencia = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryOcurrenciaNombre);


                if (queryOcurrenciaNombre != null && queryOcurrenciaNombre != "")
                {
                    return queryOcurrencia.Select(w => w.Value).FirstOrDefault();
                }
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
