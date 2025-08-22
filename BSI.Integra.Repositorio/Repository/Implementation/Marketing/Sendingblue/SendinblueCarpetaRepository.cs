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
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.IntegracionConIntegraDB.RelacionFolderConListaDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue
{
    public class SendinblueCarpetaRepository : GenericRepository<TSendinblueCarpetum>, ISendinblueCarpetaRepository
    {
        private Mapper _mapper;
        public SendinblueCarpetaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSendinblueCarpetum, CrearSendinblueCarpeta>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSendinblueCarpetum MapeoEntidad(CrearSendinblueCarpeta entidad)
        {
            try
            {
                TSendinblueCarpetum modelo = _mapper.Map<TSendinblueCarpetum>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinblueCarpetum Add(CrearSendinblueCarpeta entidad)
        {
            try
            {
                entidad.Id = 0;
                var LandingPage = MapeoEntidad(entidad);
                base.Insert(LandingPage);
                return LandingPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinblueCarpetum Update(CrearSendinblueCarpeta entidad)
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


        public IEnumerable<TSendinblueCarpetum> Add(IEnumerable<CrearSendinblueCarpeta> listadoEntidad)
        {
            try
            {
                List<TSendinblueCarpetum> listado = new List<TSendinblueCarpetum>();
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

        public IEnumerable<TSendinblueCarpetum> Update(IEnumerable<CrearSendinblueCarpeta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSendinblueCarpetum> listado = new List<TSendinblueCarpetum>();
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
        public IEnumerable<TSendinblueCarpetum> ObtenerTodaslasCampanias()
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
        public TSendinblueCarpetum ObtenerCampaniaPorId(int id)
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
        public RelacionFolderConLista ObtenerFolderMasListas(int idF)
        {
            try
            {
                RelacionFolderConLista relacion = new RelacionFolderConLista();
                var query = "select * from [mkt].[V_ObtenerListasPorFolder] where IdSendinblueCarpeta= @idF";
                var respuesta = _dapperRepository.QueryDapper(query, new
                {
                    idF
                });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    var res = JsonConvert.DeserializeObject<List<RelacionFolderConListaSQL>>(respuesta);
                    relacion = res.GroupBy(c => new
                    {
                        c.Id,
                        c.Nombre
                    }).Select(g => new RelacionFolderConLista {
                        Carpeta = new CarpetaSendingblueHelper { Id = g.Key.Id, Nombre = g.Key.Nombre },
                        Listas = res.Where(x => x.Id == g.Key.Id).Select(x => new ListasSendingblueHelper {
                            ListaId = x.ListaId,
                            NombreLista=x.NombreLista,
                            TotalExcluido=x.TotalExcluido,
                            TotalSuscrito=x.TotalExcluido,
                            UnicoSuscrito=x.UnicoSuscrito,
                            IdSendinblueLista=x.IdSendinblueLista,
                            IdSendinblueCarpeta =x.IdSendinblueCarpeta
                        }).ToList(),
                    }).FirstOrDefault();
                }
                return relacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

