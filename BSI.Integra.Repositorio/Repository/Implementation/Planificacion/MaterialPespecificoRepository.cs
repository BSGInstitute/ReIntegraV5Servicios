using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MaterialPespecificoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_MaterialPespecifico
    /// </summary>
    public class MaterialPespecificoRepository : GenericRepository<TMaterialPespecifico>, IMaterialPespecificoRepository
    {
        private Mapper _mapper;

        public MaterialPespecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialPespecifico, MaterialPespecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMaterialPespecificoDetalle, MaterialPespecificoDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMaterialPespecifico MapeoEntidad(MaterialPespecifico entidad)
        {
            try
            {
                TMaterialPespecifico modelo = _mapper.Map<TMaterialPespecifico>(entidad);
                if (entidad.MaterialPespecificoDetalles != null && entidad.MaterialPespecificoDetalles.Count >= 1)
                    modelo.TMaterialPespecificoDetalles = _mapper.Map<List<TMaterialPespecificoDetalle>>(entidad.MaterialPespecificoDetalles);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialPespecifico Add(MaterialPespecifico entidad)
        {
            try
            {
                var MaterialPespecifico = MapeoEntidad(entidad);
                base.Insert(MaterialPespecifico);
                return MaterialPespecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialPespecifico Update(MaterialPespecifico entidad)
        {
            try
            {
                var MaterialPespecifico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialPespecifico.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialPespecifico);
                return MaterialPespecifico;
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


        public IEnumerable<TMaterialPespecifico> Add(IEnumerable<MaterialPespecifico> listadoEntidad)
        {
            try
            {
                List<TMaterialPespecifico> listado = new List<TMaterialPespecifico>();
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

        public IEnumerable<TMaterialPespecifico> Update(IEnumerable<MaterialPespecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialPespecifico> listado = new List<TMaterialPespecifico>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los materiales por grupo de programa especifico a revisar
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> Lista DTO - List<ResultadoMaterialPEspecificoDetalleDTO> </returns>
        public IEnumerable<ResultadoMaterialPEspecificoDetalleDTO> ObtenerMateriales(FiltroMaterialDTO dto)
        {
            try
            {
                var query = $@"ope.SP_ObtenerMateriales";
                var filtros = new
                {
                    ListaArea = string.Join(",", dto.IdsAreas.Select(x => x)),
                    ListaSubArea = string.Join(",", dto.IdsSubAreas.Select(x => x)),
                    ListaProgramaGeneral = string.Join(",", dto.IdsProgramasGenerales.Select(x => x)),
                    ListaProgramaEspecificoPadreIndividual = string.Join(",", dto.IdsProgramasEspecificoPadreIndividual.Select(x => x)),
                    ListaProgramaEspecificoCurso = string.Join(",", dto.IdsProgramasEspecificoCurso.Select(x => x)),
                    ListaEstadoProgramaEspecifico = string.Join(",", dto.IdsEstadosPEspecifico.Select(x => x)),
                    ListaCiudad = string.Join(",", dto.IdsCiudades.Select(x => x)),
                    ListaModalidad = string.Join(",", dto.IdsModalidades.Select(x => x)),
                    ListaMaterialEstado = string.Join(",", dto.IdsEstadosMateriales.Select(x => x)),
                    ListaGrupo = string.Join(",", dto.IdsGrupos.Select(x => x)),
                    ListaMaterialVersion = ""
                };
                var resultadoDB = _dapperRepository.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ResultadoMaterialPEspecificoDetalleDTO>>(resultadoDB)!;
                }
                return new List<ResultadoMaterialPEspecificoDetalleDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPER-OM-001@Error en ObtenerMateriales() {ex.Message}", ex);
            }
        }
        /// Autor: Edmundo A. Llaza M.
        /// Fecha: 24/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialPespecifico.
        /// </summary>
        /// <returns> List<MaterialPespecificoDTO> </returns>
        public List<MaterialPespecificoDTO> ObtenerPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                MaterialPespecificoDTO rpta = new();
                var query = @"SELECT 
	                                 Id
                                    ,IdPEspecifico IdPespecifico
                                    ,Grupo
                                    ,IdMaterialTipo
                                    ,GrupoEdicion
                                    ,GrupoEdicionOrden
                                    ,IdFur
                                    ,Estado
                                    ,UsuarioCreacion
                                    ,UsuarioModificacion
                                    ,FechaCreacion
                                    ,FechaModificacion
                                    ,RowVersion
                                    ,IdMigracion
                                FROM 
	                                [ope].[T_MaterialPEspecifico]
                                WHERE
	                                Estado = 1
	                                and 
	                                IdPEspecifico = @idPEspecifico
                                order by 5, 6";
                var resultado = _dapperRepository.QueryDapper(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<MaterialPespecificoDTO>>(resultado);
                }
                else
                {
                    return new List<MaterialPespecificoDTO>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el maximo grupo de edicion
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Entero</returns>
        public int ObtenerMaximoGrupoEdicion(int idPEspecifico)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerMaximoGrupoEdicionMaterialPEspecifico";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPEspecifico = idPEspecifico });

                if (!string.IsNullOrEmpty(resultado))
                    return JsonConvert.DeserializeObject<ValorIntDTO>(resultado).Valor.Value;
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los grupos de edicion disponibles por programa especifico
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        public IEnumerable<ComboDTO> ObtenerGrupoEdicionDisponible(int idPEspecifico)
        {
            try
            {
                var query = @"SELECT Id,
                                       Nombre
                                FROM ope.V_ObtenerGrupoEdicionDisponible
                                WHERE IdPEspecifico = @IdPEspecifico
                                      OR IdPEspecifico IS NULL
                                ORDER BY Id ASC;";
                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultadoDB);
                return new List<ComboDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-02
        /// <summary>
        /// Obtiene material por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaterialPespecifico ObtenerPorId(int id)
        {
            try
            {
                MaterialPespecifico rpta = new();
                var query = @"SELECT 
	                                 Id
                                    ,IdPEspecifico IdPespecifico
                                    ,Grupo
                                    ,IdMaterialTipo
                                    ,GrupoEdicion
                                    ,GrupoEdicionOrden
                                    ,IdFur
                                    ,Estado
                                    ,UsuarioCreacion
                                    ,UsuarioModificacion
                                    ,FechaCreacion
                                    ,FechaModificacion
                                    ,RowVersion
                                    ,IdMigracion
                                FROM 
	                                [ope].[T_MaterialPEspecifico]
                                WHERE
	                                Estado = 1
	                                and 
	                                Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject <MaterialPespecifico>(resultado);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-04
        /// <summary>
        /// Obtiene todos programas especificos hijos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public async Task <List<ResultadoMaterialPEspecificoDetalleDTO>> ObtenerMaterialesGestionEnvioAsync(FiltroMaterialDTO filtro)
        {
            try
            {
                var lista = new List<ResultadoMaterialPEspecificoDetalleDTO>();
                var query = $@"ope.SP_ObtenerMaterialesGestionEnvio";
                var filtros = new
                {
                    ListaArea = string.Join(",", filtro.IdsAreas.Select(x => x)),
                    ListaSubArea = string.Join(",", filtro.IdsSubAreas.Select(x => x)),
                    ListaProgramaGeneral = string.Join(",", filtro.IdsProgramasGenerales.Select(x => x)),
                    ListaProgramaEspecificoPadreIndividual = string.Join(",", filtro.IdsProgramasEspecificoPadreIndividual.Select(x => x)),
                    ListaProgramaEspecificoCurso = string.Join(",", filtro.IdsProgramasEspecificoCurso.Select(x => x)),
                    ListaEstadoProgramaEspecifico = string.Join(",", filtro.IdsEstadosPEspecifico.Select(x => x)),
                    ListaCiudad = string.Join(",", filtro.IdsCiudades.Select(x => x)),
                    ListaModalidad = string.Join(",", filtro.IdsModalidades.Select(x => x)),
                    ListaMaterialEstado = string.Join(",", filtro.IdsEstadosMateriales.Select(x => x)),
                    ListaMaterialVersion = ""
                };
                var resultadoDB = await _dapperRepository.QuerySPDapperAsync(query, filtros);//agragar await
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ResultadoMaterialPEspecificoDetalleDTO>>(resultadoDB);
                }
                return lista;
            }



    

            catch (Exception) { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-04
        /// <summary>
        /// Obtiene las acciones asociadas a un tipo de material
        /// </summary>
        /// <param name="idMaterialTipo"></param>
        /// <returns></returns>
        public async Task<List<ComboDTO>>ObtenerMaterialAccionPorMaterialTipo(int idMaterialTipo)
        {
            try
            {
                var lista = new List<ComboDTO>();
                var query = $@"ope.SP_ObtenerMaterialAccionPorMaterialTipo";
                var filtros = new
                {
                    IdMaterialTipo = idMaterialTipo
                };
                var resultadoDB = await _dapperRepository.QuerySPDapperAsync(query, filtros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ComboDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-06
        /// <summary>
        /// Obtiene de T_MaterialPEspecifico por 
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <param name="grupoEdicion"></param>
        /// <returns></returns>
        public IEnumerable<MaterialPespecifico> ObtenerPorPEspecificoGrupo(int idPEspecifico, int grupoEdicion)
        {
            try
            {
                MaterialPespecifico rpta = new();
                var query = @"SELECT 
	                                 Id
                                    ,IdPEspecifico IdPespecifico
                                    ,Grupo
                                    ,IdMaterialTipo
                                    ,GrupoEdicion
                                    ,GrupoEdicionOrden
                                    ,IdFur
                                    ,Estado
                                    ,UsuarioCreacion
                                    ,UsuarioModificacion
                                    ,FechaCreacion
                                    ,FechaModificacion
                                    ,RowVersion
                                    ,IdMigracion
                                FROM 
	                                [ope].[T_MaterialPEspecifico]
                                WHERE
	                                IdPEspecifico = @idPEspecifico
	                                and 
	                                GrupoEdicion = @grupoEdicion";
                var resultado = _dapperRepository.QueryDapper(query, new { idPEspecifico, grupoEdicion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<List<MaterialPespecifico>>(resultado);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Obtiene valores de T_Fur por el campo PEspecifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public List<PEspecificoFurDetalleDTO> ObtenerFursAsociadosPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                var listaResultado = new List<PEspecificoFurDetalleDTO>();
                var query = $@"ope.SP_ObtenerFursAsociadosProgramaEspecifico";
                var resultado = _dapperRepository.QuerySPDapper(query, new { idPEspecifico });

                if (!string.IsNullOrEmpty(resultado))
                {
                    listaResultado = JsonConvert.DeserializeObject<List<PEspecificoFurDetalleDTO>>(resultado);
                }
                return listaResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 2023-11-03
        /// <summary>
        /// Obtiene todos materiales segun programa especifico y grupo, material accion y material version
        /// </summary>
        /// Obtiene todos programas especificos hijos
        /// <returns></returns>
        public async Task<List<MaterialPEspecificoEntregaDTO>> ObtenerMaterialesRegistroEntrega(FiltroMaterialDTO filtro)
        {
            try
            {
                var lista = new List<MaterialPEspecificoEntregaDTO>();
                var storedProcedure = "ope.SP_ObtenerMateriales_Alterno";
                var parametros = new
                {
                    ListaArea = string.Join(",", filtro.IdsAreas),
                    ListaSubArea = string.Join(",", filtro.IdsSubAreas),
                    ListaProgramaGeneral = string.Join(",", filtro.IdsProgramasGenerales),
                    ListaProgramaEspecificoPadreIndividual = string.Join(",", filtro.IdsProgramasEspecificoPadreIndividual),
                    ListaProgramaEspecificoCurso = string.Join(",", filtro.IdsProgramasEspecificoCurso),
                    ListaEstadoProgramaEspecifico = string.Join(",", filtro.IdsEstadosPEspecifico),
                    ListaCiudad = string.Join(",", filtro.IdsCiudades),
                    ListaModalidad = string.Join(",", filtro.IdsModalidades),
                    ListaMaterialEstado = "5",
                    ListaMaterialVersion="2",
                    ListaMaterialAccion = "2"
                };

                var resultadoDB = await _dapperRepository.QuerySPDapperAsync(storedProcedure, parametros);

                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<MaterialPEspecificoEntregaDTO>>(resultadoDB);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener materiales: " + e.Message);
            }
        }
    }
}



