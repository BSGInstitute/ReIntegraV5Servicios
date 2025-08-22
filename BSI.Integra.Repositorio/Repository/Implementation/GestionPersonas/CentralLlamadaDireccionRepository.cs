using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Repositorio: CentralLlamadaDireccionRepository
    /// Autor: Victor Hinojosa
    /// Fecha: 25/09/2024
    /// <summary>
    /// Gestión general de T_CentralLlamadaDireccion
    /// </summary>
    public class CentralLlamadaDireccionRepository: GenericRepository<TCentralLlamadaDireccion>, ICentralLlamadaDireccionRepository
    {
        private Mapper _mapper;
        public CentralLlamadaDireccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCentralLlamadaDireccion, CentralLlamadaDireccion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCentralLlamadaDireccion MapeoEntidad(CentralLlamadaDireccion entidad)
        {
            try
            {
                TCentralLlamadaDireccion modelo = _mapper.Map<TCentralLlamadaDireccion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCentralLlamadaDireccion Add(CentralLlamadaDireccion entidad)
        {
            try
            {
                var centralLlamadaDireccion = MapeoEntidad(entidad);
                base.Insert(centralLlamadaDireccion);
                return centralLlamadaDireccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCentralLlamadaDireccion Update(CentralLlamadaDireccion entidad)
        {
            try
            {
                var centralLlamadaDireccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                centralLlamadaDireccion.RowVersion = entidadExistente.RowVersion;

                base.Update(centralLlamadaDireccion);
                return centralLlamadaDireccion;
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


        public IEnumerable<TCentralLlamadaDireccion> Add(IEnumerable<CentralLlamadaDireccion> listadoEntidad)
        {
            try
            {
                List<TCentralLlamadaDireccion> listado = new List<TCentralLlamadaDireccion>();
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

        public IEnumerable<TCentralLlamadaDireccion> Update(IEnumerable<CentralLlamadaDireccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                var listado = new List<TCentralLlamadaDireccion>();
                foreach (var entidad in listadoEntidad)
                    listado.Add(MapeoEntidad(entidad));

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

        /// Autor: Victor Hinojosa
        /// Fecha: 25/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_CentralLlamadaDireccion.
        /// </summary>
        /// <returns>IEnumerable CentralLlamadaDireccionDTO</returns>

        public IEnumerable<CentralLlamadaDireccionDTO> Obtener()
        {

            try
            {
                IEnumerable<CentralLlamadaDireccionDTO> rpta = new List<CentralLlamadaDireccionDTO>();
                var query = @"Select  Id, Nombre,DireccionIp from conf.T_CentralLlamadaDireccion Where estado= 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<CentralLlamadaDireccionDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 25/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_DominioPbx
        /// </summary>
        /// <returns>IEnumerable CentralLlamadaDireccionDTO</returns>
        public IEnumerable<DominioPbxDTO> ObtenerComboDominioPbx()
        {

            try
            {
                IEnumerable<DominioPbxDTO> ListaDetalleDominio = new List<DominioPbxDTO>();
                var query = "SELECT Id, Nombre FROM conf.T_DominioPbx WHERE Estado = 1 ORDER BY FechaModificacion DESC";
                var DominiosPbx = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(DominiosPbx) && !DominiosPbx.Contains("[]"))
                {
                    ListaDetalleDominio = JsonConvert.DeserializeObject<IEnumerable<DominioPbxDTO>>(DominiosPbx)!;
                }
                return ListaDetalleDominio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DominioPbx ObtenerDetalleDominioUnitario(int id)
        {
            DominioPbx detalleDominio = new DominioPbx();
            var query = "SELECT Id, Nombre, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, Estado, RowVersion, IdMigracion FROM conf.T_DominioPbx WHERE Estado = 1 AND Id = @Id";
            var DominiosPbx = _dapperRepository.FirstOrDefault(query, new { Id = id });
            if (!string.IsNullOrEmpty(DominiosPbx) && !DominiosPbx.Contains("[]"))
            {
                detalleDominio = JsonConvert.DeserializeObject<DominioPbx>(DominiosPbx);
            }
            return detalleDominio;
        }

    }

}
