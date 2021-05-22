using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace T1TL2
{
    public partial class Form1 : Form
    {
        List<string> nuevo = new List<string>();
        List<string> numero = new List<string>();
        List<string> letra = new List<string>();
        List<int> analizador = new List<int>();
        List<List<int>> ListaAnalizador = new List<List<int>>();
        bool flag = false;
        bool flag2 = false;
        bool flag3 = false;
        bool bandera = false;
        bool banderachida = false;
        Stack<int> Pila = new Stack<int>();
        List<Point> ReglasList = new List<Point>();

        public Form1()
        {
            InitializeComponent();
            DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
            style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            leermatriz();
            tabla();
        }
        void tabla()
        {
            string Tabla = "GR2slrTablebien.txt";
            string Lectura;
            StreamReader Leer = new StreamReader(Tabla);
            Lectura = Leer.ReadLine();
            while (Lectura != null)
            {
                string[] cont = Lectura.Split('\t');
                List<int> num = new List<int>();
                for (int i = 0; i < cont.Length; ++i)
                {
                    num.Add(Convert.ToInt32(cont[i]));
                }
                ListaAnalizador.Add(num);
                Lectura = Leer.ReadLine();
            }
            Leer.Close();
        }
        void leermatriz()
        {
            string reglas = "GR2slrRulesId.txt";
            string Lectura;
            string[] separador = { "\t" };
            StreamReader Leer = new StreamReader(reglas);
            Lectura = Leer.ReadLine();
            while (Lectura != null)
            {
                string[] cont = Lectura.Split('\t');
                ReglasList.Add(new Point(Convert.ToInt32(cont[0]), Convert.ToInt32(cont[1])));
                Lectura = Leer.ReadLine();
            }
            Leer.Close();
        }
        void sintactico()
        {
            Pila.Clear();
            Pila.Push(0);
            while (analizador.Count >= 1)
            {
                int valor = Pila.Pop();
                int savevalor = analizador[0];
                analizador.RemoveAt(0);
                int dev = ListaAnalizador[valor][savevalor + 1];
                if (dev > 0)
                {
                    Pila.Push(valor);
                    Pila.Push(savevalor);
                    Pila.Push(dev);
                }

                else if (dev < 0)
                {
                    if (dev == -1)
                    {
                        textBox2.Text = "Analizador sintactico correcto";
                        textBox2.BackColor = Color.Green;
                        return;
                    }
                    else
                    {
                        dev = Math.Abs(dev) - 1;
                        int valor2 = (ReglasList[dev].Y) * 2;
                        Pila.Push(valor);
                        for (int i = 0; i < valor2; ++i)
                        {
                            Pila.Pop();
                        }
                        analizador.Insert(0, savevalor);
                        int valor3 = ReglasList[dev].X;
                        analizador.Insert(0, valor3);
                    }
                }
                else if (dev == 0)
                {
                    textBox2.Text = "Error sintactico";
                    textBox2.BackColor = Color.Red;
                    return;
                }
            }
        }
        void estado0(string palabra)
        {
            string nueva = "";
            string j = "";
            string prueba = "";
            List<string> listapalabra = new List<string>();
            int cantidad = 0;
            List<string> listan = new List<string>();

            for (int i = 0; i < palabra.Length; i++)
            {
                listapalabra.Add(palabra[i].ToString());
            }
            void fg2()
            {
                if (flag2 == true)
                {
                    for (int i = 0; i < numero.Count(); i++)
                    {
                        nueva = string.Join("", numero.ToArray());
                    }
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Constante";
                    dataGridView1[1, cantidad].Value = nueva;
                    analizador.Add(13);
                    numero.Clear();
                    flag2 = false;
                }
            }
            void fg3()
            {
                if (flag3 == true)
                {
                    for (int i = 0; i < letra.Count(); i++)
                    {
                        nueva = string.Join("", letra.ToArray());
                    }
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Id";
                    dataGridView1[1, cantidad].Value = nueva;
                    analizador.Add(1);
                    letra.Clear();
                    flag3 = false;
                }
            }
            void fg1()
            {
                if (flag == true)
                {
                    for (int i = 0; i < nuevo.Count(); i++)
                    {
                        nueva = string.Join("", nuevo.ToArray());
                    }
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "errollr";
                    dataGridView1[1, cantidad].Value = nueva;
                    nuevo.Clear();
                    flag = false;
                    textBox2.Text = "Error en Analizador lexico";
                    textBox2.BackColor = Color.Red;
                    banderachida = true;
                }
            }
            if (listapalabra.Count == 1 && listapalabra[0] == "$")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "18. Final";
                dataGridView1[1, cantidad].Value = "$";
                analizador.Add(18);
                if (banderachida == false)
                {
                    sintactico();
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == ";")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "2. Punto y coma";
                dataGridView1[1, cantidad].Value = ";";
                analizador.Add(2);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == ":")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "20. dospuntos";
                dataGridView1[1, cantidad].Value = ":";
                analizador.Add(20);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "%")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "21. Porcentaje";
                dataGridView1[1, cantidad].Value = "%";
                analizador.Add(21);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "¿")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "22. Preg Izq";
                dataGridView1[1, cantidad].Value = "¿";
                analizador.Add(22);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "?")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "23. Preg Der";
                dataGridView1[1, cantidad].Value = "?";
                analizador.Add(23);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "¡")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "24. Adm Izq";
                dataGridView1[1, cantidad].Value = "¡";
                analizador.Add(24);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "!")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "25. Adm Der";
                dataGridView1[1, cantidad].Value = "!";
                analizador.Add(25);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "[")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "26. Agrup Izq";
                dataGridView1[1, cantidad].Value = "[";
                analizador.Add(26);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "]")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "27. Agrup Der";
                dataGridView1[1, cantidad].Value = "]";
                analizador.Add(27);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "@")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "28. Arroba";
                dataGridView1[1, cantidad].Value = "@";
                analizador.Add(28);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 4 && listapalabra[0] == "e" && listapalabra[1] == "x" && listapalabra[2] == "i" && listapalabra[3] == "t")
            {
                if (listapalabra.Count > 4 && (char.IsLetter(Convert.ToChar(listapalabra[4])) || listapalabra[4] == "_" || char.IsDigit(Convert.ToChar(listapalabra[4]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "29. Exit";
                dataGridView1[1, cantidad].Value = "exit";
                analizador.Add(29);
                if (listapalabra.Count >= 5)
                {
                    for (int i = 4; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 3 && listapalabra[0] == "f" && listapalabra[1] == "o" && listapalabra[2] == "r")
            {
                if (listapalabra.Count > 3 && (char.IsLetter(Convert.ToChar(listapalabra[3])) || listapalabra[3] == "_" || char.IsDigit(Convert.ToChar(listapalabra[3]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "30. For";
                dataGridView1[1, cantidad].Value = "for";
                analizador.Add(30);
                if (listapalabra.Count >= 4)
                {
                    for (int i = 3; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "#")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "19. Gato";
                dataGridView1[1, cantidad].Value = "#";
                analizador.Add(19);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == ",")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "3. Coma";
                dataGridView1[1, cantidad].Value = ",";
                analizador.Add(3);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);

                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "(")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "4. Parentesis Izq";
                dataGridView1[1, cantidad].Value = "(";
                analizador.Add(4);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == ")")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "5. Parentesis Der";
                dataGridView1[1, cantidad].Value = ")";
                analizador.Add(5);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                    return;
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "{")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "6. Llave izq";
                dataGridView1[1, cantidad].Value = "{";
                analizador.Add(6);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "}")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "7. Llave Der";
                dataGridView1[1, cantidad].Value = "}";
                analizador.Add(7);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (flag == false && listapalabra.Count > 1 && listapalabra[0] == "=" && listapalabra[1] != "=")
            {
                fg3();
                fg2();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "Igual";
                dataGridView1[1, cantidad].Value = "=";
                analizador.Add(8);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count == 1 && listapalabra[0] == "=")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "8. Igual";
                dataGridView1[1, cantidad].Value = "=";
                analizador.Add(8);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 3 && listapalabra[0] == "i" && listapalabra[1] == "n" && listapalabra[2] == "t")
            {
                if (listapalabra.Count > 3 && (char.IsLetter(Convert.ToChar(listapalabra[3])) || listapalabra[3] == "_" || char.IsDigit(Convert.ToChar(listapalabra[3]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "Tipo de dato";
                dataGridView1[1, cantidad].Value = "int";
                analizador.Add(0);
                if (listapalabra.Count >= 4)
                {
                    for (int i = 3; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 4 && listapalabra[0] == "c" && listapalabra[1] == "h" && listapalabra[2] == "a" && listapalabra[3] == "r")
            {
                if (listapalabra.Count > 4 && (char.IsLetter(Convert.ToChar(listapalabra[4])) || listapalabra[4] == "_" || char.IsDigit(Convert.ToChar(listapalabra[4]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "Tipo de dato";
                dataGridView1[1, cantidad].Value = "char";
                analizador.Add(0);
                if (listapalabra.Count >= 5)
                {
                    for (int i = 4; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 5 && listapalabra[0] == "f" && listapalabra[1] == "l" && listapalabra[2] == "o" && listapalabra[3] == "a" && listapalabra[4] == "t")
            {
                if (listapalabra.Count > 5 && (char.IsLetter(Convert.ToChar(listapalabra[5])) || listapalabra[5] == "_" || char.IsDigit(Convert.ToChar(listapalabra[5]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "Tipo de dato";
                dataGridView1[1, cantidad].Value = "float";
                analizador.Add(0);
                if (listapalabra.Count >= 6)
                {
                    for (int i = 5; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 4 && listapalabra[0] == "v" && listapalabra[1] == "o" && listapalabra[2] == "i" && listapalabra[3] == "d")
            {
                if (listapalabra.Count > 4 && (char.IsLetter(Convert.ToChar(listapalabra[4])) || listapalabra[4] == "_" || char.IsDigit(Convert.ToChar(listapalabra[4]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "Tipo de dato";
                dataGridView1[1, cantidad].Value = "void";
                analizador.Add(0);
                if (listapalabra.Count >= 5)
                {
                    for (int i = 4; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 2 && listapalabra[0] == "i" && listapalabra[1] == "f")
            {
                if (listapalabra.Count > 2 && (char.IsLetter(Convert.ToChar(listapalabra[2])) || listapalabra[2] == "_" || char.IsDigit(Convert.ToChar(listapalabra[2]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "if-Control";
                dataGridView1[1, cantidad].Value = "if";
                analizador.Add(9);
                if (listapalabra.Count >= 3)
                {
                    for (int i = 2; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 5 && listapalabra[0] == "w" && listapalabra[1] == "h" && listapalabra[2] == "i" && listapalabra[3] == "l" && listapalabra[4] == "e")
            {
                if (listapalabra.Count > 5 && (char.IsLetter(Convert.ToChar(listapalabra[5])) || listapalabra[5] == "_" || char.IsDigit(Convert.ToChar(listapalabra[5]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "while-Control";
                dataGridView1[1, cantidad].Value = "while";
                analizador.Add(10);
                if (listapalabra.Count >= 6)
                {
                    for (int i = 5; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 6 && listapalabra[0] == "r" && listapalabra[1] == "e" && listapalabra[2] == "t" && listapalabra[3] == "u" && listapalabra[4] == "r" && listapalabra[5] == "n")
            {
                if (listapalabra.Count > 6 && (char.IsLetter(Convert.ToChar(listapalabra[6])) || listapalabra[6] == "_" || char.IsDigit(Convert.ToChar(listapalabra[6]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "Rtn";
                dataGridView1[1, cantidad].Value = "return";
                analizador.Add(11);
                if (listapalabra.Count >= 7)
                {
                    for (int i = 6; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 4 && listapalabra[0] == "e" && listapalabra[1] == "l" && listapalabra[2] == "s" && listapalabra[3] == "e")
            {
                if (listapalabra.Count > 4 && (char.IsLetter(Convert.ToChar(listapalabra[4])) || listapalabra[4] == "_" || char.IsDigit(Convert.ToChar(listapalabra[4]))))
                {
                    fg2();
                    fg1();
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    flag3 = true;
                    estado0(nueva);
                    return;
                }
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "else-Control";
                dataGridView1[1, cantidad].Value = "else";
                analizador.Add(12);
                if (listapalabra.Count >= 5)
                {
                    for (int i = 4; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);

                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "+" || listapalabra[0] == "-")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "OpSuma";
                dataGridView1[1, cantidad].Value = listapalabra[0];
                analizador.Add(14);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "*" || listapalabra[0] == "/")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "OpMulti";
                dataGridView1[1, cantidad].Value = listapalabra[0];
                analizador.Add(16);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 2 && (listapalabra[0] == "&" && listapalabra[1] == "&" || listapalabra[0] == "|" && listapalabra[1] == "|"))
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "OpLogico";
                dataGridView1[1, cantidad].Value = listapalabra[0] + listapalabra[1];
                analizador.Add(15);
                if (listapalabra.Count >= 3)
                {
                    for (int i = 2; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);

                }
                return;
            }
            if (listapalabra.Count >= 2 && listapalabra[0] == "=" && listapalabra[1] == "=" || listapalabra[0] == "<" && listapalabra[1] == "=" || listapalabra[0] == ">" && listapalabra[1] == "=" || listapalabra[0] == "!" && listapalabra[1] == "=")
            {
                fg3();
                fg2();
                fg1();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "OpRel";
                dataGridView1[1, cantidad].Value = listapalabra[0] + listapalabra[1];
                analizador.Add(17);
                if (listapalabra.Count >= 3)
                {
                    for (int i = 2; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (listapalabra.Count >= 1 && listapalabra[0] == "<" || listapalabra[0] == ">")
            {
                fg1();
                fg3();
                fg2();
                dataGridView1.Rows.Add();
                cantidad = dataGridView1.Rows.Count - 1;
                dataGridView1[0, cantidad].Value = "OpRel";
                dataGridView1[1, cantidad].Value = listapalabra[0];
                analizador.Add(17);
                if (listapalabra.Count >= 2)
                {
                    for (int i = 1; i < listapalabra.Count; i++)
                    {
                        listan.Add(listapalabra[i].ToString());
                        nueva = string.Join("", listan.ToArray());
                    }
                    estado0(nueva);
                }
                return;
            }
            if (flag3 == true && char.IsDigit(Convert.ToChar(listapalabra[0])))
            {
                fg2();
                fg1();
                letra.Add(listapalabra[0]);
                listapalabra.Remove(listapalabra[0]);
                nueva = string.Join("", listapalabra.ToArray());

                if (listapalabra.Count == 0)
                {
                    j = string.Join("", letra.ToArray());
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Id";
                    dataGridView1[1, cantidad].Value = j;
                    analizador.Add(1);
                    letra.Clear();
                    flag3 = false;
                    return;
                }
                if (listapalabra.Count == 2 && char.IsLetter(Convert.ToChar(listapalabra[0])) && listapalabra[1] == "$")
                {
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    j = string.Join("", letra.ToArray());
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Id";
                    dataGridView1[1, cantidad].Value = j;
                    analizador.Add(1);
                    letra.Clear();
                    flag3 = false;
                    estado0(nueva);
                    return;
                }
                flag3 = true;
                estado0(nueva);
                return;
            }
            if (listapalabra.Count >= 1 && char.IsDigit(Convert.ToChar(listapalabra[0])))
            {
                fg3();
                fg1();
                numero.Add(listapalabra[0]);
                listapalabra.Remove(listapalabra[0]);
                nueva = string.Join("", listapalabra.ToArray());
                if (listapalabra.Count == 0)
                {
                    j = string.Join("", numero.ToArray());
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Constante";
                    dataGridView1[1, cantidad].Value = j;
                    analizador.Add(13);
                    numero.Clear();
                    flag2 = false;
                    return;
                }
                if (listapalabra.Count == 2 && char.IsDigit(Convert.ToChar(listapalabra[0])) && listapalabra[1] == "$")
                {
                    numero.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    j = string.Join("", numero.ToArray());
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Constante";
                    dataGridView1[1, cantidad].Value = j;
                    analizador.Add(13);
                    numero.Clear();
                    flag2 = false;
                    estado0(nueva);
                    return;
                }
                flag2 = true;
                estado0(nueva);
                return;
            }
            if (bandera == false && listapalabra.Count > 1 && listapalabra[0] == "." && char.IsDigit(Convert.ToChar(listapalabra[1])))
            {
                fg3();
                bandera = true;
                numero.Add(listapalabra[0]);
                listapalabra.Remove(listapalabra[0]);
                nueva = string.Join("", listapalabra.ToArray());
                flag2 = true;
                estado0(nueva);


                return;
            }
            if (bandera == true && listapalabra.Count > 1 && listapalabra[0] == "." && char.IsDigit(Convert.ToChar(listapalabra[1])))
            {
                if (flag3 == true)
                {
                    fg3();
                }
                else
                {
                    j = string.Join("", numero.ToArray());
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Constante";
                    dataGridView1[1, cantidad].Value = j;
                    analizador.Add(13);
                    numero.Clear();
                }
                numero.Add(listapalabra[0]);
                listapalabra.Remove(listapalabra[0]);
                nueva = string.Join("", listapalabra.ToArray());
                flag2 = true;
                estado0(nueva);
                bandera = false;
                return;
            }
            if (char.IsLetter(Convert.ToChar(listapalabra[0])) || listapalabra[0] == "_")
            {
                fg2();
                fg1();
                letra.Add(listapalabra[0]);
                listapalabra.Remove(listapalabra[0]);
                nueva = string.Join("", listapalabra.ToArray());

                if (listapalabra.Count == 0)
                {
                    j = string.Join("", letra.ToArray());
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Id";
                    dataGridView1[1, cantidad].Value = j;
                    analizador.Add(1);
                    letra.Clear();
                    flag3 = false;
                    return;
                }
                if (listapalabra.Count == 2 && char.IsLetter(Convert.ToChar(listapalabra[0])) && listapalabra[1] == "$")
                {
                    letra.Add(listapalabra[0]);
                    listapalabra.Remove(listapalabra[0]);
                    nueva = string.Join("", listapalabra.ToArray());
                    j = string.Join("", letra.ToArray());
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "Id";
                    dataGridView1[1, cantidad].Value = j;
                    analizador.Add(1);
                    letra.Clear();
                    flag3 = false;
                    estado0(nueva);
                    return;
                }
                flag3 = true;
                estado0(nueva);
                return;
            }
            else
            {
                fg3();
                fg2();
                nuevo.Add(listapalabra[0]);
                listapalabra.Remove(listapalabra[0]);
                nueva = string.Join("", listapalabra.ToArray());
                flag = true;
                if (listapalabra.Count == 0)
                {
                    for (int i = 0; i < nuevo.Count(); i++)
                    {
                        nueva = string.Join("", nuevo.ToArray());
                    }
                    dataGridView1.Rows.Add();
                    cantidad = dataGridView1.Rows.Count - 1;
                    dataGridView1[0, cantidad].Value = "error1";
                    dataGridView1[1, cantidad].Value = nueva;
                    nuevo.Clear();
                    flag = false;
                    textBox2.Text = "Error en Analizador lexico";
                    textBox2.BackColor = Color.Red;
                    banderachida = true;
                    return;
                }
                estado0(nueva);
                return;
            }
        }
        void Grid()
        {
            int cont = 0;
            int inicio = 0;
            List<string> lista = new List<string>();
            string gg = "";
            bool flag = true;
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if ((textBox1.Text[0] == ' ') && flag == true)
                {
                    for (int j = 0; j < textBox1.Text.Length; j++)
                    {
                        if (textBox1.Text[j] == ' ')
                        {
                            cont++;
                        }
                        else
                            j = textBox1.Text.Length;
                    }
                    flag = false;
                    i = cont;
                }
                if (textBox1.Text[i] != ' ' && textBox1.Text[i] != '\r')
                {
                    cont++;
                    if (i == textBox1.Text.Length - 1)
                    {

                        inicio = i + 1 - cont;
                        for (int j = 0; j < cont; j++)
                        {
                            if (textBox1.Text[inicio] != ' ')
                            {
                                lista.Add(textBox1.Text[inicio].ToString());
                                gg = string.Join("", lista.ToArray());
                                inicio++;
                            }
                            else
                                inicio++;
                        }
                        estado0(gg);
                    }
                }
                if (textBox1.Text[i] == '\r' && textBox1.Text[i - 1] != ' ')
                {
                    inicio = i - cont;
                    for (int j = 0; j < cont; j++)
                    {
                        if (textBox1.Text[inicio] != ' ' && textBox1.Text[inicio] != '\r')
                        {
                            lista.Add(textBox1.Text[inicio].ToString());
                            gg = string.Join("", lista.ToArray());
                            inicio++;
                        }
                        else
                            inicio++;
                    }
                    estado0(gg);
                    inicio++;
                    i++;
                    cont = 0;
                    lista.Clear();
                }
                if (textBox1.Text[i] == ' ' && textBox1.Text[i - 1] != ' ')
                {
                    inicio = i - cont;
                    for (int j = 0; j < cont; j++)
                    {
                        if (textBox1.Text[inicio] != ' ')
                        {
                            lista.Add(textBox1.Text[inicio].ToString());
                            gg = string.Join("", lista.ToArray());
                            inicio++;
                        }
                        else
                            inicio++;
                    }
                    estado0(gg);
                    cont = 0;
                    lista.Clear();
                }
                /*    if (textBox1.Text[i] == '\r' || textBox1.Text[i+1] == '\n')
                    {
                        continue;
                    }*/
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 1)
            {
                banderachida = false;
                bandera = false;
                flag = false;
                flag2 = false;
                flag3 = false;
                dataGridView1.Rows.Clear();
                numero.Clear();
                analizador.Clear();
                if (textBox1.Text[textBox1.Text.Length - 1] != '$')
                {
                    textBox1.Text = textBox1.Text + "$";
                    Grid();
                }
                else
                    Grid();
            }
            else
            {
                textBox1.Text = textBox1.Text + "$";
                Grid();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* for (int i = 0; i < analizador.Count(); ++i)
             {
                 MessageBox.Show(analizador[i].ToString());
             }*/
            Pila.Clear();
            bandera = false;
            flag = false;
            flag2 = false;
            flag3 = false;
            textBox1.Text = "";
            dataGridView1.Rows.Clear();
            numero.Clear();
            analizador.Clear();

        }


        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
