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
    /// Repositorio: ConjuntoAnuncioFacebookRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConjuntoAnuncioFacebook
    /// </summary>
    public class ConjuntoAnuncioFacebookRepository : GenericRepository<TConjuntoAnuncioFacebook>, IConjuntoAnuncioFacebookRepository
    {
        private Mapper _mapper;

        public ConjuntoAnuncioFacebookRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConjuntoAnuncioFacebook, ConjuntoAnuncioFacebook>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConjuntoAnuncioFacebook MapeoEntidad(ConjuntoAnuncioFacebook entidad)
        {
            try
            {
                //crea la entidad padre
                TConjuntoAnuncioFacebook modelo = _mapper.Map<TConjuntoAnuncioFacebook>(entidad);

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

        public TConjuntoAnuncioFacebook Add(ConjuntoAnuncioFacebook entidad)
        {
            try
            {
                var ConjuntoAnuncioFacebook = MapeoEntidad(entidad);
                base.Insert(ConjuntoAnuncioFacebook);
                return ConjuntoAnuncioFacebook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConjuntoAnuncioFacebook Update(ConjuntoAnuncioFacebook entidad)
        {
            try
            {
                var ConjuntoAnuncioFacebook = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConjuntoAnuncioFacebook.RowVersion = entidadExistente.RowVersion;

                base.Update(ConjuntoAnuncioFacebook);
                return ConjuntoAnuncioFacebook;
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


        public IEnumerable<TConjuntoAnuncioFacebook> Add(IEnumerable<ConjuntoAnuncioFacebook> listadoEntidad)
        {
            try
            {
                List<TConjuntoAnuncioFacebook> listado = new List<TConjuntoAnuncioFacebook>();
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

        public IEnumerable<TConjuntoAnuncioFacebook> Update(IEnumerable<ConjuntoAnuncioFacebook> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConjuntoAnuncioFacebook> listado = new List<TConjuntoAnuncioFacebook>();
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
    }
}

        #endregion

        