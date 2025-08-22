using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Text;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: BandejaPendientePwService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/09/2023
    /// <summary>
    /// Gestión general de T_Documento_PW
    /// </summary>
    public class BandejaPendientePwService: IBandejaPendientePwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public BandejaPendientePwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    //cfg.CreateMap<BandejaPendientePw, BandejaPendientePwDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/08/23
        /// Versión: 1.0
        /// <summary>
        ///  Elimina (Actualiza estado a false ) todos las registros asociados a IdDocumento
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionBandejaPendienteLogicoPorIdDocumento(int idDocumento, string usuario, List<RevisionNivelPwFiltroIdPlantillaDTO> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.BandejaPendientePwRepository.ObtenerPorIdDocumento(idDocumento).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdRevisionNivelPw)));

                foreach (var item in listaBorrar)
                {
                    _unitOfWork.BandejaPendientePwRepository.Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
