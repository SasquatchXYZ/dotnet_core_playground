using System;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace DataParallelismWithForEach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        private void CmdProcess_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(ProcessFiles);
            // Task.Factory.StartNew(() => ProcessFiles());
        }

        private void ProcessFiles()
        {
            // Use ParallelOptions instance to store the CancellationToken.
            var parOpts = new ParallelOptions
            {
                CancellationToken = _cancellationTokenSource.Token,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            };

            // Load up all *.jpg files, and make a new folder for the modified data.
            var files = Directory.GetFiles(@".\TestPictures", "*.jpg", SearchOption.AllDirectories);
            var newDir = @".\ModifiedPictures";
            Directory.CreateDirectory(newDir);

            try
            {
                Parallel.ForEach(files, parOpts, currentFile =>
                {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();

                    var fileName = Path.GetFileName(currentFile);
                    using (var bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, fileName));

                        // this.Title = $"Processing {fileName} on thread {Thread.CurrentThread.ManagedThreadId}";

                        // We need to ensure that the secondary threads access controls created on primary thread in a safe manner.
                        this.Dispatcher.Invoke((Action) delegate
                        {
                            this.Title = $"Processing {fileName} on thread {Thread.CurrentThread.ManagedThreadId}";
                        });
                    }
                });
                this.Dispatcher.Invoke((Action) delegate { this.Title = "Done!"; });
            }
            catch (OperationCanceledException ex)
            {
                this.Dispatcher.Invoke((Action) delegate { this.Title = ex.Message; });
            }

            // Process the image data in a blocking manner
            /*foreach (var currentFile in files)
            {
                var fileName = Path.GetFileName(currentFile);
                using (var bitmap = new Bitmap(fileName))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, fileName));

                    // Print out the ID of the thread processing the current image.
                    this.Title = $"Processing {fileName} on thread {Thread.CurrentThread.ManagedThreadId}";
                }
            }*/
        }
    }
}