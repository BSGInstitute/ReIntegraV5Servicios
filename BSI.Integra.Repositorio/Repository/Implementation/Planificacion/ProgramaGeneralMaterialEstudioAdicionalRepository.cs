
using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralMaterialEstudioAdicionalRepository : GenericRepository<TProgramaGeneralMaterialEstudioAdicional>, IProgramaGeneralMaterialEstudioAdicionalRepository
    {
        private Mapper _mapper;
        public ProgramaGeneralMaterialEstudioAdicionalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralMaterialEstudioAdicional, ProgramaGeneralMaterialEstudioAdicional>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralMaterialEstudioAdicional MapeoEntidad(ProgramaGeneralMaterialEstudioAdicional entidad)
        {
            try
            {
                TProgramaGeneralMaterialEstudioAdicional modelo = _mapper.Map<TProgramaGeneralMaterialEstudioAdicional>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralMaterialEstudioAdicional Add(ProgramaGeneralMaterialEstudioAdicional entidad)
        {
            try
            {
                var programaGeneralMaterialEstudioAdicional = MapeoEntidad(entidad);
                base.Insert(programaGeneralMaterialEstudioAdicional);
                return programaGeneralMaterialEstudioAdicional;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralMaterialEstudioAdicional Update(ProgramaGeneralMaterialEstudioAdicional entidad)
        {
            try
            {
                var programaGeneralMaterialEstudioAdicional = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                programaGeneralMaterialEstudioAdicional.RowVersion = entidadExistente.RowVersion;

                base.Update(programaGeneralMaterialEstudioAdicional);
                return programaGeneralMaterialEstudioAdicional;
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
        public IEnumerable<TProgramaGeneralMaterialEstudioAdicional> Add(IEnumerable<ProgramaGeneralMaterialEstudioAdicional> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralMaterialEstudioAdicional> listado = new List<TProgramaGeneralMaterialEstudioAdicional>();
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
        public IEnumerable<TProgramaGeneralMaterialEstudioAdicional> Update(IEnumerable<ProgramaGeneralMaterialEstudioAdicional> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralMaterialEstudioAdicional> listado = new List<TProgramaGeneralMaterialEstudioAdicional>();
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
        public IEnumerable<ComboDTO> ObtenerProgramaGeneralMaterialEstudioAdicional()
        {
            IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
            var query = "SELECT IdPGeneral AS Id, Nombre FROM pla.V_ProgramaGeneralMaterialEstudioAdicional";
            var resultado = _dapperRepository.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
            }
            return rpta;
        }
        public IEnumerable<ProgramaGeneralMaterialEstudioAdicional> ObtenerPorIdPgeneral(int idPgeneral)
        {
            try
            {
                IEnumerable<ProgramaGeneralMaterialEstudioAdicional> rpta = new List<ProgramaGeneralMaterialEstudioAdicional>();
                var query = @"SELECT Id,
                                    IdPgeneral,
                                    NombreArchivo,
                                    EsEnlace,
                                    EnlaceArchivo,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    NombreConfiguracion
                                FROM pla.T_ProgramaGeneralMaterialEstudioAdicional
                                WHERE Estado = 1 AND IdPgeneral = @idPgeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPgeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralMaterialEstudioAdicional>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProgramaGeneralMaterialEstudioAdicional ObtenerPorId(int id)
        {
            try
            {
                ProgramaGeneralMaterialEstudioAdicional rpta = new ProgramaGeneralMaterialEstudioAdicional();
                var query = @"SELECT Id,
                                    IdPgeneral,
                                    NombreArchivo,
                                    EsEnlace,
                                    EnlaceArchivo,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    NombreConfiguracion
                                FROM pla.T_ProgramaGeneralMaterialEstudioAdicional
                                WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralMaterialEstudioAdicional>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
