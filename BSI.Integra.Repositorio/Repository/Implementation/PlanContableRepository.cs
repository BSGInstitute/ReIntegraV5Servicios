using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlanContableRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_PlanContable
    /// </summary>
    public class PlanContableRepository : GenericRepository<TPlanContable>, IPlanContableRepository
    {
        private Mapper _mapper;

        public PlanContableRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlanContable, PlanContable>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPlanContable, PlanContableDatosDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPlanContable MapeoEntidad(PlanContable entidad)
        {
            try
            {
                //crea la entidad padre
                TPlanContable modelo = _mapper.Map<TPlanContable>(entidad);

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

        public TPlanContable Add(PlanContable entidad)
        {
            try
            {
                var PlanContable = MapeoEntidad(entidad);
                base.Insert(PlanContable);
                return PlanContable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlanContable Update(PlanContable entidad)
        {
            try
            {
                var PlanContable = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlanContable.RowVersion = entidadExistente.RowVersion;

                base.Update(PlanContable);
                return PlanContable;
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


        public IEnumerable<TPlanContable> Add(IEnumerable<PlanContable> listadoEntidad)
        {
            try
            {
                List<TPlanContable> listado = new List<TPlanContable>();
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

        public IEnumerable<TPlanContable> Update(IEnumerable<PlanContable> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlanContable> listado = new List<TPlanContable>();
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PlanContable.
        /// </summary>
        /// <returns> List<PlanContableDTO> </returns>
        public IEnumerable<PlanContableDTO> ObtenerPlanContable()
        {
            try
            {
                List<PlanContableDTO> rpta = new List<PlanContableDTO>();
                var query = @"
                    SELECT 
                        Id, 
                        Cuenta, 
                        Descripcion, 
                        Padre, Univel, 
                        Cbal, Debe, 
                        Haber, 
                        IdTipoCuenta, 
                        TipoCuenta, 
                        Analisis, 
                        CentroCosto,
                        Estado, 
                        UsuarioModificacion, 
                        FechaModificacion,
                        UsuarioCreacion, 
                        FechaCreacion,
                        IdFurTipoSolicitud
                    FROM FIN.V_ObtenerPlanContable 
                    where Estado = 1 and cuenta like '__' order by Cuenta";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlanContableDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuentas hijas de la clase Actual que se digita.
        /// </summary>
        /// <returns> List<PlanContableDTO> </returns>
        /// <param name="Cuenta"></param>
        public IEnumerable<PlanContableDTO> ObteneCuentasHijo(long cuenta)
        {
            try
            {
                List<PlanContableDTO> planContableCuentasHijo = new List<PlanContableDTO>();
                var _query = @"
                    SELECT 
                        Id,Cuenta,Descripcion,Padre,Univel,
                        Cbal,Debe,Haber,IdTipoCuenta,TipoCuenta,
                        Analisis,CentroCosto,Estado,UsuarioModificacion,FechaModificacion,UsuarioCreacion,FechaCreacion,IdFurTipoSolicitud
                    FROM FIN.V_ObtenerPlanContable where Estado = 1 and cuenta like CONCAT(@cuenta,'_') and Padre =@cuenta order by Cuenta";

                var planContableBD = _dapperRepository.QueryDapper(_query, new { cuenta });
                if (!planContableBD.Contains("[]") && !string.IsNullOrEmpty(planContableBD))
                {
                    planContableCuentasHijo = JsonConvert.DeserializeObject<List<PlanContableDTO>>(planContableBD);
                }
                if (planContableCuentasHijo.Count() == 0 || cuenta.ToString().Length == 4)
                {
                    _query = @"
                        SELECT 
                            Id,Cuenta,Descripcion,Padre,Univel,
                            Cbal,Debe,Haber,IdTipoCuenta,TipoCuenta,
                            Analisis,CentroCosto,Estado,UsuarioModificacion,
                            FechaModificacion,UsuarioCreacion,FechaCreacion,IdFurTipoSolicitud
                        FROM FIN.V_ObtenerPlanContable where Estado = 1 and cuenta like CONCAT(@cuenta,'_%') and Padre =@cuenta order by Cuenta";
                    planContableBD = _dapperRepository.QueryDapper(_query, new { cuenta });
                    if (!planContableBD.Contains("[]") && !string.IsNullOrEmpty(planContableBD))
                    {
                        planContableCuentasHijo = JsonConvert.DeserializeObject<List<PlanContableDTO>>(planContableBD);
                    }
                }

                return planContableCuentasHijo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlanContable para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        public IEnumerable<PlanContableComboDTO> ObtenerCombo()
        {
            try
            {
                List<PlanContableComboDTO> rpta = new List<PlanContableComboDTO>();
                var query = @"SELECT Id,Cuenta as Nombre FROM [fin].[T_PlanContable] WHERE Estado = 1 order by Cuenta asc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlanContableComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PlanContable dado un Indicio de Nombre, usado para AutoComplete [Cuenta->Id, Descripcion->Nombre]
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param name="NombreParcial"></param>
        public IEnumerable<PlanContableCuentasDTO> ObtenerPlanContableAutoComplete()
        {
            try
            {
                List<PlanContableCuentasDTO> lista = new List<PlanContableCuentasDTO>();
                string _query = string.Empty;
                _query = @"select Id,Cuenta,CONVERT(VARCHAR(50),CONCAT(Cuenta,' - ',UPPER(Descripcion))) AS Nombre FROM fin.T_PlanContable WHERE  Estado=1";
                var listaDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PlanContableCuentasDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PlanContable dado un Indicio de Nombre, usado para AutoComplete [Cuenta->Id, Descripcion->Nombre]
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param name="NombreParcial"></param>
        public IEnumerable<PlanContableComboDTO> ObtenerPlanContableFiltro(string NombreParcial)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id, Nombre 
                    FROM fin.V_PlanContableEstadoCuentaPagadoPendiente where Nombre like '%" + NombreParcial + "%'";
                var planContable = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PlanContableComboDTO>>(planContable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Plan Contable con su rubro asociado (para llenado de grilla en modulo AsociarPlanContableRubro)
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        public IEnumerable<PlanContableConRubroDTO> ObtenerPlanContableConRubro()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id, Cuenta, Descripcion, IdFurTipoSolicitud, NombreFurTipoSolicitud 
                    FROM [fin].[V_PlanContableConRubro] where Estado = 1";
                var planContable = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PlanContableConRubroDTO>>(planContable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de Plan Contable con su rubro asociado (para llenado de grilla en modulo AsociarPlanContableRubro)
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param  name="IdPlanContable"></param>
        public IEnumerable<PlanContableConRubroDTO> ObtenerPlanContableConRubro(int IdPlanContable)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id, Cuenta, Descripcion, 
                        IdFurTipoSolicitud, NombreFurTipoSolicitud 
                    FROM [fin].[V_PlanContableConRubro] 
                    where Estado = 1 and Id=" + IdPlanContable;
                var planContable = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<PlanContableConRubroDTO>>(planContable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id,Nombre T_PlanContableTipoCuenta.
        /// </summary>
        /// <returns> List<PlanContableDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerPlanContableTipoCuenta()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                    select Id,Nombre from fin.T_PlanContableTipoCuenta";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Obtiene la lista de PlanContable dado un Indicio de Nombre, usado para AutoComplete [Cuenta->Id, Descripcion->Nombre]
        /// </summary>
        /// <param name="NombreParcial"></param>
        /// <returns></returns>
        public List<PlanContableFiltroDTO> ObtenerPlanContableAutoComplete(string NombreParcial)
        {
            try
            {
                List<PlanContableFiltroDTO> lista = new List<PlanContableFiltroDTO>();
                string _query = string.Empty;
                _query = "SELECT CONVERT(VARCHAR(50), Cuenta) Id, CONVERT(VARCHAR(50), Cuenta)+'_'+Descripcion Nombre FROM fin.T_PlanContable WHERE Cuenta like '%" + NombreParcial + "%' or CONVERT(VARCHAR(50), Cuenta)+'_'+Descripcion like '%" + NombreParcial + "%' AND Estado=1";
                var listaDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PlanContableFiltroDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonthan Caipo
        /// Fecha: 09/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de Proveedor por su Id
        /// </summary>
        /// <param name="idProveedor"> Id de Expositor </param>
        /// <returns> ProveedorDTO </returns>
        public PlanContable? ObtenerPlanContablePorCuenta(long cuenta)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        Cuenta,
                        Descripcion,
                        Padre,
                        Univel,
                        Cbal,
                        Debe,
                        Haber,
                        IdPlanContableTipoCuenta,
                        Analisis,
                        CentroCosto,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        IdFurTipoSolicitud 
                    FROM 
                        fin.T_PlanContable
                    WHERE 
                        Estado = 1 AND Cuenta = @cuenta";
                var resultado = _dapperRepository.FirstOrDefault(query, new { cuenta });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<PlanContable>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PCR-OPCPC-001@Error en ObtenerPlanContablePorCuenta() {ex.Message}", ex);
            }
        }


        public bool ActualizarRubroPlanContable(long Padre, string Usuario, int? IdFurTipoSolicitud=0)
        {
            try
            {
                var respuesta = false;
                var resultado = _dapperRepository.QuerySPDapper("fin.SP_ActualizarRubroPlanContable", new
                {
                    CuentaPadre = Padre,
                    IdFurTipoSolicitud = IdFurTipoSolicitud == 0 ? null : IdFurTipoSolicitud,
                    Usuario = Usuario
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = true;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

    }
}
