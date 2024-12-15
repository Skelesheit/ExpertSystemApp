using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExpertSystemApp
{
    public class Rules
    {
        public Rule EducationIsAverage { get; private set; }
        public Rule EducationIsMediumSpecial { get; private set; }
        public Rule EducationIsHigher { get; private set; }
        public Rule EducationIsMagistracy { get; private set; }
        public Rule EducationIsDoctoral { get; private set; }

        public Rule SpecializationIsIT { get; private set; }
        public Rule SpecializationIsFinance { get; private set; }
        public Rule SpecializationIsMedia { get; private set; }
        public Rule SpecializationIsScience { get; private set; }
        public Rule SpecializationIsEngineering { get; private set; }

        public Rule QualityIsStressResist { get; private set; }
        public Rule QualityIsCommunicationSkill { get; private set; }
        public Rule QualityIsLeadership { get; private set; }
        public Rule QualityIsCreativity { get; private set; }
        public Rule QualityIsAnalytical { get; private set; }

        public bool IsDrive { get; private set; }

        public Rules(Education _education, Specialization _specialization, List<Qualities> _qualities, bool _isDrive)
        {
            EducationIsAverage = new Rule("Образование: Общее", _education == Education.Average);
            EducationIsMediumSpecial = new Rule("Образование: Среднее специальное", _education == Education.MediumSpecial);
            EducationIsHigher = new Rule("Образование: Высшее", _education == Education.Higher);
            EducationIsMagistracy = new Rule("Образование: Магистратура", _education == Education.Magistracy);
            EducationIsDoctoral = new Rule("Образование: Аспирантура", _education == Education.Doctoral);

            SpecializationIsIT = new Rule("Специализация: Информационные технологии", _specialization == Specialization.IT);
            SpecializationIsFinance = new Rule("Специализация: Финансы", _specialization == Specialization.Finance);
            SpecializationIsMedia = new Rule("Специализация: Медиа", _specialization == Specialization.Media);
            SpecializationIsScience = new Rule("Специализация: Наука", _specialization == Specialization.Science);
            SpecializationIsEngineering = new Rule("Специализация: Инженерия", _specialization == Specialization.Engineering);

            QualityIsStressResist = new Rule("Качества: Стрессоустойчивость", _qualities.Contains(Qualities.StressResist));
            QualityIsCommunicationSkill = new Rule("Качества: Навыки коммуникации", _qualities.Contains(Qualities.CommunicationSkill));
            QualityIsLeadership = new Rule("Качества: Лидерство", _qualities.Contains(Qualities.Leadership));
            QualityIsCreativity = new Rule("Качества: Креативность", _qualities.Contains(Qualities.Creativity));
            QualityIsAnalytical = new Rule("Качества: Аналитические способности", _qualities.Contains(Qualities.Analytical));

            IsDrive = _isDrive;
        }

        
    }



}
