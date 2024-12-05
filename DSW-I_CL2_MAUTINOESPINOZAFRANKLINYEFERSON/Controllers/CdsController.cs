using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

using DSW_I_CL2_MAUTINOESPINOZAFRANKLINYEFERSON.Models;

namespace DSW_I_CL2_MAUTINOESPINOZA.Controllers
{
    public class CdsController : Controller
    {
        private readonly string connectionString = "Server=.;Database=CL2MAUTINO;Trusted_Connection=True;";

        // Acción para listar registros
        public IActionResult Index()
        {
            List<CalcularMautino> cds = new List<CalcularMautino>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_cdsmautino", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cds.Add(new CalcularMautino
                    {
                        Idcd = Convert.ToInt32(reader["Idcd"]),
                        Cliente = reader["Cliente"].ToString(),
                        CapaDiscoGB = Convert.ToInt32(reader["CapaDisco"]),
                        CapaCdMB = Convert.ToInt32(reader["CapaCds"]),
                        TotalCds = Convert.ToInt32(reader["TotalCds"])
                    });
                }
            }
            return View(cds);
        }

        // Acción para mostrar el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // Acción para guardar un nuevo registro
        [HttpPost]
        public IActionResult Create(CalcularMautino model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_guardar_cdsmautino", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cliente", model.Cliente);
                cmd.Parameters.AddWithValue("@CapaDisco", model.CapaDiscoGB);
                cmd.Parameters.AddWithValue("@CapaCds", model.CapaCdMB);
                cmd.Parameters.AddWithValue("@TotalCds", model.TotalCds);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // Acción para eliminar un registro
        public IActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminar_cdsmautino", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcd", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // Acción para mostrar el formulario de edición
        public IActionResult Edit(int id)
        {
            CalcularMautino model = new CalcularMautino();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_buscar_cdsmautino", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcd", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    model.Idcd = Convert.ToInt32(reader["Idcd"]);
                    model.Cliente = reader["Cliente"].ToString();
                    model.CapaDiscoGB = Convert.ToInt32(reader["CapaDisco"]);
                    model.CapaCdMB = Convert.ToInt32(reader["CapaCds"]);
                    model.TotalCds = Convert.ToInt32(reader["TotalCds"]);
                }
            }
            return View(model);
        }

        // Acción para actualizar un registro
        [HttpPost]
        public IActionResult Edit(CalcularMautino model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_cdsmautino", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Idcd", model.Idcd);
                cmd.Parameters.AddWithValue("@Cliente", model.Cliente);
                cmd.Parameters.AddWithValue("@CapaDisco", model.CapaDiscoGB);
                cmd.Parameters.AddWithValue("@CapaCds", model.CapaCdMB);
                cmd.Parameters.AddWithValue("@TotalCds", model.TotalCds);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
