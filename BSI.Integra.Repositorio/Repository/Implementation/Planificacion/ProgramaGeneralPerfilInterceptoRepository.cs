using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ProgramaGeneralPerfilInterceptoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPerfilIntercepto
    /// </summary>
    public class ProgramaGeneralPerfilInterceptoRepository : GenericRepository<TProgramaGeneralPerfilIntercepto>, IProgramaGeneralPerfilInterceptoRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPerfilInterceptoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPerfilIntercepto, ProgramaGeneralPerfilIntercepto>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralPerfilIntercepto MapeoEntidad(ProgramaGeneralPerfilIntercepto entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilIntercepto perfilIntercepto = _mapper.Map<TProgramaGeneralPerfilIntercepto>(entidad);

                return perfilIntercepto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilIntercepto Add(ProgramaGeneralPerfilIntercepto entidad)
        {
            try
            {
                var perfilIntercepto = MapeoEntidad(entidad);
                base.Insert(perfilIntercepto);
                return perfilIntercepto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilIntercepto Update(ProgramaGeneralPerfilIntercepto entidad)
        {
            try
            {
                var perfilIntercepto = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilIntercepto.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilIntercepto);
                return perfilIntercepto;
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

        public IEnumerable<TProgramaGeneralPerfilIntercepto> Add(IEnumerable<ProgramaGeneralPerfilIntercepto> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPerfilIntercepto> listado = new List<TProgramaGeneralPerfilIntercepto>();
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

        public IEnumerable<TProgramaGeneralPerfilIntercepto> Update(IEnumerable<ProgramaGeneralPerfilIntercepto> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPerfilIntercepto> listado = new List<TProgramaGeneralPerfilIntercepto>();
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
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPerfilIntercepto </returns>
        public ProgramaGeneralPerfilIntercepto? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral IdPgeneral,
                        PerfilIntercepto,
                        PerfilEstado,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion FROM pla.T_ProgramaGeneralPerfilIntercepto
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPerfilIntercepto>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPI-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 17/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro mediante el idPGeneral
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPerfilIntercepto </returns>
        public ProgramaGeneralPerfilIntercepto ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"SELECT Id,
                                       IdPGeneral IdPgeneral,
                                       PerfilIntercepto,
                                       PerfilEstado,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM pla.T_ProgramaGeneralPerfilIntercepto
                                WHERE Estado = 1
                                      AND IdPGeneral = @IdPGeneral;
";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado))
                    return JsonConvert.DeserializeObject<ProgramaGeneralPerfilIntercepto>(resultado)!;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPI-OPI-001@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
    }
}
