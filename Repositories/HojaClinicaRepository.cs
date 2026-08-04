using Microsoft.Data.SqlClient;
using System.Data;
using VeterinariaAPI.Data;
using VeterinariaAPI.Models;

namespace VeterinariaAPI.Repositories
{
    public class HojaClinicaRepository
    {
        private readonly ConexionBD _conexion;

        public HojaClinicaRepository(ConexionBD conexion)
        {
            _conexion = conexion;
        }
        public List<HojaClinica> LeerHojaClinica()
        {
            List<HojaClinica> lista = new();

            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LeerHojaClinica", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new HojaClinica
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FechaAtencion = Convert.ToDateTime(dr["FechaAtencion"]),
                        Sintomas = dr["Sintomas"].ToString(),
                        Diagnostico = dr["Diagnostico"].ToString(),
                        Tratamiento = dr["Tratamiento"].ToString(),
                        MascotaId = Convert.ToInt32(dr["MascotaId"]),
                        FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                        UsuarioCreacion = dr["UsuarioCreacion"].ToString(),
                        FechaModificacion = dr["FechaModificacion"] == DBNull.Value ? null : Convert.ToDateTime(dr["FechaModificacion"]),
                        UsuarioModificacion = dr["UsuarioModificacion"] == DBNull.Value ? null : dr["UsuarioModificacion"].ToString()
                    });
                }
            }

            return lista;
        }
        public HojaClinica? BuscarPorId(int id)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_BuscarHojaClinicaPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new HojaClinica
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FechaAtencion = Convert.ToDateTime(dr["FechaAtencion"]),
                        Sintomas = dr["Sintomas"].ToString(),
                        Diagnostico = dr["Diagnostico"].ToString(),
                        Tratamiento = dr["Tratamiento"].ToString(),
                        MascotaId = Convert.ToInt32(dr["MascotaId"]),
                        FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                        UsuarioCreacion = dr["UsuarioCreacion"].ToString(),
                        FechaModificacion = dr["FechaModificacion"] == DBNull.Value ? null : Convert.ToDateTime(dr["FechaModificacion"]),
                        UsuarioModificacion = dr["UsuarioModificacion"] == DBNull.Value ? null : dr["UsuarioModificacion"].ToString()
                    };
                }
            }

            return null;
        }
        public int Crear(HojaClinica hoja)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_CrearHojaClinica", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FechaAtencion", hoja.FechaAtencion);
                cmd.Parameters.AddWithValue("@Sintomas", hoja.Sintomas);
                cmd.Parameters.AddWithValue("@Diagnostico", hoja.Diagnostico);
                cmd.Parameters.AddWithValue("@Tratamiento", hoja.Tratamiento);
                cmd.Parameters.AddWithValue("@MascotaId", hoja.MascotaId);
                cmd.Parameters.AddWithValue("@UsuarioCreacion", hoja.UsuarioCreacion);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public void Modificar(HojaClinica hoja)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_ModificarHojaClinica", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", hoja.Id);
                cmd.Parameters.AddWithValue("@FechaAtencion", hoja.FechaAtencion);
                cmd.Parameters.AddWithValue("@Sintomas", hoja.Sintomas);
                cmd.Parameters.AddWithValue("@Diagnostico", hoja.Diagnostico);
                cmd.Parameters.AddWithValue("@Tratamiento", hoja.Tratamiento);
                cmd.Parameters.AddWithValue("@MascotaId", hoja.MascotaId);
                cmd.Parameters.AddWithValue("@UsuarioModificacion", hoja.UsuarioModificacion);

                cmd.ExecuteNonQuery();
            }
        }
        public void Eliminar(int id)

        {

            using (SqlConnection conn = _conexion.ObtenerConexion())

            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_EliminarHojaClinica", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();

            }

        }
    }
}
