namespace SistemaSolar.Entidades
{
    #region [Bibliotecas de clases]
    using System.Drawing;
    #endregion

    /// <summary>
    /// Clase que encapsula la información de una estrella.
    /// </summary>
    public class Estrella
    {
        #region [Propiedades]
        /// <summary>
        /// Coordenada X.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Coordenada Y.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Coordenada de incremento en X.
        /// </summary>
        public int Ax { get; set; }

        /// <summary>
        /// Coordenada de incremento en Y.
        /// </summary>
        public int Ay { get; set; }

        /// <summary>
        /// Radio.
        /// </summary>
        public int R { get; set; }

        /// <summary>
        /// Color.
        /// </summary>
        public Color Color { get; set; }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">Coordenada X.</param>
        /// <param name="y">Coordenada Y.</param>
        /// <param name="ax">Coordenada de incremento en X.</param>
        /// <param name="ay">Coordenada de incremento en Y.</param>
        /// <param name="r">Radio.</param>
        /// <param name="c">Color.</param>
        public Estrella(int x, int y, int ax, int ay, int r, Color color)
        { 
            X = x; 
            Y = y; 
            Ax = ax; 
            Ay = ay; 
            R = r; 
            Color = color; 
        }
        #endregion
    }
}