using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    public class WhatsAppConfiguracionEnvioPorProgramaService : IWhatsAppConfiguracionEnvioPorProgramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppConfiguracionEnvioPorProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppConfiguracionEnvioPorPrograma, WhatsAppConfiguracionEnvioPorProgramaDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        public TWhatsAppConfiguracionEnvioPorPrograma Add(WhatsAppConfiguracionEnvioPorProgramaDTO entidad)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioPorProgramaRepository.Add(entidad);
        }

        public IEnumerable<TWhatsAppConfiguracionEnvioPorPrograma> Add(IEnumerable<WhatsAppConfiguracionEnvioPorProgramaDTO> listadoEntidad)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioPorProgramaRepository.Add(listadoEntidad);
        }

        public bool Delete(int id, string usuario)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioPorProgramaRepository.Delete(id, usuario);
        }

        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioPorProgramaRepository.Delete(listadoIds, usuario);
        }

        public TWhatsAppConfiguracionEnvioPorPrograma Update(WhatsAppConfiguracionEnvioPorProgramaDTO entidad)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioPorProgramaRepository.Update(entidad);
        }

        public IEnumerable<TWhatsAppConfiguracionEnvioPorPrograma> Update(IEnumerable<WhatsAppConfiguracionEnvioPorProgramaDTO> listadoEntidad)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioPorProgramaRepository.Update(listadoEntidad);
        }
        public void InsertarWhatsAppConfiguracionEnvio(List<InsertarWhatsAppConfiguracionEnvioDTO> ObjetoDTO, string usuario)
        {
            WhatsAppConfiguracionEnvioService _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioService(_unitOfWork);
            foreach (var WhatsAppConfiguracion in ObjetoDTO)
            {
                if (WhatsAppConfiguracion.Id == 0)
                {
                    WhatsAppConfiguracionEnvioDTO whatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioDTO();
                    whatsAppConfiguracionEnvio.Nombre = WhatsAppConfiguracion.Nombre;
                    whatsAppConfiguracionEnvio.Descripcion = WhatsAppConfiguracion.Descripcion;
                    whatsAppConfiguracionEnvio.IdPersonal = WhatsAppConfiguracion.IdPersonal;
                    whatsAppConfiguracionEnvio.IdPlantilla = WhatsAppConfiguracion.IdPlantilla;
                    whatsAppConfiguracionEnvio.IdConjuntoListaDetalle = WhatsAppConfiguracion.IdConjuntoListaDetalle;
                    whatsAppConfiguracionEnvio.UsuarioCreacion = usuario;
                    whatsAppConfiguracionEnvio.UsuarioModificacion = usuario;
                    whatsAppConfiguracionEnvio.FechaModificacion = DateTime.Now;
                    whatsAppConfiguracionEnvio.FechaCreacion = DateTime.Now;
                    whatsAppConfiguracionEnvio.Activo = true;

                    _repWhatsAppConfiguracionEnvio.Add(whatsAppConfiguracionEnvio,usuario);
                    if (WhatsAppConfiguracion.ProgramaPrincipal != null)
                    {
                        List<WhatsAppConfiguracionEnvioPorProgramaDTO> LwhatsAppConfiguracionEnvioPorPrograma = new List<WhatsAppConfiguracionEnvioPorProgramaDTO>();

                        foreach (var programa in WhatsAppConfiguracion.ProgramaPrincipal)
                        {
                            WhatsAppConfiguracionEnvioPorProgramaDTO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaDTO();

                            whatsAppConfiguracionEnvioPorPrograma.IdWhatsAppConfiguracionEnvio = whatsAppConfiguracionEnvio.Id;
                            whatsAppConfiguracionEnvioPorPrograma.IdPgeneral = programa.Id;
                            whatsAppConfiguracionEnvioPorPrograma.IdTipoEnvioPrograma = 1;
                            whatsAppConfiguracionEnvioPorPrograma.UsuarioCreacion = usuario;
                            whatsAppConfiguracionEnvioPorPrograma.UsuarioModificacion = usuario;
                            whatsAppConfiguracionEnvioPorPrograma.FechaModificacion = DateTime.Now;
                            whatsAppConfiguracionEnvioPorPrograma.FechaCreacion = DateTime.Now;
                            LwhatsAppConfiguracionEnvioPorPrograma.Add(whatsAppConfiguracionEnvioPorPrograma);
                        }
                        Add(LwhatsAppConfiguracionEnvioPorPrograma);
                    }

                    if (WhatsAppConfiguracion.ProgramaSecundario != null)
                    {
                        List<WhatsAppConfiguracionEnvioPorProgramaDTO> LwhatsAppConfiguracionEnvioPorPrograma = new List<WhatsAppConfiguracionEnvioPorProgramaDTO>();

                        foreach (var programa in WhatsAppConfiguracion.ProgramaSecundario)
                        {
                            WhatsAppConfiguracionEnvioPorProgramaDTO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaDTO();

                            whatsAppConfiguracionEnvioPorPrograma.IdWhatsAppConfiguracionEnvio = whatsAppConfiguracionEnvio.Id;
                            whatsAppConfiguracionEnvioPorPrograma.IdPgeneral = programa.Id;
                            whatsAppConfiguracionEnvioPorPrograma.IdTipoEnvioPrograma = 2;
                            whatsAppConfiguracionEnvioPorPrograma.UsuarioCreacion = usuario;
                            whatsAppConfiguracionEnvioPorPrograma.UsuarioModificacion = usuario;
                            whatsAppConfiguracionEnvioPorPrograma.FechaModificacion = DateTime.Now;
                            whatsAppConfiguracionEnvioPorPrograma.FechaCreacion = DateTime.Now;
                            LwhatsAppConfiguracionEnvioPorPrograma.Add(whatsAppConfiguracionEnvioPorPrograma);
                        }
                        Add(LwhatsAppConfiguracionEnvioPorPrograma);
                    }
                }
                else
                {
                    var WhatsAppEnvio = _repWhatsAppConfiguracionEnvio.FirstById(WhatsAppConfiguracion.Id);
                    WhatsAppEnvio.Activo = false;
                    WhatsAppEnvio.UsuarioModificacion = usuario;
                    WhatsAppEnvio.FechaModificacion = DateTime.Now;
                    _repWhatsAppConfiguracionEnvio.Update(WhatsAppEnvio, usuario);

                    WhatsAppConfiguracionEnvioDTO whatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioDTO();
                    whatsAppConfiguracionEnvio.Nombre = WhatsAppConfiguracion.Nombre;
                    whatsAppConfiguracionEnvio.Descripcion = WhatsAppConfiguracion.Descripcion;
                    whatsAppConfiguracionEnvio.IdPersonal = WhatsAppConfiguracion.IdPersonal;
                    whatsAppConfiguracionEnvio.IdPlantilla = WhatsAppConfiguracion.IdPlantilla;
                    whatsAppConfiguracionEnvio.IdConjuntoListaDetalle = WhatsAppConfiguracion.IdConjuntoListaDetalle;
                    whatsAppConfiguracionEnvio.UsuarioCreacion = usuario;
                    whatsAppConfiguracionEnvio.UsuarioModificacion = usuario;
                    whatsAppConfiguracionEnvio.FechaModificacion = DateTime.Now;
                    whatsAppConfiguracionEnvio.FechaCreacion = DateTime.Now;
                    whatsAppConfiguracionEnvio.Activo = true;

                    _repWhatsAppConfiguracionEnvio.Add(whatsAppConfiguracionEnvio, usuario);
                    if (WhatsAppConfiguracion.ProgramaPrincipal != null)
                    {
                        List<WhatsAppConfiguracionEnvioPorProgramaDTO> LwhatsAppConfiguracionEnvioPorPrograma = new List<WhatsAppConfiguracionEnvioPorProgramaDTO>();
                        foreach (var programa in WhatsAppConfiguracion.ProgramaPrincipal)
                        {
                            WhatsAppConfiguracionEnvioPorProgramaDTO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaDTO();
                            whatsAppConfiguracionEnvioPorPrograma.IdWhatsAppConfiguracionEnvio = whatsAppConfiguracionEnvio.Id;
                            whatsAppConfiguracionEnvioPorPrograma.IdPgeneral = programa.Id;
                            whatsAppConfiguracionEnvioPorPrograma.IdTipoEnvioPrograma = 1;
                            whatsAppConfiguracionEnvioPorPrograma.UsuarioCreacion = usuario;
                            whatsAppConfiguracionEnvioPorPrograma.UsuarioModificacion = usuario;
                            whatsAppConfiguracionEnvioPorPrograma.FechaModificacion = DateTime.Now;
                            whatsAppConfiguracionEnvioPorPrograma.FechaCreacion = DateTime.Now;
                            LwhatsAppConfiguracionEnvioPorPrograma.Add(whatsAppConfiguracionEnvioPorPrograma);
                        }
                        Add(LwhatsAppConfiguracionEnvioPorPrograma);
                    }

                    if (WhatsAppConfiguracion.ProgramaSecundario != null)
                    {
                        List<WhatsAppConfiguracionEnvioPorProgramaDTO> LwhatsAppConfiguracionEnvioPorPrograma = new List<WhatsAppConfiguracionEnvioPorProgramaDTO>();
                        foreach (var programa in WhatsAppConfiguracion.ProgramaSecundario)
                        {
                            WhatsAppConfiguracionEnvioPorProgramaDTO whatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaDTO();

                            whatsAppConfiguracionEnvioPorPrograma.IdWhatsAppConfiguracionEnvio = whatsAppConfiguracionEnvio.Id;
                            whatsAppConfiguracionEnvioPorPrograma.IdPgeneral = programa.Id;
                            whatsAppConfiguracionEnvioPorPrograma.IdTipoEnvioPrograma = 2;
                            whatsAppConfiguracionEnvioPorPrograma.UsuarioCreacion = usuario;
                            whatsAppConfiguracionEnvioPorPrograma.UsuarioModificacion = usuario;
                            whatsAppConfiguracionEnvioPorPrograma.FechaModificacion = DateTime.Now;
                            whatsAppConfiguracionEnvioPorPrograma.FechaCreacion = DateTime.Now;
                            LwhatsAppConfiguracionEnvioPorPrograma.Add(whatsAppConfiguracionEnvioPorPrograma);
                        }
                        Add(LwhatsAppConfiguracionEnvioPorPrograma);
                    }
                }
            }
        }
        public List<WhatsAppConfiguracionEnvioPorProgramaDTO> GetBy(int id)
        {
            try
            {
                return _unitOfWork.WhatsAppConfiguracionEnvioPorProgramaRepository.GetBy(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
