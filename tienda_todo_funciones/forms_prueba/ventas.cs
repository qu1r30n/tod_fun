
using System.Windows.Forms;
using tienda_todo_funciones.modelos;
using tienda_todo_funciones.desinger;
using System;

namespace tienda_todo_funciones.forms_prueba
{
    public partial class ventas : Form
    {
        model mod = new model();
        
        variables_glob_conf var_glob = new variables_glob_conf();

        string[] G_caracter_separacion = variables_glob_conf.GG_caracter_separacion;

        public ventas()
        {   
            InitializeComponent();
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            herramientas_para_elementos_del_form herr_form = new herramientas_para_elementos_del_form();
            
            
            herr_form.fun_txt_prediccion_palabra(Txt_buscar_producto,"codbar");
            herr_form.fun_txt_prediccion_palabra(Txt_nom_producto, "producto");


            herr_form.fun_txt_procesar_tecleos(Txt_buscar_producto, Lst_ventas, Lbl_nom_product_list, Lbl_costo_product_list, Lbl_cuenta);
            

            herr_form.fun_lstb_procesar_tecleos(Lst_ventas, Lbl_nom_product_list, Lbl_costo_product_list, Lbl_cuenta);
            herr_form.fun_promociones_procesar_tecleos(Txt_buscar_producto, Lst_ventas,lstb_promociones,lstb_descripcion_promo);



            herr_form.fun_botones(Lst_ventas,Btn_eliminar_seleccionado, "eliminar_seleccionado", Lbl_nom_product_list, Lbl_costo_product_list, Lbl_cuenta);
            herr_form.fun_botones(Lst_ventas, Btn_eliminar_todo, "eliminar_todo", Lbl_nom_product_list, Lbl_costo_product_list, Lbl_cuenta);
            herr_form.fun_botones(Lst_ventas, Btn_elim_ultimo, "eliminar_ultimo", Lbl_nom_product_list, Lbl_costo_product_list, Lbl_cuenta);
            herr_form.fun_botones(Lst_ventas, Btn_procesar_venta, "procesar_venta", Lbl_nom_product_list, Lbl_costo_product_list, Lbl_cuenta);
            

            herr_form.fun_txt_nom_produc_pasar_a_txt_codigo(Txt_nom_producto, Txt_buscar_producto, "4");
        }

        private void chb_ventas_compras_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chb_ventas_compras.Checked == false)
            {
                lbl_ventas_compras_resultado.Visible = true;
            }
            else
            {
                lbl_ventas_compras_resultado.Visible = false;
            }
            Txt_buscar_producto.Focus();
        }

        private void ventas_Activated(object sender, System.EventArgs e)
        {
            string cantidad_vendida=(mod.modelo_unico("cantidad_venta_compra_resultado"))+"";
            string[] cantidad_venta_compra=cantidad_vendida.Split(Convert.ToChar(G_caracter_separacion[1]));
            double cant_vent = Convert.ToDouble(cantidad_venta_compra[1]);
            double cant_compra = Convert.ToDouble(cantidad_venta_compra[2]);
            double resultado = cant_vent - cant_compra;
            lbl_ventas_compras_resultado.Text = cant_vent + "-" + cant_compra + "=" + resultado;

            Txt_buscar_producto.Focus();
        }
    }
}
