using Microsoft.Data.SqlClient;
using AppDC_Chipagua.Models;

namespace AppDC_Chipagua.Repository
{
    public class ReclamoService(IConfiguration configuration)
    {
        private readonly string? _connetionString = configuration.GetConnectionString("DefaultConnection");


        public async Task<List<Reclamo>> GetReclamosAsync()
        {
            var reclamos = new List<Reclamo>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connetionString);
                string query = "Select * from t_reclamos";

                SqlCommand cmd = new SqlCommand(query, conn);
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
    }
}
