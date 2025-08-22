using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FaseByPlantillaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_FaseByPlantilla
    /// </summary>
    public class FaseByPlantillaRepository : GenericRepository<TFaseByPlantilla>, IFaseByPlantillaRepository
    {
        private Mapper _mapper;

        public FaseByPlantillaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFaseByPlantilla, FaseByPlantilla>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFaseByPlantilla MapeoEntidad(FaseByPlantilla entidad)
        {
            try
            {
                //crea la entidad padre
                TFaseByPlantilla modelo = _mapper.Map<TFaseByPlantilla>(entidad);

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

        public TFaseByPlantilla Add(FaseByPlantilla entidad)
        {
            try
            {
                var FaseByPlantilla = MapeoEntidad(entidad);
                base.Insert(FaseByPlantilla);
                return FaseByPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFaseByPlantilla Update(FaseByPlantilla entidad)
        {
            try
            {
                var FaseByPlantilla = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FaseByPlantilla.RowVersion = entidadExistente.RowVersion;

                base.Update(FaseByPlantilla);
                return FaseByPlantilla;
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


        public IEnumerable<TFaseByPlantilla> Add(IEnumerable<FaseByPlantilla> listadoEntidad)
        {
            try
            {
                List<TFaseByPlantilla> listado = new List<TFaseByPlantilla>();
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

        public IEnumerable<TFaseByPlantilla> Update(IEnumerable<FaseByPlantilla> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFaseByPlantilla> listado = new List<TFaseByPlantilla>();
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

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Fases Plantilla asociados a una Plantilla
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantilla == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdFaseOrigen));
                Delete(listaBorrar.Select(x => x.Id), usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public FaseByPlantilla ObtenerPorIdOrigenYPorIdPlantilla(int idFaseOrigen, int idPlantilla)
        {
            try
            {
                FaseByPlantilla rpta = new FaseByPlantilla();
                var query = @"SELECT Id,
                                   IdPlantilla,
                                   IdFaseOrigen,
                                   NombreFase,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM mkt.T_FaseByPlantilla
                            WHERE Estado = 1
                                  AND IdFaseOrigen = @IdFaseOrigen
                                  AND IdPlantilla = @IdPlantilla";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdFaseOrigen = idFaseOrigen, IdPlantilla = idPlantilla });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<FaseByPlantilla>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
