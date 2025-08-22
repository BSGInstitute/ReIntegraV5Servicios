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
    /// Repositorio: ProgramaGeneralPerfilCiudadCoeficienteRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPerfilCiudadCoeficiente
    /// </summary>
    public class ProgramaGeneralPerfilCiudadCoeficienteRepository : GenericRepository<TProgramaGeneralPerfilCiudadCoeficiente>, IProgramaGeneralPerfilCiudadCoeficienteRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPerfilCiudadCoeficienteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPerfilCiudadCoeficiente, ProgramaGeneralPerfilCiudadCoeficiente>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralPerfilCiudadCoeficiente MapeoEntidad(ProgramaGeneralPerfilCiudadCoeficiente entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilCiudadCoeficiente perfilScoringCoeficiente = _mapper.Map<TProgramaGeneralPerfilCiudadCoeficiente>(entidad);

                return perfilScoringCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilCiudadCoeficiente Add(ProgramaGeneralPerfilCiudadCoeficiente entidad)
        {
            try
            {
                var perfilScoringCoeficiente = MapeoEntidad(entidad);
                base.Insert(perfilScoringCoeficiente);
                return perfilScoringCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilCiudadCoeficiente Update(ProgramaGeneralPerfilCiudadCoeficiente entidad)
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

        public IEnumerable<TProgramaGeneralPerfilCiudadCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilCiudadCoeficiente> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPerfilCiudadCoeficiente> listado = new List<TProgramaGeneralPerfilCiudadCoeficiente>();
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

        public IEnumerable<TProgramaGeneralPerfilCiudadCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilCiudadCoeficiente> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPerfilCiudadCoeficiente> listado = new List<TProgramaGeneralPerfilCiudadCoeficiente>();
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
        /// Obtiene toda la informacion de T_ProgramaGeneralPerfilCiudadCoeficiente asociado a un Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPerfilCiudadCoeficiente </returns>
        public ProgramaGeneralPerfilCiudadCoeficiente? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
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
                        pla.T_ProgramaGeneralPerfilCiudadCoeficiente
                    WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPerfilCiudadCoeficiente>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPCCR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ProgramaGeneralPerfilCiudadCoeficiente>() </returns>
        public IEnumerable<ProgramaGeneralPerfilCiudadCoeficiente> ObtenerPorIdPGeneral(int idPGeneral)
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
                        pla.T_ProgramaGeneralPerfilCiudadCoeficiente
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralPerfilCiudadCoeficiente>>(resultado)!;
                }
                return new List<ProgramaGeneralPerfilCiudadCoeficiente>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPCCR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
    }
}
