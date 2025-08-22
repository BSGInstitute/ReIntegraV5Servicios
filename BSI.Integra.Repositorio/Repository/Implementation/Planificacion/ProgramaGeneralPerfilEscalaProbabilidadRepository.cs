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
    /// Repositorio: ProgramaGeneralPerfilEscalaProbabilidadRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPerfilEscalaProbabilidad
    /// </summary>
    public class ProgramaGeneralPerfilEscalaProbabilidadRepository : GenericRepository<TProgramaGeneralPerfilEscalaProbabilidad>, IProgramaGeneralPerfilEscalaProbabilidadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPerfilEscalaProbabilidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPerfilEscalaProbabilidad, ProgramaGeneralPerfilEscalaProbabilidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralPerfilEscalaProbabilidad MapeoEntidad(ProgramaGeneralPerfilEscalaProbabilidad entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilEscalaProbabilidad perfilEscalaProbabilidad = _mapper.Map<TProgramaGeneralPerfilEscalaProbabilidad>(entidad);

                return perfilEscalaProbabilidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilEscalaProbabilidad Add(ProgramaGeneralPerfilEscalaProbabilidad entidad)
        {
            try
            {
                var perfilEscalaProbabilidad = MapeoEntidad(entidad);
                base.Insert(perfilEscalaProbabilidad);
                return perfilEscalaProbabilidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilEscalaProbabilidad Update(ProgramaGeneralPerfilEscalaProbabilidad entidad)
        {
            try
            {
                var perfilEscalaProbabilidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilEscalaProbabilidad.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilEscalaProbabilidad);
                return perfilEscalaProbabilidad;
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

        public IEnumerable<TProgramaGeneralPerfilEscalaProbabilidad> Add(IEnumerable<ProgramaGeneralPerfilEscalaProbabilidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPerfilEscalaProbabilidad> listado = new List<TProgramaGeneralPerfilEscalaProbabilidad>();
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

        public IEnumerable<TProgramaGeneralPerfilEscalaProbabilidad> Update(IEnumerable<ProgramaGeneralPerfilEscalaProbabilidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPerfilEscalaProbabilidad> listado = new List<TProgramaGeneralPerfilEscalaProbabilidad>();
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
        /// Obtiene toda la información de T_ProgramaGeneralPerfilEscalaProbabilidad mediante el Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPerfilEscalaProbabilidad </returns>
        public ProgramaGeneralPerfilEscalaProbabilidad? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral IdPgeneral,
                        Nombre,
                        ProbabilidadInicial,
                        ProbabilidadActual,
                        Orden,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPerfilEscalaProbabilidad
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPerfilEscalaProbabilidad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPEPR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ProgramaGeneralPerfilEscalaProbabilidad>() </returns>
        public IEnumerable<ProgramaGeneralPerfilEscalaProbabilidad> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral IdPgeneral,
                        Nombre,
                        ProbabilidadInicial,
                        ProbabilidadActual,
                        Orden,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPerfilEscalaProbabilidad
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralPerfilEscalaProbabilidad>>(resultado)!;
                }
                return new List<ProgramaGeneralPerfilEscalaProbabilidad>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPEPR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
    }
}
