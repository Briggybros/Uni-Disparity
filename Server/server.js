#!/usr/bin/node

const express = require('express');
const bodyParser = require('body-parser');
const socketIO = require('socket.io');

const scoreboard = require('./scoreboard');

const PORT = 8090;

const app = express();

app.use(bodyParser.json());

app.use(express.static('./dist'));

app.post('/score', (req, res) => {
  res.send(JSON.stringify(scoreboard.insertScore({
    name: req.body.name,
    level: req.body.level,
    time: req.body.time,
  })));
});

app.get('/score', (req, res) => {
  res.send(JSON.stringify(scoreboard.getScores(
    req.query.level,
    req.query.offset,
    req.query.size,
  )));
});

app.get('*', (req, res) => {
  res.sendFile('/dist/index.html');
});

const http = app.listen(PORT, () => console.log(`Server listening on port: ${PORT}`));

const io = socketIO(http);

let clientLUT = {};

io.on('connection', (socket) => {
  socket.on('board', (board, page) => {
    clientLUT = Object.assign({}, clientLUT, {
      [socket.id]: { board, page },
    });

    const data = {
      results: scoreboard.getScores(board, 10 * (page - 1), 10),
      pages: Math.ceil(scoreboard.count(board) / 10),
    };

    socket.emit('update', data);
  });
});

scoreboard.on('update', (score) => {
  Object.entries(clientLUT).forEach(([key, value]) => {
    if (value.board === score.level) {
      if (score.rank >= 10 * (value.page - 1) && score.rank <= (10 * (value.page - 1)) + 10) {
        const data = {
          results: scoreboard.getScores(value.board, 10 * (value.page - 1), 10),
          pages: Math.ceil(scoreboard.count(value.board) / 10),
        };

        io.sockets.connected[key].emit('update', data);
      }
    }
  });
});
