using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PgeneralProyectoAplicacionAnexoRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 14/06/2023
    /// <summary>
    /// Gestión general de T_PgeneralProyectoAplicacionAnexo
    /// </summary>
    public class PgeneralProyectoAplicacionAnexoRepository : GenericRepository<TPgeneralProyectoAplicacionAnexo>, IPgeneralProyectoAplicacionAnexoRepository
    {
        private Mapper _mapper;
        public PgeneralProyectoAplicacionAnexoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralProyectoAplicacionAnexo, PgeneralProyectoAplicacionAnexo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralProyectoAplicacionAnexo MapeoEntidad(PgeneralProyectoAplicacionAnexo entidad)
        {
            try
            {
                TPgeneralProyectoAplicacionAnexo modelo = _mapper.Map<TPgeneralProyectoAplicacionAnexo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralProyectoAplicacionAnexo Add(PgeneralProyectoAplicacionAnexo entidad)
        {
            try
            {
                var PgeneralProyectoAplicacionAnexo = MapeoEntidad(entidad);
                base.Insert(PgeneralProyectoAplicacionAnexo);
                return PgeneralProyectoAplicacionAnexo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralProyectoAplicacionAnexo Update(PgeneralProyectoAplicacionAnexo entidad)
        {
            try
            {
                var PgeneralProyectoAplicacionAnexo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralProyectoAplicacionAnexo.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralProyectoAplicacionAnexo);
                return PgeneralProyectoAplicacionAnexo;
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


        public IEnumerable<TPgeneralProyectoAplicacionAnexo> Add(IEnumerable<PgeneralProyectoAplicacionAnexo> listadoEntidad)
        {
            try
            {
                List<TPgeneralProyectoAplicacionAnexo> listado = new List<TPgeneralProyectoAplicacionAnexo>();
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

        public IEnumerable<TPgeneralProyectoAplicacionAnexo> Update(IEnumerable<PgeneralProyectoAplicacionAnexo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralProyectoAplicacionAnexo> listado = new List<TPgeneralProyectoAplicacionAnexo>();
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
        /// Fecha: 16/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Trae los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Lista DTO - PgeneralProyectoAplicacionAnexoDTO </returns>
        public PgeneralProyectoAplicacionAnexo? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT 
	                    Id,
	                    IdPgeneral,
	                    NombreArchivo,
	                    RutaArchivo,
	                    EsEnlace,
	                    SoloLectura,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_PgeneralProyectoAplicacionAnexo
                    WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralProyectoAplicacionAnexo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPAAR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Trae los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Lista DTO - PgeneralProyectoAplicacionAnexoDTO </returns>
        public IEnumerable<PgeneralProyectoAplicacionAnexoDTO> ObtenerListaPgeneralProyectoAplicacionAnexoPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        IdPgeneral,
                        NombreArchivo,
                        RutaArchivo,
                        EsEnlace,
                        SoloLectura 
                    FROM 
                        [ope].[V_ObtenerDatosPgeneralProyectoAplicacionAnexo] 
                    WHERE 
                        IdPgeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralProyectoAplicacionAnexoDTO>>(resultado)!;
                }
                return new List<PgeneralProyectoAplicacionAnexoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPAAR-OLPGPAAPI-001@Error en ObtenerListaPgeneralProyectoAplicacionAnexoPorId() {ex.Message}", ex);
            }
        }
    }
}
