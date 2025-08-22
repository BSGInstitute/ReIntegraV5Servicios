using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PeriodoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_Periodo
    /// </summary>
    public class PeriodoRepository : GenericRepository<TPeriodo>, IPeriodoRepository
    {
        private Mapper _mapper;

        public PeriodoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPeriodo, Periodo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPeriodo MapeoEntidad(Periodo entidad)
        {
            try
            {
                //crea la entidad padre
                TPeriodo modelo = _mapper.Map<TPeriodo>(entidad);

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

        public TPeriodo Add(Periodo entidad)
        {
            try
            {
                var Periodo = MapeoEntidad(entidad);
                base.Insert(Periodo);
                return Periodo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPeriodo Update(Periodo entidad)
        {
            try
            {
                var Periodo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Periodo.RowVersion = entidadExistente.RowVersion;

                base.Update(Periodo);
                return Periodo;
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


        public IEnumerable<TPeriodo> Add(IEnumerable<Periodo> listadoEntidad)
        {
            try
            {
                List<TPeriodo> listado = new List<TPeriodo>();
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

        public IEnumerable<TPeriodo> Update(IEnumerable<Periodo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPeriodo> listado = new List<TPeriodo>();
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Periodo.
        /// </summary>
        /// <returns> List<PeriodoDTO> </returns>
        public IEnumerable<PeriodoDTO> ObtenerPeriodo()
        {
            try
            {
                List<PeriodoDTO> rpta = new List<PeriodoDTO>();
                var query = @"
                   SELECT [Id]
                          ,[Nombre]
	                      ,[FechaInicial]
                          ,[FechaFin]
                          ,[FechaInicialFinanzas]
                          ,[FechaFinFinanzas]
	                      ,[FechaInicialRepIngresos]
                          ,[FechaFinRepIngresos]
                    FROM [mkt].[T_Periodo]
                    WHERE [Estado] = 1
                    ORDER BY [FechaInicial] DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PeriodoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Periodo para mostrarse en combo.
        /// </summary>
        /// <returns> List<PeriodoComboDTO> </returns>
        public IEnumerable<PeriodoComboDTO> ObtenerCombo()
        {
            try
            {
                List<PeriodoComboDTO> rpta = new List<PeriodoComboDTO>();
                var query = @"SELECT [Id]
                                  ,[Nombre]
                                  ,[FechaCreacion]
                            FROM [mkt].[T_Periodo]
                            WHERE [Estado] = 1
                            ORDER BY [FechaCreacion] DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PeriodoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene id Periodo de la fecha actual.
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerIdPeriodoFechaActual()
        {
            try
            {
                var personalPeriodos = new List<FiltroDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_Periodo where getdate() between fechainicial and fechafin";
                var personalDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    personalPeriodos = JsonConvert.DeserializeObject<List<FiltroDTO>>(personalDB);
                }
                return personalPeriodos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene fecha inicial del periodo para un int?
        /// </summary>
        /// <returns></returns>
        public DateTime ObtenerFechaInicialNulo(int? IdPeriodo)
        {
            try
            {
                DateTime lista = new DateTime();

                lista = GetBy(x => x.Id == IdPeriodo).Select(y => y.FechaInicial).FirstOrDefault();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene fecha final del periodo
        /// </summary>
        /// <returns></returns>
        public DateTime ObtenerFechaFinalNulo(int? IdPeriodo)
        {
            try
            {
                DateTime lista = new DateTime();

                lista = GetBy(x => x.Id == IdPeriodo).Select(y => y.FechaFin).FirstOrDefault();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene toda la lista de Periodos 
        /// </summary>
        /// <returns> List<PeriodoFiltroDTO> </returns>
        public List<PeriodoFiltroDTO> ObtenerCombo2()
        {
            try
            {
                var rpta = new List<PeriodoFiltroDTO>();
                var query = @"SELECT Id, Nombre, FechaInicial, FechaFin, FechaCreacion 
                            FROM mkt.T_Periodo WHERE Estado = 1 ORDER BY FechaCreacion desc";

                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PeriodoFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Carlos Crispin R.
        ///Fecha: 17/03/2021
        /// <summary>
        /// Obtiene Personal responable para marketing por Filtro
        /// </summary>
        /// <returns>Lista de Personal de Marketing</returns>
        public List<FiltroCombosDTO> ObtenerPersonalMarketingFiltro()
        {
            try
            {
                return null;
                //return GetBy(x => x.Estado == true && x.Activo == true && x.Rol == "Marketing", x => new FiltroCombosDTO { Id = x.Id, Nombre = string.Concat(x.Nombres, " ", x.Apellidos) }).ToList();
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
        /// Obtiene toda la lista de Periodos para llenar en un combo en donde el estado es true
        /// </summary>
        /// <returns> Lista de periodos FiltroDTO </returns>
        public List<FiltroDTO> ObtenerPeriodosPendiente()
        {
            try
            {
                List<FiltroDTO> personalPeriodos = new List<FiltroDTO>();
                var query = @"
                            SELECT 
                                Id, Nombre 
                            FROM 
                                mkt.T_Periodo 
                            WHERE 
                                Estado = 1 ORDER BY Id DESC";
                var personalDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    personalPeriodos = JsonConvert.DeserializeObject<List<FiltroDTO>>(personalDB)!;
                }
                return personalPeriodos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene fecha inicial del periodo.
        /// </summary>
        /// <returns> DateTime: lista </returns>
        public StringDTO ObtenerFechaInicial(int idPeriodo)
        {
            try
            {
                StringDTO lista = new StringDTO();
                var query = @"
                            SELECT 
                                FechaInicial as Valor
                            FROM 
                                mkt.T_Periodo 
                            WHERE 
                                Id = @IdPeriodo AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPeriodo = idPeriodo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    lista = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene fecha final del periodo
        /// </summary>
        /// <returns> DateTime: lista </returns>
        public StringDTO ObtenerFechaFinal(int idPeriodo)
        {
            try
            {
                StringDTO lista = new StringDTO();
                var query = @"
                            SELECT 
                                FechaFin as Valor
                            FROM 
                                mkt.T_Periodo 
                            WHERE 
                                Id = @IdPeriodo AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPeriodo = idPeriodo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene toda la lista de Periodos para llenar un combo box
        /// </summary>
        /// <returns></returns>
        public List<PeriodoFiltroDTO> ObtenerPeriodos()
        {
            try
            {
                List<PeriodoFiltroDTO> lista = new List<PeriodoFiltroDTO>();

                lista = GetBy(x => x.Estado == true, y => new PeriodoFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    FechaInicial = y.FechaInicial,
                    FechaFin = y.FechaFin,
                    FechaCreacion = y.FechaCreacion
                }).OrderByDescending(x => x.FechaCreacion).ToList();


                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: margiory Ramirez
        /// Fecha: 11/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de todos los registros.
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroIdNombreDTO</returns>
        /// 
      
        public List<FiltroIdNombreDTO> ObtenerUltimoPeriodo()
        {
            try
            {
                List<FiltroIdNombreDTO> personalPeriodos = new List<FiltroIdNombreDTO>();
                var query = @"
                            SELECT 
                                Id,Nombre
                            FROM 
                                mkt.T_Periodo 
                            WHERE 
                                Estado = 1 ORDER BY FechaInicial DESC";
                var personalDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    personalPeriodos = JsonConvert.DeserializeObject<List<FiltroIdNombreDTO>>(personalDB)!;
                }
                return personalPeriodos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
