using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace ExpertSystemApp
{
    public class Rule
    {
        private string _description;
        private bool _value;

        public string Description
        {
            get { return _description; }
        }

        public bool Value
        {
            get { return _value; }
        }

        public Rule(string description, bool expression) 
        {
            _value = expression;
            _description = "Правило: " + description + " = " + Value + ";";
        }

    }

    public abstract class CombineRuleBase
    {
        protected string _description;
        protected List<Rule> _rules;
        protected bool _value;
        protected string operand;

        public string Description
        {
            get { return _description; }
        }

        public bool Value
        {
            get { return _value; }
        }

        protected CombineRuleBase(string operand, List<Rule> rules)
        {
            this.operand = operand;
            _rules = rules;
            _value = EvaluateRules(rules);
            _description = GetCombineDescription(rules);
        }

        protected abstract bool EvaluateRules(List<Rule> rules);

        private string GetCombineDescription(List<Rule> rules)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Комбинированное правило (" + operand + "): ");
            string between_rule = " " + operand + " ";
            stringBuilder.Append(String.Join(between_rule, rules.Select(x => x.Description)));
            stringBuilder.Append(". Итого: " + Value + ";");
            return stringBuilder.ToString();
        }
    }

    public class CombineRuleOR : CombineRuleBase
    {
        public CombineRuleOR(List<Rule> rules) : base("ИЛИ", rules) { }

        protected override bool EvaluateRules(List<Rule> rules)
        {
            return rules.Any(rule => rule.Value);
        }
    }

    public class CombineRuleAND : CombineRuleBase
    {
        public CombineRuleAND(List<Rule> rules) : base("И", rules) { }

        protected override bool EvaluateRules(List<Rule> rules)
        {
            return rules.All(rule => rule.Value);
        }
    }
}
