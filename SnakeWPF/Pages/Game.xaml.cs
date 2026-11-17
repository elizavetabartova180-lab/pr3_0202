using Common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnakeWPF.Pages
{
    public partial class Game : Page
    {
        public int StepCadr = 0;

        public Game()
        {
            InitializeComponent();
        }

        public void CreateUI()
        {
            if (MainWindow.mainWindow == null || MainWindow.mainWindow.ViewModelGames == null)
                return;

            Dispatcher.Invoke(() =>
            {
                if (StepCadr == 0) StepCadr = 1;
                else StepCadr = 0;

                canvas.Children.Clear();

                var snakesPlayers = MainWindow.mainWindow.ViewModelGames.SnakesPlayers;
                if (snakesPlayers == null || snakesPlayers.Points == null) return;

                for (int iPoint = snakesPlayers.Points.Count - 1; iPoint >= 0; iPoint--)
                {
                    Snakes.Point SnakePoint = snakesPlayers.Points[iPoint];

                    if (iPoint != 0)
                    {
                        Snakes.Point NextSnakePoint = snakesPlayers.Points[iPoint - 1];

                        if (SnakePoint.X > NextSnakePoint.X || SnakePoint.X < NextSnakePoint.X)
                        {
                            if (iPoint % 2 == 0)
                            {
                                if (StepCadr % 2 == 0) SnakePoint.Y -= 1;
                                else SnakePoint.Y += 1;
                            }
                            else
                            {
                                if (StepCadr % 2 == 0) SnakePoint.Y += 1;
                                else SnakePoint.Y -= 1;
                            }
                        }
                        else if (SnakePoint.Y > NextSnakePoint.Y || SnakePoint.Y < NextSnakePoint.Y)
                        {
                            if (iPoint % 2 == 0)
                            {
                                if (StepCadr % 2 == 0) SnakePoint.X -= 1;
                                else SnakePoint.X += 1;
                            }
                            else
                            {
                                if (StepCadr % 2 == 0) SnakePoint.X += 1;
                                else SnakePoint.X -= 1;
                            }
                        }
                    }

                    Brush color;
                    if (iPoint == 0)
                        color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 127, 14));
                    else
                        color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 198, 19)); // ПРОБЕЛ УБРАН

                    Ellipse ellipse = new Ellipse()
                    {
                        Width = 20,
                        Height = 20,
                        Margin = new Thickness(SnakePoint.X - 10, SnakePoint.Y - 10, 0, 0),
                        Fill = color,
                        Stroke = Brushes.Black
                    };
                    canvas.Children.Add(ellipse);
                }

                ImageBrush myBrush = new ImageBrush();
                // ПРОБЕЛЫ В СТРОКЕ УБРАНЫ
                myBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/apple.png"));

                Ellipse points = new Ellipse()
                {
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(
                        MainWindow.mainWindow.ViewModelGames.Points.X - 20,
                        MainWindow.mainWindow.ViewModelGames.Points.Y - 20, 0, 0),
                    Fill = myBrush
                };
                canvas.Children.Add(points);
            });
        }
    }
}