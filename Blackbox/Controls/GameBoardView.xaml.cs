using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Blackbox.Controls;

namespace Blackbox
{
    public partial class GameBoardView : UserControl
    {
        #region internal data
        private BoxView[,] _boxs = 
            new BoxView[BlackboxConfig.GameBoardRow, BlackboxConfig.GameBoardColumn];
        private List<MirrorView> _mirrors = new List<MirrorView>();
        private List<RaysView> _rays = new List<RaysView>();

        private GameBoardViewModel _model = new GameBoardViewModel();
        #endregion

        public GameBoardView()
        {
            InitializeComponent();

            _model.RaysCreated += 
                new EventHandler<RaysEventArgs>(RaysCreated);

            CreateBlackBoxes();
            CreateMirrors();
            CreateNookBoxes();
            CreateLightBoxes();
        }

        void RaysCreated(object sender, RaysEventArgs e)
        {
            RaysView raysView = new RaysView();
            e.Rays.SetObserver(raysView);
            raysView.DataContext = e.Rays;
            Binding binding = new Binding();
            binding.Source = e.Rays;
            binding.Path = new PropertyPath("LightSpot");
            binding.Mode = BindingMode.OneWay;
            raysView.SetBinding(RaysView.StateValueProperty, binding);
            this._rays.Add(raysView);
            this.LayoutRoot.Children.Add(raysView);
        }

        public void SetObserver(GamePage gamePage)
        {
            _model.ModelStateUpdated +=
                new EventHandler<ModelStateEventArgs>(gamePage.StateChanged);

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    _model.Boxes[row, column].ModelStateUpdated += 
                        new EventHandler<ModelStateEventArgs>(gamePage.StateChanged);
                }
            }
        }

        public void DoGuess()
        {
            _model.DoGuess();
        }

        public void Debunk()
        {
            _model.Debunk();
        }

        public void Restart()
        {
            RemoveRays();
            _model.Restart(); // it will create new Mirror-objects
            UpdateMirrorsLayout();
        }

        public void Accelerate()
        {
            for (int row = 0; row < BlackboxConfig.GameBoardRow; row++)
            {
                for (int column = 0; column < BlackboxConfig.GameBoardColumn; column++)
                {
                    if (((row == 0) || (row == BlackboxConfig.GameBoardRow - 1)) &&
                        ((column == 0) || (column == BlackboxConfig.GameBoardColumn - 1)))
                    {
                        NookBoxView nookbox = (NookBoxView)_boxs[row, column];
                        nookbox.Accelerate();
                    }
                }
            }
        }

        private void CreateNookBoxes()
        {
            for (int row = 0; row < BlackboxConfig.GameBoardRow; row++)
            {
                for (int column = 0; column < BlackboxConfig.GameBoardColumn; column++)
                {
                    if (((row == 0) || (row == BlackboxConfig.GameBoardRow - 1)) &&
                        ((column == 0) || (column == BlackboxConfig.GameBoardColumn - 1)))
                    {
                        _boxs[row, column] = new NookBoxView();
                        _boxs[row, column].HorizontalAlignment = HorizontalAlignment.Left;
                        _boxs[row, column].VerticalAlignment = VerticalAlignment.Top;
                        _boxs[row, column].Width = BlackboxConfig.BoxWidth;
                        _boxs[row, column].Height = BlackboxConfig.BoxHeight;
                        _boxs[row, column].Margin = new Thickness(row * BlackboxConfig.BoxWidth, 
                            column * BlackboxConfig.BoxHeight, 0, 0);
                        _boxs[row, column].Projection = new PlaneProjection();
                        _boxs[row, column].DataContext = _model.Boxes[row, column];
                        Binding binding = new Binding();
                        binding.Source = _model.Boxes[row, column];
                        binding.Path = new PropertyPath("BaseData");
                        binding.Mode = BindingMode.OneWay;
                        _boxs[row, column].image.Tap += new EventHandler<GestureEventArgs>(_model.OnTap);
                        this.LayoutRoot.Children.Add(_boxs[row, column]);
                    }
                }
            }
        }

        private void CreateBlackBoxes()
        {
            for (int row = 0; row < BlackboxConfig.GameBoardRow; row++)
            {
                for (int column = 0; column < BlackboxConfig.GameBoardColumn; column++)
                {
                    if ((row > 0) && 
                        (row < BlackboxConfig.GameBoardRow-1)&&
                        (column > 0) &&
                        (column < BlackboxConfig.GameBoardColumn - 1))
                    {
                        _boxs[row, column] = new BlackBoxView();
                        _boxs[row, column].HorizontalAlignment = HorizontalAlignment.Left;
                        _boxs[row, column].VerticalAlignment = VerticalAlignment.Top;
                        _boxs[row, column].Width = BlackboxConfig.BoxWidth;
                        _boxs[row, column].Height = BlackboxConfig.BoxHeight;
                        _boxs[row, column].Margin = new Thickness(row * BlackboxConfig.BoxWidth, 
                            column * BlackboxConfig.BoxHeight, 0, 0);
                        _boxs[row, column].Projection = new PlaneProjection();
                        _boxs[row, column].DataContext = _model.Boxes[row, column];
                        Binding binding = new Binding();
                        binding.Source = _model.Boxes[row, column];
                        binding.Path = new PropertyPath("BaseData");
                        binding.Mode = BindingMode.OneWay;
                        _boxs[row, column].SetBinding(BoxView.StateValueProperty, binding);
                        _boxs[row, column].image.Tap += new EventHandler<GestureEventArgs>(_model.OnTap);
                        _boxs[row, column].imageForeground.Tap += new EventHandler<GestureEventArgs>(_model.OnTap);
                        this.LayoutRoot.Children.Add(_boxs[row, column]);
                    }
                }
            }
        }

        private void CreateLightBoxes()
        {
            for (int row = 0; row < BlackboxConfig.GameBoardRow; row++)
            {
                for (int column = 0; column < BlackboxConfig.GameBoardColumn; column++)
                {
                    if (((row == 0) && (column > 0 && column < BlackboxConfig.GameBoardColumn - 1)) ||
                        ((row == BlackboxConfig.GameBoardRow - 1) && (column > 0 && column < BlackboxConfig.GameBoardColumn - 1)) ||
                        ((column == 0) && (row > 0 && row < BlackboxConfig.GameBoardRow - 1)) ||
                        ((column == BlackboxConfig.GameBoardColumn - 1) && (row > 0 && row < BlackboxConfig.GameBoardRow - 1)))
                    {
                        _boxs[row, column] = new LightBoxView();
                        _boxs[row, column].HorizontalAlignment = HorizontalAlignment.Left;
                        _boxs[row, column].VerticalAlignment = VerticalAlignment.Top;
                        _boxs[row, column].Width = BlackboxConfig.BoxWidth;
                        _boxs[row, column].Height = BlackboxConfig.BoxHeight;
                        _boxs[row, column].Margin = 
                            new Thickness(row * BlackboxConfig.BoxWidth, 
                                column * BlackboxConfig.BoxHeight, 0, 0);
                        _boxs[row, column].Projection = new PlaneProjection();
                        _boxs[row, column].DataContext = _model.Boxes[row, column];
                        Binding binding = new Binding();
                        binding.Source = _model.Boxes[row, column];
                        binding.Path = new PropertyPath("BaseData");
                        binding.Mode = BindingMode.OneWay;
                        _boxs[row, column].SetBinding(BoxView.StateValueProperty, binding);
                        _boxs[row, column].image.Tap += new EventHandler<GestureEventArgs>(_model.OnTap);
                        this.LayoutRoot.Children.Add(_boxs[row, column]);
                    }
                }
            }
        }

        private void CreateMirrors()
        {
            for (int i = 0; i < _model.Mirrors.Count; i++)
            {
                Mirror mirror = _model.Mirrors[i];
                MirrorView mirrorView = new MirrorView();
                mirrorView.HorizontalAlignment = HorizontalAlignment.Left;
                mirrorView.VerticalAlignment = VerticalAlignment.Top;
                mirrorView.Width = BlackboxConfig.BoxWidth;
                mirrorView.Height = BlackboxConfig.BoxHeight;
                mirrorView.Margin = 
                    new Thickness(mirror.Position.Row * BlackboxConfig.BoxWidth, 
                        mirror.Position.Column * BlackboxConfig.BoxHeight, 0, 0);
                mirrorView.DataContext = mirror;
                Binding binding = new Binding();
                binding.Source = mirror;
                binding.Path = new PropertyPath("Guessed");
                binding.Mode = BindingMode.OneWay;
                mirrorView.SetBinding(MirrorView.StateValueProperty, binding);
                this._mirrors.Add(mirrorView);
                this.LayoutRoot.Children.Add(mirrorView);
            }
        }

        private void UpdateMirrorsLayout()
        {
            foreach (MirrorView mirrorView in _mirrors)
            {
                Mirror mirror = (Mirror)mirrorView.DataContext;
                mirrorView.Margin = 
                    new Thickness(mirror.Position.Row * BlackboxConfig.BoxWidth, 
                        mirror.Position.Column * BlackboxConfig.BoxHeight, 0, 0);
            }
        }

        private void RemoveRays()
        {
            foreach (RaysView raysView in _rays)
            {
                this.LayoutRoot.Children.Remove(raysView);
            }
            _rays.Clear();
        }
    }
}
