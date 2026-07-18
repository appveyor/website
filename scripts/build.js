'use strict';

const crypto = require('crypto');
const fs = require('fs');
const path = require('path');
const { execFileSync } = require('child_process');

const CleanCSS = require('clean-css');
const htmlMinifier = require('html-minifier').minify;
const postcss = require('postcss');
const autoprefixer = require('autoprefixer-core');
const uglify = require('uglify-js');

const root = path.resolve(__dirname, '..');
const srcDir = path.join(root, 'src');
const siteDir = path.join(root, '_site');
const tmpDir = path.join(root, '.tmp');

const cssBundle = '/assets/css/pack.css';
const jsBundle = '/assets/js/pack.js';
const jqueryBundle = '/assets/js/jquery-pack.js';

function log(message) {
    process.stdout.write(`${message}\n`);
}

function removeDir(dir) {
    fs.rmSync(dir, { force: true, recursive: true });
}

function ensureDir(filePath) {
    fs.mkdirSync(path.dirname(filePath), { recursive: true });
}

function read(filePath) {
    return fs.readFileSync(filePath, 'utf8');
}

function write(filePath, contents) {
    ensureDir(filePath);
    fs.writeFileSync(filePath, contents);
}

function listFiles(dir, matcher) {
    if (!fs.existsSync(dir)) {
        return [];
    }

    return fs.readdirSync(dir)
        .sort()
        .flatMap((entry) => {
            const fullPath = path.join(dir, entry);
            const stat = fs.statSync(fullPath);

            if (stat.isDirectory()) {
                return listFiles(fullPath, matcher);
            }

            return matcher(fullPath) ? [fullPath] : [];
        });
}

function concatFiles(files) {
    return files.map((filePath) => read(filePath)).join('\n');
}

function hash(contents) {
    return crypto.createHash('md5').update(contents).digest('hex').slice(0, 8);
}

function revPath(assetPath, contents) {
    const parsed = path.parse(assetPath);
    return path.join(parsed.dir, `${parsed.name}.${hash(contents)}${parsed.ext}`);
}

function assetUrl(filePath) {
    return `/${path.relative(siteDir, filePath).split(path.sep).join('/')}`;
}

function revFile(filePath, rewrites) {
    const contents = fs.readFileSync(filePath);
    const nextPath = revPath(filePath, contents);

    fs.renameSync(filePath, nextPath);
    rewrites.set(assetUrl(filePath), assetUrl(nextPath));

    return nextPath;
}

function replaceAll(contents, rewrites) {
    let next = contents;

    for (const [from, to] of rewrites) {
        next = next.split(from).join(to);
    }

    return next;
}

function writeCssBundle(rewrites) {
    const foundationCss = [
        path.join(srcDir, 'assets/css/foundation/normalize.css'),
        path.join(srcDir, 'assets/css/foundation/foundation.css')
    ];
    const siteCss = listFiles(path.join(srcDir, 'assets/css/site'), (filePath) => filePath.endsWith('.css'));
    const pageCss = listFiles(path.join(srcDir, 'assets/css/pages'), (filePath) => filePath.endsWith('.css'));
    const cssPath = path.join(siteDir, cssBundle);
    const css = concatFiles([...foundationCss, ...siteCss, ...pageCss]);
    const prefixed = postcss([autoprefixer({ browsers: ["> 1%", "last 2 versions"] })]).process(css).css;
    const minified = new CleanCSS({ level: { 1: { specialComments: 0 } } }).minify(prefixed);

    if (minified.errors.length) {
        throw new Error(minified.errors.join('\n'));
    }

    write(cssPath, minified.styles);
    revFile(cssPath, rewrites);
}

function writeJsBundle(outputUrl, files, rewrites) {
    const jsPath = path.join(siteDir, outputUrl);
    const result = uglify.minify(concatFiles(files), {
        compress: true,
        mangle: true,
        output: {
            comments: false
        }
    });

    if (result.error) {
        throw result.error;
    }

    write(jsPath, result.code);
    revFile(jsPath, rewrites);
}

function writeJsBundles(rewrites) {
    writeJsBundle(jsBundle, [
        path.join(srcDir, 'assets/js/vendor/anchor.js'),
        path.join(srcDir, 'assets/js/no-js-class.js'),
        path.join(srcDir, 'assets/js/google-analytics.js'),
        path.join(srcDir, 'assets/js/main.js')
    ], rewrites);

    writeJsBundle(jqueryBundle, [
        ...listFiles(path.join(srcDir, 'assets/js/vendor'), (filePath) => /^jquery-.*\.min\.js$/.test(path.basename(filePath))),
        ...listFiles(path.join(srcDir, 'assets/js/vendor/foundation'), (filePath) => filePath.endsWith('.js')),
        path.join(srcDir, 'assets/js/jquery.js')
    ], rewrites);
}

function revImages(rewrites) {
    const imageFiles = listFiles(path.join(siteDir, 'assets/img'), (filePath) => {
        if (!/\.(jpg|jpeg|gif|png|svg)$/i.test(filePath)) {
            return false;
        }

        const relative = path.relative(siteDir, filePath).split(path.sep).join('/');

        return !relative.startsWith('assets/img/favicons/') &&
            !/^assets\/img\/appveyor-logo.*\.png$/.test(relative);
    });

    imageFiles.forEach((filePath) => revFile(filePath, rewrites));
}

function rewriteGeneratedFiles(rewrites) {
    const files = listFiles(siteDir, (filePath) => /\.(aspx|css|html)$/i.test(filePath));

    files.forEach((filePath) => {
        write(filePath, replaceAll(read(filePath), rewrites));
    });
}

function minifyHtml() {
    const files = listFiles(siteDir, (filePath) => /\.(aspx|html)$/i.test(filePath));
    const options = {
        collapseBooleanAttributes: true,
        collapseWhitespace: true,
        decodeEntities: true,
        ignoreCustomComments: [/^\s*google(off|on):\s/],
        minifyJS: true,
        processConditionalComments: true,
        processScripts: ['application/ld+json'],
        removeAttributeQuotes: true,
        removeComments: true,
        removeOptionalTags: true,
        removeRedundantAttributes: true,
        removeScriptTypeAttributes: true,
        removeStyleLinkTypeAttributes: true,
        sortAttributes: true,
        sortClassName: true
    };

    files.forEach((filePath) => {
        write(filePath, htmlMinifier(read(filePath), options));
    });
}

function run() {
    const rewrites = new Map();

    log('Cleaning output directories...');
    removeDir(siteDir);
    removeDir(tmpDir);

    log('Building Jekyll site...');
    execFileSync('bundle', ['exec', 'jekyll', 'build'], {
        cwd: root,
        stdio: 'inherit'
    });

    log('Building CSS bundle...');
    writeCssBundle(rewrites);

    log('Building JS bundles...');
    writeJsBundles(rewrites);

    log('Fingerprinting images...');
    revImages(rewrites);

    log('Rewriting generated asset references...');
    rewriteGeneratedFiles(rewrites);

    log('Minifying HTML...');
    minifyHtml();

    log('Build complete.');
}

run();
