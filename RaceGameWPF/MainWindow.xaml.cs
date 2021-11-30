﻿using OpenTK.Wpf;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Windows;
using RaceGameLibrary;
using Color = System.Drawing.Color;

namespace RaceGameWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameEngine gameEngine;
        public MainWindow()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings();
            settings.MajorVersion = 3;
            settings.MinorVersion = 6;
            OpenTKControl.Start(settings);
            gameEngine = new GameEngine();
        }

        private void OpenTKControl_Ready()
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            gameEngine.Start(new Vector2((float)Width, (float)Height));
        }

        private void OpenTKControl_Render(TimeSpan obj)
        {
            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, 0d, 1d);
            gameEngine.Update();
            gameEngine.Draw();
        }



    }
}
