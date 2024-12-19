using System.Windows.Media.Imaging;
using System.Windows;
using System;
using System.Reflection;

namespace Munchkin3
{
    public partial class MainWindow : Window
    {
        public int playerLevel;  // Уровень игрока
        public int monsterLevel; // Уровень монстра

        public MainWindow()
        {
            InitializeComponent();
            GetPlayerLevel();
            GenerateRandomBonuses();
            GenerateCharacter();
        }
        public Random random = new Random();
        public int GetMonsterLevel() => monsterLevel; // Добавляем метод для получения уровня монстра


        public void GenerateCharacter()
        {
            // Массивы рас и классов
            string[] races = { "Дварф", "Хафлинг", "Эльф" };
            string[] classes = { "Вор", "Воин", "Волшебник", "Клирик" };

            Random random = new Random();
            // Случайная раса
            string randomRace = races[random.Next(races.Length)];
            // Случайный класс
            string randomClass = classes[random.Next(classes.Length)];

            Rasa.Content = $"Раса: {randomRace}";
            Klass.Content = $"Класс: {randomClass}";

            // Пути для изображений
            string raceImagePath = null;
            string classImagePath = null;

            // Выбор изображения для расы
            switch (randomRace)
            {
                case "Эльф":
                    raceImagePath = @"C:\Users\п\source\repos\Munchkin3\Munchkin3\Elf.png";
                    break;
                case "Дварф":
                    raceImagePath = @"C:\Users\п\source\repos\Munchkin3\Munchkin3\Dwarf.png";
                    break;
                case "Хафлинг":
                    raceImagePath = @"C:\Users\п\source\repos\Munchkin3\Munchkin3\Hafling.png";
                    break;
            }

            // Выбор изображения для класса
            switch (randomClass)
            {
                case "Вор":
                    classImagePath = @"C:\Users\п\source\repos\Munchkin3\Munchkin3\Vor.png";
                    break;
                case "Воин":
                    classImagePath = @"C:\Users\п\source\repos\Munchkin3\Munchkin3\Voin.png";
                    break;
                case "Волшебник":
                    classImagePath = @"C:\Users\п\source\repos\Munchkin3\Munchkin3\Volshebnik.png";
                    break;
                case "Клирик":
                    classImagePath = @"C:\Users\п\source\repos\Munchkin3\Munchkin3\Klirik.png";
                    break;
            }

            // Отображение изображений для расы
            if (!string.IsNullOrEmpty(raceImagePath))
            {
                try
                {
                    Uri resourceUri = new Uri(raceImagePath, UriKind.Absolute);
                    Karta_Rasa.Source = new BitmapImage(resourceUri);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения расы: {ex.Message}\nПуть: {raceImagePath}");
                }
            }

            // Отображение изображений для класса
            if (!string.IsNullOrEmpty(classImagePath))
            {
                try
                {
                    Uri resourceUri = new Uri(classImagePath, UriKind.Absolute);
                    Karta_Klassa.Source = new BitmapImage(resourceUri);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения класса: {ex.Message}\nПуть: {classImagePath}");
                }
            }
            else
            {
                MessageBox.Show("Путь к изображению класса не задан!");
            }
        }

        // Генерация случайных значений для бонусов
        public void GenerateRandomBonuses()
        {
            Random random = new Random();

            // Генерируем случайные значение и обновляем контент только для ненулевых бонусов
            int bonus1 = random.Next(0, 6); // От 0 до 5
            Bonus1.Content = bonus1 > 0 ? $"Бонус +{bonus1}" : "";

            int bonus2 = random.Next(0, 6);
            Bonus2.Content = bonus2 > 0 ? $"Бонус +{bonus2}" : "";

            int bonus3 = random.Next(0, 6);
            Bonus3.Content = bonus3 > 0 ? $"Бонус +{bonus3}" : "";

            int bonus4 = random.Next(0, 6);
            Bonus4.Content = bonus4 > 0 ? $"Бонус +{bonus4}" : "";

            int bonus5 = random.Next(0, 6);
            Bonus5.Content = bonus5 > 0 ? $"Бонус +{bonus5}" : "";
        }

        public int GetTotalBonuses()
        {
            int bonus1 = string.IsNullOrEmpty(Bonus1.Content.ToString()) ? 0 : int.Parse(Bonus1.Content.ToString().Replace("Бонус +", ""));
            int bonus2 = string.IsNullOrEmpty(Bonus2.Content.ToString()) ? 0 : int.Parse(Bonus2.Content.ToString().Replace("Бонус +", ""));
            int bonus3 = string.IsNullOrEmpty(Bonus3.Content.ToString()) ? 0 : int.Parse(Bonus3.Content.ToString().Replace("Бонус +", ""));
            int bonus4 = string.IsNullOrEmpty(Bonus4.Content.ToString()) ? 0 : int.Parse(Bonus4.Content.ToString().Replace("Бонус +", ""));
            int bonus5 = string.IsNullOrEmpty(Bonus5.Content.ToString()) ? 0 : int.Parse(Bonus5.Content.ToString().Replace("Бонус +", ""));

            // Сумма всех бонусов
            return bonus1 + bonus2 + bonus3 + bonus4 + bonus5;
        }

        // Генерация уровня игрока
        public void GetPlayerLevel()
        {
            playerLevel = 1;
            LevelLabel.Content = $"Ваш уровень: {playerLevel}";
        }

        // Метод для обновления отображения уровня игрока
        public void UpdatePlayerLevel()
        {
            LevelLabel.Content = $"Ваш уровень: {playerLevel}";
        }

        // Событие при нажатии на "Открыть дверь"
        public void OpenDoor_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            // Генерируем уровень монстра
            monsterLevel = random.Next(1, 16);

            // Устанавливаем изображение монстра
            Monster.Source = new BitmapImage(new Uri("monster.png", UriKind.Relative));
            Monster.Visibility = Visibility.Visible; // Показываем изображение монстра

            // Показываем уровень прямо на изображении
            MonsterLevelText.Visibility = Visibility.Visible; // Показываем текст
            MonsterLevelText.Text = monsterLevel.ToString();   // Устанавливаем текст уровня

            // Показываем кнопку сражения
            FightButton.Visibility = Visibility.Visible;
        }

        // Обработка кнопки "Сразиться"
        public void FightButton_Click(object sender, RoutedEventArgs e)
        {
            // Суммируем временные бонусы
            int temporaryBonus = GetTotalBonuses();
            int effectivePlayerLevel = playerLevel + temporaryBonus;

            string resultMessage;

            // Сравнение уровней (учитывая бонусы)
            if (effectivePlayerLevel > monsterLevel)
            {
                playerLevel++; // Увеличиваем уровень игрока только, если он победил
                resultMessage = $"Вы победили монстра! Ваш текущий уровень: {playerLevel}";
            }
            else
            {
                playerLevel = Math.Max(1, playerLevel - 1); // Уменьшаем уровень, но не меньше 1
                resultMessage = "Вы проиграли монстру! Ваш уровень уменьшился.";
            }

            // Обновляем уровень игрока в интерфейсе
            UpdatePlayerLevel();

            // Отображение результата
            MessageBox.Show(resultMessage, "Результат сражения");

            // Скрытие кнопки и изображения монстра после боя
            FightButton.Visibility = Visibility.Hidden;
            Monster.Visibility = Visibility.Hidden;
            MonsterLevelText.Visibility = Visibility.Hidden;
        }
        
    }
}