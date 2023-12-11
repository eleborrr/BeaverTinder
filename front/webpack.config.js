module.exports = {
    resolve: {
      fallback: {
        "os": false,
        "fs": false,
        "tls": false,
        "net": false,
        "path": false,
        "zlib": false,
        "http": false,
        "https": false,
        "stream": false,
        "crypto": false,
        "crypto-browserify": false,
        "buffer": require.resolve("buffer/")
      } 
    },
    }