///////////////////////////////////////////////////////////////////////
// 
// Purpose: This program is the game data structure for
// a game of Who Wants to be a Millionaire!
//
///////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;

namespace MillGame
{
    class GamePlay
    {

        string[] Questions = new string[75];
        int round;
        Random rand = new Random();
        bool a = false, b = false, c = false, d = false;
        Dictionary<string, string> poll = new Dictionary<string, string>();
        int index;
        //Default constructor
        public GamePlay()
        {
            for (int i = 0; i < Questions.Length; i++)
            {
                Questions[i] = "";
            }
            round = 0;
        }

        //Parameterized constructor
        public void GamePlayMake(string filename)  
        {
            int i = 0;
            string[] stream = System.IO.File.ReadAllLines(filename);
            do
            {
                Questions[i] = stream[i];
                i++;
            } while (i < 75);
            round = -1;
            index = 0;
        }

        //Gets round number
        public int Round() { return round; }

        //Name: GetQuestion
        //Returns: string, question from array
        //Parameters: none
        public string GetQuestion()
        {
            round++;
            return Questions[round * 5];
        }

        //Name: GetAnswer
        //Returns: string, random answer from array
        //Parameters: none
        public string GetAnswer()
        {
            int x;
            x = rand.Next(4);

            if (a && b && c && d)
            {
                a = b = c = d = false;
                return GetAnswer();
            }
            else
            {
                //Returns random answer
                if (x == 0 && !a)
                {
                    a = true;
                    return Questions[(round * 5) + 1];
                }
                else if (x == 1 && !b)
                {
                    b = true;
                    return Questions[(round * 5) + 2];
                }
                else if (x == 2 && !c)
                {
                    c = true;
                    return Questions[(round * 5) + 3];
                }
                else if (x == 3 && !d)
                {
                    d = true;
                    return Questions[(round * 5) + 4];
                }
                else
                    return GetAnswer();
            }
        }

        //Name: EvaluateAns
        //Returns: Boolean value on whether the selected answer is the right answer
        //Parameters: answer
        public bool EvaluateAns(string ans)
        {
            return ans == Questions[(round * 5) + 1] ? true : false;
        }

        //Name: FiftyFiftyRand
        //Returns: string, random non-right answer
        //Parameters: none
        public string FiftyFiftyRand()
        {
            int x;
            x = rand.Next(1, 4);

            //Returns random non-right answer
            if (x == 1)
                return Questions[(round * 5) + 2];
            else if (x == 2)
                return Questions[(round * 5) + 3];
            else
                return Questions[(round * 5) + 4];
        }

        //Name: FiftyFiftyRight
        //Returns: string, right answer
        //Parameters: none
        public string FiftyFiftyRight()
        {
            //returns right answer
            return Questions[(round * 5) + 1];
        }

        //Name: PhoneFriend
        //Returns: string, right answer
        //Parameters: none
        public string PhoneFriend()
        {
            int x;
            x = rand.Next(4);

            //Returns random answer
            if (x == 0)
                return Questions[(round * 5) + 1];
            else if (x == 1)
                return Questions[(round * 5) + 2];
            else if (x == 2)
                return Questions[(round * 5) + 3];
            else
                return Questions[(round * 5) + 4];
        }

        //Name: PollAudience
        //Returns: none, but changes map member data
        //Parameters: none
        public void PollAudience()
        {
            int x = 0, sum = 0;
            for (int i = 1; i <= 4; i++)
            {
                if (i < 4)
                {
                    x = rand.Next(100 - sum);
                    sum += x;
                }
                else
                    x = 100 - sum;
                poll.Add(Questions[(round * 5) + i], x.ToString());
            }
        }

        //Name: PollPerc
        //Returns: percentages in poll map, in ascending order
        //Parameters: none
        public string PollPerc()
        {
            if (index <= 4)
                index++;
            else
                index = 0;
            return poll[Questions[(round * 5) + index]];
        }

        //Name: PollAns
        //Returns: answers in poll map, in ascending order
        //Parameters: none
        public string PollAns()
        {
            return Questions[(round * 5) + index];
        }
    }
}
