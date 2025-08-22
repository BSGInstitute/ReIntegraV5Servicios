using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: VerificacionManualDatosRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 10/01/2023
    /// <summary>
    /// Gestión general de T_AsignacionAutomatica
    /// </summary>
    public class VerificacionManualDatosRepository: GenericRepository<TAsignacionAutomatica>, IVerificacionManualDatosRepository
    {
        private Mapper _mapper;

        public VerificacionManualDatosRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionAutomatica, AsignacionAutomatica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de registros para Verificacion Manual Datos
        /// </summary>
        /// <param name="parametro"> Parametro </param>
        /// <returns> String </returns>
        public IEnumerable<VerificacionManualDatosCompuestoDTO> ObtenerDatosVerificacion(FiltroBusquedaVerificacionManualDatosCompuestoDTO paginador)
        {
            try
            {
                var filtros = new object();
                string _queryCondicion = "";
                string[] IdCategoriaOrigen = new string[6];
                string[] IdCentroCosto = new string[6];
                string[] IdProbabilidad = new string[6];
                string[] IdPais = new string[6];
                string[] IdIndustria = new string[6];
                string[] IdFormacion = new string[6];
                string[] IdCargo = new string[6];
                string[] IdTrabajo = new string[6];
                DateTime FechaInicio = new DateTime();
                DateTime FechaFin = new DateTime();

                if (paginador.filtroRegistros != null)
                {
                    FechaFin = DateTime.Parse(paginador.filtroRegistros.FechaFin);
                    FechaInicio = DateTime.Parse(paginador.filtroRegistros.FechaInicio);

                    if (paginador.filtroRegistros.IdCentroCosto != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdCentroCosto in @IdCentroCosto ";
                        IdCentroCosto = paginador.filtroRegistros.IdCentroCosto.Split(",");
                    }
                    if (paginador.filtroRegistros.IdCategoriaDato != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdCategoriaOrigen in @IdCategoriaOrigen ";
                        IdCategoriaOrigen = paginador.filtroRegistros.IdCategoriaDato.Split(",");
                    }
                    if (paginador.filtroRegistros.IdProbabilidad != "")
                    {
                        _queryCondicion = _queryCondicion + "And CodigoProbabilidad in @IdProbabilidad ";
                        IdProbabilidad = paginador.filtroRegistros.IdProbabilidad.Split(",");
                    }
                    if (paginador.filtroRegistros.IdPais != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdPais in @IdPais ";
                        IdPais = paginador.filtroRegistros.IdPais.Split(",");
                    }
                    if (paginador.filtroRegistros.IdIndustria != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdIndustria in @IdIndustria ";
                        IdIndustria = paginador.filtroRegistros.IdIndustria.Split(",");
                    }
                    if (paginador.filtroRegistros.IdCargo != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdCargo in @IdCargo ";
                        IdCargo = paginador.filtroRegistros.IdCargo.Split(",");
                    }
                    if (paginador.filtroRegistros.IdAreaFormacion != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdAreaFormacion in @IdFormacion ";
                        IdFormacion = paginador.filtroRegistros.IdAreaFormacion.Split(",");
                    }
                    if (paginador.filtroRegistros.IdAreaTrabajo != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdAreaTrabajo in @IdTrabajo ";
                        IdTrabajo = paginador.filtroRegistros.IdAreaTrabajo.Split(",");
                    }
                }

                string _queryRegistro = "SELECT Id,IdAlumno,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Telefono,Movil,Correo,AreaFormacion,IdAreaFormacion,Cargo,IdCargo," +
                                        "AreaTrabajo,IdAreaTrabajo,Industria,IdIndustria,FechaCreacion,FechaRegistro,HoraRegistro,NombrePrograma,Centrocosto,IdCentroCosto,TipoDato,IdTipoDato," +
                                        "Categoria,IdCategoriaOrigen,Origen,IdOrigen,OrigenCampania,Formulario,FaseOportunidad,IdFaseOportunidad,Pais,IdPais,Ciudad,IdCiudad,ProbabilidadActual,NombreProbabilidadActual,CodigoProbabilidad,AptoProcesamiento, " +
                                        "OriginalNombre1,OriginalNombre2,OriginalApellidoPaterno,OriginalApellidoMaterno,OriginalTelefono,OriginalMovil,OriginalCorreo,OriginalIdAreaFormacion,OriginalIdCargo,OriginalIdAreaTrabajo,OriginalIdIndustria " +
                                        " FROM mkt.V_VerificacionManualDatos WHERE FechaCreacion BETWEEN @FechaInicio AND @FechaFin and AptoProcesamiento=1" + _queryCondicion + "order by FechaCreacion desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                string queryRegistro = _dapperRepository.QueryDapper(_queryRegistro, new { FechaInicio, FechaFin, IdCentroCosto, IdProbabilidad, IdPais, IdCategoriaOrigen, IdIndustria, IdCargo, IdFormacion, IdTrabajo, Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                var rpta = JsonConvert.DeserializeObject<List<VerificacionManualDatosCompuestoDTO>>(queryRegistro);
                string _queryCantidad = "SELECT COUNT(*) From mkt.V_VerificacionManualDatos WHERE FechaCreacion BETWEEN @FechaInicio AND @FechaFin AND AptoProcesamiento=1 " + _queryCondicion + "";
                string queryCantidad = _dapperRepository.FirstOrDefault(_queryCantidad, new { FechaInicio, FechaFin, IdCentroCosto, IdProbabilidad, IdPais, IdCategoriaOrigen, IdIndustria, IdCargo, IdFormacion, IdTrabajo, Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);
                if (rpta.Count() > 0)
                {
                    rpta.FirstOrDefault().TotalRegistros = CantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }

                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
    }
}
