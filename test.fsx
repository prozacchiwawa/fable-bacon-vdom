#r "node_modules/fable-core/Fable.Core.dll"

open Fable.Core
open Fable.Import.Browser

#load "util.fs"
#load "vdom.fs"

type Rec = { a : int; b : string }

type Msg = NoOp | Text of string

Util.log "test" {a = 10; b = "test"}

type ('msg, 'state) Program = 
    { init : unit -> 'state;
      update : 'msg -> 'state -> 'state;
      view : 'state -> VDom.VNode
    }

let init () =
    { a = 0; b = "Hi there" }

let update action state =
    match action with
    |   NoOp -> state
    |   Text t -> { state with a = state.a + 1; b = t }

let view (vdom : Msg VDom.VDom) state =
    let response = fun evt -> (vdom.post vdom.stream (Text (Util.concat [Util.toString state.a; " Change!"]))) in
    (vdom.vnode "button" [{name="className"; value="testbutton"}] [{name="click"; response=response}] [vdom.vtext state.b])

let main vdom =
    { init = init;
      update = update;
      view = (view vdom)
    }
