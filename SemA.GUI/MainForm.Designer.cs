namespace SemA.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Dispose resources.
        /// </summary>
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
        /// Designer initialization.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxData = new GroupBox();
            btnLoadData = new Button();
            btnSaveData = new Button();
            txtTownSearch = new TextBox();
            btnFindTown = new Button();
            groupBoxTowns = new GroupBox();
            btnAddTown = new Button();
            dataGridTownList = new DataGridView();
            ColumnTownName = new DataGridViewTextBoxColumn();
            ColumnTownX = new DataGridViewTextBoxColumn();
            ColumnTownY = new DataGridViewTextBoxColumn();
            groupBoxRoads = new GroupBox();
            btnAddRoad = new Button();
            btnDeleteRoad = new Button();
            labelRoadFrom = new Label();
            comboBoxRoadFrom = new ComboBox();
            labelRoadTo = new Label();
            comboBoxRoadTo = new ComboBox();
            btnFindRoad = new Button();
            dataGridRoadList = new DataGridView();
            ColumnFrom = new DataGridViewTextBoxColumn();
            ColumnTo = new DataGridViewTextBoxColumn();
            ColumnTime = new DataGridViewTextBoxColumn();
            ColumnIsProblematic = new DataGridViewCheckBoxColumn();
            groupBoxPaths = new GroupBox();
            labelFindRoute = new Label();
            comboBoxStart = new ComboBox();
            comboBoxGoal = new ComboBox();
            btnFindPaths = new Button();
            dataGridPathSummary = new DataGridView();
            ColumnPathType = new DataGridViewTextBoxColumn();
            ColumnPathTime = new DataGridViewTextBoxColumn();
            ColumnPathEdgeCount = new DataGridViewTextBoxColumn();
            richTextBoxDetailedResult = new RichTextBox();
            groupBoxVisualization = new GroupBox();
            panelGraphCanvas = new GraphCanvasPanel();
            checkBoxShowRoadTimes = new CheckBox();
            dataGridSuccessorVector = new DataGridView();
            groupBoxSuccessorVector = new GroupBox();
            btnOpenLargeGraph = new Button();
            groupBoxData.SuspendLayout();
            groupBoxTowns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridTownList).BeginInit();
            groupBoxRoads.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridRoadList).BeginInit();
            groupBoxPaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridPathSummary).BeginInit();
            groupBoxVisualization.SuspendLayout();
            panelGraphCanvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridSuccessorVector).BeginInit();
            groupBoxSuccessorVector.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxData
            // 
            groupBoxData.Controls.Add(btnLoadData);
            groupBoxData.Controls.Add(btnSaveData);
            groupBoxData.Location = new Point(20, 20);
            groupBoxData.Name = "groupBoxData";
            groupBoxData.Size = new Size(280, 120);
            groupBoxData.TabIndex = 0;
            groupBoxData.TabStop = false;
            groupBoxData.Text = "Data";
            // 
            // btnLoadData
            // 
            btnLoadData.Location = new Point(30, 30);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(210, 32);
            btnLoadData.TabIndex = 0;
            btnLoadData.Text = "Načíst data";
            btnLoadData.UseVisualStyleBackColor = true;
            // 
            // btnSaveData
            // 
            btnSaveData.Location = new Point(30, 72);
            btnSaveData.Name = "btnSaveData";
            btnSaveData.Size = new Size(210, 32);
            btnSaveData.TabIndex = 1;
            btnSaveData.Text = "Uložit data";
            btnSaveData.UseVisualStyleBackColor = true;
            // 
            // txtTownSearch
            // 
            txtTownSearch.Location = new Point(20, 90);
            txtTownSearch.Name = "txtTownSearch";
            txtTownSearch.Size = new Size(145, 23);
            txtTownSearch.TabIndex = 1;
            // 
            // btnFindTown
            // 
            btnFindTown.Location = new Point(175, 89);
            btnFindTown.Name = "btnFindTown";
            btnFindTown.Size = new Size(65, 25);
            btnFindTown.TabIndex = 2;
            btnFindTown.Text = "Najít";
            btnFindTown.UseVisualStyleBackColor = true;
            // 
            // groupBoxTowns
            // 
            groupBoxTowns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxTowns.Controls.Add(btnAddTown);
            groupBoxTowns.Controls.Add(txtTownSearch);
            groupBoxTowns.Controls.Add(btnFindTown);
            groupBoxTowns.Controls.Add(dataGridTownList);
            groupBoxTowns.Location = new Point(20, 155);
            groupBoxTowns.Name = "groupBoxTowns";
            groupBoxTowns.Size = new Size(280, 740);
            groupBoxTowns.TabIndex = 1;
            groupBoxTowns.TabStop = false;
            groupBoxTowns.Text = "Města";
            // 
            // btnAddTown
            // 
            btnAddTown.Location = new Point(30, 35);
            btnAddTown.Name = "btnAddTown";
            btnAddTown.Size = new Size(210, 32);
            btnAddTown.TabIndex = 0;
            btnAddTown.Text = "Přidat město";
            btnAddTown.UseVisualStyleBackColor = true;
            // 
            // dataGridTownList
            // 
            dataGridTownList.AllowUserToAddRows = false;
            dataGridTownList.AllowUserToDeleteRows = false;
            dataGridTownList.AllowUserToResizeRows = false;
            dataGridTownList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridTownList.BackgroundColor = Color.White;
            dataGridTownList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridTownList.Columns.AddRange(new DataGridViewColumn[] { ColumnTownName, ColumnTownX, ColumnTownY });
            dataGridTownList.Location = new Point(20, 130);
            dataGridTownList.MultiSelect = false;
            dataGridTownList.Name = "dataGridTownList";
            dataGridTownList.ReadOnly = true;
            dataGridTownList.RowHeadersVisible = false;
            dataGridTownList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridTownList.Size = new Size(240, 580);
            dataGridTownList.TabIndex = 3;
            // 
            // ColumnTownName
            // 
            ColumnTownName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnTownName.HeaderText = "Město";
            ColumnTownName.MinimumWidth = 120;
            ColumnTownName.Name = "ColumnTownName";
            ColumnTownName.ReadOnly = true;
            // 
            // ColumnTownX
            // 
            ColumnTownX.HeaderText = "X";
            ColumnTownX.Name = "ColumnTownX";
            ColumnTownX.ReadOnly = true;
            ColumnTownX.Width = 55;
            // 
            // ColumnTownY
            // 
            ColumnTownY.HeaderText = "Y";
            ColumnTownY.Name = "ColumnTownY";
            ColumnTownY.ReadOnly = true;
            ColumnTownY.Width = 55;
            // 
            // groupBoxRoads
            // 
            groupBoxRoads.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxRoads.Controls.Add(btnAddRoad);
            groupBoxRoads.Controls.Add(btnDeleteRoad);
            groupBoxRoads.Controls.Add(labelRoadFrom);
            groupBoxRoads.Controls.Add(comboBoxRoadFrom);
            groupBoxRoads.Controls.Add(labelRoadTo);
            groupBoxRoads.Controls.Add(comboBoxRoadTo);
            groupBoxRoads.Controls.Add(btnFindRoad);
            groupBoxRoads.Controls.Add(dataGridRoadList);
            groupBoxRoads.Location = new Point(320, 20);
            groupBoxRoads.Name = "groupBoxRoads";
            groupBoxRoads.Size = new Size(749, 360);
            groupBoxRoads.TabIndex = 2;
            groupBoxRoads.TabStop = false;
            groupBoxRoads.Text = "Silnice";
            // 
            // btnAddRoad
            // 
            btnAddRoad.Location = new Point(25, 30);
            btnAddRoad.Name = "btnAddRoad";
            btnAddRoad.Size = new Size(94, 32);
            btnAddRoad.TabIndex = 0;
            btnAddRoad.Text = "Přidat silnici";
            btnAddRoad.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRoad
            // 
            btnDeleteRoad.Location = new Point(137, 32);
            btnDeleteRoad.Name = "btnDeleteRoad";
            btnDeleteRoad.Size = new Size(104, 32);
            btnDeleteRoad.TabIndex = 1;
            btnDeleteRoad.Text = "Smazat silnici";
            btnDeleteRoad.UseVisualStyleBackColor = true;
            // 
            // labelRoadFrom
            // 
            labelRoadFrom.AutoSize = true;
            labelRoadFrom.Location = new Point(247, 40);
            labelRoadFrom.Name = "labelRoadFrom";
            labelRoadFrom.Size = new Size(17, 15);
            labelRoadFrom.TabIndex = 2;
            labelRoadFrom.Text = "Z:";
            labelRoadFrom.Click += labelRoadFrom_Click;
            // 
            // comboBoxRoadFrom
            // 
            comboBoxRoadFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRoadFrom.FormattingEnabled = true;
            comboBoxRoadFrom.Location = new Point(277, 37);
            comboBoxRoadFrom.Name = "comboBoxRoadFrom";
            comboBoxRoadFrom.Size = new Size(130, 23);
            comboBoxRoadFrom.TabIndex = 3;
            // 
            // labelRoadTo
            // 
            labelRoadTo.AutoSize = true;
            labelRoadTo.Location = new Point(422, 40);
            labelRoadTo.Name = "labelRoadTo";
            labelRoadTo.Size = new Size(25, 15);
            labelRoadTo.TabIndex = 4;
            labelRoadTo.Text = "Do:";
            // 
            // comboBoxRoadTo
            // 
            comboBoxRoadTo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRoadTo.FormattingEnabled = true;
            comboBoxRoadTo.Location = new Point(452, 37);
            comboBoxRoadTo.Name = "comboBoxRoadTo";
            comboBoxRoadTo.Size = new Size(130, 23);
            comboBoxRoadTo.TabIndex = 5;
            // 
            // btnFindRoad
            // 
            btnFindRoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFindRoad.Location = new Point(609, 34);
            btnFindRoad.Name = "btnFindRoad";
            btnFindRoad.Size = new Size(90, 28);
            btnFindRoad.TabIndex = 6;
            btnFindRoad.Text = "Najít";
            btnFindRoad.UseVisualStyleBackColor = true;
            // 
            // dataGridRoadList
            // 
            dataGridRoadList.AllowUserToAddRows = false;
            dataGridRoadList.AllowUserToDeleteRows = false;
            dataGridRoadList.AllowUserToResizeRows = false;
            dataGridRoadList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridRoadList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridRoadList.BackgroundColor = Color.White;
            dataGridRoadList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridRoadList.Columns.AddRange(new DataGridViewColumn[] { ColumnFrom, ColumnTo, ColumnTime, ColumnIsProblematic });
            dataGridRoadList.Location = new Point(25, 80);
            dataGridRoadList.Name = "dataGridRoadList";
            dataGridRoadList.RowHeadersVisible = false;
            dataGridRoadList.Size = new Size(674, 255);
            dataGridRoadList.TabIndex = 7;
            // 
            // ColumnFrom
            // 
            ColumnFrom.HeaderText = "Z";
            ColumnFrom.Name = "ColumnFrom";
            // 
            // ColumnTo
            // 
            ColumnTo.HeaderText = "Do";
            ColumnTo.Name = "ColumnTo";
            // 
            // ColumnTime
            // 
            ColumnTime.HeaderText = "Čas";
            ColumnTime.Name = "ColumnTime";
            // 
            // ColumnIsProblematic
            // 
            ColumnIsProblematic.HeaderText = "Problematická";
            ColumnIsProblematic.Name = "ColumnIsProblematic";
            // 
            // groupBoxPaths
            // 
            groupBoxPaths.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBoxPaths.Controls.Add(labelFindRoute);
            groupBoxPaths.Controls.Add(comboBoxStart);
            groupBoxPaths.Controls.Add(comboBoxGoal);
            groupBoxPaths.Controls.Add(btnFindPaths);
            groupBoxPaths.Controls.Add(dataGridPathSummary);
            groupBoxPaths.Controls.Add(richTextBoxDetailedResult);
            groupBoxPaths.Location = new Point(1140, 20);
            groupBoxPaths.Name = "groupBoxPaths";
            groupBoxPaths.Size = new Size(520, 360);
            groupBoxPaths.TabIndex = 3;
            groupBoxPaths.TabStop = false;
            groupBoxPaths.Text = "Trasy";
            // 
            // labelFindRoute
            // 
            labelFindRoute.AutoSize = true;
            labelFindRoute.Location = new Point(20, 38);
            labelFindRoute.Name = "labelFindRoute";
            labelFindRoute.Size = new Size(61, 15);
            labelFindRoute.TabIndex = 0;
            labelFindRoute.Text = "Najít trasu";
            // 
            // comboBoxStart
            // 
            comboBoxStart.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStart.FormattingEnabled = true;
            comboBoxStart.Location = new Point(90, 35);
            comboBoxStart.Name = "comboBoxStart";
            comboBoxStart.Size = new Size(110, 23);
            comboBoxStart.TabIndex = 1;
            // 
            // comboBoxGoal
            // 
            comboBoxGoal.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGoal.FormattingEnabled = true;
            comboBoxGoal.Location = new Point(210, 35);
            comboBoxGoal.Name = "comboBoxGoal";
            comboBoxGoal.Size = new Size(110, 23);
            comboBoxGoal.TabIndex = 2;
            // 
            // btnFindPaths
            // 
            btnFindPaths.Location = new Point(335, 34);
            btnFindPaths.Name = "btnFindPaths";
            btnFindPaths.Size = new Size(120, 28);
            btnFindPaths.TabIndex = 3;
            btnFindPaths.Text = "Vyhledat";
            btnFindPaths.UseVisualStyleBackColor = true;
            // 
            // dataGridPathSummary
            // 
            dataGridPathSummary.AllowUserToAddRows = false;
            dataGridPathSummary.AllowUserToDeleteRows = false;
            dataGridPathSummary.AllowUserToResizeRows = false;
            dataGridPathSummary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridPathSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridPathSummary.BackgroundColor = Color.White;
            dataGridPathSummary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridPathSummary.Columns.AddRange(new DataGridViewColumn[] { ColumnPathType, ColumnPathTime, ColumnPathEdgeCount });
            dataGridPathSummary.Location = new Point(20, 85);
            dataGridPathSummary.MultiSelect = false;
            dataGridPathSummary.Name = "dataGridPathSummary";
            dataGridPathSummary.ReadOnly = true;
            dataGridPathSummary.RowHeadersVisible = false;
            dataGridPathSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridPathSummary.Size = new Size(475, 90);
            dataGridPathSummary.TabIndex = 4;
            // 
            // ColumnPathType
            // 
            ColumnPathType.HeaderText = "Trasa";
            ColumnPathType.Name = "ColumnPathType";
            ColumnPathType.ReadOnly = true;
            // 
            // ColumnPathTime
            // 
            ColumnPathTime.HeaderText = "Čas";
            ColumnPathTime.Name = "ColumnPathTime";
            ColumnPathTime.ReadOnly = true;
            // 
            // ColumnPathEdgeCount
            // 
            ColumnPathEdgeCount.HeaderText = "Hrany";
            ColumnPathEdgeCount.Name = "ColumnPathEdgeCount";
            ColumnPathEdgeCount.ReadOnly = true;
            // 
            // richTextBoxDetailedResult
            // 
            richTextBoxDetailedResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxDetailedResult.BackColor = Color.White;
            richTextBoxDetailedResult.Location = new Point(20, 190);
            richTextBoxDetailedResult.Name = "richTextBoxDetailedResult";
            richTextBoxDetailedResult.ReadOnly = true;
            richTextBoxDetailedResult.Size = new Size(475, 145);
            richTextBoxDetailedResult.TabIndex = 5;
            richTextBoxDetailedResult.Text = "";
            // 
            // groupBoxVisualization
            // 
            groupBoxVisualization.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxVisualization.Controls.Add(panelGraphCanvas);
            groupBoxVisualization.Location = new Point(320, 535);
            groupBoxVisualization.Name = "groupBoxVisualization";
            groupBoxVisualization.Padding = new Padding(15, 28, 15, 15);
            groupBoxVisualization.Size = new Size(1340, 360);
            groupBoxVisualization.TabIndex = 4;
            groupBoxVisualization.TabStop = false;
            groupBoxVisualization.Text = "Vizualizace grafu";
            // 
            // panelGraphCanvas
            // 
            panelGraphCanvas.BackColor = Color.WhiteSmoke;
            panelGraphCanvas.BorderStyle = BorderStyle.None;
            panelGraphCanvas.Dock = DockStyle.Fill;
            panelGraphCanvas.Location = new Point(15, 28);
            panelGraphCanvas.Name = "panelGraphCanvas";
            panelGraphCanvas.Size = new Size(1310, 317);
            panelGraphCanvas.TabIndex = 0;
            // 
            // checkBoxShowRoadTimes
            // 
            checkBoxShowRoadTimes.AutoSize = true;
            checkBoxShowRoadTimes.Location = new Point(1155, 14);
            checkBoxShowRoadTimes.Name = "checkBoxShowRoadTimes";
            checkBoxShowRoadTimes.Size = new Size(144, 19);
            checkBoxShowRoadTimes.TabIndex = 0;
            checkBoxShowRoadTimes.Text = "Zobrazit časy průjezdu";
            checkBoxShowRoadTimes.UseVisualStyleBackColor = true;
            // 
            // dataGridSuccessorVector
            // 
            dataGridSuccessorVector.AllowUserToAddRows = false;
            dataGridSuccessorVector.AllowUserToDeleteRows = false;
            dataGridSuccessorVector.AllowUserToResizeColumns = false;
            dataGridSuccessorVector.AllowUserToResizeRows = false;
            dataGridSuccessorVector.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridSuccessorVector.BackgroundColor = Color.White;
            dataGridSuccessorVector.BorderStyle = BorderStyle.FixedSingle;
            dataGridSuccessorVector.Dock = DockStyle.Fill;
            dataGridSuccessorVector.Location = new Point(15, 25);
            dataGridSuccessorVector.MultiSelect = false;
            dataGridSuccessorVector.Name = "dataGridSuccessorVector";
            dataGridSuccessorVector.ReadOnly = true;
            dataGridSuccessorVector.RowHeadersVisible = false;
            dataGridSuccessorVector.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridSuccessorVector.ScrollBars = ScrollBars.Horizontal;
            dataGridSuccessorVector.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridSuccessorVector.Size = new Size(1310, 80);
            dataGridSuccessorVector.TabIndex = 6;
            // 
            // groupBoxSuccessorVector
            // 
            groupBoxSuccessorVector.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxSuccessorVector.Controls.Add(dataGridSuccessorVector);
            groupBoxSuccessorVector.Location = new Point(320, 400);
            groupBoxSuccessorVector.Name = "groupBoxSuccessorVector";
            groupBoxSuccessorVector.Padding = new Padding(15, 25, 15, 15);
            groupBoxSuccessorVector.Size = new Size(1340, 150);
            groupBoxSuccessorVector.TabIndex = 4;
            groupBoxSuccessorVector.TabStop = false;
            groupBoxSuccessorVector.Text = "Vektor následníků";
            // 
            // btnOpenLargeGraph
            // 
            btnOpenLargeGraph.Location = new Point(328, 901);
            btnOpenLargeGraph.Name = "btnOpenLargeGraph";
            btnOpenLargeGraph.Size = new Size(170, 24);
            btnOpenLargeGraph.TabIndex = 5;
            btnOpenLargeGraph.Text = "Zobrazit ve větším okně";
            btnOpenLargeGraph.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1700, 950);
            Controls.Add(groupBoxVisualization);
            Controls.Add(groupBoxPaths);
            Controls.Add(groupBoxRoads);
            Controls.Add(groupBoxTowns);
            Controls.Add(groupBoxData);
            Controls.Add(groupBoxSuccessorVector);
            MinimumSize = new Size(1500, 850);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SemA - Hledání tras";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            groupBoxData.ResumeLayout(false);
            groupBoxTowns.ResumeLayout(false);
            groupBoxTowns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridTownList).EndInit();
            groupBoxRoads.ResumeLayout(false);
            groupBoxRoads.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridRoadList).EndInit();
            groupBoxPaths.ResumeLayout(false);
            groupBoxPaths.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridPathSummary).EndInit();
            groupBoxVisualization.ResumeLayout(false);
            panelGraphCanvas.ResumeLayout(false);
            panelGraphCanvas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridSuccessorVector).EndInit();
            groupBoxSuccessorVector.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBoxData;
        private GroupBox groupBoxTowns;
        private GroupBox groupBoxRoads;
        private GroupBox groupBoxPaths;
        private GroupBox groupBoxVisualization;
        private GroupBox groupBoxSuccessorVector;

        private Button btnLoadData;
        private Button btnSaveData;

        private Button btnAddTown;
        private TextBox txtTownSearch;
        private Button btnFindTown;
        private DataGridView dataGridTownList;
        private DataGridViewTextBoxColumn ColumnTownName;
        private DataGridViewTextBoxColumn ColumnTownX;
        private DataGridViewTextBoxColumn ColumnTownY;

        private Button btnAddRoad;
        private Button btnDeleteRoad;
        private Label labelRoadFrom;
        private ComboBox comboBoxRoadFrom;
        private Label labelRoadTo;
        private ComboBox comboBoxRoadTo;
        private Button btnFindRoad;
        private DataGridView dataGridRoadList;

        private Label labelFindRoute;
        private ComboBox comboBoxStart;
        private ComboBox comboBoxGoal;
        private Button btnFindPaths;

        private DataGridView dataGridPathSummary;
        private DataGridViewTextBoxColumn ColumnPathType;
        private DataGridViewTextBoxColumn ColumnPathTime;
        private DataGridViewTextBoxColumn ColumnPathEdgeCount;
        private RichTextBox richTextBoxDetailedResult;

        private GraphCanvasPanel panelGraphCanvas;
        private DataGridViewTextBoxColumn ColumnFrom;
        private DataGridViewTextBoxColumn ColumnTo;
        private DataGridViewTextBoxColumn ColumnTime;
        private DataGridViewCheckBoxColumn ColumnIsProblematic;

        private DataGridView dataGridSuccessorVector;
        private CheckBox checkBoxShowRoadTimes;
        private Button btnOpenLargeGraph;
    }
}