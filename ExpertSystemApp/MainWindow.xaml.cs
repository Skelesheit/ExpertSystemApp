using ExpertSystemApp.database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpertSystemApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, Education> EducationConvert = new() 
        { 
            {"Общее", Education.Average},
            {"Среднее специальное", Education.MediumSpecial},
            {"Высшее", Education.Higher},
            {"Магистратура", Education.Magistracy},
            {"Аспирантура", Education.Doctoral},
        };
        public Dictionary<string, Specialization> SpecializationConvert = new()
        {
            {"Информационные технологии", Specialization.IT},
            {"Финансы", Specialization.Finance},
            {"Медиа", Specialization.Media},
            {"Наука", Specialization.Science},
            {"Инженерия", Specialization.Engineering},

        };

        public MainWindow()
        {
            InitializeComponent();

            PutDefautValuesToUI();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=professions.db")
            .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new DbRepository();
                Analyzer._repository = repository;

            }
            ErrorLabel.Content = "";

        }

        private void AnalyzeBtn_Click(object sender, RoutedEventArgs e)
        {
            string selectedEducation = ComboEducation.SelectedItem?.ToString();
            string selectedSpecialization = ComboSpecialization.SelectedItem?.ToString();

            Education education = EducationConvert[selectedEducation];
            Specialization specialization = SpecializationConvert[selectedSpecialization];

            bool success = int.TryParse(ExpYearField.Text, out int outInt);
            if (!success)
            {
                SetError("Опыт работы целочисленное значение");
                return;
            }
            int workExpirence = outInt;

            bool isStress = StressCheck.IsChecked == true;
            bool isComunicate = ComunicateCheck.IsChecked == true;
            bool isLider = LiderCheck.IsChecked == true;
            bool isCreative = CreativeCheck.IsChecked == true;
            bool isAnalitics = AnaliticsCheck.IsChecked == true;

            bool isDrive = isDriveCheck.IsChecked == true;

            List<Qualities> qualities = new List<Qualities>();
            if (isStress) { qualities.Add(Qualities.StressResist); }
            if (isComunicate) { qualities.Add(Qualities.CommunicationSkill); }
            if (isLider) { qualities.Add(Qualities.Leadership); }
            if (isCreative) { qualities.Add(Qualities.Creativity); }
            if (isAnalitics) { qualities.Add(Qualities.Analytical); }

            Analyzer analyzer = new Analyzer(education, specialization, qualities, isDrive, workExpirence);

            Profession chosen_profession =  analyzer.GetAnswer();
            List<string> explanations = analyzer.explanations;

            string explanationText = String.Join("\n", explanations);
            string professionText = chosen_profession.Name + "\n" + "Описание: " + chosen_profession.Description;
            // вставляем текста в наш UI
            OutputAnswer.Text = professionText;
            OutputIntepretetionBlock.Text = explanationText;
        }

        private void ErrorClick(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Content = "";
        }
        private void SetError(string error)
        {
            ErrorLabel.Content = error;
        }


        private void PutDefautValuesToUI()
        {
            OutputIntepretetionBlock.Text = "Здесь будут объяснения";
            OutputAnswer.Text = "Выберите варинты. Вывод отобразится здесь.";

            ComboEducation.ItemsSource = EducationConvert.Keys.ToList();
            ComboEducation.SelectedIndex = 0;

            ComboSpecialization.ItemsSource = SpecializationConvert.Keys.ToList();
            ComboSpecialization.SelectedIndex = 0;

            ExpYearField.Text = "0";

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
