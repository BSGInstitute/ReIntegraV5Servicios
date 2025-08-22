using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DataCreditoDataInfMicroAnalisisVectorRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de TDataCreditoDataInfMicroAnalisisVector
    /// </summary>
    public class DataCreditoDataInfMicroAnalisisVectorRepository : GenericRepository<TDataCreditoDataInfMicroAnalisisVector>, IDataCreditoDataInfMicroAnalisisVectorRepository
    {
        private Mapper _mapper;
        public DataCreditoDataInfMicroAnalisisVectorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDataCreditoDataInfMicroAnalisisVector, DataCreditoDataInfMicroAnalisisVector>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDataCreditoDataInfMicroAnalisisVector MapeoEntidad(DataCreditoDataInfMicroAnalisisVector entidad)
        {
            try
            {
                //crea la entidad padre
                TDataCreditoDataInfMicroAnalisisVector modelo = _mapper.Map<TDataCreditoDataInfMicroAnalisisVector>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoDataInfMicroAnalisisVector Add(DataCreditoDataInfMicroAnalisisVector entidad)
        {
            try
            {
                var agregarEntidad = MapeoEntidad(entidad);
                base.Insert(agregarEntidad);
                return agregarEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoDataInfMicroAnalisisVector Update(DataCreditoDataInfMicroAnalisisVector entidad)
        {
            try
            {
                var actualizarEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                actualizarEntidad.RowVersion = entidadExistente.RowVersion;

                base.Update(actualizarEntidad);
                return actualizarEntidad;
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
        public IEnumerable<TDataCreditoDataInfMicroAnalisisVector> Add(IEnumerable<DataCreditoDataInfMicroAnalisisVector> listadoEntidad)
        {
            try
            {
                List<TDataCreditoDataInfMicroAnalisisVector> listado = new List<TDataCreditoDataInfMicroAnalisisVector>();
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

        public IEnumerable<TDataCreditoDataInfMicroAnalisisVector> Update(IEnumerable<DataCreditoDataInfMicroAnalisisVector> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDataCreditoDataInfMicroAnalisisVector> listado = new List<TDataCreditoDataInfMicroAnalisisVector>();
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