'use strict';

module.exports = function (grunt) {

    grunt.initConfig({
        dirs: {
            dest: '_site',
            src: 'src',
            tmp: '.tmp'
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
                        level: {
                            1: {
                                specialComments: 0
                            }
                        }
                    },
                    minifyJS: true,
                    minifyURLs: false,
                    processConditionalComments: true,
                    processScripts: ['application/ld+json'],
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
                cwd: '<%= dirs.dest %>',
                dest: '<%= dirs.dest %>',
                src: [
                    '**/*.aspx',
                    '**/*.html'
                ]
            }
        },

        concat: {
            foundation: {
                src: [
                    '<%= dirs.src %>/assets/css/foundation/normalize.css',
                    '<%= dirs.src %>/assets/css/foundation/foundation.css'
                ],
                dest: '<%= dirs.tmp %>/css/foundation.css'
            },
            css: {
                src: [
                    '<%= concat.foundation.dest %>',
                    '<%= dirs.src %>/assets/css/site/*.css',
                    '<%= dirs.src %>/assets/css/pages/*.css'
                ],
                dest: '<%= dirs.dest %>/assets/css/pack.css'
            },
            js: {
                src: [
                    '<%= dirs.src %>/assets/js/vendor/anchor.js',
                    '<%= dirs.src %>/assets/js/no-js-class.js',
                    '<%= dirs.src %>/assets/js/google-analytics.js',
                    '<%= dirs.src %>/assets/js/main.js'
                ],
                dest: '<%= dirs.dest %>/assets/js/pack.js'
            },
            jqueryJS: {
                src: [
                    '<%= dirs.src %>/assets/js/vendor/jquery-*.min.js',
                    '<%= dirs.src %>/assets/js/vendor/foundation/*.js',
                    '<%= dirs.src %>/assets/js/jquery.js'
                ],
                dest: '<%= dirs.dest %>/assets/js/jquery-pack.js'
            }
        },

        autoprefixer: {
            options: {
                browsers: [
                    'last 2 version',
                    '> 1%',
                    'Edge >= 12',
                    'Explorer >= 11',
                    'Firefox ESR',
                    'Opera 12.1'
                ]
            },
            pack: {
                src: '<%= concat.css.dest %>',
                dest: '<%= concat.css.dest %>'
            }
        },

        uncss: {
            options: {
                ignore: [
                    /(#|\.)top-bar(-[a-zA-Z]+)?/,
                    /(#|\.)topbar(-[a-zA-Z]+)?/,
                    /(#|\.)f-topbar-fixed(-[a-zA-Z]+)?/,
                    /(#|\.)dropdown(-[a-zA-Z]+)?/,
                    /\.no-js/,
                    /meta.foundation(-[a-zA-Z]+)?/,
                    '.anchorjs-link'
                ],
                htmlroot: '<%= dirs.dest %>',
                ignoreSheets: [/fonts.googleapis/],
                stylesheets: ['/assets/css/pack.css']
            },
            dist: {
                src: '<%= dirs.dest %>/**/*.html',
                dest: '<%= concat.css.dest %>'
            }
        },

        cssmin: {
            minify: {
                options: {
                    level: {
                        1: {
                            specialComments: 0
                        }
                    }
                },
                files: {
                    '<%= concat.css.dest %>': '<%= concat.css.dest %>'
                }
            }
        },

        uglify: {
            options: {
                compress: true,
                mangle: true,
                output: {
                    comments: false
                }
            },
            minify: {
                files: {
                    '<%= concat.js.dest %>': '<%= concat.js.dest %>',
                    '<%= concat.jqueryJS.dest %>': '<%= concat.jqueryJS.dest %>'
                }
            }
        },

        filerev: {
            css: {
                src: '<%= dirs.dest %>/assets/css/*.css'
            },
            js: {
                src: '<%= dirs.dest %>/assets/js/*.js'
            },
            images: {
                src: [
                    '<%= dirs.dest %>/assets/img/**/*.{jpg,jpeg,gif,png,svg}',
                    '!<%= dirs.dest %>/assets/img/favicons/*.{jpg,jpeg,gif,png,svg}',
                    '!<%= dirs.dest %>/assets/img/appveyor-logo*.png'
                ]
            }
        },

        useminPrepare: {
            html: '<%= dirs.dest %>/index.html',
            options: {
                dest: '<%= dirs.dest %>',
                root: '<%= dirs.dest %>'
            }
        },

        usemin: {
            css: '<%= dirs.dest %>/assets/css/pack*.css',
            html: [
                '<%= dirs.dest %>/**/*.html',
                '<%= dirs.dest %>/**/*.aspx'
            ],
            options: {
                assetsDirs: [
                    '<%= dirs.dest %>/',
                    '<%= dirs.dest %>/assets/img/'
                ]
            }
        },

        connect: {
            options: {
                base: '<%= dirs.dest %>/',
                hostname: 'localhost'
            },
            livereload: {
                options: {
                    base: '<%= dirs.dest %>/',
                    livereload: 35729,
                    open: true,  // Automatically open the webpage in the default browser
                    port: 4000
                }
            },
            linkChecker: {
                options: {
                    base: '<%= dirs.dest %>/',
                    port: 9001
                }
            }
        },

        watch: {
            options: {
                livereload: '<%= connect.livereload.options.livereload %>'
            },
            dev: {
                files: [
                    '<%= dirs.src %>/**',
                    '.eslintrc.json',
                    '_config.yml',
                    'Gruntfile.js'
                ],
                tasks: 'dev'
            },
            build: {
                files: [
                    '<%= dirs.src %>/**',
                    '.eslintrc.json',
                    '_config.yml',
                    'Gruntfile.js'
                ],
                tasks: 'build'
            }
        },

        csslint: {
            options: {
                csslintrc: '.csslintrc'
            },
            src: [
                '<%= dirs.src %>/assets/css/site/*.css',
                '<%= dirs.src %>/assets/css/pages/*.css'
            ]
        },

        eslint: {
            options: {
                config: '.eslintrc.json'
            },
            src: {
                src: [
                    'Gruntfile.js',
                    '<%= dirs.src %>/assets/js/*.js',
                    '!<%= dirs.src %>/assets/js/google-analytics.js'
                ]
            }
        },

        markdownlint: {
            all: {
                options: {
                    config: grunt.file.readJSON('./markdownlint.json')
                },
                src: [
                    'src/**/*.md'
                ]
            }
        },

        htmllint: {
            options: {
                ignore: [
                    'Consider using the "h1" element as a top-level heading only (all "h1" elements are treated as top-level headings by many screen readers and other tools).',
                    'This document appears to be written in French but the “html” start tag has “lang="en"”. Consider using “lang="fr"” (or variant) instead.',
                    'This document appears to be written in Kinyarwanda but the “html” start tag has “lang="en"”. Consider using “lang="rw"” (or variant) instead.'
                ]
            },
            src: [
                '<%= dirs.dest %>/**/*.html',
                '!<%= dirs.dest %>/updates/index.html',
                '!<%= dirs.dest %>/updates/page/**/*.html'
            ]
        },

        linkChecker: {
            options: {
                callback: function (crawler) {
                    crawler.addFetchCondition(function (url) {
                        return url.path !== '/assets/js/l.src' &&
                            url.path !== '/assets/js/+a+' &&
                            url.path !== '/feed.xml' &&
                            url.port !== '9023' &&
                            url.port !== '9001';
                    });
                },
                interval: 1,        // 1 ms; default 250
                maxConcurrency: 5   // default; bigger doesn't seem to improve time
            },
            dev: {
                site: 'localhost',
                options: {
                    initialPort: '<%= connect.linkChecker.options.port %>'
                }
            }
        },

        accessibility: {
            options: {
                accessibilityLevel: 'WCAG2AA',
                browser: true,
                reportLevels: {
                    notice: false,
                    warning: false,
                    error: true
                }
            },
            test: {
                src: [
                    '<%= htmllint.src %>',
                    '!<%= dirs.dest %>/docs/build-environment/*.html'
                ]
            }
        },

        clean: {
            dist: [
                '<%= dirs.dest %>/',
                '<%= dirs.tmp %>/'
            ]
        }

    });

    // Load any grunt plugins found in package.json.
    require('load-grunt-tasks')(grunt, { scope: 'dependencies' });
    require('time-grunt')(grunt);

    grunt.registerTask('build', [
        'clean',
        'jekyll',
        'useminPrepare',
        'concat',
        'autoprefixer',
        'uncss',
        'cssmin',
        'uglify',
        'filerev',
        'usemin',
        'htmlmin'
    ]);

    grunt.registerTask('test', [
        'build',
        'csslint',
        'eslint',
        'markdownlint',
        'htmllint',
        'connect:linkChecker',
        'linkChecker'
    ]);

    grunt.registerTask('dev', [
        'jekyll',
        'useminPrepare',
        'concat',
        'autoprefixer',
        'filerev',
        'usemin'
    ]);

    grunt.registerTask('server', [
        'build',
        'connect:livereload',
        'watch:build'
    ]);

    grunt.registerTask('default', [
        'dev',
        'connect:livereload',
        'watch:dev'
    ]);

};
