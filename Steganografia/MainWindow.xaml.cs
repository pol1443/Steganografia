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
        Bitmap zdjecieWybraneKodowanie;
        Bitmap zdjecieWybraneRozkodowanie;
        Bitmap zdjecieZInformacjami;
        string koniecTekstu = "CeinokTekstu";
        int iloscMaxZnaki = 0;
        int wartoscR;
        int wartoscG;
        int wartoscB;
        byte[] tablicaBitow;
        string tekstBinarnie;
        string wybranyKolorKodowanie;
        string wybranyKolorRozkodowanie;
        Color kolor;
        Color nowyKolor;
        ComboBoxItem comboBoxItemKodowanie;
        ComboBoxItem comboBoxItemRozkodowanie;
        int licznik = 0;
        int licznik2= 0;
        string znakBinarnie = "";
        byte[] tekstRozkodowanyWBajtach;
        string rozkodowanyTekst = "";


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
                zdjecieWybraneKodowanie = new Bitmap(openFileDlg.FileName);

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
            if (zdjecieWybraneKodowanie != null)
            {
                MaxIloscZnakow();
            }
            
        }


        private void przyciskKodujTekst_Click(object sender, RoutedEventArgs e)
        {
            if (zdjecieWybraneKodowanie == null)
            {
                MessageBox.Show("Brak wybranego zdjęcia", "Brak zdęcia", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            licznik = 0;
            zdjecieZInformacjami = new Bitmap(zdjecieWybraneKodowanie.Width, zdjecieWybraneKodowanie.Height);
            tablicaBitow = Encoding.UTF8.GetBytes(tekstDoZakodowania.Text + koniecTekstu);
            tekstBinarnie = string.Join("", tablicaBitow.Select(bit => Convert.ToString(bit, 2).PadLeft(8, '0')));
            string pom = "";
            int pomInt;
            switch (wybranyKolorKodowanie)
            {
                case "R":

                    for (int i = 0; i < zdjecieWybraneKodowanie.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybraneKodowanie.Height; j++)
                        {
                            kolor = zdjecieWybraneKodowanie.GetPixel(i, j);
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
                    for (int i = 0; i < zdjecieWybraneKodowanie.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybraneKodowanie.Height; j++)
                        {
                            kolor = zdjecieWybraneKodowanie.GetPixel(i, j);
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
                            licznik += 2;
                        }
                    }

                    break;

                case "RGB":
                    for (int i = 0; i < zdjecieWybraneKodowanie.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybraneKodowanie.Height; j++)
                        {
                            kolor = zdjecieWybraneKodowanie.GetPixel(i, j);
                            if (licznik + 2 < tekstBinarnie.Length)
                            {
                                if (tekstBinarnie[licznik] == '1')
                                {
                                    pom += "1";
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                       
                                    }
                                }
                                else
                                {
                                    pom += "0";
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 1)
                                    {
                                        wartoscR -= 1;
                                    }
                                }

                                if (tekstBinarnie[licznik + 1] == '1')
                                {
                                    pom += "1";
                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 0)
                                    {
                                        wartoscG += 1;
                                    }
                                }
                                else
                                {
                                    pom += "0";
                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 1)
                                    {
                                        wartoscG -= 1;
                                    }
                                }

                                if (tekstBinarnie[licznik + 2] == '1')
                                {
                                    pom += "1";
                                    wartoscB = kolor.B;
                                    if (wartoscB % 2 == 0)
                                    {
                                        wartoscB += 1;
                                    }
                                }
                                else
                                {
                                    pom += "0";
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
                                    pom += "1";
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                    }
                                }
                                else
                                {
                                    pom += "0";
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 1)
                                    {
                                        wartoscR -= 1;
                                    }
                                }

                                if (tekstBinarnie[licznik + 1] == '1')
                                {
                                    pom += "1";
                                    wartoscG = kolor.G;
                                    if (wartoscG % 2 == 0)
                                    {
                                        wartoscG += 1;
                                    }
                                }
                                else
                                {
                                    pom += "0";
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
                                    pom += "1";
                                    wartoscR = kolor.R;
                                    if (wartoscR % 2 == 0)
                                    {
                                        wartoscR += 1;
                                    }
                                }
                                else
                                {
                                    pom += "0";
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
                            licznik +=3;
                        }
                    }
                    break;
            }




            tekstDoZakodowania.Text = pom;
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
            comboBoxItemKodowanie = (ComboBoxItem)comboBoxIleKolorow.SelectedItem;
            wybranyKolorKodowanie= comboBoxItemKodowanie.Content.ToString();
            switch (wybranyKolorKodowanie)
            {
                case "R":
                    iloscMaxZnaki = (zdjecieWybraneKodowanie.Width * zdjecieWybraneKodowanie.Height) / 8 - koniecTekstu.Length;
                    maxZaki.Content = iloscMaxZnaki;
                    tekstDoZakodowania.MaxLength = iloscMaxZnaki;
                    break;

                case "RG":
                    iloscMaxZnaki = (zdjecieWybraneKodowanie.Width * zdjecieWybraneKodowanie.Height / 8) * 2 - koniecTekstu.Length;
                    maxZaki.Content = iloscMaxZnaki;
                    tekstDoZakodowania.MaxLength = iloscMaxZnaki;
                    break;

                case "RGB":
                    iloscMaxZnaki = (zdjecieWybraneKodowanie.Width * zdjecieWybraneKodowanie.Height / 8) * 3 - koniecTekstu.Length;
                    maxZaki.Content = iloscMaxZnaki;
                    tekstDoZakodowania.MaxLength = iloscMaxZnaki;
                    break;
            }
        }

        private void przyciskWybierzZdjecieRozkodowanie_Click(object sender, RoutedEventArgs e)
        {
            openFileDlg.Filter = "Image Files(*.PNG; *.JPG; *.JPGE)| *.PNG; *.JPG; *.JPGE";
            bool? result = openFileDlg.ShowDialog();

            if (result == true)
            {

                tabelaSciezkaZdjeciaRozkodowanie.Content = openFileDlg.FileName;
                zdjecieWybraneRozkodowanie = new Bitmap(openFileDlg.FileName);
            }
        }

        private void przyciskRozkodowanieTekst_Click(object sender, RoutedEventArgs e)
        {
            if (zdjecieWybraneRozkodowanie == null)
            {
                MessageBox.Show("Brak wybranego zdjęcia", "Brak zdęcia", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            znakBinarnie = "";
            licznik = 0;
            licznik2 = 0;
            tekstRozkodowanyWBajtach = new byte[zdjecieWybraneRozkodowanie.Height * zdjecieWybraneRozkodowanie.Width];

            switch (wybranyKolorRozkodowanie)
            {
                case "R":
                    for (int i = 0; i < zdjecieWybraneRozkodowanie.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybraneRozkodowanie.Height; j++)
                        {
                            kolor = zdjecieWybraneRozkodowanie.GetPixel(i, j);

                            if (kolor.R % 2 != 0)
                            {
                                znakBinarnie += "1";
                            }
                            else
                            {
                                znakBinarnie += "0";
                            }
                            licznik++;

                            if (licznik == 8)
                            {
                                tekstRozkodowanyWBajtach[licznik2] = Convert.ToByte(znakBinarnie, 2);
                                licznik = 0;
                                licznik2++;
                                znakBinarnie = "";
                            }

                        }
                    }
                    break;

                case "RG":
                    for (int i = 0; i < zdjecieWybraneRozkodowanie.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybraneRozkodowanie.Height; j++)
                        {
                            kolor = zdjecieWybraneRozkodowanie.GetPixel(i, j);

                            if (kolor.R % 2 != 0)
                            {
                                znakBinarnie += "1";
                            }
                            else
                            {
                                znakBinarnie += "0";
                            }
                            licznik++;

                            if (licznik == 8)
                            {
                                tekstRozkodowanyWBajtach[licznik2] = Convert.ToByte(znakBinarnie, 2);
                                licznik = 0;
                                licznik2++;
                                znakBinarnie = "";
                            }

                            if (kolor.G % 2 != 0)
                            {
                                znakBinarnie += "1";
                            }
                            else
                            {
                                znakBinarnie += "0";
                            }
                            licznik++;

                            if (licznik == 8)
                            {
                                tekstRozkodowanyWBajtach[licznik2] = Convert.ToByte(znakBinarnie, 2);
                                licznik = 0;
                                licznik2++;
                                znakBinarnie = "";
                            }
                        }
                    }
                    break;

                case "RGB":
                    for (int i = 0; i < zdjecieWybraneRozkodowanie.Width; i++)
                    {
                        for (int j = 0; j < zdjecieWybraneRozkodowanie.Height; j++)
                        {
                            kolor = zdjecieWybraneRozkodowanie.GetPixel(i, j);

                            if (kolor.R % 2 != 0)
                            {
                                znakBinarnie += "1";
                            }
                            else
                            {
                                znakBinarnie += "0";
                            }
                            licznik++;

                            if (licznik == 8)
                            {
                                tekstRozkodowanyWBajtach[licznik2] = Convert.ToByte(znakBinarnie, 2);
                                licznik = 0;
                                licznik2++;
                                znakBinarnie = "";
                            }

                            if (kolor.G % 2 != 0)
                            {
                                znakBinarnie += "1";
                            }
                            else
                            {
                                znakBinarnie += "0";
                            }
                            licznik++;

                            if (licznik == 8)
                            {
                                tekstRozkodowanyWBajtach[licznik2] = Convert.ToByte(znakBinarnie, 2);
                                licznik = 0;
                                licznik2++;
                                znakBinarnie = "";
                            }

                            if (kolor.B % 2 != 0)
                            {
                                znakBinarnie += "1";
                            }
                            else
                            {
                                znakBinarnie += "0";
                            }
                            licznik++;

                            if (licznik == 8)
                            {
                                tekstRozkodowanyWBajtach[licznik2] = Convert.ToByte(znakBinarnie, 2);
                                licznik = 0;
                                licznik2++;
                                znakBinarnie = "";
                            }
                        }
                    }
                    break;

            }
            





            rozkodowanyTekst = utf8.GetString(tekstRozkodowanyWBajtach);
            tekstRozkodowany.Content = "";

            if (rozkodowanyTekst.IndexOf(koniecTekstu) != -1)
            {
                tekstRozkodowany.Content = rozkodowanyTekst.Substring(0, rozkodowanyTekst.IndexOf(koniecTekstu));
            }
            else
            {
                tekstRozkodowany.Content = rozkodowanyTekst;
            }
        }

        private void comboBoxIleKolorowRozkodowanie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxItemRozkodowanie = (ComboBoxItem)comboBoxIleKolorowRozkodowanie.SelectedItem;
            if (comboBoxItemRozkodowanie.Content != null)
            {
                wybranyKolorRozkodowanie = comboBoxItemRozkodowanie.Content.ToString();
            }
            else
            {
                wybranyKolorRozkodowanie = "R";
            }
            
        }
    }

}