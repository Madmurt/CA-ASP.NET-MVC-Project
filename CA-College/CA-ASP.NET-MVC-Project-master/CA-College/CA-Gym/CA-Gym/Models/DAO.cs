using System;
using System.Collections.Generic;
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
            cmd.Parameters.AddWithValue("@name", trainer.Name);
            cmd.Parameters.AddWithValue("@age", trainer.Age);
            cmd.Parameters.AddWithValue("@gender", trainer.Gender);
            cmd.Parameters.AddWithValue("@specialty", trainer.Speciality);

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
        //***Update member details****
        public bool UpdateMemberDetails(Member member)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("UpdateMemberDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@firstName", member.FirstName);
            cmd.Parameters.AddWithValue("@lastName", member.LastName);
            cmd.Parameters.AddWithValue("@phone", member.Phone);
            cmd.Parameters.AddWithValue("@address", member.Address);

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if(i >=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //********Delete member**********
        public bool DeleteMember(int memberId)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("DeleteMember", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@memberID", memberId);

            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if(i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Method for checking login
        public string CheckLogin(Member member)
        {
            string firstName = null;
            SqlCommand cmd;
            SqlDataReader reader;
            string password;
            Connection();
            cmd = new SqlCommand("uspCheckLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", member.Email);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["Pass"].ToString();
                    if (Crypto.VerifyHashedPassword(password, member.Password))
                    {
                        firstName = reader["FirstName"].ToString();
                    }

                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (FormatException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return firstName;
        }

    }
    
}