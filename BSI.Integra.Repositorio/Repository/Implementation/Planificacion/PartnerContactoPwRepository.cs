using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PartnerContactoPwRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_PartnerContactoPw
    /// </summary>
    public class PartnerContactoPwRepository : GenericRepository<TPartnerContactoPw>, IPartnerContactoPwRepository
    {
        private Mapper _mapper;

        public PartnerContactoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPartnerContactoPw, PartnerContactoPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPartnerContactoPw MapeoEntidad(PartnerContactoPw entidad)
        {
            try
            {
                TPartnerContactoPw modelo = _mapper.Map<TPartnerContactoPw>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPartnerContactoPw Add(PartnerContactoPw entidad)
        {
            try
            {
                var PartnerContactoPw = MapeoEntidad(entidad);
                base.Insert(PartnerContactoPw);
                return PartnerContactoPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPartnerContactoPw Update(PartnerContactoPw entidad)
        {
            try
            {
                var PartnerContactoPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PartnerContactoPw.RowVersion = entidadExistente.RowVersion;

                base.Update(PartnerContactoPw);
                return PartnerContactoPw;
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
        public IEnumerable<TPartnerContactoPw> Add(IEnumerable<PartnerContactoPw> listadoEntidad)
        {
            try
            {
                List<TPartnerContactoPw> listado = new List<TPartnerContactoPw>();
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
        public IEnumerable<TPartnerContactoPw> Update(IEnumerable<PartnerContactoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPartnerContactoPw> listado = new List<TPartnerContactoPw>();
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
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PartnerContactoPw por id.
        /// </summary>
        /// <param name="id">Id PartnerContactoPw</param>
        /// <returns>PartnerContactoPw</returns>
        public PartnerContactoPw? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                        SELECT Id,
		                    IdPartner,
		                    Nombres,
		                    Apellidos,
		                    Email1,
		                    Email2,
		                    Telefono1,
		                    Telefono2,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion 
	                    FROM pla.T_PartnerContacto_PW
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PartnerContactoPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PCPwR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PartnerContactoPw por id.
        /// </summary>
        /// <param name="idPartner">Id Partner</param>
        /// <returns>PartnerContactoPw</returns>
        public IEnumerable<PartnerContactoPw> ObtenerPorIdPartner(int idPartner)
        {
            try
            {
                var query = @"
                        SELECT Id,
		                    IdPartner,
		                    Nombres,
		                    Apellidos,
		                    Email1,
		                    Email2,
		                    Telefono1,
		                    Telefono2,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion 
	                    FROM pla.T_PartnerContacto_PW
                        WHERE Estado = 1 AND IdPartner=@idPartner";
                var resultado = _dapperRepository.QueryDapper(query, new { idPartner });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PartnerContactoPw>>(resultado)!;
                }
                return new List<PartnerContactoPw>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PCPwR-OPIP-001@Error en ObtenerPorIdPartner(), {ex.Message}");
            }
        }
    }
}



