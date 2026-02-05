using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: RemarketingEmbudoEsquemaRepository
    /// Autor: Max Mantilla
    /// Fecha: 06/01/2026
    /// <summary>
    /// Gestión general de T_RemarketingEmbudoEsquema
    /// </summary>
    public class RemarketingEmbudoEsquemaRepository : GenericRepository<TRemarketingEmbudoEsquema>, IRemarketingEmbudoEsquemaRepository
    {
        private Mapper _mapper;

        public RemarketingEmbudoEsquemaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRemarketingEmbudoEsquema, RemarketingEmbudoEsquema>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TRemarketingEmbudoEsquema MapeoEntidad(RemarketingEmbudoEsquema entidad)
        {
            try
            {
                //crea la entidad padre
                TRemarketingEmbudoEsquema modelo = _mapper.Map<TRemarketingEmbudoEsquema>(entidad);

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

        public TRemarketingEmbudoEsquema Add(RemarketingEmbudoEsquema entidad)
        {
            try
            {
                var RemarketingEmbudoEsquema = MapeoEntidad(entidad);
                base.Insert(RemarketingEmbudoEsquema);
                return RemarketingEmbudoEsquema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemarketingEmbudoEsquema Update(RemarketingEmbudoEsquema entidad)
        {
            try
            {
                var RemarketingEmbudoEsquema = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RemarketingEmbudoEsquema.RowVersion = entidadExistente.RowVersion;

                base.Update(RemarketingEmbudoEsquema);
                return RemarketingEmbudoEsquema;
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


        public IEnumerable<TRemarketingEmbudoEsquema> Add(IEnumerable<RemarketingEmbudoEsquema> listadoEntidad)
        {
            try
            {
                List<TRemarketingEmbudoEsquema> listado = new List<TRemarketingEmbudoEsquema>();
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

        public IEnumerable<TRemarketingEmbudoEsquema> Update(IEnumerable<RemarketingEmbudoEsquema> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRemarketingEmbudoEsquema> listado = new List<TRemarketingEmbudoEsquema>();
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
