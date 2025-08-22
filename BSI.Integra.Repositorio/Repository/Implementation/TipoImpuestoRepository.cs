using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoImpuestoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_TipoImpuesto
    /// </summary>
    public class TipoImpuestoRepository : GenericRepository<TTipoImpuesto>, ITipoImpuestoRepository
    {
        private Mapper _mapper;

        public TipoImpuestoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoImpuesto, TipoImpuesto>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoImpuesto MapeoEntidad(TipoImpuesto entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoImpuesto modelo = _mapper.Map<TTipoImpuesto>(entidad);

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

        public TTipoImpuesto Add(TipoImpuesto entidad)
        {
            try
            {
                var TipoImpuesto = MapeoEntidad(entidad);
                base.Insert(TipoImpuesto);
                return TipoImpuesto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoImpuesto Update(TipoImpuesto entidad)
        {
            try
            {
                var TipoImpuesto = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoImpuesto.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoImpuesto);
                return TipoImpuesto;
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


        public IEnumerable<TTipoImpuesto> Add(IEnumerable<TipoImpuesto> listadoEntidad)
        {
            try
            {
                List<TTipoImpuesto> listado = new List<TTipoImpuesto>();
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

        public IEnumerable<TTipoImpuesto> Update(IEnumerable<TipoImpuesto> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoImpuesto> listado = new List<TTipoImpuesto>();
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoImpuesto.
        /// </summary>
        /// <returns> List<TipoImpuestoDTO> </returns>
        public IEnumerable<TipoImpuestoDTO> ObtenerTipoImpuesto()
        {
            try
            {
                List<TipoImpuestoDTO> rpta = new List<TipoImpuestoDTO>();
                var query = @"
                    select 
	                    TI.Id,
	                    TI.Nombre,
	                    TI.Descripcion,
	                    TI.Valor,
	                    TI.UsuarioCreacion,
	                    TI.UsuarioModificacion,
	                    TI.FechaCreacion,
	                    TI.FechaModificacion,
	                    P.NombrePais,
                        TI.IdPais,
	                    TI.Activo
                    from fin.T_TipoImpuesto AS TI
                    INNER JOIN conf.T_Pais AS p 
                    ON P.id=TI.IdPais
                    WHERE TI.Estado=1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoImpuestoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoImpuesto para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoImpuestoComboDTO> </returns>
        public IEnumerable<TipoImpuestoComboDTO> ObtenerCombo()
        {
            try
            {
                List<TipoImpuestoComboDTO> rpta = new List<TipoImpuestoComboDTO>();
                var query = @"SELECT Id,CONCAT(valor,'% - ',Nombre) AS Nombre,IdPais,Valor FROM fin.T_TipoImpuesto WHERE Estado = 1 AND Activo=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoImpuestoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor del IGV-EGRESO.
        /// </summary>
        /// <returns> List<TipoImpuestoValorIgvDTO> </returns>
        public List<TipoImpuestoValorIgvDTO> ObtenerValorIgv()
        {
            try
            {
                List<TipoImpuestoValorIgvDTO> IGV = new List<TipoImpuestoValorIgvDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Valor FROM fin.T_TipoImpuesto WHERE Estado = 1 AND Nombre='IGV-EGRESO'";
                var resultado = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    IGV = JsonConvert.DeserializeObject<List<TipoImpuestoValorIgvDTO>>(resultado);
                }
                return IGV;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor de Impuesto segun el Pais.
        /// </summary>
        /// <returns> List<TipoImpuestoValorIgvDTO> </returns>
        public List<TipoImpuestoValorIgvDTO> ObtenerValorIgvPorPais(int IdPais)
        {
            try
            {
                List<TipoImpuestoValorIgvDTO> IGV = new List<TipoImpuestoValorIgvDTO>();
                var _query = string.Empty;
                _query = "SELECT TOP 1 IdImpuesto as Id, ValorImpuesto as Valor FROM [fin].[V_ObtenerImpuestoAsociadoPais] WHERE IdPais = @Id";
                var respuesta = _dapperRepository.QueryDapper(_query, new { Id = IdPais });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    IGV = JsonConvert.DeserializeObject<List<TipoImpuestoValorIgvDTO>>(respuesta);
                }
                return IGV;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
