using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Linq;
using System.Transactions;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class GrupoComponenteEvaluacionService : IGrupoComponenteEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public GrupoComponenteEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGrupoComponenteEvaluacion, GrupoComponenteEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TGrupoComponenteEvaluacion, GrupoComponenteEvaluacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<GrupoComponenteEvaluacion, GrupoComponenteEvaluacionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public List<ComboDTO> ObtenerGruposPorIdEvaluacion(List<int> listaIdGrupo)
        {
            var grupos = _unitOfWork.GrupoComponenteEvaluacionRepository.ObtenerGrupoPorIds(listaIdGrupo);
            return grupos.ToList();
        }
        public AsignacionComponenteEvaluacionDTO ActualizarAsignacionComponenteAEvaluacion(AsignacionComponenteEvaluacionDTO Json, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var NoAsignado in Json.ListaComponenteNoAsignado)
                    {
                        TExaman examen = new TExaman();
                        examen = _unitOfWork.ExamenRepository.FirstById(NoAsignado.Id);
                        if (examen.Id != 0 && examen.IdExamenTest != null)
                        {

                            examen.IdExamenTest = null;
                            examen.UsuarioModificacion = usuario;
                            examen.FechaModificacion = DateTime.Now;
                            _unitOfWork.ExamenRepository.Update(examen);
                        }
                    }

                    foreach (var Asignado in Json.ListaComponenteAsignado)
                    {
                        TExaman examen = new TExaman();
                        examen = _unitOfWork.ExamenRepository.FirstById(Asignado.Id);
                        if (examen.Id != 0 && examen.IdExamenTest != Json.IdEvaluacion)
                        {

                            examen.IdExamenTest = Json.IdEvaluacion;
                            examen.UsuarioModificacion = usuario;
                            examen.FechaModificacion = DateTime.Now;
                            _unitOfWork.ExamenRepository.Update(examen);
                        }
                    }
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool ActualizarFactorGrupoComponente(GrupoComponenteFactorDTO GrupoComponente)
        {
            try
            {
                var grupoComponenteEvaluacion = _unitOfWork.GrupoComponenteEvaluacionRepository.FirstById(GrupoComponente.Id);
                grupoComponenteEvaluacion.Factor = GrupoComponente.Factor;
                grupoComponenteEvaluacion.UsuarioModificacion = GrupoComponente.Usuario ?? "System";
                grupoComponenteEvaluacion.FechaModificacion = DateTime.Now;
                var res = _unitOfWork.GrupoComponenteEvaluacionRepository.Update(grupoComponenteEvaluacion);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex) { 
                return false;
            }
        }

        public bool ActualizarGrupoComponente(GrupoComponenteEvaluacionFormularioDTO Formulario)
        {
            try
            {
                var grupoComponente = _unitOfWork.GrupoComponenteEvaluacionRepository.FirstById(Formulario.GrupoComponenteEvaluacion.Id);

                var ListaExamenExistente = _unitOfWork.ExamenRepository
                    .GetBy(x => x.IdExamenTest == Formulario.IdEvaluacion && x.IdGrupoComponenteEvaluacion == Formulario.GrupoComponenteEvaluacion.Id)
                    .ToList();

                using (var scope = new TransactionScope())
                {
                    grupoComponente.Nombre = Formulario.GrupoComponenteEvaluacion.Nombre;
                    grupoComponente.NombreAbreviado = Formulario.GrupoComponenteEvaluacion.NombreAbreviado;
                    grupoComponente.IdFormulaPuntaje = Formulario.GrupoComponenteEvaluacion.IdFormulaPuntaje;
                    grupoComponente.RequiereCentil = Formulario.GrupoComponenteEvaluacion.RequiereCentil;
                    grupoComponente.Factor = Formulario.GrupoComponenteEvaluacion.Factor;
                    grupoComponente.UsuarioModificacion = Formulario.Usuario;
                    grupoComponente.FechaModificacion = DateTime.Now;

                    _unitOfWork.GrupoComponenteEvaluacionRepository.Update(grupoComponente);

                    var listaExamenesModificados = new List<TExaman>();

                    foreach (var examen in ListaExamenExistente)
                    {
                        examen.IdGrupoComponenteEvaluacion = null;
                        listaExamenesModificados.Add(examen);
                    }

                    foreach (var item in Formulario.GrupoComponenteEvaluacion.ListaComponentes)
                    {
                        var examen = listaExamenesModificados.FirstOrDefault(e => e.Id == item.Id);

                        if (examen == null)
                        {
                            examen = _unitOfWork.ExamenRepository.FirstById(item.Id);
                        }

                        examen.IdGrupoComponenteEvaluacion = grupoComponente.Id;
                        examen.UsuarioModificacion = Formulario.Usuario;
                        examen.FechaModificacion = DateTime.Now;

                        listaExamenesModificados.Add(examen);
                    }

                    foreach (var examen in listaExamenesModificados)
                    {
                        _unitOfWork.ExamenRepository.Update(examen);
                    }

                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RegistrarGrupoComponente(GrupoComponenteEvaluacionFormularioDTO Formulario,string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var nuevoGrupoComponente = new TGrupoComponenteEvaluacion
                    {
                        Nombre = Formulario.GrupoComponenteEvaluacion.Nombre,
                        NombreAbreviado = Formulario.GrupoComponenteEvaluacion.NombreAbreviado,
                        IdFormulaPuntaje = Formulario.GrupoComponenteEvaluacion.IdFormulaPuntaje,
                        RequiereCentil = Formulario.GrupoComponenteEvaluacion.RequiereCentil,
                        Factor = Formulario.GrupoComponenteEvaluacion.Factor,
                        UsuarioCreacion = usuario,
                        FechaCreacion = DateTime.Now,
                        UsuarioModificacion = usuario,
                        FechaModificacion = DateTime.Now,
                        Estado = true 
                    };

                    _unitOfWork.GrupoComponenteEvaluacionRepository.Insert(nuevoGrupoComponente);
                    _unitOfWork.Commit();

                    foreach (var componente in Formulario.GrupoComponenteEvaluacion.ListaComponentes)
                    {
                        var examen = _unitOfWork.ExamenRepository.FirstById(componente.Id);
                        examen.IdGrupoComponenteEvaluacion = nuevoGrupoComponente.Id;
                        examen.UsuarioModificacion = usuario;
                        examen.FechaModificacion = DateTime.Now;

                        _unitOfWork.ExamenRepository.Update(examen);
                    }

                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool ActualizarCentilGrupoComponente(ObjetoCentilCompuestoDTO CentilFormulario)
        {
            try
            {
                var centil = _unitOfWork.CentilRepository.FirstById(CentilFormulario.Centil.Id);
                if (centil != null)
                {
                    centil.IdExamenTest = CentilFormulario.Centil.IdExamenTest;
                    centil.IdGrupoComponenteEvaluacion = CentilFormulario.Centil.IdGrupoComponenteEvaluacion;
                    centil.IdExamen = CentilFormulario.Centil.IdExamen;
                    centil.IdSexo = CentilFormulario.Centil.IdSexo;
                    centil.ValorMinimo = CentilFormulario.Centil.ValorMinimo;
                    centil.ValorMaximo = CentilFormulario.Centil.ValorMaximo;
                    centil.Centil = CentilFormulario.Centil.Centil;
                    centil.CentilLetra = CentilFormulario.Centil.CentilLetra;
                    centil.Estado = true;
                    centil.UsuarioModificacion = CentilFormulario.Usuario;
                    centil.FechaModificacion = DateTime.Now;
                    _unitOfWork.CentilRepository.Update(centil);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
