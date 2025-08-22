using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class WhatsAppConfiguracionEnvioPorProgramaRepository : GenericRepository<TWhatsAppConfiguracionEnvioPorPrograma>, IWhatsAppConfiguracionEnvioPorProgramaRepository
    {
        private Mapper _mapper;
        public WhatsAppConfiguracionEnvioPorProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppConfiguracionEnvioPorPrograma, WhatsAppConfiguracionEnvioPorProgramaDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppConfiguracionEnvioPorPrograma MapeoEntidad(WhatsAppConfiguracionEnvioPorProgramaDTO entidad)
        {
            try
            {
                TWhatsAppConfiguracionEnvioPorPrograma modelo = _mapper.Map<TWhatsAppConfiguracionEnvioPorPrograma>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionEnvioPorPrograma Add(WhatsAppConfiguracionEnvioPorProgramaDTO entidad)
        {
            try
            {
                var LandingPage = MapeoEntidad(entidad);
                base.Insert(LandingPage);
                return LandingPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionEnvioPorPrograma Update(WhatsAppConfiguracionEnvioPorProgramaDTO entidad)
        {
            try
            {
                var LandingPage = MapeoEntidad(entidad);
                int idenntidad = Convert.ToInt32(entidad.Id);
                var entidadExistente = base.FirstBy(w => w.Id == idenntidad, s => new { s.RowVersion });
                LandingPage.RowVersion = entidadExistente.RowVersion;

                base.Update(LandingPage);
                return LandingPage;
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


        public IEnumerable<TWhatsAppConfiguracionEnvioPorPrograma> Add(IEnumerable<WhatsAppConfiguracionEnvioPorProgramaDTO> listadoEntidad)
        {
            try
            {
                List<TWhatsAppConfiguracionEnvioPorPrograma> listado = new List<TWhatsAppConfiguracionEnvioPorPrograma>();
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

        public IEnumerable<TWhatsAppConfiguracionEnvioPorPrograma> Update(IEnumerable<WhatsAppConfiguracionEnvioPorProgramaDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppConfiguracionEnvioPorPrograma> listado = new List<TWhatsAppConfiguracionEnvioPorPrograma>();
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
        public List<WhatsAppConfiguracionEnvioPorProgramaDTO> GetBy(int id)
        {
            try
            {
                var datos = base.GetBy(w => w.IdWhatsAppConfiguracionEnvio == id && w.IdTipoEnvioPrograma == 1, y => new WhatsAppConfiguracionEnvioPorProgramaDTO { IdPgeneral = y.IdPgeneral }).ToList();
                return datos;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
