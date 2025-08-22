using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PGeneralASubPGeneralService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_PGeneralASubPGeneral
    /// </summary>
    public class PgeneralAsubPgeneralService : IPgeneralAsubPgeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PgeneralAsubPgeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralAsubPgeneral, PgeneralAsubPgeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<PgeneralAsubPgeneral, PgeneralAsubPgeneralDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para el CRUD
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<PGeneralASubPGeneralCursoHijo> </returns>
        public IEnumerable<PgeneralAsubPgeneralCursoHijoDTO> ObtenerCursosHijosPorIdPgeneral(int idPGeneral)
        {
            return _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCursosHijosPorIdPgeneral(idPGeneral);
        }
        /// <summary>
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 2024-04-12
        /// Inserta o actualiza un programa general hijo
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userName"></param>
        /// <returns>bool</returns>
        public PgeneralAsubPgeneralCursoHijoDTO Insertar(PgeneralAsubPgeneralInsertarDTO pGeneralASubPGeneralDTO, string usuario)
        {
            try
            {
                var programaHijo = new PgeneralAsubPgeneral();
                programaHijo.IdPgeneralPadre = pGeneralASubPGeneralDTO.IdPgeneralPadre;
                programaHijo.IdPgeneralHijo = pGeneralASubPGeneralDTO.IdPgeneralHijo;
                programaHijo.UsuarioCreacion = usuario;
                programaHijo.UsuarioModificacion = usuario;
                programaHijo.FechaCreacion = DateTime.Now;
                programaHijo.FechaModificacion = DateTime.Now;
                programaHijo.Estado = true;
                programaHijo.EsVisiblePortal = true;
                var resultado = _unitOfWork.PgeneralAsubPgeneralRepository.Add(programaHijo);
                _unitOfWork.Commit();
                var registro = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCursosHijosPorIdSubPgeneral(resultado.Id)!;
                return registro;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-21
        /// Actualiza [pla].[T_PGeneralASubPGeneral] e inserta o actualiza [pla].[T_PgeneralASubPgeneralVersionPrograma]
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns>bool</returns>
        public PgeneralAsubPgeneralCursoHijoDTO Actualizar(PGeneralASubPGeneralActualizarDTO dto, string usuario)
        {
            try
            {
                var programaHijo = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerPorId(dto.Id);
                if (programaHijo == null)
                {
                    throw new BadRequestException($"#PgSpg-A-001@No existe el registro {dto.Id}");
                }
                if (_unitOfWork.PgeneralAsubPgeneralRepository.Exist(w => w.IdPgeneralPadre == programaHijo.IdPgeneralPadre && w.Orden == dto.Orden && w.Id != dto.Id))
                {
                    throw new BadRequestException("#PgSpg-A-002Ya Existe una registro asociado con el mismo orden");
                }
                programaHijo.Orden = dto.Orden;
                programaHijo.IdCiclo = dto.IdCiclo;
                programaHijo.IdModulo = dto.IdModulo;
                programaHijo.UsuarioModificacion = usuario;
                programaHijo.FechaModificacion = DateTime.Now;

                if (_unitOfWork.PgeneralAsubPgeneralVersionProgramaRepository.Exist(w => w.IdPgeneralAsubPgeneral == programaHijo.Id))
                {
                    var versiones = _unitOfWork.PgeneralAsubPgeneralVersionProgramaRepository.ObtenerPoridPgeneralASubPgeneral(programaHijo.Id).ToList();
                    _unitOfWork.PgeneralAsubPgeneralVersionProgramaRepository.Delete(versiones.Select(w => w.Id), usuario);
                    _unitOfWork.Commit();
                }

                programaHijo.PgeneralAsubPgeneralVersionProgramas = dto.Versiones.Distinct().Select(x => new PgeneralAsubPgeneralVersionPrograma()
                {
                    IdVersionPrograma = x,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario
                }).ToList();

                _unitOfWork.PgeneralAsubPgeneralRepository.Update(programaHijo);
                _unitOfWork.Commit();
                var registro = _unitOfWork.PgeneralAsubPgeneralRepository.ObtenerCursosHijosPorIdSubPgeneral(dto.Id)!;
                return registro;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/12/2024
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="IdPgeneralAsubPgeneral"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool Eliminar(int IdPgeneralAsubPgeneral, string usuario)
        {
            try
            {
                bool result = false;
                if (_unitOfWork.PgeneralAsubPgeneralRepository.Exist(IdPgeneralAsubPgeneral))
                {
                    if (_unitOfWork.PgeneralAsubPgeneralVersionProgramaRepository.Exist(w => w.IdPgeneralAsubPgeneral == IdPgeneralAsubPgeneral))
                    {
                        var pgeneralAsubPgeneralVersiones = _unitOfWork.PgeneralAsubPgeneralVersionProgramaRepository.GetBy(w => w.IdPgeneralAsubPgeneral == IdPgeneralAsubPgeneral).Select(w => w.Id);
                        _unitOfWork.PgeneralAsubPgeneralVersionProgramaRepository.Delete(pgeneralAsubPgeneralVersiones, usuario);
                    }
                    result = _unitOfWork.PgeneralAsubPgeneralRepository.Delete(IdPgeneralAsubPgeneral, usuario);
                    _unitOfWork.Commit();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
