using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    /// Repositorio: ProgramaGeneralPerfilScoringCiudadRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPerfilScoringCiudad
    /// </summary>
    public class ProgramaGeneralPerfilScoringCiudadRepository : GenericRepository<TProgramaGeneralPerfilScoringCiudad>, IProgramaGeneralPerfilScoringCiudadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPerfilScoringCiudadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPerfilScoringCiudad, ProgramaGeneralPerfilScoringCiudad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralPerfilScoringCiudad MapeoEntidad(ProgramaGeneralPerfilScoringCiudad entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringCiudad perfilScoringCiudad = _mapper.Map<TProgramaGeneralPerfilScoringCiudad>(entidad);

                return perfilScoringCiudad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilScoringCiudad Add(ProgramaGeneralPerfilScoringCiudad entidad)
        {
            try
            {
                var perfilScoringCiudad = MapeoEntidad(entidad);
                base.Insert(perfilScoringCiudad);
                return perfilScoringCiudad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPerfilScoringCiudad Update(ProgramaGeneralPerfilScoringCiudad entidad)
        {
            try
            {
                var perfilScoringCiudad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilScoringCiudad.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilScoringCiudad);
                return perfilScoringCiudad;
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


        public IEnumerable<TProgramaGeneralPerfilScoringCiudad> Add(IEnumerable<ProgramaGeneralPerfilScoringCiudad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPerfilScoringCiudad> listado = new List<TProgramaGeneralPerfilScoringCiudad>();
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

        public IEnumerable<TProgramaGeneralPerfilScoringCiudad> Update(IEnumerable<ProgramaGeneralPerfilScoringCiudad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPerfilScoringCiudad> listado = new List<TProgramaGeneralPerfilScoringCiudad>();
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
        /// Obtiene toda la informacion de T_ProgramaGeneralPerfilScoringCiudad asociado a un Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ProgramaGeneralPerfilScoringCiudad </returns>
        public ProgramaGeneralPerfilScoringCiudad? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        IdPGeneral IdPgeneral,
                        Nombre,
                        IdCiudad,
                        IdSelect,
                        Valor,
                        Fila,
                        Columna,
                        Validar,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPerfilScoringCiudad
                    WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPerfilScoringCiudad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPSCR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ProgramaGeneralPerfilScoringCiudad>() </returns>
        public IEnumerable<ProgramaGeneralPerfilScoringCiudad> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral IdPgeneral,
                        Nombre,
                        IdCiudad,
                        IdSelect,
                        Valor,
                        Fila,
                        Columna,
                        Validar,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ProgramaGeneralPerfilScoringCiudad
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralPerfilScoringCiudad>>(resultado)!;
                }
                return new List<ProgramaGeneralPerfilScoringCiudad>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGPSCR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
    }
}
