using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ArticuloPGeneralRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_ArticuloPGeneral
    /// </summary>
    public class ArticuloPGeneralRepository : GenericRepository<TArticuloPgeneral>, IArticuloPGeneralRepository
    {
        private Mapper _mapper;

        public ArticuloPGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TArticuloPgeneral, ArticuloPGeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TArticuloPgeneral MapeoEntidad(ArticuloPGeneral entidad)
        {
            try
            {
                //crea la entidad padre
                TArticuloPgeneral modelo = _mapper.Map<TArticuloPgeneral>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TArticuloPgeneral Add(ArticuloPGeneral entidad)
        {
            try
            {
                var ArticuloPGeneral = MapeoEntidad(entidad);
                base.Insert(ArticuloPGeneral);
                return ArticuloPGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TArticuloPgeneral Update(ArticuloPGeneral entidad)
        {
            try
            {
                var ArticuloPGeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ArticuloPGeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(ArticuloPGeneral);
                return ArticuloPGeneral;
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


        public IEnumerable<TArticuloPgeneral> Add(IEnumerable<ArticuloPGeneral> listadoEntidad)
        {
            try
            {
                List<TArticuloPgeneral> listado = new List<TArticuloPgeneral>();
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

        public IEnumerable<TArticuloPgeneral> Update(IEnumerable<ArticuloPGeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TArticuloPgeneral> listado = new List<TArticuloPgeneral>();
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
        /// Obtiene los registros de los programas asociados por IdArticulo
        /// </summary>
        /// <returns> List<ArticuloPGeneral> </returns>
        public List<ArticuloPGeneral> ObtenerArticuloPGeneralAsociados(int IdArticulo)
        {
            List<ArticuloPGeneral> rpta = new List<ArticuloPGeneral>();
            string query = @"Select Id, IdArticulo, IdPGeneral, UsuarioCreacion,FechaCreacion From pla.T_ArticuloPGeneral Where Estado=1 and IdArticulo=@IdArticulo and Estado=1";
            string resultadoQuery = _dapperRepository.QueryDapper(query, new { IdArticulo = IdArticulo });
            if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
            {
                rpta = JsonConvert.DeserializeObject<List<ArticuloPGeneral>>(resultadoQuery);
                return rpta;
            }
            else
            {
                return null;
            }
        }
    }
}
