using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace myShootEmUp
{
    static class HitBox
    {
        public static bool Calculate(int TotalSizeX, int TotalSizeY, int TotalSizeSecondX, int TotalSizeSecondY, Vector2 Position, Vector2 ColPosition) //Denna metoden gör det enklare för att få en bra collision
        {
            Rectangle tempRect = new Rectangle((int)ColPosition.X, (int)ColPosition.Y, TotalSizeSecondX, TotalSizeSecondY); //Det skapas en rektangel längst upp till vänster av objektet och sträcker sig ut i storleken av objektet
            if (tempRect.Intersects(new Rectangle((int)Position.X, (int)Position.Y, TotalSizeX, TotalSizeY))) //Om den rektangeln kolliderar med en annan rektangel
            {
                return true; //Om den kolliderar, returnera true
            }
            return false; //Annars false
        }
    }
}
