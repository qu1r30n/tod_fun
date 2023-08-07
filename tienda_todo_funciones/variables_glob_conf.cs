using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using tienda_todo_funciones.clases;

using System.Windows.Forms;

namespace tienda_todo_funciones
{
    class variables_glob_conf
    {
        //statics--------------------------------------------------------------------------------
        static public string[] GG_caracter_separacion =
        {
            "|",
            "°",
            "¬",
            "^",
            "~"//este siempre esta al final lo estoy usando para el area de modelo_unico y para usarlo con el (GG_caracter_separacion.length-1)
        };

        //variables globales para configuracion-----------------------------------------------------------
        public string GG_string_transferir = "";
        public List<string> GG_list_transferir = new List<string>();


        static public string[] GG_direccion_base =
        {
            "",//este es para el arreglo GG_nom_archivos
            ""//este es dir bace para registros de compra venta y productos por si lo quieres enviara a otra carpeta
        };//solo modificar en esta clase y si se modifica tendras que pasar los directorios a la nueva direccion

        //formato   {direccion_de_archivo,fila_inicial_archivo}
        static public string[,] GG_dir_reg =
        {
             /*0*/{ GG_direccion_base[1]+"reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\dia\\"+ DateTime.Now.ToString("yyyyMMdd") + "_venta.txt", "hora_min_seg" + GG_caracter_separacion[0] + "codigo" + GG_caracter_separacion[2] + "nombre" + GG_caracter_separacion[2] + "cantidad" + GG_caracter_separacion[2] + "precio_venta" + GG_caracter_separacion[2] + "precio_compra" + GG_caracter_separacion[1] + "codigo_2" + GG_caracter_separacion[2] + "nombre_2" + GG_caracter_separacion[2] + "cantidad_2" + GG_caracter_separacion[2] + "precio_venta_2" + GG_caracter_separacion[2] + "precio_compra_2" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0] + "pagado_por_promocion" + GG_caracter_separacion[0]},
             /*1*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\dia\\" + DateTime.Now.ToString("yyyyMMdd") + "_compra.txt" ,"hora_min_seg" + GG_caracter_separacion[0] + "codigo" + GG_caracter_separacion[2] + "nombre" + GG_caracter_separacion[2] + "cantidad" + GG_caracter_separacion[2] + "precio_venta" + GG_caracter_separacion[2] + "precio_compra" + GG_caracter_separacion[1] + "codigo_2" + GG_caracter_separacion[2] + "nombre_2" + GG_caracter_separacion[2] + "cantidad_2" + GG_caracter_separacion[2] + "precio_venta_2" + GG_caracter_separacion[2] + "precio_compra_2" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*2*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\dia\\" + DateTime.Now.ToString("yyyyMMdd") + "_producto.txt" ,"codigo" + GG_caracter_separacion[0] + "cantidad" + GG_caracter_separacion[0] + "nombre" + GG_caracter_separacion[0]},
             //------------------------------------------------------------------------------------------------------------------------
             /*3*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "_venta.txt" ,"dia" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*4*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "_compra.txt" ,"dia" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*5*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "_producto.txt" ,"codigo" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             //-------------------------------------------------------------------------------------------------------------------------
             /*6*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "_venta.txt" ,"mes" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*7*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "_compra.txt" ,"mes" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*8*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "_producto.txt" ,"codigo" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             //----------------------------------------------------------------------------------------------------------------------------
             /*9*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "_venta.txt" ,"año" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*10*/{ GG_direccion_base[1]+"reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "_compra.txt", "año" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*11*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\" + DateTime.Now.ToString("yyyy") + "_producto.txt" ,"codigo" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             //--------------------------------------------------------------------------------------------
             /*12*/{ GG_direccion_base[1]+"reg\\vent_comp_prod\\tod_venta.txt", "año" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*13*/{ GG_direccion_base[1]+"reg\\vent_comp_prod\\tod_compra.txt", "año" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             /*14*/{ GG_direccion_base[1] + "reg\\vent_comp_prod\\tod_producto.txt" ,"codigo" + GG_caracter_separacion[0] + "total_venta" + GG_caracter_separacion[0] + "total_compra" + GG_caracter_separacion[0]},
             //------------------------------------------------------------------------------------------------------
             //-------------------------------------------------------------------------------------------------------
             /*15*/{ Directory.GetCurrentDirectory() + "\\inf\\ranking\\" + DateTime.Now.ToString("yyyy") + "_ranking.txt" ,""}
        };


        
        //---------------------------------------------------------------------------------------------

        static public string[] GG_variables_string =
        {
           /*0*/ "",//tex_esplit[0]//codbar
           /*1*/ "",//prov_anterior
           /*2*/ "", //provedores_txt//todos_los_provedores
           /*3*/ "",//impuesto anterior
           /*4*/ "", //impuestos_txt//todos_los_impuestos
           /*5*/ "",//tipo_medida_producto_anterior
           /*6*/ ""//tipo_medida_producto_txt//todos_los_tipos_de_medida
           
        };


        //creando info archivo inicio inventario por si hay modificacion

        //comentarios ejemplo de introducir datos ventana emergente---------------------------------------------------------------------------------
        //1=textbox  1|titulo_texbox|contenido_text_box|restriccion_de_dato|valor_inicial_si_se_modifico|todas_las_opciones_del_combobox_separadas_por_"°"|otras funciones      ejemplo "1|precio venta|0|2|0|prediccion1°prediccion2|no_visible" //el 2 es la restriccion que solo resivira numeros y punto decimal         
        //2=labels   2|titulo_label|abajo_pondra_otro_label_con_el_contenido    ejemplo "2|id|9999"
        //3=boton    3|titulo_del_boton|valor_del_boton|numero_de_Funcion            ejemplo "3|es_paquete|1|0" //cuando oprima el boton devolvera el valor 1 

        //4=combobox "
        //           /*0*/ 4|
        //           /*1*/ titulo_combobox|
        //           /*2*/ valor_inicial_si anteriormente_no_se_a_modificado|
        //           /*3*/ restriccion_de_dato_con_aparte_opcion_4_que_es_proyecto_quetiene_otra_funcion|
        //           /*4*/ " + valor_inicial_si_se_modifico + '|'
        //           /*5*/ + todas_las_opciones_del_combobox_separadas_por_"°"
        //           /*6*/ + otras funciones

        //            ejemplo "4|grupo|1|restricciones|1|1°2°producto_elaborado°4|ocultar_control¬25¬producto_elaborado°reyeno_textbox_ventana"



        //funciones y restricciones txt y cmb ventana_emergente cod:poison
        ////////////////////////////////////////////////////////////////////////
        //                                SI EDITAS                           //
        //                      [,] GG_ventana_emergente_productos            //
        //                             TIENES QUE EDITAR                      //
        //                      RecargarVentanaEmergenteProductos             //
        //                          ES EL DE ABAJITO A ESTE                   //
        ////////////////////////////////////////////////////////////////////////
        static public string[,] GG_ventana_emergente_productos = new string[,]
        {
            /*0*/ { "1", "producto", "" },
            /*1*/ { "1", "cant_produc", "0|2" },
            /*2*/ { "4", "tipo_medida", "nose||" + GG_variables_string[5] + '|' + GG_variables_string[6] },
            /*3*/ { "1", "precio_venta", "0|2" },
            /*4*/ { "2", "cod_barras", GG_variables_string[0] },
            /*5*/ { "1", "cantidad", "1|2" },
            /*6*/ { "1", "costo_comp", "0|2" },
            /*7*/ { "4", "provedor", "nose||" + GG_variables_string[1] + '|' + GG_variables_string[2] },
            /*8*/ { "4", "grupo", "1|restricciones|1|1°2°producto_elaborado°venta_ingrediente|ocultar_control¬23¬producto_elaborado°ocultar_control¬29¬venta_ingrediente" },
            /*9*/ { "2", "no poner nada", "" },
            /*10*/ { "1", "cant_produc_x_paquet", "1|2" },
            /*11*/ { "1", "tipo_de_producto", "||||no_visible°producto_elaborado" },
            /*12*/ { "1", "ligar_produc_sab", "" },
            /*13*/ { "1", "impuestos", "|||||reyeno_textbox_ventana_impu" },
            /*14*/ { "1", "parte_de_que_producto", "||||no_visible°venta_ingrediente" }
        };
        public static void RecargarVentanaEmergenteProductos(string al_finalizar_que_borrar_para_proxima_ventana="")
        {
            GG_ventana_emergente_productos = new string[,]
            {
                /*0*/ { "1", "producto", "" },
                /*1*/ { "1", "cant_produc", "0|2" },
                /*2*/ { "4", "tipo_medida", "nose||" + GG_variables_string[5] + '|' + GG_variables_string[6] },
                /*3*/ { "1", "precio_venta", "0|2" },
                /*4*/ { "2", "cod_barras", GG_variables_string[0] },
                /*5*/ { "1", "cantidad", "1|2" },
                /*6*/ { "1", "costo_comp", "0|2" },
                /*7*/ { "4", "provedor", "nose||" + GG_variables_string[1] + '|' + GG_variables_string[2] },
                /*8*/ { "4", "grupo", "1|restricciones|1|1°2°producto_elaborado°venta_ingrediente|ocultar_control¬23¬producto_elaborado°ocultar_control¬29¬venta_ingrediente" },
                /*9*/ { "2", "no poner nada", "" },
                /*10*/ { "1", "cant_produc_x_paquet", "1|2" },
                /*11*/ { "1", "tipo_de_producto", "||||no_visible°producto_elaborado" },
                /*12*/ { "1", "ligar_produc_sab", "" },
                /*13*/ { "1", "impuestos", "|||||reyeno_textbox_ventana_impu" },
                /*14*/ { "1", "parte_de_que_producto", "||||no_visible°venta_ingrediente" }
            };





            if (al_finalizar_que_borrar_para_proxima_ventana != null)
            {


                string[] datos_a_borrar = al_finalizar_que_borrar_para_proxima_ventana.ToString().Split(Convert.ToChar(GG_caracter_separacion[0]));

                for (int i = 0; i < datos_a_borrar.Length; i++)
                {

                    if (datos_a_borrar[i] == "todo")
                    {
                        GG_variables_string = new[]
                        {
                            /*0*/ "",//tex_esplit[0]//codbar
                            /*1*/ "",//prov_anterior
                            /*2*/ "", //provedores_txt//todos_los_provedores
                            /*3*/ "",//impuesto anterior
                            /*4*/ "", //impuestos_txt//todos_los_impuestos
                            /*5*/ "",//tipo_medida_producto_anterior
                            /*6*/ ""//tipo_medida_producto_txt//todos_los_tipos_de_medida
           
                        };
                    }

                    else if (datos_a_borrar[i] == "") { }

                    else
                    {
                        GG_variables_string[Convert.ToInt32(datos_a_borrar[i])] = "";
                    }

                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------
        public static string columnas_concatenadas()
        {
            string nombresConcatenados = "";

            for (int i = 0; i < GG_ventana_emergente_productos.GetLength(0); i++)
            {
                string nombre = GG_ventana_emergente_productos[i, 1];
                nombresConcatenados += nombre + GG_caracter_separacion[0];
            }

            if (!string.IsNullOrEmpty(nombresConcatenados))
            {
                nombresConcatenados = nombresConcatenados.TrimEnd(Convert.ToChar(GG_caracter_separacion[0]));
            }

            return nombresConcatenados;
        }


        //formato   {direccion_de_archivo,fila_inicial_archivo}
        static public string[,] GG_dir_nom_archivos =
        {
            /*0*/{ GG_direccion_base[0]+"inf\\inventario\\inventario.txt", columnas_concatenadas()},
            /*1*/{ GG_direccion_base[0]+"inf\\inventario\\provedores.txt", "provedor_0" },
            /*2*/{ GG_direccion_base[0]+"inf\\inventario\\promociones.txt","nombre_promocion" + GG_caracter_separacion[2] + "codigo_barras" + GG_caracter_separacion[4] + "cantidad" + GG_caracter_separacion[4] + "nombre_producto" + GG_caracter_separacion[3] + "codigo_barras_2" + GG_caracter_separacion[4] + "cantidad_2" + GG_caracter_separacion[4] + "nombre_producto_2" + GG_caracter_separacion[2] + "precio_anterior " + GG_caracter_separacion[2] + "precio"},
            /*3*/{GG_direccion_base[0]+"inf\\ven\\"+DateTime.Now.ToString("yyyy") + "_vent.txt","fecha" + GG_caracter_separacion[1] +  "ventas" + GG_caracter_separacion[1] + "compras"},
            /*4*/{GG_direccion_base[0]+"inf\\ven\\impuestos.txt", "nombre_impuesto" + GG_caracter_separacion[2] + "porcentage"},
            /*5*/{ GG_direccion_base[0]+"inf\\inventario\\herramientas\\tipos_de_medida.txt", "unidad_medida_0"}

        };//solo modificar en esta clase y si se modifica tendras que pasar los directorios a la nueva direccion

        //------------------------------------------------------------------------------------------------
        static public string GG_info_intercambiable = "";//despues de usarlo en otra ventana porfavor vuelvelo a dejar la variable con ""


        //------------------------------------------------------------------------------------------------

        //prueba parsar todo lo nesesario a variables estaticas----------------------------------------------------------------

        //GG_arrays_carga_de_archivos[0][]//inventario
        //GG_arrays_carga_de_archivos[1][]//provedor
        //GG_arrays_carga_de_archivos[2][]//promocion
        //GG_arrays_carga_de_archivos[3][]//ventas
        //GG_arrays_carga_de_archivos[4][]//impuestos
        //GG_arrays_carga_de_archivos[5][]//tipos_de_medida
        static public string[][] GG_arrays_carga_de_archivos = new string[GG_dir_nom_archivos.GetLength(0)][];


        //-----------------------------------------------------------------------------------------

        static public int[] GG_var_glob_int =
        {
            0,//este es donde va a empesar la lectura de los archivos y arreglos en 0 apareceran tambien los nombres de las columnas
            0,//cantidad_total_de_productos_inventario//este servira para que si dentro de ventana emergente abre otra ventana emergente al momento de guardar el producto incremente la cantidad total para el otro producto 
        };





        public string[] GG_orden_codbar_venta =
            {
                "cod_barras",
                "producto",
                "cant_produc",
                "tipo_medida",
                "cod_barras",
            };

        public string[] GG_orden_nom_produc_venta =
            {
                "producto",
                "cant_produc",
                "tipo_medida",
                "cod_barras",
                "cod_barras",
                
            };


        public string[] GG_orden_codbar_compra =
    {
                "cod_barras",
                "producto",
                "cant_produc",
                "tipo_medida",
                "cod_barras",
            };

        public string[] GG_orden_nom_produc_compra =
            {
                "producto",
                "cant_produc",
                "tipo_medida",
                "cod_barras",
                "cod_barras",
            };


        //----------------------------------------------------------------------------------------------------------------------------------------------


        public static AutoCompleteStringCollection GG_autoCompleteCollection_codbar_venta = new AutoCompleteStringCollection();
        public static AutoCompleteStringCollection GG_autoCompleteCollection_nom_produc_venta = new AutoCompleteStringCollection();

        public static AutoCompleteStringCollection GG_autoCompleteCollection_codbar_compra = new AutoCompleteStringCollection();
        public static AutoCompleteStringCollection GG_autoCompleteCollection_nom_produc_compra = new AutoCompleteStringCollection();

        //-----------------------------------------------------------------------------------------------------------------------------------------------

    }
}
