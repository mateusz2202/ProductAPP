const { env } = require('process');

const target = `https://localhost:7200` ;

const PROXY_CONFIG = [
  {
    context: [
      "/product",
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
