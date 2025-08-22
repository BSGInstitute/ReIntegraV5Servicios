using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: RecordAreaComercialRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_RecordAreaComercial
    /// </summary>
    public class RecordAreaComercialRepository : GenericRepository<TRecordAreaComercial>, IRecordAreaComercialRepository
    {
        private Mapper _mapper;

        public RecordAreaComercialRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRecordAreaComercial, RecordAreaComercial>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TRecordAreaComercial MapeoEntidad(RecordAreaComercial entidad)
        {
            try
            {
                //crea la entidad padre
                TRecordAreaComercial modelo = _mapper.Map<TRecordAreaComercial>(entidad);

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

        public TRecordAreaComercial Add(RecordAreaComercial entidad)
        {
            try
            {
                var RecordAreaComercial = MapeoEntidad(entidad);
                base.Insert(RecordAreaComercial);
                return RecordAreaComercial;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRecordAreaComercial Update(RecordAreaComercial entidad)
        {
            try
            {
                var RecordAreaComercial = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RecordAreaComercial.RowVersion = entidadExistente.RowVersion;

                base.Update(RecordAreaComercial);
                return RecordAreaComercial;
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


        public IEnumerable<TRecordAreaComercial> Add(IEnumerable<RecordAreaComercial> listadoEntidad)
        {
            try
            {
                List<TRecordAreaComercial> listado = new List<TRecordAreaComercial>();
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

        public IEnumerable<TRecordAreaComercial> Update(IEnumerable<RecordAreaComercial> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRecordAreaComercial> listado = new List<TRecordAreaComercial>();
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
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_RecordAreaComercial.
        /// </summary>
        /// <returns> List<RecordAreaComercialDTO> </returns>
        public IEnumerable<RecordAreaComercialDTO> ObtenerRecordAreaComercial()
        {
            try
            {
                List<RecordAreaComercialDTO> rpta = new List<RecordAreaComercialDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Monto,
	                    IdMoneda_Record,
	                    IdTableroComercialUnidad,
	                    Bono,
	                    IdMoneda_Bono,
	                    VisualizarMonedaLocal,
	                    EsVigente,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    FROM com.T_RecordAreaComercial
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RecordAreaComercialDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RecordAreaComercial para mostrarse en combo.
        /// </summary>
        /// <returns> List<RecordAreaComercialComboDTO> </returns>
        public IEnumerable<RecordAreaComercialComboDTO> ObtenerCombo()
        {
            try
            {
                List<RecordAreaComercialComboDTO> rpta = new List<RecordAreaComercialComboDTO>();
                var query = @"SELECT Id,Nombre FROM com.T_RecordAreaComercial WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RecordAreaComercialComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de datos para (M)Record del area comercial
        /// </summary>
        /// <returns> List<RecordAreaComercialComboDTO> </returns>
        public IEnumerable<RecordAreaComercialCompuestoDTO> ObtenerRecordAreaComercialParaTabla()
        {
            try
            {
                List<RecordAreaComercialCompuestoDTO> recordComercial = new List<RecordAreaComercialCompuestoDTO>();
                var query = @"
                    SELECT Id,Nombre,Monto,IdMonedaRecord,CodigoMonedaRecord,IdTableroComercialUnidad,TableroComercialUnidad,Bono,IdMonedaBono,
						CodigoMonedaBono,VisualizarMonedaLocal,EsVigente,Vigente
					FROM com.V_TRecordAreaComercial_DatosTablero";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    recordComercial = JsonConvert.DeserializeObject<List<RecordAreaComercialCompuestoDTO>>(resultado);
                }
                return recordComercial;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_RecordAreaComercial por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> RecordAreaComercial </returns>
        public RecordAreaComercial ObtenerPorId(int id)
        {
            try
            {
                var rpta = new RecordAreaComercial();
                var query = @"SELECT  Id,
                                       Nombre,
                                       Monto,
                                       IdMoneda_Record,
                                       IdTableroComercialUnidad,
                                       Bono,
                                       IdMoneda_Bono,
                                       VisualizarMonedaLocal,
                                       EsVigente,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion 
                                FROM com.T_RecordAreaComercial
                                WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<RecordAreaComercial>(resultado);
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
