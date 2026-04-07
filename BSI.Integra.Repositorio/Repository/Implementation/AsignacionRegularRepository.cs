using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AsignacionRegularRepository
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class AsignacionRegularRepository : GenericRepository<TAsignacionRegular>, IAsignacionRegularRepository
    {
        private Mapper _mapper;

        public AsignacionRegularRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionRegular, AsignacionRegular>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAsignacionRegular MapeoEntidad(AsignacionRegular entidad)
        {
            try
            {
                //crea la entidad padre
                TAsignacionRegular modelo = _mapper.Map<TAsignacionRegular>(entidad);

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

        public TAsignacionRegular Add(AsignacionRegular entidad)
        {
            try
            {
                var AsignacionRegular = MapeoEntidad(entidad);
                base.Insert(AsignacionRegular);
                return AsignacionRegular;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsignacionRegular Update(AsignacionRegular entidad)
        {
            try
            {
                var AsignacionRegular = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionRegular.RowVersion = entidadExistente.RowVersion;

                base.Update(AsignacionRegular);
                return AsignacionRegular;
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


        public IEnumerable<TAsignacionRegular> Add(IEnumerable<AsignacionRegular> listadoEntidad)
        {
            try
            {
                List<TAsignacionRegular> listado = new List<TAsignacionRegular>();
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

        public IEnumerable<TAsignacionRegular> Update(IEnumerable<AsignacionRegular> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsignacionRegular> listado = new List<TAsignacionRegular>();
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

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la lista de asgiancion regular, para luego validar sus configuraciones 
        /// </summary>
        /// <returns> bool </returns>
        public List<ValidacionPaisConfiguracionAsignacionRegularDTO> ObtenerListaDeAsignacionRegular()
        {
            try
            {
                List<ValidacionPaisConfiguracionAsignacionRegularDTO> ListaAsignacionRegular = new List<ValidacionPaisConfiguracionAsignacionRegularDTO>();


                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerAsignacionRegular", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaAsignacionRegular = JsonConvert.DeserializeObject<List<ValidacionPaisConfiguracionAsignacionRegularDTO>>(resultado);
                    return ListaAsignacionRegular;
                }
                return ListaAsignacionRegular;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la lista de asgiancion regular, para luego validar sus configuraciones 
        /// </summary>
        /// <returns> bool </returns>
        public RecibirConfiguracionPrincipalPorPaisDTO ObtenerConfiguracionesPorPais(int IdAsignacionRegular, int IdPais)
        {
            try
            {
                RecibirConfiguracionPrincipalPorPaisDTO ConfiguracionPais = new RecibirConfiguracionPrincipalPorPaisDTO();

                var resultado = _dapperRepository.FirstOrDefault("SELECT Coordinador,Asesor,Estado,OportunidadesAbiertas,TopeOportunidad,ActivarAsignacionAutomatica FROM mkt.V_ObtenerListaAsesor", new { });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    ConfiguracionPais = JsonConvert.DeserializeObject<RecibirConfiguracionPrincipalPorPaisDTO>(resultado);
                }
                return ConfiguracionPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la lista de asgiancion regular, para luego validar sus configuraciones 
        /// </summary>
        /// <returns> bool </returns>
        public bool VerificarConfiguracionPorPais(int idAsignacionRegular)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ValidarPaisConfiguracionAsignacionRegular", new { idAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la Configuracion Principal
        /// </summary>
        /// <returns> bool </returns>
        public List<RecibirConfiguracionPrincipalDTO> ObtenerCOnfiguracionPrincipal(int IdGrupoFiltroProgramaCritico)
        {
            try
            {
                List<RecibirConfiguracionPrincipalDTO> ListaConfiguracionPrincipal = new List<RecibirConfiguracionPrincipalDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT OC.Id,OC.IdGrupoFiltroProgramaCritico,OC.Codigo,OC.Prioridad,OC. Coordinador,OC .Asesor,OC.DatoCalidad,OC.EsLimiteCola,OC.AplicaProporcionPorPais,OC.LimiteCola,OC.PorcentajeTolerancia FROM mkt.V_ObtenerCOnfiguraciones AS OC WHERE OC.IdGrupoFiltroProgramaCritico = @IdGrupoFiltroProgramaCritico ORDER BY OC.Asesor DESC ", new { IdGrupoFiltroProgramaCritico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaConfiguracionPrincipal = JsonConvert.DeserializeObject<List<RecibirConfiguracionPrincipalDTO>>(resultado);
                }
                return ListaConfiguracionPrincipal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtine la lista de asgiancion regular
        /// </summary>
        /// <returns> bool </returns>
        public List<ObtenerListaAsesorDTO> ObtenerListaAsesor()
        {
            try
            {
                int? IdAsignacionRegular = null;
                List<ObtenerListaAsesorDTO> ConfiguracionPais = new List<ObtenerListaAsesorDTO>();
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_AsignacionRegularAsesor", new { IdAsignacionRegular = IdAsignacionRegular } );
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    ConfiguracionPais = JsonConvert.DeserializeObject<List<ObtenerListaAsesorDTO>>(resultado);
                }
                return ConfiguracionPais.OrderByDescending(x => x.Coordinador).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtine la lista de asgiancion regular
        /// </summary>
        /// <returns> bool </returns>
        public ObtenerListaAsesorDTO ObtenerAsesorConfiguracion(int id)
        {
            try
            {
                ObtenerListaAsesorDTO ConfiguracionPais = new ObtenerListaAsesorDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_AsignacionRegularAsesor", new { IdAsignacionRegular = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    ConfiguracionPais = JsonConvert.DeserializeObject<ObtenerListaAsesorDTO>(resultado);
                }
                return ConfiguracionPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtine la lista de asgiancion regular
        /// </summary>
        /// <returns> bool </returns>
        public List<ObtenerAsesorConfiguracionPorPaisDTO> ObtenerAsesorConfiguracionPorPais(int idAsignacionRegular)
        {
            try
            {
                List<ObtenerAsesorConfiguracionPorPaisDTO> ConfiguracionPais = new List<ObtenerAsesorConfiguracionPorPaisDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT Id,IdAsignacionRegular,Codigo,CantidadTotal,ActivarAsignacionPaisConfiguracion,CantidadTotalPeru,DatoCalidadPeru,DistribucionPeru,CantidadTotalColombia,DatoCalidadColombia,DistribucionColombia,CantidadTotalBolivia,DatoCalidadBolivia,DistribucionBolivia,CantidadTotalMexico,DatoCalidadMexico,DistribucionChile, CantidadTotalChile,DatoCalidadChile,DistribucionMexico,CantidadTotalInternacional,DistribucionInternacional,DatoCalidadInternacional FROM mkt.V_ObtenerConfiguracionAsesor WHERE IdAsignacionRegular = @idAsignacionRegular ORDER BY Codigo DESC", new { idAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    ConfiguracionPais = JsonConvert.DeserializeObject<List<ObtenerAsesorConfiguracionPorPaisDTO>>(resultado);
                }
                return ConfiguracionPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtine la lista de asgiancion regular
        /// </summary>
        /// <returns> bool </returns>
        public List<ComboAsesoresDTO> ComboAsesores()
        {
            try
            {
                List<ComboAsesoresDTO> ConfiguracionPais = new List<ComboAsesoresDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT Id, CONCAT(Apellidos,' ',Nombres  ) AS Asesor FROM gp.T_Personal WHERE Estado = 1 AND Activo = 1 AND IdPersonalAreaTrabajo = 8 AND Id NOT IN (SELECT IdPersonal FROM mkt.T_AsignacionRegular WHERE Estado = 1) ORDER BY Apellidos desc", new { });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    ConfiguracionPais = JsonConvert.DeserializeObject<List<ComboAsesoresDTO>>(resultado);
                }
                return ConfiguracionPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la lista de asgiancion regular, para luego validar sus configuraciones 
        /// </summary>
        /// <returns> bool </returns>
        public List<ObtenerBloquePorProgramaCriticoDTO> ObtenerBloquePorProgramaCritico()
        {
            try
            {
                List<ObtenerBloquePorProgramaCriticoDTO> ListaBloquePorProgramaCritico = new List<ObtenerBloquePorProgramaCriticoDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT 	AR.IdGrupoFiltroProgramaCritico,GFPC.Nombre,COUNT(AR.Id) AS CantidadConfiguraciones FROM mkt.T_AsignacionRegular AS AR INNER JOIN pla.T_GrupoFiltroProgramaCritico AS GFPC ON GFPC.Id = AR.IdGrupoFiltroProgramaCritico WHERE AR.Estado = 1 GROUP BY AR.IdGrupoFiltroProgramaCritico,GFPC.Nombre", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaBloquePorProgramaCritico = JsonConvert.DeserializeObject<List<ObtenerBloquePorProgramaCriticoDTO>>(resultado);
                }
                return ListaBloquePorProgramaCritico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public int ActualizarAsignacionRegular(int Id, bool DatoCalidad, bool AplicaProporcionPorPais, bool EsLimiteCola, int LimiteCola, int Prioridad, int PorcentajeTolerancia, string UsuarioModificacion)
        {
            try
            {
                int Valor = new int();

                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ActualizarAsignacionRegular", new { Id, DatoCalidad, AplicaProporcionPorPais, EsLimiteCola, LimiteCola, Prioridad, PorcentajeTolerancia, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? InsertarAsignacionRegular(string ListaIdAsignacionRegular, String UsuarioCreacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarAsignacionRegular", new { ListaIdAsignacionRegular, UsuarioCreacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? InsertarCategoriaOrigenPorSector(string ListaCategoriaOrigen, int IdOrigenSector, String UsuarioCreacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarCategoriaOrigenPorSector", new { ListaCategoriaOrigen, IdOrigenSector, UsuarioCreacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? AgruparCategoriaOrigen(bool Agrupar, int id, String UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_AgruparCategoriaOrigen", new { Agrupar, id, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? EliminarConfiguracionCategoriaOrigen(int Id, String UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_EliminarConfiguracionCategoriaOrigen", new { Id, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? InsertarOrigenSector(string nombre, string descripcion, string UsuarioCreacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarOrigenSector", new { nombre, descripcion, UsuarioCreacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? EliminarOrigenSector(int Id, string UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_EliminarOrigenSectorBloque", new { Id, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? ActualizarConfiguracionAsignacionRegular(PaisConfiguracionAsignacionRegularDTO paisConfiguracionAsignacionRegular, string usuario)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarConfiguracionAsignacionRegular", new
                {
                    paisConfiguracionAsignacionRegular.Id,
                    paisConfiguracionAsignacionRegular.ActivarAsignacionPaisConfiguracion,
                    paisConfiguracionAsignacionRegular.DatoCalidadPeru,
                    paisConfiguracionAsignacionRegular.DistribucionPeru,
                    paisConfiguracionAsignacionRegular.DatoCalidadColombia,
                    paisConfiguracionAsignacionRegular.DistribucionColombia,
                    paisConfiguracionAsignacionRegular.DatoCalidadBolivia,
                    paisConfiguracionAsignacionRegular.DistribucionBolivia,
                    paisConfiguracionAsignacionRegular.DatoCalidadMexico,
                    paisConfiguracionAsignacionRegular.DistribucionMexico,
                    paisConfiguracionAsignacionRegular.DatoCalidadChile,
                    paisConfiguracionAsignacionRegular.DistribucionChile,
                    paisConfiguracionAsignacionRegular.DistribucionInternacional,
                    paisConfiguracionAsignacionRegular.DatoCalidadInternacional,
                    UsuarioModificacion = usuario
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool? ActualizarConfiguracionDetalle(DetallePaisConfiguracionAsignacionRegularDTO paisConfiguracionAsignacionRegular, string usuario)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarDetallePaisConfiguracionAsignacionRegular", new
                {
                    paisConfiguracionAsignacionRegular.Id,
                    paisConfiguracionAsignacionRegular.IdPaisConfiguracionAsignacionRegular,
                    paisConfiguracionAsignacionRegular.DatoCalidad,
                    paisConfiguracionAsignacionRegular.DatoCalidadWhatsapp,
                    paisConfiguracionAsignacionRegular.DatoCalidadMailing,
                    paisConfiguracionAsignacionRegular.Distribucion,

                    paisConfiguracionAsignacionRegular.IdPais,
                    UsuarioModificacion = usuario
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool? InsertarConfiguracionAsignacioDetalle(DetallePaisConfiguracionAsignacionRegularDTO detalleConfiguracion, string usuario)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarDetalleConfiguracionAsignacionRegular", new

                {
                    detalleConfiguracion.IdPaisConfiguracionAsignacionRegular,
                    detalleConfiguracion.DatoCalidad,
                    detalleConfiguracion.DatoCalidadWhatsapp,
                    detalleConfiguracion.DatoCalidadMailing,
                    detalleConfiguracion.Distribucion,
                    detalleConfiguracion.IdPais,
                    UsuarioCreacion = usuario

                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? InsertarConfiguracionAsignacionRegular(int IdAsignacionRegular, int IdProgramaGeneral, String UsuarioCreacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarConfiguracionAsignacionRegular", new { IdAsignacionRegular, IdProgramaGeneral, UsuarioCreacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? EliminarPaisConfiguracionAsignacionRegular(int Id, String UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_EliminarPaisConfiguracionAsignacionRegular", new { Id, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public bool? EliminarAsignacionRegular(int IdAsignacionRegular, String UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_EliminarAsignacionRegular", new { IdAsignacionRegular, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Activa o desactiva Asignación automatica
        /// </summary>
        /// <returns> bool </returns>
        public bool? ActivarAsignacionAutomatica(int idAsignacionRegular, bool Activar, String UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActivarAsignacionAutomatica", new { idAsignacionRegular, Activar, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Activa o desactiva Asignación automatica
        /// </summary>
        /// <returns> bool </returns>
        public bool? ActualizarTopeOportunidad(int idAsignacionRegular, int TopeOportunidad, String UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarTopeOportunidad", new { idAsignacionRegular, TopeOportunidad, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// Autor: Miguel Valdivia
        /// Fecha: 27/08/2025
        /// Version: 1.0
        /// <summary>
        /// Activa o desactiva Asignación Diaria
        /// </summary>
        /// <returns> bool </returns>
        public bool? ActualizarTopeAsignacionDiaria(int idAsignacionRegular, int TopeAsignacionDiaria, String UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarTopeAsignacionDiaria", new { idAsignacionRegular, TopeAsignacionDiaria, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 20/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza la prioirdad de asignación
        /// </summary>
        /// <returns> bool </returns>
        public bool? ActualizarPrioridad(int idAsignacionRegular, int Prioridad, String UsuarioModificacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarPrioridad", new { idAsignacionRegular, Prioridad, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los registros de la tabla asignacion Regular
        /// </summary>
        /// <returns> bool </returns>
        public int ActualizarPaisAsignacionRegular(int Id, bool EsProporcionManual, int ProporcionManual, int ProporcionPorPais, string UsuarioModificacion)
        {
            try
            {
                int Valor = new int();

                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarPaisConfiguracionAsignacionRegular", new { Id, EsProporcionManual, ProporcionManual, ProporcionPorPais, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 16/10/2022
        /// Version: 1.0
        /// <summary>
        /// Regulariza la configuracion de asignacion regular con la tabla de paisprogramas otras areas
        /// </summary>
        /// <returns> bool </returns>
        public bool RegularizarPaisProgramaOtraArea(int IdAsignacionRegular)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_RegularizarPaisProgramaOtraArea", new { IdAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la Configuracion Principal
        /// </summary>
        /// <returns> bool </returns>
        public List<ObtenerConfiguracionProgramasOtrasAreasDTO> ObtenerConfiguracionProgramasOtrasAreas(int IdGrupoFiltroProgramaCritico)
        {
            try
            {
                List<ObtenerConfiguracionProgramasOtrasAreasDTO> ListaConfiguracionProgramasOtrasAreas = new List<ObtenerConfiguracionProgramasOtrasAreasDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT OTPOA.* FROM mkt.V_ObtenerTabProgramaOtraArea AS OTPOA WHERE OTPOA.IdGrupoFiltroProgramaCritico = @IdGrupoFiltroProgramaCritico ", new { IdGrupoFiltroProgramaCritico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaConfiguracionProgramasOtrasAreas = JsonConvert.DeserializeObject<List<ObtenerConfiguracionProgramasOtrasAreasDTO>>(resultado);
                }
                return ListaConfiguracionProgramasOtrasAreas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine lista de categoría origen disponible
        /// </summary>
        /// <returns> bool </returns>
        public List<ListaCategoriaOrigenNoConfigurada> ObtenerCategoriaOrigen()
        {
            try
            {
                List<ListaCategoriaOrigenNoConfigurada> ListaCategoriaOrigenNoConfigurada = new List<ListaCategoriaOrigenNoConfigurada>();
                var resultado = _dapperRepository.QueryDapper("SELECT Id,nombre FROM mkt.V_ObtenerCategoriaOrigen ", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaCategoriaOrigenNoConfigurada = JsonConvert.DeserializeObject<List<ListaCategoriaOrigenNoConfigurada>>(resultado);
                }
                return ListaCategoriaOrigenNoConfigurada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine lista de categoría origen disponible
        /// </summary>
        /// <returns> bool </returns>
        public List<CategoriaOrigenPorSectorDTO> ObtenerCategoriaOrigenPorSector(int IdOrigenSector)
        {
            try
            {
                List<CategoriaOrigenPorSectorDTO> ListaCategoriaOrigenPorSector = new List<CategoriaOrigenPorSectorDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT id,IdOrigenSector,Nombre,AgruparCategoriaOrigen FROM mkt.V_ObtenerCategoriaOrigenPorSector WHERE IdOrigenSector = @IdOrigenSector ", new { IdOrigenSector });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaCategoriaOrigenPorSector = JsonConvert.DeserializeObject<List<CategoriaOrigenPorSectorDTO>>(resultado);
                }
                return ListaCategoriaOrigenPorSector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la Configuracion Principal
        /// </summary>
        /// <returns> bool </returns>
        public List<ListaProgramasGeneralesDTO> ObtenerComboListaProgramasGenerales()
        {
            try
            {
                List<ListaProgramasGeneralesDTO> ListaProgramasGenerales = new List<ListaProgramasGeneralesDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT PG.Id AS IdProgramaGeneral,PG.Codigo FROM pla.T_PGeneral AS PG WHERE PG.Estado = 1 AND PG.AsignaVenta = 1 ", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaProgramasGenerales = JsonConvert.DeserializeObject<List<ListaProgramasGeneralesDTO>>(resultado);
                }
                return ListaProgramasGenerales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas programasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public int ActualizarProgramaOtrasAreas(int IdProgramaOtraArea, int IdGrupoFiltroProgramaCritico, int IdAsignacionRegular, string Coordinador, string Asesor, string PGventa, bool BaseHistorica, bool DatoCalidad, bool EsLimitePeru, int LimitePeru, bool EsLimiteColombia, int LimiteColombia, bool EsLimiteMexico, int LimiteMexico, bool EsLimiteBolivia, int LimiteBolivia, bool EsLimiteInternacional, int LimiteInternacional, string UsuarioModificacion)
        {

            try
            {
                int Valor = new int();

                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarProgramaGeneral", new { IdProgramaOtraArea, IdGrupoFiltroProgramaCritico, IdAsignacionRegular, Coordinador, Asesor, PGventa, BaseHistorica, DatoCalidad, EsLimitePeru, LimitePeru, EsLimiteColombia, LimiteColombia, EsLimiteMexico, LimiteMexico, EsLimiteBolivia, LimiteBolivia, EsLimiteInternacional, LimiteInternacional, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public int AgregarPaisProgramaOtrasAreas(int? IdProgramaOtraArea, int? IdProgramaGeneral)
        {

            try
            {
                int Valor = new int();

                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_AgregarPaisProgramaOtraArea", new { IdProgramaOtraArea, IdProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public int EliminarPaisProgramaOtrasAreas(int? IdProgramaOtraArea, int? IdProgramaGeneral)
        {

            try
            {
                int Valor = new int();

                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_EliminarPaisProgramaOtraArea", new { IdProgramaOtraArea, IdProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine lista de programas para Comparacion
        /// </summary>
        /// <returns> bool </returns>
        public List<IdProgramaGeneralDTO> ObtenerListaProgramas(int IdProgramaOtraArea)
        {
            try
            {
                List<IdProgramaGeneralDTO> ListaProgramasGenerales = new List<IdProgramaGeneralDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT IPOAPAR.IdProgramaGeneral FROM mkt.T_ProgramaOtraAreaPorAsignacionRegular AS IPOAPAR  WHERE IPOAPAR.IdProgramaOtraArea =@IdProgramaOtraArea	AND IPOAPAR.Estado=1", new { IdProgramaOtraArea });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaProgramasGenerales = JsonConvert.DeserializeObject<List<IdProgramaGeneralDTO>>(resultado);
                }
                return ListaProgramasGenerales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine Combo
        /// </summary>
        /// <returns> bool </returns>
        public List<ComboProgramaCriticoDTO> ComboGrupoVenta()
        {
            try
            {
                List<ComboProgramaCriticoDTO> ListaGrupoVenta = new List<ComboProgramaCriticoDTO>();
                var resultado = _dapperRepository.QueryDapper("	SELECT AR.IdGrupoFiltroProgramaCritico,GFPC.Nombre FROM mkt.T_AsignacionRegular AS AR INNER JOIN pla.T_GrupoFiltroProgramaCritico AS GFPC ON GFPC.Id = AR.IdGrupoFiltroProgramaCritico WHERE AR.Estado = 1 AND GFPC.Estado = 1 GROUP BY AR.IdGrupoFiltroProgramaCritico,GFPC.Nombre", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaGrupoVenta = JsonConvert.DeserializeObject<List<ComboProgramaCriticoDTO>>(resultado);
                }
                return ListaGrupoVenta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine Combo
        /// </summary>
        /// <returns> bool </returns>
        public List<ComboProgramaGeneralDTO> ComboProgramaGeneral()
        {
            try
            {
                List<ComboProgramaGeneralDTO> ListaProgramaGeneral = new List<ComboProgramaGeneralDTO>();
                var resultado = _dapperRepository.QueryDapper("	 	SELECT AR.IdPGeneral, PG.Codigo	FROM mkt.T_AsignacionRegular AS AR	INNER JOIN pla.T_PGeneral AS PG	ON PG.Id = AR.IdPGeneral	WHERE AR.Estado = 1	GROUP BY AR.IdPGeneral, PG.Codigo", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaProgramaGeneral = JsonConvert.DeserializeObject<List<ComboProgramaGeneralDTO>>(resultado);
                }
                return ListaProgramaGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine Combo
        /// </summary>
        /// <returns> bool </returns>
        public List<ComboAsesorDTO> ComboAsesor()
        {
            try
            {
                List<ComboAsesorDTO> ListaAsesor = new List<ComboAsesorDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT AR.IdPersonal, CONCAT(P.Nombres,' ', P.Apellidos) AS Nombres    FROM mkt.T_AsignacionRegular AS AR  INNER JOIN gp.T_Personal AS P ON P.Id = AR.IdPersonal WHERE AR.Estado = 1 GROUP BY AR.IdPersonal , P.Nombres, P.Apellidos", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaAsesor = JsonConvert.DeserializeObject<List<ComboAsesorDTO>>(resultado);
                }
                return ListaAsesor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine Combo
        /// </summary>
        /// <returns> bool </returns>
        public List<ComboCoordinadorDTO> ComboPersonalJefe()
        {
            try
            {
                List<ComboCoordinadorDTO> ListaJefe = new List<ComboCoordinadorDTO>();
                var resultado = _dapperRepository.QueryDapper(" SELECT AR.IdPersonalJefe, CONCAT(P.Nombres,' ', P.Apellidos) AS NombresJefe    FROM mkt.T_AsignacionRegular AS AR  INNER JOIN gp.T_Personal AS P ON P.Id = AR.IdPersonalJefe WHERE AR.Estado = 1 GROUP BY AR.IdPersonalJefe , P.Nombres, P.Apellidos", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaJefe = JsonConvert.DeserializeObject<List<ComboCoordinadorDTO>>(resultado);
                }
                return ListaJefe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public List<ListaIdAsignacionRegularDTO> BuscarPorComboSeleccionados(int? IdProgramasGeneral, int? IdGrupoFiltroProgramaCritico, int? IdAsesor, int? IdCoordinador)
        {

            try
            {
                List<ListaIdAsignacionRegularDTO> ListaAsignacionRegular = new List<ListaIdAsignacionRegularDTO>();

                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerComboBusqueda", new { IdProgramasGeneral, IdGrupoFiltroProgramaCritico, IdAsesor, IdCoordinador });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaAsignacionRegular = JsonConvert.DeserializeObject<List<ListaIdAsignacionRegularDTO>>(resultado);
                }
                return ListaAsignacionRegular;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la Configuracion Principal
        /// </summary>
        /// <returns> bool </returns>
        public List<ObtenerConfiguracionProgramasOtrasAreasDTO> ObtenerListaConfiguracionesSeleccionadas(int? IdAsignacionRegular)
        {
            try
            {
                List<ObtenerConfiguracionProgramasOtrasAreasDTO> ListaConfiguracionProgramasOtrasAreas = new List<ObtenerConfiguracionProgramasOtrasAreasDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT OTPOA.* FROM mkt.V_ObtenerTabProgramaOtraArea AS OTPOA WHERE OTPOA.IdAsignacionRegular = @IdAsignacionRegular ", new { IdAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaConfiguracionProgramasOtrasAreas = JsonConvert.DeserializeObject<List<ObtenerConfiguracionProgramasOtrasAreasDTO>>(resultado);
                }
                return ListaConfiguracionProgramasOtrasAreas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la Configuracion Principal
        /// </summary>
        /// <returns> bool </returns>
        public List<RecibirConfiguracionPrincipalDTO> ObtenerCOnfiguracionPrincipalCombo(int? IdAsignacionRegular)
        {
            try
            {
                List<RecibirConfiguracionPrincipalDTO> ListaConfiguracionPrincipal = new List<RecibirConfiguracionPrincipalDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT OC.Id,OC.IdGrupoFiltroProgramaCritico,OC.Codigo,OC.Prioridad,OC. Coordinador,OC .Asesor,OC.DatoCalidad,OC.EsLimiteCola,OC.AplicaProporcionPorPais,OC.LimiteCola,OC.PorcentajeTolerancia FROM mkt.V_ObtenerConfiguracion AS OC WHERE OC.Id = @IdAsignacionRegular", new { IdAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaConfiguracionPrincipal = JsonConvert.DeserializeObject<List<RecibirConfiguracionPrincipalDTO>>(resultado);
                }
                return ListaConfiguracionPrincipal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public List<ListaIdAsignacionRegularDTO> ObtenerOportunidad(int? IdProgramasGeneral, int? IdGrupoFiltroProgramaCritico, int? IdAsesor, int? IdCoordinador)
        {

            try
            {
                List<ListaIdAsignacionRegularDTO> ListaAsignacionRegular = new List<ListaIdAsignacionRegularDTO>();

                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerComboBusqueda", new { IdProgramasGeneral, IdGrupoFiltroProgramaCritico, IdAsesor, IdCoordinador });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaAsignacionRegular = JsonConvert.DeserializeObject<List<ListaIdAsignacionRegularDTO>>(resultado);
                }
                return ListaAsignacionRegular;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public List<ListaIdAsignacionRegularDTO> ActualizarEstadoEjecucion(int? IdProgramasGeneral, int? IdGrupoFiltroProgramaCritico, int? IdAsesor, int? IdCoordinador)
        {

            try
            {
                List<ListaIdAsignacionRegularDTO> ListaAsignacionRegular = new List<ListaIdAsignacionRegularDTO>();

                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerComboBusqueda", new { IdProgramasGeneral, IdGrupoFiltroProgramaCritico, IdAsesor, IdCoordinador });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaAsignacionRegular = JsonConvert.DeserializeObject<List<ListaIdAsignacionRegularDTO>>(resultado);
                }
                return ListaAsignacionRegular;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public bool AsignacionAutomatizadaAsesor()
        {

            try
            {
                bool ListaOportunidadesConfiguradas = new bool();

                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ObtenerOportunidadAsignacionAutomatizada", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetallePaisConfiguracionAsignacionRegularDTO> ObtenerConfiguracionDetalle(int IdPaisConfiguracionAsignacionRegular)
        {
            try
            {

                List<DetallePaisConfiguracionAsignacionRegularDTO> ListaConfiguracionDetalle = new List<DetallePaisConfiguracionAsignacionRegularDTO>();
                var resultado = _dapperRepository.QueryDapper(@"SELECT Id, DatoCalidad,Distribucion,DatoCalidadMailing,DatoCalidadWhatsapp,Idpais 
                            FROM mkt.T_DetallePaisConfiguracionAsignacionRegular  
                            WHERE IdPaisConfiguracionAsignacionRegular=@IdPaisConfiguracionAsignacionRegular ;", new { IdPaisConfiguracionAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaConfiguracionDetalle = JsonConvert.DeserializeObject<List<DetallePaisConfiguracionAsignacionRegularDTO>>(resultado);
                }
                return ListaConfiguracionDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DetallePaisConfiguracionAsignacionRegularDTO? ObtenerConfiguracionDetallePorIdPais(int IdPaisConfiguracionAsignacionRegular, int IdPais)
        {
            try
            {

                List<DetallePaisConfiguracionAsignacionRegularDTO> ListaConfiguracionDetalle = new List<DetallePaisConfiguracionAsignacionRegularDTO>();
                var resultado = _dapperRepository.FirstOrDefault(@"SELECT Id,IdPaisConfiguracionAsignacionRegular, DatoCalidad,Distribucion,DatoCalidadMailing,DatoCalidadWhatsapp,IdPais 
                            FROM mkt.T_DetallePaisConfiguracionAsignacionRegular  
                            WHERE IdPaisConfiguracionAsignacionRegular=@IdPaisConfiguracionAsignacionRegular AND IdPais = @IdPais ;", new { IdPaisConfiguracionAsignacionRegular, IdPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<DetallePaisConfiguracionAsignacionRegularDTO>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetallePaisConfiguracionAsignacionRegularDTO> ObtenerConfiguracionDetallePais(int IdPaisConfiguracionAsignacionRegular)
        {
            try
            {

                List<DetallePaisConfiguracionAsignacionRegularDTO> ListaConfiguracionDetalle = new List<DetallePaisConfiguracionAsignacionRegularDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT  DatoCalidad,Distribucion,DatoCalidadMailing,DatoCalidadWhatsapp,IdPais FROM mkt.T_DetallePaisConfiguracionAsignacionRegular  WHERE IdPaisConfiguracionAsignacionRegular=@IdPaisConfiguracionAsignacionRegular ;", new { IdPaisConfiguracionAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaConfiguracionDetalle = JsonConvert.DeserializeObject<List<DetallePaisConfiguracionAsignacionRegularDTO>>(resultado);
                }
                return ListaConfiguracionDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarRegistrosDetallePaisConfiguracionAsignacionRegular(List<DetallePaisConfiguracionAsignacionRegularPaisDTO> ListaConfiguracionActualizada)
        {
            try
            {
                List<int> Valor = new List<int>();
                bool InsertExitoso = false;

                foreach (DetallePaisConfiguracionAsignacionRegularPaisDTO item in ListaConfiguracionActualizada)
                {
                    var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ActualizarConfiguracionesOrigenDatoCalidadDetalleV2",
                        new
                        {


                            item.IdPaisConfiguracionAsignacionRegular,
                            item.DatoCalidad,
                            item.Distribucion,
                            item.DatoCalidadWhatsapp,
                            item.DatoCalidadMailing,
                            item.IdPais,
                            item.Estado,
                            item.UsuarioCreacion,
                            item.UsuarioModificacion,
                            item.FechaCreacion,
                            item.FechaModificacion,
                        });





                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo 
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la Configuracion Principal
        /// </summary>
        /// <returns> bool </returns>
        public List<ObtenerOportunidadConfiguradaV2DTO> ObtenerOportunidadConfigurada()
        {
            try
            {

                List<ObtenerOportunidadConfiguradaV2DTO> ListaConfiguracionPrincipal = new List<ObtenerOportunidadConfiguradaV2DTO>();
                var query = @"
                    SELECT Id,
                           IdCategoriaOrigen,
                           IdPGeneral,
                           IdPais,
                           IdProbabilidadRegistroPW,
                           AsignacionDirecta,
                           AsignacionDirectaWhatsapp,
                           AsigancionDirectaMailing,
                           AsignacionRegular
                    FROM mkt.V_ObtenerOportunidadesAsignacionV2
                    ORDER BY CASE
                        WHEN AsignacionDirecta = 1 THEN
                            1
                        WHEN AsignacionDirectaWhatsapp = 1 THEN
                            2
                        WHEN AsigancionDirectaMailing = 1 THEN
                            3
                        WHEN AsignacionRegular = 1 THEN
                            4
                        ELSE
                            5
                    END,
                    Id ASC; ";
                var resultado = _dapperRepository.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaConfiguracionPrincipal = JsonConvert.DeserializeObject<List<ObtenerOportunidadConfiguradaV2DTO>>(resultado);
                }
                return ListaConfiguracionPrincipal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtine la Configuracion Principal
        /// </summary>
        /// <returns> bool </returns>
        public List<ObtenerAsesoresPorOportunidadDTO> ObtenerAsesoresPorOportunidad(int? IdProgramaGeneral)
        {
            try
            {

                List<ObtenerAsesoresPorOportunidadDTO> ListaConfiguracionPrincipal = new List<ObtenerAsesoresPorOportunidadDTO>();
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerAsesoresPorOportunidad", new { IdProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaConfiguracionPrincipal = JsonConvert.DeserializeObject<List<ObtenerAsesoresPorOportunidadDTO>>(resultado);
                }
                return ListaConfiguracionPrincipal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public List<VerificarSiAplicaProporcionAsignacionRegularDTO> VerificarSiAplicaProporcionAsignacionRegular()
        {

            try
            {
                List<VerificarSiAplicaProporcionAsignacionRegularDTO> ListaProgramaGeneralValidado = new List<VerificarSiAplicaProporcionAsignacionRegularDTO>();

                var resultado = _dapperRepository.QueryDapper("mkt.SP_VerificarSiAplicaProporcionAsignacionRegular", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {

                    ListaProgramaGeneralValidado = JsonConvert.DeserializeObject<List<VerificarSiAplicaProporcionAsignacionRegularDTO>>(resultado);

                }
                return ListaProgramaGeneralValidado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public SenderDTO ObtenerSender()
        {

            try
            {
                SenderDTO Sender = new SenderDTO();

                var resultado = _dapperRepository.FirstOrDefault("SELECT TOP 1 Email,Contrasenia FROM mkt.T_ListaCorreoAlerta WHERE Estado = 1 AND IdTipoCorreoAlerta = 1", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {

                    Sender = JsonConvert.DeserializeObject<SenderDTO>(resultado);

                }
                return Sender;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public List<AddresseeDTO> ObtenerAddressee()
        {

            try
            {
                List<AddresseeDTO> ListaAddressee = new List<AddresseeDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT Email FROM mkt.T_ListaCorreoAlerta WHERE Estado = 1 AND IdTipoCorreoAlerta = 2", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {

                    ListaAddressee = JsonConvert.DeserializeObject<List<AddresseeDTO>>(resultado);

                }
                return ListaAddressee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public List<AlgoritmoAsignacionAutomaticaPorPaisesDTO> AlgoritmoAsignacionAutomaticaPorPaises(int? IdPais, int? IdProgramaGeneral)
        {

            try
            {
                List<AlgoritmoAsignacionAutomaticaPorPaisesDTO> ListaConfiguraciones = new List<AlgoritmoAsignacionAutomaticaPorPaisesDTO>();

                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_AlgoritmoAsignacionAutomaticaPorPaises", new { IdPais, IdProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {

                    ListaConfiguraciones = JsonConvert.DeserializeObject<List<AlgoritmoAsignacionAutomaticaPorPaisesDTO>>(resultado);

                }
                return ListaConfiguraciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public bool? ActualizarRegistroPorInsertarAsesor(int? IdOportunidadPreAsignada, int? IdAsignacionRegular)
        {
            try
            {
                EstadoActualizacionDTO? EstadoActualizacion = new EstadoActualizacionDTO();
                EstadoActualizacion.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarRegistroPorInsertarAsesor", new { IdOportunidadPreAsignada, IdAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    EstadoActualizacion = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                }
                return EstadoActualizacion.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public bool? ActualizarRegistroPorInsertarAsesorCola(int? IdOportunidadPreAsignada, int? IdAsignacionRegular)
        {
            try
            {
                EstadoActualizacionDTO EstadoActualizacion = new EstadoActualizacionDTO();
                EstadoActualizacion.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ActualizarRegistroPorInsertarAsesorCola", new { IdOportunidadPreAsignada, IdAsignacionRegular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    EstadoActualizacion = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                }
                return EstadoActualizacion.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/18/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Las tablas PaisprogramasOtrasAreas
        /// </summary>
        /// <returns> bool </returns>
        public bool? RegularizarConfiguracionTemporalAsignacionRegular()
        {
            try
            {
                EstadoActualizacionDTO? EstadoActualizacion = new EstadoActualizacionDTO();
                EstadoActualizacion.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_RegularizarConfiguracionTemporalAsignacionRegular", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    EstadoActualizacion = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                }
                return EstadoActualizacion.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<AlumnoOp> ObtenerAlumnoPorOportunidad(int IdOportunidad)
        {
            try
            {
                List<AlumnoOp> alumno = new List<AlumnoOp>();
                var resultado = _dapperRepository.QueryDapper("SELECT al.Id, al.Nombre1 as Nombre, al.Celular FROM com.T_Oportunidad op  WITH (NOLOCK)  INNER JOIN mkt.T_Alumno al  WITH (NOLOCK)  ON op.IdAlumno = al.id  WHERE op.id = @IdOportunidad", new { IdOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    alumno = JsonConvert.DeserializeObject<List<AlumnoOp>>(resultado);
                }
                return alumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ContadorBicDias ObtenerContadorBic(int IdOportunidad)
        {
            try
            {
                ContadorBicDias contador = new ContadorBicDias();
                var resultado = _dapperRepository.QueryDapper("SELECT DiasSinContactoManhana, DiasSinContactoTarde FROM com.T_ContadorBic WHERE IdOportunidad = @IdOportunidad", new { IdOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    contador = JsonConvert.DeserializeObject<ContadorBicDias>(resultado);
                }
                return contador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PGeneralOportunidad ObtenerPGeneralPorOportunidad(int idoportunidad)
        {
            try
            {
                PGeneralOportunidad pgeneral = new PGeneralOportunidad();
                var resultado = _dapperRepository.FirstOrDefault("SELECT * FROM mkt.V_ObtenerPGeneralOportunidad WHERE IdOportunidad = @idoportunidad", new { idoportunidad });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    pgeneral = JsonConvert.DeserializeObject<PGeneralOportunidad>(resultado);
                }

                return pgeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<AsesoresValidacionDTO> ObtenerAsesoresActivos()
        {
            try
            {
                List<AsesoresValidacionDTO> pgeneral = new List<AsesoresValidacionDTO>();
                var resultado = _dapperRepository.QueryDapper("SELECT * FROM mkt.V_AsesoresActivosWhatsappMarketing", new { });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    pgeneral = JsonConvert.DeserializeObject<List<AsesoresValidacionDTO>>(resultado);
                }

                return pgeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IdDTO ObtenerPaisPorOportunidad(int idoportunidad)
        {
            try
            {
                IdDTO pais = new IdDTO();
                var resultado = _dapperRepository.FirstOrDefault("SELECT * FROM mkt.V_ObtenerPaisPorOportunidad where idOportunidad = @idoportunidad", new { idoportunidad });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    pais = JsonConvert.DeserializeObject<IdDTO>(resultado);
                }

                return pais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public EstadoDTO ValidarEnvioPorDias(string celular)
        {
            try
            {
                EstadoDTO estado = new EstadoDTO();
                var resultado = _dapperRepository.FirstOrDefault("SELECT TOP 1 Estado FROM com.V_TWhatsAppMensajeEnviadoCom_Obtener WHERE WaTo LIKE '%' + @celular + '%' AND FechaCreacion >= DATEADD(DAY, -1, GETDATE()) ORDER BY FechaCreacion DESC", new { celular });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    estado = JsonConvert.DeserializeObject<EstadoDTO>(resultado);
                }

                return estado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EnviosDTO ObtenerEnvioWhatsappAsignacion(int idoportunidad)
        {
            try
            {
                EnviosDTO valor = new EnviosDTO();
                var resultado = _dapperRepository.FirstOrDefault("SELECT NumeroEnvios FROM com.V_OportunidadEnvioAsignacion where idOportunidad = @idoportunidad", new { idoportunidad });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    valor = JsonConvert.DeserializeObject<EnviosDTO>(resultado);
                }

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertarEnviosWhatsappAsignacion(int Idoportunidad)
        {
            try
            {
                EstadoActualizacionDTO EstadoActualizacion = new EstadoActualizacionDTO();
                EstadoActualizacion.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_TEnviosWhatsappAsignacion_Insertar", new { IdOportunidad = Idoportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    EstadoActualizacion = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                }
                return EstadoActualizacion.Valor.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}





