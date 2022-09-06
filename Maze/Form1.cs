using System.Drawing;
using System.Windows.Forms;

namespace Maze
{
    public partial class Form1 : Form
    {
        Labirint l;
        public Form1()
        {
            InitializeComponent();
            Options();
            StartGame();
            ShowStatusStrip();
        }

        public void Options()
        {
            Text = "Maze";

            BackColor = Color.FromArgb(255, 92, 118, 137);

            int sizeX = 40;
            int sizeY = 20;

            Width = sizeX * 16 + 16;
            Height = sizeY * 16 + 40;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartGame() {
            l = new Labirint(this, 40, 20);
            l.Show();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                l.CharMove(1, 0);
            }
            else if (e.KeyCode == Keys.Left)
            {
                l.CharMove(-1, 0);
            }
            else if (e.KeyCode == Keys.Up)
            {
                l.CharMove(0, -1);
            }
            else if (e.KeyCode == Keys.Down)
            {
                l.CharMove(0, 1);
            }
        }

        public void ShowStatusStrip()
        {
            if(l == null)
            {
                return;
            }
            foreach (var item in statusStrip1.Items)
            {
                if ((item as ToolStripStatusLabel).Name == "label")
                {
                    (item as ToolStripStatusLabel).Text = $"Health: {l.health}% | Medals left: {l.countOfMedals}";
                }
            }
        }
    }
}
