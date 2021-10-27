using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORM_SUM
{
    public partial class Form1 : Form
    {
        SOAPRequest objOne = new SOAPRequest();
        SOAPRequest objTwo = new SOAPRequest();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {
                var reqParams = new NameValueCollection();
                string address = "http://localhost:/Simplex.asmx";

                //reqParams.Add("x", firstParam);
                //reqParams.Add("y", secondParam);

                var response = webClient.UploadValues(address, reqParams);
                string str = Encoding.UTF8.GetString(response);
                MessageBox.Show(str);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            objOne.numberInt = int.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            objOne.numberFloat = float.Parse(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            objOne.str = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            objTwo.numberInt = int.Parse(textBox4.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            objTwo.numberFloat = float.Parse(textBox5.Text);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            objTwo.str = textBox6.Text;
        }

    }
}
