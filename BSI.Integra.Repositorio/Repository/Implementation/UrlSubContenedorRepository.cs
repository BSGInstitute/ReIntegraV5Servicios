using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: UrlSubContenedorRepository
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 11/08//2022
    /// <summary>
    /// Gestión general de T_UrlSubContenedor
    /// </summary>
    public class UrlSubContenedorRepository : GenericRepository<TUrlSubContenedor>, IUrlSubContenedorRepository
    {
        private Mapper _mapper;

        public UrlSubContenedorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TUrlSubContenedor, UrlSubContenedor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TUrlSubContenedor MapeoEntidad(UrlSubContenedor entidad)
        {
            try
            {
                //crea la entidad padre
                TUrlSubContenedor modelo = _mapper.Map<TUrlSubContenedor>(entidad);

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

        public TUrlSubContenedor Add(UrlSubContenedor entidad)
        {
            try
            {
                var UrlSubContenedor = MapeoEntidad(entidad);
                base.Insert(UrlSubContenedor);
                return UrlSubContenedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TUrlSubContenedor Update(UrlSubContenedor entidad)
        {
            try
            {
                var UrlSubContenedor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                UrlSubContenedor.RowVersion = entidadExistente.RowVersion;

                base.Update(UrlSubContenedor);
                return UrlSubContenedor;
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


        public IEnumerable<TUrlSubContenedor> Add(IEnumerable<UrlSubContenedor> listadoEntidad)
        {
            try
            {
                List<TUrlSubContenedor> listado = new List<TUrlSubContenedor>();
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

        public IEnumerable<TUrlSubContenedor> Update(IEnumerable<UrlSubContenedor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TUrlSubContenedor> listado = new List<TUrlSubContenedor>();
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


        public IEnumerable<UrlSubContenedorDTO> ObtenerRutaSubContenedor(int IdUrlSubContenedor)
        {
            try
            {
                List<UrlSubContenedorDTO> rpta = new List<UrlSubContenedorDTO>();
                var queryDapper = $@"SELECT TOP 1 *
                                FROM [mkt].[V_T_UrlBlockStorage_ObtenerUrl]
                                WHERE V_T_UrlBlockStorage_ObtenerUrl.Id= {IdUrlSubContenedor}";

                var resultado = _dapperRepository.QueryDapper(queryDapper, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<UrlSubContenedorDTO>>(resultado);

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





#endregion







