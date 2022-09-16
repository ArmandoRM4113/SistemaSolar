namespace SistemaSolar.Entidades
{
    #region [Bibliotecas de clases]
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Resources;
    using System.Windows.Forms;
    #endregion

    /// <summary>
    /// Clase que encapsula la información de un planeta y su comportamiento.
    /// </summary>
    public class Planeta
    {
        #region [Propiedades]
        /// <summary>
        /// Identificador.
        /// </summary>
        public byte Identificador { get; set; }

        /// <summary>
        /// Nombre.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Radio.
        /// </summary>
        public float Radio { get; set; }

        /// <summary>
        /// Posición.
        /// </summary>
        public float Posicion { get; set; }

        /// <summary>
        /// Tamaño.
        /// </summary>
        public Size Tamanio { get; set; }

        /// <summary>
        /// Velocidad.
        /// </summary>
        public float Velocidad { get; set; }

        /// <summary>
        /// Centro.
        /// </summary>
        public Point Centro { get; set; }

        /// <summary>
        /// Coordenada X.
        /// </summary>
        public float CoordenadaX { get; set; }

        /// <summary>
        /// Coordenada Y.
        /// </summary>
        public float CoordenadaY { get; set; }

        /// <summary>
        /// Imagen.
        /// </summary>
        private PictureBox Imagen { get; set; }
        #endregion

        #region [Variables]
        /// <summary>
        /// Instancia que permite acceso a los archivos de recursos.
        /// </summary>
        private readonly ResourceManager administradorRecursos;
        #endregion

        #region [Constructor]
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="identificador">Identificador.</param>
        /// <param name="nombre">Nombre.</param>        
        /// <param name="radio">Radio.</param>
        /// <param name="posicion">Posición.</param>
        /// <param name="tamanio">Tamaño.</param>
        /// <param name="velocidad">Velocidad.</param>
        /// <param name="coordenadaX">Coordenada X.</param>
        /// <param name="coordenadaY">Coordenada Y.</param>        
        public Planeta(byte identificador, string nombre,float radio, float posicion, Size tamanio, float velocidad, float coordenadaX, float coordenadaY)
        {
            administradorRecursos = Recursos.Planetas.ResourceManager;
            Identificador = identificador;
            Descripcion = identificador > 0 ? administradorRecursos.GetString(string.Concat("Descripcion", nombre)) : string.Empty;
            Nombre = nombre;
            Radio = radio;
            Posicion = posicion;
            Tamanio = tamanio;
            Velocidad = velocidad;
            CoordenadaX = coordenadaX;
            CoordenadaY = coordenadaY;            
        }
        #endregion

        #region [Métodos]
        /// <summary>
        /// Método para actualizar el control en el formulario.
        /// </summary>
        /// <param name="formulario">Control que representa al formulario.</param>
        public void Actualiza(Control formulario)
        {
            visualizaPlaneta(formulario);
        }

        /// <summary>
        /// Método para actualizar el control en el formulario.
        /// </summary>
        /// <param name="formulario">Control que representa al formulario.</param>
        /// <param name="centro">Coordenada centro.</param>
        public void Actualiza(Control formulario, Point centro)
        {
            Centro = centro; 
            visualizaPlaneta(formulario);
        }

        /// <summary>
        /// Método para cargar el control de imagen asociado a un planeta dentro del formulario.
        /// </summary>
        /// <param name="formulario">Control que representa al formulario.</param>
        private void visualizaPlaneta(Control formulario)
        {
            string controlId = string.Concat("pb", Nombre);

            if (!formulario.Controls.ContainsKey(controlId))
            {                
                Imagen = new PictureBox()
                {                    
                    AccessibleDescription = Descripcion,
                    BackgroundImage = (Bitmap)administradorRecursos.GetObject(Nombre),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Location = obtenCoordenadaPolar(),
                    Name = controlId,
                    Size = Tamanio                    
                };

                formulario.Controls.Add(Imagen);

                if (Identificador > 0)
                {
                    Imagen.Click += new EventHandler(onClick);
                }
            }
            else
            {
                formulario.Controls.Find(controlId, true).First().Location = obtenCoordenadaPolar();
                formulario.Controls.Find(controlId, true).First().Size = Tamanio;
            }
        }        

        /// <summary>
        /// Método para obtener los radianes.
        /// </summary>
        /// <param name="posicion">Posición.</param>
        /// <returns>Radianes.</returns>
        private float obtenRadianes(float posicion)
        {
            return (float)(posicion * Math.PI / 180f);
        }

        /// <summary>
        /// Método para obtener la coordenada Polar.
        /// </summary>
        /// <returns>Punto que contiene la coordenada Polar.</returns>
        private Point obtenCoordenadaPolar()
        {
            return new Point(
                Centro.X + (int)(Radio * Math.Cos(obtenRadianes(Posicion)) * CoordenadaX - Tamanio.Width / 2),
                Centro.Y + (int)(Radio * Math.Sin(obtenRadianes(Posicion)) * CoordenadaY - Tamanio.Height / 2));
        }
        #endregion

        #region [Métodos controladores de eventos]
        /// <summary>
        /// Manejador del evento click para el control de imagen.
        /// </summary>
        /// <param name="sender">Control PictureBox.</param>
        /// <param name="e">Argumentos.</param>        
        private void onClick(object sender, EventArgs e)
        {
            string detalle = string.Format(
                "Planeta: {0}\nMovimiento de traslacion: {1}",
                ((PictureBox)sender).Name.Replace("pb", ""),
                ((PictureBox)sender).AccessibleDescription
                );

            MessageBox.Show(detalle, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
