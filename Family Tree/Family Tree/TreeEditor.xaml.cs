using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace Family_Tree
{
    public partial class TreeEditor : Window
    {

        private List<People> people = new List<People>();
        private List<Places> places = new List<Places>();
        private List<Events> events = new List<Events>();
        private string databaseFilePath;

        private void ResetDataGrid()
        {
            foreach (DataGridColumn column in myDataGrid.Columns)
            {
                column.Header = "";
                column.Visibility = Visibility.Visible;
            }
        }
        private void SwitchToPeople()
        {
            myDataGrid.ItemsSource =
                null;

            myDataGrid.ItemsSource =
                people;

            myDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[1].Header = "Полное имя";
            myDataGrid.Columns[2].Header = "Пол";
            myDataGrid.Columns[3].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[4].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[5].Visibility = Visibility.Collapsed;

            myDataGrid.Columns[6].Header = "Возраст";
            myDataGrid.Columns[7].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[8].Visibility = Visibility.Collapsed;

            myDataGrid.Columns[9].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[10].Header = "Деятельность";
            myDataGrid.Columns[11].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[12].Header = "Знак зодиака";

            myDataGrid.Columns[13].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[14].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[15].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[16].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[17].Header = "В живых";

            myDataGrid.Columns[18].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[19].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[20].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[21].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[22].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[23].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[24].Visibility = Visibility.Collapsed;

            sortButton1.Content = "Живые";
            sortButton2.Content = "Взрослые";
            sortButton3.Content = "Есть фото";
            sortButton1.Visibility = Visibility.Visible;
            sortButton2.Visibility = Visibility.Visible;
            sortButton3.Visibility = Visibility.Visible;

            rect.Visibility = Visibility.Visible;

        }
        private void SwitchToPlaces()
        {
            myDataGrid.ItemsSource =
                null;
            myDataGrid.ItemsSource =
                places;

            myDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[1].Header = "Название";
            myDataGrid.Columns[2].Header = "Сокращение";
            myDataGrid.Columns[3].Header = "Историческое название";
            myDataGrid.Columns[4].Header = "Тип";
            myDataGrid.Columns[5].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[6].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[7].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[8].Visibility = Visibility.Collapsed;

            sortButton1.Visibility = Visibility.Hidden;
            sortButton2.Visibility = Visibility.Hidden;
            sortButton3.Visibility = Visibility.Hidden;
            rect.Visibility = Visibility.Hidden;
        }
        private void SwitchToEvents()
        {
            myDataGrid.ItemsSource =
                null;

            myDataGrid.ItemsSource =
                events;

            myDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[1].Header = "Тип";
            myDataGrid.Columns[2].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[3].Header = "Дата";

            myDataGrid.Columns[4].Visibility = Visibility.Collapsed;

            myDataGrid.Columns[5].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[6].Visibility = Visibility.Collapsed;
            myDataGrid.Columns[7].Header = "Статус";

            myDataGrid.Columns[8].Header = "Дней до годовщины";
            myDataGrid.Columns[9].Visibility = Visibility.Collapsed;

            sortButton1.Visibility = Visibility.Hidden;
            sortButton2.Visibility = Visibility.Hidden;
            sortButton3.Visibility = Visibility.Hidden;
            rect.Visibility = Visibility.Hidden;

        }


        public TreeEditor(string filePath)
        {
            InitializeComponent();

            try
            {


                databaseFilePath = filePath;
                events = Events.GetEvents(databaseFilePath);
                places = Places.GetPlaces(databaseFilePath);
                people = People.GetPeople(databaseFilePath);


            }
            catch
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SwitchToPeople();
        }



        private void Exit(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void MenuSwitchPeople(object sender, RoutedEventArgs e)
        {
            ResetDataGrid();
            people = People.GetPeople(databaseFilePath);

            SwitchToPeople();

        }

        private void MenuSwitchPlaces(object sender, RoutedEventArgs e)
        {
            ResetDataGrid();
            places = Places.GetPlaces(databaseFilePath);

            SwitchToPlaces();


        }

        private void MenuSwitchEvents(object sender, RoutedEventArgs e)
        {
            ResetDataGrid();
            events = Events.GetEvents(databaseFilePath);

            SwitchToEvents();
        }

        private void Sort1(object sender, RoutedEventArgs e)
        {
            people = People.GetPeople(databaseFilePath);

            sortIndicator2 = 0;
            sortIndicator3 = 0;

            sortButton2.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
            sortButton3.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));

            if (sortIndicator1 == 0)
            {
                sortButton1.Tag = new BitmapImage(new Uri("Assets/Images/Icons/icon.png", UriKind.Relative));
                people.RemoveAll(p => !p.IsAlive);

                SwitchToPeople();
                sortIndicator1++;
            }
            else if (sortIndicator1 == 2)
            {
                sortButton1.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
                SwitchToPeople();
                sortIndicator1 = 0;
            }
            else if (sortIndicator1 == 1)
            {
                sortButton1.Tag = new BitmapImage(new Uri("Assets/Images/Icons/delete.png", UriKind.Relative));
                people.RemoveAll(p => p.IsAlive);
                SwitchToPeople();
                sortIndicator1++;
            }




        }

        int sortIndicator1 = 0;
        int sortIndicator2 = 0;
        int sortIndicator3 = 0;


        private void Sort2(object sender, RoutedEventArgs e)
        {
            sortIndicator1 = 0;
            sortIndicator3 = 0;
            sortButton3.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
            sortButton1.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));

            people = People.GetPeople(databaseFilePath);
            if (sortIndicator2 == 0)
            {
                sortButton2.Tag = new BitmapImage(new Uri("Assets/Images/Icons/icon.png", UriKind.Relative));
                people.RemoveAll(p => !p.IsAdult);

                SwitchToPeople();
                sortIndicator2++;
            }
            else if (sortIndicator2 == 2)
            {
                sortButton2.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
                SwitchToPeople();
                sortIndicator2 = 0;
            }
            else if (sortIndicator2 == 1)
            {
                sortButton2.Tag = new BitmapImage(new Uri("Assets/Images/Icons/delete.png", UriKind.Relative));
                people.RemoveAll(p => p.IsAdult);
                SwitchToPeople();
                sortIndicator2++;
            }




        }

        private void Sort3(object sender, RoutedEventArgs e)
        {
            sortIndicator1 = 0;
            sortIndicator2 = 0;
            sortButton1.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
            sortButton2.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));

            people = People.GetPeople(databaseFilePath);
            if (sortIndicator3 == 0)
            {
                sortButton3.Tag = new BitmapImage(new Uri("Assets/Images/Icons/icon.png", UriKind.Relative));
                people.RemoveAll(p => string.IsNullOrEmpty(p.Photo));

                SwitchToPeople();
                sortIndicator3++;
            }
            else if (sortIndicator3 == 2)
            {
                sortButton3.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
                SwitchToPeople();
                sortIndicator3 = 0;
            }
            else if (sortIndicator3 == 1)
            {
                sortButton3.Tag = new BitmapImage(new Uri("Assets/Images/Icons/delete.png", UriKind.Relative));
                people.RemoveAll(p => !string.IsNullOrEmpty(p.Photo));
                SwitchToPeople();
                sortIndicator3++;
            }




        }

        private void Add(object sender, RoutedEventArgs e)
        {

            try
            {
                if (myDataGrid.ItemsSource == people)
                {
                    AddEditPerson addingPerson = new AddEditPerson(databaseFilePath, "Add", 0);
                    addingPerson.ShowDialog();
                }
                else if (myDataGrid.ItemsSource == places)
                {
                    AddEditPlace addingPlace = new AddEditPlace(databaseFilePath, "Add", 0);
                    addingPlace.ShowDialog();
                }
                else if (myDataGrid.ItemsSource == events)
                {
                    AddEditEvent addingEvent = new AddEditEvent(databaseFilePath, "Add", 0);
                    addingEvent.ShowDialog();
                }
            }
            catch { }
        }


        private void Window_Activated(object sender, EventArgs e)
        {
            try

            {
                sortIndicator1 = 0;
                sortIndicator2 = 0;
                sortIndicator3 = 0;
                sortButton1.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
                sortButton2.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
                sortButton3.Tag = new BitmapImage(new Uri("Assets/Images/Icons/noicon.png", UriKind.Relative));
                if (myDataGrid.ItemsSource == people)
                {
                    people = People.GetPeople(databaseFilePath);

                    SwitchToPeople();

                }
                else if (myDataGrid.ItemsSource == places)
                {
                    places = Places.GetPlaces(databaseFilePath);
                    SwitchToPlaces();

                }

                else if (myDataGrid.ItemsSource == events)
                {
                    events = Events.GetEvents(databaseFilePath);
                    SwitchToEvents();
                }
            }
            catch
            {

            }
        }

        private void Click_Show(object sender, RoutedEventArgs e)
        {
            ShowSmth();
        }

        private void ShowSmth()
        {
            if (myDataGrid.SelectedItem != null)
            {
                if (myDataGrid.SelectedItems.Count == 1)
                {
                    try
                    {
                        if (myDataGrid.ItemsSource == people)
                        {
                            People selectedPerson = myDataGrid.SelectedItem as People;
                            People.ShowPerson(databaseFilePath, selectedPerson);

                        }
                        if (myDataGrid.ItemsSource == places)
                        {
                            Places selectedPlace = myDataGrid.SelectedItem as Places;
                            Places.ShowPlace(databaseFilePath, selectedPlace);
                        }
                        if (myDataGrid.ItemsSource == events)
                        {
                            Events selectedEvent = myDataGrid.SelectedItem as Events;
                            Events.ShowEvent(databaseFilePath, selectedEvent);
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    Warning.WarningShow("Выберите лишь одну запись для просмотра и попробуйте ещё раз", "Выбрано более одной записи");

                }
            }
            else
            {
                Warning.WarningShow("Выберите запись для просмотра и попробуйте ещё раз!", "Не выбрана ни одна запись");


            }
        }

        private void myDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowSmth();
            return;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {



            if (myDataGrid.SelectedItem != null)
            {
                if (!(myDataGrid.SelectedItems.Count > 1))
                {

                    try
                    {

                        Warning warning = new Warning();


                        warning.NObutton.Visibility = Visibility.Visible;
                        warning.OKbutton.Content = "Да";

                        warning.OKbutton.Click -= warning.Button_Click;

                        warning.OKbutton.Click += warning.YesButton_Click;
                        warning.NObutton.Click += warning.NoButton_Click;
                        warning.OKbutton.Width = 100;
                        warning.NObutton.Width = 100;
                        Grid.SetColumn(warning.NObutton, 1);
                        Grid.SetColumnSpan(warning.OKbutton, 1);




                        if (myDataGrid.ItemsSource == people)
                        {
                            People selectedItem = myDataGrid.SelectedItem as People;
                            warning.Title = "Подтвердите удаление персоны";
                            warning.warning_textblock.Text = "Вы уверены, что хотите удалить данную персону и все события, в которых она ключевое лицо?";
                            warning.ShowDialog();

                            if ((bool)warning.DialogResult)
                            {

                                People.DeletePerson(databaseFilePath, selectedItem);
                                people = People.GetPeople(databaseFilePath);



                                SwitchToPeople();
                            }
                            return;
                        }

                        if (myDataGrid.ItemsSource == places)
                        {
                            Places selectedItem = myDataGrid.SelectedItem as Places;
                            warning.Title = "Подтвердите удаление места";
                            warning.warning_textblock.Text = "Вы уверены, что хотите удалить данное место и ссылки на него во всех событиях и персонах?";
                            warning.ShowDialog();

                            if ((bool)warning.DialogResult)
                            {


                                Places.DeletePlace(databaseFilePath, selectedItem);
                                places = Places.GetPlaces(databaseFilePath);
                                SwitchToPlaces();
                            }
                            return;

                        }

                        if (myDataGrid.ItemsSource == events)
                        {
                            Events selectedItem = myDataGrid.SelectedItem as Events;


                            if (selectedItem.Type == "Рождение" ||
                                selectedItem.Type == "Смерть")
                            {
                                Warning.WarningShow("Вы не можете удалить событие рождения или смерти, так как эти события являются базовыми. Удалите персону, с которой они связаны.", "Внимание");
                                return;
                            }

                            warning.Title = "Подтвердите удаление события";
                            warning.warning_textblock.Text = "Вы уверены, что хотите удалить данное событие и все соответствующие связи?";
                            warning.ShowDialog();

                            if ((bool)warning.DialogResult)
                            {
                                Events.DeleteEvent(databaseFilePath, selectedItem);
                                events = Events.GetEvents(databaseFilePath);


                                SwitchToEvents();
                            }
                            return;

                        }


                    }
                    catch
                    {
                    }
                }
                else
                {
                    Warning.WarningShow("Выберите лишь одну запись для удаления и попробуйте ещё раз", "Выбрано более одной записи");

                }
            }
            else
            {
                Warning.WarningShow("Выберите запись для удаления и попробуйте ещё раз!", "Не выбрана ни одна запись");

            }

        }





        private void MenuCreateTree(object sender, RoutedEventArgs e)
        {


            if (people.Count <= 1)
            {
                Warning.WarningShow("Слишком мало персон для построения древа. Пожалуйста, добавьте ещё и попробуйте снова!", "Построение древа не имеет смысла");
            }
            else
            {
                Tree tree = new Tree(databaseFilePath);
                tree.ShowDialog();
                return;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {

            if (myDataGrid.SelectedItem != null)
            {
                if (myDataGrid.SelectedItems.Count == 1)
                {


                    try
                    {
                        if (myDataGrid.ItemsSource == people)
                        {
                            People selectedPerson = myDataGrid.SelectedItem as People;
                            AddEditPerson addingPerson = new AddEditPerson(databaseFilePath, "Edit", selectedPerson.PersonId);
                            addingPerson.ShowDialog();
                        }
                        else if (myDataGrid.ItemsSource == places)
                        {
                            Places selectedPlace = myDataGrid.SelectedItem as Places;
                            AddEditPlace addingPlace = new AddEditPlace(databaseFilePath, "Edit", selectedPlace.PlaceId);
                            addingPlace.ShowDialog();
                        }
                        else if (myDataGrid.ItemsSource == events)
                        {

                            Events selectedEvent = myDataGrid.SelectedItem as Events;
                            AddEditEvent addingEvent = new AddEditEvent(databaseFilePath, "Edit", selectedEvent.EventId);
                            addingEvent.ShowDialog();
                        }
                    }
                    catch { }

                }
                else
                {
                    Warning.WarningShow("Выберите лишь одну запись для изменения и попробуйте ещё раз", "Выбрано более одной записи");

                }
            }
            else
            {
                Warning.WarningShow("Выберите запись для изменения и попробуйте ещё раз!", "Не выбрана ни одна запись");


            }
        }

        private void MenuItem_OpenTree(object sender, RoutedEventArgs e)

        {
            bool res = MainWindow.chooseTree(ActualHeight, ActualWidth);
            if (res) Hide();





        }
        private void MenuItem_CreateTree(object sender, RoutedEventArgs e)

        {
          bool res=  MainWindow.createTree(ActualHeight, ActualWidth);
            if (res) Hide();


        }

     

        private void Romanovs(object sender, RoutedEventArgs e)

        {





            try
            {

                string gesturefile = Path.Combine(Environment.CurrentDirectory, @"Assets\Examples\Романовы\Романовы.accdb");


                if (File.Exists(gesturefile))
                {
                    TreeEditor treeEditor = new TreeEditor(gesturefile);
                    treeEditor.Width = ActualWidth;
                    treeEditor.Height = ActualHeight;
                    treeEditor.Show();
                    Hide();
                }
             
            }


            catch
            {
                Warning.WarningShow(
                    "Непредвиденная ошибка. Попробуйте ещё раз", "Ошибка");

                return;
            }


        }
        private void British(object sender, RoutedEventArgs e)

        {



            try
            {

                string gesturefile = Path.Combine(Environment.CurrentDirectory, @"Assets\Examples\British Royal Family\British Royal Family.accdb");


                if (File.Exists(gesturefile))
                {
                    TreeEditor treeEditor = new TreeEditor(gesturefile);
                    treeEditor.Width = ActualWidth;
                    treeEditor.Height = ActualHeight;
                    treeEditor.Show();
                    Hide();
                }

            }


            catch 
            {
                Warning.WarningShow(
                    "Непредвиденная ошибка. Попробуйте ещё раз", "Ошибка");

                return;
            }



        }


        private void MagnCentury(object sender, RoutedEventArgs e)

        {



            try
            {

                string gesturefile = Path.Combine(Environment.CurrentDirectory, @"Assets\Examples\Великолепный век\Великолепный век.accdb");


                if (File.Exists(gesturefile))
                {
                    TreeEditor treeEditor = new TreeEditor(gesturefile);
                    treeEditor.Width = ActualWidth;
                    treeEditor.Height = ActualHeight;
                    treeEditor.Show();
                    Hide();
                }

            }


            catch 
            {
                Warning.WarningShow(
                    "Непредвиденная ошибка. Попробуйте ещё раз", "Ошибка");

                return;
            }



        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            string githubUrl = "https://github.com/krabochki";

            Process.Start(githubUrl)?.Close();

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string gesturefile = Path.Combine(Environment.CurrentDirectory, @"Assets\Family Tree Help.chm");


            try
            {
                Process.Start(gesturefile)?.Close();
                
            }
            catch
            {
                // Обработка ошибки при открытии файла справки
            }
        }
    }
}


        
    


