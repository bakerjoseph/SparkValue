using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SparkValueBackend.Models
{
    public class LanguageModel
    {
        public string Language { get; }

        public BitmapImage CountryFlag { get; }

        public LanguageModel(string language, string imageSource)
        {
            Language = language;
            CountryFlag = new BitmapImage(new Uri($"pack://application:,,,{imageSource}"));
        }
    }
}
