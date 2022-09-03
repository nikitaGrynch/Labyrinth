using System;
using System.Windows.Forms;
using System.Drawing;

namespace Maze
{
    class Labirint
    {
        public int height; // высота лабиринта (количество строк)
        public int width; // ширина лабиринта (количество столбцов в каждой строке)

        public MazeObject[,] maze;
        public PictureBox[,] images;

        public static Random r = new Random();
        public Form parent;

        private int prevCharPosX;
        private int prevCharPosY;

        private int countOfMedals;

        private int health;

        private Point finish;

        public Labirint(Form parent, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.parent = parent;
            this.countOfMedals = 0;
            this.health = 100;

            maze = new MazeObject[height, width];
            images = new PictureBox[height, width];

            Generate();
            ShowHeader();
        }

        private void Generate()
        {
            int smileX = 0;
            int smileY = 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    MazeObject.MazeObjectType current = MazeObject.MazeObjectType.HALL;

                    // в 1 случае из 5 - ставим стену
                    if (r.Next(5) == 0)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }

                    // в 1 случае из 100 - кладём денежку
                    if (r.Next(100) == 0)
                    {
                        current = MazeObject.MazeObjectType.MEDAL;
                        this.countOfMedals++;
                    }

                    // в 1 случае из 100 - размещаем врага
                    if (r.Next(100) == 0)
                    {
                        current = MazeObject.MazeObjectType.ENEMY;
                    }

                    // в 1 случае из 150 - размещаем таблетку
                    if (r.Next(150) == 0)
                    {
                        current = MazeObject.MazeObjectType.PILL;
                    }

                    // стены по периметру обязательны
                    if (y == 0 || x == 0 || y == height - 1 | x == width - 1)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }

                    // наш персонажик
                    if (x == smileX && y == smileY)
                    {
                        current = MazeObject.MazeObjectType.CHAR;
                        prevCharPosX = x;
                        prevCharPosY = y;
                    }

                    // есть выход, и соседняя ячейка справа всегда свободна
                    if (x == smileX + 1 && y == smileY || x == width - 1 && y == height - 3)
                    {
                        current = MazeObject.MazeObjectType.HALL;
                        this.finish = new Point(x, y);

                    }
                    
                    maze[y, x] = new MazeObject(current);
                    images[y, x] = new PictureBox();
                    images[y, x].Location = new Point(x * maze[y, x].width, y * maze[y, x].height);
                    images[y, x].Parent = parent;
                    images[y, x].Width = maze[y, x].width;
                    images[y, x].Height = maze[y, x].height;
                    images[y, x].BackgroundImage = maze[y, x].texture;
                    images[y, x].Visible = false;
                }
            }
        }

        public void Show()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    images[y, x].Visible = true;
                }
            }
        }

        public void CharMove(int x, int y)
        {
            if(prevCharPosY + y < 0 || prevCharPosX + x > this.width || prevCharPosY + y > this.height || prevCharPosX + x < 0)
            {
                return;
            }
            // Wall
            if(maze[prevCharPosY + y, prevCharPosX + x].type == MazeObject.MazeObjectType.WALL)
            {
                return;
            }
            // Enenmy
            else if (maze[prevCharPosY + y, prevCharPosX + x].type == MazeObject.MazeObjectType.ENEMY)
            {
                this.health -= 25;
                ShowHeader();
            }
            // Medal
            else if (maze[prevCharPosY + y, prevCharPosX + x].type == MazeObject.MazeObjectType.MEDAL)
            {
                this.countOfMedals--;
                ShowHeader();
            }
            // Pill
            else if (maze[prevCharPosY + y, prevCharPosX + x].type == MazeObject.MazeObjectType.PILL)
            {
                this.health += this.health == 100? 0 : 5;
                ShowHeader();
            }
            maze[prevCharPosY, prevCharPosX] = new MazeObject(MazeObject.MazeObjectType.HALL);
            images[prevCharPosY, prevCharPosX].Location = new Point(prevCharPosX * maze[prevCharPosY, prevCharPosX].width, prevCharPosY * maze[prevCharPosY, prevCharPosX].height);
            images[prevCharPosY, prevCharPosX].Parent = parent;
            images[prevCharPosY, prevCharPosX].Width = maze[prevCharPosY, prevCharPosX].width;
            images[prevCharPosY, prevCharPosX].Height = maze[prevCharPosY, prevCharPosX].height;
            images[prevCharPosY, prevCharPosX].BackgroundImage = maze[prevCharPosY, prevCharPosX].texture;
            images[prevCharPosY, prevCharPosX].Visible = false;
            
            maze[prevCharPosY + y, prevCharPosX + x] = new MazeObject(MazeObject.MazeObjectType.CHAR);
            images[prevCharPosY + y, prevCharPosX + x].Location = new Point((prevCharPosX + x) * maze[prevCharPosY + y, prevCharPosX + x].width, (prevCharPosY + y) * maze[prevCharPosY + y, prevCharPosX + x].height);
            images[prevCharPosY + y, prevCharPosX + x].Parent = parent;
            images[prevCharPosY + y, prevCharPosX + x].Width = maze[prevCharPosY + y, prevCharPosX + x].width;
            images[prevCharPosY + y, prevCharPosX + x].Height = maze[prevCharPosY + y, prevCharPosX + x].height;
            images[prevCharPosY + y, prevCharPosX + x].BackgroundImage = maze[prevCharPosY + y, prevCharPosX + x].texture;
            images[prevCharPosY + y, prevCharPosX + x].Visible = true;

            // victory or lose condition
            if(this.countOfMedals == 0)
            {
                MessageBox.Show("All medals collected", "You won!");
                Application.Exit();
            }
            else if(this.health == 0)
            {
                MessageBox.Show("Your health is over", "You lose!");
                Application.Exit();
            }
            else if(prevCharPosX + x == finish.X && prevCharPosY + y == finish.Y)
            {
                MessageBox.Show("You found a way out", "You won!");
                Application.Exit();
            }

            this.prevCharPosX += x;
            this.prevCharPosY += y;
        }

        private void ShowHeader()
        {
            this.parent.Text = $"Health: {this.health}% | Medals left: {countOfMedals}";
        }
    }
}
