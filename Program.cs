//Ejercicio 3

namespace EvaluacionMotorApp
{
    // ==========================================
    // CLASE: EvaluadorMotor
    // Encargada de gestionar las lecturas de temperatura
    // y determinar el estado del motor.
    // ==========================================
    public class EvaluadorMotor
    {
        // --------------------------------------
        // PROPIEDADES
        // --------------------------------------
        // Arreglo de 5 posiciones para almacenar las mediciones de temperatura
        public double[] Lecturas { get; private set; }

        // Umbral máximo para considerar consumo/estado normal (70.0 °C)
        public double LimiteNormal { get; } = 70.0;

        // --------------------------------------
        // CONSTRUCTOR
        // --------------------------------------
        public EvaluadorMotor()
        {
            // Inicializamos el arreglo para 5 lecturas fijas
            Lecturas = new double[5];
        }

        // --------------------------------------
        // MÉTODOS
        // --------------------------------------

        /// <summary>
        /// Captura las 5 lecturas de temperatura del usuario mediante un ciclo for,
        /// incluyendo validaciones de entrada para evitar lecturas incoherentes.
        /// </summary>
        public void RegistrarLecturas()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("    CAPTURA DE TEMPERATURAS DEL MOTOR (5 LECTURAS)");
            Console.WriteLine("=================================================");

            // Ciclo for estricto para las 5 mediciones
            for (int i = 0; i < Lecturas.Length; i++)
            {
                bool entradaValida = false;
                while (!entradaValida)
                {
                    Console.Write($"Ingrese la lectura #{i + 1} (°C): ");
                    string entrada = Console.ReadLine();

                    // Validación para evitar conversión fallida (NaN/Cero por error)
                    if (double.TryParse(entrada, out double temperatura))
                    {
                        // Validación física: Evitar valores por debajo del cero absoluto (-273.15 °C)
                        if (temperatura >= -273.15)
                        {
                            Lecturas[i] = temperatura;
                            entradaValida = true; // Salida del ciclo de validación
                        }
                        else
                        {
                            Console.WriteLine("  [ERROR] La temperatura no puede ser menor al cero absoluto (-273.15 °C).");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  [ERROR] Entrada inválida. Ingrese un valor numérico real.");
                    }
                }
            }
        }

        /// <summary>
        /// Calcula el promedio aritmético de las 5 lecturas registradas.
        /// Protegido contra división por cero en arreglos vacíos.
        /// </summary>
        /// <returns>Promedio numérico en °C</returns>
        public double CalcularPromedio()
        {
            // Validación de seguridad para prevenir división entre cero en arreglos sin longitud
            if (Lecturas == null || Lecturas.Length == 0)
            {
                return 0.0;
            }

            double suma = 0.0;
            for (int i = 0; i < Lecturas.Length; i++)
            {
                suma += Lecturas[i];
            }

            return suma / Lecturas.Length;
        }

        /// <summary>
        /// Evalúa el promedio obtenido contra el límite establecido de 70 °C.
        /// </summary>
        /// <returns>Cadena con el estado ("NORMAL" o "ALERTA DE TEMPERATURA")</returns>
        public string DeterminarEstado()
        {
            double promedio = CalcularPromedio();

            // Evaluación de la condición del problema
            if (promedio <= LimiteNormal)
            {
                return "NORMAL";
            }
            else
            {
                return "ALERTA DE TEMPERATURA";
            }
        }
    }

    // ==========================================
    // CLASE PRINCIPAL: Program
    // Punto de entrada de la aplicación de consola.
    // ==========================================
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Configurar codificación de consola para caracteres especiales (°C)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --------------------------------------
            // CREACIÓN DEL OBJETO
            // Instanciación del evaluador térmico
            // --------------------------------------
            EvaluadorMotor miEvaluador = new EvaluadorMotor();

            // Ejecución de captura de datos
            miEvaluador.RegistrarLecturas();

            // Cálculo y obtención del estado
            double promedioObtenido = miEvaluador.CalcularPromedio();
            string estadoMotor = miEvaluador.DeterminarEstado();

            // --------------------------------------
            // SALIDA DE RESULTADOS CON UNIDADES (°C)
            // --------------------------------------
            Console.WriteLine("\n=================================================");
            Console.WriteLine("           RESUMEN DEL DIAGNÓSTICO");
            Console.WriteLine("=================================================");
            Console.WriteLine("Lecturas ingresadas:");
            for (int i = 0; i < miEvaluador.Lecturas.Length; i++)
            {
                Console.WriteLine($"  - Medicion {i + 1}: {miEvaluador.Lecturas[i]:F2} °C");
            }
            Console.WriteLine("-------------------------------------------------");
            // Salida numérica mostrando la unidad de medida explícita (°C)
            Console.WriteLine($"Promedio general de temperatura : {promedioObtenido:F2} °C");
            Console.WriteLine($"Estado evaluado del motor       : {estadoMotor}");
            Console.WriteLine("=================================================\n");

            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}