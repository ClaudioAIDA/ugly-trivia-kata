using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();

        private readonly List<string> board;

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private Player _currentPlayer;
        private int currentPlayerIndex = 0;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }

            board = new List<string>
            {
                "Pop", "Science", "Sports", "Rock", "Pop", "Science", "Sports", "Rock", "Pop", "Science", "Sports",
                "Rock",
            };
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
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
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_currentPlayer.Name + " is getting out of the penalty box");
                    _currentPlayer.MovePlayer(roll);

                    Console.WriteLine(_currentPlayer.Name
                                      + "'s new location is "
                                      + _currentPlayer.Place);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_currentPlayer.Name + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _currentPlayer.MovePlayer(roll);

                Console.WriteLine(_currentPlayer.Name
                        + "'s new location is "
                        + _currentPlayer.Place);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            return board[_currentPlayer.Place];
        }

        public bool WasCorrectlyAnswered()
        {
            if (_currentPlayer.InPenaltyBox)
            {
                if (!_isGettingOutOfPenaltyBox)
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
            _currentPlayer.InPenaltyBox = true;

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
