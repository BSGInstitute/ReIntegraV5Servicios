using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class ModuloSistemaV5Repository : GenericRepository<TModuloSistemaV5>, IModuloSistemaV5Repository
    {
        private Mapper _mapper;
        public ModuloSistemaV5Repository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModuloSistemaV5, ModuloSistemaV5DTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModuloSistemaV5 MapeoEntidad(ModuloSistemaV5DTO entidad)
        {
            try
            {
                TModuloSistemaV5 modelo = _mapper.Map<TModuloSistemaV5>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModuloSistemaV5 Add(ModuloSistemaV5DTO entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                base.Insert(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModuloSistemaV5 Update(ModuloSistemaV5DTO entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralConfiguracionPlantilla.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
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


        public IEnumerable<TModuloSistemaV5> Add(IEnumerable<ModuloSistemaV5DTO> listadoEntidad)
        {
            try
            {
                List<TModuloSistemaV5> listado = new List<TModuloSistemaV5>();
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

        public IEnumerable<TModuloSistemaV5> Update(IEnumerable<ModuloSistemaV5DTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModuloSistemaV5> listado = new List<TModuloSistemaV5>();
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

        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> plantilla = new List<ComboDTO>();
                var query = string.Empty;
                query = @"
                SELECT 
                    Id,
                    Nombre 
                FROM conf.T_ModuloSistemav5 
                WHERE Estado = 1";
                var modulo = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(modulo) && modulo != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<List<ComboDTO>>(modulo);
                }
                return plantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModuloSistemaDTO> ObtenerListaModulos(int idUsuario)
        {
            try
            {
                List<ModuloSistemaDTO> modulos = new List<ModuloSistemaDTO>();
                var query = "conf.SP_ObtenerModulosListaV5";
                var res = _dapperRepository.QuerySPDapper(query, new { idUsuario });
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    modulos = JsonConvert.DeserializeObject<List<ModuloSistemaDTO>>(res);
                }
                return modulos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModuloSistemaDTO> ObtenerMisModulos(int idUsuario)
        {
            try
            {
                List<ModuloSistemaDTO> modulos = new List<ModuloSistemaDTO>();
                var query = @"SELECT
                                        Id, IdModulo, NombreGrupo, NombreModulo, URL
                                    FROM conf.V_ObtenerModulosAsignadosV5
                                    WHERE IdUsuario = @idUsuario AND Estado = 1";
                var res = _dapperRepository.QueryDapper(query, new { idUsuario });
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    modulos = JsonConvert.DeserializeObject<List<ModuloSistemaDTO>>(res);
                }
                return modulos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModuloSistemaDTO> AsignarModulo(int idUsuario)
        {
            try
            {
                List<ModuloSistemaDTO> modulos = new List<ModuloSistemaDTO>();
                var query = @"SELECT
                                        Id, IdModulo, NombreGrupo, NombreModulo, URL
                                    FROM gp.V_ObtenerModulosAsignadosV5
                                    WHERE IdUsuario = @idUsuario";
                var res = _dapperRepository.QueryDapper(query, new { idUsuario });
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    modulos = JsonConvert.DeserializeObject<List<ModuloSistemaDTO>>(res);
                }
                return modulos;
            } catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool AsignarModulo(AsignarModuloV5DTO dto, string personal)
        {
            try
            {
                var query = "conf.SP_AsignacionModuloV5";
                var res = _dapperRepository.QuerySPDapper(query, new { dto.IdUsuario, dto.IdsModulo, personal });
                if (!string.IsNullOrEmpty(res) && res.Contains("1"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DesasignarModulo(AsignarModuloV5DTO dto, string personal)
        {
            try
            {
                var query = "conf.SP_DesasignacionModuloV5";
                var res = _dapperRepository.QuerySPDapper(query, new { dto.IdUsuario, dto.IdsModulo, personal });
                if (!string.IsNullOrEmpty(res) && res.Contains("1"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ModuloSistemaDTO> VerificarModuloAsociado(int idUsuario, int idModulo)
        {
            try
            {
                List<ModuloSistemaDTO> modulos = new List<ModuloSistemaDTO>();
                var query = @"SELECT
                                        Id, IdModulo, NombreGrupo, NombreModulo, URL
                                    FROM gp.V_ObtenerModulosAsignadosV5
                                    WHERE IdUsuario = @idUsuario";
                var res = _dapperRepository.QueryDapper(query, new { idUsuario });
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    modulos = JsonConvert.DeserializeObject<List<ModuloSistemaDTO>>(res);
                }
                return modulos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModuloUrlDTO ObtenerNombreUrlModulos(string segmentoModulo)
        {
            try
            {
                ModuloUrlDTO Modulo = new ModuloUrlDTO();

                var query = @"SELECT TOP 1 Nombre as NombreModulo, Url FROM conf.T_ModuloSistemaV5
                                    WHERE Url =@segmentoModulo AND ESTADO=1";
                var res = _dapperRepository.FirstOrDefault(query, new { segmentoModulo= segmentoModulo });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                {
                    Modulo = JsonConvert.DeserializeObject<ModuloUrlDTO>(res);
                }
                return Modulo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ModuloSistemaModuloGrupoDTO> ObtenerModulosGrupoModulo()
        {
            try
            {
                List<ModuloSistemaModuloGrupoDTO> listaModuloPuestoTrabajo = new List<ModuloSistemaModuloGrupoDTO>();
                var _query = "SELECT Id, Nombre, IdModuloSistemaGrupo, ModuloSistemaGrupo, Url, IdTipo, NombreTipo FROM gp.V_TModuloSistema_ObtenerGrupoPorModuloSistema WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(_query, null);
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    listaModuloPuestoTrabajo = JsonConvert.DeserializeObject<List<ModuloSistemaModuloGrupoDTO>>(res);
                }
                return listaModuloPuestoTrabajo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
