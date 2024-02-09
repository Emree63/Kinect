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

        public Character(string imageUrl, double width, double height)
        {
            ImageUrl = imageUrl;
            Width = width;
            Height = height;
        }
    }
}
