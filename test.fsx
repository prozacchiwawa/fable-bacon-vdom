#r "node_modules/fable-core/Fable.Core.dll"

open Fable.Core
open Fable.Import.Browser

#load "util.fs"
#load "vdom.fs"

type State = { a : int; b : string }
type Msg = NoOp | Text of string

let init arg =
    { a = 0; b = arg }

let update action state =
    match action with
    |   NoOp -> state
    |   Text t -> { state with a = state.a + 1; b = t }

let view (vdom : Msg VDom.VDom) state =
    let response = fun evt -> (vdom.post vdom.stream (Text "Clicked!")) in
    (vdom.vnode "button" [{name="className"; value="testbutton"}] [{name="click"; response=response}] [vdom.vtext (String.concat "" [Util.toString state.a; " "; state.b])])

let main vdom arg =
    { VDom.init = init;
      VDom.update = update;
      VDom.view = (view vdom)
    }
