using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MontoPagoSuscripcionRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_MontoPagoSuscripcion
    /// </summary>
    public class MontoPagoSuscripcionRepository : GenericRepository<TMontoPagoSuscripcion>, IMontoPagoSuscripcionRepository
    {
        private Mapper _mapper;

        public MontoPagoSuscripcionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPagoSuscripcion, MontoPagoSuscripcion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMontoPagoSuscripcion MapeoEntidad(MontoPagoSuscripcion entidad)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoSuscripcion montoPagoSuscripcion = _mapper.Map<TMontoPagoSuscripcion>(entidad);

                return montoPagoSuscripcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPagoSuscripcion Add(MontoPagoSuscripcion entidad)
        {
            try
            {
                var montoPagoSuscripcion = MapeoEntidad(entidad);
                base.Insert(montoPagoSuscripcion);
                return montoPagoSuscripcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPagoSuscripcion Update(MontoPagoSuscripcion entidad)
        {
            try
            {
                var montoPagoSuscripcion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                montoPagoSuscripcion.RowVersion = entidadExistente.RowVersion;

                base.Update(montoPagoSuscripcion);
                return montoPagoSuscripcion;
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

        public IEnumerable<TMontoPagoSuscripcion> Add(IEnumerable<MontoPagoSuscripcion> listadoEntidad)
        {
            try
            {
                List<TMontoPagoSuscripcion> listado = new List<TMontoPagoSuscripcion>();
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

        public IEnumerable<TMontoPagoSuscripcion> Update(IEnumerable<MontoPagoSuscripcion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMontoPagoSuscripcion> listado = new List<TMontoPagoSuscripcion>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_MontoPagoSuscripcion por medio del IdMontoPago
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - MontoPagoSuscripcion </returns>
        public List<ValorIntDTO> ObtenerPorIdMontoPago(int idMontoPago)
        {
            try
            {
                var query = $@"
                    SELECT Id, IdSuscripcionProgramaGeneral AS Valor
                    FROM pla.T_MontoPagoSuscripcion
                    WHERE Estado = 1 AND IdMontoPago = @idMontoPago";
                var resultado = _dapperRepository.QueryDapper(query, new { idMontoPago });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado)!;
                }
                return new List<ValorIntDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPSR-OPIMP-001@Error en ObtenerPorIdMontoPago() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_MontoPagoSuscripcion por medio del IdMontoPago
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - MontoPagoSuscripcion </returns>
        public IEnumerable<ValorIntDTO> ObtenerPorIdsMontoPago(IEnumerable<int> idsMontoPago)
        {
            try
            {
                var query = $@"
                    SELECT IdMontoPago AS Id, IdSuscripcionProgramaGeneral AS Valor
                    FROM pla.T_MontoPagoSuscripcion
                    WHERE Estado = 1 AND IdMontoPago IN @idsMontoPago";
                var resultado = _dapperRepository.QueryDapper(query, new { idsMontoPago });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ValorIntDTO>>(resultado)!;
                }
                return new List<ValorIntDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPSR-OPIMP-001@Error en ObtenerPorIdMontoPago() {ex.Message}", ex);
            }
        }
    }
}
