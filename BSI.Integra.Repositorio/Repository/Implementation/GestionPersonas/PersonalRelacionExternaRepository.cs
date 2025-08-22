using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PersonalRelacionExternaRepository : GenericRepository<TPersonalRelacionExterna>, IPersonalRelacionExternaRepository
    {
        private Mapper _mapper;
        public PersonalRelacionExternaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalRelacionExterna, PersonalRelacionExterna>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalRelacionExterna, PersonalRelacionExternaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalRelacionExterna, TPersonalRelacionExterna>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalRelacionExterna MapeoEntidad(PersonalRelacionExterna entidad)
        {
            try
            {
                TPersonalRelacionExterna modelo = _mapper.Map<TPersonalRelacionExterna>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPersonalRelacionExterna Add(PersonalRelacionExterna entidad)
        {
            try
            {
                var PersonalRelacionExterna = MapeoEntidad(entidad);
                base.Insert(PersonalRelacionExterna);
                return PersonalRelacionExterna;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPersonalRelacionExterna Update(PersonalRelacionExterna entidad)
        {
            try
            {
                var PersonalRelacionExterna = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalRelacionExterna.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalRelacionExterna);
                return PersonalRelacionExterna;
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
        public IEnumerable<TPersonalRelacionExterna> Add(IEnumerable<PersonalRelacionExterna> listadoEntidad)
        {
            try
            {
                List<TPersonalRelacionExterna> listado = new List<TPersonalRelacionExterna>();
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
        public IEnumerable<TPersonalRelacionExterna> Update(IEnumerable<PersonalRelacionExterna> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalRelacionExterna> listado = new List<TPersonalRelacionExterna>();
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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<PersonalRelacionExternaDTO> Obtener()
        {
            try
            {
                List<PersonalRelacionExternaDTO> rpta = new List<PersonalRelacionExternaDTO>();
                var query = @"
                   SELECT Id, Nombre, IdPersonalAreaTrabajo, PersonalAreaTrabajo FROM [gp].[V_TPersonalRelacionExterna_ObtenerRegistros] WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalRelacionExternaDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 30/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>PersonalRelacionExterna || null</returns>
        public PersonalRelacionExterna? ObtenerPorId(int id)
        {
            try
            {
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
                    FROM gp.T_PersonalRelacionExterna
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalRelacionExterna>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EPS-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
