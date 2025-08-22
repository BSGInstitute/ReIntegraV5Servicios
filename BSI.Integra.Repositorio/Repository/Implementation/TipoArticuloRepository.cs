using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoArticuloRepository
    /// Autor: Max Mantilla.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_TipoArticulo
    /// </summary>
    public class TipoArticuloRepository : GenericRepository<TTipoArticulo>, ITipoArticuloRepository
    {
        private Mapper _mapper;

        public TipoArticuloRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TArticulo, TipoArticulo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTipoArticulo MapeoEntidad(TipoArticulo entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoArticulo modelo = _mapper.Map<TTipoArticulo>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoArticulo Add(TipoArticulo entidad)
        {
            try
            {
                var TipoArticulo = MapeoEntidad(entidad);
                base.Insert(TipoArticulo);
                return TipoArticulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoArticulo Update(TipoArticulo entidad)
        {
            try
            {
                var TipoArticulo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoArticulo.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoArticulo);
                return TipoArticulo;
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


        public IEnumerable<TTipoArticulo> Add(IEnumerable<TipoArticulo> listadoEntidad)
        {
            try
            {
                List<TTipoArticulo> listado = new List<TTipoArticulo>();
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

        public IEnumerable<TTipoArticulo> Update(IEnumerable<TipoArticulo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoArticulo> listado = new List<TTipoArticulo>();
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

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoArticulo
        /// </summary>
        /// <returns> IEnumerable<TipoArticuloDTO> </returns>
        public IEnumerable<TipoArticuloDTO> ObtenerFiltroTipoArticulo()
        {
            try
            {
                List<TipoArticuloDTO> rpta = new List<TipoArticuloDTO>();
                var query = @"Select Id,Nombre FROM pla.T_TipoArticulo";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoArticuloDTO>>(resultado);
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
