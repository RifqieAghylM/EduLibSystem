using eduLib.Core;
using eduLib.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using eduLib.Infrastructure.API;

namespace eduLib.UI
{
    public partial class KelolaAdmin : Form
    {
        private readonly string BASE_URL = ApiHelper.GetBaseUrl(); //mengambil url dari config runtime di appsetting json
        public KelolaAdmin()
        {
            InitializeComponent();
        }

        // Event handler untuk tombol "Cari"
        private async void buttonCari_Click(object sender, EventArgs e)
        {

            string keyword = textBoxSearch.Text.Trim();


            if (string.IsNullOrWhiteSpace(keyword))
            {

                MessageBox.Show(
                    "Masukkan kata kunci");

                return;
            }
            buttonCari.Enabled = false; // matikan tombol sementara menunggu hasil pencarian
            buttonCari.Text = "Mencari...";


            try
            {
                string url = $"{BASE_URL}/search?keyword={keyword}";

                //IMPLEMENTASI PARAMETERIZATION / GENERICS
                // Menggunakan method generic <Book> dari ApiHelper
                List<Book> books = await ApiHelper.GetListAsync<Book>(url);


                if (books != null && books.Count > 0)
                {
                    dataGridViewBuku.DataSource = null;
                    dataGridViewBuku.Columns.Clear();
                    dataGridViewBuku.DataSource = books;

                    // Menyembunyikan kolom yang tidak perlu ditampilkan ke user
                    if (dataGridViewBuku.Columns["Id"] != null)
                        dataGridViewBuku.Columns["Id"].Visible = false;
                    if (dataGridViewBuku.Columns["PdfPath"] != null)
                        dataGridViewBuku.Columns["PdfPath"].Visible = false;
                    if (dataGridViewBuku.Columns["GridFsFileId"] != null)
                        dataGridViewBuku.Columns["GridFsFileId"].Visible = false;

                    AddButtonColumns();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan.");
                    dataGridViewBuku.DataSource = null;
                    dataGridViewBuku.Columns.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
            finally
            {
                // Clean Code: Nyalakan kembali tombol setelah selesai
                buttonCari.Enabled = true;
                buttonCari.Text = "Cari";
            }
        }
        // Method untuk menambahkan kolom tombol "Edit" dan "Delete" ke DataGridView
        private void AddButtonColumns()
        {


            if (dataGridViewBuku.Columns["Edit"] == null)
            {

                DataGridViewButtonColumn edit =
                    new DataGridViewButtonColumn();


                edit.Name = "Edit";

                edit.Text = "Edit";

                edit.HeaderText = "Edit";

                edit.UseColumnTextForButtonValue = true;


                dataGridViewBuku.Columns.Add(edit);


            }

            if (dataGridViewBuku.Columns["Delete"] == null)
            {

                DataGridViewButtonColumn delete =
                    new DataGridViewButtonColumn();


                delete.Name = "Delete";

                delete.Text = "Delete";

                delete.HeaderText = "Delete";

                delete.UseColumnTextForButtonValue = true;
                dataGridViewBuku.Columns.Add(delete);


            }

        }
        // Event handler untuk klik pada sel di DataGridView (untuk Edit dan Delete)
        private async void dataGridViewBuku_CellContentClick( object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Book selected = (Book)dataGridViewBuku.Rows[e.RowIndex].DataBoundItem;

            // EDIT
            if (dataGridViewBuku.Columns[e.ColumnIndex].Name == "Edit")
            {
               using  EduLibAdmin1 frm =

                    new EduLibAdmin1(

                        selected.Id,

                        selected.Title,

                        selected.Author,

                        selected.Year

                    );


                frm.ShowDialog();
                // Opsional: Otomatis mencari ulang setelah form edit ditutup agar data refresh
                // buttonCari.PerformClick();

                return;

            }

            // DELETE
            if (dataGridViewBuku.Columns[e.ColumnIndex].Name == "Delete")
            {

                DialogResult result =

                    MessageBox.Show(

                        "Apakah anda yakin?",


                        "Konfirmasi",


                        MessageBoxButtons.YesNo,


                        MessageBoxIcon.Question

                    );

                if (result != DialogResult.Yes)
                    return;

                DialogResult result2 =

                    MessageBox.Show(

                        "Anda benar-benar yakin?",


                        "Konfirmasi Hapus",


                        MessageBoxButtons.YesNo,


                        MessageBoxIcon.Warning

                    );


                if (result2 != DialogResult.Yes)
                    return;

                using HttpClient client = new HttpClient();
                var response = await client.DeleteAsync($"{BASE_URL}/{selected.Id}");

                if (response.IsSuccessStatusCode)
                {

                    MessageBox.Show(

                        "Buku berhasil dihapus"

                    );

                    buttonCari.PerformClick();

                }
                else
                {

                    MessageBox.Show(

                        "Gagal menghapus buku"

                    );

                }

            }

        }
        // Event handler untuk tombol "Kembali"
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
