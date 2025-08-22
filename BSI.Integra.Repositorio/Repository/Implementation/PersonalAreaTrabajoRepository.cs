using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PersonalAreaTrabajoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_PersonalAreaTrabajo
    /// </summary>
    public class PersonalAreaTrabajoRepository : GenericRepository<TPersonalAreaTrabajo>, IPersonalAreaTrabajoRepository
    {
        private Mapper _mapper;

        public PersonalAreaTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalAreaTrabajo, PersonalAreaTrabajo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalAreaTrabajo MapeoEntidad(PersonalAreaTrabajo entidad)
        {
            try
            {
                TPersonalAreaTrabajo modelo = _mapper.Map<TPersonalAreaTrabajo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPersonalAreaTrabajo Add(PersonalAreaTrabajo entidad)
        {
            try
            {
                var PersonalAreaTrabajo = MapeoEntidad(entidad);
                base.Insert(PersonalAreaTrabajo);
                return PersonalAreaTrabajo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPersonalAreaTrabajo Update(PersonalAreaTrabajo entidad)
        {
            try
            {
                var PersonalAreaTrabajo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalAreaTrabajo.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalAreaTrabajo);
                return PersonalAreaTrabajo;
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
        public IEnumerable<TPersonalAreaTrabajo> Add(IEnumerable<PersonalAreaTrabajo> listadoEntidad)
        {
            try
            {
                List<TPersonalAreaTrabajo> listado = new List<TPersonalAreaTrabajo>();
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
        public IEnumerable<TPersonalAreaTrabajo> Update(IEnumerable<PersonalAreaTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalAreaTrabajo> listado = new List<TPersonalAreaTrabajo>();
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

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PersonalAreaTrabajo.
        /// </summary>
        /// <returns> List<PersonalAreaTrabajoDTO> </returns>
        public IEnumerable<PersonalAreaTrabajoDTO> Obtener()
        {
            try
            {
                List<PersonalAreaTrabajoDTO> rpta = new List<PersonalAreaTrabajoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Codigo,
	                    Nombre,
	                    Descripcion
                    FROM gp.T_PersonalAreaTrabajo
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalAreaTrabajoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PersonalAreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                string query = @"SELECT Id, UPPER(Nombre) AS Nombre FROM gp.T_PersonalAreaTrabajo WHERE Estado = 1 ORDER BY Id";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PersonalAreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string query = @"SELECT Id, UPPER(Nombre) AS Nombre FROM gp.T_PersonalAreaTrabajo WHERE Estado = 1 ORDER BY Id";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerComboAsync(): {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza una consulta mediante el Id para saber si existe el registro
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> true or false </returns>
        public bool ExistePorId(int id)
        {
            try
            {
                var query = @"SELECT Id FROM gp.T_PersonalAreaTrabajo WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { @Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/09/2023
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >CertificadoPartnerComplemento || null</returns>
        public PersonalAreaTrabajo? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Codigo,
	                    Descripcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_CertificadoPartnerComplemento
                    WHERE Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalAreaTrabajo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }


        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 03/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PersonalAreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerTodoFiltroAreaTrabajo()
        {
            try
            {
                string query = @"SELECT Id, Nombre FROM gp.T_PersonalAreaTrabajo WHERE Estado = 1 ORDER BY Id";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }

    }
}
