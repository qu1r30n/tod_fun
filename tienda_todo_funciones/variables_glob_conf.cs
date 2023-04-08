using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace tienda_todo_funciones
{
    class variables_glob_conf
    {
        //variables globales para configuracion-----------------------------------------------------------
        public string GG_string_transferir = "";
        public List<string> GG_list_transferir = new List<string>();


        public string[] GG_dir_reg =
        {
            "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\dia\\"+ DateTime.Now.ToString("yyyyMMdd") + "_venta.txt",
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\dia\\" + DateTime.Now.ToString("yyyyMMdd") + "_compra.txt",
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\dia\\" + DateTime.Now.ToString("yyyyMMdd") + "_producto.txt",
             //------------------------------------------------------------------------------------------------------------------------
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "_venta.txt",
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "_compra.txt",
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "_producto.txt",
             //-------------------------------------------------------------------------------------------------------------------------
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "_venta.txt",
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "_compra.txt",
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "_producto.txt",
             //----------------------------------------------------------------------------------------------------------------------------
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "_venta.txt",
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "_compra.txt",
             "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "_producto.txt",
             //--------------------------------------------------------------------------------------------
             "reg\\vent_comp_prod\\tod_venta.txt",
             "reg\\vent_comp_prod\\tod_compra.txt",
             "reg\\vent_comp_prod\\tod_producto.txt",
             //------------------------------------------------------------------------------------------------------
             //-------------------------------------------------------------------------------------------------------
             Directory.GetCurrentDirectory() + "\\inf\\ranking\\" + DateTime.Now.ToString("yyyy") + "_ranking.txt"


        };


        //------------------------------------------------------------------------------------------------
        static public string GG_info_intercambiable = "";//despues de usarlo en otra ventana porfavor vuelvelo a dejar la variable con ""

        static public string[] GG_direccion_base =
        {
            "",//este es para el arreglo GG_nom_archivos
            ""//este es dir bace para registros de compra venta y productos por si lo quieres enviara a otra carpeta
        };//solo modificar en esta clase y si se modifica tendras que pasar los directorios a la nueva direccion

        static public string[] GG_nom_archivos =
        {
            "inf\\inventario\\inventario.txt",
            "inf\\inventario\\provedores.txt",
            "inf\\inventario\\promociones.txt",
            "",
            "inf\\ven\\vent.txt"
        };//solo modificar en esta clase y si se modifica tendras que pasar los directorios a la nueva direccion


        static public string[] GG_caracter_separacion =
        {
            "|",
            "°",
            "¬",
            "^"
        };


        
        static public string[] GG_var_modificacion =
        {
            ""//este se supone que sirve paracambiar el nom_archivos
        };
        //------------------------------------------------------------------------------------------------

    }
}
