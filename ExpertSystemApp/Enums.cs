using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemApp
{

    public enum Education
    {
        [Description("Среднее")]
        Average,

        [Description("Среднее специальное")]
        MediumSpecial,

        [Description("Высшее")]
        Higher,

        [Description("Магистратура")]
        Magistracy,

        [Description("Аспирантура")]
        Doctoral,
    }

    public enum Specialization
    {
        [Description("Информационные технологии")]
        IT,

        [Description("Финансы")]
        Finance,

        [Description("Медиа")]
        Media,

        [Description("Наука")]
        Science,

        [Description("Инженерия")]
        Engineering,

    }

    public enum Qualities
    {
        [Description("Стрессоустойчивость")]
        StressResist,

        [Description("Навыки коммуникации")]
        CommunicationSkill,

        [Description("Лидерство")]
        Leadership,

        [Description("Креативность")]
        Creativity,

        [Description("Аналитические способности")]
        Analytical,
    }


}
