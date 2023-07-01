using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Data.OleDb;
using System.Linq;
using System.Drawing;
using System.Windows.Resources;
using System.IO;
using System.Xml.Linq;

namespace Family_Tree
{
    public static class ContextMenuLeftClickBehavior
    {
        public static bool GetIsLeftClickEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLeftClickEnabledProperty);
        }

        public static void SetIsLeftClickEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLeftClickEnabledProperty, value);
        }

        public static readonly DependencyProperty IsLeftClickEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsLeftClickEnabled", typeof(bool),
                typeof(ContextMenuLeftClickBehavior),
                new UIPropertyMetadata(false, OnIsLeftClickEnabledChanged));

        private static void OnIsLeftClickEnabledChanged(
            object sender, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = sender as UIElement;

            if (uiElement != null)
            {
                bool IsEnabled = e.NewValue is bool && (bool)e.NewValue;

                if (IsEnabled)
                {
                    if (uiElement is ButtonBase)
                        ((ButtonBase)uiElement).Click += OnMouseLeftButtonUp;
                    else
                        uiElement.MouseLeftButtonUp += OnMouseLeftButtonUp;
                }
                else
                {
                    if (uiElement is ButtonBase)
                        ((ButtonBase)uiElement).Click -= OnMouseLeftButtonUp;
                    else
                        uiElement.MouseLeftButtonUp -= OnMouseLeftButtonUp;
                }
            }
        }

        private static void OnMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            Debug.Print("OnMouseLeftButtonUp");
            var fe = sender as FrameworkElement;
            if (fe != null)
            {
                // if we use binding in our context menu, then it's DataContext won't be
                // set when we show the menu on left click (it seems setting DataContext
                // for ContextMenu is hardcoded in WPF when user right clicks on a
                // control, although I'm not sure) so we have to set up
                // ContextMenu.DataContext manually here
                if (fe.ContextMenu.DataContext == null)
                {
                    fe.ContextMenu.SetBinding(FrameworkElement.DataContextProperty,
                                              new Binding { Source = fe.DataContext });
                }

                fe.ContextMenu.IsOpen = true;
            }
        }
    }

    public partial class AddEditPerson : Window
    {
        private List<People> people = new List<People>();
        private string imageFilePath;
        private readonly string databaseFilePath;
        private People selectedMother = new People();
        private People selectedFather = new People();
        private Places birthPlace = new Places();
        private Places deathPlace = new Places();
        private Places residencePlace = new Places();
        private bool isInitialized = false;
        private readonly string AddOrEdit = "";
        private readonly int editPersonId;
        private readonly string folder;
        private readonly List<People>  manList = new List<People>();
        private readonly List<People> womanList = new List<People>();
        public AddEditPerson(string filePath, string AddOrEdit, int editPersonId)
        {
            InitializeComponent();


            folder = System.IO.Path.GetDirectoryName(filePath);

            databaseFilePath = filePath;
            this.AddOrEdit = AddOrEdit;

            if (AddOrEdit == "Edit")
            {
                this.editPersonId = editPersonId;
                Title = "Family Tree: Изменение персоны";
            }

            List<Places> placeList = new List<Places>();

            people = People.GetPeople(databaseFilePath);

            string connect =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFilePath};Jet OLEDB:Engine Type=5";
            using (OleDbConnection dbConnection = new OleDbConnection(connect))
            {
                dbConnection.Open();

                // Выполнение SQL-запроса на выборку данных

                People emptyperson = new People();

                manList.Add(emptyperson);
                womanList.Add(emptyperson);

                foreach (People person in people)
                {
                    if (person.PersonId != editPersonId)
                    {
                        if (person.Gender == "Женский пол")
                        {
                            womanList.Add(person);

                        }
                        else
                        {
                            manList.Add(person);
                        }
                    }
                }

                if (manList.Count != 1)
                {
                    fatherComboBox.ItemsSource = null;

                    fatherComboBox.ItemsSource = manList;

                }
                else
                {
                    fatherComboBox.IsEnabled = false;
                    fatherComboBox.Opacity = 0.5;
                }
                if (womanList.Count != 1)
                {
                    motherComboBox.ItemsSource = null;
                    motherComboBox.ItemsSource = womanList;

                }
                else
                {
                    motherComboBox.IsEnabled = false;
                    motherComboBox.Opacity = 0.5;
                }
                if (womanList.Count == 1 && manList.Count == 1)
                {
                    parentsCheckBox.Opacity = 0.5;
                    parentsCheckBox.IsEnabled = false;
                }

                string sqlQuery = "SELECT PlaceId, Name, Abbreviation FROM Places";
                using (OleDbCommand command =
                           new OleDbCommand(sqlQuery, dbConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        Places emptyplace = new Places();
                        placeList.Add(emptyplace);

                        // Чтение результатов запроса и добавление их в список
                        while (reader.Read())
                        {
                            int placeid = reader.GetInt32(0);
                            string abbr = string.Empty;
                            string name = string.Empty;
                            if (!reader.IsDBNull(1))
                            {
                                name = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                abbr = reader.GetString(2);
                            }
                            Places place = new Places
                            {
                                PlaceId = placeid,
                                Name = name,
                                Abbreviation = abbr
                            };

                            placeList.Add(place);
                        }

                        // Используйте peopleList как источник данных для вашего ListBox

                        if (placeList.Count != 1)
                        {
                            placeOfBirthComboBox.ItemsSource = null;
                            placeOfBirthComboBox.ItemsSource = placeList;
                            comboBoxPlaceDeath.ItemsSource = null;
                            comboBoxPlaceDeath.ItemsSource = placeList;
                            residenceComboBox.ItemsSource = null;
                            residenceComboBox.ItemsSource = placeList;

                        }
                        else
                        {
                            comboBoxPlaceDeath.IsEnabled = false;
                            comboBoxPlaceDeath.Opacity = 0.5;
                            residenceComboBox.IsEnabled = false;
                            residenceComboBox.Opacity = 0.5;
                            placeOfBirthComboBox.IsEnabled = false;
                            placeOfBirthComboBox.Opacity = 0.5;
                        }
                    }
                }
                dbConnection.Close();

                if (AddOrEdit == "Edit")
                {
                    People person = people.Find(p => p.PersonId == editPersonId);

                    genderComboBox.Text = person.Gender;
                    firstNameTextBox.Text = person.FirstName;
                    lastNameTextBox.Text = person.LastName;
                    middleNameTextBox.Text = person.MiddleName;
                    maidenNameTextBox.Text = person.MaidenName;
                    occupationTextBox.Text = person.Occupation;
                    residenceComboBox.SelectedItem =
                        placeList.Find(p => p.PlaceId == person.ResidenceId);
                    religionTextBox.Text = person.Religion;
                    DateTime emptyDate = new DateTime(1, 1, 1);
                    if (person.BirthDate != emptyDate)
                    {
                        dateOfBirthPicker.SelectedDate = person.BirthDate;
                    }
                    placeOfBirthComboBox.SelectedItem =
                        placeList.Find(p => p.PlaceId == person.BirthPlaceId);
                    notesTextBox.Text = person.Notes;
                    aliveCheckBox.IsChecked = person.IsAlive;
                    if (person.DeathDate != emptyDate)
                    {
                        dateOfDeathPicker.SelectedDate = person.DeathDate;
                    }
                    causedeathTextBox.Text = person.CauseOfDeath;
                    comboBoxPlaceDeath.SelectedItem =
                        placeList.Find(p => p.PlaceId == person.DeathPlaceId);

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

                        personPhoto.Source = photo;
                        imageFilePath = person.Photo;
                    }
                    catch
                    {
                    }


                    fatherComboBox.SelectedItem =
                        manList.Find(p => p.PersonId == person.FatherId);
                    motherComboBox.SelectedItem =
                        womanList.Find(p => p.PersonId == person.MotherId);

                    if(person.MotherId!=0 || person.FatherId != 0)
                    {
                        parentsCheckBox.IsChecked = true;
                    }
                }
            }
        }

        private void LoadPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter =
                  "Файлы изображений (*.jpg, *.png)|*.jpg;*.png|Все файлы (*.*)|*.*"
            };

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    imageFilePath = openFileDialog.FileName;

                    // Загрузка выбранного файла в Source картинки
                    

                    BitmapImage photo = new BitmapImage();

                    var stream = System.IO.File.OpenRead(imageFilePath);

                    photo.BeginInit();
                    photo.CacheOption = BitmapCacheOption.OnLoad;
                    photo.StreamSource = stream;
                    photo.EndInit();
                    stream.Close();
                    stream.Dispose();

                    personPhoto.Source = photo;

                }
            }
            catch
            {
                Warning.WarningShow(
                    "Возникла непредвиденная ошибка загрузки фотографии. Пожалуйста, попробуйте ещё раз!",
                    "Ошибка загрузки файла");
            }
        }

        private void DeletePhoto(object sender, RoutedEventArgs e)
        {
            imageFilePath = "";

            GenderChangePhoto();
        }

        private void ChangeGender(object sender, SelectionChangedEventArgs e)
        {
            if (isInitialized)
            {
                GenderChangePhoto();
            }
            else
            {
                isInitialized = true;
            }
        }

        private void GenderChangePhoto()
        {
            ComboBoxItem typeItem = (ComboBoxItem)genderComboBox.SelectedItem;
            string selectedGender = typeItem.Content.ToString();

            if (string.IsNullOrWhiteSpace(imageFilePath))
            {
                // Установка соответствующей базовой картинки в зависимости от
                // выбранного пола
                if (selectedGender == "Женский пол")
                {
                    BitmapImage bitmap = new BitmapImage();
                    
                    Uri uri = new Uri("pack://application:,,,/Assets/Images/woman.png");
                    StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);
                    Stream stream = resourceInfo.Stream;

                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    stream.Close();
                    stream.Dispose();

                    personPhoto.Source = bitmap;


                }
                else if (selectedGender == "Мужской пол")
                {
                    BitmapImage bitmap = new BitmapImage();

                    Uri uri = new Uri("pack://application:,,,/Assets/Images/man.png");
                    StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);
                    Stream stream = resourceInfo.Stream;

                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    stream.Close();
                    stream.Dispose();

                    personPhoto.Source = bitmap;
                }
                else if (selectedGender == "Неизвестно")
                {
                    BitmapImage bitmap = new BitmapImage();

                    Uri uri = new Uri("pack://application:,,,/Assets/Images/unknown.png");
                    StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);
                    Stream stream = resourceInfo.Stream;

                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    stream.Close();
                    stream.Dispose();

                    personPhoto.Source = bitmap;
                }
            }
        }
        private void AddPerson(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(firstNameTextBox.Text))
                {
                    if (AddOrEdit == "Add")
                    {
                        Warning.WarningShow(
                            "Новая персона должна иметь по крайней мере имя для узнавания! Пожалуйста, добавьте его.",
                            "Обратите внимание");
                    }
                    else
                    {
                        Warning.WarningShow(
                            "Оставьте изменяемой персоне по крайней мере имя для узнавания.",
                            "Обратите внимание");
                    }

                    return;
                }

                string query = "";

                string mindate = new DateTime(100, 1, 1).ToString();

                string gender = string.IsNullOrEmpty(genderComboBox.Text)
                                    ? ""
                                    : genderComboBox.Text;

                string firstname = string.IsNullOrEmpty(firstNameTextBox.Text)
                                       ? ""
                                       : firstNameTextBox.Text;
                firstname = firstname.Replace("'", "’");

                string lastname = string.IsNullOrEmpty(lastNameTextBox.Text)
                                      ? ""
                                      : lastNameTextBox.Text;

                lastname = lastname.Replace("'", "’");

                string middlename = string.IsNullOrEmpty(middleNameTextBox.Text)
                                        ? ""
                                        : middleNameTextBox.Text;

                middlename = middlename.Replace("'", "’");

                string maidenname = string.IsNullOrEmpty(maidenNameTextBox.Text)
                                        ? ""
                                        : maidenNameTextBox.Text;

                maidenname = maidenname.Replace("'", "’");

                string occupation = string.IsNullOrEmpty(occupationTextBox.Text)
                                        ? ""
                                        : occupationTextBox.Text;
                occupation = occupation.Replace("'", "’");

                string residenceID =
                    string.IsNullOrEmpty(residencePlace.PlaceId.ToString())
                        ? ""
                        : residencePlace.PlaceId.ToString();
                string religion = string.IsNullOrEmpty(religionTextBox.Text)
                                      ? ""
                                      : religionTextBox.Text;
                religion = religion.Replace("'", "’");

                string birthdate = dateOfBirthPicker.SelectedDate.HasValue
                                       ? dateOfBirthPicker.SelectedDate.Value.ToString()
                                       : string.Empty;
                string birthID = string.IsNullOrEmpty(birthPlace.PlaceId.ToString())
                                     ? ""
                                     : birthPlace.PlaceId.ToString();

                string notes =
                    string.IsNullOrEmpty(notesTextBox.Text) ? "" : notesTextBox.Text;
                notes = notes.Replace("'", "’");

                string isAlive = (bool)aliveCheckBox.IsChecked ? "1" : "0";
                string deathdate = dateOfDeathPicker.SelectedDate.HasValue
                                       ? dateOfDeathPicker.SelectedDate.Value.ToString()
                                       : string.Empty;
                string deathcause = string.IsNullOrEmpty(causedeathTextBox.Text)
                                        ? ""
                                        : causedeathTextBox.Text;
                deathcause = deathcause.Replace("'", "’");

                string deathID = string.IsNullOrEmpty(deathPlace.PlaceId.ToString())
                                     ? ""
                                     : deathPlace.PlaceId.ToString();

                string fatherID =
                    string.IsNullOrEmpty(selectedFather.PersonId.ToString())
                        ? ""
                        : selectedFather.PersonId.ToString();
                string motherID =
                    string.IsNullOrEmpty(selectedMother.PersonId.ToString())
                        ? ""
                        : selectedMother.PersonId.ToString();

                string photo = imageFilePath;

                string connect =
                    $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFilePath};Jet OLEDB:Engine Type=5";
                OleDbConnection dbconnection = new OleDbConnection(connect);
                dbconnection.Open();


                People person = new People();
                foreach (var pers in people)
                {
                    if (pers.PersonId == editPersonId)
                    {
                        person = pers;
                    }
                }


                if ((AddOrEdit == "Edit" && (person.Photo != imageFilePath)) || AddOrEdit == "Add")
                {

                    string sourceFilePath = imageFilePath;
                    string destinationFilePath = folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\People\\" + System.IO.Path.GetFileName(sourceFilePath);
                    ;


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
                photo = System.IO.Path.GetFileName(destinationFilePath);
            }
              
               

                if (AddOrEdit == "Add")
                {
                    string maxIdQuery = "SELECT MAX(PersonId) FROM People";
                    int lastPersonId = 0;

                
                    OleDbCommand dbCommand0 = new OleDbCommand(maxIdQuery, dbconnection);
                    object result = dbCommand0.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        lastPersonId = Convert.ToInt32(result);
                    }

                    lastPersonId++;
                    string person_id = lastPersonId.ToString();

                    birthdate =
                        (birthdate == string.Empty ? "NULL" : "'" + birthdate + "'");
                    deathdate =
                        (deathdate == string.Empty ? "NULL" : "'" + deathdate + "'");
                    query =
                        $"INSERT INTO People VALUES({person_id},'{gender}','{firstname}','{lastname}','{middlename}','{maidenname}','{occupation}',{residenceID},'{religion}',{birthdate},{birthID},'{notes}',{isAlive},{deathdate},{deathID},'{deathcause}',{fatherID},{motherID},'{photo}')";  //запрос

                    OleDbCommand dbCommand = new OleDbCommand(query, dbconnection);

                    dbCommand.ExecuteNonQuery();

                    // Новое событие "Рождение"
                    maxIdQuery = "SELECT MAX(EventId) FROM Events";
                    dbCommand0 = new OleDbCommand(maxIdQuery, dbconnection);
                    result = dbCommand0.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        lastPersonId = Convert.ToInt32(result);
                    }

                    lastPersonId++;

                    string howToName = (gender == "Женский пол") ? "родилась" : "родился";
                    string eventid = lastPersonId.ToString();
                    string eventtype = "Рождение";
                    string eventdate = birthdate;

                    string eventlocation = birthID;
                    string description =
                        $"Рождение - это особенное и долгожданное событие, когда новая жизнь появляется в мире. В этот момент {firstname} {howToName}, наполнив радостью сердца своих родителей и близких. Этот неповторимый момент приветствует нового члена семьи и запечатлевает начало удивительного жизненного пути.";
                    string comment = "";
                    query =
                        $"INSERT INTO Events VALUES({eventid},'{eventtype}',{eventdate},'{eventlocation}','{description}','{comment}','')";  //запрос
                    dbCommand = new OleDbCommand(query, dbconnection);
                    dbCommand.ExecuteNonQuery();
                    query =
                        $"INSERT INTO Participants VALUES({eventid},{person_id},'Новорождённый')";  //запрос
                    dbCommand = new OleDbCommand(query, dbconnection);
                    dbCommand.ExecuteNonQuery();

                    // Новое событие "Смерть"
                    howToName = (gender == "Женский пол") ? "ушла" : "ушёл";
                    if (isAlive == "0")
                    {
                        eventid = (int.Parse(eventid) + 1).ToString();
                        eventtype = "Смерть";
                        eventdate = deathdate;
                        eventlocation = deathID;
                        description =
                            $"Смерть - это глубоко печальное и неизбежное событие, когда жизнь человека прекращается. В этот момент {firstname} {howToName} из этого мира, оставив тоску и горечь в сердцах своих близких. Этот непростой момент открывает новую главу, напоминая нам о хрупкости и ценности человеческой жизни.";
                        query =
                            $"INSERT INTO Events VALUES({eventid},'{eventtype}',{eventdate},'{eventlocation}','{description}','{comment}','')";  //запрос
                        dbCommand = new OleDbCommand(query, dbconnection);
                        dbCommand.ExecuteNonQuery();
                        query =
                            $"INSERT INTO Participants VALUES({eventid},{person_id},'Умерший')";  //запрос
                        dbCommand = new OleDbCommand(query, dbconnection);
                        dbCommand.ExecuteNonQuery();

                        
                    }

                    dbconnection.Close();  // Закрытие соединения после использования

                   
                }

                else
                {
                  

                    birthdate =
                        (birthdate == string.Empty ? "NULL" : "'" + birthdate + "'");
                    deathdate =
                        (deathdate == string.Empty ? "NULL" : "'" + deathdate + "'");
                    
                    
                    
                    query =
                        $"UPDATE People SET Gender ='{gender}', FirstName = '{firstname}', LastName = '{lastname}', MiddleName = '{middlename}', MaidenName = '{maidenname}', Occupation = '{occupation}', ResidenceId = {residenceID}, Religion = '{religion}', BirthDate = {birthdate}, BirthPlaceId = {birthID}, Notes  ='{notes}', IsAlive ={isAlive},DeathDate = {deathdate},DeathPlaceId = {deathID}, CauseOfDeath ='{deathcause}',FatherId={fatherID},MotherId={motherID},Photo='{photo}' WHERE PersonId = {editPersonId}";
                    OleDbCommand dbCommand0 = new OleDbCommand(query, dbconnection);
                    dbCommand0.ExecuteNonQuery();

                    string howToName = (gender == "Женский пол") ? "родилась" : "родился";
                    string eventtype = "Рождение";
                    string eventdate = birthdate;
                    string eventlocation = birthID;
                    string description = $"Рождение - это особенное и долгожданное событие, когда новая жизнь появляется в мире. В этот момент {firstname} {howToName}, наполнив радостью сердца своих родителей и близких. Этот неповторимый момент приветствует нового члена семьи и запечатлевает начало удивительного жизненного пути.";
                    string comment = "";
                    query = $"SELECT EventId FROM Participants WHERE PersonId = {editPersonId} AND Role ='Новорождённый'";
                    dbCommand0 = new OleDbCommand(query, dbconnection);
                    object eventId = dbCommand0.ExecuteScalar();

                    query =
                        $"UPDATE Events SET EventDate = {eventdate}, EventLocationId = {eventlocation}, Description = '{description}' WHERE EventId = {eventId}";  //запрос
                    dbCommand0 = new OleDbCommand(query, dbconnection);
                    dbCommand0.ExecuteNonQuery();


                    howToName = (gender == "Женский пол") ? "ушла" : "ушёл";
                    eventtype = "Смерть";
                    eventdate = deathdate;
                    eventlocation = deathID;
                    description =
                        $"Смерть - это глубоко печальное и неизбежное событие, когда жизнь человека прекращается. В этот момент {firstname} {howToName} из этого мира, оставив тоску и горечь в сердцах своих близких. Этот непростой момент открывает новую главу, напоминая нам о хрупкости и ценности человеческой жизни.";

                    if (!person.IsAlive && !(bool)aliveCheckBox.IsChecked)
                    {
                       
                        query =
                            $"SELECT EventId FROM Participants WHERE PersonId = {editPersonId} AND Role ='Умерший'";

                        dbCommand0 = new OleDbCommand(query, dbconnection);
                        object result = dbCommand0.ExecuteScalar();

                        eventId = Convert.ToInt32(result);

                        query =
                            $"UPDATE Events SET EventDate = {eventdate}, EventLocationId = {eventlocation},Description = '{description}' WHERE EventId = {eventId}";  //запрос
                        dbCommand0 = new OleDbCommand(query, dbconnection);
                        dbCommand0.ExecuteNonQuery();
                    }
                    if (!person.IsAlive &&  (bool)aliveCheckBox.IsChecked)
                    {
                        query =
                            $"SELECT EventId FROM Participants WHERE PersonId = {editPersonId} AND Role ='Умерший'";
                        dbCommand0 = new OleDbCommand(query, dbconnection);
                        object result = dbCommand0.ExecuteScalar();
                        eventId = Convert.ToInt32(result);
                        query = $"DELETE FROM Events WHERE EventId = {eventId}";  //запрос
                        dbCommand0 = new OleDbCommand(query, dbconnection);
                        dbCommand0.ExecuteNonQuery();
                        query =
                            $"DELETE FROM Participants WHERE EventId = {eventId}";  //запрос
                        dbCommand0 = new OleDbCommand(query, dbconnection);
                        dbCommand0.ExecuteNonQuery();
                    }
                    if (person.IsAlive && !(bool)aliveCheckBox.IsChecked)
                    {
                        query = "SELECT MAX(EventId) FROM Events";

                        dbCommand0 = new OleDbCommand(query, dbconnection);
                        eventId = dbCommand0.ExecuteScalar();
                        int newDeathEventId = Convert.ToInt32(eventId);

                        newDeathEventId++;

                        query =
                            $"INSERT INTO Events VALUES({newDeathEventId},'{eventtype}',{eventdate},'{eventlocation}','{description}','{comment}','')";  //запрос
                        dbCommand0 = new OleDbCommand(query, dbconnection);
                        dbCommand0.ExecuteNonQuery();
                        query =
                            $"INSERT INTO Participants VALUES({newDeathEventId},{editPersonId},'Умерший')";  //запрос
                        dbCommand0 = new OleDbCommand(query, dbconnection);
                        dbCommand0.ExecuteNonQuery();
                    }

                    if (gender != person.Gender)
                    {
                        List<string> items = new List<string>() { "Невеста",
                                                      "Жена",
                                                      "Бывшая жена",
                                                      "Возлюбленная",
                                                      "Бывшая возлюбленная",
                                                      "Девушка" };
                        if (person.Gender == "Мужской пол")
                        {
                            items = new List<string>() { "Жених",
                                           "Муж",
                                           "Бывший муж",
                                           "Возлюбленный",
                                           "Бывший возлюбленный",
                                           "Парень" };
                        }
                        string joinedString =
                            string.Join(" OR Role = ", items.Select(item => $"'{item}'"));

                        string finalString = $"Role = {joinedString}";

                        string WhoAmI =
                            (person.Gender == "Женский пол") ? "MotherId" : "FatherId";


                        OleDbCommand dbCommand;

                    
                        if (WhoAmI == "MotherId")
                        {
                            query =
                                $"UPDATE People SET MotherId = 0 WHERE {WhoAmI}={person.PersonId}";
                            dbCommand = new OleDbCommand(query, dbconnection);
                            dbCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            query =
                                $"UPDATE People SET FatherId = 0 WHERE {WhoAmI}={person.PersonId}";
                            dbCommand = new OleDbCommand(query, dbconnection);
                            dbCommand.ExecuteNonQuery();
                        }


                        query = $"SELECT EventId FROM Events WHERE EventId IN (SELECT EventId FROM Participants WHERE PersonId = {person.PersonId} AND ({finalString}))";

                        List<int> genderEvents = new List<int>();
                        using (OleDbCommand command =
                                     new OleDbCommand(query, dbconnection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    {

                                        int id = reader.GetInt32(0);
                                        genderEvents.Add(id);

                                    }
                                }
                            }
                        }


                        query =
                            $"DELETE FROM Events WHERE EventId IN (SELECT EventId FROM Participants WHERE PersonId = {person.PersonId} AND ({finalString}))";
                        dbCommand = new OleDbCommand(query, dbconnection);
                        dbCommand.ExecuteNonQuery();

                        string result = string.Join(", ", genderEvents);

                        if (!string.IsNullOrEmpty(result))
                        {

                            query =
                                    $"DELETE FROM Participants WHERE EventId IN ({result})";
                            dbCommand = new OleDbCommand(query, dbconnection);
                            dbCommand.ExecuteNonQuery();


                            query =
                             $"DELETE FROM Participants WHERE EventId IN (SELECT EventId FROM Participants WHERE PersonId = {person.PersonId} AND (Role = 'Подружка невесты' OR Role ='Друг жениха')) AND PersonId<>{person.PersonId}";
                            dbCommand = new OleDbCommand(query, dbconnection);
                            dbCommand.ExecuteNonQuery();
                        }



                    }

                    dbconnection.Close();  // Закрытие соединения после использования


                    if ((AddOrEdit == "Edit" && (person.Photo != imageFilePath)) || AddOrEdit == "Add")
                    {
                        string deleteFile = folder + $"\\{Path.GetFileName(folder)}-Resources\\People\\" + person.Photo;
                        if (File.Exists(folder + $"\\{Path.GetFileName(folder)}-Resources\\People\\" + person.Photo))
                        {

                            try
                            {
                                File.Delete(deleteFile);
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                

                Close();
            }

            catch 
            {
            }
        }

        private void SelectFather(object sender, SelectionChangedEventArgs e)
        {
            selectedFather = fatherComboBox.SelectedItem as People;
        }
        private void SelectMother(object sender, SelectionChangedEventArgs e)
        {
            selectedMother = motherComboBox.SelectedItem as People;
        }
        private void SelectDeathplace(object sender, SelectionChangedEventArgs e)
        {
            deathPlace = comboBoxPlaceDeath.SelectedItem as Places;
        }

        private void SelectResidence(object sender, SelectionChangedEventArgs e)
        {
            residencePlace = residenceComboBox.SelectedItem as Places;
        }

        private void SelectBirthPlace(object sender, SelectionChangedEventArgs e)
        {
            birthPlace = placeOfBirthComboBox.SelectedItem as Places;
        }

        private void CloseForm(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
