namespace PasswdGen
{
    public partial class Form1 : Form
    {
        public string length = "";
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
            length = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox1.Text = PasswdGen();
            else if (checkBox2.Checked)
                textBox1.Text= PasswdGen(Int32.Parse(length));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private string PasswdGen(int value = 30)
        {
            char[] import = File.ReadAllText("C:\\Work\\PasswdGen\\chars.txt").ToCharArray();
            List<char> charList = new List<char>();

            for (int i = 0; i < value; i++)
                charList.Add(import[i]);

            string output = string.Join("",charList.ToArray());
            return output;
        }
    }
}