using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
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
    public class AsesorMarcadorRepository : GenericRepository<TAsesorMarcador>, IAsesorMarcadorRepository
    {
        private Mapper _mapper;

        public AsesorMarcadorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsesorMarcador, AsesorMarcador>(MemberList.None).ReverseMap();
                cfg.CreateMap<AsesorMarcador, AsesorMarcadorDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<AsesorMarcador, TAsesorMarcador>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAsesorMarcador MapeoEntidad(AsesorMarcador entidad)
        {
            try
            {
                //crea la entidad padre
                TAsesorMarcador modelo = _mapper.Map<TAsesorMarcador>(entidad);

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

        public TAsesorMarcador Add(AsesorMarcador entidad)
        {
            try
            {
                var AsesorMarcador = MapeoEntidad(entidad);
                base.Insert(AsesorMarcador);
                return AsesorMarcador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsesorMarcador Update(AsesorMarcador entidad)
        {
            try
            {
                var AsesorMarcador = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsesorMarcador.RowVersion = entidadExistente.RowVersion;

                base.Update(AsesorMarcador);
                return AsesorMarcador;
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


        public IEnumerable<TAsesorMarcador> Add(IEnumerable<AsesorMarcador> listadoEntidad)
        {
            try
            {
                List<TAsesorMarcador> listado = new List<TAsesorMarcador>();
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

        public IEnumerable<TAsesorMarcador> Update(IEnumerable<AsesorMarcador> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsesorMarcador> listado = new List<TAsesorMarcador>();
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
        public IEnumerable<AsesorMarcadorDTO> Obtener()
        {
            try
            {
                List<AsesorMarcadorDTO> rpta = new List<AsesorMarcadorDTO>();
                var query = @"
                    SELECT
	                    Id,IdPersonal,MarcadorActivo
                    FROM com.T_AsesorMarcador
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AsesorMarcadorDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsesorMarcador? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                   Id,IdPersonal,MarcadorActivo,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM com.T_AsesorMarcador
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<AsesorMarcador>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        public List<ReporteAsesorMarcadorDTO> ObtenerReporteMarcador(FiltroReporteAsesorMarcadorDTO filtro)
        {
            try
            {
                string? asesores = null;

                if (filtro.Asesores.Count() > 0)
                    asesores = string.Join(",", filtro.Asesores);

                var resultado = _dapperRepository.QuerySPDapper("com.SP_ReporteAsesorMarcadoLogTemp", new
                {
                    Asesores = asesores,
                    filtro.FechaInicio,
                    filtro.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ReporteAsesorMarcadorDTO>>(resultado)!;
                }
                return new List<ReporteAsesorMarcadorDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMR-ORM-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        public List<ReporteFinalPromedioDTO> ObtenerReporteMarcadorTiemposPromedio(FiltroReporteAsesorMarcadorDTO filtro)
        {
            try
            {
                string? asesores = null;

                if (filtro.Asesores.Count() > 0)
                    asesores = string.Join(",", filtro.Asesores);

                var resultado = _dapperRepository.QuerySPDapper("com.SP_ReporteAsesorMarcadoTiempoPromedioTemp", new
                {
                    Asesores = asesores,
                    filtro.FechaInicio,
                    filtro.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ReporteFinalPromedioDTO>>(resultado)!;
                }
                return new List<ReporteFinalPromedioDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMR-ORM-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

    }



}
