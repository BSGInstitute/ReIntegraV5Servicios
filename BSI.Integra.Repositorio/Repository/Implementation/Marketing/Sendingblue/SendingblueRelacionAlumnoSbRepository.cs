using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueRelacionAlmunoSBDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue
{
    public class SendingblueRelacionAlumnoSbRepository : GenericRepository<TSendinblueRelacionAlmunoSb>, ISendingblueRelacionAlumnoSbRepository
    {
        private Mapper _mapper;
        public SendingblueRelacionAlumnoSbRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSendinblueRelacionAlmunoSb, CrearSendinblueRelacionAlmunoSB>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSendinblueRelacionAlmunoSb MapeoEntidad(CrearSendinblueRelacionAlmunoSB entidad)
        {
            try
            {
                TSendinblueRelacionAlmunoSb modelo = _mapper.Map<TSendinblueRelacionAlmunoSb>(entidad);
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
                var query = "mkt.SP_InsertarSendingblueContactoRelacionAlumno";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    Acciones
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinblueRelacionAlmunoSb Update(CrearSendinblueRelacionAlmunoSB entidad)
        {
            try
            {
                var LandingPage = MapeoEntidad(entidad);
                int idenntidad= Convert.ToInt32(entidad.Id);
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


        public bool AddRange(string listadoEntidad)
        {
            try
            {
                //List<TSendinblueRelacionAlmunoSb> listado = new List<TSendinblueRelacionAlmunoSb>();
                //foreach (var entidad in listadoEntidad)
                //{
                //    listado.Add(MapeoEntidad(entidad));
                //}
                //base.Insert(listado);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TSendinblueRelacionAlmunoSb> Update(IEnumerable<CrearSendinblueRelacionAlmunoSB> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSendinblueRelacionAlmunoSb> listado = new List<TSendinblueRelacionAlmunoSb>();
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
        public IEnumerable<TSendinblueRelacionAlmunoSb> ObtenerTodaslasCampanias()
        {
            try
            {
                var rpta = base.GetBy(x => x.Id > 0);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSendinblueRelacionAlmunoSb ObtenerCampaniaPorId(int id)
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
    }
}
