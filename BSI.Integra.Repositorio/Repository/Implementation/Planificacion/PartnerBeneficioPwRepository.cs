using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PartnerBeneficioPwRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_PartnerBeneficioPw
    /// </summary>
    public class PartnerBeneficioPwRepository : GenericRepository<TPartnerBeneficioPw>, IPartnerBeneficioPwRepository
    {
        private Mapper _mapper;

        public PartnerBeneficioPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPartnerBeneficioPw, PartnerBeneficioPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPartnerBeneficioPw MapeoEntidad(PartnerBeneficioPw entidad)
        {
            try
            {
                TPartnerBeneficioPw modelo = _mapper.Map<TPartnerBeneficioPw>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPartnerBeneficioPw Add(PartnerBeneficioPw entidad)
        {
            try
            {
                var PartnerBeneficioPw = MapeoEntidad(entidad);
                base.Insert(PartnerBeneficioPw);
                return PartnerBeneficioPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPartnerBeneficioPw Update(PartnerBeneficioPw entidad)
        {
            try
            {
                var PartnerBeneficioPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PartnerBeneficioPw.RowVersion = entidadExistente.RowVersion;

                base.Update(PartnerBeneficioPw);
                return PartnerBeneficioPw;
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
        public IEnumerable<TPartnerBeneficioPw> Add(IEnumerable<PartnerBeneficioPw> listadoEntidad)
        {
            try
            {
                List<TPartnerBeneficioPw> listado = new List<TPartnerBeneficioPw>();
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
        public IEnumerable<TPartnerBeneficioPw> Update(IEnumerable<PartnerBeneficioPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPartnerBeneficioPw> listado = new List<TPartnerBeneficioPw>();
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
        /// Obtiene PartnerBeneficioPw por id.
        /// </summary>
        /// <param name="id">Id PartnerBeneficioPw</param>
        /// <returns>PartnerBeneficioPw</returns>
        public PartnerBeneficioPw? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                        SELECT Id,
		                    IdPartner,
		                    Descripcion,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM pla.T_PartnerBeneficio_PW
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PartnerBeneficioPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PBPwR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PartnerBeneficioPw por id.
        /// </summary>
        /// <param name="idPartner">Id Partner</param>
        /// <returns>Lista PartnerBeneficioPw</returns>
        public IEnumerable<PartnerBeneficioPw> ObtenerPorIdPartner(int idPartner)
        {
            try
            {
                var query = @"
                        SELECT Id,
		                    IdPartner,
		                    Descripcion,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM pla.T_PartnerBeneficio_PW
                        WHERE Estado = 1 AND IdPartner=@idPartner";
                var resultado = _dapperRepository.QueryDapper(query, new { idPartner });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PartnerBeneficioPw>>(resultado)!;
                }
                return new List<PartnerBeneficioPw>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PBPwR-OPIP-001@Error en ObtenerPorIdPartner(), {ex.Message}");
            }
        }

    }
}



