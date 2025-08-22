using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TableroComercialUnidadRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 02/08/2022
    /// <summary>
    /// Gestión general de T_TableroComercialUnidad
    /// </summary>
    public class TableroComercialUnidadRepository : GenericRepository<TTableroComercialUnidad>, ITableroComercialUnidadRepository
    {
        private Mapper _mapper;

        public TableroComercialUnidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTableroComercialUnidad, TableroComercialUnidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTableroComercialUnidad MapeoEntidad(TableroComercialUnidad entidad)
        {
            try
            {
                //crea la entidad padre
                TTableroComercialUnidad modelo = _mapper.Map<TTableroComercialUnidad>(entidad);

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

        public TTableroComercialUnidad Add(TableroComercialUnidad entidad)
        {
            try
            {
                var TableroComercialUnidad = MapeoEntidad(entidad);
                base.Insert(TableroComercialUnidad);
                return TableroComercialUnidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTableroComercialUnidad Update(TableroComercialUnidad entidad)
        {
            try
            {
                var TableroComercialUnidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TableroComercialUnidad.RowVersion = entidadExistente.RowVersion;

                base.Update(TableroComercialUnidad);
                return TableroComercialUnidad;
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


        public IEnumerable<TTableroComercialUnidad> Add(IEnumerable<TableroComercialUnidad> listadoEntidad)
        {
            try
            {
                List<TTableroComercialUnidad> listado = new List<TTableroComercialUnidad>();
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

        public IEnumerable<TTableroComercialUnidad> Update(IEnumerable<TableroComercialUnidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTableroComercialUnidad> listado = new List<TTableroComercialUnidad>();
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
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TableroComercialUnidad.
        /// </summary>
        /// <returns> List<TableroComercialUnidadDTO> </returns>
        public IEnumerable<TableroComercialUnidadDTO> ObtenerTableroComercialUnidad()
        {
            try
            {
                List<TableroComercialUnidadDTO> rpta = new List<TableroComercialUnidadDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Valor,
	                    Simbolo,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_TableroComercialUnidad
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TableroComercialUnidadDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TableroComercialUnidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<TableroComercialUnidadComboDTO> </returns>
        public IEnumerable<TableroComercialUnidadComboDTO> ObtenerCombo()
        {
            try
            {
                List<TableroComercialUnidadComboDTO> rpta = new List<TableroComercialUnidadComboDTO>();
                var query = @"SELECT Id,Nombre FROM com.T_TableroComercialUnidad WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TableroComercialUnidadComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_TableroComercialUnidad exceptuando campos de auditoria.
        /// </summary>
        /// <returns> List<TableroComercialUnidadSinAuditoriaDTO> </returns>
        public IEnumerable<TableroComercialUnidadSinAuditoriaDTO> ObtenerTableroComercialUnidadSinAuditoria()
        {
            try
            {
                List<TableroComercialUnidadSinAuditoriaDTO> rpta = new List<TableroComercialUnidadSinAuditoriaDTO>();
                var query = @"SELECT Id, Nombre, Valor, Simbolo FROM com.T_TableroComercialUnidad WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TableroComercialUnidadSinAuditoriaDTO>>(resultado);
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
