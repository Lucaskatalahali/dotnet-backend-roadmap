namespace TranslationGame
{
    internal class Program
    {

        static bool AskQuestion(List<string> options, string englishWord, string correctAnswer)
        {
            options = options.Where(answer => answer != correctAnswer)
                .OrderBy(_ => Random.Shared.Next())
                .Take(3)
                .ToList();

            options.Add(correctAnswer);
            options = options.OrderBy(_=> Random.Shared.Next()).ToList();
            Console.WriteLine($"Translate this word: {englishWord}\n");

            for(int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i+1} - { options[i]}");
            }

            int answer = ReadAnswer();

            return options[answer - 1] == correctAnswer;
        }

        static int ReadAnswer()
        {
            while (true)
            {
                Console.Write($"\nAnswer: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int answer) && answer >= 1 && answer <= 4)
                {
                    return answer;
                }
                Console.WriteLine("Invalid input. Try again...");
            }
        }
        static void Main(string[] args)
        {
            int score = 0;
            Dictionary<string, string> dictionary = new()
            {
                ["House"] = "Casa",
                ["Car"] = "Carro",
                ["Person"] = "Pessoa",
                ["Life"] = "Vida",
                ["Computer"] = "Computador",
                ["Woman"] = "Mulher",
                ["Book"] = "Livro",
                ["Pen"] = "Caneta",
                ["Pencil"] = "Lápis",
                ["Monkey"] = "Macaco",
                ["School"] = "Escola",
                ["Window"] = "Janela",
                ["Food"] = "Comida",
                ["City"] = "Cidade",
                ["Music"] = "Música"
            };

            List<string> portugueseWords = dictionary.Values.ToList();
            var questions = dictionary.OrderBy(_ => Random.Shared.Next()).ToList();

            int totalQuestions = questions.Count;
            for(int i = 0; i < totalQuestions; i++)
            {
                Console.WriteLine($"Question {i + 1}/{totalQuestions}");
                Console.WriteLine($"Score: {score} pts");

                bool isCorrect = AskQuestion(portugueseWords, questions[i].Key, questions[i].Value);

                if (isCorrect)
                {
                    score++;
                    Console.WriteLine("\nCorrect!");
                }
                else
                {
                    Console.WriteLine("\nWrong!");
                    Console.WriteLine($"Correct answer: {questions[i].Value}\n");
                }
                Console.Write("\nPress any key to continue");
                Console.ReadKey(true);
                Console.Clear();
            }

            Console.WriteLine($"Game Finished.\nYour Score: {score}/{totalQuestions}");

        }
    }
}