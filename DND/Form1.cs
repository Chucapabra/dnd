using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace DND
{
    public partial class Form1 : MaterialForm
    {

        int Level = 1;

        int Points = 72;
        int PointsNow = 0;

        int MinLock = 5;
        int MaxLock = 20;

        int MinLockSkills = 3;
        int MaxLockSkills = 30;


        int Strength = 0;

        int Agility = 0;
        int PointsAgilitySkills = 0;
        int PointsAgilitySkillsNow = 0;
        int Acrobatics = 0;
        int SleightOfHand = 0;
        int Stealth = 0;

        int Body = 0;

        int Intellect = 0;
        int PointsIntellectSkills = 0;
        int PointsIntellectSkillsNow = 0;
        int Magic = 0;
        int Religion = 0;
        int Nature = 0;
        int History = 0;
        int Investigation = 0;
        int Technology = 0;

        int Wisdom = 0;
        int PointsWisdomSkills = 0;
        int PointsWisdomSkillsNow = 0;
        int Medicine = 0;
        int Perception = 0;
        int Insight = 0;
        int Survival = 0;
        int TreatmentOfAnimals = 0;

        int Charisma = 0;
        int PointsCharismaSkills = 0;
        int PointsCharismaSkillsNow = 0;
        int Deception = 0;
        int Intimidation = 0;
        int Speech = 0;
        int Persuasion = 0;

        int BaffStrength = 0;
        int BaffAgility = 0;
        int BaffBody = 0;
        int BaffIntellect = 0;
        int BaffWisdom = 0;
        int BaffCharisma = 0;
        int BaffKD = 0;
        double BaffMagicDamage = 0;
        double BaffPhysDamage = 0;

        int BaffClassStrength = 0;
        int BaffClassAgility = 0;
        int BaffClassAcrobatics = 0;
        int BaffClassSleightOfHand = 0;
        int BaffClassStealth = 0;
        int BaffClassBody = 0;
        int BaffClassIntellect = 0;
        int BaffClassInvestigation = 0;
        int BaffClassWisdom = 0;
        int BaffClassCharisma = 0;
        int BaffClassDeception = 0;
        int BaffClassSpeech = 0;
        int BaffClassPersuasion = 0;

        int BaffClassAcrobaticsAdd = 0;
        int BaffClassDeceptionAdd = 0;

        int BaffClassStrength1 = 0;
        int BaffClassAgility1 = 0;
        int BaffClassAcrobatics1 = 0;
        int BaffClassSleightOfHand1 = 0;
        int BaffClassStealth1 = 0;
        int BaffClassBody1 = 0;
        int BaffClassIntellect1 = 0;
        int BaffClassInvestigation1 = 0;
        int BaffClassWisdom1 = 0;
        int BaffClassCharisma1 = 0;
        int BaffClassDeception1 = 0;
        int BaffClassSpeech1 = 0;
        int BaffClassPersuasion1 = 0;

        int BaffClassStrength2 = 0;
        int BaffClassAgility2 = 0;
        int BaffClassAcrobatics2 = 0;
        int BaffClassSleightOfHand2 = 0;
        int BaffClassStealth2 = 0;
        int BaffClassBody2 = 0;
        int BaffClassIntellect2 = 0;
        int BaffClassInvestigation2 = 0;
        int BaffClassWisdom2 = 0;
        int BaffClassCharisma2 = 0;
        int BaffClassDeception2 = 0;
        int BaffClassSpeech2 = 0;
        int BaffClassPersuasion2 = 0;

        int BaffClassStrength3 = 0;
        int BaffClassAgility3 = 0;
        int BaffClassAcrobatics3 = 0;
        int BaffClassSleightOfHand3 = 0;
        int BaffClassStealth3 = 0;
        int BaffClassBody3 = 0;
        int BaffClassIntellect3 = 0;
        int BaffClassInvestigation3 = 0;
        int BaffClassWisdom3 = 0;
        int BaffClassCharisma3 = 0;
        int BaffClassDeception3 = 0;
        int BaffClassSpeech3 = 0;
        int BaffClassPersuasion3 = 0;
        double BaffHealth = 0;

        int BaffPoints = 0;
        int BaffLevelPoints = 0;


        int LockLevelTree = 2;
        int PointsTree = 0;
        int PointsTreeNow = 0;

        int PointsTree1 = 0;
        int PointsTree2 = 0;
        int PointsTree3 = 0;

        float[] QualityBaff;
        float[] QualityDebaff;

        char[] DeleteKDSymbols = { '*', '^' };

        double UsetWeight;
        double UsetItemWeight;
        int BaffWeight;

        int LockNumberBackpack;
        int AddWeightBackpack;
        int DivideWeightBackpack;

        double TotalDamage;

        int TotalKD = 0;
        int SubtractKD = 0;
        int ItemsKD = 0;
        int ItemsKDHead = 0;
        int BodyKD = 0;
        double GeneralDamage = 0;

        double WalkingStickBaffRace = 1; 


        public Form1()
        {
            

            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);



            Level = Properties.Settings.Default.Level;

            Strength = Properties.Settings.Default.Strength;

            Agility = Properties.Settings.Default.Agility;
            Acrobatics = Properties.Settings.Default.Acrobatics;
            SleightOfHand = Properties.Settings.Default.SleightOfHand;

            Stealth = Properties.Settings.Default.Stealth;

            Body = Properties.Settings.Default.Body;

            Intellect = Properties.Settings.Default.Intellect;
            Magic = Properties.Settings.Default.Magic;
            Religion = Properties.Settings.Default.Religion;
            Nature = Properties.Settings.Default.Nature;
            History = Properties.Settings.Default.History;
            Investigation = Properties.Settings.Default.Investigation;
            Technology = Properties.Settings.Default.Technology;

            Wisdom = Properties.Settings.Default.Wisdom;
            Medicine = Properties.Settings.Default.Medicine;
            Perception = Properties.Settings.Default.Perception;
            Insight = Properties.Settings.Default.Insight;
            Survival = Properties.Settings.Default.Survival;
            TreatmentOfAnimals = Properties.Settings.Default.TreatmentOfAnimals;

            Charisma = Properties.Settings.Default.Charisma;
            Deception = Properties.Settings.Default.Deception;
            Intimidation = Properties.Settings.Default.Intimidation;
            Speech = Properties.Settings.Default.Speech;
            Persuasion = Properties.Settings.Default.Persuasion;

            RaceName.Text = Properties.Settings.Default.Race;
            ClassName.Text = Properties.Settings.Default.Class;

            PointsTree1 = Properties.Settings.Default.LevelTree1;
            PointsTree2 = Properties.Settings.Default.LevelTree2;
            PointsTree3 = Properties.Settings.Default.LevelTree3;

            BackpackBox.Text = Properties.Settings.Default.Backpack;
            BackpackNumber.Value = Properties.Settings.Default.BackpackNumber;

            CountWeightItem.Checked = true;

            GeneralDamage = Properties.Settings.Default.Health;
            AddSubstractHealthBox.Text = "-";

            NameText.Text = Properties.Settings.Default.Name;

            CupperBox.Value = Properties.Settings.Default.Copper;
            SerebrenicBox.Value = Properties.Settings.Default.Serebrenic;
            GoldenBox.Value = Properties.Settings.Default.Golden;

            InfoTextBox.ScrollBars = ScrollBars.Both;
            InfoTextBox.Text = Properties.Settings.Default.InfoTextBox;
            
            if (Properties.Settings.Default.Inventory == null)
            {
                Properties.Settings.Default.Inventory = new StringCollection();
            }

            var items = new ListViewItem[Properties.Settings.Default.Inventory.Count];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ListViewItem(Properties.Settings.Default.Inventory[i].Split('|'));
            }

            this.Inventory.Items.AddRange(items);

            SetRace();
        



            int[] Check = { Strength, Agility, Intellect, Body, Wisdom, Charisma };
            for (int i = 0; i < Check.Length; i++)
            {
                if (Check[i] < MinLock)
                {
                    Check[i] = MinLock;
                }
            }
            Strength = Check[0];
            Agility = Check[1];
            Intellect = Check[2];
            Body = Check[3];
            Wisdom = Check[4];
            Charisma = Check[5];

            int[] CheckSkills =
                {
                Acrobatics, SleightOfHand, Stealth, Magic, Religion, Nature, History,
                Investigation, Technology, Medicine, Perception, Insight, Survival,
                TreatmentOfAnimals, Deception, Intimidation, Speech, Persuasion
                };
            for (int i = 0; i < CheckSkills.Length; i++)
            {
                if (CheckSkills[i] < MinLockSkills)
                {
                    CheckSkills[i] = MinLockSkills;
                }
            }
            Acrobatics = CheckSkills[0];
            SleightOfHand = CheckSkills[1];
            Stealth = CheckSkills[2];
            Magic = CheckSkills[3];
            Religion = CheckSkills[4];
            Nature = CheckSkills[5];
            History = CheckSkills[6];
            Investigation = CheckSkills[7];
            Technology = CheckSkills[8];
            Medicine = CheckSkills[9];
            Perception = CheckSkills[10];
            Insight = CheckSkills[11];
            Survival = CheckSkills[12];
            TreatmentOfAnimals = CheckSkills[13];
            Deception = CheckSkills[14];
            Intimidation = CheckSkills[15];
            Speech = CheckSkills[16];
            Persuasion = CheckSkills[17];

            CallMethodPoints();
            LevelMethod();
            SetPointsTree();
            SetBaffClassTree();
            UsetWeightM();
            HealthSet();
            WeightSet();
            WalkingStickMethod();
        }



        private void CallMethodPoints()
        {
            SetPoint();
            StrengthMethod();
            AgilityMethod();
            BodyMethod();
            IntellectMethod();
            WisdomMethod();
            CharismaMethod();
            ArmorKDSet();
            SetTotalKD();
        }

        private void SetPoint()
        {
            PointsNow = (BaffLevelPoints + BaffPoints + Points) - (Strength + Agility + Body + Intellect + Wisdom + Charisma);
            PointsValueLabel.Text = PointsNow.ToString();

            if (PointsNow < 0)
            {
                PointsValueLabel.ForeColor = Color.Red;
            }
            else
            {
                PointsValueLabel.ForeColor = Color.Black;
            }

        }

        private void EditCharacteristics_CheckedChanged(object sender, EventArgs e)
        {
            bool StatusButton = EditCharacteristics.Checked;


            if (StatusButton == true)
            {
                LevelAdd.Visible = StatusButton;
                LevelLower.Visible = StatusButton;

                PointsLabel.Visible = StatusButton;
                PointsValueLabel.Visible = StatusButton;

                AgilitySkilsEdit.Visible = StatusButton;

                StrengthAdd.Visible = StatusButton;
                StrengthLower.Visible = StatusButton;

                AgilityAdd.Visible = StatusButton;
                AgilityLower.Visible = StatusButton;

                BodyAdd.Visible = StatusButton;
                BodyLower.Visible = StatusButton;

                IntellectAdd.Visible = StatusButton;
                IntellectLower.Visible = StatusButton;
                IntellectSkillsEdit.Visible = StatusButton;

                WisdomLower.Visible = StatusButton;
                WisdomAdd.Visible = StatusButton;
                WisdomSkillsEdit.Visible = StatusButton;

                CharismaAdd.Visible = StatusButton;
                CharismaLower.Visible = StatusButton;
                CharismaSkillsEdit.Visible = StatusButton;
                CharismaSkillsEdit.Enabled = StatusButton;


                RaceBox.Visible = StatusButton;
                СlassBox.Visible = StatusButton;

                DevelopmentTreeAdd1.Visible = StatusButton;
                DevelopmentTreeAdd2.Visible = StatusButton;
                DevelopmentTreeAdd3.Visible = StatusButton;
                DevelopmentTreeLower1.Visible = StatusButton;
                DevelopmentTreeLower2.Visible = StatusButton;
                DevelopmentTreeLower3.Visible = StatusButton;
                DevelopmentTreePointsLabel.Visible = StatusButton;
                DevelopmentTreePointsValueLabel.Visible = StatusButton;

                NameText.Visible = StatusButton;

                BackpackBox.Visible = StatusButton;
                BackpackLabel.Visible = StatusButton;
                BackpackNumber.Visible = StatusButton;
                BackpackNumberLabel.Visible = StatusButton;
            }
            if (StatusButton == false)
            {
                LevelAdd.Visible = StatusButton;
                LevelLower.Visible = StatusButton;

                PointsLabel.Visible = StatusButton;
                PointsValueLabel.Visible = StatusButton;


                StrengthAdd.Visible = StatusButton;
                StrengthLower.Visible = StatusButton;

                AgilityAdd.Visible = StatusButton;
                AgilityLower.Visible = StatusButton;
                AgilitySkilsEdit.Visible = StatusButton;
                AgilitySkilsEdit.Checked = StatusButton;


                BodyAdd.Visible = StatusButton;
                BodyLower.Visible = StatusButton;

                IntellectAdd.Visible = StatusButton;
                IntellectLower.Visible = StatusButton;
                IntellectSkillsEdit.Visible = StatusButton;
                IntellectSkillsEdit.Checked = StatusButton;

                WisdomLower.Visible = StatusButton;
                WisdomAdd.Visible = StatusButton;
                WisdomSkillsEdit.Visible = StatusButton;
                WisdomSkillsEdit.Checked = StatusButton;

                CharismaAdd.Visible = StatusButton;
                CharismaLower.Visible = StatusButton;
                CharismaSkillsEdit.Visible = StatusButton;
                CharismaSkillsEdit.Enabled = StatusButton;


                RaceBox.Visible = StatusButton;
                СlassBox.Visible = StatusButton;

                DevelopmentTreeAdd1.Visible = StatusButton;
                DevelopmentTreeAdd2.Visible = StatusButton;
                DevelopmentTreeAdd3.Visible = StatusButton;
                DevelopmentTreeLower1.Visible = StatusButton;
                DevelopmentTreeLower2.Visible = StatusButton;
                DevelopmentTreeLower3.Visible = StatusButton;
                DevelopmentTreePointsLabel.Visible = StatusButton;
                DevelopmentTreePointsValueLabel.Visible = StatusButton;

                NameText.Visible = StatusButton;

                BackpackBox.Visible = StatusButton;
                BackpackLabel.Visible = StatusButton;
                BackpackNumber.Visible = StatusButton;
                BackpackNumberLabel.Visible = StatusButton;

                
            }
        }
        private void StrengthMethod()
        {
            int Strength_ = Strength + BaffStrength + BaffClassStrength;
            StrengthValue.Text = Strength_.ToString();
            АthleteValue.Text = Strength_.ToString();


            Properties.Settings.Default.Strength = Strength;
            Properties.Settings.Default.Save();

            SetPoint();
            WeightSet();
            CharismaMethod();
            WalkingStickMethod();
            StrengthRollValue.Text = Convert.ToInt32(Math.Floor((Strength_ - 10) * 0.5)).ToString();
            АthleteRollValue.Text = Convert.ToInt32(Math.Floor((Strength_ - 10) * 0.5)).ToString();
        }

        private void AgilityMethod()
        {
            int Stick;
            int Agility_ = Agility + BaffAgility + BaffClassAgility;
            AgilityValue.Text = Agility_.ToString();
            if (Agility_ < 20)
            {
                Stick = 1;
            }
            else if (Agility_ < 30)
            {
                Stick = 2;
            }
            else
            {
                Stick = 3;
            }
            StickLabel.Text = Stick.ToString();
            СounteractionLabel.Text = Stick.ToString();

            Properties.Settings.Default.Agility = Agility;
            Properties.Settings.Default.Save();

            SetPointsAgility();
            SetPoint();
            AgilityRollValue.Text = Convert.ToInt32(Math.Floor((Agility_ - 10) * 0.5)).ToString();
            if (RaceName.Text == "Три-крин" && !CheckArmorTricrin.Checked)
            {
                BaffKD = 13 + Convert.ToInt32(Math.Floor((Agility_ - 10) * 0.5));
                SetTotalKD();

            }
            else if (RaceName.Text == "Три-крин")
            {
                BaffKD = 0;
                SetTotalKD();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StrengthAdd_Click(object sender, EventArgs e)
        {
            if (PointsNow > 0 && MaxLock > Strength)
            {
                Strength += 1;
                StrengthMethod();
            }
        }

        private void StrengthLower_Click(object sender, EventArgs e)
        {
            if (Strength > MinLock)
            {
                Strength -= 1;
                StrengthMethod();
            }
        }

        private void IntellectAdd_Click(object sender, EventArgs e)
        {
            if (PointsNow > 0 && MaxLock > Intellect)
            {
                Intellect += 1;
                IntellectMethod();
            }
        }

        private void IntellectLower_Click(object sender, EventArgs e)
        {
            if (Intellect > MinLock)
            {
                Intellect -= 1;
                IntellectMethod();
            }
        }
        private void BodyAdd_Click(object sender, EventArgs e)
        {
            if (PointsNow > 0 && MaxLock > Body)
            {
                Body += 1;
                BodyMethod();
            }
        }

        private void BodyLower_Click(object sender, EventArgs e)
        {
            if (Body > MinLock)
            {
                Body -= 1;
                BodyMethod();
            }
        }

        private void BodyMethod()
        {
            int Body_ = Body + BaffBody + BaffClassBody;
            BodyValueLabel.Text = Body_.ToString();

            Properties.Settings.Default.Body = Body;
            Properties.Settings.Default.Save();

            SetPoint();
            HealthSet();
            SetTotalKD();
            BodyRollValue.Text = Convert.ToInt32(Math.Floor((Body_ - 10) * 0.5)).ToString();
        }

        private void WisdomAdd_Click(object sender, EventArgs e)
        {
            if (PointsNow > 0 && MaxLock > Wisdom)
            {
                Wisdom += 1;
                WisdomMethod();
            }
        }

        private void WisdomLower_Click(object sender, EventArgs e)
        {
            if (Wisdom > MinLock)
            {
                Wisdom -= 1;
                WisdomMethod();
            }
        }
        private void CharismaAdd_Click(object sender, EventArgs e)
        {
            if (PointsNow > 0 && MaxLock > Charisma)
            {
                Charisma += 1;
                CharismaMethod();
            }
        }

        private void CharismaLower_Click(object sender, EventArgs e)
        {
            if (Charisma > MinLock)
            {
                Charisma -= 1;
                CharismaMethod();
            }
        }







        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LeghtValue_Click(object sender, EventArgs e)
        {

        }


        private void АthleteValue_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }


        private void label5_Click(object sender, EventArgs e)
        {

        }











        private void AgilitySkilsEdit_CheckedChanged(object sender, EventArgs e)
        {
            bool StatusButton = AgilitySkilsEdit.Checked;

            if (StatusButton == true)
            {
                AgilityPointsLebel.Visible = StatusButton;
                AgilityPointsValueLebel.Visible = StatusButton;

                AcrobaticsAdd.Visible = StatusButton;
                AcrobaticsLower.Visible = StatusButton;

                SleightOfHandAdd.Visible = StatusButton;
                SleightOfHandLower.Visible = StatusButton;

                StealthAdd.Visible = StatusButton;
                StealthLower.Visible = StatusButton;
            }
            if (StatusButton == false)
            {
                AgilityPointsLebel.Visible = StatusButton;
                AgilityPointsValueLebel.Visible = StatusButton;

                AcrobaticsAdd.Visible = StatusButton;
                AcrobaticsLower.Visible = StatusButton;

                SleightOfHandAdd.Visible = StatusButton;
                SleightOfHandLower.Visible = StatusButton;

                StealthAdd.Visible = StatusButton;
                StealthLower.Visible = StatusButton;
            }


        }

        private void SetPointsAgility()
        {
            PointsAgilitySkills = Convert.ToInt32(Math.Floor((BaffAgility + Agility) * 3 * 0.75));

            PointsAgilitySkillsNow = PointsAgilitySkills - (Acrobatics + SleightOfHand + Stealth);
            AgilityPointsValueLebel.Text = PointsAgilitySkillsNow.ToString();

            int Acrobatics_ = Acrobatics + BaffClassAcrobatics;
            AcrobaticsValueLabel.Text = Acrobatics_.ToString();
            Properties.Settings.Default.Acrobatics = Acrobatics;
            AcrobaticsRollValue.Text = Convert.ToInt32(Math.Floor((Acrobatics - 10) * 0.5)).ToString();

            int SleightOfHand_ = SleightOfHand + BaffClassSleightOfHand;
            SleightOfHandValueLabel.Text = SleightOfHand_.ToString();
            Properties.Settings.Default.SleightOfHand = SleightOfHand;
            SleightOfHandRollValue.Text = Convert.ToInt32(Math.Floor((SleightOfHand - 10) * 0.5)).ToString();

            int Stealth_ = Stealth + BaffClassStealth;
            StealthValueLabel.Text = Stealth_.ToString();
            Properties.Settings.Default.Stealth = Stealth;
            StealthRollValue.Text = Convert.ToInt32(Math.Floor((Stealth_ - 10) * 0.5)).ToString();

            Properties.Settings.Default.Save();

            if (PointsAgilitySkillsNow < 0)
            {
                AgilityPointsValueLebel.ForeColor = Color.Red;
                AgilityPointsLebel.Visible = true;
                AgilityPointsValueLebel.Visible = true;
            }
            else
            {
                AgilityPointsValueLebel.ForeColor = Color.Black;
            }
            if (PointsAgilitySkillsNow > 0 && !AgilitySkilsEdit.Checked)
            {
                AgilityPointsLebel.Visible = false;
                AgilityPointsValueLebel.Visible = false;
            }
            WalkingStickMethod();
        }

        private void AgilityPointsValueLebel_Click(object sender, EventArgs e)
        {

        }

        private void AgilityAdd_Click_1(object sender, EventArgs e)
        {
            if (PointsNow > 0 && MaxLock > Agility)
            {
                Agility += 1;
                AgilityMethod();
            }
        }

        private void AgilityLower_Click_1(object sender, EventArgs e)
        {
            if (Agility > MinLock)
            {
                Agility -= 1;
                AgilityMethod();
            }
        }

        private void AcrobaticsAdd_Click(object sender, EventArgs e)
        {
            if (PointsAgilitySkillsNow > 0 && MaxLockSkills > Acrobatics)
            {
                Acrobatics += 1;
                SetPointsAgility();
            }
        }

        private void AcrobaticsLower_Click(object sender, EventArgs e)
        {
            {
                if (Acrobatics > MinLockSkills)
                {
                    Acrobatics -= 1;
                    SetPointsAgility();
                }
            }
        }

        private void SleightOfHandAdd_Click(object sender, EventArgs e)
        {
            if (PointsAgilitySkillsNow > 0 && MaxLockSkills > SleightOfHand)
            {
                SleightOfHand += 1;
                SetPointsAgility();
            }
        }

        private void SleightOfHandLower_Click(object sender, EventArgs e)
        {
            if (SleightOfHand > MinLockSkills)
            {
                SleightOfHand -= 1;
                SetPointsAgility();
            }
        }

        private void StealthAdd_Click(object sender, EventArgs e)
        {
            if (PointsAgilitySkillsNow > 0 && MaxLockSkills > Stealth)
            {
                Stealth += 1;
                SetPointsAgility();
            }
        }

        private void StealthLower_Click(object sender, EventArgs e)
        {
            if (Stealth > MinLockSkills)
            {
                Stealth -= 1;
                SetPointsAgility();
            }
        }








        private void IntellectSkillsEdit_CheckedChanged(object sender, EventArgs e)
        {
            bool StatusButton = IntellectSkillsEdit.Checked;

            if (StatusButton == true)
            {
                IntellectPointsValueLebel.Visible = StatusButton;
                IntellectLabel.Visible = StatusButton;

                MagicAdd.Visible = StatusButton;
                MagicLower.Visible = StatusButton;

                ReligionAdd.Visible = StatusButton;
                ReligionLower.Visible = StatusButton;

                NatureAdd.Visible = StatusButton;
                NatureLower.Visible = StatusButton;

                HistoryAdd.Visible = StatusButton;
                HistoryLower.Visible = StatusButton;

                InvestigationAdd.Visible = StatusButton;
                InvestigationLower.Visible = StatusButton;

                TechnologyAdd.Visible = StatusButton;
                TechnologyLower.Visible = StatusButton;


            }
            if (StatusButton == false)
            {
                IntellectPointsValueLebel.Visible = StatusButton;
                IntellectLabel.Visible = StatusButton;

                MagicAdd.Visible = StatusButton;
                MagicLower.Visible = StatusButton;

                ReligionAdd.Visible = StatusButton;
                ReligionLower.Visible = StatusButton;

                NatureAdd.Visible = StatusButton;
                NatureLower.Visible = StatusButton;

                HistoryAdd.Visible = StatusButton;
                HistoryLower.Visible = StatusButton;

                InvestigationAdd.Visible = StatusButton;
                InvestigationLower.Visible = StatusButton;

                TechnologyAdd.Visible = StatusButton;
                TechnologyLower.Visible = StatusButton;
            }
        }

        private void IntellectMethod()
        {
            int Intellect_ = Intellect + BaffIntellect + BaffClassIntellect;
            IntellectValueLabel.Text = Intellect_.ToString();

            Properties.Settings.Default.Intellect = Intellect;
            Properties.Settings.Default.Save();

            SetPoint();
            SetPointsIntellect();
            IntellectRollValue.Text = Convert.ToInt32(Math.Floor((Intellect - 10) * 0.5)).ToString();

            MentalKDLabel.Text = Convert.ToString(Intellect * 2);
        }


        private void SetPointsIntellect()
        {
            PointsIntellectSkills = Convert.ToInt32(Math.Floor((BaffIntellect + Intellect) * 6 * 0.75));

            PointsIntellectSkillsNow = PointsIntellectSkills - (Magic + Religion + Nature + History + Investigation + Technology);
            IntellectPointsValueLebel.Text = PointsIntellectSkillsNow.ToString();

            MagicValueLabel.Text = Magic.ToString();
            Properties.Settings.Default.Magic = Magic;
            MagicRollValue.Text = Convert.ToInt32(Math.Floor((Magic - 10) * 0.5)).ToString();

            ReligionValueLabel.Text = Religion.ToString();
            Properties.Settings.Default.Religion = Religion;
            ReligionRollValue.Text = Convert.ToInt32(Math.Floor((Religion - 10) * 0.5)).ToString();

            NatureValueLabel.Text = Nature.ToString();
            Properties.Settings.Default.Nature = Nature;
            NatureRollValue.Text = Convert.ToInt32(Math.Floor((Nature - 10) * 0.5)).ToString();

            HistoryValueLabel.Text = History.ToString();
            Properties.Settings.Default.History = History;
            HistoryRollValue.Text = Convert.ToInt32(Math.Floor((History - 10) * 0.5)).ToString();

            int Investigation_ = Investigation + BaffClassInvestigation;
            InvestigationValueLabel.Text = Investigation_.ToString();
            Properties.Settings.Default.Investigation = Investigation;
            InvestigationRollValue.Text = Convert.ToInt32(Math.Floor((Investigation - 10) * 0.5)).ToString();

            TechnologyValueLabel.Text = Technology.ToString();
            Properties.Settings.Default.Technology = Technology;
            TechnologyRollValue.Text = Convert.ToInt32(Math.Floor((Technology - 10) * 0.5)).ToString();

            Properties.Settings.Default.Save();

            if (PointsIntellectSkillsNow < 0)
            {
                IntellectPointsValueLebel.ForeColor = Color.Red;
                IntellectPointsValueLebel.Visible = true;
                IntellectLabel.Visible = true;
            }
            else
            {
                IntellectPointsValueLebel.ForeColor = Color.Black;
            }
            if (PointsIntellectSkillsNow > 0 && !IntellectSkillsEdit.Checked)
            {
                IntellectPointsValueLebel.Visible = false;
                IntellectLabel.Visible = false;
            }

        }

        private void MagicAdd_Click(object sender, EventArgs e)
        {
            if (PointsIntellectSkillsNow > 0 && MaxLockSkills > Magic)
            {
                Magic += 1;
                SetPointsIntellect();
            }
        }

        private void MagicLower_Click(object sender, EventArgs e)
        {
            if (Magic > MinLockSkills)
            {
                Magic -= 1;
                SetPointsIntellect();
            }
        }

        private void ReligionAdd_Click(object sender, EventArgs e)
        {
            if (PointsIntellectSkillsNow > 0 && MaxLockSkills > Religion)
            {
                Religion += 1;
                SetPointsIntellect();
            }
        }

        private void ReligionLower_Click(object sender, EventArgs e)
        {
            if (Religion > MinLockSkills)
            {
                Religion -= 1;
                SetPointsIntellect();
            }
        }

        private void NatureAdd_Click(object sender, EventArgs e)
        {
            if (PointsIntellectSkillsNow > 0 && MaxLockSkills > Nature)
            {
                Nature += 1;
                SetPointsIntellect();
            }
        }

        private void NatureLower_Click(object sender, EventArgs e)
        {
            if (Nature > MinLockSkills)
            {
                Nature -= 1;
                SetPointsIntellect();
            }
        }

        private void HistoryAdd_Click(object sender, EventArgs e)
        {
            if (PointsIntellectSkillsNow > 0 && MaxLockSkills > History)
            {
                History += 1;
                SetPointsIntellect();
            }
        }

        private void HistoryLower_Click(object sender, EventArgs e)
        {
            if (History > MinLockSkills)
            {
                History -= 1;
                SetPointsIntellect();
            }
        }

        private void InvestigationAdd_Click(object sender, EventArgs e)
        {
            if (PointsIntellectSkillsNow > 0 && MaxLockSkills > Investigation)
            {
                Investigation += 1;
                SetPointsIntellect();
            }
        }

        private void InvestigationLower_Click(object sender, EventArgs e)
        {
            if (Investigation > MinLockSkills)
            {
                Investigation -= 1;
                SetPointsIntellect();
            }
        }

        private void TechnologyAdd_Click(object sender, EventArgs e)
        {
            if (PointsIntellectSkillsNow > 0 && MaxLockSkills > Technology)
            {
                Technology += 1;
                SetPointsIntellect();
            }
        }

        private void TechnologyLower_Click(object sender, EventArgs e)
        {
            if (Technology > MinLockSkills)
            {
                Technology -= 1;
                SetPointsIntellect();
            }
        }










        private void WisdomSkillsEdit_CheckedChanged(object sender, EventArgs e)
        {
            bool StatusButton = WisdomSkillsEdit.Checked;

            if (StatusButton == true)
            {
                WisdomLabel.Visible = StatusButton;
                WisdomPointsValueLebel.Visible = StatusButton;

                MedicineAdd.Visible = StatusButton;
                MedicineLower.Visible = StatusButton;

                PerceptionAdd.Visible = StatusButton;
                PerceptionLower.Visible = StatusButton;

                InsightAdd.Visible = StatusButton;
                InsightLower.Visible = StatusButton;

                SurvivalAdd.Visible = StatusButton;
                SurvivalLower.Visible = StatusButton;

                TreatmentOfAnimalsAdd.Visible = StatusButton;
                TreatmentOfAnimalsLower.Visible = StatusButton;
            }
            if (StatusButton == false)
            {
                WisdomLabel.Visible = StatusButton;
                WisdomPointsValueLebel.Visible = StatusButton;

                MedicineAdd.Visible = StatusButton;
                MedicineLower.Visible = StatusButton;

                PerceptionAdd.Visible = StatusButton;
                PerceptionLower.Visible = StatusButton;

                InsightAdd.Visible = StatusButton;
                InsightLower.Visible = StatusButton;

                SurvivalAdd.Visible = StatusButton;
                SurvivalLower.Visible = StatusButton;

                TreatmentOfAnimalsAdd.Visible = StatusButton;
                TreatmentOfAnimalsLower.Visible = StatusButton;
            }
        }

        private void WisdomMethod()
        {
            int Wisdom_ = Wisdom + BaffWisdom + BaffClassWisdom;
            WisdomValueLabel.Text = Wisdom_.ToString();

            Properties.Settings.Default.Wisdom = Wisdom;
            Properties.Settings.Default.Save();

            SetPointsWisdom();
            SetPoint();
            WisdomRollValue.Text = Convert.ToInt32(Math.Floor((Wisdom - 10) * 0.5)).ToString();
        }
        private void SetPointsWisdom()
        {
            PointsWisdomSkills = Convert.ToInt32(Math.Floor((BaffWisdom + Wisdom) * 5 * 0.75));

            PointsWisdomSkillsNow = PointsWisdomSkills - (Medicine + Perception + Insight + Survival + TreatmentOfAnimals);
            WisdomPointsValueLebel.Text = PointsWisdomSkillsNow.ToString();

            MedicineValueLabel.Text = Medicine.ToString();
            Properties.Settings.Default.Medicine = Medicine;
            MedicineRollValue.Text = Convert.ToInt32(Math.Floor((Medicine - 10) * 0.5)).ToString();

            PerceptionValueLabel.Text = Perception.ToString();
            Properties.Settings.Default.Perception = Perception;
            PerceptionRollValue.Text = Convert.ToInt32(Math.Floor((Perception - 10) * 0.5)).ToString();

            InsightValueLabel.Text = Insight.ToString();
            Properties.Settings.Default.Insight = Insight;
            InsightRollValue.Text = Convert.ToInt32(Math.Floor((Insight - 10) * 0.5)).ToString();

            SurvivalValueLabel.Text = Survival.ToString();
            Properties.Settings.Default.Survival = Survival;
            SurviveRollValue.Text = Convert.ToInt32(Math.Floor((Survival - 10) * 0.5)).ToString();

            TreatmentOfAnimalsValueLabel.Text = TreatmentOfAnimals.ToString();
            Properties.Settings.Default.TreatmentOfAnimals = TreatmentOfAnimals;
            TreatmentOfAnimalsRollValue.Text = Convert.ToInt32(Math.Floor((TreatmentOfAnimals - 10) * 0.5)).ToString();

            Properties.Settings.Default.Save();

            if (PointsWisdomSkillsNow < 0)
            {
                WisdomPointsValueLebel.ForeColor = Color.Red;
                WisdomPointsValueLebel.Visible = true;
                WisdomLabel.Visible = true;
            }
            else
            {
                WisdomPointsValueLebel.ForeColor = Color.Black;
            }
            if (PointsWisdomSkillsNow > 0 && !WisdomSkillsEdit.Checked)
            {
                WisdomPointsValueLebel.Visible = false;
                WisdomLabel.Visible = false;
            }
        }

        private void MedicineAdd_Click(object sender, EventArgs e)
        {
            if (PointsWisdomSkillsNow > 0 && MaxLockSkills > Medicine)
            {
                Medicine += 1;
                SetPointsWisdom();
            }
        }

        private void MedicineLower_Click(object sender, EventArgs e)
        {
            if (Medicine > MinLockSkills)
            {
                Medicine -= 1;
                SetPointsWisdom();
            }
        }

        private void PerceptionAdd_Click(object sender, EventArgs e)
        {
            if (PointsWisdomSkillsNow > 0 && MaxLockSkills > Perception)
            {
                Perception += 1;
                SetPointsWisdom();
            }
        }

        private void PerceptionLower_Click(object sender, EventArgs e)
        {
            if (Perception > MinLockSkills)
            {
                Perception -= 1;
                SetPointsWisdom();
            }
        }

        private void InsightAdd_Click(object sender, EventArgs e)
        {
            if (PointsWisdomSkillsNow > 0 && MaxLockSkills > Insight)
            {
                Insight += 1;
                SetPointsWisdom();
            }
        }

        private void InsightLower_Click(object sender, EventArgs e)
        {
            if (Insight > MinLockSkills)
            {
                Insight -= 1;
                SetPointsWisdom();
            }
        }

        private void SurvivalAdd_Click(object sender, EventArgs e)
        {
            if (PointsWisdomSkillsNow > 0 && MaxLockSkills > Survival)
            {
                Survival += 1;
                SetPointsWisdom();
            }
        }

        private void SurvivalLower_Click(object sender, EventArgs e)
        {
            if (Survival > MinLockSkills)
            {
                Survival -= 1;
                SetPointsWisdom();
            }
        }

        private void TreatmentOfAnimalsAdd_Click(object sender, EventArgs e)
        {
            if (PointsWisdomSkillsNow > 0 && MaxLockSkills > TreatmentOfAnimals)
            {
                TreatmentOfAnimals += 1;
                SetPointsWisdom();
            }
        }

        private void TreatmentOfAnimalsLower_Click(object sender, EventArgs e)
        {
            if (TreatmentOfAnimals > MinLockSkills)
            {
                TreatmentOfAnimals -= 1;
                SetPointsWisdom();
            }
        }










        private void CharismaMethod()
        {
            int Strength_ = Strength + BaffStrength + BaffClassStrength;
            var BaffAthleticaToCharisma = (Strength_ - 10 ) / 5;
            int Charisma_ = Charisma + BaffCharisma + BaffClassCharisma + BaffAthleticaToCharisma;
            CharismaValueLabel.Text = Charisma_.ToString();
            Properties.Settings.Default.Charisma = Charisma;
            Properties.Settings.Default.Save();

            SetPointsCharisma();
            SetPoint();

            CharismaRollValue.Text = Convert.ToInt32(Math.Floor((Charisma - 10) * 0.5)).ToString();
        }
        private void SetPointsCharisma()
        {
            PointsCharismaSkills = Convert.ToInt32(Math.Floor(BaffCharisma + Charisma * 4 * 0.75));

            PointsCharismaSkillsNow = PointsCharismaSkills - (Deception + Intimidation + Speech + Persuasion);
            CharismaPointsValueLebel.Text = PointsCharismaSkillsNow.ToString();

            int Deception_ = Deception + BaffClassDeception;
            DeceptionValueLabel.Text = Deception_.ToString();
            Properties.Settings.Default.Deception = Deception;
            DeceptionRollValue.Text = Convert.ToInt32(Math.Floor((Deception - 10) * 0.5)).ToString();

            IntimidationValueLabel.Text = Intimidation.ToString();
            Properties.Settings.Default.Intimidation = Intimidation;
            IntimidationRollValue.Text = Convert.ToInt32(Math.Floor((Intimidation - 10) * 0.5)).ToString();

            int Speech_ = Speech + BaffClassSpeech;
            SpeechValueLabel.Text = Speech_.ToString();
            Properties.Settings.Default.Speech = Speech;
            SpeechRollValue.Text = Convert.ToInt32(Math.Floor((Speech - 10) * 0.5)).ToString();

            int Persuasion_ = Persuasion + BaffClassPersuasion;
            PersuasionValueLabel.Text = Persuasion_.ToString();
            Properties.Settings.Default.Persuasion = Persuasion;
            PersuasionRollValue.Text = Convert.ToInt32(Math.Floor((Persuasion - 10) * 0.5)).ToString();

            Properties.Settings.Default.Save();

            if (PointsCharismaSkillsNow < 0)
            {
                CharismaPointsValueLebel.ForeColor = Color.Red;
                CharismaPointsValueLebel.Visible = true;
                CharismaLabel.Visible = true;
            }
            else
            {
                CharismaPointsValueLebel.ForeColor = Color.Black;
            }
            if (PointsCharismaSkillsNow > 0 && !CharismaSkillsEdit.Checked)
            {
                CharismaPointsValueLebel.Visible = false;
                CharismaLabel.Visible = false;
            }
        }
        private void CharismaSkillsEdit_CheckedChanged(object sender, EventArgs e)
        {
            bool StatusButton = CharismaSkillsEdit.Checked;

            if (StatusButton == true)
            {
                CharismaLabel.Visible = StatusButton;
                CharismaPointsValueLebel.Visible = StatusButton;

                DeceptionAdd.Visible = StatusButton;
                DeceptionLower.Visible = StatusButton;

                IntimidationAdd.Visible = StatusButton;
                IntimidationLower.Visible = StatusButton;

                SpeechAdd.Visible = StatusButton;
                SpeechLower.Visible = StatusButton;

                PersuasionAdd.Visible = StatusButton;
                PersuasionLower.Visible = StatusButton;
            }
            if (StatusButton == false)
            {
                CharismaLabel.Visible = StatusButton;
                CharismaPointsValueLebel.Visible = StatusButton;

                DeceptionAdd.Visible = StatusButton;
                DeceptionLower.Visible = StatusButton;

                IntimidationAdd.Visible = StatusButton;
                IntimidationLower.Visible = StatusButton;

                SpeechAdd.Visible = StatusButton;
                SpeechLower.Visible = StatusButton;

                PersuasionAdd.Visible = StatusButton;
                PersuasionLower.Visible = StatusButton;
            }

        }

        private void DeceptionAdd_Click(object sender, EventArgs e)
        {
            if (PointsCharismaSkillsNow > 0 && MaxLockSkills > Deception)
            {
                Deception += 1;
                SetPointsCharisma();
            }
        }

        private void DeceptionLower_Click(object sender, EventArgs e)
        {
            if (Deception > MinLockSkills)
            {
                Deception -= 1;
                SetPointsCharisma();
            }
        }

        private void IntimidationAdd_Click(object sender, EventArgs e)
        {
            if (PointsCharismaSkillsNow > 0 && MaxLockSkills > Intimidation)
            {
                Intimidation += 1;
                SetPointsCharisma();
            }
        }

        private void IntimidationLower_Click(object sender, EventArgs e)
        {
            if (Intimidation > MinLockSkills)
            {
                Intimidation -= 1;
                SetPointsCharisma();
            }
        }

        private void SpeechAdd_Click(object sender, EventArgs e)
        {
            if (PointsCharismaSkillsNow > 0 && MaxLockSkills > Speech)
            {
                Speech += 1;
                SetPointsCharisma();
            }
        }

        private void SpeechLower_Click(object sender, EventArgs e)
        {
            if (Speech > MinLockSkills)
            {
                Speech -= 1;
                SetPointsCharisma();
            }
        }

        private void PersuasionAdd_Click(object sender, EventArgs e)
        {
            if (PointsCharismaSkillsNow > 0 && MaxLockSkills > Persuasion)
            {
                Persuasion += 1;
                SetPointsCharisma();
            }
        }

        private void PersuasionLower_Click(object sender, EventArgs e)
        {
            if (Persuasion > MinLockSkills)
            {
                Persuasion -= 1;
                SetPointsCharisma();
            }
        }

        private void RaceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RaceName.Text = RaceBox.Text;

            Properties.Settings.Default.Race = RaceName.Text;
            Properties.Settings.Default.Save();

            SetRace();

        }

        private void SetRace()
        {
            BaffMagicDamage = 1;
            BaffPhysDamage = 1;
            WalkingStickBaffRace = 0;

            CheckArmorTricrin.Visible = false;
            if (RaceName.Text == "Человек")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 1;
                BaffAgility = 1;
                BaffBody = 1;
                BaffIntellect = 1;
                BaffWisdom = 1;
                BaffCharisma = 1;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Полурослик")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 2;
                BaffBody = 0;
                BaffIntellect = 0;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Гном")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = -1;
                BaffAgility = 0;
                BaffBody = -1;
                BaffIntellect = 2;
                BaffWisdom = 0;
                BaffCharisma = 1;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Кованый")
            {
                BaffKD = 15;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 2;
                BaffIntellect = 0;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 1;
                WalkingStickBaffRace = 0.5;
            }

            if (RaceName.Text == "Эльф")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 2;
                BaffBody = 0;
                BaffIntellect = 0;
                BaffWisdom = 1;
                BaffCharisma = 0;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Дженази")
            {
                BaffMagicDamage = 2;
                BaffPhysDamage = 0.5;
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 1;
                BaffIntellect = 0;
                BaffWisdom = 2;
                BaffCharisma = 0;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Орк")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 3;
                BaffAgility = 0;
                BaffBody = 3;
                BaffIntellect = -3;
                BaffWisdom = 1;
                BaffCharisma = 0;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Голиаф")
            {
                BaffKD = 5;
                BaffWeight = 0;
                BaffStrength = 3;
                BaffAgility = 0;
                BaffBody = 2;
                BaffIntellect = 0;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Полуэльф")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 0;
                BaffIntellect = 1;
                BaffWisdom = 0;
                BaffCharisma = 2;
                BaffPoints = 1;
            }

            if (RaceName.Text == "Полуорк")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 1;
                BaffAgility = 0;
                BaffBody = 2;
                BaffIntellect = 0;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 1;
            }

            if (RaceName.Text == "Дварф")
            {
                BaffKD = 10;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 0;
                BaffIntellect = 2;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Кенку")
            { 
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 2;
                BaffBody = 0;
                BaffIntellect = 0;
                BaffWisdom = 1;
                BaffCharisma = 0;
                BaffPoints = 1;
            }

            if (RaceName.Text == "Людоящер")
            { 
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 2;
                BaffIntellect = 0;
                BaffWisdom = 1;
                BaffCharisma = 0;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Гифф")
            {
                BaffKD = 0;
                BaffWeight = 500;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 0;
                BaffIntellect = 0;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 3;
            }

            if (RaceName.Text == "Три-крин")    
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 0;
                BaffIntellect = 0;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 3;
                CheckArmorTricrin.Visible = true;
            }

            if (RaceName.Text == "Грунг")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 2;
                BaffBody = 1;
                BaffIntellect = 0;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 0;
            }

            if (RaceName.Text == "Тортл")
            {
                BaffKD = 30;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 1;
                BaffIntellect = 0;
                BaffWisdom = 2;
                BaffCharisma = 0;
                BaffPoints = 0;
                WalkingStickBaffRace = 0.5;
            }

            if (RaceName.Text == "Нет")
            {
                BaffKD = 0;
                BaffWeight = 0;
                BaffStrength = 0;
                BaffAgility = 0;
                BaffBody = 0;
                BaffIntellect = 0;
                BaffWisdom = 0;
                BaffCharisma = 0;
                BaffPoints = 0;
            }


            StrengthMethod();
            AgilityMethod();
            BodyMethod();
            IntellectMethod();
            WisdomMethod();
            CharismaMethod();
            SetPoint();
            WeightSet();
        }

        private void СlassBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassName.Text = СlassBox.Text;

            Properties.Settings.Default.Class = ClassName.Text;
            Properties.Settings.Default.Save();

            SetClass();
            SetBaffClassTree();
        }
        private void SetBaffClassTree()
        {
            BaffClassStrength1 = 0;
            BaffClassAgility1 = 0;
            BaffClassAcrobatics1 = 0;
            BaffClassSleightOfHand1 = 0;
            BaffClassStealth1 = 0;
            BaffClassBody1 = 0;
            BaffClassIntellect1 = 0;
            BaffClassInvestigation1 = 0;
            BaffClassCharisma1 = 0;
            BaffClassDeception1 = 0;
            BaffClassSpeech1 = 0;
            BaffClassPersuasion1 = 0;

            BaffClassStrength2 = 0;
            BaffClassAgility2 = 0;
            BaffClassAcrobatics2 = 0;
            BaffClassSleightOfHand2 = 0;
            BaffClassStealth2 = 0;
            BaffClassBody2 = 0;
            BaffClassIntellect2 = 0;
            BaffClassInvestigation2 = 0;
            BaffClassCharisma2 = 0;
            BaffClassDeception2 = 0;
            BaffClassSpeech2 = 0;
            BaffClassPersuasion2 = 0;

            BaffClassStrength3 = 0;
            BaffClassAgility3 = 0;
            BaffClassAcrobatics3 = 0;
            BaffClassSleightOfHand3 = 0;
            BaffClassStealth3 = 0;
            BaffClassBody3 = 0;
            BaffClassIntellect3 = 0;
            BaffClassInvestigation3 = 0;
            BaffClassCharisma3 = 0;
            BaffClassDeception3 = 0;
            BaffClassSpeech3 = 0;
            BaffClassPersuasion3 = 0;

            BaffClassAcrobaticsAdd = 0;
            BaffClassDeceptionAdd = 0;
        }



        private void SetClass()
        {
            if (ClassName.Text == "Изобретатель")
            {
                BaffHealth = 1;

                DevelopmentTreeNameLabel1.Text = "Кузнец";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassStrength1 = 0;
                        break;

                    case 1:
                        BaffClassStrength1 = 1;
                        break;

                    case 2:
                        BaffClassStrength1 = 3;
                        break;

                    case 3:
                        BaffClassStrength1 = 4;
                        break;

                    case 4:
                        BaffClassStrength1 = 5;
                        break;

                    case 5:
                        BaffClassStrength1 = 6;
                        break;

                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Алхимик";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassIntellect2 = 0;
                        BaffClassWisdom2 = 0;
                        break;

                    case 1:
                        BaffClassIntellect2 = 1;
                        BaffClassWisdom2 = 1;
                        break;

                    case 2:
                        BaffClassIntellect2 = 1;
                        BaffClassWisdom2 = 2;
                        break;

                    case 3:
                        BaffClassIntellect2 = 1;
                        BaffClassWisdom2 = 3;
                        break;

                    case 4:
                        BaffClassIntellect2 = 1;
                        BaffClassWisdom2 = 4;
                        break;

                    case 5:
                        BaffClassIntellect2 = 1;
                        BaffClassWisdom2 = 5;
                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Магический техник";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassIntellect3 = 0;
                        BaffClassWisdom3 = 0;
                        break;

                    case 1:
                        BaffClassIntellect3 = 1;
                        BaffClassWisdom3 = 1;
                        break;

                    case 2:
                        BaffClassIntellect3 = 2;
                        BaffClassWisdom3 = 1;
                        break;

                    case 3:
                        BaffClassIntellect3 = 3;
                        BaffClassWisdom3 = 1;
                        break;

                    case 4:
                        BaffClassIntellect3 = 3;
                        BaffClassWisdom3 = 2;
                        break;

                    case 5:
                        BaffClassIntellect3 = 4;
                        BaffClassWisdom3 = 2;
                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Боец")
            {
                BaffHealth = 1.5;

                DevelopmentTreeNameLabel1.Text = "Путь клинка";
                /*switch (PointsTree1)
                {
                    case 0:

                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
                */

                DevelopmentTreeNameLabel2.Text = "Тактик";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassIntellect2 = 0;
                        BaffClassWisdom2 = 0;
                        BaffClassInvestigation2 = 0;
                        BaffClassAgility2 = 0;
                        break;

                    case 1:
                        BaffClassIntellect2 = 1;
                        BaffClassWisdom2 = 1;
                        BaffClassInvestigation2 = 2;
                        BaffClassAgility2 = 0;
                        break;

                    case 2:
                        BaffClassIntellect2 = 2;
                        BaffClassWisdom2 = 1;
                        BaffClassInvestigation2 = 2;
                        BaffClassAgility2 = 1;
                        break;

                    case 3:
                        BaffClassIntellect2 = 3;
                        BaffClassWisdom2 = 1;
                        BaffClassInvestigation2 = 2;
                        BaffClassAgility2 = 2;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Магический воин";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassIntellect3 = 0;
                        BaffClassWisdom3 = 0;
                        break;

                    case 1:
                        BaffClassIntellect3 = 1;
                        BaffClassWisdom3 = 0;
                        break;

                    case 2:
                        BaffClassIntellect3 = 2;
                        BaffClassWisdom3 = 0;
                        break;

                    case 3:
                        BaffClassIntellect3 = 2;
                        BaffClassWisdom3 = 1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Боевой Бард")
            {
                BaffHealth = 0.8;

                DevelopmentTreeNameLabel1.Text = "Скоромох";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassCharisma1 = 0;
                        BaffClassSpeech1 = 0;
                        BaffClassAcrobatics1 = 0;
                        break;

                    case 1:
                        BaffClassCharisma1 = 1;
                        BaffClassSpeech1 = 1;
                        BaffClassAcrobatics1 = 2;
                        break;

                    case 2:
                        BaffClassCharisma1 = 2;
                        BaffClassSpeech1 = 3;
                        BaffClassAcrobatics1 = 4;
                        break;

                    case 3:
                        BaffClassCharisma1 = 4;
                        BaffClassSpeech1 = 6;
                        BaffClassAcrobatics1 = 6;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Магия слова и песни";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassCharisma2 = 0;
                        BaffClassIntellect2 = 0;
                        BaffClassWisdom2 = 0;
                        break;

                    case 1:
                        BaffClassCharisma2 = 1;
                        BaffClassIntellect2 = 0;
                        BaffClassWisdom2 = 0;
                        break;

                    case 2:
                        BaffClassCharisma2 = 2;
                        BaffClassIntellect2 = 1;
                        BaffClassWisdom2 = 0;
                        break;

                    case 3:
                        BaffClassCharisma2 = 3;
                        BaffClassIntellect2 = 1;
                        BaffClassWisdom2 = 1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Мотиватор";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassCharisma3 = 0;
                        break;

                    case 1:
                        BaffClassCharisma3 = 1;
                        break;

                    case 2:
                        BaffClassCharisma3 = 3;
                        break;

                    case 3:
                        BaffClassCharisma3 = 5;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Варвар")
            {
                BaffHealth = 2;

                DevelopmentTreeNameLabel1.Text = "Слепой гнев";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassStrength1 = 0;
                        BaffClassAgility1 = 0;
                        BaffClassWisdom1 = 0;
                        break;

                    case 1:
                        BaffClassStrength1 = 1;
                        BaffClassAgility1 = 1;
                        BaffClassWisdom1 = 0;
                        break;

                    case 2:
                        BaffClassStrength1 = 1;
                        BaffClassAgility1 = 1;
                        BaffClassWisdom1 = 0;
                        break;

                    case 3:
                        BaffClassStrength1 = 1;
                        BaffClassAgility1 = 1;
                        BaffClassWisdom1 = -1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Буревестник";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassAgility2 = 0;
                        BaffClassIntellect2 = 0;
                        break;

                    case 1:
                        BaffClassAgility2 = 1;
                        BaffClassIntellect2 = 0;
                        break;

                    case 2:
                        BaffClassAgility2 = 2;
                        BaffClassIntellect2 = 0;
                        break;

                    case 3:
                        BaffClassAgility2 = 2;
                        BaffClassIntellect2 = 2;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Слишком злай, чтобы умирать";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassBody3 = 0;
                        break;

                    case 1:
                        BaffClassBody3 = 1;
                        break;

                    case 2:
                        BaffClassBody3 = 2;
                        break;

                    case 3:
                        BaffClassBody3 = 3;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Друид")
            {
                BaffHealth = 1;

                DevelopmentTreeNameLabel1.Text = "Лесной волшебник";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassIntellect1 = 0;
                        BaffClassWisdom1 = 0;
                        break;

                    case 1:
                        BaffClassIntellect1 = 1;
                        BaffClassWisdom1 = 0;
                        break;

                    case 2:
                        BaffClassIntellect1 = 1;
                        BaffClassWisdom1 = 1;
                        break;

                    case 3:
                        BaffClassIntellect1 = 2;
                        BaffClassWisdom1 = 2;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Друг природы";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassBody2 = 0;
                        BaffClassWisdom2 = 0;
                        BaffClassStrength2 = 0;
                        BaffClassAgility2 = 0;
                        break;

                    case 1:
                        BaffClassBody2 = 1;
                        BaffClassWisdom2 = 1;
                        BaffClassStrength2 = 0;
                        BaffClassAgility2 = 0;
                        break;

                    case 2:
                        BaffClassBody2 = 1;
                        BaffClassWisdom2 = 2;
                        BaffClassStrength2 = 2;
                        BaffClassAgility2 = 0;
                        break;

                    case 3:
                        BaffClassBody2 = 2;
                        BaffClassWisdom2 = 2;
                        BaffClassStrength2 = 4;
                        BaffClassAgility2 = 2;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Любимая стая";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassBody3 = 0;
                        BaffClassCharisma3 = 0;
                        break;

                    case 1:
                        BaffClassBody3 = 1;
                        BaffClassCharisma3 = 1;
                        break;

                    case 2:
                        BaffClassBody3 = 3;
                        BaffClassCharisma3 = 2;
                        break;

                    case 3:
                        BaffClassBody3 = 4;
                        BaffClassCharisma3 = 3;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Рейнджер")
            {
                BaffHealth = 1.2;

                DevelopmentTreeNameLabel1.Text = "";
                switch (PointsTree1)
                {
                    case 0:

                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "";
                switch (PointsTree2)
                {
                    case 0:

                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "";
                switch (PointsTree3)
                {
                    case 0:

                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Маг")
            {
                BaffHealth = 0.5;

                DevelopmentTreeNameLabel1.Text = "Преобразователь мира";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassIntellect1 = 0;
                        break;

                    case 1:
                        BaffClassIntellect1 = 1;
                        break;

                    case 2:
                        BaffClassIntellect1 = 2;
                        break;

                    case 3:
                        BaffClassIntellect1 = 3;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Вознесение";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassWisdom2 = 0;
                        break;

                    case 1:
                        BaffClassWisdom2 = 1;
                        break;

                    case 2:
                        BaffClassWisdom2 = 2;
                        break;

                    case 3:
                        BaffClassWisdom2 = 3;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Бессметрие";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassBody3 = 0;
                        break;

                    case 1:
                        BaffClassBody3 = 1;
                        break;

                    case 2:
                        BaffClassBody3 = 2;
                        break;

                    case 3:
                        BaffClassBody3 = 3;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Колдун")
            {
                BaffHealth = 0.7;

                DevelopmentTreeNameLabel1.Text = "Щедрость покровителя";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassCharisma1 = 0;
                        BaffClassIntellect1 = 0;
                        BaffClassBody1 = 0;
                        break;

                    case 1:
                        BaffClassCharisma1 = 1;
                        BaffClassIntellect1 = 1;
                        BaffClassBody1 = 0;
                        break;

                    case 2:
                        BaffClassCharisma1 = 2;
                        BaffClassIntellect1 = 1;
                        BaffClassBody1 = 0;
                        break;

                    case 3:
                        BaffClassCharisma1 = 3;
                        BaffClassIntellect1 = 1;
                        BaffClassBody1 = 1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Фрилансер";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassIntellect2 = 0;
                        BaffClassCharisma2 = 0;
                        BaffClassDeception2 = 0;
                        BaffClassPersuasion2 = 0;
                        break;

                    case 1:
                        BaffClassIntellect2 = 1;
                        BaffClassCharisma2 = 0;
                        BaffClassDeception2 = 2;
                        BaffClassPersuasion2 = 0;
                        break;

                    case 2:
                        BaffClassIntellect2 = 1;
                        BaffClassCharisma2 = 1;
                        BaffClassDeception2 = 4;
                        BaffClassPersuasion2 = 0;
                        break;

                    case 3:
                        BaffClassIntellect2 = 2;
                        BaffClassCharisma2 = 1;
                        BaffClassDeception2 = 6;
                        BaffClassPersuasion2 = 1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Подчинение";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassIntellect3 = 0;
                        BaffClassCharisma3 = 0;
                        break;

                    case 1:
                        BaffClassIntellect3 = 0;
                        BaffClassCharisma3 = 0;
                        break;

                    case 2:
                        BaffClassIntellect3 = 1;
                        BaffClassCharisma3 = 1;
                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Жрец")
            {
                BaffHealth = 1;

                DevelopmentTreeNameLabel1.Text = "";
                switch (PointsTree1)
                {
                    case 0:

                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "";
                switch (PointsTree2)
                {
                    case 0:

                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "";
                switch (PointsTree3)
                {
                    case 0:

                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Паладин")
            {
                BaffHealth = 1.7;

                DevelopmentTreeNameLabel1.Text = "Фанатик";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassBody1 = 0;
                        BaffClassStrength2 = 0;
                        BaffClassAgility1 = 0;
                        break;

                    case 1:
                        BaffClassBody1 = 0;
                        BaffClassStrength2 = 0;
                        BaffClassAgility1 = 0;
                        break;

                    case 2:
                        BaffClassBody1 = 2;
                        BaffClassStrength2 = 0;
                        BaffClassAgility1 = 0;
                        break;

                    case 3:
                        BaffClassBody1 = 3;
                        BaffClassStrength2 = 1;
                        BaffClassAgility1 = 1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Глашатай";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassCharisma2 = 0;
                        BaffClassPersuasion2 = 0;
                        break;

                    case 1:
                        BaffClassCharisma2 = 1;
                        BaffClassPersuasion2 = 1;
                        break;

                    case 2:
                        BaffClassCharisma2 = 2;
                        BaffClassPersuasion2 = 3;
                        break;

                    case 3:
                        BaffClassCharisma2 = 3;
                        BaffClassPersuasion2 = 5;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Апостол";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassWisdom3 = 0;
                        break;

                    case 1:
                        BaffClassWisdom3 = 1;
                        break;

                    case 2:
                        BaffClassWisdom3 = 2;
                        break;

                    case 3:
                        BaffClassWisdom3 = 3;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "КЛЯТВОПРЕСТУПНИК")
            {
                BaffHealth = 1.7;

                DevelopmentTreeNameLabel1.Text = "Борец с богами";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassBody1 = 0;
                        break;

                    case 1:
                        BaffClassBody1 = 0;
                        break;

                    case 2:
                        BaffClassBody1 = 1;
                        break;

                    case 3:
                        BaffClassBody1 = 3;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Просветитель";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassCharisma2 = 0;
                        BaffClassIntellect2 = 0;
                        BaffClassPersuasion2 = 0;
                        BaffClassWisdom2 = 0;
                        break;

                    case 1:
                        BaffClassCharisma2 = 1;
                        BaffClassIntellect2 = 1;
                        BaffClassPersuasion2 = 2;
                        BaffClassWisdom2 = 0;
                        break;

                    case 2:
                        BaffClassCharisma2 = 2;
                        BaffClassIntellect2 = 1;
                        BaffClassPersuasion2 = 4;
                        BaffClassWisdom2 = 1;
                        break;

                    case 3:
                        BaffClassCharisma2 = 3;
                        BaffClassIntellect2 = 2;
                        BaffClassPersuasion2 = 5;
                        BaffClassWisdom2 = 1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Тёмная душа";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassIntellect3 = 0;
                        BaffClassBody3 = 0;
                        BaffClassStealth3 = 0;
                        break;

                    case 1:
                        BaffClassIntellect3 = 1;
                        BaffClassBody3 = 1;
                        BaffClassStealth3 = 0;
                        break;

                    case 2:
                        BaffClassIntellect3 = 2;
                        BaffClassBody3 = 2;
                        BaffClassStealth3 = 0;
                        break;

                    case 3:
                        BaffClassIntellect3 = 2;
                        BaffClassBody3 = 3;
                        BaffClassStealth3 = 1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            if (ClassName.Text == "Плут")
            {
                BaffHealth = 1;

                BaffClassAcrobaticsAdd = 2;
                BaffClassDeceptionAdd = 2;

                DevelopmentTreeNameLabel1.Text = "Тень";
                switch (PointsTree1)
                {
                    case 0:
                        BaffClassStealth1 = 0;
                        break;

                    case 1:
                        BaffClassStealth1 = 2;
                        break;

                    case 2:
                        BaffClassStealth1 = 4;
                        break;

                    case 3:
                        BaffClassStealth1 = 7;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel2.Text = "Ассасин";
                switch (PointsTree2)
                {
                    case 0:
                        BaffClassAcrobatics2 = 2;
                        break;

                    case 1:
                        BaffClassAcrobatics2 = 4;
                        break;

                    case 2:
                        BaffClassAcrobatics2 = 6;
                        break;

                    case 3:
                        BaffClassAcrobatics2 = 8;


                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }

                DevelopmentTreeNameLabel3.Text = "Криминальный гений";
                switch (PointsTree3)
                {
                    case 0:
                        BaffClassCharisma3 = 0;
                        BaffClassSleightOfHand3 = 0;
                        BaffClassAcrobatics3 = 0;
                        BaffClassStealth3 = 0;
                        BaffClassDeception3 = 0;
                        BaffClassPersuasion3 = 0;
                        BaffClassIntellect3 = 0;
                        break;

                    case 1:
                        BaffClassCharisma3 = 1;
                        BaffClassSleightOfHand3 = 2;
                        BaffClassAcrobatics3 = 1;
                        BaffClassStealth3 = 0;
                        BaffClassDeception3 = 0;
                        BaffClassPersuasion3 = 0;
                        BaffClassIntellect3 = 0;
                        break;

                    case 2:
                        BaffClassCharisma3 = 2;
                        BaffClassSleightOfHand3 = 3;
                        BaffClassAcrobatics3 = 3;
                        BaffClassStealth3 = 2;
                        BaffClassDeception3 = 2;
                        BaffClassPersuasion3 = 0;
                        BaffClassIntellect3 = 0;
                        break;
                    case 3:
                        BaffClassCharisma3 = 2;
                        BaffClassSleightOfHand3 = 3;
                        BaffClassAcrobatics3 = 3;
                        BaffClassStealth3 = 2;
                        BaffClassDeception3 = 4;
                        BaffClassPersuasion3 = 1;
                        BaffClassIntellect3 = 1;
                        break;

                    case 4:

                        break;

                    case 5:

                        break;
                    default:
                        break;
                }
            }

            BaffClassStrength = BaffClassStrength1 + BaffClassStrength2 + BaffClassStrength3;
            BaffClassAgility = BaffClassAgility1 + BaffClassAgility2 + BaffClassAgility3;
            BaffClassAcrobatics = BaffClassAcrobaticsAdd + BaffClassAcrobatics1 + BaffClassAcrobatics2 + BaffClassAcrobatics3;
            BaffClassSleightOfHand = BaffClassSleightOfHand1 + BaffClassSleightOfHand2 + BaffClassSleightOfHand3;
            BaffClassStealth = BaffClassStealth1 + BaffClassStealth2 + BaffClassStealth3;
            BaffClassBody = BaffClassBody1 + BaffClassBody2 + BaffClassBody3;
            BaffClassIntellect = BaffClassIntellect1 + BaffClassIntellect2 + BaffClassIntellect3;
            BaffClassInvestigation = BaffClassInvestigation1 + BaffClassInvestigation2 + BaffClassInvestigation3;
            BaffClassWisdom = BaffClassWisdom1 + BaffClassWisdom2 + BaffClassWisdom3;
            BaffClassCharisma = BaffClassCharisma1 + BaffClassCharisma2 + BaffClassCharisma3;
            BaffClassDeception = BaffClassDeceptionAdd + BaffClassDeception1 + BaffClassDeception2 + BaffClassDeception3;
            BaffClassSpeech = BaffClassSpeech1 + BaffClassSpeech2 + BaffClassSpeech3;
            BaffClassPersuasion = BaffClassPersuasion1 + BaffClassPersuasion2 + BaffClassPersuasion3;

            CallMethodPoints();
            HealthSet();

        }
        private void SetPointsTree()
        {

            PointsTreeNow = PointsTree - (PointsTree1 + PointsTree2 + PointsTree3);
            DevelopmentTreePointsValueLabel.Text = PointsTreeNow.ToString();

            Properties.Settings.Default.LevelTree1 = PointsTree1;
            DevelopmentTreeValueLabel1.Text = PointsTree1.ToString();

            Properties.Settings.Default.LevelTree2 = PointsTree2;
            DevelopmentTreeValueLabel2.Text = PointsTree2.ToString();

            Properties.Settings.Default.LevelTree3 = PointsTree3;
            DevelopmentTreeValueLabel3.Text = PointsTree3.ToString();

            Properties.Settings.Default.Save();

            SetClass();
        }

        private void DevelopmentTreeAdd1_Click(object sender, EventArgs e)
        {
            if (PointsTreeNow > 0 && PointsTree1 < LockLevelTree)
            {
                PointsTree1 += 1;
                SetPointsTree();
            }
        }

        private void DevelopmentTreeLower1_Click(object sender, EventArgs e)
        {
            if (PointsTree1 > 0)
            {
                PointsTree1 -= 1;
                SetPointsTree();
            }
        }

        private void DevelopmentTreeAdd2_Click(object sender, EventArgs e)
        {
            if (PointsTreeNow > 0 && PointsTree2 < LockLevelTree)
            {
                PointsTree2 += 1;
                SetPointsTree();
            }
        }

        private void DevelopmentTreeLower2_Click(object sender, EventArgs e)
        {
            if (PointsTree2 > 0)
            {
                PointsTree2 -= 1;
                SetPointsTree();
            }
        }

        private void DevelopmentTreeAdd3_Click(object sender, EventArgs e)
        {
            if (PointsTreeNow > 0 && PointsTree3 < LockLevelTree)
            {
                PointsTree3 += 1;
                SetPointsTree();
            }
        }

        private void DevelopmentTreeLower3_Click(object sender, EventArgs e)
        {
            if (PointsTree3 > 0)
            {
                PointsTree3 -= 1;
                SetPointsTree();
            }
        }


        private void LevelAdd_Click(object sender, EventArgs e)
        {
            if (Level < 20)
            {
                Level += 1;
                LevelMethod();
            }
        }

        private void LevelLower_Click(object sender, EventArgs e)
        {
            if (Level > 1)
            {
                Level -= 1;
                LevelMethod();
            }
        }

        private void LevelMethod()
        {
            LevelLabel.Text = Level.ToString();
            Properties.Settings.Default.Level = Level;
            Properties.Settings.Default.Save();

            switch (Level)
            {
                case 1:
                    MaxLock = 20;
                    BaffLevelPoints = 0;
                    PointsTree = 0;
                    LockLevelTree = 2;
                    break;

                case 2:
                    MaxLock = 30;
                    BaffLevelPoints = 1;
                    PointsTree = 0;
                    LockLevelTree = 2;
                    break;

                case 3:
                    MaxLock = 30;

                    BaffLevelPoints = 1;
                    PointsTree = 1;
                    LockLevelTree = 2;
                    break;

                case 4:
                    MaxLock = 30;

                    BaffLevelPoints = 2;
                    PointsTree = 1;
                    LockLevelTree = 2;
                    break;

                case 5:
                    BaffLevelPoints = 2;
                    PointsTree = 2;
                    LockLevelTree = 2;
                    break;

                case 6:
                    BaffLevelPoints = 3;
                    PointsTree = 2;
                    LockLevelTree = 2;
                    break;

                case 7:
                    BaffLevelPoints = 3;
                    PointsTree = 3;
                    LockLevelTree = 2;
                    break;

                case 8:
                    BaffLevelPoints = 4;
                    PointsTree = 3;
                    LockLevelTree = 2;
                    break;

                case 9:
                    BaffLevelPoints = 5;
                    PointsTree = 3;
                    LockLevelTree = 2;
                    break;

                case 10:
                    BaffLevelPoints = 5;
                    PointsTree = 4;
                    LockLevelTree = 3;
                    break;

                case 11:
                    BaffLevelPoints = 6;
                    PointsTree = 4;
                    LockLevelTree = 3;
                    break;

                case 12:
                    BaffLevelPoints = 7;
                    PointsTree = 4;
                    LockLevelTree = 3;
                    break;

                case 13:
                    BaffLevelPoints = 7;
                    PointsTree = 5;
                    LockLevelTree = 3;
                    break;

                case 14:
                    BaffLevelPoints = 9;
                    PointsTree = 5;
                    LockLevelTree = 3;
                    break;

                case 15:
                    BaffLevelPoints = 9;
                    PointsTree = 6;
                    LockLevelTree = 4;
                    break;

                case 16:
                    BaffLevelPoints = 11;
                    PointsTree = 6;
                    LockLevelTree = 4;
                    break;

                case 17:
                    BaffLevelPoints = 13;
                    PointsTree = 6;
                    LockLevelTree = 4;
                    break;

                case 18:
                    BaffLevelPoints = 13;
                    PointsTree = 7;
                    LockLevelTree = 4;
                    break;

                case 19:
                    BaffLevelPoints = 15;
                    PointsTree = 7;
                    LockLevelTree = 4;
                    break;

                case 20:
                    BaffLevelPoints = 13;
                    PointsTree = 9;
                    LockLevelTree = 5;
                    break;

                default:
                    break;
            }

            SetPoint();
            SetPointsTree();


        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Inventory.SelectedItems.Count > 0)
            {
                TextItem.Text = Inventory.SelectedItems[0].SubItems[0].Text;

                if (Inventory.SelectedItems[0].SubItems[1].Text.Length > 0)
                {
                    TextWeight.Text = Convert.ToString(Convert.ToDouble(Inventory.SelectedItems[0].SubItems[1].Text.Trim('*')) / Convert.ToInt32(Inventory.SelectedItems[0].SubItems[7].Text)); 
                }
                else
                {
                    TextWeight.Text = "";
                }

                TextQuality.Text = Inventory.SelectedItems[0].SubItems[2].Text;

                if (Inventory.SelectedItems[0].SubItems[3].Text.Length > 0)
                {
                    TextChopping.Text = Convert.ToString(Convert.ToDouble(Inventory.SelectedItems[0].SubItems[3].Text) / IsQualityBaff(Inventory.SelectedItems[0].SubItems[2].Text));
                }
                else
                {
                    TextChopping.Text = "";
                }

                if (Inventory.SelectedItems[0].SubItems[4].Text.Length > 0)
                {
                    TextStabbing.Text = Convert.ToString(Convert.ToDouble(Inventory.SelectedItems[0].SubItems[4].Text) / IsQualityBaff(Inventory.SelectedItems[0].SubItems[2].Text));
                }
                else
                {
                    TextStabbing.Text = "";
                }

                if (Inventory.SelectedItems[0].SubItems[5].Text.Length > 0)
                {
                    TextCrushing.Text = Convert.ToString(Convert.ToDouble(Inventory.SelectedItems[0].SubItems[5].Text) / IsQualityBaff(Inventory.SelectedItems[0].SubItems[2].Text));
                }
                else
                {
                    TextCrushing.Text = "";
                }

                if (Inventory.SelectedItems[0].SubItems[6].Text.Length > 0)
                {
                    TextKD.Text = Convert.ToString(Convert.ToDouble(Inventory.SelectedItems[0].SubItems[6].Text.Trim(DeleteKDSymbols)) / IsQualityBaff(Inventory.SelectedItems[0].SubItems[2].Text));
                }
                else
                {
                    TextKD.Text = "";
                }

                NumberItems.Value = Convert.ToInt32(Inventory.SelectedItems[0].SubItems[7].Text);

                CountWeightItem.Checked = TextToBool(Inventory.SelectedItems[0].SubItems[8].Text);

                DescriptionItem.Text = Inventory.SelectedItems[0].SubItems[9].Text;

            }
        }

        static bool IsNum(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c)) return false;
            }
            return true;
        }

        static bool TextToBool(string s)
        {
            if (s == "True")
            {
                return true;
            }
            return false;
        }

        static float IsQualityBaff(string Quality)
        {
            float Baff = 0f;
            switch (Quality)
            {
                case "Прото":
                    Baff = 0.5f;
                    break;

                case "Плохое":
                    Baff = 0.75f;
                    break;

                case "Среднее":
                    Baff = 1.0f;
                    break;

                case "Высокое":
                    Baff = 1.25f;
                    break;

                case "Великолепное":
                    Baff = 1.5f;
                    break;

                case "Мастерское":
                    Baff = 2.0f;
                    break;

                case "Мифическое":
                    Baff = 3.0f;
                    break;

                default:
                    break;
            }
            return Baff;
        }

        private void AddItem_Click(object sender, EventArgs e)
        {


            if (TextItem.Text.Length > 0 && TextWeight.Text.Length > 0 && NumberItems.Value > 0 && IsNum(TextWeight.Text) && IsNum(TextStabbing.Text) && IsNum(TextCrushing.Text) && IsNum(TextKD.Text))
            {
                string Chopping = "";
                string Stabbing = "";
                string Crushing = "";
                string KD = "";
                string Weight = "";

                if (TextChopping.Text.Length > 0 && TextQuality.Text != "")
                {
                    var Damage = Convert.ToInt32(TextChopping.Text) * IsQualityBaff(TextQuality.Text);
                    Chopping = Damage.ToString();
                }

                if (TextStabbing.Text.Length > 0 && TextQuality.Text != "")
                {
                    var Damage = Convert.ToInt32(TextStabbing.Text) * IsQualityBaff(TextQuality.Text);
                    Stabbing = Damage.ToString();
                }

                if (TextCrushing.Text.Length > 0 && TextQuality.Text != "")
                {
                    var Damage = Convert.ToInt32(TextCrushing.Text) * IsQualityBaff(TextQuality.Text);
                    Crushing = Damage.ToString();
                }

                if (TextKD.Text.Length > 0 && TextQuality.Text != "")
                {
                    var Damage = Convert.ToInt32(TextKD.Text) * IsQualityBaff(TextQuality.Text);
                    KD = Damage.ToString();
                }

                if (CountWeightItem.Checked)
                {
                    Weight = Convert.ToString(Convert.ToInt32(TextWeight.Text) * NumberItems.Value);
                }
                else
                {
                    Weight = Convert.ToString(Convert.ToInt32(TextWeight.Text) * NumberItems.Value) + "*";
                }
                if (CheckKDHead.Checked && CountKDItem.Checked && TextKD.Text.Length > 0)
                {
                    KD = KD + "^^";
                }
                else if (CheckKDHead.Checked && TextKD.Text.Length > 0)
                {
                    KD = KD + "^";
                }
                else if (!CountKDItem.Checked && TextKD.Text.Length > 0)
                {
                    KD = KD + "*";
                }



                string[] Item = { TextItem.Text, Weight, TextQuality.Text, Chopping, Stabbing, Crushing, KD, NumberItems.Value.ToString(), CountWeightItem.Checked.ToString(), DescriptionItem.Text, CountKDItem.Checked.ToString(),CheckKDHead.Checked.ToString()};
                var Itemlv = new ListViewItem(Item);
                Inventory.Items.Add(Itemlv);
              

                SaveInventory();
                UsetWeightM();
                ArmorKDSet();
            }
        }

        private void SaveInventory()
        {
            Properties.Settings.Default.Inventory = new StringCollection();

            var items = new List<string>();

            foreach (ListViewItem item in this.Inventory.Items)
            {
                var subitems = new List<string>();

                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    subitems.Add(subitem.Text);
                }

                items.Add(string.Join("|", subitems));
            }

            Properties.Settings.Default.Inventory.AddRange(items.ToArray());
            Properties.Settings.Default.Save();
        }

        private void DeleteSelectItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in Inventory.SelectedItems)
            {
                Inventory.Items.Remove(item);
            }
            SaveInventory();
            UsetWeightM();
            ArmorKDSet();
        }

        private void UsetWeightM()
        {
            double WeightItem;
            UsetWeight = 0;
            UsetItemWeight = 0;

           for (int i = 0; i < Inventory.Items.Count; i++)
                        {
                            if (Inventory.Items[i].SubItems[8].Text == "True")
                            {
                                if (Inventory.Items[i].SubItems[3].Text.Length > 0 || Inventory.Items[i].SubItems[4].Text.Length > 0 || Inventory.Items[i].SubItems[5].Text.Length > 0 || Inventory.Items[i].SubItems[6].Text.Length > 0)
                                {
                                    WeightItem = Convert.ToDouble(Inventory.Items[i].SubItems[1].Text);
                                    UsetWeight = UsetWeight + WeightItem;
                                }
                                else
                                {
                                    WeightItem = Convert.ToDouble(Inventory.Items[i].SubItems[1].Text);
                                    UsetItemWeight = UsetItemWeight + WeightItem;

                                }
                            }  
                        }
                        WeightSet();
        }

        private void ClearAddBoxes_Click(object sender, EventArgs e)
        {
            TextItem.Text = "";
            TextWeight.Text = "";
            TextQuality.Text = "";
            TextChopping.Text = "";
            TextStabbing.Text = "";
            TextCrushing.Text = "";
            TextKD.Text = "";
            NumberItems.Value = 1;
            DescriptionItem.Text = "";
        }

        private void ResetItem_Click(object sender, EventArgs e)
        {
            if (Inventory.SelectedItems.Count > 0 && TextItem.Text.Length > 0 && TextWeight.Text.Length > 0 && NumberItems.Value > 0 && IsNum(TextWeight.Text) && IsNum(TextStabbing.Text) && IsNum(TextCrushing.Text) && IsNum(TextKD.Text))
            {
                Inventory.SelectedItems[0].SubItems[0].Text = TextItem.Text;

                if (TextWeight.Text.Length > 0)
                {
                    if (CountWeightItem.Checked)
                    {
                        Inventory.SelectedItems[0].SubItems[1].Text = Convert.ToString(Convert.ToDouble(TextWeight.Text) * Convert.ToInt32(NumberItems.Value));
                    }
                    else
                    {
                        Inventory.SelectedItems[0].SubItems[1].Text = Convert.ToString(Convert.ToDouble(TextWeight.Text) * Convert.ToInt32(NumberItems.Value)) + "*";
                    }
                    
                }
                else
                {
                    Inventory.SelectedItems[0].SubItems[1].Text = "";
                }

                Inventory.SelectedItems[0].SubItems[2].Text = TextQuality.Text;

                if (TextChopping.Text.Length > 0)
                {
                    Inventory.SelectedItems[0].SubItems[3].Text = Convert.ToString(Convert.ToDouble(TextChopping.Text) * IsQualityBaff(TextQuality.Text));
                }
                else
                {
                    Inventory.SelectedItems[0].SubItems[3].Text = "";
                }

                if (TextStabbing.Text.Length > 0)
                {
                    Inventory.SelectedItems[0].SubItems[4].Text = Convert.ToString(Convert.ToDouble(TextStabbing.Text) * IsQualityBaff(TextQuality.Text));
                }
                else
                {
                    Inventory.SelectedItems[0].SubItems[4].Text = "";
                }

                if (TextCrushing.Text.Length > 0)
                {
                    Inventory.SelectedItems[0].SubItems[5].Text = Convert.ToString(Convert.ToDouble(TextCrushing.Text) * IsQualityBaff(TextQuality.Text));
                }
                else
                {
                    Inventory.SelectedItems[0].SubItems[5].Text = "";
                }

                if (TextKD.Text.Length > 0)
                {
                    if (CheckKDHead.Checked && CountKDItem.Checked)
                    {
                        Inventory.SelectedItems[0].SubItems[6].Text = Convert.ToString(Convert.ToDouble(TextKD.Text) * IsQualityBaff(TextQuality.Text)) + "^^";
                    }
                    else if (CountKDItem.Checked)
                    {
                        Inventory.SelectedItems[0].SubItems[6].Text = Convert.ToString(Convert.ToDouble(TextKD.Text) * IsQualityBaff(TextQuality.Text));
                    }
                    else if (CheckKDHead.Checked)
                    {
                        Inventory.SelectedItems[0].SubItems[6].Text = Convert.ToString(Convert.ToDouble(TextKD.Text) * IsQualityBaff(TextQuality.Text)) + "^";
                    }
                    else 
                    {
                        Inventory.SelectedItems[0].SubItems[6].Text = Convert.ToString(Convert.ToDouble(TextKD.Text) * IsQualityBaff(TextQuality.Text)) + "*";
                    }
                }
                else
                {
                    Inventory.SelectedItems[0].SubItems[6].Text = "";
                }

                Inventory.SelectedItems[0].SubItems[7].Text = NumberItems.Value.ToString();

                Inventory.SelectedItems[0].SubItems[8].Text = CountWeightItem.Checked.ToString();
 
                Inventory.SelectedItems[0].SubItems[9].Text = DescriptionItem.Text;

                Inventory.SelectedItems[0].SubItems[10].Text = CountKDItem.Checked.ToString();


                UsetWeightM();
                SaveInventory();
                ArmorKDSet();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            NameLabel.Text = NameText.Text;
            Properties.Settings.Default.Name = NameText.Text;
            Properties.Settings.Default.Save();
        }

        private void HealthSet()
        {
            double MaxHealth = 0;
            double HealthNow = 0;
            int Body_ = Body + BaffBody + BaffClassBody;
            MaxHealth = (Body_ * 10) * BaffHealth;
            GeneralDamage = GeneralDamage + TotalDamage;
            HealthNow = MaxHealth - GeneralDamage;
            if (HealthNow > MaxHealth)
            {
                HealthNow = MaxHealth;
            }

            HealthLabel.Text = HealthNow.ToString() + "/" + MaxHealth.ToString();
            Properties.Settings.Default.Health = GeneralDamage;
            Properties.Settings.Default.Save(); 
        }
       
        private void WeightSet()
        {
            int MaxWeight;
            double Weight;
            double WeightItem;

            int Strength_ = Strength + BaffStrength + BaffClassStrength;

            if (BackpackNumber.Value > 0)
            {
                MaxWeight = (Strength_ * 30) + BaffWeight + (AddWeightBackpack * Convert.ToInt32(BackpackNumber.Value));
                WeightItem = UsetItemWeight / DivideWeightBackpack;
                Weight = Math.Floor(MaxWeight - (UsetWeight + WeightItem));
            }
            else
            {
                MaxWeight = (Strength_ * 30) + BaffWeight + (AddWeightBackpack * Convert.ToInt32(BackpackNumber.Value));
                WeightItem = UsetItemWeight; 
                Weight = MaxWeight - (UsetWeight + WeightItem);
            }

            WeightNowLabel.Text = Weight.ToString() + "/" + MaxWeight.ToString();

            if (Weight < 0)
            {
                WeightNowLabel.ForeColor = Color.Red;
            }
            else
            {
                WeightNowLabel.ForeColor = Color.Black;
            }

        }

        private void ArmorKDSet()
        {
            int KDItem;
            ItemsKD = 0;
            ItemsKDHead = 0;

                   for (int i = 0; i < Inventory.Items.Count; i++)
                   {
                   if (Inventory.Items[i].SubItems[6].Text.Length > 0)
                {

                    if (Inventory.Items[i].SubItems[10].Text == "True" && Inventory.Items[i].SubItems[6].Text.Substring(Inventory.Items[i].SubItems[6].Text.Length - 1) == "^")
                    {
                        KDItem = Convert.ToInt32(Math.Floor(Convert.ToDouble(Inventory.Items[i].SubItems[6].Text.Trim(DeleteKDSymbols))));
                        ItemsKD = ItemsKD + KDItem;
                        ItemsKDHead = ItemsKDHead + KDItem;
                    }
                    else if (Inventory.Items[i].SubItems[10].Text == "True" && Inventory.Items[i].SubItems[6].Text.Substring(Inventory.Items[i].SubItems[6].Text.Length - 1) != "^")
                    {
                        KDItem = Convert.ToInt32(Math.Floor(Convert.ToDouble(Inventory.Items[i].SubItems[6].Text.Trim(DeleteKDSymbols))));
                        ItemsKD = ItemsKD + KDItem;
                    }
                    else if (Inventory.Items[i].SubItems[10].Text == "False" && Inventory.Items[i].SubItems[6].Text.Substring(Inventory.Items[i].SubItems[6].Text.Length - 1) == "^")
                    {
                        KDItem = Convert.ToInt32(Math.Floor(Convert.ToDouble(Inventory.Items[i].SubItems[6].Text.Trim(DeleteKDSymbols))));
                        ItemsKDHead = ItemsKDHead + KDItem;
                    }

                }
                   }
                SetTotalKD();
        }


        private void BackpackBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BackpackNameLabel.Text = BackpackBox.Text;

            switch (BackpackBox.Text)
            {
                case "Нет":
                    LockNumberBackpack = 0;
                    AddWeightBackpack = 0;
                    DivideWeightBackpack = 1;
                    break;

                case "Прото":
                    LockNumberBackpack = 3;
                    AddWeightBackpack = 30;
                    DivideWeightBackpack = 1;
                    break;

                case "Плохой":
                    LockNumberBackpack = 2;
                    AddWeightBackpack = 60;
                    DivideWeightBackpack = 1;
                    break;

                case "Средний":
                    LockNumberBackpack = 2;
                    AddWeightBackpack = 0;
                    DivideWeightBackpack = 2;
                    break;

                case "Высокий":
                    LockNumberBackpack = 2;
                    AddWeightBackpack = 30;
                    DivideWeightBackpack = 2;
                    break;

                case "Великолепный":
                    LockNumberBackpack = 2;
                    AddWeightBackpack = 60;
                    DivideWeightBackpack = 2;
                    break;

                case "Мастерский":
                    LockNumberBackpack = 2;
                    AddWeightBackpack = 100;
                    DivideWeightBackpack = 2;
                    break;

                case "Мифический":
                    LockNumberBackpack = 2;
                    AddWeightBackpack = 150;
                    DivideWeightBackpack = 3;
                    break;

                default:
                    break;
            }
            
            BackpackNumber.Value = 0;

            Properties.Settings.Default.Backpack = BackpackBox.Text;
            Properties.Settings.Default.Save();

            WeightSet();

        }


        private void BackpackNumber_ValueChanged(object sender, EventArgs e)
        {
            if (BackpackNumber.Value > LockNumberBackpack)
            {
                BackpackNumber.Value = LockNumberBackpack;
            }
            else if(BackpackNumber.Value < 0)
            {
                BackpackNumber.Value = 0;
            }
            
            NumberBackpackLabel.Text = BackpackNumber.Value.ToString();
            WeightSet();
            Properties.Settings.Default.BackpackNumber = BackpackNumber.Value;
            Properties.Settings.Default.Save();
        }

        private void CalculationDamage_Click(object sender, EventArgs e)
        {
            int HeadKDvsTotalKD; 
            if (!FightCheckHeadKD.Checked)
            {
                HeadKDvsTotalKD = TotalKD;
            }
            else
            {
                HeadKDvsTotalKD = ItemsKDHead;
            }

            double DamageC = 0;
            decimal HeavinessDamage = HeavinessDamageNum.Value;
            int Damage = Convert.ToInt32(Math.Floor(Convert.ToDouble((DamageNumber.Value + HeavinessDamage)) * BaffPhysDamage));
            int BaffKD = 0;
            BaffKD = (HeadKDvsTotalKD + Convert.ToInt32(AddKDBox.Value)) - Convert.ToInt32(SubtractKDBox.Value);
            int MagicKD = Convert.ToInt32(Math.Floor(Convert.ToDouble(ItemsKD) / 2) + Convert.ToInt32(AddKDBox.Value)) - Convert.ToInt32(SubtractKDBox.Value);
            int BaffBodyKD = (BodyKD + Convert.ToInt32(AddKDBox.Value)) - Convert.ToInt32(SubtractKDBox.Value);



            switch (TypeDamage.Text)
            {
                case "Дробящий":
                    if (Damage > BaffKD)
                    {
                        DamageC = Damage - BaffKD;
                    }
                    break;

                case "Рубящий":
                    int DeBaffDamage = 0;
                    if (ArmorBox.Text == "Средняя")
                    {
                        DeBaffDamage = Convert.ToInt32(Math.Floor(Damage * 0.5));
                        if (DeBaffDamage > BaffKD)
                        {
                            DamageC = DeBaffDamage - BaffKD;
                        }
                    }
                    else if(ArmorBox.Text == "Тяжелая")
                    {
                        DeBaffDamage = Convert.ToInt32(Math.Floor(Damage * 0.25));
                        if (DeBaffDamage > BaffKD)
                        {
                            DamageC = DeBaffDamage - BaffKD;
                        }
                    }
                    else
                    {
                        if (Damage > BaffKD)
                        {
                            DamageC = Damage - BaffKD;

                            int BleedingsNumber = Convert.ToInt32(Math.Floor(Convert.ToDouble(DamageC / 40)));
                            BleedingsLabelNumber.Text = BleedingsNumber.ToString();
                        }
                    }
                    break;

                case "Колющий":
                    if (CheckBlockBox.Checked)
                    {
                        if (Damage > BaffKD)
                        {
                            DamageC = Damage - BaffKD;
                        }
                    }
                    else
                    {
                        int BaffDamage = Convert.ToInt32(Math.Floor(Damage * 1.25));
                        if (BaffDamage > BaffKD)
                        {
                            DamageC = BaffDamage - BaffKD;
                        }
                    }
                    break;

                case "Ментальный":
                    if (Damage > Convert.ToDouble(MentalKDLabel.Text))
                    {
                        DamageC = Damage - Convert.ToDouble(MentalKDLabel.Text);
                    }
                    break;

                case "Некротический":
                    if (Damage > BaffBodyKD)
                    {
                        DamageC = Damage - BaffBodyKD;
                    }
                    break;

                case "Эфирный":
                    if (Damage > BaffBodyKD)
                    {
                        DamageC = Damage - BaffBodyKD;
                    }   
                    break;

                case "Электрический":
                    if (Damage > MagicKD)
                    {
                        DamageC = Damage - MagicKD;
                    }
                    break;

                case "Холодный":
                    if(Damage > MagicKD)
                    {
                        DamageC = Damage - MagicKD;
                    }
                    break;

                case "Без Дебаффов":
                    DamageC = Damage; 
                    break;

                default:
                    break;
            }

            if(AddSubstractHealthBox.Text == "+")
            {
                TotalDamage = Math.Floor(DamageC) * -1;
                DeltaHealthLabel.Text = AddSubstractHealthBox.Text + TotalDamage.ToString().Trim('-');
            }
            else
            {
                TotalDamage = Math.Floor(DamageC);
                DeltaHealthLabel.Text = AddSubstractHealthBox.Text + TotalDamage.ToString();
            }

            HealthSet();
        }

        private void HeavinessQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateHeavinessDamage();
        }

        private void HeavinessDamageNum_ValueChanged(object sender, EventArgs e)
        { 
            
        }

        private void HeavinessNumber_ValueChanged(object sender, EventArgs e)
        {
            CalculateHeavinessDamage();
        }

        private void CalculateHeavinessDamage()
        {
            decimal Baff = 0;
            decimal HeavinessDamage = 0;
            switch (HeavinessQuality.Text)
            {

                case "Прото":
                    Baff = 1;
                    break;

                case "Плохое":
                    Baff = 2;
                    break;

                case "Среднее":
                    Baff = 3;
                    break;

                case "Высокое":
                    Baff = 4;
                    break;

                case "Великолепное":
                    Baff = 5;
                    break;

                case "Мастерское":
                    Baff = 6;
                    break;

                case "Мифическое":
                    Baff = 7;
                    break;

                default:
                    break;
            }

            HeavinessDamage = 5 * Baff * HeavinessNumber.Value;
            HeavinessDamageNum.Value = HeavinessDamage;
        }

        private void SetTotalKD()
        {
            int KDArmor = ItemsKD;
            int KD = BaffKD + Body + KDArmor;
            int KDNow = (KD + Convert.ToInt32(AddKDBox.Value)) - Convert.ToInt32(SubtractKDBox.Value);
            TotalKD = KD;
            BodyKD = BaffKD + Body;
            KDLabel.Text = KDNow.ToString();
            HeadKDLabel.Text = ItemsKDHead.ToString();
        }

        private void TypeDamage_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBlockBox.Visible = false;

            ArmorBox.Visible = false;
            ArmorLabel.Visible = false;
            BleedingsLabel.Visible = false;
            BleedingsLabelNumber.Visible = false;
            BleedingsLabelNumber.Text = "0";

            switch (TypeDamage.Text)
            {
                case "Рубящий":
                    ArmorBox.Visible = true;
                    ArmorLabel.Visible = true;
                    BleedingsLabel.Visible = true;
                    BleedingsLabelNumber.Visible = true;
                    break;

                case "Колющий":
                    CheckBlockBox.Visible = true;    
                    CheckBlockBox.Checked = true;
                    break;


                default:
                    break;
            }
        }

        private void FightStart_CheckedChanged(object sender, EventArgs e)
        {
            bool StatusButton = FightStart.Checked;

            if (StatusButton == true)
            {
                CalculationDamage.Visible = StatusButton;
                TypeDamage.Visible = StatusButton;
                TypeDamageLabel.Visible = StatusButton;
                HeavinessDamageNum.Visible = StatusButton;
                HeavinessFlat.Visible = StatusButton;   
                HeavinessLabel.Visible = StatusButton;    
                HeavinessLabel2.Visible = StatusButton;
                HeavinessQuality.Visible = StatusButton;
                HeavinessNumber.Visible = StatusButton;
                DamageLabel.Visible = StatusButton;
                DamageNumber.Visible = StatusButton;
                BaffKDLabel.Visible = StatusButton;
                BaffKDLabel1.Visible = StatusButton;
                BaffKDLabel2.Visible = StatusButton;
                AddKDBox.Visible = StatusButton;
                SubtractKDBox.Visible = StatusButton;
                AddSubstractHealthBox.Visible = StatusButton;
                DeltaHealthLabel.Visible = StatusButton;
                ResetHealth.Visible = StatusButton;
                FightCheckHeadKD.Visible = StatusButton;
            }
            if (StatusButton == false)
            {
                CalculationDamage.Visible = StatusButton;
                TypeDamage.Visible = StatusButton;
                TypeDamageLabel.Visible = StatusButton;
                HeavinessDamageNum.Visible = StatusButton;
                HeavinessFlat.Visible = StatusButton;
                HeavinessLabel.Visible = StatusButton;
                HeavinessLabel2.Visible = StatusButton;
                HeavinessQuality.Visible = StatusButton;
                HeavinessNumber.Visible = StatusButton;
                DamageLabel.Visible = StatusButton;
                DamageNumber.Visible = StatusButton;
                BaffKDLabel.Visible = StatusButton;
                BaffKDLabel1.Visible = StatusButton;
                BaffKDLabel2.Visible = StatusButton;
                AddKDBox.Visible = StatusButton;
                SubtractKDBox.Visible = StatusButton;
                AddSubstractHealthBox.Visible = StatusButton;
                DeltaHealthLabel.Visible = StatusButton;
                ResetHealth.Visible = StatusButton;
                FightCheckHeadKD.Visible = StatusButton;
            }
        }

        private void CupperBox_ValueChanged(object sender, EventArgs e)
        {
            SetTotalCopper();
            Properties.Settings.Default.Copper = CupperBox.Value;
            Properties.Settings.Default.Save();
        }

        private void SerebrenicBox_ValueChanged(object sender, EventArgs e)
        {
            SetTotalCopper();
            Properties.Settings.Default.Serebrenic = SerebrenicBox.Value;
            Properties.Settings.Default.Save();
        }

        private void GoldenBox_ValueChanged(object sender, EventArgs e)
        {
            SetTotalCopper();
            Properties.Settings.Default.Golden = GoldenBox.Value;
            Properties.Settings.Default.Save();
        }

        private void SetTotalCopper()
        {
            var Total = CupperBox.Value + (SerebrenicBox.Value * 100) + (GoldenBox.Value * 1000);
            TotalCopper.Text = Total.ToString();
        }

        private void AddKDBox_ValueChanged(object sender, EventArgs e)
        {
            SetTotalKD();
        }

        private void SubtractKDBox_ValueChanged(object sender, EventArgs e)
        {
            SetTotalKD();
        }

        private void ResetHealth_Click(object sender, EventArgs e)
        {
            GeneralDamage = 0;
            TotalDamage = 0;
            HealthSet();
        }

        private void InfoTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.InfoTextBox = InfoTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CountKDItem_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void CheckKDHead_CheckedChanged(object sender, EventArgs e)
        { 

        }

        private void CountWeightItem_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DescriptionItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void NumberBackpackLabel_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void BackpackNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void BackpackNumberLabel_Click(object sender, EventArgs e)
        {

        }

        private void BackpackLabel_Click(object sender, EventArgs e)
        {

        }

        private void WeightNowLabel_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void NumberItems_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TextKD_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextCrushing_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextStabbing_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextChopping_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextQuality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextWeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void TotalCopper_Click(object sender, EventArgs e)
        {

        }

        private void WalkingStickMethod()
        {
            int Agility_ = Agility + BaffAgility + BaffClassAgility;
            int Strength_ = Strength + BaffStrength + BaffClassStrength;
            double AthleticaBaff;
            double AgilityBaff;

            if(Strength_ >= 20)
            {
                AthleticaBaff = 1;
            }
            else
            {
                AthleticaBaff = 0;
            }

            if (Agility_ >= 20)
            {
                AgilityBaff = 0.5;
            }
            else
            {
                AgilityBaff = 0;
            }

            var WalkingStick =  (1 + AgilityBaff + AthleticaBaff) - WalkingStickBaffRace;
            WalkingStickLabel.Text = WalkingStick.ToString();
        }

        private void CheckArmorTricrin_CheckedChanged(object sender, EventArgs e)
        {
            AgilityMethod();
        }

        private void CharismaValueLabel_Click(object sender, EventArgs e)
        {

        }

        private void CharismaRollValue_Click(object sender, EventArgs e)
        {

        }
    }

}
