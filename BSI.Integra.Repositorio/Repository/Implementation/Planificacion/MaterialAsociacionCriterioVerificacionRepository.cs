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
    public class MaterialAsociacionCriterioVerificacionRepository : GenericRepository<TMaterialAsociacionCriterioVerificacion>, IMaterialAsociacionCriterioVerificacionRepository
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MaterialAsociacionCriterioVerificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MaterialAsociacionCriterioVerificacion, TMaterialAsociacionCriterioVerificacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialAsociacionCriterioVerificacion MapeoEntidad(MaterialAsociacionCriterioVerificacion entidad)
        {
            try
            {
                TMaterialAsociacionCriterioVerificacion modelo = _mapper.Map<TMaterialAsociacionCriterioVerificacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAsociacionCriterioVerificacion Add(MaterialAsociacionCriterioVerificacion entidad)
        {
            try
            {
                var materialAsociacionCriterioVerificacion = MapeoEntidad(entidad);
                base.Insert(materialAsociacionCriterioVerificacion);
                return materialAsociacionCriterioVerificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAsociacionCriterioVerificacion Update(MaterialAsociacionCriterioVerificacion entidad)
        {
            try
            {
                var materialAsociacionCriterioVerificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                materialAsociacionCriterioVerificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(materialAsociacionCriterioVerificacion);
                return materialAsociacionCriterioVerificacion;
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
        public IEnumerable<TMaterialAsociacionCriterioVerificacion> Add(IEnumerable<MaterialAsociacionCriterioVerificacion> listadoEntidad)
        {
            try
            {
                List<TMaterialAsociacionCriterioVerificacion> listado = new List<TMaterialAsociacionCriterioVerificacion>();
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
        public IEnumerable<TMaterialAsociacionCriterioVerificacion> Update(IEnumerable<MaterialAsociacionCriterioVerificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialAsociacionCriterioVerificacion> listado = new List<TMaterialAsociacionCriterioVerificacion>();
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
        public IEnumerable<MaterialAsociacionCriterioVerificacion> ObtenerPorIdMaterialTipo(int idMaterialTipo)
        {
            IEnumerable<MaterialAsociacionCriterioVerificacion> rpta = new List<MaterialAsociacionCriterioVerificacion>();
            var query = @"SELECT Id,
                                   IdMaterialTipo,
                                   IdMaterialCriterioVerificacion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM ope.T_MaterialAsociacionCriterioVerificacion
                            WHERE Estado = 1
                                  AND IdMaterialTipo = @IdMaterialTipo;";
            var resultado = _dapperRepository.QueryDapper(query, new { IdMaterialTipo = idMaterialTipo });
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<MaterialAsociacionCriterioVerificacion>>(resultado)!;
            }
            return rpta;
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 2023-11-03
        /// <summary>
        /// Obtiene lista de criterios de verificacion asociados a un material pespecifico detalle
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle"></param>
        /// <returns></returns>


        public async Task<List<MaterialDetalleCriterioVerificacionDTO>> ObtenerCriteriosVerificacionPorMaterialDetalleAsync(int idMaterialPEspecificoDetalle)
        {
            try
            {
                var lista = new List<MaterialDetalleCriterioVerificacionDTO>();
                var query = "SELECT Id, IdMaterialPEspecificoDetalle, IdMaterialCriterioVerificacion, MaterialCriterioVerificacion, EsAprobado FROM ope.V_TMaterialCriterioVerificacionDetalle_ObtenerCriterios WHERE IdMaterialPEspecificoDetalle = @IdMaterialPEspecificoDetalle AND Estado = 1";

                var parametros = new { IdMaterialPEspecificoDetalle = idMaterialPEspecificoDetalle };

                var resultadoDB = await _dapperRepository.QueryDapperAsync(query, parametros);

                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<MaterialDetalleCriterioVerificacionDTO>>(resultadoDB);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener criterios de verificación: " + e.Message);
            }
        }
        




    }
}
