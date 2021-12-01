using System;
using System.Windows;
using System.Windows.Input;

using OpenTK.Wpf;
using OpenTK.Graphics.OpenGL;
using RaceGame.Wpf.Client.ClientState;

namespace RaceGame.Wpf.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //GameEngine gameEngine;
        private readonly IClientStateService _clientStateService = new ClientStateService();

        public MainWindow()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings();
            settings.MajorVersion = 4;
            settings.MinorVersion = 0;
            OpenTKControl.Start(settings);

            // init game
            //_clientStateService = new ClientStateService();
            // gameEngine = new GameEngine(); 
        }

        private void OpenTKControl_Ready()
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // подключает игрока к серверу, получает текущее состояние игры.
            _clientStateService.ConnectClient();
            // gameEngine.Start(new Vector2((float)Width, (float)Height)); 
        }

        private void OpenTKControl_Render(TimeSpan obj)
        {
            GL.ClearColor(System.Drawing.Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, 0d, 1d);

            // вызывается раз в n момент времени
            // обновляет состояние игры - получает текущее состояние игровых объектов
            //_clientStateService.Update();
            // отрисовывает игровые объекты
            //_clientStateService.Draw();

            // gameEngine.Update();
            // gameEngine.Draw();
        }

        private void glWpfControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.W || e.Key == Key.S || e.Key == Key.D)
            {
                // обновляет состояние игры (временно)
                _clientStateService.Update();

                // обновляет состояние игрока по нажатой клавище

                // отрисовывает игровые объекты
                _clientStateService.Draw();
            }
        }
    }
}
