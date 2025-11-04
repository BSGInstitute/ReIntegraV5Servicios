using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class ProgramaGeneralProblemaFactorDetalleRepository : GenericRepository<TProgramaGeneralProblemaFactorDetalle>, IProgramaGeneralProblemaFactorDetalleRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactorDetalle, ProgramaGeneralProblemaFactorDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaFactorDetalle MapeoEntidad(ProgramaGeneralProblemaFactorDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaFactorDetalle modelo = _mapper.Map<TProgramaGeneralProblemaFactorDetalle>(entidad);

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

        public TProgramaGeneralProblemaFactorDetalle Add(ProgramaGeneralProblemaFactorDetalle entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorDetalle = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaFactorDetalle);
                return ProgramaGeneralProblemaFactorDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaFactorDetalle Update(ProgramaGeneralProblemaFactorDetalle entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaFactorDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaFactorDetalle);
                return ProgramaGeneralProblemaFactorDetalle;
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


        public IEnumerable<TProgramaGeneralProblemaFactorDetalle> Add(IEnumerable<ProgramaGeneralProblemaFactorDetalle> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaFactorDetalle> listado = new List<TProgramaGeneralProblemaFactorDetalle>();
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

        public IEnumerable<TProgramaGeneralProblemaFactorDetalle> Update(IEnumerable<ProgramaGeneralProblemaFactorDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaFactorDetalle> listado = new List<TProgramaGeneralProblemaFactorDetalle>();
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
        public IEnumerable<ProgramaGeneralProblemaFactorDetalleDTO> Obtener()
        {
            try
            {
                List<ProgramaGeneralProblemaFactorDetalleDTO> rpta = new List<ProgramaGeneralProblemaFactorDetalleDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre,Titulo
                    FROM pla.T_ProgramaGeneralProblemaFactorDetalle
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaFactorDetalleDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
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
        public ProgramaGeneralProblemaFactorDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
                        Titulo,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM pla.T_ProgramaGeneralProblemaFactorDetalle 
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralProblemaFactorDetalle>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        public bool ExistePorNombre(string nombre)
        {
            try
            {
                bool rpta = false;

                var query = "pla.SP_T_ProgramaGeneralProblemaFactorDetalle_ExisteNombre";
                var parametros = new
                {
                    Nombre = (nombre ?? string.Empty).Trim()
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);

                if (!string.IsNullOrWhiteSpace(resultado) && !resultado.Equals("null", StringComparison.OrdinalIgnoreCase)
                    && !resultado.Contains("[]"))
                {
                    var filas = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>>(resultado);

                    if (filas != null && filas.Count > 0 && filas[0] != null && filas[0].TryGetValue("Existe", out var v))
                    {
                        var s = Convert.ToString(v);
                        rpta = s == "1" || string.Equals(s, "true", StringComparison.OrdinalIgnoreCase);
                    }
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ExistePorNombreTitulo() {ex.Message}", ex);
            }
        }
    }
}
