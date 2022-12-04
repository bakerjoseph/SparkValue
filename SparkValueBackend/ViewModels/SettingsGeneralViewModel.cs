using SparkValueBackend.Models;
using SparkValueBackend.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SparkValueBackend.ViewModels
{
    public class SettingsGeneralViewModel : ViewModelBase
    {
        #region Language
        private readonly ObservableCollection<LanguageModel> languages;
        public IEnumerable<LanguageModel> Languages => languages;
        #endregion

        #region Font Size
        private readonly ObservableCollection<int> fontSize;
        public IEnumerable<int> FontSize => fontSize;

        private readonly int DefaultFontSize = 12;

        private int _selectedFontSize;
        public int SelectedFontSize
        {
            get { return _selectedFontSize; }
            set
            {
                _selectedFontSize = value;
                OnPropertyChanged(nameof(SelectedFontSize));
            }
        }
        #endregion

        #region Font Color
        private readonly ObservableCollection<SolidColorBrush> fontColor;
        public IEnumerable<SolidColorBrush> FontColor => fontColor;

        private readonly SolidColorBrush DefaultFontColor = Brushes.Black;

        private SolidColorBrush _selectedFontColor;
        public SolidColorBrush SelectedFontColor
        {
            get { return _selectedFontColor; }
            set
            {
                _selectedFontColor = value;
                OnPropertyChanged(nameof(SelectedFontColor));
            }
        }
        #endregion

        public SettingsGeneralViewModel(List<NavigationService> navigationServices)
        {
            languages = new ObservableCollection<LanguageModel>
            {
                new LanguageModel("American English", "/Images/AmericanFlag.png")
            };

            fontSize = new ObservableCollection<int>
            {
                12
            };

            SelectedFontSize = DefaultFontSize;

            fontColor = new ObservableCollection<SolidColorBrush>
            {
                Brushes.Black
            };

            SelectedFontColor = DefaultFontColor;
        }
    }
}
