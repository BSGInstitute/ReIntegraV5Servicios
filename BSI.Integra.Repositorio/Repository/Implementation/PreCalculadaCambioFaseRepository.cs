using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PreCalculadaCambioFaseRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_PreCalculadaCambioFase
    /// </summary>
    public class PreCalculadaCambioFaseRepository : GenericRepository<TPreCalculadaCambioFase>, IPreCalculadaCambioFaseRepository
    {
        private Mapper _mapper;

        public PreCalculadaCambioFaseRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreCalculadaCambioFase, PreCalculadaCambioFase>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPreCalculadaCambioFase MapeoEntidad(PreCalculadaCambioFase entidad)
        {
            try
            {
                //crea la entidad padre
                TPreCalculadaCambioFase modelo = _mapper.Map<TPreCalculadaCambioFase>(entidad);

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

        public TPreCalculadaCambioFase Add(PreCalculadaCambioFase entidad)
        {
            try
            {
                var PreCalculadaCambioFase = MapeoEntidad(entidad);
                base.Insert(PreCalculadaCambioFase);
                return PreCalculadaCambioFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPreCalculadaCambioFase AddAsync(PreCalculadaCambioFase entidad)
        {
            try
            {
                var PreCalculadaCambioFase = MapeoEntidad(entidad);
                base.InsertAsync(PreCalculadaCambioFase);
                return PreCalculadaCambioFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreCalculadaCambioFase Update(PreCalculadaCambioFase entidad)
        {
            try
            {
                var PreCalculadaCambioFase = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreCalculadaCambioFase.RowVersion = entidadExistente.RowVersion;

                base.Update(PreCalculadaCambioFase);
                return PreCalculadaCambioFase;
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


        public IEnumerable<TPreCalculadaCambioFase> Add(IEnumerable<PreCalculadaCambioFase> listadoEntidad)
        {
            try
            {
                List<TPreCalculadaCambioFase> listado = new List<TPreCalculadaCambioFase>();
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

        public IEnumerable<TPreCalculadaCambioFase> Update(IEnumerable<PreCalculadaCambioFase> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreCalculadaCambioFase> listado = new List<TPreCalculadaCambioFase>();
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
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreCalculadaCambioFase.
        /// </summary>
        /// <returns> List<PreCalculadaCambioFaseDTO> </returns>
        public IEnumerable<PreCalculadaCambioFaseDTO> ObtenerPreCalculadaCambioFase()
        {
            try
            {
                List<PreCalculadaCambioFaseDTO> rpta = new List<PreCalculadaCambioFaseDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPersonal,
	                    Fecha,
	                    IdCentroCosto,
	                    IdFaseOportunidad_Origen AS IdFaseOportunidadOrigen,
	                    IdFaseOportunidad_Destino AS IdFaseOportunidadDestino,
	                    IdTipoDato,
	                    IdOrigen,
	                    IdCategoriaOrigen,
	                    IdCampania,
	                    Contador,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_PreCalculadaCambioFase
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PreCalculadaCambioFaseDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el numero de Contador basado en el parametro recibido.
        /// </summary>
        /// <returns> int </returns>
        public int ExistePreCalculadaCambioFase(PreCalculadaCambioFase precalculadaCambioFase)
        {
            try
            {
                IntDTO rpta = new IntDTO() { Valor = 1 };
                var query = @"
                    SELECT
	                    Contador AS Valor
                    FROM com.T_PreCalculadaCambioFase
                    WHERE Estado = 1
	                    AND IdPersonal = @IdPersonal 
	                    AND Fecha = @Fecha
	                    AND IdCentroCosto = @IdCentroCosto
	                    AND IdFaseOportunidad_Origen = @IdFaseOportunidadOrigen
	                    AND IdFaseOportunidad_Destino = @IdFaseOportunidadDestino
	                    AND IdTipoDato = @IdTipoDato
	                    AND IdOrigen = @IdOrigen
	                    AND IdCategoriaOrigen = @IdCategoriaOrigen
	                    AND IdCampania= @IdCampania";
                var resultado = _dapperRepository.FirstOrDefault(query, new
                {
                    precalculadaCambioFase.IdPersonal,
                    precalculadaCambioFase.Fecha,
                    precalculadaCambioFase.IdCentroCosto,
                    precalculadaCambioFase.IdFaseOportunidadOrigen,
                    precalculadaCambioFase.IdFaseOportunidadDestino,
                    precalculadaCambioFase.IdTipoDato,
                    precalculadaCambioFase.IdOrigen,
                    precalculadaCambioFase.IdCategoriaOrigen,
                    precalculadaCambioFase.IdCampania
                });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado);
                    rpta.Valor++;
                }
                return rpta.Valor.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
