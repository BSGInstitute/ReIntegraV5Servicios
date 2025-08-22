using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FurConfiguracionAutomaticaRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_FurConfiguracionAutomatica
    /// </summary>
    public class FurConfiguracionAutomaticaRepository : GenericRepository<TFurConfiguracionAutomatica>, IFurConfiguracionAutomaticaRepository
    {
        private Mapper _mapper;

        public FurConfiguracionAutomaticaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFurConfiguracionAutomatica, FurConfiguracionAutomatica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFurConfiguracionAutomatica MapeoEntidad(FurConfiguracionAutomatica entidad)
        {
            try
            {
                //crea la entidad padre
                TFurConfiguracionAutomatica modelo = _mapper.Map<TFurConfiguracionAutomatica>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurConfiguracionAutomatica Add(FurConfiguracionAutomatica entidad)
        {
            try
            {
                var FurConfiguracionAutomatica = MapeoEntidad(entidad);
                base.Insert(FurConfiguracionAutomatica);
                return FurConfiguracionAutomatica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurConfiguracionAutomatica Update(FurConfiguracionAutomatica entidad)
        {
            try
            {
                var FurConfiguracionAutomatica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FurConfiguracionAutomatica.RowVersion = entidadExistente.RowVersion;

                base.Update(FurConfiguracionAutomatica);
                return FurConfiguracionAutomatica;
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


        public IEnumerable<TFurConfiguracionAutomatica> Add(IEnumerable<FurConfiguracionAutomatica> listadoEntidad)
        {
            try
            {
                List<TFurConfiguracionAutomatica> listado = new List<TFurConfiguracionAutomatica>();
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

        public IEnumerable<TFurConfiguracionAutomatica> Update(IEnumerable<FurConfiguracionAutomatica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFurConfiguracionAutomatica> listado = new List<TFurConfiguracionAutomatica>();
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
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por ID
        /// </summary>
        /// <returns> FurConfiguracionAutomatica </returns>
        public FurConfiguracionAutomatica ObtenerFurConfiguracionAutomaticaById( int id )
        {
            try
            {
                FurConfiguracionAutomatica rpta = new FurConfiguracionAutomatica();
                var query = @"
                    SELECT *
                    FROM fin.T_FurConfiguracionAutomatica
                    WHERE Estado = 1 AND Id =@Id ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<FurConfiguracionAutomatica>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de FurConfiguracionAutomatica por IdArea
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaByIdArea(string idArea)
        {
            try
            {
                List<FurConfiguracionAutomaticaVersionDetalleDTO> rpta = new List<FurConfiguracionAutomaticaVersionDetalleDTO>();
                var query = @"
                     SELECT Id, IdSede,NombreSede, IdFurTipoPedido,NombreFurTipoPedido,IdPersonalAreaTrabajo,NombrePersonalAreaTrabajo,Cantidad,IdMonedaPagoReal,NombreMonedaPagoReal,AjusteNumeroSemana,RucProveedor,
                            NombreProveedor,NombreProducto, IdFrecuencia,NombreFrecuencia,
                            NombreCentroCosto,Descripcion, FechaGeneracionFur,FechaInicioConfiguracion, FechaFinConfiguracion,
                            IdProducto, IdProveedor, IdProductoPresentacion, 
                            IdCentroCosto AS IdCentroCosto, PrecioUnitario, 
                            MontoProyectado, UsuarioModificacion AS UsuarioSolicitud, NombreFurTipoSolicitud,IdEmpresa,RazonSocial AS NombreEmpresa,IdHistoricoProductoProveedor,UsuarioCreacion
                    FROM fin.V_ObtenerConfiguracionAutomaticaConProveedorYProducto 
                    where  Estado=1  AND IdPersonalAreaTrabajo in (select item from conf.F_Splitstring(@IdArea,',')) order by Id desc";
                var resultado = _dapperRepository.QueryDapper(query, new { IdArea = idArea });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FurConfiguracionAutomaticaVersionDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros Activos de FurConfiguracionAutomatica por IdArea
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaByIdAreaActivo(string idArea)
        {
            try
            {
                List<FurConfiguracionAutomaticaVersionDetalleDTO> rpta = new List<FurConfiguracionAutomaticaVersionDetalleDTO>();
                var query = @"
                     SELECT Id, IdSede,NombreSede, IdFurTipoPedido,NombreFurTipoPedido,IdPersonalAreaTrabajo,NombrePersonalAreaTrabajo,Cantidad,IdMonedaPagoReal,NombreMonedaPagoReal,AjusteNumeroSemana,RucProveedor,
                            NombreProveedor,NombreProducto, IdFrecuencia,NombreFrecuencia,
                            NombreCentroCosto,Descripcion, FechaGeneracionFur,FechaInicioConfiguracion, FechaFinConfiguracion,
                            IdProducto, IdProveedor, IdProductoPresentacion, 
                            IdCentroCosto AS IdCentroCosto, PrecioUnitario, 
                            MontoProyectado, UsuarioModificacion AS UsuarioSolicitud, NombreFurTipoSolicitud,IdEmpresa,RazonSocial AS NombreEmpresa,IdHistoricoProductoProveedor,UsuarioCreacion
                    FROM fin.V_ObtenerConfiguracionAutomaticaConProveedorYProducto 
                    where Activo=1 AND Estado=1  AND IdPersonalAreaTrabajo in (select item from conf.F_Splitstring(@IdArea,',')) order by Id desc";
                var resultado = _dapperRepository.QueryDapper(query, new { IdArea = idArea });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FurConfiguracionAutomaticaVersionDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de FurConfiguracionAutomatica por IdArea que no sean validos
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaNoValida(ParametrosEnvioDTO data)
        {
            try
            {
                List<FurConfiguracionAutomaticaVersionDetalleDTO> rpta = new List<FurConfiguracionAutomaticaVersionDetalleDTO>();
                var query = @"
                    SELECT Id, IdSede,NombreSede, IdFurTipoPedido,NombreFurTipoPedido,IdPersonalAreaTrabajo,NombrePersonalAreaTrabajo,Cantidad,IdMonedaPagoReal,NombreMonedaPagoReal,AjusteNumeroSemana,RucProveedor,
                            NombreProveedor,NombreProducto, IdFrecuencia,NombreFrecuencia,
                            NombreCentroCosto,Descripcion, FechaGeneracionFur,FechaInicioConfiguracion, FechaFinConfiguracion,
                            IdProducto, IdProveedor, IdProductoPresentacion, 
                            IdCentroCosto AS IdCentroCosto, PrecioUnitario, 
                            MontoProyectado, UsuarioModificacion AS UsuarioSolicitud, NombreFurTipoSolicitud,IdEmpresa,RazonSocial AS NombreEmpresa,IdHistoricoProductoProveedor,UsuarioCreacion
                    FROM fin.V_ObtenerConfiguracionAutomaticaConProveedorYProducto 
                    where Estado=1 AND IdPersonalAreaTrabajo IN (select item from conf.F_Splitstring(@IdArea,',')) AND Id IN (select item from conf.F_Splitstring(@IdSeleccion,',')) 
	                AND ( IdHistoricoProductoProveedor not in (SELECT [IdHistoricoProductoProveedor] FROM [fin].[V_ObtenerProductoPorProveedorV5]) or IdEmpresa = 0 ) order by Id desc ";

                var resultado = _dapperRepository.QueryDapper(query, new { IdArea = data.IdAreas , IdSeleccion = data .IdSeleccion});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FurConfiguracionAutomaticaVersionDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta los FUrs PRoyecctados para CostosFijos
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public bool InsertarFursParaProyeccionCostosFijos(string data)
        {
            try
            {
                var query = @"[fin].[SP_InsertarFurProyectado]";

                var resultado = _dapperRepository.QuerySPDapper(query, new { Json = data });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null") return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Activa las configuracion 
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public bool CambiarActivoFurConfiguracionAutomatica(string IdSeleccion,string Usuario)
        {
            try
            {
                var query = @"[fin].[SP_CambiarActivoFurConfiguracionAutomatica]";

                var resultado = _dapperRepository.QuerySPDapper(query, new { IdSeleccion = IdSeleccion, Usuario = Usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null") return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Desactiva las configuracion es
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public bool DesactivarFurConfiguracionAutomatica(int IdArea, string Usuario)
        {
            try
            {
                var query = @"[fin].[SP_DesactivarFurConfiguracionAutomatica]";

                var resultado = _dapperRepository.QuerySPDapper(query, new { IdArea = IdArea, Usuario = Usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null") return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
