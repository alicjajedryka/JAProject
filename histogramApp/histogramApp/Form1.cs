/*
 * Temat projektu: Histogram aplikacji z obs³ug¹ DLL w jêzykach ASM i C
 * 
 * Opis algorytmu:
 * Program umo¿liwia generowanie histogramu kolorów obrazu BMP przy u¿yciu bibliotek DLL
 * napisanych w jêzykach ASM i C. Obliczenia s¹ równoleg³e, a wyniki s¹ zapisywane w pliku tekstowym.
 *
 * Data wykonania: Styczeñ 2025, Semestr Zimowy / Rok akademicki 2024/2025
 * Autor: Alicja Jêdryka
 *
 * Wersja 1.0:
 * - Obs³uga generowania histogramów za pomoc¹ bibliotek ASM i C
 * - Wielow¹tkowoœæ dla przyspieszenia obliczeñ
 * - Mo¿liwoœæ zapisu wyników w formacie tekstowym
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HistogramApp
{
    public partial class Form1 : Form
    {
        // Import funkcji z DLL napisanych w ASM i C
        [DllImport("C:\\Users\\hp\\source\\repos\\histogramApp\\x64\\Debug\\HistogramAsm.dll")]
        private static extern int calculateHistogramAsm(
            byte[] imageData, int pixels, int[] histogramB, int[] histogramG, int[] histogramR);

        [DllImport("C:\\Users\\hp\\source\\repos\\histogramApp\\histogramApp\\libs\\HistogramDLL.dll")]
        private static extern int calculateHistogram(
            byte[] imageData, int pixels, int[] histogramB, int[] histogramG, int[] histogramR);

        // Delegat umo¿liwiaj¹cy dynamiczne prze³¹czanie funkcji DLL
        private Func<byte[], int, int[], int[], int[], int> calculateHistogramMethod;

        // Konstruktor inicjalizuj¹cy komponenty GUI
        public Form1()
        {
            InitializeComponent();
        }

        // Procedura obs³uguj¹ca wybór pliku BMP
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Bitmap Images (.bmp)|*.bmp",
                Title = "Select a BMP Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = openFileDialog.FileName;
            }
        }

        // Procedura obs³uguj¹ca zapis histogramu do pliku
        private void btnSaveHistogram_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (.txt)|*.txt",
                Title = "Save Histogram as TXT"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtHistogramPath.Text = saveFileDialog.FileName;
            }
        }

        /*
         * Procedura generuj¹ca histogram
         * Parametry wejœciowe:
         * - Pobiera œcie¿kê do pliku BMP i œcie¿kê zapisu histogramu z pól tekstowych GUI
         * Parametry wyjœciowe:
         * - Generuje histogramy dla kolorów R, G, B i zapisuje je w pliku tekstowym
         */
        private void btnGenerateHistogram_Click(object sender, EventArgs e)
        {
            if (calculateHistogramMethod == null)
            {
                MessageBox.Show("Please select a DLL method (ASM or C).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string inputFilePath = txtImagePath.Text;
            string outputFilePath = txtHistogramPath.Text;

            if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
            {
                MessageBox.Show("Please select an image and specify a save location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // £adowanie danych obrazu
            byte[] imageData;
            int width, height;
            if (!LoadBMP(inputFilePath, out imageData, out width, out height))
            {
                MessageBox.Show("Failed to load image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int[] histogramB = new int[256];
            int[] histogramG = new int[256];
            int[] histogramR = new int[256];

            Stopwatch stopwatch = Stopwatch.StartNew();

            // Wielow¹tkowe obliczanie histogramu
            int pixels = width * height;
            int result = calculateHistogramMethod(imageData, pixels, histogramB, histogramG, histogramR);
            

            stopwatch.Stop();
            if (result != 0)
            {
                MessageBox.Show("Error during histogram calculation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblExecutionTime.Text = $"Execution Time: {stopwatch.ElapsedMilliseconds} ms";

            SaveHistogramToFile(histogramR, histogramG, histogramB, outputFilePath);
            MessageBox.Show("Histogram generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /*
         * Procedura ³aduj¹ca dane obrazu BMP
         * Parametry wejœciowe:
         * - filename: œcie¿ka do pliku BMP
         * Parametry wyjœciowe:
         * - imageData: tablica bajtów z danymi obrazu
         * - width: szerokoœæ obrazu
         * - height: wysokoœæ obrazu
         */
        private bool LoadBMP(string filename, out byte[] imageData, out int width, out int height)
        {
            imageData = null;
            width = 0;
            height = 0;

            try
            {
                Bitmap bitmap = new Bitmap(filename);
                width = bitmap.Width;
                height = bitmap.Height;

                var data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                int size = data.Stride * bitmap.Height;
                imageData = new byte[size];
                Marshal.Copy(data.Scan0, imageData, 0, size);
                bitmap.UnlockBits(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
         * Procedura zapisuj¹ca histogram do pliku tekstowego
         * Parametry wejœciowe:
         * - histogramB: tablica z wartoœciami histogramu dla koloru niebieskiego
         * - histogramG: tablica z wartoœciami histogramu dla koloru zielonego
         * - histogramR: tablica z wartoœciami histogramu dla koloru czerwonego
         * - outputFilePath: œcie¿ka zapisu pliku tekstowego z histogramem
         * Parametry wyjœciowe:
         * - Brak bezpoœredniego zwrotu wartoœci. Tworzony jest plik tekstowy zawieraj¹cy dane histogramu.
         */
        private void SaveHistogramToFile(int[] histogramB, int[] histogramG, int[] histogramR, string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Intensity Level\tBlue\tGreen\tRed");
                writer.WriteLine("=====================================");

                for (int i = 0; i < 256; i++)
                {
                    writer.WriteLine($"{i,-15}\t{histogramB[i]}\t{histogramG[i]}\t{histogramR[i],-10}");
                }
            }
        }

        /*
         * Procedura ³¹cz¹ca dane histogramów lokalnych z globalnym histogramem
         * Parametry wejœciowe:
         * - source: tablica z danymi lokalnego histogramu
         * - target: tablica z danymi globalnego histogramu
         * Parametry wyjœciowe:
         * - Brak zwracanych wartoœci. Funkcja aktualizuje globalny histogram, sumuj¹c wartoœci lokalne.
         */
        //private void MergeHistograms(int[] source, int[] target)
        //{
        //    for (int i = 0; i < 256; i++)
        //    {
        //        target[i] += source[i];
        //    }
        //}

        /*
         * Procedura ustawiaj¹ca metodê obliczania histogramu na funkcjê z biblioteki ASM
         * Parametry wejœciowe:
         * - Brak
         * Parametry wyjœciowe:
         * - Ustawia delegat `calculateHistogramMethod` na `calculateHistogramAsm`
         */
        private void btnAsmDLL_Click(object sender, EventArgs e)
        {
            calculateHistogramMethod = calculateHistogramAsm;
            MessageBox.Show("Using ASM DLL", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        /*
         * Procedura ustawiaj¹ca metodê obliczania histogramu na funkcjê z biblioteki C
         * Parametry wejœciowe:
         * - Brak
         * Parametry wyjœciowe:
         * - Ustawia delegat `calculateHistogramMethod` na `calculateHistogram`
         */
        private void btnCDLL_Click(object sender, EventArgs e)
        {
            calculateHistogramMethod = calculateHistogram;
            MessageBox.Show("Using C DLL", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
