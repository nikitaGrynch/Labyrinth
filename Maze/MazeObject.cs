using System;
using System.Drawing;

namespace Maze
{
    class MazeObject
    {
        public enum MazeObjectType { HALL, WALL, MEDAL, ENEMY, CHAR, PILL };

        public Bitmap[] images = {new Bitmap(@"C:\1\hall.png"),
            new Bitmap(@"C:\1\wall.png"),
            new Bitmap(@"C:\1\medal.png"),
            new Bitmap(@"C:\1\enemy.png"),
            new Bitmap(@"C:\1\player.png"),
            new Bitmap(@"C:\1\pill.png") };

        public MazeObjectType type;
        public int width;
        public int height;
        public Image texture;

        public MazeObject(MazeObjectType type)
        {
            this.type = type;
            width = 16;
            height = 16;
            texture = images[(int)type];
        }

    }
}
