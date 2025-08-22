using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using System.Net;
using System.Security.Claims;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: OportunidadErradoService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OportunidadErrado
    /// </summary>
    public class OportunidadErradoService : IOportunidadErradoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadErradoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOportunidadErrado, OportunidadErrado>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OportunidadErrado Add(OportunidadErrado entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadErradoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadErrado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadErrado Update(OportunidadErrado entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadErradoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadErrado>(modelo);
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
                _unitOfWork.OportunidadErradoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadErrado> Add(List<OportunidadErrado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadErradoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadErrado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadErrado> Update(List<OportunidadErrado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadErradoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadErrado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.OportunidadErradoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public object CorregirDatoOportunidad(AsignacionAutomaticaCompuestoDTO obj, string usuario)
        {

            try
            {
                obj.Usuario = usuario;
                obj.UsuarioModificacion = usuario;
                var servicio = new AsignacionAutomaticaService(_unitOfWork);
                var servicioError = new AsignacionAutomaticaErrorService(_unitOfWork);
                var servicioOportunidad = new OportunidadService(_unitOfWork);
                var servicioCategoriaOrigen = new CategoriaOrigenService(_unitOfWork);

                AsignacionAutomatica AsignacionAutomaticaAntigua = new AsignacionAutomatica();
                AsignacionAutomaticaAntigua = servicio.ObtenerAsignacionAutomaticaPorId(obj.Id);

                AsignacionAutomaticaAntigua.Nombre1 = obj.Nombre1;
                AsignacionAutomaticaAntigua.Nombre2 = obj.Nombre2;
                AsignacionAutomaticaAntigua.ApellidoPaterno = obj.ApellidoPaterno;
                AsignacionAutomaticaAntigua.ApellidoMaterno = obj.ApellidoMaterno;
                AsignacionAutomaticaAntigua.Celular = obj.Celular;
                AsignacionAutomaticaAntigua.Email = obj.Email;
                AsignacionAutomaticaAntigua.IdCentroCosto = obj.IdCentroCosto;
                AsignacionAutomaticaAntigua.NombrePrograma = obj.NombrePrograma;
                AsignacionAutomaticaAntigua.IdAreaFormacion = obj.IdAreaFormacion;
                AsignacionAutomaticaAntigua.IdAreaTrabajo = obj.IdAreaTrabajo;
                AsignacionAutomaticaAntigua.IdIndustria = obj.IdIndustria;
                AsignacionAutomaticaAntigua.IdCargo = obj.IdCargo;
                AsignacionAutomaticaAntigua.IdPais = obj.IdPais;
                AsignacionAutomaticaAntigua.IdCiudad = obj.IdCiudad;
                AsignacionAutomaticaAntigua.FechaModificacion = DateTime.Now;
                AsignacionAutomaticaAntigua.UsuarioModificacion = obj.Usuario;

                servicio.CorregirErroneo(obj);

                var lista_errores = servicio.ValidarV2(obj);
                AsignacionAutomaticaAntigua.IdAlumno = obj.IdAlumno;

                if (lista_errores.Count == 0)
                {
                    //_repAsignacionAutomatica.Update(AsignacionAutomaticaAntigua); 
                    try
                    {
                        OportunidadBoDTO Oportunidad = new OportunidadBoDTO();
                        using (TransactionScope scope = new TransactionScope())
                        {
                            var hoy = DateTime.Now;
                            var cadena = hoy.DayOfWeek;
                            DateTime.Now.ToString("hh:mm:ss");
                            Dictionary<string, string> Dias = new Dictionary<string, string>() {
                                    { "Monday","Lunes"},
                                    { "Tuesday","Martes"},
                                    { "Wednesday","Miercoles"},
                                    { "Thursday","Jueves"},
                                    { "Friday","Viernes"},
                                    { "Saturday","Sabado"},
                                    { "Sunday","Domingo"}
                                };
                            var Horacio = hoy.TimeOfDay;
                            var dia = Dias[cadena.ToString()];
                            var diaDto = _unitOfWork.BloqueHorarioRepository.ObtenerConfiguracion(dia);

                            int idTipoCategoriaOrigen = servicioCategoriaOrigen.ObtenerTipoCategoriaOrigenPorId(AsignacionAutomaticaAntigua.IdCategoriaOrigen == null ? 0 : AsignacionAutomaticaAntigua.IdCategoriaOrigen.Value);

                            var errores = (_unitOfWork.AsignacionAutomaticaErrorRepository.GetBy(w => w.IdAsignacionAutomatica == AsignacionAutomaticaAntigua.Id, w => new { w.Id }).Select(x => x.Id).ToList());

                            servicioError.Delete(errores, obj.Usuario);

                            if (AsignacionAutomaticaAntigua.IdAlumno == null)
                            {
                                Oportunidad.Alumno = new Alumno();
                            }

                            //se agrego el flag-venta-cruzada; 1:proceza; 0:No proceza

                            Oportunidad = servicioOportunidad.GenerarOportunidad(AsignacionAutomaticaAntigua, false, "", idTipoCategoriaOrigen);

                            Oportunidad.Estado = true;
                            Oportunidad.FechaCreacion = DateTime.Now;
                            Oportunidad.FechaModificacion = DateTime.Now;
                            Oportunidad.UsuarioCreacion = obj.Usuario;
                            Oportunidad.UsuarioModificacion = obj.Usuario;
                            if (Oportunidad.Alumno.Id == 0)
                            {
                                servicioOportunidad.CrearOportunidadCrearPersona(ref Oportunidad, false, TipoPersona.Alumno); //se agrego el flag-venta-cruzada
                            }
                            else
                            {
                                servicioOportunidad.CrearOportunidadActualizarPersona(ref Oportunidad, false, TipoPersona.Alumno); //se agrego el flag-venta-cruzada
                            }


                            AsignacionAutomaticaAntigua.Validado = true;
                            AsignacionAutomaticaAntigua.Corregido = true;
                            AsignacionAutomaticaAntigua.IdOportunidad = Oportunidad.Id;
                            AsignacionAutomaticaAntigua.FechaModificacion = DateTime.Now;
                            AsignacionAutomaticaAntigua.UsuarioModificacion = obj.Usuario;
                            servicio.Update(AsignacionAutomaticaAntigua);
                            scope.Complete();
                        }

                        ///15/03/2021
                        ///Calculo nuevo modelo predictivo
                        ///Carlos Crispin Riquelme
                        try
                        {
                            var nuevaProbabilidad = servicioOportunidad.ObtenerProbabilidadModeloPredictivo(Oportunidad.Id);
                        }
                        catch (Exception e)
                        {
                            //throw;
                        }

                        //asignacion automatica
                        try
                        {
                            string URI = "https://integrav4-syncv3.bsginstitute.com/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomaticaAntigua.Id;
                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(URI);
                            }
                        }
                        catch (Exception)
                        {
                        }
                        //MetodoODyOM(Oportunidad.Id);
                        return (Oportunidad.Id);

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Se Actualizo contacto pero NO se creo la OPORTUNIDAD porque tiene Un BNC del tipo lanzamiento"))
                        {
                            AsignacionAutomaticaAntigua.Validado = true;
                            AsignacionAutomaticaAntigua.Corregido = false;
                            AsignacionAutomaticaAntigua.Estado = false;
                            AsignacionAutomaticaAntigua.FechaModificacion = DateTime.Now;
                            AsignacionAutomaticaAntigua.UsuarioModificacion = obj.Usuario;
                            servicio.Update(AsignacionAutomaticaAntigua);

                            try
                            {
                                string URI = "https://integrav4-syncv3.bsginstitute.com/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomaticaAntigua.Id;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    wc.DownloadString(URI);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        throw ex;
                    }
                }
                else
                {
                    //Elimianmos errores anteriores e insertamos nuevos ... ademas retornamos errores a la vista
                    using (TransactionScope scope = new TransactionScope())
                    {
                        servicio.Update(AsignacionAutomaticaAntigua);
                        var errorAnterior = _unitOfWork.AsignacionAutomaticaErrorRepository.FirstBy(w => w.IdAsignacionAutomatica == AsignacionAutomaticaAntigua.Id, w => new { w.Id }).Id;

                        var aux = _unitOfWork.AsignacionAutomaticaErrorRepository.Delete(errorAnterior, obj.Usuario);
                        foreach (var error in lista_errores)
                        {
                            error.FechaCreacion = DateTime.Now;
                            error.FechaModificacion = DateTime.Now;
                            error.Estado = true;
                            error.UsuarioCreacion = obj.Usuario;
                            error.UsuarioModificacion = obj.Usuario;
                            servicioError.Add(error);
                        }
                        scope.Complete();
                    }
                    try
                    {
                        string URI = "https://integrav4-syncv3.bsginstitute.com/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomaticaAntigua.Id;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                return (null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
