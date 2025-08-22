using AutoMapper;

using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Operacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Operacion;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CentroCostoCertificadoRepository
    /// Autor: Marco Jose Villanueva Torres
    /// Fecha: 15/09/2023
    /// <summary>
    /// Gestión general de T_ CentroCostoCertificado
    /// </summary>
    public class CentroCostoCertificadoRepository : GenericRepository<TCentroCostoCertificado>, ICentroCostoCertificadoRepository
    {
        private Mapper _mapper;

        public CentroCostoCertificadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCentroCostoCertificado, CentroCostoCertificado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCentroCostoCertificado MapeoEntidad(CentroCostoCertificado entidad)
        {
            try
            {
                //crea la entidad padre
                TCentroCostoCertificado modelo = _mapper.Map<TCentroCostoCertificado>(entidad);


                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCentroCostoCertificado Add(CentroCostoCertificado entidad)
        {
            try
            {
                var CentroCostoCertificado = MapeoEntidad(entidad);
                base.Insert(CentroCostoCertificado);
                return CentroCostoCertificado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCentroCostoCertificado Update(CentroCostoCertificado entidad)
        {
            try
            {
                var CentroCostoCertificado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CentroCostoCertificado.RowVersion = entidadExistente.RowVersion;

                base.Update(CentroCostoCertificado);
                return CentroCostoCertificado;
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


        public IEnumerable<TCentroCostoCertificado> Add(IEnumerable<CentroCostoCertificado> listadoEntidad)
        {
            try
            {
                List<TCentroCostoCertificado> listado = new List<TCentroCostoCertificado>();
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

        public IEnumerable<TCentroCostoCertificado> Update(IEnumerable<CentroCostoCertificado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCentroCostoCertificado> listado = new List<TCentroCostoCertificado>();
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
        public CentroCostoCertificado ObtenerPorId(int id)
        {
            try
            {
                CentroCostoCertificado rpta = new();
                var query = @"
                            SELECT 
                                     Id,
                                     IdCentroCosto,
                                     IdCertificadoBrochure,
                                     IdCertificadoPartnerComplemento,
                                     FechaCreacion,
	                                 FechaModificacion,
	                                 UsuarioCreacion,
	                                 UsuarioModificacion,
                                     Estado,
	                                 RowVersion,
	                                 IdMigracion
                            FROM ope.T_CentroCostoCertificado
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CentroCostoCertificado>(resultado)!;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }    
        public CentroCostoCertificado ObtenerPorCentroCosto(int idCentroCosto)
        {
            try
            {
                CentroCostoCertificado rpta = new();
                var query = @"
                            SELECT 
                                     Id,
                                     IdCentroCosto,
                                     IdCertificadoBrochure,
                                     IdCertificadoPartnerComplemento,
                                     FechaCreacion,
	                                 FechaModificacion,
	                                 UsuarioCreacion,
	                                 UsuarioModificacion,
                                     Estado,
	                                 RowVersion,
	                                 IdMigracion
                            FROM ope.T_CentroCostoCertificado
                            WHERE Estado = 1 AND IdCentroCosto = @idCentroCosto";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CentroCostoCertificado>(resultado)!;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"#CCCR-OPCC-001@Error en ObtenerPorCentroCosto(), {ex.Message}");
            }
        }
    }
}
