using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AdwordsApiPalabraClaveRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AdwordsApiPalabraClave
    /// </summary>
    public class AdwordsApiPalabraClaveRepository : GenericRepository<TAdwordsApiPalabraClave>, IAdwordsApiPalabraClaveRepository
    {
        private Mapper _mapper;

        public AdwordsApiPalabraClaveRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAdwordsApiPalabraClave, AdwordsApiPalabraClave>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAdwordsApiPalabraClave MapeoEntidad(AdwordsApiPalabraClave entidad)
        {
            try
            {
                //crea la entidad padre
                TAdwordsApiPalabraClave modelo = _mapper.Map<TAdwordsApiPalabraClave>(entidad);

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

        public TAdwordsApiPalabraClave Add(AdwordsApiPalabraClave entidad)
        {
            try
            {
                var AdwordsApiPalabraClave = MapeoEntidad(entidad);
                base.Insert(AdwordsApiPalabraClave);
                return AdwordsApiPalabraClave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAdwordsApiPalabraClave Update(AdwordsApiPalabraClave entidad)
        {
            try
            {
                var AdwordsApiPalabraClave = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AdwordsApiPalabraClave.RowVersion = entidadExistente.RowVersion;

                base.Update(AdwordsApiPalabraClave);
                return AdwordsApiPalabraClave;
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


        public IEnumerable<TAdwordsApiPalabraClave> Add(IEnumerable<AdwordsApiPalabraClave> listadoEntidad)
        {
            try
            {
                List<TAdwordsApiPalabraClave> listado = new List<TAdwordsApiPalabraClave>();
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

        public IEnumerable<TAdwordsApiPalabraClave> Update(IEnumerable<AdwordsApiPalabraClave> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAdwordsApiPalabraClave> listado = new List<TAdwordsApiPalabraClave>();
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

        public int InsertarPalabraClaveYretornarId(AdwordsApiPalabraClave entidad)
        {
            try
            {
                int numero = 0;
                string sql = @"Insert into mkt.T_AdwordsApiPalabraClave (PalabraClave,Estado,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion) 
                            VALUES (@PalabraClave,
                @Estado,
                @FechaCreacion,
                @FechaModificacion,
                @UsuarioModificacion,
                @UsuarioCreacion) SELECT SCOPE_IDENTITY() AS Id";
                var result = _dapperRepository.FirstOrDefault(sql, new
                {
                    entidad.PalabraClave,
                    entidad.Estado,
                    entidad.FechaCreacion,
                    entidad.FechaModificacion,
                    entidad.UsuarioModificacion,
                    entidad.UsuarioCreacion,
                });
                if(result != null)
                {
                    var res= JsonConvert.DeserializeObject<Dictionary<string, double>>(result);
                    numero = Convert.ToInt32(res["Id"]);
                }
                return numero;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
