using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Dnevnik3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Inc> Pics = new List<Inc>();  //Основной список
        List<Inc> PicsDate = new List<Inc>();  //Допольнительный список (с отбором по дате)
        Stream stream = new Stream();
        ObservableCollection<string> Pics2 = new ObservableCollection<string>(); //для отображения заметок в листбоксе
        string selected;
        string SelDate;
        int i = 0;
        int ii;
        string path = "C:\\Users\\Andre\\source\\repos\\Dnevnik3\\Dnevnik3\\Dnevnik.xml";
        public MainWindow()
        {
            if(File.Exists(path))
            {
                Pics = stream.Deserial();   //Выгрузка данных из файла
            }
            SelDate = DateTime.Now.ToShortDateString();
            foreach (var item in Pics)
            {
                if (item.Date == SelDate)
                {
                    PicsDate.Add(item);
                }
            }
            
            Pics2 = new ObservableCollection<string>();
            foreach (Inc f in PicsDate)
            {
                Pics2.Add(f.Name);
            }
            InitializeComponent();
            Mylbox.ItemsSource = Pics2;
            //Mylbox.ItemsSource = Pics;
        }

        private void Bsave_Click(object sender, RoutedEventArgs e)
        {
            Pics[ii].Name = Names.Text;  //Перезаписываем переменную основного списка
            Pics[ii].Script = Scripts.Text;
            stream.Serial(Pics); //Сохраняем в файл
            Scripts.Text = "";
            PicsDate.Clear();
            foreach (var item in Pics)  //Перезаписываем список всех заметок с отбором по дате
            {
                if (item.Date == SelDate)
                {
                    PicsDate.Add(item);
                }
            }
            Pics2.Clear();
            foreach (Inc f in PicsDate)  //Перезаписываем ObservableCollection
            {
                Pics2.Add(f.Name);
            }
        }

        private void Bnew_Click(object sender, RoutedEventArgs e)
        {
            //Inc Text = new Inc(Names.Text, Scripts.Text, Dates.Text);
            Inc Text = new Inc();
            Text.Name = Names.Text;
            Text.Script = Scripts.Text;
            Text.Date = Dates.Text;
            Pics.Add(Text);
            stream.Serial(Pics); //Сохраняем в файл
            PicsDate.Clear();
            foreach (var item in Pics)  //Перезаписываем список заметок (отбор по дате)
            {
                if (item.Date == SelDate)
                {
                    PicsDate.Add(item);
                }
            }
            Pics2.Clear();
            foreach (Inc f in PicsDate)  //Перезаписываем ObservableCollection
            {
                Pics2.Add(f.Name);
            }

        }

        private void Mylbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = Mylbox.SelectedItem as string;
            Names.Text = selected;
            foreach (var item in PicsDate)
            {
                if (item.Name == selected)
                {
                    Scripts.Text = item.Script;
                    ii = Pics.IndexOf(item);  //Ищем эту переменную в основном списке (для возможности её перезаписи)
                }
            }
            
        }

        private void Dates_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //SelDate = Dates.Text;
            if (i > 3)  //Это чтобы эта часть не работала припервом запуске
            {
                SelDate = Dates.Text;
            }
            i++;
            PicsDate.Clear();
            foreach (var item in Pics)
            {
                if (item.Date == SelDate)
                {
                    PicsDate.Add(item);
                }
            }
            Pics2.Clear();
            foreach (Inc f in PicsDate)
            {
                Pics2.Add(f.Name);
            }
        }

        private void Bdel_Click(object sender, RoutedEventArgs e)
        {
            Pics.RemoveAt(ii);
            PicsDate.Clear();
            foreach (var item in Pics)  //Перезаписываем список заметок (отбор по дате)
            {
                if (item.Date == SelDate)
                {
                    PicsDate.Add(item);
                }
            }
            Pics2.Clear();
            foreach (Inc f in PicsDate)  //Перезаписываем ObservableCollection
            {
                Pics2.Add(f.Name);
            }
        }
    }
}
