﻿using System.Windows.Controls;

namespace FacePalm.ViewModel {
    public class CanvasVm {
        public delegate void ScaleChangedHandler(CanvasVm c);

        private double _scale = 1.0;

        public CanvasVm(Canvas c) => Canvas = c;

        public Canvas Canvas { get; }

        public double Scale {
            get => _scale;
            set {
                if (value.Equals(_scale)) return;
                _scale = value;
                ScaleChanged?.Invoke(this);
            }
        }

        public void ZoomIn() => _scale += 0.125;

        public void ZoomOut() => _scale -= 0.125;

        public event ScaleChangedHandler ScaleChanged;

        public void Add(PointVm p) {
            Canvas.Children.Add(p.Marker.Path);
            Canvas.Children.Add(p.Marker.Label);
            p.RedrawRequired += ShowPoint;
            ScaleChanged += c => c.ShowPoint(p);
        }

        public void ShowPoint(PointVm p) {
            if (!p.IsVisible) return;
            p.Rescale(Scale);
        }
    }
}