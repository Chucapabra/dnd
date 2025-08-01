using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDHelper.Modules.Сharacteristics
{
	public static class CharacteristicTable
	{
      
        public static int PointsAgilitySkills = 0;
        public static int PointsAgilitySkillsNow = 0;

        public static int PointsIntellectSkills = 0;
        public static int PointsIntellectSkillsNow = 0;

        public static int PointsWisdomSkills = 0;
        public static int PointsWisdomSkillsNow = 0;

        public static int PointsCharismaSkills = 0;
        public static int PointsCharismaSkillsNow = 0;


        public enum StatName
        {
            // Физические
            Strength, Athlete,
            Agility, Acrobatics, SleightOfHand, Stealth,
            Body,

            // Интеллектуальные
            Intellect, Magic, Religion,
            Nature, History,
            Investigation, Technology,

            // Мудрость/Восприятие
            Wisdom, Wisdom_, Medicine,
            Perception, Insight, Survival, TOAnimals,

            // Харизма
            Charisma, Deception,
            Intimidation, Speech, Persuasion
        }

        private static readonly int[] _baseStats = new int[Enum.GetValues(typeof(StatName)).Length];
        private static readonly int[] _buffedStats = new int[_baseStats.Length];

        // === Доступ по имени ===
        private static ref int GetBase(StatName name) => ref _baseStats[(int)name];
        private static ref int GetBuffed(StatName name) => ref _buffedStats[(int)name];

        // === Доступ по индексу ===
        private static ref int GetBaseByIndex(int index) => ref _baseStats[index];
        private static ref int GetBuffedByIndex(int index) => ref _buffedStats[index];

        // === Индексаторы для удобства ===
        public static ref int Base(StatName name) => ref GetBase(name);
        public static ref int Base(int index) => ref GetBaseByIndex(index);

        public static ref int Buffed(StatName name) => ref GetBuffed(name);
        public static ref int Buffed(int index) => ref GetBuffedByIndex(index);

    }
}


