using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using System.Data;
using System.Text.RegularExpressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class FacturamaRepository : IFacturamaRepository
    {
        private IDapperRepository _dapperRepository;
        public FacturamaRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public FacturamaCredencialesDTO ObtenerCredencialesActivas()
        {
            try
            {
                string query = @"
                    SELECT 
                        UserName,
                        IssuerEmail,
                        Password,
                        Sandbox,
                        Estado
                    FROM mkt.T_FacturamaCredencialApi 
                    WHERE Estado = 1 and Sandbox = 0";

                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FacturamaCredencialesDTO>(resultado);
                }

                return null; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegimenFiscalDTO> ObtenerListaRegimenFiscal()
        {
            try
            {
                List<RegimenFiscalDTO> listaRegimenFiscal = new List<RegimenFiscalDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT Id, Clave As FiscalRegime, Descripcion FROM mkt.T_FacturamaRegimenFiscal WHERE Estado = 1 ORDER BY Descripcion ASC", new { });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    listaRegimenFiscal = JsonConvert.DeserializeObject<List<RegimenFiscalDTO>>(resultado);
                }

                return listaRegimenFiscal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormapagoFacturamaDTO> ObtenerFormapagoFacturama()
        {
            try
            {
                List<FormapagoFacturamaDTO> listaUsoCfdi = new List<FormapagoFacturamaDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT Id, Clave As PaymentForm, Descripcion FROM mkt.T_FacturamaFormaPago WHERE Estado = 1 ORDER BY Descripcion ASC", new { });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    listaUsoCfdi = JsonConvert.DeserializeObject<List<FormapagoFacturamaDTO>>(resultado);
                }

                return listaUsoCfdi;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsoCfdiDTO> ObtenerListaUsoCfdi()
        {
            try
            {
                List<UsoCfdiDTO> listaUsoCfdi = new List<UsoCfdiDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT Id, Clave As CfdiUse, Descripcion FROM mkt.T_FacturamaUsoCfdi WHERE Estado = 1 ORDER BY Descripcion ASC", new { });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    listaUsoCfdi = JsonConvert.DeserializeObject<List<UsoCfdiDTO>>(resultado);
                }

                return listaUsoCfdi;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizaEnviadoFacturama(int id, String UsuarioModificacion)
        {
            try
            {
                _dapperRepository.QuerySPFirstOrDefault("[fin].[SP_CronogramaPagoDetalleFinal_ActualizarFacturama]", new { id, UsuarioModificacion });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public string InsertarRegimenFiscal(string clave, string descripcion, string usuario)
        {
            try
            {
                var query = "[mkt].[SP_FacturamaRegimenFiscal_Insertar]";

                var parametros = new
                {
                    Clave = clave,
                    Usuario = usuario,
                    Descripcion = descripcion
                };


                var resultado = _dapperRepository.QuerySPDapper(query, parametros);


                Console.WriteLine(" El registro se inserto Correctamente.");
                return resultado; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar Registro", ex);
            }
        }

        public bool ActualizarRegimenFiscal(int id, string clave, string descripcion, string usuario)
        {
            try
            {
                var query = "[mkt].[SP_FacturamaRegimenFiscal_Actualizar]";
                var parametros = new
                {
                    Id = id,
                    Clave = clave,
                    Descripcion = descripcion,
                    Usuario = usuario
                };

                _dapperRepository.QuerySPDapper(query, parametros); 
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el régimen fiscal", ex);
            }
        }

        public bool EliminarRegimenFiscal(int id, string usuario)
        {
            try
            {
                var query = "[mkt].[SP_FacturamaRegimenFiscal_Eliminar]";
                var parametros = new { Id = id, UsuarioModificacion = usuario };
                _dapperRepository.QuerySPDapper(query, parametros);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string InsertarUsoComprobante(string clave, string descripcion, string usuario)
        {
            try
            {
                var query = "[mkt].[SP_FacturamaUsoCfdi_Insertar]";

                var parametros = new
                {
                    Clave = clave,
                    Usuario = usuario,
                    Descripcion = descripcion
                };


                var resultado = _dapperRepository.QuerySPDapper(query, parametros);


                Console.WriteLine(" El registro se inserto Correctamente.");
                return resultado; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar Registro", ex);
            }
        }

        public bool ActualizarUsoComprobante(int id, string clave, string descripcion, string usuario)
        {
            try
            {
                var query = "[mkt].[SP_FacturamaUsoCfdi_Actualizar]";
                var parametros = new
                {
                    Id = id,
                    Clave = clave,
                    Descripcion = descripcion,
                    Usuario = usuario
                };

                _dapperRepository.QuerySPDapper(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el régimen fiscal", ex);
            }
        }
        public bool EliminarUsoComprobante(int id, string usuario)
        {
            try
            {
                var query = "[mkt].[SP_FacturamaUsoCfdi_Eliminar]";
                var parametros = new { Id = id, UsuarioModificacion = usuario };
                _dapperRepository.QuerySPDapper(query, parametros);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<ResumenMatriculaDTO> ObtenerResumenMatriculas(FiltroFechaDTO filtro)
        {
            try
            {
                List<ResumenMatriculaDTO> items = new List<ResumenMatriculaDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_Reporte_ResumenMatricula]", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    CodigoMatricula = filtro.CodigoMatricula,
                    IdCodigoPais = filtro.IdCodigoPais
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ResumenMatriculaDTO>>(query);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



      
        public int InsertarCliente(FacturamaClienteDTO cliente, string usuario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("Rfc", cliente.Rfc);
            parametros.Add("Nombre", cliente.Name);
            parametros.Add("Email", cliente.Email);
            parametros.Add("RegimenFiscal", cliente.FiscalRegime);
            parametros.Add("CfdiUse", cliente.CfdiUse);
            parametros.Add("UsuarioCreacion", usuario);
            parametros.Add("UsuarioModificacion", usuario);
            parametros.Add("IdFacturamaCliente", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _dapperRepository.QuerySPDapper("fin.SP_FacturamaCliente_Insertar", parametros);

            return parametros.Get<int>("IdFacturamaCliente");
        }



        public void InsertarDireccionCliente(FacturamaAddressDTO direccion, int idFacturamaCliente, string usuario)
        {
            var parametros = new
            {
                IdFacturamaCliente = idFacturamaCliente,
                Calle = direccion.Street,
                NumeroExterior = direccion.ExteriorNumber,
                NumeroInterior = direccion.InteriorNumber,
                Colonia = direccion.Neighborhood,
                CodigoPostal = direccion.ZipCode,
                Municipio = direccion.Municipality,
                EstadoDireccion = direccion.State,
                //Pais = direccion.Country,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario
            };
            _dapperRepository.QuerySPDapper("fin.SP_FacturamaClienteDireccion_Insertar", parametros);
        }

        public int InsertarFactura(FacturamaFacturaDTO factura, int idFacturamaCliente, string codigoMatricula, int idCronogramaPagoDetalleFinal,string usuario)
        {
            DateTime fechaEnvio;

            if (string.IsNullOrEmpty(factura.Date))
                fechaEnvio = DateTime.Now;
            else
            {
                if (!DateTime.TryParse(factura.Date, out fechaEnvio))
                    fechaEnvio = DateTime.Now;
            }

            var parametros = new DynamicParameters();
            parametros.Add("IdFacturamaCliente", idFacturamaCliente);
            //parametros.Add("CodigoMatricula", codigoMatricula);
            parametros.Add("IdCronogramaPagoDetalleFinal", idCronogramaPagoDetalleFinal);
            parametros.Add("CfdiType", factura.CfdiType);
            parametros.Add("Currency", factura.Currency);
            parametros.Add("PaymentForm", factura.PaymentForm);
            parametros.Add("PaymentMethod", factura.PaymentMethod);
            parametros.Add("ExpeditionPlace", factura.ExpeditionPlace);
            parametros.Add("UsuarioCreacion", usuario);
            parametros.Add("UsuarioModificacion", usuario);
            parametros.Add("Periodicity", factura.GlobalInformation?.Periodicity);
            parametros.Add("Months", factura.GlobalInformation?.Months);
            parametros.Add("Year", factura.GlobalInformation?.Year);
            parametros.Add("IdFacturamaFactura", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("FechaEnvio", fechaEnvio);

            _dapperRepository.QuerySPDapper("fin.SP_FacturamaFactura_Insertar", parametros);

            return parametros.Get<int>("IdFacturamaFactura");
        }


      
        public int InsertarItemFactura(FacturamaItemDTO item, int idFacturamaFactura, string usuario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("IdFacturamaFactura", idFacturamaFactura);
            parametros.Add("ProductCode", item.ProductCode);
            parametros.Add("Description", item.Description);
            parametros.Add("UnitCode", item.UnitCode);
            parametros.Add("Quantity", item.Quantity);
            parametros.Add("Unit", item.Unit);
            parametros.Add("UnitPrice", item.UnitPrice);
            parametros.Add("Subtotal", item.Subtotal);
            parametros.Add("TaxObject", item.TaxObject);
            parametros.Add("Total", item.Total);
            parametros.Add("UsuarioCreacion", usuario);
            parametros.Add("UsuarioModificacion", usuario);
            parametros.Add("IdFacturaFacturamaItem", dbType: DbType.Int32, direction: ParameterDirection.Output); 

            _dapperRepository.QuerySPDapper("fin.SP_FacturamaFacturaItem_Insertar", parametros);

            return parametros.Get<int>("IdFacturaFacturamaItem"); 
        }


        public void InsertarImpuestoItem(FacturamaTaxDTO tax, int idFacturamaFacturaItem, string usuario)
        {
            var parametros = new
            {
                IdFacturamaFacturaItem = idFacturamaFacturaItem,
                Nombre = tax.Name,
                tax.Base,
                tax.Rate,
                tax.IsRetention,
                tax.Total,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario
            };
            _dapperRepository.QuerySPDapper("fin.SP_FacturamaFacturaItemImpuesto_Insertar", parametros);
        }
       

        public FacturamaFacturaClienteDTO ObtenerDatosFacturaClientePorId(int idFactura)
        {
            var resultado = _dapperRepository.QuerySPDapper(
                "[fin].[SP_FacturamaFacturaCliente_ObtenerPorId]",
                new { Id = idFactura });

            if (string.IsNullOrEmpty(resultado) || resultado == "null")
                throw new Exception("No se encontró información para la factura solicitada.");

            
            var contenedor = JsonConvert.DeserializeObject<JArray>(resultado)?[0];
            if (contenedor == null)
                throw new Exception("Formato JSON inválido.");

            var facturaJson = contenedor["factura"]?.ToString();
            var clienteJson = contenedor["cliente"]?.ToString();

            var dto = new FacturamaFacturaClienteDTO
            {
                factura = JsonConvert.DeserializeObject<FacturamaFacturaDTO>(facturaJson),
                cliente = JsonConvert.DeserializeObject<FacturamaClienteDTO>(clienteJson)
            };

            return dto;
        }


        public int ObtenerIdCronogramaPorIdFactura(int idFactura)
        {
            try
            {
                var sql = @"
            SELECT IdCronogramaPagoDetalleFinal 
            FROM fin.T_FacturamaFactura 
            WHERE Id = @idFactura";

                var resultado = _dapperRepository.QueryDapper(sql, new { idFactura });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var lista = JsonConvert.DeserializeObject<List<CornogramaFacturmaDTO>>(resultado);
                    return lista.FirstOrDefault()?.IdCronogramaPagoDetalleFinal ?? 0;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarFacturaComoEnviada(int idFactura, string uuid, string ? cfdiId, DateTime? fechaEnvio, string usuario)
        {
            var parametros = new
            {
                Id = idFactura,
                UUID = uuid,
                CfdiId = cfdiId,
                FechaEnvio = fechaEnvio,
                UsuarioModificacion = usuario
            };

            _dapperRepository.QuerySPDapper("fin.SP_FacturamaFactura_ActualizarUUIDEnvio", parametros);
        }
        public FacturamaFacturaClienteCronogrmaDTO ObtenerFacturaClientePorCodigoMatricula(string codigoMatricula)
        {
            var resultado = _dapperRepository.QuerySPDapper(
                "fin.SP_ObtenerFacturaClientePorCodigoMatricula",
                new { CodigoMatricula = codigoMatricula }
            );

            if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                return null;

            var lista = JsonConvert.DeserializeObject<List<dynamic>>(resultado);

            if (lista == null || lista.Count == 0)
                return null;

            var primer = lista[0];

            var dto = new FacturamaFacturaClienteCronogrmaDTO
            {
                cliente = new FacturamaClienteDTO
                {
                    Rfc = primer.Rfc,
                    Name = primer.Nombre,
                    Email = primer.Email,
                    FiscalRegime = primer.RegimenFiscal,
                    CfdiUse = primer.CfdiUse,
                    Address = new FacturamaAddressDTO
                    {
                        Street = primer.Street,
                        ExteriorNumber = primer.NumeroExterior,
                        InteriorNumber = primer.NumeroInterior,
                        Neighborhood = primer.Neighborhood,
                        ZipCode = primer.ZipCode,
                        Municipality = primer.Municipio,
                        State = primer.EstadoDireccion,
                        // Country = primer.Pais
                    }
                },
                factura = new FacturamaFacturaDTO
                {
                    CfdiType = primer.CfdiType,
                    Currency = primer.Currency,
                    PaymentForm = primer.PaymentForm,
                    PaymentMethod = primer.PaymentMethod,
                    ExpeditionPlace = primer.ExpeditionPlace,
                    Receiver = new FacturamaReceiverDTO
                    {
                        Rfc = primer.Rfc,
                        Name = primer.Nombre,
                        CfdiUse = primer.CfdiUse,
                        FiscalRegime = primer.RegimenFiscal,
                        TaxZipCode = primer.ZipCode
                    },
                    Items = lista.Select(x => new FacturamaItemDTO
                    {
                        ProductCode = x.ProductCode,
                        Description = x.Description,
                        UnitCode = x.UnitCode,

                        Quantity = x.Quantity as double?,
                        Unit = x.Unit,
                        UnitPrice = x.UnitPrice as double?,
                        Subtotal = x.Subtotal as double?,
                        TaxObject = x.TaxObject,
                        Total = x.Total as double?,
                        Taxes = string.IsNullOrEmpty((string)x.TaxName) ? new List<FacturamaTaxDTO>() : new List<FacturamaTaxDTO>
         {
             new FacturamaTaxDTO
             {
                 Name = x.TaxName,

                  Base = x.Base as double?,
                     Rate = x.Rate as double?,
                     IsRetention = x.IsRetention ?? false,
                     Total = x.Total as double?
             }
         }
                    }).ToList()
                },
                IdCronogramaPagoDetalleFinal = primer.IdCronogramaPagoDetalleFinal

            };

            return dto;
        }

        public int ObtenerIdFacturaPorCodigoMatricula(string codigoMatricula)
        {
            var result = _dapperRepository.QuerySPFirstOrDefault("[fin].[SP_FacturamaFactura_ObtenerIdPorMatricula]", new { codigoMatricula });

            var match = Regex.Match(result?.ToString() ?? "", "\"Id\"\\s*:\\s*(\\d+)");
            if (match.Success)
                return int.Parse(match.Groups[1].Value);

            return 0;
        }





        public List<FacturamaFacturaMasivoDTO> ObtenerFacturasPendientesEnvio()
        {
            
            
            var resultado = _dapperRepository.QueryDapper("SELECT * FROM fin.V_FacturamaFactura_PendienteEnvio", null);

            if (string.IsNullOrWhiteSpace(resultado))
                return new List<FacturamaFacturaMasivoDTO>();

            var array = JArray.Parse(resultado);

            var lista = array.Select(item => new FacturamaFacturaMasivoDTO
            {
                IdFactura = (int)item["IdFacturamaFactura"],
                IdCliente = (int)item["IdFacturamaCliente"],
                CodigoMatricula = (string)item["CodigoMatricula"],
                EstadoEnvio = item["EstadoEnvio"]?.Value<bool?>() ?? false,
                Identificador = (string)item["Identificador"] ?? "",
                Pais = (string?)item["Pais"],
                Monto = (decimal?)item["Monto"] ?? 0,
                Nombre = (string)item["Nombre"] ?? "",

                ApiDestino = (string?)item["ApiDestino"]
            }).ToList();

            return lista;
        }
        public FacturamaFacturaCronogramaDetalleDTO ObtenerDetalleFacturaFacturamaCronograma(int IdFacturamaFactura)
        {
            try
            {
                FacturamaFacturaCronogramaDetalleDTO DetalleFactura = new FacturamaFacturaCronogramaDetalleDTO();

                var resultado = _dapperRepository.FirstOrDefault("SELECT IdFacturamaFactura,IdCronogramaPagoDetalleFinal,IdMatriculaCabecera,CodigoMatricula,IdAlumno,NombreCompletoAlumno,DescripcionPago,MontoPago FROM fin.V_Facturama_CronogramaMatriculaAlumno WHERE IdFacturamaFactura=@IdFacturamaFactura", new { IdFacturamaFactura = IdFacturamaFactura });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    DetalleFactura = JsonConvert.DeserializeObject<FacturamaFacturaCronogramaDetalleDTO>(resultado);
                }

                return DetalleFactura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int ObtenerIdAlumnoPorIdCronograma(int idCronograma)
        {
            try
            {
                var query = @"
            SELECT TOP 1 IdAlumno
            FROM fin.V_ObtenerCronogramaAlumno
            WHERE IdCronogramaPagoDetalleFinal = @id";

                var resultado = _dapperRepository.QueryDapper(query, new { id = idCronograma });

                if (!string.IsNullOrWhiteSpace(resultado) && resultado != "null")
                {
                    var json = JArray.Parse(resultado);
                    var idAlumno = (int)json[0]["IdAlumno"];
                    return idAlumno;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener IdAlumno: {ex.Message}", ex);
            }
        }


        public void InsertarActualizarFacturamaAlumno(int idAlumno, FacturamaClienteDTO cliente, FacturamaFacturaDTO factura, string usuario)
        {

            string claveFormaPago = factura.PaymentForm;

            var resultado = _dapperRepository.QueryDapper(
                "SELECT TOP 1 Id FROM mkt.T_FacturamaFormaPago WHERE Clave = @Clave AND Estado = 1",
                new { Clave = claveFormaPago }
            );

            int? idFormaPago = null;
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                var lista = JsonConvert.DeserializeObject<List<IdTemporalDTO>>(resultado);
                idFormaPago = lista.FirstOrDefault()?.Id;
            }

            var parametros = new
            {
                IdAlumno = idAlumno,
                Rfc = cliente.Rfc,
                ClaveFacturamaRegimenFiscal = cliente.FiscalRegime,
                ClaveFacturamaUsoCfdi = cliente.CfdiUse,
                CodigoPostal = cliente.Address.ZipCode,
                IdMunicipioMexico = (int?)null,
                Calle = cliente.Address.Street,
                Colonia = cliente.Address.Neighborhood,
                RazonSocial = cliente.Name,
                NumeroExterior = cliente.Address.ExteriorNumber,
                NumeroInterior = cliente.Address.InteriorNumber,
                IdFacturamaFormaPago = idFormaPago,
                Usuario = usuario
            };

            _dapperRepository.QuerySPDapper("pw.SP_FacturamaAlumno_Insertar", parametros);
        }



        public bool ExisteFacturaConfigurada(int idCronogramaPagoDetalleFinal)
        {
            string query = @"
                            SELECT 1 
                            FROM fin.V_FacturamaFactura_PendienteEnvio 
                            WHERE IdCronogramaPagoDetalleFinal = @id";

            var result = _dapperRepository.FirstOrDefault(query, new { id = idCronogramaPagoDetalleFinal });
            return !string.IsNullOrEmpty(result) && result != "null";
        }

        public bool EliminarFacturasPendientesFacturama(List<int> idsFacturas, string usuario)
        {
            try
            {
                if (idsFacturas == null || !idsFacturas.Any())
                    return false;
                
                const string query = @"UPDATE fin.T_FacturamaFactura
                                          SET Estado = 0, UsuarioModificacion = @usuario, FechaModificacion = GETDATE()
                                          WHERE Id IN @ids AND Estado = 1 AND EstadoEnvio = 0";
                var parametros = new { ids = idsFacturas, usuario };
                var filasAfectadas = _dapperRepository.QueryDapper(query, parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
    