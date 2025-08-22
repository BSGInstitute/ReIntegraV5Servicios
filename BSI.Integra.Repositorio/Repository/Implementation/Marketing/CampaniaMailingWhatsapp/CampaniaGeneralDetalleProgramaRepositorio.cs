using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp.CampaniaMailingWhatsAppFiltradoDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    public class CampaniaGeneralDetalleProgramaRepositorio : GenericRepository<TCampaniaGeneralDetallePrograma>, ICampaniaGeneralDetalleProgramaRepositorio
    {

        private Mapper _mapper;

        public CampaniaGeneralDetalleProgramaRepositorio(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaGeneralDetallePrograma, CampaniaGeneralDetallePrograma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public CampaniaGeneralDetallePrograma AddEntity(TCampaniaGeneralDetallePrograma entidad)
        {
            Insert(entidad);
            var daesmap = _mapper.Map<CampaniaGeneralDetallePrograma>(entidad);
            return daesmap;
        }

        #region Metodos Base
        private TCampaniaGeneralDetallePrograma MapeoEntidad(CampaniaGeneralDetallePrograma entidad)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralDetallePrograma modelo = _mapper.Map<TCampaniaGeneralDetallePrograma>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneralDetallePrograma Add(CampaniaGeneralDetallePrograma entidad)
        {
            try
            {
                var CampaniaGeneralDetallePrograma = MapeoEntidad(entidad);
                base.Insert(CampaniaGeneralDetallePrograma);
                return CampaniaGeneralDetallePrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneralDetallePrograma Update(CampaniaGeneralDetallePrograma entidad)
        {
            try
            {
                var CampaniaGeneralDetallePrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CampaniaGeneralDetallePrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(CampaniaGeneralDetallePrograma);
                return CampaniaGeneralDetallePrograma;
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


        public IEnumerable<TCampaniaGeneralDetallePrograma> Add(IEnumerable<CampaniaGeneralDetallePrograma> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneralDetallePrograma> listado = new List<TCampaniaGeneralDetallePrograma>();
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

        public IEnumerable<TCampaniaGeneralDetallePrograma> Update(IEnumerable<CampaniaGeneralDetallePrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneralDetallePrograma> listado = new List<TCampaniaGeneralDetallePrograma>();
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

        public TCampaniaGeneralDetallePrograma Add(TCampaniaGeneralDetallePrograma entidad)
        {
            try
            {
                base.Insert(entidad);
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TCampaniaGeneralDetallePrograma> Add(IEnumerable<TCampaniaGeneralDetallePrograma> listadoEntidad)
        {
            try
            {
                base.Insert(listadoEntidad);
                return listadoEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

    }
}
