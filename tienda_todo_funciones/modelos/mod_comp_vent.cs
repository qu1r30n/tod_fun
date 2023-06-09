﻿using System;

using tienda_todo_funciones.desinger;

using tienda_todo_funciones.procesos;

namespace tienda_todo_funciones.modelos
{
    class mod_comp_vent
    {
        string[] G_caracter_separacion = variables_glob_conf.GG_caracter_separacion;

        procesamientos pr = new procesamientos();

        //-------------------------------------------------------------------------------

        public object modelo_unico(string operacion, string[] descripcion_arreglo_opcional = null, string[][] arreglos_de_entrada = null, string[] informacion_de_variables = null, string ubicacion_rapida = null, string texto_rapido = null)
        {
            object objeto_a_retornar = null;
            //funciones a hacer
            //--------------------------------------------------------------------------------------------------------------
            //"crear_archivos_inicio"
            //----------------------------------------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------------------------------
            //"pasar_arreglo_a_archivo"
            //direccion_rapido
            //informacion_de_variables//este es arreglo pero se pasa todo el arreglo
            //------------------------------------------------------------------------------------------------------------
            //agregar_string_al_archivo
            //direccion_rapido = string direccion
            //texto_rapido = string texto
            //-----------------------------------------------------------------------------------------------------------
            //agregar_string_al_inventario
            //texto_rapido = string texto

            if (operacion == "crear_archivos_inicio")
            {
                pr.crear_archivos_inicio_programa();
            }

            else if (operacion == "pasar_arreglo_a_archivo")
            {
                if (ubicacion_rapida != null && informacion_de_variables != null)
                {
                    pr.cambiar_archivo_por_arreglo(ubicacion_rapida, informacion_de_variables);
                }
                else
                {
                    // Manejar el caso cuando faltan parámetros requeridos para la operación "pasar_arreglo_a_archivo"
                }
            }

            else if (operacion == "agregar_string_al_archivo")
            {
                if (ubicacion_rapida != null && texto_rapido != null)
                {
                    pr.agregar_string_archivo(ubicacion_rapida, texto_rapido);
                }
                else
                {
                    // Manejar el caso cuando faltan parámetros requeridos para la operación "agregar_string_al_archivo"
                }
            }

            else if (operacion == "agregar_string_al_inventario")
            {
                if (texto_rapido != null)
                {
                    pr.agregar_string_archivo(variables_glob_conf.GG_direccion_base[0], texto_rapido);
                }
                else
                {
                    // Manejar el caso cuando falta el parámetro requerido para la operación "agregar_string_al_inventario"
                }
            }

            else if (operacion == "mod_form_chequeo_info_producto_nuevo")
            {
                mod_form_chequeo_dat_nuevo_produc(texto_rapido);
            }

            else if (operacion == "mod_chequeo_info_arch")
            {

                if (texto_rapido != null)
                {
                    objeto_a_retornar = mod_chequeo_info_arch(ubicacion_rapida, texto_rapido);
                }
                else
                {
                    // Manejar el caso cuando falta el parámetro requerido para la operación "agregar_string_al_inventario"
                }
            }


            else
            {
                // Manejar el caso cuando se proporciona una operación no válida
            }


            return objeto_a_retornar;

        }


        public void mod_form_chequeo_dat_nuevo_produc(string datos_introducidos)
        {
            modelo_unico("mod_chequeo_info_arch", ubicacion_rapida: "chequeo_provedor_sino_agrega", texto_rapido: datos_introducidos);
            modelo_unico("mod_chequeo_info_arch", ubicacion_rapida: "chequeo_tipo_medida_sino_agrega", texto_rapido: datos_introducidos);
            modelo_unico("mod_chequeo_info_arch", ubicacion_rapida: "form_chequeo_impuesto_sino_agrega", texto_rapido: datos_introducidos);
        }

        private string[] mod_chequeo_info_arch(string operacion, string texto, string[] info_extra = null, string[][] caracter_separacion_string_archivos = null)
        {
            string[] arreglo_a_retornar = null;
            //pr.chequeo_datos_esten_en_archivo("1|1|2|3|4|5|6|7|8|venta_ingrediente||11|12¬0°0_5¬1|13|14°14|15", "0|12", 0, 0, "0",5);
            if (operacion == "chequeo_info_en_archivo")
            {
                arreglo_a_retornar = pr.chequeo_datos_esten_en_archivo_retorna_todo_el_texto_que_ingresaste(texto, "0|12", 0, "0");
            }


            else if (operacion == "chequeo_provedor_sino_agrega")
            {
                arreglo_a_retornar = pr.chequeo_datos_esten_en_archivo_retorna_solo_el_elemento_buscado(texto, "0|8", 1, "0");
                if (arreglo_a_retornar != null)
                {
                    modelo_unico("agregar_string_al_archivo", ubicacion_rapida: variables_glob_conf.GG_nom_archivos[1, 0], texto_rapido: arreglo_a_retornar[0]);

                }
            }

            else if (operacion == "chequeo_tipo_medida_sino_agrega")
            {
                arreglo_a_retornar = pr.chequeo_datos_esten_en_archivo_retorna_solo_el_elemento_buscado(texto, "0|3", 5, "0|0");
                if (arreglo_a_retornar != null)
                {
                    modelo_unico("agregar_string_al_archivo", ubicacion_rapida: variables_glob_conf.GG_nom_archivos[5, 0], texto_rapido: arreglo_a_retornar[0]);

                }
            }

            else if (operacion == "chequeo_informacion_abierto")
            {
                if (info_extra != null)
                {
                    if (caracter_separacion_string_archivos == null)
                    {
                        arreglo_a_retornar = pr.chequeo_datos_esten_en_archivo_retorna_todo_el_texto_que_ingresaste(texto, info_extra[0], Convert.ToInt32(info_extra[2]), info_extra[3]);
                    }
                    else
                    {
                        arreglo_a_retornar = pr.chequeo_datos_esten_en_archivo_retorna_todo_el_texto_que_ingresaste(texto, info_extra[0], Convert.ToInt32(info_extra[2]), info_extra[3], caracter_separacion_string_archivos[0], caracter_separacion_string_archivos[1]);
                    }
                }

            }

            else if (operacion == "form_chequeo_y_agregar_codbar_si_no_esta")
            {
                
                arreglo_a_retornar =pr.chequeo_datos_esten_en_archivo_retorna_todo_el_texto_que_ingresaste(texto, "0|5", 0, "0|5");
                variables_glob_conf.GG_variables_string[0]= arreglo_a_retornar[0];
                variables_glob_conf var_glob = new variables_glob_conf();

                if (arreglo_a_retornar[0] != "" && arreglo_a_retornar.Length < 1) 
                {
                    Ventana_emergente emergent_ventana = new Ventana_emergente();
                    string info_a_agregar = emergent_ventana.Proceso_ventana_emergente(var_glob.GG_ventana_emergente_productos);
                    modelo_unico("agregar_string_al_inventario", texto_rapido: info_a_agregar);
                }
                
            }

            else if (operacion == "form_chequeo_impuesto_sino_agrega")
            {
                arreglo_a_retornar = pr.chequeo_datos_esten_en_archivo_retorna_solo_el_elemento_buscado(texto, "0|8",4, "0|0");
                if (arreglo_a_retornar != null)
                {
                    Ventana_emergente vent_emer = new Ventana_emergente();
                    string[] enviar = { "1" + G_caracter_separacion[0] + "porsentaje_impuesto" + G_caracter_separacion[0] + "0" };
                    string porcentaje=vent_emer.Proceso_ventana_emergente(enviar, "impuesto: " + arreglo_a_retornar[0]);
                    string impuesto_y_porcentaje = arreglo_a_retornar[0] + G_caracter_separacion[0] + porcentaje;


                    modelo_unico("agregar_string_al_archivo", ubicacion_rapida: variables_glob_conf.GG_nom_archivos[4, 0], texto_rapido: impuesto_y_porcentaje);

                }
            }



            return arreglo_a_retornar;
        }





        //-------------------------------------------------------------------------------



    }
}
