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
using tienda_todo_funciones.procesos;

namespace tienda_todo_funciones.desinger
{
    
    public partial class herramientas_para_elementos_del_form : Form
    {
        
        Tex_base bas = new Tex_base();
        Operaciones_textos op_text = new Operaciones_textos();
           
        
        

        public herramientas_para_elementos_del_form()
        {
            InitializeComponent();
            
        }

        //txt------------------------------------------------------------

        AutoCompleteStringCollection autoComplete;
        public void fun_txt_prediccion_palabra(TextBox txt_a_configurar, string orden_conlumnas = null)
        {
            autoComplete = new AutoCompleteStringCollection();
            // Inicializar AutoCompleteStringCollection
            txt_a_configurar.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_a_configurar.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txt_a_configurar.AutoCompleteCustomSource = autoComplete;

            string[] inv = OrdenarColumnas_arreglo(variables_glob_conf.GG_arrays_carga_de_archivos[0], orden_conlumnas, (variables_glob_conf.GG_caracter_separacion[0]+""));

            for (int i = 0; i < inv.Length; i++)
            {
                autoComplete.Add(inv[i]+(variables_glob_conf.GG_caracter_separacion[0]+"")+i);
            }
        }

        
        public void fun_txt_procesar_tecleos(TextBox txt_a_configurar, ListBox lstb_a_configurar)
        {
            
            

            txt_a_configurar.KeyDown += (sender, e) =>
            {
                if (e.KeyValue == (char)Keys.Enter)
                {
                    
                    string[] tex_esplit = txt_a_configurar.Text.Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
                    variables_glob_conf.GG_provedor_anterior_y_codbar[0] = tex_esplit[0];
                    string indice_producto = "";
                    for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
                    {
                        string[] producto_espliteado = variables_glob_conf.GG_arrays_carga_de_archivos[0][i].Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
                        if (variables_glob_conf.GG_provedor_anterior_y_codbar[0] == producto_espliteado[3])
                        {
                            indice_producto = "" + i;
                            break;
                        }
                            
                    }
                    if (indice_producto!="")
                    {
                        fun_lstb_agregar_elim(lstb_a_configurar, txt_a_configurar, "agregar_producto", 1);
                    }

                    else
                    {

                        
                        if (variables_glob_conf.GG_provedor_anterior_y_codbar[1] == null || variables_glob_conf.GG_provedor_anterior_y_codbar[1] == "")
                        {
                            variables_glob_conf.GG_provedor_anterior_y_codbar[1] = "nose";
                        }

                        variables_glob_conf.GG_provedor_anterior_y_codbar[2] = op_text.join_paresido(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[1]), variables_glob_conf.GG_arrays_carga_de_archivos[1]);

                        Ventana_emergente emergente_vent = new Ventana_emergente();
                        variables_glob_conf var_glob = new variables_glob_conf();
                        string datos_introducidos=emergente_vent.Proceso_ventana_emergente(var_glob.GG_ventana_emergente_productos,caracter_spliteo:Convert.ToChar(variables_glob_conf.GG_caracter_separacion[1]));
                        

                        if (datos_introducidos!=variables_glob_conf.GG_caracter_separacion[0])
                        {
                            registro_nuevo_producto(txt_a_configurar, datos_introducidos);
                        }
                        
                    }
                }

                else if (e.KeyValue == (char)(Keys.Add))
                {
                    //e.KeyChar = '\0';//este evita que escrivas cuando usas keypress
                    e.SuppressKeyPress = true;
                    string[] elemento_spliteado = lstb_a_configurar.Items[lstb_a_configurar.Items.Count - 1].ToString().Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));

                    sumar_o_restar_producto(lstb_a_configurar, elemento_spliteado[0], 1);
                }
                else if (e.KeyValue == (char)(Keys.Subtract))
                {
                    //e.KeyChar = '\0';//este evita que escrivas cuando usas keypress
                    e.SuppressKeyPress = true;
                    string[] elemento_spliteado = lstb_a_configurar.Items[lstb_a_configurar.Items.Count - 1].ToString().Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));

                    sumar_o_restar_producto(lstb_a_configurar, elemento_spliteado[0], -1);
                }

            };
        }

        public void fun_txt_nom_produc_pasar_a_txt_codigo(TextBox txt_origen, TextBox txt_destino,string orden)
        {
            
            txt_origen.KeyDown += (sender, e) =>
            {
                if (e.KeyValue == (char)Keys.Enter)
                {
                    string[] info_espliteado = txt_origen.Text.Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
                    if (info_espliteado.Length > 1)
                    {
                        string info_ordenado = OrdenarColumnas_string(txt_origen.Text, orden, variables_glob_conf.GG_caracter_separacion[0]);
                        info_ordenado = op_text.Trimend_paresido(info_ordenado, Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
                        txt_destino.Text = info_ordenado;
                        txt_destino.Focus();
                        txt_origen.Text = "";
                    }
                }

            };
        }

        //fin txt-------------------------------------------------------------
        //listbox--------------------------------------------------------------------
        public void fun_lstb_procesar_tecleos(ListBox lstb_a_configurar)
        {
            lstb_a_configurar.KeyDown += (sender, e) =>
            {
                if (e.KeyValue == (char)(Keys.Add))
                {
                    //e.KeyChar = '\0';//este evita que escrivas cuando usas keypress
                    e.SuppressKeyPress = true;
                    string[] elemento_spliteado = lstb_a_configurar.SelectedItem.ToString().Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));

                    sumar_o_restar_producto(lstb_a_configurar, elemento_spliteado[0], 1);
                }
                else if (e.KeyValue == (char)(Keys.Subtract))
                {
                    //e.KeyChar = '\0';//este evita que escrivas cuando usas keypress
                    e.SuppressKeyPress = true;
                    string[] elemento_spliteado = lstb_a_configurar.SelectedItem.ToString().Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));

                    sumar_o_restar_producto(lstb_a_configurar, elemento_spliteado[0], -1);
                }
            };
        }

        public void fun_lstb_agregar_elim(ListBox lstb_a_configurar, TextBox txt_de_donde_agregara_info, string accion_realisar, double cantidad = 0, string proceso = "")
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
                        string[] info_pa_comparar = txt_de_donde_agregara_info.Text.Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
                        sumar_o_restar_producto(lstb_a_configurar, info_pa_comparar[0], cantidad);

                        txt_de_donde_agregara_info.Text = "";
                    }
                   
                    break;

                //fin agregar_productos---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


                //i------------------------------------------------------------------------------------------------------------------------
                case "eliminar":

                    eliminacion_element_listbox(lstb_a_configurar, proceso);

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
        public void fun_boton_elim(ListBox lstb_a_configurar,Button btn_usar, string proceso )
        {
            btn_usar.Click += (sender, e) =>
            {
                //eliminar_por_item,eliminar_seleccionado,eliminar_ultimo,eliminar_todo
                eliminacion_element_listbox(lstb_a_configurar, proceso);
            };
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

        public string[] agregar_registro_del_array(string[] arreglo, string registro)
        {
            string[] temp = new string[arreglo.Length + 1];

            for (int i = 0; i < arreglo.Length; i++)
            {
                temp[i] = arreglo[i];
            }

            temp[arreglo.Length] = registro;

            return temp;
        }

        public void eliminacion_element_listbox(ListBox lstb_a_configurar, string proceso,string info_extra="")
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
                default:
                    break;
            }
        }

        public void sumar_o_restar_producto(ListBox lstb_a_configurar, string codigo, double cantidad = 1)
        {
            bool esta_el_mismo_producto = false;
            for (int i = 0; i < lstb_a_configurar.Items.Count; i++)
            {
                string[] elemento_espliteado = lstb_a_configurar.Items[i].ToString().Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
                if (codigo == elemento_espliteado[3])
                {
                    esta_el_mismo_producto = true;
                    double resultado = (Convert.ToDouble(elemento_espliteado[5]) + cantidad);
                    elemento_espliteado[5] = "" + resultado;
                    if (resultado<=0)
                    {
                        eliminacion_element_listbox(lstb_a_configurar, "eliminar_por_item", i + "");
                    }
                    else
                    {
                        lstb_a_configurar.Items[i] = string.Join(variables_glob_conf.GG_caracter_separacion[0], elemento_espliteado);
                    }
                    
                }
            }
            if (!esta_el_mismo_producto)
            {
                
                for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
                {
                    string[] producto_invent=variables_glob_conf.GG_arrays_carga_de_archivos[0][i].Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
                    if (codigo== producto_invent[3])
                    {
                       //se agrega +(i + "") + antes de cantidad para saber en que indice del inventario se encuentra
                       lstb_a_configurar.Items.Add(producto_invent[3] + variables_glob_conf.GG_caracter_separacion[0] + producto_invent[2] + variables_glob_conf.GG_caracter_separacion[0] + producto_invent[1] + variables_glob_conf.GG_caracter_separacion[0] + producto_invent[3] + variables_glob_conf.GG_caracter_separacion[0] + producto_invent[4] + variables_glob_conf.GG_caracter_separacion[0] + (i+"") + variables_glob_conf.GG_caracter_separacion[0] + cantidad);
                    }
                }
            }
        }

        public string indice_producto(string cod_bar)
        {
            string existe = "";

            for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
            {
                string[] produc_inv_esplit = variables_glob_conf.GG_arrays_carga_de_archivos[0][i].Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
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


                string[] promo_1_nom_produc_precio = promos[i].Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
                string[] promo_produc = promo_1_nom_produc_precio[1].Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[1]));

                string[] si_cumple_cantidad_pa_promo = new string[promo_produc.Length];

                for (int j = 0; j < promo_produc.Length; j++)
                {
                    string[] datos_producto_promo = promo_produc[j].Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[2]));


                    //chequeo_lista_ventas-----------------------------------------------------------------------------------
                    for (int k = 0; k < Lst_ventas.Items.Count; k++)
                    {
                        string[] produc_list_split = Lst_ventas.Items[k].ToString().Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
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
                        promos[i] = string.Join(variables_glob_conf.GG_caracter_separacion[0], promo_1_nom_produc_precio);

                    }
                    //-------------------------------------------------------------------------------------------
                }
            }
            //colocar en listbox promociones------------------------------------------------------------------
            lstb_promociones.Items.Clear();
            for (int i = 1; i < promos.Length; i++)
            {
                string[] pro_split = promos[i].Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
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

            string[] nuevo_producto_espliteado = registro_agregar.Split(Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));
            bool existe_provedor = false;
            for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[1].Length; i++)
            {
                if (nuevo_producto_espliteado[6]== variables_glob_conf.GG_arrays_carga_de_archivos[1][i])
                {
                    existe_provedor = true;
                    
                }
            }
            if (existe_provedor==false)
            {
                
                bas.Agregar(variables_glob_conf.GG_nom_archivos[1,0], nuevo_producto_espliteado[6] + variables_glob_conf.GG_caracter_separacion[0] + variables_glob_conf.GG_arrays_carga_de_archivos[1].Length);
                variables_glob_conf.GG_arrays_carga_de_archivos[1] = agregar_registro_del_array(variables_glob_conf.GG_arrays_carga_de_archivos[1], nuevo_producto_espliteado[6]);
                variables_glob_conf.GG_provedor_anterior_y_codbar[1] = nuevo_producto_espliteado[6];

            }

            
            string orden_1 = "3" + variables_glob_conf.GG_caracter_separacion[0] + "2" + variables_glob_conf.GG_caracter_separacion[0] + "1" + variables_glob_conf.GG_caracter_separacion[0] + "3" + variables_glob_conf.GG_caracter_separacion[0] + "7";//se repite el 3 en la [3] pocicion por que de hay se agarrara el codigo de barras para procesarlo
            string reg_ordenado_para_txt = OrdenarColumnas_string(registro_agregar, orden_1);
            txt_a_configurar.AutoCompleteCustomSource.Add(reg_ordenado_para_txt+(variables_glob_conf.GG_caracter_separacion[0]+variables_glob_conf.GG_arrays_carga_de_archivos[0].Length+""));

            variables_glob_conf.GG_arrays_carga_de_archivos[0] = agregar_registro_del_array(variables_glob_conf.GG_arrays_carga_de_archivos[0], reg_ordenado_para_txt + (variables_glob_conf.GG_caracter_separacion[0] + variables_glob_conf.GG_arrays_carga_de_archivos[0].Length + ""));
            procesamientos prc = new procesamientos();
            bas.Agregar(variables_glob_conf.GG_nom_archivos[1,0],registro_agregar);
            

        }

    }
}