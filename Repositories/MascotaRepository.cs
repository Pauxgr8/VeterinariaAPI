using Microsoft.Data.SqlClient;
using System.Data;
using VeterinariaAPI.Data;
using VeterinariaAPI.Models;

namespace VeterinariaAPI.Repositories
{
    public class MascotaRepository
    {
        private readonly ConexionBD _conexion;

        public MascotaRepository(ConexionBD conexion)
        {
            _conexion = conexion;
        }

        //=================================
        // LEER TODAS LAS MASCOTAS
        //=================================
        public List<Mascota> LeerMascotas()
        {
            List<Mascota> lista = new List<Mascota>();

            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_LeerMascotas", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Mascota
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        Sexo = dr["Sexo"].ToString(),
                        Peso = Convert.ToDecimal(dr["Peso"]),
                        Alergias = dr["Alergias"].ToString(),
                        PropietarioId = Convert.ToInt32(dr["PropietarioId"]),
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
        //=================================
        // BUSCAR POR ID
        //=================================
        public Mascota? BuscarPorId(int id)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_BuscarMascotaPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new Mascota
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        Sexo = dr["Sexo"].ToString(),
                        Peso = Convert.ToDecimal(dr["Peso"]),
                        Alergias = dr["Alergias"].ToString(),
                        PropietarioId = Convert.ToInt32(dr["PropietarioId"]),
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
        //=================================
        // CREAR MASCOTA
        //=================================
        public int CrearMascota(Mascota mascota)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_CrearMascota", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", mascota.Nombre);
                cmd.Parameters.AddWithValue("@FechaNacimiento", mascota.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Sexo", mascota.Sexo);
                cmd.Parameters.AddWithValue("@Peso", mascota.Peso);
                cmd.Parameters.AddWithValue("@Alergias", mascota.Alergias);
                cmd.Parameters.AddWithValue("@PropietarioId", mascota.PropietarioId);
                cmd.Parameters.AddWithValue("@UsuarioCreacion", mascota.UsuarioCreacion);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //=================================
        // MODIFICAR MASCOTA
        //=================================
        public bool ModificarMascota(Mascota mascota)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_ModificarMascota", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", mascota.Id);
                cmd.Parameters.AddWithValue("@Nombre", mascota.Nombre);
                cmd.Parameters.AddWithValue("@FechaNacimiento", mascota.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Sexo", mascota.Sexo);
                cmd.Parameters.AddWithValue("@Peso", mascota.Peso);
                cmd.Parameters.AddWithValue("@Alergias", mascota.Alergias);
                cmd.Parameters.AddWithValue("@PropietarioId", mascota.PropietarioId);
                cmd.Parameters.AddWithValue("@UsuarioModificacion", mascota.UsuarioModificacion);

                cmd.ExecuteNonQuery();

                return true;
            }
        }
        //=================================
        // ELIMINAR MASCOTA
        //=================================
        public bool EliminarMascota(int id)
        {
            using (SqlConnection conn = _conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_EliminarMascota", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();

                return true;
            }
        }
    }
}