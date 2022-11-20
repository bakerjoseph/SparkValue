using SparkValueBackend.ViewModels.LessonInteractiveElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SparkValueBackend.Commands
{
    public class ChangeResistorBandCommand : CommandBase
    {
        private readonly ResistorChartViewModel _resistorViewModel;

        public ChangeResistorBandCommand(ResistorChartViewModel resistorViewModel)
        {
            _resistorViewModel = resistorViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter != null && parameter is (int, Brush, string))
            {
                (int BandNum, Brush TargetColor, string BandValue) param = ((int, Brush, string))parameter;

                switch (param.BandNum)
                {
                    case 1:
                        _resistorViewModel.BandOne = param.TargetColor; 
                        _resistorViewModel.BandOneValue = param.BandValue;
                        break;
                    case 2:
                        _resistorViewModel.BandTwo = param.TargetColor;
                        _resistorViewModel.BandTwoValue = param.BandValue;
                        break;
                    case 3:
                        _resistorViewModel.BandThree = param.TargetColor;
                        _resistorViewModel.BandThreeValue = param.BandValue;
                        break;
                    case 4:
                        _resistorViewModel.BandFour = param.TargetColor;
                        _resistorViewModel.BandFourValue = param.BandValue;
                        break;
                    case 5:
                        _resistorViewModel.BandFive = param.TargetColor;
                        _resistorViewModel.BandFiveValue = param.BandValue;
                        break;
                    case 6:
                        _resistorViewModel.BandSix = param.TargetColor;
                        _resistorViewModel.BandSixValue = param.BandValue;
                        break;
                }
            }
        }
    }
}
