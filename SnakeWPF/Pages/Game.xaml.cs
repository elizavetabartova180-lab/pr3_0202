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

                ImageBrush myBrush = new ImageBrush();
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

                if (MainWindow.mainWindow.ViewModelGames.OtherSnakes != null)
                {
                    foreach (var otherSnake in MainWindow.mainWindow.ViewModelGames.OtherSnakes)
                    {
                        if (otherSnake == null || otherSnake.Points == null) continue;

                        for (int iPoint = otherSnake.Points.Count - 1; iPoint >= 0; iPoint--)
                        {
                            Snakes.Point SnakePoint = otherSnake.Points[iPoint];
                            Ellipse ellipse = new Ellipse()
                            {
                                Width = 20,
                                Height = 20,
                                Margin = new Thickness(SnakePoint.X - 10, SnakePoint.Y - 10, 0, 0),
                                Fill = new SolidColorBrush(Color.FromArgb(255, 200, 50, 50)),
                                Stroke = Brushes.Black
                            };
                            canvas.Children.Add(ellipse);
                        }
                    }
                }

                var snakesPlayers = MainWindow.mainWindow.ViewModelGames.SnakesPlayers;
                if (snakesPlayers == null || snakesPlayers.Points == null) return;

                for (int iPoint = snakesPlayers.Points.Count - 1; iPoint >= 0; iPoint--)
                {
                    Snakes.Point SnakePoint = snakesPlayers.Points[iPoint];
                    double drawX = SnakePoint.X;
                    double drawY = SnakePoint.Y;

                    if (iPoint != 0)
                    {
                        Snakes.Point NextSnakePoint = snakesPlayers.Points[iPoint - 1];

                        if (drawX > NextSnakePoint.X || drawX < NextSnakePoint.X)
                        {
                            if (iPoint % 2 == 0)
                            {
                                if (StepCadr % 2 == 0) drawY -= 1;
                                else drawY += 1;
                            }
                            else
                            {
                                if (StepCadr % 2 == 0) drawY += 1;
                                else drawY -= 1;
                            }
                        }
                        else if (drawY > NextSnakePoint.Y || drawY < NextSnakePoint.Y)
                        {
                            if (iPoint % 2 == 0)
                            {
                                if (StepCadr % 2 == 0) drawX -= 1;
                                else drawX += 1;
                            }
                            else
                            {
                                if (StepCadr % 2 == 0) drawX += 1;
                                else drawX -= 1;
                            }
                        }
                    }

                    Brush color;
                    if (iPoint == 0)
                        color = new SolidColorBrush(Color.FromArgb(255, 0, 127, 14));
                    else
                        color = new SolidColorBrush(Color.FromArgb(255, 0, 198, 19));

                    Ellipse ellipse = new Ellipse()
                    {
                        Width = 20,
                        Height = 20,
                        Margin = new Thickness(drawX - 10, drawY - 10, 0, 0),
                        Fill = color,
                        Stroke = Brushes.Black
                    };
                    canvas.Children.Add(ellipse);
                }
            });
        }
    }
}