using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Neuro.Core;
using Neuro.Properties;
using Neuro.Properties.Learning;
using Neuro.Controls;

namespace TimeSeriesPrediction
{
    public partial class MainForm : Form
    {
        private double[] _data = null;
        private double[,] _dataToShow = null;

        private double _learningRate = 0.1;
        private double _momentum = 0.0;
        private double _sigmoidAlphaValue = 2.0;
        private int _windowSize = 5;
        private int _predictionSize = 1;
        private int _iterations = 1000;

        private Thread _workerThread = null;
        private bool _needToStop = false;
        private BackgroundWorker _backgroundWorker = null;
        delegate void SetIterationCallback(int iteration);
        delegate void SetLearningErrorCallback(double learningError);
        delegate void SetPredictionErrorCallback(double predictionError);
        delegate void SetDataListViewCallback(ListView dataList, int j, int k, double[,] solution);
        delegate void SetEnableControlsCallback(bool enable, Button loadDataButton, TextBox learningRateBox, TextBox momentumBox, TextBox alphaBox,
            TextBox windowSizeBox, TextBox predictionSizeBox, TextBox iterationsBox, Button startButton, Button stopButton);

        private double[,] _windowDelimiter = new double[2, 2] { { 0, 0 }, { 0, 0 } };
        private double[,] _predictionDelimiter = new double[2, 2] { { 0, 0 }, { 0, 0 } };

        public MainForm()
        {
            InitializeComponent();

            // Initialize a chart control
            chart.AddDataSeries("data", Color.Red, Chart.SeriesType.Dots, 5);
            chart.AddDataSeries("solution", Color.Blue, Chart.SeriesType.Line, 1);
            chart.AddDataSeries("window", Color.LightGray, Chart.SeriesType.Line, 1, false);
            chart.AddDataSeries("prediction", Color.Gray, Chart.SeriesType.Line, 1, false);

            // Update controls
            UpdateSettings();
        }

        /// <summary>
        /// Handles the Closing event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void MainFormClosing(object sender, CancelEventArgs e)
        {
            // check if worker thread is running
            if ((_workerThread != null) && (_workerThread.IsAlive))
            {
                _needToStop = true;
                _workerThread.Join();
            }
        }

        #region Operations

        /// <summary>
        /// Updates the settings of the controls.
        /// </summary>
        private void UpdateSettings()
        {
            learningRateBox.Text = _learningRate.ToString();
            momentumBox.Text = _momentum.ToString();
            alphaBox.Text = _sigmoidAlphaValue.ToString();
            windowSizeBox.Text = _windowSize.ToString();
            predictionSizeBox.Text = _predictionSize.ToString();
            iterationsBox.Text = _iterations.ToString();
        }

        /// <summary>
        /// Loads the data from a file.
        /// </summary>
        private void LoadData()
        {
            openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            // show file selection dialog
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = null;
                // read a maximum 50 points
                //double[] tempData = new double[50];

                // read a maximum 100 points
                //double[] tempData = new double[100];

                // read a maximum of 200 points
                double[] tempData = new double[200];

                try
                {
                    // open selected file
                    reader = File.OpenText(openFileDialog.FileName);
                    string str = null;
                    int i = 0;

                    // read the data
                    while ((i < 100) && ((str = reader.ReadLine()) != null))
                    {
                        // parse the value
                        tempData[i] = double.Parse(str);

                        i++;
                    }

                    // allocate and set data
                    _data = new double[i];
                    _dataToShow = new double[i, 2];
                    Array.Copy(tempData, 0, _data, 0, i);
                    for (int j = 0; j < i; j++)
                    {
                        _dataToShow[j, 0] = j;
                        _dataToShow[j, 1] = _data[j];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed reading the file:  " + Environment.NewLine + Environment.NewLine + ex.Message, "File read error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    // close file
                    if (reader != null)
                        reader.Close();
                }

                // update list and chart
                UpdateDataListView();
                chart.RangeX = new DoubleRange(0, _data.Length - 1);
                chart.UpdateDataSeries("data", _dataToShow);
                chart.UpdateDataSeries("solution", null);
                // set delimiters
                UpdateDelimiters();
                // enable "Start" button
                startButton.Enabled = true;
            }
        }

        /// <summary>
        /// Updates the delimiters.
        /// </summary>
        private void UpdateDelimiters()
        {
            // window delimiter
            _windowDelimiter[0, 0] = _windowDelimiter[1, 0] = _windowSize;
            _windowDelimiter[0, 1] = chart.RangeY.Min;
            _windowDelimiter[1, 1] = chart.RangeY.Max;
            chart.UpdateDataSeries("window", _windowDelimiter);
            // prediction delimiter
            _predictionDelimiter[0, 0] = _predictionDelimiter[1, 0] = _data.Length - 1 - _predictionSize;
            _predictionDelimiter[0, 1] = chart.RangeY.Min;
            _predictionDelimiter[1, 1] = chart.RangeY.Max;
            chart.UpdateDataSeries("prediction", _predictionDelimiter);
        }

        /// <summary>
        /// Updates the data list view.
        /// </summary>
        private void UpdateDataListView()
        {
            // remove all current records
            dataList.Items.Clear();
            // add new records
            for (int i = 0, n = _data.GetLength(0); i < n; i++)
            {
                dataList.Items.Add(_data[i].ToString());
            }
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.SetEnableControls(enable, loadDataButton, learningRateBox, momentumBox, alphaBox, windowSizeBox, predictionSizeBox, iterationsBox,
                startButton, stopButton);
        }

        /// <summary>
        /// Updates the size of the window.
        /// </summary>
        private void UpdateWindowSize()
        {
            if (_data != null)
            {
                // get new window size value
                try
                {
                    _windowSize = Math.Max(1, Math.Min(15, int.Parse(windowSizeBox.Text)));
                }
                catch
                {
                    _windowSize = 5;
                }
                // check if we have too few data
                if (_windowSize >= _data.Length)
                    _windowSize = 1;
                // update delimiters
                UpdateDelimiters();
            }
        }

        /// <summary>
        /// Updates the size of the prediction.
        /// </summary>
        private void UpdatePredictionSize()
        {
            if (_data != null)
            {
                // get new prediction size value
                try
                {
                    _predictionSize = Math.Max(1, Math.Min(10, int.Parse(predictionSizeBox.Text)));
                }
                catch
                {
                    _predictionSize = 1;
                }
                // check if we have too few data
                if (_data.Length - _predictionSize - 1 < _windowSize)
                    _predictionSize = 1;
                // update delimiters
                UpdateDelimiters();
            }
        }

        /// <summary>
        /// Calculates the solution based on learning rate, momentum and sigmoid's alpha value.
        /// </summary>
        private void CalculateSolution()
        {
            // clear previous solution
            for (int j = 0, n = _data.Length; j < n; j++)
            {
                if (dataList.Items[j].SubItems.Count > 1)
                    dataList.Items[j].SubItems.RemoveAt(1);
            }

            // get learning rate
            try
            {
                _learningRate = Math.Max(0.00001, Math.Min(1, double.Parse(learningRateBox.Text)));
            }
            catch
            {
                _learningRate = 0.1;
            }
            // get momentum
            try
            {
                _momentum = Math.Max(0, Math.Min(0.5, double.Parse(momentumBox.Text)));
            }
            catch
            {
                _momentum = 0;
            }
            // get sigmoid's alpha value
            try
            {
                _sigmoidAlphaValue = Math.Max(0.001, Math.Min(50, double.Parse(alphaBox.Text)));
            }
            catch
            {
                _sigmoidAlphaValue = 2;
            }
            // iterations
            try
            {
                _iterations = Math.Max(0, int.Parse(iterationsBox.Text));
            }
            catch
            {
                _iterations = 1000;
            }
            // update settings controls
            UpdateSettings();

            // disable all settings controls except "Stop" button
            EnableControls(false);

            // run worker thread
            _needToStop = false;
            _workerThread = new Thread(new ThreadStart(SearchSolution));
            _workerThread.Start();
        }

        /// <summary>
        /// Searches the solution.
        /// </summary>
        private void SearchSolution()
        {
            // number of learning samples
            int samples = _data.Length - _predictionSize - _windowSize;
            // data transformation factor
            double factor = 1.7 / chart.RangeY.Length;
            double yMin = chart.RangeY.Min;
            // prepare learning data
            double[][] input = new double[samples][];
            double[][] output = new double[samples][];

            for (int i = 0; i < samples; i++)
            {
                input[i] = new double[_windowSize];
                output[i] = new double[1];

                // set input
                for (int j = 0; j < _windowSize; j++)
                {
                    input[i][j] = (_data[i + j] - yMin) * factor - 0.85;
                }
                // set output
                output[i][0] = (_data[i + _windowSize] - yMin) * factor - 0.85;
            }

            // create multi-layer neural network
            ActivationNetwork network = new ActivationNetwork(new BipolarSigmoidFunction(_sigmoidAlphaValue), _windowSize, _windowSize * 2, 1);

            // create teacher
            BackPropagationLearning teacher = new BackPropagationLearning(network);

            // set learning rate and momentum
            teacher.LearningRate = _learningRate;
            teacher.Momentum = _momentum;

            // iterations
            int iteration = 1;

            // solution array
            int solutionSize = _data.Length - _windowSize;
            double[,] solution = new double[solutionSize, 2];
            double[] networkInput = new double[_windowSize];

            // calculate X values to be used with solution function
            for (int j = 0; j < solutionSize; j++)
            {
                solution[j, 0] = j + _windowSize;
            }

            // loop
            while (!_needToStop)
            {
                // run epoch of learning procedure
                double error = teacher.RunEpoch(input, output) / samples;

                // calculate solution and learning and prediction errors
                double learningError = 0.0;
                double predictionError = 0.0;
                // go through all the data
                for (int i = 0, n = _data.Length - _windowSize; i < n; i++)
                {
                    // put values from current window as network's input
                    for (int j = 0; j < _windowSize; j++)
                    {
                        networkInput[j] = (_data[i + j] - yMin) * factor - 0.85;
                    }

                    // evalue the function
                    solution[i, 1] = (network.Compute(networkInput)[0] + 0.85) / factor + yMin;

                    // calculate prediction error
                    if (i >= n - _predictionSize)
                    {
                        predictionError += Math.Abs(solution[i, 1] - _data[_windowSize + i]);
                    }
                    else
                    {
                        learningError += Math.Abs(solution[i, 1] - _data[_windowSize + i]);
                    }
                }
                // update solution on the chart
                chart.UpdateDataSeries("solution", solution);

                //Engage the background worker (optional)
                //SetBackgroundWorker();

                // set current iteration's info via the delegate
                this.SetIteration(iteration);
                this.SetLearningError(learningError);
                this.SetPredictionError(predictionError);

                // increase current iteration
                iteration++;

                // check if we need to stop
                if ((_iterations != 0) && (iteration > _iterations))
                    break;
            }

            // show new solution via the delegate
            for (int j = _windowSize, k = 0, n = _data.Length; j < n; j++, k++)
            {
                this.SetDataListView(dataList, j, k, solution);
            }

            // enable settings controls via the delegate
            EnableControls(true);
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the loadDataButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LoadDataButtonClick(object sender, System.EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Handles the Click event of the startButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StartButtonClick(object sender, System.EventArgs e)
        {
            CalculateSolution();
        }

        /// <summary>
        /// Handles the Click event of the stopButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StopButtonClick(object sender, System.EventArgs e)
        {
            // stop worker thread
            _needToStop = true;
            _workerThread.Join();
            _workerThread = null;
        }

        /// <summary>
        /// Handles the TextChanged event of the windowSizeBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WindowSizeBoxTextChanged(object sender, System.EventArgs e)
        {
            UpdateWindowSize();
        }

        /// <summary>
        /// Handles the TextChanged event of the predictionSizeBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PredictionSizeBoxTextChanged(object sender, System.EventArgs e)
        {
            UpdatePredictionSize();
        }

        #endregion

        #region Multiple Thread Handlers

        /// <summary>
        /// Sets the iteration sequence.
        /// </summary>
        /// <param name="iteration">The iteration.</param>
        private void SetIteration(int iteration)
        {
            // InvokeRequired required compares the thread ID of the calling and creation threads. If these threads are different, it returns true.
            if (this.currentIterationBox.InvokeRequired)
            {
                SetIterationCallback d = new SetIterationCallback(SetIteration);
                this.Invoke(d, new object[] { iteration });
            }
            else
            {
                this.currentIterationBox.Text = iteration.ToString();
            }
        }

        private void SetLearningError(double learningError)
        {
            if (this.currentLearningErrorBox.InvokeRequired)
            {
                SetLearningErrorCallback d = new SetLearningErrorCallback(SetLearningError);
                this.Invoke(d, new object[] { learningError });
            }
            else
            {
                this.currentLearningErrorBox.Text = learningError.ToString("F3");
            }
        }

        private void SetPredictionError(double predictionError)
        {
            if (this.currentPredictionErrorBox.InvokeRequired)
            {
                SetPredictionErrorCallback d = new SetPredictionErrorCallback(SetPredictionError);
                this.Invoke(d, new object[] { predictionError });
            }
            else
            {
                this.currentPredictionErrorBox.Text = predictionError.ToString("F3");
            }
        }

        private void SetDataListView(ListView dataList, int j, int k, double[,] solution)
        {
            if (this.dataList.InvokeRequired)
            {
                SetDataListViewCallback d = new SetDataListViewCallback(SetDataListView);
                this.Invoke(d, new object[] { dataList, j, k, solution });
            }
            else
            {
                this.dataList.Items[j].SubItems.Add(solution[k, 1].ToString());
            }
        }

        private void SetEnableControls(bool enable, Button loadDataButton, TextBox learningRateBox, TextBox momentumBox, TextBox alphaBox, TextBox windowSizeBox,
            TextBox predictionSizeBox, TextBox iterationsBox, Button startButton, Button stopButton)
        {
            if (this.loadDataButton.InvokeRequired)
            {
                SetEnableControlsCallback d = new SetEnableControlsCallback(SetEnableControls);
                this.Invoke(d, new object[] { enable, loadDataButton, learningRateBox, momentumBox, alphaBox, windowSizeBox, predictionSizeBox, iterationsBox,
                    startButton, stopButton });
            }
            else
            {
                loadDataButton.Enabled = enable;
                learningRateBox.Enabled = enable;
                momentumBox.Enabled = enable;
                alphaBox.Enabled = enable;
                windowSizeBox.Enabled = enable;
                predictionSizeBox.Enabled = enable;
                iterationsBox.Enabled = enable;

                startButton.Enabled = enable;
                stopButton.Enabled = !enable;
            }
        }

        #endregion

        #region Optional Background worker for enhanced thread safety during prediction operations
        private void SetBackgroundWorker()
        {
            this._backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        #endregion
    }
}