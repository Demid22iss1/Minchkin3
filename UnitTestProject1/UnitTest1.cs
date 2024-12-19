using Munchkin3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Munchkin3.Tests
{
    [TestClass] // Атрибут для MSTest
    public class MainWindowTests
    {
        [TestMethod] // Атрибут для тестового метода
        public void GetPlayerLevel_ShouldSetInitialLevelAndLabelContent()
        {
            // Arrange
            var mainWindow = new MainWindow();

            // Act
            mainWindow.GetPlayerLevel();

            // Assert (доступ через рефлексию)
            var levelLabelField = typeof(MainWindow).GetField("LevelLabel",
                BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(levelLabelField, "Поле LevelLabel не найдено!"); // Проверяем существование LevelLabel

            var levelLabel = (System.Windows.Controls.Label)levelLabelField.GetValue(mainWindow);
            Assert.IsNotNull(levelLabel, "Объект LevelLabel равен null!"); // Проверяем, что LevelLabel получен через рефлексию

            // Проверка ожидаемого значения контента
            Assert.AreEqual("Ваш уровень: 1", levelLabel.Content.ToString());
        }
        [TestMethod]
        public void FightButton_Click_ShouldUpdatePlayerLevelAndDisplayMessage()
        {
            // Arrange
            var mainWindow = new MainWindow();

            // Доступ к закрытым полям через рефлексию:
            var playerLevelField = typeof(MainWindow).GetField("playerLevel", BindingFlags.NonPublic | BindingFlags.Instance);
            var monsterLevelField = typeof(MainWindow).GetField("monsterLevel", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(playerLevelField, "Поле playerLevel не найдено!");
            Assert.IsNotNull(monsterLevelField, "Поле monsterLevel не найдено!");

            playerLevelField.SetValue(mainWindow, 3); // начальный уровень игрока
            monsterLevelField.SetValue(mainWindow, 2); // уровень монстра

            // Сравнительный бонус = 0 для теста
            var getTotalBonusesMethod = typeof(MainWindow).GetMethod("GetTotalBonuses", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(getTotalBonusesMethod, "Метод GetTotalBonuses не найден!");
            getTotalBonusesMethod.Invoke(mainWindow, null);

            // Act: Эмулируем нажатие кнопки FightButton
            var fightButtonClickMethod = typeof(MainWindow).GetMethod("FightButton_Click", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(fightButtonClickMethod, "Метод FightButton_Click не найден!");
            fightButtonClickMethod.Invoke(mainWindow, new object[] { null, null });

            // Assert: проверяем, что уровень игрока увеличен
            var updatedPlayerLevel = (int)playerLevelField.GetValue(mainWindow);
            Assert.AreEqual(4, updatedPlayerLevel, "Уровень игрока должен увеличиться на 1 после победы над монстром!");

            // Проверка вскрытия окна с результатом
            // Возможна эмуляция MessageBox'а или передача текста в UI, если метод перегружается для тестов.
        }
    }
}   


