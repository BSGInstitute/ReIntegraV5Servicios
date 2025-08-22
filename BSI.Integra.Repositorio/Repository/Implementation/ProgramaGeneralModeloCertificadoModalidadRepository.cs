using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralModeloCertificadoModalidadRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 26/07/2023
    /// <summary>
    /// Gestión general de TProgramaGeneralModeloCertificadoModalidad
    /// </summary>
    public class ProgramaGeneralModeloCertificadoModalidadRepository : GenericRepository<TProgramaGeneralModeloCertificadoModalidad>, IProgramaGeneralModeloCertificadoModalidadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralModeloCertificadoModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralModeloCertificadoModalidad, ProgramaGeneralModeloCertificadoModalidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralModeloCertificadoModalidad MapeoEntidad(ProgramaGeneralModeloCertificadoModalidad entidad)
        {
            try
            {
                return _mapper.Map<TProgramaGeneralModeloCertificadoModalidad>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralModeloCertificadoModalidad Add(ProgramaGeneralModeloCertificadoModalidad entidad)
        {
            try
            {
                var ProgramaGeneralModeloCertificadoModalidad = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralModeloCertificadoModalidad);
                return ProgramaGeneralModeloCertificadoModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralModeloCertificadoModalidad Update(ProgramaGeneralModeloCertificadoModalidad entidad)
        {
            try
            {
                var ProgramaGeneralModeloCertificadoModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralModeloCertificadoModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralModeloCertificadoModalidad);
                return ProgramaGeneralModeloCertificadoModalidad;
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
        public IEnumerable<TProgramaGeneralModeloCertificadoModalidad> Add(IEnumerable<ProgramaGeneralModeloCertificadoModalidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralModeloCertificadoModalidad> listado = new List<TProgramaGeneralModeloCertificadoModalidad>();
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
        public IEnumerable<TProgramaGeneralModeloCertificadoModalidad> Update(IEnumerable<ProgramaGeneralModeloCertificadoModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralModeloCertificadoModalidad> listado = new List<TProgramaGeneralModeloCertificadoModalidad>();
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
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Courier.
        /// </summary>
        /// <returns> List<CourierDTO> </returns>
        public ProgramaGeneralModeloCertificadoModalidad? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                   SELECT Id,
	                    IdProgramaGeneralModeloCertificado,
	                    IdModalidadCurso,
	                    Nombre,
	                    IdPGeneral AS IdPgeneral,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_ProgramaGeneralModeloCertificadoModalidad 
                    WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralModeloCertificadoModalidad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Courier.
        /// </summary>
        /// <returns> List<CourierDTO> </returns>
        public ProgramaGeneralModeloCertificadoModalidad? ObtenerPorIdModeloCertificadoIdModalidadCurso(int idModeloCertificado, int idModalidadCurso)
        {
            try
            {
                var query = @"
                   SELECT Id,
	                    IdProgramaGeneralModeloCertificado,
	                    IdModalidadCurso,
	                    Nombre,
	                    IdPGeneral AS IdPgeneral,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_ProgramaGeneralModeloCertificadoModalidad 
                    WHERE Estado=1 AND IdProgramaGeneralModeloCertificado=@idModeloCertificado AND IdModalidadCurso=@idModalidadCurso";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idModeloCertificado, idModalidadCurso });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralModeloCertificadoModalidad>(resultado)!;
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
