using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioProject.Models
{
    public class Character
    {
        public string ImageUrl { get; }
        public double Width { get; }
        public double Height { get; }
        public string WeaponImage { get; }
        public double WidthWeapon { get; }
        public double HeightWeapon { get; }

        public Character(string imageUrl, double width, double height, string weaponImage, double widthWeapon, double heightWeapon)
        {
            ImageUrl = imageUrl;
            Width = width;
            Height = height;
            WeaponImage = weaponImage;
            WidthWeapon = widthWeapon;
            HeightWeapon = heightWeapon;
        }
    }
}
