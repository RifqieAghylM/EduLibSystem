using eduLib.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace eduLib.UI
{
    public partial class KelolaAdmin : Form
    {
        private const string BASE_URL = "https://localhost:7053/api/Books";
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


            buttonCari.Enabled = false;
            buttonCari.Text = "Mencari...";


            try
            {

                using HttpClient client =
                    new HttpClient();



                var response =

                await client.GetAsync($"{BASE_URL}/search?keyword={keyword}");



                if (response.IsSuccessStatusCode)
                {

                    string json = await response.Content.ReadAsStringAsync();



                    var options = new JsonSerializerOptions();



                    options.PropertyNameCaseInsensitive = true;



                    List<Book> books =
                        JsonSerializer.Deserialize<List<Book>>

                        (

                            json,

                            options

                        );


                    dataGridViewBuku.DataSource = null;

                    dataGridViewBuku.Columns.Clear();



                    dataGridViewBuku.DataSource =
                        books;

                    // hide column

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

                    MessageBox.Show(

                        "Gagal mengambil data"

                    );

                }



            }

            catch (Exception ex)
            {

                MessageBox.Show(

                    ex.Message

                );

            }



            buttonCari.Enabled = true;

            buttonCari.Text = "Cari";

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
