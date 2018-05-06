const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
  entry: path.join(__dirname, 'frontend', 'index.js'),
  output: {
    filename: 'bundle.js',
    path: path.join(__dirname, 'dist'),
    publicPath: '/',
  },
  module: {
    rules: [
      {
        test: /\.(js|jsx)?$/,
        loader: 'babel-loader?cacheDirectory',
        exclude: path.resolve(__dirname, '..', 'node_modules'),
      },
    ],
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: path.join(__dirname, 'frontend', 'index.html'),
      filename: 'index.html',
    }),
    new CopyWebpackPlugin([{
      from: path.join(__dirname, 'frontend', 'static'),
      to: path.join(__dirname, 'dist'),
    }]),
  ],
};
