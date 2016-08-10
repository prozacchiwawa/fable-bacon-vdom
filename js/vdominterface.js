var h = require('virtual-dom/h');
var diff = require('virtual-dom/diff');
var patch = require('virtual-dom/patch');
var createElement = require('virtual-dom/create-element');
var test = require('./test');
var VText = require('virtual-dom/vnode/vtext');
var VNode = require('virtual-dom/vnode/vnode');
var Bacon = require('baconjs');

var rootElement = null;
var oldTree = null;
var element = document.getElementById('app');
var eventQueue = [];
var eventHookType = function(eventfun) { this.eventfun = eventfun; };
eventHookType.prototype.hook = function(node, propertyName, previousValue) {
    console.log(node);
    var eventName = propertyName.substr(2);
    node.__eventHandlers = node.__eventHandlers || {};
    if (node.__eventHandlers[eventName]) {
        node.removeEventListener(eventName, node.__eventHandlers[eventName]);
    }
    var self = this;
    var eventHandler = node.__eventHandlers[eventName] = function(event) {
        self.eventfun(event);
    };
    node.addEventListener(eventName, eventHandler);
};
var vtext = function(t) { return new VText(t); }
var vnode = function(tag) {
    return function(props) {
        return function(on) {
            return function(children) {
                var propdata = {};
                while (props.head) {
                    propdata[props.head.name] = props.head.value;
                    props = props.tail;
                }
                while (on.head) {
                    propdata["on" + on.head.name] = new eventHookType(on.head.response);
                    on = on.tail;
                }
                var childdata = [];
                while (children.head) {
                    childdata.push(children.head);
                    children = children.tail;
                }
                return new VNode(tag, propdata, childdata);
            }
        }
    }
}
var post = function(stream) {
    return function(msg) {
        console.log('post',stream,msg);
        stream.push(msg);
    };
}
var msgStream = Bacon.Bus();
var program = test.default.main({ vnode: vnode, vtext, vtext, post: post, stream: msgStream });
var stateStream = msgStream.scan(program.init(), function(state,msg) {
    console.log('update',state,msg);
    return program.update(msg, state);
});
var domStream = stateStream.map(function (state) {
    console.log('view',state);
    return program.view(state);
});
domStream.subscribe(function(tree_) {
    var tree = tree_.value();
    console.log('dom',tree);
    if (!rootElement) {
        rootElement = createElement(tree);
        element.appendChild(rootElement);
    } else {
        var patches = diff(oldTree, tree);
        rootElement = patch(rootElement, patches);
    }
    oldTree = tree;
});
