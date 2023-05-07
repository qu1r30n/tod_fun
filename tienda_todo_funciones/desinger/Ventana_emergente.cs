using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tienda_todo_funciones.clases;
using System.IO;

namespace tienda_todo_funciones.desinger
{
    public partial class Ventana_emergente : Form
    {

        //char[] G_parametros = { '|', '°', '¬', '^' };
        string[] G_parametros = variables_glob_conf.GG_caracter_separacion;
        string G_datos_de_boton = "";
        int G_contador = 0;
        int G_control_a_ocultar;
        int G_bandera = 0;



        public Ventana_emergente()
        {
            InitializeComponent();


        }

        public string Proceso_ventana_emergente(string[] nom_datos_recolectados, int modificara = 0, string titulo_ventana = "ventana_emergente", string[] infoextra = null, char caracter_spliteo = '°')
        {
            //1=textbox  1°titulo_texbox°contenido_text_box°restriccion_de_dato      ejemplo "1°precio venta°0°2" //el 2 es la restriccion que solo resivira numeros y punto decimal         
            //2=labels   2°titulo_label°abajo_pondra_otro_label_con_el_contenido    ejemplo "2°id°9999"
            //3=boton    3°titulo_del_boton°valor_del_boton°numero_de_Funcion            ejemplo "3°es_paquete°1°0" //cuando oprima el boton devolvera el valor 1 
            //4=combobox "4°
            //            titulo_combobox°
            //            valor_inicial_si anteriormente_no_se_a_modificado°
            //            restriccion_de_dato_con_aparte_opcion_4_que_es_proyecto_quetiene_otra_funcion°
            //            " + valor_inicial_si_se_modifico + '°'
            //            + todas_las_opciones_del_combobox_separadas_por_"°"
            //
            //            ejemplo "4°grupo°2°4°1°1°2°3°4"

            this.Text = titulo_ventana;
            this.AutoScroll = true;
            this.VerticalScroll.Enabled = true;

            int x = 120;
            int y__ = 0;
            int ancho = 100;
            int alto = 50;
            int acumleft = 0;
            int separacion_y = 15;
            int contador_en_horisontal_Txtbox = 0;

            string[] info = { "" };

            string nuevo_boton = "";
            string union = "";

            string bandera1 = "0", bandera2 = "0", bandera3 = "0", bandera4 = "0";

            for (int i = 0; i < nom_datos_recolectados.Length; i++)
            {
                string[] tipo_de_datos = nom_datos_recolectados[i].Split(caracter_spliteo);

                if (tipo_de_datos[0] == "1")
                {
                    bandera1 = "1";
                }
                else if (tipo_de_datos[0] == "2")
                {
                    bandera2 = "1";
                }
                else if (tipo_de_datos[0] == "3")
                {
                    bandera3 = "1";
                }
                else if (tipo_de_datos[0] == "4")
                {
                    bandera4 = "1";
                }
            }

            if (bandera3 == "1" && bandera1 == "0" && bandera2 == "0" && bandera4 == "0")
            {
                info[0] = "solo_botones";
            }

            string[] arraytextbox = new string[nom_datos_recolectados.Length];
            //xc = 1;

            if (nom_datos_recolectados.Length != 0)
            {
                for (int i = 0; i < nom_datos_recolectados.Length; i++)
                {
                    string[] espliteado = nom_datos_recolectados[i].Split(caracter_spliteo);

                    //labels y textbox
                    if (espliteado[0] == "1")
                    {
                        Label lb = new Label();
                        TextBox Txt = new TextBox();
                        if (contador_en_horisontal_Txtbox <= 4)
                        {
                            lb.Top = y__;
                            lb.Left = acumleft;

                            Txt.Top = y__ + separacion_y;
                            Txt.Left = acumleft;

                        }
                        else
                        {
                            contador_en_horisontal_Txtbox = 0;
                            y__ = y__ + 40;
                            acumleft = 0;
                            lb.Top = y__;
                            lb.Left = acumleft;

                            Txt.Top = y__ + separacion_y;
                            Txt.Left = acumleft;
                        }

                        contador_en_horisontal_Txtbox = contador_en_horisontal_Txtbox + 1;


                        if (espliteado.Length >= 3)
                        {
                            Txt.Text = espliteado[2];
                        }



                        if (espliteado.Length >= 4)
                        {
                            string[] restriccion_de_caracteres_a_usar = espliteado[3].Split(Convert.ToChar(G_parametros[3]));
                            for (int j = 0; j < restriccion_de_caracteres_a_usar.Length; j++)
                            {
                                string parametros = "";
                                switch (restriccion_de_caracteres_a_usar[j])
                                {
                                    case "1":
                                        parametros = "solo_letras";
                                        break;
                                    case "2":
                                        parametros = "solo_numeros";
                                        break;
                                    case "3":
                                        parametros = "ingredientes_primarios";
                                        break;
                                    default:
                                        break;
                                }
                                //Txt.KeyPress += new KeyPressEventHandler(restriccion_caracteres_forma_1);//llamar forma normal al precionar un carcter
                                Txt.KeyPress += new KeyPressEventHandler((sender1, e1) => restriccion_caracteres(sender1, e1, parametros));////llama funcion al precionar un carcter envia imformacion extra y parametros 
                            }
                        }

                        if (espliteado.Length >= 4)
                        {
                            Txt.Text = espliteado[2];
                            Txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            Txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            for (int j = 5; j < espliteado.Length; j++)
                            {
                                Txt.AutoCompleteCustomSource.Add(espliteado[j]);

                            }

                        }


                        nom_datos_recolectados[i] = espliteado[1];

                        lb.Text = nom_datos_recolectados[i];

                        Txt.Width = ancho;
                        Txt.Height = alto;

                        lb.AutoSize = true;


                        this.Controls.Add(lb);//le agrega un indice al control para luego utilisarlo por su indise en  la funcion devolver string
                        this.Controls.Add(Txt);//le agrega un indice al control para luego utilisarlo por su indise en  la funcion devolver string
                        acumleft = acumleft + x;



                    }

                    //labels
                    else if (espliteado[0] == "2")
                    {
                        Label lb = new Label();
                        Label Lbl2 = new Label();
                        arraytextbox[i] = espliteado[2];
                        if (contador_en_horisontal_Txtbox <= 4)
                        {
                            lb.Top = y__;
                            lb.Left = acumleft;

                            Lbl2.Top = y__ + separacion_y;
                            Lbl2.Left = acumleft;

                        }
                        else
                        {
                            contador_en_horisontal_Txtbox = 0;
                            y__ = y__ + 40;
                            acumleft = 0;
                            lb.Top = y__;
                            lb.Left = acumleft;

                            Lbl2.Top = y__ + separacion_y;
                            Lbl2.Left = acumleft;

                        }

                        contador_en_horisontal_Txtbox = contador_en_horisontal_Txtbox + 1;


                        lb.Text = espliteado[1];
                        if (espliteado.Length == 3)
                        {
                            Lbl2.Text = espliteado[2];
                        }






                        Lbl2.Width = ancho;
                        //Lbl2.Height = alto;


                        lb.AutoSize = true;
                        this.Controls.Add(lb);//le agrega un indice al control para luego utilisarlo por su indise en  la funcion devolver string
                        this.Controls.Add(Lbl2);//le agrega un indice al control para luego utilisarlo por su indise en  la funcion devolver string
                        acumleft = acumleft + x;



                    }

                    //botones
                    else if (espliteado[0] == "3")
                    {
                        Button Btn_nuevoboton = new Button();

                        Btn_nuevoboton.Name = espliteado[2];
                        Btn_nuevoboton.Text = espliteado[1];


                        if (contador_en_horisontal_Txtbox <= 4)
                        {
                            Btn_nuevoboton.Top = y__ + separacion_y;
                            Btn_nuevoboton.Left = acumleft;
                        }
                        else
                        {
                            contador_en_horisontal_Txtbox = 0;
                            y__ = y__ + 40;
                            acumleft = 0;

                            Btn_nuevoboton.Top = y__ + separacion_y;
                            Btn_nuevoboton.Left = acumleft;
                        }

                        contador_en_horisontal_Txtbox = contador_en_horisontal_Txtbox + 1;

                        Btn_nuevoboton.Width = ancho;
                        Btn_nuevoboton.Height = alto;

                        this.Controls.Add(Btn_nuevoboton);//le agrega un indice al control para luego utilisarlo por su indise en  la funcion devolver string
                        acumleft = acumleft + x;

                        this.Controls.Add(Btn_nuevoboton);

                        string parametros = i + "" + G_parametros[1] + espliteado[2] + G_parametros[1] + espliteado[3];
                        //Btn_nuevoboton.Click += new EventHandler(nuevoBoton_Click); 

                        Btn_nuevoboton.Click += new EventHandler((sender1, e1) => nuevo_boton = NuevoBoton_Click(sender1, e1, parametros, info));

                    }

                    //combobox
                    else if (espliteado[0] == "4")
                    {

                        Label lb = new Label();
                        ComboBox cmb = new ComboBox();
                        if (contador_en_horisontal_Txtbox <= 4)
                        {
                            lb.Top = y__;
                            lb.Left = acumleft;

                            cmb.Top = y__ + separacion_y;
                            cmb.Left = acumleft;

                        }
                        else
                        {
                            contador_en_horisontal_Txtbox = 0;
                            y__ = y__ + 40;
                            acumleft = 0;
                            lb.Top = y__;
                            lb.Left = acumleft;

                            cmb.Top = y__ + separacion_y;
                            cmb.Left = acumleft;
                        }

                        contador_en_horisontal_Txtbox = contador_en_horisontal_Txtbox + 1;


                        if (espliteado.Length >= 3)
                        {
                            string[] restriccion_de_caracteres_a_usar = espliteado[3].Split(Convert.ToChar(G_parametros[3]));
                            for (int j = 0; j < restriccion_de_caracteres_a_usar.Length; j++)
                            {
                                string parametros = "";
                                switch (restriccion_de_caracteres_a_usar[j])
                                {
                                    case "1":
                                        parametros = "solo_letras";
                                        cmb.Text = espliteado[2];

                                        break;
                                    case "2":
                                        parametros = "solo_numeros";
                                        cmb.Text = espliteado[2];

                                        break;
                                    case "3":
                                        parametros = "ingredientes_primarios";
                                        cmb.Text = espliteado[2];

                                        break;
                                    case "4":
                                        parametros = "ocultar_control";
                                        cmb.TextChanged += new EventHandler((sender2, e2) => tex_change_y_oculta_control_21(sender2, e2, parametros));
                                        cmb.Text = espliteado[2];
                                        break;

                                    case "5":
                                        parametros = "pasar_impuestos";
                                        cmb.KeyDown += new KeyEventHandler((sender2, e2) => pasar_datos_impuestos_si_da_enter(sender2, e2, parametros));
                                        cmb.Text = espliteado[2];
                                        break;

                                    default:
                                        cmb.Text = espliteado[2];
                                        break;
                                }
                                //Txt.KeyPress += new KeyPressEventHandler(restriccion_caracteres_forma_1);//llamar forma normal al precionar un carcter
                                //cmb.KeyPress += new KeyPressEventHandler((sender1, e1) => restriccion_caracteres(sender1, e1, parametros));////llama funcion al precionar un carcter envia imformacion extra y parametros 


                                //xa = 1;//eliminar cuando convines el textchange con creo el keypress algo por el estilo hay que checar
                            }
                        }

                        if (espliteado.Length >= 4)
                        {

                            cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            cmb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            for (int j = 5; j < espliteado.Length; j++)
                            {
                                cmb.Items.Add(espliteado[j]);
                                cmb.AutoCompleteCustomSource.Add(espliteado[j]);

                            }

                        }



                        nom_datos_recolectados[i] = espliteado[1];

                        lb.Text = nom_datos_recolectados[i];

                        cmb.Width = ancho;
                        cmb.Height = alto;

                        lb.AutoSize = true;
                        this.Controls.Add(lb);//le agrega un indice al control para luego utilisarlo por su indise en  la funcion devolver string
                        this.Controls.Add(cmb);//le agrega un indice al control para luego utilisarlo por su indise en  la funcion devolver string
                        acumleft = acumleft + x;

                    }


                    
                }

                //tamaño_ventana
                tamaño_ventana(nom_datos_recolectados.Length);

                //recuerda que el for que esta aqui arriba crea todos los controles
                G_control_a_ocultar = 21;//se usa para ocultar el textbox que es el de productos_elaborados que es el control 21 y se pone aqui por que es cuando termina de poner todos los controles

                if (G_bandera == 1)
                {
                    this.Controls[15].Text = "1";// se pone 1 para que cambie el combobox de grupo que es el control 21 y luego la funcion  oculte el contro que usa G_control_a_ocultar
                }

                //agrega el boton aceptar si hay un textbox o un combobox
                if (bandera1 == "1" || bandera4 == "1")
                {
                    Button Btn_aceptar = new Button();

                    Btn_aceptar.Width = ancho;
                    Btn_aceptar.Height = alto;
                    Btn_aceptar.Top = y__ + 60;
                    Btn_aceptar.Left = 10;
                    Btn_aceptar.Name = "Btn_aceptar_1";
                    Btn_aceptar.Text = "aceptar";
                    this.Controls.Add(Btn_aceptar);//le agrega un indice al control para luego utilisarlo por su indise en  la funcion devolver string

                    Btn_aceptar.DialogResult = DialogResult.OK;
                    this.ShowDialog();

                    //----------------------------------------------------------------------------------------------------------------------------
                    if (Btn_aceptar.DialogResult == DialogResult)
                    {
                        arraytextbox = Boton_aceptar(arraytextbox, modificara, null, caracter_spliteo);

                    }
                    else
                    {
                        arraytextbox = new[] { "" };
                    }

                    //------------------------------------------------------------------------------------------------------------------

                }

                else if ((bandera2 == "1" || bandera3 == "1") && (bandera1 != "1" && bandera1 != "4"))
                {
                    this.ShowDialog();
                    union = nuevo_boton;
                }


            }

            else { MessageBox.Show("no has puesto ningun dato"); }


            if (arraytextbox[0] != null)
            {
                for (int i = 0; i < arraytextbox.Length; i++)
                {
                    union = union + arraytextbox[i] + G_parametros[0];
                }
            }

            return union;
        }

        public void tamaño_ventana(int cantidad_elementos_para_la_ventana)
        {

            if (cantidad_elementos_para_la_ventana<=1)
            {
                this.Size = new Size(150, 150);
            }

            else if (cantidad_elementos_para_la_ventana == 2)
            {
                this.Size = new Size(250, 150);
            }

            else if (cantidad_elementos_para_la_ventana == 3)
            {
                this.Size = new Size(350, 150);
            }

            else if (cantidad_elementos_para_la_ventana == 4)
            {
                this.Size = new Size(500, 150);
            }
            else if (cantidad_elementos_para_la_ventana == 5)
            {
                this.Size = new Size(650, 150);
            }

            else if (cantidad_elementos_para_la_ventana >= 6 && cantidad_elementos_para_la_ventana <= 10)
            {
                this.Size = new Size(650, 150);
            }

            else if (cantidad_elementos_para_la_ventana >= 11 && cantidad_elementos_para_la_ventana <= 15)
            {
                this.Size = new Size(650, 250);
            }

            else if (cantidad_elementos_para_la_ventana >= 16)
            {
                this.Size = new Size(650, 270);
            }


        }

        public string[] Boton_aceptar(string[] arraytextbox, int modificara = 0, string[] infoextra = null, char caracter_spliteo = '°')
        {
            Tex_base bas = new Tex_base();
            Operaciones_archivos op = new Operaciones_archivos();
            string temp2 = "";

            string[] info_detro_celda = G_datos_de_boton.Split(Convert.ToChar((G_parametros[0])));

            for (int i = 0; i < info_detro_celda.Length; i++)
            {
                string[] posicion_y_datos = info_detro_celda[i].Split(caracter_spliteo);
                if (posicion_y_datos.Length >= 2)
                {
                    arraytextbox[Convert.ToInt32(posicion_y_datos[0])] = posicion_y_datos[1];
                }
            }

            int k = 0;
            for (int j = 0; j < this.Controls.Count; j++) //aqui agrega al arreglo global "arraytextbox" la informacion
            {
                object obj = this.Controls[j];

                for (int i = k; i < arraytextbox.Length; i++)
                {

                    if (obj is TextBox && arraytextbox[i] == null)
                    {
                        TextBox temp = (TextBox)obj;
                        arraytextbox[i] = temp.Text;
                        k = i;
                        break;
                    }
                    else if (obj is ComboBox && arraytextbox[i] == null)
                    {
                        ComboBox temp = (ComboBox)obj;
                        arraytextbox[i] = temp.Text;
                        k = i;
                        break;
                    }
                }

            }




            for (int i = 0; i < arraytextbox.Length; i++)
            {
                temp2 = temp2 + arraytextbox[i] + G_parametros[0];
            }
            Operaciones_textos op_tex = new Operaciones_textos();
            op_tex.Trimend_paresido(temp2, Convert.ToChar(G_parametros[0]));


            bas.Crear_archivo_y_directorio("inf\\inventario\\cosas_no_estaban.txt");

            switch (modificara)
            {
                case 0:

                    break;
                case 1:
                    bas.Agregar("inf\\inventario\\cosas_no_estaban.txt", "movimiento origen: " + modificara + G_parametros[0] + temp2);
                    bas.Agregar("inf\\inventario\\invent.txt", temp2);
                    break;
                case 3:
                    bas.Agregar("inf\\inventario\\cosas_no_estaban.txt", "movimiento origen: " + modificara + G_parametros[0] + temp2);
                    break;
                default:
                    bas.Agregar("inf\\inventario\\cosas_no_estaban.txt", "movimiento origen: " + modificara + G_parametros[0] + temp2);
                    break;
            }

            this.Close();
            return arraytextbox;

        }

        public string NuevoBoton_Click(object sender, EventArgs e, string seccion, string[] info_extra = null)
        {
            //comprobamos en que boton se a clicado
            Button Btn = sender as Button;

            //G_datos_de_boton = G_datos_de_boton + seccion + G_parametros[0];
            string[] seccion_espliteado = seccion.Split('°');
            if (info_extra != null)
            {
                if (info_extra[0] == "solo_botones")
                {

                    if (seccion_espliteado[2] == "0")
                    {
                        seccion = seccion_espliteado[1];
                        G_datos_de_boton = seccion;
                        this.Close();
                    }
                    else
                    {
                        funcion_elegida_por_boton(seccion_espliteado[2]);
                    }


                }

                else
                {
                    if (seccion_espliteado[2] == "0")
                    {
                        G_datos_de_boton = G_datos_de_boton + seccion + G_parametros[0];
                        this.Close();
                    }
                    else
                    {
                        funcion_elegida_por_boton(seccion_espliteado[2]);
                    }
                }
            }
            else
            {
                G_datos_de_boton = G_datos_de_boton + seccion + G_parametros[0];
            }

            //y vemos el resutado
            //MessageBox.Show("pulsado el boton: " + Btn.Text + "\nsu valor es: " + seccion);

            return G_datos_de_boton;
        }

        public static string txt_entrada(string title = "aqui tu titulo", string promptText = "aqui la pregunta", string value = "aqui el valor")
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return value;
        }


        public void tex_change_y_oculta_control_21(Object sender, EventArgs e, string parametros)
        {

            ComboBox contenido_contol = sender as ComboBox;

            if (parametros == "ocultar_control")
            {
                G_bandera = 1;
                if (G_control_a_ocultar == 21)
                {


                    if (contenido_contol.Text == "3")
                    {
                        this.Controls[21].Visible = true;
                    }

                    else
                    {
                        this.Controls[21].Visible = false;
                    }
                }

            }
        }

        public void pasar_datos_impuestos_si_da_enter(Object sender, KeyEventArgs e, string parametros)
        {
            ComboBox contenido_contol = sender as ComboBox;
            if (e.KeyCode == Keys.Enter)
            {
                
                string[] dato_espliteado= contenido_contol.Text.Split(Convert.ToChar(G_parametros[1]));
                this.Controls[25].Text = dato_espliteado[0];
                //destino.Items.Add(origen.Text);

            }
        }


        public void restriccion_caracteres(Object sender, KeyPressEventArgs e, string parametros)
        {
            if (parametros == "solo_letras")
            {

                if (char.IsLetter(e.KeyChar))//checa si lo introducido fue letra o no chart.IsLetter devuelve true o falce
                {

                }
                else
                {
                    e.KeyChar = '\0';
                }

            }

            else if (parametros == "solo_numeros")
            {
                //a = 1;
                if (char.IsNumber(e.KeyChar) || '.' == e.KeyChar || '\b' == e.KeyChar)
                {

                }

                else
                {
                    e.KeyChar = '\0';
                }
            }

            else if (parametros == "ingredientes_primarios")
            {
                G_contador++;
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    if (G_contador != 1)
                    {
                        string[] enviar = new string[] { "1°cantidad_ingrediente°1°2" };
                        Ventana_emergente cantidad_ingrediente = new Ventana_emergente();
                        string mensaje = cantidad_ingrediente.Proceso_ventana_emergente(enviar);
                        string[] mensaje_espliteado = mensaje.Split('|');

                        string temp = this.ActiveControl.Text;
                        this.ActiveControl.Text = temp + G_parametros[3] + mensaje_espliteado[0] + "°";

                        //e.KeyChar = '°';
                        G_contador = 0;
                    }
                    else
                    {
                        G_contador = 0;
                    }
                }


            }

        }

        public void restriccion_caracteres_forma_1(Object o, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))//checa si lo introducido fue letra o no chart.IsLetter devuelve true o falce
            {
                MessageBox.Show("" + e.KeyChar);
            }
            else if (char.IsNumber(e.KeyChar))
            {
                MessageBox.Show("" + e.KeyChar);
            }
            else
            {

            }
        }

        public void funcion_elegida_por_boton(string id_funcion)
        {
            switch (id_funcion)
            {
                case "1":
                    OpenFileDialog opfd = new OpenFileDialog();
                    opfd.InitialDirectory = Directory.GetCurrentDirectory() + "\\pedidos";
                    if (opfd.ShowDialog() == DialogResult.OK)
                    {

                        G_datos_de_boton = opfd.FileName;
                        this.Close();
                    }
                    break;
                case "2":
                    break;
                case "3":
                    break;
                default:
                    break;
            }
        }
    }
}
