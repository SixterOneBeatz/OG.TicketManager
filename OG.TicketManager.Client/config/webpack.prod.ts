import HtmlWebPackPlugin from "html-webpack-plugin";
import * as webpack from "webpack";
import * as path from "path";
import DotenvWebpackPlugin from "dotenv-webpack";

const dotEnvPlugin = new DotenvWebpackPlugin({
  path: path.resolve(__dirname, "../environments/.env.prod"),
  allowEmptyValues: true,
  safe: true,
  silent: true,
});

const htmlPlugin = new HtmlWebPackPlugin({
  template: "public/index.html",
  hash: true,
  filename: "../dist/index.html",
});

const config: webpack.Configuration = {
  entry: "./src/index.tsx",
  devtool: false,
  mode: "production",
  output: {
    path: path.resolve("dist"),
    filename: "bundle.js",
  },
  module: {
    rules: [
      {
        test: /\.(ts|tsx)?$/,
        loader: "ts-loader",
        exclude: /node_modules/,
      },
      {
        test: /\.s[ac]ss$/i,
        use: [
          // Creates `style` nodes from JS strings
          "style-loader",
          // Translates CSS into CommonJS
          "css-loader",
          // Compiles Sass to CSS
          "sass-loader",
        ],
      },
      {
        test: /\.css$/i,
        use: ["style-loader", "css-loader"],
      },
      {
        test: /\.(png|jp(e*)g|svg|gif)$/i,
        loader: "url-loader",
        options: {
          name: "[path][name].[ext]",
          limit: 8192,
          fallback: require.resolve("file-loader"),
        },
      },
    ],
  },
  resolve: {
    extensions: [".ts", ".js", ".json", ".tsx"],
    alias: {
      "@mui/styles": path.resolve(__dirname, "node_modules", "@mui/styles"),
    },
  },
  plugins: [htmlPlugin, dotEnvPlugin],
};

export default config;
