using Microsoft.Data.SqlClient;
using AppDC_Chipagua.Models;

namespace AppDC_Chipagua.Repository
{
    public class ReclamoService(IConfiguration configuration)
    {
        private readonly string? _connetionString = configuration.GetConnectionString("DefaultConnection");

        
        public async Task<List<Reclamo>> GetReclamosAsync(string direccionMonto, decimal monto)
        {
            var reclamos = new List<Reclamo>();
            try
            {
                using SqlConnection conn = new (_connetionString);
                string operador = direccionMonto == "Mayor a" ? ">" : "<";
                string query = $"SELECT * FROM t_reclamos WHERE montoReclamado {operador} @monto";

                using SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@monto", monto);
                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync()) {
                    reclamos.Add(new Reclamo() {
                        IdReclamo = Convert.ToInt32(reader["idReclamo"]),
                        NombreProveedor = reader["nombreProveedor"].ToString(),
                        DireccionProveedor = reader["direccionProveedor"].ToString(),
                        NombresConsumidor = reader["nombresConsumidor"].ToString(),
                        ApellidosConsumidor = reader["apellidosConsumidor"].ToString(),
                        DUI = reader["DUI"].ToString(),
                        DetalleReclamo = reader["detalleReclamo"].ToString(),
                        MontoReclamado = Convert.ToDecimal(reader["montoReclamado"]),
                        Telefono = reader["telefono"].ToString(),
                        FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]),
                    });
                }

            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }
            return reclamos;
        }






        public async Task<bool> GetDUIExistAsync(string DUI)
        {
            try
            {
                using SqlConnection conn = new (_connetionString);
                string query = "SELECT idReclamo FROM t_reclamos WHERE DUI = @duiReclamo";

                SqlCommand cmd = new (query, conn);
                cmd.Parameters.AddWithValue("@duiReclamo", DUI);
                await conn.OpenAsync();

                int affectedRows =  Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return true;
            }
        }




        public async Task AddReclamoAsync(Reclamo reclamo)
        {
            try
            {
                using SqlConnection conn = new(_connetionString);
                string query = "INSERT INTO t_reclamos   (nombreProveedor" +
                    ",direccionProveedor" +
                    ",nombresConsumidor" +
                    ",apellidosConsumidor" +
                    ",DUI" +
                    ",detalleReclamo" +
                    ",montoReclamado" +
                    ",telefono" +
                    ",fechaIngreso)  VALUES(@nombreProveedor" +
                    ",@direccionProveedor" +
                    ",@nombresConsumidor" +
                    ",@apellidosConsumidor" +
                    ",@DUI" +
                    ",@detalleReclamo" +
                    ",@montoReclamado" +
                    ",@telefono" +
                    ",@fechaIngreso)";

                SqlCommand cmd = new(query, conn);
                var paramsQuery = new Dictionary<string, object>
            {
                { "@nombreProveedor", reclamo.NombreProveedor},
                { "@direccionProveedor", reclamo.DireccionProveedor },
                { "@nombresConsumidor", reclamo.NombresConsumidor },
                { "@apellidosConsumidor", reclamo.ApellidosConsumidor },
                { "@DUI", reclamo.DUI },
                { "@detalleReclamo", reclamo.DetalleReclamo },
                { "@montoReclamado", reclamo.MontoReclamado },
                { "@telefono", reclamo.Telefono },
                { "@fechaIngreso", reclamo.FechaIngreso }
            };

                foreach (var item in paramsQuery)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }

}
