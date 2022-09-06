using System;
using System.Drawing;

namespace Maze
{
    class MazeObject
    {
        public enum MazeObjectType { HALL, WALL, MEDAL, ENEMY, CHAR, PILL };

        public Bitmap[] images = {
            Properties.Resources.hall,
            Properties.Resources.wall,
            Properties.Resources.medal,
            Properties.Resources.enemy,
            Properties.Resources.player,
            Properties.Resources.pill
        };

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
