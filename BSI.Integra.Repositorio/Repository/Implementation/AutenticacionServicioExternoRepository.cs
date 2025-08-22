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
    /// Repositorio: AutenticacionServicioExternoRepository
    /// Autor: Margiory Ramirez.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AutenticacionServicioExterno
    /// </summary>
    public class AutenticacionServicioExternoRepository : GenericRepository<TAutenticacionServicioExterno>, IAutenticacionServicioExternoRepository
    {
        private Mapper _mapper;

        public AutenticacionServicioExternoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAutenticacionServicioExterno, AutenticacionServicioExterno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAutenticacionServicioExterno MapeoEntidad(AutenticacionServicioExterno entidad)
        {
            try
            {
                //crea la entidad padre
                TAutenticacionServicioExterno modelo = _mapper.Map<TAutenticacionServicioExterno>(entidad);

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

        public TAutenticacionServicioExterno Add(AutenticacionServicioExterno entidad)
        {
            try
            {
                var AutenticacionServicioExterno = MapeoEntidad(entidad);
                base.Insert(AutenticacionServicioExterno);
                return AutenticacionServicioExterno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAutenticacionServicioExterno Update(AutenticacionServicioExterno entidad)
        {
            try
            {
                var AutenticacionServicioExterno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AutenticacionServicioExterno.RowVersion = entidadExistente.RowVersion;

                base.Update(AutenticacionServicioExterno);
                return AutenticacionServicioExterno;
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


        public IEnumerable<TAutenticacionServicioExterno> Add(IEnumerable<AutenticacionServicioExterno> listadoEntidad)
        {
            try
            {
                List<TAutenticacionServicioExterno> listado = new List<TAutenticacionServicioExterno>();
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

        public IEnumerable<TAutenticacionServicioExterno> Update(IEnumerable<AutenticacionServicioExterno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAutenticacionServicioExterno> listado = new List<TAutenticacionServicioExterno>();
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
        /// Autor: Margiory Ramirez.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AutenticacionServicioExterno para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public TokenFacebookDTO ObtenerTokenpoId(int id )
        {
            try
            {
                var rpta = new TokenFacebookDTO();
                var query = @" SELECT Valor FROM conf.T_AutenticacionServicioExterno WHERE id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new {id= id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TokenFacebookDTO>(resultado);
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
