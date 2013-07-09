using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOFTPLUSPROJECT.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace SOFTPLUSPROJECT
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                var userName = this.UserName.Text;
                var password = this.Password.Text;
                var isRememberMe = this.RememberMe.Checked;

                string connectionString = GetConnectionStringToAccessOtherDb(userName, password);

                if (!String.IsNullOrEmpty(connectionString))
                {

                    SetAuthCookie(connectionString);

                    Response.Redirect("Default.aspx", false);

                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                Response.Redirect("Login.aspx", false);
            }
        }

        #region My Methods
        
        /// <summary>
        /// Get Connection String To Access Other Database by sp_GetConnectionString
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GetConnectionStringToAccessOtherDb(string userName, string password)
        {
            string connectionString = string.Empty;

            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
            {

                SqlConnection sqlConnection = null;

                try
                {

                    string dbConnectionString = ConfigurationManager.ConnectionStrings["softplusOLConStr"].ConnectionString;
                    sqlConnection = new SqlConnection(dbConnectionString);

                    SqlCommand command = new SqlCommand("sp_GetConnectionString_New", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter parmUserName = new SqlParameter("@userName", SqlDbType.VarChar, 20);
                    parmUserName.Value = userName;
                    command.Parameters.Add(parmUserName);

                    SqlParameter parmPassword = new SqlParameter("@password", SqlDbType.VarChar, 20);
                    parmPassword.Value = password;
                    command.Parameters.Add(parmPassword);

                    SqlParameter parmConnString = new SqlParameter("@connString", SqlDbType.NVarChar, 200);
                    parmConnString.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parmConnString);

                    SqlParameter parmResult = new SqlParameter("@result", SqlDbType.NVarChar, 100);
                    parmResult.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parmResult);

                    SqlParameter parmMessage = new SqlParameter("@message", SqlDbType.NVarChar, 100);
                    parmMessage.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parmMessage);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();


                    string result = command.Parameters["@result"].Value.ToString();

                    if (!String.IsNullOrEmpty(result) && result == "Fail")
                    {
                        return connectionString;

                    }
                    else
                    {
                        string connString = command.Parameters["@connString"].Value.ToString();
                        connectionString = connString;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();

                }

            }

            return connectionString;
        }

        /// <summary>
        /// Set Connection String to Cookie
        /// </summary>
        /// <param name="connectionString"></param>
        private void SetAuthCookie(string connectionString)
        {
            HttpCookie cookie = new HttpCookie("CookieConStr");

            if (!String.IsNullOrEmpty(connectionString))
            {
                //set Connection String
                cookie.Value = connectionString;
            }

            this.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Set Connection String to Session
        /// </summary>
        /// <param name="connectionString"></param>
        private void SetAuthSession(string connectionString)
        {

            if (!String.IsNullOrEmpty(connectionString))
            {
                //set Connection String
                Session["SessionConStr"] = connectionString;
            }

        }

        #endregion
    }
}
