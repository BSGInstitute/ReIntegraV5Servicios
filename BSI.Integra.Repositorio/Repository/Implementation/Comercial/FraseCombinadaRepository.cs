using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{
    /// Repositorio: FraseCombinadaRepository
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2025
    /// <summary>
    /// Gestión general de TFraseCombinadum
    /// </summary>
    public class FraseCombinadaRepository : GenericRepository<TFraseCombinadum>, IFraseCombinadaRepository
    {
        private Mapper _mapper;

        public FraseCombinadaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFraseCombinadum, FraseCombinada>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFraseCombinadum MapeoEntidad(FraseCombinada entidad)
        {
            try
            {
                //crea la entidad padre
                TFraseCombinadum modelo = _mapper.Map<TFraseCombinadum>(entidad);

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

        public TFraseCombinadum Add(FraseCombinada entidad)
        {
            try
            {
                var FraseCombinada = MapeoEntidad(entidad);
                base.Insert(FraseCombinada);
                return FraseCombinada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFraseCombinadum Update(FraseCombinada entidad)
        {
            try
            {
                var FraseCombinada = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FraseCombinada.RowVersion = entidadExistente.RowVersion;
                base.Update(FraseCombinada);
                return FraseCombinada;
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


        public IEnumerable<TFraseCombinadum> Add(IEnumerable<FraseCombinada> listadoEntidad)
        {
            try
            {
                List<TFraseCombinadum> listado = new List<TFraseCombinadum>();
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

        public IEnumerable<TFraseCombinadum> Update(IEnumerable<FraseCombinada> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFraseCombinadum> listado = new List<TFraseCombinadum>();
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
