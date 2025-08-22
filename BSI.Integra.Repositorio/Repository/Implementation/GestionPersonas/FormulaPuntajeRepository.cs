using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class FormulaPuntajeRepository : GenericRepository<TSexo>, IFormulaPuntajeRepository
    {
        private Mapper _mapper;
        public FormulaPuntajeRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSexo, FormulaPuntaje>(MemberList.None).ReverseMap();
                cfg.CreateMap<FormulaPuntaje, FormulaPuntajeDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<FormulaPuntaje, TSexo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSexo MapeoEntidad(FormulaPuntaje entidad)
        {
            try
            {
                TSexo modelo = _mapper.Map<TSexo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSexo Add(FormulaPuntaje entidad)
        {
            try
            {
                var FormulaPuntaje = MapeoEntidad(entidad);
                Insert(FormulaPuntaje);
                return FormulaPuntaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSexo Update(FormulaPuntaje entidad)
        {
            try
            {
                var FormulaPuntaje = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FormulaPuntaje.RowVersion = entidadExistente.RowVersion;

                Update(FormulaPuntaje);
                return FormulaPuntaje;
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
        public IEnumerable<TSexo> Add(IEnumerable<FormulaPuntaje> listadoEntidad)
        {
            try
            {
                List<TSexo> listado = new List<TSexo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TSexo> Update(IEnumerable<FormulaPuntaje> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSexo> listado = new List<TSexo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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

        public FormulaPuntaje? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT
                        Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_FormulaPuntaje WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<FormulaPuntaje>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM gp.T_FormulaPuntaje WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<FormulaPuntajeDTO> Obtener()
        {
            try
            {
                IEnumerable<FormulaPuntajeDTO> rpta = new List<FormulaPuntajeDTO>();

                var query = @"
                    SELECT
                        Id,
                        Nombre
                    FROM gp.T_FormulaPuntaje 
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<FormulaPuntajeDTO>>(resultado)!;
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
