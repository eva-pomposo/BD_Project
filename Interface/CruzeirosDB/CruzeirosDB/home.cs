using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace CruzeirosDB
{

    public partial class home : Form
    {
        private SqlConnection cn;
        private int currentBarco;
        private int currentTipoBarcoBC;
        private int currentTipoBarcoBL;
        private int currentCruzeiro;
        private int currentCliente;
        private int currentClienteReserva;
        private int currentTripulante;
        private int currentCruzeiroReserva;
        private int currentReserva;
        private int currentCaisCriarCruzeiro;
        private int currentCaisCriarCruzeiroDesembarque;
        private bool adding;
        private Dictionary<int, Cruzeiro> cruzeiros;
        private int currentCaisEmbarqueCruzeiro;
        private int currentCaisDesembarqueCruzeiro;

        public home()
        {
            InitializeComponent();
        }



        private void home_Load(object sender, EventArgs e)
        {
            //cn = getSGBDConnection();
            //verifySGBDConnection();

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_Barco", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBoxBarcos.Items.Clear();

            while (reader.Read())
            {
                Barco B = new Barco();
                B.CodigoBarco = reader["codigoBarco"].ToString();
                B.NumTotalBilhetes = reader["numTotalBilhetes"].ToString();
                B.NomeBarco = reader["nomeBarco"].ToString();
                B.NomeTipoBarco = reader["C_TipoBarco_nomeTipoBarco"].ToString();
                listBoxBarcos.Items.Add(B);
            }

            cn.Close();

            currentBarco = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_TipoBarco", cn);
            reader = cmd.ExecuteReader();
            BC_tipoBarco.Items.Clear();

            while (reader.Read())
            {
                TipoBarco B = new TipoBarco();
                B.NomeTipoBarco = reader["nomeTipoBarco"].ToString();
                B.MaxBilhetes = reader["maxBilhetes"].ToString();
                B.Classificacao = reader["classificacao"].ToString();
                BC_tipoBarco.Items.Add(B);
            }

            cn.Close();

            currentTipoBarcoBC = 0;


            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCais", cn);
            reader = cmd.ExecuteReader();
            CRUC_caisEmbarque.Items.Clear();
            CRUL_caisEmbarques.Items.Clear();

            while (reader.Read())
            {
                CRUC_caisEmbarque.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
                CRUL_caisEmbarques.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
            }

            cn.Close();

            currentCaisCriarCruzeiro = 0;
            currentCaisEmbarqueCruzeiro = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCais", cn);
            reader = cmd.ExecuteReader();
            CRUC_caisDesembarque.Items.Clear();
            CRUL_caisDesembarques.Items.Clear();

            while (reader.Read())
            {
                CRUC_caisDesembarque.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
                CRUL_caisDesembarques.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
            }

            cn.Close();

            currentCaisCriarCruzeiroDesembarque = 0;
            currentCaisDesembarqueCruzeiro = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCruzeiro", cn);
            reader = cmd.ExecuteReader();
            CRU_list.Items.Clear();
            RC_cruzeiro.Items.Clear();
            cruzeiros = new Dictionary<int, Cruzeiro>();
            while (reader.Read())
            {
                Cruzeiro C = new Cruzeiro();
                C.NumCruzeiro = reader["numCruzeiro"].ToString();
                C.DataEmbarque = reader["dataEmbarque"].ToString().Substring(0, 10);
                C.DataDesembarque = reader["dataDesembarque"].ToString().Substring(0, 10);
                C.Vagas = reader["vagas"].ToString();
                C.codigoBarco = reader["C_Barco_codigoBarco"].ToString();
                C.HoraPartida = reader["horaPartida"].ToString();
                C.HoraChegada = reader["horaChegada"].ToString();
                C.Localidadepartida = reader["localidadepartida"].ToString();
                C.Nomepartida = reader["nomepartida"].ToString();
                C.LocalidadeChegada = reader["localidadeChegada"].ToString();
                C.NomeChegada = reader["nomeChegada"].ToString();
                C.IdChegada = reader["idChegada"].ToString();
                C.IdPartida = reader["idPartida"].ToString();
                cruzeiros.Add(int.Parse(C.NumCruzeiro), C);
                CRU_list.Items.Add(C);
                RC_cruzeiro.Items.Add(C);
            }

            cn.Close();

            currentCruzeiro = 0;
            currentCruzeiroReserva = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;
            
            cmd = new SqlCommand("EXEC CruzeirosDB.Cliente", cn);

            reader = cmd.ExecuteReader();
            CL_listBoxClientes.Items.Clear();
            RC_nome.Items.Clear();

            while (reader.Read())
            {
                Cliente C = new Cliente();
                C.C_Pessoa_numCC = reader["c_Pessoa_numCC"].ToString();
                C.NumCliente = reader["numCliente"].ToString();
                C.NumTelemovel = reader["numTelemovel"].ToString();
                C.Nome = reader["nome"].ToString();
                C.Email = reader["email"].ToString();
                CL_listBoxClientes.Items.Add(C);
                RC_nome.Items.Add(C);

            }

            cn.Close();

            currentCliente = 0;
            currentClienteReserva = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("EXEC CruzeirosDB.Tripulante", cn);
            reader = cmd.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                Tripulante C = new Tripulante();
                C.C_Pessoa_numCC = reader["c_Pessoa_numCC"].ToString();
                C.NumTripulante = reader["numTripulante"].ToString();
                C.NumTelemovel = reader["numTelemovel"].ToString();
                C.Nome = reader["nome"].ToString();
                C.Email = reader["email"].ToString();
                listBox1.Items.Add(C);
            }

            cn.Close();

            currentTripulante = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_Reserva", cn);
            reader = cmd.ExecuteReader();
            RL_list.Items.Clear();

            while (reader.Read())
            {
                Reserva R = new Reserva();
                R.NomeCliente = reader["nomeCliente"].ToString();
                R.NumBilhete = reader["numBilhete"].ToString();
                R.DataCamp = reader["C_Data"].ToString().Substring(0, 10);
                R.NumCruzeiro = reader["C_Cruzeiro_numCruzeiro"].ToString();
                R.ClienteNumCC = reader["C_Cliente_C_Pessoa_numCC"].ToString();
                RL_list.Items.Add(R);
            }

            cn.Close();

            currentReserva = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_TipoBarco", cn);
            reader = cmd.ExecuteReader();
            BL_tipoBarco.Items.Clear();

            while (reader.Read())
            {
                TipoBarco B = new TipoBarco();
                B.NomeTipoBarco = reader["nomeTipoBarco"].ToString();
                B.MaxBilhetes = reader["maxBilhetes"].ToString();
                B.Classificacao = reader["classificacao"].ToString();
                BL_tipoBarco.Items.Add(B);
            }

            cn.Close();

            currentTipoBarcoBL = 0;
        }

        // SQL methods 
        private SqlConnection getSGBDConnection()
        {
            // Constructs a new SqlConnection 
            return new SqlConnection("data source=tcp:mednat.ieeta.pt\\SQLSERVER,8101; Initial Catalog = p5g7; uid = p5g7; password = 12345eva!MARTA");
        }
        private bool verifySGBDConnection()
        {
            // Opens connection to the DB 
            if (cn == null)
                cn = getSGBDConnection();

            if (cn != null)
            {
                try
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }
                }
                catch (SqlException e)
                {
                    return false;
                }

                return cn.State == ConnectionState.Open;
            }

            return false;
        }

        private void BL_tipoBarco_Click(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_TipoBarco", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            BL_tipoBarco.Items.Clear();

            while (reader.Read())
            {
                TipoBarco B = new TipoBarco();
                B.NomeTipoBarco = reader["nomeTipoBarco"].ToString();
                B.MaxBilhetes = reader["maxBilhetes"].ToString();
                B.Classificacao = reader["classificacao"].ToString();
                BL_tipoBarco.Items.Add(B);
            }

            cn.Close();

            currentTipoBarcoBL = 0;
        }

        private void BC_tipoBarco_Click(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_TipoBarco", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            BC_tipoBarco.Items.Clear();

            while (reader.Read())
            {
                TipoBarco B = new TipoBarco();
                B.NomeTipoBarco = reader["nomeTipoBarco"].ToString();
                B.MaxBilhetes = reader["maxBilhetes"].ToString();
                B.Classificacao = reader["classificacao"].ToString();
                BC_tipoBarco.Items.Add(B);
            }

            cn.Close();

            currentTipoBarcoBC = 0;
        }

        private void listBoxBarcos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxBarcos.SelectedIndex >= 0)
            {
                currentBarco = listBoxBarcos.SelectedIndex;
                ShowBarco();
            }
        }

        private void RL_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RL_list.SelectedIndex >= 0)
            {
                currentReserva  = RL_list.SelectedIndex;
                ShowReserva();
            }

        }

        private void BC_tipoBarco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BC_tipoBarco.SelectedIndex >= 0)
            {
                currentTipoBarcoBC = BC_tipoBarco.SelectedIndex;
            }
        }

        private void BL_tipoBarco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BL_tipoBarco.SelectedIndex >= 0)
            {
                currentTipoBarcoBL = BL_tipoBarco.SelectedIndex;
            }
        }

        private void listaBoxCruzeiros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaBoxCruzeiros.SelectedIndex >= 0)
            {
                currentCruzeiro = listaBoxCruzeiros.SelectedIndex;
            }
        }

        private void CRU_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CRU_list.SelectedIndex >= 0)
            {
                currentCruzeiro = CRU_list.SelectedIndex;
                ShowCruzeiro();
            }
        }

        private void CL_listBoxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CL_listBoxClientes.SelectedIndex >= 0)
            {
                currentCliente = CL_listBoxClientes.SelectedIndex;
            }
        }

        private void RC_cruzeiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RC_cruzeiro.SelectedIndex >= 0)
            {
                currentCruzeiroReserva = RC_cruzeiro.SelectedIndex;
            }

        }

        private void RC_nome_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (RC_nome.SelectedIndex >= 0)
            {
                currentClienteReserva = RC_nome.SelectedIndex;
            }
        }


        private void TL_listaBoxTripulantes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabBarcosLista_Click(object sender, EventArgs e)
        {
            RL_campoNomeCliente.Text = "";
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_Barco", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBoxBarcos.Items.Clear();

            while (reader.Read())
            {
                Barco B = new Barco();
                B.CodigoBarco = reader["codigoBarco"].ToString();
                B.NumTotalBilhetes = reader["numTotalBilhetes"].ToString();
                B.NomeBarco = reader["nomeBarco"].ToString();
                B.NomeTipoBarco = reader["C_TipoBarco_nomeTipoBarco"].ToString();
                listBoxBarcos.Items.Add(B);
            }

            cn.Close();

            currentBarco = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCruzeiro", cn);
            reader = cmd.ExecuteReader();
            CRU_list.Items.Clear();
            RC_cruzeiro.Items.Clear();
            cruzeiros = new Dictionary<int, Cruzeiro>();

            while (reader.Read())
            {
                Cruzeiro C = new Cruzeiro();
                C.NumCruzeiro = reader["numCruzeiro"].ToString();
                C.DataEmbarque = reader["dataEmbarque"].ToString().Substring(0, 10);
                C.DataDesembarque = reader["dataDesembarque"].ToString().Substring(0, 10);
                C.Vagas = reader["vagas"].ToString();
                C.codigoBarco = reader["C_Barco_codigoBarco"].ToString();
                C.HoraPartida = reader["horaPartida"].ToString();
                C.HoraChegada = reader["horaChegada"].ToString();
                C.Localidadepartida = reader["localidadepartida"].ToString();
                C.Nomepartida = reader["nomepartida"].ToString();
                C.LocalidadeChegada = reader["localidadeChegada"].ToString();
                C.NomeChegada = reader["nomeChegada"].ToString();
                cruzeiros.Add(int.Parse(C.NumCruzeiro), C);
                CRU_list.Items.Add(C);
                RC_cruzeiro.Items.Add(C);
            }

            cn.Close();

            currentCruzeiro = 0;
            currentCruzeiroReserva = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("EXEC CruzeirosDB.Cliente", cn);
            reader = cmd.ExecuteReader();
            RC_nome.Items.Clear();

            while (reader.Read())
            {
                Cliente C = new Cliente();
                C.C_Pessoa_numCC = reader["c_Pessoa_numCC"].ToString();
                C.NumCliente = reader["numCliente"].ToString();
                C.NumTelemovel = reader["numTelemovel"].ToString();
                C.Nome = reader["nome"].ToString();
                C.Email = reader["email"].ToString();
                CL_listBoxClientes.Items.Add(C);
                RC_nome.Items.Add(C);

            }

            cn.Close();

            currentCliente = 0;
            currentClienteReserva = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("EXEC CruzeirosDB.Tripulante", cn);
            reader = cmd.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                Tripulante C = new Tripulante();
                C.C_Pessoa_numCC = reader["c_Pessoa_numCC"].ToString();
                C.NumTripulante = reader["numTripulante"].ToString();
                C.NumTelemovel = reader["numTelemovel"].ToString();
                C.Nome = reader["nome"].ToString();
                C.Email = reader["email"].ToString();
                listBox1.Items.Add(C);
            }

            cn.Close();

            currentTripulante = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_Reserva", cn);
            reader = cmd.ExecuteReader();
            RL_list.Items.Clear();

            while (reader.Read())
            {
                Reserva R = new Reserva();
                R.NomeCliente = reader["nomeCliente"].ToString();
                R.NumBilhete = reader["numBilhete"].ToString();
                R.DataCamp = reader["C_Data"].ToString().Substring(0, 10);
                R.NumCruzeiro = reader["C_Cruzeiro_numCruzeiro"].ToString();
                R.ClienteNumCC = reader["C_Cliente_C_Pessoa_numCC"].ToString();
                RL_list.Items.Add(R);
            }

            cn.Close();

            currentReserva = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCais", cn);
            reader = cmd.ExecuteReader();
            CRUC_caisEmbarque.Items.Clear();
            CRUL_caisEmbarques.Items.Clear();

            while (reader.Read())
            {
                CRUC_caisEmbarque.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
                CRUL_caisEmbarques.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
            }

            cn.Close();

            currentCaisCriarCruzeiro = 0;
            currentCaisEmbarqueCruzeiro = 0;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCais", cn);
            reader = cmd.ExecuteReader();
            CRUC_caisDesembarque.Items.Clear();
            CRUL_caisDesembarques.Items.Clear();

            while (reader.Read())
            {
                CRUC_caisDesembarque.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
                CRUL_caisDesembarques.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
            }

            cn.Close();

            currentCaisCriarCruzeiroDesembarque = 0;
            currentCaisDesembarqueCruzeiro = 0;
        }

        private void SubmitBarco(Barco C)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT CruzeirosDB.C_Barco (codigoBarco, numTotalBilhetes, nomeBarco, C_TipoBarco_nomeTipoBarco) " + "VALUES (@codigoBarco, @numTotalBilhetes, @nomeBarco, @C_TipoBarco_nomeTipoBarco) ";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@codigoBarco", C.CodigoBarco);
            cmd.Parameters.AddWithValue("@numTotalBilhetes", C.NumTotalBilhetes);
            cmd.Parameters.AddWithValue("@nomeBarco", C.NomeBarco);
            cmd.Parameters.AddWithValue("@C_TipoBarco_nomeTipoBarco", C.NomeTipoBarco);
            cmd.Connection = cn;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update barco in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void UpdateBarco(Barco C)
        {
            int rows = 0;

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE CruzeirosDB.C_Barco " + "SET C_TipoBarco_nomeTipoBarco = @C_TipoBarco_nomeTipoBarco, " + "    numTotalBilhetes = @numTotalBilhetes, " + "    nomeBarco = @nomeBarco WHERE codigoBarco = @codigoBarco";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@codigoBarco", C.CodigoBarco);
            cmd.Parameters.AddWithValue("@numTotalBilhetes", C.NumTotalBilhetes);
            cmd.Parameters.AddWithValue("@nomeBarco", C.NomeBarco);
            cmd.Parameters.AddWithValue("@C_TipoBarco_nomeTipoBarco", C.NomeTipoBarco);
            cmd.Connection = cn;

            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                if (rows == 1)
                    MessageBox.Show("Update OK");
                else
                    MessageBox.Show("Update NOT OK");

                cn.Close();
            }
        }

        private void RemoveBarco(string codigo)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "EXEC CruzeirosDB.DeleteBarco @codigoBarco ";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@codigoBarco", codigo);
            cmd.Connection = cn;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private bool SaveBarco()
        {
            Barco barco = new Barco();

            if (adding)
            {
                try
                {
                    barco.CodigoBarco = barco.nextCodigo().ToString();
                    barco.NumTotalBilhetes = BC_numTotalBilhetes.Text;
                    barco.NomeBarco = BC_nomebarco.Text;
                    barco.NomeTipoBarco = BC_tipoBarco.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

                SubmitBarco(barco);
                listBoxBarcos.Items.Add(barco);
            }
            else
            {
                try
                {
                    barco.CodigoBarco = BL_codigoBarco.Text;
                    barco.NomeBarco = BL_nomeBarco.Text;
                    barco.NomeTipoBarco = BL_tipoBarco.Text;
                    barco.NumTotalBilhetes = BL_bilhetes.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

                UpdateBarco(barco);
                listBoxBarcos.Items[currentBarco] = barco;
            }
            return true;
        }

        public void ShowBarco()
        {
            if (listBoxBarcos.Items.Count == 0 | currentBarco < 0)
                return;
            Barco barco = new Barco();
            barco = (Barco)listBoxBarcos.Items[currentBarco];

            BL_codigoBarco.Text = barco.CodigoBarco;
            BL_nomeBarco.Text = barco.NomeBarco;
            BL_tipoBarco.Text = barco.NomeTipoBarco;
            BL_bilhetes.Text = barco.NumTotalBilhetes;

        }

        public void ShowReserva()
        {
            if (RL_list.Items.Count == 0 | currentReserva < 0)
                return;
            Reserva reserva = new Reserva();
            reserva = (Reserva)RL_list.Items[currentReserva];

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("EXEC CruzeirosDB.showCruzeirodaReserva " + reserva.NumCruzeiro, cn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cruzeiro C = new Cruzeiro();
                C.NumCruzeiro = reader["numCruzeiro"].ToString();
                C.DataEmbarque = reader["dataEmbarque"].ToString().Substring(0, 10);
                C.DataDesembarque = reader["dataDesembarque"].ToString().Substring(0, 10);
                C.Vagas = reader["vagas"].ToString();
                C.codigoBarco = reader["C_Barco_codigoBarco"].ToString();
                C.HoraPartida = reader["horaPartida"].ToString();
                C.HoraChegada = reader["horaChegada"].ToString();
                C.Localidadepartida = reader["localidadepartida"].ToString();
                C.Nomepartida = reader["nomepartida"].ToString();
                C.LocalidadeChegada = reader["localidadeChegada"].ToString();
                C.NomeChegada = reader["nomeChegada"].ToString();
                RL_cruzeiro.Text = C.ToString();
            }

            cn.Close();

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            cmd = new SqlCommand("EXEC CruzeirosDB.showClientedaReserva " + reserva.ClienteNumCC, cn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cliente C = new Cliente();
                C.C_Pessoa_numCC = reader["c_Pessoa_numCC"].ToString();
                C.NumCliente = reader["numCliente"].ToString();
                C.NumTelemovel = reader["numTelemovel"].ToString();
                C.Nome = reader["nome"].ToString();
                C.Email = reader["email"].ToString();
                RL_nomeCliente.Text = C.ToString();
            }
            cn.Close();


        }


        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Barcos_Click(object sender, EventArgs e)
        {

        }

        private void tabControl5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void tabClientesCriar_Click(object sender, EventArgs e)
        {

        }

        private void tabTripulanteCriar_Click(object sender, EventArgs e)
        {

        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void listBoxCruzeiros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BC_nomebarco_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabBarcosLista_Click_1(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void AdicionarBarco_Click(object sender, EventArgs e)
        {
            adding = true;
            if (BC_numTotalBilhetes.Text.Length == 0)
            {
                MessageBox.Show("Insira o número total de bilhetes!");
            }
            else if (BC_nomebarco.Text.Length == 0)
            {
                MessageBox.Show("Insira o nome do Barco!");
            }
            else if (BC_tipoBarco.Text.Length == 0)
            {
                MessageBox.Show("Insira o tipo de Barco!");
            }
            else
            {
                string message = "Do you want to add this Barco?";
                string title = "Close Window";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveBarco();
                }
            }
        }

        private void BL_edite_Click(object sender, EventArgs e)
        {
            currentBarco = listBoxBarcos.SelectedIndex;
            if (currentBarco < 0)
            {
                MessageBox.Show("Please select a barco to edit");
                return;
            }
            adding = false;

            string message = "Do you want to edit this Barco?\n" + BL_codigoBarco.Text + "  " + BL_nomeBarco.Text + "  " + BL_tipoBarco.Text + "  " + BL_bilhetes.Text;
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    SaveBarco();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }


        private void BL_delete_Click(object sender, EventArgs e)
        {
            if (listBoxBarcos.SelectedIndex > -1)
            {

                string message = "Do you want to delete this Barco?\n" + ((Barco)listBoxBarcos.SelectedItem);
                string title = "Close Window";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        RemoveBarco(((Barco)listBoxBarcos.SelectedItem).CodigoBarco);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    listBoxBarcos.Items.RemoveAt(listBoxBarcos.SelectedIndex);
                    if (currentBarco == listBoxBarcos.Items.Count)
                        currentBarco = listBoxBarcos.Items.Count - 1;
                    if (currentBarco == -1)
                    {
                        BL_codigoBarco.Text = "";
                        BL_nomeBarco.Text = "";
                        BL_tipoBarco.Text = "";
                        BL_bilhetes.Text = "";
                        MessageBox.Show("There are no more Barcos");
                    }
                    else
                    {
                        ShowBarco();
                    }
                }


            }
        }

        private void Reserva_Click(object sender, EventArgs e)
        {

        }

        private void tabCruzeirosLista_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void tabBarcosLista_Click_2(object sender, EventArgs e)
        {

        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown14_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabReservaCriar_Click(object sender, EventArgs e)
        {

        }

        private void label93_Click(object sender, EventArgs e)
        {

        }

        private void RL_campoNomeCliente_TextChanged(object sender, EventArgs e)
        {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd;
                if (RL_campoNomeCliente.Text == "")
                {
                    cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_Reserva", cn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_Reserva where nomeCliente like '" + RL_campoNomeCliente.Text.ToString() + "%'", cn);
                }

                SqlDataReader reader = cmd.ExecuteReader();
                RL_list.Items.Clear();

                while (reader.Read())
                {
                    Reserva R = new Reserva();
                    R.NomeCliente = reader["nomeCliente"].ToString();
                    R.NumBilhete = reader["numBilhete"].ToString();
                    R.DataCamp = reader["C_Data"].ToString().Substring(0, 10);
                    R.NumCruzeiro = reader["C_Cruzeiro_numCruzeiro"].ToString();
                    R.ClienteNumCC = reader["C_Cliente_C_Pessoa_numCC"].ToString();
                    RL_list.Items.Add(R);
                }

                cn.Close();

                currentReserva = 0;
                RL_cruzeiro.Text = "";
                RL_nomeCliente.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM CruzeirosDB.C_Reserva", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            RL_list.Items.Clear();

            while (reader.Read())
            {
                Reserva R = new Reserva();
                R.NomeCliente = reader["nomeCliente"].ToString();
                R.NumBilhete = reader["numBilhete"].ToString();
                R.DataCamp = reader["C_Data"].ToString().Substring(0, 10);
                R.NumCruzeiro = reader["C_Cruzeiro_numCruzeiro"].ToString();
                R.ClienteNumCC = reader["C_Cliente_C_Pessoa_numCC"].ToString();
                RL_list.Items.Add(R);
            }

            cn.Close();

            currentReserva = 0;
            RL_cruzeiro.Text = "";
            RL_nomeCliente.Text = "";
            RL_campoNomeCliente.Text = "";
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown17_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label116_Click(object sender, EventArgs e)
        {

        }

        private void RC_criarReserva_Click(object sender, EventArgs e)
        {
            if (RC_nome.Items.Count == 0 | RC_cruzeiro.Items.Count == 0 | currentClienteReserva < 0 | currentCruzeiroReserva < 0)
                return;
            Cliente cliente = new Cliente();
            cliente = (Cliente)RC_nome.Items[currentClienteReserva];
            Cruzeiro cruzeiro = new Cruzeiro();
            cruzeiro = (Cruzeiro)RC_cruzeiro.Items[currentCruzeiroReserva];

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "EXEC CruzeirosDB.criarReserva @nomeCliente, @numCCcliente, @numCruzeiro";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@nomeCliente", cliente.Nome);
            cmd.Parameters.AddWithValue("@numCCcliente", cliente.C_Pessoa_numCC);
            cmd.Parameters.AddWithValue("@numCruzeiro", cruzeiro.NumCruzeiro);
            cmd.Connection = cn;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Reserva in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                MessageBox.Show("Update OK");
                cn.Close();
            }
        }

        private void CC_add_Click(object sender, EventArgs e)
        {
            if (CC_numcc.Text == "" || CC_nome.Text == "" || CC_numtel.Text == "" || CC_email.Text == "")
            {
                MessageBox.Show("Insira os campos todos para adicionar Cliente!");
            }
            else
            {
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "EXEC CruzeirosDB.AddCliente  @numCC, @email, @nome, @numTelemovel";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@numCC", CC_numcc.Text);
                cmd.Parameters.AddWithValue("@email", CC_email.Text);
                cmd.Parameters.AddWithValue("@nome", CC_nome.Text);
                cmd.Parameters.AddWithValue("@numTelemovel", CC_numtel.Text);
                cmd.Connection = cn;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to add Cliente in database. \n ERROR MESSAGE: \n" + ex.Message);
                }
                finally
                {
                    MessageBox.Show("Update OK");
                    cn.Close();
                }
            }
        }

        private void CC_numcc_TextChanged(object sender, EventArgs e)
        {

        }

        private void TC_criarTripulante_Click(object sender, EventArgs e)
        {
            if (TC_numCC.Text == "" || TC_tripulante.Text == "" || TC_numTelemovel.Text == "" || TC_email.Text == "")
            {
                MessageBox.Show("Insira os campos todos para adicionar Cliente!");
            }
            else
            {
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "EXEC CruzeirosDB.AddTripulante  @numCC, @email, @nome, @numTelemovel";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@numCC",TC_numCC.Text);
                cmd.Parameters.AddWithValue("@email", TC_email.Text);
                cmd.Parameters.AddWithValue("@nome", TC_tripulante.Text);
                cmd.Parameters.AddWithValue("@numTelemovel", TC_numTelemovel.Text);
                cmd.Connection = cn;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to add Cliente in database. \n ERROR MESSAGE: \n" + ex.Message);
                }
                finally
                {
                    MessageBox.Show("Update OK");
                    cn.Close();
                }
            }

        }

        private void TC_numCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void CRUC_criar_Click(object sender, EventArgs e)
        {   
            if (CRUC_codigoBraco.Text == "" || CRUC_caisEmbarque.Text == "" || CRUC_dataEmbarque.Text == "" || CRUC_dataDesembarque.Text == "")
            {
                MessageBox.Show("Insira os campos todos para adicionar Cruzeiro!");
            }
            else
            {
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();
                List<string> myList = new List<string>() { "janeiro", "fevereiro", "março", "abril", "maio", "junho", "julho", "agosto", "setembro", "outobro", "novembro", "dezembro" };
                cmd.CommandText = "EXEC CruzeirosDB.criarCruzeiro @codigoBarco, @dataEmbarque, @dataDesembarque, @horaEmbarque, @horaDesembarque, @codigoCaisChegada, @codigoCaisPartida";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@codigoBarco", CRUC_codigoBraco.Text).Value.ToString();
                cmd.Parameters.AddWithValue("@dataEmbarque", CRUC_dataEmbarque.Text.Split(' ')[4] + "-" + (myList.FindIndex(a => a.Contains(CRUC_dataEmbarque.Text.Split(' ')[2])) + 1).ToString() + "-" + CRUC_dataEmbarque.Text.Split(' ')[0]).Value.ToString();
                cmd.Parameters.AddWithValue("@dataDesembarque", CRUC_dataDesembarque.Text.Split(' ')[4] + "-" + (myList.FindIndex(a => a.Contains(CRUC_dataDesembarque.Text.Split(' ')[2])) + 1).ToString() + "-" + CRUC_dataDesembarque.Text.Split(' ')[0]).Value.ToString();
                cmd.Parameters.AddWithValue("@horaEmbarque", CRUC_EH.Text + ":" + CRUC_EM.Text + ":" + CRUC_ES.Text).Value.ToString();
                cmd.Parameters.AddWithValue("@horaDesembarque", CRUC_DH.Text + ":" + CRUC_DM.Text + ":" + CRUC_DS.Text).Value.ToString();
                cmd.Parameters.AddWithValue("@codigoCaisChegada", CRUC_caisDesembarque.Text.Split(' ')[0]).Value.ToString();
                cmd.Parameters.AddWithValue("@codigoCaisPartida", CRUC_caisEmbarque.Text.Split(' ')[0]).Value.ToString();
                cmd.Connection = cn;


                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to add Cliente in database. \n ERROR MESSAGE: \n" + ex.Message);
                }
                finally
                {
                    MessageBox.Show("Update OK");
                    cn.Close();
                }
            }
        }

        private void label112_Click(object sender, EventArgs e)
        {

        }

        private void label107_Click(object sender, EventArgs e)
        {

        }

        private void CRUC_caisEmbarque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CRUC_caisEmbarque.SelectedIndex >= 0)
            {
                currentCaisCriarCruzeiro = CRUC_caisEmbarque.SelectedIndex;
            }
        }


        private void CRUL_caisEmbarques_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CRUL_caisEmbarques.SelectedIndex >= 0)
            {
                currentCaisEmbarqueCruzeiro = CRUL_caisEmbarques.SelectedIndex;
            }
        }

        private void CRUC_caisEmbarque_Click(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCais", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            CRUC_caisEmbarque.Items.Clear();

            while (reader.Read())
            {
                CRUC_caisEmbarque.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
            }

            cn.Close();

            currentCaisCriarCruzeiro = 0;
        }

        private void CRUC_caisDesembarque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CRUC_caisDesembarque.SelectedIndex >= 0)
            {
                currentCaisCriarCruzeiroDesembarque = CRUC_caisDesembarque.SelectedIndex;
            }
        }

        private void CRUL_caisDesembarques_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CRUL_caisDesembarques.SelectedIndex >= 0)
            {
                currentCaisDesembarqueCruzeiro = CRUL_caisDesembarques.SelectedIndex;
            }
        }

        /*
        private void CRUC_caisDesembarque_Click(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCais", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            CRUC_caisDesembarque.Items.Clear();

            while (reader.Read())
            {
                CRUC_caisDesembarque.Items.Add(reader["codigo"].ToString() + "      " + reader["localidade"].ToString() + "                                " + reader["nome"].ToString());
            }

            cn.Close();

            currentCaisCriarCruzeiroDesembarque = 0;
        }*/

        private void tabCruzeiro_Cruzeiro(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM CruzeirosDB.listCruzeiro", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            CRU_list.Items.Clear();
            RC_cruzeiro.Items.Clear();
            cruzeiros = new Dictionary<int, Cruzeiro>();



            while (reader.Read())
            {
                Cruzeiro C = new Cruzeiro();
                C.NumCruzeiro = reader["numCruzeiro"].ToString();
                C.DataEmbarque = reader["dataEmbarque"].ToString().Substring(0, 10);
                C.DataDesembarque = reader["dataDesembarque"].ToString().Substring(0, 10);
                C.Vagas = reader["vagas"].ToString();
                C.codigoBarco = reader["C_Barco_codigoBarco"].ToString();
                C.HoraPartida = reader["horaPartida"].ToString();
                C.HoraChegada = reader["horaChegada"].ToString();
                C.Localidadepartida = reader["localidadepartida"].ToString();
                C.Nomepartida = reader["nomepartida"].ToString();
                C.LocalidadeChegada = reader["localidadeChegada"].ToString();
                C.NomeChegada = reader["nomeChegada"].ToString();
                cruzeiros.Add(int.Parse(C.NumCruzeiro), C);
                CRU_list.Items.Add(C);
                RC_cruzeiro.Items.Add(C);
            }

            cn.Close();

            currentCruzeiro = 0;
            currentCruzeiroReserva = 0;
        }

        private void CRUL_CodigoBarcoN_TextChanged(object sender, EventArgs e)
        {

        }

        public void ShowCruzeiro()
        {
            if (CRU_list.Items.Count == 0 | currentCruzeiro < 0)
                return;
            Cruzeiro cruzeiro = new Cruzeiro();
            cruzeiro = (Cruzeiro)CRU_list.Items[currentCruzeiro];
            int numeroCruzeiro = int.Parse(cruzeiro.NumCruzeiro);
            cruzeiro = cruzeiros[numeroCruzeiro];

            CRUL_CodigoBarcoN.Text = cruzeiro.codigoBarco;
            CRUL_numC.Text = cruzeiro.NumCruzeiro;
            CRUL_vagas.Text = cruzeiro.Vagas;
            CRUL_embarque.Text = cruzeiro.DataEmbarque;
            CRUL_desembarque.Text = cruzeiro.DataDesembarque;
            CRUL_caisEmbarques.Text = cruzeiro.IdPartida + "      " + cruzeiro.Localidadepartida + "                                " + cruzeiro.Nomepartida;
            CRUL_caisDesembarques.Text = cruzeiro.IdChegada + "      " + cruzeiro.LocalidadeChegada + "                                " + cruzeiro.NomeChegada;
            String[] horaPartida = cruzeiro.HoraPartida.Split(':');
            String[] horaChegada = cruzeiro.HoraChegada.Split(':');
            CRUL_EmbHoras.Value = int.Parse(horaPartida[0]);
            CRUL_EmbMin.Value = int.Parse(horaPartida[1]);
            CRUL_EmSeg.Value = int.Parse(horaPartida[2]);
            CRUL_DesHoras.Value = int.Parse( horaChegada[0]);
            CRUL_DesMin.Value = int.Parse(horaChegada[1]);
            CRUL_DesSeg.Value = int.Parse( horaChegada[2]);
        }

        private void CRUL_numC_TextChanged(object sender, EventArgs e)
        {

        }

        private void CRUL_editN_Click(object sender, EventArgs e)
        {
            currentCruzeiro = CRU_list.SelectedIndex;
            if (currentCruzeiro < 0)
            {
                MessageBox.Show("Please select a cruzeiro to edit");
                return;
            }

            if (CRUL_CodigoBarcoN.Text == "" || CRUL_embarque.Text == "" || CRUL_desembarque.Text == "")
            {
                MessageBox.Show("Insira os campos todos para editar Cruzeiro!");
            }
            else
            {
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();
                List<string> myList = new List<string>() { "janeiro", "fevereiro", "março", "abril", "maio", "junho", "julho", "agosto", "setembro", "outobro", "novembro", "dezembro" };
                cmd.CommandText = "EXEC CruzeirosDB.editCruzeiro @numeroCruzeiro, @vagas, @codigoBarco, @dataEmbarque, @dataDesembarque, @horaEmbarque, @horaDesembarque, @codigoCaisChegada, @codigoCaisPartida";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@numeroCruzeiro", CRUL_numC.Text).Value.ToString();
                cmd.Parameters.AddWithValue("@vagas", CRUL_vagas.Text).Value.ToString();
                cmd.Parameters.AddWithValue("@codigoBarco", CRUL_CodigoBarcoN.Text).Value.ToString();
                cmd.Parameters.AddWithValue("@dataEmbarque", CRUL_embarque.Text.Split(' ')[4] + "-" + (myList.FindIndex(a => a.Contains(CRUL_embarque.Text.Split(' ')[2])) + 1).ToString() + "-" + CRUL_embarque.Text.Split(' ')[0]);
                cmd.Parameters.AddWithValue("@dataDesembarque", CRUL_desembarque.Text.Split(' ')[4] + "-" + (myList.FindIndex(a => a.Contains(CRUL_desembarque.Text.Split(' ')[2])) + 1).ToString() + "-" + CRUL_desembarque.Text.Split(' ')[0]);
                cmd.Parameters.AddWithValue("@horaEmbarque", CRUL_EmbHoras.Text + ":" + CRUL_EmbMin.Text + ":" + CRUL_EmSeg.Text).Value.ToString();
                cmd.Parameters.AddWithValue("@horaDesembarque", CRUL_DesHoras.Text + ":" + CRUL_DesMin.Text + ":" + CRUL_DesSeg.Text).Value.ToString();
                cmd.Parameters.AddWithValue("@codigoCaisChegada", CRUL_caisDesembarques.Text.Split(' ')[0]).Value.ToString();
                cmd.Parameters.AddWithValue("@codigoCaisPartida", CRUL_caisEmbarques.Text.Split(' ')[0]).Value.ToString();
                cmd.Connection = cn;


                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to edit Cruzeiro in database. \n ERROR MESSAGE: \n" + ex.Message);
                }
                finally
                {
                    MessageBox.Show("Update OK");
                    cn.Close();
                }
            }
        }

        private void CRUL_deleteN_Click(object sender, EventArgs e)
        {
            if (CRU_list.SelectedIndex > -1)
            {
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();
                List<string> myList = new List<string>() { "janeiro", "fevereiro", "março", "abril", "maio", "junho", "julho", "agosto", "setembro", "outobro", "novembro", "dezembro" };
                cmd.CommandText = "EXEC CruzeirosDB.DeleteCruzeiro @numeroCruzeiro";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@numeroCruzeiro", CRUL_numC.Text).Value.ToString();
                cmd.Connection = cn;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to delete Cruzeiro in database. \n ERROR MESSAGE: \n" + ex.Message);
                }
                finally
                {
                    MessageBox.Show("Update OK");
                    cn.Close();
                }

                CRU_list.Items.RemoveAt(CRU_list.SelectedIndex);
                if (currentCruzeiro == CRU_list.Items.Count)
                    currentCruzeiro = CRU_list.Items.Count - 1;
                if (currentCruzeiro == -1)
                {
                    CRUL_CodigoBarcoN.Text = "";
                    CRUL_numC.Text = "";
                    MessageBox.Show("There are no more Cruzeiros");
                }
                else
                {
                    ShowCruzeiro();
                }
            }
        }

        private void label101_Click(object sender, EventArgs e)
        {

        }
    }
}