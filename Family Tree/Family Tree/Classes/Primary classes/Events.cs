using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Tree
{
    public class Events
    {
        public int EventId { get; set; }
        public string Type { get; set; }
        public DateTime? EventDate { get; set; }
        public string FormattedEventDate
        {
            get
            {
        return EventDate.HasValue ? EventDate.Value.ToString("dd.MM.yyyy") : string.Empty;
            }
        }
        public int EventLocationId { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string EventStatus
        {
            get
            {
               
                if ((EventDate < DateTime.Today) || (Type == "Рождение") || (Type=="Смерть"))
                {
                    return "Прошло";
                }
                if (!EventDate.HasValue) // Проверяем, есть ли значение в EventDate
                {
                    return "Неизвестно"; // Возвращаем "Неизвестно", если EventDate не имеет значения
                }
                else
                {
                    return "Предстоит";
                }

            }
        }


        public string DaysUntilAnniversary
        {
            get
            {
                DateTime today = DateTime.Today;
                DateTime? eventDate = EventDate; // Присваиваем значение EventDate другой переменной типа DateTime?

                if (!eventDate.HasValue) // Проверяем, есть ли значение в eventDate
                {
                    return "?"; // Возвращаем пустую строку, если eventDate не имеет значения
                }

                DateTime nextAnniversary = new DateTime(today.Year, eventDate.Value.Month, eventDate.Value.Day);

                if (nextAnniversary < today)
                {
                    nextAnniversary = nextAnniversary.AddYears(1);
                }

                TimeSpan timeUntilAnniversary = nextAnniversary - today;
                int daysUntilAnniversary = timeUntilAnniversary.Days;

                return $"{daysUntilAnniversary}";

            }
        }

        public string Picture { get; set; }




        public static List<Events> GetEvents(string nameOfDatabaseFile)
        {
            List<Events> events = new List<Events>();

            string connect =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={nameOfDatabaseFile};Jet OLEDB:Engine Type=5";
            using (OleDbConnection dbConnection = new OleDbConnection(connect))
            {
                dbConnection.Open();

                string query = "SELECT * FROM Events";
                using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                {
                    using (OleDbDataReader dbDataReader = dbCommand.ExecuteReader())
                    {
                        while (dbDataReader.Read())
                        {
                            Events evt = new Events();
                            evt.EventId = Convert.ToInt32(dbDataReader["EventId"]);
                            evt.Type = dbDataReader["Type"] != DBNull.Value
                                           ? dbDataReader["Type"].ToString()
                                           : null;
                            if (dbDataReader["EventDate"] != DBNull.Value)
                            {
                                evt.EventDate =
                                    Convert.ToDateTime(dbDataReader["EventDate"]).Date;
                            }
                            evt.EventLocationId =
                                (dbDataReader["EventLocationId"] != DBNull.Value)
                                    ? Convert.ToInt32(dbDataReader["EventLocationId"])
                                    : 0;
                            evt.Description = dbDataReader["Description"] != DBNull.Value
                                                  ? dbDataReader["Description"].ToString()
                                                  : null;
                            evt.Comment = dbDataReader["Comment"] != DBNull.Value
                                              ? dbDataReader["Comment"].ToString()
                                              : null;
                            evt.Picture = dbDataReader["Picture"] != DBNull.Value
                                              ? dbDataReader["Picture"].ToString()
                                              : null;

                            events.Add(evt);
                        }
                    }
                }
                dbConnection.Close();
            }

            return events;
        }

        public static void DeleteEvent(string nameOfDatabaseFile, Events selectedItem)
        {

            string connect =
                          $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={nameOfDatabaseFile};Jet OLEDB:Engine Type=5";
            OleDbConnection dbconnection = new OleDbConnection(connect);
            dbconnection.Open();



            string query = $"DELETE * FROM Events WHERE EventId={selectedItem.EventId}";
            OleDbCommand dbCommand = new OleDbCommand(query, dbconnection);
            dbCommand.ExecuteNonQuery();

            query =
                $"DELETE * FROM Participants WHERE EventId={selectedItem.EventId}";
            dbCommand = new OleDbCommand(query, dbconnection);
            dbCommand.ExecuteNonQuery();

            string folder = System.IO.Path.GetDirectoryName(nameOfDatabaseFile);
            ;
            string deleteFile = folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Events\\" + selectedItem.Picture;
            if (System.IO.File.Exists(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Events\\" + selectedItem.Picture))
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

        public static void ShowEvent(string nameOfDatabaseFile, Events selectedItem)
        {

            string selectedEventId = selectedItem.EventId.ToString();

            Show showEvent =
                new Show(nameOfDatabaseFile, selectedEventId, "Event");
            showEvent.Show();
        }

    }
}
