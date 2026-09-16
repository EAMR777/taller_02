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
    public partial class FRMCliente_Lista : DevComponents.DotNetBar.Office2007Form
    {
        #region Variables

        private aclient cliente = new aclient();
        private List<aclient> lista = new List<aclient>();

        #endregion

        #region Constructor
        public FRMCliente_Lista()
        {
            InitializeComponent();
        }
        #endregion

        #region Metodos
        private void ActualizarGrid()
        {
            DTGLista.Rows.Clear();
            lista.Clear();
            lista = cliente.Lista("(caclrazcli like '%" + TXTFiltrar.Text + "%' or " +
                                            "caclnitcli like '%" + TXTFiltrar.Text + "%' or " +
                                            "cacldircli like '%" + TXTFiltrar.Text + "%' or " +
                                            "cacltelcli like '%" + TXTFiltrar.Text + "%') limit " +
                                           IINFilas.Value.ToString()
                                           );

            foreach (aclient a in lista)
            {
                DTGLista.Rows.Add();

                if (a.caclestcli)
                {
                    if (DTGLista.Rows.Count % 2 == 0)
                    {
                        DTGLista.Rows[DTGLista.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Gainsboro;
                    }
                }
                else
                {
                    DTGLista.Rows[DTGLista.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Tomato;
                    DTGLista.Rows[DTGLista.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                }
                DTGLista[0, DTGLista.Rows.Count - 1].Value = a.faclcodper;
                DTGLista[1, DTGLista.Rows.Count - 1].Value = a.caclestcli;
                DTGLista[2, DTGLista.Rows.Count - 1].Value = a.caclrazcli;
                DTGLista[3, DTGLista.Rows.Count - 1].Value = a.caclnitcli;

                DTGLista[4, DTGLista.Rows.Count - 1].Value = a.cacldircli;
                DTGLista[5, DTGLista.Rows.Count - 1].Value = a.cacltelcli;


            }

        }

        #endregion

        #region Eventos
        private void BTNRegistrar_Click(object sender, EventArgs e)
        {
            FRMCliente_Registrar F1 = new FRMCliente_Registrar();
            F1.modificar = false;
            F1.ShowDialog();
            if (F1.actualizar)
            {
                ActualizarGrid();
            }
        }

        private void BTNModificar_Click(object sender, EventArgs e)
        {
            if (DTGLista.SelectedRows.Count > 0)
            {
                FRMCliente_Registrar F1 = new FRMCliente_Registrar();
                F1.modificar = true;
                F1.codCliMod = DTGLista[0, DTGLista.SelectedRows[0].Index].Value.ToString();
                F1.ShowDialog();
                if (F1.actualizar)
                {
                    ActualizarGrid();
                }
            }
        }

        private void FRMCliente_Lista_Load(object sender, EventArgs e)
        {
            ActualizarGrid();
        }
        #endregion
    }
}
