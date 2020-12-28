using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        int secondselapsed;
        int matchesfound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();      
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondselapsed++;

            timeTextBlock.Text = (secondselapsed / 10F).ToString("0.0s");
            if (matchesfound == 8) {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " -  Play again?";
            }

        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐒","🐒",
                "🐅","🐅",
                "🦄","🦄",
                "🐔","🐔",
                "🦓","🦓",
                "🦊","🦊",
                "🐘","🐘",
                "🐇","🐇"
            };

            Random random = new Random();

   
                foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
                {
                if (textBlock.Name != "timeTextBlock")
                    {


                        int index = random.Next(animalEmoji.Count);
                        string nextEmoji = animalEmoji[index];
                        textBlock.Text = nextEmoji;
                        animalEmoji.RemoveAt(index);
                    textBlock.Visibility = Visibility.Visible;
                }
                }

            timer.Start();
            secondselapsed = 0;
            matchesfound = 0;

        }

        TextBlock lastTextBoxClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (findingMatch == false) {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBoxClicked = textBlock;
                findingMatch = true;
            } else if (textBlock.Text == lastTextBoxClicked.Text) {
                matchesfound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else {

                lastTextBoxClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesfound == 8) {
                SetUpGame();
            }

        }
    }
}
