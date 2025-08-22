using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FeedbackTipoRepository
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 12/05/2023
    /// <summary>
    /// Gestión general de T_FeedbackTipo
    /// </summary>
    public class MaterialAsociacionAccionRepository : GenericRepository<TMaterialAsociacionAccion>, IMaterialAsociacionAccionRepository
    {
        private Mapper _mapper;

        public MaterialAsociacionAccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialAsociacionAccion, MaterialAsociacionAccion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialAsociacionAccion MapeoEntidad(MaterialAsociacionAccion entidad)
        {
            try
            {
                TMaterialAsociacionAccion modelo = _mapper.Map<TMaterialAsociacionAccion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAsociacionAccion Add(MaterialAsociacionAccion entidad)
        {
            try
            {
                var MaterialAsociacionAccion = MapeoEntidad(entidad);
                base.Insert(MaterialAsociacionAccion);
                return MaterialAsociacionAccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAsociacionAccion Update(MaterialAsociacionAccion entidad)
        {
            try
            {
                var MaterialAsociacionAccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialAsociacionAccion.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialAsociacionAccion);
                return MaterialAsociacionAccion;
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
        public IEnumerable<TMaterialAsociacionAccion> Add(IEnumerable<MaterialAsociacionAccion> listadoEntidad)
        {
            try
            {
                List<TMaterialAsociacionAccion> listado = new List<TMaterialAsociacionAccion>();
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
        public IEnumerable<TMaterialAsociacionAccion> Update(IEnumerable<MaterialAsociacionAccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialAsociacionAccion> listado = new List<TMaterialAsociacionAccion>();
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
        public IEnumerable<MaterialAsociacionAccion> ObtenerPorIdMaterialTipo(int idMaterialTipo)
        {
            IEnumerable<MaterialAsociacionAccion> rpta = new List<MaterialAsociacionAccion>();
            var query = @"SELECT Id,
                                   IdMaterialTipo,
                                   IdMaterialAccion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM ope.T_MaterialAsociacionAccion
                            WHERE Estado = 1
                                  AND IdMaterialTipo = @IdMaterialTipo;";
            var resultado = _dapperRepository.QueryDapper(query, new { IdMaterialTipo = idMaterialTipo });
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<MaterialAsociacionAccion>>(resultado)!;
            }
            return rpta;
        }
    }
}
