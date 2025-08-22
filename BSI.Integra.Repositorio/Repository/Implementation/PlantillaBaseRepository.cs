using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaBaseRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_PlantillaBase
    /// </summary>
    public class PlantillaBaseRepository : GenericRepository<TPlantillaBase>, IPlantillaBaseRepository
    {
        private Mapper _mapper;

        public PlantillaBaseRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaBase, PlantillaBase>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPlantillaBase MapeoEntidad(PlantillaBase entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaBase modelo = _mapper.Map<TPlantillaBase>(entidad);

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

        public TPlantillaBase Add(PlantillaBase entidad)
        {
            try
            {
                var PlantillaBase = MapeoEntidad(entidad);
                base.Insert(PlantillaBase);
                return PlantillaBase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaBase Update(PlantillaBase entidad)
        {
            try
            {
                var PlantillaBase = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaBase.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaBase);
                return PlantillaBase;
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


        public IEnumerable<TPlantillaBase> Add(IEnumerable<PlantillaBase> listadoEntidad)
        {
            try
            {
                List<TPlantillaBase> listado = new List<TPlantillaBase>();
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

        public IEnumerable<TPlantillaBase> Update(IEnumerable<PlantillaBase> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaBase> listado = new List<TPlantillaBase>();
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
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PlantillaBase.
        /// </summary>
        /// <returns> List<PlantillaBaseDTO> </returns>
        public IEnumerable<PlantillaBaseDTO> ObtenerPlantillaBase()
        {
            try
            {
                List<PlantillaBaseDTO> rpta = new List<PlantillaBaseDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Descripcion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_PlantillaBase
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaBaseDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaBase para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlantillaBaseComboDTO> </returns>
        public IEnumerable<PlantillaBaseComboDTO> ObtenerCombo()
        {
            try
            {
                List<PlantillaBaseComboDTO> rpta = new List<PlantillaBaseComboDTO>();
                var query = @"SELECT Id,Nombre,Descripcion FROM pla.T_PlantillaBase WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaBaseComboDTO>>(resultado);
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
        /// Obtiene el registro de T_PlantillaBase asociado a un Identificador.
        /// </summary>
        /// <param name="idPlantillaBase">Id de Plantilla Base</param>
        /// <returns> PlantillaBaseDTO </returns>
        public PlantillaBase? ObtenerPorId(int idPlantillaBase)
        {
            try
            {
                var query = @"
                        SELECT
	                        Id	,
						    Nombre,
						    Descripcion,
						    Estado,
						    UsuarioCreacion,
						    UsuarioModificacion,
						    FechaCreacion,
						    FechaModificacion,
						    RowVersion,
						    IdMigracion,
						    EsHorizontal
                        FROM pla.T_PlantillaBase
                        WHERE Estado = 1 AND Id = @idPlantillaBase";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPlantillaBase });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PlantillaBase>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene el idPlantilla por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public PlantillaBaseDTO ObtenerIdPorNombre(string nombre)
        {
            try
            {
                PlantillaBaseDTO idPlantillaBase = new PlantillaBaseDTO();
                string _queryPlantilla = "SELECT Id FROM pla.V_TPlantillaBase_IdNombre WHERE Nombre = @nombre and Estado = 1";
                var plantillaBase = _dapperRepository.FirstOrDefault(_queryPlantilla, new { nombre });
                idPlantillaBase = JsonConvert.DeserializeObject<PlantillaBaseDTO>(plantillaBase);
                return idPlantillaBase;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene el idPlantilla de Speech-Bienvenida por IdActividad y idPlantillaBase
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechBienvenida(int idActividadDetalle, int idPlantillaBase)
        {
            try
            {
                SpeechBienvenidaDespedidaDTO speechBienvenidaDespedida = new SpeechBienvenidaDespedidaDTO()
                {
                    IdPlantillaBienvenida = 0,
                    IdPlantillaDespedida = 0
                };
                string query = "SELECT IdPlantilla AS IdPlantillaBienvenida FROM  pla.V_DetalleActividadSpeechBienvenida WHERE IdActividadDetalle = @idActividadDetalle AND IdPlantillaBase = @idPlantillaBase";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idActividadDetalle, idPlantillaBase });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    speechBienvenidaDespedida = JsonConvert.DeserializeObject<SpeechBienvenidaDespedidaDTO>(resultado)!;
                }
                return speechBienvenidaDespedida;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene el idPlantilla de Speech-Despedidad por IdActividad y idPlantillaBase
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechDespedida(int idActividadDetalle, int idPlantillaBase)
        {
            try
            {
                SpeechBienvenidaDespedidaDTO speechBienvenidaDespedida = new SpeechBienvenidaDespedidaDTO()
                {
                    IdPlantillaBienvenida = 0,
                    IdPlantillaDespedida = 0
                };
                string query = "SELECT IdPlantilla AS IdPlantillaDespedida FROM  pla.V_DetalleActividadSpeechDespedida WHERE IdActividadDetalle = @idActividadDetalle AND IdPlantillaBase = @idPlantillaBase";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idActividadDetalle, idPlantillaBase });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    speechBienvenidaDespedida = JsonConvert.DeserializeObject<SpeechBienvenidaDespedidaDTO>(resultado)!;
                }
                return speechBienvenidaDespedida;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor:Eliot Arias F.
        /// Fecha: 20/01/2025
        /// Version 1.0
        /// <summary>
        /// Obtiene la lista de plantillas de contratos asociados a tipos de contrato
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public IEnumerable<ComboPlantillaContratoDTO> ObtenerPlantillasContrato()
        {
            try
            {
                List<ComboPlantillaContratoDTO> comboPlantillaContratoDTO = new List<ComboPlantillaContratoDTO>();
                string query = "SELECT * FROM [gp].[V_TPlantillaBase_ObtenerPlantillasContrato] WHERE idPlantillaBase = 17;";
                string queryRespuesta = _dapperRepository.QueryDapper(query, null);
                //return JsonConvert.DeserializeObject<List<ComboPlantillaContratoDTO>>(queryRespuesta);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]") && queryRespuesta != "null")
                {
                    comboPlantillaContratoDTO = JsonConvert.DeserializeObject<List<ComboPlantillaContratoDTO>>(queryRespuesta)!;
                    // Después de deserializar, conviertes etiquetasPlantilla a una lista
                    foreach (var item in comboPlantillaContratoDTO)
                    {
                        if (!string.IsNullOrEmpty(item.EtiquetasPlantilla))
                        {
                            // Dividimos por comas y quitamos espacios en blanco
                            item.ListaEtiquetas = item.EtiquetasPlantilla
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(s => s.Trim())
                                .ToList();
                        }
                    }
                }
                return comboPlantillaContratoDTO;
            }
            catch (Exception e)
            {
                throw new Exception($"Error al obtener las plantillas de contrato: {e.Message}");
            }
        }
    }
}
