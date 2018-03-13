#!/usr/bin/node

const express = require('express');
const bodyParser = require('body-parser');

const scoreboard = require('./scoreboard');

const PORT = 8090;

const app = express();

app.use(bodyParser.json());

app.post('/score', (req, res) => {
  res.status(200).send(JSON.stringify(scoreboard.insertScore({
    name: req.body.name,
    level: req.body.level,
    time: req.body.time,
  })));
});

app.get('/score', (req, res) => {
  res.status(200).send(JSON.stringify(scoreboard.getScores(
    req.query.level,
    req.query.offset,
    req.query.size,
  )));
});

app.listen(PORT, () => console.log(`Server listening on port: ${PORT}`));
