using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SemaforoFinancieroDetalleVariableRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_SemaforoFinancieroDetalleVariable
    /// </summary>
    public class SemaforoFinancieroDetalleVariableRepository : GenericRepository<TSemaforoFinancieroDetalleVariable>, ISemaforoFinancieroDetalleVariableRepository
    {
        private Mapper _mapper;

        public SemaforoFinancieroDetalleVariableRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSemaforoFinancieroDetalleVariable, SemaforoFinancieroDetalleVariable>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSemaforoFinancieroDetalleVariable MapeoEntidad(SemaforoFinancieroDetalleVariable entidad)
        {
            try
            {
                //crea la entidad padre
                TSemaforoFinancieroDetalleVariable modelo = _mapper.Map<TSemaforoFinancieroDetalleVariable>(entidad);

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

        public TSemaforoFinancieroDetalleVariable Add(SemaforoFinancieroDetalleVariable entidad)
        {
            try
            {
                var SemaforoFinancieroDetalleVariable = MapeoEntidad(entidad);
                base.Insert(SemaforoFinancieroDetalleVariable);
                return SemaforoFinancieroDetalleVariable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSemaforoFinancieroDetalleVariable Update(SemaforoFinancieroDetalleVariable entidad)
        {
            try
            {
                var SemaforoFinancieroDetalleVariable = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SemaforoFinancieroDetalleVariable.RowVersion = entidadExistente.RowVersion;

                base.Update(SemaforoFinancieroDetalleVariable);
                return SemaforoFinancieroDetalleVariable;
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


        public IEnumerable<TSemaforoFinancieroDetalleVariable> Add(IEnumerable<SemaforoFinancieroDetalleVariable> listadoEntidad)
        {
            try
            {
                List<TSemaforoFinancieroDetalleVariable> listado = new List<TSemaforoFinancieroDetalleVariable>();
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

        public IEnumerable<TSemaforoFinancieroDetalleVariable> Update(IEnumerable<SemaforoFinancieroDetalleVariable> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSemaforoFinancieroDetalleVariable> listado = new List<TSemaforoFinancieroDetalleVariable>();
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
        /// Obtiene todos los registros de T_SemaforoFinancieroDetalleVariable.
        /// </summary>
        /// <returns> List<SemaforoFinancieroDetalleVariableDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleVariableDTO> ObtenerSemaforoFinancieroDetalleVariable()
        {
            try
            {
                List<SemaforoFinancieroDetalleVariableDTO> rpta = new List<SemaforoFinancieroDetalleVariableDTO>();
                var query = $@"
                    SELECT
	                    Id,
	                    IdSemaforoFinancieroDetalle,
	                    IdSemaforoFinancieroVariable,
	                    ValorMinimo,
	                    IdMoneda,
	                    ValorMaximo,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    FROM com.T_SemaforoFinancieroDetalleVariable
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDetalleVariableDTO>>(resultado);
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
        /// Obtiene registros de T_SemaforoFinancieroDetalleVariable para mostrarse en combo.
        /// </summary>
        /// <returns> List<SemaforoFinancieroDetalleVariableComboDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleVariableComboDTO> ObtenerCombo()
        {
            try
            {
                List<SemaforoFinancieroDetalleVariableComboDTO> rpta = new List<SemaforoFinancieroDetalleVariableComboDTO>();
                var query = $@"
                    SELECT
	                    SFDV.Id,
	                    SFD.Nombre AS NombreDetalle,
	                    SFV.Nombre AS NombreVariable
                    FROM com.T_SemaforoFinancieroDetalleVariable AS SFDV
                    INNER JOIN com.T_SemaforoFinancieroDetalle AS SFD
	                    ON SFDV.IdSemaforoFinancieroDetalle = SFD.Id
	                    AND SFD.Estado = 1
                    INNER JOIN com.T_SemaforoFinancieroVariable AS SFV
	                    ON SFDV.IdSemaforoFinancieroVariable = SFV.Id
	                    AND SFV.Estado = 1
                    WHERE SFDV.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDetalleVariableComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros detallados de T_SemaforoFinancieroDetalleVariable asociados a un IdSemaforoFinancieroDetalle.
        /// </summary>
        /// <param name="idSemaforoFinancieroDetalle">Id de Semaforo Financiero Detalle</param>
        /// <returns> List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> ObtenerSemaforoFinancieroDetalleVariablePorIdSemaforoFinancieroDetalle(int idSemaforoFinancieroDetalle)
        {
            try
            {
                List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> rpta = new List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO>();
                var query = "com.SP_ObtenerSemaforoFinancieroDetalleVariable";
                var resultado = _dapperRepository.QuerySPDapper(query, new { semaforoDetalle = idSemaforoFinancieroDetalle });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de la tabla T_SemaforoFinancieroDetalleVariable por el id
        /// </summary>
        ///<param name="idSemaforoDetalleVariable">id del T_SemaforoFinancieroVariable/param>
        /// <returns> SemaforoFinancieroDetalleVariable </returns>
        public SemaforoFinancieroDetalleVariable ObtenerSemaforoDetalleVariablePorId(int idSemaforoDetalleVariable)
        {
            try
            {
                SemaforoFinancieroDetalleVariable rpta = new SemaforoFinancieroDetalleVariable();
                var query = @"SELECT Id,IdSemaforoFinancieroDetalle,IdSemaforoFinancieroVariable,ValorMinimo,
                            ValorMaximo,IdMoneda,Estado, UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion, IdMigracion 
                            FROM com.T_SemaforoFinancieroDetalleVariable 
                            WHERE Id = " + idSemaforoDetalleVariable + " AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<SemaforoFinancieroDetalleVariable>(resultado);

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
