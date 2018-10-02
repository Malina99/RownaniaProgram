using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RownaniaProgram
{
    public partial class MainWindow : Window
    {
        int startIndex = 0;
        int endIndex = 0;
        List<char> sep = new List<char> { '+', '-', '/', '*', '=' };



        private string ZnajdzX(string operation, int startIndex, ref int firstIndex, ref int secondIndex, List<char> separatory)
        {
            string x = "";
            firstIndex = 0;
            secondIndex = 0;

            for (int i = startIndex; i > 0; i--)
            {
                foreach (var item in separatory)
                {
                    if (operation[i] == item)
                    {
                        firstIndex = i;
                    }
                }
                if (firstIndex != 0)
                {
                    break;
                }

            }
            for (int i = startIndex; i < operation.Length; i++)
            {
                foreach (var item in separatory)
                {
                    if (operation[i] == item)
                    {
                        secondIndex = i;
                    }
                }
                if (secondIndex != 0)
                {
                    break;
                }
            }

            x = (operation.Substring(firstIndex, secondIndex - firstIndex)).ToString();
            return x;
        }

        private string ZnajdzLiczbeZ(string operation, ref int startIndex, ref int secondIndex, List<char> separatory, char znak)
        {
            startIndex = 0;
            secondIndex = 0;
            startIndex = operation.IndexOf(znak);
            for (int i = startIndex + 1; i < operation.Length; i++)
            {
                foreach (var item in separatory)
                {
                    if (operation[i] == item)
                    {
                        secondIndex = i;
                    }
                }
                if (secondIndex != 0)
                {
                    break;
                }
            }
            if (secondIndex == 0)
            {
                secondIndex = operation.Length;
            }
            return operation.Substring(startIndex, secondIndex - startIndex);
        }

        private string IloscX(List<char> separatory, string operation)
        {
            int end = 0;
            int index = operation.IndexOf('x');
            for (int i = index; i > 0; i--)
            {
                foreach (var item in separatory)
                {
                    if (operation[i] == item)
                    {
                        end = i + 1;
                    }
                }
                if (end != 0)
                {
                    break;
                }
            }
            string wynik;
            try
            {
                wynik = operation.Substring(end - 1, operation.IndexOf('x') - end + 1);
            }
            catch
            {
                wynik = operation.Substring(end, operation.IndexOf('x') - end);
            }
            try
            {
                if (wynik[0] == '/' || wynik[0] == '*')
                {
                    wynik = wynik.Substring(1);
                }
            }
            catch
            {
                wynik = "1";
            }


            return wynik;
        }

        string MnozenieX(string operation, List<char> separatory)
        {
            int znakIndex = 0;
            int za = 0;
            int przed = 0;
            int tempIndex = 0;
            string tempString = "";
            string x;

            do
            {
                znakIndex = operation.IndexOf('x');
                przed = 0;

                for (int i = znakIndex - 1; i > 0; i--)
                {
                    if (operation[i].Equals('*'))
                    {
                        przed = i;
                        break;
                    }
                    else if (operation[i].Equals('/') || operation[i].Equals('-') || operation[i].Equals('+'))
                    {
                        break;
                    }
                }
                if (przed != 0)
                {

                    for (int i = przed - 1; i > 0; i--)
                    {
                        foreach (var item in separatory)
                        {
                            if (operation[i].Equals(item))
                            {
                                tempIndex = i;
                            }
                        }
                        if (tempIndex != 0)
                        {
                            break;
                        }
                    }
                    tempString = operation.Substring(tempIndex, przed - tempIndex);
                    char tempZnak = ' ';
                    foreach (var item in separatory)
                    {
                        if (tempString[0].Equals(item))
                        {
                            tempString = tempString.Substring(1);
                            tempZnak = item;
                        }
                    }
                    double a;
                    try
                    {
                        a = Convert.ToDouble(IloscX(sep, operation));
                    }
                    catch
                    {
                        a = 1;
                    }
                    
                    double b = Convert.ToDouble(tempString);
                    x = Convert.ToString(a * b) + "x";
                    string xtemp = ZnajdzX(operation, operation.IndexOf('x'), ref startIndex, ref endIndex, sep);
                    operation = operation.Substring(0, tempIndex) + tempZnak + x + operation.Substring(endIndex);
                    tempIndex = 0;
                }

            } while (przed != 0);


            do
            {
                za = 0;
                for (int i = znakIndex; i < operation.Length; i++)
                {
                    if (operation[i].Equals('*'))
                    {
                        za = i;
                        break;
                    }
                    else if (operation[i].Equals('/') || operation[i].Equals('-') || operation[i].Equals('+'))
                    {
                        break;
                    }
                }
                if (za != 0)
                {
                    za++;
                    for (int i = za; i < operation.Length; i++)
                    {
                        foreach (var item in separatory)
                        {
                            if (operation[i] == item)
                            {
                                tempIndex = i;
                            }
                        }
                        if (tempIndex != 0)
                        {
                            break;
                        }
                    }
                    if (tempIndex == 0)
                    {
                        tempIndex = operation.Length;
                    }
                    tempString = operation.Substring(za, tempIndex - za);
                    double a;
                    try
                    {
                        a = Convert.ToDouble(IloscX(sep, operation));
                    }
                    catch
                    {
                        a = 1;
                    }
                    double b = Convert.ToDouble(tempString);
                    x = Convert.ToString(a * b) + "x";
                    operation = operation.Substring(0, startIndex) + x + operation.Substring(tempIndex);
                    tempIndex = 0;


                }
            } while (za != 0);



            return operation;





        }

        string DzielenieX(string operation, List<char> separatory)
        {
            int znakIndex = 0;
            int za = 0;
            int przed = 0;
            int tempIndex = 0;
            string tempString = "";
            string x;

            do
            {
                znakIndex = operation.IndexOf('x');
                przed = 0;

                for (int i = znakIndex - 1; i > 0; i--)
                {
                    if (operation[i].Equals('/'))
                    {
                        przed = i;
                        break;
                    }
                    else if (operation[i].Equals('*') || operation[i].Equals('-') || operation[i].Equals('+'))
                    {
                        break;
                    }
                }
                if (przed != 0)
                {

                    for (int i = przed - 1; i > 0; i--)
                    {
                        foreach (var item in separatory)
                        {
                            if (operation[i].Equals(item))
                            {
                                tempIndex = i;
                            }
                        }
                        if (tempIndex != 0)
                        {
                            break;
                        }
                    }
                    tempString = operation.Substring(tempIndex, przed - tempIndex);
                    char tempZnak = ' ';
                    foreach (var item in separatory)
                    {
                        if (tempString[0].Equals(item))
                        {
                            tempString = tempString.Substring(1);
                            tempZnak = item;
                        }
                    }
                    double a = Convert.ToDouble(IloscX(sep, operation));
                    double b = Convert.ToDouble(tempString);
                    x = Convert.ToString(b / a) + "x";
                    string xtemp = ZnajdzX(operation, operation.IndexOf('x'), ref startIndex, ref endIndex, sep);
                    operation = operation.Substring(0, tempIndex) + tempZnak + x + operation.Substring(endIndex);
                    tempIndex = 0;
                }

            } while (przed != 0);


            do
            {
                za = 0;
                for (int i = znakIndex; i < operation.Length; i++)
                {
                    if (operation[i].Equals('/'))
                    {
                        za = i;
                        break;
                    }
                    else if (operation[i].Equals('*') || operation[i].Equals('-') || operation[i].Equals('+'))
                    {
                        break;
                    }
                }
                if (za != 0)
                {
                    za++;
                    for (int i = za; i < operation.Length; i++)
                    {
                        foreach (var item in separatory)
                        {
                            if (operation[i] == item)
                            {
                                tempIndex = i;
                            }
                        }
                        if (tempIndex != 0)
                        {
                            break;
                        }
                    }
                    if (tempIndex == 0)
                    {
                        tempIndex = operation.Length;
                    }
                    tempString = operation.Substring(za, tempIndex - za);
                    x = Convert.ToString(Convert.ToDouble(IloscX(sep, operation)) / Convert.ToDouble(tempString)) + "x";
                    operation = operation.Substring(0, startIndex) + x + operation.Substring(tempIndex);
                    tempIndex = 0;


                }
            } while (za != 0);



            return operation;





        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void liczBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            string operation = rownanieTextBox.Text;
            string temp;
            string x = ZnajdzX(operation, operation.IndexOf('x'), ref startIndex, ref endIndex, sep);

            operation = MnozenieX(operation, sep);

            operation = DzielenieX(operation, sep);




            x = ZnajdzX(operation, operation.IndexOf('x'), ref startIndex, ref endIndex, sep);
            temp = operation.Substring(0, startIndex) + operation.Substring(endIndex);
            string lewa = temp.Substring(0, temp.IndexOf('='));
            string prawa = temp.Substring(temp.IndexOf('=') + 1);
            string iloscX = IloscX(sep, operation);
            DataTable dt = new DataTable();
            var result = dt.Compute(lewa, "");
            if (result == DBNull.Value)
            {
                result = 0;
            }
            var result2 = dt.Compute(prawa, "");
            if (result2 == DBNull.Value)
            {
                result2 = 0;
            }
            temp = Convert.ToString((Convert.ToDouble(result2) - Convert.ToDouble(result)));
            string wynik = x + " = " + temp;
            try
            {
                temp = Convert.ToString(Convert.ToDouble(temp) / (Convert.ToDouble(iloscX)));
                wynik = "x = " + temp;
            }
            catch
            {
                wynik = "x = " + temp;
            }

            wynikLab.Content = wynik;
            }
            catch
            {
                MessageBox.Show("Błąd zapisu!");
            }





        }
    }
}
