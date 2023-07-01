using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GiGraph.Dot.Entities.Graphs;
using GiGraph.Dot.Types.Nodes;
using GiGraph.Dot.Extensions;
using GiGraph.Dot.Entities.Edges;
using GiGraph.Dot.Types.Edges;
using System.Data.OleDb;
using System.Xml.Linq;
using System.Windows.Forms;
using ComboBox = System.Windows.Controls.ComboBox;
using System.IO;
using GiGraph.Dot.Entities.Html.Builder;
using GiGraph.Dot.Types.Fonts;

namespace Family_Tree
{

    public partial class Tree : System.Windows.Window
    {
        string databasefile;
        public Tree(string databasefileinput)
        {
            InitializeComponent();
            this.databasefile = databasefileinput;
        }

        private DotNodeShape Get_Node_Shape(ComboBox picker)
        {
            DotNodeShape shape = new DotNodeShape();
            if (picker.Text == "Прямоугольник")
            {
                shape = DotNodeShape.Box;
            }
            else if (picker.Text == "Овал")
            {
                shape = DotNodeShape.Oval;

            }

            else if (picker.Text == "Овал")
            {
                shape = DotNodeShape.Oval;

            }
            else if (picker.Text == "Звезда")
            {
                shape = DotNodeShape.Star;

            }
            else if (picker.Text == "Треугольник")
            {
                shape = DotNodeShape.Triangle;

            }
            else if (picker.Text == "Дом")
            {
                shape = DotNodeShape.House;

            }
            else if (picker.Text == "Цилиндр")
            {
                shape = DotNodeShape.Cylinder;

            }
            else if (picker.Text == "Заметка")
            {
                shape = DotNodeShape.Note;

            }
            else if (picker.Text == "Компонент")
            {
                shape = DotNodeShape.Component;

            }
            else if (picker.Text == "Параллелограм")
            {
                shape = DotNodeShape.Parallelogram;

            }

            else if (picker.Text == "Папка")
            {
                shape = DotNodeShape.Folder;

            }

            else if (picker.Text == "Яйцо")
            {
                shape = DotNodeShape.Egg;
            }
            return shape;
        }
        private string Get_Color(Ellipse ellipse)
        {
            string hexString = "#000000";
            if (ellipse.Fill is SolidColorBrush solidColorBrush)
            {
                System.Windows.Media.Color color = solidColorBrush.Color;

                // Получение шестнадцатеричных значений каналов цвета
                string redHex = color.R.ToString("X2");
                string greenHex = color.G.ToString("X2");
                string blueHex = color.B.ToString("X2");

                // Формирование строки в формате HEX
                hexString = "#" + redHex + greenHex + blueHex;
            }
            return hexString;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {


            
                try
            {string husbandOrWife = "";
                    string exhusbandOrexWife = "";
                    string momOrDad = "";

                    List<People> branches = new List<People>();
                branches = People.GetPeople(databasefile);

                foreach(People branch in branches) { 
                 

                        husbandOrWife = (branch.Gender == "Женский пол") ? "Муж" : "Жена";
                        string whoAmi1 = (branch.Gender == "Женский пол") ? "Жена" : "Муж";
                        string whoAmi2 =
                            (branch.Gender == "Женский пол") ? "Бывшая жена" : "Бывший муж";
                        exhusbandOrexWife =
                            (branch.Gender == "Женский пол") ? "Бывший муж" : "Бывшая жена";
                        momOrDad =
                            (branch.Gender == "Женский пол") ? "FatherId" : "MotherId";

                        List<People> husbands = new List<People>();
                        List<People> exes = new List<People>();
                        List<People> parents = new List<People>();

                    string connect =
                      $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasefile};Jet OLEDB:Engine Type=5";
                    OleDbConnection dbconnection = new OleDbConnection(connect);
                    dbconnection.Open();


                    using (OleDbConnection dbConnection =
                                   new OleDbConnection(connect))
                        {
                            dbConnection.Open();
                            string sqlQuery =
                                $"SELECT PersonId FROM Participants WHERE EventID IN ( SELECT EventId FROM Events WHERE (EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {branch.PersonId} AND Role ='{whoAmi1}') AND Type = 'Брак' AND Role = '{husbandOrWife}') ) AND PersonId <> {branch.PersonId};";
                            using (OleDbCommand command =
                                       new OleDbCommand(sqlQuery, dbConnection))
                            {
                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                while (reader.Read())
                                {
                                    {
                                      
                                            People husbik = new People()
                                            {

                                                PersonId = reader.GetInt32(0)
                                            };
                                            husbands.Add(husbik);
                                        
                                    }
                                }
                                }
                            }

                            sqlQuery =
                                $"SELECT PersonId FROM Participants WHERE EventID IN ( SELECT EventId FROM Events WHERE (EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {branch.PersonId} AND Role = '{whoAmi2}') AND Type = 'Развод' AND Role = '{exhusbandOrexWife}') ) AND PersonId <> {branch.PersonId};";
                            using (OleDbCommand command =
                                       new OleDbCommand(sqlQuery, dbConnection))
                            {
                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                while (reader.Read())
                                {
                                  
                                        People ex = new People()
                                            {

                                                PersonId = reader.GetInt32(0)
                                            };

                                        exes.Add(ex); 

                                    
                                }
                                }
                            }

                            string dadOrMom =
                                momOrDad == "FatherId" ? "MotherId" : "FatherId";
                            string role1 =
                                momOrDad == "FatherId" ? "Бывший муж" : "Бывшая жена";
                            string role2 = momOrDad == "FatherId" ? "Муж" : "Жена";

                            sqlQuery =
                                $"SELECT DISTINCT {momOrDad} FROM People Where ({dadOrMom}={branch.PersonId} AND {momOrDad} NOT IN ( SELECT PersonId FROM Participants WHERE EventID IN ( SELECT EventId FROM Events WHERE (EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {branch.PersonId} ) AND Type = 'Брак' AND Role = '{role2}') ) AND PersonId <> {branch.PersonId} )) AND {momOrDad} NOT IN (SELECT PersonId FROM Participants WHERE EventID IN ( SELECT EventId FROM Events WHERE (EventId IN ( SELECT EventId FROM Participants WHERE PersonId = {branch.PersonId} ) AND Type = 'Развод' AND Role = '{role1}') ) AND PersonId <> {branch.PersonId})";
                            using (OleDbCommand command =
                                       new OleDbCommand(sqlQuery, dbConnection))
                            {
                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0)
                                        {
                                        People parent = new People()
                                            {

                                                PersonId = reader.GetInt32(0)
                                            };
                                            parents
                                                .Add(parent);
                                        }
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

                            if (parents.Count != 0)
                            {
                                foreach (var parent in parents)
                                {
                                    branch.relates.Add(new Participants
                                    {
                                        PersonId = parent.PersonId,
                                        Role = $"Родитель"
                                    });
                                }
                            }
                            if (exes.Count != 0)
                            {
                                foreach (var ex in exes)
                                {
                                    branch.relates.Add(
                                        new Participants { PersonId = ex.PersonId, Role = $"Бывший" });
                                }
                            }
                            if (husbands.Count != 0)
                            {
                                foreach (var husband in husbands.ToList())
                                {
                                    branch.relates.Add(new Participants
                                    {
                                        PersonId = husband.PersonId,
                                        Role = $"Супруг"
                                    });
                                }
                            }
                        }
                    }


                    // Создание графа
                    var graph = new DotGraph();
                    graph.Edges.Directions =
                        GiGraph.Dot.Types.Edges.DotEdgeDirections.None;
                    graph.EdgeShape = DotEdgeShape.Line;

                    string rankdir = "";

                    if ((bool)ascending.IsChecked && (bool)vertical.IsChecked)
                    {
                        rankdir = "TB";
                    }
                    else if ((bool)descending.IsChecked && (bool)vertical.IsChecked)
                    {
                        rankdir = "BT";

                    }

                    else if ((bool)ascending.IsChecked && (bool)horizontal.IsChecked)
                    {
                        rankdir = "LR";

                    }
                    else if ((bool)descending.IsChecked && (bool)horizontal.IsChecked)
                    {
                        rankdir = "RL";
                    }

                    graph.Attributes.Collection.SetCustom("rankdir", rankdir);
                    graph.Attributes.Collection.SetCustom("bgcolor",
                                                          Get_Color(backgroundcolor));

                  
                    var relationshipNodes =
                        new List<string>();  // Словарь для хранения существующих узлов
                                             // отношений

                    foreach (var branch in branches)



                    {


                    string birthdate = branch.BirthDate.ToShortDateString();
                    string deathdate = branch.DeathDate.ToShortDateString();
                    string date = "";

                    if ((bool)showyears.IsChecked)
                    {

                        if (birthdate != "01.01.0001")
                        {
                            date += birthdate;
                            if (deathdate != "01.01.0001")
                            {
                                date += " - " + deathdate;
                            }
                            else if (!branch.IsAlive)
                            {
                                date += " - ?";

                            }
                           
                        }
                        else if (deathdate != "01.01.0001")
                        {
                            date += "? - " + deathdate;
                        }
                        else
                        {
                            date = "?";
                            if (!branch.IsAlive)
                            {
                                date = "???";
                            }
                        }
                    }
                  

                    graph.Nodes.Add(branch.PersonId.ToString(),
                                        node => {
                                            node.Font.Size = int.Parse(fontsizetextbox.Text);

                                            node.Label = ((bool)showlastname.IsChecked ? $"{branch.FirstName} {branch.LastName}" : branch.FirstName);
                                          
                                            if((bool)pelastname.IsChecked && (bool)showlastname.IsChecked && !string.IsNullOrEmpty(branch.LastName))
                                            {
                                                node.Label = $"{branch.FirstName}\n{branch.LastName}";

                                            }
                                            if ((bool)showyears.IsChecked)
                                                if ((bool)showyears.IsChecked)
                                            {
                                                node.Label += $"\n{date}";
                                            }
                                            node.Style.FillStyle = DotNodeFillStyle.Normal;
                                            node.Size.Height = 0.5;
                                            if (branch.Gender == "Женский пол")
                                            {
                                                node.Shape = Get_Node_Shape(womanshape);

                                                node.Attributes.Collection.SetCustom(
                                        "fillcolor", Get_Color(womanfillcolor));
                                                node.Attributes.Collection.SetCustom(
                                        "color", Get_Color(womancolor));

                                            }
                                            else
                                            {
                                                node.Shape = Get_Node_Shape(manshape);

                                                node.Attributes.Collection.SetCustom(
                                        "fillcolor", Get_Color(manfillcolor));
                                                node.Attributes.Collection.SetCustom(
                                        "color", Get_Color(mancolor));
                                            }

                                            string fontname = font.Text;
                                            if (fontname == "Times New Roman")
                                            {
                                                fontname = "Palatino-Roman";
                                            }
                                            node.Attributes.Collection.SetCustom(
                                    "fontcolor", Get_Color(fontcolor));
                                            node.Attributes.Collection.SetCustom(
                                    "fontname", $"{fontname}");
                                            node.BorderWidth = 3;


                                            if(deathUniqe.IsChecked == true){                                          
                                                if (!branch.IsAlive)
                                                {
                                                    node.Attributes.Collection.SetCustom(
                                            "fillcolor", Get_Color(deathfillcolor));
                                                    node.Attributes.Collection.SetCustom(
                                            "color", Get_Color(deathcolor));
                                                }
                                            }
                                        }

                        );

                        if (branch.relates != null && branch.relates.Count > 0)
                        {
                            foreach (var relationship in branch.relates)
                            {
                                if (relationship.PersonId != 0)
                                {
                                string nodeId = "";

                                
                                
                                     nodeId =
                                       $"Relationship_{relationship.PersonId}_{branch.PersonId}";

                                    if (!relationshipNodes.Contains(nodeId))
                                    {
                                        string creatingNodeId =
                                            $"Relationship_{branch.PersonId}_{relationship.PersonId}";

                                        // Создаем новый узел отношений и связываем его с человеком
                                        graph.Nodes.Add(creatingNodeId, node => {

                                            node.Label = "";
                                            node.Style.FillStyle = DotNodeFillStyle.Normal;
                                            node.Shape = DotNodeShape.Circle;
                                            node.Size.Width = 0.2;
                                            node.Size.Height = 0.2;
                                            node.Attributes.Collection.SetCustom(
                                                "image",

                                                ((relationship.Role == "Бывший")
                                                     ? "Assets/Images/Icons/broken_heart.png"
                                                     : ((relationship.Role == "Супруг")
                                                            ? "Assets/Images/Icons/heart.png"
                                                            : (relationship.Role == "Родитель")? "Assets/Images/Icons/puzzle.png" : "")));
                                            node.Attributes.Collection.SetCustom(
                                                "color", ((relationship.Role == "Бывший")
                                                              ? Get_Color(connectdivorsion)
                                                              : ((relationship.Role == "Супруг")
                                                                     ? Get_Color(connectmarrriage)
                                                                     : Get_Color(connectunknown))));
                                            node.Attributes.Collection.SetCustom("style", "bold");
                                        });

                                        relationshipNodes.Add(creatingNodeId);

                                        var edge = new DotEdge(relationship.PersonId.ToString(),
                                                               creatingNodeId.ToString());

                                        edge.Attributes.Collection.SetCustom(
                                            "color", ((relationship.Role == "Бывший")
                                                          ? Get_Color(connectdivorsion)
                                                          : ((relationship.Role == "Супруг")
                                                                 ? Get_Color(connectmarrriage)
                                                                 : Get_Color(connectunknown))));
                                        graph.Edges.Add(edge);

                                        edge = new DotEdge(branch.PersonId.ToString(),
                                                           creatingNodeId.ToString());

                                        edge.Attributes.Collection.SetCustom(
                                            "color", ((relationship.Role == "Бывший")
                                                          ? Get_Color(connectdivorsion)
                                                          : ((relationship.Role == "Супруг")
                                                                 ? Get_Color(connectmarrriage)
                                                                 : Get_Color(connectunknown))));
                                    graph.Edges.Add(edge);
                                    }
                                }
                            }
                        }

                        if (branch.MotherId != 0 && branch.FatherId != 0)
                        {
                            if (branch.MotherId < branch.FatherId )
                            {
                                var edge =
                                    new DotEdge($"Relationship_{branch.MotherId}_{branch.FatherId}",
                                                branch.PersonId.ToString());

                                edge.Attributes.Collection.SetCustom(
                                    "color", Get_Color(connectchildren));

                                graph.Edges.Add(edge);
                            }
                            else
                            {
                                var edge =
                                    new DotEdge($"Relationship_{branch.FatherId}_{branch.MotherId}",
                                                branch.PersonId.ToString());

                                edge.Attributes.Collection.SetCustom(
                                    "color", Get_Color(connectchildren));

                                graph.Edges.Add(edge);

                        }

                    }

                        else
                        {
                            if (branch.MotherId != 0)
                            {

                            var mom = branches.FirstOrDefault(b => b.PersonId == branch.MotherId);
                                if (mom != null)
                                {
                                    var edge =
                                        new DotEdge(mom.PersonId.ToString(), branch.PersonId.ToString());

                                    edge.Attributes.Collection.SetCustom(
                                        "color", Get_Color(connectchildren));

                                    graph.Edges.Add(edge);
                                }
                            }

                            if (branch.FatherId != 0)
                            {

                            var dad = branches.FirstOrDefault(b => b.PersonId == branch.FatherId);
                                if (dad != null)
                                {
                                    var edge =
                                        new DotEdge(dad.PersonId.ToString(), branch.PersonId.ToString());

                                    edge.Attributes.Collection.SetCustom(
                                        "color", Get_Color(connectchildren));

                                    graph.Edges.Add(edge);
                                }
                            }
                        }
                    }

                if (lbl.Text != "")
                {
                    graph.Label = new DotHtmlBuilder()
            // appends a <font> element to the builder, with a custom size, color and style
            .AppendStyledFont(new DotStyledFont(DotFontStyles.Bold),
                // specifies content of the parent <font> element
                font => font
                   // appends any custom HTML
                   // appends plain text and text embedded in another <font> tag with a color specified
                   .AppendText(lbl.Text)
            )
            // appends a <br/> element
            .AppendLine()
            .AppendLine()

            .Build();
                }
            ; string fontname_ = font.Text;
                if (fontname_ == "Times New Roman")
                {
                    fontname_ = "Palatino-Roman";
                }
                graph.Attributes.Collection.SetCustom("fontname", fontname_);

                graph.Attributes.Collection.SetCustom("labelloc", "t");
                graph.Font.Size = int.Parse( numberTextBox.Text);




                try
                {
                    graph.SaveToFile("graph.gv");
                    // Путь к утилите dot.exe
                    string dotPath = "Assets/Graphviz/bin/dot.exe";
                    // Путь к файлу GV
                    string inputPath = "graph.gv";
                    // Путь к выходному PNG-изображению
                    string outputPath = "graph.png";

                    Process process = new Process();


                    process.StartInfo.FileName = dotPath;

                    process.StartInfo.Arguments =
                        $"-Tpng \"{inputPath}\" -o \"{outputPath}\"";

                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;


                    process.Start();
                    process.WaitForExit();







                    ShowTree showTree = new ShowTree(databasefile, this);

                    showTree.Background = backgroundcolor.Fill;










                    var bitmap = new BitmapImage();
                    var stream = System.IO.File.OpenRead("graph.png");

                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    stream.Close();
                    stream.Dispose();



                    showTree.imageviewer.Source = bitmap;

                    showTree.Closed += Form_Closed;

                    showTree.ShowDialog();
                }
                catch (UnauthorizedAccessException ex)
                {
                    Warning.WarningShow(
                        "Перезапустите приложение от имени Администратора или переустановите приложение не в системную папку.",
                        "Недостаточно прав");
                   

                }



            }
            catch(Exception ex)
                {

                Warning.WarningShow("Непредвиденная ошибка. Попробуйте ещё раз.", "Ошибка");

                return;
                }
           // }
        }
        private void Form_Closed(object sender, EventArgs e)
        {
            try
            {


                System.IO.File.Delete("graph.gv");
                System.IO.File.Delete("graph.png");
            }
            catch 
            {


            }


        }

    
        
     
        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color selectedColor = colorDialog.Color;

                // Преобразование цвета в формат, поддерживаемый WPF
                System.Windows.Media.Color wpfColor =
                    System.Windows.Media.Color.FromArgb(
                        selectedColor.A, selectedColor.R, selectedColor.G,
                        selectedColor.B);

                // Создание кисти с новым цветом
                System.Windows.Media.SolidColorBrush brush =
                    new System.Windows.Media.SolidColorBrush(wpfColor);

                // Проверка, является ли sender экземпляром Shape
                if (sender is System.Windows.Shapes.Shape shape)
                {
                    // Установка новой кисти в качестве заливки фигуры
                    shape.Fill = brush;
                }
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
        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(numberTextBox.Text, out int number))
            {
                if (int.Parse(numberTextBox.Text) < 65)
                {
                    number ++;
                    numberTextBox.Text = number.ToString();
                }
            }
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(numberTextBox.Text, out int number))
            {
                if (int.Parse(numberTextBox.Text)>25)
                {
                    number--;
                    numberTextBox.Text = number.ToString();
                }
              
            }
        }

        private void IncreaseButton2_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(fontsizetextbox.Text, out int number))
            {
                if (int.Parse(fontsizetextbox.Text) < 30)
                {
                    number++;
                    fontsizetextbox.Text = number.ToString();
                }
            }
        }

        private void DecreaseButton2_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(fontsizetextbox.Text, out int number))
            {
                if (int.Parse(fontsizetextbox.Text) > 8)
                {
                    number--;
                    fontsizetextbox.Text = number.ToString();
                }

            }
        }





    }
}
