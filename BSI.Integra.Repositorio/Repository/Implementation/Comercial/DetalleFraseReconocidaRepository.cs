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

    /// Repositorio: DetalleFraseReconocidaRepository
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2025
    /// <summary>
    /// Gestión general de TDetalleFraseReconocidum
    /// </summary>
    public class DetalleFraseReconocidaRepository : GenericRepository<TDetalleFraseReconocidum>, IDetalleFraseReconocidaRepository
    {
        private Mapper _mapper;

        public DetalleFraseReconocidaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDetalleFraseReconocidum, DetalleFraseReconocida>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDetalleFraseReconocidum MapeoEntidad(DetalleFraseReconocida entidad)
        {
            try
            {
                //crea la entidad padre
                TDetalleFraseReconocidum modelo = _mapper.Map<TDetalleFraseReconocidum>(entidad);

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

        public TDetalleFraseReconocidum Add(DetalleFraseReconocida entidad)
        {
            try
            {
                var DetalleFraseReconocida = MapeoEntidad(entidad);
                base.Insert(DetalleFraseReconocida);
                return DetalleFraseReconocida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDetalleFraseReconocidum Update(DetalleFraseReconocida entidad)
        {
            try
            {
                var DetalleFraseReconocida = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DetalleFraseReconocida.RowVersion = entidadExistente.RowVersion;
                base.Update(DetalleFraseReconocida);
                return DetalleFraseReconocida;
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


        public IEnumerable<TDetalleFraseReconocidum> Add(IEnumerable<DetalleFraseReconocida> listadoEntidad)
        {
            try
            {
                List<TDetalleFraseReconocidum> listado = new List<TDetalleFraseReconocidum>();
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

        public IEnumerable<TDetalleFraseReconocidum> Update(IEnumerable<DetalleFraseReconocida> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDetalleFraseReconocidum> listado = new List<TDetalleFraseReconocidum>();
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
