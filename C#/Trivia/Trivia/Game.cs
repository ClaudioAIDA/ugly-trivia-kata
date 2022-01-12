using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();

        private readonly List<string> board;

        private readonly Quiz quiz;

        private Player _currentPlayer;
        private int currentPlayerIndex = 0;

        public Game()
        {
            quiz = new Quiz();

            board = new List<string>
            {
                "Pop", "Science", "Sports", "Rock", "Pop", "Science", "Sports", "Rock", "Pop", "Science", "Sports",
                "Rock",
            };
        }

        public bool Add(string playerName)
        {
            _players.Add(new Player(playerName));
            _currentPlayer ??= _players.First();

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " +HowManyPlayers());
            return true;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_currentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_currentPlayer.InPenaltyBox)
            {
                _currentPlayer.ValidateRollToGetOutOfPenaltyBox(roll);
                if (!_currentPlayer.IsGettingOutPenaltyBox)
                {
                    Console.WriteLine(_currentPlayer.Name + " is not getting out of the penalty box");
                    return;
                }

                Console.WriteLine(_currentPlayer.Name + " is getting out of the penalty box");
            }

            _currentPlayer.MovePlayer(roll);

            Console.WriteLine(_currentPlayer.Name
                              + "'s new location is "
                              + _currentPlayer.Place);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void AskQuestion()
        {
            var category = CurrentCategory();
            var question = quiz.NextQuestion(category);
            Console.WriteLine(question);
        }

        private string CurrentCategory()
        {
            return board[_currentPlayer.Place];
        }

        public bool WasCorrectlyAnswered()
        {
            if (_currentPlayer.InPenaltyBox)
            {
                if (!_currentPlayer.IsGettingOutPenaltyBox)
                {
                    NextPlayer();
                    return true;
                }
                Console.WriteLine("Answer was correct!!!!");
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
            }

            _currentPlayer.WinQuesito();
            Console.WriteLine(_currentPlayer.Name
                              + " now has "
                              + _currentPlayer.Quesitos
                              + " Gold Coins.");

            var winner = _currentPlayer.DidWin();
            NextPlayer();

            return winner;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_currentPlayer.Name + " was sent to the penalty box");
            _currentPlayer.SendToPenaltyBox();

            NextPlayer();
            return true;
        }

        private void NextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % HowManyPlayers();
            _currentPlayer = _players[currentPlayerIndex];
        }
    }
}
