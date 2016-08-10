#r "node_modules/fable-core/Fable.Core.dll"

open Fable.Core
open Fable.Import.Browser

let window_setTimeout f a = window.setTimeout(f,a)

[<Emit("console.log($0,$1)")>]
let log : string -> 'a -> unit = fun s o -> failwith "JS only"

[<Emit("('' + $0)")>]
let toString : 'a -> string = fun a -> failwith "JS only"

[<Emit("{ function toList(a) { var target = []; while (a.head) { target.push(a.head); a = a.tail; } return target; }; return toList($0).join(''); }")>]
let concat : string list -> string = fun l -> failwith "JS only"

type VNode = Unused0
type Property = { name : string; value : string }
type Response = { name : string; response : unit -> unit }

type 'msg MsgStream = Unused1

type 'msg VDom =
    { vnode  : string -> Property list -> Response list -> VNode list -> VNode;
      vtext  : string -> VNode;
      post   : 'msg MsgStream -> 'msg -> unit;
      stream : 'msg MsgStream;
    }

type Rec = { a : int; b : string }

type Msg = NoOp | Text of string

log "test" {a = 10; b = "test"}

type ('msg, 'state) Program = 
    { init : unit -> 'state;
      update : 'msg -> 'state -> 'state;
      view : 'state -> VNode
    }

let init () =
    { a = 0; b = "Hi there" }

let update action state =
    match action with
    |   NoOp -> state
    |   Text t -> { state with a = state.a + 1; b = t }

let view vdom state =
    let response = fun evt -> (vdom.post vdom.stream (Text (concat [toString state.a; " Change!"]))) in
    (vdom.vnode "button" [{name="className"; value="testbutton"}] [{name="click"; response=response}] [vdom.vtext state.b])

let main vdom =
    { init = init;
      update = update;
      view = (view vdom)
    }
