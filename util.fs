module Util

open Fable.Core

[<Emit("console.log($0,$1)")>]
let log : string -> 'a -> unit = fun s o -> failwith "JS only"

[<Emit("('' + $0)")>]
let toString : 'a -> string = fun a -> failwith "JS only"

[<Emit("{ function toList(a) { var target = []; while (a.head) { target.push(a.head); a = a.tail; } return target; }; return toList($0).join(''); }")>]
let concat : string list -> string = fun l -> failwith "JS only"
