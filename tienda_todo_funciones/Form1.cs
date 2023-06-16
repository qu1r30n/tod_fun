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
using tienda_todo_funciones.desinger;
using tienda_todo_funciones.modelos;

namespace tienda_todo_funciones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            
            procesamientos procesos = new procesamientos();
            procesos.crear_archivos_inicio_programa();
            mod_comp_vent mod = new mod_comp_vent();
            //mod.chequeo_datos_esten_en_archivo("1|1|2|3|4|5|6|7|8|venta_ingrediente||11|12¬0°0_5¬1|13|14°14|15", "0|12", 0, 0, "0",5);


            string[] a =
            {
                "4|grupo|2|4|1|1°2°3|ingredientes_primarios",
                "3|titulo_del_boton|2|0",
                "3|titulo_del_boton|3|0",
                "3|titulo_del_boton|4|0"
            };
            Ventana_emergente b = new Ventana_emergente();
            //string k =b.Proceso_ventana_emergente(a, 0, "prueba");
            ventas vent = new ventas();
            vent.Show();
        }

    }
}
