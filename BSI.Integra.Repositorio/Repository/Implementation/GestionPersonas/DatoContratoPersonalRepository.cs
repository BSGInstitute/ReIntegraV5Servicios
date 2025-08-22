using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class DatoContratoPersonalRepository : GenericRepository<TDatoContratoPersonal>, IDatoContratoPersonalRepository
    {
        private Mapper _mapper;

        public DatoContratoPersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDatoContratoPersonal, DatoContratoPersonal>(MemberList.None).ReverseMap();
                cfg.CreateMap<DatoContratoPersonal, DatoContratoPersonalDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<DatoContratoPersonal, TDatoContratoPersonal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDatoContratoPersonal MapeoEntidad(DatoContratoPersonal entidad)
        {
            try
            {
                //crea la entidad padre
                TDatoContratoPersonal modelo = _mapper.Map<TDatoContratoPersonal>(entidad);

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

        public TDatoContratoPersonal Add(DatoContratoPersonal entidad)
        {
            try
            {
                var DatoContratoPersonal = MapeoEntidad(entidad);
                base.Insert(DatoContratoPersonal);
                return DatoContratoPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDatoContratoPersonal Update(DatoContratoPersonal entidad)
        {
            try
            {
                var DatoContratoPersonal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DatoContratoPersonal.RowVersion = entidadExistente.RowVersion;

                base.Update(DatoContratoPersonal);
                return DatoContratoPersonal;
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


        public IEnumerable<TDatoContratoPersonal> Add(IEnumerable<DatoContratoPersonal> listadoEntidad)
        {
            try
            {
                List<TDatoContratoPersonal> listado = new List<TDatoContratoPersonal>();
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

        public IEnumerable<TDatoContratoPersonal> Update(IEnumerable<DatoContratoPersonal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDatoContratoPersonal> listado = new List<TDatoContratoPersonal>();
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

        /// Autor: Eliot Arias F.
        /// Fecha: 28/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion de contrato por filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<DatoContratoPersonalFiltroDTO> ObtenerContratosPorFiltro(ContratoFiltroDTO filtro)
        {
            try
            {
                var filtros = new
                {
                    ListaPersonalAreaTrabajo = filtro.ListaPersonalAreaTrabajo == null ? "" : string.Join(",", filtro.ListaPersonalAreaTrabajo.Select(x => x)),
                    ListaPuestoTrabajo = filtro.ListaPuestoTrabajo == null ? "" : string.Join(",", filtro.ListaPuestoTrabajo.Select(x => x)),
                    ListaPersonal = filtro.ListaPersonal == null ? "" : string.Join(",", filtro.ListaPersonal.Select(x => x)),
                    ListaSedeTrabajo = filtro.ListaSedeTrabajo == null ? "" : string.Join(",", filtro.ListaSedeTrabajo.Select(x => x)),
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    OpcionFecha = filtro.OpcionFecha
                };

                List<DatoContratoPersonalFiltroDTO> contratoFiltro = new List<DatoContratoPersonalFiltroDTO>();
                string query = string.Empty;
                query = "gp.SP_ObtenerContratos";
                var PContratoDB = _dapperRepository.QuerySPDapper(query, filtros);

                if (!string.IsNullOrEmpty(PContratoDB) && !PContratoDB.Contains("[]"))
                {
                    contratoFiltro = JsonConvert.DeserializeObject<List<DatoContratoPersonalFiltroDTO>>(PContratoDB);
                }
                return contratoFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 2/01/2025
        /// Versión: 2.0
        /// <summary>
        /// Obtiene informacion de contrato por filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<DatoContratoPersonalFiltroDTO> ObtenerContratosPorFiltrov2(ContratoFiltroDTO filtro)
        {
            try
            {
                var sqlBuilder = new StringBuilder();
                sqlBuilder.AppendLine("SELECT * FROM [gp].[V_ObtenerContratosVigentes] WHERE 1=1");

                bool seAplicoFiltro = false;

                var parametros = new DynamicParameters();

                // Filtrar por ListaPersonalAreaTrabajo
                if (filtro.ListaPersonalAreaTrabajo != null && filtro.ListaPersonalAreaTrabajo.Any())
                {
                    sqlBuilder.AppendLine("AND IdPersonalAreaTrabajo IN @ListaPersonalAreaTrabajo");
                    parametros.Add("@ListaPersonalAreaTrabajo", filtro.ListaPersonalAreaTrabajo);
                    seAplicoFiltro = true;
                }

                // Filtrar por ListaPuestoTrabajo
                if (filtro.ListaPuestoTrabajo != null && filtro.ListaPuestoTrabajo.Any())
                {
                    sqlBuilder.AppendLine("AND IdPuestoTrabajo IN @ListaPuestoTrabajo");
                    parametros.Add("@ListaPuestoTrabajo", filtro.ListaPuestoTrabajo);
                    seAplicoFiltro = true;
                }

                // Filtrar por ListaPersonal
                if (filtro.ListaPersonal != null && filtro.ListaPersonal.Any())
                {
                    sqlBuilder.AppendLine("AND IdPersonal IN @ListaPersonal");
                    parametros.Add("@ListaPersonal", filtro.ListaPersonal);
                    seAplicoFiltro = true;
                }

                // Filtrar por ListaSedeTrabajo
                if (filtro.ListaSedeTrabajo != null && filtro.ListaSedeTrabajo.Any())
                {
                    sqlBuilder.AppendLine("AND IdSedeTrabajo IN @ListaSedeTrabajo");
                    parametros.Add("@ListaSedeTrabajo", filtro.ListaSedeTrabajo);
                    seAplicoFiltro = true;
                }

                //// Filtrar por fechas (FechaInicio y FechaFin)
                //if (filtro.FechaInicio.HasValue)
                //{
                //    sqlBuilder.AppendLine("AND FechaInicio >= @FechaInicio");
                //    parametros.Add("@FechaInicio", filtro.FechaInicio.Value);
                //}

                //if (filtro.FechaFin.HasValue)
                //{
                //    sqlBuilder.AppendLine("AND FechaFin <= @FechaFin");
                //    parametros.Add("@FechaFin", filtro.FechaFin.Value);
                //}

                //// Manejo de fechas basado en OpcionFecha
                //if (filtro.OpcionFecha.HasValue)
                //{
                //    switch (filtro.OpcionFecha.Value)
                //    {
                //        case 1: // Solo considerar FechaInicio
                //            if (filtro.FechaInicio.HasValue)
                //            {
                //                sqlBuilder.AppendLine("AND FechaInicio >= @FechaInicio");
                //                parametros.Add("@FechaInicio", filtro.FechaInicio.Value);
                //                seAplicoFiltro = true;
                //            }
                //            break;

                //        case 2: // Solo considerar FechaFin
                //            if (filtro.FechaFin.HasValue)
                //            {
                //                sqlBuilder.AppendLine("AND FechaFin <= @FechaFin");
                //                parametros.Add("@FechaFin", filtro.FechaFin.Value);
                //                seAplicoFiltro = true;
                //            }
                //            break;

                //        default:
                //            throw new Exception("Opción de fecha no válida");
                //    }
                //}
                //else
                //{
                //    // Si OpcionFecha es null, considerar ambos rangos de fechas
                //    if (filtro.FechaInicio.HasValue)
                //    {
                //        sqlBuilder.AppendLine("AND FechaInicio >= @FechaInicio");
                //        parametros.Add("@FechaInicio", filtro.FechaInicio.Value);
                //        seAplicoFiltro = true;
                //    }

                //    if (filtro.FechaFin.HasValue)
                //    {
                //        sqlBuilder.AppendLine("AND FechaFin <= @FechaFin");
                //        parametros.Add("@FechaFin", filtro.FechaFin.Value);
                //        seAplicoFiltro = true;
                //    }
                //}

                // Manejo de fechas basado en OpcionFecha
                if (filtro.OpcionFecha.HasValue)
                {
                    switch (filtro.OpcionFecha.Value)
                    {
                        case 1: // Contratos cuya fecha inicio entre FechaInicio y FechaFin
                            if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
                            {
                                sqlBuilder.AppendLine("AND FechaInicio BETWEEN @FechaInicio AND @FechaFin");
                                parametros.Add("@FechaInicio", filtro.FechaInicio.Value);
                                parametros.Add("@FechaFin", filtro.FechaFin.Value);
                                seAplicoFiltro = true;
                            }
                            break;

                        case 2: // Contratos cuya FechaFin esté en el rango FechaInicio y FechaFin
                            if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
                            {
                                sqlBuilder.AppendLine("AND FechaFin BETWEEN @FechaInicio AND @FechaFin");
                                parametros.Add("@FechaInicio", filtro.FechaInicio.Value);
                                parametros.Add("@FechaFin", filtro.FechaFin.Value);
                                seAplicoFiltro = true;
                            }
                            break;

                        default:
                            throw new Exception("Opción de fecha no válida");
                    }
                }
                else
                {
                    // Si no se especifica OpcionFecha, usar ambos rangos de fechas de manera predeterminada
                    //if (filtro.FechaInicio.HasValue)
                    //{
                    //    sqlBuilder.AppendLine("AND FechaInicio >= @FechaInicio");
                    //    parametros.Add("@FechaInicio", filtro.FechaInicio.Value);
                    //    seAplicoFiltro = true;
                    //}

                    //if (filtro.FechaFin.HasValue)
                    //{
                    //    sqlBuilder.AppendLine("AND FechaFin <= @FechaFin");
                    //    parametros.Add("@FechaFin", filtro.FechaFin.Value);
                    //    seAplicoFiltro = true;
                    //}
                    sqlBuilder.AppendLine("ORDER BY FechaCreacion DESC");
                    seAplicoFiltro = true;
                }

                // Agregar ORDER BY FechaCreacion DESC si no se aplicó ningún filtro
                if (!seAplicoFiltro)
                {
                    sqlBuilder.AppendLine("ORDER BY FechaCreacion DESC");
                }


                // Ejecutar la consulta
                var query = sqlBuilder.ToString();
                var resultadoDB = _dapperRepository.QueryDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<DatoContratoPersonalFiltroDTO>>(resultadoDB);
                }

                return new List<DatoContratoPersonalFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener contratos por filtro: {ex.Message}", ex);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 06/01/2025
        /// Versión: 2.0
        /// <summary>
        /// Obtiene datos personales para Formulario de Contrato
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public DatosFormularioPersonalDTO ObtenerDatosPersonalesFormulario(int IdPersonal)
        {
            try
            {
                DatosFormularioPersonalDTO datoContrato = new DatosFormularioPersonalDTO();
                string query = string.Empty;
                query = "SELECT * FROM [gp].[V_TPersonal_DatosPersonales] WHERE idPersonal = @idPersonal";
                var PContratoDB = _dapperRepository.FirstOrDefault(query, new { idPersonal = IdPersonal });

                if (!string.IsNullOrEmpty(PContratoDB) && !PContratoDB.Contains("[]"))
                {
                    datoContrato = JsonConvert.DeserializeObject<DatosFormularioPersonalDTO>(PContratoDB);
                }
                return datoContrato;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener datos personales de formulario: {ex.Message}", ex);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 16/01/2025
        /// Versión: 2.0
        /// <summary>
        /// Obtiene los datos de remuneración variable
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<DatosRemuneracionVariableDTO> ObtenerComboDatosRemuneracionVariable()
        {
            try
            {
                List<DatosRemuneracionVariableDTO> listaDatos = new List<DatosRemuneracionVariableDTO>();
                string query = string.Empty;
                query = "SELECT * FROM gp.V_TPuestoTrabajoRemuneracionDetalle_ObtenerDatosRemuneracion WHERE Estado = 1";
                var respuesta = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaDatos = JsonConvert.DeserializeObject<List<DatosRemuneracionVariableDTO>>(respuesta);
                }
                return listaDatos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los datos remuneracion variable por puesto: {ex.Message}", ex);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 16/01/2025
        /// Versión: 2.0
        /// <summary>
        /// Obtiene combo remuneracionTipo
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<ComboDTO> ObtenerComboRemuneracionTipo()
        {
            try
            {
                List<ComboDTO> listaDatos = new List<ComboDTO>();
                string query = string.Empty;
                query = "SELECT * FROM gp.T_RemuneracionTipo WHERE Estado = 1;";
                var respuesta = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaDatos = JsonConvert.DeserializeObject<List<ComboDTO>>(respuesta);
                }
                return listaDatos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los datos remuneracion variable por puesto: {ex.Message}", ex);
            }
        }



        public DatoContratoPersonalDTO ObtenerByIdPersonal(int idPersonal)
        {
            try
            {
                var query = @"
                    SELECT Id,
                       IdPersonal,
                       IdTipoContrato,
                       EstadoContrato,
                       FechaInicio,
                       FechaFin,
                       RemuneracionFija,
                       IdTipoPagoRemuneracion,
                       IdEntidadFinanciera_Pago,
                       NumeroCuentaPago,
                       IdPuestoTrabajo,
                       IdSedeTrabajo,
                       IdPersonalAreaTrabajo,
                       IdCargo,
                       IdTipoPerfil,
                       IdPersonal_Jefe,
                       IdEntidadFinanciera_Cts,
                       NumeroCuentaCts,
                       EsPeridoPrueba,
                       FechaFinPeriodoPrueba,
                       IdContratoEstado,
                       UrlDocumentoContrato FROM gp.T_DatoContratoPersonal
                    WHERE IdPersonal=@idPersonal AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<DatoContratoPersonalDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
