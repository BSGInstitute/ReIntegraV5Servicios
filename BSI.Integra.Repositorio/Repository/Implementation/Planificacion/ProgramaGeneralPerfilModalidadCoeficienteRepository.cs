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
    /// Repositorio: ProgramaGeneralPerfilModalidadCoeficienteRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPerfilModalidadCoeficiente
    /// </summary>
    public class ProgramaGeneralPerfilModalidadCoeficienteRepository : GenericRepository<TProgramaGeneralPerfilModalidadCoeficiente>, IProgramaGeneralPerfilModalidadCoeficienteRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPerfilModalidadCoeficienteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPerfilModalidadCoeficiente, ProgramaGeneralPerfilModalidadCoeficiente>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralPerfilModalidadCoeficiente MapeoEntidad(ProgramaGeneralPerfilModalidadCoeficiente entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilModalidadCoeficiente perfilModalidadCoeficiente = _mapper.Map<TProgramaGeneralPerfilModalidadCoeficiente>(entidad);

                return perfilModalidadCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilModalidadCoeficiente Add(ProgramaGeneralPerfilModalidadCoeficiente entidad)
        {
            try
            {
                var perfilModalidadCoeficiente = MapeoEntidad(entidad);
                base.Insert(perfilModalidadCoeficiente);
                return perfilModalidadCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilModalidadCoeficiente Update(ProgramaGeneralPerfilModalidadCoeficiente entidad)
        {
            try
            {
                var perfilModalidadCoeficiente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilModalidadCoeficiente.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilModalidadCoeficiente);
                return perfilModalidadCoeficiente;
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

        public IEnumerable<TProgramaGeneralPerfilModalidadCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilModalidadCoeficiente> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPerfilModalidadCoeficiente> listado = new List<TProgramaGeneralPerfilModalidadCoeficiente>();
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

        public IEnumerable<TProgramaGeneralPerfilModalidadCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilModalidadCoeficiente> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPerfilModalidadCoeficiente> listado = new List<TProgramaGeneralPerfilModalidadCoeficiente>();
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
        /// Obtiene toda la información de la tabla T_ProgramaGeneralPerfilModalidadCoeficiente por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPerfilModalidadCoeficiente </returns>
        public ProgramaGeneralPerfilModalidadCoeficiente? ObtenerPorId(int id)
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
                        pla.T_ProgramaGeneralPerfilModalidadCoeficiente
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPerfilModalidadCoeficiente>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPMCR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ProgramaGeneralPerfilModalidadCoeficiente>() </returns>
        public IEnumerable<ProgramaGeneralPerfilModalidadCoeficiente> ObtenerPorIdPGeneral(int idPGeneral)
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
                        pla.T_ProgramaGeneralPerfilModalidadCoeficiente
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralPerfilModalidadCoeficiente>>(resultado)!;
                }
                return new List<ProgramaGeneralPerfilModalidadCoeficiente>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPMCR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
    }
}
