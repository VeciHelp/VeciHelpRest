using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using WebApiSegura.Models;
using System.Drawing;

namespace VeciHelp.BD
{
    public class BaseDatos
    {
        #region Atributos

        System.Data.SqlClient.SqlConnection cnn;
        public String svr;
        public String dbs;
        public String usr;
        public String pss;
        public String connString;

        #endregion

        #region Constructores

        public BaseDatos()
        {
            cnn = new System.Data.SqlClient.SqlConnection();
            svr = "DESKTOP-U70SU7T\\VHELP";
            dbs = "VeciHelp";
            usr = "usrConsulta";
            pss = DecodificaBase64("Q1gqKTIwa0NpMnNsbQ==").ToString();
            connString = @"Data Source=" + svr + ";Initial Catalog=" + dbs + ";User Id=" + usr + ";Password=" + pss + ";";
        }

        #endregion

        #region Metodos Base
        // Decodificar
        public static string DecodificaBase64(string str)
        {
            try
            {
                byte[] decbuff = Convert.FromBase64String(str);
                return System.Text.Encoding.UTF8.GetString(decbuff);
            }
            catch (Exception e)
            {
                { return ""; }
            }
        }

        public bool Open()
        {
            bool Funciona;
            Funciona = false;
            try
            {
                cnn.ConnectionString = connString;
                cnn.Open();
                Funciona = true;
            }
            catch (Exception)
            {
                throw;
            }
            return Funciona;
        }

        public void Close()
        {
            try
            {
                cnn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Login

        //metodo que valida si el correo y contraseña son correctos y retorna el tipo de usuario
        public ResponseLogin P_Login(string correo, string clave)
        {
            ResponseLogin response = new ResponseLogin();

            //creamos variables sueltas para poder recibirlas por out
            int existe = 0;
            string nombre = string.Empty;
            string apellido = string.Empty;
            int idUsuario = 0;
            string roleName = string.Empty;
            string mensaje = string.Empty;


            String _sql = string.Format("P_Login");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@correo", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@clave", SqlDbType.VarChar, 200);
                    sqlComm.Parameters.Add("@Existe", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlComm.Parameters.Add("@Nombre ", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                    sqlComm.Parameters.Add("@Apellido ", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    sqlComm.Parameters.Add("@Id_Usuario ", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlComm.Parameters.Add("@RoleName", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    sqlComm.Parameters.Add("@Mensaje ", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;


                    sqlComm.Parameters[0].Value = correo;
                    sqlComm.Parameters[1].Value = clave;

                    sqlComm.ExecuteNonQuery();

                    int.TryParse(sqlComm.Parameters[2].Value.ToString(), out existe);
                    nombre = sqlComm.Parameters[3].Value.ToString();
                    apellido = sqlComm.Parameters[4].Value.ToString();
                    int.TryParse(sqlComm.Parameters[5].Value.ToString(), out idUsuario);
                    roleName = sqlComm.Parameters[6].Value.ToString();
                    mensaje = sqlComm.Parameters[7].Value.ToString();


                    //asignamos las variables al objeto response para devolverlo al metodo
                    response.existe = existe;
                    response.nombre = nombre;
                    response.apellido = apellido;
                    response.id_Usuario = idUsuario;
                    response.roleName = roleName;
                    response.mensaje = mensaje;

                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }
            //devuelvo el objeto response ya lleno
            return response;
        }
        #endregion

        #region Administrador

        //metodo para que el administrador se registre, esto debe hacerse posterior a ser enrolado
        public bool p_UsuarioAdministradorIns(string correo, string codigoVerificacion,string nombre,string apellido,string rut,char digito,string antecedentesSalud,DateTime fechaNacimiento, int celular,string direccion,string clave, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_UsuarioAdministradorIns");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@CodigoVerificacion", SqlDbType.VarChar, 10);
                    sqlComm.Parameters.Add("@Nombre", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@Rut", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Digito", SqlDbType.VarChar, 1);
                    sqlComm.Parameters.Add("@AntecedentesSalud", SqlDbType.VarChar, 500);
                    sqlComm.Parameters.Add("@FechaNacimiento", SqlDbType.Date);
                    sqlComm.Parameters.Add("@Celular", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Direccion", SqlDbType.VarChar, 500);
                    sqlComm.Parameters.Add("@Clave", SqlDbType.VarChar, 50);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;


                    sqlComm.Parameters[0].Value = correo;
                    sqlComm.Parameters[1].Value = codigoVerificacion;
                    sqlComm.Parameters[2].Value = nombre;
                    sqlComm.Parameters[3].Value = apellido;
                    sqlComm.Parameters[4].Value = rut;
                    sqlComm.Parameters[5].Value = digito;
                    sqlComm.Parameters[6].Value = antecedentesSalud;
                    sqlComm.Parameters[7].Value = fechaNacimiento;
                    sqlComm.Parameters[8].Value = celular;
                    sqlComm.Parameters[9].Value = direccion;
                    sqlComm.Parameters[10].Value = clave;

                    sqlComm.ExecuteNonQuery();
                    mensaje = sqlComm.Parameters[11].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }
        
        //metodo con el que el administrador enrola a los usuarios(internamente se envia un correo con un codigo de verificacion)
        public bool p_CodigoVerificacionUsuarioGenera(string correo, int idUsuarioCreador, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_CodigoVerificacionUsuarioGenera");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = correo;
                    sqlComm.Parameters[1].Value = idUsuarioCreador;

                    sqlComm.ExecuteNonQuery();
                    mensaje = sqlComm.Parameters[2].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }

        //metodo con el cual el administrador asocia los usuarios con los vecinos cercanos
        public bool p_AsociacionVecinoIns(int idUsuario,int idVecino,int idAdmin, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_AsociacionVecinoIns");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Id_Vecino", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Id_Administrador", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = idVecino;
                    sqlComm.Parameters[2].Value = idAdmin;

                    sqlComm.ExecuteNonQuery();

                    mensaje = sqlComm.Parameters[3].Value.ToString();

                    this.Close();
                    return true;
                 }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }

        //metodo con el cual el administrador elimina una asociaciond e vecino cercano de un usuario especifico
        public bool p_AsociacionVecinoDel(int idUsuario, int idVecino, int idAdmin, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_AsociacionVecinoDel");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Id_Vecino", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Id_Administrador", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = idVecino;
                    sqlComm.Parameters[2].Value = idAdmin;

                    sqlComm.ExecuteNonQuery();

                    mensaje = sqlComm.Parameters[3].Value.ToString();
                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }

        //metodo que retora un usuario por correo
        public List<Usuario> p_AsociacionVecinosByCorreoLst(string correo)
        {
            List<Usuario> usrLst = new List<Usuario>();

            String _sql = string.Format("p_AsociacionVecinosByCorreoLst");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Correo", SqlDbType.VarChar);
                    sqlComm.Parameters[0].Value = correo;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Usuario usr = new Usuario();
                            usr.id_Usuario = int.Parse(dr[1].ToString());
                            usr.nombre = dr[2].ToString();
                            usr.apellido = dr[3].ToString();
                            usr.direccion = dr[4].ToString();
                            usr.celular = int.Parse(dr[5].ToString());
                            usrLst.Add(usr);
                        }

                    }
                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }

            return usrLst;
        }

        //metodo que retora un usuario por id
        public List<Usuario> p_UsuariosLst(int idUsuario)
        {
            List<Usuario> usrLst = new List<Usuario>();

            String _sql = string.Format("p_UsuariosLst");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters[0].Value = idUsuario;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Usuario usr = new Usuario();
                            usr.id_Usuario = int.Parse(dr[0].ToString());
                            usr.nombre = dr[1].ToString();
                            usr.apellido = dr[2].ToString();
                            usr.direccion = dr[3].ToString();
                            usr.celular = int.Parse(dr[4].ToString());
                            usr.Foto = dr[5].ToString();
                            usrLst.Add(usr);
                        }

                    }
                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }

            return usrLst;
        }

        //metodo que elimina un usuario
        public bool p_DesactivaUsuarioUpd(int idUsuario,out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_DesactivaUsuarioUpd");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;

                    sqlComm.ExecuteNonQuery();

                    mensaje = sqlComm.Parameters[1].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }

        //metodo que retorna los datos de un usuario en especifico por correo
        public Usuario p_UsuarioByCorreoGet(string  correo)
        {
            Usuario usr = new Usuario();

            String _sql = string.Format("p_UsuarioByCorreoGet");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Correo", SqlDbType.VarChar,100);
                    sqlComm.Parameters[0].Value = correo;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            usr.id_Usuario = int.Parse(dr[0].ToString());
                            usr.correo = dr[1].ToString();
                            usr.nombre = dr[2].ToString();
                            usr.apellido = dr[3].ToString();
                            usr.rut = dr[4].ToString();
                            usr.digito = char.Parse(dr[5].ToString());
                            usr.Foto = dr[6].ToString();
                            usr.antecedentesSalud = dr[7].ToString();
                            usr.fechaNacimiento = DateTime.Parse(dr[8].ToString());
                            usr.celular = int.Parse(dr[9].ToString());
                            usr.direccion = dr[10].ToString();
                            usr.numeroEmergencia = dr[13].ToString();
                        }
                    }
                    else
                        usr = null;

                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }

            return usr;
        }
        #endregion

        #region Usuarios
        //metodo que lista los vecinos cercanos de un usuario, para saber cuales son sus casas cercanas
        public List<Usuario> p_AsociacionVecinosLst(int idUsuario)
        {
            List<Usuario> usrLst = new List<Usuario>();

            String _sql = string.Format("p_AsociacionVecinosLst");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters[0].Value = idUsuario;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Usuario usr = new Usuario();
                            usr.id_Usuario = int.Parse(dr[0].ToString());
                            usr.nombre = dr[1].ToString();
                            usr.apellido = dr[2].ToString();
                            usr.direccion = dr[3].ToString();
                            usr.celular = int.Parse(dr[4].ToString());
                            usrLst.Add(usr);
                        }

                    }
                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }

            return usrLst;
        }

        //metodo que retorna los datos de un usuario en especifico por id
        public Usuario p_UsuarioGet(int idUsuario)
        {
            Usuario usr = new Usuario();

            String _sql = string.Format("p_UsuarioGet");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.VarChar, 100);
                    sqlComm.Parameters[0].Value = idUsuario;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            usr.id_Usuario = int.Parse(dr[0].ToString());
                            usr.correo = dr[1].ToString();
                            usr.nombre = dr[2].ToString();
                            usr.apellido = dr[3].ToString();
                            usr.rut = dr[4].ToString();
                            usr.digito = char.Parse(dr[5].ToString());
                            usr.Foto = dr[6].ToString();
                            usr.antecedentesSalud = dr[7].ToString();
                            usr.fechaNacimiento = DateTime.Parse(dr[8].ToString());
                            usr.celular = int.Parse(dr[9].ToString());
                            usr.direccion = dr[10].ToString();
                            usr.numeroEmergencia = dr[13].ToString();
                        }
                    }
                    else
                        usr = null;

                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }

            return usr;
        }

        //metodo para que el usuario se registre, esto debe hacerse posterior a ser enrolado por un administrador
        public bool p_UsuarioIns(string correo, string codigoVerificacion, string nombre, string apellido, string rut, char digito, string antecedentesSalud, DateTime fechaNacimiento, int celular, string direccion, string clave, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_UsuarioIns");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@CodigoVerificacion", SqlDbType.VarChar, 10);
                    sqlComm.Parameters.Add("@Nombre", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@Rut", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Digito", SqlDbType.VarChar, 1);
                    sqlComm.Parameters.Add("@AntecedentesSalud", SqlDbType.VarChar, 500);
                    sqlComm.Parameters.Add("@FechaNacimiento", SqlDbType.Date);
                    sqlComm.Parameters.Add("@Celular", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Direccion", SqlDbType.VarChar, 500);
                    sqlComm.Parameters.Add("@Clave", SqlDbType.VarChar, 50);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;


                    sqlComm.Parameters[0].Value = correo;
                    sqlComm.Parameters[1].Value = codigoVerificacion;
                    sqlComm.Parameters[2].Value = nombre;
                    sqlComm.Parameters[3].Value = apellido;
                    sqlComm.Parameters[4].Value = rut;
                    sqlComm.Parameters[5].Value = digito;
                    sqlComm.Parameters[6].Value = antecedentesSalud;
                    sqlComm.Parameters[7].Value = fechaNacimiento;
                    sqlComm.Parameters[8].Value = celular;
                    sqlComm.Parameters[9].Value = direccion;
                    sqlComm.Parameters[10].Value = clave;

                    sqlComm.ExecuteNonQuery();
                    mensaje = sqlComm.Parameters[11].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }
        
        //Metodo con el cual un usuario puede actualizar sus datos personales
        public bool p_UsuarioUpd(int idUsuario, string nombre, string apellido, string rut, char digito, string antecedentesSalud, DateTime fechaNacimiento, int celular, string direccion, string clave, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_UsuarioUpd");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure; 

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Nombre", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@Apellidos", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@Rut", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Digito", SqlDbType.VarChar, 1);
                    sqlComm.Parameters.Add("@AntecedentesSalud", SqlDbType.VarChar, 500);
                    sqlComm.Parameters.Add("@FechaNacimiento", SqlDbType.Date);
                    sqlComm.Parameters.Add("@Celular", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Direccion", SqlDbType.VarChar, 500);
                    sqlComm.Parameters.Add("@Clave", SqlDbType.VarChar, 50);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = nombre;
                    sqlComm.Parameters[2].Value = apellido;
                    sqlComm.Parameters[3].Value = rut;
                    sqlComm.Parameters[4].Value = digito;
                    sqlComm.Parameters[5].Value = antecedentesSalud;
                    sqlComm.Parameters[6].Value = fechaNacimiento;
                    sqlComm.Parameters[7].Value = celular;
                    sqlComm.Parameters[8].Value = direccion;
                    sqlComm.Parameters[9].Value = clave;

                    sqlComm.ExecuteNonQuery();

                    mensaje = sqlComm.Parameters[10].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }
        
        //metodo con el cual el usuario actualiza su foto de perfil
        public bool p_FotoUsuarioUpd(int idUsuario, string foto, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_FotoUsuarioUpd");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
    
                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Foto", SqlDbType.VarChar, 2147483647);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = foto;

                    sqlComm.ExecuteNonQuery();
                    mensaje = sqlComm.Parameters[2].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }
        #endregion

        #region Alertas
         
        //metodo que inserta una alerta por sospecha , tanto en casa propia como en casa vecino
        public bool P_AlertaSospechaIns(int idUsuario,string coordenadas,string texto, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_AlertaSospechaIns");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@CoordenadaSospechosa", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@TextoSospecha", SqlDbType.VarChar, 500);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = coordenadas;
                    sqlComm.Parameters[2].Value = texto;

                    sqlComm.ExecuteNonQuery();

                    mensaje = sqlComm.Parameters[3].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }

        //metodo que inserta una alerta por SOS , tanto en casa propia como en casa vecino
        public bool P_AlertaSOSIns(int idUsuario,int idVecino, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_AlertaSOSIns");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Id_Vecino", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = idVecino;

                    sqlComm.ExecuteNonQuery();

                    mensaje = sqlComm.Parameters[2].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }

        //metodo que inserta una alerta por Emergencia , tanto en casa propia como en casa vecino
        public bool P_AlertaEmergenciaIns(int idUsuario, int idVecino, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_AlertaEmergenciaIns");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Id_Vecino", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = idVecino;

                    sqlComm.ExecuteNonQuery();

                    mensaje = sqlComm.Parameters[2].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }

        //metodo que inserta la participacion de un usuario en una alerta 
        public bool P_AcudirLlamadoIns(int idUsuario, int idAlerta, out string mensaje)
        {
            mensaje = string.Empty;
            String _sql = string.Format("p_AcudirLlamadoIns");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Id_Alerta", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = idAlerta;

                    sqlComm.ExecuteNonQuery();

                    mensaje = sqlComm.Parameters[2].Value.ToString();

                    this.Close();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
                return false;
            }
        }

        //metodo que lista las alertas activas en curso
        public List<Alerta> P_AlertaLst()
        {
            List<Alerta> alertLst = new List<Alerta>();

            String _sql = string.Format("p_AlertaLst");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Alerta alert = new Alerta();

                            alert.idAlerta = int.Parse(dr[0].ToString());
                            alert.fechaAlerta = DateTime.Parse(dr[1].ToString());
                            alert.horaAlerta = DateTime.Parse(dr[2].ToString());
                            alert.TipoAlerta = dr[3].ToString();
                            alert.nombreGenerador = dr[4].ToString();
                            alert.apellidoGenerador = dr[5].ToString();
                            alert.nombreAyuda = dr[6].ToString();
                            alert.apellidoAyuda = dr[7].ToString();
                            alert.coordenadaSospecha = dr[8].ToString();
                            alert.textoSospecha = dr[9].ToString();
                            alert.direccion = dr[10].ToString();
                            alert.organizacion = dr[11].ToString();
                            alert.participantes = Int32.Parse(dr[18].ToString());
                            alertLst.Add(alert);
                        }
                    }
                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }
            return alertLst;
        }

        public Alerta P_Alerta(int idAlerta)
        {
            Alerta alert = new Alerta();

            String _sql = string.Format("p_AlertaLst");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Id_Alerta", SqlDbType.Int);
                    sqlComm.Parameters[0].Value = idAlerta;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            

                            alert.idAlerta = int.Parse(dr[0].ToString());
                            alert.fechaAlerta = DateTime.Parse(dr[1].ToString());
                            alert.horaAlerta = DateTime.Parse(dr[2].ToString());
                            alert.TipoAlerta = dr[3].ToString();
                            alert.nombreGenerador = dr[4].ToString();
                            alert.apellidoGenerador = dr[5].ToString();
                            alert.nombreAyuda = dr[6].ToString();
                            alert.apellidoAyuda = dr[7].ToString();
                            alert.coordenadaSospecha = dr[8].ToString();
                            alert.textoSospecha = dr[9].ToString();
                            alert.direccion = dr[10].ToString();
                            alert.organizacion = dr[11].ToString();
                            alert.participantes = Int32.Parse(dr[18].ToString());
                        }
                    }
                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }
            return alert;
        }

        #endregion
    }
}