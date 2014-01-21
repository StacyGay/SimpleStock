module.exports = function (grunt) {
    "use strict";
    grunt.initConfig({

        ts: {
            options: {                      // use to override the default options, See : http://gruntjs.com/configuring-tasks#options
                target: 'es3',              // es3 (default) / or es5
                module: 'amd',              // amd (default), commonjs
                sourcemap: true,            // true  (default) | false
                declaration: false,         // true | false  (default)
                nolib: false,               // true | false (default)
                comments: false             // true | false (default)
            },
            dev: {                          // a particular target   
                src: ["app/**/*.ts"],           // The source typescript files, See : http://gruntjs.com/configuring-tasks#files
                reference: "app/reference.ts",  // If specified, generate this file that you can use for your reference management
                watch: 'app',                // If specified, configures this target to watch the specified director for ts changes and reruns itself.
                out: 'app.js',
                options: {                  // override the main options, See : http://gruntjs.com/configuring-tasks#options
                    sourcemap: true
                },
            },
            live: {                         // a particular target   
                src: ["app/**/*.ts"],       // The source typescript files, See : http://gruntjs.com/configuring-tasks#files                
                out: 'app.js',
                options: {                  // override the main options, See : http://gruntjs.com/configuring-tasks#options
                    sourcemap: false
                },
            },
        },
        uglify: {
            my_target: {
                files: {
                    'app.min.js': ['app.js']
                }
            }
        }
    });

    grunt.loadNpmTasks("grunt-ts");
    grunt.loadNpmTasks('grunt-contrib-uglify'); // minifies


    grunt.registerTask("default", ["ts:dev"]);
    grunt.registerTask("ts-dev", "Compile all typescript files to dev folder and watch for changes", ["ts:dev"]);
    grunt.registerTask("ts-live-uglify", "Compiles all Typescript files into one and minifies it", ["ts:live", "uglify"]);

};