using System.Security.Cryptography;

namespace PasswdGen
{
    public partial class Form1 : Form
    {
        public char[] import = File.ReadAllText("C:\\Work\\PasswdGen\\chars.txt").ToCharArray();
        public string passWd = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox2.AutoCheck = true;
                textBox2.BackColor = Color.LightGray;
                textBox2.Enabled = false;
                textBox2.Text = string.Empty;
            }
            else
                checkBox2.AutoCheck = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox1.AutoCheck = true;
                textBox2.BackColor = Color.White;
                textBox2.Enabled = true;
            }
            else
                checkBox1.AutoCheck = false;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            passWd = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Text = PasswdGen();
            }

            else if (checkBox2.Checked)
            {
                try
                {
                    textBox1.Text = PasswdGen(Int32.Parse(passWd));
                    if (textBox1.Text == "")
                    {
                        textBox1.Text = PasswdGen(import.Length);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }


            label2.Text = $"Passwd Length: {textBox1.Text.Length}";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private string PasswdGen(int value = 30)
        {
            Random rand = new Random();
            var scrambledList = import.ToList().OrderBy(x => rand.Next()).ToArray();

            List<char> newList = new List<char>();

            //var bytes = new byte[value];
            //string newPasswd = "";

            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(bytes,0,v);
            //    newPasswd = Convert.ToBase64String(bytes);
            //}
            try
            {
                for (int i = 0; i < value; i++)
                {
                    int r = rand.Next(1, value);
                    newList.Add(scrambledList[r]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Input length is too high. Max lenght is 107.\nSetting value to max length.");
                return "";
            }

            string newPasswd = string.Join("", newList.ToArray());
            newList.Clear();
            return newPasswd;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}