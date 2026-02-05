using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: RemarketingEmbudoNivelRepository
    /// Autor: Max Mantilla
    /// Fecha: 06/01/2026
    /// <summary>
    /// Gestión general de T_RemarketingEmbudoNivel
    /// </summary>
    public class RemarketingEmbudoNivelRepository : GenericRepository<TRemarketingEmbudoNivel>, IRemarketingEmbudoNivelRepository
    {
        private Mapper _mapper;

        public RemarketingEmbudoNivelRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRemarketingEmbudoNivel, RemarketingEmbudoNivel>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TRemarketingEmbudoNivel MapeoEntidad(RemarketingEmbudoNivel entidad)
        {
            try
            {
                //crea la entidad padre
                TRemarketingEmbudoNivel modelo = _mapper.Map<TRemarketingEmbudoNivel>(entidad);

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

        public TRemarketingEmbudoNivel Add(RemarketingEmbudoNivel entidad)
        {
            try
            {
                var RemarketingEmbudoNivel = MapeoEntidad(entidad);
                base.Insert(RemarketingEmbudoNivel);
                return RemarketingEmbudoNivel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemarketingEmbudoNivel Update(RemarketingEmbudoNivel entidad)
        {
            try
            {
                var RemarketingEmbudoNivel = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RemarketingEmbudoNivel.RowVersion = entidadExistente.RowVersion;

                base.Update(RemarketingEmbudoNivel);
                return RemarketingEmbudoNivel;
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


        public IEnumerable<TRemarketingEmbudoNivel> Add(IEnumerable<RemarketingEmbudoNivel> listadoEntidad)
        {
            try
            {
                List<TRemarketingEmbudoNivel> listado = new List<TRemarketingEmbudoNivel>();
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

        public IEnumerable<TRemarketingEmbudoNivel> Update(IEnumerable<RemarketingEmbudoNivel> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRemarketingEmbudoNivel> listado = new List<TRemarketingEmbudoNivel>();
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
