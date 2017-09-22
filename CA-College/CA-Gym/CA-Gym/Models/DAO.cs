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

        public int Insert(Member user, int result)
        {
            int count = 0;

            SqlCommand cmd;
            //string password;
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
            cmd.Parameters.AddWithValue("@gender", user.Gender);
            cmd.Parameters.AddWithValue("@age", user.Age);
            cmd.Parameters.AddWithValue("@phone", user.Phone);
            cmd.Parameters.AddWithValue("@address", user.MemAddress);
            cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
            //password = Crypto.HashPassword(user.MemPass); - Need to add unhashing when logging in
            cmd.Parameters.AddWithValue("@memPass", user.MemPass);
            cmd.Parameters.AddWithValue("@memTypeID", result);

            try
            {
                conn.Open();
                count += cmd.ExecuteNonQuery();
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
            int count = 0;


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
                count = cmd2.ExecuteNonQuery();
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


        public int Insert(Booking book, int classID, int memberID, string classTime)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertBookingTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@classID", classID);
            cmd.Parameters.AddWithValue("@memberID", memberID);
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

        public int Insert(PTSession ptSess)
        {
            //count shows the number of affected rows
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertPTSessionTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@trainerID", ptSess.TrainerID);
            cmd.Parameters.AddWithValue("@memberID", ptSess.MemberID);
            cmd.Parameters.AddWithValue("@sessionLength", ptSess.SessionLength);
            cmd.Parameters.AddWithValue("@sessionDate", ptSess.SessionDate);
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

        public List<string> GetGymLocation()
        {
            List<string> gymList = new List<string>();

            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetGymLocation", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string result = reader["GymLocation"].ToString();
                    gymList.Add(result);
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

            return gymList;
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

        public int getMemberIDFromSession(string email)
        {
            int result = 0;
            SqlDataReader reader;
            Connection();

            SqlCommand cmd = new SqlCommand("SELECT MemberID From Member WHERE Email = '" + email + "'", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["MemberID"].ToString());
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
            cmd.Parameters.AddWithValue("@address", member.MemAddress);

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
            cmd.Parameters.AddWithValue("@password", member.MemPass);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["MemPass"].ToString();
                    //if (Crypto.VerifyHashedPassword(password, member.Password))
                    //{
                    //    firstName = reader["FirstName"].ToString();
                    //}

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
        public int GetMemTypeID()
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conStringLocal"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT TOP(1) MemTypeID FROM MembershipType ORDER BY MemTypeID DESC", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            int result = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    result += int.Parse(dr[0].ToString());

                }
            }
            conn.Close();
            return result;

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

        public Member getMemberObject(string email)
        {
            Member result = null;
            SqlDataReader reader;
            Connection();

            SqlCommand cmd = new SqlCommand("SELECT * From Member WHERE Email = '" + email + "'" , conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        result = new Member(int.Parse(reader["memTypeID"].ToString()),
                            reader["email"].ToString(), reader["memPass"].ToString(), reader["firstName"].ToString(),
                            reader["lastName"].ToString(), reader["gender"].ToString(),
                            int.Parse(reader["age"].ToString()), reader["phone"].ToString(), reader["memAddress"].ToString(), (bool)reader["isAdmin"]);
                    }
                }
                else
                {
                    return null;
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

        public List<Class> ShowAllClasses()
        {
            List<Class> classList = new List<Class>();
            SqlCommand cmd;
            SqlDataReader reader;
            //Calling connection method to establish connection string
            Connection();
            cmd = new SqlCommand("SELECT * FROM Class", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Class newClass = new Class();

                    newClass.ClassID = int.Parse(reader["ClassID"].ToString());
                    newClass.TrainerID = int.Parse(reader["TrainerID"].ToString());
                    newClass.Time = reader["Time"].ToString();
                    newClass.ClassType = reader["ClassType"].ToString();
                    newClass.Location = reader["Location"].ToString();
                    newClass.MaxMembers = int.Parse(reader["MaxMembers"].ToString());
                    classList.Add(newClass);
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

    }
}