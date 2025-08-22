using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FuentesPortalWebRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_FuentesPortalWeb
    /// </summary>
    public class FuentesPortalWebRepository : GenericRepository<TFuentesPortalWeb>, IFuentesPortalWebRepository
    {
        private Mapper _mapper;

        public FuentesPortalWebRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFuentesPortalWeb, FuentesPortalWeb>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TFuentesPortalWeb MapeoEntidad(FuentesPortalWeb entidad)
        {
            try
            {
                //crea la entidad padre
                TFuentesPortalWeb modelo = _mapper.Map<TFuentesPortalWeb>(entidad);

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

        public TFuentesPortalWeb Add(FuentesPortalWeb entidad)
        {
            try
            {
                var FuentesPortalWeb = MapeoEntidad(entidad);
                base.Insert(FuentesPortalWeb);
                return FuentesPortalWeb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFuentesPortalWeb Update(FuentesPortalWeb entidad)
        {
            try
            {
                var FuentesPortalWeb = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FuentesPortalWeb.RowVersion = entidadExistente.RowVersion;

                base.Update(FuentesPortalWeb);
                return FuentesPortalWeb;
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


        public IEnumerable<TFuentesPortalWeb> Add(IEnumerable<FuentesPortalWeb> listadoEntidad)
        {
            try
            {
                List<TFuentesPortalWeb> listado = new List<TFuentesPortalWeb>();
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

        public IEnumerable<TFuentesPortalWeb> Update(IEnumerable<FuentesPortalWeb> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFuentesPortalWeb> listado = new List<TFuentesPortalWeb>();
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
        /// Obtiene registros de T_FuentesPortalWeb para mostrarse en combo.
        /// </summary>
        /// <returns> List<FuentesPortalWebComboDTO> </returns>
        public IEnumerable<comboFuentes> ObtenerCombo()
        {
            try
            {
                List<comboFuentes> rpta = new List<comboFuentes>();

                var query = "SELECT Id, NombreArchivo FROM mkt.T_FuentesPortalWeb WHERE Estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<comboFuentes>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FuentesPortalWeb
        /// </summary>
        /// <returns> List<FuentesPortalWeb> </returns>
        public IEnumerable<FuentesPortalWeb> ObtenerFuentesPortalWeb()
        {
            try
            {
                List<FuentesPortalWeb> rpta = new List<FuentesPortalWeb>();
                var query = @"SELECT Id, NombreArchivo, Url FROM mkt.T_FuentesPortalWeb WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FuentesPortalWeb>>(resultado);
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
