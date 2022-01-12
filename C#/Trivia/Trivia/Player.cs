using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            Place = 0;
            Quesitos = 0;
            InPenaltyBox = false;
        }

        public string Name { get; }
        public int Place { get; private set; }
        public int Quesitos { get; private set; }
        public bool InPenaltyBox { get; private set; }
        public bool IsGettingOutPenaltyBox { get; private set; }

        public void MovePlayer(int roll)
        {
            Place = (Place + roll) % 12;
        }

        public void WinQuesito()
        {
            Quesitos++;
        }

        public bool DidWin()
        {
            return Quesitos != 6;;
        }

        public void ValidateRollToGetOutOfPenaltyBox(int roll)
        {
            IsGettingOutPenaltyBox = roll % 2 != 0;
        }

        public void SendToPenaltyBox()
        {
            IsGettingOutPenaltyBox = false;
            InPenaltyBox = true;
        }
    }
}
