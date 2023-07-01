
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Family_Tree
{
    /// <summary>
    /// Логика взаимодействия для ShowPerson.xaml
    /// </summary>
    /// 
    public partial class Show : Window
    {
        string folder = "";

        string ID;
        string databaseFilePath;
        string address;
        bool doesPhoto;
        string whattoshow;

        public Show(string databasefilepath, string id, string whatToShow)
        {
            InitializeComponent();

            folder = System.IO.Path.GetDirectoryName(databasefilepath);
            whattoshow = whatToShow;
            ID = id;
            databaseFilePath = databasefilepath;
            switch (whatToShow)
            {
                case "Person":
                    Show_Person();
                    break;
                case "Event":
                    Show_Event();
                    break;
                case "Place":
                    Show_Place();
                    break;
            }

        }


        private void Show_New_Show(string whattoshow, int id)
        {

            try
            {
                if (whattoshow == "people")
                {
                    int selected_person_id = id;

                    Show show_person =
                        new Show(databaseFilePath, id.ToString(), "Person");
                    show_person.Show();
                }
                if (whattoshow == "places")
                {
                    int selected_place_id = id;

                    Show show_place =
                        new Show(databaseFilePath, id.ToString(), "Place");
                    show_place.Show();
                }
                if (whattoshow == "events")
                {
                    int selected_event_id = id;

                    Show show_event =
                        new Show(databaseFilePath, id.ToString(), "Event");
                    show_event.Show();
                }
            }
            catch
            {

            }
        }
        private void Show_Person()
        {
            geo_button.Width = 0;
            geo_button.Height = 0;
            People person = new People();
            Title += ": Просмотр персоны";
            List<People> husbands = new List<People>();
            List<People> exes = new List<People>();
            List<People> parentsOfChildren = new List<People>();


            string mumName = "",
                   dadName = "",
                   birthPlaceName = "",
                   deathPlaceName = "",
                   residenceName = "";

            List<People> siblings = new List<People>();
            List<People> children = new List<People>();
            string husbandOrWife = "";
            string momOrDad = "";
            string exhusbandOrexWife = "";
            string connect = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFilePath};Jet OLEDB:Engine Type=5";

            using (OleDbConnection dbConnection = new OleDbConnection(connect))
            {
                dbConnection.Open();
                string sqlQuery = $"SELECT * FROM People WHERE PersonId = {ID}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            person.PersonId = reader.GetInt32(0);
                            person.FirstName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            person.LastName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                            person.MiddleName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            person.Gender = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            person.MaidenName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                            person.Occupation = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                            person.ResidenceId = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                            person.Religion = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                            person.BirthDate = reader.IsDBNull(9) ? default(DateTime) : reader.GetDateTime(9);
                            person.BirthPlaceId = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                            person.Notes = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                            person.IsAlive = reader.IsDBNull(12) ? false : reader.GetBoolean(12);
                            person.DeathDate = reader.IsDBNull(13) ? DateTime.MinValue : reader.GetDateTime(13);
                            person.DeathPlaceId = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                            person.CauseOfDeath = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                            person.FatherId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                            person.MotherId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                            person.Photo = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);

                        }
                    }
                }
                husbandOrWife = (person.Gender == "Женский пол") ? "Муж" : "Жена";
                string whoAmi1 = (person.Gender == "Женский пол") ? "Жена" : "Муж";
                string whoAmi2 = (person.Gender == "Женский пол") ? "Бывшая жена" : "Бывший муж";
                husbandOrWife = (person.Gender == "Женский пол") ? "Муж" : "Жена";
                exhusbandOrexWife = (person.Gender == "Женский пол") ? "Бывший муж" : "Бывшая жена";
                momOrDad = (person.Gender == "Женский пол") ? "FatherId" : "MotherId";

                sqlQuery = $"SELECT Name FROM Places WHERE PlaceId = {person.BirthPlaceId}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            birthPlaceName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        }
                    }
                }
                sqlQuery = $"SELECT Name FROM Places WHERE PlaceId = {person.DeathPlaceId}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            deathPlaceName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);


                        }
                    }
                }
                sqlQuery = $"SELECT Name FROM Places WHERE PlaceId = {person.ResidenceId}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            residenceName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);


                        }
                    }
                }

                sqlQuery = $"SELECT FirstName,LastName FROM People WHERE PersonId = {person.MotherId}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            string firstname = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                            string lastname = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                mumName = firstname + " " + lastname;



                        }
                    }

                }
                sqlQuery = $"SELECT FirstName,LastName FROM People WHERE PersonId = {person.FatherId}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstname = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                            string lastname = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            

                                dadName = firstname + " " + lastname;
                           

                        }
                    }
                }

                int personId = person.PersonId; // Заданный айди человека


                sqlQuery = $"SELECT PersonId FROM Participants WHERE EventID IN ( SELECT EventId FROM Events WHERE (EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {personId} AND Role = '{whoAmi1}' ) AND Type = 'Брак' AND Role = '{husbandOrWife}') ) AND PersonId <> {personId};";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            People husband = new People()
                            {
                                PersonId = reader.GetInt32(0)
                           
                            };
                            husbands.Add(husband);
                        }
                    }
                }





                sqlQuery = $"SELECT PersonId FROM Participants WHERE EventID IN ( SELECT EventId FROM Events WHERE (EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {personId}  AND Role = '{whoAmi2}') AND Type = 'Развод' AND Role = '{exhusbandOrexWife}') ) AND PersonId <> {personId};";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            People ex = new People()
                            {
                                PersonId = reader.GetInt32(0)

                            };
                            exes.Add(ex);

                            
                        } }
                }

                string dadOrMom = momOrDad == "FatherId" ? "MotherId" : "FatherId";
                string role1 = momOrDad == "FatherId" ? "Бывший муж" : "Бывшая жена";
                string role2 = momOrDad == "FatherId" ? "Муж" : "Жена";

                sqlQuery = $"SELECT DISTINCT {momOrDad} FROM People Where ({dadOrMom}={person.PersonId} AND {momOrDad} NOT IN ( SELECT PersonId FROM Participants WHERE EventID IN ( SELECT EventId FROM Events WHERE (EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {person.PersonId} ) AND Type = 'Брак' AND Role = '{role2}') ) AND PersonId <> {person.PersonId} )) AND {momOrDad} NOT IN (SELECT PersonId FROM Participants WHERE EventID IN ( SELECT EventId FROM Events WHERE (EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {person.PersonId} ) AND Type = 'Развод' AND Role = '{role1}') ) AND PersonId <> {person.PersonId})";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            People parent = new People()
                            {
                                PersonId = reader.GetInt32(0)

                            };
                            parentsOfChildren.Add(parent);


                        }
                    }
                }



                foreach (var husband in husbands.ToList())
                {
                    foreach (var ex in exes)
                    {
                        if (husband.PersonId == ex.PersonId)
                        {
                            husbands.Remove(husband);
                        }
                    }
                }

                foreach (var husband in husbands) {
                    if (husband.PersonId!=0)
                    {
                        sqlQuery = $"SELECT FirstName,LastName FROM People WHERE PersonId = {husband.PersonId}";
                        using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {

                                    string firstname = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                                    string lastname = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                    husband.LastName = lastname;
                                    husband.FirstName = firstname;





                                }
                            }
                        }
                    } }

                foreach (var parent in parentsOfChildren)
                {
                    if (parent.PersonId != 0)
                    {
                        sqlQuery = $"SELECT FirstName,LastName FROM People WHERE PersonId = {parent.PersonId}";
                        using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {

                                    string firstname = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                                    string lastname = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                    parent.LastName = lastname;
                                    parent.FirstName = firstname;





                                }
                            }
                        }
                    }
                }


                foreach (var ex in exes)
                {
                    if (ex.PersonId != 0)
                    {
                        sqlQuery = $"SELECT FirstName,LastName FROM People WHERE PersonId = {ex.PersonId}";
                        using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {

                                    string firstname = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                                    string lastname = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                    ex.LastName = lastname;
                                    ex.FirstName = firstname;





                                }
                            }
                        }
                    }
                }




                if (person.MotherId != 0 && person.FatherId != 0)
                {
                    sqlQuery = $"SELECT PersonId,FirstName,LastName, Gender FROM People WHERE (MotherId = {person.MotherId} OR FatherId =  {person.FatherId}) AND PersonId <> {person.PersonId}";
                }


                else if (person.FatherId == 0 && person.MotherId != 0)
                {
                    sqlQuery = $"SELECT PersonId,FirstName,LastName, Gender FROM People WHERE (MotherId = {person.MotherId}) AND PersonId <> {person.PersonId}";

                }
                else if (person.MotherId == 0 && person.FatherId != 0)
                {
                    sqlQuery = $"SELECT PersonId,FirstName,LastName, Gender FROM People WHERE (FatherId = {person.FatherId}) AND PersonId <> {person.PersonId}";

                }

                if (!((person.FatherId == 0) && (person.MotherId == 0)))
                {

                    using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {

                            // Чтение результатов запроса и добавление их в список
                            while (reader.Read())
                            {
                                int siblingid = 0;
                                string siblingfirstname = string.Empty;
                                string siblinglastname = string.Empty;
                                string siblinggender = string.Empty;
                                if (!reader.IsDBNull(0))
                                {
                                    siblingid = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    siblingfirstname = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    siblinglastname = reader.GetString(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    siblinggender = reader.GetString(3);
                                }
                                People sibling = new People
                                {
                                    PersonId = siblingid,
                                    LastName = siblinglastname,
                                    FirstName = siblingfirstname,
                                    Gender = siblinggender
                                };

                                siblings.Add(sibling);
                            }

                        }
                    }

                }
                string whoAmI = (person.Gender == "Женский пол") ? "MotherId" : "FatherId";
                string whichId = (person.Gender == "Женский пол") ? person.PersonId.ToString() : person.PersonId.ToString();

                sqlQuery = $"SELECT PersonId,FirstName,LastName, Gender FROM People WHERE ({whoAmI} = {whichId})";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {

                        // Чтение результатов запроса и добавление их в список
                        while (reader.Read())
                        {
                            string childfirstname = string.Empty;
                            int childid = 0;
                            string childlastname = string.Empty;
                            string childgender = string.Empty;
                            if (!reader.IsDBNull(0))
                            {
                                childid = reader.GetInt32(0);
                            }
                            if (!reader.IsDBNull(1))
                            {
                                childfirstname = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                childlastname = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                childgender = reader.GetString(3);
                            }
                            People child = new People
                            {
                                PersonId = childid,
                                LastName = childlastname,
                                FirstName = childfirstname,
                                Gender = childgender
                            };

                            children.Add(child);
                        }

                    }
                }

                dbConnection.Close();


            }

            fullname_textblock.Text = person.FullName;

           

            if (!person.IsAlive)
            {
                fullname_textblock.Inlines.Add(new Run { Text = $" †", FontSize = 30, FontFamily = new FontFamily("Arial") });
            }


            string gender = (person.Gender == "Женский пол") ? "Женский" : ((person.Gender == "Мужской пол") ? "Мужской" : "Неизвестно");
            TextBlock textBlock = new TextBlock();
            textBlock.Inlines.Add(new Run { Text = $"Пол: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
            textBlock.Inlines.Add(new Run { Text = $"{gender}", FontSize = 14 });
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.Margin = new Thickness(0, 0, 0, 2);
            textblock.Inlines.Add(textBlock);

            string birthdate = (person.BirthDate != DateTime.MinValue) ? person.BirthDate.ToShortDateString() : "";
            string zodiac_sign = (birthdate != "") ? (" (" + person.ZodiacSign + ")") : "";
            string birth_place = !string.IsNullOrEmpty(birthPlaceName) ? (((person.BirthDate == DateTime.MinValue) ? birthPlaceName : ", " + birthPlaceName)) : "";

            if (!(birthdate == "" && birth_place == ""))
            {
                textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку

                textBlock = new TextBlock();
                textBlock.Inlines.Add(new Run { Text = $"Рождение: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                textBlock.Inlines.Add(new Run { Text = $"{birthdate}{zodiac_sign}", FontSize = 14 });

                if (!string.IsNullOrEmpty(birth_place))
                {
                    Hyperlink hyperlink = new Hyperlink(new Run(birth_place));
                    hyperlink.FontSize = 14;
                    hyperlink.Click += (sender, e) => Show_New_Show("places", person.BirthPlaceId);
                    hyperlink.Foreground = Brushes.White;
                    hyperlink.Cursor = Cursors.Hand;
                    hyperlink.TextDecorations = new TextDecorationCollection();

                    textBlock.Inlines.Add(hyperlink);
                }
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 2);
                textblock.Inlines.Add(textBlock);

            }


            if (!person.IsAlive)
            {

                string deathdate = (person.DeathDate != DateTime.MinValue) ? person.DeathDate.ToShortDateString() : "";
                string death_place = !string.IsNullOrEmpty(deathPlaceName) ? (((person.DeathDate == DateTime.MinValue) ? deathPlaceName : ", " + deathPlaceName)) : "";
                string death_reason = !string.IsNullOrEmpty(person.CauseOfDeath) ? (((string.IsNullOrEmpty(deathPlaceName) && string.IsNullOrEmpty(deathdate)) ? person.CauseOfDeath : (" (" + person.CauseOfDeath.ToLower() + ")"))) : "";

                if (!(deathdate == "" && death_place == "" && death_reason == ""))
                {
                    textBlock = new TextBlock();
                    textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку


                    textBlock.Inlines.Add(new Run { Text = $"Смерть: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });

                    textBlock.Inlines.Add(new Run { Text = $"{deathdate}", FontSize = 14 });
                    textBlock.TextAlignment = TextAlignment.Left;
                    textBlock.Margin = new Thickness(0, 0, 0, 2);


                    if (!string.IsNullOrEmpty(deathPlaceName))
                    {
                        Hyperlink hyperlink = new Hyperlink(new Run(death_place));
                        hyperlink.FontSize = 14;
                        hyperlink.Click += (sender, e) => Show_New_Show("places", person.DeathPlaceId);
                        hyperlink.Foreground = Brushes.White;
                        hyperlink.Cursor = Cursors.Hand;
                        hyperlink.TextDecorations = new TextDecorationCollection();

                        textBlock.Inlines.Add(hyperlink);
                    }

                    textBlock.Inlines.Add(new Run { Text = $"{death_reason}", FontSize = 14 });
                    textBlock.TextAlignment = TextAlignment.Left;
                    textBlock.Margin = new Thickness(0, 0, 0, 2);

                    textblock.Inlines.Add(textBlock);


                }

            }


            string age_or_live_length = !person.IsAlive ? "Продолжительность жизни" : "Возраст";
            string age = person.Age.ToString();

            textBlock = new TextBlock();
            textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку

            textBlock.Inlines.Add(new Run { Text = $"{age_or_live_length}: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
            textBlock.Inlines.Add(new Run { Text = $"{age}", FontSize = 14 });
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.Margin = new Thickness(0, 0, 0, 2);
            textblock.Inlines.Add(textBlock);
            textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку

            string live_place = residenceName;
            if (!string.IsNullOrEmpty(live_place))
            {
                textBlock = new TextBlock();
                textBlock.Inlines.Add(new Run { Text = "Место жительства: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });


                Hyperlink hyperlink = new Hyperlink(new Run(live_place));
                hyperlink.FontSize = 14;
                hyperlink.Click += (sender, e) => Show_New_Show("places", person.ResidenceId);
                hyperlink.Foreground = Brushes.White;
                hyperlink.Cursor = Cursors.Hand;
                hyperlink.TextDecorations = new TextDecorationCollection();

                textBlock.Inlines.Add(hyperlink);
                
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 2);
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку
            }


            string occupation = person.Occupation;
            if (!string.IsNullOrEmpty(occupation))
            {
                textBlock = new TextBlock();
                textBlock.Inlines.Add(new Run { Text = "Деятельность: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                textBlock.Inlines.Add(new Run { Text = $"{occupation}", FontSize = 14 });
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 2);
                textblock.Inlines.Add(textBlock);

                textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку
            }

            string religion = person.Religion;
            if (!string.IsNullOrEmpty(religion))
            {
                textBlock = new TextBlock();
                textBlock.Inlines.Add(new Run { Text = "Религия: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                textBlock.Inlines.Add(new Run { Text = $"{religion}", FontSize = 14 });
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 2);
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak());
            }

            string note = person.Notes;
            if (!string.IsNullOrEmpty(note))
            {
                textBlock = new TextBlock();
                textblock.Inlines.Add(new LineBreak());
                textBlock.Inlines.Add(new Run { Text = $"❝ {note} ❞", FontSize = 14, FontFamily= (FontFamily)Application.Current.Resources["Bold"] });
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 2);
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak());
            }

            textblock.Inlines.Add(new LineBreak());

            if (person.FatherId != 0)
            {
                textBlock = new TextBlock();
                textBlock.Inlines.Add(new Run { Text = "Отец: ", FontSize = 12, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });

                Hyperlink hyperlink = new Hyperlink(new Run(dadName));
                hyperlink.FontSize = 12;
                hyperlink.Click += (sender, e) => Show_New_Show("people", person.FatherId);
                hyperlink.Foreground = Brushes.White;
                hyperlink.Cursor = Cursors.Hand;
                hyperlink.TextDecorations = new TextDecorationCollection();

                textBlock.Inlines.Add(hyperlink);


                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 1);
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak());
            }
            if (person.MotherId != 0)
            {
                textBlock = new TextBlock();
                textBlock.Inlines.Add(new Run { Text = "Мать: ", FontSize = 12, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });


                Hyperlink hyperlink = new Hyperlink(new Run(mumName));
                hyperlink.FontSize = 12;
                hyperlink.Click += (sender, e) => Show_New_Show("people", person.MotherId);
                hyperlink.Foreground = Brushes.White;
                hyperlink.Cursor = Cursors.Hand;
                hyperlink.TextDecorations = new TextDecorationCollection();


                textBlock.Inlines.Add(hyperlink);
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 1);
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak());
            }

            if (husbands.Count != 0)

            {
                foreach (var husband in husbands)
                {
                    if (husband.PersonId != 0)
                    {
                        textBlock = new TextBlock();
                        textBlock.Inlines.Add(new Run { Text = $"{husbandOrWife}: ", FontSize = 12, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                        Hyperlink hyperlink = new Hyperlink(new Run(husband.FirstName + " " + husband.LastName));
                        hyperlink.FontSize = 12;
                        hyperlink.Click += (sender, e) => Show_New_Show("people", husband.PersonId);
                        hyperlink.Foreground = Brushes.White;
                        hyperlink.Cursor = Cursors.Hand;
                        hyperlink.TextDecorations = new TextDecorationCollection();

                        textBlock.Inlines.Add(hyperlink);

                        textBlock.TextAlignment = TextAlignment.Left;
                        textBlock.Margin = new Thickness(0, 0, 0, 1);
                        textblock.Inlines.Add(textBlock);
                        textblock.Inlines.Add(new LineBreak());
                    }

                }
            }


            if (exes.Count != 0)

            {
                foreach (var ex in exes)
                {
                    if (ex.PersonId != 0)
                    {
                        textBlock = new TextBlock();
                        textBlock.Inlines.Add(new Run { Text = $"{exhusbandOrexWife}: ", FontSize = 12, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                        Hyperlink hyperlink = new Hyperlink(new Run(ex.FirstName + " " + ex.LastName));
                        hyperlink.FontSize = 12;
                        hyperlink.Click += (sender, e) => Show_New_Show("people", ex.PersonId);
                        hyperlink.Foreground = Brushes.White;
                        hyperlink.Cursor = Cursors.Hand;
                        hyperlink.TextDecorations = new TextDecorationCollection();

                        textBlock.Inlines.Add(hyperlink);

                        textBlock.TextAlignment = TextAlignment.Left;
                        textBlock.Margin = new Thickness(0, 0, 0, 1);
                        textblock.Inlines.Add(textBlock);
                        textblock.Inlines.Add(new LineBreak());
                    }

                }
            }

            momOrDad = (momOrDad == "FatherId") ?"Отец ребенка":"Мать ребенка";

            if (parentsOfChildren.Count != 0)

            {
                foreach (var parent in parentsOfChildren)
                {
                    if (parent.PersonId != 0)
                    {
                        textBlock = new TextBlock();
                        textBlock.Inlines.Add(new Run { Text = $"{momOrDad}: ", FontSize = 12, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                        Hyperlink hyperlink = new Hyperlink(new Run(parent.FirstName + " " + parent.LastName));
                        hyperlink.FontSize = 12;
                        hyperlink.Click += (sender, e) => Show_New_Show("people", parent.PersonId);
                        hyperlink.Foreground = Brushes.White;
                        hyperlink.Cursor = Cursors.Hand;
                        hyperlink.TextDecorations = new TextDecorationCollection();

                        textBlock.Inlines.Add(hyperlink);

                        textBlock.TextAlignment = TextAlignment.Left;
                        textBlock.Margin = new Thickness(0, 0, 0, 1);
                        textblock.Inlines.Add(textBlock);
                        textblock.Inlines.Add(new LineBreak());
                    }

                }
            }

            if (siblings.Count != 0)

            {
                foreach (People sibling in siblings)
                {
                    string whoAreYou = (sibling.Gender == "Женский пол") ? "Сестра" : (sibling.Gender == "Мужской пол") ? "Брат" : "Неизвестно";
                    textBlock = new TextBlock();
                    textBlock.Inlines.Add(new Run { Text = $"{whoAreYou}: ", FontSize = 12, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });

                    Hyperlink hyperlink = new Hyperlink(new Run($"{sibling.FirstName} {sibling.LastName}"));
                    hyperlink.FontSize = 12;
                    hyperlink.Click += (sender, e) => Show_New_Show("people", sibling.PersonId);
                    hyperlink.Foreground = Brushes.White;
                    hyperlink.Cursor = Cursors.Hand;
                    hyperlink.TextDecorations = new TextDecorationCollection();

                    textBlock.Inlines.Add(hyperlink);

                    textBlock.TextAlignment = TextAlignment.Left;
                    textBlock.Margin = new Thickness(0, 0, 0, 1);
                    textblock.Inlines.Add(textBlock);
                    textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку

                }
            }

            if (children.Count != 0)
            {
                foreach (People child in children)
                {
                    string whoAreYou = (child.Gender == "Женский пол") ? "Дочь" : (child.Gender == "Мужской пол") ? "Сын" : "Неизвестно";

                    textBlock = new TextBlock();
                    textBlock.Inlines.Add(new Run { Text = $"{whoAreYou}: ", FontSize = 12, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                    Hyperlink hyperlink = new Hyperlink(new Run($"{child.FirstName} {child.LastName}"));
                    hyperlink.FontSize = 12;
                    hyperlink.Click += (sender, e) => Show_New_Show("people", child.PersonId);
                    hyperlink.Foreground = Brushes.White;
                    hyperlink.Cursor = Cursors.Hand;
                    hyperlink.TextDecorations = new TextDecorationCollection();

                    textBlock.Inlines.Add(hyperlink);
                    textblock.Inlines.Add(textBlock);

                    textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку

                }
            }





            doesPhoto = false;

            try
            {





                BitmapImage photo = new BitmapImage();

                var stream = System.IO.File.OpenRead(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\People\\" + person.Photo);

                photo.BeginInit();
                photo.CacheOption = BitmapCacheOption.OnLoad;
                photo.StreamSource = stream;
                photo.EndInit();
                stream.Close();
                stream.Dispose();

                personPhoto_image.Source = photo;


                textblock.Margin = new Thickness(30, 20, 25, 0);
                doesPhoto = true;

            }
            catch
            {
                imageblock.Height = 0;
                imageblock.Width = 0;
                Grid.SetColumn(textblock, 0);
                Grid.SetColumnSpan(textblock, 2);
                doesPhoto = false;
            }
        }


        private void Show_Place()
        {

            Places place = new Places();

            Title += ": Просмотр места";


            string connect = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFilePath};Jet OLEDB:Engine Type=5";

            using (OleDbConnection dbConnection = new OleDbConnection(connect))
            {

                dbConnection.Open();
                string sqlQuery = $"SELECT * FROM Places WHERE PlaceId = {ID}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {



                            place.PlaceId = reader.GetInt32(0);
                            place.Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            place.Abbreviation = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            place.Historical_Name = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                            place.Type = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            place.Description = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                            place.Address = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                            place.Comment = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                            place.Picture = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);


                        }
                    }
                }





                dbConnection.Close();


            }

            string abbreviation = place.Abbreviation;
            fullname_textblock.Text = place.Name;



            TextBlock textBlock;


            if (!string.IsNullOrEmpty(abbreviation))
            {
                textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;

                textBlock.Margin = new Thickness(0, 0, 0, 2);

                textBlock.Inlines.Add(new Run { Text = "Аббревиатура: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                textBlock.Inlines.Add(new Run { Text = $"{abbreviation}", FontSize = 14 });
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку
            }


            string historical_name = place.Historical_Name;
            if (!string.IsNullOrEmpty(historical_name))
            {
                textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;

                textBlock.Margin = new Thickness(0, 0, 0, 2);

                textBlock.Inlines.Add(new Run { Text = "Историческое название: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                textBlock.Inlines.Add(new Run { Text = $"{historical_name}", FontSize = 14 });
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку
            }


            string type = place.Type;
            if (!string.IsNullOrEmpty(type))
            {
                textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;

                textBlock.Margin = new Thickness(0, 0, 0, 2);

                textBlock.Inlines.Add(new Run { Text = "Тип: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                textBlock.Inlines.Add(new Run { Text = $"{type}", FontSize = 14 });
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку
            }

            string description = place.Description;
            if (!string.IsNullOrEmpty(description))
            {
                textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 2);

                textBlock.Inlines.Add(new Run { Text = "Описание: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                textBlock.Inlines.Add(new Run { Text = $"{description}", FontSize = 14 });
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak());


            }



            string address = place.Address;

            if (!string.IsNullOrEmpty(address))
            {

                this.address = address;
                textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Margin = new Thickness(0, 0, 0, 2);
                textblock.VerticalAlignment = VerticalAlignment.Center;

                textBlock.Inlines.Add(new Run { Text = "Адрес: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                textBlock.Inlines.Add(new Run { Text = $"{address}", FontSize = 14 });
                textblock.Inlines.Add(textBlock);
                textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку
            }
            else
            {
                geo_button.Height = 0;
                geo_button.Width = 0;
            }
            string comment = place.Comment;




            if (!string.IsNullOrEmpty(comment))
            {
                comment_block.Text += "❝ " + comment + " ❞";





                comment_block.SizeChanged += (sender, e) =>
                {
                    double height = comment_block.ActualHeight;
                    // Используйте значение высоты здесь
                    Height += height + 27;

                };


            }

            doesPhoto = false;


            try
            {
               


                BitmapImage photo = new BitmapImage();

                var stream = System.IO.File.OpenRead(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Places\\" + place.Picture);

                photo.BeginInit();
                photo.CacheOption = BitmapCacheOption.OnLoad;
                photo.StreamSource = stream;
                photo.EndInit();
                stream.Close();
                stream.Dispose();

                personPhoto_image.Source = photo;

                doesPhoto = true;
            } 
            catch
            {

                personPhoto_image.Height = 0;
                personPhoto_image.Width = 0;
                Grid.SetColumn(textblock, 0);
                Grid.SetColumnSpan(textblock, 2);
                doesPhoto = false;

            }

            double heightblock = textblock_height;

            if (doesPhoto == true)
            {
                heightblock = 300;
                personPhoto_image.ToolTip = new ToolTip()
                {
                    Style = (Style)FindResource("ToolTipStyle"),
                    Content = "Фото места " + place.Name
                };
                textblock.Margin = new Thickness(30, 20, 25, 0);
                Height += 12;
 

            }





        }

        double textblock_height = 0;


        private void textblock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textblock_height = textblock.ActualHeight;



            if (doesPhoto && (textblock_height < 200))
            {

                Height += 10;


                textblock_height = 205;
            }
            if(doesPhoto && (textblock_height > 200) && (whattoshow =="Person"))
            {
                Height += 15;
            }



            Height += textblock_height;


            








        }

        private void Find_Geo(object sender, RoutedEventArgs e)
        {

            string encodedAddress = Uri.EscapeDataString(address);
            string mapsUrl = $"https://www.google.com/maps/search/?api=1&query={encodedAddress}";
            Process.Start(mapsUrl);
        }




        private void Show_Event()
        {

            Events my_event = new Events();


            Places location = new Places();
            Title += ": Просмотр события";
            List<People> event_participants = new List<People>();
            List<Participants> participants = new List<Participants>();

            string connect = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFilePath};Jet OLEDB:Engine Type=5";

            using (OleDbConnection dbConnection = new OleDbConnection(connect))
            {

                dbConnection.Open();
                string sqlQuery = $"SELECT * FROM Events WHERE EventId = {ID}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {

                    using (OleDbDataReader dbDataReader = command.ExecuteReader())
                    {
                        while (dbDataReader.Read())
                        {
                            my_event.EventId = Convert.ToInt32(dbDataReader["EventId"]);
                            my_event.Type = dbDataReader["Type"] != DBNull.Value
                                           ? dbDataReader["Type"].ToString()
                                           : null;
                            if (dbDataReader["EventDate"] != DBNull.Value)
                            {
                                my_event.EventDate =
                                    Convert.ToDateTime(dbDataReader["EventDate"]).Date;
                            }
                            my_event.EventLocationId =
                                (dbDataReader["EventLocationId"] != DBNull.Value)
                                    ? Convert.ToInt32(dbDataReader["EventLocationId"])
                                    : 0;
                            my_event.Description = dbDataReader["Description"] != DBNull.Value
                                                  ? dbDataReader["Description"].ToString()
                                                  : null;
                            my_event.Comment = dbDataReader["Comment"] != DBNull.Value
                                              ? dbDataReader["Comment"].ToString()
                                              : null;
                            my_event.Picture = dbDataReader["Picture"] != DBNull.Value
                                             ? dbDataReader["Picture"].ToString()
                                             : null;

                        }
                    }
                }

                sqlQuery = $"SELECT Name FROM Places WHERE PlaceId = {my_event.EventLocationId}";
                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            location.PlaceId = my_event.EventLocationId;
                            location.Name = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);

                        }
                    }
                }

                sqlQuery = $"SELECT PersonId, Role FROM Participants WHERE EventId = {my_event.EventId}";

                using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {

                        // Чтение результатов запроса и добавление их в список
                        while (reader.Read())
                        {
                            int personid = 0;
                            int eventid = my_event.EventId;
                            string role = string.Empty;

                            if (!reader.IsDBNull(0))
                            {
                                personid = reader.GetInt32(0);
                            }
                            if (!reader.IsDBNull(1))
                            {
                                role = reader.GetString(1);
                            }

                            Participants participant = new Participants
                            {
                                EventId = eventid,
                                PersonId = personid,
                                Role = role
                            };

                            participants.Add(participant);
                        }

                    }
                }



                foreach (Participants participant in participants)
                {
                    sqlQuery = $"SELECT FirstName, LastName FROM People WHERE PersonId = {participant.PersonId}";
                    using (OleDbCommand command = new OleDbCommand(sqlQuery, dbConnection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {

                            // Чтение результатов запроса и добавление их в список
                            while (reader.Read())
                            {
                                string firstname = string.Empty;
                                string lastname = string.Empty;
                                if (!reader.IsDBNull(0))
                                {
                                    firstname = reader.GetString(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    lastname = reader.GetString(1);
                                }

                                People person = new People
                                {
                                    LastName = lastname,
                                    FirstName = firstname,
                                };

                                event_participants.Add(person);
                            }

                        }

                    }

                }
                dbConnection.Close();
            }


                fullname_textblock.Text = my_event.Type;

                TextBlock textBlock;

                if (my_event.EventDate.HasValue)
                {
                    textBlock = new TextBlock();
                    textBlock.TextAlignment = TextAlignment.Left;
                    textBlock.Margin = new Thickness(0, 0, 0, 2);
                    textBlock.Inlines.Add(new Run { Text = "Дата события: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                    textBlock.Inlines.Add(new Run { Text = $"{my_event.FormattedEventDate}", FontSize = 14 });
                    textblock.Inlines.Add(textBlock);
                    textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку

                    textBlock = new TextBlock();
                    textBlock.TextAlignment = TextAlignment.Left;

                    textBlock.Margin = new Thickness(0, 0, 0, 2);

                    textBlock.Inlines.Add(new Run { Text = "Статус: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                    textBlock.Inlines.Add(new Run { Text = $"{my_event.EventStatus}", FontSize = 14 });
                    textblock.Inlines.Add(textBlock);
                    textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку

                    textBlock = new TextBlock();

                    textBlock.Inlines.Add(new Run { Text = "Дней до годовщины: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                    textBlock.Inlines.Add(new Run { Text = $"{my_event.DaysUntilAnniversary}", FontSize = 14 });
                    textblock.Inlines.Add(textBlock);
                    textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку
                }



                if (!string.IsNullOrEmpty(location.Name))
                {
                    textBlock = new TextBlock();
                    textBlock.TextAlignment = TextAlignment.Left;
                    textBlock.Margin = new Thickness(0, 0, 0, 2);
                    textBlock.Inlines.Add(new Run { Text = "Место события: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });

                    // Создание гиперссылки для названия локации
                    Hyperlink hyperlink = new Hyperlink(new Run(location.Name));
                    hyperlink.FontSize = 14;
                    hyperlink.Click += (sender, e) => Show_New_Show("places", location.PlaceId);
                    hyperlink.Foreground = Brushes.White;
                    hyperlink.Cursor = Cursors.Hand;
                    hyperlink.TextDecorations =  new TextDecorationCollection();

                textBlock.Inlines.Add(hyperlink);
                    textblock.Inlines.Add(textBlock);

                    textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку
                }

               

                 
                


                string description = my_event.Description;
                if (!string.IsNullOrEmpty(description))
                {
                    textBlock = new TextBlock();
                    textBlock.TextAlignment = TextAlignment.Left;
                    textBlock.Margin = new Thickness(0, 0, 0, 2);

                    textBlock.Inlines.Add(new Run { Text = "Описание: ", FontSize = 14, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });
                    textBlock.Inlines.Add(new Run { Text = $"{description}", FontSize = 14 });
                    textblock.Inlines.Add(textBlock);
                    textblock.Inlines.Add(new LineBreak());


                }

                geo_button.Height = 0;
                geo_button.Width = 0;


            doesPhoto = false;


            try
            {
               
                BitmapImage photo = new BitmapImage();

                var stream = System.IO.File.OpenRead(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Events\\" + my_event.Picture);

                photo.BeginInit();
                photo.CacheOption = BitmapCacheOption.OnLoad;
                photo.StreamSource = stream;
                photo.EndInit();
                stream.Close();
                stream.Dispose();

                personPhoto_image.Source = photo;

                textblock.Margin = new Thickness(30, 20, 25, 0);
                doesPhoto = true;

            }
            catch
            {
                imageblock.Height = 0;
                imageblock.Width = 0;
                Grid.SetColumn(textblock, 0);
                Grid.SetColumnSpan(textblock, 2);
                doesPhoto = false;
            }


           

                string comment = my_event.Comment;

            textblock.Inlines.Add(new LineBreak());

            if (event_participants.Count != 0)
            {
                foreach (People person in event_participants)
                {

                    int index = event_participants.IndexOf(person);

                    string whoAreYou = participants[index].Role;

                    textBlock = new TextBlock();
                    textBlock.Inlines.Add(new Run { Text = $"{whoAreYou}: ", FontSize = 12, FontFamily = (FontFamily)Application.Current.Resources["DemiBold"] });



                    Hyperlink hyperlink = new Hyperlink(new Run($"{person.FirstName} {person.LastName}"));
                    hyperlink.FontSize = 12;
                    hyperlink.Click += (sender, e) => Show_New_Show("people", participants[index].PersonId);
                    hyperlink.Foreground = Brushes.White;
                    hyperlink.Cursor = Cursors.Hand;
                    hyperlink.TextDecorations = new TextDecorationCollection();

                    textBlock.Inlines.Add(hyperlink);

                    textBlock.TextAlignment = TextAlignment.Left;
                    textBlock.Margin = new Thickness(0, 0, 0, 1);


                    textblock.Inlines.Add(textBlock);
                    textblock.Inlines.Add(new LineBreak()); // Добавление перехода на новую строку

                }
            }




            if (!string.IsNullOrEmpty(comment))
                {
                    comment_block.Text += "❝ " + comment + " ❞";

                    comment_block.SizeChanged += (sender, e) =>
                    {
                        double height = comment_block.ActualHeight;
                        // Используйте значение высоты здесь
                        Height += height + 20;

                    };


                }

            

        }
  

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            double newWindowHeight = e.NewSize.Height;
            double newWindowWidth = e.NewSize.Width;



            int resultHeight;
            int resultScreenHeight;
            int resultWinHeight;
            int resultWidth;
            int resultScreenWidth;
            int resultWinWidth;


            resultScreenHeight = Convert.ToInt32(SystemParameters.PrimaryScreenHeight);
            resultWinHeight = Convert.ToInt32(newWindowHeight);
            resultHeight = (resultScreenHeight - resultWinHeight) / 2;
            Top = resultHeight;

            resultScreenWidth = Convert.ToInt32(SystemParameters.PrimaryScreenWidth);
            resultWinWidth = Convert.ToInt32(newWindowWidth);

            resultWidth = (resultScreenWidth - resultWinWidth) / 2;
            //Применение результата, к положению окна по ширине
            Left = resultWidth;
        }

        private void fullname_textblock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double height = e.NewSize.Height;
            if (height > 30)
            {
                Height += e.NewSize.Height - 30;
            }
        }
    }
}
