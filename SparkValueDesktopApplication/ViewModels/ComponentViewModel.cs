using SparkValueDesktopApplication.Models.Components;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SparkValueDesktopApplication.ViewModels
{
    public class ComponentViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get 
            { 
                return _name; 
            }
            set 
            { 
                _name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private BitmapImage _picture;
        public BitmapImage Picture 
        { 
            get 
            { 
                return _picture; 
            } 
            set 
            { 
                _picture = value;
                OnPropertyChanged(nameof(Picture));
            } 
        }

        private Point _position;
        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        private IComponentModel _component;

        public ComponentViewModel(IComponentModel component)
        {
            _name = component.Name;
            _description = component.Description;
            _picture = component.Image;
            _component = component;
            _position = new Point();
        }

        public ComponentViewModel(IComponentModel component, Point position)
        {
            _name = component.Name;
            _description = component.Description;
            _picture = component.Image;
            _component = component;
            _position = position;
        }
    }
}