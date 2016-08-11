# fable-bacon-vdom
Minimal f# program with virtual-dom and baconjs

I've tried to come up with the minimum possible practical example of using
f# with virtual-dom to make an elm-architecture like program.  I came up
with about 180loc providing a single run method which can host an
elm-architecture like f# program in an element.  This effort was mainly an
exercise, and isn't as complete as other examples, but it's very short,
and provides a simple build example using fable and browserify to build
f# code to javascript.

To try it out, do ```make``` and then view test.html
