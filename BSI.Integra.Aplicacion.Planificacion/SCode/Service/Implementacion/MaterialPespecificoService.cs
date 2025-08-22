using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.DTO;
using System.Transactions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Globalization;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using System.Web.Mvc;
using BSI.Integra.Repositorio.Repository.Implementation;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion
{
    /// Service: MaterialPespecificoService
    /// Autor: Jonathan Caipo
    /// Fecha: 03/07/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_MaterialPespecifico
    /// </summary>
    public class MaterialPespecificoService : IMaterialPespecificoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MaterialPespecificoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TMaterialPespecifico, MaterialPespecifico>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TMaterialPespecifico, MaterialPespecificoDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<MaterialPespecifico, MaterialPespecificoDTO>(MemberList.None).ReverseMap();
                }
                );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de todos los filtros
        /// </summary>
        /// <returns> DTO - MaterialPespecificoDTO </returns>
        public MaterialPespecificoCombosDTO ObtenerCombos()
        {
            try
            {
                var area = _unitOfWork.AreaCapacitacionRepository.ObtenerFiltro();
                var subArea = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro();
                var programaGeneral = _unitOfWork.PGeneralRepository.ObtenerFiltroPorTipo(false);
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerFiltroPorTipo(false);
                var pEspecificoCurso = _unitOfWork.PEspecificoRepository.ObtenerPEspecificoGruposPorPEspecificoPadre();
                var grupos = _unitOfWork.PEspecificoSesionRepository.ObtenerGruposProgramaEspecificoFiltro();
                var estadoPespecifico = _unitOfWork.EstadoPespecificoRepository.ObtenerCombo();
                var ciudadBs = _unitOfWork.RegionCiudadRepository.ObtenerCiudadBs();
                var modalidades = _unitOfWork.ModalidadCursoRepository.ObtenerCombo();
                var listaMaterialEstado = _unitOfWork.MaterialEstadoRepository.ObtenerCombo();
                var listaMaterialTipo = _unitOfWork.MaterialTipoRepository.ObtenerCombo();
                var listaMaterialVersion = _unitOfWork.MaterialVersionRepository.ObtenerCombo();



                MaterialPespecificoCombosDTO dto = new MaterialPespecificoCombosDTO()
                {
                    ListaArea = area,
                    ListaSubArea = subArea,
                    ListaProgramaGeneral = programaGeneral,
                    ListaProgramaEspecifico = programaEspecifico,
                    ListaPEspecificoCurso = pEspecificoCurso,
                    ListaGrupo = grupos,
                    ListaEstadoPEspecifico = estadoPespecifico,
                    ListaCiudadBS = ciudadBs,
                    ListaModalidad = modalidades,
                    ListaMaterialEstado = listaMaterialEstado,
                    ListaMaterialTipo = listaMaterialTipo,
                    ListaMaterialVersion = listaMaterialVersion,

                };
                return dto;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los materiales por grupo de programa especifico a revisar
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> Lista DTO - List<ResultadoMaterialPEspecificoDetalleDTO> </returns>
        public IEnumerable<ResultadoMaterialPEspecificoDetalleDTO> ObtenerMateriales(FiltroMaterialDTO dto)
        {
            try
            {
                return _unitOfWork.MaterialPespecificoRepository.ObtenerMateriales(dto);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la tabla de T_MaterialPespecificoDetalle el campo IdMaterialEstado
        /// </summary>
        /// <param name="idMaterialPespecificoDetalle"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool AprobarMaterialVersion(int idMaterialPespecificoDetalle, string usuario)
        {
            try
            {
                var materialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(idMaterialPespecificoDetalle);
                if (materialPespecificoDetalle != null)
                {
                    materialPespecificoDetalle.IdMaterialEstado = 3; //aprobado
                    materialPespecificoDetalle.UsuarioAprobacion = usuario;
                    materialPespecificoDetalle.FechaAprobacion = DateTime.Now;
                    materialPespecificoDetalle.UsuarioModificacion = usuario;
                    materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                    _unitOfWork.MaterialPespecificoDetalleRepository.Update(materialPespecificoDetalle);
                    _unitOfWork.Commit();
                }
                else
                {
                    throw new BadRequestException("Version de material no existente");
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la tabla de T_MaterialPespecificoDetalle el campo de IdMaterialEstado
        /// </summary>
        /// <param name="idMaterialPespecificoDetalle"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool DesaprobarMaterialVersion(int idMaterialPespecificoDetalle, string usuario)
        {
            try
            {
                var materialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(idMaterialPespecificoDetalle);
                if (materialPespecificoDetalle != null)
                {
                    materialPespecificoDetalle.IdMaterialEstado = 4; //observado
                    materialPespecificoDetalle.UsuarioModificacion = usuario;
                    materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                    _unitOfWork.MaterialPespecificoDetalleRepository.Update(materialPespecificoDetalle);
                    _unitOfWork.Commit();
                }
                else
                {
                    throw new BadRequestException("Version de material no existente");
                }
                return true;
            }
            catch
            {
                throw;
            }

        }
        /// Autor: Edmundo A. Llaza M.
        /// Fecha: 24/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialPespecifico.
        /// </summary>
        /// <returns> List<MaterialPespecificoDTO> </returns>
        public List<MaterialPespecificoDTO> ObtenerPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                var respuesta = _unitOfWork.MaterialPespecificoRepository.ObtenerPorIdPEspecifico(idPEspecifico);
                if (respuesta.Count() >= 1)
                {
                    return _mapper.Map<List<MaterialPespecificoDTO>>(respuesta);
                }
                else
                {
                    return new List<MaterialPespecificoDTO>();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-02
        /// <summary>
        /// Obtiene el combo para material
        /// </summary>
        /// <returns>ComboMaterialPespecificoDTO</returns>
        public ComboMaterialPespecificoDTO ObtenerComboMaterial()
        {
            var comboMaterialPespecifico = new ComboMaterialPespecificoDTO();
            comboMaterialPespecifico.ObtenerCiudadBs = _unitOfWork.RegionCiudadRepository.ObtenerCiudadBs().ToList();
            comboMaterialPespecifico.ObtenerComboEstado = _unitOfWork.EstadoPespecificoRepository.ObtenerCombo().ToList();
            comboMaterialPespecifico.ObtenerComboModalidad = _unitOfWork.ModalidadCursoRepository.ObtenerCombo().ToList();
            comboMaterialPespecifico.ObtenerFiltroArea = _unitOfWork.AreaCapacitacionRepository.ObtenerFiltro().ToList();
            comboMaterialPespecifico.ObtenerFiltroSubArea = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro().ToList();
            comboMaterialPespecifico.ObtenerProgramaGeneralPadre = _unitOfWork.PGeneralRepository.ObtenerProgramaGeneralPadre(null);
            comboMaterialPespecifico.ObtenerProgramasEspecificosPadres = _unitOfWork.PEspecificoRepository.ObtenerProgramasEspecificosPadres(null);
            comboMaterialPespecifico.ObtenerCentroCostoPadres = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoPadres(null);
            comboMaterialPespecifico.ObtenerComboMaterial = _unitOfWork.MaterialTipoRepository.ObtenerCombo().ToList();
            return comboMaterialPespecifico;
        }
        /// <summary>
        /// Obtiene el proximo grupo de edicion
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Entero</returns>
        public int ObtenerProximoGrupoEdicion(int idPEspecifico)
        {
            try
            {
                return _unitOfWork.MaterialPespecificoRepository.ObtenerMaximoGrupoEdicion(idPEspecifico) + 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una insercion 
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> Insertar(MaterialPespecificoDTO materialPespecificoDTO, string usuario)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (!_unitOfWork.MaterialTipoRepository.Exist(materialPespecificoDTO.IdMaterialTipo))
                        throw new Exception("Tipo de material no existente!");

                    if (materialPespecificoDTO.GrupoEdicion == -1)
                    {
                        materialPespecificoDTO.GrupoEdicion = ObtenerProximoGrupoEdicion(materialPespecificoDTO.IdPespecifico);
                    }

                    var materialPEspecifico = new MaterialPespecifico()
                    {
                        IdMaterialTipo = materialPespecificoDTO.IdMaterialTipo,
                        IdPespecifico = materialPespecificoDTO.IdPespecifico,
                        Grupo = materialPespecificoDTO.Grupo,
                        GrupoEdicion = materialPespecificoDTO.GrupoEdicion,
                        GrupoEdicionOrden = materialPespecificoDTO.GrupoEdicionOrden,
                        IdFur = null,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };

                    //IdMaterialTipo = materialPespecificoDTO.IdMaterialTipo,
                    //Insertamos todas las versiones del tipo de material
                    var idMaterialEstado_PorEditar = 1;
                    var listaMaterialAsociacionVersion = _unitOfWork.MaterialAsociacionVersionRepository.ObtenerPorIdMaterialTipo(materialPespecificoDTO.IdMaterialTipo);

                    if (listaMaterialAsociacionVersion.Count() >= 1)
                    {
                        materialPEspecifico.MaterialPespecificoDetalles = new List<MaterialPespecificoDetalle>();
                        MaterialPespecificoDetalle materialPespecificoDetalle;
                        foreach (var item in listaMaterialAsociacionVersion)
                        {
                            materialPespecificoDetalle = new MaterialPespecificoDetalle()
                            {
                                IdMaterialEstado = idMaterialEstado_PorEditar,
                                ComentarioSubida = "",
                                UrlArchivo = "",
                                FechaSubida = null,
                                IdFur = null,
                                DireccionEntrega = "",
                                FechaEntrega = null,
                                NombreArchivo = "",
                                IdMaterialVersion = item.IdMaterialVersion,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                            materialPEspecifico.MaterialPespecificoDetalles.Add(materialPespecificoDetalle);
                        }
                    }
                    var nuevoDato = _unitOfWork.MaterialPespecificoRepository.Add(materialPEspecifico);
                    _unitOfWork.Commit();
                    materialPEspecifico.Id = nuevoDato.Id;
                    var listaMaterialAsociacionAccion = _unitOfWork.MaterialAsociacionAccionRepository.ObtenerPorIdMaterialTipo(materialPespecificoDTO.IdMaterialTipo);
                    var listaMaterialAsociacionCriterioVerificacion = _unitOfWork.MaterialAsociacionCriterioVerificacionRepository.ObtenerPorIdMaterialTipo(materialPespecificoDTO.IdMaterialTipo).ToList();
                    var materialVersionAlumno = listaMaterialAsociacionVersion.Where(x => x.IdMaterialVersion == 2).FirstOrDefault();
                    var materialAccionProveedor = listaMaterialAsociacionAccion.Where(x => x.IdMaterialAccion == 2).FirstOrDefault();
                    if (materialVersionAlumno != null && materialAccionProveedor != null && listaMaterialAsociacionCriterioVerificacion.Count > 0)
                    {
                        var listaMaterialDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerDetalleMaterialPEspecifico(materialPEspecifico.Id, materialAccionProveedor.IdMaterialAccion, materialVersionAlumno.IdMaterialVersion);
                        List<MaterialCriterioVerificacionDetalle> materialCriterioVerificacionDetalles = new List<MaterialCriterioVerificacionDetalle>();
                        foreach (var material in listaMaterialDetalle)
                        {
                            foreach (var item in listaMaterialAsociacionCriterioVerificacion)
                            {
                                MaterialCriterioVerificacionDetalle materialCriterioVerificacionDetalle = new MaterialCriterioVerificacionDetalle()
                                {
                                    IdMaterialPespecificoDetalle = material.IdMaterialPEspecificoDetalle,
                                    IdMaterialCriterioVerificacion = item.IdMaterialCriterioVerificacion,
                                    EsAprobado = false,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                materialCriterioVerificacionDetalles.Add(materialCriterioVerificacionDetalle);
                            }
                        }
                        _unitOfWork.MaterialCriterioVerificacionDetalleRepository.Add(materialCriterioVerificacionDetalles);
                        _unitOfWork.Commit();
                    }
                    var respuesta = _unitOfWork.MaterialPespecificoRepository.ObtenerGrupoEdicionDisponible(materialPespecificoDTO.IdPespecifico).ToList();
                    scope.Complete();
                    return respuesta;
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-02
        /// <summary>
        /// Actualiza MaterialPespecifico
        /// </summary>
        /// <param name="materialPespecificoDTO"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<ComboDTO> Actualizar(MaterialPespecificoDTO materialPespecificoDTO, string usuario)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (!_unitOfWork.MaterialPespecificoRepository.Exist(materialPespecificoDTO.Id))
                    {
                        throw new Exception("Material por programa especifico no existente!");
                    }
                    if (!_unitOfWork.MaterialTipoRepository.Exist(materialPespecificoDTO.IdMaterialTipo))
                    {
                        throw new Exception("Tipo de material no existente!");
                    }

                    if (materialPespecificoDTO.GrupoEdicion == -1)
                    {
                        materialPespecificoDTO.GrupoEdicion = ObtenerProximoGrupoEdicion(materialPespecificoDTO.IdPespecifico);
                    }

                    var materialPEspecifico = _unitOfWork.MaterialPespecificoRepository.ObtenerPorId(materialPespecificoDTO.Id);
                    materialPEspecifico.IdMaterialTipo = materialPespecificoDTO.IdMaterialTipo;
                    materialPEspecifico.Grupo = materialPespecificoDTO.Grupo;
                    materialPEspecifico.GrupoEdicion = materialPespecificoDTO.GrupoEdicion;
                    materialPEspecifico.GrupoEdicionOrden = materialPespecificoDTO.GrupoEdicionOrden;
                    materialPEspecifico.UsuarioModificacion = usuario;
                    materialPEspecifico.FechaModificacion = DateTime.Now;

                    var prb = _unitOfWork.MaterialPespecificoRepository.Update(materialPEspecifico);
                    _unitOfWork.Commit();
                    var listaGrupoEdicion = _unitOfWork.MaterialPespecificoRepository.ObtenerGrupoEdicionDisponible(materialPespecificoDTO.IdPespecifico);
                    scope.Complete();
                    return listaGrupoEdicion.ToList();
                }
                catch { throw; }
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-03
        /// <summary>
        /// Elimina de materialpespecifico
        /// </summary>
        /// <param name="materialPespecificoDTO"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (!_unitOfWork.MaterialPespecificoRepository.Exist(id))
                {
                    throw new Exception("Material por sesion no existente");
                }
                var busqueda = _unitOfWork.MaterialPespecificoRepository.ObtenerPorId(id);
                if (busqueda != null && busqueda.Id > 0)
                {
                    _unitOfWork.MaterialPespecificoRepository.Delete(busqueda.Id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-04
        /// <summary>
        /// Obtendra los materiales que estan con estado aprobado, todos los materiales del grupo de edicion deben estar aprobados
        /// </summary>
        /// <param name="filtroMaterial"></param>
        /// <returns></returns>
        public async Task<List<ResultadoMaterialPEspecificoDetalleDTO>> ObtenerMaterialesGestionEnvioAsync(FiltroMaterialDTO filtroMaterial)

        {
            {
                try
                {
                    var lista = await _unitOfWork.MaterialPespecificoRepository.ObtenerMaterialesGestionEnvioAsync(filtroMaterial);
                    var listatareas = new List<Task<List<ComboDTO>>>();//lista tareas
                    foreach (var item in lista)
                    {
                        var task = _unitOfWork.MaterialPespecificoRepository.ObtenerMaterialAccionPorMaterialTipo(item.IdMaterialTipo);
                        listatareas.Add(task);
                    }



                    for (int i = 0; i < lista.Count(); i++)
                    {
                        lista[i].ListaMaterialAccion = await listatareas[i];
                    }
                    return lista;


                }
                catch { throw; }
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-05
        /// <summary>
        /// Arma correo para enviar a alumnos por version material
        /// </summary>D
        /// <param name="idMaterialPEspecificoDetalle"></param>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        public bool NotificarMaterialVersionAlumnoPorCorreo(List<int> idMaterialPEspecificoDetalle, string nombreUsuario)
        {
            try
            {
                var materialPEspecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(idMaterialPEspecificoDetalle.FirstOrDefault());
                var materialPEspecifico = _unitOfWork.MaterialPespecificoRepository.ObtenerPorId(materialPEspecificoDetalle.IdMaterialPespecifico);
                ///Envia por correo para todos los alumnos
                var listaOportunidades = new List<ValorEnteroDTO>();
                listaOportunidades = _unitOfWork.MatriculaCabeceraRepository.ObtenerAlumnosMatriculaProgramaEspecificoGrupo(materialPEspecifico.IdPespecifico, materialPEspecifico.Grupo);
                var idPlantilla = 117; ///Envio material digital para modalidad presencial
                var listaGmailCorreo = new List<GmailCorreo>();
                foreach (var item in listaOportunidades)
                {
                    var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaDTO();
                    {
                        reemplazoEtiquetaPlantilla.IdOportunidad = item.Valor;
                        reemplazoEtiquetaPlantilla.IdPlantilla = idPlantilla;
                        reemplazoEtiquetaPlantilla.IdPEspecifico = materialPEspecifico.Id;
                    }
                    var _etiqueta = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                    var etiqueta = _etiqueta.ReemplazarEtiquetas(reemplazoEtiquetaPlantilla);
                    var emailCalculado = etiqueta.EmailReemplazado;

                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        //"lhuallpa@bsginstitute.com",
                        //"gchirinos@bsginstitute.com",
                        //"aarcana@bsginstitute.com"
                        "mramirez@bsginstitute.com"
                    };
                    var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(item.Valor);
                    var personal = _unitOfWork.PersonalRepository.ObtenerPorId(oportunidad.IdPersonalAsignado.Value);
                    var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno.Value);

                    List<string> correosPersonalizados = new List<string>
                    {
                        //alumno.Email1


                    };

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO()
                    {
                        Sender = personal.Email,
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = "",
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailService();
                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    var gmailCorreo = new GmailCorreo
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = personal.Email,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = personal.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = nombreUsuario,
                        UsuarioModificacion = nombreUsuario,
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };
                    listaGmailCorreo.Add(gmailCorreo);
                    _unitOfWork.Commit();// agrado commit
                }
                foreach (var item in idMaterialPEspecificoDetalle)
                {
                    var materialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(item);
                    materialPespecificoDetalle.IdMaterialEstado = 5;
                    materialPespecificoDetalle.UsuarioEnvio = nombreUsuario;
                    materialPespecificoDetalle.UsuarioModificacion = nombreUsuario;
                    materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                    if (materialPespecificoDetalle.Id != 0 || materialPespecificoDetalle.Id != null)
                    {
                        _unitOfWork.MaterialPespecificoDetalleRepository.Update(materialPespecificoDetalle);
                        _unitOfWork.Commit();// agregado commit
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                _unitOfWork.GmailCorreoRepository.Add(listaGmailCorreo);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"No se encontro el id {ex.Message}");
            }
        }



        /// Autor: Edmundo llaza
        /// Fecha: 2023-09-13
        /// <summary>
        /// Notifica a proveedor con impresion de material
        /// </summary>
        /// <param name="idMaterialPEspecifico"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public bool NotificarMaterialVersionAlumnoImpresoPorCorreoAProveedor(int idMaterialPEspecifico, string usuario)
        {
            try
            {
                if (!_unitOfWork.MaterialPespecificoRepository.Exist(idMaterialPEspecifico))
                {
                    throw new BadRequestException($"No se encontro la entidad con el id");
                }

                var materialPespecifico = _unitOfWork.MaterialPespecificoRepository.ObtenerPorId(idMaterialPEspecifico);
                var listaMaterialPEspecifoMismoGrupoEdicion = _unitOfWork.MaterialPespecificoRepository.ObtenerPorPEspecificoGrupo(materialPespecifico.IdPespecifico, materialPespecifico.GrupoEdicion).ToList();
                var listaIdMaterialPEspecifoMismoGrupoEdicion = listaMaterialPEspecifoMismoGrupoEdicion.Select(x => x.Id).ToList();
                var listaIdMaterialVersion = new List<int>() {
                    2,//Material para alumno - impreso
                };
                var listaIdMaterialEstado = new List<int>() {
                    3,//aprobado
                };

                var listaMaterialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorMaterial(listaIdMaterialPEspecifoMismoGrupoEdicion, listaIdMaterialVersion, listaIdMaterialEstado).FirstOrDefault();
                var idPlantilla = 1222;
                var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaDTO();
                {

                    reemplazoEtiquetaPlantilla.IdPlantilla = idPlantilla;
                    reemplazoEtiquetaPlantilla.IdPEspecifico = materialPespecifico.Grupo;
                    reemplazoEtiquetaPlantilla.IdMaterialPEspecificoDetalle = listaMaterialPespecificoDetalle.Id;
                }
                var reemplazoEtiqueta = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                var etiqueta = reemplazoEtiqueta.ReemplazarEtiquetasProveedor(reemplazoEtiquetaPlantilla);
                var emailCalculado = etiqueta.EmailReemplazado;
                List<string> correosPersonalizadosCopiaOculta = new List<string>
                {
                    "lhuallpa@bsginstitute.com"
                };
                var fur = _unitOfWork.FurRepository.ObtenerPorId(listaMaterialPespecificoDetalle.IdFur.Value);
                var proveedor = _unitOfWork.ProveedorRepository.ObtenerPorId(fur.IdProveedor.Value);
                List<string> correosPersonalizados = new List<string>
                {
                    proveedor.Email
                };

                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO()
                {
                    Sender = "modpru@bsginstitute.com",
                    //Sender = personal.Email,
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = emailCalculado.Asunto,
                    Message = emailCalculado.CuerpoHTML,
                    Cc = "",
                    Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                    AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                };
                var mailServie = new TMK_MailService();
                mailServie.SetData(mailDataPersonalizado);
                mailServie.SendMessageTask();

                var materialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(listaMaterialPespecificoDetalle.Id);
                materialPespecificoDetalle.IdMaterialEstado = 5;//enviado
                materialPespecificoDetalle.UsuarioEnvio = usuario;
                materialPespecificoDetalle.FechaEnvio = DateTime.Now;
                materialPespecificoDetalle.UsuarioModificacion = usuario;
                materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                if (materialPespecificoDetalle.Id != null || materialPespecificoDetalle.Id >= 0)
                {
                    _unitOfWork.MaterialPespecificoDetalleRepository.Update(materialPespecificoDetalle);
                }
                return true;
            }
            catch (Exception ex) { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Obtiene de T_MaterialPespecificoDetalle filtrado por mateial pespecifico version y estado
        /// </summary>
        /// <param name="idMaterialPEspecifico"></param>
        /// <returns></returns>
        public List<MaterialPespecificoDetalle> ObtenerMaterialesAlumnoDigital(int idMaterialPEspecifico)
        {
            try
            {
                var listaIdMaterialVersion = new List<int>() {
                    1,//enviar correo alumno
                };

                var listaIdMaterialEstado = new List<int>() {
                    3,//aprobado
                };
                var materialPEspecifico = _unitOfWork.MaterialPespecificoRepository.ObtenerPorId(idMaterialPEspecifico);
                var listaMaterialPEspecifico = _unitOfWork.MaterialPespecificoRepository.ObtenerPorPEspecificoGrupo(materialPEspecifico.IdPespecifico, materialPEspecifico.GrupoEdicion);
                var listaIdMaterialPEspecifico = listaMaterialPEspecifico.Select(x => x.Id).ToList();
                var lista = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorMaterial(listaIdMaterialPEspecifico, listaIdMaterialVersion, listaIdMaterialEstado);
                foreach (var item in lista)
                {
                    item.IdMaterialTipo = listaMaterialPEspecifico.Where(x => item.IdMaterialPespecifico == x.Id).FirstOrDefault().IdMaterialTipo;
                }
                return lista;
            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Obtiene valores de T_Fur por el campo PEspecifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public List<PEspecificoFurDetalleDTO> ObtenerFursAsociadosPorIdPEspecifico(int idPEspecifico)
        {
            try
            {
                if (!_unitOfWork.PEspecificoRepository.Exist(idPEspecifico))
                {
                    throw new BadRequestException($"No se encontro el id ");
                }
                var asociados = _unitOfWork.MaterialPespecificoRepository.ObtenerFursAsociadosPorIdPEspecifico(idPEspecifico);
                return asociados;
            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Actualiza T_Fur y T_MaterialPEspecificoDetalle
        /// </summary>
        /// <param name="materialPespecificoDetalle"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool AsociarActualizarFur(AsociarActualizarFurMaterialVersionDTO materialPespecificoDetalle, string usuario)
        {
            try
            {
                var materialPEspecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(materialPespecificoDetalle.IdMaterialPEspecificoDetalle);
                if (_unitOfWork.MaterialPespecificoDetalleRepository.Exist(materialPespecificoDetalle.IdMaterialPEspecificoDetalle))
                {
                    materialPEspecificoDetalle.IdFur = materialPespecificoDetalle.IdFur;
                    materialPEspecificoDetalle.FechaEntrega = materialPespecificoDetalle.FechaEntrega;
                    materialPEspecificoDetalle.DireccionEntrega = materialPespecificoDetalle.DireccionEntrega;
                    materialPEspecificoDetalle.FechaModificacion = DateTime.Now;
                    materialPEspecificoDetalle.UsuarioModificacion = usuario;
                    _unitOfWork.MaterialPespecificoDetalleRepository.Update(materialPEspecificoDetalle);

                    var detalleFur = _unitOfWork.HistoricoProductoProveedorRepository.ObtenerDetalleFUR(materialPespecificoDetalle.IdProducto.Value, materialPespecificoDetalle.IdProveedor.Value);
                    var semana = obtenerNumeroSemana(materialPespecificoDetalle.FechaEntrega.Value);
                    var producto = _unitOfWork.ProductoRepository.ObtenerPorId(materialPespecificoDetalle.IdProducto.Value);
                    var planContable = _unitOfWork.PlanContableRepository.ObtenerPlanContablePorCuenta(long.Parse(producto.CuentaGeneral));

                    var fur = _unitOfWork.FurRepository.ObtenerPorId(materialPespecificoDetalle.IdFur.Value);
                    fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
                    fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                    fur.IdMonedaPagoReal = detalleFur.IdMoneda;
                    fur.NumeroCuenta = detalleFur.NumeroCuenta;
                    fur.Descripcion = planContable.Descripcion;
                    fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
                    fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * materialPespecificoDetalle.Cantidad.Value);
                    fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                    fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * materialPespecificoDetalle.Cantidad.Value);
                    fur.IdProveedor = materialPespecificoDetalle.IdProveedor;
                    fur.Cuenta = detalleFur.CuentaDescripcion;
                    fur.IdProducto = materialPespecificoDetalle.IdProducto;
                    fur.NumeroSemana = semana;
                    fur.Cantidad = materialPespecificoDetalle.Cantidad.Value;
                    fur.Descripcion = detalleFur.Descripcion;
                    fur.UsuarioModificacion = usuario;
                    fur.FechaModificacion = DateTime.Now;
                    fur.IdMonedaProveedor = detalleFur.IdMoneda;
                    fur.IdFurFaseAprobacion1 = 1;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * materialPespecificoDetalle.Cantidad;
                    fur.PagoDolares = detalleFur.PrecioDolares * materialPespecificoDetalle.Cantidad;
                    fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    _unitOfWork.FurRepository.Update(fur);
                    _unitOfWork.Commit();
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Obtiene Fur por MaterialPEspecifico
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public AsociarActualizarFurMaterialVersionDTO ObtenerFurAsociadoPorIdPEspecificoDetalle(int idMaterialPEspecificoDetalle)
        {
            try
            {
                return _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerFurAsociadoPorIdPEspecificoDetalle(idMaterialPEspecificoDetalle);
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"No se encontro el id {ex.Message}");
            }
        }

        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Calcula el numero de semanas
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private int obtenerNumeroSemana(DateTime fecha)
        {
            var d = fecha;
            CultureInfo cul = CultureInfo.CurrentCulture;

            var firstDayWeek = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int weekNum = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            return weekNum;
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 2023-11-07
        /// <summary>
        /// Obtiene los criterios de programa especifico
        /// </summary>
        public async Task<EntregaMaterialDTO> ObtenerCriteriosMaterialesProgramaEspecifico(FiltroMaterialDTO Filtro)

        {

            try
            {
                var _repMaterialPEspecifico = new MaterialPespecificoService(_unitOfWork);
                var listaMateriales = await _unitOfWork.MaterialPespecificoRepository.ObtenerMaterialesRegistroEntrega(Filtro);
                List<CriterioVerificacionColumnasDTO> colCriterios = new List<CriterioVerificacionColumnasDTO>();
                foreach (var item in listaMateriales)
                {

                    var listaCriterios = await _unitOfWork.MaterialAsociacionCriterioVerificacionRepository.ObtenerCriteriosVerificacionPorMaterialDetalleAsync(item.IdMaterialPEspecificoDetalle);
                    item.CriteriosVerificacion = listaCriterios;
                    colCriterios.AddRange(listaCriterios.Select(x => new CriterioVerificacionColumnasDTO { IdMaterialCriterioVerificacion = x.IdMaterialCriterioVerificacion, MaterialCriterioVerificacion = x.MaterialCriterioVerificacion }).ToList());
                }
                var columnas = colCriterios.GroupBy(x => new { x.IdMaterialCriterioVerificacion, x.MaterialCriterioVerificacion }).Select(x => new CriterioVerificacionColumnasDTO
                {
                    IdMaterialCriterioVerificacion = x.Key.IdMaterialCriterioVerificacion,
                    MaterialCriterioVerificacion = x.Key.MaterialCriterioVerificacion
                }).ToList();

                var respuesta = new EntregaMaterialDTO();
                respuesta.listaMateriales = listaMateriales;
                respuesta.columnas = columnas;

                return respuesta;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaIdMaterialPEspecificoDetalle"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool NotificarListaMaterialVersionAlumnoPorCorreo(List<int> listaIdMaterialPEspecificoDetalle, string usuario)
        {
            try
            {
                var materialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorIds(listaIdMaterialPEspecificoDetalle).FirstOrDefault();//nos traemos el 1ro
                var materialPespecifico = _unitOfWork.MaterialPespecificoRepository.ObtenerPorId(materialPespecificoDetalle.IdMaterialPespecifico);
                //logica enviar por correo a todos los alumnos 
                var listaOportunidades = new List<ValorEnteroDTO>();

                var listaOPortunidad = _unitOfWork.MatriculaCabeceraRepository.ObtenerAlumnosMatriculaProgramaEspecificoGrupo(materialPespecifico.IdPespecifico, materialPespecifico.Grupo);

                //1117    Envío de Material Digital para la Modalidad Presencial
                var idPlantilla = 1117;
                var listaGmailCorreo = new List<GmailCorreo>();

                foreach (var item in listaOportunidades)
                {
                    var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaDTO()
                    {
                        IdOportunidad = item.Valor,
                        IdPlantilla = idPlantilla,
                        IdPEspecifico = 0,//validar
                        Grupo = 0,//validar
                        IdMaterialPEspecificoDetalle = 0,
                        ListaIdMaterialPEspecificoDetalle = listaIdMaterialPEspecificoDetalle//en el caso que envie una lista de materiales a enviar
                    };

                    var _etiqueta = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                    var etiqueta = _etiqueta.ReemplazarEtiquetas(_reemplazoEtiquetaPlantilla);

                    var emailCalculado = etiqueta.EmailReemplazado;
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "lhuallpa@bsginstitute.com",
                        "gchirinos@bsginstitute.com",
                        "aarcana@bsginstitute.com"

                    };

                    var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(item.Valor);
                    var personal = _unitOfWork.PersonalRepository.ObtenerPorId(oportunidad.IdPersonalAsignado.Value);
                    var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno.Value);


                    List<string> correosPersonalizados = new List<string>
                    {
                        alumno.Email1


                    };
                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = personal.Email,
                        //Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = "",
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailService();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica envio
                    var gmailCorreo = new GmailCorreo
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = personal.Email,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = personal.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };
                    listaGmailCorreo.Add(gmailCorreo);
                }

                foreach (var item in listaIdMaterialPEspecificoDetalle)
                {
                    var _materialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(item);//nos traemos el 1ro

                    if (_materialPespecificoDetalle != null)
                    {
                        _materialPespecificoDetalle.IdMaterialEstado = 5;//enviado
                        _materialPespecificoDetalle.UsuarioEnvio = usuario;
                        _materialPespecificoDetalle.FechaEnvio = DateTime.Now;
                        _materialPespecificoDetalle.UsuarioModificacion = usuario;
                        _materialPespecificoDetalle.FechaModificacion = DateTime.Now;

                        _unitOfWork.MaterialPespecificoDetalleRepository.Update(_materialPespecificoDetalle);
                        _unitOfWork.Commit();
                    }
                }

                _unitOfWork.GmailCorreoRepository.Add(listaGmailCorreo);
                _unitOfWork.Commit();
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 2023-11-07
        /// <summary>
        /// Aprueba y rechaza los registros entrega
        /// </summary>
        public async Task<bool> AprobarRechazarRegistroEntrega(AprobarRechazarRegistroEntregaMaterialDTO Registro)
        {
            try
            {
                var _repMaterialPEspecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository;
                var listaCriterios = await _unitOfWork.MaterialAsociacionCriterioVerificacionRepository.ObtenerCriteriosVerificacionPorMaterialDetalleAsync(Registro.IdMaterialPEspecificoDetalle);
                var _repMaterialCriterioVerificacionDetalle = _unitOfWork.MaterialCriterioVerificacionDetalleRepository;

                foreach (var item in Registro.ClaveValor)
                {
                    var criterio = listaCriterios.FirstOrDefault(x => x.IdMaterialCriterioVerificacion == item.Key);

                    if (criterio != null)
                    {
                        var criterioVerificacion = _repMaterialCriterioVerificacionDetalle.FirstById(criterio.Id);

                        if (Registro.EstadoRegistroMaterial == 2)
                        {
                            criterioVerificacion.EsAprobado = false;
                        }
                        else
                        {
                            criterioVerificacion.EsAprobado = item.Value;
                        }

                        criterioVerificacion.UsuarioModificacion = Registro.Usuario;
                        criterioVerificacion.FechaModificacion = DateTime.Now;

                        _repMaterialCriterioVerificacionDetalle.Update(criterioVerificacion);
                        _unitOfWork.Commit();
                    }
                }

                var materialPEspecificoDetalle = _repMaterialPEspecificoDetalle.FirstById(Registro.IdMaterialPEspecificoDetalle);

                if (materialPEspecificoDetalle != null)
                {
                    materialPEspecificoDetalle.IdEstadoRegistroMaterial = Registro.EstadoRegistroMaterial;
                    materialPEspecificoDetalle.UsuarioModificacion = Registro.Usuario;
                    materialPEspecificoDetalle.FechaModificacion = DateTime.Now;

                    _repMaterialPEspecificoDetalle.Update(materialPEspecificoDetalle);
                    _unitOfWork.Commit();

                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 2023-11-07
        /// <summary>
        /// Actualiza furs
        /// </summary>
        public bool AsociarActualizarFures(AsociarActualizarFurMaterialVersionDTO materialPespecificoDetalle, string usuario)
        {
            try
            {
                var materialPEspecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository.ObtenerPorId(materialPespecificoDetalle.IdMaterialPEspecificoDetalle);
                if (_unitOfWork.MaterialPespecificoDetalleRepository.Exist(materialPespecificoDetalle.IdMaterialPEspecificoDetalle))
                {
                    materialPEspecificoDetalle.IdFur = materialPespecificoDetalle.IdFur;
                    materialPEspecificoDetalle.FechaEntrega = materialPespecificoDetalle.FechaEntrega;
                    materialPEspecificoDetalle.DireccionEntrega = materialPespecificoDetalle.DireccionEntrega;
                    materialPEspecificoDetalle.FechaModificacion = DateTime.Now;
                    materialPEspecificoDetalle.UsuarioModificacion = usuario;
                    _unitOfWork.MaterialPespecificoDetalleRepository.Update(materialPEspecificoDetalle);

                    var detalleFur = _unitOfWork.HistoricoProductoProveedorRepository.ObtenerDetalleFUR(materialPespecificoDetalle.IdProducto.Value, materialPespecificoDetalle.IdProveedor.Value);
                    var semana = obtenerNumeroSemana(materialPespecificoDetalle.FechaEntrega.Value);
                    var producto = _unitOfWork.ProductoRepository.ObtenerPorId(materialPespecificoDetalle.IdProducto.Value);
                    var planContable = _unitOfWork.PlanContableRepository.ObtenerPlanContablePorCuenta(long.Parse(producto.CuentaGeneral));

                    var fur = _unitOfWork.FurRepository.ObtenerPorId(materialPespecificoDetalle.IdFur.Value);
                    fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
                    fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                    fur.IdMonedaPagoReal = detalleFur.IdMoneda;
                    fur.NumeroCuenta = detalleFur.NumeroCuenta;
                    fur.Descripcion = planContable.Descripcion;
                    fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
                    fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * materialPespecificoDetalle.Cantidad.Value);
                    fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                    fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * materialPespecificoDetalle.Cantidad.Value);
                    fur.IdProveedor = materialPespecificoDetalle.IdProveedor;
                    fur.Cuenta = detalleFur.CuentaDescripcion;
                    fur.IdProducto = materialPespecificoDetalle.IdProducto;
                    fur.NumeroSemana = semana;
                    fur.Cantidad = materialPespecificoDetalle.Cantidad.Value;
                    fur.Descripcion = detalleFur.Descripcion;
                    fur.UsuarioModificacion = usuario;
                    fur.FechaModificacion = DateTime.Now;
                    fur.IdMonedaProveedor = detalleFur.IdMoneda;
                    fur.IdFurFaseAprobacion1 = 1;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * materialPespecificoDetalle.Cantidad;
                    fur.PagoDolares = detalleFur.PrecioDolares * materialPespecificoDetalle.Cantidad;
                    fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    _unitOfWork.FurRepository.Update(fur);
                    _unitOfWork.Commit();
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {

                throw;
            }




        }



        public bool ActualizarFurRegistroMaterial(FurRegistroMaterialDTO FurMaterial)
        {

            try
            {

                var _repPespecificoSesion = _unitOfWork.PEspecificoSesionRepository;
                var _repHistoricoProductoProveedor = _unitOfWork.HistoricoProductoProveedorRepository;
                var _repFur = _unitOfWork.FurRepository;
                var _repProducto = _unitOfWork.ProductoRepository;
                var _repPlanContable = _unitOfWork.PlanContableRepository;
                var _repMaterialPespecificoDetalle = _unitOfWork.MaterialPespecificoDetalleRepository;
                var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(FurMaterial.IdProducto, FurMaterial.IdProveedor);
                var estado = false;
                var semana = obtenerNumeroSemana(FurMaterial.FechaEntrega);
                var producto = _repProducto.FirstById(FurMaterial.IdProducto);
                var planContable = _repPlanContable.FirstBy(x => x.Cuenta == long.Parse(producto.CuentaGeneral));

                var fur = _repFur.FirstById(FurMaterial.Id);
                fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
                fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                fur.IdMonedaPagoReal = detalleFur.IdMoneda;
                fur.NumeroCuenta = detalleFur.NumeroCuenta;
                fur.Descripcion = planContable.Descripcion;
                fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
                fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * FurMaterial.Cantidad);
                fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * FurMaterial.Cantidad);
                fur.IdProveedor = FurMaterial.IdProveedor;
                fur.Cuenta = detalleFur.CuentaDescripcion;
                fur.IdProducto = FurMaterial.IdProducto;
                fur.NumeroSemana = semana;
                fur.Cantidad = FurMaterial.Cantidad;
                fur.Descripcion = detalleFur.Descripcion;
                fur.UsuarioModificacion = FurMaterial.Usuario;
                fur.FechaModificacion = DateTime.Now;
                fur.IdMonedaProveedor = detalleFur.IdMoneda;
                fur.IdFurFaseAprobacion1 = 1;
                fur.Monto = fur.PrecioTotalMonedaOrigen;
                fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * FurMaterial.Cantidad;
                fur.PagoDolares = detalleFur.PrecioDolares * FurMaterial.Cantidad;
                fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                fur.Monto = fur.PrecioTotalMonedaOrigen;
                estado = _repFur.Update(fur);
                _unitOfWork.Commit();

                var materialDetalle = _repMaterialPespecificoDetalle.FirstById(FurMaterial.IdMaterialPEspecificoDetalle);
                materialDetalle.FechaEntrega = FurMaterial.FechaEntrega;
                materialDetalle.DireccionEntrega = FurMaterial.DireccionEntrega;
                materialDetalle.UsuarioModificacion = FurMaterial.Usuario;
                materialDetalle.FechaModificacion = DateTime.Now;
                _repMaterialPespecificoDetalle.Update(materialDetalle);
                _unitOfWork.Commit();

                return estado;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
