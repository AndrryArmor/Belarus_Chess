﻿using BelarusChess.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Xaml.Behaviors;
using EventTrigger = Microsoft.Xaml.Behaviors.EventTrigger;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;
using System.Runtime.Remoting.Channels;

namespace BelarusChess.UI.ViewModels
{
    public class PieceViewModel : BoardImageViewModelBase
    {
        public PieceViewModel(Piece piece) : base()
        {
            Piece = piece;

            Image.Source = new BitmapImage(new Uri(GetPieceSpriteUriString(Piece), UriKind.Relative));
            Image.Margin = new Thickness(LeftMargin + Piece.Cell.Col * CellEdge, TopMargin + Piece.Cell.Row * CellEdge, 0, 0);
            Panel.SetZIndex(Image, 2);

            Piece.OnCellChange += Piece_OnCellChange;
        }

        public Piece Piece { get; set; }

        private string GetPieceSpriteUriString(Piece piece)
        {
            return "/UI;Component/Resources/PiecesSprites/" + GetPieceName(piece) + ".png";
        }

        private string GetPieceName(Piece piece)
        {
            return Enum.GetName(piece.Color.GetType(), piece.Color) + " " + 
                Enum.GetName(piece.Type.GetType(), piece.Type).ToLower();
        }

        private void Piece_OnCellChange(object sender, Cell e)
        {
            if (e != null)
            {
                Image.Margin = new Thickness(LeftMargin + e.Col * CellEdge, TopMargin + e.Row * CellEdge, 0, 0);
                Image.Visibility = Visibility.Visible;
            }
            else
                Image.Visibility = Visibility.Collapsed;
        }
    }
}