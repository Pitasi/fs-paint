module Bottone

open System.Windows.Forms
open System.Drawing

type Bottone() as this =
  inherit UserControl()
   
  let mutable FColor = Color.Red
  let mutable SColor = Color.FromArgb(175, 0, 0, 0)
  let mutable LTickness = 1
  let mutable STickness = 3

  let margin = 10
  let mutable l = new Label(
                          Location=Point(margin/2, margin/2),                     // margine per testo
                          Size=Size(this.Size.Width - margin, this.Size.Height - margin),
                          BackColor=Color.FromArgb(0, 0, 0, 0),
                          TextAlign=ContentAlignment.BottomCenter,
                          Text="")
  let updateLabel(s: Size) =
    l.Size <- Size(s.Width - margin, s.Height - margin)
    l.Invalidate() 

  // da eseguire dopo aver creato l'oggetto
  do this.Controls.Add(l)

  member this.MouseClick
    with set(v) = l.MouseClick.Add(v)

  member this.ForegroundColor
    with get() = FColor
    and set(v) = FColor <- v

  member this.ShadowTickness
    with get() = STickness
    and set(v) = STickness <- v

  member this.LightTickness
    with get() = LTickness
    and set(v) = LTickness <- v

  member this.LabelText
    with get() = l.Text
    and set(v) =
        l.Text <- v
        l.Invalidate()

  override this.OnPaint e =
    let g = e.Graphics
    g.SmoothingMode <- Drawing2D.SmoothingMode.HighQuality

    let w, h = this.Width, this.Height
    let r = new Rectangle(Point(0, 0), this.Size)

    use MainBrush = new SolidBrush(FColor)
    use ShadowBrush = new SolidBrush(SColor)

    // rettangolo
    g.FillRectangle(MainBrush, r)
    // effetto ombra in basso a destra
    if STickness > 0 then
      use p = new Pen(ShadowBrush, Width=single STickness)
      g.DrawLine(p, w-(STickness/2), 0, w-(STickness/2), h)
      g.DrawLine(p, 0, h-(STickness/2), w-(STickness/2), h-(STickness/2))
    // effetto luce in alto a sinistra
    if LTickness > 0 then
      use p = new Pen(Brushes.White, Width=single LTickness)
      g.DrawLine(p, 0, 0, w, 0)
      g.DrawLine(p, 0, 0, 0, h)

    // ridisegno la Label (in caso di cambiamento di dimensioni)
    updateLabel(this.Size)
