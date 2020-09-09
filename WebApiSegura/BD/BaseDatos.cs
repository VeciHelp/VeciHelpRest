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
        public void P_Login(string correo, string clave, out int retorna,out string rolename)
        {
            retorna = 0;
            rolename = string.Empty;
                        String _sql = string.Format("P_Login");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@correo", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@clave", SqlDbType.VarChar, 200);
                    sqlComm.Parameters.Add("@Retorna", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlComm.Parameters.Add("@Tipo", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;


                    sqlComm.Parameters[0].Value = correo;
                    sqlComm.Parameters[1].Value = clave;

                    sqlComm.ExecuteNonQuery();

                    int.TryParse(sqlComm.Parameters[2].Value.ToString(), out retorna);
                    rolename=sqlComm.Parameters[3].Value.ToString();

                    this.Close();
                }
            }
            catch (SqlException e)
            {
                //Logger.InformeErrores(maquina.ToString(), e.Message, "Insertar_Registro [BaseDatos]");
                this.Close();
            }
        }
        #endregion

        #region Administrador

        //metodo para que el administrador se registre, esto debe hacerse posterior a ser enrolado
        public bool p_UsuarioAdministradorIns(string correo, string codigoVerificacion,string nombre,string apellido,string rut,char digito,string antecedentesSalud,DateTime fechaNacimiento, int celular,string direccion,string clave)
        {
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
        public bool p_CodigoVerificacionUsuarioGenera(string correo, int idUsuarioCreador)
        {
            String _sql = string.Format("p_CodigoVerificacionUsuarioGenera");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                  
                    sqlComm.Parameters[0].Value = correo;
                    sqlComm.Parameters[1].Value = idUsuarioCreador;

                    sqlComm.ExecuteNonQuery();

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
        public bool p_AsociacionVecinoIns(int idUsuario,int idVecino,int idAdmin)
        {
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

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = idVecino;
                    sqlComm.Parameters[2].Value = idAdmin;

                    sqlComm.ExecuteNonQuery();

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
        public bool p_AsociacionVecinoDel(int idUsuario, int idVecino, int idAdmin)
        {
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

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = idVecino;
                    sqlComm.Parameters[2].Value = idAdmin;

                    sqlComm.ExecuteNonQuery();

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

        //metodo que lista los vecinos cercanos de un usuario sirve tanto al administrador como a el usuario , para saber cuales son sus casas cercanas
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

                    sqlComm.Parameters.Add("@Id_Vecino", SqlDbType.Int);
                    sqlComm.Parameters[0].Value = idUsuario;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Usuario usr = new Usuario();
                            usr.nombre = dr[0].ToString();
                            usr.apellido = dr[1].ToString();
                            usr.direccion = dr[2].ToString();
                            usr.celular = int.Parse(dr[3].ToString());
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
        #endregion

        #region Usuarios

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

                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters[0].Value = idUsuario;

                    SqlDataReader dr = sqlComm.ExecuteReader();

                    if (dr.HasRows)
                    {
                            usr.nombre = dr[0].ToString();
                            usr.apellido = dr[1].ToString();
                            usr.direccion = dr[2].ToString();
                            usr.celular = int.Parse(dr[3].ToString());
                    }
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
        public bool p_UsuarioIns(string correo, string codigoVerificacion, string nombre, string apellido, string rut, char digito, string antecedentesSalud, DateTime fechaNacimiento, int celular, string direccion, string clave)
        {
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
        public bool p_UsuarioUpd(int idUsuario, string nombre, string apellido, string rut, char digito, string antecedentesSalud, DateTime fechaNacimiento, int celular, string direccion, string clave)
        {
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
        public bool p_FotoUsuarioUpd(int idUsuario, byte[] foto)
        {
            String _sql = string.Format("p_FotoUsuarioUpd");
            try
            {
                if (this.Open())
                {
                    SqlCommand sqlComm = new SqlCommand(_sql, cnn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
    
                    sqlComm.Parameters.Add("@Id_Usuario", SqlDbType.Int);
                    sqlComm.Parameters.Add("@Foto", SqlDbType.VarBinary, 2147483647);

                    sqlComm.Parameters[0].Value = idUsuario;
                    sqlComm.Parameters[1].Value = foto;

                    sqlComm.ExecuteNonQuery();

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
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100);

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
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100);

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
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100);

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
                    sqlComm.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100);

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
        #endregion
    }
}