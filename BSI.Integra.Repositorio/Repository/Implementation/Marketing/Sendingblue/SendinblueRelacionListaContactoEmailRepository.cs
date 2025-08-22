using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue
{
    public class SendinblueRelacionListaContactoEmailRepository : GenericRepository<TSendinblueRelacionListaContactoEmail>, ISendinblueRelacionListaContactoEmailRepository
    {
        private Mapper _mapper;
        public SendinblueRelacionListaContactoEmailRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSendinblueRelacionListaContactoEmail, SendinblueRelacionListaContactoEmailDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSendinblueRelacionListaContactoEmail MapeoEntidad(SendinblueRelacionListaContactoEmailDTO entidad)
        {
            try
            {
                TSendinblueRelacionListaContactoEmail modelo = _mapper.Map<TSendinblueRelacionListaContactoEmail>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinblueRelacionListaContactoEmail Add(SendinblueRelacionListaContactoEmailDTO entidad)
        {
            try
            {
                var AdwordsApiPalabraClave = MapeoEntidad(entidad);
                base.Insert(AdwordsApiPalabraClave);
                return AdwordsApiPalabraClave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TSendinblueRelacionListaContactoEmail> Add(IEnumerable<SendinblueRelacionListaContactoEmailDTO> listadoEntidad)
        {
            try
            {
                List<TSendinblueRelacionListaContactoEmail> listado = new List<TSendinblueRelacionListaContactoEmail>();
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

        public TSendinblueRelacionListaContactoEmail Update(SendinblueRelacionListaContactoEmailDTO entidad)
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
        public IEnumerable<TSendinblueRelacionListaContactoEmail> Update(IEnumerable<SendinblueRelacionListaContactoEmailDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSendinblueRelacionListaContactoEmail> listado = new List<TSendinblueRelacionListaContactoEmail>();
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
        public IEnumerable<TSendinblueRelacionListaContactoEmail> Update(IEnumerable<TSendinblueRelacionListaContactoEmail> listadoEntidad)
        {
            try
            {

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listadoEntidad)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listadoEntidad);
                return listadoEntidad;
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
    }
    #endregion
}
