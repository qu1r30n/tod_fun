using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tienda_todo_funciones.clases;

namespace tienda_todo_funciones.procesos
{
    class procesamientos
    {
        //string direccion_inventario = G_dir_base + variables_glob_conf.GG_nom_archivos[0,0];//direccion_inventario

        
        Tex_base bas = new Tex_base();

        string[] G_caracter_separacion = variables_glob_conf.GG_caracter_separacion;
        
        //----------------------------------------------------------------------------------------------
        public void crear_archivos_inicio_programa()
        {

            
            //archivos del programa
            for (int i = 0; i < variables_glob_conf.GG_dir_nom_archivos.GetLength(0); i++)
            {
                bas.Crear_archivo_y_directorio(variables_glob_conf.GG_dir_nom_archivos[i,0], variables_glob_conf.GG_dir_nom_archivos[i, 1]);
            }

            //archivos de registro
            for (int i = 0; i < variables_glob_conf.GG_dir_reg.GetLength(0); i++)
            {
                bas.Crear_archivo_y_directorio(variables_glob_conf.GG_dir_reg[i, 0], variables_glob_conf.GG_dir_reg[i, 1]);
            }
            //recarga informacion arreglos
            for (int i = 0; i < variables_glob_conf.GG_dir_nom_archivos.GetLength(0); i++)
            {
               variables_glob_conf.GG_arrays_carga_de_archivos[i] = bas.Leer(variables_glob_conf.GG_dir_nom_archivos[i, 0]);
            }

            variables_glob_conf var_glob = new variables_glob_conf();
            string orden_venta = bas.arr_str_conv_nom_a_indice(var_glob.GG_orden_codbar_venta, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(G_caracter_separacion[0]));
            string orden_venta_2 = bas.arr_str_conv_nom_a_indice(var_glob.GG_orden_nom_produc_venta, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(G_caracter_separacion[0]));

            string orden_compra = bas.arr_str_conv_nom_a_indice(var_glob.GG_orden_codbar_compra, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(G_caracter_separacion[0]));
            string orden_compra_2 = bas.arr_str_conv_nom_a_indice(var_glob.GG_orden_nom_produc_compra, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(G_caracter_separacion[0]));

            //carga predicciones en la varable globar GG_autoCompleteCollection que sirve para las predicciones uno es para predictor codbar y el otro para nombre de producto
            for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
            {    
                Operaciones_textos op_tex = new Operaciones_textos();

                string texto_a_agregar_venta = op_tex.ordenar_string_con_caractere_separacion(variables_glob_conf.GG_arrays_carga_de_archivos[0][i], orden_venta, G_caracter_separacion[0]);
                string texto_a_agregar_nom_produc_venta = op_tex.ordenar_string_con_caractere_separacion(variables_glob_conf.GG_arrays_carga_de_archivos[0][i], orden_venta_2, G_caracter_separacion[0]);

                string texto_a_agregar_compra = op_tex.ordenar_string_con_caractere_separacion(variables_glob_conf.GG_arrays_carga_de_archivos[0][i], orden_compra, G_caracter_separacion[0]);
                string texto_a_agregar_nom_produc_compra = op_tex.ordenar_string_con_caractere_separacion(variables_glob_conf.GG_arrays_carga_de_archivos[0][i], orden_compra_2, G_caracter_separacion[0]);

                variables_glob_conf.GG_autoCompleteCollection_codbar_venta.Add(texto_a_agregar_venta);
                variables_glob_conf.GG_autoCompleteCollection_nom_produc_venta.Add(texto_a_agregar_nom_produc_venta);

                variables_glob_conf.GG_autoCompleteCollection_codbar_compra.Add(texto_a_agregar_venta);
                variables_glob_conf.GG_autoCompleteCollection_nom_produc_compra.Add(texto_a_agregar_nom_produc_venta);
            }
            
        }

        //-----------------------------------------------------------------------------------------------

        public void agregar_string_archivo(string direccion, string texto)
        {
            bas.Crear_archivo_y_directorio(direccion);
            bas.Agregar(direccion, texto);

            for (int i = 0; i < variables_glob_conf.GG_dir_nom_archivos.GetLength(0); i++)
            {
                if (direccion == variables_glob_conf.GG_dir_nom_archivos[i, 0])
                {
                    // Agregar el nuevo registro al arreglo correspondiente
                    variables_glob_conf.GG_arrays_carga_de_archivos[i] = agregar_registro_del_array(variables_glob_conf.GG_arrays_carga_de_archivos[i], texto);
                    break;
                }

            }
        }

        //-----------------------------------------------------------------------------------------------
        public void cambiar_archivo_por_arreglo(string direccion, string[] arreglo)
        {
            bas.cambiar_archivo_con_arreglo(direccion, arreglo);
        }


        //-----------------------------------------------------------------------------------------------

        public void procesar_venta(string[]info_lista_venta,string caracter_separacion=null)
        {
            if (caracter_separacion==null)
            {
                caracter_separacion = G_caracter_separacion[0];
            }
            for (int i = 0; i < info_lista_venta.Length; i++)
            {
                string[] detalles_del_producto_lista = info_lista_venta[i].Split(Convert.ToChar(caracter_separacion));
                int indice_producto = Convert.ToInt32(detalles_del_producto_lista[detalles_del_producto_lista.Length - 1]);
                string[] produ_invent = variables_glob_conf.GG_arrays_carga_de_archivos[0][indice_producto].Split(Convert.ToChar(G_caracter_separacion[0]));

                if (produ_invent[5] != detalles_del_producto_lista[0])
                {
                    produ_invent = extraer_info_e_indise(detalles_del_producto_lista[0]);

                    if (produ_invent[0] == null)
                    {
                        return;
                    }
                    //producto|cant_produc|tipo_medida|precio_venta|cod_barras|cantidad|costo_comp|provedor|grupo|no poner nada|cant_produc_x_paquet|tipo_de_producto|ligar_produc_sab|impuestos|parte_de_que_producto
                    produ_invent = produ_invent[0].Split(Convert.ToChar(G_caracter_separacion[0]));


                }



                //producto|cant_produc|tipo_medida|precio_venta|cod_barras|cantidad|costo_comp|provedor|grupo|no poner nada|cant_produc_x_paquet|tipo_de_producto|ligar_produc_sab|impuestos|parte_de_que_producto


            }

        }

        public void procesar_compra(string[] info_lista_venta, string caracter_separacion = null)
        {
            if (caracter_separacion == null)
            {
                caracter_separacion = G_caracter_separacion[0];
            }
            for (int i = 0; i < info_lista_venta.Length; i++)
            {
                string[] detalles_del_producto_lista = info_lista_venta[i].Split(Convert.ToChar(caracter_separacion));
                int indice_producto = Convert.ToInt32(detalles_del_producto_lista[detalles_del_producto_lista.Length - 1]);
                string[] produ_invent = variables_glob_conf.GG_arrays_carga_de_archivos[0][indice_producto].Split(Convert.ToChar(G_caracter_separacion[0]));

                if (produ_invent[5] != detalles_del_producto_lista[0])
                {
                    produ_invent = extraer_info_e_indise(detalles_del_producto_lista[0]);

                    if (produ_invent[0] == null)
                    {
                        return;
                    }
                    //producto|cant_produc|tipo_medida|precio_venta|cod_barras|cantidad|costo_comp|provedor|grupo|no poner nada|cant_produc_x_paquet|tipo_de_producto|ligar_produc_sab|impuestos|parte_de_que_producto
                    produ_invent = produ_invent[0].Split(Convert.ToChar(G_caracter_separacion[0]));


                }



                //producto|cant_produc|tipo_medida|precio_venta|cod_barras|cantidad|costo_comp|provedor|grupo|no poner nada|cant_produc_x_paquet|tipo_de_producto|ligar_produc_sab|impuestos|parte_de_que_producto


            }

        }

        public string[] agregar_registro_del_array(string[] arreglo, string registro)
        {
            string[] temp = { "" };

            if (arreglo==null)
            {
                arreglo = new string[] { "" };
                temp = new string[arreglo.Length];

                for (int i = 0; i < arreglo.Length; i++)
                {
                    temp[i] = arreglo[i];
                }

                temp[arreglo.Length-1] = registro;

            }

            else
            {
                temp = new string[arreglo.Length + 1];
                
                for (int i = 0; i < arreglo.Length; i++)
                {
                    temp[i] = arreglo[i];
                }

                temp[arreglo.Length] = registro;
            }
            
            return temp;
        }

        public string[] chequeo_datos_esten_en_archivo_retorna_todo_el_texto_que_ingresaste(string info_texto, string columna_a_recorer_del_string, int id_arreglo_archivo, string columna_a_recorer_del_archivo, string[] caracter_separacion_del_string = null, string[] caracter_separacion_del_archivo = null)
        {

            if (caracter_separacion_del_string == null)
            {
                caracter_separacion_del_string = G_caracter_separacion;
            }
            if (caracter_separacion_del_archivo == null)
            {
                caracter_separacion_del_archivo = G_caracter_separacion;
            }

            string[] info_spliteado_del_string = info_texto.Split(Convert.ToChar(caracter_separacion_del_string[0]));
            //aqui se extrae el arreglo de la info del las columnas recorridas
            string[] columnas_del_string = columna_a_recorer_del_string.Split(Convert.ToChar(caracter_separacion_del_string[0]));
            
                int id_columna_recorrida_string = 1; //esta es la inicialisasion del for se hace arriba para poder usar la variable despues 
                for (; id_columna_recorrida_string < columnas_del_string.Length; id_columna_recorrida_string++)
                {
                    info_spliteado_del_string = info_spliteado_del_string[Convert.ToInt32(columnas_del_string[id_columna_recorrida_string])].Split(Convert.ToChar(caracter_separacion_del_string[id_columna_recorrida_string]));

                }



            //este es el de extraccion informacion del archivo
            string[] faltantes_a_retornar = null;
            for (int id_resul_del_string = 0; id_resul_del_string < info_spliteado_del_string.Length; id_resul_del_string++)
            {
                

                bool se_encontro_el_producto = false;

                for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo_archivo].Length; i++)
                {
                    string[] info_spliteado_del_archivo = variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo_archivo][i].Split(Convert.ToChar(caracter_separacion_del_archivo[0]));

                    //aqui se extrae el arreglo de la info del las columnas recorridas
                    string[] columnas_del_archivo = columna_a_recorer_del_archivo.Split(Convert.ToChar(caracter_separacion_del_archivo[0]));

                    int id_columna_recorrida_archivo = 1; //esta es la inicialisasion del for se hace arriba para poder usar la variable despues 
                    for (; id_columna_recorrida_archivo < columnas_del_archivo.Length; id_columna_recorrida_archivo++)
                    {
                        info_spliteado_del_archivo = info_spliteado_del_archivo[Convert.ToInt32(columnas_del_archivo[id_columna_recorrida_archivo])].Split(Convert.ToChar(caracter_separacion_del_archivo[id_columna_recorrida_archivo]));

                    }


                    //aqui la comparacion para ver si existe o no
                    if (info_spliteado_del_archivo[0] == info_spliteado_del_string[0])
                    {
                        se_encontro_el_producto = true;
                        break;
                    }

                }


                if (se_encontro_el_producto == false)
                {
                    faltantes_a_retornar = agregar_registro_del_array(faltantes_a_retornar, info_texto);
                }

            }

            return faltantes_a_retornar;
        }


        public string[] chequeo_datos_esten_en_archivo_retorna_solo_el_elemento_buscado(string info_texto, string columna_a_recorer_del_string, int id_arreglo_archivo, string columna_a_recorer_del_archivo, string[] caracter_separacion_del_string = null, string[] caracter_separacion_del_archivo = null)
        {

            if (caracter_separacion_del_string == null)
            {
                caracter_separacion_del_string = G_caracter_separacion;
            }
            if (caracter_separacion_del_archivo == null)
            {
                caracter_separacion_del_archivo = G_caracter_separacion;
            }

            string[] info_spliteado_del_string = info_texto.Split(Convert.ToChar(caracter_separacion_del_string[0]));
            //aqui se extrae el arreglo de la info del las columnas recorridas
            string[] columnas_del_string = columna_a_recorer_del_string.Split(Convert.ToChar(caracter_separacion_del_string[0]));

            int id_columna_recorrida_string = 1; //esta es la inicialisasion del for se hace arriba para poder usar la variable despues 
            for (; id_columna_recorrida_string < columnas_del_string.Length; id_columna_recorrida_string++)
            {
                info_spliteado_del_string = info_spliteado_del_string[Convert.ToInt32(columnas_del_string[id_columna_recorrida_string])].Split(Convert.ToChar(caracter_separacion_del_string[id_columna_recorrida_string]));

            }



            //este es el de extraccion informacion del archivo
            string[] faltantes_a_retornar = null;
            for (int id_resul_del_string = 0; id_resul_del_string < info_spliteado_del_string.Length; id_resul_del_string++)
            {


                    bool se_encontro_el_producto = false;

                for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo_archivo].Length; i++)
                {
                    string[] info_spliteado_del_archivo = variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo_archivo][i].Split(Convert.ToChar(caracter_separacion_del_archivo[0]));

                    //aqui se extrae el arreglo de la info del las columnas recorridas
                    string[] columnas_del_archivo = columna_a_recorer_del_archivo.Split(Convert.ToChar(caracter_separacion_del_archivo[0]));

                    


                    //aqui la comparacion para ver si existe o no
                    if (info_spliteado_del_archivo[0] == info_spliteado_del_string[id_resul_del_string])
                    {
                        se_encontro_el_producto = true;
                        break;
                    }

                }


                if (se_encontro_el_producto == false)
                {
                    faltantes_a_retornar = agregar_registro_del_array(faltantes_a_retornar, info_spliteado_del_string[id_resul_del_string]);
                }

            }

            return faltantes_a_retornar;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public string[] extraer_info_e_indise(string cod_bar, string caracter_separacion = null)
        {

            if (caracter_separacion == null)
            {
                caracter_separacion = G_caracter_separacion[0];
            }

            string[] informacionProducto = { null, null };

            bool encontrado = false;
            for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
            {
                string[] producto_espliteado = variables_glob_conf.GG_arrays_carga_de_archivos[0][i].Split(Convert.ToChar(caracter_separacion));
                if (producto_espliteado[5] == cod_bar)
                {
                    informacionProducto[0] = variables_glob_conf.GG_arrays_carga_de_archivos[0][i];
                    informacionProducto[1] = "" + i;
                    encontrado = true;
                    break;
                }
            }

            if (encontrado == false) {/*hacer algo si no lo encuentra*/}

            return informacionProducto;
        }

    }
}
