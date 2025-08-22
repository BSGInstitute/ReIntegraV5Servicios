using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DiferenciaHorariaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_DiferenciaHoraria
    /// </summary>
    public class DiferenciaHorariaRepository : GenericRepository<TDiferenciaHorarium>, IDiferenciaHorariaRepository
    {
        private Mapper _mapper;

        public DiferenciaHorariaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDiferenciaHorarium, DiferenciaHorarium>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDiferenciaHorarium MapeoEntidad(DiferenciaHorarium entidad)
        {
            try
            {
                return _mapper.Map<TDiferenciaHorarium>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDiferenciaHorarium Add(DiferenciaHorarium entidad)
        {
            try
            {
                var DiferenciaHoraria = MapeoEntidad(entidad);
                base.Insert(DiferenciaHoraria);
                return DiferenciaHoraria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDiferenciaHorarium Update(DiferenciaHorarium entidad)
        {
            try
            {
                var DiferenciaHoraria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DiferenciaHoraria.RowVersion = entidadExistente.RowVersion;

                base.Update(DiferenciaHoraria);
                return DiferenciaHoraria;
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


        public IEnumerable<TDiferenciaHorarium> Add(IEnumerable<DiferenciaHorarium> listadoEntidad)
        {
            try
            {
                List<TDiferenciaHorarium> listado = new List<TDiferenciaHorarium>();
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

        public IEnumerable<TDiferenciaHorarium> Update(IEnumerable<DiferenciaHorarium> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDiferenciaHorarium> listado = new List<TDiferenciaHorarium>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DiferenciaHoraria para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DiferenciaHorarium> ObtenerPorIdPaisOrigen(int idPaisOrigen)
        {
            try
            {
                var query = @"
                    SELECT Id	,
		                IdPais_Origen AS IdPaisOrigen,
		                IdPais_Destino AS IdPaisDestino,
		                DiferenciaHoraria AS DiferenciaHoraria,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
                    FROM conf.T_DiferenciaHoraria WHERE Estado=1 AND Id=@idPaisOrigen";
                var resultado = _dapperRepository.QueryDapper(query, new { idPaisOrigen });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<DiferenciaHorarium>>(resultado)!;
                }
                return new List<DiferenciaHorarium>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdPaisOrigen(): {ex.Message}", ex);
            }
        }
    }
}
