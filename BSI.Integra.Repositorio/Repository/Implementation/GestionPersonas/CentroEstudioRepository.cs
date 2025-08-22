using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Repositorio: CentroEstudioRepository
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 08/04/2024
    /// <summary>
    /// Gestión general de T_CentroEstudio
    /// </summary>
    public class CentroEstudioRepository : GenericRepository<TCentroEstudio>, ICentroEstudioRepository
    {
        private Mapper _mapper;

        public CentroEstudioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCentroEstudio, CentroEstudio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCentroEstudio MapeoEntidad(CentroEstudio entidad)
        {
            try
            {
                TCentroEstudio modelo = _mapper.Map<TCentroEstudio>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCentroEstudio Add(CentroEstudio entidad)
        {
            try
            {
                var centroEstudio = MapeoEntidad(entidad);
                base.Insert(centroEstudio);
                return centroEstudio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCentroEstudio Update(CentroEstudio entidad)
        {
            try
            {
                var centroEstudio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                centroEstudio.RowVersion = entidadExistente.RowVersion;

                base.Update(centroEstudio);
                return centroEstudio;
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


        public IEnumerable<TCentroEstudio> Add(IEnumerable<CentroEstudio> listadoEntidad)
        {
            try
            {
                List<TCentroEstudio> listado = new List<TCentroEstudio>();
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

        public IEnumerable<TCentroEstudio> Update(IEnumerable<CentroEstudio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                var listado = new List<TCentroEstudio>();
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


        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 08/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CentroEstudio.
        /// </summary>
        /// <returns>IEnumerable CentroEstudioDTO</returns>
        IEnumerable<CentroEstudioDTO> ICentroEstudioRepository.Obtener()
        {
            try
            {
                var rpta = new List<CentroEstudioDTO>();
                var query = @"
                    SELECT Id, Nombre, IdPais, IdCiudad, Pais, Ciudad, IdTipoCentroEstudio, TipoCentroEstudio
                    FROM gp.V_TCentroEstudio_ObtenerCentroEstudio
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CentroEstudioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 08/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_CentroEstudio asociado a un identificador.
        /// </summary>
        /// <param name="idCentroEstudio">Id de CentroEstudio</param>
        /// <returns>CentroEstudioDTO</returns>
        CentroEstudio? ICentroEstudioRepository.ObtenerPorId(int idCentroEstudio)
        {
            try
            {
                CentroEstudio rpta = new();
                var query = @"
                    SELECT Id,
						Nombre,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
						RowVersion,
						IdMigracion
                    FROM gp.T_CentroEstudio
                    WHERE Estado = 1 AND Id = @idCentroEstudio";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCentroEstudio });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CentroEstudio>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }


        /// Autor: Eliot Arias
        /// Fecha: 26/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_CentroEstudio asociado a un identificador.
        /// </summary>
        /// <param name="idCentroEstudio">Id de CentroEstudio</param>
        /// <returns>CentroEstudioDTO</returns>
        public IEnumerable<CentroEstudioComboDTO> ObtenerComboCentroEstudio()
        {
            try
            {
                var rpta = new List<CentroEstudioComboDTO>();
                var query = @"SELECT
                                 Id,
                                 Nombre
                              FROM
                                 gp.V_TCentroEstudio_ObtenerCentroEstudio
                              WHERE
                                 Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CentroEstudioComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 28/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de elementos registrados de la tabla T_EstadoEstudio para combo
        /// </summary>
        /// <returns> List<FiltroIdNombreDTO> </returns>
        public IEnumerable<CentroEstudioComboDTO> ObtenerListaEstadoEstudioCombo()
        {
            try
            {
                var respuesta = new List<CentroEstudioComboDTO>();
                var query = @"SELECT
                                  Id,
                                  Nombre
                              FROM
                                  gp.T_EstadoEstudio
                              WHERE
                                  Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!String.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<CentroEstudioComboDTO>>(resultado);
                    return respuesta;
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
