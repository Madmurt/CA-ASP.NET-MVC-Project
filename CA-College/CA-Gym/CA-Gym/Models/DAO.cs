using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.Web.Configuration;

namespace CA_Gym.Models
{
    public class DAO
    {
        SqlConnection conn;
        public string message = "";

        //Intialises a connection object
        public void Connection()
        {
            conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString);
        }

        //Method for inserting data to Database
        public int Insert(Member user)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            string password;
            Connection();
            cmd = new SqlCommand("uspInsertMemberTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            cmd.Parameters.AddWithValue("@lastName", user.LastName);
            cmd.Parameters.AddWithValue("@gender", user.Gender);
            cmd.Parameters.AddWithValue("@age", user.Age);
            cmd.Parameters.AddWithValue("@phone", user.Phone);
            cmd.Parameters.AddWithValue("@address", user.Address);
            cmd.Parameters.AddWithValue("@email", user.Email);
            password = Crypto.HashPassword(user.Password);
            message = password;
            cmd.Parameters.AddWithValue("@memPass", password);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        public int Insert(Trainer trainer)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertTrainerTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", trainer.name);
            cmd.Parameters.AddWithValue("@age", trainer.age);
            cmd.Parameters.AddWithValue("@gender", trainer.gender);
            cmd.Parameters.AddWithValue("@specialty", trainer.speciality);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

    }
}