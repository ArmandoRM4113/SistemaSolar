namespace SistemaSolar
{
    #region [Bibliotecas de clases]
    using SistemaSolar.Entidades;
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    #endregion

    /// <summary>
    /// Clase que contiene la lógica del formulario.
    /// </summary>
    public partial class frmAplicacion : Form
    {
        #region [Variables y constantes]
        /// <summary>
        /// Hilo de ejecución.
        /// </summary>
        private Thread subProceso;

        /// <summary>
        /// Centro.
        /// </summary>
        private Point centro = new Point();

        /// <summary>
        /// Constante X.
        /// </summary>
        private const float DX = 1f;

        /// <summary>
        /// Constante Y.
        /// </summary>
        private const float DY = 0.5f;

        /// <summary>
        /// Arreglo de planetas.
        /// </summary>
        private readonly Planeta[] planetas = {
                        new Planeta(0,"Sol", 0, 0, new Size(80, 80),0f, DX, DY),
                        new Planeta(1,"Mercurio", 120, 0, new Size(30, 30),1.5f,DX, DY),
                        new Planeta(2,"Venus", 180, 10, new Size(40, 40), 1.5f, DX, DY),
                        new Planeta(3,"Tierra", 240, 150, new Size(40, 40),1f, DX, DY),
                        new Planeta(4,"Marte", 490, 220, new Size(30, 30), 1.1f, DX, DY),
                        new Planeta(5,"Jupiter",520, 300, new Size(70, 70), 0.7f, DX, DY),
                        new Planeta(6,"Saturno",600, 20, new Size(100, 100), 1.4f, DX, DY),
                        new Planeta(7,"Urano", 650, 20, new Size(15,15), 0.9f, DX, DY),
                        new Planeta(8,"Neptuno",680, 80, new Size(20, 20),1.1f,DX, DY)
        };

        /// <summary>
        /// Generador de numeros aleatorios.
        /// </summary>
        private readonly Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Lienzo de dibujo.
        /// </summary>
        private Graphics lienzo;

        /// <summary>
        /// Arreglo de estrellas.
        /// </summary>
        private Estrella[] estrellas = new Estrella[100];
        #endregion

        #region [Constructor]
        /// <summary>
        /// Constructor.
        /// Inicializa el diseño del formulario.
        /// </summary>
        public frmAplicacion()
        {
            InitializeComponent();
        }
        #endregion

        #region [Métodos]
        /// <summary>
        /// Método que realiza la animación de movimiento de traslación de los planetas.
        /// </summary>
        private void animacion()
        {
            while (true)
            {
                for (int iterador = 0; iterador < estrellas.Length; iterador++)
                {
                    lienzo.FillEllipse(new SolidBrush(BackColor), estrellas[iterador].X, estrellas[iterador].Y, estrellas[iterador].R, estrellas[iterador].R);
                    estrellas[iterador].X += estrellas[iterador].Ax;
                    estrellas[iterador].Y += estrellas[iterador].Ay;
                    lienzo.FillEllipse(new SolidBrush(estrellas[iterador].Color), estrellas[iterador].X, estrellas[iterador].Y, estrellas[iterador].R, estrellas[iterador].R);

                    if (estrellas[iterador].X + estrellas[iterador].R > Width || estrellas[iterador].Y + estrellas[iterador].R > Height)
                    {
                        estrellas[iterador].X = random.Next(50);
                        estrellas[iterador].Y = random.Next(1, Height);
                        estrellas[iterador].Ax = random.Next(5, 15);
                        estrellas[iterador].Color = Color.FromArgb(200, 200, 200);
                        estrellas[iterador].Ay = 0;
                    }
                }

                for (int iterador = 0; iterador < planetas.Length; iterador++)
                {
                    planetas[iterador].Posicion += planetas[iterador].Velocidad;
                    planetas[iterador].Actualiza(this, centro);
                }

                Thread.Sleep(1);
            }
        }
        #endregion

        #region [Métodos controladores de eventos]
        /// <summary>
        /// Controlador del evento de carga del formulario.
        /// </summary>
        /// <param name="sender">Control frmAplicacion.</param>
        /// <param name="e">Objeto que contiene información del evento.</param>
        private void frmAplicacion_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            lienzo = CreateGraphics();
            BackColor = Color.Black;

            for (int iterador = 0; iterador < estrellas.Length; iterador++)
            {
                estrellas[iterador] = new Estrella(random.Next(Width), random.Next(Height), random.Next(5, 15), 0, 2, Color.FromArgb(200, 200, 200));
            }

            centro = new Point((int)(Width * 0.49f), (int)(Height * 0.47f));

            for (int iterador = 0; iterador < planetas.Length; iterador++)
            {
                planetas[iterador].Centro = centro;
                planetas[iterador].Actualiza(this);
            }

            subProceso = new Thread(animacion);
            subProceso.Start();
        }

        /// <summary>
        /// Controlador del evento de cierre del formulario.
        /// </summary>
        /// <param name="sender">Control frmAplicacion.</param>
        /// <param name="e">Objeto que contiene información del evento.</param>
        private void frmAplicacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            subProceso.Abort();
            Environment.Exit(0);
        }
        #endregion
    }
}