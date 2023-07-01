using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Family_Tree
{


    public partial class AddEditEvent : Window
    {
        private string databaseFilePath;
        private readonly string folder="";
        List<People> selectedPeople = new List<People>();
        List<Events> events = new List<Events>();
        Places selectedPlace = new Places();
        People selectedPerson = new People();
        string imageFilePath;
        private readonly string AddOrEdit = "";
        private readonly int editEventId;
        public AddEditEvent(string filePath, string AddOrEdit, int editEventId)
        {
            InitializeComponent();

            databaseFilePath = filePath;
            folder = System.IO.Path.GetDirectoryName(filePath);

            this.AddOrEdit = AddOrEdit;
            if (AddOrEdit == "Edit")
            {
                this.editEventId = editEventId;
                Title = "Family Tree: Изменение места";
            }

            events = Events.GetEvents(databaseFilePath);


            roleTextBox.IsEnabled = false;
            participantsComboBox.IsEnabled = false;
            buttonDeleteParticipant.IsEnabled = false;
            buttonAddParticipant.IsEnabled = false;
            roleTextBox.Opacity = 0.5;
            participantsComboBox.Opacity = 0.5;
            buttonAddParticipant.Opacity = 0.5;
            buttonDeleteParticipant.Opacity = 0.5;
            textBox.Opacity = 0.5;

            string connect = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFilePath};Jet OLEDB:Engine Type=5";
            using (OleDbConnection dbConnection = new OleDbConnection(connect))
            {
                dbConnection.Open();
                List<Places> placeList = new List<Places>();
                List<Participants> partList = new List<Participants>();
                Events event_ = events.Find(p => p.EventId == editEventId);
                List<People> peopleList = new List<People>();

                // Выполнение SQL-запроса на выборку данных
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = dbConnection;
                    // Запрос и чтение данных для списка людей
                    string sqlQuery = "SELECT PersonId, FirstName, LastName, Gender FROM People";
                    command.CommandText = sqlQuery;
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        People emptyPerson = new People();

                        peopleList.Add(emptyPerson);


                        // Чтение результатов запроса и добавление их в список
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            string lastName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            string gender = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);

                            People person = new People
                            {
                                PersonId = id,
                                FirstName = firstName,
                                Gender = gender,
                                LastName = lastName
                            };

                            peopleList.Add(person);

                        }
                        participantsComboBox.ItemsSource = peopleList;
                        participantsComboBox.DisplayMemberPath = "FullName";


                        if (peopleList.Count == 1)
                        {
                            participantsComboBox.IsEnabled = false;
                            participantsComboBox.Opacity = 0.5;
                            participantsComboBox.ItemsSource = null;


                        }



                    }

                    // Запрос и чтение данных для списка мест
                    sqlQuery = "SELECT PlaceId, Name, Abbreviation FROM Places";
                    command.CommandText = sqlQuery;

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        Places emptyPlace = new Places();
                        placeList.Add(emptyPlace);


                        // Чтение результатов запроса и добавление их в список
                        while (reader.Read())
                        {
                            int placeId = reader.GetInt32(0);
                            string name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            string abbreviation = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);


                            Places place = new Places
                            {
                                PlaceId = placeId,
                                Name = name,
                                Abbreviation = abbreviation
                            };

                            placeList.Add(place);
                        }


                        placesComboBox.ItemsSource = placeList;


                        if (placeList.Count == 1)
                        {
                            placesComboBox.IsEnabled = false;
                            placesComboBox.Opacity = 0.5;
                            placesComboBox.ItemsSource = null;
                        }
                    }

                    if (AddOrEdit == "Edit")
                    {
                        sqlQuery = $"SELECT PersonId, Role FROM Participants WHERE EventId  = {event_.EventId}";

                        command.CommandText = sqlQuery;

                        using (OleDbDataReader reader2 = command.ExecuteReader())
                        {


                            using (reader2)
                            {

                                while (reader2.Read())
                                {
                                    int personId = reader2.GetInt32(0);
                                    string role = reader2.IsDBNull(1) ? string.Empty : reader2.GetString(1);


                                    Participants part = new Participants
                                    {
                                        PersonId = personId,
                                        Role = role
                                    };

                                    partList.Add(part);
                                }
                                foreach (var part in partList)
                                {
                                    foreach (var pipl in peopleList)
                                    {
                                        if (pipl.PersonId == part.PersonId)
                                        {
                                            People selected = new People()
                                            {
                                                PersonId = part.PersonId,
                                                FirstName = pipl.FirstName,
                                                LastName = pipl.LastName,
                                                Role = part.Role
                                            };
                                            selectedPeople.Add(selected);
                                        }
                                    }
                                }
                            }
                        }



                    }
                }
                dbConnection.Close();


                if (AddOrEdit == "Edit")
                {
                    UpdateParticipantsTextBox();
                   
                    typeComboBox.Text = event_.Type;
                    roleTextBox.IsEnabled = true;
                    participantsComboBox.IsEnabled = true;
                    buttonDeleteParticipant.IsEnabled = true;
                    buttonAddParticipant.IsEnabled = true;
                    roleTextBox.Opacity = 1;
                    participantsComboBox.Opacity = 1;
                    buttonAddParticipant.Opacity = 1;
                    buttonDeleteParticipant.Opacity = 1;
                    textBox.Opacity = 1;

                    if (event_.Type == "Рождение" || event_.Type == "Смерть")
                    {

                        typeComboBox.Opacity = 0.5;
                        typeComboBox.IsEnabled = false;
                        Warning warning = new Warning();
                        warning.Title = "Внимание!";
                        warning.Height += 50;
                        warning.Width += 50;
                        warning.warning_textblock.Height += 30;
                        warning.warning_textblock.Width += 35;
                        warning.OKbutton.Content = "Понятно";
                        
                        datePickerOfEvent.Opacity = 0.5;
                        datePickerOfEvent.IsEnabled = false;
                        placesComboBox.Opacity = 0.5;
                        placesComboBox.IsEnabled = false;


                      






                        if (event_.Type == "Рождение")
                        {
                            warning.warning_textblock.Text = "Чтобы изменить дату и место события рождения, измените соответствующие поля у родившейся персоны. Удалить или поменять главного участника нельзя, так как это событие является обязательным для каждой персоны!";

                            People borningPerson = selectedPeople.Find(person => person.Role == "Новорождённый");
                            peopleList.RemoveAll(person => person.PersonId == borningPerson.PersonId);
                            participantsComboBox.ItemsSource = peopleList;


                        }
                        else
                        {
                            warning.warning_textblock.Text = "Чтобы изменить дату и место события смерти, измените соответствующие поля у умершей персоны. Удалить или поменять главного участника нельзя, так как это событие является обязательным для умерших персон!";

                            People deceasedPerson = selectedPeople.Find(person => person.Role == "Умерший");
                            peopleList.RemoveAll(person => person.PersonId == deceasedPerson.PersonId);
                            participantsComboBox.ItemsSource = peopleList;



                        }
                        warning.ShowDialog();

                        UpdateRoles();
                    }
                        DateTime emptyDate = new DateTime(1, 1, 1);
                        if (event_.EventDate != emptyDate)
                        {
                            datePickerOfEvent.SelectedDate = event_.EventDate;
                        }

                        placesComboBox.SelectedItem =
                           placeList.Find(p => p.PlaceId == event_.EventLocationId);


                        notesTextBox.Text = event_.Comment;
                        descriptiontextbox.Text = event_.Description;


                    if (event_.Picture != "")
                    {

                        try
                        {

                            imageFilePath = event_.Picture;

                            BitmapImage photo = new BitmapImage();

                            var stream = System.IO.File.OpenRead(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Events\\" + event_.Picture);

                            photo.BeginInit();
                            photo.CacheOption = BitmapCacheOption.OnLoad;
                            photo.StreamSource = stream;
                            photo.EndInit();
                            stream.Close();
                            stream.Dispose();

                            eventPhoto.Source = photo;
                        }
                        catch 
                        {
                        }
                    }
                    

                
                
            }
        }


        }

        private void DeleteParticipantFromList(object sender, RoutedEventArgs e)
        {
            if (participantsComboBox.SelectedItem != null)
            {
                People deletablePerson = participantsComboBox.SelectedItem as People;
                int personid = deletablePerson.PersonId;

                foreach(var select in selectedPeople)
                {
                    if (select.PersonId == personid)
                    {
                        selectedPeople.Remove(select);
                    }
                }
              
                roleTextBox.SelectedItem = null;

                participantsComboBox.SelectedItem = null;
                                UpdateParticipantsTextBox();
            }
        }
        private void AddParticipantToList(object sender, RoutedEventArgs e)
        {
            string itsrole = roleTextBox.Text;
 

                foreach (var selected in selectedPeople)
                {
                if (((itsrole == "Муж" || itsrole == "Жена" || itsrole == "Невеста"
                  || itsrole == "Жених" || itsrole == "Бывший муж" || itsrole == "Бывшая жена" || itsrole == "Возлюбленный" || itsrole == "Возлюбленная" || itsrole == "Бывший возлюбленный"
                  || itsrole == "Бывшая возлюбленная") )&& itsrole == selected.Role)
                {
                    return;
                }
                }
            People selectedPerson = participantsComboBox.SelectedItem as People;
            selectedPerson.Role = roleTextBox.Text;
            if (selectedPerson != null && !string.IsNullOrEmpty(selectedPerson.Role))
            {
                if (!selectedPeople.Contains(selectedPerson))
                {
                    selectedPeople.Add(selectedPerson);
                }

                participantsComboBox.SelectedItem = null;
                roleTextBox.SelectedItem = null;

                UpdateParticipantsTextBox();
                

                roleTextBox.Text = "";

            }
           
          
        }
        private void UpdateParticipantsTextBox()
        {

            textBox.Text = string.Join(", ", selectedPeople);
        }
        private void SaveEventToDatabase(object sender, RoutedEventArgs e)
        {


          

            

           

            bool first = false;
            bool second =  false;
            switch (typeComboBox.Text)
            {


                case "Свадьба":
                    foreach (var selected in selectedPeople)
                    {
                        if (selected.Role == "Жених")
                        {
                            first = true;
                        }
                        else if (selected.Role == "Невеста")
                        {
                            second = true;
                        }

                    }
               
                    break;
                case "Брак":
                    foreach (var selected in selectedPeople)
                    {
                        if (selected.Role == "Муж")
                        {
                            first = true;
                        }
                        else if (selected.Role == "Жена")
                        {
                            second = true;
                        }

                    }
                    break;
                case "Развод":
                   foreach (var selected in selectedPeople)
                    {
                        if (selected.Role == "Бывший муж")
                        {
                            first = true;
                        }
                        else if (selected.Role == "Бывшая жена")
                        {
                            second = true;
                        }

                    }
                    break;
              
                case "Расставание":
                  
                    foreach (var selected in selectedPeople)
                    {
                        if (selected.Role == "Бывший возлюбленный")
                        {
                            first = true;
                        }
                        else if (selected.Role == "Бывшая возлюбленная")
                        {
                            second = true;
                        }

                    }
                    break;
             
         
                case "Свидание":
                    foreach (var selected in selectedPeople)
                    {
                        if (selected.Role == "Девушка")
                        {
                            first = true;
                        }
                        else if (selected.Role == "Парень")
                        {
                            second = true;
                        }

                    }
                    break;
                case "Начало отношений":
                    foreach (var selected in selectedPeople)
                    {
                        if (selected.Role == "Возлюбленный")
                        {
                            first = true;
                        }
                        else if (selected.Role == "Возлюбленная")
                        {
                            second = true;
                        }

                    }

                    break;
                default:
                    second = true;
                    first = true;
                    break;
             



            }
                
            
            if (!(second && first))
            {

                Warning.WarningShow("Для добавления события не хватает участников с нужными ролями. Попробуйте ещё раз.", "Не хватает участников");
                return;
            }

            if (selectedPeople.Count == 0)
            {
                Warning.WarningShow("У события нет ни единого участника! Свяжите его хотя бы с одной персоной.", "Не хватает участников");
                return;
            }

            string eventtype = typeComboBox.Text;

            eventtype = eventtype.Replace("'", "’");



            string eventdate = "";
            if (datePickerOfEvent.SelectedDate.HasValue)

            {
                eventdate = datePickerOfEvent.SelectedDate?.ToString();

            }
            string eventlocation = selectedPlace.PlaceId.ToString();
            string description = descriptiontextbox.Text;
            description = description.Replace("'", "’");

            string comment = notesTextBox.Text;
            comment = comment.Replace("'", "’");

            string image = imageFilePath;

            string sourceFilePath = imageFilePath;
            string destinationFilePath = folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Events\\" + System.IO.Path.GetFileName(sourceFilePath);
            ;


            
            Events event_ = events.Find(p => p.EventId == editEventId);



            if ( ( AddOrEdit=="Edit" && (event_.Picture != imageFilePath)) || AddOrEdit =="Add")
            {
                if (System.IO.File.Exists(destinationFilePath))
                {
                    string targetDirectory = System.IO.Path.GetDirectoryName(destinationFilePath);
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(destinationFilePath);
                    string fileExtension = System.IO.Path.GetExtension(destinationFilePath);

                    // Генерация нового имени файла с добавлением числа
                    int count = 1;
                    string newFileName = $"{fileName} ({count}){fileExtension}";
                    string newFilePath = System.IO.Path.Combine(targetDirectory, newFileName);

                    while (System.IO.File.Exists(newFilePath))
                    {
                        count++;
                        newFileName = $"{fileName} ({count}){fileExtension}";
                        newFilePath = System.IO.Path.Combine(targetDirectory, newFileName);
                    }

                    destinationFilePath = newFilePath;
                }

                try
                {

                    System.IO.File.Copy(sourceFilePath, destinationFilePath);

                }

                catch
                {
                }
                image = System.IO.Path.GetFileName(destinationFilePath);


            }

            eventdate = string.IsNullOrEmpty(eventdate) ? "NULL" : $"'{eventdate}'";
            string connect = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFilePath};Jet OLEDB:Engine Type=5";


            using (OleDbConnection dbConnection = new OleDbConnection(connect))
            {
                try
                {
                    dbConnection.Open();

                    if (AddOrEdit == "Add")
                    {
                        string maxIdQuery = "SELECT MAX(EventId) FROM Events";
                        int lastEventId = 0;

                        string event_id = lastEventId.ToString();




                        using (OleDbCommand maxIdCommand = new OleDbCommand(maxIdQuery, dbConnection))
                        {
                            object result = maxIdCommand.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lastEventId = Convert.ToInt32(result);
                            }
                        }

                        lastEventId++;
                         event_id = lastEventId.ToString();


                        string query = $"INSERT INTO Events VALUES({event_id},'{eventtype}',{eventdate},'{eventlocation}','{description}','{comment}','{image}')";

                        using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                        {
                            dbCommand.ExecuteNonQuery();
                        }

                        foreach (People person in selectedPeople)
                        {
                            string personID = person.PersonId.ToString();
                            string eventID = event_id;
                            string role = person.Role.ToString();

                            query = $"INSERT INTO Participants VALUES({eventID},{personID},'{role}')";

                            using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                            {
                                dbCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    else
                    {
                        string query = $"UPDATE Events SET EventDate ={eventdate},EventLocationId='{eventlocation}',Description='{description}',Comment='{comment}',Picture='{image}' WHERE EventId = {editEventId}";
                        using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                        {
                            dbCommand.ExecuteNonQuery();
                        }
                        query = $"DELETE FROM Participants WHERE EventId = {editEventId}";
                        using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                        {
                            dbCommand.ExecuteNonQuery();
                        }
                        foreach (People person in selectedPeople)
                        {
                            string personID = person.PersonId.ToString();
                            string eventID = editEventId.ToString();
                            string role = person.Role.ToString();

                            query = $"INSERT INTO Participants VALUES({eventID},{personID},'{role}')";

                            using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                            {
                                dbCommand.ExecuteNonQuery();
                            }
                        }


                        if ((AddOrEdit == "Edit" && (event_.Picture != imageFilePath)) || AddOrEdit == "Add")
                        {
                            string deleteFile = folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Events\\" + event_.Picture;
                            if (System.IO.File.Exists(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Events\\" + event_.Picture))
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
                    }
                
                }
                catch
                {
                }
            }


            Close();
            

        }
        private void SelectingPlaceFromCombobox(object sender,
                                                SelectionChangedEventArgs e)
        {
            selectedPlace = placesComboBox.SelectedItem as Places;
        }
        private void CloseForm(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void LoadPhoto(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
                "Файлы изображений (*.jpg, *.png)|*.jpg;*.png|Все файлы (*.*)|*.*";

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    imageFilePath = openFileDialog.FileName;

                    // Загрузка выбранного файла в Source картинки
                    BitmapImage bitmap = new BitmapImage(new Uri(imageFilePath));
                    eventPhoto.Source = bitmap;
                }
            }
            catch
            {
                Warning.WarningShow("Возникла непредвиденная ошибка загрузки фотографии. Попробуйте ещё раз!", "Ошибка загрузки файла");
            
                
            }
        }
        private void DeletePhoto(object sender, RoutedEventArgs e)
        {
            imageFilePath = string.Empty;
        }

        private void SelectingTypeFromCombobox(object sender, EventArgs e)
        {
            string type = typeComboBox.Text;
            selectedPeople.Clear();
            textBox.Text = "";
            switch (type)
            {

                case "Свадьба":
                    descriptiontextbox.Text =
                        "Свадьба - это особенное и радостное событие, когда две любящие сердца объединяются в браке. " +
                        "В этот день наступает новая глава их жизни, наполненная любовью и обещаниями верности.";
           

                    break;
                case "Брак":
                    descriptiontextbox.Text =
                        "Брак - это официальное закрепление любви и союза двух людей. В этот момент возникает особая прочность и взаимопонимание, которые сопровождают их на протяжении всего совместного пути.";
              
                    break;
                case "Развод":
                    descriptiontextbox.Text =
                        "Развод - это окончание брака и разрыв семейных уз, который может быть сопряжен с горечью и разочарованием. В этот момент наступает переосмысление и новый этап жизни для обоих партнеров.";
    
                    break;
                case "Путешествие":
                    descriptiontextbox.Text =
                        "Путешествие - это возможность исследовать новые места, познакомиться с разными культурами и создать незабываемые воспоминания. В этот период открываются новые горизонты и укрепляются отношения.";

                    break;
                case "Переезд":
                    descriptiontextbox.Text =
                        "Переезд - это перемещение в новое место жительства, что может быть связано с началом новой главы в жизни, новыми возможностями и вызовами.";
             



                    break;
                case "Расставание":
                    descriptiontextbox.Text =
                        "Расставание - это трудный и эмоциональный момент, когда двое людей принимают решение о завершении своих отношений и разъезде. В этот период происходят изменения в жизни и поиск новых путей.";
              

                    break;
                case "Выпускной":
                    descriptiontextbox.Text =
                        "Выпускной - это особое событие, отмечающее завершение учебы или курса обучения. В этот день студенты и учащиеся отмечают свои достижения и готовятся к новому этапу в своей жизни.";



                    break;
                case "Повышение":
                    descriptiontextbox.Text =
                        "Повышение - это признание и вознаграждение за усилия и профессиональные достижения. В этот момент человек получает новые возможности и ответственность в рабочей сфере.";
             


                    break;
                case "Свидание":
                    descriptiontextbox.Text =
                        "Свидание - это специальное время, проведенное двумя влюбленными людьми, чтобы укрепить их отношения и насладиться взаимной компанией.";
                    break;
                case "Начало отношений":
                    descriptiontextbox.Text =
                        "Начало отношений - это момент, когда два человека осознают свою взаимную привлекательность и начинают строить романтические отношения. В этот период возникают новые эмоции и ощущения.";

                

            
                    break;
                case "Поступление":

                    descriptiontextbox.Text =
                        "Поступление - это принятие в учебное заведение или образовательную программу. В этот момент начинается новая глава в учебной карьере и открываются новые возможности для личностного и профессионального развития.";
               

                    break;
                case "Первый рабочий день":

                    descriptiontextbox.Text =
                        "Первый рабочий день - это начало работы в новой организации или на новой должности. В этот момент человек вступает в новый коллектив и начинает свой профессиональный путь.";
                  
                    break;
                case "Увольнение":

                    descriptiontextbox.Text =
                        "Увольнение - это окончание работы или трудового сотрудничества с организацией. В этот момент возникают изменения в карьере и возможность искать новые вызовы и возможности.";
                    break;
                case "Первый день в школе":

                    descriptiontextbox.Text =
                        "Первый день в школе - это особый день для детей, когда они начинают свое образовательное путешествие. В этот день они встречают новых учителей и одноклассников, начинают учиться и осваивать новые знания.";
                  
                    break;
                case "Поминки":
                    descriptiontextbox.Text = 
                       "Поминки — это церемония или обряд, проводимые в память о усопших. Они служат временем для воспоминаний, почтения и скорби, где близкие люди собираются вместе, чтобы отдать дань уважения ушедшим и поддержать друг друга в процессе скорби и горя.";

        break;
                case "Gender party":

                    descriptiontextbox.Text = "Гендер пати — это особое событие, организуемое с целью подчеркнуть и отметить различия и многообразие гендерных идентификаций. Оно предоставляет возможность участникам выразить свою индивидуальность, осознание и принятие своего гендера.";
                    break;
                case "":
                    roleTextBox.Items.Clear();

                    descriptiontextbox.Text = "";
                    break;
               
            }
            
            if (!string.IsNullOrEmpty(type))
            {
                participantsComboBox.IsEnabled = true;
                
                participantsComboBox.Opacity = 1;

            }
            else
            {

                roleTextBox.IsEnabled = false;
                roleTextBox.SelectedItem = null;
                participantsComboBox.SelectedItem = null;
                participantsComboBox.IsEnabled = false;
                buttonDeleteParticipant.IsEnabled = false;
                buttonAddParticipant.IsEnabled = false;
                roleTextBox.Opacity = 0.5;
                participantsComboBox.Opacity = 0.5;
                buttonAddParticipant.Opacity = 0.5;
                buttonDeleteParticipant.Opacity = 0.5;
                textBox.Opacity = 0.5;

            }

        }

        

        private void ParticipantSelectionChecking(object sender, EventArgs e)
        {
            bool isComboBoxEmpty = string.IsNullOrEmpty(participantsComboBox.Text);
            roleTextBox.IsReadOnly = isComboBoxEmpty;
            roleTextBox.Cursor = isComboBoxEmpty ? Cursors.No : Cursors.IBeam;
            buttonAddParticipant.Cursor = isComboBoxEmpty ? Cursors.No : Cursors.Hand;
            buttonDeleteParticipant.Cursor = isComboBoxEmpty ? Cursors.No : Cursors.Hand;


            if (!isComboBoxEmpty)
            {
                UpdateRoles();
                roleTextBox.IsEnabled = true;
                buttonDeleteParticipant.IsEnabled = true;
                buttonAddParticipant.IsEnabled = true;
                roleTextBox.Opacity = 1;
                buttonAddParticipant.Opacity = 1;
                buttonDeleteParticipant.Opacity = 1;
                
            }
            else
            {
                roleTextBox.IsEnabled = false;
                buttonDeleteParticipant.IsEnabled = false;
                buttonAddParticipant.IsEnabled = false;
                roleTextBox.Opacity = 0.5;
                buttonAddParticipant.Opacity = 0.5;
                buttonDeleteParticipant.Opacity = 0.5;
            }
        }

        private void participantsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPerson = participantsComboBox.SelectedItem as People;
            

        }
        private void UpdateRoles()
        {

     
            try
            {
                roleTextBox.Items.Clear();

                switch (typeComboBox.Text)
                {

                    case "Рождение":
                        roleTextBox.Items.Add(new ComboBoxItem { Content = "Свидетель" });

                        break;
                    case "Смерть":
                        roleTextBox.Items.Add(new ComboBoxItem { Content = "Свидетель" });

                        break;

                    case "Свадьба":


                        if (selectedPerson.Gender == "Женский пол")
                        {
                            roleTextBox.Items.Add(new ComboBoxItem { Content = "Невеста" });
                            roleTextBox.Items.Add(new ComboBoxItem { Content = "Подружка невесты" });

                        }
                        else if (selectedPerson.Gender == "Мужской пол")
                        {
                            roleTextBox.Items.Add("Жених");
                            roleTextBox.Items.Add("Друг жениха");

                        }

                        roleTextBox.Items.Add("Гость");

                        break;
                    case "Брак":


                        if (selectedPerson.Gender == "Женский пол")
                        {
                            roleTextBox.Items.Add("Жена");
                        }
                        else if (selectedPerson.Gender == "Мужской пол")
                        {
                            roleTextBox.Items.Add("Муж");
                        }

                        roleTextBox.Items.Add("Свидетель");
                        roleTextBox.Items.Add("Гость");
                        break;
                    case "Развод":


                        if (selectedPerson.Gender == "Женский пол")
                        {
                            roleTextBox.Items.Add("Бывшая жена");
                        }
                        else if (selectedPerson.Gender == "Мужской пол")

                        {
                            roleTextBox.Items.Add("Бывший муж");
                        }

                        roleTextBox.Items.Add("Свидетель");
                        roleTextBox.Items.Add("Гость");
                        break;
                    case "Путешествие":
                        roleTextBox.Items.Add("Организатор");
                        roleTextBox.Items.Add("Путешественник");
                        break;
                    case "Переезд":


                        roleTextBox.Items.Add("Переезжающий");
                        roleTextBox.Items.Add("Гость");
                        roleTextBox.Items.Add("Свидетель");



                        break;
                    case "Расставание":


                        if (selectedPerson.Gender == "Женский пол")
                        {
                            roleTextBox.Items.Add("Бывшая возлюбленная");
                        }
                        else if (selectedPerson.Gender == "Мужской пол")

                        {
                            roleTextBox.Items.Add("Бывший возлюбленный");
                        }

                        roleTextBox.Items.Add("Свидетель");

                        break;
                    case "Выпускной":

                        roleTextBox.Items.Add("Выпускник");
                        roleTextBox.Items.Add("Гость");

                        break;
                    case "Повышение":

                        roleTextBox.Items.Add("Сотрудник");
                        roleTextBox.Items.Add("Свидетель");

                        roleTextBox.Items.Add("Гость");


                        break;
                    case "Свидание":



                        if (selectedPerson.Gender == "Женский пол")
                        {
                            roleTextBox.Items.Add("Девушка");
                        }
                        else if (selectedPerson.Gender == "Мужской пол")

                        {
                            roleTextBox.Items.Add("Парень");
                        }

                        roleTextBox.Items.Add("Гость");

                        roleTextBox.Items.Add("Свидетель");


                        break;
                    case "Начало отношений":


                        if (selectedPerson.Gender == "Женский пол")
                        {
                            roleTextBox.Items.Add("Девушка");
                        }
                        else if (selectedPerson.Gender == "Мужской пол")

                        {
                            roleTextBox.Items.Add("Парень");
                        }
                        roleTextBox.Items.Add("Гость");

                        roleTextBox.Items.Add("Свидетель");





                        break;
                    case "Поступление":
                        roleTextBox.Items.Add("Поступающий");
                        roleTextBox.Items.Add("Свидетель");
                        roleTextBox.Items.Add("Гость");

                        break;
                    case "Первый рабочий день":
                        roleTextBox.Items.Add("Новый сотрудник");
                        roleTextBox.Items.Add("Коллега");
                        roleTextBox.Items.Add("Свидетель");
                        break;
                    case "Увольнение":
                        roleTextBox.Items.Add("Увольняемый");
                        roleTextBox.Items.Add("Коллега");
                        roleTextBox.Items.Add("Свидетель"); break;
                    case "Первый день в школе":
                        roleTextBox.Items.Add("Новоиспеченный школьник");
                        roleTextBox.Items.Add("Гость");
                        break;
                    case "Поминки":
                        roleTextBox.Items.Add("Усопший");

                        roleTextBox.Items.Add("Гость");

                        break;
                    case "Gender party":
                        roleTextBox.Items.Add("Организатор");

                        roleTextBox.Items.Add("Гость");

                        break;
                    case "":

                        break;

                }
            }
            catch
            {
            }
        }

    

     }
}
