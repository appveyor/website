/* jshint browser:false, node:true */

"use strict";

module.exports = function(grunt) {

    grunt.initConfig({
        dirs: {
            dest: "_site",
            src: "src",
            tmp: ".tmp"
        },

        jekyll: {
            site: {
                options: {
                    bundleExec: true,
                    incremental: false
                }
            }
        },

        htmlmin: {
            dist: {
                options: {
                    collapseBooleanAttributes: true,
                    collapseWhitespace: true,
                    conservativeCollapse: false,
                    decodeEntities: true,
                    ignoreCustomComments: [/^\s*google(off|on):\s/],
                    minifyCSS: {
                        compatibility: "ie9",
                        keepSpecialComments: 0
                    },
                    minifyJS: true,
                    minifyURLs: false,
                    processConditionalComments: true,
                    removeAttributeQuotes: true,
                    removeComments: true,
                    removeOptionalAttributes: true,
                    removeOptionalTags: true,
                    removeRedundantAttributes: true,
                    removeScriptTypeAttributes: true,
                    removeStyleLinkTypeAttributes: true,
                    removeTagWhitespace: false,
                    sortAttributes: true,
                    sortClassName: true
                },
                expand: true,
                cwd: "<%= dirs.dest %>",
                dest: "<%= dirs.dest %>",
                src: [
                    "**/*.aspx",
                    "**/*.html"
                ]
            }
        },

        concat: {
            foundation: {
                src: [
                    "<%= dirs.src %>/assets/css/foundation/normalize.css",
                    "<%= dirs.src %>/assets/css/foundation/foundation.css"
                ],
                dest: "<%= dirs.tmp %>/css/foundation.css"
            },
            css: {
                src: [
                    "<%= concat.foundation.dest %>",
                    "<%= dirs.src %>/assets/css/site/*.css",
                    "<%= dirs.src %>/assets/css/pages/*.css"
                ],
                dest: "<%= dirs.dest %>/assets/css/pack.css"
            },
            js: {
                src: [
                    "<%= dirs.src %>/assets/js/vendor/anchor.js",
                    "<%= dirs.src %>/assets/js/google-analytics.js",
                    "<%= dirs.src %>/assets/js/main.js"
                ],
                dest: "<%= dirs.dest %>/assets/js/pack.js"
            },
            jqueryJS: {
                src: [
                    "<%= dirs.src %>/assets/js/vendor/jquery-*.min.js",
                    "<%= dirs.src %>/assets/js/vendor/foundation/*.js",
                    "<%= dirs.src %>/assets/js/jquery.js"
                ],
                dest: "<%= dirs.dest %>/assets/js/jquery-pack.js"
            }
        },

        autoprefixer: {
            options: {
                browsers: [
                    "last 2 version",
                    "> 1%",
                    "Edge >= 12",
                    "Explorer >= 9",
                    "Firefox ESR",
                    "Opera 12.1"
                ]
            },
            pack: {
                src: "<%= concat.css.dest %>",
                dest: "<%= concat.css.dest %>"
            }
        },

        uncss: {
            options: {
                ignore: [
                    /(#|\.)top-bar(\-[a-zA-Z]+)?/,
                    /(#|\.)topbar(\-[a-zA-Z]+)?/,
                    /(#|\.)f-topbar-fixed(\-[a-zA-Z]+)?/,
                    /(#|\.)dropdown(\-[a-zA-Z]+)?/,
                    /\.no\-js/,
                    /meta.foundation(\-[a-zA-Z]+)?/,
                    ".anchorjs-link"
                ],
                htmlroot: "<%= dirs.dest %>",
                ignoreSheets: [/fonts.googleapis/],
                stylesheets: ["/assets/css/pack.css"]
            },
            dist: {
                src: "<%= dirs.dest %>/**/*.html",
                dest: "<%= concat.css.dest %>"
            }
        },

        cssmin: {
            minify: {
                options: {
                    keepSpecialComments: 0,
                    compatibility: "ie9"
                },
                files: {
                    "<%= concat.css.dest %>": "<%= concat.css.dest %>"
                }
            }
        },

        uglify: {
            options: {
                compress: {
                    warnings: false
                },
                mangle: true,
                preserveComments: false
            },
            minify: {
                files: {
                    "<%= concat.js.dest %>": "<%= concat.js.dest %>",
                    "<%= concat.jqueryJS.dest %>": "<%= concat.jqueryJS.dest %>"
                }
            }
        },

        filerev: {
            css: {
                src: "<%= dirs.dest %>/assets/css/*.css"
            },
            js: {
                src: "<%= dirs.dest %>/assets/js/*.js"
            },
            images: {
                src: [
                    "<%= dirs.dest %>/assets/images/**/*.{jpg,jpeg,gif,png,svg}",
                    "!<%= dirs.dest %>/assets/images/appveyor-logo*.png",
                    "!<%= dirs.dest %>/assets/images/favicons/*.{jpg,jpeg,gif,png,svg}",
                    "!<%= dirs.dest %>/assets/images/testimonials/*.png"
                ]
            }
        },

        useminPrepare: {
            html: "<%= dirs.dest %>/index.html",
            options: {
                dest: "<%= dirs.dest %>",
                root: "<%= dirs.dest %>"
            }
        },

        usemin: {
            css: "<%= dirs.dest %>/assets/css/pack*.css",
            html: ["<%= dirs.dest %>/**/*.html", "<%= dirs.dest %>/**/*.aspx"],
            options: {
                assetsDirs: [
                    "<%= dirs.dest %>/",
                    "<%= dirs.dest %>/assets/images/"
                ]
            }
        },

        connect: {
            options: {
                base: "<%= dirs.dest %>/",
                hostname: "localhost"
            },
            livereload: {
                options: {
                    base: "<%= dirs.dest %>/",
                    livereload: 35729,
                    open: true,  // Automatically open the webpage in the default browser
                    port: 4000
                }
            },
            linkChecker: {
                options: {
                    base: "<%= dirs.dest %>/",
                    port: 9001
                }
            }
        },

        watch: {
            options: {
                livereload: "<%= connect.livereload.options.livereload %>"
            },
            dev: {
                files: [
                    "<%= dirs.src %>/**",
                    ".jshintrc",
                    "_config.yml",
                    "Gruntfile.js"
                ],
                tasks: "dev"
            },
            build: {
                files: [
                    "<%= dirs.src %>/**",
                    ".jshintrc",
                    "_config.yml",
                    "Gruntfile.js"
                ],
                tasks: "build"
            }
        },

        csslint: {
            options: {
                csslintrc: ".csslintrc"
            },
            src: [
                "<%= dirs.src %>/assets/css/site/*.css",
                "<%= dirs.src %>/assets/css/pages/*.css"
            ]
        },

        jshint: {
            options: {
                jshintrc: ".jshintrc"
            },
            files: {
                src: [
                    "Gruntfile.js",
                    "<%= dirs.src %>/assets/js/*.js"
                ]
            }
        },

        htmllint: {
            options: {
                ignore: [
                    "Consider using the \"h1\" element as a top-level heading only (all \"h1\" elements are treated as top-level headings by many screen readers and other tools)."
                ]
            },
            src: [
                "<%= dirs.dest %>/**/*.html",
                "!<%= dirs.dest %>/updates/**/*.html"
            ]
        },

        linkChecker: {
            options: {
                callback: function (crawler) {
                    crawler.addFetchCondition(function (url) {
                        return url.path !== "/assets/js/j.src" &&
                               url.path !== "/assets/js/+g+" &&
                               url.path !== "/feed.xml" &&
                               url.port !== "9023";
                    });
                },
                interval: 1,        // 1 ms; default 250
                maxConcurrency: 5   // default; bigger doesn't seem to improve time
            },
            dev: {
                site: "localhost",
                options: {
                    initialPort: "<%= connect.linkChecker.options.port %>"
                }
            }
        },

        clean: {
            dist: [
                "<%= dirs.dest %>/",
                "<%= dirs.tmp %>/"
            ]
        }

    });

    // Load any grunt plugins found in package.json.
    require("load-grunt-tasks")(grunt, { scope: "dependencies" });
    require("time-grunt")(grunt);

    grunt.registerTask("markdownlint", "Run markdownlint", function () {
        var done = this.async();
        var markdownlint = require("markdownlint");
        var options = {
            config: require("./markdownlint.json"),
            files: grunt.file.expand({ filter: "isFile" }, ["src/**/*.md"])
        };

        markdownlint(options, function callback(err, result) {
            var resultString = err || (result || "").toString();

            if (resultString) {
                grunt.fail.warn("\n" + resultString + "\n");
            }
            done(!err || !resultString);
        });
    });

    grunt.registerTask("build", [
        "clean",
        "jekyll",
        "useminPrepare",
        "concat",
        "autoprefixer",
        "uncss",
        "cssmin",
        "uglify",
        "filerev",
        "usemin",
        "htmlmin"
    ]);

    grunt.registerTask("test", [
        "build",
        "csslint",
        "jshint",
        "markdownlint",
        "htmllint",
        "connect:linkChecker",
        "linkChecker"
    ]);

    grunt.registerTask("dev", [
        "jekyll",
        "useminPrepare",
        "concat",
        "autoprefixer",
        "filerev",
        "usemin"
    ]);

    grunt.registerTask("server", [
        "build",
        "connect:livereload",
        "watch:build"
    ]);

    grunt.registerTask("default", [
        "dev",
        "connect:livereload",
        "watch:dev"
    ]);

};
