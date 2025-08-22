using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: CriteriosEvaluacionProgramasEspecificosRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 22/07/2023
    /// <summary>
    /// Gestión general de T_PEspecificoEsquema
    /// </summary>
    public class CriteriosEvaluacionProgramasEspecificosRepository : GenericRepository<TPespecificoEsquema>, ICriteriosEvaluacionProgramasEspecificosRepository
    {
        private Mapper _mapper;

        public CriteriosEvaluacionProgramasEspecificosRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoEsquema, PEspecificoEsquema>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPespecificoEsquema MapeoEntidad(PEspecificoEsquema entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoEsquema modelo = _mapper.Map<TPespecificoEsquema>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoEsquema Add(PEspecificoEsquema entidad)
        {
            try
            {
                var PEspecificoEsquema = MapeoEntidad(entidad);
                base.Insert(PEspecificoEsquema);
                return PEspecificoEsquema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoEsquema Update(PEspecificoEsquema entidad)
        {
            try
            {
                var PEspecificoEsquema = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PEspecificoEsquema.RowVersion = entidadExistente.RowVersion;

                base.Update(PEspecificoEsquema);
                return PEspecificoEsquema;
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


        public IEnumerable<TPespecificoEsquema> Add(IEnumerable<PEspecificoEsquema> listadoEntidad)
        {
            try
            {
                List<TPespecificoEsquema> listado = new List<TPespecificoEsquema>();
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

        public IEnumerable<TPespecificoEsquema> Update(IEnumerable<PEspecificoEsquema> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoEsquema> listado = new List<TPespecificoEsquema>();
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

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 21/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de programas específicos asociados a esquemas de evaluación
        /// </summary>
        /// <returns>IEnumerable<DatosListaPespecificoEsquemaDTO></returns>
        public IEnumerable<DatosListaPespecificoEsquemaDTO> ObtenerProgramasEspecificoEsquemasFiltrosPadreIndividual(FiltroProgramaEspecificoEsquemaFiltroCompuestoDTO paginador)
        {
            try
            {
                var filtros = new object();
                string _queryCondicion = "";
                string[] IdArea = new string[6];
                string[] IdSubArea = new string[6];
                string[] IdPGeneral = new string[6];
                string[] IdProgramaEspecifico = new string[6];
                string[] IdCentroCosto = new string[6];
                string[] IdEstadoPEspecifico = new string[6];
                string[] CodigoBs = new string[6];
                string[] IdCentroCostoD = new string[6];
                if (paginador.filtroRegistros != null)
                {
                    if (paginador.filtroRegistros.IdArea != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdArea in @IdArea ";
                        IdArea = paginador.filtroRegistros.IdArea.Split(",");
                    }
                    if (paginador.filtroRegistros.IdSubArea != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdSubArea in @IdSubArea ";
                        IdSubArea = paginador.filtroRegistros.IdSubArea.Split(",");
                    }
                    if (paginador.filtroRegistros.IdPGeneral != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdPGeneral in @IdPGeneral ";
                        IdPGeneral = paginador.filtroRegistros.IdPGeneral.Split(",");
                    }
                    if (paginador.filtroRegistros.IdProgramaEspecifico != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdProgramaEspecifico in @IdProgramaEspecifico ";
                        IdProgramaEspecifico = paginador.filtroRegistros.IdProgramaEspecifico.Split(",");
                    }
                    if (paginador.filtroRegistros.IdCentroCosto != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdCentroCosto in @IdCentroCosto ";
                        IdCentroCosto = paginador.filtroRegistros.IdCentroCosto.Split(",");
                    }
                    if (paginador.filtroRegistros.IdEstadoPEspecifico != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdEstadoPEspecifico in @IdEstadoPEspecifico ";
                        IdEstadoPEspecifico = paginador.filtroRegistros.IdEstadoPEspecifico.Split(",");
                    }
                    if (paginador.filtroRegistros.CodigoBs != "")
                    {
                        _queryCondicion = _queryCondicion + "And CodigoBs in @CodigoBs ";
                        CodigoBs = paginador.filtroRegistros.CodigoBs.Split(",");
                    }
                    if (paginador.filtroRegistros.IdCentroCostoD != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdCentroCosto in @IdCentroCostoD ";
                        IdCentroCostoD = paginador.filtroRegistros.IdCentroCostoD.Split(",");
                    }
                }
                string queryRegistro = string.Empty;
                string queryCantidad = string.Empty;
                if (paginador.filtroRegistros.IdCentroCostoD == "")
                {
                    string _queryRegistro = "SELECT IdArea,Area,IdSubArea,SubArea,IdPGeneral,PGeneral,IdProgramaEspecifico,ProgramaEspecifico,IdCentroCosto," +
                        "CentroCosto,IdEstadoPEspecifico,EstadoProgramaEspecifico,CodigoBs,Ciudad,IdModalidadCurso,ModalidadCurso,TipoSesion,TipoProgramaGeneral,Estado" +
                        " FROM pla.V_ProgramaEspecificoEsquemaEvaluacionFiltro WHERE Estado=1 " + _queryCondicion + "order by IdProgramaEspecifico desc";

                    queryRegistro = _dapperRepository.QueryDapper(_queryRegistro, new
                    {
                        IdArea,
                        IdSubArea,
                        IdPGeneral,
                        IdProgramaEspecifico,
                        IdCentroCosto,
                        IdEstadoPEspecifico,
                        CodigoBs
                    });
                    string _queryCantidad = "SELECT COUNT(*) FROM pla.V_ProgramaEspecificoEsquemaEvaluacionFiltro WHERE Estado=1 " + _queryCondicion + "";
                    queryCantidad = _dapperRepository.FirstOrDefault(_queryCantidad, new {
                        IdArea,
                        IdSubArea,
                        IdPGeneral,
                        IdProgramaEspecifico,
                        IdCentroCosto,
                        IdEstadoPEspecifico,
                        CodigoBs
                    });
                }
                else
                {
                    string _queryRegistro = "SELECT IdArea,Area,IdSubArea,SubArea,IdPGeneral,PGeneral,IdProgramaEspecifico,ProgramaEspecifico,IdCentroCosto," +
                        "CentroCosto,IdEstadoPEspecifico,EstadoProgramaEspecifico,CodigoBs,Ciudad,IdModalidadCurso,ModalidadCurso,TipoSesion,TipoProgramaGeneral,Estado" +
                        " FROM pla.V_ProgramaEspecificoEsquemaEvaluacionFiltro WHERE Estado=1 " + _queryCondicion + "order by IdProgramaEspecifico desc ";

                    queryRegistro = _dapperRepository.QueryDapper(_queryRegistro, new
                    {
                        IdCentroCostoD
                    });
                    string _queryCantidad = "SELECT COUNT(*) FROM pla.V_ProgramaEspecificoEsquemaEvaluacionFiltro WHERE Estado=1 " + _queryCondicion + "";
                    queryCantidad = _dapperRepository.FirstOrDefault(_queryCantidad, new
                    {
                        IdCentroCostoD
                    });
                }
                var Datos = JsonConvert.DeserializeObject<List<DatosListaPespecificoEsquemaDTO>>(queryRegistro);
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);
                if (Datos.Count() > 0)
                {
                    Datos.FirstOrDefault().TotalRegistros = CantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }

                return Datos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 21/07/2023
        /// <summary>
        /// Obtiene el esquema de evaluación por IdPEspecificp
        /// </summary>
        /// <returns> ValorDTO </returns>  
        public ValorDTO ObtenerEsquemaPorIdPEspecifico(int IdPEspecifico)
        {
            try
            {
                var _query = string.Empty;
                ValorDTO Listado = new ValorDTO();
                _query = "SELECT Top 1 Id,IdEsquemaEvaluacion AS Valor FROM pla.T_PEspecificoEsquema WHERE IdPEspecifico=@IdPEspecifico and estado=1";

                string respuesta = _dapperRepository.FirstOrDefault(_query, new { IdPEspecifico = IdPEspecifico });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    Listado = JsonConvert.DeserializeObject<ValorDTO>(respuesta);
                }
                return Listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
