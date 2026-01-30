using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
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
    public class ProgramaGeneralProblemaFactorRepository : GenericRepository<TProgramaGeneralProblemaFactor>, IProgramaGeneralProblemaFactorRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactor, ProgramaGeneralProblemaFactor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaFactor MapeoEntidad(ProgramaGeneralProblemaFactor entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaFactor modelo = _mapper.Map<TProgramaGeneralProblemaFactor>(entidad);

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

        public TProgramaGeneralProblemaFactor Add(ProgramaGeneralProblemaFactor entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactor = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaFactor);
                return ProgramaGeneralProblemaFactor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaFactor Update(ProgramaGeneralProblemaFactor entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaFactor.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaFactor);
                return ProgramaGeneralProblemaFactor;
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


        public IEnumerable<TProgramaGeneralProblemaFactor> Add(IEnumerable<ProgramaGeneralProblemaFactor> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaFactor> listado = new List<TProgramaGeneralProblemaFactor>();
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

        public IEnumerable<TProgramaGeneralProblemaFactor> Update(IEnumerable<ProgramaGeneralProblemaFactor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaFactor> listado = new List<TProgramaGeneralProblemaFactor>();
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

        /// Autor:  Marco Jose Villanueva Torres.
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoFormacion.
        /// </summary>
        /// <returns> List<TipoFormacionDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaFactorDTO> Obtener()
        {
            try
            {
                List<ProgramaGeneralProblemaFactorDTO> rpta = new List<ProgramaGeneralProblemaFactorDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM pla.T_ProgramaGeneralProblemaFactor
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaFactorDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ProgramaGeneralProblemaFactorDTO>> ObtenerAsync()
        {
            try
            {
                var query = @"
                SELECT Id, Nombre
                FROM pla.T_ProgramaGeneralProblemaFactor
                WHERE Estado = 1
                ORDER BY Id DESC";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralProblemaFactorDTO>>(resultado)!;
                }
                return new List<ProgramaGeneralProblemaFactorDTO>();
            }
            catch (Exception ex)
            {
                // Recomiendo loggear aquí y usar "throw;" para mantener el stacktrace.
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >TipoFormacion || null</returns>
        public ProgramaGeneralProblemaFactor? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM pla.T_ProgramaGeneralProblemaFactor
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralProblemaFactor>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        public async Task<HashSet<int>> ObtenerSolucionesMarcadasPorOportunidadAsync(IEnumerable<int> oportunidadIds)
        {
            if (oportunidadIds == null || !oportunidadIds.Any())
            {
                return new HashSet<int>();
            }

            try
            {
                var query = @"
        SELECT DISTINCT
            T.IdProgramaGeneralProblemaFactorSolucion
        FROM
            [pla].[T_ProgramaGeneralProblemaFactorSolucionRespuesta] AS T
        WHERE
            T.IdOportunidad IN @OportunidadIds
            AND T.EsSolucionado = 1
            AND T.Estado = 1";

                var resultadoJson = await _dapperRepository.QueryDapperAsync(query, new { OportunidadIds = oportunidadIds });

                if (string.IsNullOrEmpty(resultadoJson) || resultadoJson.Contains("[]"))
                {
                    return new HashSet<int>();
                }

                var listaResultados = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(resultadoJson);

                var setDeIds = new HashSet<int>();
                foreach (var item in listaResultados)
                {
                    setDeIds.Add((int)item.IdProgramaGeneralProblemaFactorSolucion);
                }

                return setDeIds;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerSolucionesMarcadasPorOportunidadAsync() al consultar Dapper.", ex);
            }
        }

    }
}
