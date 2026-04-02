using System.Text;
using SemA.Core;
using System.Globalization;
using System.Drawing.Drawing2D;


namespace SemA.GUI
{
    public partial class MainForm : Form
    {
        private readonly GraphFileLoader graphFileLoader = new();
        private readonly GraphFileSaver graphFileSaver = new();
        private Graph<string, Town, Road>? graph;
        private bool isFillingRoadGrid;
        private List<string> currentlyHighlightedPath = new();
        private Panel? panelGraphToolbar;

        public MainForm()
        {
            InitializeComponent();

            InitializeGraphToolbarLayout();

            btnLoadData.Click += btnLoadData_Click;
            btnSaveData.Click += btnSaveData_Click;
            btnAddTown.Click += btnAddTown_Click;
            btnAddRoad.Click += btnAddRoad_Click;
            btnFindPaths.Click += btnFindPaths_Click;
            btnFindTown.Click += btnFindTown_Click;
            txtTownSearch.KeyDown += txtTownSearch_KeyDown;

            btnDeleteRoad.Click += btnDeleteRoad_Click;
            btnFindRoad.Click += btnFindRoad_Click;
            panelGraphCanvas.Paint += panelGraphCanvas_Paint;

            dataGridRoadList.CurrentCellDirtyStateChanged += dataGridRoadList_CurrentCellDirtyStateChanged;
            dataGridRoadList.CellValueChanged += dataGridRoadList_CellValueChanged;
            dataGridRoadList.SelectionChanged += dataGridRoadList_SelectionChanged;
            dataGridRoadList.CellValidating += dataGridRoadList_CellValidating;

            btnDeleteRoad.Enabled = false;

            dataGridRoadList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridRoadList.MultiSelect = false;

            ColumnFrom.ReadOnly = true;
            ColumnTo.ReadOnly = true;
            ColumnTime.ReadOnly = false;
            ColumnIsProblematic.ReadOnly = false;

            dataGridTownList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridTownList.MultiSelect = false;
            dataGridTownList.ReadOnly = true;
            dataGridTownList.AllowUserToAddRows = false;
            dataGridTownList.AllowUserToDeleteRows = false;
            dataGridTownList.AllowUserToResizeRows = false;

            ColumnTownName.ReadOnly = true;
            ColumnTownX.ReadOnly = true;
            ColumnTownY.ReadOnly = true;

            richTextBoxDetailedResult.Font = new Font("Consolas", 10F);
            dataGridSuccessorVector.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridSuccessorVector.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridSuccessorVector.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridSuccessorVector.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridSuccessorVector.RowTemplate.Height = 24;
            dataGridSuccessorVector.ColumnHeadersHeight = 24;
            dataGridSuccessorVector.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridSuccessorVector.ScrollBars = ScrollBars.Horizontal;

            dataGridSuccessorVector.EnableHeadersVisualStyles = false;
            dataGridSuccessorVector.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridSuccessorVector.Font, FontStyle.Bold);

            dataGridPathSummary.SelectionChanged += dataGridPathSummary_SelectionChanged;
            dataGridPathSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridPathSummary.MultiSelect = false;
            dataGridPathSummary.ReadOnly = true;
            dataGridPathSummary.AllowUserToAddRows = false;
            dataGridPathSummary.AllowUserToDeleteRows = false;
            dataGridPathSummary.AllowUserToResizeRows = false;

            checkBoxShowRoadTimes.Checked = false;
            checkBoxShowRoadTimes.CheckedChanged += checkBoxShowRoadTimes_CheckedChanged;

            btnOpenLargeGraph.Click += btnOpenLargeGraph_Click;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGraphVisualization();
        }

        private void InitializeGraphToolbarLayout()
        {
            if (panelGraphCanvas.Parent is not Control graphContainer)
            {
                return;
            }

            panelGraphToolbar = new Panel
            {
                Name = "panelGraphToolbar",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = graphContainer.BackColor
            };

            graphContainer.SuspendLayout();

            panelGraphToolbar.Controls.Add(btnOpenLargeGraph);
            panelGraphToolbar.Controls.Add(checkBoxShowRoadTimes);

            btnOpenLargeGraph.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnOpenLargeGraph.AutoSize = false;
            btnOpenLargeGraph.Size = new Size(150, 24);

            checkBoxShowRoadTimes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxShowRoadTimes.AutoSize = true;

            graphContainer.Controls.Add(panelGraphToolbar);

            panelGraphCanvas.Dock = DockStyle.Fill;
           

            PositionGraphToolbarControls();

            panelGraphToolbar.Resize += panelGraphToolbar_Resize;

            graphContainer.ResumeLayout();
        }

        private void panelGraphToolbar_Resize(object? sender, EventArgs e)
        {
            PositionGraphToolbarControls();
        }

        private void PositionGraphToolbarControls()
        {
            if (panelGraphToolbar is null)
            {
                return;
            }
            
            btnOpenLargeGraph.Location = new Point(
                8,
                (panelGraphToolbar.ClientSize.Height - btnOpenLargeGraph.Height) / 2);

            checkBoxShowRoadTimes.Location = new Point(
                panelGraphToolbar.ClientSize.Width - checkBoxShowRoadTimes.PreferredSize.Width - 12,
                (panelGraphToolbar.ClientSize.Height - checkBoxShowRoadTimes.Height) / 2);
        }
        private void checkBoxShowRoadTimes_CheckedChanged(object? sender, EventArgs e)
        {
            RefreshGraphVisualization();
        }

        private void DrawGraphCanvasBorder(Graphics graphics, Rectangle drawingArea)
        {
            using Pen borderPen = new(Color.FromArgb(160, 160, 160));

            graphics.DrawRectangle(
                borderPen,
                0,
                0,
                Math.Max(0, drawingArea.Width - 1),
                Math.Max(0, drawingArea.Height - 1));
        }
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();

            openFileDialog.Title = "Vyber soubor s grafem";
            openFileDialog.Filter = "Textové soubory (*.txt)|*.txt|Všechny soubory (*.*)|*.*";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                graph = graphFileLoader.LoadFromFile(openFileDialog.FileName);

                FillCityList();
                FillRoadGrid();
                FillStartAndGoalComboBoxes();
                ClearPathOutput();

                MessageBox.Show(
                    "Data byla úspìšnì naètena.",
                    "Hotovo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"Pøi naèítání dat došlo k chybì:\n{exception.Message}",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAddTown_Click(object sender, EventArgs e)
        {
            using AddTownForm addTownForm = new();

            if (addTownForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            graph ??= new Graph<string, Town, Road>();

            Town newTown = new(
                addTownForm.TownName,
                new Coordinates(addTownForm.PositionX, addTownForm.PositionY));

            bool wasAdded = graph.AddVertex(addTownForm.TownName, newTown);

            if (!wasAdded)
            {
                MessageBox.Show(
                    "Mìsto s tímto názvem už v grafu existuje.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            FillCityList();
            FillStartAndGoalComboBoxes();
        }

        private void btnFindPaths_Click(object sender, EventArgs e)
        {
            ClearPathOutput();

            if (graph is null)
            {
                MessageBox.Show(
                    "Nejprve naèti data nebo vytvoø graf.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (comboBoxStart.SelectedItem is not string startVertexKey)
            {
                MessageBox.Show(
                    "Vyber startovní mìsto.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (comboBoxGoal.SelectedItem is not string goalVertexKey)
            {
                MessageBox.Show(
                    "Vyber cílové mìsto.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                AStarPathFinder<string, Town, Road> aStarPathFinder = new();
                AlternativeRouteFinder<string, Town, Road> alternativeRouteFinder = new();

                List<string> shortestPath = aStarPathFinder.FindPath(
     graph,
     startVertexKey,
     goalVertexKey);

                List<List<string>> alternativePaths = new();

                if (shortestPath.Count > 0)
                {
                    alternativePaths = alternativeRouteFinder.FindAlternativeRoutes(
                        graph,
                        startVertexKey,
                        goalVertexKey,
                        road => road.IsProblematic);
                }

                SuccessorVector<string> successorVector = SuccessorVectorBuilder.Build(
                    shortestPath,
                    alternativePaths);

                FillFoundPathsList(shortestPath, alternativePaths);
                FillSuccessorVectorGrid(successorVector);
                FillDetailedResult(startVertexKey, goalVertexKey, shortestPath, alternativePaths);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"Pøi hledání tras došlo k chybì:\n{exception.Message}",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FillCityList()
        {
            dataGridTownList.Rows.Clear();

            if (graph is null)
            {
                return;
            }

            foreach (string vertexKey in graph.VertexKeys)
            {
                if (!graph.TryGetVertexData(vertexKey, out Town town))
                {
                    continue;
                }

                dataGridTownList.Rows.Add(
                    vertexKey,
                    town.Position.X,
                    town.Position.Y);
            }
            RefreshGraphVisualization();
        }

        private void FillRoadGrid()
        {
            isFillingRoadGrid = true;

            dataGridRoadList.Rows.Clear();

            if (graph is null)
            {
                isFillingRoadGrid = false;
                RefreshGraphVisualization();
                return;
            }

            foreach (string fromVertexKey in graph.VertexKeys)
            {
                foreach ((string neighborKey, Road edgeData) in graph.GetNeighbors(fromVertexKey))
                {
                    if (string.Compare(fromVertexKey, neighborKey, StringComparison.Ordinal) > 0)
                    {
                        continue;
                    }

                    dataGridRoadList.Rows.Add(
                        fromVertexKey,
                        neighborKey,
                        edgeData.Time,
                        edgeData.IsProblematic);
                }
            }

            isFillingRoadGrid = false;
            UpdateDeleteRoadButtonState();
            RefreshGraphVisualization();
        }

        private void FillStartAndGoalComboBoxes()
        {
            comboBoxStart.Items.Clear();
            comboBoxGoal.Items.Clear();
            comboBoxRoadFrom.Items.Clear();
            comboBoxRoadTo.Items.Clear();

            if (graph is null)
            {
                return;
            }

            foreach (string vertexKey in graph.VertexKeys)
            {
                comboBoxStart.Items.Add(vertexKey);
                comboBoxGoal.Items.Add(vertexKey);
                comboBoxRoadFrom.Items.Add(vertexKey);
                comboBoxRoadTo.Items.Add(vertexKey);
            }

            if (comboBoxStart.Items.Count > 0)
            {
                comboBoxStart.SelectedIndex = 0;
            }

            if (comboBoxGoal.Items.Count > 1)
            {
                comboBoxGoal.SelectedIndex = 1;
            }
            else if (comboBoxGoal.Items.Count > 0)
            {
                comboBoxGoal.SelectedIndex = 0;
            }

            if (comboBoxRoadFrom.Items.Count > 0)
            {
                comboBoxRoadFrom.SelectedIndex = 0;
            }

            if (comboBoxRoadTo.Items.Count > 1)
            {
                comboBoxRoadTo.SelectedIndex = 1;
            }
            else if (comboBoxRoadTo.Items.Count > 0)
            {
                comboBoxRoadTo.SelectedIndex = 0;
            }
        }
        private void FillFoundPathsList(
         List<string> shortestPath,
         List<List<string>> alternativePaths)
        {
            dataGridPathSummary.Rows.Clear();
            currentlyHighlightedPath.Clear();

            if (shortestPath.Count == 0)
            {
                dataGridPathSummary.Rows.Add("Nenalezena", "-", "-");
                RefreshGraphVisualization();
                return;
            }

            AddPathSummaryRow("Nejkratší", shortestPath);

            for (int alternativePathIndex = 0;
                 alternativePathIndex < alternativePaths.Count;
                 alternativePathIndex++)
            {
                AddPathSummaryRow(
                    $"Alternativa {alternativePathIndex + 1}",
                    alternativePaths[alternativePathIndex]);
            }

            if (dataGridPathSummary.Rows.Count > 0)
            {
                dataGridPathSummary.ClearSelection();
                dataGridPathSummary.CurrentCell = dataGridPathSummary.Rows[0].Cells[0];
                dataGridPathSummary.Rows[0].Selected = true;

                UpdateHighlightedPathFromSelectedRow();
            }
        }
        private string FormatTime(double time)
        {
            return time.ToString("0.##", CultureInfo.CurrentCulture);
        }
        private void AddPathSummaryRow(string pathLabel, List<string> path)
        {
            int addedRowIndex = dataGridPathSummary.Rows.Add(
                pathLabel,
                FormatTime(CalculatePathTime(path)),
                Math.Max(path.Count - 1, 0));

            dataGridPathSummary.Rows[addedRowIndex].Tag = new List<string>(path);
        }

        private void dataGridPathSummary_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateHighlightedPathFromSelectedRow();
        }

        private void UpdateHighlightedPathFromSelectedRow()
        {
            currentlyHighlightedPath.Clear();

            if (dataGridPathSummary.SelectedRows.Count != 1)
            {
                RefreshGraphVisualization();
                return;
            }

            object? selectedRowTag = dataGridPathSummary.SelectedRows[0].Tag;

            if (selectedRowTag is List<string> selectedPath)
            {
                currentlyHighlightedPath = new List<string>(selectedPath);
            }

            RefreshGraphVisualization();
        }

        private double GetEdgeTime(string fromVertexKey, string toVertexKey)
        {
            if (graph is null)
            {
                throw new InvalidOperationException("Graf není naètený.");
            }

            if (!graph.TryGetEdgeData(fromVertexKey, toVertexKey, out Road road))
            {
                throw new InvalidOperationException(
                    $"Nepodaøilo se najít hranu mezi vrcholy '{fromVertexKey}' a '{toVertexKey}'.");
            }

            return road.Time;
        }

        private void AppendPathDetail(StringBuilder resultBuilder, string sectionTitle, List<string> path)
        {
            resultBuilder.AppendLine(sectionTitle);
            resultBuilder.AppendLine(new string('=', sectionTitle.Length));

            if (path.Count == 0)
            {
                resultBuilder.AppendLine("Cesta nebyla nalezena.");
                resultBuilder.AppendLine();
                return;
            }

            resultBuilder.AppendLine($"Trasa: {FormatPath(path)}");
            resultBuilder.AppendLine($"Celkový èas: {FormatTime(CalculatePathTime(path))}");
            resultBuilder.AppendLine($"Poèet hran: {Math.Max(path.Count - 1, 0)}");
            resultBuilder.AppendLine();

            resultBuilder.AppendLine("Rozpis hran:");
            for (int vertexIndex = 0; vertexIndex < path.Count - 1; vertexIndex++)
            {
                string fromVertexKey = path[vertexIndex];
                string toVertexKey = path[vertexIndex + 1];
                double edgeTime = GetEdgeTime(fromVertexKey, toVertexKey);

                resultBuilder.AppendLine(
                    $"  {fromVertexKey} -> {toVertexKey}   (èas: {FormatTime(edgeTime)})");
            }

            resultBuilder.AppendLine();
        }

        private void FillDetailedResult(
          string startVertexKey,
          string goalVertexKey,
          List<string> shortestPath,
          List<List<string>> alternativePaths)
        {
            StringBuilder resultBuilder = new();

            resultBuilder.AppendLine("VÝSLEDEK HLEDÁNÍ TRAS");
            resultBuilder.AppendLine("====================");
            resultBuilder.AppendLine();
            resultBuilder.AppendLine($"Start: {startVertexKey}");
            resultBuilder.AppendLine($"Cíl:   {goalVertexKey}");
            resultBuilder.AppendLine();

            AppendPathDetail(resultBuilder, "NEJKRATŠÍ TRASA", shortestPath);

            resultBuilder.AppendLine("ALTERNATIVNÍ TRASY");
            resultBuilder.AppendLine("==================");

            if (alternativePaths.Count == 0)
            {
                resultBuilder.AppendLine("Žádné alternativní trasy nebyly nalezeny.");
                resultBuilder.AppendLine();
            }
            else
            {
                for (int alternativePathIndex = 0;
                     alternativePathIndex < alternativePaths.Count;
                     alternativePathIndex++)
                {
                    AppendPathDetail(
                        resultBuilder,
                        $"ALTERNATIVA {alternativePathIndex + 1}",
                        alternativePaths[alternativePathIndex]);
                }
            }



            richTextBoxDetailedResult.Text = resultBuilder.ToString();
        }

        private void ClearPathOutput()
        {
            dataGridPathSummary.Rows.Clear();
            dataGridSuccessorVector.Columns.Clear();
            dataGridSuccessorVector.Rows.Clear();
            richTextBoxDetailedResult.Clear();

            currentlyHighlightedPath.Clear();
            RefreshGraphVisualization();
        }

        private string FormatPath(List<string> path)
        {
            if (path.Count == 0)
            {
                return "(žádná cesta)";
            }

            return string.Join(" -> ", path);
        }

        private double CalculatePathTime(List<string> path)
        {
            if (graph is null || path.Count < 2)
            {
                return 0;
            }

            double totalTime = 0;

            for (int vertexIndex = 0; vertexIndex < path.Count - 1; vertexIndex++)
            {
                string fromVertexKey = path[vertexIndex];
                string toVertexKey = path[vertexIndex + 1];

                bool edgeFound = false;

                foreach ((string neighborKey, Road edgeData) in graph.GetNeighbors(fromVertexKey))
                {
                    if (neighborKey == toVertexKey)
                    {
                        totalTime += edgeData.Time;
                        edgeFound = true;
                        break;
                    }
                }

                if (!edgeFound)
                {
                    throw new InvalidOperationException(
                        $"Nepodaøilo se najít hranu mezi vrcholy '{fromVertexKey}' a '{toVertexKey}'.");
                }
            }

            return totalTime;
        }

        private void dataGridRoadList_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dataGridRoadList.IsCurrentCellDirty)
            {
                dataGridRoadList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridRoadList_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (isFillingRoadGrid)
            {
                return;
            }

            if (graph is null)
            {
                return;
            }

            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridRoadList.Rows[e.RowIndex];

            string? fromVertexKey = row.Cells[ColumnFrom.Index].Value?.ToString();
            string? toVertexKey = row.Cells[ColumnTo.Index].Value?.ToString();

            if (string.IsNullOrWhiteSpace(fromVertexKey) || string.IsNullOrWhiteSpace(toVertexKey))
            {
                return;
            }

            if (!graph.TryGetEdgeData(fromVertexKey, toVertexKey, out Road road))
            {
                return;
            }

            if (e.ColumnIndex == ColumnIsProblematic.Index)
            {
                bool isProblematic = false;

                object? rawValue = row.Cells[ColumnIsProblematic.Index].Value;
                if (rawValue is bool boolValue)
                {
                    isProblematic = boolValue;
                }

                road.IsProblematic = isProblematic;
                ClearPathOutput();
                RefreshGraphVisualization();
                return;
            }

            if (e.ColumnIndex == ColumnTime.Index)
            {
                string? rawTimeText = row.Cells[ColumnTime.Index].Value?.ToString();

                if (!TryParseRoadTime(rawTimeText, out double newTime))
                {
                    return;
                }

                road.Time = newTime;
                ClearPathOutput();
                RefreshGraphVisualization();
            }
        }

        private void UpdateRoadProblematicState(string fromVertexKey, string toVertexKey, bool isProblematic)
        {
            if (graph is null)
            {
                return;
            }

            foreach ((string neighborKey, Road edgeData) in graph.GetNeighbors(fromVertexKey))
            {
                if (neighborKey == toVertexKey)
                {
                    edgeData.IsProblematic = isProblematic;
                    break;
                }
            }

            foreach ((string neighborKey, Road edgeData) in graph.GetNeighbors(toVertexKey))
            {
                if (neighborKey == fromVertexKey)
                {
                    edgeData.IsProblematic = isProblematic;
                    break;
                }
            }
        }

        private void btnAddRoad_Click(object sender, EventArgs e)
        {
            if (graph is null)
            {
                MessageBox.Show(
                    "Nejprve naèti data nebo vytvoø graf s mìsty.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            List<string> townKeys = graph.VertexKeys.ToList();

            if (townKeys.Count < 2)
            {
                MessageBox.Show(
                    "Pro pøidání silnice musí v grafu existovat alespoò dvì mìsta.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using AddRoadForm addRoadForm = new AddRoadForm(townKeys);

            if (addRoadForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                Road newRoad = new Road(
                    addRoadForm.RoadTime,
                    addRoadForm.IsProblematic);

                bool wasAdded = graph.AddEdge(
                    addRoadForm.FromTownKey,
                    addRoadForm.ToTownKey,
                    newRoad);

                if (!wasAdded)
                {
                    MessageBox.Show(
                        "Silnice mezi tìmito mìsty už v grafu existuje.",
                        "Chyba",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                FillRoadGrid();
                ClearPathOutput();

                MessageBox.Show(
                    "Silnice byla úspìšnì pøidána.",
                    "Hotovo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"Pøi pøidávání silnice došlo k chybì:\n{exception.Message}",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdateDeleteRoadButtonState()
        {
            btnDeleteRoad.Enabled = dataGridRoadList.SelectedRows.Count == 1;
        }

        private void dataGridRoadList_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateDeleteRoadButtonState();
        }

        private void btnDeleteRoad_Click(object sender, EventArgs e)
        {
            if (graph is null)
            {
                MessageBox.Show(
                    "Graf ještì není naètený.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (dataGridRoadList.SelectedRows.Count != 1)
            {
                MessageBox.Show(
                    "Nejprve vyber silnici, kterou chceš smazat.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow selectedRow = dataGridRoadList.SelectedRows[0];

            string? fromVertexKey = selectedRow.Cells[ColumnFrom.Index].Value?.ToString();
            string? toVertexKey = selectedRow.Cells[ColumnTo.Index].Value?.ToString();

            if (string.IsNullOrWhiteSpace(fromVertexKey) || string.IsNullOrWhiteSpace(toVertexKey))
            {
                MessageBox.Show(
                    "Nepodaøilo se zjistit, kterou silnici chceš smazat.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            DialogResult dialogResult = MessageBox.Show(
                $"Opravdu chceš smazat silnici {fromVertexKey} - {toVertexKey}?",
                "Potvrzení",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult != DialogResult.Yes)
            {
                return;
            }

            bool wasRemoved = graph.RemoveEdge(fromVertexKey, toVertexKey);

            if (!wasRemoved)
            {
                MessageBox.Show(
                    "Silnici se nepodaøilo smazat.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            FillRoadGrid();
            ClearPathOutput();
            UpdateDeleteRoadButtonState();

            MessageBox.Show(
                "Silnice byla úspìšnì smazána.",
                "Hotovo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool TryParseRoadTime(string? text, out double time)
        {
            time = 0;

            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            return double.TryParse(
                       text,
                       NumberStyles.Float,
                       CultureInfo.CurrentCulture,
                       out time)
                   || double.TryParse(
                       text,
                       NumberStyles.Float,
                       CultureInfo.InvariantCulture,
                       out time);
        }

        private void dataGridRoadList_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (isFillingRoadGrid)
            {
                return;
            }

            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex != ColumnTime.Index)
            {
                return;
            }

            string enteredText = e.FormattedValue?.ToString()?.Trim() ?? string.Empty;

            if (!TryParseRoadTime(enteredText, out double parsedTime) || parsedTime <= 0)
            {
                e.Cancel = true;

                MessageBox.Show(
                    "Zadej platný èas vìtší než 0. Mùžeš použít èárku i teèku.",
                    "Neplatná hodnota",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (graph is null)
            {
                MessageBox.Show(
                    "Nejprve naèti data nebo vytvoø graf, který chceš uložit.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using SaveFileDialog saveFileDialog = new();

            saveFileDialog.Title = "Uložit graf";
            saveFileDialog.Filter = "Textové soubory (*.txt)|*.txt|Všechny soubory (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = "graf.txt";

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                graphFileSaver.SaveToFile(graph, saveFileDialog.FileName);

                MessageBox.Show(
                    "Data byla úspìšnì uložena.",
                    "Hotovo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"Pøi ukládání dat došlo k chybì:\n{exception.Message}",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnFindTown_Click(object sender, EventArgs e)
        {
            if (graph is null)
            {
                MessageBox.Show(
                    "Nejprve naèti data nebo vytvoø graf.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string searchedTownKey = txtTownSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchedTownKey))
            {
                MessageBox.Show(
                    "Zadej název mìsta, které chceš vyhledat.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtTownSearch.Focus();
                return;
            }

            if (!graph.TryGetVertexData(searchedTownKey, out Town foundTown))
            {
                MessageBox.Show(
                    $"Mìsto '{searchedTownKey}' nebylo nalezeno.",
                    "Nenalezeno",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            bool rowWasSelected = SelectTownRowByKey(searchedTownKey);

            if (!rowWasSelected)
            {
                MessageBox.Show(
                    $"Mìsto '{searchedTownKey}' bylo nalezeno v grafu, ale nepodaøilo se oznaèit jeho øádek v tabulce.",
                    "Upozornìní",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            MessageBox.Show(
                $"Mìsto '{searchedTownKey}' bylo nalezeno.\nSouøadnice: X = {foundTown.Position.X}, Y = {foundTown.Position.Y}",
                "Mìsto nalezeno",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool SelectTownRowByKey(string searchedTownKey)
        {
            foreach (DataGridViewRow row in dataGridTownList.Rows)
            {
                string? townKey = row.Cells[ColumnTownName.Index].Value?.ToString();

                if (townKey != searchedTownKey)
                {
                    continue;
                }

                dataGridTownList.ClearSelection();
                row.Selected = true;
                dataGridTownList.CurrentCell = row.Cells[ColumnTownName.Index];

                if (row.Index >= 0)
                {
                    dataGridTownList.FirstDisplayedScrollingRowIndex = row.Index;
                }

                return true;
            }

            return false;
        }
        private void txtTownSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFindTown_Click(sender ?? this, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }

        private void btnFindRoad_Click(object sender, EventArgs e)
        {
            if (graph is null)
            {
                MessageBox.Show(
                    "Nejprve naèti data nebo vytvoø graf.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (comboBoxRoadFrom.SelectedItem is not string fromVertexKey)
            {
                MessageBox.Show(
                    "Vyber poèáteèní mìsto silnice.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (comboBoxRoadTo.SelectedItem is not string toVertexKey)
            {
                MessageBox.Show(
                    "Vyber cílové mìsto silnice.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (fromVertexKey == toVertexKey)
            {
                MessageBox.Show(
                    "Vyber dvì rùzná mìsta.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!graph.TryGetEdgeData(fromVertexKey, toVertexKey, out Road foundRoad))
            {
                MessageBox.Show(
                    $"Silnice mezi mìsty '{fromVertexKey}' a '{toVertexKey}' nebyla nalezena.",
                    "Nenalezeno",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            bool rowWasSelected = SelectRoadRowByKeys(fromVertexKey, toVertexKey);

            if (!rowWasSelected)
            {
                MessageBox.Show(
                    $"Silnice byla nalezena v grafu, ale nepodaøilo se oznaèit její øádek v tabulce.",
                    "Upozornìní",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            MessageBox.Show(
                $"Silnice byla nalezena.\nÈas: {foundRoad.Time}\nProblematická: {(foundRoad.IsProblematic ? "ano" : "ne")}",
                "Silnice nalezena",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool SelectRoadRowByKeys(string fromVertexKey, string toVertexKey)
        {
            foreach (DataGridViewRow row in dataGridRoadList.Rows)
            {
                string? rowFrom = row.Cells[ColumnFrom.Index].Value?.ToString();
                string? rowTo = row.Cells[ColumnTo.Index].Value?.ToString();

                bool sameDirection =
                    rowFrom == fromVertexKey && rowTo == toVertexKey;

                bool oppositeDirection =
                    rowFrom == toVertexKey && rowTo == fromVertexKey;

                if (!sameDirection && !oppositeDirection)
                {
                    continue;
                }

                dataGridRoadList.ClearSelection();
                row.Selected = true;
                dataGridRoadList.CurrentCell = row.Cells[ColumnFrom.Index];

                if (row.Index >= 0)
                {
                    dataGridRoadList.FirstDisplayedScrollingRowIndex = row.Index;
                }

                return true;
            }

            return false;
        }

        private void RefreshGraphVisualization()
        {
            panelGraphCanvas.Invalidate();
        }

        private void panelGraphCanvas_Paint(object? sender, PaintEventArgs e)
        {
            DrawGraph(
                e.Graphics,
                panelGraphCanvas.ClientRectangle,
                checkBoxShowRoadTimes.Checked);
        }

        private void DrawGraph(Graphics graphics, Rectangle drawingArea, bool showRoadTimes)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.FromArgb(248, 249, 252));

            if (graph is null)
            {
                using SolidBrush messageBrush = new(Color.FromArgb(90, 90, 100));
                graphics.DrawString(
                    "Naèti data pro vykreslení grafu.",
                    Font,
                    messageBrush,
                    new PointF(18, 18));
                DrawGraphCanvasBorder(graphics, drawingArea);
                return;
            }

            Dictionary<string, PointF> townScreenPoints = BuildTownScreenPoints(drawingArea);

            if (townScreenPoints.Count == 0)
            {
                using SolidBrush messageBrush = new(Color.FromArgb(90, 90, 100));
                graphics.DrawString(
                    "Graf neobsahuje žádná mìsta.",
                    Font,
                    messageBrush,
                    new PointF(18, 18));
                DrawGraphCanvasBorder(graphics, drawingArea);
                return;
            }

            using Pen normalRoadPen = new(Color.FromArgb(120, 130, 145), 2.2f);
            using Pen problematicRoadPen = new(Color.FromArgb(210, 75, 75), 2.4f);
            using Pen highlightedPathPen = new(Color.FromArgb(235, 255, 140, 0), 4.2f);
            using Pen townOutlinePen = new(Color.FromArgb(35, 75, 135), 1.8f);
            using SolidBrush townFillBrush = new(Color.FromArgb(90, 155, 235));
            using SolidBrush townLabelBrush = new(Color.FromArgb(50, 50, 60));
            using SolidBrush legendNormalBrush = new(Color.FromArgb(120, 130, 145));
            using SolidBrush legendProblemBrush = new(Color.FromArgb(210, 75, 75));
            using SolidBrush legendHighlightedBrush = new(Color.FromArgb(255, 140, 0));

            problematicRoadPen.DashStyle = DashStyle.Dash;
            highlightedPathPen.StartCap = LineCap.Round;
            highlightedPathPen.EndCap = LineCap.Round;
            highlightedPathPen.LineJoin = LineJoin.Round;

            foreach (string fromVertexKey in graph.VertexKeys)
            {
                foreach ((string toVertexKey, Road road) in graph.GetNeighbors(fromVertexKey))
                {
                    if (string.Compare(fromVertexKey, toVertexKey, StringComparison.Ordinal) > 0)
                    {
                        continue;
                    }

                    if (!townScreenPoints.TryGetValue(fromVertexKey, out PointF fromPoint))
                    {
                        continue;
                    }

                    if (!townScreenPoints.TryGetValue(toVertexKey, out PointF toPoint))
                    {
                        continue;
                    }

                    Pen roadPen = road.IsProblematic ? problematicRoadPen : normalRoadPen;
                    graphics.DrawLine(roadPen, fromPoint, toPoint);

                    if (showRoadTimes)
                    {
                        DrawRoadTimeLabel(graphics, fromPoint, toPoint, FormatTime(road.Time));
                    }
                }
            }

            if (currentlyHighlightedPath.Count >= 2)
            {
                for (int vertexIndex = 0; vertexIndex < currentlyHighlightedPath.Count - 1; vertexIndex++)
                {
                    string fromVertexKey = currentlyHighlightedPath[vertexIndex];
                    string toVertexKey = currentlyHighlightedPath[vertexIndex + 1];

                    if (!townScreenPoints.TryGetValue(fromVertexKey, out PointF fromPoint))
                    {
                        continue;
                    }

                    if (!townScreenPoints.TryGetValue(toVertexKey, out PointF toPoint))
                    {
                        continue;
                    }

                    graphics.DrawLine(highlightedPathPen, fromPoint, toPoint);
                }
            }

            const float townRadius = 7f;

            foreach ((string townKey, PointF townPoint) in townScreenPoints)
            {
                RectangleF townRectangle = new(
                    townPoint.X - townRadius,
                    townPoint.Y - townRadius,
                    townRadius * 2,
                    townRadius * 2);

                graphics.FillEllipse(townFillBrush, townRectangle);
                graphics.DrawEllipse(townOutlinePen, townRectangle);

                graphics.DrawString(
                    townKey,
                    Font,
                    townLabelBrush,
                    townPoint.X + townRadius + 4f,
                    townPoint.Y - townRadius - 2f);
            }

            graphics.FillRectangle(legendNormalBrush, 16, 16, 20, 4);
            graphics.DrawString("bìžná silnice", Font, townLabelBrush, 42, 9);

            graphics.FillRectangle(legendProblemBrush, 16, 38, 20, 4);
            graphics.DrawString("problematická silnice", Font, townLabelBrush, 42, 31);

            graphics.FillRectangle(legendHighlightedBrush, 16, 60, 20, 4);
            graphics.DrawString("vybraná trasa", Font, townLabelBrush, 42, 53);

            DrawGraphCanvasBorder(graphics, drawingArea);
        }

        private void btnOpenLargeGraph_Click(object? sender, EventArgs e)
        {
            using GraphPreviewForm graphPreviewForm = new(
                DrawGraph,
                checkBoxShowRoadTimes.Checked);

            graphPreviewForm.ShowDialog(this);
        }
        private void DrawRoadTimeLabel(Graphics graphics, PointF fromPoint, PointF toPoint, string timeText)
        {
            float middleX = (fromPoint.X + toPoint.X) / 2f;
            float middleY = (fromPoint.Y + toPoint.Y) / 2f;

            float deltaX = toPoint.X - fromPoint.X;
            float deltaY = toPoint.Y - fromPoint.Y;
            float lineLength = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (lineLength <= 0.01f)
            {
                lineLength = 1f;
            }

            float perpendicularOffsetX = -deltaY / lineLength * 10f;
            float perpendicularOffsetY = deltaX / lineLength * 10f;

            float labelX = middleX + perpendicularOffsetX;
            float labelY = middleY + perpendicularOffsetY;

            using Font labelFont = new(Font.FontFamily, 8.5f, FontStyle.Regular);
            using SolidBrush textBrush = new(Color.FromArgb(55, 55, 65));
            using SolidBrush backgroundBrush = new(Color.FromArgb(235, 255, 255, 255));
            using Pen borderPen = new(Color.FromArgb(190, 200, 210));

            SizeF textSize = graphics.MeasureString(timeText, labelFont);

            RectangleF backgroundRectangle = new(
                labelX - textSize.Width / 2f - 4f,
                labelY - textSize.Height / 2f - 2f,
                textSize.Width + 8f,
                textSize.Height + 4f);

            graphics.FillRectangle(backgroundBrush, backgroundRectangle);
            graphics.DrawRectangle(
                borderPen,
                backgroundRectangle.X,
                backgroundRectangle.Y,
                backgroundRectangle.Width,
                backgroundRectangle.Height);

            graphics.DrawString(
                timeText,
                labelFont,
                textBrush,
                backgroundRectangle.X + 4f,
                backgroundRectangle.Y + 2f);
        }

        private Dictionary<string, PointF> BuildTownScreenPoints(Rectangle drawingArea)
        {
            Dictionary<string, Town> townsByKey = new();

            if (graph is null)
            {
                return new Dictionary<string, PointF>();
            }

            foreach (string vertexKey in graph.VertexKeys)
            {
                if (!graph.TryGetVertexData(vertexKey, out Town town))
                {
                    continue;
                }

                townsByKey[vertexKey] = town;
            }

            Dictionary<string, PointF> townPoints = new();

            if (townsByKey.Count == 0)
            {
                return townPoints;
            }

            double minX = townsByKey.Values.Min(town => town.Position.X);
            double maxX = townsByKey.Values.Max(town => town.Position.X);
            double minY = townsByKey.Values.Min(town => town.Position.Y);
            double maxY = townsByKey.Values.Max(town => town.Position.Y);

            double rangeX = maxX - minX;
            double rangeY = maxY - minY;

            if (rangeX <= 0)
            {
                rangeX = 1;
            }

            if (rangeY <= 0)
            {
                rangeY = 1;
            }

            const float padding = 35f;

            float availableWidth = Math.Max(1f, drawingArea.Width - 2f * padding);
            float availableHeight = Math.Max(1f, drawingArea.Height - 2f * padding);

            float scale = (float)Math.Min(
                availableWidth / rangeX,
                availableHeight / rangeY);

            float drawingWidth = (float)(rangeX * scale);
            float drawingHeight = (float)(rangeY * scale);

            float offsetX = drawingArea.Left + (drawingArea.Width - drawingWidth) / 2f;
            float bottomY = drawingArea.Top + (drawingArea.Height + drawingHeight) / 2f;

            foreach ((string townKey, Town town) in townsByKey)
            {
                float screenX = offsetX + (float)((town.Position.X - minX) * scale);
                float screenY = bottomY - (float)((town.Position.Y - minY) * scale);

                townPoints[townKey] = new PointF(screenX, screenY);
            }

            return townPoints;
        }

        private void labelRoadFrom_Click(object sender, EventArgs e)
        {

        }

        private void FillSuccessorVectorGrid(SuccessorVector<string> successorVector)
        {
            dataGridSuccessorVector.Columns.Clear();
            dataGridSuccessorVector.Rows.Clear();

            DataGridViewTextBoxColumn rowHeaderColumn = new()
            {
                Name = "ColumnLabel",
                HeaderText = "",
                ReadOnly = true,
                Frozen = true,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };

            dataGridSuccessorVector.Columns.Add(rowHeaderColumn);

            foreach (string sourceVertexKey in successorVector.OrderedSourceVertices)
            {
                DataGridViewTextBoxColumn vertexColumn = new()
                {
                    Name = $"Column_{sourceVertexKey}",
                    HeaderText = sourceVertexKey,
                    ReadOnly = true,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };

                dataGridSuccessorVector.Columns.Add(vertexColumn);
            }

            dataGridSuccessorVector.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dataGridSuccessorVector.Columns[0].DefaultCellStyle.Font =
                new Font(dataGridSuccessorVector.Font, FontStyle.Bold);

            int startRowIndex = dataGridSuccessorVector.Rows.Add();
            int goalRowIndex = dataGridSuccessorVector.Rows.Add();

            dataGridSuccessorVector.Rows[startRowIndex].Height = 24;
            dataGridSuccessorVector.Rows[goalRowIndex].Height = 24;

            dataGridSuccessorVector.Rows[startRowIndex].Cells[0].Value = "Start";
            dataGridSuccessorVector.Rows[goalRowIndex].Cells[0].Value = "Cíl";

            for (int columnIndex = 0; columnIndex < successorVector.OrderedSourceVertices.Count; columnIndex++)
            {
                string sourceVertexKey = successorVector.OrderedSourceVertices[columnIndex];
                string successorsText = successorVector.FormatSuccessors(sourceVertexKey, " | ");

                dataGridSuccessorVector.Rows[startRowIndex].Cells[columnIndex + 1].Value = sourceVertexKey;
                dataGridSuccessorVector.Rows[goalRowIndex].Cells[columnIndex + 1].Value =
                    string.IsNullOrWhiteSpace(successorsText) ? "-" : successorsText;
            }

            dataGridSuccessorVector.ClearSelection();
        }
    }

}