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

namespace tienda_todo_funciones.forms_prueba
{
    public partial class ventas : Form
    {
        
        

        public ventas()
        {   
            InitializeComponent();
            //id_0|producto_1|precio_de_venta_2|cod_bar_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|productos_elaborados_10|ligar_productos_para_sabor_11|impuesto_12|tipo_producto_para_impuesto_13|
            herramientas_para_elementos_del_form herr_form = new herramientas_para_elementos_del_form();

            string orden_1 = "3" + variables_glob_conf.GG_caracter_separacion[0] + "2" + variables_glob_conf.GG_caracter_separacion[0] + "1" + variables_glob_conf.GG_caracter_separacion[0] + "3" + variables_glob_conf.GG_caracter_separacion[0] + "7";//se repite el 3 en la [3] pocicion por que de hay se agarrara el codigo de barras para procesarlo
            string orden_2 = "1" + variables_glob_conf.GG_caracter_separacion[0] + "2" + variables_glob_conf.GG_caracter_separacion[0] + "3" + variables_glob_conf.GG_caracter_separacion[0] + "3" + variables_glob_conf.GG_caracter_separacion[0] + "7";//se repite el 3 en la [3] pocicion por que de hay se agarrara el codigo de barras para procesarlo
            

            herr_form.fun_txt_prediccion_palabra(Txt_buscar_producto, orden_1);
            herr_form.fun_txt_prediccion_palabra(Txt_nom_producto, orden_2);

            herr_form.fun_txt_procesar_tecleos(Txt_buscar_producto, Lst_ventas);
            string orden_3 = "2" + variables_glob_conf.GG_caracter_separacion[0] + "1" + variables_glob_conf.GG_caracter_separacion[0] + "0" + variables_glob_conf.GG_caracter_separacion[0] + "3" + variables_glob_conf.GG_caracter_separacion[0] + "4";//se repite el 3 en la [3] pocicion por que de hay se agarrara el codigo de barras para procesarlo
            herr_form.fun_txt_nom_produc_pasar_a_txt_codigo(Txt_nom_producto, Txt_buscar_producto,orden_3);

            herr_form.fun_lstb_procesar_tecleos(Lst_ventas);
            herr_form.fun_promociones_procesar_tecleos(Txt_buscar_producto, Lst_ventas,lstb_promociones,lstb_descripcion_promo);

            herr_form.fun_boton_elim(Lst_ventas,Btn_eliminar_seleccionado, "eliminar_seleccionado");
            herr_form.fun_boton_elim(Lst_ventas, Btn_eliminar_todo, "eliminar_todo");
            herr_form.fun_boton_elim(Lst_ventas, Btn_elim_ultimo, "eliminar_ultimo");

        }
    }
}
