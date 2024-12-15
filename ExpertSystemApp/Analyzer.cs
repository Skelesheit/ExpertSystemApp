using ExpertSystemApp.database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemApp
{
    internal class Analyzer
    {
        public Education education;
        public Specialization specialization;
        public List<Qualities> qualities;
        public bool isDrive;
        public int workExperience;
        public Rules rules;
        public static DbRepository _repository;

        

        public List<string> explanations = new List<string>();
        public Analyzer(Education _education, Specialization _specialization,
                        List<Qualities> _qualities, bool _isDrive, int _workExperience)
        {
            education = _education;
            specialization = _specialization;
            qualities = _qualities;
            isDrive = _isDrive;
            workExperience = _workExperience;
            rules = new Rules(_education, _specialization, _qualities, _isDrive);
            if (_repository == null)
            {
                throw new Exception("Нет контекста для базы данных");
            }
        }

        public Profession GetAnswer() // вот он! Главный метод всей программы!
        {
            // Начало
            explanations = new List<string> ();
            

            var educate_rule_lower = new CombineRuleOR(new List<Rule>() { rules.EducationIsAverage, rules.EducationIsMediumSpecial });
            string explain = educate_rule_lower.Description;
            explanations.Add(explain);
            
            if (educate_rule_lower.Value)
            {
                explanations.Add(rules.EducationIsAverage.Description);
                if (rules.EducationIsAverage.Value)
                {
                    ChooseWithAvarageEducation();
                }
                else
                {
                    explanations.Add(rules.SpecializationIsIT.Description);
                    if (rules.SpecializationIsIT.Value)
                    {
                        return _repository.ChooseProfession("Оператор техподдержки");
                    }
                    explanations.Add(rules.SpecializationIsFinance.Description);
                    if (rules.SpecializationIsFinance.Value)
                    {
                        return _repository.ChooseProfession("Бухгалтер");
                    }
                    explanations.Add(rules.SpecializationIsMedia.Description);
                    if (rules.SpecializationIsMedia.Value)
                    {
                        return _repository.ChooseProfession("Фотограф");
                    }
                    explanations.Add(rules.SpecializationIsEngineering.Description);
                    if (rules.SpecializationIsEngineering.Value)
                    {
                        return _repository.ChooseProfession("Электрик");
                    }
                    else
                    {
                        ChooseWithAvarageEducation();
                    }
                }
            }
            else 
            {
                explanations.Add(rules.EducationIsDoctoral.Description);
                if (rules.EducationIsDoctoral.Value)
                {
                    return _repository.ChooseProfession("Учёный ОИЯИ");
                }
                explanations.Add(rules.SpecializationIsIT.Description);
                if (rules.SpecializationIsIT.Value)
                {
                    var qualitites = new List<Rule>()
                    {
                        rules.QualityIsCommunicationSkill,
                        rules.QualityIsLeadership,
                        rules.QualityIsStressResist
                    };
                    var communicate_rules = new CombineRuleOR(qualitites);
                    explanations.Add(communicate_rules.Description);
                    if (communicate_rules.Value)
                    {
                        explanations.Add(rules.QualityIsAnalytical.Description);
                        if (rules.QualityIsAnalytical.Value)
                        {
                            return _repository.ChooseProfession("Системный аналитик");
                        }
                        else
                        {
                            bool expRole = workExperience < 1;
                            explanations.Add("год опыта меньше года?" + " : " + expRole);
                            if (expRole)
                            {
                                return _repository.ChooseProfession("Программист стажёр");
                            }
                            expRole = workExperience < 3;
                            explanations.Add("Опыт менее 3 лет?" + " : " + expRole);
                            if (expRole)
                            {
                                return _repository.ChooseProfession("Программист Джуниор");
                            }
                            expRole = workExperience < 5;
                            explanations.Add("Опыт менее 5 лет?" + " : " + expRole);
                            if (expRole)
                            {
                                return _repository.ChooseProfession("Программист мидл");
                            }
                            explanations.Add(rules.QualityIsLeadership.Description);
                            if (rules.QualityIsLeadership.Value)
                            {
                                return _repository.ChooseProfession("Тимлид");
                            }
                            else
                            {
                                return _repository.ChooseProfession("Программист сеньор");
                            }
                        }
                    }
                    else
                    {
                        return _repository.ChooseProfession("Фрилансер");
                    }
                }
                explanations.Add(rules.SpecializationIsFinance.Description);
                if (rules.SpecializationIsFinance.Value)
                {
                    bool expRole;
                    expRole = workExperience < 3;
                    explanations.Add("Опыт менее 3 лет? : " + expRole.ToString());
                    if (expRole)
                    {
                        return _repository.ChooseProfession("Бухгалтер");
                    }
                    expRole = workExperience < 5;
                    explanations.Add("Опыт менее 5 лет? : " + expRole.ToString());
                    if (expRole)
                    {
                        return _repository.ChooseProfession("Менеджер");
                    }
                    explanations.Add(rules.QualityIsLeadership.Description);
                    if (rules.QualityIsLeadership.Value)
                    {
                        return _repository.ChooseProfession("Руководитель финансового отдела");
                    }
                    else
                    {
                        return _repository.ChooseProfession("Финансовый аналитик");
                    }
                }
                explanations.Add(rules.SpecializationIsMedia.Description);
                if (rules.SpecializationIsMedia.Value)
                {
                    explanations.Add(rules.QualityIsCreativity.Description);
                    if (rules.QualityIsCreativity.Value)
                    {
                        explanations.Add(rules.QualityIsAnalytical.Description);
                        if (rules.QualityIsAnalytical.Value)
                        {
                            explanations.Add(rules.QualityIsLeadership.Description);
                            if (rules.QualityIsLeadership.Value)
                            {
                                return _repository.ChooseProfession("Руководитель медиа-отдела");
                            }
                            else
                            {
                                return _repository.ChooseProfession("Продюсер контента");
                            }
                        }
                        else
                        {
                            return _repository.ChooseProfession("Создатель контента");
                        }
                    }
                    else
                    {
                        return _repository.ChooseProfession("Оператор камеры");
                    }
                }
                explanations.Add(rules.SpecializationIsEngineering.Description);
                if (rules.SpecializationIsEngineering.Value)
                {
                    bool expRole;
                    explanations.Add(rules.QualityIsAnalytical.Description);
                    if (rules.QualityIsAnalytical.Value)
                    {
                        expRole = workExperience < 3;
                        explanations.Add("опыт менее 3 лет? : " + expRole.ToString());
                        if (expRole)
                        {
                            return _repository.ChooseProfession("Инженер аналитик");
                        }
                        else
                        {
                            return _repository.ChooseProfession("Специалист по моделированию данных");
                        }
                    }
                    expRole = workExperience < 5;
                    explanations.Add("опыт менее 5 лет? : " + expRole.ToString());
                    if (expRole)
                    {
                        return _repository.ChooseProfession("Инженер производства");
                    }
                    else
                    {
                        explanations.Add(rules.QualityIsLeadership.Description);
                        if (rules.QualityIsLeadership.Value)
                        {
                            return _repository.ChooseProfession("Руководитель инженерного отдела");
                        }
                        else
                        {
                            return _repository.ChooseProfession("Инженер-эксперт");
                        }
                    }
                }
                else
                {
                    explanations.Add("остаётся только научная область, а значит:");
                    return _repository.ChooseProfession("Лаборант ОИЯИ");
                }
            }
            // если что то другое (неизвестное), то default
            return _repository.ChooseProfession("Грузчик");
        }

        private Profession ChooseWithAvarageEducation()
        {
            if (rules.IsDrive)
            {
                explanations.Add("Есть права");
                return _repository.ChooseProfession("Грузчик");
            }
            else
            {
                explanations.Add("Прав нет");
                return _repository.ChooseProfession("Курьер");
            }
        }
    }
    
}
