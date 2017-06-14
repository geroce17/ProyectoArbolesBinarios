using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoArbolesBinarios
{
    class Arbol
    {
        Nodo inicio;
        Nodo actual;
        Nodo selected;
        Nodo raiz;
        Nodo primero;

        public void descomponerYLlenar(string cadena)
        {
            for (int i = 0; i < cadena.Length; i++)
            {
                Nodo nuevo = new Nodo();
                nuevo.dato = cadena[i];
                if (inicio == null)
                {
                    inicio = nuevo;
                    raiz = inicio;
                }
                else
                {
                    actual = inicio;
                    while (actual.siguiente != null)
                        actual = actual.siguiente;

                    actual.siguiente = nuevo;
                    actual.siguiente.anterior = actual;
                }
            }
            int cont=0;
            selected = inicio;

            while (cont < cadena.Length)
            {
                if(selected != null && (selected.dato == '/' || selected.dato == '*'))
                {
                    selected.izquierda = selected.anterior;
                    selected.derecha = selected.siguiente;
                    selected.anterior = selected.anterior.anterior;
                    selected.siguiente = selected.siguiente.siguiente;
                    if (selected.siguiente != null)
                        selected.siguiente.anterior = selected;
                    if(selected.anterior != null)
                        selected.anterior.siguiente = selected;
                    raiz = selected;
                }
                if (selected != null)
                    selected = selected.siguiente;
                cont++;
            }
            cont = 0;
            selected = inicio;
            while (cont < cadena.Length)
            {
                if (selected != null && (selected.dato == '+' || selected.dato == '-'))
                {
                    selected.izquierda = selected.anterior;
                    selected.derecha = selected.siguiente;
                    selected.anterior = selected.anterior.anterior;
                    selected.siguiente = selected.siguiente.siguiente;
                    if (selected.siguiente != null)
                        selected.siguiente.anterior = selected;
                    if (selected.anterior != null)
                        selected.anterior.siguiente = selected;
                    raiz = selected;
                }
                if (selected != null)
                    selected = selected.siguiente;
                cont++;
            }
        }

        public string preOrden()
        {
            if (raiz == null)
                return "";
            else
            {
                return PreOrden(raiz);
            }
        }

        private string PreOrden(Nodo n)
        {
            string res = "";
            res += n.dato;
            if (n.izquierda != null)
                res += PreOrden(n.izquierda);
            if (n.derecha != null)
                res += PreOrden(n.derecha);

            return res;
        }

        public string postOrden()
        {
            if (raiz == null)
                return "";
            else
            {
                return PostOrden(raiz);
            }
        }

        private string PostOrden(Nodo n)
        {
            string res = "";
            if (n.izquierda != null)
                res += PostOrden(n.izquierda);
            if (n.derecha != null)
                res += PostOrden(n.derecha);
            res += n.dato;

            return res;
        }

        public int evaluarPre(string preOrden)
        {
            string charizard;
            string[] pendientes = new string[preOrden.Length];
            int cont = 0;
            for (int i = preOrden.Length - 1; i >= 0; i--)
            {
                if (preOrden[i] != '/' && preOrden[i] != '*' && preOrden[i] != '+' && preOrden[i] != '-')
                {
                    Nodo nuevo = new Nodo();
                    if (primero == null)
                    {
                        primero = nuevo;
                        charizard = Convert.ToString(preOrden[i]);
                        primero.numero = Convert.ToInt32(charizard);
                    }
                    else
                    {
                        charizard = Convert.ToString(preOrden[i]);
                        for (actual=primero; actual.siguiente != null;)
                            actual = actual.siguiente;
                        nuevo.numero = Convert.ToInt32(charizard);
                        actual.siguiente = nuevo;
                        actual.siguiente.anterior = actual;
                    }
                    if (pendientes[0] != null)
                    {
                        actual = actual.siguiente;
                        if (actual != null || actual.anterior != null)
                            if (pendientes[0] == "/")
                            {
                                actual.anterior.numero = actual.numero / actual.anterior.numero;
                                pendientes[0] = null;
                            }
                            else
                            {
                                if (pendientes[0] == "*")
                                {
                                    actual.anterior.numero = actual.numero * actual.anterior.numero;
                                    pendientes[0] = null;
                                }
                                else
                                {
                                    if (pendientes[0] == "+")
                                    {
                                        actual.anterior.numero = actual.numero + actual.anterior.numero;
                                        pendientes[0] = null;
                                    }
                                    else
                                    {
                                        if (pendientes[0] == "-")
                                        {
                                            actual.anterior.numero = actual.numero - actual.anterior.numero;
                                            pendientes[0] = null;
                                        }
                                    }
                                }
                                Array.Sort(pendientes);
                            }
                    }
                }
                else
                {
                    if (primero!=null)
                    {
                        for (actual = primero; actual.siguiente != null;)
                            actual = actual.siguiente;
                        if (preOrden[i] == '/')
                        {
                            if (actual.anterior != null || actual != null)
                                actual.anterior.numero = actual.numero / actual.anterior.numero;
                            else
                            {
                                pendientes[cont] = "/"; cont++;
                            }
                        }
                        else
                        {
                            if (preOrden[i] == '*')
                                if (actual.anterior != null || actual != null)
                                    actual.anterior.numero = actual.numero * actual.anterior.numero;
                                else
                                {
                                    pendientes[cont] = "*"; cont++;
                                }
                            else
                            {
                                if (preOrden[i] == '+')
                                    if (actual.anterior != null || actual != null)
                                        actual.anterior.numero = actual.numero + actual.anterior.numero;
                                    else
                                    {
                                        pendientes[cont] = "+"; cont++;
                                    }
                                else
                                {
                                    if (preOrden[i] == '-')
                                        if (actual.anterior != null || actual != null)
                                            actual.anterior.numero = actual.numero - actual.anterior.numero;
                                        else
                                        {
                                            pendientes[cont] = "-"; cont++;
                                        }
                                }
                            }
                        }
                        actual.anterior.siguiente = null;
                    }
                }
            }
            return primero.numero;
        }

        public int evaluarPost(string postOrden)
        {
            string charizard;
            int [] vec = new int[postOrden.Length];
            int cont = 0;
            for (int i = 0; i < postOrden.Length; i++)
            {
                if (postOrden[i] != '/' && postOrden[i] != '*' && postOrden[i] != '+' && postOrden[i] != '-')
                {
                    charizard = Convert.ToString(postOrden[i]);
                    for (cont = 0; vec[cont] != 0; cont++) { }
                    vec[cont] = Convert.ToInt32(charizard);
                }
                else
                {

                    if (vec[cont - 2] != 0 && vec[cont - 1] != 0)
                    {
                        if (postOrden[i] == '/')
                            vec[cont - 2] = vec[cont - 2] / vec[cont - 1];
                        else
                        {
                            if (postOrden[i] == '*')
                                vec[cont - 2] = vec[cont - 2] * vec[cont - 1];
                            else
                            {
                                if (postOrden[i] == '+')
                                    vec[cont - 2] = vec[cont - 2] + vec[cont - 1];
                                else
                                {
                                    if (postOrden[i] == '-')
                                        vec[cont - 2] = vec[cont - 2] - vec[cont - 1];

                                }
                            }
                        }
                        vec[cont - 1] = 0;
                        cont = cont - 2;
                    }
                }
                cont++;
            }
            return vec[0];
        }
    }
}
