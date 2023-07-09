using System.Security.Cryptography;

namespace PasswdGen
{
    public partial class Form1 : Form
    {
        //==== Importing file with alphanumerics and special characters
        public char[] import = File.ReadAllText("C:\\Work\\PasswdGen\\chars.txt").ToCharArray();

        //When entering size of password, the number is stored in passWd below
        public string passWd = "";

        public Form1()
        {
            InitializeComponent();
        }

        //=== Goal here was to 1. not be able to uncheck a box and 2. two boxes should not be checked at the same time.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox2.AutoCheck = true;
                textBox2.BackColor = Color.LightGray;  //Also when checkbox2 is checked the textbox beside it is accessible, else it is not and gray.
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

        //=== User input (length) goes into textbox2
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            passWd = textBox2.Text;
        }


        //=== I decided that standard length should be 30, checkbox1 will generate a password with length of 30.
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
                    // If user enters wrong input, error message will pop up.
                    // If user enters too high number, error message will pop up and length will be set to highest possible and password will be generated.
                    textBox1.Text = PasswdGen(Int32.Parse(passWd));
                    if (textBox1.Text == "")
                        textBox1.Text = PasswdGen(import.Length);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            // This was mostly for me to keep track of password length when debugging
            // But it shows user the length of the password generated.
            // This can come in handy if using bytes to generate password.
            label2.Text = $"Passwd Length: {textBox1.Text.Length}";
        }

        private string PasswdGen(int value = 30)
        {
            Random rand = new Random();

            //Scrambling the imported characters
            var scrambledList = import.ToList().OrderBy(x => rand.Next()).ToArray();

            //new password will be stored in this list
            List<char> newList = new List<char>();

            //====== Tested other forms of generating random characters in array
            //====== Might go back to it later
            //var bytes = new byte[value];
            //string newPasswd = "";

            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(bytes,0,v);
            //    newPasswd = Convert.ToBase64String(bytes);
            //}

            // error handling of indexOutOfBound exception
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
    }
}