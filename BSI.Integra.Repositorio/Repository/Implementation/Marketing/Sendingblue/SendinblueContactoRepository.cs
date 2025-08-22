using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue
{
    public class SendinblueContactoRepository : GenericRepository<TSendinblueContacto>, ISendinblueContactoRepository
    {
        private Mapper _mapper;
        public SendinblueContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSendinblueContacto, CrearSendingblueContactos>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSendinblueContacto MapeoEntidad(CrearSendingblueContactos entidad)
        {
            try
            {
                TSendinblueContacto modelo = _mapper.Map<TSendinblueContacto>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Add(string Acciones)
        {
            try
            {
                CrearSendingblueContactos rpta = new CrearSendingblueContactos();
                TSendinblueContacto retorno = new TSendinblueContacto();

                var query = "mkt.SP_InsertarSendingblueContacto";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    Acciones
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TSendinblueContacto Update(CrearSendingblueContactos entidad)
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
        public IEnumerable<TSendinblueContacto> Update(IEnumerable<CrearSendingblueContactos> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSendinblueContacto> listado = new List<TSendinblueContacto>();
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
        public IEnumerable<TSendinblueContacto> Update(IEnumerable<TSendinblueContacto> listadoEntidad)
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

        #endregion
        public IEnumerable<TSendinblueContacto> ObtenerTodaslasCampanias()
        {
            try
            {
                var rpta = base.GetBy(x => x.Estado != false);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSendinblueContacto ObtenerCampaniaPorId(int id)
        {
            try
            {
                var rpta = base.FirstBy(x => x.Id == id);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSendinblueContacto ObtenerCampaniaPorEmail(string email)
        {
            try
            {
                var rpta = base.FirstBy(x => x.Email == email && x.Estado==true);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
