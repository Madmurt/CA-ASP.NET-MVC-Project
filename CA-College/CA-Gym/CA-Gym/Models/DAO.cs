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
        public int Insert(Member user, MemberShipType mType)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            string password;
            Connection();
            cmd = new SqlCommand("uspInsertMemberTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            cmd.Parameters.AddWithValue("@lastName", user.LastName);
            cmd.Parameters.AddWithValue("@gender", user.Gender);
            cmd.Parameters.AddWithValue("@age", user.Age);
            cmd.Parameters.AddWithValue("@phone", user.Phone);
            cmd.Parameters.AddWithValue("@address", user.Address);
            cmd.Parameters.AddWithValue("@email", user.Email);
            password = Crypto.HashPassword(user.Password);
            message = password;
            cmd.Parameters.AddWithValue("@memPass", password);*/

            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            cmd.Parameters.AddWithValue("@lastName", user.LastName);
            cmd.Parameters.AddWithValue("@gender", user.LastName);
            cmd.Parameters.AddWithValue("@age", user.LastName);
            cmd.Parameters.AddWithValue("@phone", user.LastName);
            cmd.Parameters.AddWithValue("@memAddress", user.LastName);
            cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
            password = Crypto.HashPassword(user.Password);
            cmd.Parameters.AddWithValue("@memPass", password);

            SqlCommand cmd2;
            Connection();
            cmd2 = new SqlCommand("uspInsertMembershipTypeTable", conn);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@memType", mType.MemType);
            cmd2.Parameters.AddWithValue("@joinDate", mType.JoinDate);
            cmd2.Parameters.AddWithValue("@renewalDate", mType.RenewalDate);
            cmd2.Parameters.AddWithValue("@gymLocation", mType.GymLocation);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
                count += cmd2.ExecuteNonQuery();
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
            cmd.Parameters.AddWithValue("@speciality", trainer.Speciality);

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

        public int Insert(Class c)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertClassTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@trainerID", trainer.ID); How to add this from drop down list? 
            //SELECT trainerID FROM Trainer WHERE Name = etc.;
            cmd.Parameters.AddWithValue("@time", c.Time);
            cmd.Parameters.AddWithValue("@classType", c.ClassType);
            cmd.Parameters.AddWithValue("@location", c.Location);
            cmd.Parameters.AddWithValue("@maxMembers", c.MaxMembers);

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

        public int Insert(MemberShipType mType)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertMembershipTypeTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@memType", mType.MemType);
            cmd.Parameters.AddWithValue("@joinDate", mType.JoinDate);
            cmd.Parameters.AddWithValue("@renewalDate", mType.RenewalDate);
            cmd.Parameters.AddWithValue("@gymLocation", mType.GymLocation);

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

        public int Insert(Booking book)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertBookingTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@classID", book.ClassID);
            //SELECT classID FROM Class WHERE location,classType,Time = etc.;
            //cmd.Parameters.AddWithValue("@memberID", book.MemberID); Use logged in member ID.
            cmd.Parameters.AddWithValue("@date", book.Date);
            cmd.Parameters.AddWithValue("@time", book.Time);

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

        public int Insert(PTSession ptSess)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertPTSessionTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@trainerID", ptSess.TrainerID);
            //SELECT trainerID FROM Trainer WHERE Name = etc.;
            //cmd.Parameters.AddWithValue("@memberID", ptSess.MemberID); Use logged in member ID.
            cmd.Parameters.AddWithValue("@sessionLength", ptSess.SessionLength);
            cmd.Parameters.AddWithValue("@sesionDate", ptSess.SessionDate);
            cmd.Parameters.AddWithValue("@sessionTime", ptSess.SessionTime);
            cmd.Parameters.AddWithValue("@sessType", ptSess.SessType);
            cmd.Parameters.AddWithValue("@Cost", ptSess.Cost);
            cmd.Parameters.AddWithValue("@sessLocation", ptSess.SessLocation);

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

        public List<string> GetTrainerName()
        {
            List<string> trainerList = new List<string>();

            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetTrainer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Trainer t = new Trainer();
                    t.Name = reader["Name"].ToString();
                    trainerList.Add(t.Name);
                }

            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return trainerList;
        }

        public int getTrainerIDFromDropDown()
        {
            int result = 0;
            SqlDataReader reader;
            Connection();

            SqlCommand cmd = new SqlCommand("SELECT TrainerID From Trainer", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["TrainerID"].ToString());
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}