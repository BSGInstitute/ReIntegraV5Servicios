using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: TipoDocumentoAlumnoRepository
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 16/05/2023
    /// <summary>
    /// Gestión general de T_MaterialTipo
    /// </summary>
    public class MaterialTipoRepository : GenericRepository<TMaterialTipo>, IMaterialTipoRepository
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MaterialTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialTipo, MaterialTipo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMaterialAsociacionAccion, MaterialAsociacionAccion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMaterialAsociacionVersion, MaterialAsociacionVersion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMaterialAsociacionCriterioVerificacion, MaterialAsociacionCriterioVerificacion>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialTipo MapeoEntidad(MaterialTipo entidad)
        {
            try
            {
                TMaterialTipo modelo = _mapper.Map<TMaterialTipo>(entidad);
                if (entidad.MaterialAsociacionAccions != null && entidad.MaterialAsociacionAccions.Count >= 1)
                {
                    modelo.TMaterialAsociacionAccions = _mapper.Map<List<TMaterialAsociacionAccion>>(entidad.MaterialAsociacionAccions);
                }
                if (entidad.MaterialAsociacionVersions != null && entidad.MaterialAsociacionVersions.Count >= 1)
                {
                    modelo.TMaterialAsociacionVersions = _mapper.Map<List<TMaterialAsociacionVersion>>(entidad.MaterialAsociacionVersions);
                }
                if (entidad.MaterialAsociacionCriterioVerificacions != null && entidad.MaterialAsociacionCriterioVerificacions.Count >= 1)
                {
                    modelo.TMaterialAsociacionCriterioVerificacions = _mapper.Map<List<TMaterialAsociacionCriterioVerificacion>>(entidad.MaterialAsociacionCriterioVerificacions);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialTipo Add(MaterialTipo entidad)
        {
            try
            {
                var MaterialTipo = MapeoEntidad(entidad);
                base.Insert(MaterialTipo);
                return MaterialTipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialTipo Update(MaterialTipo entidad)
        {
            try
            {
                var MaterialTipo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialTipo.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialTipo);
                return MaterialTipo;
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
        public IEnumerable<TMaterialTipo> Add(IEnumerable<MaterialTipo> listadoEntidad)
        {
            try
            {
                List<TMaterialTipo> listado = new List<TMaterialTipo>();
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
        public IEnumerable<TMaterialTipo> Update(IEnumerable<MaterialTipo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialTipo> listado = new List<TMaterialTipo>();
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

        public IEnumerable<MaterialTipoDetalleDTO> Obtener()
        {
            try
            {
                IEnumerable<MaterialTipoDetalleDTO> rpta = new List<MaterialTipoDetalleDTO>();
                var query = @"select 
	                        Id
	                        ,Nombre
	                        ,Descripcion
	                        ,IdAccion
	                        ,IdMaterialAccion
	                        ,NombreMaterialAccion
	                        ,IdCriterio
	                        ,IdMaterialCriterioVerificacion
	                        ,NombreMaterialCriterioVerificacion
	                        ,IdVersion
	                        ,IdMaterialVersion
	                        ,NombreMaterialVersion
                        from
	                        pla.V_MaterialTipoDetalle";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<MaterialTipoDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<MaterialTipoDetalleDTO> ObtenerRelacionesPorId(int idTipoMaterial)
        {
            try
            {
                IEnumerable<MaterialTipoDetalleDTO> rpta = new List<MaterialTipoDetalleDTO>();
                var query = @"select 
	                Id
	                ,Nombre
	                ,Descripcion
	                ,IdAccion
	                ,IdMaterialAccion
	                ,NombreMaterialAccion
	                ,IdCriterio
	                ,IdMaterialCriterioVerificacion
	                ,NombreMaterialCriterioVerificacion
	                ,IdVersion
	                ,IdMaterialVersion
	                ,NombreMaterialVersion
                from
	                pla.V_MaterialTipoDetalle where Id = @idTipoMaterial";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoMaterial });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<MaterialTipoDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MaterialTipo ObtenerPorId(int idTipoMaterial)
        {
            MaterialTipo rpta = new MaterialTipo();
            var query = "SELECT Id, Nombre, Descripcion, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion FROM ope.T_MaterialTipo WHERE ESTADO = 1 AND Id = @IdTipoMaterial";
            var resultado = _dapperRepository.FirstOrDefault(query, new { idTipoMaterial });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<MaterialTipo>(resultado);
            }
            return rpta;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MaterialTipo para mostrarse en combo.
        /// </summary>
        /// <returns> List<MaterialVersionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = "SELECT Id,Nombre FROM ope.T_MaterialTipo WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MTR-OC-001@Error en ObtenerCombo() {ex.Message}", ex);
            }
        }
    }
}
