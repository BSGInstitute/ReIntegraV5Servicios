using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{
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

        /// Autor: Jose Vega
        /// Fecha: 13/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public LineamientoCalificacionFase ObtenerPorId(int id)
        {
            try
            {
                var rpta = new LineamientoCalificacionFase();
                var query = @"
					SELECT Id,
						IdCriterioCalificacionFaseOportunidad,
                        Orden,
						IdCriticidadCalificacion,
                        Nombre,
                        Descripcion,
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
        public LineamientoCalificacionFase ObtenerLineamientoCalificacionFasePorId(int idLineamientoCalificacionFase)
        {
            try
            {
                var rpta = new LineamientoCalificacionFase();
                var query = @"SELECT Id,
                                 IdCriterioCalificacionFaseOportunidad,
                                 IdCriticidadCalificacion,
                                 Orden,
                                 Nombre,
                                 Descripcion
                          FROM com.T_LineamientoCalificacionFase
                    WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idLineamientoCalificacionFase });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<LineamientoCalificacionFase>(resultado)!;
                    return rpta;
                }
                return null;


            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        public IEnumerable<ComboDTO> ListaLineamientos()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id, Nombre
                    FROM 
                        FROM com.T_LineamientoCalificacionFase
                    WHERE 
                        Estado = 1
                    ORDER BY
                        Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CCFR-OC-001@Error en ObtenerCombo: {ex.Message}", ex);
            }
        }
    }
}