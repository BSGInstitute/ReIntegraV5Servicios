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
    /// Repositorio: ModeloDataMiningRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión general de T_ModeloDataMining
    /// </summary>
    public class ModeloDataMiningRepository : GenericRepository<TModeloDataMining>, IModeloDataMiningRepository
    {
        private Mapper _mapper;

        public ModeloDataMiningRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloDataMining, ModeloDataMining>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModeloDataMining MapeoEntidad(ModeloDataMining entidad)
        {
            try
            {
                //crea la entidad padre
                TModeloDataMining modelo = _mapper.Map<TModeloDataMining>(entidad);

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
        public TModeloDataMining Add(ModeloDataMining entidad)
        {
            try
            {
                var AlumnoLog = MapeoEntidad(entidad);
                base.Insert(AlumnoLog);
                return AlumnoLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TModeloDataMining Update(ModeloDataMining entidad)
        {
            try
            {
                var AlumnoLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AlumnoLog.RowVersion = entidadExistente.RowVersion;

                base.Update(AlumnoLog);
                return AlumnoLog;
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
        public IEnumerable<TModeloDataMining> Add(IEnumerable<ModeloDataMining> listadoEntidad)
        {
            try
            {
                List<TModeloDataMining> listado = new List<TModeloDataMining>();
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

        public IEnumerable<TModeloDataMining> Update(IEnumerable<ModeloDataMining> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloDataMining> listado = new List<TModeloDataMining>();
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
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de modeloDataMining por la oportunidad
        /// </summary>
        /// <param name="idOportunidad"> id de la oportunidad </param>
        /// <returns> Entidad List<ModeloDataMining> </returns>
        public List<ValorIntDTO> ObtenerListaPorOportunidad(int idOportunidad)
        {
            var rpta = new List<ValorIntDTO>();
            string query = @"SELECT Id AS Valor
                            FROM com.T_ModeloDataMining 
                            WHERE Estado=1  AND IdOportunidad = @idOportunidad";
            string resultado = _dapperRepository.QueryDapper(query, new { idOportunidad });
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                rpta = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
            return rpta;
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el modeloDataMining por la oportunidad
        /// </summary>
        /// <param name="idOportunidad"> id de la oportunidad </param>
        /// <returns> Entidad ModeloDataMining </returns>
        public ModeloDataMining ObtenerPorOportunidad(int idOportunidad)
        {
            var rpta = new ModeloDataMining();
            string _query = @"SELECT
                                Id,
	                            ProbabilidadInicial,
	                            IdProbabilidadRegistroPW_Inicial AS IdProbabilidadRegistroPwInicial,
	                            ProbabilidadActual,
	                            IdProbabilidadRegistroPW_Actual AS IdProbabilidadRegistroPwActual,
	                            IdOportunidad,
	                            IdAlumno,
	                            FechaCreacionContacto,
	                            FechaCreacionOportunidad,
	                            DiasEntreCreacionContactoOportunidad,
	                            Nombres,
	                            Apellidos,
	                            IdCiudad,
	                            IdPais,
	                            IdCargo,
	                            IdAreaFormacion,
	                            IdAreaTrabajo,
	                            IdIndustria,
	                            Email1,
	                            TelefonoFijo,
	                            TelefonoMovil,
	                            IdEmpresa,
	                            IdTamanioEmpresa,
	                            CIIUEmpresa,
	                            TelefonoEmpresa,
	                            IdCiudadEmpresa,
	                            IdPaisEmpresa,
	                            IdCentroCosto,
	                            IdArea,
	                            IdAreaCapacitacion,
	                            IdSubArea,
	                            IdSubAreaCapacitacion,
	                            IdPGeneral,
	                            IdCategoriaPrograma,
	                            ProgramaGeneralDuracion,
	                            IdPartner,
	                            IdPEspecifico,
	                            Modalidad,
	                            PrecioProgramaEspecifico,
	                            PrecioProgramaEspecificoDolares,
	                            MonedaPrecioProgramaEspecifico,
	                            IdTipoDato,
	                            IdCategoriaOrigen,
	                            IdOrigen,
	                            FaseMaximaAlcanzada,
	                            FaseActual,
	                            IdActividadFinal,
	                            IdOcurrenciaFinal,
	                            IdPersonal,
	                            IdSubCategoriaDato
                            FROM com.T_ModeloDataMining 
                            WHERE Estado=1  AND IdOportunidad = @IdOportunidad";
            string query = _dapperRepository.FirstOrDefault(_query, new { IdOportunidad = idOportunidad });
            if (!string.IsNullOrEmpty(query) && query != "null")
            {
                rpta = JsonConvert.DeserializeObject<ModeloDataMining>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la probabilidad de la oportunidad
        /// </summary>
        /// <param name="idOportunidad"> id de la oportunidad </param>
        /// <returns> Entidad ProbabilidadModeloDataMiningDTO </returns>
        public ProbabilidadModeloDataMiningDTO ObtenerProbabilidad(int idOportunidad)
        {
            try
            {
                var valorModeloDataMining = new ProbabilidadModeloDataMiningDTO();
                var valorModeloDataMiningDB = _dapperRepository.QuerySPFirstOrDefault("com.SP_CalcularProbabilidad", new { idOportunidad });

                if (!string.IsNullOrEmpty(valorModeloDataMiningDB) && !valorModeloDataMiningDB.Contains("[]") && !valorModeloDataMiningDB.Contains("null"))
                {
                    valorModeloDataMining = JsonConvert.DeserializeObject<ProbabilidadModeloDataMiningDTO>(valorModeloDataMiningDB);
                }
                return valorModeloDataMining;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloDataMining ObtenerPorId(int id)
        {
            var rpta = new ModeloDataMining();
            string _query = @"SELECT    Id,
                                        ProbabilidadInicial,
                                        IdProbabilidadRegistroPW_Inicial AS IdProbabilidadRegistroPwInicial,
                                        ProbabilidadActual,
                                        IdProbabilidadRegistroPW_Actual AS IdProbabilidadRegistroPwActual,
                                        IdOportunidad,
                                        IdAlumno,
                                        FechaCreacionContacto,
                                        FechaCreacionOportunidad,
                                        DiasEntreCreacionContactoOportunidad,
                                        Nombres,
                                        Apellidos,
                                        IdCiudad,
                                        IdPais,
                                        IdCargo,
                                        IdAreaFormacion,
                                        IdAreaTrabajo,
                                        IdIndustria,
                                        Email1,
                                        TelefonoFijo,
                                        TelefonoMovil,
                                        IdEmpresa,
                                        IdTamanioEmpresa,
                                        CIIUEmpresa,
                                        TelefonoEmpresa,
                                        IdCiudadEmpresa,
                                        IdPaisEmpresa,
                                        IdCentroCosto,
                                        IdArea,
                                        IdAreaCapacitacion,
                                        IdSubArea,
                                        IdSubAreaCapacitacion,
                                        IdPGeneral,
                                        IdCategoriaPrograma,
                                        ProgramaGeneralDuracion,
                                        IdPartner,
                                        IdPEspecifico,
                                        Modalidad,
                                        PrecioProgramaEspecifico,
                                        PrecioProgramaEspecificoDolares,
                                        MonedaPrecioProgramaEspecifico,
                                        IdTipoDato,
                                        IdCategoriaOrigen,
                                        IdOrigen,
                                        FaseMaximaAlcanzada,
                                        FaseActual,
                                        IdActividadFinal,
                                        IdOcurrenciaFinal,
                                        IdPersonal,
                                        IdSubCategoriaDato,
                                        Estado,
                                        UsuarioCreacion,
                                        UsuarioModificacion,
                                        FechaCreacion,
                                        FechaModificacion,
                                        RowVersion,
                                        IdMigracion
                            FROM com.T_ModeloDataMining 
                            WHERE Estado=1  AND Id = @Id";
            string query = _dapperRepository.FirstOrDefault(_query, new { Id = id });
            if (!string.IsNullOrEmpty(query) && query != "null")
            {
                rpta = JsonConvert.DeserializeObject<ModeloDataMining>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }
    }
}
