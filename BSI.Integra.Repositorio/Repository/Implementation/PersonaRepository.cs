using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PersonaRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/09/2022
    /// <summary>
    /// Gestión general de T_Persona
    /// </summary>
    public class PersonaRepository : GenericRepository<TPersona>, IPersonaRepository
    {
        private Mapper _mapper;

        public PersonaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersona, Persona>(MemberList.None).ReverseMap();
                cfg.CreateMap<TClasificacionPersona, ClasificacionPersona>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPersona MapeoEntidad(Persona entidad)
        {
            try
            {
                TPersona modelo = _mapper.Map<TPersona>(entidad);
                if (entidad.ClasificacionPersona != null)
                {
                    modelo.TClasificacionPersonas.Add(_mapper.Map<TClasificacionPersona>(entidad.ClasificacionPersona));
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersona Add(Persona entidad)
        {
            try
            {
                var Persona = MapeoEntidad(entidad);
                base.Insert(Persona);
                return Persona;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersona Update(Persona entidad)
        {
            try
            {
                var Persona = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Persona.RowVersion = entidadExistente.RowVersion;

                base.Update(Persona);
                return Persona;
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


        public IEnumerable<TPersona> Add(IEnumerable<Persona> listadoEntidad)
        {
            try
            {
                List<TPersona> listado = new List<TPersona>();
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

        public IEnumerable<TPersona> Update(IEnumerable<Persona> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersona> listado = new List<TPersona>();
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




        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de la tabla por su Id.
        /// </summary>
        /// <param name="idPersona">Id de la persona</param>
        /// <returns> Persona </returns>
        public Persona ObtenerPorId(int? idPersona)
        {
            try
            {
                var rpta = new Persona();

                var query = @"
                    SELECT
	                    Id,
	                    Email1,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM conf.T_Persona
                    WHERE
	                    Estado=1 
                        AND Id=@idPersona";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersona });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<Persona>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza una busqueda de la persona por su email
        /// </summary>
        /// <param name="email">email de la persona</param>
        /// <returns> 1 o 0 </returns>
        public bool ExistePorEmail(string email)
        {
            try
            {
                var query = @"SELECT TOP 1 Id AS Valor FROM conf.T_Persona WHERE Estado = 1 AND Email1 = @Email";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Email = email });
                return (!string.IsNullOrEmpty(resultado) && resultado != "null");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de la tabla por su email.
        /// </summary>
        /// <param name="email"> Email de la persona </param>
        /// <returns> Persona </returns>
        public Persona? ObtenerPorEmail(string email)
        {
            try
            {
                var query = @"SELECT TOP 1 Id,Email1,Estado,UsuarioCreacion,UsuarioModificacion,
                                        FechaCreacion,FechaModificacion,RowVersion,IdMigracion 
	                           FROM conf.T_Persona 
	                           WHERE Estado = 1 AND Email1 = @Email";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Email = email });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<Persona>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
