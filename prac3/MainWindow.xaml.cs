using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using lib_13;
using LibMas;

namespace prac3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Random_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                Rez.Text = ""; 
                Int32.TryParse(random.Text, out int ran);
                Int32.TryParse(Stroki.Text, out int rows);
                Int32.TryParse(Stolbzi.Text, out int column);
                mas = LibMas.Masssiv.DVZapol(rows, column, ran);
                dataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Введите значения размерности массива больше -1");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Введите значения рандома больше 0");
            }
        }

        private void Chistk_Click(object sender, RoutedEventArgs e)
        {
            mas = LibMas.Masssiv.Clear(mas);
            dataGrid.ItemsSource = null;
            Rez.Clear();
        }

        private void Itog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rez.Text = Resh.SredArif(mas);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Массив не может быть пустым");
            }
        }

        private void Quit_Clicl(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Spravka_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кузнецов Алексей Алексееевич ИСП-31. Вариант 13 Дана матрица размера M x N В каждом ее столбце найти количество элементов,меньших среднего арифметического всех элементов этого столбца");
        }

        private void Zapisat_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                LibMas.Masssiv.DVSaveMassiv(mas);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Массив не может быть пустым");
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LibMas.Masssiv.DVOpenMassiv(ref mas); 
                dataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Массив не может быть пустым");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Размерность массива не может быть меньше 0");
            }
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexColumn = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            mas[indexRow, indexColumn] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
            Rez.Text = "";
        }
    }
}
