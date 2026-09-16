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
    public partial class FRMCliente_Registrar : DevComponents.DotNetBar.Office2007Form
    {
        #region Variables
        private aclient cliente = new aclient();
        private aperson persona = new aperson();
        public bool personaOK = false;
        private xnumcor correlativo = new xnumcor();
        public bool modificar = false;
        public String codCliMod = "";
        public bool actualizar = false;
        #endregion

        #region Constructor
        public FRMCliente_Registrar()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos
        private bool VerificarIntegridad()
        {
            bool respuesta = true;


            if (TXTNIT.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Introduzca el NIT", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXTNIT.Focus();
                respuesta = false;
            }
            else if (TXTRazonSocial.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Introduzca la Razón Social", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXTRazonSocial.Focus();
                respuesta = false;
            }
            else if (TXTDireccion.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Introduzca la dirección", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXTDireccion.Focus();
                respuesta = false;
            }
            else if (TXTTelefono.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Introduzca el teléfono", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXTTelefono.Focus();
                respuesta = false;
            }
            else if (TXTNombre.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("No seleccionó una persona", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXTTelefono.Focus();
                respuesta = false;
            }



            return respuesta;
        }
        private void LimpiarCasillas()
        {
            SWBEstado.Value = true;
            TXTRazonSocial.Text = "";
            TXTNIT.Text = "";
            TXTDireccion.Text = "";
            TXTTelefono.Text = "";
            TXTNombre.Text = "";
        }

        private void JalarDatos()
        {

            persona.papscodper = this.codCliMod;
            persona.ObtenerDatos();

            cliente.faclcodper = this.codCliMod;
            cliente.ObtenerDatos();

            SWBEstado.Value = cliente.caclestcli;
            TXTRazonSocial.Text = cliente.caclrazcli;
            TXTNIT.Text = cliente.caclnitcli;
            TXTDireccion.Text = cliente.cacldircli;
            TXTTelefono.Text = cliente.cacltelcli;
            TXTNombre.Text = persona.capsapepat + " " + persona.capsapemat + " " + persona.capsnomper;
        }
        #endregion

        #region Eventos
        private void BTNBuscarUsuario_Click(object sender, EventArgs e)
        {
            FRMPersona_Buscar a = new FRMPersona_Buscar();
            a.condicion = "papscodper not in (select papscodper from aperson,aclient where papscodper=faclcodper order by papscodper)";
            a.ShowDialog();
            if (a.seleccionadoOk)
            {
                this.persona = a.persona;
                this.personaOK = true;

                TXTNombre.Text = persona.capsapepat + " " + persona.capsapemat + " " + persona.capsnomper;
                
            }
            else
            {
                this.personaOK = false;
                TXTNombre.Text = "Persona";

            }
        }

        private void BTNGrabar_Click(object sender, EventArgs e)
        {
            if (VerificarIntegridad())
            {


                cliente.caclestcli = SWBEstado.Value;
                cliente.caclnitcli = TXTNIT.Text;
                cliente.caclrazcli = TXTRazonSocial.Text;
                cliente.cacldircli = TXTDireccion.Text;
                cliente.cacltelcli = TXTTelefono.Text;





                if (!this.modificar)
                {
                    cliente.faclcodper = persona.papscodper;
                    if (cliente.Grabar())
                    {
                        MessageBox.Show("Cliente guardado correctamente!!",
                                        "Mensaje",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        LimpiarCasillas();
                        this.actualizar = true;

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cliente no se pudo guardar!!",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (cliente.Modificar())
                    {
                        MessageBox.Show("Cliente modificado correctamente!!",
                                        "Mensaje",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        LimpiarCasillas();
                        this.actualizar = true;

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cliente no se pudo modificar!!",
                                            "Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void FRMCliente_Registrar_Load(object sender, EventArgs e)
        {
            if (this.modificar)
            {
                JalarDatos();
                BTNGrabar.Text = "&Modificar";
                this.Text = "Modificar Cliente";
                GP_Panel_Usuario.Text = "Modificar Cliente";
                TXTNIT.Focus();
            }
            else
            {
                LimpiarCasillas();
                BTNGrabar.Text = "&Guardar";
                this.Text = "Registrar Cliente";
                GP_Panel_Usuario.Text = "Registrar Cliente";
                TXTNIT.Focus();
            }
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
