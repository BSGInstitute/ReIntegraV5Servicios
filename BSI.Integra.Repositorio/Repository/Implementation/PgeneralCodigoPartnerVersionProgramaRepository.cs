using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PgeneralCodigoPartnerVersionProgramaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PgeneralCodigoPartnerVersionPrograma
    /// </summary>
    public class PgeneralCodigoPartnerVersionProgramaRepository : GenericRepository<TPgeneralCodigoPartnerVersionPrograma>, IPgeneralCodigoPartnerVersionProgramaRepository
    {
        private Mapper _mapper;

        public PgeneralCodigoPartnerVersionProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralCodigoPartnerVersionPrograma, PgeneralCodigoPartnerVersionPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPgeneralCodigoPartnerVersionPrograma MapeoEntidad(PgeneralCodigoPartnerVersionPrograma entidad)
        {
            try
            {
                TPgeneralCodigoPartnerVersionPrograma modelo = _mapper.Map<TPgeneralCodigoPartnerVersionPrograma>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralCodigoPartnerVersionPrograma Add(PgeneralCodigoPartnerVersionPrograma entidad)
        {
            try
            {
                var PgeneralCodigoPartnerVersionPrograma = MapeoEntidad(entidad);
                base.Insert(PgeneralCodigoPartnerVersionPrograma);
                return PgeneralCodigoPartnerVersionPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPgeneralCodigoPartnerVersionPrograma Update(PgeneralCodigoPartnerVersionPrograma entidad)
        {
            try
            {
                var PgeneralCodigoPartnerVersionPrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralCodigoPartnerVersionPrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralCodigoPartnerVersionPrograma);
                return PgeneralCodigoPartnerVersionPrograma;
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
        public IEnumerable<TPgeneralCodigoPartnerVersionPrograma> Add(IEnumerable<PgeneralCodigoPartnerVersionPrograma> listadoEntidad)
        {
            try
            {
                List<TPgeneralCodigoPartnerVersionPrograma> listado = new List<TPgeneralCodigoPartnerVersionPrograma>();
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
        public IEnumerable<TPgeneralCodigoPartnerVersionPrograma> Update(IEnumerable<PgeneralCodigoPartnerVersionPrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralCodigoPartnerVersionPrograma> listado = new List<TPgeneralCodigoPartnerVersionPrograma>();
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
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> PgeneralCodigoPartnerModalidadCurso </returns>
        public PgeneralCodigoPartnerVersionPrograma? ObtenerPorIdVersionProgramaIdPgeneralCodigoPartner(int idVersionPrograma, int idPgeneralCodigoPartner)
        {
            try
            {
                var query = @"
                    SELECT 
	                    Id,
	                    IdPgeneralCodigoPartner,
	                    IdVersionPrograma,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_PgeneralCodigoPartnerVersionPrograma
                    WHERE Estado=1 AND IdVersionPrograma=@idVersionPrograma AND IdPgeneralCodigoPartner=@idPgeneralCodigoPartner";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idVersionPrograma, idPgeneralCodigoPartner });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralCodigoPartnerVersionPrograma>(resultado)!;
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
