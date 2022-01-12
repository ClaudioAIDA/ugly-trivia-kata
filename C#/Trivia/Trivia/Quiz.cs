using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Quiz
    {

        private readonly Dictionary<string, LinkedList<string>> questionsDictionary;

        public Quiz()
        {
            questionsDictionary = new Dictionary<string, LinkedList<string>>();
            questionsDictionary["Pop"] = new LinkedList<string>();
            questionsDictionary["Science"] = new LinkedList<string>();
            questionsDictionary["Sports"] = new LinkedList<string>();
            questionsDictionary["Rock"] = new LinkedList<string>();
            for (var i = 0; i < 50; i++)
            {
                questionsDictionary["Pop"].AddLast("Pop Question " + i);
                questionsDictionary["Science"].AddLast("Science Question " + i);
                questionsDictionary["Sports"].AddLast("Sports Question " + i);
                questionsDictionary["Rock"].AddLast("Rock Question " + i);
            }
        }

        public string NextQuestion(string category)
        {
            var question = questionsDictionary[category].First();
            questionsDictionary[category].RemoveFirst();
            return question;
        }
    }
}