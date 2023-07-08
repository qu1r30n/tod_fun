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
            for (int i = 0; i < variables_glob_conf.GG_nom_archivos.GetLength(0); i++)
            {
                bas.Crear_archivo_y_directorio(variables_glob_conf.GG_nom_archivos[i,0], variables_glob_conf.GG_nom_archivos[i, 1]);
            }

            //archivos de registro
            for (int i = 0; i < variables_glob_conf.GG_dir_reg.GetLength(0); i++)
            {
                bas.Crear_archivo_y_directorio(variables_glob_conf.GG_dir_reg[i, 0], variables_glob_conf.GG_dir_reg[i, 1]);
            }

            for (int i = 0; i < variables_glob_conf.GG_nom_archivos.GetLength(0); i++)
            {
               variables_glob_conf.GG_arrays_carga_de_archivos[i] = bas.Leer(variables_glob_conf.GG_nom_archivos[i, 0]);
            }
    
            
        }

        public bool existe_info(int num_columna, string dato_a_comparar)
        {
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];
            bool existe = false;
            string texto = bas.Seleccionar(direccion_inventario, num_columna, dato_a_comparar);
            if (texto != "")
            {
                existe = true;
            }
            return existe;
        }

        //-----------------------------------------------------------------------------------------------

        public void agregar_string_archivo(string direccion, string texto)
        { 
            bas.Agregar(direccion, texto);

            for (int i = 0; i < variables_glob_conf.GG_nom_archivos.GetLength(0); i++)
            {
                if (direccion == variables_glob_conf.GG_nom_archivos[i, 0])
                {
                    // Agregar el nuevo registro al arreglo correspondiente
                    variables_glob_conf.GG_arrays_carga_de_archivos[i] = agregar_registro_del_array(variables_glob_conf.GG_arrays_carga_de_archivos[i], texto);
                }

            }
        }

        //-----------------------------------------------------------------------------------------------
        public void cambiar_archivo_por_arreglo(string direccion, string[] arreglo)
        {
            bas.cambiar_archivo_con_arreglo(direccion, arreglo);
        }


        //-----------------------------------------------------------------------------------------------

        public string proceso_venta(string[] codigos, string[] cantidades, double descuento = 0)
        {
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];
            string direccion_ventas = variables_glob_conf.GG_nom_archivos[3,0];
            
            DateTime fecha_hora = DateTime.Now;
            string año_mes_dia = fecha_hora.ToString("yyyyMMdd");

            double total = acumulador_de_precios(codigos, cantidades);

            for (int i = 1; i < codigos.Length; i++)
            {
                bas.Incrementa_celda(direccion_inventario, 3, codigos[i], "4", "-" + cantidades[i]);
            }
            bas.si_existe_suma_sino_agega_extra(direccion_ventas, 0, año_mes_dia, "1", (total - descuento) + "", año_mes_dia + G_caracter_separacion[0] + total + G_caracter_separacion[0] + "0");//incrementa la cantidad de dinero ganado

            registro_ventas(codigos,cantidades, (total - descuento) + "");
            
            return "";
        }

        public void registro_ventas(string[] codigo, string[] cantidad,string dinero_pagado="-0")
        {
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];

            string[] inv = bas.Leer(direccion_inventario);

            
            //hora_min_seg|codigo¬nombre¬cantidad¬precio_venta¬precio_compra°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2|total_venta|total_compra|pagado_por_promocion
            string info = "";
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            double acum_venta = 0;
            double acum_compra = 0;
            for (int i = 0; i < inv.Length; i++)
            {
                string[] info_produc = inv[i].Split(Convert.ToChar(G_caracter_separacion[0]));
                for (int j = 0; j < codigo.Length; j++)
                {
                    if (info_produc[3]==codigo[j])
                    {
                        double costo_venta = Convert.ToDouble(info_produc[2]);
                        double costo_compra = Convert.ToDouble(info_produc[5]);
                        double cantidad_double = Convert.ToDouble(cantidad[j]);
                        acum_venta = acum_venta + (costo_venta*cantidad_double);
                        acum_compra = acum_compra + (costo_compra*cantidad_double);
                        //hora_min_seg|codigo¬nombre¬cantidad¬precio_venta¬precio_compra°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2|total_venta|total_compra|pagado_por_promocion
                        info = info + codigo[j] + G_caracter_separacion[2] + info_produc[1] + G_caracter_separacion[2] + cantidad[j] + G_caracter_separacion[2] + (costo_venta * cantidad_double) + G_caracter_separacion[2] + (costo_compra * cantidad_double) + G_caracter_separacion[2];
                        codigo = eliminar_registro_del_array(codigo, j);
                        cantidad = eliminar_registro_del_array(cantidad, j);
                        j = j - 1;
                        if (codigo.Length == 0)
                        {
                            break;
                        }
                        info = info + G_caracter_separacion[1];

                    }
                    
                }
                if (codigo.Length == 0)
                {
                    break;
                }
            }

            //hora_min_seg|codigo¬nombre¬cantidad¬precio_venta¬precio_compra°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2|total_venta|total_compra|dinero_pagado
            if (dinero_pagado=="-0")
            {
                info = info + G_caracter_separacion[0] + acum_venta + G_caracter_separacion[0] + acum_compra + G_caracter_separacion[0] + acum_venta;
            }
            else
            {
                info = info + G_caracter_separacion[0] + acum_venta + G_caracter_separacion[0] + acum_compra + G_caracter_separacion[0] + dinero_pagado;
            }

            variables_glob_conf var_glob = new variables_glob_conf();
            string dir_temp = variables_glob_conf.GG_dir_reg[0,0];
            DateTime fecha_hora = DateTime.Now;
            string hora_min_seg = fecha_hora.ToString("HH:mm:ss");
            bas.Agregar(dir_temp, hora_min_seg + G_caracter_separacion[0] + info);
        }

        public string[] eliminar_registro_del_array(string[] arreglo,int num_registro)
        {
            string[] temp = new string[arreglo.Length - 1];
            int j = 0;
            for (int i = 0; i < arreglo.Length; i++)
            {
                if (i!=num_registro)
                {
                    temp[j] = arreglo[i];
                    j++;
                }
            }

            return temp;
        }


        public string procesar_compra(string[] codigo, string[] cantidad, string[] precio, string[] impuesto_porcentage, bool aplicar_impuesto_a_la_compra = false, string descuento = "0", double minimo_porcentaje_ganancia = 15, double porcentaje_elevar = 20)
        {
            string[] cod_para_registro = codigo;
            string[] cantidad_para_registro = cantidad;
            

            minimo_porcentaje_ganancia = 1 + (minimo_porcentaje_ganancia / 100);
            porcentaje_elevar = 1 + (porcentaje_elevar / 100);
            

            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            string[] inv = bas.Leer(direccion_inventario);
            for (int i = 0; i < inv.Length; i++)
            {
                string[] info = inv[i].Split(Convert.ToChar(G_caracter_separacion[0]));
                for (int j = 0; j < codigo.Length; j++)
                {
                    if (codigo[j] == info[i])
                    {
                        
                        if (impuesto_porcentage[j] == null || impuesto_porcentage[j] == "")
                        {
                            impuesto_porcentage[j] = "0";
                        }
                        
                        double impuesto_double = Convert.ToDouble(impuesto_porcentage[j]);
                        double descuento_double= Convert.ToDouble(descuento);
                        double precio_nuevo_double = Convert.ToDouble(precio[j]);
                        double precio_inven_comp = Convert.ToDouble(info[5]);
                        double cantidad_en_inventario = Convert.ToDouble(info[4]);
                        double cantidad_comprada = Convert.ToDouble(cantidad[j]);
                        double cantidad_total = cantidad_comprada + cantidad_en_inventario;

                        string columnas_editar = "4" + G_caracter_separacion[0] + "5";
                        string info_editar = "";
                        
                        if (aplicar_impuesto_a_la_compra)
                        {
                            info_editar = cantidad_total + G_caracter_separacion[0] + (precio_nuevo_double * porcentaje_elevar*(1+(impuesto_double/100)));
                        }
                        else
                        {
                            info_editar = cantidad_total + G_caracter_separacion[0] + (precio_nuevo_double * porcentaje_elevar);
                        }

                        if (descuento!="0")
                        {
                            info_editar = cantidad_total + G_caracter_separacion[0] + (precio_nuevo_double * porcentaje_elevar - descuento_double);
                        }
                        else
                        {
                            info_editar = cantidad_total + G_caracter_separacion[0] + (precio_nuevo_double * porcentaje_elevar);
                        }

                        columnas_editar = columnas_editar + G_caracter_separacion[0] + "11";
                        info_editar = info_editar + G_caracter_separacion[0] + impuesto_porcentage;
                        
                        

                        if (precio_nuevo_double<=(precio_inven_comp*minimo_porcentaje_ganancia))
                        {
                            bas.Editar_espesifico(direccion_inventario, 3, codigo[j], columnas_editar, info_editar,G_caracter_separacion[0]);
                        }
                        
                        
                        codigo = eliminar_registro_del_array(codigo, j);
                        j = j-1;
                    }
                    registro_compras(cod_para_registro, cantidad_para_registro, descuento);
                }
            }
            
            return "";
        }

        
        public void registro_compras(string[] codigo, string[] cantidad, string descuento = "0")
        {
            variables_glob_conf var_glob = new variables_glob_conf();

            
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];

            string[] inv = bas.Leer(direccion_inventario);

            
            //hora_min_seg|codigo¬nombre¬cantidad¬precio_venta¬precio_compra°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2|total_venta|total_compra|pagado_por_promocion
            string info = "";
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            double acum_venta = 0;
            double acum_compra = 0;
            for (int i = 0; i < inv.Length; i++)
            {
                string[] info_produc = inv[i].Split(Convert.ToChar(G_caracter_separacion[0]));
                for (int j = 0; j < codigo.Length; j++)
                {
                    if (info_produc[3] == codigo[j])
                    {
                        double costo_venta = Convert.ToDouble(info_produc[2]);
                        double costo_compra = Convert.ToDouble(info_produc[5]);
                        double cantidad_double = Convert.ToDouble(cantidad[j]);
                        //acum_venta = acum_venta + (costo_venta * cantidad_double);
                        acum_compra = acum_compra + (costo_compra * cantidad_double);
                        //hora_min_seg|codigo¬nombre¬cantidad¬precio_venta¬precio_compra°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2|total_venta|total_compra|pagado_por_promocion
                        info = info + codigo[j] + G_caracter_separacion[2] + info_produc[1] + G_caracter_separacion[2] + cantidad[j] + G_caracter_separacion[2] + (costo_venta * cantidad_double) + G_caracter_separacion[2] + (costo_compra * cantidad_double) + G_caracter_separacion[2];
                        codigo = eliminar_registro_del_array(codigo, j);
                        cantidad = eliminar_registro_del_array(cantidad, j);
                        j = j - 1;
                        if (codigo.Length == 0)
                        {
                            break;
                        }
                        info = info + G_caracter_separacion[1];

                    }

                }
                if (codigo.Length == 0)
                {
                    break;
                }
            }

            //hora_min_seg|codigo¬nombre¬cantidad¬precio_venta¬precio_compra°codigo_2¬nombre_2¬cantidad_2¬precio_venta_2¬precio_compra_2|total_venta|total_compra|pagado_por_promocion
            info = info + G_caracter_separacion[0] + acum_venta + G_caracter_separacion[0] + acum_compra + G_caracter_separacion[0] + (acum_compra - Convert.ToDouble(descuento));

            DateTime fecha_hora = DateTime.Now;
            string hora_min_seg = fecha_hora.ToString("HH:mm:ss");
            string dir_temp = variables_glob_conf.GG_dir_reg[1,0];
            bas.Agregar(dir_temp, hora_min_seg + G_caracter_separacion[0] + info);
        }


        public string agregar_promocion(string nombrePromo, string[] codigosBarras, string[] nombre_producto , string[] cantidades, string total)
        {
            Tex_base bas = new Tex_base();
            string direccion_promo = variables_glob_conf.GG_nom_archivos[2,0];
            string concatenado_info = "";


            double precio_anterior = acumulador_de_precios(codigosBarras, cantidades);

            for (int i = 0; i < codigosBarras.Length; i++)
            {
                //nombre_promocion|codigo_barras_1¬cantidad_del_producto¬nombre_producto_1°codigo_barras_2¬cantidad_del_producto¬nombre_productp_2|precio_anterior|precio_pagar
                concatenado_info = concatenado_info + codigosBarras[i] + G_caracter_separacion[2] + nombre_producto[i] + G_caracter_separacion[2] + cantidades[i] + precio_anterior + cantidades[i] + G_caracter_separacion[1];
            }
            concatenado_info=bas.Trimend_paresido(concatenado_info,Convert.ToChar(G_caracter_separacion[0]));
            string promo = nombrePromo + G_caracter_separacion[0] + concatenado_info + G_caracter_separacion[0] + total;
            string resultado = bas.si_no_existe_agega_comparacion(direccion_promo, promo);
            return "";
        }


        public string[] promociones_ventas(string[] codigo, string[] cantidad)
        {
            // Se define la dirección del archivo que contiene la información sobre las promociones
            string direccion_promo = variables_glob_conf.GG_nom_archivos[2,0];

            // Se lee el contenido del archivo y se guarda en la variable "info_promos"
            string[] info_promos = bas.Leer(direccion_promo);

            

            // Se recorre cada una de las promociones disponibles en el archivo
            for (int i = 0; i < info_promos.Length; i++)
            {
                // Se separa la información de la promoción en sus componentes
                string[] promo_1_nom_produc_precio = info_promos[i].Split(Convert.ToChar(G_caracter_separacion[0]));
                string[] promo_produc = promo_1_nom_produc_precio[1].Split(Convert.ToChar(G_caracter_separacion[1]));

                // Se crea un arreglo de cadenas de texto para indicar si cada producto de la promoción ha sido encontrado en la lista de ventas
                string[] si_cumple_cantidad_pa_promo = new string[promo_produc.Length];

                // Se recorre cada uno de los productos que forman parte de la promoción
                for (int j = 0; j < promo_produc.Length; j++)
                {
                    // Se separa la información del producto en sus componentes
                    string[] datos_producto_promo = promo_produc[j].Split(Convert.ToChar(G_caracter_separacion[2]));

                    // Se recorre la lista de ventas para buscar el producto correspondiente
                    for (int k = 0; k < codigo.Length; k++)
                    {
                        // Se separa la información del producto en la lista de ventas en sus componentes
                        string[] produc_list_split = codigo[k].ToString().Split(Convert.ToChar(G_caracter_separacion[0]));

                        // Se compara el código del producto en la lista de ventas con el código del producto en la promoción
                        if (produc_list_split[0] == datos_producto_promo[0])
                        {
                            // Si la cantidad de producto en la lista de ventas es mayor o igual a la cantidad requerida por la promoción,
                            // se marca este producto como "cumplido"
                            double dato_list_comparar = Convert.ToDouble(produc_list_split[1]);
                            double dato_promo_comparar = Convert.ToDouble(datos_producto_promo[1]);
                            if (dato_promo_comparar <= dato_list_comparar)
                            {
                                si_cumple_cantidad_pa_promo[j] = "1";
                            }
                        }
                    }
                }

                // Se comprueba si todos los productos de la promoción han sido encontrados en la lista de ventas
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
                    info_promos[i] = string.Join(G_caracter_separacion[0].ToString(), promo_1_nom_produc_precio);
                }
            }

            return info_promos;
        }


        public string leer_info_producto(string codigo)
        {
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            string info_produc =bas.Seleccionar(direccion_inventario, 3, codigo, null, G_caracter_separacion[0]);
            return info_produc;
        }
        public void agregar_producto(string nom_produc,string precio_venta, string cod_bar, string cantidad, string costo_compra, string provedor, string grupo, string cantidad_productos_por_paquete, string ligar_productos_para_sabor, string impuesto, string tipo_producto_para_impuesto)
        {
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];
            string[] inf_inv = bas.Leer(direccion_inventario);
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            string info_a_agregar = inf_inv.Length + G_caracter_separacion[0] + nom_produc + G_caracter_separacion[0] + precio_venta + G_caracter_separacion[0] + cod_bar + G_caracter_separacion[0] + cantidad + G_caracter_separacion[0] + costo_compra + G_caracter_separacion[0] + provedor + G_caracter_separacion[0] + grupo + G_caracter_separacion[0] + "" + G_caracter_separacion[0] + cantidad_productos_por_paquete + G_caracter_separacion[0] + ligar_productos_para_sabor + G_caracter_separacion[0] + impuesto + G_caracter_separacion[0] + tipo_producto_para_impuesto + G_caracter_separacion[0];
            bas.Agregar(direccion_inventario, info_a_agregar);
        }

        public void editar_producto(string codigo,string columna_editar,string info_editar)
        {
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];
            bas.Editar_espesifico(direccion_inventario, 3, codigo, columna_editar, info_editar, G_caracter_separacion[0]);
        }

        public void eliminar_producto(string codigo)
        {
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];
            bas.Eliminar(direccion_inventario, 3, codigo,G_caracter_separacion[0]);
        }

        //--------------------------------------------------------------------------------------------

        public double acumulador_de_precios(string[] codigos, string[] cantidad)
        {
            //id_0|producto_1|cantidad_producto_2|tipo_de_medida_3|precio_de_venta_4|cod_bar_5|cantidad_6|costo_compra_7|provedor_8|grupo_9|multiusos_10|cantidad_productos_por_paquete_11|productos_elaborados_12|ligar_productos_para_sabor_13|impuesto_14|tipo_producto_para_impuesto_15|
            string direccion_inventario = variables_glob_conf.GG_nom_archivos[0,0];
            string[] productos_info=bas.Leer(direccion_inventario, null, Convert.ToChar(G_caracter_separacion[0]));

            double acum = 0;
            for (int i = 0; i < productos_info.Length; i++)
            {
                string[] produc_espliteado = productos_info[i].Split(Convert.ToChar(G_caracter_separacion[0]));
                for (int j = 0; j < codigos.Length; j++)
                {
                    if (produc_espliteado[3]==codigos[j])
                    {
                        acum = acum + (Convert.ToDouble(produc_espliteado[2]) * Convert.ToDouble(cantidad[j]));
                    }
                }
            }
            return acum;
        }

        //-------------------------------------------------------------------------------------------

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

        public string[] chequeo_datos_esten_en_archivo(string info_texto, string columna_a_recorer_del_string, int id_columna_a_comparar_del_string, int id_arreglo_archivo, string columna_a_recorer_del_archivo, int id_columna_a_comparar_archivo, string[] caracter_separacion_del_string = null, string[] caracter_separacion_del_archivo = null)
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



                    if (info_spliteado_del_archivo[id_columna_a_comparar_archivo] == info_spliteado_del_string[id_columna_a_comparar_del_string])
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
    }
}
