 using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family_Tree
{
    public class Places
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Historical_Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public string Picture { get; set; }



        public static List<Places> GetPlaces(string nameOfDatabaseFile)
        {


            List<Places> places = new List<Places>();

            string connect =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={nameOfDatabaseFile};Jet OLEDB:Engine Type=5";
            OleDbConnection dbconnection = new OleDbConnection(connect);
            dbconnection.Open();

            string query = "SELECT * FROM Places";  //запрос
            OleDbCommand dbCommand = new OleDbCommand(query, dbconnection);
            OleDbDataReader dbDataReader = dbCommand.ExecuteReader();

            while (dbDataReader.Read())
            {
                Places place = new Places();
                place.PlaceId = Convert.ToInt32(dbDataReader["PlaceId"]);
                place.Name = dbDataReader["Name"] != DBNull.Value
                                 ? dbDataReader["Name"].ToString()
                                 : string.Empty;
                place.Abbreviation = dbDataReader["Abbreviation"] != DBNull.Value
                                         ? dbDataReader["Abbreviation"].ToString()
                                         : string.Empty;
                place.Historical_Name = dbDataReader["Historical_name"] != DBNull.Value
                                            ? dbDataReader["Historical_name"].ToString()
                                            : string.Empty;
                place.Type = dbDataReader["Type"] != DBNull.Value
                                 ? dbDataReader["Type"].ToString()
                                 : string.Empty;
                place.Description = dbDataReader["Description"] != DBNull.Value
                                        ? dbDataReader["Description"].ToString()
                                        : string.Empty;
                place.Address = dbDataReader["Address"] != DBNull.Value
                                    ? dbDataReader["Address"].ToString()
                                    : string.Empty;
                place.Comment = dbDataReader["Comment"] != DBNull.Value
                                    ? dbDataReader["Comment"].ToString()
                                    : string.Empty;
                place.Picture = dbDataReader["Picture"] != DBNull.Value
                                    ? dbDataReader["Picture"].ToString()
                                    : string.Empty;
                places.Add(place);
            }

            dbDataReader.Close();
            dbconnection.Close();

            return places;
        }

        public static void DeletePlace(string nameOfDatabaseFile, Places selectedItem)
        {

            string connect =
                          $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={nameOfDatabaseFile};Jet OLEDB:Engine Type=5";
            OleDbConnection dbconnection = new OleDbConnection(connect);
            dbconnection.Open();


            string query = $"DELETE * FROM Places WHERE PlaceId={selectedItem.PlaceId}";

            OleDbCommand dbCommand = new OleDbCommand(query, dbconnection);
            dbCommand.ExecuteNonQuery();

            query =
                $"UPDATE People SET ResidenceId = 0 WHERE ResidenceId={selectedItem.PlaceId}";
            dbCommand = new OleDbCommand(query, dbconnection);
            dbCommand.ExecuteNonQuery();


            query =
                $"UPDATE People SET BirthPlaceId = 0 WHERE BirthPlaceId={selectedItem.PlaceId}";
            dbCommand = new OleDbCommand(query, dbconnection);
            dbCommand.ExecuteNonQuery();
            query =
                $"UPDATE People SET DeathPlaceId = 0 WHERE DeathPlaceId={selectedItem.PlaceId}";
            dbCommand = new OleDbCommand(query, dbconnection);
            dbCommand.ExecuteNonQuery();
            query =
                $"UPDATE Events SET EventLocationId = 0 WHERE EventLocationId={selectedItem.PlaceId}";
            dbCommand = new OleDbCommand(query, dbconnection);
            dbCommand.ExecuteNonQuery();

            string folder = System.IO.Path.GetDirectoryName(nameOfDatabaseFile);
            ;
            string deleteFile = folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Places\\" + selectedItem.Picture;
            if (System.IO.File.Exists(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Places\\" + selectedItem.Picture))
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
        public static void ShowPlace(string nameOfDatabaseFile, Places selectedItem)
        {

            string selectedPlaceId = selectedItem.PlaceId.ToString();
            Show show_place =
                new Show(nameOfDatabaseFile, selectedPlaceId, "Place");
            show_place.Show();
        }

        public override string ToString()
        {
           
            
            return Name;
        }

    }
}