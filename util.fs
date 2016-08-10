module Util

open Fable.Core

[<Emit("console.log($0,$1)")>]
let log : string -> 'a -> unit = fun s o -> failwith "JS only"

[<Emit("('' + $0)")>]
let toString : 'a -> string = fun a -> failwith "JS only"
