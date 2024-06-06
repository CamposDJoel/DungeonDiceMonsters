namespace DungeonDiceMonsters
{
    partial class JsonGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCardName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.listAttribute = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listType = new System.Windows.Forms.ListBox();
            this.listCategory = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtATK = new System.Windows.Forms.TextBox();
            this.txtDef = new System.Windows.Forms.TextBox();
            this.txtLp = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listFace6Value = new System.Windows.Forms.ListBox();
            this.listFace6Crest = new System.Windows.Forms.ListBox();
            this.label18 = new System.Windows.Forms.Label();
            this.listFace5Value = new System.Windows.Forms.ListBox();
            this.listFace5Crest = new System.Windows.Forms.ListBox();
            this.label17 = new System.Windows.Forms.Label();
            this.listFace4Value = new System.Windows.Forms.ListBox();
            this.listFace4Crest = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.listFace3Value = new System.Windows.Forms.ListBox();
            this.listFace3Crest = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.listFace2Value = new System.Windows.Forms.ListBox();
            this.listFace2Crest = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.numDiceLevel = new System.Windows.Forms.NumericUpDown();
            this.listFace1Value = new System.Windows.Forms.ListBox();
            this.listFace1Crest = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.listCardList = new System.Windows.Forms.ListBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnAddCard = new System.Windows.Forms.Button();
            this.btnEditSelected = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.numID = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.listSecType = new System.Windows.Forms.ListBox();
            this.txtOnSumon = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtContiEffect = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtIgnitionEffect = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtAbility = new System.Windows.Forms.TextBox();
            this.txtFullCardData = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnFullCardStringAddToDB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiceLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Card Name:";
            // 
            // txtCardName
            // 
            this.txtCardName.Location = new System.Drawing.Point(234, 66);
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.Size = new System.Drawing.Size(281, 20);
            this.txtCardName.TabIndex = 3;
            this.txtCardName.Text = "tmp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Level:";
            // 
            // numLevel
            // 
            this.numLevel.Location = new System.Drawing.Point(368, 21);
            this.numLevel.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(55, 20);
            this.numLevel.TabIndex = 5;
            this.numLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Attribute:";
            // 
            // listAttribute
            // 
            this.listAttribute.FormattingEnabled = true;
            this.listAttribute.Items.AddRange(new object[] {
            "DARK",
            "LIGHT",
            "WATER",
            "FIRE",
            "EARTH",
            "WIND",
            "DIVINE",
            "SPELL",
            "TRAP"});
            this.listAttribute.Location = new System.Drawing.Point(236, 165);
            this.listAttribute.Name = "listAttribute";
            this.listAttribute.Size = new System.Drawing.Size(120, 121);
            this.listAttribute.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(357, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Type:";
            // 
            // listType
            // 
            this.listType.FormattingEnabled = true;
            this.listType.Items.AddRange(new object[] {
            "Aqua",
            "Beast",
            "Beast-Warrior",
            "Cyberse",
            "Dinosaur",
            "Divine-Beast",
            "Dragon",
            "Fairy",
            "Fiend",
            "Fish",
            "Insect",
            "Machine",
            "Plant",
            "Psychic",
            "Pyro",
            "Reptile",
            "Rock",
            "Sea Serpent",
            "Spellcaster",
            "Thunder",
            "Warrior",
            "Winged Beast",
            "Wyrm",
            "Zombie",
            "Normal",
            "Continuous",
            "Equip",
            "Field",
            "Ritual"});
            this.listType.Location = new System.Drawing.Point(360, 104);
            this.listType.Name = "listType";
            this.listType.Size = new System.Drawing.Size(120, 56);
            this.listType.TabIndex = 9;
            // 
            // listCategory
            // 
            this.listCategory.FormattingEnabled = true;
            this.listCategory.Items.AddRange(new object[] {
            "Monster",
            "Spell",
            "Trap"});
            this.listCategory.Location = new System.Drawing.Point(234, 104);
            this.listCategory.Name = "listCategory";
            this.listCategory.Size = new System.Drawing.Size(120, 43);
            this.listCategory.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(233, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Category:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(433, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "ATA:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(433, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "DEF:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(441, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "LP:";
            // 
            // txtATK
            // 
            this.txtATK.Location = new System.Drawing.Point(475, 2);
            this.txtATK.Name = "txtATK";
            this.txtATK.Size = new System.Drawing.Size(71, 20);
            this.txtATK.TabIndex = 15;
            this.txtATK.Text = "0000";
            // 
            // txtDef
            // 
            this.txtDef.Location = new System.Drawing.Point(475, 23);
            this.txtDef.Name = "txtDef";
            this.txtDef.Size = new System.Drawing.Size(71, 20);
            this.txtDef.TabIndex = 16;
            this.txtDef.Text = "0000";
            // 
            // txtLp
            // 
            this.txtLp.Location = new System.Drawing.Point(475, 44);
            this.txtLp.Name = "txtLp";
            this.txtLp.Size = new System.Drawing.Size(71, 20);
            this.txtLp.TabIndex = 17;
            this.txtLp.Text = "0000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listFace6Value);
            this.groupBox1.Controls.Add(this.listFace6Crest);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.listFace5Value);
            this.groupBox1.Controls.Add(this.listFace5Crest);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.listFace4Value);
            this.groupBox1.Controls.Add(this.listFace4Crest);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.listFace3Value);
            this.groupBox1.Controls.Add(this.listFace3Crest);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.listFace2Value);
            this.groupBox1.Controls.Add(this.listFace2Crest);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.numDiceLevel);
            this.groupBox1.Controls.Add(this.listFace1Value);
            this.groupBox1.Controls.Add(this.listFace1Crest);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(15, 283);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 155);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dice Info";
            // 
            // listFace6Value
            // 
            this.listFace6Value.FormattingEnabled = true;
            this.listFace6Value.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.listFace6Value.Location = new System.Drawing.Point(662, 54);
            this.listFace6Value.Name = "listFace6Value";
            this.listFace6Value.Size = new System.Drawing.Size(23, 95);
            this.listFace6Value.TabIndex = 27;
            // 
            // listFace6Crest
            // 
            this.listFace6Crest.FormattingEnabled = true;
            this.listFace6Crest.Items.AddRange(new object[] {
            "STAR",
            "ATK",
            "DEF",
            "MOV",
            "MAG",
            "TRAP",
            "RITU"});
            this.listFace6Crest.Location = new System.Drawing.Point(590, 54);
            this.listFace6Crest.Name = "listFace6Crest";
            this.listFace6Crest.Size = new System.Drawing.Size(66, 95);
            this.listFace6Crest.TabIndex = 26;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(595, 38);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(43, 13);
            this.label18.TabIndex = 25;
            this.label18.Text = "Face 6:";
            // 
            // listFace5Value
            // 
            this.listFace5Value.FormattingEnabled = true;
            this.listFace5Value.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.listFace5Value.Location = new System.Drawing.Point(545, 54);
            this.listFace5Value.Name = "listFace5Value";
            this.listFace5Value.Size = new System.Drawing.Size(23, 95);
            this.listFace5Value.TabIndex = 24;
            // 
            // listFace5Crest
            // 
            this.listFace5Crest.FormattingEnabled = true;
            this.listFace5Crest.Items.AddRange(new object[] {
            "STAR",
            "ATK",
            "DEF",
            "MOV",
            "MAG",
            "TRAP",
            "RITU"});
            this.listFace5Crest.Location = new System.Drawing.Point(473, 54);
            this.listFace5Crest.Name = "listFace5Crest";
            this.listFace5Crest.Size = new System.Drawing.Size(66, 95);
            this.listFace5Crest.TabIndex = 23;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(478, 38);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 13);
            this.label17.TabIndex = 22;
            this.label17.Text = "Face 5:";
            // 
            // listFace4Value
            // 
            this.listFace4Value.FormattingEnabled = true;
            this.listFace4Value.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.listFace4Value.Location = new System.Drawing.Point(430, 54);
            this.listFace4Value.Name = "listFace4Value";
            this.listFace4Value.Size = new System.Drawing.Size(23, 95);
            this.listFace4Value.TabIndex = 21;
            // 
            // listFace4Crest
            // 
            this.listFace4Crest.FormattingEnabled = true;
            this.listFace4Crest.Items.AddRange(new object[] {
            "STAR",
            "ATK",
            "DEF",
            "MOV",
            "MAG",
            "TRAP",
            "RITU"});
            this.listFace4Crest.Location = new System.Drawing.Point(358, 54);
            this.listFace4Crest.Name = "listFace4Crest";
            this.listFace4Crest.Size = new System.Drawing.Size(66, 95);
            this.listFace4Crest.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(363, 38);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 19;
            this.label16.Text = "Face 4:";
            // 
            // listFace3Value
            // 
            this.listFace3Value.FormattingEnabled = true;
            this.listFace3Value.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.listFace3Value.Location = new System.Drawing.Point(313, 54);
            this.listFace3Value.Name = "listFace3Value";
            this.listFace3Value.Size = new System.Drawing.Size(23, 95);
            this.listFace3Value.TabIndex = 18;
            // 
            // listFace3Crest
            // 
            this.listFace3Crest.FormattingEnabled = true;
            this.listFace3Crest.Items.AddRange(new object[] {
            "STAR",
            "ATK",
            "DEF",
            "MOV",
            "MAG",
            "TRAP",
            "RITU"});
            this.listFace3Crest.Location = new System.Drawing.Point(241, 54);
            this.listFace3Crest.Name = "listFace3Crest";
            this.listFace3Crest.Size = new System.Drawing.Size(66, 95);
            this.listFace3Crest.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(246, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Face 3:";
            // 
            // listFace2Value
            // 
            this.listFace2Value.FormattingEnabled = true;
            this.listFace2Value.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.listFace2Value.Location = new System.Drawing.Point(196, 54);
            this.listFace2Value.Name = "listFace2Value";
            this.listFace2Value.Size = new System.Drawing.Size(23, 95);
            this.listFace2Value.TabIndex = 15;
            // 
            // listFace2Crest
            // 
            this.listFace2Crest.FormattingEnabled = true;
            this.listFace2Crest.Items.AddRange(new object[] {
            "STAR",
            "ATK",
            "DEF",
            "MOV",
            "MAG",
            "TRAP",
            "RITU"});
            this.listFace2Crest.Location = new System.Drawing.Point(124, 54);
            this.listFace2Crest.Name = "listFace2Crest";
            this.listFace2Crest.Size = new System.Drawing.Size(66, 95);
            this.listFace2Crest.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(129, 38);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Face 2:";
            // 
            // numDiceLevel
            // 
            this.numDiceLevel.Location = new System.Drawing.Point(272, 13);
            this.numDiceLevel.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numDiceLevel.Name = "numDiceLevel";
            this.numDiceLevel.Size = new System.Drawing.Size(120, 20);
            this.numDiceLevel.TabIndex = 12;
            this.numDiceLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // listFace1Value
            // 
            this.listFace1Value.FormattingEnabled = true;
            this.listFace1Value.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.listFace1Value.Location = new System.Drawing.Point(77, 54);
            this.listFace1Value.Name = "listFace1Value";
            this.listFace1Value.Size = new System.Drawing.Size(23, 95);
            this.listFace1Value.TabIndex = 11;
            // 
            // listFace1Crest
            // 
            this.listFace1Crest.FormattingEnabled = true;
            this.listFace1Crest.Items.AddRange(new object[] {
            "STAR",
            "ATK",
            "DEF",
            "MOV",
            "MAG",
            "TRAP",
            "RITU"});
            this.listFace1Crest.Location = new System.Drawing.Point(5, 54);
            this.listFace1Crest.Name = "listFace1Crest";
            this.listFace1Crest.Size = new System.Drawing.Size(66, 95);
            this.listFace1Crest.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 38);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Face 1:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(205, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Dice Level:";
            // 
            // listCardList
            // 
            this.listCardList.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listCardList.FormattingEnabled = true;
            this.listCardList.ItemHeight = 12;
            this.listCardList.Location = new System.Drawing.Point(15, 11);
            this.listCardList.Name = "listCardList";
            this.listCardList.Size = new System.Drawing.Size(213, 244);
            this.listCardList.TabIndex = 24;
            this.listCardList.SelectedIndexChanged += new System.EventHandler(this.listCardList_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(521, 75);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(99, 13);
            this.label19.TabIndex = 25;
            this.label19.Text = "On Summon Effect:";
            // 
            // btnAddCard
            // 
            this.btnAddCard.BackColor = System.Drawing.Color.Green;
            this.btnAddCard.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAddCard.Location = new System.Drawing.Point(740, 337);
            this.btnAddCard.Name = "btnAddCard";
            this.btnAddCard.Size = new System.Drawing.Size(48, 62);
            this.btnAddCard.TabIndex = 27;
            this.btnAddCard.Text = "Button Add To DB";
            this.btnAddCard.UseVisualStyleBackColor = false;
            this.btnAddCard.Click += new System.EventHandler(this.btnAddCard_Click);
            // 
            // btnEditSelected
            // 
            this.btnEditSelected.BackColor = System.Drawing.Color.Yellow;
            this.btnEditSelected.Location = new System.Drawing.Point(122, 257);
            this.btnEditSelected.Name = "btnEditSelected";
            this.btnEditSelected.Size = new System.Drawing.Size(106, 25);
            this.btnEditSelected.TabIndex = 28;
            this.btnEditSelected.Text = "Update";
            this.btnEditSelected.UseVisualStyleBackColor = false;
            this.btnEditSelected.Click += new System.EventHandler(this.btnEditSelected_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.DarkRed;
            this.btnDelete.Location = new System.Drawing.Point(12, 257);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(106, 25);
            this.btnDelete.TabIndex = 29;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // numID
            // 
            this.numID.Location = new System.Drawing.Point(236, 21);
            this.numID.Name = "numID";
            this.numID.Size = new System.Drawing.Size(123, 20);
            this.numID.TabIndex = 31;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(357, 165);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 13);
            this.label20.TabIndex = 32;
            this.label20.Text = "SecType:";
            // 
            // listSecType
            // 
            this.listSecType.FormattingEnabled = true;
            this.listSecType.Items.AddRange(new object[] {
            "NONE",
            "Normal",
            "Fusion",
            "Ritual"});
            this.listSecType.Location = new System.Drawing.Point(360, 181);
            this.listSecType.Name = "listSecType";
            this.listSecType.Size = new System.Drawing.Size(120, 56);
            this.listSecType.TabIndex = 33;
            // 
            // txtOnSumon
            // 
            this.txtOnSumon.Location = new System.Drawing.Point(523, 91);
            this.txtOnSumon.Multiline = true;
            this.txtOnSumon.Name = "txtOnSumon";
            this.txtOnSumon.Size = new System.Drawing.Size(264, 39);
            this.txtOnSumon.TabIndex = 34;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(520, 133);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 13);
            this.label21.TabIndex = 35;
            this.label21.Text = "Continuous Effect:";
            // 
            // txtContiEffect
            // 
            this.txtContiEffect.Location = new System.Drawing.Point(523, 149);
            this.txtContiEffect.Multiline = true;
            this.txtContiEffect.Name = "txtContiEffect";
            this.txtContiEffect.Size = new System.Drawing.Size(264, 39);
            this.txtContiEffect.TabIndex = 36;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(521, 191);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(75, 13);
            this.label22.TabIndex = 37;
            this.label22.Text = "Ignition Effect:";
            // 
            // txtIgnitionEffect
            // 
            this.txtIgnitionEffect.Location = new System.Drawing.Point(523, 207);
            this.txtIgnitionEffect.Multiline = true;
            this.txtIgnitionEffect.Name = "txtIgnitionEffect";
            this.txtIgnitionEffect.Size = new System.Drawing.Size(264, 79);
            this.txtIgnitionEffect.TabIndex = 38;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(359, 242);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(37, 13);
            this.label23.TabIndex = 39;
            this.label23.Text = "Ability:";
            // 
            // txtAbility
            // 
            this.txtAbility.Location = new System.Drawing.Point(362, 259);
            this.txtAbility.Multiline = true;
            this.txtAbility.Name = "txtAbility";
            this.txtAbility.Size = new System.Drawing.Size(152, 24);
            this.txtAbility.TabIndex = 40;
            // 
            // txtFullCardData
            // 
            this.txtFullCardData.Location = new System.Drawing.Point(557, 23);
            this.txtFullCardData.Multiline = true;
            this.txtFullCardData.Name = "txtFullCardData";
            this.txtFullCardData.Size = new System.Drawing.Size(230, 37);
            this.txtFullCardData.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(555, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 13);
            this.label10.TabIndex = 42;
            this.label10.Text = "Enter a full card data string:";
            // 
            // btnFullCardStringAddToDB
            // 
            this.btnFullCardStringAddToDB.BackColor = System.Drawing.Color.Green;
            this.btnFullCardStringAddToDB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFullCardStringAddToDB.Location = new System.Drawing.Point(710, 62);
            this.btnFullCardStringAddToDB.Name = "btnFullCardStringAddToDB";
            this.btnFullCardStringAddToDB.Size = new System.Drawing.Size(77, 27);
            this.btnFullCardStringAddToDB.TabIndex = 43;
            this.btnFullCardStringAddToDB.Text = "Add To DB";
            this.btnFullCardStringAddToDB.UseVisualStyleBackColor = false;
            this.btnFullCardStringAddToDB.Click += new System.EventHandler(this.btnFullCardStringAddToDB_Click);
            // 
            // JsonGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnFullCardStringAddToDB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtFullCardData);
            this.Controls.Add(this.txtAbility);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtIgnitionEffect);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtContiEffect);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtOnSumon);
            this.Controls.Add(this.listSecType);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.numID);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEditSelected);
            this.Controls.Add(this.btnAddCard);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.listCardList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLp);
            this.Controls.Add(this.txtDef);
            this.Controls.Add(this.txtATK);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listCategory);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listAttribute);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCardName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "JsonGenerator";
            this.Text = "DB Manager";
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiceLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCardName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listAttribute;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listType;
        private System.Windows.Forms.ListBox listCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtATK;
        private System.Windows.Forms.TextBox txtDef;
        private System.Windows.Forms.TextBox txtLp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listFace1Crest;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numDiceLevel;
        private System.Windows.Forms.ListBox listFace1Value;
        private System.Windows.Forms.ListBox listFace6Value;
        private System.Windows.Forms.ListBox listFace6Crest;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListBox listFace5Value;
        private System.Windows.Forms.ListBox listFace5Crest;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ListBox listFace4Value;
        private System.Windows.Forms.ListBox listFace4Crest;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ListBox listFace3Value;
        private System.Windows.Forms.ListBox listFace3Crest;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox listFace2Value;
        private System.Windows.Forms.ListBox listFace2Crest;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox listCardList;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnAddCard;
        private System.Windows.Forms.Button btnEditSelected;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox numID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ListBox listSecType;
        private System.Windows.Forms.TextBox txtOnSumon;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtContiEffect;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtIgnitionEffect;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtAbility;
        private System.Windows.Forms.TextBox txtFullCardData;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnFullCardStringAddToDB;
    }
}