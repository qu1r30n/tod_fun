using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using tienda_todo_funciones.desinger;
using tienda_todo_funciones.clases;

namespace tienda_todo_funciones.forms_prueba
{
    public partial class ventas : Form
    {

        Tex_base bas = new Tex_base();
        variables_glob_conf var_glob = new variables_glob_conf();

        public ventas()
        {   
            InitializeComponent();
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            herramientas_para_elementos_del_form herr_form = new herramientas_para_elementos_del_form();
            string[] orden_arr =
            {
                "0_5",
                "producto_1",
                "cantidad_producto_2",
                "tipo_de_medida_3",
                "0_5",
                "precio_de_venta_4",
                "grupo_9"

            };
            string orden_1 = bas.arr_str_conv_nom_a_indice(orden_arr, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));

            orden_arr = new string[]
            {
                "producto_1",
                "cantidad_producto_2",
                "tipo_de_medida_3",
                "0_5",
                "0_5",
                "precio_de_venta_4",
                "grupo_9"
            };
            string orden_2 = bas.arr_str_conv_nom_a_indice(orden_arr, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(variables_glob_conf.GG_caracter_separacion[0]));


            herr_form.fun_txt_prediccion_palabra(Txt_buscar_producto, orden_1);
            herr_form.fun_txt_prediccion_palabra(Txt_nom_producto, orden_2);

            herr_form.fun_txt_procesar_tecleos(Txt_buscar_producto, Lst_ventas);
            
            
            herr_form.fun_txt_nom_produc_pasar_a_txt_codigo(Txt_nom_producto, Txt_buscar_producto,"4");

            herr_form.fun_lstb_procesar_tecleos(Lst_ventas);
            herr_form.fun_promociones_procesar_tecleos(Txt_buscar_producto, Lst_ventas,lstb_promociones,lstb_descripcion_promo);

            herr_form.fun_boton_elim(Lst_ventas,Btn_eliminar_seleccionado, "eliminar_seleccionado");
            herr_form.fun_boton_elim(Lst_ventas, Btn_eliminar_todo, "eliminar_todo");
            herr_form.fun_boton_elim(Lst_ventas, Btn_elim_ultimo, "eliminar_ultimo");

        }
    }
}
