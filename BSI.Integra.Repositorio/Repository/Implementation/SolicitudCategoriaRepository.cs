using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SolicitudCategoriaRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudCategoria
    /// </summary>
    public class SolicitudCategoriaRepository : GenericRepository<TSolicitudCategorium>, ISolicitudCategoriaRepository
    {
        private Mapper _mapper;

        public SolicitudCategoriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolicitudCategorium, SolicitudCategoria>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSolicitudCategorium MapeoEntidad(SolicitudCategoria entidad)
        {
            try
            {
                //crea la entidad padre
                TSolicitudCategorium modelo = _mapper.Map<TSolicitudCategorium>(entidad);

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

        public TSolicitudCategorium Add(SolicitudCategoria entidad)
        {
            try
            {
                var SolicitudCategoria = MapeoEntidad(entidad);
                base.Insert(SolicitudCategoria);
                return SolicitudCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudCategorium Update(SolicitudCategoria entidad)
        {
            try
            {
                var SolicitudCategoria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudCategoria.RowVersion = entidadExistente.RowVersion;

                base.Update(SolicitudCategoria);
                return SolicitudCategoria;
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


        public IEnumerable<TSolicitudCategorium> Add(IEnumerable<SolicitudCategoria> listadoEntidad)
        {
            try
            {
                List<TSolicitudCategorium> listado = new List<TSolicitudCategorium>();
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

        public IEnumerable<TSolicitudCategorium> Update(IEnumerable<SolicitudCategoria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolicitudCategorium> listado = new List<TSolicitudCategorium>();
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
        /// Autor: Gilmer Quispe
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudCategoria </returns>
        public SolicitudCategoria ObtenerPorId(int id)
        {
            try
            {
                var rpta = new SolicitudCategoria();
                var query = @"SELECT Id,
                                       Nombre,
                                       IdSolicitudTipoReporte,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM ope.T_SolicitudCategoria 
                                WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<SolicitudCategoria>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla con Estado = 1 
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboSolicitudDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboSolicitudDTO>();
                var query = @"SELECT Id,Nombre,IdSolicitudTipoReporte
                                FROM ope.T_SolicitudCategoria 
                                WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboSolicitudDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los tipos de reporte y sus categorias 
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<TipoReporteCategoriaDTO> ObtenerTipoReporteCategoria()
        {
            try
            {
                var categoriaDTOs = new List<TipoReporteCategoriaDTO>();
                var query = @"SELECT idCategoria,nombreCategoria ,idTipoReporte,nombreReporte FROM ope.V_ObtenerCategoriaTipoReporte ORDER BY idCategoria desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    categoriaDTOs = JsonConvert.DeserializeObject<List<TipoReporteCategoriaDTO>>(resultado);
                }
                return categoriaDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los tipos de reporte y sus categorias 
        /// </summary> 
        /// <returns> IEnumerable<TipoReporteCategoriaDTO> </returns>
        public IEnumerable<TipoReporteSubCategoriaDTO> ObtenerTipoReporteSubCategoria()
        {
            try
            {
                var categoriaDTOs = new List<TipoReporteSubCategoriaDTO>();
                var query = @"SELECT idCategoria
		                        ,nombreCategoria
		                        ,idTipoReporte
		                        ,nombreReporte
		                        ,idProblema
		                        ,nombreProblema
                                ,descripcionSolucion
		                        ,prioridad
		                        ,idAreaRevision
		                        ,areaRevisión
		                        ,idPersonalRevision
		                        ,personalRevision
		                        ,idAreaSolucion
		                        ,areaSolucion
		                        ,idPersonalSolucion
		                        ,personalSolución 
                            FROM [ope].[V_ObtenerSolicitudProblema] ORDER BY idProblema desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    categoriaDTOs = JsonConvert.DeserializeObject<List<TipoReporteSubCategoriaDTO>>(resultado);
                }
                return categoriaDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de la tabla asociados al IdTipoReporte
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboSolicitudDTO> ObtenerComboPorTipoReporte(int idTipoReporte)
        {
            try
            {
                var comboDTOs = new List<ComboSolicitudDTO>();
                var query = @"SELECT Id,Nombre 
                                FROM ope.T_SolicitudCategoria 
                                WHERE Estado =1 AND IdSolicitudTipoReporte = @IdSolicitudTipoReporte";
                var resultado = _dapperRepository.QueryDapper(query, new { IdSolicitudTipoReporte  = idTipoReporte});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboSolicitudDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}