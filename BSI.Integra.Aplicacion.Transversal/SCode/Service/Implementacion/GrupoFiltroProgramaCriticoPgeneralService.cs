using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: GrupoFiltroProgramaCriticoPgeneralService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_GrupoFiltroProgramaCriticoPgeneral
    /// </summary>
    public class GrupoFiltroProgramaCriticoPgeneralService : IGrupoFiltroProgramaCriticoPgeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public GrupoFiltroProgramaCriticoPgeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TGrupoFiltroProgramaCriticoPgeneral, GrupoFiltroProgramaCriticoPgeneral>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public GrupoFiltroProgramaCriticoPgeneral Add(GrupoFiltroProgramaCriticoPgeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GrupoFiltroProgramaCriticoPgeneral>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GrupoFiltroProgramaCriticoPgeneral Update(GrupoFiltroProgramaCriticoPgeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GrupoFiltroProgramaCriticoPgeneral>(modelo);
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
                _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoFiltroProgramaCriticoPgeneral> Add(List<GrupoFiltroProgramaCriticoPgeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GrupoFiltroProgramaCriticoPgeneral>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoFiltroProgramaCriticoPgeneral> Update(List<GrupoFiltroProgramaCriticoPgeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GrupoFiltroProgramaCriticoPgeneral>>(modelo);
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
                _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_GrupoFiltroProgramaCritico para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        /// 

        public bool GuardarAsociacion(AsociacionGrupoFiltroPGeneralDTO Json)
        {

            try
            {
                var _repGrupoFiltroProgramaCriticoPgeneral = _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository;
                var _repPgeneral = _unitOfWork.PGeneralRepository;

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        this.EliminadoLogicoPorGrupo(Json.IdGrupo, Json.Usuario, Json.ListaPGeneral);

                        GrupoFiltroProgramaCriticoPgeneral grupoFiltroProgramaCriticoPgeneral = new GrupoFiltroProgramaCriticoPgeneral();

                        foreach (var item in Json.ListaPGeneral)

                        {
                            PGeneralAsignaVentaDTO Pgneral = new PGeneralAsignaVentaDTO();
                            Pgneral.Id = item.Id;
                            Pgneral.UsuarioModificacion = Json.Usuario;
                            Pgneral.AsignaVenta = item.AsignaVenta.Value;
                            _repPgeneral.ActualizarPgeneral(Pgneral);
                            _unitOfWork.Commit();


                            grupoFiltroProgramaCriticoPgeneral = _mapper.Map<GrupoFiltroProgramaCriticoPgeneral>(_repGrupoFiltroProgramaCriticoPgeneral.FirstBy(x => x.IdGrupoFiltroProgramaCritico == Json.IdGrupo && x.IdPgeneral == item.Id));
                            if (grupoFiltroProgramaCriticoPgeneral == null)
                            {
                                grupoFiltroProgramaCriticoPgeneral = new GrupoFiltroProgramaCriticoPgeneral();
                                grupoFiltroProgramaCriticoPgeneral.IdPgeneral = item.Id;
                                grupoFiltroProgramaCriticoPgeneral.IdGrupoFiltroProgramaCritico = Json.IdGrupo;
                                grupoFiltroProgramaCriticoPgeneral.Estado = true;
                                grupoFiltroProgramaCriticoPgeneral.UsuarioCreacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPgeneral.UsuarioModificacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPgeneral.FechaCreacion = DateTime.Now;
                                grupoFiltroProgramaCriticoPgeneral.FechaModificacion = DateTime.Now;
                                _repGrupoFiltroProgramaCriticoPgeneral.Add(grupoFiltroProgramaCriticoPgeneral);
                                _unitOfWork.Commit();
                            }
                            else
                            {

                                
                                grupoFiltroProgramaCriticoPgeneral.UsuarioModificacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPgeneral.FechaModificacion = DateTime.Now;
                                _repGrupoFiltroProgramaCriticoPgeneral.Update(grupoFiltroProgramaCriticoPgeneral);
                                _unitOfWork.Commit();

                            }
                        }
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminadoLogicoPorGrupo(int idGrupo, string usuario, List<PGeneralSubAreaDTO> nuevos)
        {
            try

            {
                var _rep = _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository;
                List<EliminacionGrupoFiltroPGeneralDTO> listaBorrar = new List<EliminacionGrupoFiltroPGeneralDTO>();
                listaBorrar = _rep.GetBy(x => x.IdGrupoFiltroProgramaCritico == idGrupo, y => new EliminacionGrupoFiltroPGeneralDTO()
                {
                    Id = y.Id,
                    IdPGeneral = y.IdPgeneral
                }).ToList();

                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.IdPGeneral));
                foreach (var item in listaBorrar)
                {
                    this.Delete(item.Id, usuario);
                }
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
