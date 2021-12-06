using OpenTK.Wpf;
using OpenTK.Graphics.OpenGL;
using System;
using System.Windows;
using Color = System.Drawing.Color;
using RaceGame.Wpf.Client.ClientState;
using System.Windows.Input;

namespace RaceGameWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IClientStateService _clientStateService = new ClientStateService();

        public MainWindow()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings();
            settings.MajorVersion = 3;
            settings.MinorVersion = 6;
            OpenTKControl.Start(settings);
        }

        private void OpenTKControl_Ready()
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // подключает игрока к серверу, получает текущее состояние игры.
            _clientStateService.ConnectClient();
        }

        private void OpenTKControl_Render(TimeSpan obj)
        {
            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, 0d, 1d);

            // вызывается раз в n момент времени
            // обновляет состояние игры - получает текущее состояние игровых объектов
            //_clientStateService.Update();

            // отрисовывает игровые объекты
            _clientStateService.Update();
            _clientStateService.Draw();
        }

        private void glWpfControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.W || e.Key == Key.S || e.Key == Key.D)
            {
                // обновляет состояние игры через действие игрока - по пришедшей от клиента нажатой клавише
                _clientStateService.ClientAction(e.Key);
            }
        }
    }
}
