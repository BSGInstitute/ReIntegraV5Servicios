using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
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
    public class CampaniaGeneralDetalleSubAreaRepositorio : GenericRepository<TCampaniaGeneralDetalleSubArea>, ICampaniaGeneralDetalleSubAreaRepositorio
    {

        private Mapper _mapper;

        public CampaniaGeneralDetalleSubAreaRepositorio(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaGeneralDetalleSubArea, CampaniaGeneralDetalleSubArea>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCampaniaGeneralDetalleSubArea, CampaniaGeneralDetalleSubAreaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        public void Eliminarrelacion(List<TCampaniaGeneralDetalleSubArea> listaBorrar, List<int> nuevos, string usuario)
        {
            throw new NotImplementedException();
        }

        public TCampaniaGeneralDetalleSubArea FirstBy(int item, int Id)
        {
            try
            {
                TCampaniaGeneralDetalleSubArea entidad = base.FirstBy(x => x.IdSubAreaCapacitacion == item && x.IdCampaniaGeneralDetalle == Id);

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ExistFunction(int data, int id)
        {

            return base.Exist(x => x.IdSubAreaCapacitacion == data && x.IdCampaniaGeneralDetalle == id);
        }

        #region Metodos Base
        private TCampaniaGeneralDetalleSubArea MapeoEntidad(CampaniaGeneralDetalleSubArea entidad)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralDetalleSubArea modelo = _mapper.Map<TCampaniaGeneralDetalleSubArea>(entidad);

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

        public TCampaniaGeneralDetalleSubArea Add(CampaniaGeneralDetalleSubArea entidad)
        {
            try
            {
                var CampaniaGeneralDetalleSubArea = MapeoEntidad(entidad);
                base.Insert(CampaniaGeneralDetalleSubArea);
                return CampaniaGeneralDetalleSubArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCampaniaGeneralDetalleSubArea Add(TCampaniaGeneralDetalleSubArea entidad)
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

        public TCampaniaGeneralDetalleSubArea Update(CampaniaGeneralDetalleSubArea entidad)
        {
            try
            {
                var CampaniaGeneralDetalleSubArea = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CampaniaGeneralDetalleSubArea.RowVersion = entidadExistente.RowVersion;

                base.Update(CampaniaGeneralDetalleSubArea);
                return CampaniaGeneralDetalleSubArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCampaniaGeneralDetalleSubArea UpdateByEntity(TCampaniaGeneralDetalleSubArea entidad)
        {
            try
            {
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                entidad.RowVersion = entidadExistente.RowVersion;

                base.Update(entidad);
                return entidad;
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


        public IEnumerable<TCampaniaGeneralDetalleSubArea> Add(IEnumerable<CampaniaGeneralDetalleSubAreaDTO> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneralDetalleSubArea> listado = new List<TCampaniaGeneralDetalleSubArea>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(_mapper.Map<TCampaniaGeneralDetalleSubArea>(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TCampaniaGeneralDetalleSubArea> Add(IEnumerable<CampaniaGeneralDetalleSubArea> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneralDetalleSubArea> listado = new List<TCampaniaGeneralDetalleSubArea>();
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

        public IEnumerable<TCampaniaGeneralDetalleSubArea> Update(IEnumerable<CampaniaGeneralDetalleSubArea> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneralDetalleSubArea> listado = new List<TCampaniaGeneralDetalleSubArea>();
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

        public TCampaniaGeneralDetalleSubArea Add(CampaniaGeneralDetalleSubAreaDTO entidad)
        {
            try
            {   
                var CampaniaGeneralDetalleSubArea = _mapper.Map<TCampaniaGeneralDetalleSubArea>(entidad);
                base.Insert(CampaniaGeneralDetalleSubArea);
                return CampaniaGeneralDetalleSubArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        public void EliminacionLogicoPorCampaniaGeneral(int idCampaniaGeneralDetalle, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdCampaniaGeneralDetalle == idCampaniaGeneralDetalle && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdSubAreaCapacitacion));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
