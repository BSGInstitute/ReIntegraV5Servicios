using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MaterialCriterioVerificacionDetalleRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 02/08/2023
    /// <summary>
    /// Gestión general de T_MaterialCriterioVerificacionDetalle
    /// </summary>
    public class MaterialCriterioVerificacionDetalleRepository : GenericRepository<TMaterialCriterioVerificacionDetalle>, IMaterialCriterioVerificacionDetalleRepository
    {
        private Mapper _mapper;

        public MaterialCriterioVerificacionDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialCriterioVerificacionDetalle, MaterialCriterioVerificacionDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMaterialCriterioVerificacionDetalle MapeoEntidad(MaterialCriterioVerificacionDetalle entidad)
        {
            try
            {
                TMaterialCriterioVerificacionDetalle modelo = _mapper.Map<TMaterialCriterioVerificacionDetalle>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialCriterioVerificacionDetalle Add(MaterialCriterioVerificacionDetalle entidad)
        {
            try
            {
                var MaterialCriterioVerificacionDetalle = MapeoEntidad(entidad);
                base.Insert(MaterialCriterioVerificacionDetalle);
                return MaterialCriterioVerificacionDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialCriterioVerificacionDetalle Update(MaterialCriterioVerificacionDetalle entidad)
        {
            try
            {
                var MaterialCriterioVerificacionDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialCriterioVerificacionDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialCriterioVerificacionDetalle);
                return MaterialCriterioVerificacionDetalle;
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


        public IEnumerable<TMaterialCriterioVerificacionDetalle> Add(IEnumerable<MaterialCriterioVerificacionDetalle> listadoEntidad)
        {
            try
            {
                List<TMaterialCriterioVerificacionDetalle> listado = new List<TMaterialCriterioVerificacionDetalle>();
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

        public IEnumerable<TMaterialCriterioVerificacionDetalle> Update(IEnumerable<MaterialCriterioVerificacionDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialCriterioVerificacionDetalle> listado = new List<TMaterialCriterioVerificacionDetalle>();
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
