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
        
        string[] G_caracteres_separacion = variables_glob_conf.GG_caracter_separacion;

        public ventas()
        {
            InitializeComponent();
            //id_0|producto_1|precio_de_venta_2|cod_bar_3|cantidad_4|costo_compra_5|provedor_6|grupo_7|multiusos_8|cantidad_productos_por_paquete_9|ligar_productos_para_sabor_10|impuesto_11|tipo_producto_para_impuesto_12|
            herramientas_para_elementos_del_form herr_form = new herramientas_para_elementos_del_form();

            string orden_1 = "3" + G_caracteres_separacion[0] + "2" + G_caracteres_separacion[0] + "1" + G_caracteres_separacion[0] + "3" + G_caracteres_separacion[0] + "7";//se repite el 3 en la [3] pocicion por que de hay se agarrara el codigo de barras para procesarlo
            string orden_2 = "1" + G_caracteres_separacion[0] + "2" + G_caracteres_separacion[0] + "3" + G_caracteres_separacion[0] + "3" + G_caracteres_separacion[0] + "7";//se repite el 3 en la [3] pocicion por que de hay se agarrara el codigo de barras para procesarlo

            herr_form.fun_txt_prediccion_palabra(Txt_buscar_producto, orden_1, G_caracteres_separacion[0]);
            herr_form.fun_txt_prediccion_palabra(Txt_nom_producto, orden_2, G_caracteres_separacion[0]);

            herr_form.fun_txt_procesar_tecleos(Txt_buscar_producto, Lst_ventas);
            herr_form.fun_txt_procesar_tecleos(Txt_nom_producto, Lst_ventas);

            herr_form.fun_lstb_procesar_tecleos(Lst_ventas);

            herr_form.fun_promociones_procesar_tecleos(Txt_buscar_producto, Lst_ventas,lstb_promociones,lstb_descripcion_promo);


        }
    }
}
