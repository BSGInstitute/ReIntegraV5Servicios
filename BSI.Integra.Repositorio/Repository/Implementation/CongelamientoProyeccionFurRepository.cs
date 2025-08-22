using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CongelamientoProyeccionFurRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CongelamientoProyeccionFur
    /// </summary>
    public class CongelamientoProyeccionFurRepository : GenericRepository<TCongelamientoProyeccionFur>, ICongelamientoProyeccionFurRepository
    {
        private Mapper _mapper;

        public CongelamientoProyeccionFurRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCongelamientoProyeccionFur, CongelamientoProyeccionFur>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCongelamientoProyeccionFur MapeoEntidad(CongelamientoProyeccionFur entidad)
        {
            try
            {
                //crea la entidad padre
                TCongelamientoProyeccionFur modelo = _mapper.Map<TCongelamientoProyeccionFur>(entidad);

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

        public TCongelamientoProyeccionFur Add(CongelamientoProyeccionFur entidad)
        {
            try
            {
                var CongelamientoProyeccionFur = MapeoEntidad(entidad);
                base.Insert(CongelamientoProyeccionFur);
                return CongelamientoProyeccionFur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCongelamientoProyeccionFur Update(CongelamientoProyeccionFur entidad)
        {
            try
            {
                var CongelamientoProyeccionFur = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CongelamientoProyeccionFur.RowVersion = entidadExistente.RowVersion;

                base.Update(CongelamientoProyeccionFur);
                return CongelamientoProyeccionFur;
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


        public IEnumerable<TCongelamientoProyeccionFur> Add(IEnumerable<CongelamientoProyeccionFur> listadoEntidad)
        {
            try
            {
                List<TCongelamientoProyeccionFur> listado = new List<TCongelamientoProyeccionFur>();
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

        public IEnumerable<TCongelamientoProyeccionFur> Update(IEnumerable<CongelamientoProyeccionFur> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCongelamientoProyeccionFur> listado = new List<TCongelamientoProyeccionFur>();
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
        /// Obtiene todos los registros de T_CongelamientoProyeccionFur.
        /// </summary>
        /// <returns> List<CongelamientoProyeccionFurDTO> </returns>
        public CongelamientoProyeccionFurStringDTO ObtenerCongelamientoProyeccionFur(int idCabecera)
        {
            try
            {
                CongelamientoProyeccionFurStringDTO rpta = new CongelamientoProyeccionFurStringDTO();
                var query = @" 
                    SELECT 
	                    IdCabeceraFurConfiguracionAutomatica AS IdCabeceraSolicitud,
	                    IdArea,
	                    Configuracion AS ConfiguracionProyeccionFur,
	                    DetalleFurConfiguracionAutomatica AS  DetalleCabeceraProyeccionFur
                    FROM [fin].[T_CongelamientoProyeccionFur] where IdCabeceraFurConfiguracionAutomatica=@IdCabecera";
                var resultadoFur = _dapperRepository.FirstOrDefault(query, new { IdCabecera = idCabecera });
                if (!string.IsNullOrEmpty(resultadoFur) && resultadoFur!= "null")
                {
                    rpta = JsonConvert.DeserializeObject<CongelamientoProyeccionFurStringDTO>(resultadoFur);
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
