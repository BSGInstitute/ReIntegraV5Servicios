using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{

    /// Repositorio: RecomendacionRepository
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2025
    /// <summary>
    /// Gestión general de TRecomendacionTranscripcion
    /// </summary>
    public class RecomendacionRepository : GenericRepository<TRecomendacionTranscripcion>, IRecomendacionRepository
    {
        private Mapper _mapper;

        public RecomendacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRecomendacionTranscripcion, RecomendacionTranscripcion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TRecomendacionTranscripcion MapeoEntidad(RecomendacionTranscripcion entidad)
        {
            try
            {
                //crea la entidad padre
                TRecomendacionTranscripcion modelo = _mapper.Map<TRecomendacionTranscripcion>(entidad);

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

        public TRecomendacionTranscripcion Add(RecomendacionTranscripcion entidad)
        {
            try
            {
                var RecomendacionTranscripcion = MapeoEntidad(entidad);
                base.Insert(RecomendacionTranscripcion);
                return RecomendacionTranscripcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRecomendacionTranscripcion Update(RecomendacionTranscripcion entidad)
        {
            try
            {
                var RecomendacionTranscripcion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RecomendacionTranscripcion.RowVersion = entidadExistente.RowVersion;
                base.Update(RecomendacionTranscripcion);
                return RecomendacionTranscripcion;
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


        public IEnumerable<TRecomendacionTranscripcion> Add(IEnumerable<RecomendacionTranscripcion> listadoEntidad)
        {
            try
            {
                List<TRecomendacionTranscripcion> listado = new List<TRecomendacionTranscripcion>();
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

        public IEnumerable<TRecomendacionTranscripcion> Update(IEnumerable<RecomendacionTranscripcion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRecomendacionTranscripcion> listado = new List<TRecomendacionTranscripcion>();
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
