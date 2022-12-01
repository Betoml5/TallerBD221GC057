using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AgendaDigitaldDesconectado.Helpers
{
    public class IdAmigoToImageConverter
    {
        
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string rutaArchivo = "";
                if (value is int)
                {
                    int idimg = (int)value;
                    rutaArchivo = $"{AppDomain.CurrentDomain.BaseDirectory}imagenes//{idimg}.jpg";
                }
                else
                    rutaArchivo = (string)value;

                BitmapImage? bitMapImage = null;

                if (File.Exists(rutaArchivo))
                {
                    bitMapImage = new BitmapImage();
                    bitMapImage.BeginInit();
                    bitMapImage.StreamSource = new FileStream(rutaArchivo, FileMode.Open);
                    bitMapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitMapImage.EndInit();
                    bitMapImage.StreamSource.Dispose();
                }

                return bitMapImage;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
   
}
