using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORM_WSDL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox1.Text);
            int y = int.Parse(textBox2.Text);

            var service = GetSimplexService();

            var result = service.Add(x, y);
            MessageBox.Show("Add result - " + result);
        }

        public static Simplex GetSimplexService()
        {
            var service = new Simplex();
            service.Url = "http://localhost:63964/Simplex.asmx";
            service.Timeout = 100 * 1000;
            return service;
        }
    }
}
