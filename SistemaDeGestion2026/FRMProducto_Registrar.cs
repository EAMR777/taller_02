using AForge.Video.DirectShow;
using CapaRN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeGestion2026
{    
    public partial class FRMProducto_Registrar : DevComponents.DotNetBar.Office2007Form
    {
        #region Variables
        private bool lectorCBHabilitado = false;
        public aproduc producto = new aproduc();        
        private xnumcor correlativo = new xnumcor();
        public bool modificar = false;
        public String codProMod = "";
        public bool actualizar = false;

        //Variables para la camara
        private FilterInfoCollection CaptureDevice; // list of webcam
        private VideoCaptureDevice FinalFrame;
        private bool TieneFoto = false;
        #endregion

        #region Constructor
        public FRMProducto_Registrar()
        {
            InitializeComponent();
        }
        #endregion

        #region Métodos

        private void CargarComboCategorias()
        {
            List<acatpro> ListaCategorias = new List<acatpro>();
            acatpro categoria = new acatpro();
            ListaCategorias = categoria.Lista("cacpestcat = true order by cacpnomcat");
            CMBCategoria.Items.Clear();
            CMBCategoria.DisplayMember = "cacpnomcat";
            CMBCategoria.ValueMember = "pacpcodcat";
            CMBCategoria.DataSource = ListaCategorias;
            CMBCategoria.SelectedIndex = -1;

        }
        private void CargarCombo(String campo, ComboBox combo)
        {
            List<String> ListaNombresProducto = new List<String>();
            
            ListaNombresProducto = producto.Combo(campo);
            combo.Items.Clear();
            combo.DisplayMember = campo;            
            combo.DataSource = ListaNombresProducto;
            combo.SelectedIndex = -1;
           
        }

        #endregion

        #region Eventos


        private void BTNCodigoDeBarras_Click(object sender, EventArgs e)
        {
            if (!lectorCBHabilitado)
            {
                lectorCBHabilitado = true;
                LBLCodigoDeBarras.Text = "LECTOR ACTIVO";
                LBLCodigoDeBarras.BackColor = Color.PaleGreen;
            }
            else
            {
                if (LBLCodigoDeBarras.Text == "LECTOR ACTIVO")
                {
                    LBLCodigoDeBarras.Text = "SIN CÓDIGO";
                    LBLCodigoDeBarras.BackColor = Color.Salmon;
                }
                else
                {
                    LBLCodigoDeBarras.BackColor = Color.LightBlue;
                }
                lectorCBHabilitado = false;
                TXTModelo.Focus();
            }
        }

        private void BTNCodigoDeBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (LBLCodigoDeBarras.Text == "LECTOR ACTIVO")
            {
                LBLCodigoDeBarras.Text = "" + e.KeyChar;
            }
            else
            {
                LBLCodigoDeBarras.Text += e.KeyChar;
            }
        }

        #endregion

        private void FRMProducto_Registrar_Load(object sender, EventArgs e)
        {
            CargarComboCategorias();
            CargarCombo("capdnompro", CMBNombreProducto);
            CargarCombo("capdmarpro", CMBMarca);
            CargarCombo("capdmatpro", CMBMaterial);
            CargarCombo("capdcolpro", CMBColor);
            CargarCombo("capdtalpro", CMBTalla);
        }
    }
}
