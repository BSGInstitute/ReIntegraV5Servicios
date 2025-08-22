using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FacebookFormularioLeadgenRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_FacebookFormularioLeadgen
    /// </summary>
    public class FacebookFormularioLeadgenRepository : GenericRepository<TFacebookFormularioLeadgen>, IFacebookFormularioLeadgenRepository
    {
        private Mapper _mapper;

        public FacebookFormularioLeadgenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFacebookFormularioLeadgen, FacebookFormularioLeadgen>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFacebookFormularioLeadgen MapeoEntidad(FacebookFormularioLeadgen entidad)
        {
            try
            {
                //crea la entidad padre
                TFacebookFormularioLeadgen modelo = _mapper.Map<TFacebookFormularioLeadgen>(entidad);

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

        public TFacebookFormularioLeadgen Add(FacebookFormularioLeadgen entidad)
        {
            try
            {
                var FacebookFormularioLeadgen = MapeoEntidad(entidad);
                base.Insert(FacebookFormularioLeadgen);
                return FacebookFormularioLeadgen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFacebookFormularioLeadgen Update(FacebookFormularioLeadgen entidad)
        {
            try
            {
                var FacebookFormularioLeadgen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FacebookFormularioLeadgen.RowVersion = entidadExistente.RowVersion;

                base.Update(FacebookFormularioLeadgen);
                return FacebookFormularioLeadgen;
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


        public IEnumerable<TFacebookFormularioLeadgen> Add(IEnumerable<FacebookFormularioLeadgen> listadoEntidad)
        {
            try
            {
                List<TFacebookFormularioLeadgen> listado = new List<TFacebookFormularioLeadgen>();
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

        public IEnumerable<TFacebookFormularioLeadgen> Update(IEnumerable<FacebookFormularioLeadgen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFacebookFormularioLeadgen> listado = new List<TFacebookFormularioLeadgen>();
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

        /// Autor: Margiory Ramirez
        /// Fecha: 23/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Ciudad para mexico.
        /// </summary>
        /// <returns><DatosCiudadDTO> </returns>
        public DatosCiudadDTO ObtenerDatosCiudadMexico(string nombre)
        {
            try
            {

                DatosCiudadDTO ciudad = null;


                string query = @"
                                    DECLARE @Id INT, @NombreCiudad VARCHAR(100), @IdPais INT;

                            SELECT 
                                @Id = C.Id, 
                                @NombreCiudad = C.Nombre, 
                                @IdPais = C.IdPais
                            FROM 
                                conf.T_Ciudad C
                            WHERE 
                                C.Nombre = @NombreParam 
                                AND C.Estado = 1;

                       
                            IF @Id IS NULL
                            BEGIN
                                SELECT 
                                    @Id = Ciudad.Id, 
                                    @NombreCiudad = Ciudad.Nombre, 
                                    @IdPais = Ciudad.IdPais
                                FROM 
                                    conf.T_CiudadMexico CM
                                INNER JOIN 
                                    conf.T_EstadoMexico EM ON CM.IdEstadoMexico = EM.Id
                                INNER JOIN 
                                    conf.T_Ciudad Ciudad ON EM.IdCiudad = Ciudad.Id
                                WHERE 
                                    CM.Nombre = @NombreParam
                                    AND Ciudad.Estado = 1;
                            END

                            SELECT 
                                ISNULL(@Id, NULL) AS Id, 
                                ISNULL(@NombreCiudad, NULL) AS NombreCiudad, 
                                ISNULL(@IdPais, NULL) AS IdPais WHERE @Id IS NOT NULL";
                var ciudadData = _dapperRepository.QueryDapper(query, new { NombreParam = nombre });
                if (!string.IsNullOrEmpty(ciudadData) && !ciudadData.Equals("[]"))
                {
                    ciudad = JsonConvert.DeserializeObject<List<DatosCiudadDTO>>(ciudadData)
                        [0];

                }
                return ciudad;

            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }



    }
}
