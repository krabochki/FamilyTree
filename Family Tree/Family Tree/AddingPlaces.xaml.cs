using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Media;
using System.Collections;
using System.Drawing;
using System.Management.Instrumentation;
using System.Windows.Input;

namespace Family_Tree
{
    public partial class AddEditPlace : Window
    {
        private string imageFilePath;
        private string databaseFilePath;
        private readonly string AddOrEdit = "";
        private readonly string folder = "";
        private readonly int editPlaceId;
        private readonly List<Places> places = new List<Places>();

        public AddEditPlace(string filePath, string AddOrEdit, int editPlaceId)
        {
            InitializeComponent();
            folder = System.IO.Path.GetDirectoryName(filePath);
            this.AddOrEdit = AddOrEdit;

            if (AddOrEdit == "Edit")
            {
                this.editPlaceId = editPlaceId;
                Title = "Family Tree: Изменение места";
            }
            databaseFilePath = filePath;
            places = Places.GetPlaces(databaseFilePath);


            if (AddOrEdit == "Edit")
            {
                Places place = places.Find(p => p.PlaceId == editPlaceId);

                name_textbox.Text = place.Name;
                abbr_textbox.Text = place.Abbreviation;
                historicalname_textbox.Text = place.Historical_Name;
                typeTextBox.Text = place.Type;
                addressTextBox.Text = place.Address;
                descrTextBox.Text = place.Description;
                placeNotesTextBox.Text = place.Comment;
            

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

                    place_Photo.Source = photo;
                    imageFilePath = place.Picture;




                }
                catch
                {
                }


               
            }
        }

        private void LoadPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
                "Файлы изображений (*.jpg, *.png)|*.jpg;*.png|Все файлы (*.*)|*.*";

         

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    imageFilePath = openFileDialog.FileName;



                    BitmapImage photo = new BitmapImage();

                    var stream = System.IO.File.OpenRead(imageFilePath);

                    photo.BeginInit();
                    photo.CacheOption = BitmapCacheOption.OnLoad;
                    photo.StreamSource = stream;
                    photo.EndInit();
                    stream.Close();
                    stream.Dispose();

                    place_Photo.Source = photo;
                }
            }
            catch
            {
               
                Warning.WarningShow("Возникла непредвиденная ошибка загрузки фотографии. Пожалуйста, попробуйте ещё раз!", "Ошибка загрузки файла");
            }
        }

        private void DeletePhoto(object sender, RoutedEventArgs e)
        {
            imageFilePath = "Assets/Images/place.png";
        }


        private void AddPerson(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(name_textbox.Text))
                {
                    if (AddOrEdit == "Add")
                    {
                        Warning.WarningShow(
                            "Новое место должно иметь по крайней мере название для узнавания! Пожалуйста, добавьте его.",
                            "Обратите внимание");
                    }
                    else
                    {
                        Warning.WarningShow(
                            "Оставьте изменяемому месту по крайней мере название для узнавания.",
                            "Обратите внимание");
                    }

                    return;
                }
                else
                {

                    string name =
                        string.IsNullOrEmpty(name_textbox.Text) ? "" : name_textbox.Text;

                     name = name.Replace("'", "’");

                    string abbr =
                        string.IsNullOrEmpty(abbr_textbox.Text) ? "" : abbr_textbox.Text;
                    abbr = abbr.Replace("'", "’");

                    string hist_name = string.IsNullOrEmpty(historicalname_textbox.Text)
                                           ? ""
                                           : historicalname_textbox.Text;

                    hist_name = hist_name.Replace("'", "’");

                    string type =
                        string.IsNullOrEmpty(typeTextBox.Text) ? "" : typeTextBox.Text;
                    type  = type.Replace("'", "’");

                    string descr =
                        string.IsNullOrEmpty(descrTextBox.Text) ? "" : descrTextBox.Text; descr = descr.Replace("'", "’");

                    string address =
                        string.IsNullOrEmpty(addressTextBox.Text) ? "" : addressTextBox.Text;
                    address = address.Replace("'", "’");
                    string comment = string.IsNullOrEmpty(placeNotesTextBox.Text)
                                         ? ""
                                         : placeNotesTextBox.Text;
                    comment = comment.Replace("'", "’");


                    string image = imageFilePath;


                    Places place = places.Find(p => p.PlaceId == editPlaceId);


                   
                    string sourceFilePath = imageFilePath;
                        string destinationFilePath = folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Places\\" + System.IO.Path.GetFileName(sourceFilePath);
                        ;


                    if ((AddOrEdit == "Edit" && (place.Picture != imageFilePath)) || AddOrEdit == "Add")
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

                        string connect = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFilePath};Jet OLEDB:Engine Type=5";
                        OleDbConnection dbconnection = new OleDbConnection(connect);
                        dbconnection.Open();
                        if (AddOrEdit == "Add")
                        {
                            string maxIdQuery = "SELECT MAX(PlaceId) FROM Places";
                            int lastPlaceId = 0;
                            OleDbCommand dbCommand0 = new OleDbCommand(maxIdQuery, dbconnection);
                            object result = dbCommand0.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                lastPlaceId = Convert.ToInt32(result);
                            }
                            lastPlaceId++;
                            string id = lastPlaceId.ToString();
                            string query =
                                $"INSERT INTO Places VALUES({id},'{name}','{abbr}','{hist_name}','{type}','{descr}','{address}','{comment}','{image}')";
                            OleDbCommand dbCommand = new OleDbCommand(query, dbconnection);
                            dbCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            string query =
                               $"UPDATE Places SET Name ='{name}', Abbreviation = '{abbr}', Historical_name = '{hist_name}', Type = '{type}', Description = '{descr}', Address = '{address}', Comment = '{comment}', Picture = '{image}' WHERE PlaceId = {editPlaceId}";
                            OleDbCommand dbCommand0 = new OleDbCommand(query, dbconnection);
                            dbCommand0.ExecuteNonQuery();




                            place = places.Find(p => p.PlaceId == editPlaceId);

                        if ((AddOrEdit == "Edit" && (place.Picture != imageFilePath)) || AddOrEdit == "Add")
                        {

                            string deleteFile = folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Places\\" + place.Picture;
                                if (System.IO.File.Exists(folder + $"\\{System.IO.Path.GetFileName(folder)}-Resources\\Places\\" + place.Picture))
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
                        dbconnection.Close();  // Закрытие соединения после использования
                        Close();
                    }
                
            }
            catch
            {

            }
        }

   
        private void CloseForm(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
