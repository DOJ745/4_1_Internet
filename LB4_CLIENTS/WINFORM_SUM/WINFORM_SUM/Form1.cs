using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Flurl;
using Flurl.Http;

namespace WINFORM_SUM
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        SimpleClass objOne = new SimpleClass();
        SimpleClass objTwo = new SimpleClass();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            objOne.numberInt = Convert.ToInt32(textBox1.Text);
            objOne.numberFloat = float.Parse(textBox2.Text, CultureInfo.InvariantCulture.NumberFormat);
            objOne.str = textBox3.Text;

            objTwo.numberInt = Convert.ToInt32(textBox4.Text);
            objTwo.numberFloat = float.Parse(textBox5.Text, CultureInfo.InvariantCulture.NumberFormat);
            objTwo.str = textBox6.Text;

            var url = new Url("http://localhost:63964/Simplex.asmx").WithHeader("Content-Type", "text/xml");

            var responseString = await url.PostStringAsync(objOne.formSOAP(objTwo)).ReceiveString();

            MessageBox.Show(responseString.ToString());

            /*var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

            var responseString = await response.Content.ReadAsStringAsync();*/


            /*WebRequest req = WebRequest.Create("http://localhost:63964/Simplex.asmx");
            req.Method = "POST";
            string data = objOne.formSOAP(objTwo);
            byte[] byteArray = Encoding.UTF8.GetBytes(data);

            req.ContentType = "text/xml";
            req.ContentLength = byteArray.Length;

            Stream sendStream = req.GetRequestStream();
            sendStream.Write(byteArray, 0, byteArray.Length);
            sendStream.Close();

            using (Stream dataStream = req.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse res = req.GetResponse();
            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    MessageBox.Show(reader.ReadToEnd());
                }
            }
            res.Close();*/


            /*using (var webClient = new WebClient())
            {
                var reqParams = new NameValueCollection();
                string address = "http://localhost:63964/Simplex.asmx";

                var response = webClient.UploadValues(address, reqParams);
                string str = Encoding.UTF8.GetString(response);
                MessageBox.Show(str);
            }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //objOne.numberInt = int.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //objOne.numberFloat = float.Parse(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //objOne.str = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //objTwo.numberInt = int.Parse(textBox4.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //objTwo.numberFloat = float.Parse(textBox5.Text);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //objTwo.str = textBox6.Text;
        }

    }
}
