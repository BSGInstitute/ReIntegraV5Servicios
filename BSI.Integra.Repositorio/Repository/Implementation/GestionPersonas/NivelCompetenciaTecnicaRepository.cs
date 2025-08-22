using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Repositorio: NivelCompetenciaTecnicaRepository
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 06/04/2024
    /// <summary>
    /// Gestión general de T_NivelCompetenciaTecnica
    /// </summary>
    public class NivelCompetenciaTecnicaRepository : GenericRepository<TNivelCompetenciaTecnica>, INivelCompetenciaTecnicaRepository
    {
        private Mapper _mapper;
        public NivelCompetenciaTecnicaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TNivelCompetenciaTecnica, NivelCompetenciaTecnica>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TNivelCompetenciaTecnica MapeoEntidad(NivelCompetenciaTecnica entidad)
        {
            try
            {
                TNivelCompetenciaTecnica modelo = _mapper.Map<TNivelCompetenciaTecnica>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        TNivelCompetenciaTecnica INivelCompetenciaTecnicaRepository.Add(NivelCompetenciaTecnica entidad)
        {
            try
            {
                var nivelCompetenciaTecnica = MapeoEntidad(entidad);
                base.Insert(nivelCompetenciaTecnica);
                return nivelCompetenciaTecnica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        IEnumerable<TNivelCompetenciaTecnica> INivelCompetenciaTecnicaRepository.Add(IEnumerable<NivelCompetenciaTecnica> listadoEntidad)
        {
            try
            {
                List<TNivelCompetenciaTecnica> listado = new List<TNivelCompetenciaTecnica>();
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

        bool INivelCompetenciaTecnicaRepository.Delete(int id, string usuario)
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
            base.Delete(id, usuario);
        }

        bool INivelCompetenciaTecnicaRepository.Delete(IEnumerable<int> listadoIds, string usuario)
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

        TNivelCompetenciaTecnica INivelCompetenciaTecnicaRepository.Update(NivelCompetenciaTecnica entidad)
        {
            try
            {
                var nivelCompetenciaTecnica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                nivelCompetenciaTecnica.RowVersion = entidadExistente.RowVersion;

                base.Update(nivelCompetenciaTecnica);
                return nivelCompetenciaTecnica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        IEnumerable<TNivelCompetenciaTecnica> INivelCompetenciaTecnicaRepository.Update(IEnumerable<NivelCompetenciaTecnica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                var listado = new List<TNivelCompetenciaTecnica>();
                foreach (var entidad in listadoEntidad)
                    listado.Add(MapeoEntidad(entidad));

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
        #endregion

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 06/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_NivelCompetenciaTecnica.
        /// </summary>
        /// <returns>IEnumerable NivelCompetenciaTecnica</returns>
        IEnumerable<NivelCompetenciaTecnicaDTO> INivelCompetenciaTecnicaRepository.Obtener()
        {
            try
            {
                var rpta = new List<NivelCompetenciaTecnicaDTO>();
                var query = @"
                    SELECT Id,Nombre
                    FROM gp.T_NivelCompetenciaTecnica
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<NivelCompetenciaTecnicaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método Obtener()", ex);
            }
        }

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 06/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_NivelCompetenciaTecnica asociado a un identificador.
        /// </summary>
        /// <param name="idNivelCompetenciaTecnica">Id de NivelCompetenciaTecnica</param>
        /// <returns>NivelCompetenciaTecnicaDTO</returns>
        NivelCompetenciaTecnica? INivelCompetenciaTecnicaRepository.ObtenerPorId(int idNivelCompetenciaTecnica)
        {
            try
            {
                GradoEstudio rpta = new();
                var query = @"
                    SELECT
	                    Id,
						Nombre,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
						RowVersion,
						IdMigracion
                    FROM gp.T_NivelCompetenciaTecnica
                    WHERE Estado = 1 AND Id = @idNivelCompetenciaTecnica";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idNivelCompetenciaTecnica});
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<NivelCompetenciaTecnica>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }
    }
}
