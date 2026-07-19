namespace VocabularyGame
{
    enum GameLevel
    {
        Easy = 1,
        Normal,
        Hard,
        Impossible
    }
    enum VocabularyCategory
    {
        Animals = 1,
        Food,
        Sports,
        Places,
        Health,
        Objects,
        Transportation,
        Verbs,
        Adjectives,
        Other
    }
    internal record VocabularyWord(string English, string Translation, VocabularyCategory Category, GameLevel Level);
    internal class Program
    {
        private static void GameInterface()
        {
            Console.WriteLine("\t\t== Learn English Vocabulary Game ==");
            Console.WriteLine();
        }

        private static bool Menu(ref GameLevel level)
        {
            GameInterface();
            Console.WriteLine($"1 - Start Game ({level})");
            Console.WriteLine("2 - Change Level");
            Console.WriteLine("3 - Select Category (option temporarily unavailable)");

            int option = ReadOption(3);

            if (option == 1)
            {
                Console.Clear();
                return false;
            }
            else if (option == 2)
            {
                level = ChangeGameLevel();
                Console.Clear();
                return true;
            }
            else return false;
        }

        private static GameLevel ChangeGameLevel()
        {
            Console.Clear();
            GameInterface();

            foreach(var level in Enum.GetValues<GameLevel>())
            {
                Console.WriteLine($"{(int)level} - {level}");
            }
            GameLevel newLevel = (GameLevel)ReadOption(Enum.GetValues<GameLevel>().Length);

            return newLevel;
        }

        private static bool AskQuestion(List<string> options, VocabularyWord vocabularyWord, ref int help)
        {
            options = options
                .Where(word => word != vocabularyWord.Translation)
                .OrderBy(_ => Random.Shared.Next())
                .Take(3)
                .ToList();

            options.Add(vocabularyWord.Translation);
            options = options.OrderBy(_ => Random.Shared.Next()).ToList();

            Console.WriteLine();
            Console.WriteLine(vocabularyWord.English);
            Console.WriteLine();

            for(int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {options[i]}");

            }
            int index;
            int limitIndex = options.Count;

            if(help > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{options.Count + 1} - Ask for help (see word category)");
                limitIndex++;
            }

            index = ReadOption(limitIndex);
            if(index == options.Count + 1)
            {
                help--;
                Console.WriteLine($"Word category: {vocabularyWord.Category}");
                limitIndex--;
                index = ReadOption(limitIndex);
            }

            return options[index - 1] == vocabularyWord.Translation;
        }

        private static int ReadOption(int limit)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Select Option: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int option) && option >= 1 && option <= limit)
                {
                    return option;
                }
                Console.WriteLine("Invalid option! Try again...");
            }
        }
        static void Main(string[] args)
        {
            int score = 0;
            int help = 5;
            GameLevel level = GameLevel.Easy;
            List<VocabularyWord> words =
            [
                new("Dog", "Cão", VocabularyCategory.Animals, GameLevel.Easy),
                 new("Cat", "Gato",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Easy),

                new("Horse", "Cavalo",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Easy),

                new("Rabbit", "Coelho",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Normal),

                new("Turtle", "Tartaruga",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Normal),

                new("Squirrel", "Esquilo",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Normal),

                new("Hedgehog", "Ouriço",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Hard),

                new("Rhinoceros", "Rinoceronte",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Hard),

                new("Platypus", "Ornitorrinco",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Impossible),

                new("Axolotl", "Axolote",
                    Category: VocabularyCategory.Animals,
                    Level: GameLevel.Impossible),


                // Food
                new("Bread", "Pão",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Easy),

                new("Cheese", "Queijo",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Easy),

                new("Apple", "Maçã",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Easy),

                new("Breakfast", "Café da manhã",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Normal),

                new("Vegetable", "Legume",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Normal),

                new("Strawberry", "Morango",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Normal),

                new("Seasoning", "Tempero",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Hard),

                new("Wholegrain", "Integral",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Hard),

                new("Delicatessen", "Iguarias ou alimentos finos",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Impossible),

                new("Confectionery", "Confeitaria ou doces",
                    Category: VocabularyCategory.Food,
                    Level: GameLevel.Impossible),


                // Sports
                new("Ball", "Bola",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Easy),

                new("Team", "Time",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Easy),

                new("Player", "Jogador",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Easy),

                new("Referee", "Árbitro",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Normal),

                new("Championship", "Campeonato",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Normal),

                new("Scoreboard", "Placar",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Normal),

                new("Endurance", "Resistência",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Hard),

                new("Weightlifting", "Levantamento de peso",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Hard),

                new("Steeplechase", "Corrida com obstáculos",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Impossible),

                new("Decathlon", "Decatlo",
                    Category: VocabularyCategory.Sports,
                    Level: GameLevel.Impossible),


                // Places
                new("House", "Casa",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Easy),

                new("School", "Escola",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Easy),

                new("Park", "Parque",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Easy),

                new("Library", "Biblioteca",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Normal),

                new("Hospital", "Hospital",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Normal),

                new("Neighborhood", "Bairro",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Normal),

                new("Warehouse", "Armazém",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Hard),

                new("Courthouse", "Tribunal",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Hard),

                new("Observatory", "Observatório",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Impossible),

                new("Archipelago", "Arquipélago",
                    Category: VocabularyCategory.Places,
                    Level: GameLevel.Impossible),


                // Health
                new("Doctor", "Médico",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Easy),

                new("Pain", "Dor",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Easy),

                new("Medicine", "Remédio",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Easy),

                new("Headache", "Dor de cabeça",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Normal),

                new("Treatment", "Tratamento",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Normal),

                new("Heartbeat", "Batimento cardíaco",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Normal),

                new("Prescription", "Receita médica",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Hard),

                new("Recovery", "Recuperação",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Hard),

                new("Cardiovascular", "Cardiovascular",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Impossible),

                new("Immunodeficiency", "Imunodeficiência",
                    Category: VocabularyCategory.Health,
                    Level: GameLevel.Impossible),


                // Objects
                new("Table", "Mesa",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Easy),

                new("Chair", "Cadeira",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Easy),

                new("Phone", "Telefone",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Easy),

                new("Backpack", "Mochila",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Normal),

                new("Scissors", "Tesoura",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Normal),

                new("Flashlight", "Lanterna",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Normal),

                new("Screwdriver", "Chave de fenda",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Hard),

                new("Bookshelf", "Estante de livros",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Hard),

                new("Miscellaneous", "Diversos ou variados",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Impossible),

                new("Paraphernalia", "Conjunto de objetos ou acessórios",
                    Category: VocabularyCategory.Objects,
                    Level: GameLevel.Impossible),


                // Transportation
                new("Car", "Carro",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Easy),

                new("Bus", "Ônibus",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Easy),

                new("Train", "Trem",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Easy),

                new("Airplane", "Avião",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Normal),

                new("Bicycle", "Bicicleta",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Normal),

                new("Motorcycle", "Motocicleta",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Normal),

                new("Ambulance", "Ambulância",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Hard),

                new("Sailboat", "Veleiro",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Hard),

                new("Hovercraft", "Aerodeslizador",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Impossible),

                new("Locomotive", "Locomotiva",
                    Category: VocabularyCategory.Transportation,
                    Level: GameLevel.Impossible),


                // Verbs
                new("Run", "Correr",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Easy),

                new("Eat", "Comer",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Easy),

                new("Sleep", "Dormir",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Easy),

                new("Choose", "Escolher",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Normal),

                new("Remember", "Lembrar",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Normal),

                new("Improve", "Melhorar",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Normal),

                new("Overcome", "Superar",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Hard),

                new("Accomplish", "Realizar ou concluir",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Hard),

                new("Obfuscate", "Tornar confuso ou difícil de entender",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Impossible),

                new("Circumvent", "Contornar ou evitar um obstáculo",
                    Category: VocabularyCategory.Verbs,
                    Level: GameLevel.Impossible),


                // Adjectives
                new("Happy", "Feliz",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Easy),

                new("Small", "Pequeno",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Easy),

                new("Beautiful", "Bonito",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Easy),

                new("Careful", "Cuidadoso",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Normal),

                new("Friendly", "Amigável",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Normal),

                new("Dangerous", "Perigoso",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Normal),

                new("Resilient", "Resiliente",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Hard),

                new("Overwhelming", "Avassalador",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Hard),

                new("Ephemeral", "Efêmero ou de curta duração",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Impossible),

                new("Sesquipedalian", "Que usa palavras muito longas",
                    Category: VocabularyCategory.Adjectives,
                    Level: GameLevel.Impossible),


                // Other
                new("Hello", "Olá",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Easy),

                new("Today", "Hoje",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Easy),

                new("Friend", "Amigo",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Easy),

                new("Although", "Embora",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Normal),

                new("Perhaps", "Talvez",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Normal),

                new("Knowledge", "Conhecimento",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Normal),

                new("Furthermore", "Além disso",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Hard),

                new("Nevertheless", "No entanto",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Hard),

                new("Ineffable", "Indescritível em palavras",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Impossible),

                new("Serendipity", "Descoberta feliz e inesperada",
                    Category: VocabularyCategory.Other,
                    Level: GameLevel.Impossible)
            ];

            while(Menu(ref level));

            List<string> options = words.Select(word => word.Translation).ToList();
            words = words
                .Where(word => word.Level == level)
                .OrderBy(_ => Random.Shared.Next())
                .ToList();

            for(int i = 0; i < words.Count; i++)
            {
                GameInterface();
                Console.Write($"Level: {level}");
                Console.WriteLine($"\t\t\tRemaining aids: {help}");
                Console.Write($"Question {i + 1}/{words.Count}");
                Console.WriteLine($"\t\t\tScore: {score}");

                bool isCorrect = AskQuestion(options, words[i], ref help);

                if (isCorrect)
                {
                    score++;
                    Console.WriteLine("Correct! +1 pts");
                }
                else
                {
                    Console.WriteLine($"Wrong! Correct option: {words[i].Translation}");
                }
                Console.Write("press any key to continue...");
                Console.ReadKey(true);
                Console.Clear();
            }
            Console.WriteLine($"Game finished. Your score: {score}/{words.Count}");

            if (score < words.Count * 50 / 100)
                Console.WriteLine("You need more practice!");
            else if (score < words.Count * 70 / 100)
                Console.WriteLine("Not bad!");
            else
            {
                Console.WriteLine("Good job!");
            }
        }
    }
}
