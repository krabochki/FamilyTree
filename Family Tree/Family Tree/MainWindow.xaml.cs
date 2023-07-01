using System;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using System.IO;
using ADOX;
using System.Security;
using System.Data;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace Family_Tree
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public  void CreateNewFamilyTree(object sender, RoutedEventArgs e)
        {

           bool x = createTree(550,1100);
            if(x) Hide();
            
            
        }

        public static bool createTree(double height, double width)
        {

            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.Description = "Выберите или создайте папку для проекта семейного древа внутри неё. Назовите её так, как хотите, чтобы назывался файл с базой данных и ресурсами.";


            DialogResult res = FBD.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {






                try
                {

                    string dn = System.IO.Path.GetFileName(FBD.SelectedPath);

                    string selectedFilePath = FBD.SelectedPath + $"/{dn}.accdb";


                    Directory.CreateDirectory(FBD.SelectedPath + $"/{dn}-Resources");
                    Directory.CreateDirectory(FBD.SelectedPath + $"/{dn}-Resources/People");
                    Directory.CreateDirectory(FBD.SelectedPath + $"/{dn}-Resources/Places");
                    Directory.CreateDirectory(FBD.SelectedPath + $"/{dn}-Resources/Events");


                    // Проверка расширения файла
                    if (File.Exists(selectedFilePath))
                    {
                        Warning.WarningShow("В этой папке проект уже существует. Очистите папку или выберите новую",
                                             "Ошибка");
                        return false;
                    }


                    Catalog catalog = new Catalog();

                    // Создание строки соединения с базой данных
                    string connectionString =
                        $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={selectedFilePath};Jet OLEDB:Engine Type=5";
                    catalog.Create(connectionString);

                    using (OleDbConnection connection =
                               new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        // Создание таблицы "Персоны"
                        string createPeopleTableQuery =
                            "CREATE TABLE People ( PersonId INT PRIMARY KEY, Gender TEXT, FirstName TEXT, LastName TEXT, MiddleName TEXT, MaidenName TEXT, Occupation TEXT, ResidenceId INT, Religion TEXT, BirthDate DATE, BirthPlaceId INT, Notes TEXT, IsAlive YesNo, DeathDate DATE, DeathPlaceId INT, CauseOfDeath TEXT, FatherId INT, MotherId INT, Photo TEXT)";
                        using (OleDbCommand command =
                                   new OleDbCommand(createPeopleTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Создание таблицы "Места"
                        string createPlacesTableQuery =
                            "CREATE TABLE Places ( PlaceId INT PRIMARY KEY, Name TEXT, Abbreviation TEXT, Historical_name TEXT, Type TEXT, Description TEXT, Address TEXT, Comment TEXT, Picture TEXT);";
                        using (OleDbCommand command =
                                   new OleDbCommand(createPlacesTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Создание таблицы "События"
                        string createEventsTableQuery =
                            "CREATE TABLE Events ( EventId INT PRIMARY KEY, Type TEXT, EventDate DATE, EventLocationId INT, Description TEXT, Comment TEXT, Picture  TEXT)";
                        using (OleDbCommand command =
                                   new OleDbCommand(createEventsTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        string createParticipantsTableQuery =
                            "CREATE TABLE Participants (EventId INT, PersonId INT, Role TEXT);";
                        using (OleDbCommand command = new OleDbCommand(
                                   createParticipantsTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Здесь вы можете выполнить другие операции с базой данных,
                        // например, добавить данные в таблицы.

                        connection.Close();
                    }

                    // Создание базы данных

                    // Закрытие соединения с базой данных
                    catalog.ActiveConnection = null;

                    TreeEditor treeEditor = new TreeEditor(selectedFilePath);
                    treeEditor.Width = width;
                    treeEditor.Height = height;
                    treeEditor.Show();  // или otherForm.ShowDialog();
                    return true;


                }
                catch (FileNotFoundException ex)
                {
                    Warning.WarningShow(
                        "Выбранный файл не найден. Проверьте корректность файла.",
                        "Ошибка");

                    return false;
                }
                catch (DirectoryNotFoundException ex)
                {
                    Warning.WarningShow(
                        "Указанный каталог не найден. Проверьте корректность файла.",
                        "Ошибка");

                    return false;
                }
                catch (UnauthorizedAccessException ex)
                {
                    Warning.WarningShow(
                        "Перезапустите приложение от имени Администратора или выберите не системную папку для создания проекта.",
                        "Недостаточно прав");
                    return false;

                }
                
                catch (Exception ex)
                {
                    
                    System.Windows.MessageBox.Show(ex.ToString());
                    Warning.WarningShow("Непредвиденная ошибка. Попробуйте ещё раз.",
                                        "Ошибка");

                    return false;
                }

            }
            else
            {
                return false;
            }

        }
        public static bool chooseTree(double height, double width)
        {
            try

            {
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    DefaultExt = ".accdb",  // Расширение файла по умолчанию
                    Filter =
                      "Файлы базы данных (*.accdb)|*.accdb"  // Фильтр для отображения
                                                             // только файлов с
                                                             // расширением .accdb
                };
                bool? result = dialog.ShowDialog();


                // Проверка результата диалога
                if (result == true)
                {
                    string selectedFilePath = dialog.FileName;




                    bool isValidFile = false;
                    using (
                        var connection = new OleDbConnection(
                            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={selectedFilePath};"))
                    {
                        try
                        {
                            connection.Open();

                            // Проверяем наличие таблиц "Events", "People", "Places" и
                            // "Participants"
                            using (var schema = connection.GetSchema("Tables"))
                            {
                                var tableNames =
                                    schema.AsEnumerable()
                                        .Select(row => row["TABLE_NAME"].ToString())
                                        .ToList();

                                if (tableNames.Contains("Events") &&
                                    tableNames.Contains("People") &&
                                    tableNames.Contains("Places") &&
                                    tableNames.Contains("Participants"))
                                {
                                    isValidFile = true;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }

                    if (isValidFile)
                    {
                        string selectedFullFolder = System.IO.Path.GetDirectoryName(dialog.FileName);
                        string selectedFolderName = System.IO.Path.GetFileName(selectedFullFolder);


                        if (!System.IO.Directory.Exists(selectedFullFolder + $"\\{selectedFolderName}-Resources"))
                        {
                            Directory.CreateDirectory(selectedFullFolder + $"\\{selectedFolderName}-Resources");

                        }
                        if (!System.IO.Directory.Exists(selectedFullFolder + $"\\{selectedFolderName}-Resources\\People"))
                        {
                            Directory.CreateDirectory(selectedFullFolder + $"\\{selectedFolderName}-Resources\\People");

                        }
                        if (!System.IO.Directory.Exists(selectedFullFolder + $"\\{selectedFolderName}-Resources\\Places"))
                        {
                            Directory.CreateDirectory(selectedFullFolder + $"\\{selectedFolderName}-Resources\\Places");

                        }

                        if (!System.IO.Directory.Exists(selectedFullFolder + $"\\{selectedFolderName}-Resources\\Events"))
                        {
                            Directory.CreateDirectory(selectedFullFolder + $"\\{selectedFolderName}-Resources\\Events");

                        }
                        TreeEditor treeEditor = new TreeEditor(selectedFilePath);
                        treeEditor.Width = width;
                        treeEditor.Height = height;
                        treeEditor.Show();                    }
                    else
                    {
                        Warning.WarningShow(
                            "Выбранный файл базы данных не является файлом базы данных генеалогического древа нашей программы.",
                            "Ошибка");

                        return false;
                    }
                }
                return (bool)result;

            }

            catch (FileNotFoundException ex)
            {
                Warning.WarningShow(
                    "Выбранный файл не найден. Проверьте корректность файла.",
                    "Ошибка");

                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                Warning.WarningShow(
                    "Указанный каталог не найден. Проверьте корректность файла.",
                    "Ошибка");

                return false;
            }
            catch (SecurityException ex)
            {
                Warning.WarningShow(
                    "Отсутствуют необходимые разрешения для доступа к файлу.",
                    "Ошибка");
                return false;

            }
            catch
            {
                Warning.WarningShow(
                    "Непредвиденная ошибка. Проверьте корректность файла.", "Ошибка");

                return false;
            }

        }
        public  void ChooseExistingFamilyTree(object sender, RoutedEventArgs e)
        {
          bool x=  chooseTree(550,1100);
           if(x) Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}