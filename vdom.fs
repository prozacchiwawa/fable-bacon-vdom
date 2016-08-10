module VDom

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

