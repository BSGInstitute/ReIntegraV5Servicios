using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class MaterialAsociacionVersionRepository : GenericRepository<TMaterialAsociacionVersion>, IMaterialAsociacionVersionRepository
    {
        private Mapper _mapper;

        public MaterialAsociacionVersionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialAsociacionVersion, MaterialAsociacionVersion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialAsociacionVersion MapeoEntidad(MaterialAsociacionVersion entidad)
        {
            try
            {
                TMaterialAsociacionVersion modelo = _mapper.Map<TMaterialAsociacionVersion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAsociacionVersion Add(MaterialAsociacionVersion entidad)
        {
            try
            {
                var materialAsociacionVersion = MapeoEntidad(entidad);
                base.Insert(materialAsociacionVersion);
                return materialAsociacionVersion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAsociacionVersion Update(MaterialAsociacionVersion entidad)
        {
            try
            {
                var materialAsociacionVersion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                materialAsociacionVersion.RowVersion = entidadExistente.RowVersion;

                base.Update(materialAsociacionVersion);
                return materialAsociacionVersion;
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
        public IEnumerable<TMaterialAsociacionVersion> Add(IEnumerable<MaterialAsociacionVersion> listadoEntidad)
        {
            try
            {
                List<TMaterialAsociacionVersion> listado = new List<TMaterialAsociacionVersion>();
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
        public IEnumerable<TMaterialAsociacionVersion> Update(IEnumerable<MaterialAsociacionVersion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialAsociacionVersion> listado = new List<TMaterialAsociacionVersion>();
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
        public IEnumerable<MaterialAsociacionVersion> ObtenerPorIdMaterialTipo(int idMaterialTipo)
        {
            IEnumerable<MaterialAsociacionVersion> rpta = new List<MaterialAsociacionVersion>();
            var query = @"SELECT Id,
                                   IdMaterialTipo,
                                   IdMaterialVersion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM ope.T_MaterialAsociacionVersion
                            WHERE Estado = 1
                                  AND IdMaterialTipo = @IdMaterialTipo;";
            var resultado = _dapperRepository.QueryDapper(query, new { IdMaterialTipo = idMaterialTipo });
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<MaterialAsociacionVersion>>(resultado)!;
            }
            return rpta;
        }
    }
}
