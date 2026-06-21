using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using eduLib.Core;
using eduLib.Infrastructure.API;

namespace eduLib.UI
{
    
    public partial class EduLibAdmin1 : Form
    {
        private readonly string BASE_URL = ApiHelper.GetBaseUrl(); //mengambiil url dari config runtime di appsetting json
        private string selectedPdfPath = ""; // variabel untuk menyimpan path file PDF yang dipilih
        private bool isPdfChanged = false; // variaebl ini untuk menandakan apakah file pdf diganti saat mode edit
        private bool isEditMode = false; //menandakan form dalam mode upload atau edit
        private string currentBookId = ""; //menyimpan id buku yang sedang diedit

        //constructor upload buku
        public EduLibAdmin1()
        {
            InitializeComponent();
        }
        //constructor edit buku
        public EduLibAdmin1(string id,string title,string author,int year)
        {
            InitializeComponent();

            isEditMode = true;

            currentBookId = id;

            textBoxTitle.Text = title;

            textBoxAuthor.Text = author;

            textBoxYear.Text = year.ToString();

            buttonUpload.Text = "Update";

            buttonBrowser.Text ="PDF Lama Digunakan";

        }
        //validasi input form
        private bool ValidateForm()
        {
            if (!ValidateTitle()) return false;

            if (!ValidateAuthor()) return false;

            if (!ValidateYear()) return false;

            if (!ValidatePdf()) return false;

            if (!isEditMode &&
                !File.Exists(selectedPdfPath))
            {
                MessageBox.Show("PDF tidak ditemukan");
                return false;
            }

            return true;
        }
        private bool ValidateTitle()
        {
            if (string.IsNullOrWhiteSpace(textBoxTitle.Text))
            {
                MessageBox.Show("Title harus diisi");
                return false;
            }

            if (Regex.IsMatch(textBoxTitle.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Title tidak boleh hanya angka");
                return false;
            }

            return true;
        }

        private bool ValidateAuthor()
        {
            if (string.IsNullOrWhiteSpace(textBoxAuthor.Text))
            {
                MessageBox.Show("Author harus diisi");
                return false;
            }

            if (Regex.IsMatch(textBoxAuthor.Text.Trim(), @"^\d+$"))
            {
                MessageBox.Show("Author tidak boleh hanya angka");
                return false;
            }

            return true;
        }

        private bool ValidateYear()
        {
            if (!int.TryParse(textBoxYear.Text, out int year))
            {
                MessageBox.Show("Year harus angka");
                return false;
            }

            if (year < 1500 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Year tidak valid");
                return false;
            }

            return true;
        }
        //validasi file pdf
        private bool ValidatePdf()
        {
            if (!isEditMode && string.IsNullOrEmpty(selectedPdfPath))
            {
                MessageBox.Show("Pilih PDF");
                return false;
            }

            if (!string.IsNullOrEmpty(selectedPdfPath))
            {
                FileInfo file = new FileInfo(selectedPdfPath);

                long maxSize = 50 * 1024 * 1024; // 50 MB

                if (file.Length > maxSize)
                {
                    MessageBox.Show("Ukuran PDF harus kurang dari 50 MB.");
                    return false;
                }
            }

            return true;
        }

        private StreamContent CreatePdfContent()
        {
            FileStream stream = File.OpenRead(selectedPdfPath);

            StreamContent fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return fileContent;
        }
        //Membuat konten multipart untuk dikirim ke API
        private MultipartFormDataContent CreateContent()
        {

            MultipartFormDataContent content =
                new MultipartFormDataContent();


            content.Add(

                new StringContent(
                    textBoxTitle.Text),

                "title"

            );

            content.Add(

                new StringContent(
                    textBoxAuthor.Text),

                "author"

            );

            content.Add(

                new StringContent(
                    textBoxYear.Text),

                "year"

            );

            return content;

        }

        //mengirim data buku ke API
        private async Task UploadBookAsync()
        {
            using HttpClient client = new();
            // PERBAIKAN BUG: Menangkap return value dari CreateContent()
            MultipartFormDataContent content = CreateContent();

            byte[] bytes =
                File.ReadAllBytes(selectedPdfPath); // Membaca file PDF menjadi byte array

            ByteArrayContent pdf =
                new ByteArrayContent(bytes); //mengubah byte array menjadi Byte array http content

            pdf.Headers.ContentType =
                new MediaTypeHeaderValue(
                    "application/pdf");

            content.Add(

                pdf,

                "pdfFile",

                Path.GetFileName(selectedPdfPath)

            );


            var response =
                await client.PostAsync(

                    $"{BASE_URL}/upload",

                    content

                );


            string result =
                await response.Content.ReadAsStringAsync();


            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show(
                    "Upload berhasil");

                buttonReset.PerformClick();
            }

            else
            {
                MessageBox.Show(result);
            }
        }
        //mengirim data buku yang diubah (update)ke API
        private async Task UpdateBookAsync()
        {
            using HttpClient client = new HttpClient();
            MultipartFormDataContent content = CreateContent();

            // hanya kirim PDF kalau diganti
            if (isPdfChanged)
            {

                FileStream stream = File.OpenRead(selectedPdfPath);

                StreamContent fileContent =
                    new StreamContent(stream);

                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue(
                        "application/pdf"
                    );

                content.Add(
                    fileContent,
                    "pdfFile",
                    Path.GetFileName(selectedPdfPath)
                );

            }

            HttpResponseMessage response = await client.PutAsync( $"{BASE_URL}/{currentBookId}", content);
            string result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Update berhasil");
                this.Close();
            }

            else
            {
                MessageBox.Show(result);
            }

        }
        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAuthor_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonBrowser_Click(object sender,EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "PDF Files (*.pdf)|*.pdf";

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                    selectedPdfPath = ofd.FileName;
                    buttonBrowser.Text =

                    Path.GetFileName(selectedPdfPath);
                    isPdfChanged = true;
            }

        }

        private async void buttonUpload_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;
            buttonUpload.Enabled = false; // matikan tombol 
            try
            {
                if (isEditMode)
                {
                    await UpdateBookAsync();

                }
                else
                {
                    await UploadBookAsync();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                buttonUpload.Enabled = true; // hidupkan kembali tombol 
            }
        }

        private void ResetForm()
        {
            textBoxTitle.Clear();
            textBoxAuthor.Clear();
            textBoxYear.Clear();

            selectedPdfPath = "";
            isPdfChanged = false;

            buttonBrowser.Text = "Browser";

            buttonUpload.Text = isEditMode ? "Update" : "Upload";
        }
        private void buttonReset_Click(object sender,EventArgs e)
        {
           ResetForm();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        
    }
}
