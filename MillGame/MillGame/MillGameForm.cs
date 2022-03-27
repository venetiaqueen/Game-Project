///////////////////////////////////////////////////////////////////////
// 
// Purpose: This program uses a game data structure to implement
// a game of Who Wants to be a Millionaire!
//
///////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MillGame
{
    public partial class MillGameForm : Form
    {
        //initializes game and timeLeft
        GamePlay G = new GamePlay();
        int timeLeft;

        //Form Constructor
        public MillGameForm()
        {
            InitializeComponent();
        }

        //Name: MillGameForm_Load
        //Purpose: Loads form and initializes data structure with values from input file
        // and resets buttons and labels
        private void MillGameForm_Load(object sender, EventArgs e)
        {
            //Refreshes form variables
            this.Refresh();

            //Open file and send it to data structure
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open text file"
            };
            string input = "file.txt";
            if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                input = openFileDialog1.FileName;
            }
            else
            { 
                MessageBox.Show("Could not read file.");
            }
            G.GamePlayMake(input);

            //Sets game instructions
            Instruction1Label.Text = "Answer questions correctly to advance\nthe rounds and win BIG $$$!" +
                "\nYou have 4 lifelines: use them wisely!";
            Instruction2Label.Text = "Click Next to display each question.";

            //Resets answer buttons to blank and disables them
            Answer1Button.Text = "";
            Answer1Button.Enabled = false;
            Answer2Button.Text = "";
            Answer2Button.Enabled = false;
            Answer3Button.Text = "";
            Answer3Button.Enabled = false;
            Answer4Button.Text = "";
            Answer4Button.Enabled = false;

            //Sets initial questions label text
            QuestionLabel.Text = "Press Next to start game.";

            //Re-enables all lifelines 
            FiftyFiftyPB.Enabled = true;
            WalkAwayPB.Enabled = true;
            PhoneFriendPB.Enabled = true;
            PollAudiencePB.Enabled = true;
            NextButton.Enabled = true;
        }

        //Name: WalkAwayPB_Click
        //Purpose: When this is clicked, a message box will display safehaven
        // winnings and reloads game
        private void WalkAwayPB_Click(object sender, EventArgs e)
        {
            //Sets winnings based on current round and last reached safehaven
            string winnings;
            if (G.Round() >= 9)
                winnings = "$32,000";
            else if (G.Round() >= 4)
                winnings = "$1,000";
            else
                winnings = "$100";

            //Displays winnings to message box
            MessageBox.Show("You won " + winnings + " dollars!\nThanks for playing!");
            
            //Calls form load method to reload game
            MillGameForm_Load(sender, e);
        }

        //Name: FiftyFiftyPB_Click
        //Purpose: When this is clicked, two answer buttons are highlighted,
        // one being the correct answer, and the other a random answer
        private void FiftyFiftyPB_Click(object sender, EventArgs e)
        {
            //Initialize and declare answer strings
            string right, random;
            right = G.FiftyFiftyRight();
            random = G.FiftyFiftyRand();

            //Find right answer button and highlight it
            if (Answer1Button.Text == right)
                Answer1Button.BackColor = System.Drawing.Color.Orange;
            else if (Answer2Button.Text == right)
                Answer2Button.BackColor = System.Drawing.Color.Orange;
            else if (Answer3Button.Text == right)
                Answer3Button.BackColor = System.Drawing.Color.Orange;
            else if (Answer4Button.Text == right)
                Answer4Button.BackColor = System.Drawing.Color.Orange;
            else;

            //Find other answer button and highlight it
            if (Answer1Button.Text == random)
                Answer1Button.BackColor = System.Drawing.Color.Orange;
            else if (Answer2Button.Text == random)
                Answer2Button.BackColor = System.Drawing.Color.Orange;
            else if (Answer3Button.Text == random)
                Answer3Button.BackColor = System.Drawing.Color.Orange;
            else if (Answer4Button.Text == random)
                Answer4Button.BackColor = System.Drawing.Color.Orange;
            else;

            //Disable button for the rest of the game
            FiftyFiftyPB.Enabled = false;
        }

        //Name: PhoneFriendPB_Click
        //Purpose: When this is clicked, a message box displays a randomly
        // chosen answer
        private void PhoneFriendPB_Click(object sender, EventArgs e)
        {
            //Displays friends choice to message box
            MessageBox.Show("Your friend says that you should choose:\n" + G.PhoneFriend());
            
            //Disable button for the rest of the game
            PhoneFriendPB.Enabled = false;
        }

        //Name: PollAudiencePB_Click
        //Purpose: When this is clicked, a message box diplays random
        // percentages for random answers
        private void PollAudiencePB_Click(object sender, EventArgs e)
        {
            //Calls poll audience method from game data structure
            G.PollAudience();
            
            //initializes poll to the empty string
            string poll = "";
            
            //Set poll to a random percentage and answer
            for (int j = 0; j < 4; j++)
            {
                poll += G.PollPerc() + "% of the audience says " + G.PollAns() + " is the correct answer. \n";
            }

            //Displays poll string in message box
            MessageBox.Show(poll);

            //Disables button for the rest of the game
            PollAudiencePB.Enabled = false;
        }

        //Name: Answer1Button_Click
        //Purpose: When this is clicked, the back color of the button changes 
        // if it is the correct answer, or it ends the game.
        private void Answer1Button_Click(object sender, EventArgs e)
        {
            //Evaluates whether the answer in this label matches the correct
            //answer provided in the game data structure
            if (G.EvaluateAns(Answer1Button.Text))
            {
                //Change back color to green if correct
                Answer1Button.BackColor = System.Drawing.Color.Green;
                
                //Disables answer buttons
                Answer1Button.Enabled = false;
                Answer2Button.Enabled = false;
                Answer3Button.Enabled = false;
                Answer4Button.Enabled = false;
            }
            else
            {
                //Displays wrong answer announcement in message box
                MessageBox.Show("Sorry! Wrong Answer! Game over.");

                //Calls form load method to reload game
                MillGameForm_Load(sender, e);
            }

            //Enables Next button
            NextButton.Enabled = true;
        }

        //Name: Answer1Button_Click
        //Purpose: When this is clicked, the back color of the button changes 
        // if it is the correct answer, or it ends the game.
        private void Answer2Button_Click(object sender, EventArgs e)
        {
            //Evaluates whether the answer in this label matches the correct
            //answer provided in the game data structure
            if (G.EvaluateAns(Answer2Button.Text))
            {
                //Change back color to green if correct
                Answer2Button.BackColor = System.Drawing.Color.Green;
                
                //Disables answer buttons
                Answer1Button.Enabled = false;
                Answer2Button.Enabled = false;
                Answer3Button.Enabled = false;
                Answer4Button.Enabled = false;
            }
            else
            {
                //Displays wrong answer announcement in message box
                MessageBox.Show("Sorry! Wrong Answer! Game over.");

                //Calls form load method to reload game
                MillGameForm_Load(sender, e);
            }

            //Enables Next button
            NextButton.Enabled = true;
        }

        //Name: Answer1Button_Click
        //Purpose: When this is clicked, the back color of the button changes 
        // if it is the correct answer, or it ends the game.
        private void Answer3Button_Click(object sender, EventArgs e)
        {
            //Evaluates whether the answer in this label matches the correct
            //answer provided in the game data structure
            if (G.EvaluateAns(Answer3Button.Text))
            {
                //Change back color to green if correct
                Answer3Button.BackColor = System.Drawing.Color.Green;

                //Disables answer buttons
                Answer1Button.Enabled = false;
                Answer2Button.Enabled = false;
                Answer3Button.Enabled = false;
                Answer4Button.Enabled = false;
            }
            else
            {
                //Displays wrong answer announcement in message box
                MessageBox.Show("Sorry! Wrong Answer! Game over.");

                //Calls form load method to reload game
                MillGameForm_Load(sender, e);
            }

            //Enables Next button
            NextButton.Enabled = true;
        }

        //Name: Answer1Button_Click
        //Purpose: When this is clicked, the back color of the button changes 
        // if it is the correct answer, or it ends the game.
        private void Answer4Button_Click(object sender, EventArgs e)
        {
            //Evaluates whether the answer in this label matches the correct
            //answer provided in the game data structure
            if (G.EvaluateAns(Answer4Button.Text))
            {
                //Change back color to green if correct
                Answer4Button.BackColor = System.Drawing.Color.Green;

                //Disable answer buttons
                Answer1Button.Enabled = false;
                Answer2Button.Enabled = false;
                Answer3Button.Enabled = false;
                Answer4Button.Enabled = false;
            }
            else
            {
                //Displays wrong answer announcement in message box
                MessageBox.Show("Sorry! Wrong Answer! Game over.");

                //Calls form load method to reload game
                MillGameForm_Load(sender, e);
            }

            //Enables Next button
            NextButton.Enabled = true;
        }

        //Name: NextButton_Click
        //Purpose: When this is clicked, new questions and answers will be displayed
        // and answer buttons enabled. Round labels will change back color
        // depending on the round. Timer is started and countdown begins. If final
        // round is reached and completed, game is won and restarted.
        private void NextButton_Click(object sender, EventArgs e)
        {
            //If round is less that 14, display next question to label
            //and answers to buttons and enable buttons. Otherwise,
            //end game and display that the user won in message box
            if (G.Round() < 14)
            {
                QuestionLabel.Text = G.GetQuestion();
                QuestionLabel.BackColor = Color.Transparent;
                Answer1Button.Text = G.GetAnswer();
                Answer1Button.Enabled = true;
                Answer1Button.BackColor = Color.Transparent;
                Answer2Button.Text = G.GetAnswer();
                Answer2Button.Enabled = true;
                Answer2Button.BackColor = Color.Transparent;
                Answer3Button.Text = G.GetAnswer();
                Answer3Button.Enabled = true;
                Answer3Button.BackColor = Color.Transparent;
                Answer4Button.Text = G.GetAnswer();
                Answer4Button.Enabled = true;
                Answer4Button.BackColor = Color.Transparent;
            }
            else
            {
                //Display that user won in message box
                MessageBox.Show("You won!\nCongratulations!");

                //Calls form load method to reload game
                MillGameForm_Load(sender, e);
            }

            //start timer and reset timeLeft to 30 seconds
            CountdownTimer.Start();
            timeLeft = 30;

            //reset round labels
            Round1Label.BackColor = Color.Transparent;
            Round2Label.BackColor = Color.Transparent; 
            Round3Label.BackColor = Color.Transparent;
            Round4Label.BackColor = Color.Transparent;
            Round5Label.BackColor = Color.Transparent;
            Round6Label.BackColor = Color.Transparent;
            Round7Label.BackColor = Color.Transparent;
            Round8Label.BackColor = Color.Transparent;
            Round9Label.BackColor = Color.Transparent;
            Round10Label.BackColor = Color.Transparent;
            Round11Label.BackColor = Color.Transparent;
            Round12Label.BackColor = Color.Transparent;
            Round13Label.BackColor = Color.Transparent;
            Round14Label.BackColor = Color.Transparent;
            Round15Label.BackColor = Color.Transparent;
            
            //Highlight round labels depending on round
            if (G.Round() == 0)
            {
                Round1Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 1)
            {
                Round2Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 2)
            {
                Round3Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 3)
            {
                Round4Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 4)
            {
                Round5Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 5)
            {
                Round6Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 6)
            {
                Round7Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 7)
            {
                Round8Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 8)
            {
                Round9Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 9)
            {
                Round10Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 10)
            {
                Round11Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 11)
            {
                Round12Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 12)
            {
                Round13Label.BackColor = Color.DarkRed;
            }
            else if (G.Round() == 13)
            {
                Round14Label.BackColor = Color.DarkRed;
            }
            else 
            {
                Round15Label.BackColor = Color.DarkRed;
            }
            
            //Disable Next button
            NextButton.Enabled = false;
        }

        //Name: CountdownTimer_Tick
        //Purpose: Decrements timer from 30 to 0 seconds, then stops 
        //timer and ends game.
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            //If time is greater than 0, decrement timer and display
            //time left to label
            if(timeLeft > 0)
            {
                CountDown.Text = "00:" + timeLeft.ToString();
                timeLeft--;
            }
            else
            {
                //timeLeft = 0, stop the timer
                CountdownTimer.Stop();

                //Display to user that they ran out of time in message box
                MessageBox.Show("You ran out of time!\nTry thinking faster! :)");

                //Calls form load method to reload game
                MillGameForm_Load(sender, e);
            }
        }
    }
}
