using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
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
    /// Repositorio: ModeloPredictivoProbabilidadRepository
    /// Autor: Margiory Ramirez 
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ModeloPredictivoProbabilidad
    /// </summary>
    public class ModeloPredictivoProbabilidadRepository : GenericRepository<TModeloPredictivoProbabilidad>, IModeloPredictivoProbabilidadRepository
    {
        private Mapper _mapper;

        public ModeloPredictivoProbabilidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloPredictivoProbabilidad, ModeloPredictivoProbabilidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TModeloPredictivoProbabilidad MapeoEntidad(ModeloPredictivoProbabilidad entidad)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoProbabilidad modelo = _mapper.Map<TModeloPredictivoProbabilidad>(entidad);

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

        public TModeloPredictivoProbabilidad Add(ModeloPredictivoProbabilidad entidad)
        {
            try
            {
                var ModeloPredictivoProbabilidad = MapeoEntidad(entidad);
                base.Insert(ModeloPredictivoProbabilidad);
                return ModeloPredictivoProbabilidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoProbabilidad Update(ModeloPredictivoProbabilidad entidad)
        {
            try
            {
                var ModeloPredictivoProbabilidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ModeloPredictivoProbabilidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ModeloPredictivoProbabilidad);
                return ModeloPredictivoProbabilidad;
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


        public IEnumerable<TModeloPredictivoProbabilidad> Add(IEnumerable<ModeloPredictivoProbabilidad> listadoEntidad)
        {
            try
            {
                List<TModeloPredictivoProbabilidad> listado = new List<TModeloPredictivoProbabilidad>();
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

        public IEnumerable<TModeloPredictivoProbabilidad> Update(IEnumerable<ModeloPredictivoProbabilidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloPredictivoProbabilidad> listado = new List<TModeloPredictivoProbabilidad>();
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
        /// Autor: Margiory Ramirez 
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ModeloPredictivoProbabilidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<FiltroDTO> ObtenerCombo()
        {
            try
            {
                List<FiltroDTO> rpta = new List<FiltroDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ModeloPredictivoProbabilidad WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene Programa y Probabilidad de Inscripcion por idOportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> ColorPerfilProgramaDTO </returns>
        public ColorPerfilProgramaV2DTO ObtenerProgramaYProbabilidad(int idOportunidad)
        {
            try
            {
                string query = "EXEC mkt.SP_ModeloPredictivoProbabilidadObtenerPrograma @IdOportunidad = @IdOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<ColorPerfilProgramaV2DTO>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene analisis de programas del alumno
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> AnalisisProgramasAlumnoDTO </returns>
        public AnalisisProgramasAlumnoDTO ObtenerAnalisisProgramasAlumno(int idOportunidad)
        {
            try
            {
                var response = new AnalisisProgramasAlumnoDTO();

                string queryAlumno = "SELECT IdAlumno FROM com.T_Oportunidad WITH(NOLOCK) WHERE Id = @idOportunidad";
                var idAlumnoStr = _dapperRepository.FirstOrDefault(queryAlumno, new { idOportunidad });

                if (string.IsNullOrEmpty(idAlumnoStr)) return response;

                var alumnoObj = JsonConvert.DeserializeObject<dynamic>(idAlumnoStr);
                int idAlumno = alumnoObj.IdAlumno;

                var jsonUltima = _dapperRepository.FirstOrDefault(
                    "EXEC mkt.SP_ModeloPredictivoProbabilidadObtenerUltimaOportunidadAlumno @IdAlumno = @IdAlumno",
                    new { IdAlumno = idAlumno }
                );

                if (!string.IsNullOrEmpty(jsonUltima))
                {
                    response.UltimaOportunidad = JsonConvert.DeserializeObject<ProgramaProbabilidadDTO>(jsonUltima);
                }

                var jsonMayor = _dapperRepository.QueryDapper(
                    "EXEC mkt.SP_ModeloPredictivoProbabilidadObtenerMayorProbabilidadAlumno @IdAlumno = @IdAlumno",
                    new { IdAlumno = idAlumno }
                );

                if (!string.IsNullOrEmpty(jsonMayor) && !jsonMayor.Contains("[]"))
                {
                    response.MayorProbabilidad = JsonConvert.DeserializeObject<List<ProgramaProbabilidadDTO>>(jsonMayor);
                }

                var jsonVenta = _dapperRepository.QueryDapper(
                    "EXEC mkt.SP_ModeloPredictivoProbabilidadObtenerVentaCruzadaAlumno @IdAlumno = @IdAlumno",
                    new { IdAlumno = idAlumno }
                );

                if (!string.IsNullOrEmpty(jsonVenta) && !jsonVenta.Contains("[]"))
                {
                    response.VentaCruzada = JsonConvert.DeserializeObject<List<ProgramaProbabilidadDTO>>(jsonVenta);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
