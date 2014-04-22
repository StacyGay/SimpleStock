var Main;
(function (Main) {
    var App = (function () {
        function App(name) {
            this.name = name;
        }
        App.prototype.getMessage = function () {
            return "Hello " + this.name + "!";
        };
        return App;
    })();
    Main.App = App;
})(Main || (Main = {}));

var app = new Main.App("Stacy");
console.log(app.getMessage());
//# sourceMappingURL=app.js.map
