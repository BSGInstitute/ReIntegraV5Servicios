using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCamapaniaDTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue
{
    public class SendingblueCampaniaRepository : GenericRepository<TSendinblueCampanium>, ISendinblueCampaniaRepository
    {
        private Mapper _mapper;
        public SendingblueCampaniaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSendinblueCampanium, CrearSendinblueCamapaniaDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSendinblueCampanium MapeoEntidad(CrearSendinblueCamapaniaDTO entidad)
        {
            try
            {
                TSendinblueCampanium modelo = _mapper.Map<TSendinblueCampanium>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinblueCampanium Add(CrearSendinblueCamapaniaDTO entidad)
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

        public TSendinblueCampanium Update(CrearSendinblueCamapaniaDTO entidad)
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


        public IEnumerable<TSendinblueCampanium> Add(IEnumerable<CrearSendinblueCamapaniaDTO> listadoEntidad)
        {
            try
            {
                List<TSendinblueCampanium> listado = new List<TSendinblueCampanium>();
                foreach (var entidad in listadoEntidad)
                {
                    if (entidad.IdSendinblueRemitente != 2758)
                    {
                        listado.Add(MapeoEntidad(entidad));
                    }
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TSendinblueCampanium> Update(IEnumerable<CrearSendinblueCamapaniaDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSendinblueCampanium> listado = new List<TSendinblueCampanium>();
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
        public IEnumerable<TSendinblueCampanium> UpdateLis(List<TSendinblueCampanium> listadoEntidad)
        {
            try
            {

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
        public IEnumerable<TSendinblueCampanium> ObtenerTodaslasCampanias()
        {
            try
            {
                var rpta= base.GetBy(x => x.Id > 0);
                return rpta;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public TSendinblueCampanium ObtenerCampaniaPorId(int id)
        {
            try
            {
                var rpta = base.FirstBy(x => x.Id ==id);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

