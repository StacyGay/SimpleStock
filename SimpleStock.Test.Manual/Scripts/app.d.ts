declare module Main {
    class App {
        public name: string;
        constructor(name: string);
        public getMessage(): string;
    }
}
declare var app: Main.App;
