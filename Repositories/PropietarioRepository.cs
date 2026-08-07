using Microsoft.Data.SqlClient;
using System.Data;
using VeterinariaAPI.Data;
using VeterinariaAPI.Models;

namespace VeterinariaAPI.Repositories
{
    public class PropietarioRepository
    {
        private readonly ConexionBD _conexion;

        public PropietarioRepository(ConexionBD conexion)
        {
            _conexion = conexion;
        }
        public List<Propietario> LeerPropietarios()
        {
            List<Propietario> lista = new List<Propietario>();

            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LeerPropietarios", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Propietario
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Identificacion = dr["Identificacion"].ToString(),
                        PrimerNombre = dr["PrimerNombre"].ToString(),
                        SegundoNombre = dr["SegundoNombre"].ToString(),
                        PrimerApellido = dr["PrimerApellido"].ToString(),
                        SegundoApellido = dr["SegundoApellido"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                        UsuarioCreacion = dr["UsuarioCreacion"].ToString(),
                        FechaModificacion = dr["FechaModificacion"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(dr["FechaModificacion"]),
                        UsuarioModificacion = dr["UsuarioModificacion"] == DBNull.Value
                            ? null
                            : dr["UsuarioModificacion"].ToString()
                    });
                }
            }

            return lista;
        }
        public Propietario? BuscarPorId(int id)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_BuscarPropietarioPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new Propietario
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Identificacion = dr["Identificacion"].ToString(),
                        PrimerNombre = dr["PrimerNombre"].ToString(),
                        SegundoNombre = dr["SegundoNombre"].ToString(),
                        PrimerApellido = dr["PrimerApellido"].ToString(),
                        SegundoApellido = dr["SegundoApellido"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                        UsuarioCreacion = dr["UsuarioCreacion"].ToString(),
                        FechaModificacion = dr["FechaModificacion"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(dr["FechaModificacion"]),
                        UsuarioModificacion = dr["UsuarioModificacion"] == DBNull.Value
                            ? null
                            : dr["UsuarioModificacion"].ToString()
                    };
                }

                return null;
            }
        }
        public int CrearPropietario(Propietario propietario)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_CrearPropietario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Identificacion", propietario.Identificacion);
                cmd.Parameters.AddWithValue("@PrimerNombre", propietario.PrimerNombre);
                cmd.Parameters.AddWithValue("@SegundoNombre", propietario.SegundoNombre);
                cmd.Parameters.AddWithValue("@PrimerApellido", propietario.PrimerApellido);
                cmd.Parameters.AddWithValue("@SegundoApellido", propietario.SegundoApellido);
                cmd.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                cmd.Parameters.AddWithValue("@Correo", propietario.Correo);
                cmd.Parameters.AddWithValue("@UsuarioCreacion", propietario.UsuarioCreacion);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public bool ModificarPropietario(Propietario propietario)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_ModificarPropietario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", propietario.Id);
                cmd.Parameters.AddWithValue("@Identificacion", propietario.Identificacion);
                cmd.Parameters.AddWithValue("@PrimerNombre", propietario.PrimerNombre);
                cmd.Parameters.AddWithValue("@SegundoNombre", propietario.SegundoNombre);
                cmd.Parameters.AddWithValue("@PrimerApellido", propietario.PrimerApellido);
                cmd.Parameters.AddWithValue("@SegundoApellido", propietario.SegundoApellido);
                cmd.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                cmd.Parameters.AddWithValue("@Correo", propietario.Correo);
                cmd.Parameters.AddWithValue("@UsuarioModificacion", propietario.UsuarioModificacion);

                cmd.ExecuteNonQuery();

                return true;
            }
        }
        public bool EliminarPropietario(int id)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                // 1. Eliminar hojas clínicas de las mascotas del propietario
                SqlCommand cmdHojas = new SqlCommand(
                    "DELETE FROM HojaClinica WHERE MascotaId IN (SELECT Id FROM Mascotas WHERE PropietarioId = @Id)", conn);
                cmdHojas.Parameters.AddWithValue("@Id", id);
                cmdHojas.ExecuteNonQuery();

                // 2. Eliminar mascotas del propietario
                SqlCommand cmdMascotas = new SqlCommand("DELETE FROM Mascotas WHERE PropietarioId = @Id", conn);
                cmdMascotas.Parameters.AddWithValue("@Id", id);
                cmdMascotas.ExecuteNonQuery();

                // 3. Eliminar propietario
                SqlCommand cmd = new SqlCommand("sp_EliminarPropietario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

                return true;
            }
        }
    }


}