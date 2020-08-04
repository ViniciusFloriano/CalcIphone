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

namespace Calculadora
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private double Evaluate(string expression)
        {
            expression = expression.Replace(",", ".");
            expression = expression.Replace("÷", "/");
            expression = expression.Replace("×", "*");
            try
            {
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("expression", string.Empty.GetType(), expression);
                System.Data.DataRow row = table.NewRow();
                table.Rows.Add(row);
                return double.Parse((string)row["expression"]);
            }
            catch
            {
                return double.Parse("0");
            }
        }

        private void Limpa()
        {
            Style style = this.Resources["RoundedButtonStyleEsp"] as Style;
            Operator1.Style = style;
            Operator2.Style = style;
            Operator3.Style = style;
            Operator4.Style = style;
        }

        bool vaiMudar = false;
        string aux = "";

        private void Ac_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text = "0";
            txtResultado.FontSize = 65;
            aux = "";
            Limpa();
        }

        private void Num_Click(object sender, RoutedEventArgs e)
        {
            Button number = sender as Button;
            if (txtResultado.Text.Length < 11)
            {
                if(txtResultado.Text.Length > 6)
                {
                    txtResultado.FontSize -= 5;
                }
                txtResultado.Text = txtResultado.Text == "0" || vaiMudar ? double.Parse(number.Content.ToString()).ToString("#,0.########") : double.Parse(txtResultado.Text + number.Content.ToString()).ToString("#,0.########");
                vaiMudar = false;
            }
            Limpa();
        }

        private void MaisMenos_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text = (double.Parse(txtResultado.Text) * -1).ToString("#,0.########");
        }

        private void PorCem_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text = (double.Parse(txtResultado.Text) / 100).ToString("#,0.########");
            vaiMudar = true;
        }

        private void Virgula_Click(object sender, RoutedEventArgs e)
        {
            if(!txtResultado.Text.Contains(','))
                txtResultado.Text += ",";
        }
        
        private void Operador_Click(object sender, RoutedEventArgs e)
        {
            Limpa();
            Button operador = sender as Button;
            txtResultado.Text = Evaluate(aux + txtResultado.Text).ToString("#,0.########");
            aux = txtResultado.Text + operador.Content.ToString();
            vaiMudar = true;
            Style style = this.Resources["RoundedButtonStyleEspActive"] as Style;
            operador.Style = style;
        }

        private void Igual_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text = Evaluate(aux + txtResultado.Text).ToString("#,0.########");
            vaiMudar = true;
            aux = "";
            Limpa();
        }
    }
}
