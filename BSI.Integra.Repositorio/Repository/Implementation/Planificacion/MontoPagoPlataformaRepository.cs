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
    /// Repositorio: MontoPagoPlataformaRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_MontoPagoPlataforma
    /// </summary>
    public class MontoPagoPlataformaRepository : GenericRepository<TMontoPagoPlataforma>, IMontoPagoPlataformaRepository
    {
        private Mapper _mapper;

        public MontoPagoPlataformaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPagoPlataforma, MontoPagoPlataforma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMontoPagoPlataforma MapeoEntidad(MontoPagoPlataforma entidad)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoPlataforma perfilAtrabajoCoeficiente = _mapper.Map<TMontoPagoPlataforma>(entidad);

                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPagoPlataforma Add(MontoPagoPlataforma entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                base.Insert(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPagoPlataforma Update(MontoPagoPlataforma entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilAtrabajoCoeficiente.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
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

        public IEnumerable<TMontoPagoPlataforma> Add(IEnumerable<MontoPagoPlataforma> listadoEntidad)
        {
            try
            {
                List<TMontoPagoPlataforma> listado = new List<TMontoPagoPlataforma>();
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

        public IEnumerable<TMontoPagoPlataforma> Update(IEnumerable<MontoPagoPlataforma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMontoPagoPlataforma> listado = new List<TMontoPagoPlataforma>();
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
        /// Obtiene toda información de T_MontoPagoPlataforma por medio del IdMontoPago
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - MontoPagoPlataforma </returns>
        public List<ValorIntDTO> ObtenerPorIdMontoPago(int idMontoPago)
        {
            try
            {
                var query = $@"
                    SELECT Id, IdPlataformaPago AS Valor
                    FROM pla.T_MontoPagoPlataforma
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
                throw new Exception($"#MPPR-OPIMP-001@Error en ObtenerPorIdMontoPago() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/03/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_MontoPagoPlataforma por medio de los ids MontoPago
        /// </summary>
        /// <param name="id"></param>
        /// <returns> ValorIntDTO </returns>
        public IEnumerable<ValorIntDTO> ObtenerPorIdsMontoPago(IEnumerable<int> idsMontoPago)
        {
            try
            {
                var query = $@"
                    SELECT IdMontoPago AS Id, IdPlataformaPago AS Valor FROM pla.T_MontoPagoPlataforma
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
                throw new Exception($"#MPPR-OPIMP-001@Error en ObtenerPorIdMontoPago() {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el IdPlataformaPago y el IdMontoPago
        /// </summary>
        /// <param name="idPlataformaPago"> (PK) de T_PlataformaPago </param>
        /// <param name="idMontoPago"> (PK) de T_MontoPago </param>
        /// <returns> Entidad - MontoPagoPlataforma </returns>
        public MontoPagoPlataforma ObtenerPorIdPlataformaPagoYIdMontoPago(int idPlataformaPago, int idMontoPago)
        {
            try
            {
                var query = $@"SELECT Id,
                                   IdMontoPago,
                                   IdPlataformaPago,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_MontoPagoPlataforma
                            WHERE Estado = 1
                                  AND IdPlataformaPago = @IdPlataformaPago
                                  AND IdMontoPago = @IdMontoPago;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPlataformaPago = idPlataformaPago, IdMontoPago = idMontoPago });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<MontoPagoPlataforma>(resultado);
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPPR-OPIMP-001@Error en ObtenerPorIdPlataformaPagoYIdMontoPago() {ex.Message}", ex);
            }
        }
    }
}
