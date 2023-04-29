using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tienda_todo_funciones.forms_prueba;
using tienda_todo_funciones.procesos;
using tienda_todo_funciones.clases;

namespace tienda_todo_funciones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            procesamientos procesos = new procesamientos();
            procesos.crear_archivos_inicio_programa();

            ventas vent = new ventas();
            vent.Show();
        }

    }
}
