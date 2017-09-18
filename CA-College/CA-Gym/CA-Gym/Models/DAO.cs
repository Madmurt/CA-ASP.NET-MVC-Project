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

        public void Connection()
        {
            conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString);
        }

        public int Insert(Member user, MemberShipType mType)
        {
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
                if (conn.State == ConnectionState.Closed)  //Added to hopefully resolve connection issues.
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

        public int Insert(Class c, int trainerID)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertClassTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@trainerID", trainerID);
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

        public int Insert(Booking book, int classID, int memberID, string classTime)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertBookingTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@classID", classID);
            cmd.Parameters.AddWithValue("@memberID", memberID); //Use logged in member ID.
            cmd.Parameters.AddWithValue("@date", book.Date);
            cmd.Parameters.AddWithValue("@time", classTime); 

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

        public int Insert(PTSession ptSess, int memberID, int trainerID)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertPTSessionTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@trainerID", trainerID);
            cmd.Parameters.AddWithValue("@memberID", memberID); //Use logged in member ID.
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

        public List<string> GetClassType()
        {
            List<string> classList = new List<string>();

            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetClassType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader["ClassType"].ToString();
                    classList.Add(result);
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

            return classList;
        }

        public List<string> GetMemberName()
        {
            List<string> memberList = new List<string>();

            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetMemberName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string result;
                    result = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    memberList.Add(result);
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

            return memberList;
        }

        public int getTrainerIDFromDropDown(string trainerName)
        {
            int result = 0;
            SqlDataReader reader;
            Connection();
        
            SqlCommand cmd = new SqlCommand("SELECT TrainerID From Trainer WHERE Name = '" + trainerName + "'" , conn);
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

        public int getClassIDFromDropDown(string classType)
        {
            int result = 0;
            SqlDataReader reader;
            Connection();

            SqlCommand cmd = new SqlCommand("SELECT ClassID From Class WHERE ClassType = '" + classType + "'", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["ClassID"].ToString());
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
        
        public string getClassTimeFromDropDown(string classType)
        {
            string result = null;
            SqlDataReader reader;
            Connection();

            SqlCommand cmd = new SqlCommand("SELECT Time From Class WHERE ClassType = '" + classType + "'", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = reader["Time"].ToString();
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

            if (i >= 1)
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

            if (i >= 1)
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
                    password = reader["MemPass"].ToString();                   
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

        //*********************************************
        public List<string> GetMemberType()
        {
            List<string> memTypeList = new List<string>();

            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetMemberType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader["MemType"].ToString();
                    memTypeList.Add(result);
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

            return memTypeList;
        }
        //**********************************
        public void GetMemTypeID()
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT MAX (MemTypeID) FROM MembershipType ", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            int result = 1; ;
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    result += int.Parse(dr[0].ToString());

                }
            }
            conn.Close();
            // return result;

        }
        //public int getMemTypeIDFromDropDown(string memType) 
        //{
        //    int result = 0;
        //    SqlDataReader reader;
        //    Connection();

        //    SqlCommand cmd = new SqlCommand("SELECT MemTypeID From MembershipType WHERE MemType = '" + memType + "'", conn);
        //    //cmd.CommandType = CommandType.StoredProcedure;
        //    try
        //    {
        //        conn.Open();
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            result = int.Parse(reader["MemTypeID"].ToString());
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        message = ex.Message;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return result;
        //}

    }
}