'use strict';

const AssetsPlugin = require('assets-webpack-plugin');
const CleanPlugin = require('clean-webpack-plugin');
const path = require('path');
const pkg = require('./package');
const webpack = require('webpack');

const BUILD_DIRECTORY = 'build';
const BUILD_DROP_PATH = path.resolve(__dirname, BUILD_DIRECTORY);
const CHUNK_FILE_NAME = '[name].[chunkhash].js';
const WEB_ROOT = path.resolve(__dirname, 'wwwroot');

let config = {
    context: WEB_ROOT,

    entry: {
        vendor: Object.keys(pkg.dependencies),

        app: './app'
    },

    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                loader: 'babel',
                include: WEB_ROOT
            }
        ]
    },

    output: {
        chunkFilename: CHUNK_FILE_NAME,
        filename: CHUNK_FILE_NAME,
        libraryTarget: 'var',
        path: BUILD_DROP_PATH
    },

    plugins: [
        new AssetsPlugin({
            filename: 'webpack.assets.json',
            path: BUILD_DROP_PATH,
            prettyPrint: true
        }),

        new CleanPlugin(BUILD_DIRECTORY),

        new webpack.optimize.CommonsChunkPlugin('vendor', CHUNK_FILE_NAME),

        new webpack.optimize.UglifyJsPlugin({
            compress: {
                warnings: false
            },
            output: {
                comments: false
            }
        })
    ],

    resolve: {
        extensions: ['', '.js', '.json', '.jsx']
    }
};

if (process.env.NODE_ENV === 'development') {
    config.cache = true;
    config.devtool = 'eval';
    config.watch = true;
}

module.exports = config;