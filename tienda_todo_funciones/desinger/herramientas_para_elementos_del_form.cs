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
using tienda_todo_funciones.modelos;

namespace tienda_todo_funciones.desinger
{
    
    public partial class herramientas_para_elementos_del_form : Form
    {

        //Tex_base bas = new Tex_base();
        operaciones_arreglos op_arr = new operaciones_arreglos();
        Operaciones_textos op_text = new Operaciones_textos();
        model mod = new model();

        

        string[] G_caracter_separacion = variables_glob_conf.GG_caracter_separacion;

        
        public herramientas_para_elementos_del_form()
        {
            InitializeComponent();
            
        }

        //txt------------------------------------------------------------

        
        public void fun_txt_prediccion_palabra(TextBox txt_a_configurar,string tipo)
        {
            // Inicializar AutoCompleteStringCollection
            txt_a_configurar.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_a_configurar.AutoCompleteSource = AutoCompleteSource.CustomSource;

            if (tipo=="codbar")
            {
                txt_a_configurar.AutoCompleteCustomSource = variables_glob_conf.GG_autoCompleteCollection_codbar_venta;
            }
            else if (tipo=="producto")
            {
                txt_a_configurar.AutoCompleteCustomSource = variables_glob_conf.GG_autoCompleteCollection_nom_produc_venta;
            }

        }

        
        public void fun_txt_procesar_tecleos(TextBox txt_a_configurar, ListBox lstb_a_configurar,Label lbl_configurar_desc_produc, Label lbl_configurar_cantidad_costo, Label lbl_configurar_total)
        {
            txt_a_configurar.KeyDown += (sender, e) =>
            {
                if (e.KeyValue == (char)Keys.Enter)
                {
                    
                    string[] tex_esplit = txt_a_configurar.Text.Split(Convert.ToChar(G_caracter_separacion[0]));
                    variables_glob_conf.GG_variables_string[0] = tex_esplit[0];

                    string indice_producto = "";
                    for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
                    {
                        string[] producto_espliteado = variables_glob_conf.GG_arrays_carga_de_archivos[0][i].Split(Convert.ToChar(G_caracter_separacion[0]));
                        if (variables_glob_conf.GG_variables_string[0] == producto_espliteado[4])
                        {
                            indice_producto = "" + i;
                            break;
                        }
                            
                    }
                    if (indice_producto!="")
                    {
                        fun_lstb_agregar_elim(lstb_a_configurar, txt_a_configurar, "agregar_producto", 1, lbl_configurar_desc_produc: lbl_configurar_desc_produc, lbl_configurar_cantidad_costo: lbl_configurar_cantidad_costo, lbl_configurar_total: lbl_configurar_total);
                        
                    }

                    else
                    {
                        //tipos_medida_producto
                        if(variables_glob_conf.GG_variables_string[5] == null || variables_glob_conf.GG_variables_string[5] == "")
                        {
                            variables_glob_conf.GG_variables_string[5] = "NOSE";
                        }
                        //fin_tipos_medida_producto
                        //provedores
                        if (variables_glob_conf.GG_variables_string[1] == null || variables_glob_conf.GG_variables_string[1] == "")
                        {
                            variables_glob_conf.GG_variables_string[1] = "NOSE";
                        }
                        variables_glob_conf.GG_variables_string[2] = op_text.join_paresido(Convert.ToChar(G_caracter_separacion[1]), variables_glob_conf.GG_arrays_carga_de_archivos[1]);
                        //fin_provedores
                        variables_glob_conf.GG_variables_string[6] = op_text.join_paresido(Convert.ToChar(G_caracter_separacion[1]), variables_glob_conf.GG_arrays_carga_de_archivos[5]);

                        
                        
                        Ventana_emergente emergente_vent = new Ventana_emergente();
                        variables_glob_conf.RecargarVentanaEmergenteProductos();
                        string datos_introducidos=emergente_vent.Proceso_ventana_emergente(variables_glob_conf.GG_ventana_emergente_productos);


                        if (datos_introducidos != "")
                        {

                            mod.modelo_unico("mod_form_chequeo_info_producto_nuevo", texto_rapido: datos_introducidos);
                            registro_nuevo_producto(txt_a_configurar, datos_introducidos);
                            //mod.modelo_unico("agregar_string_al_inventario", texto_rapido: datos_introducidos);

                            
                            Tex_base bas = new Tex_base();
                            fun_txt_prediccion_palabra(txt_a_configurar,"codbar");
                            
                            
                            txt_a_configurar.Text = "";
                            txt_a_configurar.Focus();

                        }
                        else
                        {
                            return;
                        }
                    }
                }

                else if (e.KeyValue == (char)(Keys.Add))
                {
                    //e.KeyChar = '\0';//este evita que escrivas cuando usas keypress
                    e.SuppressKeyPress = true;

                    if (lstb_a_configurar.Items.Count > 0)
                    {


                        string[] elemento_espliteado = lstb_a_configurar.Items[lstb_a_configurar.Items.Count - 1].ToString().Split(Convert.ToChar(G_caracter_separacion[0]));

                        double cantidad = 1;
                        string[] info_produc_del_invent = extraer_info_e_indise(elemento_espliteado[0]);
                        string[] info_split_produc_inv = info_produc_del_invent[0].Split(Convert.ToChar(G_caracter_separacion[0]));
                        if (info_split_produc_inv[8] == "2")
                        {
                            Ventana_emergente vent_emergent = new Ventana_emergente();
                            string[] enviar = { "2°producto°" + info_produc_del_invent[3], "1°cantidad en litros o kilos(se puede decimal)°°2" };
                            string result = vent_emergent.Proceso_ventana_emergente(enviar);
                            if (result != "")
                            {
                                cantidad = Convert.ToDouble(result);
                            }

                        }
                        string cantidad_sumada_o_restada = sumar_o_restar_producto(lstb_a_configurar, elemento_espliteado[0], cantidad);

                        if (info_produc_del_invent[0] != null)
                        {


                            if (lbl_configurar_desc_produc != null) { lbl_configurar_desc_produc.Text = elemento_espliteado[1] + " " + elemento_espliteado[2] + " " + elemento_espliteado[3] + " precio:" + info_split_produc_inv[3]; }
                            if (lbl_configurar_cantidad_costo != null) { lbl_configurar_cantidad_costo.Text = cantidad_sumada_o_restada + " " + elemento_espliteado[1] + " " + elemento_espliteado[2] + "" + elemento_espliteado[3] + " " + (Convert.ToDouble(cantidad_sumada_o_restada) * Convert.ToDouble(info_split_produc_inv[3])); }
                            sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5, lbl_configurar_total);

                        }

                    }

                }
                else if (e.KeyValue == (char)(Keys.Subtract))
                {
                    //e.KeyChar = '\0';//este evita que escrivas cuando usas keypress
                    e.SuppressKeyPress = true;

                    if (lstb_a_configurar.Items.Count > 0)
                    {


                        string[] elemento_espliteado = lstb_a_configurar.Items[lstb_a_configurar.Items.Count - 1].ToString().Split(Convert.ToChar(G_caracter_separacion[0]));

                        double cantidad = Convert.ToDouble(-1);
                        string[] info_produc_del_invent = extraer_info_e_indise(elemento_espliteado[0]);
                        string[] info_split_produc_inv = info_produc_del_invent[0].Split(Convert.ToChar(G_caracter_separacion[0]));
                        if (info_split_produc_inv[8] == "2")
                        {
                            Ventana_emergente vent_emergent = new Ventana_emergente();
                            string[] enviar = { "2°producto°" + info_produc_del_invent[3], "1°cantidad en litros o kilos(se puede decimal)°°2" };
                            string result = vent_emergent.Proceso_ventana_emergente(enviar);
                            
                            if (result != "")
                            {
                                cantidad = (-1*Convert.ToDouble(result));

                            }
                            else
                            {
                                return;
                            }

                        }
                        string cantidad_sumada_o_restada = sumar_o_restar_producto(lstb_a_configurar, elemento_espliteado[0], (cantidad));
                        if (cantidad_sumada_o_restada=="")
                        {
                            cantidad_sumada_o_restada = "0";
                        }
                        if (info_produc_del_invent[0] != null && cantidad_sumada_o_restada != "0") 
                        {


                            if (lbl_configurar_desc_produc != null) { lbl_configurar_desc_produc.Text = elemento_espliteado[1] + " " + elemento_espliteado[2] + " " + elemento_espliteado[3] + " precio:" + info_split_produc_inv[3]; }
                            if (lbl_configurar_cantidad_costo != null) { lbl_configurar_cantidad_costo.Text = cantidad_sumada_o_restada + " " + elemento_espliteado[1] + " " + elemento_espliteado[2] + "" + elemento_espliteado[3] + " " + (Convert.ToDouble(cantidad_sumada_o_restada) * Convert.ToDouble(info_split_produc_inv[3])); }
                            sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5, lbl_configurar_total);

                        }

                        else
                        {
                            lbl_configurar_desc_produc.Text = "nombre del producto";
                            lbl_configurar_cantidad_costo.Text = "$";
                            sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5, lbl_configurar_total);

                        }

                    }


                }

            };
        }

        public void fun_txt_nom_produc_pasar_a_txt_codigo(TextBox txt_origen, TextBox txt_destino,string orden)
        {
            
            txt_origen.KeyDown += (sender, e) =>
            {
                if (e.KeyValue == (char)Keys.Enter)
                {
                    string[] info_espliteado = txt_origen.Text.Split(Convert.ToChar(G_caracter_separacion[0]));
                    if (info_espliteado.Length > 1)
                    {
                        string info_ordenado = OrdenarColumnas_string(txt_origen.Text, orden, G_caracter_separacion[0]);
                        info_ordenado = op_text.Trimend_paresido(info_ordenado, Convert.ToChar(G_caracter_separacion[0]));
                        txt_destino.Text = info_ordenado;
                        txt_destino.Focus();
                        txt_origen.Text = "";
                    }
                }

            };
        }

        //fin txt-------------------------------------------------------------
        //listbox--------------------------------------------------------------------
        public void fun_lstb_procesar_tecleos(ListBox lstb_a_configurar, Label lbl_configurar_desc_produc, Label lbl_configurar_cantidad_costo, Label lbl_configurar_total)
        {
            lstb_a_configurar.KeyDown += (sender, e) =>
            {

                if (e.KeyValue == (char)(Keys.Add))
                {
                    //e.KeyChar = '\0';//este evita que escrivas cuando usas keypress
                    e.SuppressKeyPress = true;
                    

                    if (lstb_a_configurar.Items.Count > 0)
                    {

                        string[] elemento_espliteado = lstb_a_configurar.SelectedItem.ToString().Split(Convert.ToChar(G_caracter_separacion[0]));


                        double cantidad = 1;
                        string[] info_produc_del_invent = extraer_info_e_indise(elemento_espliteado[0]);
                        string[] info_split_produc_inv = info_produc_del_invent[0].Split(Convert.ToChar(G_caracter_separacion[0]));
                        if (info_split_produc_inv[9] == "2")
                        {
                            Ventana_emergente vent_emergent = new Ventana_emergente();
                            string[] enviar = { "2°producto°" + info_produc_del_invent[3], "1°cantidad en litros o kilos(se puede decimal)°°2" };
                            string result = vent_emergent.Proceso_ventana_emergente(enviar);
                            if (result != "")
                            {
                                cantidad = Convert.ToDouble(result);
                            }

                        }
                        string cantidad_sumada_o_restada = sumar_o_restar_producto(lstb_a_configurar, elemento_espliteado[0], cantidad);

                        if (info_produc_del_invent[0] != null)
                        {


                            if (lbl_configurar_desc_produc != null) { lbl_configurar_desc_produc.Text = elemento_espliteado[1] + " " + elemento_espliteado[2] + " " + elemento_espliteado[3] + " precio:" + info_split_produc_inv[4]; }
                            if (lbl_configurar_cantidad_costo != null) { lbl_configurar_cantidad_costo.Text = cantidad_sumada_o_restada + " " + elemento_espliteado[1] + " " + elemento_espliteado[2] + "" + elemento_espliteado[3] + " " + (Convert.ToDouble(cantidad_sumada_o_restada) * Convert.ToDouble(info_split_produc_inv[4])); }
                            sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5, lbl_configurar_total);

                        }

                    }
                }

                
                else if (e.KeyValue == (char)(Keys.Subtract))
                {
                    //e.KeyChar = '\0';//este evita que escrivas cuando usas keypress
                    e.SuppressKeyPress = true;
                    

                    if (lstb_a_configurar.Items.Count > 0)
                    {

                        string[] elemento_espliteado = lstb_a_configurar.SelectedItem.ToString().Split(Convert.ToChar(G_caracter_separacion[0]));

                        double cantidad = Convert.ToDouble(-1);
                        string[] info_produc_del_invent = extraer_info_e_indise(elemento_espliteado[0]);
                        string[] info_split_produc_inv = info_produc_del_invent[0].Split(Convert.ToChar(G_caracter_separacion[0]));
                        if (info_split_produc_inv[9] == "2")
                        {
                            Ventana_emergente vent_emergent = new Ventana_emergente();
                            string[] enviar = { "2°producto°" + info_produc_del_invent[3], "1°cantidad en litros o kilos(se puede decimal)°°2" };
                            string result = vent_emergent.Proceso_ventana_emergente(enviar);

                            if (result != "")
                            {
                                cantidad = (-1 * Convert.ToDouble(result));

                            }
                            else
                            {
                                return;
                            }

                        }
                        string cantidad_sumada_o_restada = sumar_o_restar_producto(lstb_a_configurar, elemento_espliteado[0], (cantidad));
                        if (cantidad_sumada_o_restada == "")
                        {
                            cantidad_sumada_o_restada = "0";
                        }
                        if (info_produc_del_invent[0] != null && cantidad_sumada_o_restada != "0")
                        {


                            if (lbl_configurar_desc_produc != null) { lbl_configurar_desc_produc.Text = elemento_espliteado[1] + " " + elemento_espliteado[2] + " " + elemento_espliteado[3] + " precio:" + info_split_produc_inv[4]; }
                            if (lbl_configurar_cantidad_costo != null) { lbl_configurar_cantidad_costo.Text = cantidad_sumada_o_restada + " " + elemento_espliteado[1] + " " + elemento_espliteado[2] + "" + elemento_espliteado[3] + " " + (Convert.ToDouble(cantidad_sumada_o_restada) * Convert.ToDouble(info_split_produc_inv[4])); }
                            sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5, lbl_configurar_total);

                        }

                        else
                        {
                            lbl_configurar_desc_produc.Text = "nombre del producto";
                            lbl_configurar_cantidad_costo.Text = "$";
                            sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5, lbl_configurar_total);

                        }

                    }
                }


            };
        }

        public void fun_lstb_agregar_elim(ListBox lstb_a_configurar, TextBox txt_de_donde_agregara_info, string accion_realisar, double cantidad = 0, string proceso = "", string indice_producto = "", Label lbl_configurar_desc_produc = null, Label lbl_configurar_cantidad_costo = null, Label lbl_configurar_total = null)
        {
            
            switch (accion_realisar)
            {
                case "agregar":
                    if (txt_de_donde_agregara_info.Text != "" && txt_de_donde_agregara_info.Text != null)
                    {

                        lstb_a_configurar.Items.Add(txt_de_donde_agregara_info.Text);
                        txt_de_donde_agregara_info.Text = "";
                    }
                    break;

                //inicio agregar_productos----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case "agregar_producto":

                    //agrega por agregar falta revisar si ya existe el codigo
                    if (txt_de_donde_agregara_info.Text != "" && txt_de_donde_agregara_info.Text != null)
                    {
                        string[] info_pa_comparar = txt_de_donde_agregara_info.Text.Split(Convert.ToChar(G_caracter_separacion[0]));
                        string[] info_produc_del_invent = extraer_info_e_indise(info_pa_comparar[0]);
                        string[] info_split_produc_inv = info_produc_del_invent[0].Split(Convert.ToChar(G_caracter_separacion[0]));
                        if (info_split_produc_inv[9]=="2")
                        {
                            Ventana_emergente vent_emergent = new Ventana_emergente();
                            string[] enviar = { "2°producto°" + info_produc_del_invent[3], "1°cantidad en litros o kilos(se puede decimal)°°2" };
                            string result = vent_emergent.Proceso_ventana_emergente(enviar);
                            if (result!="")
                            {
                                cantidad = Convert.ToDouble(result);
                            }

                        }
                        string cantidad_sumada_o_restada = sumar_o_restar_producto(lstb_a_configurar, info_pa_comparar[0], cantidad);
                        
                        if (info_produc_del_invent[0]!=null)
                        {
                            
                            
                            if (lbl_configurar_desc_produc != null) { lbl_configurar_desc_produc.Text = info_pa_comparar[1] + " " + info_pa_comparar[2] + " " + info_pa_comparar[3] + " precio:" + info_split_produc_inv[3]; }
                            if (lbl_configurar_cantidad_costo != null) { lbl_configurar_cantidad_costo.Text = cantidad_sumada_o_restada + " " + info_pa_comparar[1] + " " + info_pa_comparar[2] + "" + info_pa_comparar[3] + " " + (Convert.ToDouble(cantidad_sumada_o_restada) * Convert.ToDouble(info_split_produc_inv[3])); }
                            sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5, lbl_configurar_total);

                        }


                        txt_de_donde_agregara_info.Text = "";
                    }
                   
                    break;

                //fin agregar_productos---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


                //i------------------------------------------------------------------------------------------------------------------------
                case "eliminar":

                    funciones_de_botones(lstb_a_configurar, proceso);

                    break;

                //f--------------------------------------------------------------------------------------------------------------------



                default:
                    break;
            }


        }
        //fin listbox-----------------------------------------------------------------------------
        //extras---------------------------------------------------
        public void fun_promociones_procesar_tecleos(TextBox txt_a_configurar, ListBox lstb_a_configurar, ListBox lstb_promociones, ListBox lstb_descripcion_promo)
        {
            lstb_a_configurar.KeyDown += (sender, e) =>
            {
                if (e.KeyValue == (char)(Keys.Add))
                {
                    //promociones(lstb_a_configurar, lstb_promociones, lstb_descripcion_promo);
                }
                else if (e.KeyValue == (char)(Keys.Subtract))
                {
                    //promociones(lstb_a_configurar, lstb_promociones, lstb_descripcion_promo);
                }
            };



            txt_a_configurar.KeyDown += (sender, e) =>
            {
                if (e.KeyValue == (char)Keys.Enter)
                {
                    //promociones(lstb_a_configurar, lstb_promociones, lstb_descripcion_promo);

                }

                else if (e.KeyValue == (char)(Keys.Add))
                {
                    //promociones(lstb_a_configurar, lstb_promociones, lstb_descripcion_promo);
                }
                else if (e.KeyValue == (char)(Keys.Subtract))
                {
                    //promociones(lstb_a_configurar, lstb_promociones, lstb_descripcion_promo);
                }

            };


        }
        //fin extras--------------------------------------------------------
        //botones
        public void fun_botones(ListBox lstb_a_configurar, Button btn_usar, string proceso, Label lbl_configurar_desc_produc, Label lbl_configurar_cantidad_costo, Label lbl_configurar_total)
        {
            btn_usar.Click += (sender, e) =>
            {
                //eliminar_por_item,eliminar_seleccionado,eliminar_ultimo,eliminar_todo
                funciones_de_botones(lstb_a_configurar, proceso);


                lbl_configurar_desc_produc.Text = "nombre del producto";
                lbl_configurar_cantidad_costo.Text = "$";
                sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5, lbl_configurar_total);



            };
        }

        
        private void ventana_introduce_cantidad_pagada(ListBox lstb_a_configurar)
        {
            double total = sumar_precio_columna_lstbox(lstb_a_configurar, 4, 5);
            string[] arreglo_info_lstb = lstb_a_configurar.Items.Cast<string>().ToArray();//convierte el listbox a arreglo


            Ventana_emergente vent_emergent = new Ventana_emergente();
            string[] enviar = { "1|cantidad pagada|" + total + "|2" };

            string cantidad_en_dinero_dada = vent_emergent.Proceso_ventana_emergente(enviar);
            double cantidad_en_dinero_dada_double = Convert.ToDouble(cantidad_en_dinero_dada);



            if (total + "" == cantidad_en_dinero_dada)
            {
                mod.modelo_unico("mod_venta",informacion_de_variables: arreglo_info_lstb);
            }
            else if (cantidad_en_dinero_dada_double > total)
            {
                mod.modelo_unico("mod_venta", arreglo_info_lstb);
                MessageBox.Show("cambio: " + (cantidad_en_dinero_dada_double - total));
            }
            else if (cantidad_en_dinero_dada_double < total)
            {
                MessageBox.Show("falta: " + (total - cantidad_en_dinero_dada_double));
                ventana_introduce_cantidad_pagada(lstb_a_configurar);
            }

        }

        //herramientas--------------------------------------------------------------------------------------------------------------------------------------
        public string OrdenarColumnas_string(string columnas, string orden, string caracter_separacion = "|")
        {
            char caracter = Convert.ToChar(caracter_separacion);
            string[] columnasArr = columnas.Split(caracter);
            string[] ordenArr = orden.Split(caracter);

            string resultado = "";
            for (int i = 0; i < ordenArr.Length; i++)
            {
                int indice = Convert.ToInt32(ordenArr[i]);

                resultado = resultado + columnasArr[indice] + caracter;
            }
            return resultado;
        }

        public string[] OrdenarColumnas_arreglo(string[] columnasArr, string orden, string caracter_separacion = "|")
        {
            char caracter = Convert.ToChar(caracter_separacion);
            string[] ordenArr = orden.Split(caracter);

            string[] columnasOrdenadas = new string[columnasArr.Length];

            for (int j = 0; j < columnasArr.Length; j++)
            {
                string[] columnas = columnasArr[j].Split(caracter);

                string resultado = "";
                for (int i = 0; i < ordenArr.Length; i++)
                {
                    int indice = Convert.ToInt32(ordenArr[i]);

                    resultado = resultado + columnas[indice] + caracter;
                }
                columnasOrdenadas[j] = resultado.TrimEnd(caracter);
            }
            return columnasOrdenadas;
        }

        public string[] eliminar_registro_del_array(string[] arreglo, int num_registro)
        {
            string[] temp = new string[arreglo.Length - 1];
            int j = 0;
            for (int i = 0; i < arreglo.Length; i++)
            {
                if (i != num_registro)
                {
                    temp[j] = arreglo[i];
                    j++;
                }
            }

            return temp;
        }

        

        public void funciones_de_botones(ListBox lstb_a_configurar, string proceso,string info_extra="")
        {
            //eliminar_por_item,eliminar_seleccionado,eliminar_ultimo,eliminar_todo
            switch (proceso)
            {
                case "eliminar_por_item":
                    if (lstb_a_configurar.Items.Count > 0)
                    {
                        lstb_a_configurar.Items.RemoveAt(Convert.ToInt32(info_extra));
                    }
                    break;
                case "eliminar_seleccionado":
                    
                    lstb_a_configurar.Items.RemoveAt(lstb_a_configurar.SelectedIndex);
                    break;

                case "eliminar_ultimo":
                    if (lstb_a_configurar.Items.Count > 0)
                    {
                        lstb_a_configurar.Items.RemoveAt(lstb_a_configurar.Items.Count - 1);
                    }
                    break;
                case "eliminar_todo":
                    lstb_a_configurar.Items.Clear();
                    break;

                case "procesar_venta":
                    ventana_introduce_cantidad_pagada(lstb_a_configurar);
                    lstb_a_configurar.Items.Clear();
                    break;

                default:
                    break;
            }
        }

        public string sumar_o_restar_producto(ListBox lstb_a_configurar, string codigo, double cantidad = 1)
        {
            bool esta_el_mismo_producto = false;
            string cantidad_retornar_string = "";
            for (int i = 0; i < lstb_a_configurar.Items.Count; i++)
            {
                string[] elemento_espliteado = lstb_a_configurar.Items[i].ToString().Split(Convert.ToChar(G_caracter_separacion[0]));
                if (codigo == elemento_espliteado[0])
                {
                    esta_el_mismo_producto = true;
                    double resultado = (Convert.ToDouble(elemento_espliteado[5]) + cantidad);
                    elemento_espliteado[5] = "" + resultado;
                    if (resultado<=0)
                    {
                        funciones_de_botones(lstb_a_configurar, "eliminar_por_item", i + "");
                    }
                    else
                    {
                        lstb_a_configurar.Items[i] = string.Join(G_caracter_separacion[0], elemento_espliteado);
                        cantidad_retornar_string = elemento_espliteado[5];
                    }
                    
                }
            }

            
            if (!esta_el_mismo_producto)
            {
                
                for (int j = 0; j < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; j++)
                {
                    string[] producto_invent=variables_glob_conf.GG_arrays_carga_de_archivos[0][j].Split(Convert.ToChar(G_caracter_separacion[0]));
                    if (codigo== producto_invent[4])
                    {
                        //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|0_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|produc_elaborados_12|ligar_productos_para_sabo_13|impuesto_14|tipo_producto_para_impuesto_15

                        lstb_a_configurar.Items.Add(producto_invent[4] + G_caracter_separacion[0] + producto_invent[0] + G_caracter_separacion[0] + producto_invent[1] + G_caracter_separacion[0] + producto_invent[2] + G_caracter_separacion[0] + producto_invent[3] + G_caracter_separacion[0] + cantidad + G_caracter_separacion[0] + j);
                        cantidad_retornar_string = ""+cantidad;
                        break;
                    }
                }
            }
            return cantidad_retornar_string;
        }

        public double sumar_precio_columna_lstbox(ListBox lstb_a_configurar, int columna_precios, int columna_cantidad, Label lbl_total = null, string caracter_separacion = null)
        {
            if (caracter_separacion==null)
            {
                caracter_separacion = G_caracter_separacion[0];
            }
            double total = 0;
            for (int i = 0; i < lstb_a_configurar.Items.Count; i++)
            {
                string[]dat_produc=lstb_a_configurar.Items[i].ToString().Split(Convert.ToChar(caracter_separacion));
                total=total+(Convert.ToDouble(dat_produc[columna_cantidad]) * Convert.ToDouble(dat_produc[columna_precios]));
            }
            
            if (lbl_total != null) { lbl_total.Text = "" + total; }

            return total;
        }

        public string indice_producto(string cod_bar)
        {
            string existe = "";

            for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
            {
                string[] produc_inv_esplit = variables_glob_conf.GG_arrays_carga_de_archivos[0][i].Split(Convert.ToChar(G_caracter_separacion[0]));
                if (produc_inv_esplit[3]==cod_bar)
                {
                    existe = i+"";
                    break;
                }
            }
            return existe;
        }

        private void promociones(ListBox Lst_ventas, ListBox lstb_promociones, ListBox lstb_descripcion_promo)
        {
            lstb_descripcion_promo.Items.Clear();
            Tex_base bas = new Tex_base();
            //promociones-----------------------------------------------------------------------------------------------------------
            //"nombre_promocion|codigo_barras_1¬cantidad_del_producto¬nombre¬precio_comp¬precio_vent°codigo_barras_2¬cantidad_del_producto¬nombre¬precio_comp¬precio_vent|precio|
            string[] promos = bas.Leer("promosiones\\promos.txt");

            for (int i = 1; i < promos.Length; i++)
            {


                string[] promo_1_nom_produc_precio = promos[i].Split(Convert.ToChar(G_caracter_separacion[0]));
                string[] promo_produc = promo_1_nom_produc_precio[1].Split(Convert.ToChar(G_caracter_separacion[1]));

                string[] si_cumple_cantidad_pa_promo = new string[promo_produc.Length];

                for (int j = 0; j < promo_produc.Length; j++)
                {
                    string[] datos_producto_promo = promo_produc[j].Split(Convert.ToChar(G_caracter_separacion[2]));


                    //chequeo_lista_ventas-----------------------------------------------------------------------------------
                    for (int k = 0; k < Lst_ventas.Items.Count; k++)
                    {
                        string[] produc_list_split = Lst_ventas.Items[k].ToString().Split(Convert.ToChar(G_caracter_separacion[0]));
                        double dato_list_comparar = Convert.ToDouble(produc_list_split[8]);
                        double dato_promo_comparar = Convert.ToDouble(datos_producto_promo[1]);
                        if (produc_list_split[0] == datos_producto_promo[0] && dato_promo_comparar <= dato_list_comparar)
                        {
                            si_cumple_cantidad_pa_promo[j] = "1";
                        }

                    }

                    bool estan_todos_produc = false;
                    for (int l = 0; l < si_cumple_cantidad_pa_promo.Length; l++)
                    {
                        if (si_cumple_cantidad_pa_promo[l] == "1")
                        {
                            estan_todos_produc = true;

                        }
                        else
                        {
                            estan_todos_produc = false;
                            break;
                        }

                    }
                    if (estan_todos_produc == true)
                    {

                        promo_1_nom_produc_precio[3] = "1";
                        promos[i] = string.Join(G_caracter_separacion[0], promo_1_nom_produc_precio);

                    }
                    //-------------------------------------------------------------------------------------------
                }
            }
            //colocar en listbox promociones------------------------------------------------------------------
            lstb_promociones.Items.Clear();
            for (int i = 1; i < promos.Length; i++)
            {
                string[] pro_split = promos[i].Split(Convert.ToChar(G_caracter_separacion[0]));
                if (pro_split[3] == "1")
                {
                    lstb_promociones.Items.Add(promos[i]);
                }


            }
            //------------------------------------------------------------------------------------------------


            //promociones fin-----------------------------------------------------------------------------------------------------------
        }

        public void registro_nuevo_producto(TextBox txt_a_configurar, string registro_agregar)
        {
            string orden_1 = "3" + G_caracter_separacion[0] + "2" + G_caracter_separacion[0] + "1" + G_caracter_separacion[0] + "3" + G_caracter_separacion[0] + "7";//se repite el 3 en la [3] pocicion por que de hay se agarrara el codigo de barras para procesarlo
            string reg_ordenado_para_txt = OrdenarColumnas_string(registro_agregar, orden_1);
            txt_a_configurar.AutoCompleteCustomSource.Add(reg_ordenado_para_txt+(G_caracter_separacion[0]+variables_glob_conf.GG_arrays_carga_de_archivos[0].Length+""));

            variables_glob_conf.GG_arrays_carga_de_archivos[0] = op_arr.agregar_registro_del_array(variables_glob_conf.GG_arrays_carga_de_archivos[0], reg_ordenado_para_txt + (G_caracter_separacion[0] + variables_glob_conf.GG_arrays_carga_de_archivos[0].Length + ""));
            
            
            mod.modelo_unico("agregar_string_al_archivo", ubicacion_rapida: variables_glob_conf.GG_dir_nom_archivos[0, 0], texto_rapido: registro_agregar);


        }

        public string[] extraer_info_e_indise(string cod_bar,string caracter_separacion = null)
        {

            if (caracter_separacion==null)
            {
                caracter_separacion = G_caracter_separacion[0];
            }

            string[] informacionProducto = { null, null };

            bool encontrado = false;
            for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
            {
                string[] producto_espliteado = variables_glob_conf.GG_arrays_carga_de_archivos[0][i].Split(Convert.ToChar(caracter_separacion));
                if (producto_espliteado[4] == cod_bar)
                {
                    informacionProducto[0] = variables_glob_conf.GG_arrays_carga_de_archivos[0][i];
                    informacionProducto[1] = ""+i;
                    encontrado = true;
                    break;
                }
            }

            if (encontrado==false){/*hacer algo si no lo encuentra*/}

            return informacionProducto;
        }

    }


}