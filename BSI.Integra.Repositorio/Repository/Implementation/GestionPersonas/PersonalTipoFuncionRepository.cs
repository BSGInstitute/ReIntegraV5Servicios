using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PersonalTipoFuncionRepository : GenericRepository<TPersonalTipoFuncion>, IPersonalTipoFuncionRepository
    {
        private Mapper _mapper;
        public PersonalTipoFuncionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalTipoFuncion, PersonalTipoFuncion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalTipoFuncion, PersonalTipoFuncionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalTipoFuncion, TPersonalTipoFuncion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalTipoFuncion MapeoEntidad(PersonalTipoFuncion entidad)
        {
            try
            {
                TPersonalTipoFuncion modelo = _mapper.Map<TPersonalTipoFuncion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPersonalTipoFuncion Add(PersonalTipoFuncion entidad)
        {
            try
            {
                var PersonalTipoFuncion = MapeoEntidad(entidad);
                base.Insert(PersonalTipoFuncion);
                return PersonalTipoFuncion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPersonalTipoFuncion Update(PersonalTipoFuncion entidad)
        {
            try
            {
                var PersonalTipoFuncion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalTipoFuncion.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalTipoFuncion);
                return PersonalTipoFuncion;
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
        public IEnumerable<TPersonalTipoFuncion> Add(IEnumerable<PersonalTipoFuncion> listadoEntidad)
        {
            try
            {
                List<TPersonalTipoFuncion> listado = new List<TPersonalTipoFuncion>();
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
        public IEnumerable<TPersonalTipoFuncion> Update(IEnumerable<PersonalTipoFuncion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalTipoFuncion> listado = new List<TPersonalTipoFuncion>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<PersonalTipoFuncionDTO> Obtener()
        {
            try
            {
                List<PersonalTipoFuncionDTO> rpta = new List<PersonalTipoFuncionDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_PersonalTipoFuncion
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalTipoFuncionDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>PersonalTipoFuncion || null</returns>
        public PersonalTipoFuncion? ObtenerPorId(int id)
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
                    FROM gp.T_PersonalTipoFuncion
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PersonalTipoFuncion>(resultado)!;
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
