using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameEngine
{
    class Player
    {
        public int x, y, size, speed, images;
        public Image[] playerImages = new Image[1];



        public Player(int _x, int _y, int _size, int _speed, Image[] _playerImages)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            playerImages = _playerImages;

        }



        public void move(Player Pl, string direction)
        {
            if (direction == "Left")
            {
                Pl.x -= Pl.speed;
            }

            else if (direction == "Right")
            {
                Pl.x += Pl.speed;
            }

            else if (direction == "Up")
            {
                Pl.y -= Pl.speed;
            }

            else
            {
                Pl.y += Pl.speed;
            }
        }

        public bool collision (Player Pl, Enemy En)
        {
            Rectangle PlRec = new Rectangle(Pl.x, Pl.y, Pl.size, Pl.size);
            Rectangle EnRec = new Rectangle(En.x, En.y, En.size, En.size);
            if (PlRec.IntersectsWith(EnRec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


