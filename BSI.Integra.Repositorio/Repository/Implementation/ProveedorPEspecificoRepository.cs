using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProveedorPEspecificoRepository
    /// Fecha: 2026-03-19
    /// Version: 1.0
    /// <summary>
    /// Gestión de la tabla pla.T_ProveedorPEspecifico
    /// </summary>
    public class ProveedorPEspecificoRepository : GenericRepository<TProveedorPespecifico>, IProveedorPEspecificoRepository
    {
        private Mapper _mapper;

        public ProveedorPEspecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedorPespecifico, ProveedorPespecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProveedorPespecifico MapeoEntidad(ProveedorPespecifico entidad)
        {
            try
            {
                return _mapper.Map<TProveedorPespecifico>(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en MapeoEntidad(ProveedorPespecifico)", ex);
            }
        }

        public TProveedorPespecifico Add(ProveedorPespecifico entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                base.Insert(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Add(ProveedorPespecifico)", ex);
            }
        }

        public TProveedorPespecifico Update(ProveedorPespecifico entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                var existente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modelo.RowVersion = existente.RowVersion;
                base.Update(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Update(ProveedorPespecifico)", ex);
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
                throw new Exception("Error en Delete(ProveedorPespecifico)", ex);
            }
        }
        #endregion

  
        public IEnumerable<ProveedorActivoPEspecificoDTO> ObtenerActivoPEspecifico()
        {
            try
            {
                List<ProveedorActivoPEspecificoDTO> rpta = new();
                var query = @"
                    SELECT
                        Id,
                        Nombre,
                       FechaAsignacion
                    FROM pla.V_ProveedorPEspecificoListado ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<ProveedorActivoPEspecificoDTO>>(resultado)!;
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerActivoPEspecifico()", ex);
            }
        }

        /// <summary>
        /// Retorna todos los cursos (PEspecificos) asignados a un proveedor específico con Estado=1.
        /// </summary>
        public IEnumerable<ProveedorPEspecificoGridDTO> ObtenerPorIdProveedor(int idProveedor)
        {
            try
            {
                List<ProveedorPEspecificoGridDTO> rpta = new List<ProveedorPEspecificoGridDTO>();

                var query = "pla.SP_ProveedorPEspecificoObtenerPorProveedor";
                var parametros = new
                {
                    IdProveedor = idProveedor
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProveedorPEspecificoGridDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-PLA-001@Error en ObtenerPorIdProveedor() {ex.Message}", ex);
            }
            
        }

        /// <summary>
        /// Verifica si ya existe una asignación activa entre un proveedor y un PEspecifico.
        /// </summary>
        public bool ExistePorProveedorYPespecifico(int idProveedor, int idPespecifico)
        {
            try
            {
                var query = @"
                    SELECT TOP 1 Id
                    FROM pla.T_ProveedorPEspecifico
                    WHERE IdProveedor = @idProveedor AND IdPespecifico = @idPespecifico AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idProveedor, idPespecifico });
                return !string.IsNullOrEmpty(resultado) && resultado != "null";
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ExistePorProveedorYPespecifico()", ex);
            }
        }

        /// <summary>
        /// Obtiene los proveedores asociados a un PEspecifico para el combo del cronograma.
        /// Si se proporciona un filtro de nombre, busca proveedores adicionales que coincidan.
        /// </summary>
        /// <param name="idPEspecifico">Id del programa específico</param>
        /// <param name="filtroNombre">Filtro opcional por nombre del proveedor</param>
        /// <returns>Lista de proveedores para combo</returns>
        public IEnumerable<ProveedorPorPEspecificoComboDTO> ObtenerProveedoresPorPEspecifico(int idPEspecifico, string? filtroNombre = null)
        {
            try
            {
                List<ProveedorPorPEspecificoComboDTO> rpta = new();

                string sp;
                object parametros;

                if (string.IsNullOrWhiteSpace(filtroNombre))
                {
                    sp = "pla.SP_ProveedorPEspecifico_ObtenerPorIdPEspecifico";
                    parametros = new { IdPEspecifico = idPEspecifico };
                }
                else
                {
                    sp = "pla.SP_ProveedorPEspecifico_BuscarPorNombre";
                    parametros = new { FiltroNombre = filtroNombre };
                }

                var resultado = _dapperRepository.QuerySPDapper(sp, parametros);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<ProveedorPorPEspecificoComboDTO>>(resultado)!;

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerProveedoresPorPEspecifico(): {ex.Message}", ex);
            }
        }
    }
}
