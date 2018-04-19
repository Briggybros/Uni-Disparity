#!/usr/bin/node

const express = require('express');
const bodyParser = require('body-parser');
const ejs = require('ejs');

const scoreboard = require('./scoreboard');

const PORT = 8090;

const app = express();

app.use(bodyParser.json());

app.use(express.static('./static'));

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

app.get('/:level', (req, res) => {
  const pageNumber = req.query.page ? parseInt(req.query.page, 10) : 0;
  ejs.renderFile('scoreboard.ejs', {
    scores: scoreboard.getScores(
      req.params.level,
      pageNumber * 10,
      (pageNumber * 10) + 9,
    ),
  }, (err, page) => {
    res.status(200).send(page);
  });
});

app.listen(PORT, () => console.log(`Server listening on port: ${PORT}`));
