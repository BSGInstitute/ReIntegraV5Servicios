using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FacebookAudienciaAlumnoRepository
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FacebookAudienciaAlumnoRepository : GenericRepository<TFacebookAudienciaAlumno>, IFacebookAudienciaAlumnoRepository
    {
        private Mapper _mapper;

        public FacebookAudienciaAlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFacebookAudienciaAlumno, FacebookAudienciaAlumno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFacebookAudienciaAlumno MapeoEntidad(FacebookAudienciaAlumno entidad)
        {
            try
            {
                //crea la entidad padre
                TFacebookAudienciaAlumno modelo = _mapper.Map<TFacebookAudienciaAlumno>(entidad);

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

        public TFacebookAudienciaAlumno Add(FacebookAudienciaAlumno entidad)
        {
            try
            {
                var FacebookAudienciaAlumno = MapeoEntidad(entidad);
                base.Insert(FacebookAudienciaAlumno);
                return FacebookAudienciaAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFacebookAudienciaAlumno Update(FacebookAudienciaAlumno entidad)
        {
            try
            {
                var FacebookAudienciaAlumno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FacebookAudienciaAlumno.RowVersion = entidadExistente.RowVersion;

                base.Update(FacebookAudienciaAlumno);
                return FacebookAudienciaAlumno;
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


        public IEnumerable<TFacebookAudienciaAlumno> Add(IEnumerable<FacebookAudienciaAlumno> listadoEntidad)
        {
            try
            {
                List<TFacebookAudienciaAlumno> listado = new List<TFacebookAudienciaAlumno>();
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

        public IEnumerable<TFacebookAudienciaAlumno> Update(IEnumerable<FacebookAudienciaAlumno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFacebookAudienciaAlumno> listado = new List<TFacebookAudienciaAlumno>();
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


    }
}




