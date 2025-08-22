using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Repositorio: ControlSolicitudOrigenRepository
    /// Autor: Jorge Gamero
    /// Fecha: 18/07/2024
    /// <summary>
    /// Gestión general de T_ControlSolicitudOrigen
    /// </summary>
    public class ControlSolicitudOrigenRepository : GenericRepository<TControlSolicitudOrigen>, IControlSolicitudOrigenRepository
    {
        private Mapper _mapper;

        public ControlSolicitudOrigenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TControlSolicitudOrigen, ControlSolicitudOrigen>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TControlSolicitudOrigen MapeoEntidad(ControlSolicitudOrigen entidad)
        {
            try
            {
                TControlSolicitudOrigen modelo = _mapper.Map<TControlSolicitudOrigen>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TControlSolicitudOrigen Add(ControlSolicitudOrigen entidad)
        {
            try
            {
                var ControlSolicitudOrigen = MapeoEntidad(entidad);
                base.Insert(ControlSolicitudOrigen);
                return ControlSolicitudOrigen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TControlSolicitudOrigen Update(ControlSolicitudOrigen entidad)
        {
            try
            {
                var ControlSolicitudOrigen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ControlSolicitudOrigen.RowVersion = entidadExistente.RowVersion;

                base.Update(ControlSolicitudOrigen);
                return ControlSolicitudOrigen;
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

        #endregion
        /// Autor: Jorge Gamero
        /// Fecha: 18/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> ControlSolicitudOrigen </returns>
        public ControlSolicitudOrigen ObtenerPorId(int id)
        {
            try
            {
                var rpta = new ControlSolicitudOrigen();
                var query = @"SELECT * FROM ope.T_ControlSolicitudOrigen WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ControlSolicitudOrigen>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jorge Gamero
        /// Fecha: 18/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM ope.T_ControlSolicitudOrigen WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 18/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id, Nombre y Descripcíon de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ControlSolicitudOrigen> ObtenerRegistros()
        {
            try
            {
                var rpta = new List<ControlSolicitudOrigen>();
                var query = @"SELECT Id, Nombre, Descripcion FROM ope.T_ControlSolicitudOrigen WHERE Estado =1 ORDER BY Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ControlSolicitudOrigen>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
