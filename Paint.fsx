#load "lib\Bottone.fsx"
#load "lib\VWCoordinates.fsx"
open System.Windows.Forms
open System.Drawing
open Bottone
open VWCoordinates

(**  MAIN  **)

// CONFIGURAZIONE
// array di colori selezionabili
let colori = [| Color.LightBlue; Color.LightGreen; Color.LightPink; Color.BlueViolet |]
// altezza toolbar
let tHeight = 100

// FINE CONFIGURAZIONE

let toolbar = new Panel(Dock=DockStyle.Top, Height=tHeight)
let mw = new Panel(Dock=DockStyle.Fill)
let f = new Form(Text="Paint",TopMost=true,Size=Size(500,500))
f.Show()

for i=0 to colori.Length - 1 do
  let b = new Bottone(
                      Location=Point(i*tHeight, 0),
                      Size=Size(tHeight, tHeight),
                      ForegroundColor=colori.[i],
                      LabelText=colori.[i].Name,
                      LightTickness=0,
                      ShadowTickness=0,
                      MouseClick=fun e -> printfn "Selezionato: %s" colori.[i].Name
                      )
  toolbar.Controls.Add(b)

f.Controls.Add(mw)
f.Controls.Add(toolbar)
