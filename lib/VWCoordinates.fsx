module VWCoordinates

open System.Drawing

type VWCoordinates() =
  let wv = new Drawing2D.Matrix()
  let vw = new Drawing2D.Matrix()

  let toPointF(p:Point) = PointF(single p.X, single p.Y)

  member this.WV = wv // matrice per convertire mondo in vista
  member this.VW = vw // matrice per convertire vista in mondo

  member this.Rotate(a:single) =
    wv.Rotate(a)
    vw.Rotate(-a, Drawing2D.MatrixOrder.Append)

  member this.RotateAt(a:single, p:PointF) =
    wv.RotateAt(a, p)
    vw.RotateAt(-a, p, Drawing2D.MatrixOrder.Append)

  member this.Translate(tx:single, ty:single) =
    wv.Translate(tx, ty)
    vw.Translate(-tx, -ty, Drawing2D.MatrixOrder.Append)

  member this.Scale(sx:single, sy:single) =
    wv.Scale(sx, sy)
    vw.Scale(1.f / sx, 1.f / sy, Drawing2D.MatrixOrder.Append)

  member this.ScaleAt(sx: single, sy: single, p: PointF) =
    this.Translate(p.X, p.Y)
    this.Scale(sx, sy)
    this.Translate(-p.X, -p.Y)

  member this.TransformPointVW(p:Point) = 
    let a = [| p |> toPointF |] // array con il solo punto p (PointF)
    this.VW.TransformPoints(a)
    a.[0]
