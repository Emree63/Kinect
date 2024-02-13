using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MarioProject.Converters
{
    class BoolToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTrue = (bool)value;

            // Obtenez les noms d'images des paramètres
            string[] imageNames = (parameter as string)?.Split(',');

            if (imageNames != null && imageNames.Length == 2)
            {
                // Chargez les images
                BitmapImage image1 = new BitmapImage(new Uri($"/Images/{imageNames[0].Trim()}", UriKind.Relative));
                BitmapImage image2 = new BitmapImage(new Uri($"/Images/{imageNames[1].Trim()}", UriKind.Relative));

                // Retournez l'image appropriée en fonction de la valeur booléenne
                return isTrue ? image1 : image2;
            }

            // Si les noms d'images ne sont pas correctement fournis, retournez null
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
