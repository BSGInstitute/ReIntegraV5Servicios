using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CriterioDocRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CriterioDoc
    /// </summary>
    public class CriterioDocRepository : GenericRepository<TCriterioDoc>, ICriterioDocRepository
    {
        private Mapper _mapper;

        public CriterioDocRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioDoc, CriterioDoc>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCriterioDoc MapeoEntidad(CriterioDoc entidad)
        {
            try
            {
                //crea la entidad padre
                TCriterioDoc modelo = _mapper.Map<TCriterioDoc>(entidad);

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

        public TCriterioDoc Add(CriterioDoc entidad)
        {
            try
            {
                var CriterioDoc = MapeoEntidad(entidad);
                base.Insert(CriterioDoc);
                return CriterioDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioDoc Update(CriterioDoc entidad)
        {
            try
            {
                var CriterioDoc = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CriterioDoc.RowVersion = entidadExistente.RowVersion;

                base.Update(CriterioDoc);
                return CriterioDoc;
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


        public IEnumerable<TCriterioDoc> Add(IEnumerable<CriterioDoc> listadoEntidad)
        {
            try
            {
                List<TCriterioDoc> listado = new List<TCriterioDoc>();
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

        public IEnumerable<TCriterioDoc> Update(IEnumerable<CriterioDoc> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioDoc> listado = new List<TCriterioDoc>();
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
      

        public List<ComboDTO> ObtenerTodoSeleccionar()
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT id, nombre, ModalidadPresencial, ModalidadOnline, ModalidadAonline FROM mkt.T_CriterioDoc WHERE estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ComboDTO> ObtenerCriterioModalidad(List<int> idModalidades)
        {
            try
            {
                var wheres = "";
                idModalidades = idModalidades.OrderBy(x => x).ToList();
                foreach (var item in idModalidades)
                {
                    if (item == 0)
                    {
                        wheres += " ModalidadPresencial = 1 ";
                    }
                    if (item == 1)
                    {
                        if (wheres.Length > 0)
                        {
                            wheres += "or";
                        }
                        wheres += " ModalidadAonline = 1 ";
                    }
                    if (item == 2)
                    {
                        if (wheres.Length > 0)
                        {
                            wheres += "or";
                        }
                        wheres += " ModalidadOnline = 1 ";
                    }
                }
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT id, nombre FROM fin.T_CriterioDoc  WHERE estado = 1 AND (" + wheres+")";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idModalidades });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
