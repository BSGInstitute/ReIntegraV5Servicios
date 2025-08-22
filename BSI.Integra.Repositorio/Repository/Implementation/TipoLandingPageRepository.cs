using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoLandingPageRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoLandingPage
    /// </summary>
    public class TipoLandingPageRepository : GenericRepository<TTipoLandingPage>, ITipoLandingPageRepository
    {
        private Mapper _mapper;

        public TipoLandingPageRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoLandingPage, TipoLandingPage>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TTipoLandingPage MapeoEntidad(TipoLandingPage entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoLandingPage modelo = _mapper.Map<TTipoLandingPage>(entidad);

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

        public TTipoLandingPage Add(TipoLandingPage entidad)
        {
            try
            {
                var TipoLandingPage = MapeoEntidad(entidad);
                base.Insert(TipoLandingPage);
                return TipoLandingPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoLandingPage Update(TipoLandingPage entidad)
        {
            try
            {
                var TipoLandingPage = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoLandingPage.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoLandingPage);
                return TipoLandingPage;
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


        public IEnumerable<TTipoLandingPage> Add(IEnumerable<TipoLandingPage> listadoEntidad)
        {
            try
            {
                List<TTipoLandingPage> listado = new List<TTipoLandingPage>();
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

        public IEnumerable<TTipoLandingPage> Update(IEnumerable<TipoLandingPage> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoLandingPage> listado = new List<TTipoLandingPage>();
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
        /// Obtiene registros de T_TipoLandingPage para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoLandingPageComboDTO> </returns>
        public IEnumerable<ComboTipoLandingPage> ObtenerCombo()
        {
            try
            {
                List<ComboTipoLandingPage> rpta = new List<ComboTipoLandingPage>();
                var query = "SELECT Id, Nombre FROM mkt.T_TipoLandingPage WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboTipoLandingPage>>(resultado);
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
        /// Obtiene todos los registros de T_TipoLandingPage
        /// </summary>
        /// <returns> List<TipoLandingPage> </returns>
        public IEnumerable<TipoLandingPage> ObtenerTipoLandingPage()
        {
            try
            {
                List<TipoLandingPage> rpta = new List<TipoLandingPage>();
                var query = "SELECT Id, Nombre FROM mkt.T_TipoLandingPage WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoLandingPage>>(resultado);
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
