﻿using SparkValueBackend.Models;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SparkValueBackend.Commands
{
    public class UpdateBreadboardWireCommand : CommandBase
    {
        private readonly BreadboardViewModel _breadboard;

        public UpdateBreadboardWireCommand(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public override void Execute(object? parameter)
        {
            if (parameter != null && parameter is WireModel)
            {
                WireModel wire = (WireModel)parameter;
                _breadboard.PlacedWires.Add(wire);   
                MessageBox.Show($"Wire was drawn from {wire.StartPositionToString()} to {wire.EndPositionToString()}\nPowered: {wire.IsPowered}\nGrounded: {wire.IsGounded}");
            }
        }
    }
}