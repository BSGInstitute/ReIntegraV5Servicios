using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{
    /// Repositorio: LineamientoCalificacionFaseRepository
    /// Autor: Jose Vega
    /// Fecha: 15/09/2025
    /// <summary>
    /// Gestión general de T_LineamientoCalificacionFase
    /// </summary>
    public class LineamientoCalificacionFaseRepository : GenericRepository<TLineamientoCalificacionFase>, ILineamientoCalificacionFaseRepository
    {
        private Mapper _mapper;
        public LineamientoCalificacionFaseRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLineamientoCalificacionFase, LineamientoCalificacionFase>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region
        private TLineamientoCalificacionFase MapeoEntidad(LineamientoCalificacionFase entidad)
        {
            try
            {
                TLineamientoCalificacionFase modelo = _mapper.Map<TLineamientoCalificacionFase>(entidad);


                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TLineamientoCalificacionFase Add(LineamientoCalificacionFase entidad)
        {
            try
            {
                var LineamientoCalificacionFase = MapeoEntidad(entidad);
                base.Insert(LineamientoCalificacionFase);
                return LineamientoCalificacionFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TLineamientoCalificacionFase Update(LineamientoCalificacionFase entidad)
        {
            try
            {
                var LineamientoCalificacionFase = MapeoEntidad(entidad);
                base.Update(LineamientoCalificacionFase);
                return LineamientoCalificacionFase;
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
        #endregion
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_LineamientoCalificacionFase por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> LineamientoCalificacionFase </returns>
        public LineamientoCalificacionFase ObtenerPorId(int id)
        {
            try
            {
                var rpta = new LineamientoCalificacionFase();
                var query = @"
					SELECT 
                        Id,
                        Nombre AS NombreLineamiento,
                        Orden,
                        Descripcion,
                        IdCriterioCalificacionFaseOportunidad,
                        IdCriticidadCalificacion,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                    FROM com.T_LineamientoCalificacionFase
                    WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<LineamientoCalificacionFase>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: José Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con los detalles de T_LineamientoCalificacionFase
        /// </summary>
        /// <returns> List<LineamientoCalificacionFaseDTO> </returns>
        public List<LineamientoCalificacionFaseDTO> Obtener()
        {
            try
            {
                List<LineamientoCalificacionFaseDTO> lineamientoFiltro = new();
                var query = @"
                    SELECT 
                        Id,
                        Nombre AS NombreLineamiento,
                        Orden,
                        Descripcion,
                        IdCriterioCalificacionFaseOportunidad,
                        IdCriticidadCalificacion,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                    FROM 
                        com.T_LineamientoCalificacionFase
                    WHERE 
                        Estado = 1
                    ORDER BY
                        Orden";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    lineamientoFiltro = JsonConvert.DeserializeObject<List<LineamientoCalificacionFaseDTO>>(resultado)!;
                }
                return lineamientoFiltro;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CCFR-OC-001@Error en ObtenerCombo: {ex.Message}", ex);
            }
        }
    }
}