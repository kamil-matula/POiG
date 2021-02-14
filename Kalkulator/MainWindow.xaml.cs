// Kamil Matula, gr. D, 10.03.2020, "Kalkulator"

using System;
using System.Collections.Generic;
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

namespace Kalkulator
{
    public partial class MainWindow : Window
    {
        private string actualNumber = "", memoryString = "", previouslyClicked = "Number", btnContent = "";

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D1 || e.Key == Key.NumPad1) buttonClicked(D1, null);
            if (e.Key == Key.D2 || e.Key == Key.NumPad2) buttonClicked(D2, null);
            if (e.Key == Key.D3 || e.Key == Key.NumPad3) buttonClicked(D3, null);
            if (e.Key == Key.D4 || e.Key == Key.NumPad4) buttonClicked(D4, null);
            if (e.Key == Key.D5 || e.Key == Key.NumPad5) buttonClicked(D5, null);
            if (e.Key == Key.D6 || e.Key == Key.NumPad6) buttonClicked(D6, null);
            if (e.Key == Key.D7 || e.Key == Key.NumPad7) buttonClicked(D7, null);
            if (e.Key == Key.D8 || e.Key == Key.NumPad8) buttonClicked(D8, null);
            if (e.Key == Key.D9 || e.Key == Key.NumPad9) buttonClicked(D9, null);
            if (e.Key == Key.D0 || e.Key == Key.NumPad0) buttonClicked(D0, null);
            if (e.Key == Key.Add || e.Key == Key.OemPlus) buttonClicked(Add, null);
            if (e.Key == Key.Subtract || e.Key == Key.OemMinus) buttonClicked(Subtract, null);
            if (e.Key == Key.Multiply) buttonClicked(Multiply, null);   //przydałoby się dodać obsługę 8 z shiftem, ale nie wiem jak
            if (e.Key == Key.Divide || e.Key == Key.OemQuestion) buttonClicked(Divide, null);
            if (e.Key == Key.Enter) buttonClicked(Equal, null);
            if (e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Decimal) buttonClicked(Comma, null);
            if (e.Key == Key.Back) buttonClicked(Backspace, null);
            if (e.Key == Key.Q) buttonClicked(CE, null);
            if (e.Key == Key.W) buttonClicked(C, null);
            if (e.Key == Key.Escape) Close();
        }

        public MainWindow()
        {
            InitializeComponent();
            actualBlock_blck.Text = "0";
        }

        private void buttonClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btnContent = btn.Content.ToString();
            if (btnContent == "0" || btnContent == "1" || btnContent == "2" || btnContent == "3" || btnContent == "4" ||
                btnContent == "5" || btnContent == "6" || btnContent == "7" || btnContent == "8" || btnContent == "9")
            {
                if (previouslyClicked == "Number") // jeśli wcześniej wciśnięto liczbę, dopisujemy liczbę (bez 0 na początku)
                {
                    if (actualNumber.Length < 12)
                    {
                        if (actualNumber == "0") actualNumber = btnContent;
                        else actualNumber += btnContent;
                    }
                }
                else if (previouslyClicked == "Operator") actualNumber = btnContent; // jeśli operator, zmieniamy aktualną
                else if (previouslyClicked == "Equal") // jeśli znak równa się, usuwamy memory i zmieniamy aktualną
                {
                    actualNumber = btnContent;
                    memoryString = "";
                    memoryBlock_blck.Text = "";
                }
                actualBlock_blck.Text = actualNumber;
                previouslyClicked = "Number";
            }
            else if (btnContent == "+" || btnContent == "-" || btnContent == "×" || btnContent == "÷")
            {
                if (previouslyClicked == "Number") // jeśli wcześniej wciśnięto liczbę, do memory wpisujemy aktualną + znak
                {
                    if (actualNumber.Length > 0) //usuwa przecinek z końca
                        if (actualNumber[actualNumber.Length - 1] == ',')
                        {
                            actualNumber = actualNumber.Remove(actualNumber.Length - 1);
                            actualBlock_blck.Text = actualNumber;
                        }
                    if (actualNumber == "") memoryString += "0 " + btnContent + " ";
                    else 
                    {
                        memoryString += actualNumber + " " + btnContent + " ";
                        if (memoryString.Length > 5)  //sumowanie na bieżąco
                        {
                            actualNumber = calculate(memoryString);
                            actualBlock_blck.Text = actualNumber;
                        }
                    }
                }
                else if (previouslyClicked == "Operator") // jeśli operator, zmieniamy go w memory
                {
                    memoryString = memoryString.Remove(memoryString.Length - 3);
                    memoryString += " " + btnContent + " ";
                }
                else if (previouslyClicked == "Equal")
                {
                    memoryString = actualNumber + " " + btnContent + " ";
                }
                memoryBlock_blck.Text = memoryString;
                previouslyClicked = "Operator";
            }
            else if (btnContent == "C")  // czyszczenie wszystkiego
            {
                actualBlock_blck.Text = "0";
                actualNumber = "";
                memoryBlock_blck.Text = "";
                memoryString = "";
                previouslyClicked = "Number";
            }
            else if (btnContent == "CE") // czyszczenie wprowadzanej liczby
            {
                actualBlock_blck.Text = "0";
                actualNumber = "0";
                previouslyClicked = "Equal";
            }
            else if (btnContent == "⌫") // usuwanie ostatniej cyfry
            {
                if (actualNumber != "" && previouslyClicked == "Number")
                {
                    actualNumber = actualNumber.Remove(actualNumber.Length - 1);
                    if (actualNumber != "") actualBlock_blck.Text = actualNumber;
                    else actualBlock_blck.Text = "0";
                }
                else if (previouslyClicked == "Equal")
                {
                    memoryString = "";
                    memoryBlock_blck.Text = "";
                }
            }
            else if (btnContent == ",") // wstawianie przecinka
            {
                if (previouslyClicked == "Number")
                {
                    if (actualNumber == "") actualNumber = "0,";  // jeśli zaczyna się od 0
                    else if (actualNumber.Contains(",") == false) actualNumber += ","; // jeśli nie zaczyna się od 0 i nie ma przecinka
                }
                else if (previouslyClicked == "Operator") actualNumber = "0,";
                else if (previouslyClicked == "Equal")
                {
                    memoryString = "";
                    memoryBlock_blck.Text = memoryString;
                    actualNumber = "0,";
                }
                actualBlock_blck.Text = actualNumber;
                previouslyClicked = "Number";
            }
            else if (btnContent == "=")
            {
                if (actualNumber.Length > 0) //usuwa przecinek z końca
                    if (actualNumber[actualNumber.Length - 1] == ',') actualNumber = actualNumber.Remove(actualNumber.Length - 1);
                if (actualNumber == "") actualNumber = "0"; // zapewnia wykonywanie działań na pustym stringu
                if (memoryString.Length > 0 && actualNumber == "0" && memoryString[memoryString.Length - 2] == '÷' && previouslyClicked == "Number") //blokada dzielenia przez zero
                {
                    actualBlock_blck.Text = "Nie dziel przez 0!";
                    actualNumber = "";
                    memoryString = "";
                    previouslyClicked = "Number";
                }
                else
                {
                    if (previouslyClicked == "Number" || previouslyClicked == "Operator") memoryString += actualNumber + " = ";
                    else if (previouslyClicked == "Equal" && memoryString.Split(' ').Length > 3)
                    {
                        string[] data = memoryString.Split(' ');
                        memoryString = actualNumber + " " + data[data.Length - 4] + " " + data[data.Length - 3] + " = ";
                    } 
                    actualNumber = calculate(memoryString);
                    actualBlock_blck.Text = actualNumber;
                    previouslyClicked = "Equal";
                }
                memoryBlock_blck.Text = memoryString;
            }
            else if (btnContent == "+/-")
            {
                if (actualNumber != "" && actualNumber != "0")
                {
                    if (actualNumber.Contains("-") == true) actualNumber = actualNumber.Substring(1, actualNumber.Length - 1); // usuwa minus
                    else actualNumber = "-" + actualNumber; // wstawia minus
                    actualBlock_blck.Text = actualNumber;
                }
            }
        }

        private string calculate(string memory) // Ta metoda będzie zwracała takie wyniki jak windowsowy kalkulator
        // - nie dba o kolejnośc wykonywania działań (wymagane jest zastosowanie ONP, jeśli ma być to poprawne)
        {
            string[] data = memory.Split(' '); double result = double.Parse(data[0]);
            for (int i = 1; i < data.Length - 2; i += 2)
            {
                if (data[i] == "+") result += double.Parse(data[i + 1]);
                else if (data[i] == "-") result -= double.Parse(data[i + 1]);
                else if (data[i] == "×") result *= double.Parse(data[i + 1]);
                else if (data[i] == "÷") result /= double.Parse(data[i + 1]);
            }
            return result.ToString(); //dobrze by było też zaokrąglić, bo czasem wychodzi poza zakres
        }
    }
}