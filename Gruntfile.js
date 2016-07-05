"use strict";

module.exports = function(grunt) {

    grunt.initConfig({
        dirs: {
            dest: "_site",
            src: "src"
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
                    "**/*.html",
                    "!404.html"
                ]
            }
        },

        concat: {
            css: {
                src: ["<%= dirs.src %>/assets/css/**/*.css"],
                dest: "<%= dirs.dest %>/assets/css/pack.css"
            },
            js: {
                src: [
                    "<%= dirs.src %>/assets/js/jquery-*.min.js",
                    "<%= dirs.src %>/assets/js/foundation/*.js",
                    "<%= dirs.src %>/assets/js/anchor.js",
                    "<%= dirs.src %>/assets/js/main.js"
                ],
                dest: "<%= dirs.dest %>/assets/js/pack.js"
            }
        },

        uncss: {
            options: {
                ignore: [
                    /(#|\.)anchorjs(\-[a-zA-Z]+)?/,
                    // Bootstrap selectors added via JS
                    /\w\.in/,
                    ".fade",
                    ".collapse",
                    ".collapsing",
                    /(#|\.)top-bar(\-[a-zA-Z]+)?/,
                    /(#|\.)topbar(\-[a-zA-Z]+)?/,
                    /(#|\.)f-topbar-fixed(\-[a-zA-Z]+)?/,
                    /(#|\.)dropdown(\-[a-zA-Z]+)?/,
                    /(#|\.)(open)/,
                    /meta.foundation(\-[a-zA-Z]+)?/
                ],
                htmlroot: "<%= dirs.dest %>",
                ignoreSheets: [/fonts.googleapis/, /gist.github/],
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
                    "<%= concat.js.dest %>": "<%= concat.js.dest %>"
                }
            }
        },

        filerev: {
            css: {
                src: "<%= dirs.dest %>/assets/css/**/{,*/}*.css"
             },
            js: {
                src: "<%= dirs.dest %>/assets/js/**/{,*/}*.js"
            },
            images: {
                src: [
                    "<%= dirs.dest %>/assets/images/**/*.{jpg,jpeg,gif,png}",
                    "!<%= dirs.dest %>/assets/images/logo-144x144.png",
                    "!<%= dirs.dest %>/assets/images/logo-256x256.png",
                    "!<%= dirs.dest %>/assets/images/TileImage.png"
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
            html: "<%= dirs.dest %>/**/*.html",
            options: {
                assetsDirs: ["<%= dirs.dest %>/", "<%= dirs.dest %>/assets/images/"]
            }
        },

        connect: {
            options: {
                hostname: "localhost",
                livereload: 35729,
                port: 4000
            },
            livereload: {
                options: {
                    base: "<%= dirs.dest %>/",
                    open: true  // Automatically open the webpage in the default browser
                }
            }
        },

        watch: {
            options: {
                livereload: "<%= connect.options.livereload %>"
            },
            dev: {
                files: ["<%= dirs.src %>/**", ".jshintrc", "_config.yml", "Gruntfile.js"],
                tasks: "dev"
            },
            build: {
                files: ["<%= dirs.src %>/**", ".jshintrc", "_config.yml", "Gruntfile.js"],
                tasks: "build"
            }
        },

        csslint: {
            options: {
                csslintrc: ".csslintrc"
            },
            src: [
                "<%= dirs.src %>/assets/css/02-site/*.css",
                "!<%= dirs.src %>/assets/css/02-site/anchor.css",
                "<%= dirs.src %>/assets/css/03-pages/*.css"
            ]
        },

        jshint: {
            options: {
                jshintrc: ".jshintrc"
            },
            files: {
                src: "Gruntfile.js"
            }
        },

        htmllint: {
            options: {
                ignore: [
                    "Section lacks heading. Consider using \"h2\"-\"h6\" elements to add identifying headings to all sections.",
                    "Consider using the \"h1\" element as a top-level heading only (all \"h1\" elements are treated as top-level headings by many screen readers and other tools)."
                ]
            },
            src: [
                "<%= dirs.dest %>/**/*.html",
                "!<%= dirs.dest %>/newsletters/**/*.html",  // ignore newsletters for now
                "!<%= dirs.dest %>/testimonials/**/*.html"
            ]
        },

        clean: {
            dist: "<%= dirs.dest %>/"
        }

    });

    // Load any grunt plugins found in package.json.
    require("load-grunt-tasks")(grunt, {scope: "devDependencies"});
    require("time-grunt")(grunt);

    grunt.registerTask("build", [
        "clean",
        "jekyll",
        "useminPrepare",
        "concat",
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
        "htmllint"
    ]);

    grunt.registerTask("dev", [
        "jekyll",
        "useminPrepare",
        "concat",
        "filerev",
        "usemin"
    ]);

    grunt.registerTask("server", [
        "build",
        "connect",
        "watch:build"
    ]);

    grunt.registerTask("default", [
        "dev",
        "connect",
        "watch:dev"
    ]);

};
