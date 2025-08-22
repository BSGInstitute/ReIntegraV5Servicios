using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TableroComercialCategoriaAsesorRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_TableroComercialCategoriaAsesor
    /// </summary>
    public class TableroComercialCategoriaAsesorRepository : GenericRepository<TTableroComercialCategoriaAsesor>, ITableroComercialCategoriaAsesorRepository
    {
        private Mapper _mapper;

        public TableroComercialCategoriaAsesorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTableroComercialCategoriaAsesor, TableroComercialCategoriaAsesor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTableroComercialCategoriaAsesor MapeoEntidad(TableroComercialCategoriaAsesor entidad)
        {
            try
            {
                //crea la entidad padre
                TTableroComercialCategoriaAsesor modelo = _mapper.Map<TTableroComercialCategoriaAsesor>(entidad);

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

        public TTableroComercialCategoriaAsesor Add(TableroComercialCategoriaAsesor entidad)
        {
            try
            {
                var TableroComercialCategoriaAsesor = MapeoEntidad(entidad);
                base.Insert(TableroComercialCategoriaAsesor);
                return TableroComercialCategoriaAsesor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTableroComercialCategoriaAsesor Update(TableroComercialCategoriaAsesor entidad)
        {
            try
            {
                var TableroComercialCategoriaAsesor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TableroComercialCategoriaAsesor.RowVersion = entidadExistente.RowVersion;

                base.Update(TableroComercialCategoriaAsesor);
                return TableroComercialCategoriaAsesor;
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


        public IEnumerable<TTableroComercialCategoriaAsesor> Add(IEnumerable<TableroComercialCategoriaAsesor> listadoEntidad)
        {
            try
            {
                List<TTableroComercialCategoriaAsesor> listado = new List<TTableroComercialCategoriaAsesor>();
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

        public IEnumerable<TTableroComercialCategoriaAsesor> Update(IEnumerable<TableroComercialCategoriaAsesor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTableroComercialCategoriaAsesor> listado = new List<TTableroComercialCategoriaAsesor>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TableroComercialCategoriaAsesor.
        /// </summary>
        /// <returns> List<TableroComercialCategoriaAsesorDTO> </returns>
        public IEnumerable<TableroComercialCategoriaAsesorDTO> ObtenerTableroComercialCategoriaAsesor()
        {
            try
            {
                List<TableroComercialCategoriaAsesorDTO> rpta = new List<TableroComercialCategoriaAsesorDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    MontoVenta,
	                    IdMoneda_Venta,
	                    IdTableroComercialUnidad_Venta,
	                    MontoPremio,
	                    IdMoneda_Premio,
	                    VisualizarMonedaLocal,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    FROM com.T_TableroComercialCategoriaAsesor
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TableroComercialCategoriaAsesorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TableroComercialCategoriaAsesor para mostrarse en combo.
        /// </summary>
        /// <returns> List<TableroComercialCategoriaAsesorComboDTO> </returns>
        public IEnumerable<TableroComercialCategoriaAsesorComboDTO> ObtenerCombo()
        {
            try
            {
                List<TableroComercialCategoriaAsesorComboDTO> rpta = new List<TableroComercialCategoriaAsesorComboDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre
                    FROM com.T_TableroComercialCategoriaAsesor
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TableroComercialCategoriaAsesorComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion de TableroComercialCategoriaAsesor.
        /// </summary>
        /// <returns> List<TableroComercialCategoriaAsesorDatosTableroDTO> </returns>
        public IEnumerable<TableroComercialCategoriaAsesorDatosTableroDTO> ObtenerDatosTablero()
        {
            try
            {
                List<TableroComercialCategoriaAsesorDatosTableroDTO> datosTablero = new List<TableroComercialCategoriaAsesorDatosTableroDTO>();
                var query = @"
                    SELECT Id,Nombre,MontoVenta,IdMonedaVenta,CodigoMonedaVenta,IdVisualizacionMonedaVenta,VisualizacionMonedaVenta,
	                    MontoPremio,IdMonedaPremio,CodigoMonedaPremio,VisualizarMonedaLocal
                    FROM com.V_TTableroComercialCategoriaAsesor_DatosTablero";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    datosTablero = JsonConvert.DeserializeObject<List<TableroComercialCategoriaAsesorDatosTableroDTO>>(resultadoQuery);
                }
                return datosTablero;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
