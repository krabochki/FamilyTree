using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Family_Tree
{
    public class People
    {

        public int PersonId { get; set; }
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(MiddleName) && string.IsNullOrEmpty(LastName))
                {
                    return $"{FirstName}";
                }
                else if (string.IsNullOrEmpty(MiddleName))
                {
                    return $"{LastName} {FirstName}";
                }
                else if (string.IsNullOrEmpty(LastName))
                {
                    return $"{FirstName} {MiddleName}";
                }
                else
                {
                    return $"{LastName} {FirstName} {MiddleName}";
                }
            }
        }

        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }


        public string Age
        {
            get
            {
                if (BirthDate == DateTime.MinValue)
                {
                    return "?";
                }
                if (IsAlive == false && DeathDate == DateTime.MinValue)
                {
                    return "?";
                }
                else
                {
                    int age = 0;

                    if (IsAlive == false)
                    {
                        DateTime today = DateTime.Today;
                        age = DeathDate.Year - BirthDate.Year;
                        if (today.Month < BirthDate.Month || (today.Month == BirthDate.Month && today.Day < BirthDate.Day))
                        {

                            age--;
                        }
                    }
                    else
                    {
                        DateTime today = DateTime.Today;
                        age = today.Year - BirthDate.Year;
                        if (today.Month < BirthDate.Month || (today.Month == BirthDate.Month && today.Day < BirthDate.Day))
                        {
                            age--;
                        }
                    }
                    if (age >= 0 && age <= 130)
                    {
                        return age.ToString();
                    }
                    else {  return "?"; }

                }
            }
        }

        public bool IsAdult
        {
            get {

                if (Age == "?") return true;
                if (int.Parse(Age) <= 17) return false;
                return true;
                    
            }

        }
        public bool IsSenior
        {
            get
            {

                if (Age == "?") return false;
                if (int.Parse(Age) >= 65) return true;
                return false;

            }

        }
        public string MaidenName { get; set; }
        public string Occupation { get; set; }
        public int ResidenceId { get; set; }
        public string ZodiacSign
        {
            get
            {
               
                int day = BirthDate.Day;
                int month = BirthDate.Month;
                string sign = string.Empty;

                DateTime emptyDate = new DateTime(1, 1, 1);
                if (BirthDate == emptyDate)
                {
                    sign = "";
                    return sign;
                }


                if ((month == 3 && day >= 21) || (month == 4 && day <= 19))
                {
                    sign = "Овен";
                }
                else if ((month == 4 && day >= 20) || (month == 5 && day <= 20))
                {
                    sign = "Телец";
                }
                else if ((month == 5 && day >= 21) || (month == 6 && day <= 20))
                {
                    sign = "Близнецы";
                }
                else if ((month == 6 && day >= 21) || (month == 7 && day <= 22))
                {
                    sign = "Рак";
                }
                else if ((month == 7 && day >= 23) || (month == 8 && day <= 22))
                {
                    sign = "Лев";
                }
                else if ((month == 8 && day >= 23) || (month == 9 && day <= 22))
                {
                    sign = "Дева";
                }
                else if ((month == 9 && day >= 23) || (month == 10 && day <= 22))
                {
                    sign = "Весы";
                }
                else if ((month == 10 && day >= 23) || (month == 11 && day <= 21))
                {
                    sign = "Скорпион";
                }
                else if ((month == 11 && day >= 22) || (month == 12 && day <= 21))
                {
                    sign = "Стрелец";
                }
                else if ((month == 12 && day >= 22) || (month == 1 && day <= 19))
                {
                    sign = "Козерог";
                }
                else if ((month == 1 && day >= 20) || (month == 2 && day <= 18))
                {
                    sign = "Водолей";
                }
                else if ((month == 2 && day >= 19) || (month == 3 && day <= 20))
                {
                    sign = "Рыбы";
                }

                return sign;
            }
        }
        public string Religion { get; set; }
        public DateTime BirthDate { get; set; }
        public int BirthPlaceId { get; set; }
        public string Notes { get; set; }
        public bool IsAlive { get; set; }
        public DateTime DeathDate { get; set; }
        public int DeathPlaceId { get; set; }
        public string CauseOfDeath { get; set; }
        public int FatherId { get; set; }
        public int MotherId { get; set; }
        public string Photo
        {
            get; set;


        }



        public string Role { get; set; }

        public List<Participants> relates = new List<Participants>();


        public override string ToString()
        {
            string showRole = "", showName = "";

            if (!string.IsNullOrEmpty(LastName))
            {
                showName = $"{FirstName} {LastName}";
            }
            else
            {
                showName = $"{FirstName}";
            }
            if (!string.IsNullOrEmpty(Role))
            {
                showRole = $" ({Role})";
            }
            return $"{showName}{showRole}";
        }
        public static List<People> GetPeople(string nameOfDatabaseFile)
        {

            List<People> people = new List<People>();

            string connect =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={nameOfDatabaseFile};Jet OLEDB:Engine Type=5";
            OleDbConnection dbconnection = new OleDbConnection(connect);
            dbconnection.Open();

            string query = "SELECT * FROM People";  // запрос
            OleDbCommand dbCommand = new OleDbCommand(query, dbconnection);
            OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

            while (dbDataReader.Read())
            {
                People person = new People();
                person.PersonId = Convert.ToInt32(dbDataReader["PersonId"]);
                person.Gender = dbDataReader["Gender"] != DBNull.Value
                                    ? dbDataReader["Gender"].ToString()
                                    : null;
                person.FirstName = dbDataReader["FirstName"] != DBNull.Value
                                       ? dbDataReader["FirstName"].ToString()
                                       : null;
                person.LastName = dbDataReader["LastName"] != DBNull.Value
                                      ? dbDataReader["LastName"].ToString()
                                      : null;
                person.MiddleName = dbDataReader["MiddleName"] != DBNull.Value
                                        ? dbDataReader["MiddleName"].ToString()
                                        : null;
                person.MaidenName = dbDataReader["MaidenName"] != DBNull.Value
                                        ? dbDataReader["MaidenName"].ToString()
                                        : null;
                person.Occupation = dbDataReader["Occupation"] != DBNull.Value
                                        ? dbDataReader["Occupation"].ToString()
                                        : null;
                person.ResidenceId = dbDataReader["ResidenceId"] != DBNull.Value
                                         ? Convert.ToInt32(dbDataReader["ResidenceId"])
                                         : 0;
                person.Religion = dbDataReader["Religion"] != DBNull.Value
                                      ? dbDataReader["Religion"].ToString()
                                      : null;

                if (dbDataReader["BirthDate"] != DBNull.Value)
                {
                    person.BirthDate = Convert.ToDateTime(dbDataReader["BirthDate"]).Date;
                }
                person.BirthPlaceId =
                    dbDataReader["BirthPlaceId"] != DBNull.Value
                        ? Convert.ToInt32(dbDataReader["BirthPlaceId"])
                        : 0;
                person.Notes = dbDataReader["Notes"] != DBNull.Value
                                   ? dbDataReader["Notes"].ToString()
                                   : null;
                person.IsAlive = dbDataReader["IsAlive"] != DBNull.Value
                                     ? Convert.ToBoolean(dbDataReader["IsAlive"])
                                     : false;

                if (dbDataReader["DeathDate"] != DBNull.Value)
                {
                    person.DeathDate = Convert.ToDateTime(dbDataReader["DeathDate"]).Date;
                }
                person.DeathPlaceId =
                    (dbDataReader["DeathPlaceId"] != DBNull.Value)
                        ? Convert.ToInt32(dbDataReader["DeathPlaceId"])
                        : 0;
                person.CauseOfDeath = dbDataReader["CauseOfDeath"] != DBNull.Value
                                          ? dbDataReader["CauseOfDeath"].ToString()
                                          : null;
                person.FatherId = (dbDataReader["FatherId"] != DBNull.Value)
                                      ? Convert.ToInt32(dbDataReader["FatherId"])
                                      : 0;
                person.MotherId = (dbDataReader["MotherId"] != DBNull.Value)
                                      ? Convert.ToInt32(dbDataReader["MotherId"])
                                      : 0;
                person.Photo = dbDataReader["Photo"] != DBNull.Value
                                   ? dbDataReader["Photo"].ToString()
                                   : null;

                people.Add(person);
            }

            dbDataReader.Close();
            dbconnection.Close();

            return people;
        }
        public static void DeletePerson(string nameOfDatabaseFile, People selectedItem ){

            try
            {
                string connect =
                               $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={nameOfDatabaseFile};Jet OLEDB:Engine Type=5";
                OleDbConnection dbconnection = new OleDbConnection(connect);
                dbconnection.Open();


                string query =
                $"DELETE FROM People WHERE PersonId={selectedItem.PersonId}";

                OleDbCommand dbCommand = new OleDbCommand(query, dbconnection);
                dbCommand.ExecuteNonQuery();


                    query =
                        $"UPDATE People SET MotherId = 0 WHERE MotherId={selectedItem.PersonId}";
                    dbCommand = new OleDbCommand(query, dbconnection);
                    dbCommand.ExecuteNonQuery();
               
                    query =
                        $"UPDATE People SET FatherId = 0 WHERE FatherId={selectedItem.PersonId}";
                    dbCommand = new OleDbCommand(query, dbconnection);
                    dbCommand.ExecuteNonQuery();
                
              


                query = $"DELETE FROM Events WHERE EventId IN (SELECT EventId FROM Participants WHERE EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {selectedItem.PersonId} GROUP BY EventId HAVING COUNT(*) = 1 ) AND PersonId = {selectedItem.PersonId} AND EventId IN ( SELECT EventId FROM Participants GROUP BY EventId HAVING COUNT(*) = 1 ));";

                dbCommand = new OleDbCommand(query, dbconnection);
                dbCommand.ExecuteNonQuery();


                query = $"DELETE FROM Events WHERE EventId IN (SELECT EventId FROM Participants WHERE EventId IN ( SELECT Participants.EventId FROM Participants WHERE Participants.EventId IN ( SELECT EventId FROM Participants GROUP BY EventId HAVING COUNT(PersonId) = {selectedItem.PersonId} )) AND PersonId={selectedItem.PersonId});";

                dbCommand = new OleDbCommand(query, dbconnection);
                dbCommand.ExecuteNonQuery();



                query = $"DELETE FROM Events WHERE EventId IN (SELECT EventId FROM Participants WHERE PersonId = {selectedItem.PersonId} AND (Role = 'Новорождённый' OR Role = 'Умерший'));";

                dbCommand = new OleDbCommand(query, dbconnection);
                dbCommand.ExecuteNonQuery();


                query = $"DELETE FROM Participants WHERE EventId IN (SELECT EventId FROM Participants WHERE PersonId = {selectedItem.PersonId} AND (Role = 'Новорождённый' OR Role = 'Умерший'));";

                dbCommand = new OleDbCommand(query, dbconnection);
                dbCommand.ExecuteNonQuery();

                query =
                  $"DELETE FROM Participants Where PersonId = {selectedItem.PersonId}";

                dbCommand = new OleDbCommand(query, dbconnection);
                dbCommand.ExecuteNonQuery();

                dbconnection.Close();

               string folder  = System.IO.Path.GetDirectoryName(nameOfDatabaseFile);
                ;
                string deleteFile = folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\People\\" + selectedItem.Photo;
                    if (System.IO.File.Exists(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\People\\" + selectedItem.Photo))
                    {

                        try
                        {
                            System.IO.File.Delete(deleteFile);
                        }
                        catch
                        {
                        }
                    }
                
            }
            catch 
            {
            }
        }

        public static void ShowPerson(string nameOfDatabaseFile, People selectedItem)
        {

            string selectedPersonId = selectedItem.PersonId.ToString();
            Show show_person =
            new Show(nameOfDatabaseFile, selectedPersonId, "Person");
            show_person.ShowDialog();
        }
    }
}
