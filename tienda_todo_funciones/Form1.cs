﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tienda_todo_funciones.forms_prueba;

namespace tienda_todo_funciones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ventas vent = new ventas();
            vent.Show();
            
        }

    }
}
