using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ProgramaGeneralCertificacionModalidadRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 25/07/2023
    /// <summary>
    /// Gestión general de T_ProgramaGeneralCertificacionModalidad
    /// </summary>
    public class ProgramaGeneralCertificacionModalidadRepository : GenericRepository<TProgramaGeneralCertificacionModalidad>, IProgramaGeneralCertificacionModalidadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralCertificacionModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralCertificacionModalidad, ProgramaGeneralCertificacionModalidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralCertificacionModalidad MapeoEntidad(ProgramaGeneralCertificacionModalidad entidad)
        {
            try
            {
                TProgramaGeneralCertificacionModalidad modelo = _mapper.Map<TProgramaGeneralCertificacionModalidad>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralCertificacionModalidad Add(ProgramaGeneralCertificacionModalidad entidad)
        {
            try
            {
                var ProgramaGeneralCertificacionModalidad = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralCertificacionModalidad);
                return ProgramaGeneralCertificacionModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralCertificacionModalidad Update(ProgramaGeneralCertificacionModalidad entidad)
        {
            try
            {
                var ProgramaGeneralCertificacionModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralCertificacionModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralCertificacionModalidad);
                return ProgramaGeneralCertificacionModalidad;
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


        public IEnumerable<TProgramaGeneralCertificacionModalidad> Add(IEnumerable<ProgramaGeneralCertificacionModalidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralCertificacionModalidad> listado = new List<TProgramaGeneralCertificacionModalidad>();
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

        public IEnumerable<TProgramaGeneralCertificacionModalidad> Update(IEnumerable<ProgramaGeneralCertificacionModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralCertificacionModalidad> listado = new List<TProgramaGeneralCertificacionModalidad>();
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
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralCertificacionModalidad.
        /// </summary>
        /// <param name="id">Id ProgramaGeneralCertificacionModalidad</param>
        /// <returns> ProgramaGeneralCertificacionModalidad </returns>
        public ProgramaGeneralCertificacionModalidad? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                   SELECT Id,
		                IdProgramaGeneralCertificacion,
		                IdModalidadCurso,
		                Nombre,
		                IdPGeneral,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM pla.T_ProgramaGeneralCertificacionModalidad WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralCertificacionModalidad>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralCertificacionModalidad.
        /// </summary>
        /// <param name="ids">Ids ProgramaGeneralCertificacionModalidad</param>
        /// <returns> ProgramaGeneralCertificacionModalidad </returns>
        public IEnumerable<ProgramaGeneralCertificacionModalidad> ObtenerPorIds(List<int> ids)
        {
            try
            {
                var query = @"
                   SELECT Id,
		                IdProgramaGeneralCertificacion,
		                IdModalidadCurso,
		                Nombre,
		                IdPGeneral,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM pla.T_ProgramaGeneralCertificacionModalidad
                    WHERE Estado=1 AND Id IN @ids";
                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralCertificacionModalidad>>(resultado)!;
                }
                return new List<ProgramaGeneralCertificacionModalidad>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



