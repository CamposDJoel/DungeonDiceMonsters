namespace DungeonDiceMonsters
{
    partial class BoardForm
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
            this.PanelBoard = new System.Windows.Forms.Panel();
            this.PanelActionMenu = new System.Windows.Forms.Panel();
            this.btnActionCancel = new System.Windows.Forms.Button();
            this.btnActionEffect = new System.Windows.Forms.Button();
            this.btnActionAttack = new System.Windows.Forms.Button();
            this.btnActionMove = new System.Windows.Forms.Button();
            this.PanelBluePlayer = new System.Windows.Forms.Panel();
            this.PanelBlueCrests = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.PicBlueTrapImage = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.PicBlueMagImage = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PicBlueDEFImage = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PicBlueATKImage = new System.Windows.Forms.PictureBox();
            this.lblBlueMovCount = new System.Windows.Forms.Label();
            this.PicBlueMovImage = new System.Windows.Forms.PictureBox();
            this.lblBlueLP = new System.Windows.Forms.Label();
            this.lblLPLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.PicRedTrapImage = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.PicRedMagImage = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.PicRedDEFImage = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.PicRedATKImage = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.PicRedMovImage = new System.Windows.Forms.PictureBox();
            this.lblRedLP = new System.Windows.Forms.Label();
            this.lblLPlabel2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelDebug = new System.Windows.Forms.Panel();
            this.lblMouseCords = new System.Windows.Forms.Label();
            this.lblDebugCardOwner = new System.Windows.Forms.Label();
            this.lblDebugCard = new System.Windows.Forms.Label();
            this.lblDebugIsOccupied = new System.Windows.Forms.Label();
            this.lblDebugOwner = new System.Windows.Forms.Label();
            this.lblDebugWestAdj = new System.Windows.Forms.Label();
            this.lblDebugEastAdj = new System.Windows.Forms.Label();
            this.lblDebugSouthAdj = new System.Windows.Forms.Label();
            this.lblDebugNorthAdj = new System.Windows.Forms.Label();
            this.lblDebugTileID = new System.Windows.Forms.Label();
            this.PanelCardInfo = new System.Windows.Forms.Panel();
            this.lblCardText = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblAttribute = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblCardLevel = new System.Windows.Forms.Label();
            this.lblCardName = new System.Windows.Forms.Label();
            this.PicCardArtworkBottom = new System.Windows.Forms.PictureBox();
            this.PanelBoard.SuspendLayout();
            this.PanelActionMenu.SuspendLayout();
            this.PanelBluePlayer.SuspendLayout();
            this.PanelBlueCrests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueTrapImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueMagImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueDEFImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueATKImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueMovImage)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedTrapImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedMagImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedDEFImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedATKImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedMovImage)).BeginInit();
            this.PanelDebug.SuspendLayout();
            this.PanelCardInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicCardArtworkBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelBoard
            // 
            this.PanelBoard.AutoScroll = true;
            this.PanelBoard.BackColor = System.Drawing.Color.Black;
            this.PanelBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBoard.Controls.Add(this.PanelActionMenu);
            this.PanelBoard.Location = new System.Drawing.Point(151, 0);
            this.PanelBoard.Name = "PanelBoard";
            this.PanelBoard.Size = new System.Drawing.Size(633, 561);
            this.PanelBoard.TabIndex = 0;
            // 
            // PanelActionMenu
            // 
            this.PanelActionMenu.BackColor = System.Drawing.Color.Black;
            this.PanelActionMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelActionMenu.Controls.Add(this.btnActionCancel);
            this.PanelActionMenu.Controls.Add(this.btnActionEffect);
            this.PanelActionMenu.Controls.Add(this.btnActionAttack);
            this.PanelActionMenu.Controls.Add(this.btnActionMove);
            this.PanelActionMenu.Location = new System.Drawing.Point(3, 5);
            this.PanelActionMenu.Name = "PanelActionMenu";
            this.PanelActionMenu.Size = new System.Drawing.Size(83, 99);
            this.PanelActionMenu.TabIndex = 5;
            this.PanelActionMenu.Visible = false;
            // 
            // btnActionCancel
            // 
            this.btnActionCancel.BackColor = System.Drawing.Color.Gray;
            this.btnActionCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActionCancel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnActionCancel.Location = new System.Drawing.Point(3, 73);
            this.btnActionCancel.Name = "btnActionCancel";
            this.btnActionCancel.Size = new System.Drawing.Size(75, 23);
            this.btnActionCancel.TabIndex = 3;
            this.btnActionCancel.Text = "Cancel";
            this.btnActionCancel.UseVisualStyleBackColor = false;
            this.btnActionCancel.Click += new System.EventHandler(this.btnActionCancel_Click);
            // 
            // btnActionEffect
            // 
            this.btnActionEffect.BackColor = System.Drawing.Color.Gray;
            this.btnActionEffect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActionEffect.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionEffect.Location = new System.Drawing.Point(3, 50);
            this.btnActionEffect.Name = "btnActionEffect";
            this.btnActionEffect.Size = new System.Drawing.Size(75, 23);
            this.btnActionEffect.TabIndex = 2;
            this.btnActionEffect.Text = "Effect";
            this.btnActionEffect.UseVisualStyleBackColor = false;
            // 
            // btnActionAttack
            // 
            this.btnActionAttack.BackColor = System.Drawing.Color.Gray;
            this.btnActionAttack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActionAttack.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionAttack.Location = new System.Drawing.Point(3, 27);
            this.btnActionAttack.Name = "btnActionAttack";
            this.btnActionAttack.Size = new System.Drawing.Size(75, 23);
            this.btnActionAttack.TabIndex = 1;
            this.btnActionAttack.Text = "Attack";
            this.btnActionAttack.UseVisualStyleBackColor = false;
            // 
            // btnActionMove
            // 
            this.btnActionMove.BackColor = System.Drawing.Color.Gray;
            this.btnActionMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActionMove.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionMove.Location = new System.Drawing.Point(3, 4);
            this.btnActionMove.Name = "btnActionMove";
            this.btnActionMove.Size = new System.Drawing.Size(75, 23);
            this.btnActionMove.TabIndex = 0;
            this.btnActionMove.Text = "Move";
            this.btnActionMove.UseVisualStyleBackColor = false;
            // 
            // PanelBluePlayer
            // 
            this.PanelBluePlayer.BackColor = System.Drawing.Color.MidnightBlue;
            this.PanelBluePlayer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelBluePlayer.Controls.Add(this.PanelBlueCrests);
            this.PanelBluePlayer.Controls.Add(this.lblBlueLP);
            this.PanelBluePlayer.Controls.Add(this.lblLPLabel);
            this.PanelBluePlayer.Controls.Add(this.label1);
            this.PanelBluePlayer.Location = new System.Drawing.Point(3, 2);
            this.PanelBluePlayer.Name = "PanelBluePlayer";
            this.PanelBluePlayer.Size = new System.Drawing.Size(144, 222);
            this.PanelBluePlayer.TabIndex = 1;
            // 
            // PanelBlueCrests
            // 
            this.PanelBlueCrests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBlueCrests.Controls.Add(this.label7);
            this.PanelBlueCrests.Controls.Add(this.PicBlueTrapImage);
            this.PanelBlueCrests.Controls.Add(this.label6);
            this.PanelBlueCrests.Controls.Add(this.PicBlueMagImage);
            this.PanelBlueCrests.Controls.Add(this.label5);
            this.PanelBlueCrests.Controls.Add(this.PicBlueDEFImage);
            this.PanelBlueCrests.Controls.Add(this.label4);
            this.PanelBlueCrests.Controls.Add(this.PicBlueATKImage);
            this.PanelBlueCrests.Controls.Add(this.lblBlueMovCount);
            this.PanelBlueCrests.Controls.Add(this.PicBlueMovImage);
            this.PanelBlueCrests.Location = new System.Drawing.Point(7, 50);
            this.PanelBlueCrests.Name = "PanelBlueCrests";
            this.PanelBlueCrests.Size = new System.Drawing.Size(86, 165);
            this.PanelBlueCrests.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(42, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "x 99";
            // 
            // PicBlueTrapImage
            // 
            this.PicBlueTrapImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBlueTrapImage.Location = new System.Drawing.Point(5, 128);
            this.PicBlueTrapImage.Name = "PicBlueTrapImage";
            this.PicBlueTrapImage.Size = new System.Drawing.Size(30, 30);
            this.PicBlueTrapImage.TabIndex = 8;
            this.PicBlueTrapImage.TabStop = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(42, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "x 99";
            // 
            // PicBlueMagImage
            // 
            this.PicBlueMagImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBlueMagImage.Location = new System.Drawing.Point(5, 97);
            this.PicBlueMagImage.Name = "PicBlueMagImage";
            this.PicBlueMagImage.Size = new System.Drawing.Size(30, 30);
            this.PicBlueMagImage.TabIndex = 6;
            this.PicBlueMagImage.TabStop = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(42, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "x 99";
            // 
            // PicBlueDEFImage
            // 
            this.PicBlueDEFImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBlueDEFImage.Location = new System.Drawing.Point(5, 66);
            this.PicBlueDEFImage.Name = "PicBlueDEFImage";
            this.PicBlueDEFImage.Size = new System.Drawing.Size(30, 30);
            this.PicBlueDEFImage.TabIndex = 4;
            this.PicBlueDEFImage.TabStop = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(42, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "x 99";
            // 
            // PicBlueATKImage
            // 
            this.PicBlueATKImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBlueATKImage.Location = new System.Drawing.Point(5, 35);
            this.PicBlueATKImage.Name = "PicBlueATKImage";
            this.PicBlueATKImage.Size = new System.Drawing.Size(30, 30);
            this.PicBlueATKImage.TabIndex = 2;
            this.PicBlueATKImage.TabStop = false;
            // 
            // lblBlueMovCount
            // 
            this.lblBlueMovCount.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlueMovCount.ForeColor = System.Drawing.Color.White;
            this.lblBlueMovCount.Location = new System.Drawing.Point(42, 9);
            this.lblBlueMovCount.Name = "lblBlueMovCount";
            this.lblBlueMovCount.Size = new System.Drawing.Size(53, 20);
            this.lblBlueMovCount.TabIndex = 1;
            this.lblBlueMovCount.Text = "x 99";
            // 
            // PicBlueMovImage
            // 
            this.PicBlueMovImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBlueMovImage.Location = new System.Drawing.Point(5, 4);
            this.PicBlueMovImage.Name = "PicBlueMovImage";
            this.PicBlueMovImage.Size = new System.Drawing.Size(30, 30);
            this.PicBlueMovImage.TabIndex = 0;
            this.PicBlueMovImage.TabStop = false;
            // 
            // lblBlueLP
            // 
            this.lblBlueLP.AutoSize = true;
            this.lblBlueLP.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlueLP.ForeColor = System.Drawing.Color.White;
            this.lblBlueLP.Location = new System.Drawing.Point(49, 24);
            this.lblBlueLP.Name = "lblBlueLP";
            this.lblBlueLP.Size = new System.Drawing.Size(54, 22);
            this.lblBlueLP.TabIndex = 2;
            this.lblBlueLP.Text = "8000";
            // 
            // lblLPLabel
            // 
            this.lblLPLabel.AutoSize = true;
            this.lblLPLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLPLabel.ForeColor = System.Drawing.Color.White;
            this.lblLPLabel.Location = new System.Drawing.Point(3, 24);
            this.lblLPLabel.Name = "lblLPLabel";
            this.lblLPLabel.Size = new System.Drawing.Size(40, 22);
            this.lblLPLabel.TabIndex = 1;
            this.lblLPLabel.Text = "LP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "BLUE";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkRed;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.lblRedLP);
            this.panel2.Controls.Add(this.lblLPlabel2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(3, 336);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(144, 222);
            this.panel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.PicRedTrapImage);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.PicRedMagImage);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.PicRedDEFImage);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.PicRedATKImage);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.PicRedMovImage);
            this.panel1.Location = new System.Drawing.Point(6, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(86, 165);
            this.panel1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(42, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "x 99";
            // 
            // PicRedTrapImage
            // 
            this.PicRedTrapImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicRedTrapImage.Location = new System.Drawing.Point(5, 128);
            this.PicRedTrapImage.Name = "PicRedTrapImage";
            this.PicRedTrapImage.Size = new System.Drawing.Size(30, 30);
            this.PicRedTrapImage.TabIndex = 8;
            this.PicRedTrapImage.TabStop = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(42, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "x 99";
            // 
            // PicRedMagImage
            // 
            this.PicRedMagImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicRedMagImage.Location = new System.Drawing.Point(5, 97);
            this.PicRedMagImage.Name = "PicRedMagImage";
            this.PicRedMagImage.Size = new System.Drawing.Size(30, 30);
            this.PicRedMagImage.TabIndex = 6;
            this.PicRedMagImage.TabStop = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(42, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 20);
            this.label9.TabIndex = 5;
            this.label9.Text = "x 99";
            // 
            // PicRedDEFImage
            // 
            this.PicRedDEFImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicRedDEFImage.Location = new System.Drawing.Point(5, 66);
            this.PicRedDEFImage.Name = "PicRedDEFImage";
            this.PicRedDEFImage.Size = new System.Drawing.Size(30, 30);
            this.PicRedDEFImage.TabIndex = 4;
            this.PicRedDEFImage.TabStop = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(42, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "x 99";
            // 
            // PicRedATKImage
            // 
            this.PicRedATKImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicRedATKImage.Location = new System.Drawing.Point(5, 35);
            this.PicRedATKImage.Name = "PicRedATKImage";
            this.PicRedATKImage.Size = new System.Drawing.Size(30, 30);
            this.PicRedATKImage.TabIndex = 2;
            this.PicRedATKImage.TabStop = false;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(42, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "x 99";
            // 
            // PicRedMovImage
            // 
            this.PicRedMovImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicRedMovImage.Location = new System.Drawing.Point(5, 4);
            this.PicRedMovImage.Name = "PicRedMovImage";
            this.PicRedMovImage.Size = new System.Drawing.Size(30, 30);
            this.PicRedMovImage.TabIndex = 0;
            this.PicRedMovImage.TabStop = false;
            // 
            // lblRedLP
            // 
            this.lblRedLP.AutoSize = true;
            this.lblRedLP.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedLP.ForeColor = System.Drawing.Color.White;
            this.lblRedLP.Location = new System.Drawing.Point(48, 24);
            this.lblRedLP.Name = "lblRedLP";
            this.lblRedLP.Size = new System.Drawing.Size(54, 22);
            this.lblRedLP.TabIndex = 4;
            this.lblRedLP.Text = "8000";
            // 
            // lblLPlabel2
            // 
            this.lblLPlabel2.AutoSize = true;
            this.lblLPlabel2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLPlabel2.ForeColor = System.Drawing.Color.White;
            this.lblLPlabel2.Location = new System.Drawing.Point(2, 24);
            this.lblLPlabel2.Name = "lblLPlabel2";
            this.lblLPlabel2.Size = new System.Drawing.Size(40, 22);
            this.lblLPlabel2.TabIndex = 3;
            this.lblLPlabel2.Text = "LP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "RED";
            // 
            // PanelDebug
            // 
            this.PanelDebug.BackColor = System.Drawing.Color.Black;
            this.PanelDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDebug.Controls.Add(this.lblMouseCords);
            this.PanelDebug.Controls.Add(this.lblDebugCardOwner);
            this.PanelDebug.Controls.Add(this.lblDebugCard);
            this.PanelDebug.Controls.Add(this.lblDebugIsOccupied);
            this.PanelDebug.Controls.Add(this.lblDebugOwner);
            this.PanelDebug.Controls.Add(this.lblDebugWestAdj);
            this.PanelDebug.Controls.Add(this.lblDebugEastAdj);
            this.PanelDebug.Controls.Add(this.lblDebugSouthAdj);
            this.PanelDebug.Controls.Add(this.lblDebugNorthAdj);
            this.PanelDebug.Controls.Add(this.lblDebugTileID);
            this.PanelDebug.Location = new System.Drawing.Point(790, 353);
            this.PanelDebug.Name = "PanelDebug";
            this.PanelDebug.Size = new System.Drawing.Size(139, 205);
            this.PanelDebug.TabIndex = 3;
            // 
            // lblMouseCords
            // 
            this.lblMouseCords.AutoSize = true;
            this.lblMouseCords.ForeColor = System.Drawing.Color.White;
            this.lblMouseCords.Location = new System.Drawing.Point(9, 167);
            this.lblMouseCords.Name = "lblMouseCords";
            this.lblMouseCords.Size = new System.Drawing.Size(32, 13);
            this.lblMouseCords.TabIndex = 9;
            this.lblMouseCords.Text = "Card:";
            // 
            // lblDebugCardOwner
            // 
            this.lblDebugCardOwner.AutoSize = true;
            this.lblDebugCardOwner.ForeColor = System.Drawing.Color.White;
            this.lblDebugCardOwner.Location = new System.Drawing.Point(7, 145);
            this.lblDebugCardOwner.Name = "lblDebugCardOwner";
            this.lblDebugCardOwner.Size = new System.Drawing.Size(32, 13);
            this.lblDebugCardOwner.TabIndex = 8;
            this.lblDebugCardOwner.Text = "Card:";
            // 
            // lblDebugCard
            // 
            this.lblDebugCard.AutoSize = true;
            this.lblDebugCard.ForeColor = System.Drawing.Color.White;
            this.lblDebugCard.Location = new System.Drawing.Point(8, 130);
            this.lblDebugCard.Name = "lblDebugCard";
            this.lblDebugCard.Size = new System.Drawing.Size(32, 13);
            this.lblDebugCard.TabIndex = 7;
            this.lblDebugCard.Text = "Card:";
            // 
            // lblDebugIsOccupied
            // 
            this.lblDebugIsOccupied.AutoSize = true;
            this.lblDebugIsOccupied.ForeColor = System.Drawing.Color.White;
            this.lblDebugIsOccupied.Location = new System.Drawing.Point(8, 117);
            this.lblDebugIsOccupied.Name = "lblDebugIsOccupied";
            this.lblDebugIsOccupied.Size = new System.Drawing.Size(56, 13);
            this.lblDebugIsOccupied.TabIndex = 6;
            this.lblDebugIsOccupied.Text = "Occupied:";
            // 
            // lblDebugOwner
            // 
            this.lblDebugOwner.AutoSize = true;
            this.lblDebugOwner.ForeColor = System.Drawing.Color.White;
            this.lblDebugOwner.Location = new System.Drawing.Point(9, 102);
            this.lblDebugOwner.Name = "lblDebugOwner";
            this.lblDebugOwner.Size = new System.Drawing.Size(41, 13);
            this.lblDebugOwner.TabIndex = 5;
            this.lblDebugOwner.Text = "Owner:";
            // 
            // lblDebugWestAdj
            // 
            this.lblDebugWestAdj.AutoSize = true;
            this.lblDebugWestAdj.ForeColor = System.Drawing.Color.White;
            this.lblDebugWestAdj.Location = new System.Drawing.Point(8, 80);
            this.lblDebugWestAdj.Name = "lblDebugWestAdj";
            this.lblDebugWestAdj.Size = new System.Drawing.Size(69, 13);
            this.lblDebugWestAdj.TabIndex = 4;
            this.lblDebugWestAdj.Text = "West Tile ID:";
            // 
            // lblDebugEastAdj
            // 
            this.lblDebugEastAdj.AutoSize = true;
            this.lblDebugEastAdj.ForeColor = System.Drawing.Color.White;
            this.lblDebugEastAdj.Location = new System.Drawing.Point(8, 63);
            this.lblDebugEastAdj.Name = "lblDebugEastAdj";
            this.lblDebugEastAdj.Size = new System.Drawing.Size(65, 13);
            this.lblDebugEastAdj.TabIndex = 3;
            this.lblDebugEastAdj.Text = "East Tile ID:";
            // 
            // lblDebugSouthAdj
            // 
            this.lblDebugSouthAdj.AutoSize = true;
            this.lblDebugSouthAdj.ForeColor = System.Drawing.Color.White;
            this.lblDebugSouthAdj.Location = new System.Drawing.Point(8, 46);
            this.lblDebugSouthAdj.Name = "lblDebugSouthAdj";
            this.lblDebugSouthAdj.Size = new System.Drawing.Size(72, 13);
            this.lblDebugSouthAdj.TabIndex = 2;
            this.lblDebugSouthAdj.Text = "South Tile ID:";
            // 
            // lblDebugNorthAdj
            // 
            this.lblDebugNorthAdj.AutoSize = true;
            this.lblDebugNorthAdj.ForeColor = System.Drawing.Color.White;
            this.lblDebugNorthAdj.Location = new System.Drawing.Point(8, 28);
            this.lblDebugNorthAdj.Name = "lblDebugNorthAdj";
            this.lblDebugNorthAdj.Size = new System.Drawing.Size(70, 13);
            this.lblDebugNorthAdj.TabIndex = 1;
            this.lblDebugNorthAdj.Text = "North Tile ID:";
            // 
            // lblDebugTileID
            // 
            this.lblDebugTileID.AutoSize = true;
            this.lblDebugTileID.ForeColor = System.Drawing.Color.White;
            this.lblDebugTileID.Location = new System.Drawing.Point(8, 7);
            this.lblDebugTileID.Name = "lblDebugTileID";
            this.lblDebugTileID.Size = new System.Drawing.Size(41, 13);
            this.lblDebugTileID.TabIndex = 0;
            this.lblDebugTileID.Text = "Tile ID:";
            // 
            // PanelCardInfo
            // 
            this.PanelCardInfo.BackColor = System.Drawing.Color.DarkRed;
            this.PanelCardInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelCardInfo.Controls.Add(this.lblCardText);
            this.PanelCardInfo.Controls.Add(this.lblStats);
            this.PanelCardInfo.Controls.Add(this.lblAttribute);
            this.PanelCardInfo.Controls.Add(this.lblCardType);
            this.PanelCardInfo.Controls.Add(this.lblCardLevel);
            this.PanelCardInfo.Controls.Add(this.lblCardName);
            this.PanelCardInfo.Controls.Add(this.PicCardArtworkBottom);
            this.PanelCardInfo.Location = new System.Drawing.Point(788, 3);
            this.PanelCardInfo.Name = "PanelCardInfo";
            this.PanelCardInfo.Size = new System.Drawing.Size(214, 237);
            this.PanelCardInfo.TabIndex = 4;
            // 
            // lblCardText
            // 
            this.lblCardText.BackColor = System.Drawing.Color.Black;
            this.lblCardText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardText.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardText.ForeColor = System.Drawing.Color.White;
            this.lblCardText.Location = new System.Drawing.Point(3, 88);
            this.lblCardText.Name = "lblCardText";
            this.lblCardText.Size = new System.Drawing.Size(205, 145);
            this.lblCardText.TabIndex = 12;
            this.lblCardText.Text = "Card Text";
            // 
            // lblStats
            // 
            this.lblStats.BackColor = System.Drawing.Color.Black;
            this.lblStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStats.ForeColor = System.Drawing.Color.White;
            this.lblStats.Location = new System.Drawing.Point(3, 72);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(205, 15);
            this.lblStats.TabIndex = 16;
            this.lblStats.Text = "Stats";
            // 
            // lblAttribute
            // 
            this.lblAttribute.BackColor = System.Drawing.Color.Black;
            this.lblAttribute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAttribute.ForeColor = System.Drawing.Color.White;
            this.lblAttribute.Location = new System.Drawing.Point(159, 37);
            this.lblAttribute.Name = "lblAttribute";
            this.lblAttribute.Size = new System.Drawing.Size(48, 15);
            this.lblAttribute.TabIndex = 15;
            this.lblAttribute.Text = "Attribute";
            // 
            // lblCardType
            // 
            this.lblCardType.BackColor = System.Drawing.Color.Black;
            this.lblCardType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardType.ForeColor = System.Drawing.Color.White;
            this.lblCardType.Location = new System.Drawing.Point(80, 53);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(127, 15);
            this.lblCardType.TabIndex = 4;
            this.lblCardType.Text = "Type";
            // 
            // lblCardLevel
            // 
            this.lblCardLevel.BackColor = System.Drawing.Color.Black;
            this.lblCardLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardLevel.ForeColor = System.Drawing.Color.White;
            this.lblCardLevel.Location = new System.Drawing.Point(80, 37);
            this.lblCardLevel.Name = "lblCardLevel";
            this.lblCardLevel.Size = new System.Drawing.Size(35, 15);
            this.lblCardLevel.TabIndex = 3;
            this.lblCardLevel.Text = "Level";
            // 
            // lblCardName
            // 
            this.lblCardName.BackColor = System.Drawing.Color.Black;
            this.lblCardName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCardName.ForeColor = System.Drawing.Color.White;
            this.lblCardName.Location = new System.Drawing.Point(80, 2);
            this.lblCardName.Name = "lblCardName";
            this.lblCardName.Size = new System.Drawing.Size(128, 34);
            this.lblCardName.TabIndex = 2;
            this.lblCardName.Text = "Card Name";
            // 
            // PicCardArtworkBottom
            // 
            this.PicCardArtworkBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicCardArtworkBottom.Location = new System.Drawing.Point(3, 2);
            this.PicCardArtworkBottom.Name = "PicCardArtworkBottom";
            this.PicCardArtworkBottom.Size = new System.Drawing.Size(68, 68);
            this.PicCardArtworkBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicCardArtworkBottom.TabIndex = 1;
            this.PicCardArtworkBottom.TabStop = false;
            // 
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(1006, 561);
            this.Controls.Add(this.PanelCardInfo);
            this.Controls.Add(this.PanelDebug);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PanelBluePlayer);
            this.Controls.Add(this.PanelBoard);
            this.Name = "BoardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDM - Board";
            this.PanelBoard.ResumeLayout(false);
            this.PanelActionMenu.ResumeLayout(false);
            this.PanelBluePlayer.ResumeLayout(false);
            this.PanelBluePlayer.PerformLayout();
            this.PanelBlueCrests.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueTrapImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueMagImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueDEFImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueATKImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlueMovImage)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicRedTrapImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedMagImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedDEFImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedATKImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRedMovImage)).EndInit();
            this.PanelDebug.ResumeLayout(false);
            this.PanelDebug.PerformLayout();
            this.PanelCardInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicCardArtworkBottom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelBoard;
        private System.Windows.Forms.Panel PanelBluePlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBlueLP;
        private System.Windows.Forms.Label lblLPLabel;
        private System.Windows.Forms.Label lblRedLP;
        private System.Windows.Forms.Label lblLPlabel2;
        private System.Windows.Forms.Panel PanelDebug;
        private System.Windows.Forms.Label lblDebugWestAdj;
        private System.Windows.Forms.Label lblDebugEastAdj;
        private System.Windows.Forms.Label lblDebugSouthAdj;
        private System.Windows.Forms.Label lblDebugNorthAdj;
        private System.Windows.Forms.Label lblDebugTileID;
        private System.Windows.Forms.Label lblDebugOwner;
        private System.Windows.Forms.Label lblDebugIsOccupied;
        private System.Windows.Forms.Label lblDebugCard;
        private System.Windows.Forms.Label lblDebugCardOwner;
        private System.Windows.Forms.Panel PanelCardInfo;
        private System.Windows.Forms.PictureBox PicCardArtworkBottom;
        private System.Windows.Forms.Label lblCardName;
        private System.Windows.Forms.Label lblCardLevel;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblAttribute;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblCardText;
        private System.Windows.Forms.Label lblMouseCords;
        private System.Windows.Forms.Panel PanelBlueCrests;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox PicBlueTrapImage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox PicBlueMagImage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox PicBlueDEFImage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox PicBlueATKImage;
        private System.Windows.Forms.Label lblBlueMovCount;
        private System.Windows.Forms.PictureBox PicBlueMovImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox PicRedTrapImage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox PicRedMagImage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox PicRedDEFImage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox PicRedATKImage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox PicRedMovImage;
        private System.Windows.Forms.Panel PanelActionMenu;
        private System.Windows.Forms.Button btnActionMove;
        private System.Windows.Forms.Button btnActionEffect;
        private System.Windows.Forms.Button btnActionAttack;
        private System.Windows.Forms.Button btnActionCancel;
    }
}