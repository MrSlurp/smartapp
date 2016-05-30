/*
    This file is part of SmartApp.

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GradientBaloon
{
    /// <summary>
    /// Enregistre les informations nécessaires à la création d'un Rectangle dont
    /// les coins sont arrondis
    /// </summary>
    [Flags]
    public enum RoundedCorner
    {
        TopLeft = 2,
        TopRight = 4,
        BottomLeft = 8,
        BottomRight = 16,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    public class RoundRectangle
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private float radius;
        private RoundedCorner roundedCorners;
        public static readonly RoundRectangle Empty;

        #region Constructeurs
        /// <summary>
        /// Initialise un rectangle avec les coins arrondis dont le point supérieur gauche
        /// aura pour coordonnées (<i>x</i>,<i>y</i>), comme largeur/hauteur <i>width</i> et <i>height</i>, des arrondis
        /// aux coins <i>rc</i> et enfin les arrondis auront comme rayon <i>radius</i>
        /// </summary>
        /// <param name="x">Abscisse du point supérieur gauche</param>
        /// <param name="y">Ordonnée du point supérieur gauche</param>
        /// <param name="width">Largeur du rectangle</param>
        /// <param name="height">Hauteur du rectangle</param>
        /// <param name="rc">Coins arrondis</param>
        /// <param name="radius">Rayon des arrondis</param>
        public RoundRectangle(int x, int y, int width, int height, RoundedCorner rc, float radius)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.radius = radius;
            this.roundedCorners = rc;
        }

        /// <summary>
        /// Initialise un rectangle à partir du Rectangle <i>rect</i>, des arrondis
        /// aux coins <i>rc</i> et enfin les arrondis auront comme rayon <i>radius</i>
        /// </summary>
        /// <param name="rect">Rectangle de base</param>
        /// <param name="rc">Coins arrondis</param>
        /// <param name="radius">Rayon des arrondis</param>
        public RoundRectangle(Rectangle rect, RoundedCorner rc, float radius)
        {
            this.x = rect.X;
            this.y = rect.Y;
            this.width = rect.Width;
            this.height = rect.Height;
            this.radius = radius;
            this.roundedCorners = rc;
        }

        /// <summary>
        /// Initialise un rectangle avec les coins arrondis dont le point supérieur gauche
        /// aura pour coordonnées <i>location</i>, comme taille <i>size</i>, des arrondis
        /// aux coins <i>rc</i> et enfin les arrondis auront comme rayon <i>radius</i>
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="rc">Coins arrondis</param>
        /// <param name="radius">Rayon des arrondis</param>
        public RoundRectangle(Point location, Size size, RoundedCorner rc, float radius)
        {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.Width;
            this.height = size.Height;
            this.radius = radius;
            this.roundedCorners = rc;
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// Obtient ou défini une enumération indiquant les coins arrondis
        /// </summary>
        public RoundedCorner RoundedCorners
        {
            get { return this.roundedCorners; }
            set { this.roundedCorners = value; }
        }

        /// <summary>
        /// Obtient ou défini la taille du rectangle
        /// </summary>
        public Size Size
        {
            get { return new Size(this.width, this.height); }
            set
            {
                this.width = value.Width;
                this.height = value.Height;
            }
        }

        /// <summary>
        /// Obtient ou défini l'abscisse du coin supérieur gauche du rectangle
        /// </summary>
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        /// <summary>
        /// Obtient ou défini l'ordonnée du coin supérieur gauche du rectangle
        /// </summary>
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        /// <summary>
        /// Obtient ou défini la largeur du rectangle
        /// </summary>
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        /// <summary>
        /// Obtient ou défini la hauteur du rectangle
        /// </summary>
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        /// <summary>
        /// Obtient l'abscisse des coins de gauche
        /// </summary>
        public int Left
        {
            get { return this.x; }
        }

        /// <summary>
        /// Obtient l'abscisse des coins de droit
        /// </summary>
        public int Right
        {
            get { return this.x + this.width; }
        }

        /// <summary>
        /// Obtient l'ordonnée des coins du haut
        /// </summary>
        public int Top
        {
            get { return this.y; }
        }

        /// <summary>
        /// Obtient l'ordonnée des coins du bas
        /// </summary>
        public int Bottom
        {
            get { return this.y + this.height; }
        }

        /// <summary>
        /// Obtient ou défini l'emplacement du coin supérieur gauche
        /// </summary>
        public Point Location
        {
            get { return new Point(this.x, this.y); }
            set
            {
                this.x = value.X;
                this.y = value.Y;
            }
        }

        /// <summary>
        /// Obtient ou défini le rayon de courbure des coins
        /// </summary>
        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Agrandi le rectangle de la taille spécifiée
        /// </summary>
        /// <param name="width">largeur</param>
        /// <param name="height">hauteur</param>
        public void Inflate(int width, int height)
        {
            this.x -= width;
            this.y -= height;
            this.width += (2 * width);
            this.height += (2 * height);
        }

        /// <summary>
        /// Agrandi le rectangle de la taille spécifiée
        /// </summary>
        /// <param name="size">taille</param>
        public void Inflate(Size size)
        {
            this.Inflate(size.Width, size.Height);
        }

        /// <summary>
        /// Crée et retourne une copie RoundRectangle agrandi de la taille spécifié.
        /// </summary>
        /// <param name="rrect">RoundRectangle</param>
        /// <param name="width">Largeur</param>
        /// <param name="height">Hauteur</param>
        /// <returns>Une copie du RoundRectangle agrandie</returns>
        public static RoundRectangle Inflate(RoundRectangle rrect, int width, int height)
        {
            RoundRectangle r = rrect;
            r.Inflate(width, height);
            return r;
        }

        /// <summary>
        /// Retourne un RoundRectangle correspondant à l'intersection des RoundRectangles a et b
        /// </summary>
        /// <param name="a">RoundRectangle</param>
        /// <param name="b">RoundRectangle</param>
        /// <param name="rc">Coins à arrondir</param>
        /// <param name="radius">Rayon de coubure des arrondis</param>
        /// <returns></returns>
        /// <remarks>Si aucune intersection n'est trouvée entre les RoundRectangles, un RoundRectangle est retourné</remarks>
        public static RoundRectangle Intersect(RoundRectangle a, RoundRectangle b, RoundedCorner rc, float radius)
        {
            int left = Math.Max(a.Left, b.Left);
            int right = Math.Min(a.Right, b.Right);
            int top = Math.Max(a.Top, b.Top);
            int bottom = Math.Min(a.Bottom, b.Bottom);

            if (left <= right && top <= bottom)
            {
                return new RoundRectangle(
                left, right - left, top, bottom - top, rc, radius);
            }
            return RoundRectangle.Empty;
        }

        public static RoundRectangle Intersect(RoundRectangle a, Rectangle b, RoundedCorner rc, float radius)
        {
            int left = Math.Max(a.Left, b.Left);
            int right = Math.Min(a.Right, b.Right);
            int top = Math.Max(a.Top, b.Top);
            int bottom = Math.Min(a.Bottom, b.Bottom);

            if (left <= right && top <= bottom)
            {
                return new RoundRectangle(
                left, right - left, top, bottom - top, rc, radius);
            }
            return RoundRectangle.Empty;
        }

        /// <summary>
        /// Replace ce RoundRectangle par l'intersection de celui-ci de rrect
        /// </summary>
        /// <param name="rrect">RoundRectangle</param>
        public void Intersect(RoundRectangle rrect)
        {
            RoundRectangle r = RoundRectangle.Intersect(rrect, this, this.roundedCorners, this.radius);
            this.x = r.x;
            this.y = r.y;
            this.width = r.width;
            this.height = r.height;
        }

        /// <summary>
        /// Retourne <i>true</i> si ce RoundRectangle possède une intersection avec rrect, <i>false</i> sinon
        /// </summary>
        /// <param name="rrect">RoundRectangle</param>
        /// <returns><i>true</i> si intersection, <i>false</i> sinon</returns>
        public bool IntersectWith(RoundRectangle rrect)
        {
            return ((rrect.Left < this.Right && this.Left < rrect.Right) &&
            (rrect.Top < this.Bottom && this.Top < rrect.Bottom));
            //if (((rect.X < (this.X + this.Width)) && (this.X < (rect.X + rect.Width))) && (rect.Y < (this.Y + this.Height)))
            //{
            // return (this.Y < (rect.Y + rect.Height));
            //}
            //return false;
        }

        /// <summary>
        /// Retourne un RoundRectangle qui correspond à l'intersection des RoundRectangle a et b et dont les
        /// coins <i>rc</i> sont arrondis d'un rayon de <i>radius</i>
        /// </summary>
        /// <param name="a">RoundRectangle</param>
        /// <param name="b">RoundRectangle</param>
        /// <param name="rc">Coins à arrondir</param>
        /// <param name="radius">Rayon de courbure des arrondis</param>
        /// <returns>RoundRectangle de l'union</returns>
        public static RoundRectangle Union(RoundRectangle a, RoundRectangle b, RoundedCorner rc, float radius)
        {
            int left = Math.Min(a.Left, b.Left);
            int right = Math.Max(a.Right, b.Right);
            int top = Math.Min(a.Top, b.Top);
            int bottom = Math.Max(a.Bottom, b.Bottom);

            return new RoundRectangle(
            left, right - left, top, bottom - top, rc, radius);
        }

        /// <summary>
        /// Déplace le RoundRectangle d'après les coordonnées spécifiés
        /// </summary>
        /// <param name="x">Déplacement sur l'axe des abscisses</param>
        /// <param name="y">Déplacement sur l'axe des ordonées</param>
        public void Offset(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        /// <summary>
        /// Déplace le RoundRectangle d'après les ccordonnées spécifiées
        /// </summary>
        /// <param name="point">Point contenant les informations de déplacements</param>
        public void Offset(Point point)
        {
            this.x += point.X;
            this.y += point.Y;
        }

        /// <summary>
        /// Retourne un booléen indiquant si le point spécifié fait partie du RoundRectangle
        /// </summary>
        /// <param name="x">Abscisse</param>
        /// <param name="y">Ordonnée</param>
        /// <returns><i>true</i> si le point se trouve à l'intérieur du RoundRectangle, <i>false</i> sinon</returns>
        public bool Contains(int x, int y)
        {
            return (
            this.x <= x &&
            x <= this.Right &&
            this.y <= y &&
            y <= this.Bottom);
        }

        /// <summary>
        /// Retourne un booléen indiquant si le point spécifié fait partie du RoundRectangle
        /// </summary>
        /// <param name="pt">Point à vérifier</param>
        /// <returns><i>true</i> si le point se trouve à l'intérieur du RoundRectangle, <i>false</i> sinon</returns>
        public bool Contains(Point pt)
        {
            return Contains(pt.X, pt.Y);
        }

        /// <summary>
        /// Retourne un <see cref="GraphicsPath"/> représentant le RoundRectangle.
        /// </summary>
        /// <returns><see cref="GraphicsPath"/></returns>
        /// <seealso cref="GraphicsPath"/>
        public GraphicsPath ToGraphicsPath()
        {
            GraphicsPath gp;
            gp = new GraphicsPath();

            //Rectangle baseRect = new Rectangle(this.Location, this.Size);

            // si le rayon est inférieur ou égal à 0
            // on retourne le rectangle de base
            if (radius <= 0F)
            {
                gp.AddRectangle(new Rectangle(this.Location, this.Size));
                gp.CloseFigure();
                return gp;
            }

            float diameter = radius * 2F;

            if (this.height <= this.width)
            {
                if (diameter >= this.height &&
                (((this.roundedCorners & (RoundedCorner.BottomLeft | RoundedCorner.TopLeft)) == (RoundedCorner.BottomLeft | RoundedCorner.TopLeft))
                || ((this.roundedCorners & (RoundedCorner.BottomRight | RoundedCorner.TopRight)) == (RoundedCorner.BottomRight | RoundedCorner.TopRight))))
                {
                    diameter = this.height;
                }
            }
            else
            {
                if (diameter >= this.width &&
                (((this.roundedCorners & (RoundedCorner.BottomLeft | RoundedCorner.BottomRight)) == (RoundedCorner.BottomLeft | RoundedCorner.BottomRight))
                || ((this.roundedCorners & (RoundedCorner.TopLeft | RoundedCorner.TopRight)) == (RoundedCorner.TopLeft | RoundedCorner.TopRight))))
                {
                    diameter = this.width;
                }
            }
            SizeF size = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(this.Location, size);

            // arc en haut à gauche
            if ((this.roundedCorners & RoundedCorner.TopLeft) == RoundedCorner.TopLeft)
            {
                gp.AddArc(arc, 180, 90);
            }
            else
            {
                gp.AddLine(new PointF(arc.Left, arc.Top), new PointF(arc.Left, arc.Top));
            }



            // arc en haut à droite
            arc.X = (float)this.Right - diameter;
            if ((this.roundedCorners & RoundedCorner.TopRight) == RoundedCorner.TopRight)
            {
                gp.AddArc(arc, 270, 90);
            }
            else
            {
                gp.AddLine(new PointF(arc.Right, arc.Top), new PointF(arc.Right, arc.Top));
            }

            arc.Y = (float)this.Bottom - diameter;

            // arc en bas à droite
            if ((this.roundedCorners & RoundedCorner.BottomRight) == RoundedCorner.BottomRight)
            {
                gp.AddArc(arc, 0, 90);
            }
            else
            {
                gp.AddLine(new PointF(arc.Right, arc.Bottom), new PointF(arc.Right, arc.Bottom));
            }

            // arc en bas à gauche
            arc.X = (float)this.Left;
            if ((this.roundedCorners & RoundedCorner.BottomLeft) == RoundedCorner.BottomLeft)
            {
                gp.AddArc(arc, 90, 90);
            }
            else
            {
                gp.AddLine(new PointF(arc.Left, arc.Bottom), new PointF(arc.Left, arc.Bottom));
            }

            gp.CloseFigure();

            return gp;

        }

        /// <summary>
        /// Retourne un <see cref="Rectangle"/>
        /// </summary>
        /// <returns><see cref="Rectangle"/></returns>
        public Rectangle ToRectangle()
        {
            return new Rectangle(this.Location, this.Size);
        }
        #endregion

        #region overrides
        public override string ToString()
        {
            return string.Format("{{X={0},Y={1},Width={2},Height={3},Radius={4}}}",
            this.x,
            this.y,
            this.width,
            this.height,
            this.radius);
        }

        public static bool operator ==(RoundRectangle a, RoundRectangle b)
        {
            return (a.x == b.x &&
            a.y == b.y &&
            a.width == b.width &&
            a.height == b.height);
        }

        public static bool operator !=(RoundRectangle a, RoundRectangle b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            bool ret = false;
            if (obj is RoundRectangle)
            {
                RoundRectangle r = (RoundRectangle)obj;
                ret = (r.x == this.x && r.y == this.y &&
                r.width == this.width && r.height == this.height);
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}

