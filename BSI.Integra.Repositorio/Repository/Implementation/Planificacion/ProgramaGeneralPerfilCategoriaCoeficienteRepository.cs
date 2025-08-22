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
    /// Repositorio: ProgramaGeneralPerfilCategoriaCoeficienteRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPerfilCategoriaCoeficiente
    /// </summary>
    public class ProgramaGeneralPerfilCategoriaCoeficienteRepository : GenericRepository<TProgramaGeneralPerfilCategoriaCoeficiente>, IProgramaGeneralPerfilCategoriaCoeficienteRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPerfilCategoriaCoeficienteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPerfilCategoriaCoeficiente, ProgramaGeneralPerfilCategoriaCoeficiente>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralPerfilCategoriaCoeficiente MapeoEntidad(ProgramaGeneralPerfilCategoriaCoeficiente entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilCategoriaCoeficiente perfilScoringCargo = _mapper.Map<TProgramaGeneralPerfilCategoriaCoeficiente>(entidad);

                return perfilScoringCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilCategoriaCoeficiente Add(ProgramaGeneralPerfilCategoriaCoeficiente entidad)
        {
            try
            {
                var perfilScoringCargo = MapeoEntidad(entidad);
                base.Insert(perfilScoringCargo);
                return perfilScoringCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilCategoriaCoeficiente Update(ProgramaGeneralPerfilCategoriaCoeficiente entidad)
        {
            try
            {
                var perfilScoringCargo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilScoringCargo.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilScoringCargo);
                return perfilScoringCargo;
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

        public IEnumerable<TProgramaGeneralPerfilCategoriaCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilCategoriaCoeficiente> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPerfilCategoriaCoeficiente> listado = new List<TProgramaGeneralPerfilCategoriaCoeficiente>();
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

        public IEnumerable<TProgramaGeneralPerfilCategoriaCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilCategoriaCoeficiente> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPerfilCategoriaCoeficiente> listado = new List<TProgramaGeneralPerfilCategoriaCoeficiente>();
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
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPerfilCategoriaCoeficiente </returns>
        public ProgramaGeneralPerfilCategoriaCoeficiente? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral IdPgeneral,
                        Nombre,
                        Coeficiente,
                        IdSelect,
                        Columna,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPerfilCategoriaCoeficiente
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPerfilCategoriaCoeficiente>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPCCoR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ProgramaGeneralPerfilCategoriaCoeficiente>() </returns>
        public IEnumerable<ProgramaGeneralPerfilCategoriaCoeficiente> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral IdPgeneral,
                        Nombre,
                        Coeficiente,
                        IdSelect,
                        Columna,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPerfilCategoriaCoeficiente
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralPerfilCategoriaCoeficiente>>(resultado)!;
                }
                return new List<ProgramaGeneralPerfilCategoriaCoeficiente>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPCCoR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
    }
}
