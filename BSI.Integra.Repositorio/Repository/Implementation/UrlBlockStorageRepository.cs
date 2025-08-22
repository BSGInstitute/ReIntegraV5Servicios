using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoUrlBlockStorageDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: UrlBlockStorageRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 01/09/2022
    /// <summary>
    /// Gestión general de T_UrlBlockStorage
    /// </summary>
    public class UrlBlockStorageRepository : GenericRepository<TUrlBlockStorage>, IUrlBlockStorageRepository
    {
        private Mapper _mapper;
        private object idPersonal;

        public UrlBlockStorageRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TUrlBlockStorage, UrlBlockStorage>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TUrlBlockStorage MapeoEntidad(UrlBlockStorage entidad)
        {
            try
            {
                //crea la entidad padre
                TUrlBlockStorage modelo = _mapper.Map<TUrlBlockStorage>(entidad);

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

        public TUrlBlockStorage Add(UrlBlockStorage entidad)
        {
            try
            {
                var UrlBlockStorage = MapeoEntidad(entidad);
                base.Insert(UrlBlockStorage);
                return UrlBlockStorage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TUrlBlockStorage Update(UrlBlockStorage entidad)
        {
            try
            {
                var UrlBlockStorage = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                UrlBlockStorage.RowVersion = entidadExistente.RowVersion;

                base.Update(UrlBlockStorage);
                return UrlBlockStorage;
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


        public IEnumerable<TUrlBlockStorage> Add(IEnumerable<UrlBlockStorage> listadoEntidad)
        {
            try
            {
                List<TUrlBlockStorage> listado = new List<TUrlBlockStorage>();
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

        public IEnumerable<TUrlBlockStorage> Update(IEnumerable<UrlBlockStorage> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TUrlBlockStorage> listado = new List<TUrlBlockStorage>();
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


        public IEnumerable<ContenedorArchivoCompletoDTO> ObtenerInformacionPorIdUrlSubcontenedor(int IdUrlSubContenedor)
        {
            try
            {
                List<ContenedorArchivoCompletoDTO> rpta = new List<ContenedorArchivoCompletoDTO>();
                var queryDapper = $@"SELECT TOP (1) IdContenedor,
                                        Contenedor,
                                        IdProveedorNube,
                                        Subdominio,
                                        AplicaSubcontenedores,
                                        AplicaSubidaMultiple,
                                        AplicaValidacion,
                                        IdSubcontenedor,
                                        Subcontenedor
                                    FROM [mkt].[V_RegistroArchivosContenedoresSubcontenedores]
                                    WHERE IdSubcontenedor = {IdUrlSubContenedor}";

                var resultado = _dapperRepository.QueryDapper(queryDapper, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ContenedorArchivoCompletoDTO>>(resultado);

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






