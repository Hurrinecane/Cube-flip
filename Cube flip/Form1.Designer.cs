
namespace Cube_flip
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.gameField = new System.Windows.Forms.Panel();
			this.buttonNewGame = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownStartPositionX = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownEndPositionX = new System.Windows.Forms.NumericUpDown();
			this.domainUpDownSideRedFace = new System.Windows.Forms.DomainUpDown();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonFindSolutionWidth = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.domainUpDownLevelSelection = new System.Windows.Forms.DomainUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.numericUpDownEndPositionY = new System.Windows.Forms.NumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDownStartPositionY = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.buttonSolutionDemoDepth = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.buttonFindSolutionDepth = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.buttonSolutionDemoWidth = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.numericUpDownTime = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.buttonShowmapExploredPassagesDepth = new System.Windows.Forms.Button();
			this.buttonShowmapExploredPassagesWidth = new System.Windows.Forms.Button();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.collectionStatistics = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.buttonShowmapExploredPassagesAlgorithm2 = new System.Windows.Forms.Button();
			this.buttonShowmapExploredPassagesAlgorithm1 = new System.Windows.Forms.Button();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.collectionStatistics2 = new System.Windows.Forms.Button();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.buttonSolutionDemoAlgorithm2 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.buttonAlgorithm2 = new System.Windows.Forms.Button();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.buttonSolutionDemoAlgorithm1 = new System.Windows.Forms.Button();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.buttonAlgorithm1 = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.numericUpDownTime2 = new System.Windows.Forms.NumericUpDown();
			this.label17 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartPositionX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndPositionX)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndPositionY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartPositionY)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).BeginInit();
			this.groupBox5.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime2)).BeginInit();
			this.SuspendLayout();
			// 
			// gameField
			// 
			this.gameField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gameField.ForeColor = System.Drawing.SystemColors.ControlText;
			this.gameField.Location = new System.Drawing.Point(12, 29);
			this.gameField.Name = "gameField";
			this.gameField.Size = new System.Drawing.Size(550, 550);
			this.gameField.TabIndex = 1;
			this.gameField.Paint += new System.Windows.Forms.PaintEventHandler(this.RedrawGameField);
			// 
			// buttonNewGame
			// 
			this.buttonNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonNewGame.Location = new System.Drawing.Point(6, 28);
			this.buttonNewGame.Name = "buttonNewGame";
			this.buttonNewGame.Size = new System.Drawing.Size(338, 32);
			this.buttonNewGame.TabIndex = 3;
			this.buttonNewGame.Text = "Новая игра";
			this.buttonNewGame.UseVisualStyleBackColor = true;
			this.buttonNewGame.Click += new System.EventHandler(this.ButtonNewGameClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(6, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(197, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "Стартовая позиция (";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(6, 102);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(187, 24);
			this.label2.TabIndex = 5;
			this.label2.Text = "Конечная позиция (";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(6, 135);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(186, 24);
			this.label3.TabIndex = 6;
			this.label3.Text = "Выигрышная грань:";
			// 
			// numericUpDownStartPositionX
			// 
			this.numericUpDownStartPositionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.numericUpDownStartPositionX.InterceptArrowKeys = false;
			this.numericUpDownStartPositionX.Location = new System.Drawing.Point(234, 66);
			this.numericUpDownStartPositionX.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numericUpDownStartPositionX.Name = "numericUpDownStartPositionX";
			this.numericUpDownStartPositionX.Size = new System.Drawing.Size(110, 29);
			this.numericUpDownStartPositionX.TabIndex = 7;
			this.numericUpDownStartPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDownStartPositionX.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
			// 
			// numericUpDownEndPositionX
			// 
			this.numericUpDownEndPositionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.numericUpDownEndPositionX.InterceptArrowKeys = false;
			this.numericUpDownEndPositionX.Location = new System.Drawing.Point(224, 100);
			this.numericUpDownEndPositionX.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numericUpDownEndPositionX.Name = "numericUpDownEndPositionX";
			this.numericUpDownEndPositionX.Size = new System.Drawing.Size(120, 29);
			this.numericUpDownEndPositionX.TabIndex = 8;
			this.numericUpDownEndPositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDownEndPositionX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// domainUpDownSideRedFace
			// 
			this.domainUpDownSideRedFace.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.domainUpDownSideRedFace.InterceptArrowKeys = false;
			this.domainUpDownSideRedFace.Items.Add("right");
			this.domainUpDownSideRedFace.Items.Add("left");
			this.domainUpDownSideRedFace.Items.Add("top");
			this.domainUpDownSideRedFace.Items.Add("bottom");
			this.domainUpDownSideRedFace.Items.Add("front");
			this.domainUpDownSideRedFace.Items.Add("back");
			this.domainUpDownSideRedFace.Location = new System.Drawing.Point(193, 133);
			this.domainUpDownSideRedFace.Name = "domainUpDownSideRedFace";
			this.domainUpDownSideRedFace.ReadOnly = true;
			this.domainUpDownSideRedFace.Size = new System.Drawing.Size(347, 29);
			this.domainUpDownSideRedFace.TabIndex = 9;
			this.domainUpDownSideRedFace.Text = "top";
			this.domainUpDownSideRedFace.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.domainUpDownSideRedFace.Wrap = true;
			this.domainUpDownSideRedFace.SelectedItemChanged += new System.EventHandler(this.DomainUpDownSideRedFace_SelectedItemChanged);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(6, 64);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(249, 192);
			this.textBox1.TabIndex = 10;
			// 
			// buttonFindSolutionWidth
			// 
			this.buttonFindSolutionWidth.Location = new System.Drawing.Point(6, 28);
			this.buttonFindSolutionWidth.Name = "buttonFindSolutionWidth";
			this.buttonFindSolutionWidth.Size = new System.Drawing.Size(249, 30);
			this.buttonFindSolutionWidth.TabIndex = 11;
			this.buttonFindSolutionWidth.Text = "Найти решение";
			this.buttonFindSolutionWidth.UseVisualStyleBackColor = true;
			this.buttonFindSolutionWidth.Click += new System.EventHandler(this.ButtonFindSolutionWidthClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.domainUpDownLevelSelection);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.numericUpDownEndPositionY);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.numericUpDownStartPositionY);
			this.groupBox1.Controls.Add(this.buttonNewGame);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.domainUpDownSideRedFace);
			this.groupBox1.Controls.Add(this.numericUpDownEndPositionX);
			this.groupBox1.Controls.Add(this.numericUpDownStartPositionX);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(572, 29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(546, 168);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Настройки игры";
			// 
			// domainUpDownLevelSelection
			// 
			this.domainUpDownLevelSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.domainUpDownLevelSelection.InterceptArrowKeys = false;
			this.domainUpDownLevelSelection.Items.Add("Level 6");
			this.domainUpDownLevelSelection.Items.Add("Level 5");
			this.domainUpDownLevelSelection.Items.Add("Level 4");
			this.domainUpDownLevelSelection.Items.Add("Level 3");
			this.domainUpDownLevelSelection.Items.Add("Level 2");
			this.domainUpDownLevelSelection.Items.Add("Level 1");
			this.domainUpDownLevelSelection.Location = new System.Drawing.Point(350, 30);
			this.domainUpDownLevelSelection.Name = "domainUpDownLevelSelection";
			this.domainUpDownLevelSelection.ReadOnly = true;
			this.domainUpDownLevelSelection.Size = new System.Drawing.Size(188, 29);
			this.domainUpDownLevelSelection.TabIndex = 20;
			this.domainUpDownLevelSelection.Text = "Level 1";
			this.domainUpDownLevelSelection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.domainUpDownLevelSelection.Wrap = true;
			this.domainUpDownLevelSelection.SelectedItemChanged += new System.EventHandler(this.DomainUpDownLevelSelectionSelectedItemChanged);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.Location = new System.Drawing.Point(522, 100);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(16, 24);
			this.label13.TabIndex = 19;
			this.label13.Text = ")";
			// 
			// numericUpDownEndPositionY
			// 
			this.numericUpDownEndPositionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.numericUpDownEndPositionY.InterceptArrowKeys = false;
			this.numericUpDownEndPositionY.Location = new System.Drawing.Point(404, 98);
			this.numericUpDownEndPositionY.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numericUpDownEndPositionY.Name = "numericUpDownEndPositionY";
			this.numericUpDownEndPositionY.Size = new System.Drawing.Size(112, 29);
			this.numericUpDownEndPositionY.TabIndex = 18;
			this.numericUpDownEndPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDownEndPositionY.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label12.Location = new System.Drawing.Point(371, 102);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(27, 24);
			this.label12.TabIndex = 17;
			this.label12.Text = "Y:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label11.Location = new System.Drawing.Point(350, 102);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(15, 24);
			this.label11.TabIndex = 16;
			this.label11.Text = ",";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.Location = new System.Drawing.Point(189, 102);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(29, 24);
			this.label10.TabIndex = 15;
			this.label10.Text = "X:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.Location = new System.Drawing.Point(522, 68);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(16, 24);
			this.label9.TabIndex = 14;
			this.label9.Text = ")";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.Location = new System.Drawing.Point(371, 68);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(27, 24);
			this.label8.TabIndex = 13;
			this.label8.Text = "Y:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.Location = new System.Drawing.Point(350, 71);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(15, 24);
			this.label7.TabIndex = 12;
			this.label7.Text = ",";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(199, 68);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 24);
			this.label6.TabIndex = 11;
			this.label6.Text = "X:";
			// 
			// numericUpDownStartPositionY
			// 
			this.numericUpDownStartPositionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.numericUpDownStartPositionY.InterceptArrowKeys = false;
			this.numericUpDownStartPositionY.Location = new System.Drawing.Point(404, 66);
			this.numericUpDownStartPositionY.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.numericUpDownStartPositionY.Name = "numericUpDownStartPositionY";
			this.numericUpDownStartPositionY.Size = new System.Drawing.Size(112, 29);
			this.numericUpDownStartPositionY.TabIndex = 10;
			this.numericUpDownStartPositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDownStartPositionY.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.groupBox4);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.numericUpDownTime);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.Location = new System.Drawing.Point(572, 203);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(546, 376);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Поиск решения";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.buttonSolutionDemoDepth);
			this.groupBox4.Controls.Add(this.textBox2);
			this.groupBox4.Controls.Add(this.buttonFindSolutionDepth);
			this.groupBox4.Location = new System.Drawing.Point(277, 28);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(261, 303);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "В глубину";
			// 
			// buttonSolutionDemoDepth
			// 
			this.buttonSolutionDemoDepth.Location = new System.Drawing.Point(6, 262);
			this.buttonSolutionDemoDepth.Name = "buttonSolutionDemoDepth";
			this.buttonSolutionDemoDepth.Size = new System.Drawing.Size(249, 35);
			this.buttonSolutionDemoDepth.TabIndex = 12;
			this.buttonSolutionDemoDepth.Text = "Демонстрация решения";
			this.buttonSolutionDemoDepth.UseVisualStyleBackColor = true;
			this.buttonSolutionDemoDepth.Click += new System.EventHandler(this.ButtonSolutionDemoDepthClick);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(6, 64);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox2.Size = new System.Drawing.Size(249, 192);
			this.textBox2.TabIndex = 10;
			// 
			// buttonFindSolutionDepth
			// 
			this.buttonFindSolutionDepth.Location = new System.Drawing.Point(6, 28);
			this.buttonFindSolutionDepth.Name = "buttonFindSolutionDepth";
			this.buttonFindSolutionDepth.Size = new System.Drawing.Size(249, 30);
			this.buttonFindSolutionDepth.TabIndex = 11;
			this.buttonFindSolutionDepth.Text = "Найти решение";
			this.buttonFindSolutionDepth.UseVisualStyleBackColor = true;
			this.buttonFindSolutionDepth.Click += new System.EventHandler(this.ButtonFindSolutionDepthClick);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.buttonSolutionDemoWidth);
			this.groupBox3.Controls.Add(this.textBox1);
			this.groupBox3.Controls.Add(this.buttonFindSolutionWidth);
			this.groupBox3.Location = new System.Drawing.Point(10, 28);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(261, 303);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "В ширину";
			// 
			// buttonSolutionDemoWidth
			// 
			this.buttonSolutionDemoWidth.Location = new System.Drawing.Point(6, 262);
			this.buttonSolutionDemoWidth.Name = "buttonSolutionDemoWidth";
			this.buttonSolutionDemoWidth.Size = new System.Drawing.Size(249, 35);
			this.buttonSolutionDemoWidth.TabIndex = 12;
			this.buttonSolutionDemoWidth.Text = "Демонстрация решения";
			this.buttonSolutionDemoWidth.UseVisualStyleBackColor = true;
			this.buttonSolutionDemoWidth.Click += new System.EventHandler(this.ButtonSolutionDemoWidthClick);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(494, 339);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 24);
			this.label5.TabIndex = 13;
			this.label5.Text = "мс.";
			// 
			// numericUpDownTime
			// 
			this.numericUpDownTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.numericUpDownTime.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numericUpDownTime.InterceptArrowKeys = false;
			this.numericUpDownTime.Location = new System.Drawing.Point(203, 337);
			this.numericUpDownTime.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numericUpDownTime.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numericUpDownTime.Name = "numericUpDownTime";
			this.numericUpDownTime.Size = new System.Drawing.Size(291, 29);
			this.numericUpDownTime.TabIndex = 10;
			this.numericUpDownTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDownTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(6, 339);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(198, 24);
			this.label4.TabIndex = 10;
			this.label4.Text = "Задержка анимации:";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.buttonShowmapExploredPassagesDepth);
			this.groupBox5.Controls.Add(this.buttonShowmapExploredPassagesWidth);
			this.groupBox5.Controls.Add(this.label15);
			this.groupBox5.Controls.Add(this.label14);
			this.groupBox5.Controls.Add(this.collectionStatistics);
			this.groupBox5.Controls.Add(this.textBox4);
			this.groupBox5.Controls.Add(this.textBox3);
			this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox5.Location = new System.Drawing.Point(12, 585);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(1106, 222);
			this.groupBox5.TabIndex = 0;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Статистика";
			// 
			// buttonShowmapExploredPassagesDepth
			// 
			this.buttonShowmapExploredPassagesDepth.Location = new System.Drawing.Point(553, 181);
			this.buttonShowmapExploredPassagesDepth.Name = "buttonShowmapExploredPassagesDepth";
			this.buttonShowmapExploredPassagesDepth.Size = new System.Drawing.Size(545, 30);
			this.buttonShowmapExploredPassagesDepth.TabIndex = 18;
			this.buttonShowmapExploredPassagesDepth.Text = "Показать карту исследованных ходов";
			this.buttonShowmapExploredPassagesDepth.UseVisualStyleBackColor = true;
			this.buttonShowmapExploredPassagesDepth.Click += new System.EventHandler(this.ButtonShowmapExploredPassagesDepthClick);
			// 
			// buttonShowmapExploredPassagesWidth
			// 
			this.buttonShowmapExploredPassagesWidth.Location = new System.Drawing.Point(6, 181);
			this.buttonShowmapExploredPassagesWidth.Name = "buttonShowmapExploredPassagesWidth";
			this.buttonShowmapExploredPassagesWidth.Size = new System.Drawing.Size(545, 30);
			this.buttonShowmapExploredPassagesWidth.TabIndex = 17;
			this.buttonShowmapExploredPassagesWidth.Text = "Показать карту исследованных ходов";
			this.buttonShowmapExploredPassagesWidth.UseVisualStyleBackColor = true;
			this.buttonShowmapExploredPassagesWidth.Click += new System.EventHandler(this.ButtonShowmapExploredPassagesWidth_Click);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label15.Location = new System.Drawing.Point(691, 35);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(239, 24);
			this.label15.TabIndex = 16;
			this.label15.Text = "Поиск решения в глубину";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label14.Location = new System.Drawing.Point(177, 38);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(236, 24);
			this.label14.TabIndex = 15;
			this.label14.Text = "Поиск решения в ширину";
			// 
			// collectionStatistics
			// 
			this.collectionStatistics.Location = new System.Drawing.Point(419, 28);
			this.collectionStatistics.Name = "collectionStatistics";
			this.collectionStatistics.Size = new System.Drawing.Size(266, 31);
			this.collectionStatistics.TabIndex = 12;
			this.collectionStatistics.Text = "Собрать статистику";
			this.collectionStatistics.UseVisualStyleBackColor = true;
			this.collectionStatistics.Click += new System.EventHandler(this.CollectionStatisticsClick);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(553, 65);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox4.Size = new System.Drawing.Size(545, 110);
			this.textBox4.TabIndex = 14;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(6, 65);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox3.Size = new System.Drawing.Size(545, 110);
			this.textBox3.TabIndex = 13;
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.buttonShowmapExploredPassagesAlgorithm2);
			this.groupBox9.Controls.Add(this.buttonShowmapExploredPassagesAlgorithm1);
			this.groupBox9.Controls.Add(this.label18);
			this.groupBox9.Controls.Add(this.label19);
			this.groupBox9.Controls.Add(this.collectionStatistics2);
			this.groupBox9.Controls.Add(this.textBox7);
			this.groupBox9.Controls.Add(this.textBox8);
			this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox9.Location = new System.Drawing.Point(12, 585);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(1106, 222);
			this.groupBox9.TabIndex = 19;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Статистика";
			this.groupBox9.Visible = false;
			// 
			// buttonShowmapExploredPassagesAlgorithm2
			// 
			this.buttonShowmapExploredPassagesAlgorithm2.Location = new System.Drawing.Point(553, 181);
			this.buttonShowmapExploredPassagesAlgorithm2.Name = "buttonShowmapExploredPassagesAlgorithm2";
			this.buttonShowmapExploredPassagesAlgorithm2.Size = new System.Drawing.Size(545, 30);
			this.buttonShowmapExploredPassagesAlgorithm2.TabIndex = 18;
			this.buttonShowmapExploredPassagesAlgorithm2.Text = "Показать карту исследованных ходов";
			this.buttonShowmapExploredPassagesAlgorithm2.UseVisualStyleBackColor = true;
			this.buttonShowmapExploredPassagesAlgorithm2.Click += new System.EventHandler(this.ButtonShowmapExploredPassagesAlgorithm2Click);
			// 
			// buttonShowmapExploredPassagesAlgorithm1
			// 
			this.buttonShowmapExploredPassagesAlgorithm1.Location = new System.Drawing.Point(6, 181);
			this.buttonShowmapExploredPassagesAlgorithm1.Name = "buttonShowmapExploredPassagesAlgorithm1";
			this.buttonShowmapExploredPassagesAlgorithm1.Size = new System.Drawing.Size(545, 30);
			this.buttonShowmapExploredPassagesAlgorithm1.TabIndex = 17;
			this.buttonShowmapExploredPassagesAlgorithm1.Text = "Показать карту исследованных ходов";
			this.buttonShowmapExploredPassagesAlgorithm1.UseVisualStyleBackColor = true;
			this.buttonShowmapExploredPassagesAlgorithm1.Click += new System.EventHandler(this.ButtonShowmapExploredPassagesAlgorithm1Click);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label18.Location = new System.Drawing.Point(691, 35);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(114, 24);
			this.label18.TabIndex = 16;
			this.label18.Text = "Алгоритм-2";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label19.Location = new System.Drawing.Point(299, 38);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(114, 24);
			this.label19.TabIndex = 15;
			this.label19.Text = "Алгоритм-1";
			// 
			// collectionStatistics2
			// 
			this.collectionStatistics2.Location = new System.Drawing.Point(419, 28);
			this.collectionStatistics2.Name = "collectionStatistics2";
			this.collectionStatistics2.Size = new System.Drawing.Size(266, 31);
			this.collectionStatistics2.TabIndex = 12;
			this.collectionStatistics2.Text = "Собрать статистику";
			this.collectionStatistics2.UseVisualStyleBackColor = true;
			this.collectionStatistics2.Click += new System.EventHandler(this.CollectionStatistics2Click);
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(553, 65);
			this.textBox7.Multiline = true;
			this.textBox7.Name = "textBox7";
			this.textBox7.ReadOnly = true;
			this.textBox7.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox7.Size = new System.Drawing.Size(545, 110);
			this.textBox7.TabIndex = 14;
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(6, 65);
			this.textBox8.Multiline = true;
			this.textBox8.Name = "textBox8";
			this.textBox8.ReadOnly = true;
			this.textBox8.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox8.Size = new System.Drawing.Size(545, 110);
			this.textBox8.TabIndex = 13;
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(1122, 24);
			this.menuStrip.TabIndex = 14;
			this.menuStrip.Text = "Меню";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(53, 20);
			this.toolStripMenuItem1.Text = "Меню";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Checked = true;
			this.toolStripMenuItem2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(276, 22);
			this.toolStripMenuItem2.Text = "Неинформированный метод поиска";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.MenuItemUninformativeSearchClick);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(276, 22);
			this.toolStripMenuItem3.Text = "Информированный метод поиска";
			this.toolStripMenuItem3.Click += new System.EventHandler(this.MenuItemInformativeSearchClick);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.groupBox7);
			this.groupBox6.Controls.Add(this.groupBox8);
			this.groupBox6.Controls.Add(this.label16);
			this.groupBox6.Controls.Add(this.numericUpDownTime2);
			this.groupBox6.Controls.Add(this.label17);
			this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox6.Location = new System.Drawing.Point(572, 203);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(546, 376);
			this.groupBox6.TabIndex = 14;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Поиск решения";
			this.groupBox6.Visible = false;
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.buttonSolutionDemoAlgorithm2);
			this.groupBox7.Controls.Add(this.textBox5);
			this.groupBox7.Controls.Add(this.buttonAlgorithm2);
			this.groupBox7.Location = new System.Drawing.Point(277, 28);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(261, 303);
			this.groupBox7.TabIndex = 13;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Алгоритм-2";
			// 
			// buttonSolutionDemoAlgorithm2
			// 
			this.buttonSolutionDemoAlgorithm2.Location = new System.Drawing.Point(6, 262);
			this.buttonSolutionDemoAlgorithm2.Name = "buttonSolutionDemoAlgorithm2";
			this.buttonSolutionDemoAlgorithm2.Size = new System.Drawing.Size(249, 35);
			this.buttonSolutionDemoAlgorithm2.TabIndex = 12;
			this.buttonSolutionDemoAlgorithm2.Text = "Демонстрация решения";
			this.buttonSolutionDemoAlgorithm2.UseVisualStyleBackColor = true;
			this.buttonSolutionDemoAlgorithm2.Click += new System.EventHandler(this.ButtonSolutionDemoAlgorithm2Click);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(6, 64);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox5.Size = new System.Drawing.Size(249, 192);
			this.textBox5.TabIndex = 10;
			// 
			// buttonAlgorithm2
			// 
			this.buttonAlgorithm2.Location = new System.Drawing.Point(6, 28);
			this.buttonAlgorithm2.Name = "buttonAlgorithm2";
			this.buttonAlgorithm2.Size = new System.Drawing.Size(249, 30);
			this.buttonAlgorithm2.TabIndex = 11;
			this.buttonAlgorithm2.Text = "Найти решение";
			this.buttonAlgorithm2.UseVisualStyleBackColor = true;
			this.buttonAlgorithm2.Click += new System.EventHandler(this.ButtonAlgorithm2Click);
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.buttonSolutionDemoAlgorithm1);
			this.groupBox8.Controls.Add(this.textBox6);
			this.groupBox8.Controls.Add(this.buttonAlgorithm1);
			this.groupBox8.Location = new System.Drawing.Point(10, 28);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(261, 303);
			this.groupBox8.TabIndex = 3;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Алгоритм-1";
			// 
			// buttonSolutionDemoAlgorithm1
			// 
			this.buttonSolutionDemoAlgorithm1.Location = new System.Drawing.Point(6, 262);
			this.buttonSolutionDemoAlgorithm1.Name = "buttonSolutionDemoAlgorithm1";
			this.buttonSolutionDemoAlgorithm1.Size = new System.Drawing.Size(249, 35);
			this.buttonSolutionDemoAlgorithm1.TabIndex = 12;
			this.buttonSolutionDemoAlgorithm1.Text = "Демонстрация решения";
			this.buttonSolutionDemoAlgorithm1.UseVisualStyleBackColor = true;
			this.buttonSolutionDemoAlgorithm1.Click += new System.EventHandler(this.ButtonSolutionDemoAlgorithm1Click);
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(6, 64);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox6.Size = new System.Drawing.Size(249, 192);
			this.textBox6.TabIndex = 10;
			// 
			// buttonAlgorithm1
			// 
			this.buttonAlgorithm1.Location = new System.Drawing.Point(6, 28);
			this.buttonAlgorithm1.Name = "buttonAlgorithm1";
			this.buttonAlgorithm1.Size = new System.Drawing.Size(249, 30);
			this.buttonAlgorithm1.TabIndex = 11;
			this.buttonAlgorithm1.Text = "Найти решение";
			this.buttonAlgorithm1.UseVisualStyleBackColor = true;
			this.buttonAlgorithm1.Click += new System.EventHandler(this.ButtonAlgorithm1Click);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label16.Location = new System.Drawing.Point(494, 339);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(38, 24);
			this.label16.TabIndex = 13;
			this.label16.Text = "мс.";
			// 
			// numericUpDownTime2
			// 
			this.numericUpDownTime2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.numericUpDownTime2.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numericUpDownTime2.InterceptArrowKeys = false;
			this.numericUpDownTime2.Location = new System.Drawing.Point(203, 337);
			this.numericUpDownTime2.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numericUpDownTime2.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numericUpDownTime2.Name = "numericUpDownTime2";
			this.numericUpDownTime2.Size = new System.Drawing.Size(291, 29);
			this.numericUpDownTime2.TabIndex = 10;
			this.numericUpDownTime2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDownTime2.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label17.Location = new System.Drawing.Point(6, 339);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(198, 24);
			this.label17.TabIndex = 10;
			this.label17.Text = "Задержка анимации:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(1122, 811);
			this.Controls.Add(this.groupBox9);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gameField);
			this.Controls.Add(this.menuStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "Form1";
			this.Text = "Cube flip";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartPositionX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndPositionX)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndPositionY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartPositionY)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel gameField;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownStartPositionX;
        private System.Windows.Forms.NumericUpDown numericUpDownEndPositionX;
        private System.Windows.Forms.DomainUpDown domainUpDownSideRedFace;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonFindSolutionWidth;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSolutionDemoWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonSolutionDemoDepth;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonFindSolutionDepth;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownStartPositionY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDownEndPositionY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button collectionStatistics;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonShowmapExploredPassagesWidth;
        private System.Windows.Forms.Button buttonShowmapExploredPassagesDepth;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonSolutionDemoAlgorithm2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button buttonAlgorithm2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button buttonSolutionDemoAlgorithm1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button buttonAlgorithm1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numericUpDownTime2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button buttonShowmapExploredPassagesAlgorithm2;
        private System.Windows.Forms.Button buttonShowmapExploredPassagesAlgorithm1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button collectionStatistics2;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.DomainUpDown domainUpDownLevelSelection;
    }
}

