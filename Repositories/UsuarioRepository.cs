using Microsoft.Data.SqlClient;
using System.Data;
using VeterinariaAPI.Data; 
using VeterinariaAPI.Models;

namespace VeterinariaAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly ConexionBD _conexion;

        public UsuarioRepository(ConexionBD conexion)
        {
            _conexion = conexion;
        }

        // ===========================
        // LEER TODOS
        // ===========================
        public List<Usuario> LeerUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LeerUsuarios", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        UsuarioLogin = dr["Usuario"].ToString()!,
                        Clave = dr["Clave"].ToString()!,
                        Estado = dr["Estado"].ToString()!,
                        FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                        UsuarioCreacion = dr["UsuarioCreacion"].ToString()!,
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
        public Usuario? BuscarPorId(int id)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_BuscarUsuarioPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new Usuario
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        UsuarioLogin = dr["Usuario"].ToString(),
                        Clave = dr["Clave"].ToString(),
                        Estado = dr["Estado"].ToString(),
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
        public int CrearUsuario(Usuario usuario)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_CrearUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Usuario", usuario.UsuarioLogin);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
                cmd.Parameters.AddWithValue("@UsuarioCreacion", usuario.UsuarioCreacion);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void ModificarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_ModificarUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Usuario", usuario.UsuarioLogin);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
                cmd.Parameters.AddWithValue("@UsuarioModificacion", usuario.UsuarioModificacion);

                cmd.ExecuteNonQuery();
            }
        }
        
        public void EliminarUsuario(int id)

        {

            using (SqlConnection conn = _conexion.ObtenerConexion())

            {

                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();

            }

        }

    }
}