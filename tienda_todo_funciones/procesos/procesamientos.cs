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
        // funcion para crear archivos al inicio del programa
        public void crear_archivos_inicio_programa()
        {


            // Crear archivos del programa
            for (int i = 0; i < variables_glob_conf.GG_dir_nom_archivos.GetLength(0); i++)
            {
                bas.Crear_archivo_y_directorio(variables_glob_conf.GG_dir_nom_archivos[i, 0], variables_glob_conf.GG_dir_nom_archivos[i, 1]);
            }

            // Crear archivos de registro
            for (int i = 0; i < variables_glob_conf.GG_dir_reg.GetLength(0); i++)
            {
                bas.Crear_archivo_y_directorio(variables_glob_conf.GG_dir_reg[i, 0], variables_glob_conf.GG_dir_reg[i, 1]);
            }
            // Recargar información de arreglos desde archivos
            for (int i = 0; i < variables_glob_conf.GG_dir_nom_archivos.GetLength(0); i++)
            {
                variables_glob_conf.GG_arrays_carga_de_archivos[i] = bas.Leer(variables_glob_conf.GG_dir_nom_archivos[i, 0]);
            }

            // Recargar información de arreglos desde registros
            for (int i = 0; i < variables_glob_conf.GG_dir_reg.GetLength(0); i++)
            {
                variables_glob_conf.GG_arrays_carga_de_registros[i] = bas.Leer(variables_glob_conf.GG_dir_reg[i, 0]);
            }

            variables_glob_conf var_glob = new variables_glob_conf();

            // orden que el text box venta deve tener por codigo y por nombre
            string orden_venta = bas.arr_str_conv_nom_a_indice(var_glob.GG_orden_codbar_venta, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(G_caracter_separacion[0]));
            string orden_venta_2 = bas.arr_str_conv_nom_a_indice(var_glob.GG_orden_nom_produc_venta, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(G_caracter_separacion[0]));

            // orden que el text box venta deve tener por codigo y por nombre
            string orden_compra = bas.arr_str_conv_nom_a_indice(var_glob.GG_orden_codbar_compra, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(G_caracter_separacion[0]));
            string orden_compra_2 = bas.arr_str_conv_nom_a_indice(var_glob.GG_orden_nom_produc_compra, variables_glob_conf.GG_arrays_carga_de_archivos[0][0], Convert.ToChar(G_caracter_separacion[0]));

            // Cargar predicciones en las variables globales GG_autoCompleteCollection (para predictor de código de barras y nombre de producto)
            for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
            {
                Operaciones_textos op_tex = new Operaciones_textos();
                
                // Obtener texto ordenado para ventas y compras
                string texto_a_agregar_venta = op_tex.ordenar_string_con_caractere_separacion(variables_glob_conf.GG_arrays_carga_de_archivos[0][i], orden_venta, G_caracter_separacion[0]);
                string texto_a_agregar_nom_produc_venta = op_tex.ordenar_string_con_caractere_separacion(variables_glob_conf.GG_arrays_carga_de_archivos[0][i], orden_venta_2, G_caracter_separacion[0]);

                string texto_a_agregar_compra = op_tex.ordenar_string_con_caractere_separacion(variables_glob_conf.GG_arrays_carga_de_archivos[0][i], orden_compra, G_caracter_separacion[0]);
                string texto_a_agregar_nom_produc_compra = op_tex.ordenar_string_con_caractere_separacion(variables_glob_conf.GG_arrays_carga_de_archivos[0][i], orden_compra_2, G_caracter_separacion[0]);

                // Agregar a las variables globales GG_autoCompleteCollection
                variables_glob_conf.GG_autoCompleteCollection_codbar_venta.Add(texto_a_agregar_venta);
                variables_glob_conf.GG_autoCompleteCollection_nom_produc_venta.Add(texto_a_agregar_nom_produc_venta);

                variables_glob_conf.GG_autoCompleteCollection_codbar_compra.Add(texto_a_agregar_venta);
                variables_glob_conf.GG_autoCompleteCollection_nom_produc_compra.Add(texto_a_agregar_nom_produc_venta);
            }

        }

        //-----------------------------------------------------------------------------------------------


        public void modelo_de_registros_venta(string informacion_venta, string[] caracter_separacion = null)
        {
            if (caracter_separacion == null)
            {
                caracter_separacion = G_caracter_separacion;
            }
            string[] informacion_venta_spliteado = informacion_venta.Split(Convert.ToChar(caracter_separacion[0]));
            string[] productos_spliteado = informacion_venta_spliteado[4].Split(Convert.ToChar(caracter_separacion[1]));
            //registro producto
            for (int i = 0; i < productos_spliteado.Length; i++)
            {
                string[] datos_producto = productos_spliteado[i].Split(Convert.ToChar(caracter_separacion[2]));

                bas.si_existe_suma_sino_agega_extra(variables_glob_conf.GG_dir_reg[2, 0], 0, datos_producto[0], "1", datos_producto[2], datos_producto[0] + caracter_separacion[0] + datos_producto[2] + caracter_separacion[0] + datos_producto[1]);
                bas.si_existe_suma_sino_agega_extra(variables_glob_conf.GG_dir_reg[5, 0], 0, datos_producto[0], "1", datos_producto[2], datos_producto[0] + caracter_separacion[0] + datos_producto[2] + caracter_separacion[0] + datos_producto[1]);
                bas.si_existe_suma_sino_agega_extra(variables_glob_conf.GG_dir_reg[8, 0], 0, datos_producto[0], "1", datos_producto[2], datos_producto[0] + caracter_separacion[0] + datos_producto[2] + caracter_separacion[0] + datos_producto[1]);
                bas.si_existe_suma_sino_agega_extra(variables_glob_conf.GG_dir_reg[11, 0], 0, datos_producto[0], "1", datos_producto[2], datos_producto[0] + caracter_separacion[0] + datos_producto[2] + caracter_separacion[0] + datos_producto[1]);
                bas.si_existe_suma_sino_agega_extra(variables_glob_conf.GG_dir_reg[14, 0], 0, datos_producto[0], "1", datos_producto[2], datos_producto[0] + caracter_separacion[0] + datos_producto[2] + caracter_separacion[0] + datos_producto[1]);

            }


            bas.Agregar(variables_glob_conf.GG_dir_reg[0, 0], informacion_venta);
            string[] impuestos_spliteado = informacion_venta_spliteado[3].Split(Convert.ToChar(caracter_separacion[1]));


            string[] productos = informacion_venta_spliteado[4].Split(Convert.ToChar(caracter_separacion[1]));


            string[,] info_lugar_a_buscar = new string[,]
            {
                { DateTime.Now.ToString("yyyyMMdd"), "3" },
                { DateTime.Now.ToString("yyyyMM"), "6" },
                { DateTime.Now.ToString("yyyy"), "9" }
            };

            for (int i = 0; i < info_lugar_a_buscar.GetLength(0); i++)
            {
                informacion_venta_spliteado[0] = info_lugar_a_buscar[i, 0];
                for (int j = 0; j < productos.Length; j++)
                {
                    string[] datos_producto = productos[j].Split(Convert.ToChar(caracter_separacion[2]));

                    editar_string_registro_y_arreglo_todo_conjunto("incrementar_decrementar_celda_sino_existe_agrega_texto", Convert.ToInt32(info_lugar_a_buscar[i, 1]), datos_producto[2], id_o_nom_columna_comparar: 0, comparacion: informacion_venta_spliteado[0], id_o_nom_columna_editar: 1, si_no_existe_info_que_texto_agregar: string.Join(caracter_separacion[0], informacion_venta_spliteado));
                    editar_string_registro_y_arreglo_todo_conjunto("incrementar_decrementar_celda_sino_existe_agrega_texto", Convert.ToInt32(info_lugar_a_buscar[i, 1]), datos_producto[2], id_o_nom_columna_comparar: 0, comparacion: informacion_venta_spliteado[0], id_o_nom_columna_editar: 2, si_no_existe_info_que_texto_agregar: string.Join(caracter_separacion[0], informacion_venta_spliteado));
                    editar_string_registro_y_arreglo_todo_conjunto("incrementar_decrementar_celda_sino_existe_agrega_texto", Convert.ToInt32(info_lugar_a_buscar[i, 1]), datos_producto[2], id_o_nom_columna_comparar: 0, comparacion: informacion_venta_spliteado[0], id_o_nom_columna_editar: 3, si_no_existe_info_que_texto_agregar: string.Join(caracter_separacion[0], informacion_venta_spliteado));


                }
            }

        }    
            

            
            


        


        // Método para agregar un texto a un archivo y actualizar un arreglo
        public void agregar_string_archivo(string direccion, string texto)
        {
            // Crear el archivo si no existe crea directorios, archivos y agregar el texto
            bas.Crear_archivo_y_directorio(direccion);
            bas.Agregar(direccion, texto);

            // Recorrer los nombres de archivos en el arreglo global GG_dir_nom_archivos
            //  si coincide el nombre el agrega nuevo registro
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


        public void agregar_string_registro(string direccion, string texto)
        {
            // Crear el archivo si no existe crea directorios, archivos y agregar el texto
            bas.Crear_archivo_y_directorio(direccion);
            bas.Agregar(direccion, texto);

            // Recorrer los nombres de archivos en el arreglo global GG_dir_nom_archivos
            //  si coincide el nombre el agrega nuevo registro
            for (int i = 0; i < variables_glob_conf.GG_dir_reg.GetLength(0); i++)
            {
                if (direccion == variables_glob_conf.GG_dir_reg[i, 0])
                {
                    // Agregar el nuevo registro al arreglo correspondiente
                    variables_glob_conf.GG_arrays_carga_de_registros[i] = agregar_registro_del_array(variables_glob_conf.GG_arrays_carga_de_registros[i], texto);
                    break;
                }

            }
        }

        // entrada Método para editar un string en un archivo y actualizar un arreglo en conjunto
        public void editar_string_archivo_y_arreglo_todo_conjunto(string operacion, object dir_arch_o_id, string texto = null, int id_fila = -1, object id_o_nom_columna_comparar = null, string comparacion = null, string[] caracter_separacion = null, object id_o_nom_columna_editar = null,string si_no_existe_info_que_texto_agregar=null)
        {
            // Variables para almacenar los índices de los arreglos y columnas
            // no esta el de fila por que es parametro
            int id_arreglo = -1;
            int id_columna_comparar = -1;
            int id_columna_editar = -1;

            // Si el arreglo de separación de caracteres no está definido, usar el valor predeterminado
            if (caracter_separacion == null)
            {
                caracter_separacion = G_caracter_separacion;
            }
            //sacamos id_del_arreglo------------------------------------------------------------
            bool se_encontro_arreglo = false;

            if (dir_arch_o_id is string)
            {
                string temp = dir_arch_o_id.ToString();
                for (int i = 0; i < variables_glob_conf.GG_dir_nom_archivos.GetLength(0); i++)
                {
                    if (temp == variables_glob_conf.GG_dir_nom_archivos[i, 0])
                    {
                        id_arreglo = i;
                        se_encontro_arreglo = true;
                        break;
                    }
                }
            }

            else if (dir_arch_o_id is int)
            {
                id_arreglo = Convert.ToInt32(dir_arch_o_id);
                string tem = variables_glob_conf.GG_dir_nom_archivos[id_arreglo, 0];//esto solo es para checar que exista el arreglo
                se_encontro_arreglo = true;

            }
            //sacamos id_fila--------------------------------------------------------------------------



            bool se_encontro_fila = false;
            if (id_fila >= 0)
            {
                se_encontro_fila = true;
            }
            //sacar id_fila con columna_comparar ------------------------------------------------------------------------------------

            bool se_encontro_columna_comparar = false;
            if (id_o_nom_columna_comparar is string)
            {
                string[] nom_columnas = variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][0].Split(Convert.ToChar(caracter_separacion[0]));
                for (int i = 0; i < nom_columnas.Length; i++)
                {

                    if ((string)id_o_nom_columna_comparar == nom_columnas[i])
                    {
                        id_columna_comparar = i;
                        se_encontro_columna_comparar = true;
                        break;
                    }
                }

                if (se_encontro_fila == false)
                {
                    string[] columnas_a_recorrer;

                    if (se_encontro_columna_comparar==true)
                    {
                        columnas_a_recorrer = new string[] { "0", ""+id_columna_comparar };
                    }
                    else
                    {
                        columnas_a_recorrer = id_o_nom_columna_comparar.ToString().Split(Convert.ToChar(G_caracter_separacion[0]));
                    }
                    

                    for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo].Length; i++)
                    {
                        string[] columnas_de_la_fila_comp = variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][i].Split(Convert.ToChar(caracter_separacion[0]));
                        for (int j = 1; j < columnas_a_recorrer.Length; j++)
                        {
                            columnas_de_la_fila_comp = columnas_de_la_fila_comp[Convert.ToInt32(columnas_a_recorrer[j])].Split(Convert.ToChar(G_caracter_separacion[j]));
                            if (j == columnas_a_recorrer.Length - 1)
                            {
                                if (comparacion == columnas_de_la_fila_comp[0])
                                {
                                    id_fila = i;
                                    se_encontro_fila = true;
                                    break;
                                }
                            }
                            
                        }
                        if (se_encontro_fila)
                        {
                            break;
                        }
                    }

                }



            }

            else if (id_o_nom_columna_comparar is int)
            {
                for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo].Length; i++)
                {
                    string[] info_columnas = variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][i].Split(Convert.ToChar(G_caracter_separacion[0]));
                    if (comparacion == info_columnas[(int)id_o_nom_columna_comparar])
                    {
                        id_fila = i;
                        se_encontro_fila = true;
                    }
                }

                
            }

            //sacar columna a editar-------------------------------------------------------------------------------------------------------------------------
            bool se_encontro_columna_editar = false;
            if (id_o_nom_columna_editar is string)
            {
                string[] nom_columnas = variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][0].Split(Convert.ToChar(caracter_separacion[0]));
                for (int i = 0; i < nom_columnas.Length; i++)
                {

                    if ((string)id_o_nom_columna_editar == nom_columnas[i])
                    {
                        id_columna_editar = i;
                        se_encontro_columna_editar = true;
                        break;
                    }
                }


                // mod__ talves falte columnas a recorrer  para checar y como se hace en la funcion incrementar_decrementar_stringElementoEnMultiArregloRecursivo
                //por que si hay que cambiar de lugar el de editar tendremos que hacerlo desde la llamada a la funcion
                //incrementar_decrementar_stringElementoEnMultiArregloRecursivo
                //y
                //EditarstringElementoEnMultiArregloRecursivo
                string[] columnas_a_recorrer;

                if (se_encontro_columna_editar == true)
                {
                    columnas_a_recorrer = new string[] { "0", "" + id_columna_editar };
                }

            }

            else if (id_o_nom_columna_editar is int)
            {
                id_columna_editar = (int)id_o_nom_columna_editar;

            }

            //-------------------------------------------------------------------------------------------------------------------------
            if (operacion == "editar_celda")
            {
                variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila] = EditarstringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila], "0|" + id_columna_editar, texto);
                //chequeo_datos_esten_en_archivo_retorna_toda_la_fila(comparacion, "" + id_o_nom_columna,);
                //nose_como_llamarlo(dir_arch_o_id,id_fila,id_o_nom_columna,comparacion,texto);
            }
            else if (operacion == "editar_fila")
            {
                //chequeo_datos_esten_en_archivo_retorna_toda_la_fila(comparacion, id_o_nom_columna);

                variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila] = texto;

                //nose_como_llamarlo(dir_arch_o_id, id_fila, id_o_nom_columna, comparacion, texto);
            }

            else if (operacion == "incrementar_decrementar_celda")
            {
                variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila]= incrementar_decrementar_stringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila], "0|" + id_columna_editar, texto);
                //EditarstringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila], "0|" + id_columna_editar, texto);
            }

            else if (operacion == "incrementar_decrementar_celda_sino_existe_agrega_texto")
            {
                if (id_arreglo >= -1 && id_fila >= -1) 
                {

                    //string a = incrementar_decrementar_comparando_datos_stringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila], "0|" + id_columna_editar, "0","1",);
                    //variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila] =
                }
                else
                {
                    if (si_no_existe_info_que_texto_agregar!=null)
                    {
                        agregar_string_archivo(variables_glob_conf.GG_dir_nom_archivos[id_arreglo, 0], si_no_existe_info_que_texto_agregar);
                    }
                    
                }
                //EditarstringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo][id_fila], "0|" + id_columna_editar, texto);
            }

            bas.cambiar_archivo_con_arreglo(variables_glob_conf.GG_dir_nom_archivos[id_arreglo, 0], variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo]);

        }

        public void editar_string_registro_y_arreglo_todo_conjunto(string operacion, object dir_arch_o_id, string texto = null, int id_fila = -1, object id_o_nom_columna_comparar = null, string comparacion = null, string[] caracter_separacion = null, object id_o_nom_columna_editar = null, string si_no_existe_info_que_texto_agregar = null)
        {
            // Variables para almacenar los índices de los arreglos y columnas
            // no esta el de fila por que es parametro
            int id_arreglo = -1;
            int id_columna_comparar = -1;
            int id_columna_editar = -1;

            // Si el arreglo de separación de caracteres no está definido, usar el valor predeterminado
            if (caracter_separacion == null)
            {
                caracter_separacion = G_caracter_separacion;
            }
            //sacamos id_del_arreglo------------------------------------------------------------
            bool se_encontro_arreglo = false;

            if (dir_arch_o_id is string)
            {
                string temp = dir_arch_o_id.ToString();
                for (int i = 0; i < variables_glob_conf.GG_dir_reg.GetLength(0); i++)
                {
                    if (temp == variables_glob_conf.GG_dir_reg[i, 0])
                    {
                        id_arreglo = i;
                        se_encontro_arreglo = true;
                        break;
                    }
                }
            }

            else if (dir_arch_o_id is int)
            {
                id_arreglo = Convert.ToInt32(dir_arch_o_id);
                string tem = variables_glob_conf.GG_dir_reg[id_arreglo, 0];//esto solo es para checar que exista el arreglo
                se_encontro_arreglo = true;

            }
            //sacamos id_fila--------------------------------------------------------------------------

            bool se_encontro_fila = false;
            if (id_fila >= 0)
            {
                se_encontro_fila = true;
            }
            //sacar id_fila con columna_comparar ------------------------------------------------------------------------------------

            bool se_encontro_columna_comparar = false;
            if (id_o_nom_columna_comparar is string)
            {
                string[] nom_columnas = variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][0].Split(Convert.ToChar(caracter_separacion[0]));
                for (int i = 0; i < nom_columnas.Length; i++)
                {

                    if ((string)id_o_nom_columna_comparar == nom_columnas[i])
                    {
                        id_columna_comparar = i;
                        se_encontro_columna_comparar = true;
                        break;
                    }
                }

                if (se_encontro_fila == false)
                {
                    string[] columnas_a_recorrer;

                    if (se_encontro_columna_comparar == true)
                    {
                        columnas_a_recorrer = new string[] { "0", "" + id_columna_comparar };
                    }
                    else
                    {
                        columnas_a_recorrer = id_o_nom_columna_comparar.ToString().Split(Convert.ToChar(G_caracter_separacion[0]));
                    }


                    for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo].Length; i++)
                    {
                        string[] columnas_de_la_fila_comp = variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][i].Split(Convert.ToChar(caracter_separacion[0]));
                        for (int j = 1; j < columnas_a_recorrer.Length; j++)
                        {
                            columnas_de_la_fila_comp = columnas_de_la_fila_comp[Convert.ToInt32(columnas_a_recorrer[j])].Split(Convert.ToChar(G_caracter_separacion[j]));
                            if (j == columnas_a_recorrer.Length - 1)
                            {
                                if (comparacion == columnas_de_la_fila_comp[0])
                                {
                                    id_fila = i;
                                    se_encontro_fila = true;
                                    break;
                                }
                            }

                        }
                        if (se_encontro_fila)
                        {
                            break;
                        }
                    }

                }



            }

            else if (id_o_nom_columna_comparar is int)
            {
                for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo].Length; i++)
                {
                    string[] info_columnas = variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][i].Split(Convert.ToChar(G_caracter_separacion[0]));
                    if (comparacion == info_columnas[(int)id_o_nom_columna_comparar])
                    {
                        id_fila = i;
                        se_encontro_fila = true;
                    }
                }

                if (se_encontro_fila == false)
                {
                    string[] columnas_a_recorrer;

                    if (se_encontro_columna_comparar == true)
                    {
                        columnas_a_recorrer = new string[] { "0", "" + id_columna_comparar };
                    }
                    else
                    {
                        columnas_a_recorrer = id_o_nom_columna_comparar.ToString().Split(Convert.ToChar(G_caracter_separacion[0]));
                    }


                    for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo].Length; i++)
                    {
                        string[] columnas_de_la_fila_comp = variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][i].Split(Convert.ToChar(caracter_separacion[0]));
                        for (int j = 1; j < columnas_a_recorrer.Length; j++)
                        {
                            columnas_de_la_fila_comp = columnas_de_la_fila_comp[Convert.ToInt32(columnas_a_recorrer[j])].Split(Convert.ToChar(G_caracter_separacion[j]));
                            if (j == columnas_a_recorrer.Length - 1)
                            {
                                if (comparacion == columnas_de_la_fila_comp[0])
                                {
                                    id_fila = i;
                                    se_encontro_fila = true;
                                    break;
                                }
                            }

                        }
                        if (se_encontro_fila)
                        {
                            break;
                        }
                    }

                }
            }

            //sacar columna a editar-------------------------------------------------------------------------------------------------------------------------
            bool se_encontro_columna_editar = false;
            if (id_o_nom_columna_editar is string)
            {
                string[] nom_columnas = variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][0].Split(Convert.ToChar(caracter_separacion[0]));
                for (int i = 0; i < nom_columnas.Length; i++)
                {

                    if ((string)id_o_nom_columna_editar == nom_columnas[i])
                    {
                        id_columna_editar = i;
                        se_encontro_columna_editar = true;
                        break;
                    }
                }


                // mod__ talves falte columnas a recorrer  para checar y como se hace en la funcion incrementar_decrementar_stringElementoEnMultiArregloRecursivo
                //por que si hay que cambiar de lugar el de editar tendremos que hacerlo desde la llamada a la funcion
                //incrementar_decrementar_stringElementoEnMultiArregloRecursivo
                //y
                //EditarstringElementoEnMultiArregloRecursivo
                string[] columnas_a_recorrer;

                if (se_encontro_columna_editar == true)
                {
                    columnas_a_recorrer = new string[] { "0", "" + id_columna_editar };
                }

            }

            else if (id_o_nom_columna_editar is int)
            {
                id_columna_editar = (int)id_o_nom_columna_editar;

            }

            //-------------------------------------------------------------------------------------------------------------------------
            if (operacion == "editar_celda")
            {
                variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila] = EditarstringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila], "0|" + id_columna_editar, texto);
                //chequeo_datos_esten_en_archivo_retorna_toda_la_fila(comparacion, "" + id_o_nom_columna,);
                //nose_como_llamarlo(dir_arch_o_id,id_fila,id_o_nom_columna,comparacion,texto);
            }
            else if (operacion == "editar_fila")
            {
                //chequeo_datos_esten_en_archivo_retorna_toda_la_fila(comparacion, id_o_nom_columna);

                variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila] = texto;

                //nose_como_llamarlo(dir_arch_o_id, id_fila, id_o_nom_columna, comparacion, texto);
            }

            else if (operacion == "incrementar_decrementar_celda")
            {
                variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila] = incrementar_decrementar_stringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila], "0|" + id_columna_editar, texto);
                //EditarstringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila], "0|" + id_columna_editar, texto);
            }

            else if (operacion == "incrementar_decrementar_celda_sino_existe_agrega_texto")
            {
                if (id_arreglo > -1 && id_fila > -1)
                {
                    
                    variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila] = incrementar_decrementar_comparando_datos_stringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila], "0|" + id_columna_editar, "0","1",texto);
                }
                else
                {
                    if (si_no_existe_info_que_texto_agregar != null)
                    {
                        agregar_string_registro(variables_glob_conf.GG_dir_reg[id_arreglo, 0], si_no_existe_info_que_texto_agregar);
                    }

                }
                //EditarstringElementoEnMultiArregloRecursivo(variables_glob_conf.GG_arrays_carga_de_registros[id_arreglo][id_fila], "0|" + id_columna_editar, texto);
            }

            

        }
        // Método para editar un elemento en un string de formato arreglo multiple, recursivo
        public string EditarstringElementoEnMultiArregloRecursivo(string texto, object columnas_a_recorrer, string texto_a_sustituir, string[] caracterSeparacion = null)
        {
            //si no introdujo caracteres de separacion se usara predeterminado
            if (caracterSeparacion == null)
            {
                caracterSeparacion = G_caracter_separacion;
            }

            string[] espliteado_columnas_recorrer = { };

            //Sí es un string lo splitea Este normalmente es al inicio de la función 
            if (columnas_a_recorrer is string)
            {
                espliteado_columnas_recorrer = columnas_a_recorrer.ToString().Split(Convert.ToChar(caracterSeparacion[0]));
            }
            else if (columnas_a_recorrer is string[] temp)
            {
                
                espliteado_columnas_recorrer = temp;
            }
            
            string[] espliteado_texto = texto.Split(Convert.ToChar(caracterSeparacion[0]));

            string texto_a_retornar = "";
            //En esta parte Se inicia desde el segundo elemento y se guardan los caracteres y
            //las columnas para sí hay otro elemento En el arreglo múltiple 
            if (espliteado_columnas_recorrer.Length > 1)
            {
                string[] tem_array_col_recorrer = new string[espliteado_columnas_recorrer.Length - 1];
                string[] tem_array_caracter_separacion = new string[caracterSeparacion.Length - 1];
                for (int i = 1; i < espliteado_columnas_recorrer.Length; i++)
                {
                    tem_array_col_recorrer[i - 1] = espliteado_columnas_recorrer[i];
                    tem_array_caracter_separacion[i - 1] = caracterSeparacion[i];
                }

                
                texto_a_retornar = espliteado_texto[Convert.ToInt32(tem_array_col_recorrer[0])];

                espliteado_texto[Convert.ToInt32(espliteado_columnas_recorrer[1])]=EditarstringElementoEnMultiArregloRecursivo(texto_a_retornar, tem_array_col_recorrer, texto_a_sustituir,tem_array_caracter_separacion); // Llamada recursiva
            }
            else
            {
                //Sí es el último elemento del arreglo múltiple modifica el texto
                //para finalmente concatenarlos Con Los otros textos a retornar de la función recursiva 
                texto_a_retornar = texto_a_sustituir;
                espliteado_texto[0] = texto_a_retornar;
            }
            //Este concatena todos los resultados para el final ya tener toda la fila recuerda que es un proceso recursivo 
            texto_a_retornar = string.Join(caracterSeparacion[0], espliteado_texto);
            return texto_a_retornar;
        }

        // Método para incrementar un elemento en un string de formato arreglo multiple, recursivo
        public string incrementar_decrementar_stringElementoEnMultiArregloRecursivo(string texto, object columnas_a_recorrer, string cantidad_a_incrementar_decrementar, string[] caracterSeparacion = null)
        {
            //si no introdujo caracteres de separacion se usara predeterminado
            if (caracterSeparacion == null)
            {
                caracterSeparacion = G_caracter_separacion;
            }

            string[] espliteado_columnas_recorrer = { };
            //Sí es un string lo splitea Este normalmente es al inicio de la función 
            if (columnas_a_recorrer is string)
            {
                espliteado_columnas_recorrer = columnas_a_recorrer.ToString().Split(Convert.ToChar(caracterSeparacion[0]));
            }
            else if (columnas_a_recorrer is string[] temp)
            {

                espliteado_columnas_recorrer = temp;
            }
            string[] espliteado_texto = texto.Split(Convert.ToChar(caracterSeparacion[0]));

            //En esta parte Se inicia desde el segundo elemento y se guardan los caracteres y
            //las columnas para sí hay otro elemento En el arreglo múltiple 
            string texto_a_retornar = "";
            if (espliteado_columnas_recorrer.Length > 1)
            {
                string[] tem_array_col_recorrer = new string[espliteado_columnas_recorrer.Length - 1];
                string[] tem_array_caracter_separacion = new string[caracterSeparacion.Length - 1];
                for (int i = 1; i < espliteado_columnas_recorrer.Length; i++)
                {
                    tem_array_col_recorrer[i - 1] = espliteado_columnas_recorrer[i];
                    tem_array_caracter_separacion[i - 1] = caracterSeparacion[i];
                }

                //espliteado_texto = texto.Split(Convert.ToChar(tem_array_caracter_separacion[0]));
                texto_a_retornar = espliteado_texto[Convert.ToInt32(tem_array_col_recorrer[0])];

                espliteado_texto[Convert.ToInt32(espliteado_columnas_recorrer[1])]=incrementar_decrementar_stringElementoEnMultiArregloRecursivo(texto_a_retornar, tem_array_col_recorrer, cantidad_a_incrementar_decrementar,tem_array_caracter_separacion); // Llamada recursiva
                
                
            }

            else
            {
                //Sí es el último elemento del arreglo múltiple modifica el texto
                //para finalmente concatenarlos Con Los otros textos a retornar de la función recursiva 
                texto_a_retornar = ""+(Convert.ToDouble(espliteado_texto[0]) +Convert.ToDouble(cantidad_a_incrementar_decrementar));
                espliteado_texto[0] = texto_a_retornar;
            }

            //Este concatena todos los resultados para el final ya tener toda la fila recuerda que es un proceso recursivo 
            texto_a_retornar = string.Join(caracterSeparacion[0], espliteado_texto);
            return texto_a_retornar;
        }

        

        //Metodo para incrementar comparando un dato igual en un arreglo multiple

        public string incrementar_decrementar_comparando_datos_stringElementoEnMultiArregloRecursivo(string texto, object columnas_a_recorrer,string id_string_al_esplitear_comparar, string id_string_al_esplitear_editar, string cantidad_a_incrementar_decrementar, string[] caracterSeparacion = null)
        {
            //si no introdujo caracteres de separacion se usara predeterminado
            if (caracterSeparacion == null)
            {
                caracterSeparacion = G_caracter_separacion;
            }

            string[] espliteado_columnas_recorrer = { };
            //Sí es un string lo splitea Este normalmente es al inicio de la función 
            if (columnas_a_recorrer is string)
            {
                espliteado_columnas_recorrer = columnas_a_recorrer.ToString().Split(Convert.ToChar(caracterSeparacion[0]));
            }
            else if (columnas_a_recorrer is string[] temp)
            {

                espliteado_columnas_recorrer = temp;
            }
            string[] espliteado_texto = texto.Split(Convert.ToChar(caracterSeparacion[0]));

            //En esta parte Se inicia desde el segundo elemento y se guardan los caracteres y
            //las columnas para sí hay otro elemento En el arreglo múltiple 
            string texto_a_retornar = "";
            if (espliteado_columnas_recorrer.Length > 1)
            {
                string[] tem_array_col_recorrer = new string[espliteado_columnas_recorrer.Length - 1];
                string[] tem_array_caracter_separacion = new string[caracterSeparacion.Length - 1];
                for (int i = 1; i < espliteado_columnas_recorrer.Length; i++)
                {
                    tem_array_col_recorrer[i - 1] = espliteado_columnas_recorrer[i];
                    tem_array_caracter_separacion[i - 1] = caracterSeparacion[i];
                }

                //espliteado_texto = texto.Split(Convert.ToChar(tem_array_caracter_separacion[0]));
                texto_a_retornar = espliteado_texto[Convert.ToInt32(tem_array_col_recorrer[0])];

                espliteado_texto[Convert.ToInt32(espliteado_columnas_recorrer[1])] = incrementar_decrementar_stringElementoEnMultiArregloRecursivo(texto_a_retornar, tem_array_col_recorrer, cantidad_a_incrementar_decrementar, tem_array_caracter_separacion); // Llamada recursiva


            }

            else
            {
                //Sí es el último elemento del arreglo múltiple modifica el texto
                //para finalmente concatenarlos Con Los otros textos a retornar de la función recursiva 
                texto_a_retornar = "" + (Convert.ToDouble(espliteado_texto[0]) + Convert.ToDouble(cantidad_a_incrementar_decrementar));
                espliteado_texto[0] = texto_a_retornar;
            }

            //Este concatena todos los resultados para el final ya tener toda la fila recuerda que es un proceso recursivo 
            texto_a_retornar = string.Join(caracterSeparacion[0], espliteado_texto);
            return texto_a_retornar;
        }

        //-----------------------------------------------------------------------------------------------

        public void cambiar_archivo_por_arreglo(string direccion, string[] arreglo)
        {
            bas.cambiar_archivo_con_arreglo(direccion, arreglo);
        }


        //-----------------------------------------------------------------------------------------------

        // Método para procesar una venta utilizando información de la lista de venta
        public void procesar_venta(string[] info_lista_venta, string[] caracter_separacion = null)
        {
            if (caracter_separacion == null)
            {
                caracter_separacion = G_caracter_separacion;
            }
            double total_venta = 0;
            double total_costo_compra = 0;


            string[] impuestos_columnas = variables_glob_conf.GG_arrays_carga_de_archivos[4][0].Split(Convert.ToChar(caracter_separacion[2]));
            string[,] impuestos = new string[variables_glob_conf.GG_arrays_carga_de_archivos[4].Length, impuestos_columnas.Length+1];
            for (int i = 0; i < variables_glob_conf.GG_arrays_carga_de_archivos[4].Length; i++)
            {
                string[] info_imp_esplit = variables_glob_conf.GG_arrays_carga_de_archivos[4][i].Split(Convert.ToChar(caracter_separacion[2]));
                impuestos[i, 0] = info_imp_esplit[0];
                impuestos[i, 1] = info_imp_esplit[1];
            }


            string texto_info_producto_agregar = "";

            for (int i = 0; i < info_lista_venta.Length; i++)
            {
                // Dividir los detalles del producto en la lista de venta utilizando el caracter de separación
                string[] detalles_del_producto_lista = info_lista_venta[i].Split(Convert.ToChar(caracter_separacion[0]));

                // Obtener el índice del producto en el arreglo de carga
                int indice_producto = Convert.ToInt32(detalles_del_producto_lista[detalles_del_producto_lista.Length - 1]);

                // Dividir la información del producto en el inventario utilizando el caracter de separación global
                string[] produ_invent = variables_glob_conf.GG_arrays_carga_de_archivos[0][indice_producto].Split(Convert.ToChar(G_caracter_separacion[0]));

                if (produ_invent[4] != detalles_del_producto_lista[0])
                {
                    // Extraer información e índice del producto utilizando la función extraer_info_e_indise
                    produ_invent = extraer_info_e_indise(detalles_del_producto_lista[0]);

                    // Si no se pudo extraer la información del producto, detener el proceso
                    if (produ_invent[0] == null)
                    {
                        return;
                    }

                    // Dividir la información del producto extraída utilizando el caracter de separación global
                    produ_invent = produ_invent[0].Split(Convert.ToChar(caracter_separacion[0]));
                    // Actualizar el índice del producto a partir de la nueva información obtenida
                    indice_producto = Convert.ToInt32(produ_invent[1]);

                }

                // Construir una cadena con los detalles del producto para despues agregarla al registro de venta
                if (i==info_lista_venta.Length)
                {
                    //codigo¬nombre¬cantidad¬precio_venta¬precio_compra¬provedor°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2¬provedor_2
                    texto_info_producto_agregar = texto_info_producto_agregar + produ_invent[4] + caracter_separacion[2] + produ_invent[0] + " " + produ_invent[1] + " " + produ_invent[2] + caracter_separacion[2] + detalles_del_producto_lista[5] + caracter_separacion[2] + produ_invent[3] + caracter_separacion[2] + produ_invent[6] + caracter_separacion[2] + produ_invent[7] + caracter_separacion[1];
                }
                else
                {
                    //codigo¬nombre¬cantidad¬precio_venta¬precio_compra¬provedor°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2¬provedor_2
                    texto_info_producto_agregar = texto_info_producto_agregar + produ_invent[4] + caracter_separacion[2] + produ_invent[0] + " " + produ_invent[1] + " " + produ_invent[2] + caracter_separacion[2] + detalles_del_producto_lista[5] + caracter_separacion[2] + produ_invent[3] + caracter_separacion[2] + produ_invent[7];
                }
                
                // Llamar al método para editar una celda en el archivo y arreglo
                //decrementa cantidad del producto en el inventario
                editar_string_archivo_y_arreglo_todo_conjunto("incrementar_decrementar_celda", variables_glob_conf.GG_dir_nom_archivos[0, 0], "-" + detalles_del_producto_lista[5], -1, "cod_barras", detalles_del_producto_lista[0], null, "cantidad", info_lista_venta[i]);

                string dinero_venta_provedor = "" + (Convert.ToDouble(detalles_del_producto_lista[5]) * Convert.ToDouble(detalles_del_producto_lista[4]));
                editar_string_archivo_y_arreglo_todo_conjunto("incrementar_decrementar_celda", variables_glob_conf.GG_dir_nom_archivos[1, 0], dinero_venta_provedor, -1, "provedor", produ_invent[7], null, "dinero");

                double precio_venta_multiplicado_con_cantidad_producto = (Convert.ToDouble(produ_invent[3]) * Convert.ToDouble(detalles_del_producto_lista[5]));
                total_venta = total_venta + precio_venta_multiplicado_con_cantidad_producto;

                double precio_compra_multiplicado_con_cantidad_producto = (Convert.ToDouble(produ_invent[6]) * Convert.ToDouble(detalles_del_producto_lista[5]));
                total_costo_compra = total_costo_compra + precio_compra_multiplicado_con_cantidad_producto;

                
                string[] info_impuestos_del_string = produ_invent[13].Split(Convert.ToChar(caracter_separacion[1]));
                for (int j = variables_glob_conf.GG_var_glob_int[0]; j < variables_glob_conf.GG_arrays_carga_de_archivos[4].Length; j++)
                {
                    bool esta_el_impuesto = false;
                    for (int k = 0; k < info_impuestos_del_string.Length; k++)
                    {
                        if (impuestos[j, 0] == info_impuestos_del_string[k])
                        {
                            esta_el_impuesto = true;
                            impuestos[j, impuestos_columnas.Length]= ""+(Convert.ToDouble(impuestos[j, impuestos_columnas.Length])+((Convert.ToDouble(impuestos[j, 1])/ 100)* precio_venta_multiplicado_con_cantidad_producto));
                            break;
                        }
                    }
                }


                


            }

            string impuestos_agregar_registros = "";
            for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_archivos[4].Length; i++)
            {
                if (impuestos[i, impuestos_columnas.Length] != null)
                {
                    // impuesto_1¬cantidad_a_pagar_impuesto_1¬porcentage_de_impuesto_1°impuesto_2¬cantidad_a_pagar_impuesto_2¬porcentage_de_impuesto_2
                    impuestos_agregar_registros = impuestos_agregar_registros + impuestos[i, 0] + caracter_separacion[2] + impuestos[i, 2] + caracter_separacion[2] + impuestos[i, 1] + caracter_separacion[1];
                }
            }
            Operaciones_textos op = new Operaciones_textos();
            impuestos_agregar_registros = op.Trimend_paresido(impuestos_agregar_registros, Convert.ToChar(G_caracter_separacion[1]));

            //hora_min_seg | total_transaccion_vendida | costo_compra | impuesto_1¬cantidad_a_pagar_impuesto_1¬porcentage_de_impuesto_1°impuesto_2¬cantidad_a_pagar_impuesto_2¬porcentage_de_impuesto_2 | codigo¬nombre¬cantidad¬precio_venta¬precio_compra°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2 | total_venta | total_compra|alguna_informacion
            string text_agregar = DateTime.Now.ToString("HH:mm:ss") + G_caracter_separacion[0] + total_venta + G_caracter_separacion[0] + total_costo_compra + G_caracter_separacion[0] + impuestos_agregar_registros + G_caracter_separacion[0] + texto_info_producto_agregar + G_caracter_separacion[0] + "";
            
            modelo_de_registros_venta(text_agregar);

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

        // Método para agregar un registro a un arreglo de strings
        public string[] agregar_registro_del_array(string[] arreglo, string registro)
        {
            string[] temp = { "" };

            //Este if es por si no tienen nada el arreglo Agrega el registro inicial 
            if (arreglo == null)
            {
                arreglo = new string[] { "" };// Crear un nuevo arreglo con un elemento vacío
                temp = new string[arreglo.Length];
                
                // Copiar los elementos del arreglo original al arreglo temporal
                for (int i = 0; i < arreglo.Length; i++)
                {
                    temp[i] = arreglo[i];
                }

                // Agregar el nuevo registro al último elemento del arreglo temporal
                temp[arreglo.Length - 1] = registro;

            }
            //Este si tiene datos el arreglo le agrega otro registro más 
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

        public string[] chequeo_datos_esten_en_archivo_retorna_toda_la_fila(string info_texto, string columna_a_recorer_del_string, int id_arreglo_archivo, string columna_a_recorer_del_archivo, string[] caracter_separacion_del_string = null, string[] caracter_separacion_del_archivo = null)
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
            string[] retornar_fila = null;
            for (int id_resul_del_string = 0; id_resul_del_string < info_spliteado_del_string.Length; id_resul_del_string++)
            {




                for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo_archivo].Length; i++)
                {
                    string[] info_spliteado_del_archivo = variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo_archivo][i].Split(Convert.ToChar(caracter_separacion_del_archivo[0]));

                    //aqui se extrae el arreglo de la info del las columnas recorridas
                    string[] columnas_del_archivo = columna_a_recorer_del_archivo.Split(Convert.ToChar(caracter_separacion_del_archivo[0]));




                    //aqui la comparacion para ver si existe o no
                    if (info_spliteado_del_archivo[0] == info_spliteado_del_string[id_resul_del_string])
                    {

                        return retornar_fila = new string[] { variables_glob_conf.GG_arrays_carga_de_archivos[id_arreglo_archivo][i], "" + i };

                    }

                }

            }

            return retornar_fila;
        }

        public string[] chequeo_datos_esten_en_archivo_retorna_todo_el_texto_que_ingresaste_faltante(string info_texto, string columna_a_recorer_del_string, int id_arreglo_archivo, string columna_a_recorer_del_archivo, string[] caracter_separacion_del_string = null, string[] caracter_separacion_del_archivo = null)
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


        public string[] chequeo_datos_esten_en_archivo_retorna_solo_el_elemento_buscado_faltantes(string info_texto, string columna_a_recorer_del_string, int id_arreglo_archivo, string columna_a_recorer_del_archivo, string[] caracter_separacion_del_string = null, string[] caracter_separacion_del_archivo = null)
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
            for (int i = variables_glob_conf.GG_var_glob_int[0]; i < variables_glob_conf.GG_arrays_carga_de_archivos[0].Length; i++)
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