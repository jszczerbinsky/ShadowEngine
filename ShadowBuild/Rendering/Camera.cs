﻿using ShadowBuild.Objects;
using System.Collections.Generic;
using System.Windows;

namespace ShadowBuild.Rendering
{
    public class Camera : _2DobjectResizable
    {
        public static Camera Default;
        public static SizeMode DefaultSizeMode;

        public readonly List<Layer> RenderLayers = new List<Layer>() { Layer.Default };
        public System.Drawing.Color Background = System.Drawing.Color.Gray;

        public Camera(Point position, Point size)
        {
            this.Position = position;
            this.Size = size;
        }
        public Camera(double x, double y, double width, double height)
        {
            this.Position = new Point(x, y);
            this.Size = new Point(width, height);
        }
        public Camera(Point position, Point size, List<Layer> layers)
        {
            this.Position = position;
            this.Size = size;
            this.RenderLayers = layers;
        }

        public bool IsRendering(Layer l)
        {
            foreach (Layer l1 in RenderLayers)
            {
                if (l1 == l) return true;
            }
            return false;
        }
    }
}
