using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Steganografia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap zdjecieWybrane;
        Bitmap zdjecieZInformacjami;
        string koniecTekstu = "CeinokTekstu";
        int iloscMaxZnaki = 0;
        int wartoscR;
        int wartoscG;
        int wartoscB;
        byte[] tablicaBitow;
        string tekstBinarnie;
        string wybranyKolor;
        Color kolor;
        Color nowyKolor;
        ComboBoxItem comboBoxItem;
        int licznik = 0;


        OpenFileDialog openFileDlg = new OpenFileDialog();
        SaveFileDialog savedialog = new SaveFileDialog();
        UTF8Encoding utf8 = new UTF8Encoding();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void przyciskWybierzZdjecie_Click(object sender, RoutedEventArgs e)
        {
            maxZaki.Content = 0;
            tekstDoZakodowania.Text = "";
            iloscMaxZnaki = 0;

            openFileDlg.Filter = "Image Files(*.PNG; *.JPG; *.JPGE)| *.PNG; *.JPG; *.JPGE";
            bool? result = openFileDlg.ShowDialog();

            if (result == true)
            {

                tabelaSciezkaZdjecia.Content = openFileDlg.FileName;
                zdjecieWybrane = new Bitmap(openFileDlg.FileName);

                MaxIloscZnakow();
                uzyteZnaki.Content = tekstDoZakodowania.Text.ToString().Length;
            }
        }

        private void tekstDoZakodowania_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (iloscMaxZnaki != 0)
            {
                uzyteZnaki.Content = tekstDoZakodowania.Text.ToString().Length;
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (zdjecieWybrane != null)
            {
                MaxIloscZnakow();
            }
            
        }


        private void przyciskKodujTekst_Click(object sender, RoutedEventArgs e)
        {
            if (zdjecieWybrane == null)
            {
                MessageBox.Show("Brak wybranego zdjęcia", "Brak zdęcia", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            licznik = 0;
            zdjecieZInformacjami = new Bitmap(zdjecieWybrane.Width, zdjecieWybrane.Height);
            tablicaBitow = Encoding.UTF8.GetBytes(tekstDoZakodowania.Text + koniecTekstu);
            tekstBinarnie = string.Join("", tablicaBitow.Select(bit => Convert.ToString(bit, 2).PadLeft(8, '0')));

            switch (wybranyKolor)
            {
                case "R":

                    for (int i = 0; i < zdjecieWybrane.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybrane.Height; j++)
                        {
                            kolor = zdjecieWybrane.GetPixel(i, j);
                            if (licznik < tekstBinarnie.Length)
                            {
                                if (tekstBinarnie[licznik] == '1')
                                {
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                    }
                                }
                                else
                                {
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 1)
                                    {
                                        wartoscR -= 1;
                                    }
                                }
                                nowyKolor = Color.FromArgb(wartoscR, kolor.G, kolor.B);
                            }
                            else
                            {
                                nowyKolor = Color.FromArgb(kolor.R, kolor.G, kolor.B);
                            }
                            zdjecieZInformacjami.SetPixel(i, j, nowyKolor);
                            licznik++;
                        }
                    }
                    break;

                case "RG":
                    for (int i = 0; i < zdjecieWybrane.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybrane.Height; j++)
                        {
                            kolor = zdjecieWybrane.GetPixel(i, j);
                            if (licznik + 1 < tekstBinarnie.Length)
                            {
                                if (tekstBinarnie[licznik] == '1')
                                {
                                    
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                    }
                                }
                                else
                                {
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 1)
                                    {
                                        wartoscR -= 1;
                                    }
                                }

                                if (tekstBinarnie[licznik + 1] == '1')
                                {

                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 0)
                                    {
                                        wartoscG += 1;
                                    }
                                }
                                else
                                {
                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 1)
                                    {
                                        wartoscG -= 1;
                                    }
                                }
                                nowyKolor = Color.FromArgb(wartoscR, wartoscG, kolor.B);
                            }
                            else if (licznik < tekstBinarnie.Length)
                            {
                                if (tekstBinarnie[licznik] == '1')
                                {

                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                    }
                                }
                                else
                                {
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 1)
                                    {
                                        wartoscR -= 1;
                                    }
                                }
                                nowyKolor = Color.FromArgb(wartoscR, kolor.G, kolor.B);
                            }
                            else
                            {
                                nowyKolor = Color.FromArgb(kolor.R, kolor.G, kolor.B);
                            }
                            zdjecieZInformacjami.SetPixel(i, j, nowyKolor);
                            licznik=+2;
                        }
                    }

                    break;

                case "RGB":
                    for (int i = 0; i < zdjecieWybrane.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybrane.Height; j++)
                        {
                            kolor = zdjecieWybrane.GetPixel(i, j);
                            if (licznik + 2 < tekstBinarnie.Length)
                            {
                                if (tekstBinarnie[licznik] == '1')
                                {

                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                    }
                                }
                                else
                                {
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 1)
                                    {
                                        wartoscR -= 1;
                                    }
                                }

                                if (tekstBinarnie[licznik + 1] == '1')
                                {

                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 0)
                                    {
                                        wartoscG += 1;
                                    }
                                }
                                else
                                {
                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 1)
                                    {
                                        wartoscG -= 1;
                                    }
                                }

                                if (tekstBinarnie[licznik + 2] == '1')
                                {

                                    wartoscB = kolor.B;
                                    if (wartoscB % 2 == 0)
                                    {
                                        wartoscG += 1;
                                    }
                                }
                                else
                                {
                                    wartoscB = kolor.B;
                                    if (wartoscB % 2 == 1)
                                    {
                                        wartoscB -= 1;
                                    }
                                }
                                nowyKolor = Color.FromArgb(wartoscR, wartoscG, wartoscB);
                            }
                            else if (licznik + 1 < tekstBinarnie.Length)
                            {
                                if (tekstBinarnie[licznik] == '1')
                                {

                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                    }
                                }
                                else
                                {
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 1)
                                    {
                                        wartoscR -= 1;
                                    }
                                }

                                if (tekstBinarnie[licznik + 1] == '1')
                                {

                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 0)
                                    {
                                        wartoscG += 1;
                                    }
                                }
                                else
                                {
                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 1)
                                    {
                                        wartoscG -= 1;
                                    }
                                }
                                nowyKolor = Color.FromArgb(wartoscR, wartoscG, kolor.B);
                            }
                            else if (licznik < tekstBinarnie.Length)
                            {
                                if (tekstBinarnie[licznik] == '1')
                                {

                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                    }
                                }
                                else
                                {
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 1)
                                    {
                                        wartoscR -= 1;
                                    }
                                }
                                nowyKolor = Color.FromArgb(wartoscR, kolor.G, kolor.B);
                            }
                            else
                            {
                                nowyKolor = Color.FromArgb(kolor.R, kolor.G, kolor.B);
                            }
                            zdjecieZInformacjami.SetPixel(i, j, nowyKolor);
                            licznik = +3;
                        }
                    }
                    break;
            }


            


            savedialog.Filter = "(*.jpg) | *.jpg";
            savedialog.FileName = "";
            bool? result = savedialog.ShowDialog();
            if (result == true)
            {
                zdjecieZInformacjami.Save(savedialog.FileName + savedialog.DefaultExt);//Coś jest zjebane z zapisywanie,formarem lub spospbem kompresji
            }
        }

        private void MaxIloscZnakow()
        {
            comboBoxItem = (ComboBoxItem)comboBoxIleKolorow.SelectedItem;
            wybranyKolor= comboBoxItem.Content.ToString();
            switch (wybranyKolor)
            {
                case "R":
                    iloscMaxZnaki = (zdjecieWybrane.Width * zdjecieWybrane.Height) / 8 - koniecTekstu.Length;
                    maxZaki.Content = iloscMaxZnaki;
                    tekstDoZakodowania.MaxLength = iloscMaxZnaki;
                    break;

                case "RG":
                    iloscMaxZnaki = (zdjecieWybrane.Width * zdjecieWybrane.Height / 8) * 2 - koniecTekstu.Length;
                    maxZaki.Content = iloscMaxZnaki;
                    tekstDoZakodowania.MaxLength = iloscMaxZnaki;
                    break;

                case "RGB":
                    iloscMaxZnaki = (zdjecieWybrane.Width * zdjecieWybrane.Height / 8) * 3 - koniecTekstu.Length;
                    maxZaki.Content = iloscMaxZnaki;
                    tekstDoZakodowania.MaxLength = iloscMaxZnaki;
                    break;
            }
        }
    }

}