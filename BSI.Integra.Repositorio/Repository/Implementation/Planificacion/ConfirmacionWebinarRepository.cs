using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ConfirmacionWebinarRepository : IConfirmacionWebinarRepository
    {
        
        private readonly IDapperRepository _dapperRepository;
        public ConfirmacionWebinarRepository(
        IntegraDBContext context,
        IConnectionFactory connectionFactory,
        IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public AsistenciaConfirmacionWebinarDTO? ObtenerConfirmacionWebinarPorIdMatriculaYIdSesion(int IdMatriculaCabecera, int IdPEspecificoSesion)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdMatriculaCabecera,
	                    IdPEspecificoSesion,
	                    Confirmo,
	                    Asistio,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    EnvioCorreoWebinar,
	                    EnvioWhatsAppWebinar
                    FROM pla.T_ConfirmacionWebinar
                    WHERE IdPEspecificoSesion=@IdPEspecificoSesion AND IdMatriculaCabecera=@IdMatriculaCabecera AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPEspecificoSesion, IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<AsistenciaConfirmacionWebinarDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        public bool Insertar(AsistenciaConfirmacionWebinarDTO obj)
        {
            try
            {
                var query = @"EXEC pla.SP_ConfirmacionWebinar_Insertar
                      @IdMatriculaCabecera,
                      @IdPEspecificoSesion,
                      @Confirmo,
                      @Asistio,
                      @UsuarioCreacion,
                      @EnvioCorreoWebinar,
                      @EnvioWhatsAppWebinar";

                var parametros = new
                {
                    IdMatriculaCabecera = obj.IdMatriculaCabecera,
                    IdPEspecificoSesion = obj.IdPEspecificoSesion,
                    Confirmo = obj.Confirmo,
                    Asistio = obj.Asistio,
                    UsuarioCreacion = obj.UsuarioCreacion,
                    EnvioCorreoWebinar = obj.EnvioCorreoWebinar,
                    EnvioWhatsAppWebinar = obj.EnvioWhatsAppWebinar
                };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Actualizar(AsistenciaConfirmacionWebinarDTO obj)
        {
            try
            {
                var query = @"EXEC pla.SP_ConfirmacionWebinar_Actualizar
                      @Id,
                      @Confirmo,
                      @UsuarioModificacion";
                var parametros = new
                {
                    Id = obj.Id,
                    Confirmo = obj.Confirmo,
                    UsuarioModificacion = obj.UsuarioModificacion
                };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}