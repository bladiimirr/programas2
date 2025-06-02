using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;


namespace InterfacesGraficasinamicas
{
    public partial class Form1 : Form
    {
        private string[] imageFiles;
        private FlowLayoutPanel flowLayoutPanel;
        private readonly string imageFolderPath = @"C:\Users\GL66\Desktop\autosModelo"; //CARPETA DE IMAGENES 

        public Form1()
        {
            InitializeComponent();

          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigureMainForm();
            LoadImages();
        }

        private void ConfigureMainForm()
        {
            this.Text = "imagenes";
            this.Size = new Size(800, 600);

            flowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            this.Controls.Add(flowLayoutPanel);
        }



        private void LoadImages()
        {
            try
            {
                if (Directory.Exists(imageFolderPath))
                {
                    imageFiles = Directory.GetFiles(imageFolderPath)
                        .Where(file => IsImageFile(file))
                        .OrderBy(file => file)
                        .ToArray();

                    if (imageFiles.Length == 0)
                    {
                        MessageBox.Show("No se encontraron imágenes en la carpeta especificada.",
                                      "Carpeta vacía",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    else
                    {
                        DisplayThumbnails();
                    }
                }
                else
                {
                    MessageBox.Show($"La carpeta {imageFolderPath} no existe.",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al acceder a la carpeta: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private bool IsImageFile(string file)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return validExtensions.Contains(Path.GetExtension(file).ToLower());
        }
        private void DisplayThumbnails()
        {
            foreach (string imagePath in imageFiles)
            {
                try
                {
                    
                    using (Image originalImage = Image.FromFile(imagePath))
                    {

                        Image thumbnail = CreateThumbnail(originalImage);

                        PictureBox pictureBox = new PictureBox
                        {
                            Image = thumbnail, 
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Width = 150,
                            Height = 150,
                            Margin = new Padding(5),
                            Tag = imagePath
                        };

                        pictureBox.Click += PictureBox_Click;
                        flowLayoutPanel.Controls.Add(pictureBox);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eror al cargar la imagen {Path.GetFileName(imagePath)}: {ex.Message}");
                }
            }
        }
        /* private void DisplayThumbnails()
         {
             forech (string imagePath in imageFiles)
             {
                 try
                 {
                     using (Image originalImage = Image.FromFile(imagePath))
                     {
                         PictureBox pictureBox = new Picturebox
                         {
                             Image = CreateThumbnail(originalImage),
                             SizeMode = PictureBoxSizeMode.Zoom,
                             Width = 150,
                             Height = 150,
                             Margin = new Padding(5),
                             Tag = imagePath
                         };
                 {
                     MessageBox.Show($"Errorcargar la imagen {Path.GetFileName(imagePath)}: {ex.Message}";
                 }
             }
         }*/

        private Image CreateThumbnail(Image original)
        {
            return original.GetThumbnailImage(150, 150, null, IntPtr.Zero);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPicture = (PictureBox)sender;
            string imagePath = (string)clickedPicture.Tag;

            ImageViewerForm viewerForm = new ImageViewerForm(imagePath);
            viewerForm.ShowDialog();
        }
    }

    public class ImageViewerForm : Form
    {
        public ImageViewerForm(string imagePath)
        {
            this.Text = Path.GetFileName(imagePath);
            this.WindowState = FormWindowState.Maximized;

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile(imagePath)
            };

            this.Controls.Add(pictureBox);

            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    pictureBox.Image.Dispose();
                this.Close();
            };
        }
    }
}