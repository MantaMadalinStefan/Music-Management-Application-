using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MusicWinFormsApp
{
    public class MainForm : Form
    {
        // UI Components
        private ListBox listBoxMuzicieni;
        private ListBox listBoxAlbume;
        private ListBox listBoxRelatii;
        private TextBox txtNumeMuzician, txtGenMuzical, txtTitluAlbum, txtAnAlbum;
        private Button btnAdaugaMuzician, btnModificaMuzician, btnStergeMuzician;
        private Button btnAdaugaAlbum, btnModificaAlbum, btnStergeAlbum;
        private Button btnCreareRelatie, btnStergeRelatie;

        // Connection string
        private string connectionString = "Server=127.0.0.1;Database=music;User ID=root;Password=root;";

        public MainForm()
        {
            Text = "Music Management";
            Width = 1000;
            Height = 600;

            InitializeUI();

            // Load data when the form starts
            AfiseazaMuzicieni();
            AfiseazaAlbume();
            AfiseazaRelatii();
        }

        private void InitializeUI()
        {
            // ListBoxes
            listBoxMuzicieni = new ListBox { Top = 20, Left = 20, Width = 250, Height = 200 };
            listBoxAlbume = new ListBox { Top = 20, Left = 290, Width = 250, Height = 200 };
            listBoxRelatii = new ListBox { Top = 20, Left = 560, Width = 250, Height = 200 };

            // TextBoxes
            txtNumeMuzician = new TextBox { Top = 240, Left = 20, Width = 150, PlaceholderText = "Nume Muzician" };
            txtGenMuzical = new TextBox { Top = 270, Left = 20, Width = 150, PlaceholderText = "Gen Muzical" };
            txtTitluAlbum = new TextBox { Top = 240, Left = 290, Width = 150, PlaceholderText = "Titlu Album" };
            txtAnAlbum = new TextBox { Top = 270, Left = 290, Width = 150, PlaceholderText = "An Lansare" };

            // Buttons for Muzicieni
            btnAdaugaMuzician = new Button { Top = 310, Left = 20, Width = 150, Text = "Adaugă Muzician" };
            btnModificaMuzician = new Button { Top = 350, Left = 20, Width = 150, Text = "Modifică Muzician" };
            btnStergeMuzician = new Button { Top = 390, Left = 20, Width = 150, Text = "Șterge Muzician" };

            // Buttons for Albume
            btnAdaugaAlbum = new Button { Top = 310, Left = 290, Width = 150, Text = "Adaugă Album" };
            btnModificaAlbum = new Button { Top = 350, Left = 290, Width = 150, Text = "Modifică Album" };
            btnStergeAlbum = new Button { Top = 390, Left = 290, Width = 150, Text = "Șterge Album" };

            // Buttons for Relatii
            btnCreareRelatie = new Button { Top = 310, Left = 560, Width = 150, Text = "Crează Relație" };
            btnStergeRelatie = new Button { Top = 350, Left = 560, Width = 150, Text = "Șterge Relație" };

            // Add controls to the form
            Controls.AddRange(new Control[] {
                listBoxMuzicieni, listBoxAlbume, listBoxRelatii,
                txtNumeMuzician, txtGenMuzical, txtTitluAlbum, txtAnAlbum,
                btnAdaugaMuzician, btnModificaMuzician, btnStergeMuzician,
                btnAdaugaAlbum, btnModificaAlbum, btnStergeAlbum,
                btnCreareRelatie, btnStergeRelatie
            });

            // Attach event handlers
            btnAdaugaMuzician.Click += BtnAdaugaMuzician_Click;
            btnModificaMuzician.Click += BtnModificaMuzician_Click;
            btnStergeMuzician.Click += BtnStergeMuzician_Click;
            btnAdaugaAlbum.Click += BtnAdaugaAlbum_Click;
            btnModificaAlbum.Click += BtnModificaAlbum_Click;
            btnStergeAlbum.Click += BtnStergeAlbum_Click;
            btnCreareRelatie.Click += BtnCreareRelatie_Click;
            btnStergeRelatie.Click += BtnStergeRelatie_Click;
        }

        // ---------- Muzicieni CRUD ----------
        private void BtnAdaugaMuzician_Click(object sender, EventArgs e)
        {
            string nume = txtNumeMuzician.Text;
            string gen = txtGenMuzical.Text;

            if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(gen))
            {
                MessageBox.Show("Completează toate câmpurile!");
                return;
            }

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Musicians (nume, gen_muzical) VALUES (@nume, @gen)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nume", nume);
                    cmd.Parameters.AddWithValue("@gen", gen);
                    cmd.ExecuteNonQuery();
                }
            }

            AfiseazaMuzicieni();
        }

        private void BtnModificaMuzician_Click(object sender, EventArgs e)
        {
            if (listBoxMuzicieni.SelectedItem == null)
            {
                MessageBox.Show("Selectează un muzician!");
                return;
            }

            var parts = listBoxMuzicieni.SelectedItem.ToString().Split('|');
            string muzicianId = parts[0].Trim();
            string numeNou = txtNumeMuzician.Text;
            string genNou = txtGenMuzical.Text;

            if (string.IsNullOrWhiteSpace(numeNou) || string.IsNullOrWhiteSpace(genNou))
            {
                MessageBox.Show("Completează toate câmpurile!");
                return;
            }

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE Musicians SET nume=@nume, gen_muzical=@gen WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nume", numeNou);
                    cmd.Parameters.AddWithValue("@gen", genNou);
                    cmd.Parameters.AddWithValue("@id", muzicianId);
                    cmd.ExecuteNonQuery();
                }
            }

            AfiseazaMuzicieni();
        }

        private void BtnStergeMuzician_Click(object sender, EventArgs e)
        {
            if (listBoxMuzicieni.SelectedItem == null)
            {
                MessageBox.Show("Selectează un muzician!");
                return;
            }

            var parts = listBoxMuzicieni.SelectedItem.ToString().Split('|');
            string muzicianId = parts[0].Trim();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql1 = "DELETE FROM Albums_Musicians WHERE id_muzician=@id";
                string sql2 = "DELETE FROM Musicians WHERE id=@id";
                using (var cmd1 = new MySqlCommand(sql1, conn))
                {
                    cmd1.Parameters.AddWithValue("@id", muzicianId);
                    cmd1.ExecuteNonQuery();
                }
                using (var cmd2 = new MySqlCommand(sql2, conn))
                {
                    cmd2.Parameters.AddWithValue("@id", muzicianId);
                    cmd2.ExecuteNonQuery();
                }
            }

            AfiseazaMuzicieni();
            AfiseazaRelatii();
        }

        // ---------- Albume CRUD ----------
        private void BtnAdaugaAlbum_Click(object sender, EventArgs e)
        {
            string titlu = txtTitluAlbum.Text;
            if (!int.TryParse(txtAnAlbum.Text, out int an))
            {
                MessageBox.Show("Anul trebuie să fie un număr valid!");
                return;
            }

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Albums (titlu, an_lansare) VALUES (@titlu, @an)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@titlu", titlu);
                    cmd.Parameters.AddWithValue("@an", an);
                    cmd.ExecuteNonQuery();
                }
            }

            AfiseazaAlbume();
        }

        private void BtnModificaAlbum_Click(object sender, EventArgs e)
        {
            if (listBoxAlbume.SelectedItem == null)
            {
                MessageBox.Show("Selectează un album!");
                return;
            }

            var parts = listBoxAlbume.SelectedItem.ToString().Split('|');
            string albumId = parts[0].Trim();
            string titluNou = txtTitluAlbum.Text;

            if (!int.TryParse(txtAnAlbum.Text, out int anNou) || string.IsNullOrWhiteSpace(titluNou))
            {
                MessageBox.Show("Completează toate câmpurile corect!");
                return;
            }

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE Albums SET titlu=@titlu, an_lansare=@an WHERE id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@titlu", titluNou);
                    cmd.Parameters.AddWithValue("@an", anNou);
                    cmd.Parameters.AddWithValue("@id", albumId);
                    cmd.ExecuteNonQuery();
                }
            }

            AfiseazaAlbume();
        }

        private void BtnStergeAlbum_Click(object sender, EventArgs e)
        {
            if (listBoxAlbume.SelectedItem == null)
            {
                MessageBox.Show("Selectează un album!");
                return;
            }

            var parts = listBoxAlbume.SelectedItem.ToString().Split('|');
            string albumId = parts[0].Trim();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql1 = "DELETE FROM Albums_Musicians WHERE id_album=@id";
                string sql2 = "DELETE FROM Albums WHERE id=@id";
                using (var cmd1 = new MySqlCommand(sql1, conn))
                {
                    cmd1.Parameters.AddWithValue("@id", albumId);
                    cmd1.ExecuteNonQuery();
                }
                using (var cmd2 = new MySqlCommand(sql2, conn))
                {
                    cmd2.Parameters.AddWithValue("@id", albumId);
                    cmd2.ExecuteNonQuery();
                }
            }

            AfiseazaAlbume();
            AfiseazaRelatii();
        }

        // ---------- Relații ----------
        private void BtnCreareRelatie_Click(object sender, EventArgs e)
        {
            if (listBoxMuzicieni.SelectedItem == null || listBoxAlbume.SelectedItem == null)
            {
                MessageBox.Show("Selectează un muzician și un album!");
                return;
            }

            var partsMuzician = listBoxMuzicieni.SelectedItem.ToString().Split('|');
            string muzicianId = partsMuzician[0].Trim();
            var partsAlbum = listBoxAlbume.SelectedItem.ToString().Split('|');
            string albumId = partsAlbum[0].Trim();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Albums_Musicians (id_muzician, id_album) VALUES (@muzician, @album)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@muzician", muzicianId);
                    cmd.Parameters.AddWithValue("@album", albumId);
                    cmd.ExecuteNonQuery();
                }
            }

            AfiseazaRelatii();
        }

        private void BtnStergeRelatie_Click(object sender, EventArgs e)
        {
            if (listBoxRelatii.SelectedItem == null)
            {
                MessageBox.Show("Selectează o relație!");
                return;
            }

            var parts = listBoxRelatii.SelectedItem.ToString().Split('|');
            string muzicianNume = parts[0].Trim();
            string albumTitlu = parts[1].Trim();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    DELETE AM 
                    FROM Albums_Musicians AM
                    JOIN Musicians M ON AM.id_muzician = M.id
                    JOIN Albums A ON AM.id_album = A.id
                    WHERE M.nume = @nume AND A.titlu = @titlu";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nume", muzicianNume);
                    cmd.Parameters.AddWithValue("@titlu", albumTitlu);
                    cmd.ExecuteNonQuery();
                }
            }

            AfiseazaRelatii();
        }

        private void AfiseazaRelatii()
        {
            listBoxRelatii.Items.Clear();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT M.nume, A.titlu 
                               FROM Albums_Musicians AM
                               JOIN Musicians M ON AM.id_muzician = M.id
                               JOIN Albums A ON AM.id_album = A.id";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string muzician = reader.GetString("nume");
                        string album = reader.GetString("titlu");
                        listBoxRelatii.Items.Add($"{muzician} | {album}");
                    }
                }
            }
        }

        private void AfiseazaMuzicieni()
        {
            listBoxMuzicieni.Items.Clear();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Musicians";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string nume = reader.GetString("nume");
                        string gen = reader.GetString("gen_muzical");
                        listBoxMuzicieni.Items.Add($"{id} | {nume} | {gen}");
                    }
                }
            }
        }

        private void AfiseazaAlbume()
        {
            listBoxAlbume.Items.Clear();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Albums";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string titlu = reader.GetString("titlu");
                        int an = reader.GetInt32("an_lansare");
                        listBoxAlbume.Items.Add($"{id} | {titlu} | {an}");
                    }
                }
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
