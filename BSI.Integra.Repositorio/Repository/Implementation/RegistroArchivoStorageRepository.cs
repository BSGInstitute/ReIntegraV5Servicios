using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoRegistroArchivoStorageDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: RegistroArchivoStorageRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 01/09/2022
    /// <summary>
    /// Gestión general de T_RegistroArchivoStorage
    /// </summary>
    public class RegistroArchivoStorageRepository : GenericRepository<TRegistroArchivoStorage>, IRegistroArchivoStorageRepository
    {
        private Mapper _mapper;
        private object idPersonal;

        public RegistroArchivoStorageRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRegistroArchivoStorage, RegistroArchivoStorage>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TRegistroArchivoStorage MapeoEntidad(RegistroArchivoStorage entidad)
        {
            try
            {
                //crea la entidad padre
                TRegistroArchivoStorage modelo = _mapper.Map<TRegistroArchivoStorage>(entidad);

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

        public TRegistroArchivoStorage Add(RegistroArchivoStorage entidad)
        {
            try
            {
                var RegistroArchivoStorage = MapeoEntidad(entidad);
                base.Insert(RegistroArchivoStorage);
                return RegistroArchivoStorage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRegistroArchivoStorage Update(RegistroArchivoStorage entidad)
        {
            try
            {
                var RegistroArchivoStorage = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RegistroArchivoStorage.RowVersion = entidadExistente.RowVersion;

                base.Update(RegistroArchivoStorage);
                return RegistroArchivoStorage;
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


        public IEnumerable<TRegistroArchivoStorage> Add(IEnumerable<RegistroArchivoStorage> listadoEntidad)
        {
            try
            {
                List<TRegistroArchivoStorage> listado = new List<TRegistroArchivoStorage>();
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

        public IEnumerable<TRegistroArchivoStorage> Update(IEnumerable<RegistroArchivoStorage> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRegistroArchivoStorage> listado = new List<TRegistroArchivoStorage>();
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
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RegistroArchivoStorage para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<RegistroArchivoStorageComboDTO> ObtenerCombo()
        {
            try
            {
                List<RegistroArchivoStorageComboDTO> rpta = new List<RegistroArchivoStorageComboDTO>();

                var query = "SELECT Id,Nombrearchivo FROM mkt.T_RegistroArchivoStorage WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegistroArchivoStorageComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_RegistroArchivoStorage
        /// </summary>
        /// <returns> List<RegistroArchivoStorageDTO> </returns>
        public IEnumerable<RegistroArchivoStorageDTO> ObtenerRegistroArchivoStorage()
        {
            try
            {
                List<RegistroArchivoStorageDTO> rpta = new List<RegistroArchivoStorageDTO>();
                var query = @"select  Id,IdUrlSubContenedor,Nombrearchivo,Ruta,Estado,IdMigracion,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion  from mkt.T_RegistroArchivoStorage where Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegistroArchivoStorageDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RegistroArchivoStoragePermisosDTO> ObtenerTodoPorPermisosRegistroArchivoStorage(RegistroArchivoMostrarFiltroDTO registroArchivoMostrarFiltro)
        {
            try
            {
                List<RegistroArchivoStoragePermisosDTO> rpta = new List<RegistroArchivoStoragePermisosDTO>();
                var spDapper = "[ope].[SP_ObtenerContenedoresPorPermisos]";

                var resultado = _dapperRepository.QuerySPDapper(spDapper,
                    new
                    {
                        registroArchivoMostrarFiltro.IdPersonal,
                        registroArchivoMostrarFiltro.IdUrlBlockStorage,
                        registroArchivoMostrarFiltro.NombreArchivo
                    });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegistroArchivoStoragePermisosDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RegistroArchivoMostrarFiltroDTO> ObtenerMostrarFiltroArchivoStorage()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComboContenedorArchivoDTO> ObtenerContenedores(int IdPersonal)
        {

            try
            {
                List<ComboContenedorArchivoDTO> rpta = new List<ComboContenedorArchivoDTO>();
                var query = $@"SELECT DISTINCT
                                       IdContenedor,
                                       Contenedor,
                                       AplicaSubcontenedores,
                                       AplicaSubidaMultiple,
                                       AplicaValidacion
                                FROM mkt.V_RegistroArchivosContenedoresSubcontenedores RACS
                                INNER JOIN mkt.V_RegistroArchivoStoragePermisoUsuario RASP
                                    ON RACS.IdContenedor = RASP.IdUrlBlockStorage
                                WHERE IdPersonal = {IdPersonal}";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboContenedorArchivoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboSubcontenedorArchivoDTO> ObtenerSubcontenedores(int IdPersonal)
        {

            try
            {
                List<ComboSubcontenedorArchivoDTO> rpta = new List<ComboSubcontenedorArchivoDTO>();
                var query = $@"SELECT DISTINCT
                                       IdSubcontenedor,
                                       Subcontenedor,
                                       IdContenedor
                                FROM mkt.V_RegistroArchivosContenedoresSubcontenedores RACS
                                INNER JOIN mkt.V_RegistroArchivoStoragePermisoUsuario RASP
                                    ON RACS.IdContenedor = RASP.IdUrlBlockStorage
                                WHERE IdPersonal = {IdPersonal}";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboSubcontenedorArchivoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<ComboTipoSubcontenedorArchivoDTO> ObtenerTipoSubcontenedores(int IdPersonal)
        {
            try
            {
                List<ComboTipoSubcontenedorArchivoDTO> rpta = new List<ComboTipoSubcontenedorArchivoDTO>();

                var query = $@"SELECT DISTINCT RATS.Id,
                                        RATS.IdContenedor,
		                                RATS.IdUrlSubContenedor,
		                                RATS.Tipo,
		                                RATS.Ruta
                                    FROM mkt.V_RegistroArchivoTipoSubContenedor AS RATS
                                    INNER JOIN mkt.V_RegistroArchivoStoragePermisoUsuario AS RASP
	                                    ON RATS.IdContenedor = RASP.IdUrlBlockStorage
                                    WHERE RASP.IdPersonal = {IdPersonal}";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboTipoSubcontenedorArchivoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RegistroArchivoStorage para mostrarse en combo y sacar url para plantillas.
        /// </summary>
        /// <returns> List<RegistroArchivoObtenerUrlComboDTO> </returns>
        public IEnumerable<RegistroArchivoObtenerUrlComboDTO> ObtenerComboFirma()
        {
            try
            {
                List<RegistroArchivoObtenerUrlComboDTO> rpta = new List<RegistroArchivoObtenerUrlComboDTO>();

                var query = "SELECT id, NombreArchivo,Ruta FROM [mkt].[V_RegistroArchivoStorageFiltro] WHERE Contenedor = 'Firma' ORDER BY Id DESC ";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegistroArchivoObtenerUrlComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 28/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RegistroArchivoStorage filtrados por idUrlSubContenedor
        /// </summary>
        /// <returns> List<RegistroArchivoObtenerUrlComboDTO> </returns>
        public IEnumerable<RegistroArchivoObtenerUrlComboDTO> ObtenerRegistroArchivoStoragePorIdUrlSubContenedor(int idUrlSubContenedor)
        {
            try
            {
                List<RegistroArchivoObtenerUrlComboDTO> rpta = new List<RegistroArchivoObtenerUrlComboDTO>();
                var spDapper = "[mkt].[SP_ObtenerRegistroArchivoStoragePorIdUrlSubContenedor]";

                var resultado = _dapperRepository.QuerySPDapper(spDapper,
                new
                {
                    idUrlSubContenedor
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegistroArchivoObtenerUrlComboDTO>>(resultado);
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









