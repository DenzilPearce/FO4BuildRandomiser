using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace FO4BuildRandomiser
{
    public partial class FormMain : Form
    {
        private Config config;
        private Build build;
        private int[] nextPerk;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // initialise tab control aesthetics
            tabControlMain.SelectedIndex = 0;
            tabControlMain.SizeMode = TabSizeMode.Fixed;
            tabControlMain.ItemSize = new Size(0, 1);
            tabControlMain.Dock = DockStyle.Fill;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            config = new Config(checkIncludeFarHarbour.Checked, checkIncludeNukaWorld.Checked, Decimal.ToInt32(numericMinimumSPECIAL.Value), Decimal.ToInt32(numericMaximumSPECIAL.Value), checkAllowRerolls.Checked, Decimal.ToInt32(numericBias.Value));

            build = new Build(config);

            // switch to roll tab
            updateRollTab();
            tabControlMain.SelectedIndex = 1;
        }

        private void updateRollTab()
        {
            // stat labels
            labelRollStrength.Text = build.baseSPECIALs[(int)SPECIAL.Strength].ToString();
            labelRollPerception.Text = build.baseSPECIALs[(int)SPECIAL.Perception].ToString();
            labelRollEndurance.Text = build.baseSPECIALs[(int)SPECIAL.Endurance].ToString();
            labelRollCharisma.Text = build.baseSPECIALs[(int)SPECIAL.Charisma].ToString();
            labelRollIntelligence.Text = build.baseSPECIALs[(int)SPECIAL.Intelligence].ToString();
            labelRollAgility.Text = build.baseSPECIALs[(int)SPECIAL.Agility].ToString();
            labelRollLuck.Text = build.baseSPECIALs[(int)SPECIAL.Luck].ToString();

            // reroll button
            buttonReroll.Enabled = config.allowRerolls;
        }

        private void updateMainTab()
        {
            // level label
            labelLevel.Text = "Level: " + build.level.ToString();

            // stat labels
            labelMainStrength.Text = build.modifiedSPECIAL(SPECIAL.Strength).ToString();
            labelMainPerception.Text = build.modifiedSPECIAL(SPECIAL.Perception).ToString();
            labelMainEndurance.Text = build.modifiedSPECIAL(SPECIAL.Endurance).ToString();
            labelMainCharisma.Text = build.modifiedSPECIAL(SPECIAL.Charisma).ToString();
            labelMainIntelligence.Text = build.modifiedSPECIAL(SPECIAL.Intelligence).ToString();
            labelMainAgility.Text = build.modifiedSPECIAL(SPECIAL.Agility).ToString();
            labelMainLuck.Text = build.modifiedSPECIAL(SPECIAL.Luck).ToString();

            // you're SPECIAL buttons
            buttonYoureSPECIALStrength.Enabled = build.youreSPECIAL == -1;
            buttonYoureSPECIALPerception.Enabled = build.youreSPECIAL == -1;
            buttonYoureSPECIALEndurance.Enabled = build.youreSPECIAL == -1;
            buttonYoureSPECIALCharisma.Enabled = build.youreSPECIAL == -1;
            buttonYoureSPECIALIntelligence.Enabled = build.youreSPECIAL == -1;
            buttonYoureSPECIALAgility.Enabled = build.youreSPECIAL == -1;
            buttonYoureSPECIALLuck.Enabled = build.youreSPECIAL == -1;

            buttonYoureSPECIALStrength.Visible = build.youreSPECIAL == -1 || build.youreSPECIAL == (int)SPECIAL.Strength;
            buttonYoureSPECIALPerception.Visible = build.youreSPECIAL == -1 || build.youreSPECIAL == (int)SPECIAL.Perception;
            buttonYoureSPECIALEndurance.Visible = build.youreSPECIAL == -1 || build.youreSPECIAL == (int)SPECIAL.Endurance;
            buttonYoureSPECIALCharisma.Visible = build.youreSPECIAL == -1 || build.youreSPECIAL == (int)SPECIAL.Charisma;
            buttonYoureSPECIALIntelligence.Visible = build.youreSPECIAL == -1 || build.youreSPECIAL == (int)SPECIAL.Intelligence;
            buttonYoureSPECIALAgility.Visible = build.youreSPECIAL == -1 || build.youreSPECIAL == (int)SPECIAL.Agility;
            buttonYoureSPECIALLuck.Visible = build.youreSPECIAL == -1 || build.youreSPECIAL == (int)SPECIAL.Luck;

            // bobblehead buttons
            buttonBobbleheadStrength.Enabled = !build.bobbleheads[(int)SPECIAL.Strength];
            buttonBobbleheadPerception.Enabled = !build.bobbleheads[(int)SPECIAL.Perception];
            buttonBobbleheadEndurance.Enabled = !build.bobbleheads[(int)SPECIAL.Endurance];
            buttonBobbleheadCharisma.Enabled = !build.bobbleheads[(int)SPECIAL.Charisma];
            buttonBobbleheadIntelligence.Enabled = !build.bobbleheads[(int)SPECIAL.Intelligence];
            buttonBobbleheadAgility.Enabled = !build.bobbleheads[(int)SPECIAL.Agility];
            buttonBobbleheadLuck.Enabled = !build.bobbleheads[(int)SPECIAL.Luck];

            // perk level labels
            labelS0.Text = build.perks[(int)SPECIAL.Strength][0].level.ToString();
            labelS1.Text = build.perks[(int)SPECIAL.Strength][1].level.ToString();
            labelS2.Text = build.perks[(int)SPECIAL.Strength][2].level.ToString();
            labelS3.Text = build.perks[(int)SPECIAL.Strength][3].level.ToString();
            labelS4.Text = build.perks[(int)SPECIAL.Strength][4].level.ToString();
            labelS5.Text = build.perks[(int)SPECIAL.Strength][5].level.ToString();
            labelS6.Text = build.perks[(int)SPECIAL.Strength][6].level.ToString();
            labelS7.Text = build.perks[(int)SPECIAL.Strength][7].level.ToString();
            labelS8.Text = build.perks[(int)SPECIAL.Strength][8].level.ToString();
            labelS9.Text = build.perks[(int)SPECIAL.Strength][9].level.ToString();

            labelP0.Text = build.perks[(int)SPECIAL.Perception][0].level.ToString();
            labelP1.Text = build.perks[(int)SPECIAL.Perception][1].level.ToString();
            labelP2.Text = build.perks[(int)SPECIAL.Perception][2].level.ToString();
            labelP3.Text = build.perks[(int)SPECIAL.Perception][3].level.ToString();
            labelP4.Text = build.perks[(int)SPECIAL.Perception][4].level.ToString();
            labelP5.Text = build.perks[(int)SPECIAL.Perception][5].level.ToString();
            labelP6.Text = build.perks[(int)SPECIAL.Perception][6].level.ToString();
            labelP7.Text = build.perks[(int)SPECIAL.Perception][7].level.ToString();
            labelP8.Text = build.perks[(int)SPECIAL.Perception][8].level.ToString();
            labelP9.Text = build.perks[(int)SPECIAL.Perception][9].level.ToString();

            labelE0.Text = build.perks[(int)SPECIAL.Endurance][0].level.ToString();
            labelE1.Text = build.perks[(int)SPECIAL.Endurance][1].level.ToString();
            labelE2.Text = build.perks[(int)SPECIAL.Endurance][2].level.ToString();
            labelE3.Text = build.perks[(int)SPECIAL.Endurance][3].level.ToString();
            labelE4.Text = build.perks[(int)SPECIAL.Endurance][4].level.ToString();
            labelE5.Text = build.perks[(int)SPECIAL.Endurance][5].level.ToString();
            labelE6.Text = build.perks[(int)SPECIAL.Endurance][6].level.ToString();
            labelE7.Text = build.perks[(int)SPECIAL.Endurance][7].level.ToString();
            labelE8.Text = build.perks[(int)SPECIAL.Endurance][8].level.ToString();
            labelE9.Text = build.perks[(int)SPECIAL.Endurance][9].level.ToString();

            labelC0.Text = build.perks[(int)SPECIAL.Charisma][0].level.ToString();
            labelC1.Text = build.perks[(int)SPECIAL.Charisma][1].level.ToString();
            labelC2.Text = build.perks[(int)SPECIAL.Charisma][2].level.ToString();
            labelC3.Text = build.perks[(int)SPECIAL.Charisma][3].level.ToString();
            labelC4.Text = build.perks[(int)SPECIAL.Charisma][4].level.ToString();
            labelC5.Text = build.perks[(int)SPECIAL.Charisma][5].level.ToString();
            labelC6.Text = build.perks[(int)SPECIAL.Charisma][6].level.ToString();
            labelC7.Text = build.perks[(int)SPECIAL.Charisma][7].level.ToString();
            labelC8.Text = build.perks[(int)SPECIAL.Charisma][8].level.ToString();
            labelC9.Text = build.perks[(int)SPECIAL.Charisma][9].level.ToString();

            labelI0.Text = build.perks[(int)SPECIAL.Intelligence][0].level.ToString();
            labelI1.Text = build.perks[(int)SPECIAL.Intelligence][1].level.ToString();
            labelI2.Text = build.perks[(int)SPECIAL.Intelligence][2].level.ToString();
            labelI3.Text = build.perks[(int)SPECIAL.Intelligence][3].level.ToString();
            labelI4.Text = build.perks[(int)SPECIAL.Intelligence][4].level.ToString();
            labelI5.Text = build.perks[(int)SPECIAL.Intelligence][5].level.ToString();
            labelI6.Text = build.perks[(int)SPECIAL.Intelligence][6].level.ToString();
            labelI7.Text = build.perks[(int)SPECIAL.Intelligence][7].level.ToString();
            labelI8.Text = build.perks[(int)SPECIAL.Intelligence][8].level.ToString();
            labelI9.Text = build.perks[(int)SPECIAL.Intelligence][9].level.ToString();

            labelA0.Text = build.perks[(int)SPECIAL.Agility][0].level.ToString();
            labelA1.Text = build.perks[(int)SPECIAL.Agility][1].level.ToString();
            labelA2.Text = build.perks[(int)SPECIAL.Agility][2].level.ToString();
            labelA3.Text = build.perks[(int)SPECIAL.Agility][3].level.ToString();
            labelA4.Text = build.perks[(int)SPECIAL.Agility][4].level.ToString();
            labelA5.Text = build.perks[(int)SPECIAL.Agility][5].level.ToString();
            labelA6.Text = build.perks[(int)SPECIAL.Agility][6].level.ToString();
            labelA7.Text = build.perks[(int)SPECIAL.Agility][7].level.ToString();
            labelA8.Text = build.perks[(int)SPECIAL.Agility][8].level.ToString();
            labelA9.Text = build.perks[(int)SPECIAL.Agility][9].level.ToString();

            labelL0.Text = build.perks[(int)SPECIAL.Luck][0].level.ToString();
            labelL1.Text = build.perks[(int)SPECIAL.Luck][1].level.ToString();
            labelL2.Text = build.perks[(int)SPECIAL.Luck][2].level.ToString();
            labelL3.Text = build.perks[(int)SPECIAL.Luck][3].level.ToString();
            labelL4.Text = build.perks[(int)SPECIAL.Luck][4].level.ToString();
            labelL5.Text = build.perks[(int)SPECIAL.Luck][5].level.ToString();
            labelL6.Text = build.perks[(int)SPECIAL.Luck][6].level.ToString();
            labelL7.Text = build.perks[(int)SPECIAL.Luck][7].level.ToString();
            labelL8.Text = build.perks[(int)SPECIAL.Luck][8].level.ToString();
            labelL9.Text = build.perks[(int)SPECIAL.Luck][9].level.ToString();

            // perk level labels grey out
            labelS0.Visible = build.perks[(int)SPECIAL.Strength][0].level > 0;
            labelS1.Visible = build.perks[(int)SPECIAL.Strength][1].level > 0;
            labelS2.Visible = build.perks[(int)SPECIAL.Strength][2].level > 0;
            labelS3.Visible = build.perks[(int)SPECIAL.Strength][3].level > 0;
            labelS4.Visible = build.perks[(int)SPECIAL.Strength][4].level > 0;
            labelS5.Visible = build.perks[(int)SPECIAL.Strength][5].level > 0;
            labelS6.Visible = build.perks[(int)SPECIAL.Strength][6].level > 0;
            labelS7.Visible = build.perks[(int)SPECIAL.Strength][7].level > 0;
            labelS8.Visible = build.perks[(int)SPECIAL.Strength][8].level > 0;
            labelS9.Visible = build.perks[(int)SPECIAL.Strength][9].level > 0;

            labelP0.Visible = build.perks[(int)SPECIAL.Perception][0].level > 0;
            labelP1.Visible = build.perks[(int)SPECIAL.Perception][1].level > 0;
            labelP2.Visible = build.perks[(int)SPECIAL.Perception][2].level > 0;
            labelP3.Visible = build.perks[(int)SPECIAL.Perception][3].level > 0;
            labelP4.Visible = build.perks[(int)SPECIAL.Perception][4].level > 0;
            labelP5.Visible = build.perks[(int)SPECIAL.Perception][5].level > 0;
            labelP6.Visible = build.perks[(int)SPECIAL.Perception][6].level > 0;
            labelP7.Visible = build.perks[(int)SPECIAL.Perception][7].level > 0;
            labelP8.Visible = build.perks[(int)SPECIAL.Perception][8].level > 0;
            labelP9.Visible = build.perks[(int)SPECIAL.Perception][9].level > 0;

            labelE0.Visible = build.perks[(int)SPECIAL.Endurance][0].level > 0;
            labelE1.Visible = build.perks[(int)SPECIAL.Endurance][1].level > 0;
            labelE2.Visible = build.perks[(int)SPECIAL.Endurance][2].level > 0;
            labelE3.Visible = build.perks[(int)SPECIAL.Endurance][3].level > 0;
            labelE4.Visible = build.perks[(int)SPECIAL.Endurance][4].level > 0;
            labelE5.Visible = build.perks[(int)SPECIAL.Endurance][5].level > 0;
            labelE6.Visible = build.perks[(int)SPECIAL.Endurance][6].level > 0;
            labelE7.Visible = build.perks[(int)SPECIAL.Endurance][7].level > 0;
            labelE8.Visible = build.perks[(int)SPECIAL.Endurance][8].level > 0;
            labelE9.Visible = build.perks[(int)SPECIAL.Endurance][9].level > 0;

            labelC0.Visible = build.perks[(int)SPECIAL.Charisma][0].level > 0;
            labelC1.Visible = build.perks[(int)SPECIAL.Charisma][1].level > 0;
            labelC2.Visible = build.perks[(int)SPECIAL.Charisma][2].level > 0;
            labelC3.Visible = build.perks[(int)SPECIAL.Charisma][3].level > 0;
            labelC4.Visible = build.perks[(int)SPECIAL.Charisma][4].level > 0;
            labelC5.Visible = build.perks[(int)SPECIAL.Charisma][5].level > 0;
            labelC6.Visible = build.perks[(int)SPECIAL.Charisma][6].level > 0;
            labelC7.Visible = build.perks[(int)SPECIAL.Charisma][7].level > 0;
            labelC8.Visible = build.perks[(int)SPECIAL.Charisma][8].level > 0;
            labelC9.Visible = build.perks[(int)SPECIAL.Charisma][9].level > 0;

            labelI0.Visible = build.perks[(int)SPECIAL.Intelligence][0].level > 0;
            labelI1.Visible = build.perks[(int)SPECIAL.Intelligence][1].level > 0;
            labelI2.Visible = build.perks[(int)SPECIAL.Intelligence][2].level > 0;
            labelI3.Visible = build.perks[(int)SPECIAL.Intelligence][3].level > 0;
            labelI4.Visible = build.perks[(int)SPECIAL.Intelligence][4].level > 0;
            labelI5.Visible = build.perks[(int)SPECIAL.Intelligence][5].level > 0;
            labelI6.Visible = build.perks[(int)SPECIAL.Intelligence][6].level > 0;
            labelI7.Visible = build.perks[(int)SPECIAL.Intelligence][7].level > 0;
            labelI8.Visible = build.perks[(int)SPECIAL.Intelligence][8].level > 0;
            labelI9.Visible = build.perks[(int)SPECIAL.Intelligence][9].level > 0;

            labelA0.Visible = build.perks[(int)SPECIAL.Agility][0].level > 0;
            labelA1.Visible = build.perks[(int)SPECIAL.Agility][1].level > 0;
            labelA2.Visible = build.perks[(int)SPECIAL.Agility][2].level > 0;
            labelA3.Visible = build.perks[(int)SPECIAL.Agility][3].level > 0;
            labelA4.Visible = build.perks[(int)SPECIAL.Agility][4].level > 0;
            labelA5.Visible = build.perks[(int)SPECIAL.Agility][5].level > 0;
            labelA6.Visible = build.perks[(int)SPECIAL.Agility][6].level > 0;
            labelA7.Visible = build.perks[(int)SPECIAL.Agility][7].level > 0;
            labelA8.Visible = build.perks[(int)SPECIAL.Agility][8].level > 0;
            labelA9.Visible = build.perks[(int)SPECIAL.Agility][9].level > 0;

            labelL0.Visible = build.perks[(int)SPECIAL.Luck][0].level > 0;
            labelL1.Visible = build.perks[(int)SPECIAL.Luck][1].level > 0;
            labelL2.Visible = build.perks[(int)SPECIAL.Luck][2].level > 0;
            labelL3.Visible = build.perks[(int)SPECIAL.Luck][3].level > 0;
            labelL4.Visible = build.perks[(int)SPECIAL.Luck][4].level > 0;
            labelL5.Visible = build.perks[(int)SPECIAL.Luck][5].level > 0;
            labelL6.Visible = build.perks[(int)SPECIAL.Luck][6].level > 0;
            labelL7.Visible = build.perks[(int)SPECIAL.Luck][7].level > 0;
            labelL8.Visible = build.perks[(int)SPECIAL.Luck][8].level > 0;
            labelL9.Visible = build.perks[(int)SPECIAL.Luck][9].level > 0;

            // perk name labels
            labelS0name.Text = build.perks[(int)SPECIAL.Strength][0].name;
            labelS1name.Text = build.perks[(int)SPECIAL.Strength][1].name;
            labelS2name.Text = build.perks[(int)SPECIAL.Strength][2].name;
            labelS3name.Text = build.perks[(int)SPECIAL.Strength][3].name;
            labelS4name.Text = build.perks[(int)SPECIAL.Strength][4].name;
            labelS5name.Text = build.perks[(int)SPECIAL.Strength][5].name;
            labelS6name.Text = build.perks[(int)SPECIAL.Strength][6].name;
            labelS7name.Text = build.perks[(int)SPECIAL.Strength][7].name;
            labelS8name.Text = build.perks[(int)SPECIAL.Strength][8].name;
            labelS9name.Text = build.perks[(int)SPECIAL.Strength][9].name;

            labelP0name.Text = build.perks[(int)SPECIAL.Perception][0].name;
            labelP1name.Text = build.perks[(int)SPECIAL.Perception][1].name;
            labelP2name.Text = build.perks[(int)SPECIAL.Perception][2].name;
            labelP3name.Text = build.perks[(int)SPECIAL.Perception][3].name;
            labelP4name.Text = build.perks[(int)SPECIAL.Perception][4].name;
            labelP5name.Text = build.perks[(int)SPECIAL.Perception][5].name;
            labelP6name.Text = build.perks[(int)SPECIAL.Perception][6].name;
            labelP7name.Text = build.perks[(int)SPECIAL.Perception][7].name;
            labelP8name.Text = build.perks[(int)SPECIAL.Perception][8].name;
            labelP9name.Text = build.perks[(int)SPECIAL.Perception][9].name;

            labelE0name.Text = build.perks[(int)SPECIAL.Endurance][0].name;
            labelE1name.Text = build.perks[(int)SPECIAL.Endurance][1].name;
            labelE2name.Text = build.perks[(int)SPECIAL.Endurance][2].name;
            labelE3name.Text = build.perks[(int)SPECIAL.Endurance][3].name;
            labelE4name.Text = build.perks[(int)SPECIAL.Endurance][4].name;
            labelE5name.Text = build.perks[(int)SPECIAL.Endurance][5].name;
            labelE6name.Text = build.perks[(int)SPECIAL.Endurance][6].name;
            labelE7name.Text = build.perks[(int)SPECIAL.Endurance][7].name;
            labelE8name.Text = build.perks[(int)SPECIAL.Endurance][8].name;
            labelE9name.Text = build.perks[(int)SPECIAL.Endurance][9].name;

            labelC0name.Text = build.perks[(int)SPECIAL.Charisma][0].name;
            labelC1name.Text = build.perks[(int)SPECIAL.Charisma][1].name;
            labelC2name.Text = build.perks[(int)SPECIAL.Charisma][2].name;
            labelC3name.Text = build.perks[(int)SPECIAL.Charisma][3].name;
            labelC4name.Text = build.perks[(int)SPECIAL.Charisma][4].name;
            labelC5name.Text = build.perks[(int)SPECIAL.Charisma][5].name;
            labelC6name.Text = build.perks[(int)SPECIAL.Charisma][6].name;
            labelC7name.Text = build.perks[(int)SPECIAL.Charisma][7].name;
            labelC8name.Text = build.perks[(int)SPECIAL.Charisma][8].name;
            labelC9name.Text = build.perks[(int)SPECIAL.Charisma][9].name;

            labelI0name.Text = build.perks[(int)SPECIAL.Intelligence][0].name;
            labelI1name.Text = build.perks[(int)SPECIAL.Intelligence][1].name;
            labelI2name.Text = build.perks[(int)SPECIAL.Intelligence][2].name;
            labelI3name.Text = build.perks[(int)SPECIAL.Intelligence][3].name;
            labelI4name.Text = build.perks[(int)SPECIAL.Intelligence][4].name;
            labelI5name.Text = build.perks[(int)SPECIAL.Intelligence][5].name;
            labelI6name.Text = build.perks[(int)SPECIAL.Intelligence][6].name;
            labelI7name.Text = build.perks[(int)SPECIAL.Intelligence][7].name;
            labelI8name.Text = build.perks[(int)SPECIAL.Intelligence][8].name;
            labelI9name.Text = build.perks[(int)SPECIAL.Intelligence][9].name;

            labelA0name.Text = build.perks[(int)SPECIAL.Agility][0].name;
            labelA1name.Text = build.perks[(int)SPECIAL.Agility][1].name;
            labelA2name.Text = build.perks[(int)SPECIAL.Agility][2].name;
            labelA3name.Text = build.perks[(int)SPECIAL.Agility][3].name;
            labelA4name.Text = build.perks[(int)SPECIAL.Agility][4].name;
            labelA5name.Text = build.perks[(int)SPECIAL.Agility][5].name;
            labelA6name.Text = build.perks[(int)SPECIAL.Agility][6].name;
            labelA7name.Text = build.perks[(int)SPECIAL.Agility][7].name;
            labelA8name.Text = build.perks[(int)SPECIAL.Agility][8].name;
            labelA9name.Text = build.perks[(int)SPECIAL.Agility][9].name;

            labelL0name.Text = build.perks[(int)SPECIAL.Luck][0].name;
            labelL1name.Text = build.perks[(int)SPECIAL.Luck][1].name;
            labelL2name.Text = build.perks[(int)SPECIAL.Luck][2].name;
            labelL3name.Text = build.perks[(int)SPECIAL.Luck][3].name;
            labelL4name.Text = build.perks[(int)SPECIAL.Luck][4].name;
            labelL5name.Text = build.perks[(int)SPECIAL.Luck][5].name;
            labelL6name.Text = build.perks[(int)SPECIAL.Luck][6].name;
            labelL7name.Text = build.perks[(int)SPECIAL.Luck][7].name;
            labelL8name.Text = build.perks[(int)SPECIAL.Luck][8].name;
            labelL9name.Text = build.perks[(int)SPECIAL.Luck][9].name;

            // perk name labels grey out
            labelS0name.Enabled = build.perks[(int)SPECIAL.Strength][0].level > 0;
            labelS1name.Enabled = build.perks[(int)SPECIAL.Strength][1].level > 0;
            labelS2name.Enabled = build.perks[(int)SPECIAL.Strength][2].level > 0;
            labelS3name.Enabled = build.perks[(int)SPECIAL.Strength][3].level > 0;
            labelS4name.Enabled = build.perks[(int)SPECIAL.Strength][4].level > 0;
            labelS5name.Enabled = build.perks[(int)SPECIAL.Strength][5].level > 0;
            labelS6name.Enabled = build.perks[(int)SPECIAL.Strength][6].level > 0;
            labelS7name.Enabled = build.perks[(int)SPECIAL.Strength][7].level > 0;
            labelS8name.Enabled = build.perks[(int)SPECIAL.Strength][8].level > 0;
            labelS9name.Enabled = build.perks[(int)SPECIAL.Strength][9].level > 0;

            labelP0name.Enabled = build.perks[(int)SPECIAL.Perception][0].level > 0;
            labelP1name.Enabled = build.perks[(int)SPECIAL.Perception][1].level > 0;
            labelP2name.Enabled = build.perks[(int)SPECIAL.Perception][2].level > 0;
            labelP3name.Enabled = build.perks[(int)SPECIAL.Perception][3].level > 0;
            labelP4name.Enabled = build.perks[(int)SPECIAL.Perception][4].level > 0;
            labelP5name.Enabled = build.perks[(int)SPECIAL.Perception][5].level > 0;
            labelP6name.Enabled = build.perks[(int)SPECIAL.Perception][6].level > 0;
            labelP7name.Enabled = build.perks[(int)SPECIAL.Perception][7].level > 0;
            labelP8name.Enabled = build.perks[(int)SPECIAL.Perception][8].level > 0;
            labelP9name.Enabled = build.perks[(int)SPECIAL.Perception][9].level > 0;

            labelE0name.Enabled = build.perks[(int)SPECIAL.Endurance][0].level > 0;
            labelE1name.Enabled = build.perks[(int)SPECIAL.Endurance][1].level > 0;
            labelE2name.Enabled = build.perks[(int)SPECIAL.Endurance][2].level > 0;
            labelE3name.Enabled = build.perks[(int)SPECIAL.Endurance][3].level > 0;
            labelE4name.Enabled = build.perks[(int)SPECIAL.Endurance][4].level > 0;
            labelE5name.Enabled = build.perks[(int)SPECIAL.Endurance][5].level > 0;
            labelE6name.Enabled = build.perks[(int)SPECIAL.Endurance][6].level > 0;
            labelE7name.Enabled = build.perks[(int)SPECIAL.Endurance][7].level > 0;
            labelE8name.Enabled = build.perks[(int)SPECIAL.Endurance][8].level > 0;
            labelE9name.Enabled = build.perks[(int)SPECIAL.Endurance][9].level > 0;

            labelC0name.Enabled = build.perks[(int)SPECIAL.Charisma][0].level > 0;
            labelC1name.Enabled = build.perks[(int)SPECIAL.Charisma][1].level > 0;
            labelC2name.Enabled = build.perks[(int)SPECIAL.Charisma][2].level > 0;
            labelC3name.Enabled = build.perks[(int)SPECIAL.Charisma][3].level > 0;
            labelC4name.Enabled = build.perks[(int)SPECIAL.Charisma][4].level > 0;
            labelC5name.Enabled = build.perks[(int)SPECIAL.Charisma][5].level > 0;
            labelC6name.Enabled = build.perks[(int)SPECIAL.Charisma][6].level > 0;
            labelC7name.Enabled = build.perks[(int)SPECIAL.Charisma][7].level > 0;
            labelC8name.Enabled = build.perks[(int)SPECIAL.Charisma][8].level > 0;
            labelC9name.Enabled = build.perks[(int)SPECIAL.Charisma][9].level > 0;

            labelI0name.Enabled = build.perks[(int)SPECIAL.Intelligence][0].level > 0;
            labelI1name.Enabled = build.perks[(int)SPECIAL.Intelligence][1].level > 0;
            labelI2name.Enabled = build.perks[(int)SPECIAL.Intelligence][2].level > 0;
            labelI3name.Enabled = build.perks[(int)SPECIAL.Intelligence][3].level > 0;
            labelI4name.Enabled = build.perks[(int)SPECIAL.Intelligence][4].level > 0;
            labelI5name.Enabled = build.perks[(int)SPECIAL.Intelligence][5].level > 0;
            labelI6name.Enabled = build.perks[(int)SPECIAL.Intelligence][6].level > 0;
            labelI7name.Enabled = build.perks[(int)SPECIAL.Intelligence][7].level > 0;
            labelI8name.Enabled = build.perks[(int)SPECIAL.Intelligence][8].level > 0;
            labelI9name.Enabled = build.perks[(int)SPECIAL.Intelligence][9].level > 0;

            labelA0name.Enabled = build.perks[(int)SPECIAL.Agility][0].level > 0;
            labelA1name.Enabled = build.perks[(int)SPECIAL.Agility][1].level > 0;
            labelA2name.Enabled = build.perks[(int)SPECIAL.Agility][2].level > 0;
            labelA3name.Enabled = build.perks[(int)SPECIAL.Agility][3].level > 0;
            labelA4name.Enabled = build.perks[(int)SPECIAL.Agility][4].level > 0;
            labelA5name.Enabled = build.perks[(int)SPECIAL.Agility][5].level > 0;
            labelA6name.Enabled = build.perks[(int)SPECIAL.Agility][6].level > 0;
            labelA7name.Enabled = build.perks[(int)SPECIAL.Agility][7].level > 0;
            labelA8name.Enabled = build.perks[(int)SPECIAL.Agility][8].level > 0;
            labelA9name.Enabled = build.perks[(int)SPECIAL.Agility][9].level > 0;

            labelL0name.Enabled = build.perks[(int)SPECIAL.Luck][0].level > 0;
            labelL1name.Enabled = build.perks[(int)SPECIAL.Luck][1].level > 0;
            labelL2name.Enabled = build.perks[(int)SPECIAL.Luck][2].level > 0;
            labelL3name.Enabled = build.perks[(int)SPECIAL.Luck][3].level > 0;
            labelL4name.Enabled = build.perks[(int)SPECIAL.Luck][4].level > 0;
            labelL5name.Enabled = build.perks[(int)SPECIAL.Luck][5].level > 0;
            labelL6name.Enabled = build.perks[(int)SPECIAL.Luck][6].level > 0;
            labelL7name.Enabled = build.perks[(int)SPECIAL.Luck][7].level > 0;
            labelL8name.Enabled = build.perks[(int)SPECIAL.Luck][8].level > 0;
            labelL9name.Enabled = build.perks[(int)SPECIAL.Luck][9].level > 0;

            // remove focus from buttons
            tabPageMain.Focus();
        }

        private void updateLevelUpTab()
        {
            nextPerk = build.selectRandomPerk();

            // labels
            labelLevelUpLevel.Text = "Level " + build.level.ToString() + "!";
            labelNextPerk.Text = build.perks[nextPerk[0]][nextPerk[1]].name + ": " + nextPerk[2];

            // reroll button
            buttonPerkReroll.Enabled = config.allowRerolls;
        }

        private void buttonReroll_Click(object sender, EventArgs e)
        {
            build = new Build(config);
            updateRollTab();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            // switch to main tab
            updateMainTab();
            tabControlMain.SelectedIndex = 2;
        }

        private void buttonYoureSPECIALStrength_Click(object sender, EventArgs e)
        {
            build.applyYoureSPECIAL(SPECIAL.Strength);
            updateMainTab();
        }

        private void buttonYoureSPECIALPerception_Click(object sender, EventArgs e)
        {
            build.applyYoureSPECIAL(SPECIAL.Perception);
            updateMainTab();
        }

        private void buttonYoureSPECIALEndurance_Click(object sender, EventArgs e)
        {
            build.applyYoureSPECIAL(SPECIAL.Endurance);
            updateMainTab();
        }

        private void buttonYoureSPECIALCharisma_Click(object sender, EventArgs e)
        {
            build.applyYoureSPECIAL(SPECIAL.Charisma);
            updateMainTab();
        }

        private void buttonYoureSPECIALIntelligence_Click(object sender, EventArgs e)
        {
            build.applyYoureSPECIAL(SPECIAL.Intelligence);
            updateMainTab();
        }

        private void buttonYoureSPECIALAgility_Click(object sender, EventArgs e)
        {
            build.applyYoureSPECIAL(SPECIAL.Agility);
            updateMainTab();
        }

        private void buttonYoureSPECIALLuck_Click(object sender, EventArgs e)
        {
            build.applyYoureSPECIAL(SPECIAL.Luck);
            updateMainTab();
        }

        private void buttonBobbleheadStrength_Click(object sender, EventArgs e)
        {
            build.applyBobblehead(SPECIAL.Strength);
            updateMainTab();
        }

        private void buttonBobbleheadPerception_Click(object sender, EventArgs e)
        {
            build.applyBobblehead(SPECIAL.Perception);
            updateMainTab();
        }

        private void buttonBobbleheadEndurance_Click(object sender, EventArgs e)
        {
            build.applyBobblehead(SPECIAL.Endurance);
            updateMainTab();
        }

        private void buttonBobbleheadCharisma_Click(object sender, EventArgs e)
        {
            build.applyBobblehead(SPECIAL.Charisma);
            updateMainTab();
        }

        private void buttonBobbleheadIntelligence_Click(object sender, EventArgs e)
        {
            build.applyBobblehead(SPECIAL.Intelligence);
            updateMainTab();
        }

        private void buttonBobbleheadAgility_Click(object sender, EventArgs e)
        {
            build.applyBobblehead(SPECIAL.Agility);
            updateMainTab();
        }

        private void buttonBobbleheadLuck_Click(object sender, EventArgs e)
        {
            build.applyBobblehead(SPECIAL.Luck);
            updateMainTab();
        }

        private void buttonLevelUp_Click(object sender, EventArgs e)
        {
            build.levelUp();

            // switch to level up tab
            updateLevelUpTab();
            tabControlMain.SelectedIndex = 3;
        }

        private void buttonPerkReroll_Click(object sender, EventArgs e)
        {
            updateLevelUpTab();
        }

        private void buttonPerkAccept_Click(object sender, EventArgs e)
        {
            build.takePerk((SPECIAL)nextPerk[0], nextPerk[1], nextPerk[2]);

            // switch to main tab
            updateMainTab();
            tabControlMain.SelectedIndex = 2;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(build, jsonOptions);
                Console.WriteLine(jsonString);

                Stream fileStream = saveFileDialog.OpenFile();
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.Write(jsonString);
                }
                fileStream.Close();
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream fileStream = openFileDialog.OpenFile();
                    string jsonString;
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        jsonString = reader.ReadToEnd();
                    }

                    Build readBuild = JsonSerializer.Deserialize<Build>(jsonString);
                    build = readBuild;
                    config = build.config;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error opening " + openFileDialog.FileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // switch to main tab
                updateMainTab();
                tabControlMain.SelectedIndex = 2;
            }
        }

        private void linkLabelGit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/SimplexFatberg/FO4BuildRandomiser/");
        }
    }
}
